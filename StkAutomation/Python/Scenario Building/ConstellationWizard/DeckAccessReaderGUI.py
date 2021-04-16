# Helper functions to create MTOs (visual representations of objects, no analysis)

# Run a deck access report then the writeTLE function can be called to create a TLE file with all objects fromt the deck access report.


# Deck Access Report Format
# =============================================================================
#                                                            2 Jul 2019 08:50:41
# Facility-Facility1
# 
# 
#  Name        Start Time (UTCG)           Stop Time (UTCG)        Duration (sec)
# -----    ------------------------    ------------------------    --------------
# 00124    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:00.177             0.177
# 00020    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:00.194             0.194
# 00054    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:00.540             0.540
# 00040    19 Jun 2019 16:00:00.000    19 Jun 2019 16:00:03.785             3.785
# =============================================================================

# Data begins at line 7
# SCID = cols 0-4
import pandas as pd
import numpy as np
import math
import os
from comtypes.client import CreateObject
from comtypes.client import GetActiveObject
from comtypes.gen import STKObjects
from tkinter import Tk
from tkinter.ttk import *
from tkinter import scrolledtext
from tkinter import INSERT
from tkinter import END
cwd = os.getcwd()
cwdFiles = cwd+'\\Files'

def readDeck(deckAccessRpt):
    
    report = open(deckAccessRpt, "r")
    lines = report.readlines()
    scn = []
    for i in range(6, len(lines)):
        tokenLine = lines[i].split()
        scid = tokenLine[0]
        if scid in scn:
            #do nothing
            scid = scid
        else:
            scn.append(scid) 
    report.close()
    #print(len(scn))
    return scn
#readDeck()
# Able to get unique spacecraft id's out of D.A. Report

def getTLEs(TLEFile,deckAccessRpt=''):
    
    if deckAccessRpt == '':
        tleFile = open(TLEFile, "r")
        tleList = []
        tles = tleFile.readlines()
        for i in range(1, int(round(len(tles)/2))+1):
            line = tles[2*i - 1].split()
            tleList.append(tles[2*i - 2])
            tleList.append(tles[2*i - 1])
        tleFile.close()
        return tleList
    else: 
        tleFile = open(TLEFile, "r")
        scnList = readDeck(deckAccessRpt)
        tleList = []
        tles = tleFile.readlines()
        for i in range(1, int(round(len(tles)/2))+1):
            line = tles[2*i - 1].split()
            if line[1] in scnList:
                tleList.append(tles[2*i - 2])
                tleList.append(tles[2*i - 1])
        tleFile.close()
        return tleList

def writeTLEs(TLEFile,deckAccessRpt,deckAccessTLE):
    
    satFile = open(deckAccessTLE, "w")
    tleList = getTLEs(TLEFile,deckAccessRpt)
    for item in tleList:
        satFile.write("%s" % item)
    satFile.close()
    return int(len(tleList)/2)
    
def FilterObjectsByType(root,objType,name = ''):

    # Send objects to an xml
    xml = root.AllInstanceNamesToXML()

    # split the xml by object paths
    objs = xml.split('path=')
    objs = objs[1:] # remove first string of '<'

    # Loop through each object and parse by object path
    objPaths = []

    for i in range(len(objs)):
        obji = objs[i].split('"')
        objiPath = obji[1] # the 2nd string is the file path
        objiSplit = objiPath.split('/')
        objiClass = objiSplit[-2]
        objiName = objiSplit[-1]
        if objiClass.lower() == objType.lower():
            if name.lower() in objiName.lower():
                objPaths.append(objiPath)
    return objPaths

def ExportChildren(obj):
    children = []
    for ii in range(obj.Children.Count):
        child = obj.Children.Item(ii)
        child.Export(cwdFiles+'\\ChildrenObjects\\'+child.InstanceName)
        children.append(child.ClassName+'/'+child.InstanceName)
        if child.ClassName == 'Sensor':
            for jj in range(child.Children.Count):
                grandChild = child.Children.Item(jj)
                grandChild.Export(cwdFiles+'\\ChildrenObjects\\'+grandChild.InstanceName)     
    return children

def ImportChildren(children,obj):
    childrenObjs = []
    for ii in range(len(children)):
        childType,childName = children[ii].split('/')
        try:
            child = obj.Children.ImportObject(cwdFiles+'\\ChildrenObjects\\'+childName+ObjectExtension(childType))
        except:
            child = obj.Children.Item(childName)
        childrenObjs.append(child)
    return childrenObjs

def ObjectExtension(objType):
    ext = {'Sensor':'.sn',
           'Receiver':'.r',
           'Transmitter':'.x',
           'Radar':'.rd',
           'Antenna':'.antenna',
          }
    return ext[objType]

def GetChildren(obj):
    children = []
    for ii in range(obj.Children.Count):
        child = obj.Children.Item(ii)
        children.append(child.ClassName+'/'+child.InstanceName)
    return children

def tleListToDF(tleList):
    if len(tleList)>1:
        for i in range(len(tleList)):
            if i % 2 == 0:
                tleList[i] = tleList[i][0]+','+tleList[i][2:8]+','+tleList[i][9:17]+','+tleList[i][18:32]+','+tleList[i][33:43]+','+tleList[i][44:52]+','+tleList[i][53:61]+','+tleList[i][62]+','+tleList[i][64:69]
            elif i % 2 == 1:
                tleList[i] = tleList[i][0]+','+tleList[i][2:7]+','+tleList[i][8:16]+','+tleList[i][17:25]+','+tleList[i][26:33]+','+tleList[i][34:42]+','+tleList[i][43:51]+','+tleList[i][52:69]

        dfTLEList = pd.DataFrame(tleList)  

        # new data frame with split value columns 
        tleSplit = dfTLEList[dfTLEList.columns[0]].str.split(',',expand=True) 
        line1 = tleSplit[0::2]  
        line2 = tleSplit[1::2] 
        line1 = line1.reset_index(drop=True)
        line2 = line2.reset_index(drop=True)
        line1.columns =['Line1','Ssc','Launch','Epoch','Mean motion 1st','Mean motion 2nd','Drag','Eph Type','Elem Set']
        line2.columns =['Line2','Ssc2','i','RAAN','e','AoP','MA','Mean motion','temp']
        # Need to handle the space in some of the second lines. Replacing this with a 0
        line2['Mean motion'] = line2['Mean motion'].str.replace(' ','0')
        line2 = line2.drop('temp',axis=1)

        # Create new data frame with both lines in the same row
        dfTLE = pd.concat([line1,line2],axis=1)

        # Convert mean motion to approximate semimajor axis and add this as a column to the dataframe
        dfTLE['i']= dfTLE['i'].astype(float)
        dfTLE['Mean motion'] = dfTLE['Mean motion'].astype(float)
        mu = 3.986004e14
        n = dfTLE['Mean motion']/(86400)*2*np.pi # Technically the mean motion is only the first 8 digits past the decimal but removing the extra digits won't affect much
        a = (mu/(n**2))**(1/3)/1000
        dfTLE['a'] = a
    else:
        dfTLE = pd.DataFrame()
    return dfTLE

def dfToTLE(df,TLEFileNamedf):
    df1 = df[df.columns[0:9]].astype(str)
    df1.loc[:,'Ssc'] = df1.loc[:,'Ssc'].apply(lambda x: x.ljust(6))
    df2 = df[df.columns[9:]]
    df2.loc[:,'i'] = df2.loc[:,'i'].apply(lambda x: '{:08.4f}'.format(x))
    df2.loc[:,'Mean motion'] = df2.loc[:,'Mean motion'].apply(lambda x: '{:11.8f}'.format(x))
    df2 = df2.astype(str).drop('a',axis=1)
    lines1= df1.apply(lambda x: ' '.join(x),axis=1)
    lines2= df2.apply(lambda x: ' '.join(x),axis=1)
    f = open(TLEFileNamedf,'w')
    for line in range(len(df1)):
        f.write(lines1[line]+'\n')
        f.write(lines2[line]+'\n')
    f.close()

# Create a TLE constellation of satelite objects  
# Example
# 1 44292U 19029BK  19171.04714474  .00001365  00000-0  11317-3 0  9993
# 2 44292  50.0075  51.5253 0002397 120.4102 239.7123 15.05462229  3427

def createTLEConstellation(fileName,epoch,a,e,i,aop,numPlanes,satsPerPlane):

    mu = 3.986004e14   
    meanMotion = '{:11.8f}'.format((mu/(a*1000)**3)**(1/2)*86400/(2*np.pi))
    e = '{:.7f}'.format(e)[2:]
    i = '{:8.4f}'.format(i)
    aop = '{:8.4f}'.format(aop)
    epoch = '{:14.8f}'.format(epoch)
    
    RAAN = 0 
    dMA = 360 / satsPerPlane
    dRAAN = 360 / numPlanes
    
    p1 = open(fileName, "w+")
    for j in range(numPlanes):
        MA = 0

        RAANstr = '{:8.4f}'.format(RAAN)
        for ii in range(satsPerPlane):
            scID = str(ii + satsPerPlane*j).rjust(5, '0')   # pad id so that it is length 5
            scIDU = scID + 'U'   # add U to end of id to denote Unclassified

            MAstr = '{:8.4f}'.format(MA)

            line1 = "1 %s 20000    %s  .00000000  00000-0  00000-0 0  9999\n" % (scIDU,epoch)
            line2 = "2 %s %s %s %s %s %s %s     0\n" % (scID, i,RAANstr,e, aop, MAstr, meanMotion)
                        
            p1.write(line1)
            p1.write(line2)
            
            MA+=dMA
            
        RAAN+=dRAAN
    
    p1.close()
    
    
# Connect to STK
def ConnectToSTK(version=12,scenarioPath = cwd+'\\ConstellationWizardExampleScenario',scenarioName='ConstellationAnalysis'):
    # Launch or connect to STK
    try:
        app = GetActiveObject('STK{}.Application'.format(version))
        root = app.Personality2
        root.Isolate()
    except:
        app = CreateObject('STK{}.Application'.format(version))
        app.Visible = True
        app.UserControl= True
        root = app.Personality2
        root.Isolate()
        try:
            root.LoadScenario(scenarioPath+'\\'+scenarioName+'.sc')
        except:    
            root.NewScenario(scenarioName)
    root.UnitPreferences.SetCurrentUnit('DateFormat','Epsec')
    root.ExecuteCommand('Units_SetConnect / Date "Epsec"')
    return root
    
# Create Constellation
def CreateConstellation(root,txt,txtBox,ssc=00000,howToCreate='satsinstk'):
    TLEFileName = cwdFiles+'\\Constellations\\'+txt.get().replace(' ','-').replace(' ','-')+'.tce'
    if howToCreate == 'code':
        epoch = 19329 # Format: yyddd, last two digits of the year and the day of year. Ex: Nov 25 2019 is '19329'. Use all 3 digits for the day of year
        a = 6800
        e = 0.01
        i = 40
        aop = 30
        numPlanes = 5
        satsPerPlane = 3
        createTLEConstellation(TLEFileName,epoch,a,e,i,aop,numPlanes,satsPerPlane)
    elif howToCreate == 'satsinstk':
        sc = root.CurrentScenario
        sc2 = sc.QueryInterface(STKObjects.IAgScenario)
        satPaths = FilterObjectsByType('satellite',name = '')
        if sc.Children.Contains(STKObjects.eSatellite,'tempsat'):
            tempsat =root.GetObjectFromPath('Satellite/tempsat')
            tempsat.Unload();

        fid = open(TLEFileName,'w+')
        for ii in range(len(satPaths)):
             # Generate a dummy TLE sat
             satName = str(satPaths[ii].split('/')[-1])
             cmd = 'GenerateTLE */Satellite/'+satName+' Sampling "'+str(sc2.StartTime)+'" "'+str(sc2.StopTime)+'" 60.0 "'+str(sc2.StartTime)+'" '+'{:05.0f}'.format(ssc)+' 20 0.0001 SGP4 tempsat'
             root.ExecuteCommand(cmd)

             # Make sure TLE information is valid and propagated on dummy satellite
             tempsat =root.GetObjectFromPath('Satellite/tempsat');
             cmd = 'GenerateTLE */Satellite/tempsat Sampling "'+str(sc2.StartTime)+'" "'+str(sc2.StopTime)+'" 60.0 "'+str(sc2.StartTime)+'" '+'{:05.0f}'.format(ssc)+' 20 0.0001 SGP4 tempsat'
             root.ExecuteCommand(cmd)

             # Extract TLE information from dummy satellite 
             satDP = tempsat.DataProviders.Item('TLE Summary Data').QueryInterface(STKObjects.IAgDataPrvFixed).Exec()
             TLEData = satDP.DataSets.GetDataSetByName('TLE').GetValues()
             tempsat.Unload()

             # Write TLE to file
             fid.write('%s\n%s\n' % (TLEData[0],TLEData[1]));
             ssc += 1;
        fid.close()
        txtBox.insert(END,'Created: '+txt.get().replace(' ','-').replace(' ','-')+'.tce\n')

def LoadMTO(root,txtBox,MTOName = 'deckAccessMTO',timestep=60,color='green',orbitsOnOrOff='off',orbitFrame='Inertial'):
    TLEFileName = cwdFiles+'\\Constellations\\'+MTOName+'.tce'
    # Add all visibile satellites as an MTO
    if root.CurrentScenario.Children.Contains(STKObjects.eMTO,MTOName):
        cmd = 'Unload / */MTO/'+MTOName
        root.ExecuteCommand(cmd)
    cmd = 'New / */MTO '+MTOName
    root.ExecuteCommand(cmd)
    cmd = 'VO */MTO/'+MTOName+' MTOAttributes ShowAlllabels off'
    root.ExecuteCommand(cmd)
    cmd = 'VO */MTO/'+MTOName+' MTOAttributes ShowAllLines '+orbitsOnOrOff
    root.ExecuteCommand(cmd)
    cmd = 'VO */MTO/'+MTOName+' System "CentralBody/Earth '+orbitFrame+'"'
    root.ExecuteCommand(cmd)
    cmd = 'DefaultTrack */MTO/'+MTOName+' Interpolate On'
    root.ExecuteCommand(cmd)
    cmd = 'DefaultTrack2d */MTO/'+MTOName+' color '+color
    root.ExecuteCommand(cmd)
    try:
        cmd = 'Track */MTO/'+MTOName+' TleFile Filename "' + TLEFileName + '" TimeStep '+str(timestep) # Decrease the TimeStep for better resolution at the cost of computation time 
        root.ExecuteCommand(cmd)
        txtBox.insert(END,'Loaded: '+MTOName+'\n')
    except:   
        txtBox.insert(END,'Failed To Load: '+MTOName+'\n')

def deckAccessAvailableObjs(root):
    objs = root.ExecuteCommand('AllInstanceNames /')
    objsAll = objs.Item(0).split()
    objs = []
    for obj in objsAll:
        objType = obj.split('/')[-2]
        if objType in ['Place','Facility','Target','Aircraft','Ship','GroundVehicle','Satellite','LaunchVehicle','Missile','Sensor']:
            objs.append(obj)
    return objs

def chainCovAvailableObjs(root):
    objs = root.ExecuteCommand('AllInstanceNames /')
    objsAll = objs.Item(0).split()
    objs = []
    for obj in objsAll:
        objType = obj.split('/')[-2]
        if objType in ['Place','Facility','Target','Aircraft','Ship','GroundVehicle','Satellite','LaunchVehicle','Missile','Sensor','Transmitter','Receiver','Constellation','Radar']:
            objs.append(obj)
    return objs

def runDeckAccess(root,startTime,stopTime,comboCon,comboDA,txtBox,constraintSatName = ''):
    startTime = root.ConversionUtility.ConvertDate('UTCG','EpSec',str(startTime))
    stopTime = root.ConversionUtility.ConvertDate('UTCG','EpSec',str(stopTime))
    TLEFileName = cwdFiles+'\\Constellations\\'+comboCon.get()+'.tce'
    accessObjPath = comboDA.get()
    if accessObjPath == '':
        txtBox.insert(END,'Invalid: Access From Object\n')
        NumOfSC = 0
        deckAccessFileName = cwdFiles+'\\Misc\\deckAccessRpt.txt' # Created
        deckAccessTLEFileName = cwdFiles+'\\Constellations\\deckAccessTLE.tce' # Created
    else:
        # Deck Access for the current time. Save the deck access file to the specified
        sc2 = root.CurrentScenario.QueryInterface(STKObjects.IAgScenario)
        deckAccessFileName = cwdFiles+'\\Misc\\deckAccessRpt.txt' # Created
        deckAccessTLEFileName = cwdFiles+'\\Constellations\\deckAccessTLE.tce' # Created
        startTime = str(startTime)
        stopTime = str(stopTime)
        if root.CurrentScenario.Children.Contains(STKObjects.eSatellite,constraintSatName):
            cmd = 'DeckAccess */' + accessObjPath + ' "' + startTime + '" "'+ stopTime +'" Satellite "' + TLEFileName+ '" SortObj OutFile "'+ deckAccessFileName+'" ConstraintObject */Satellite/'+constraintSatName
            cmdOut = root.ExecuteCommand(cmd)
        else:
            cmd = 'DeckAccess */' + accessObjPath + ' "' + startTime + '" "'+ stopTime +'" Satellite "' + TLEFileName+ '" SortObj OutFile "'+ deckAccessFileName+'"'
            cmdOut = root.ExecuteCommand(cmd)
        NumOfSC = writeTLEs(TLEFileName,deckAccessFileName,deckAccessTLEFileName)
        txtBox.insert(END,accessObjPath.split('/')[-1]+': '+str(NumOfSC)+' sats w/ access\n')
        
    return NumOfSC,deckAccessFileName,deckAccessTLEFileName

def deckAccessReportToDF(deckAccessFileName):
    f = open(deckAccessFileName,'r')
    txt = f.readlines()
    f.close()
    header = txt[4].replace('[','').replace(']','').split()
    dfAccess = pd.DataFrame(txt[6:])[0].str.split(expand=True)
    if len(dfAccess.columns) == 10:
        dfAccess[1] = dfAccess[1]+' '+dfAccess[2]+' '+dfAccess[3]+' '+dfAccess[4]
        dfAccess[5] = dfAccess[5]+' '+dfAccess[6]+' '+dfAccess[7]+' '+dfAccess[8]
        dfAccess = dfAccess.drop([2,3,4,6,7,8],axis=1)
    dfAccess.columns = [header[0],header[1]+' '+header[2]+' '+header[3],header[4]+' '+header[5]+' '+header[6],header[7]+' '+header[8]]
    dfAccess
    return dfAccess

def LoadSatsFromFileUsingTemplate(root,startTime,stopTime,comboCon,selected,txtBox,satTempName,color='cyan'):
    startTime = root.ConversionUtility.ConvertDate('UTCG','EpSec',str(startTime))
    stopTime = root.ConversionUtility.ConvertDate('UTCG','EpSec',str(stopTime))
    TLEFileName = cwdFiles+'\\Constellations\\'+comboCon.get()+'.tce'
    deckAccessTLEFileName = cwdFiles+'\\Constellations\\deckAccessTLE.tce'
    if selected.get() == 1:
        tleList = getTLEs(deckAccessTLEFileName)
        dfLoad = tleListToDF(tleList)
    elif selected.get() == 2:
        tleList = getTLEs(TLEFileName)
        dfLoad = tleListToDF(tleList)
    LoadSatsUsingTemplate(root,dfLoad,startTime,stopTime,TLEFileName,satTempName,color=color)
    txtBox.insert(END,'Loaded: '+str(len(dfLoad))+' satellites\n')
    
def LoadSatsUsingTemplate(root,dfLoad,startTime,stopTime,TLEFileName,satTempName,color='cyan'):
    root.BeginUpdate()
    root.ExecuteCommand('BatchGraphics * On')
#     startTime = root.ConversionUtility.ConvertDate('UTCG','EpSec',str(startTime))
#     stopTime = root.ConversionUtility.ConvertDate('UTCG','EpSec',str(stopTime))
#     startTime = str(startTime)
#     stopTime = str(stopTime)

    # Create Constellations for Further Analysis
    satConName = TLEFileName.split('\\')[-1].split('.')[0]
    if root.CurrentScenario.Children.Contains(STKObjects.eConstellation,satConName):
        satCon = root.GetObjectFromPath('Constellation/'+satConName)
    else:
        satCon = root.CurrentScenario.Children.New(STKObjects.eConstellation,satConName)
    satCon2 = satCon.QueryInterface(STKObjects.IAgConstellation)
    
    # Create Constellation for each child object
    if satTempName != '':
        satTemp = root.GetObjectFromPath('Satellite/'+satTempName)
        children = ExportChildren(satTemp)
        conObjs = []
        conGrandChildObjs = []
        grandChildObjs = []
        for ii in range(len(children)):
            childType,childName = children[ii].split('/')
            name = satConName+childName+'s'
            if root.CurrentScenario.Children.Contains(STKObjects.eConstellation,name):
                conObj = root.GetObjectFromPath('Constellation/'+name)
            else:
                conObj = root.CurrentScenario.Children.New(STKObjects.eConstellation,name)
            conObjs.append(conObj.QueryInterface(STKObjects.IAgConstellation))
            if childType == 'Sensor':
                child = satTemp.Children.Item(ii)
                for jj in range(child.Children.Count):       
                    grandChild = child.Children.Item(jj)
                    grandChildObjs.append(grandChild)
                    name = satConName+childName+grandChild.InstanceName+'s'
                    if root.CurrentScenario.Children.Contains(STKObjects.eConstellation,name):
                        conObj = root.GetObjectFromPath('Constellation/'+name)
                    else:
                        conObj = root.CurrentScenario.Children.New(STKObjects.eConstellation,name)
                    conGrandChildObjs.append(conObj.QueryInterface(STKObjects.IAgConstellation))
        
        
    try:
        satNames = ' '.join('tle-'+dfLoad['Ssc2'].values)
        root.ExecuteCommand('NewMulti / */Satellite '+str(len(dfLoad))+' '+satNames)
        for ii in range(len(dfLoad)): 
            cmd = 'Graphics */Satellite/tle-'+ dfLoad.loc[ii,'Ssc2'] + ' Show Off'
            root.ExecuteCommand(cmd)
            cmd = 'Graphics */Satellite/tle-'+ dfLoad.loc[ii,'Ssc2'] + ' SetColor '+color
            root.ExecuteCommand(cmd)
            sat = root.GetObjectFromPath('Satellite/tle-'+str(dfLoad.loc[ii,'Ssc2']))
            sat2 = sat.QueryInterface(STKObjects.IAgSatellite)
            sat2.SetPropagatorType(STKObjects.ePropagatorSGP4)
            prop = sat2.Propagator.QueryInterface(STKObjects.IAgVePropagatorSGP4)
            prop.CommonTasks.AddSegsFromFile(dfLoad.loc[ii,'Ssc2'],TLEFileName)
            prop.Propagate()
            
            try:
                satCon2.Objects.AddObject(sat)
            except:
                pass
            if satTempName != '':
                childrenObj = ImportChildren(children,sat)
                for jj in range(len(conObjs)):
                    child = childrenObj[jj]
                    try:
                        conObjs[jj].Objects.AddObject(child)
                    except:
                        pass
                for jj in range(len(conGrandChildObjs)):
                    grandChild = grandChildObjs[jj]
                    try:
                        conGrandChildObjs[jj].Objects.AddObject(grandChild)
                    except:
                        pass

    except:
        for ii in range(len(dfLoad)): 
            cmd = 'ImportTLEFile * "'+ TLEFileName  +'" SSCNumber '+ str(dfLoad.loc[ii,'Ssc2']) +' AutoPropagate On Merge On StartStop "' + startTime + '" "' + stopTime + '"'
            cmdOut = root.ExecuteCommand(cmd)
            cmd = 'Graphics */Satellite/tle-'+ dfLoad.loc[ii,'Ssc2'] + ' Show Off'
            root.ExecuteCommand(cmd)
            cmd = 'Graphics */Satellite/tle-'+ dfLoad.loc[ii,'Ssc2'] + ' SetColor '+color
            root.ExecuteCommand(cmd)
            sat = root.GetObjectFromPath('Satellite/tle-'+str(dfLoad.loc[ii,'Ssc2']))
            try:
                satCon2.Objects.AddObject(sat)
            except:
                pass
            if satTempName != '':
                childrenObj = ImportChildren(children,sat)
                for jj in range(len(conObjs)):
                    try:
                        conObjs[jj].Objects.AddObject(childrenObj[jj])
                    except:
                        pass
                for jj in range(len(conGrandChildObjs)):
                    grandChild = grandChildObjs[jj]
                    try:
                        conGrandChildObjs[jj].Objects.AddObject(grandChild)
                    except:
                        pass
    root.ExecuteCommand('BatchGraphics * Off')
    root.EndUpdate();

def UnloadObjs(root,objType,pattern='*'):
    root.BeginUpdate()
    root.ExecuteCommand('UnloadMulti / */'+objType+'/'+pattern)
    root.EndUpdate();


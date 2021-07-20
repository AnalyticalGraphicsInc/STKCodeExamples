# -*- coding: utf-8 -*-
"""
Created on Tue Jun 22 10:26:51 2021

@author: wlawrie
"""
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.stkobjects.astrogator import *
import math
from tkinter import *

#%%
#Define units in terms of seconds
days = 86400
hours = 3600
mins = 60
pi = math.pi
deg = pi/180
#Rename to location of ephemeris data storage (create your own)
directory = "D://EngineeringLab//RPOSimulator"
#%% Create New STK instance
# '17 Jun 2021 00:00:00.000','+30 days'
def newSTK(startTime,stopTime): #Create new STK instance
    global scenario
    global root
    i = 0
    #stk = STKDesktop.StartApplication(visible=False, userControl=False)
    stk = STKDesktop.StartApplication(visible=True, userControl=True)
    # Grab a handle on the STK application root.
    root = stk.Root
    root.NewScenario("PythonLuchSat")
    scenario = root.CurrentScenario
    scenario.SetTimePeriod(str(startTime),str(stopTime))
    root.Rewind();

#%% Satellite class
class satellite:
    def __createSat(self):
        self.sat = AgSatellite(scenario.Children.New(AgESTKObjectType.eSatellite,self.name))
    def __changeProp(self):
        self.sat.SetPropagatorType(12)
    def __init__(self,name): #Initial setup
        self.name = name
        self.__createSat()
        self.__changeProp()
        self.driver = self.sat.Propagator
    def fromTLE(self,SSCNum,FileName): #Create a satellite from a TLE
        self.reference = AgSatellite(scenario.Children.New(AgESTKObjectType.eSatellite,"ref"+self.name)) #Make a reference satellite
        self.reference.SetPropagatorType(4) #Change the propagator to be from the TLE
        propagator=self.reference.Propagator
        propagator.AutoUpdateEnabled = False
        propagator.CommonTasks.AddSegsFromFile(SSCNum,FileName)
        propagator.Propagate()
        self.reference.Graphics.IsObjectGraphicsVisible = False
        
        # Make below into a follow sequence method?
        self.driver.MainSequence.RemoveAll()
        follow = self.driver.MainSequence.Insert(3,"Follow","-") #Follow the reference satellite
        follow.Leader.BindTo("Satellite/ref"+self.name)
        StopDuration = follow.SeparationConditions.Item(0)
        TripVal = 0
        AgVAStoppingCondition(StopDuration.Properties).Trip = TripVal #Make the satellite separate at the beginning
        self.driver.RunMCS()
        self.sat.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(7) #Change lead and trail paths to be time based
        self.sat.VO.Pass.TrackData.PassData.Orbit.LeadData.Time = 12*hours
        self.sat.VO.Pass.TrackData.PassData.Orbit.SetTrailDataType(7)
        self.sat.VO.Pass.TrackData.PassData.Orbit.TrailData.Time = 3*days
        #self.FixedByWindow()
    def fromOnline(self,SSCNum): #Create a satellite from online database
        self.reference = AgSatellite(scenario.Children.New(AgESTKObjectType.eSatellite,"ref"+self.name)) #Make a reference satellite
        self.reference.SetPropagatorType(4) #Change the propagator to be from Online
        propagator=self.reference.Propagator
        propagator.CommonTasks.AddSegsFromOnlineSource(SSCNum)
        propagator.Propagate()
        self.reference.Graphics.IsObjectGraphicsVisible = False
        
        self.driver.MainSequence.RemoveAll()
        follow = self.driver.MainSequence.Insert(3,"Follow","-") #Follow the reference satellite
        follow.Leader.BindTo("Satellite/ref"+self.name)
        StopDuration = follow.SeparationConditions.Item(0)
        TripVal = 0
        AgVAStoppingCondition(StopDuration.Properties).Trip = TripVal #Make the satellite separate at the beginning
        self.driver.RunMCS()
        self.sat.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(7) #Change lead and trail paths to be time based
        self.sat.VO.Pass.TrackData.PassData.Orbit.LeadData.Time = 12*hours
        self.sat.VO.Pass.TrackData.PassData.Orbit.SetTrailDataType(7)
        self.sat.VO.Pass.TrackData.PassData.Orbit.TrailData.Time = 3*days
        #self.FixedByWindow()
    def updateEphemerisReference(self,fileName): #Update a satellite's reference by exporting ephemeris
        self.exportToEphemeris(fileName)
        self.reference.SetPropagatorType(6)
        propagator = self.reference.Propagator
        propagator.Filename = fileName
        propagator.Propagate()
        #self.VVLH(self)
        self.reference.Graphics.IsObjectGraphicsVisible = False
    def followreference(self):
        self.removeMCS()
        follow = self.driver.MainSequence.InsertByName("Follow","-")
        follow.Leader.BindTo("Satellite/ref"+self.name)
        follow.SeparationType=1
    def removeMCS(self):
        self.driver.MainSequence.RemoveAll()
    def followAtTime(self,tripVal):
        self.removeMCS()
        follow = self.driver.MainSequence.InsertByName("Follow","-")
        follow.Leader.BindTo("Satellite/ref"+self.name)
        StopDuration = follow.SeparationConditions.Item(0)
        AgVAStoppingCondition(StopDuration.Properties).Trip = tripVal
    def follow(self,tripVal):
        follow = self.driver.MainSequence.Item("Follow")
        follow.Leader.BindTo("Satellite/ref"+self.name)
        StopDuration = follow.SeparationConditions.Item(0)
        AgVAStoppingCondition(StopDuration.Properties).Trip = tripVal
    def fromEphemeris(self,fileName,separationTime):
        self.reference = AgSatellite(scenario.Children.New(AgESTKObjectType.eSatellite,"ref"+self.name))
        self.reference.SetPropagatorType(6)
        propagator = self.reference.Propagator
        propagator.Filename = fileName
        propagator.Propagate()
        self.reference.Graphics.IsObjectGraphicsVisible = False
        
        self.removeMCS()
        follow = self.driver.MainSequence.Insert(3,"Follow","-")
        follow.Leader.BindTo("Satellite/ref"+self.name)
        StopDuration = follow.SeparationConditions.Item(0)
        TripVal = separationTime * hours
        AgVAStoppingCondition(StopDuration.Properties).Trip = TripVal
        self.driver.RunMCS()
        self.sat.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(7)
        self.sat.VO.Pass.TrackData.PassData.Orbit.LeadData.Time = 12*hours
        self.sat.VO.Pass.TrackData.PassData.Orbit.SetTrailDataType(7)
        self.sat.VO.Pass.TrackData.PassData.Orbit.TrailData.Time = 3*days
    def fromReference(self,inTrack,duration,epoch,targetsat):
        self.sat.ReferenceVehicle.BindTo("Satellite/"+ targetsat)
        self.driver.MainSequence.RemoveAll()
        VbarRef = self.driver.MainSequence.InsertByName("Vbar Reference","-")
        VbarRef.ScriptingTool.Parameters.Item("Desired_InTrack").ParamValue = str(inTrack) #Desired_InTrack
        VbarRef.ScriptingTool.Parameters.Item("Ephem_Duration").ParamValue = str(duration) #Ephem_Duration
        VbarRef.ScriptingTool.Parameters.Item("VBar_Epoch").ParamValue = epoch #VBar_Epoch
        VbarRef.ScriptingTool.Parameters.Item("Target_Satellite").ParamValue = targetsat #Target_Satellite
        VbarRef.ScriptingTool.Parameters.Item("WhoAmI").ParamValue = self.name #WhoAmI
        self.driver.RunMCS()
        self.sat.Graphics.IsObjectGraphicsVisible = False   
        #self.FixedByWindow()
    def setRef(self,targetsat):
        driver = self.sat.Propagator
        SetReference = driver.MainSequence.InsertByName("Set Reference Vehicle", "-")
        SetReference.ScriptingTool.Parameters.Item("WhoAmI").ParamValue = self.name
        SetReference.ScriptingTool.Parameters.Item("Reference Vehicle").ParamValue = "ref"+targetsat.name
        self.VVLH(targetsat)
    def GEOtoGEORendezvous(self,targetsat,departure,drift):
        driver = self.sat.Propagator
        self.setRef(targetsat)
        GeoRendez = driver.MainSequence.InsertByName("GEO to GEO Rendezvous", "-")
        GeoRendez.ScriptingTool.Parameters.Item("Lead_Satellite").ParamValue = "ref"+self.name
        GeoRendez.ScriptingTool.Parameters.Item("Target_Satellite").ParamValue = "ref"+targetsat.name
        GeoRendez.ScriptingTool.Parameters.Item("Desired_DriftRate").ParamValue = str(drift)
        GeoRendez.ScriptingTool.Parameters.Item("Earliest_Departure_Time").ParamValue = departure
        GeoRendez.ScriptingTool.Parameters.Item("WhoAmI").ParamValue = self.name
        #driver.RunMCS()
        driver.ClearDWCGraphics()
    def insertPropagate(self,TripVal):
        driver = self.driver
        propagate = driver.MainSequence.InsertByName("Propagate","-")
        StopDuration = propagate.StoppingConditions.Item(0)
        AgVAStoppingCondition(StopDuration.Properties).Trip = TripVal
        driver.Options.DrawTrajectoryIn3D = False
        #driver.RunMCS()
    def propagateToStop(self):
        driver = self.driver
        propagate = driver.MainSequence.InsertByName("Propagate","-")
        propToStop = propagate.StoppingConditions.Add("UserSelect")
        StopDuration = propagate.StoppingConditions.Item(0)
        propagate.StoppingConditions.Remove(0)
        propToStop.Properties.Trip = root.CurrentScenario.StopTime
    def coast(self,TripVal,method):
        driver = self.sat.Propagator
        Coast = driver.MainSequence.InsertByName("Coast","-")
        if method == "Duration":
            Coast.ScriptingTool.Parameters.Item("Coast_Duration").ParamValue = str(TripVal)
        elif method == "Revs":
            Coast.ScriptingTool.Parameters.Item("Coast_Definition").ParamValue = method
            Coast.ScriptingTool.Parameters.Item("Coast_Duration").ParamValue = str(TripVal)
        
    def runMCS(self):
        driver = self.driver
        driver.RunMCS()
        driver.ClearDWCGraphics()
    def NMC(self,SMinAxis,RBarCross,VBarCross,initPhaseAng,duration,inTrackOffset,inTrackDrift,wayMaxDeg):
        driver = self.sat.Propagator
        NMCircum = driver.MainSequence.InsertByName("NMCircumnav","-")
        NMCircum.ScriptingTool.Parameters.Item("SemiMinor_Axis").ParamValue = SMinAxis
        NMCircum.ScriptingTool.Parameters.Item("RBar_CrossTrack_Offset").ParamValue = RBarCross
        NMCircum.ScriptingTool.Parameters.Item("VBar_CrossTrack_Offset").ParamValue = VBarCross
        NMCircum.ScriptingTool.Parameters.Item("Init_Phase_Angle").ParamValue = initPhaseAng
        NMCircum.ScriptingTool.Parameters.Item("Transfer_Duration").ParamValue = str(duration)
        NMCircum.ScriptingTool.Parameters.Item("InTrack_Offset").ParamValue = inTrackOffset
        NMCircum.ScriptingTool.Parameters.Item("InTrack_Drift").ParamValue = inTrackDrift
        NMCircum.ScriptingTool.Parameters.Item("Way_MaxDegrees").ParamValue = wayMaxDeg
        NMCircum.ScriptingTool.Parameters.Item("WhoAmI").ParamValue = self.name
        #driver.RunMCS()
        driver.ClearDWCGraphics()
    def VBarHop(self,offset):
        driver = self.sat.Propagator
        VBarHop = driver.MainSequence.InsertByName("VBar Hop","-")
        VBarHop.ScriptingTool.Parameters.Item("Final_VBar_Offset").ParamValue = str(offset)
        VBarHop.ScriptingTool.Parameters.Item("WhoAmI").ParamValue = self.name
    def FixedByWindow(self):
        self.sat.VO.OrbitSystems.InertialByWindow.IsVisible = False
        self.sat.VO.OrbitSystems.FixedByWindow.IsVisible = True
    def VVLH(self,targetsat):
        self.sat.VO.OrbitSystems.RemoveAll()
        self.sat.VO.OrbitSystems.InertialByWindow.IsVisible = False
        self.sat.VO.OrbitSystems.Add("Satellite/ref"+targetsat.name+" VVLH System")
        #self.sat.VO.OrbitSystems.Add("Satellite/"+targetsat.name+" VVLH System")
        #return VVLH
    def GEOtoGEORendezvousNoLead(self,targetsat,drift):
        self.setRef(targetsat)
        driver = self.sat.Propagator
        GeoRendez = driver.MainSequence.InsertByName("GEO to GEO Rendezvous NoLead","-")
        GeoRendez.ScriptingTool.Parameters.Item("Desired_DriftRate").ParamValue = str(drift)
        GeoRendez.ScriptingTool.Parameters.Item("WhoAmI").ParamValue = self.name
        #driver.RunMCS()
        driver.ClearDWCGraphics()
    def exportToEphemeris(self,fileName):
        export = self.sat.ExportTools.GetEphemerisStkExportTool()
        export.Export(fileName)

#%% GUI and Logic

gui = Tk()
gui.title('Create New Scenario')
gui.geometry("500x300")
#Methods
def createScenario():
    global myFrame
    
    startTime = estart.get()
    stopTime = estop.get()
    
    myFrame.destroy()
    displaySatList()
    newSTK(startTime,stopTime)
def displaySatList():
    gui.title('Satellite List')
    satWizard.grid_forget()
    entryFrame.grid_forget()
    satFrame.grid(row=0,column=0,sticky=W)
    menuFrame.grid(row=1,column=0)
def addSatMenu():
    var = r.get()
    
    if var==1:
        gui.title('Add Satellite from Ephemeris')
        eDirectory.grid(row=1,column=1,columnspan=2,sticky=W)
        directoryLabel.grid(row=1,column=0)
    elif var==2:
        gui.title('Add Satellite from TLE File')
        eDirectory.grid(row=1,column=1,columnspan=2,sticky=W)
        directoryLabel.grid(row=1,column=0)
    elif var==3:
        gui.title('Add Satellite from Online Database')
        eDirectory.grid_forget()
        directoryLabel.grid_forget()
    if var!=0:
        satFrame.grid_forget()
        menuFrame.grid_forget()
        satWizard.grid(row=1,column=0)
        entryFrame.grid(row=0,column=0)
        global wizard
        wizard=0
    
def addSatellite():
    name = eSatName.get()
    myName = Label(satFrame,text=name)
    myName.grid(row=len(satList)+1,column=0,sticky=W)
    direc = eDirectory.get()
    myDir = Label(satFrame,text=direc)
    myDir.grid(row=len(satList)+1,column=1,sticky=W)
    SSCNum = eSSC.get()
    myNum = Label(satFrame,text=SSCNum)
    myNum.grid(row=len(satList)+1,column=2,sticky=W)
    intercept=False
    myTeam = Label(satFrame,text='No')
    myTeam.grid(row=len(satList)+1,column=3,sticky=W)
    myIndex = Radiobutton(satFrame,variable=w,value=len(satList))
    myIndex.grid(row=len(satList)+1,column=4,sticky=W)
    
    eSatName.delete(0,END)
    eDirectory.delete(0,END)
    eSSC.delete(0,END)
    
    addType = r.get()
    alive = True
    displaySatList()
    myList = [name,direc,SSCNum,alive,myName,myDir,myNum,myIndex,addType,myTeam,intercept]
    satList.append(myList)
    textvar='yes'
def createSats():
    index = 0
    global turn
    global directory
    for sat in satList:
        if sat[3]:
            if sat[8] == 2:
                newSat = satellite(sat[0])
                newSat.fromTLE(sat[2], sat[1])
                newSat.propagateToStop()
                newSat.runMCS()
                fileName = directory+sat[0]+"_turn"+str(turn)+".e"
                newSat.updateEphemerisReference(fileName)
                newSat.followAtTime(0)
            elif sat[8] == 3:
                newSat = satellite(sat[0])
                newSat.fromOnline(sat[2])
                newSat.propagateToStop()
                newSat.runMCS()
                fileName = directory+sat[0]+"_turn"+str(turn)+".e"
                newSat.updateEphemerisReference(fileName)
                newSat.followAtTime(0)
            elif sat[8] == 1:
                newSat = satellite(sat[0])
                newSat.fromEphemeris(sat[1],0)
            satList[index].append(newSat)
            satList[index].append(False)
            if not sat[10]:
                satList[index][11].FixedByWindow()
                #satList[index][11].insertPropagate(30*days) #Change to length of scenario
            if index != intSat:
                nameList.append(sat[0])
            index+=1
    global satclicked
    global satelliteDropdown
    satclicked = StringVar()
    satclicked.set(nameList[0])
    satelliteDropdown = OptionMenu(propertiesFrame,satclicked,*nameList)
    global player
    player = 1
    playerTurn()
def updateTurn():
    global turn
    global p1Pass
    p1Pass = False
    turn += 1
    currentTime = 43200*(turn-1)
    root.CurrentTime = currentTime
def passTurn():
    global player
    global turn
    global p1Pass
    if player == 1:
        p1Pass = True
        player = 2
    elif player == 2:
        if p1Pass:
            updateTurn()
            player = 1
        else: 
            player = 3
    playerTurn()
def playerTurn():
    global player
    global turn
    gui.title("Turn "+str(turn))
    if player == 1:
        player2Label.grid_forget()
        player1Label.grid(column=0,row=0)
    elif player == 2:
        player1Label.grid_forget()
        player2Label.grid(column=0,row=0)
    elif player == 3:
        runAllMCS()
        updateRefs()
        player = 1
        updateTurn()
        playerTurn()
        return
    satFrame.grid_forget()
    menuFrame.grid_forget()
    maneuverDefFrame.grid_forget()
    maneuverIntFrame.grid_forget()
    maneuverMenuFrame.grid_forget()
    turnFrame.grid(column=0,row=0)

def addMCSSegments():
    global player
    global turn
    if player == 1:
        i = 0
        for sat in satList:
            if sat[10]:
                maneuveringSat = sat[11]
                if sat[12]:
                    maneuveringSat.removeMCS()
                    currentTime = 43200*(turn-1)
                    maneuveringSat.followAtTime(currentTime)
                    satList[i][12]=False
                break
            i+=1
        index = 0
        for maneuver in intProps:
            if maneuver[0]:
                for sat in satList:
                    if sat[0]==maneuver[2]:
                        targetSat = sat[11]
                        break
                if maneuver[1]=="Set Reference":
                    maneuveringSat.setRef(targetSat)
                elif maneuver[1]=="GEO to GEO Rendezvous":
                    maneuveringSat.GEOtoGEORendezvous(targetSat,maneuver[3],float(maneuver[4])/days)
                elif maneuver[1]=="GEO to GEO Rendezvous NoLead":
                    maneuveringSat.GEOtoGEORendezvousNoLead(targetSat,float(maneuver[4])/days)
                elif maneuver[1]=="NMC":
                    maneuveringSat.NMC(maneuver[6],maneuver[7],maneuver[8],maneuver[9],maneuver[5],maneuver[10],maneuver[11],maneuver[12])
                elif maneuver[1]=="VBar Hop":
                    maneuveringSat.VBarHop(maneuver[13])
                elif maneuver[1]=="Coast":
                    maneuveringSat.coast(maneuver[5],maneuver[14])
                intProps[index][0] = False
                index+=1
        #maneuveringSat.runMCS()
        player = 2
    elif player == 2:
        global defenseSat
        i = 0
        for maneuver in defProps:
            if maneuver[0]:
                maneuveringSatName = defenseSat[i].get()
                index = 0
                for sat in satList:
                    if maneuveringSatName == sat[0]:
                        maneuveringSat = sat[11]
                        if sat[12]:
                            maneuveringSat.removeMCS()
                            currentTime = 43200*(turn-1)
                            maneuveringSat.followAtTime(currentTime)
                            satList[index][12]=False
                        break
                    index+=1
                for sat in satList:
                    if sat[0]==maneuver[2]:
                        targetSat = sat[11]
                        break
                if maneuver[1]=="Set Reference":
                    maneuveringSat.setRef(targetSat)
                elif maneuver[1]=="GEO to GEO Rendezvous":
                    maneuveringSat.GEOtoGEORendezvous(targetSat,maneuver[3],int(maneuver[4])/days)
                elif maneuver[1]=="GEO to GEO Rendezvous NoLead":
                    maneuveringSat.GEOtoGEORendezvousNoLead(targetSat,int(maneuver[4])/days)
                elif maneuver[1]=="NMC":
                    maneuveringSat.NMC(maneuver[6],maneuver[7],maneuver[8],maneuver[9],maneuver[5],maneuver[10],maneuver[11],maneuver[12])
                elif maneuver[1]=="VBar Hop":
                    maneuveringSat.VBarHop(maneuver[13])
                elif maneuver[1]=="Coast":
                    maneuveringSat.coast(maneuver[5],maneuver[14])
                defProps[i][0] = False
                index+=1
            i +=1
        player = 3
    playerTurn()
def runAllMCS():
    for sat in satList:
        if sat[3]:
            #sat[11].follow(0)
            sat[11].propagateToStop()
            sat[11].runMCS()
            sat[11].followreference()
    
def updateRefs():
    global turn
    global directory
    for sat in satList:
        if sat[3]:
            fileName = directory+sat[0]+"_turn"+str(turn)+".e"
            sat[11].updateEphemerisReference(fileName)
            sat[11].follow(0)
def exportAndClear():
    global turn
    global directory
    for sat in satList:
        if sat[3] and sat[12]:
            fileName = directory+sat[0]+"_turn"+str(turn)+".e"
            sat[11].exportToEphemeris(fileName)
            sat[11].sat.Unload()
            sat[11].reference.Unload()
            separationTime = (turn)*12
            sat[11] = satellite(sat[0]).fromEphemeris(fileName,separationTime)
    #Export satellite into .e file
    intManeuvers.clear()
    intProps.clear()
    defManeuvers.clear()
    defProps.clear()
def clearSats():
    for sat in satList:
        if sat[3]:
            sat[11].sat.Unload()
            sat[11].reference.Unload()
            sat[3]=False
def removeSat():
    var=w.get()
    if satList[var][3]:
        satList[var][4].destroy()
        satList[var][5].destroy()
        satList[var][6].destroy()
        satList[var][7].destroy()
        satList[var][9].destroy()
        satList[var][3]=False
def changeInterceptor():
    var=w.get()
    index = 0
    for sat in satList:
        if sat[3] and sat[10]:
            satList[index][10]=False
            satList[index][9].destroy()
            satList[index][9] = Label(satFrame,text='No')
            satList[index][9].grid(row=index+1,column=3,sticky=W)
        index+=1
    if satList[var][3]:
        satList[var][10]= not satList[var][10]   
        if satList[var][10]:
            satList[var][9].destroy()
            satList[var][9] = Label(satFrame,text='Yes')
            satList[var][9].grid(row=var+1,column=3,sticky=W)
        else:
            satList[var][9].destroy()
            satList[var][9] = Label(satFrame,text='No')
            satList[var][9].grid(row=var+1,column=3,sticky=W)
    global intSat 
    intSat= var
def displayManeuvers():
    gui.title("Add Maneuvers")
    satFrame.grid_forget()
    menuFrame.grid_forget()
    turnFrame.grid_forget()
    propertiesFrame.grid_forget()
    satWizard.grid_forget()
    if player==1:
        maneuverIntFrame.grid(row=0,column=0,sticky=W)
    elif player==2:
        maneuverDefFrame.grid(row=0,column=0,sticky=W)
    maneuverMenuFrame.grid(row=1,column=0,sticky=W)
def addManeuverMenu():
    var=maneuverType.get()
    
    targetSatLabel.grid_forget()
    departureLabel.grid_forget()
    driftLabel.grid_forget()
    TripValLabel.grid_forget()
    coastDefLabel.grid_forget()
    offsetLabel.grid_forget()
    SMinAxisLabel.grid_forget()
    VBarCrossLabel.grid_forget()
    RBarCrossLabel.grid_forget()
    initPhaseAngleLabel.grid_forget()
    inTrackOffsetAngleLabel.grid_forget()
    inTrackDragLabel.grid_forget()
    WayMaxDegLabel.grid_forget()
    
    satelliteDropdown.grid_forget()
    eDeparture.grid_forget()
    eDrift.grid_forget()
    eTripVal.grid_forget()
    eCoastDef.grid_forget()
    eOffset.grid_forget()
    eSMinAxis.grid_forget()
    eRBarCross.grid_forget()
    eVBarCross.grid_forget()
    eInitPhaseAngle.grid_forget()
    eInTrackOffsetAngle.grid_forget()
    eInTrackDrag.grid_forget()
    eWayMaxDeg.grid_forget()
    global wizard
    if wizard != 2:
        wizard = 1
    satWizard.grid(row=1,column=0)
    if var=="Set Reference":
        gui.title(var)
        
        
        targetSatLabel.grid(row=0,column=0)
        satelliteDropdown.grid(row=0,column=1)
    elif var=="GEO to GEO Rendezvous":
        gui.title(var)
        
        targetSatLabel.grid(row=0,column=0)
        departureLabel.grid(row=1,column=0)
        driftLabel.grid(row=2,column=0)
        
        satelliteDropdown.grid(row=0,column=1)
        eDeparture.grid(row=1,column=1)
        eDrift.grid(row=2,column=1)
    elif var=="GEO to GEO Rendezvous NoLead":
        gui.title(var)
        
        targetSatLabel.grid(row=0,column=0)
        driftLabel.grid(row=2,column=0)
        
        satelliteDropdown.grid(row=0,column=1)
        eDrift.grid(row=2,column=1)
    elif var=="NMC":
        gui.title(var)
        
        TripValLabel.grid(row=3,column=0)
        SMinAxisLabel.grid(row=6,column=0)
        RBarCrossLabel.grid(row=7,column=0)
        VBarCrossLabel.grid(row=8,column=0)
        initPhaseAngleLabel.grid(row=9,column=0)
        inTrackOffsetAngleLabel.grid(row=10,column=0)
        inTrackDragLabel.grid(row=11,column=0)
        WayMaxDegLabel.grid(row=12,column=0)
        
        eTripVal.grid(row=3,column=1)
        eSMinAxis.grid(row=6,column=1)
        eRBarCross.grid(row=7,column=1)
        eVBarCross.grid(row=8,column=1)
        eInitPhaseAngle.grid(row=9,column=1)
        eInTrackOffsetAngle.grid(row=10,column=1)
        eInTrackDrag.grid(row=11,column=1)
        eWayMaxDeg.grid(row=12,column=1)
    elif var=="VBar Hop":
        gui.title(var)
        
        offsetLabel.grid(row=5,column=0)
        eOffset.grid(row=5,column=1)
    elif var=="Coast":
        gui.title(var)
        
        coastDefLabel.grid(row=4,column=0)
        TripValLabel.grid(row=3,column=0)
        
        eCoastDef.grid(row=4,column=1)
        eTripVal.grid(row=3,column=1)
    
    propertiesFrame.grid(row=0,column=0)
    maneuverMenuFrame.grid_forget()
    maneuverIntFrame.grid_forget()
    maneuverDefFrame.grid_forget()
def display():
    if wizard==0:
        displaySatList()
    if wizard==1:
        displayManeuvers()
    if wizard==2:
        displayManeuvers()
def add():
    if wizard==0:
        addSatellite()
    if wizard==1:
        addManeuver()
    if wizard==2:
        editManeuver()
def editMCS():
    global turn
    global player
    if turn==1:
        displayManeuvers()
    elif player==1:
        for sat in satList:
            if sat[10]:
                maneuveringSat = sat[11]
                break
        maneuveringSat.followreference()
        displayManeuvers()
    elif player==2:
        for sat in satList:
            if not sat[10]:
                maneuveringSat = sat[11]
                maneuveringSat.followreference()
            displayManeuvers()
def addManeuver():
    name=maneuverType.get()
    global manList
    global player
    if player==1:
        myName = Label(maneuverIntFrame,text=name)
        myName.grid(row=len(intManeuvers)+1,column=0,sticky=W)
        mySat = Label(maneuverIntFrame,text=satList[intSat][0])
        mySat.grid(row=len(intManeuvers)+1,column=1,sticky=W)
        myIndex = Radiobutton(maneuverIntFrame,variable=v,value=len(intManeuvers))
        myIndex.grid(row=len(intManeuvers)+1,column=2,sticky=W)
    elif player==2:
        global defenseSat
        option = StringVar()
        myName = Label(maneuverDefFrame,text=name)
        myName.grid(row=len(defManeuvers)+1,column=0,sticky=W)
        mySat = OptionMenu(maneuverDefFrame,option,*nameList)
        mySat.grid(row=len(defManeuvers)+1,column=1,sticky=W)
        myIndex = Radiobutton(maneuverDefFrame,variable=v,value=len(defManeuvers))
        myIndex.grid(row=len(defManeuvers)+1,column=2,sticky=W)
        defenseSat.append(option)
    targetSat = ""
    departure = ""
    driftRate = ""
    tripVal = ""
    SMinAxis = ""
    RBarCross = ""
    VBarCross = ""
    initPhaseAngle = ""
    inTrackOffsetAngle = ""
    inTrackDrag = ""
    wayMaxDeg = ""
    VBarOffset = ""
    coastDef = ""
    if name=="Set Reference":
        targetSat = satclicked.get()
    elif name=="GEO to GEO Rendezvous":
        targetSat = satclicked.get()
        departure = eDeparture.get()
        driftRate = eDrift.get()
    elif name=="GEO to GEO Rendezvous NoLead":
        targetSat = satclicked.get()
        driftRate = eDrift.get()
    elif name=="NMC":
        tripVal = eTripVal.get()
        SMinAxis = eSMinAxis.get()
        RBarCross = eRBarCross.get()
        VBarCross = eVBarCross.get()
        initPhaseAngle = eInitPhaseAngle.get()
        inTrackOffsetAngle = eInTrackOffsetAngle.get()
        inTrackDrag = eInTrackDrag.get()
        wayMaxDeg = eWayMaxDeg.get()
    elif name=="VBar Hop":
        VBarOffset = eOffset.get()
    elif name=="Coast":
        tripVal = eTripVal.get()
        coastDef = eCoastDef.get()
    
    eDeparture.delete(0,END)
    eDrift.delete(0,END)
    eTripVal.delete(0,END)
    eCoastDef.delete(0,END)
    eOffset.delete(0,END)
    eSMinAxis.delete(0,END)
    eRBarCross.delete(0,END)
    eVBarCross.delete(0,END)
    eInitPhaseAngle.delete(0,END)
    eInTrackOffsetAngle.delete(0,END)
    eInTrackDrag.delete(0,END)
    eWayMaxDeg.delete(0,END)
    
    alive = True
    
    myList = [myName,mySat,myIndex,alive]
    tempList = [alive,name,targetSat,departure,driftRate,tripVal,SMinAxis,
                RBarCross,VBarCross,initPhaseAngle,inTrackOffsetAngle,inTrackDrag,
                wayMaxDeg,VBarOffset,coastDef]
    #manList.append(myList)
    if player==1:
        satList[intSat][12] = True
        intManeuvers.append(myList)
        intProps.append(tempList)
    elif player==2:
        defManeuvers.append(myList)
        defProps.append(tempList)
        index = 0
        for sat in satList:
            if not sat[10]:
                satList[index][12] = True
            index += 1
    #manProps.append(tempList)
    
    global wizard
    wizard=1
    display()
def displayEditManeuver():
    var=v.get()
    
    eDeparture.delete(0,END)
    eDrift.delete(0,END)
    eTripVal.delete(0,END)
    eCoastDef.delete(0,END)
    eOffset.delete(0,END)
    eSMinAxis.delete(0,END)
    eRBarCross.delete(0,END)
    eVBarCross.delete(0,END)
    eInitPhaseAngle.delete(0,END)
    eInTrackOffsetAngle.delete(0,END)
    eInTrackDrag.delete(0,END)
    eWayMaxDeg.delete(0,END)
    global player
    global wizard
    if player==1:
        alive = intProps[var][0]
        if alive:
            satclicked.set(intProps[var][2])
            eDeparture.insert(0,intProps[var][3])
            eDrift.insert(0,intProps[var][4])
            eTripVal.insert(0,intProps[var][5])
            eCoastDef.insert(0,intProps[var][14])
            eOffset.insert(0,intProps[var][13])
            eSMinAxis.insert(0,intProps[var][6])
            eRBarCross.insert(0,intProps[var][7])
            eVBarCross.insert(0,intProps[var][8])
            eInitPhaseAngle.insert(0,intProps[var][9])
            eInTrackOffsetAngle.insert(0,intProps[var][10])
            eInTrackDrag.insert(0,intProps[var][11])
            eWayMaxDeg.insert(0,intProps[var][12])
        
            wizard = 2
            add()
    if player==2:
        alive = defProps[var][0]
        if alive:
            satclicked.set(defProps[var][2])
            eDeparture.insert(0,defProps[var][3])
            eDrift.insert(0,defProps[var][4])
            eTripVal.insert(0,defProps[var][5])
            eCoastDef.insert(0,defProps[var][14])
            eOffset.insert(0,defProps[var][13])
            eSMinAxis.insert(0,defProps[var][6])
            eRBarCross.insert(0,defProps[var][7])
            eVBarCross.insert(0,defProps[var][8])
            eInitPhaseAngle.insert(0,defProps[var][9])
            eInTrackOffsetAngle.insert(0,defProps[var][10])
            eInTrackDrag.insert(0,defProps[var][11])
            eWayMaxDeg.insert(0,defProps[var][12])
        
            wizard = 2
            # Not sure which of these to keep/change, but as of yet this function is not working properly
            addManeuverMenu()
            add()
def editManeuver():
    var=v.get()
    
    if player==1:
        name = intProps[var][1]
    if player==2:
        name = defProps[var][1]
    
    targetSat = ""
    departure = ""
    driftRate = ""
    tripVal = ""
    SMinAxis = ""
    RBarCross = ""
    VBarCross = ""
    initPhaseAngle = ""
    inTrackOffsetAngle = ""
    inTrackDrag = ""
    wayMaxDeg = ""
    VBarOffset = ""
    coastDef = ""
    if name=="Set Reference":
        targetSat = satclicked.get()
    elif name=="GEO to GEO Rendezvous":
        targetSat = satclicked.get()
        departure = eDeparture.get()
        driftRate = eDrift.get()
    elif name=="GEO to GEO Rendezvous NoLead":
        targetSat = satclicked.get()
        driftRate = eDrift.get()
    elif name=="NMC":
        tripVal = eTripVal.get()
        SMinAxis = eSMinAxis.get()
        RBarCross = eRBarCross.get()
        VBarCross = eVBarCross.get()
        initPhaseAngle = eInitPhaseAngle.get()
        inTrackOffsetAngle = eInTrackOffsetAngle.get()
        inTrackDrag = eInTrackDrag.get()
        wayMaxDeg = eWayMaxDeg.get()
    elif name=="VBar Hop":
        VBarOffset = eOffset.get()
    elif name=="Coast":
        tripVal = eTripVal.get()
        coastDef = eCoastDef.get()
    
    eDeparture.delete(0,END)
    eDrift.delete(0,END)
    eTripVal.delete(0,END)
    eCoastDef.delete(0,END)
    eOffset.delete(0,END)
    eSMinAxis.delete(0,END)
    eRBarCross.delete(0,END)
    eVBarCross.delete(0,END)
    eInitPhaseAngle.delete(0,END)
    eInTrackOffsetAngle.delete(0,END)
    eInTrackDrag.delete(0,END)
    eWayMaxDeg.delete(0,END)
    
    #manProps[var] = [manProps[var][0],manProps[var][1],targetSat,departure,driftRate,tripVal,SMinAxis,
    #           RBarCross,VBarCross,initPhaseAngle,inTrackOffsetAngle,inTrackDrag,
    #           wayMaxDeg,VBarOffset,coastDef]
    if player==1:
        intProps[var] = [intProps[var][0],intProps[var][1],targetSat,departure,driftRate,tripVal,SMinAxis,
                    RBarCross,VBarCross,initPhaseAngle,inTrackOffsetAngle,inTrackDrag,
                    wayMaxDeg,VBarOffset,coastDef]
    elif player==2:
        defProps[var] = [defProps[var][0],defProps[var][1],targetSat,departure,driftRate,tripVal,SMinAxis,
                    RBarCross,VBarCross,initPhaseAngle,inTrackOffsetAngle,inTrackDrag,
                    wayMaxDeg,VBarOffset,coastDef]
    
    global wizard
    wizard = 1
    display()
def removeManeuver():
    var=v.get()
    #if manList[var][3]:
    #    manList[var][0].destroy()
    #    manList[var][1].destroy()
    #    manList[var][2].destroy()
    #    manList[var][3]=False
    #    manProps[var][0]=False
    if player==1 and intManeuvers[var][3]:
        intManeuvers[var][0].destroy()
        intManeuvers[var][1].destroy()
        intManeuvers[var][2].destroy()
        intManeuvers[var][3]=False
        intProps[var][0]=False
    if player==2 and defManeuvers[var][3]:
        defManeuvers[var][0].destroy()
        defManeuvers[var][1].destroy()
        defManeuvers[var][2].destroy()
        defManeuvers[var][3]=False
        defProps[var][0]=False

#Global Variables
intSat=int

#Starting fields
myFrame = Frame(gui)
estart = Entry(myFrame,width = 35,borderwidth=5)
estop = Entry(myFrame,width = 35,borderwidth=5)
startLabel = Label(myFrame, text="Enter Start Time:")
stopLabel = Label(myFrame, text="Enter Stop Time:")

#Place on window
myFrame.grid(row=0,column=0,columnspan=3,rowspan=3)
estart.grid(row=0,column=1,columnspan=3,padx=10,pady=10)
estop.grid(row=1,column=1,columnspan=3,padx=10,pady=10)
startLabel.grid(row=0,column=0)
stopLabel.grid(row=1,column=0)

newScenario = Button(myFrame,text="Create", command=createScenario)
newScenario.grid(row=2,column=1,columnspan=2)

#Sat List Frame
satFrame = Frame(gui)
satLabel1 = Label(satFrame,text="Satellite")
satLabel1.grid(row=0,column=0,columnspan=1,sticky=W)
satLabel2 = Label(satFrame,text="Directory")
satLabel2.grid(row=0,column=1,columnspan=1,sticky=W)
satLabel3 = Label(satFrame,text="SSC")
satLabel3.grid(row=0,column=2,columnspan=1,sticky=W)
satLabel4 = Label(satFrame,text="Interceptor?")
satLabel4.grid(row=0,column=3,columnspan=1,sticky=W)
satLabel5 = Label(satFrame,text="Select")
satLabel5.grid(row=0,column=4,columnspan=1,sticky=W)
interceptButton = Button(satFrame,text="Change Interceptor",command=changeInterceptor)
interceptButton.grid(row=0,column=5,sticky=W)

#Menu Frame
menuFrame = Frame(gui)
r = IntVar()
w = IntVar()
v = IntVar()
TLERadio = Radiobutton(menuFrame,text="From TLE",variable=r,value=2)
DatabaseRadio = Radiobutton(menuFrame,text="From Database",variable=r,value=3)
EphemerisRadio = Radiobutton(menuFrame,text="From Ephemeris",variable=r,value=1)
EphemerisRadio.grid(row=0,column=1,sticky=W)
TLERadio.grid(row=1,column=1,sticky=W)
DatabaseRadio.grid(row=2,column=1,sticky=W)
    
#Menu Buttons
addButton = Button(menuFrame,text="Add",command=addSatMenu)
createButton = Button(menuFrame,text="Start Simulation",command=createSats)
removeButton = Button(menuFrame,text="Remove",command=removeSat)
removeButton.grid(row=1,column=0,sticky=W)
addButton.grid(row=0,column=0)
createButton.grid(row=1,column=2)

wizard = int
#Wizard Frame
satWizard = Frame(gui)
cancel = Button(satWizard,text="Cancel",command=display)
confirm = Button(satWizard,text="Confirm",command=add)
cancel.grid(row=1,column=0)
confirm.grid(row=1,column=2)

#Entry Frame
entryFrame = Frame(gui)
eSatName = Entry(entryFrame,width = 35,borderwidth=5)
eSatName.grid(row=0,column=1,columnspan=2,sticky=W)
eDirectory = Entry(entryFrame,width = 35,borderwidth=5)
eDirectory.grid(row=1,column=1,columnspan=2,sticky=W)
eSSC = Entry(entryFrame,width = 15,borderwidth=5)
eSSC.grid(row=2,column=1,sticky=W)

#Entry Frame Labels
satNameLabel = Label(entryFrame,text="Satellite Name")
satNameLabel.grid(row=0,column=0)
directoryLabel = Label(entryFrame,text="File Directory")
directoryLabel.grid(row=1,column=0)
SSCLabel = Label(entryFrame,text="SSC Number")
SSCLabel.grid(row=2,column=0)

#Maneuver Frame
"""
maneuverFrame = Frame(gui)
maneuverLabel = Label(maneuverFrame,text="Maneuver List")
maneuverLabel.grid(row=0,column=0,sticky=W)
maneuverSat = Label(maneuverFrame,text="Maneuver Sat")
maneuverSat.grid(row=0,column=1,sticky=W)
removeManButton = Button(maneuverFrame,text="Remove",command=removeManeuver)
removeManButton.grid(row=0,column=2,sticky=W)
editManButton = Button(maneuverFrame,text="Edit",command=displayEditManeuver)
editManButton.grid(row=0,column=3,sticky=W)
"""
maneuverIntFrame = Frame(gui)
maneuverIntLabel = Label(maneuverIntFrame,text="Maneuver List")
maneuverIntLabel.grid(row=0,column=0,sticky=W)
maneuverIntSat = Label(maneuverIntFrame,text="Maneuver Sat")
maneuverIntSat.grid(row=0,column=1,sticky=W)
removeIntManButton = Button(maneuverIntFrame,text="Remove",command=removeManeuver)
removeIntManButton.grid(row=0,column=2,sticky=W)
editIntManButton = Button(maneuverIntFrame,text="Edit",command=displayEditManeuver)
editIntManButton.grid(row=0,column=3,sticky=W)

maneuverDefFrame = Frame(gui)
maneuverDefLabel = Label(maneuverDefFrame,text="Maneuver List")
maneuverDefLabel.grid(row=0,column=0,sticky=W)
maneuverDefSat = Label(maneuverDefFrame,text="Maneuver Sat")
maneuverDefSat.grid(row=0,column=1,sticky=W)
removeDefManButton = Button(maneuverDefFrame,text="Remove",command=removeManeuver)
removeDefManButton.grid(row=0,column=2,sticky=W)
editDefManButton = Button(maneuverDefFrame,text="Edit",command=displayEditManeuver)
editDefManButton.grid(row=0,column=3,sticky=W)
#Dropdown Options
options=[
    "Set Reference",
    "GEO to GEO Rendezvous",
    "GEO to GEO Rendezvous NoLead",
    "VBar Hop",
    "NMC",
    "Coast"
]
maneuverType = StringVar()
maneuverType.set("Set Reference")
#Maneuver Menu Frame
maneuverMenuFrame = Frame(gui)
addManeuverButton = Button(maneuverMenuFrame,text="Add",command=addManeuverMenu)
addManeuverButton.grid(row=0,column=0)
maneuverDropdown = OptionMenu(maneuverMenuFrame,maneuverType,*options)
maneuverDropdown.grid(row=0,column=1)
computeManeuverButton = Button(maneuverMenuFrame,text="Add to MCS",command = addMCSSegments)
computeManeuverButton.grid(row=0,column=2)


#Properties Frame
propertiesFrame = Frame(gui)
targetSatLabel = Label(propertiesFrame,text="Target Sat")#Dropdown
departureLabel = Label(propertiesFrame,text="Departure")#EField
driftLabel = Label(propertiesFrame,text="Drift Rate")
TripValLabel = Label(propertiesFrame,text="TripVal")
coastDefLabel = Label(propertiesFrame,text="Coast Definition")
offsetLabel = Label(propertiesFrame,text="VBar Offset")
SMinAxisLabel = Label(propertiesFrame,text="SMin Axis")
RBarCrossLabel = Label(propertiesFrame,text="RBar Cross")
VBarCrossLabel = Label(propertiesFrame,text="VBar Cross")
initPhaseAngleLabel = Label(propertiesFrame,text="Init Phase Angle")
inTrackOffsetAngleLabel = Label(propertiesFrame,text="In Track Offset Angle")
inTrackDragLabel = Label(propertiesFrame,text="In Track Drag")
WayMaxDegLabel = Label(propertiesFrame,text="Way Max Deg")

eDeparture = Entry(propertiesFrame,width = 20,borderwidth=5)
eDrift = Entry(propertiesFrame,width = 20,borderwidth=5)
eTripVal = Entry(propertiesFrame,width = 20,borderwidth=5)
eCoastDef = Entry(propertiesFrame,width = 20,borderwidth=5)
eDeparture = Entry(propertiesFrame,width = 20,borderwidth=5)
eOffset = Entry(propertiesFrame,width = 20,borderwidth=5)
eSMinAxis = Entry(propertiesFrame,width = 20,borderwidth=5)
eRBarCross = Entry(propertiesFrame,width = 20,borderwidth=5)
eVBarCross = Entry(propertiesFrame,width = 20,borderwidth=5)
eInitPhaseAngle = Entry(propertiesFrame,width = 20,borderwidth=5)
eInTrackOffsetAngle = Entry(propertiesFrame,width = 20,borderwidth=5)
eInTrackDrag = Entry(propertiesFrame,width = 20,borderwidth=5)
eWayMaxDeg = Entry(propertiesFrame,width = 20,borderwidth=5)

targetSatLabel.grid(row=0,column=0)
departureLabel.grid(row=1,column=0)
driftLabel.grid(row=2,column=0)
TripValLabel.grid(row=3,column=0)
coastDefLabel.grid(row=4,column=0)
offsetLabel.grid(row=5,column=0)
SMinAxisLabel.grid(row=6,column=0)
RBarCrossLabel.grid(row=7,column=0)
VBarCrossLabel.grid(row=8,column=0)
initPhaseAngleLabel.grid(row=9,column=0)
inTrackOffsetAngleLabel.grid(row=10,column=0)
inTrackDragLabel.grid(row=11,column=0)
WayMaxDegLabel.grid(row=12,column=0)

player = 1
turn = 1
turnFrame = Frame(gui)
player1Label = Label(turnFrame,text="Player 1 Turn")
player2Label = Label(turnFrame,text="Player 2 Turn")
continueButton = Button(turnFrame,text="Continue to Maneuver Phase",command=editMCS)
continueButton.grid(column=0,row=1)
passButton = Button(turnFrame,text="Pass Turn",command=passTurn)
passButton.grid(column=0,row=2)

p1Pass = False
defenseSat = []
satList = []
nameList = []
manList = []
intManeuvers = []
intProps = []
defProps = []
defManeuvers = []
manProps = []
mainloop()
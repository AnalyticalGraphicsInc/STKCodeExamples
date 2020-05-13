# -*- coding: utf-8 -*-
"""
Created on Mon May  4 09:33:16 2020

@author: jvergere

Ideas: Something similar to the Iridium Constellation:
    66 Sats
    781 km (7159 semimajor axis)
    86.4 inclination
    6 Orbit planes 30 degrees apart
    11 in each plane
"""
import datetime as dt
import numpy as np
import os

#Need to cleanup this file before running each time, 
#or refactor code to avoid writing to file in append mode
if os.path.exists("MaxOutageData.txt"):
    os.remove("MaxOutageData.txt")

from comtypes.client import CreateObject  # Will allow you to launch STK
#from comtypes.client import GetActiveObject  #Will allow you to connect a running instance of STK

#Start the application, it will return a pointer to the Application Interface
app = CreateObject("STK12.Application")
#app = GetActiveObject("STK12.Application")

#app is a pointer to IAgUiApplication
#type info is available with python builtin type method

#type(app)

#More info is available via python built in dir method, which will list
#all the available properties and methods available

#dir(app)

#Additional useful information is available via the python builtin help

#help(app)

app.Visible = True
app.UserControl = True

root = app.Personality2   #root ->IAgStkObjectRoot

#These are not available to import until this point if this is the first time
#running STK via COM with python....it won't hurt to leave them there, but after running once they can be
#included at the top with all the other import statements
from comtypes.gen import STKUtil
from comtypes.gen import STKObjects

root.NewScenario("NewTestScenario")

scenario = root.CurrentScenario  #scenario -> IAgStkObject

scenario2 = scenario.QueryInterface(STKObjects.IAgScenario) #scenaro2 -> IAgScenario
scenario2.StartTime = "1 Jun 2016 16:00:00.000"
scenario2.StopTime = "2 Jun 2016 16:00:00.000"

root.Rewind()

#Insert Facilites from text file using connect.  Each line of the text file is
#formatted:
#FacName,Longitude,Latitude

with open("Facilities.txt", "r") as faclist:
    for line in faclist:
        facData = line.strip().split(",")
        
        insertNewFacCmd = "New / */Facility {}".format(facData[0])
        root.ExecuteCommand(insertNewFacCmd)
        
        setPositionCmd = "SetPosition */Facility/{} Geodetic {} {} Terrain".format(facData[0], facData[2], facData[1])
        root.ExecuteCommand(setPositionCmd)
        
        setColorCommand = "Graphics */Facility/{} SetColor blue".format(facData[0])
        root.ExecuteCommand(setColorCommand)
        
#Create sensor constellation, used later to hold all the sensor objects
sensorConst = scenario.Children.New(STKObjects.eConstellation, "SensorConst")
sensorConst2 = sensorConst.QueryInterface(STKObjects.IAgConstellation)

#Build satellite constellation, attach sensors, assign sensor to constellation object
i = 1
for RAAN in range(0,180,45):    # 4 orbit planes
    j = 1
    for trueAnomaly in range(0,360,45):  # 8 sats per plane
        
        #insert satellite
        newSat = scenario.Children.New(STKObjects.eSatellite, "Sat{}{}".format(i,j))
        newSat2 = newSat.QueryInterface(STKObjects.IAgSatellite)
        
        #change some basic display attributes
        newSat2.Graphics.Attributes.QueryInterface(STKObjects.IAgVeGfxAttributesBasic).Color = 65535
        newSat2.Graphics.Attributes.QueryInterface(STKObjects.IAgVeGfxAttributesBasic).Line.Width = STKObjects.e1
        newSat2.Graphics.Attributes.QueryInterface(STKObjects.IAgVeGfxAttributesBasic).Inherit = False
        newSat2.Graphics.Attributes.QueryInterface(STKObjects.IAgVeGfxAttributesOrbit).IsGroundTrackVisible = False
        
        
        #Buildup Initial State using TwoBody Propagator and Classical Orbital Elements
        keplarian = newSat2.Propagator.QueryInterface(STKObjects.IAgVePropagatorTwoBody).InitialState.Representation.ConvertTo(STKUtil.eOrbitStateClassical).QueryInterface(STKObjects.IAgOrbitStateClassical)
        keplarian.SizeShapeTpye = STKObjects.eSizeShapeSemimajorAxis
        keplarian.SizeShape.QueryInterface(STKObjects.IAgClassicalSizeShapeSemimajorAxis).SemiMajorAxis = 7159
        keplarian.SizeShape.QueryInterface(STKObjects.IAgClassicalSizeShapeSemimajorAxis).Eccentricity = 0
        keplarian.Orientation.Inclination = 86.4
        keplarian.Orientation.ArgOfPerigee = 0
        keplarian.Orientation.AscNodeType = STKObjects.eAscNodeRAAN
        keplarian.Orientation.AscNode.QueryInterface(STKObjects.IAgOrientationAscNodeRAAN).Value = RAAN
        keplarian.LocationType = STKObjects.eLocationTrueAnomaly
        keplarian.Location.QueryInterface(STKObjects.IAgClassicalLocationTrueAnomaly).Value = trueAnomaly + (45/2)*(i%2)  #Stagger TrueAnomalies for every other orbital plane
        newSat2.Propagator.QueryInterface(STKObjects.IAgVePropagatorTwoBody).InitialState.Representation.Assign(keplarian)
        newSat2.Propagator.QueryInterface(STKObjects.IAgVePropagatorTwoBody).Propagate()
        
        #Attach sensors to each satellite
        sensor = newSat.Children.New(STKObjects.eSensor,"Sensor{}{}".format(i,j))
        sensor2 = sensor.QueryInterface(STKObjects.IAgSensor)
        sensor2.CommonTasks.SetPatternSimpleConic(62.5, 2)
        
        #Add the sensor to the SensorConstellation
        sensorConst2.Objects.Add("Satellite/Sat{0}{1}/Sensor/Sensor{0}{1}".format(i,j))
        
        #Adjust the translucenty of the sensor projections
        sensor2.VO.PercentTranslucency = 75
        sensor2.Graphics.LineStyle = STKUtil.eDotted
        
        j+=1
    i+=1
    

    
#Create a Chain object for each Facility to the constellation.
facCount = scenario.Children.GetElements(STKObjects.eFacility).Count
for i in range(facCount):
    
    #Create Chain
    facName = scenario.Children.GetElements(STKObjects.eFacility).Item(i).InstanceName
    chain = scenario.Children.New(STKObjects.eChain, "{}ToSensorConst".format(facName))
    chain2 = chain.QueryInterface(STKObjects.IAgChain)
    
    #Modify some display properties
    chain2.Graphics.Animation.Color = 65280
    chain2.Graphics.Animation.LineWidth = STKObjects.e1
    chain2.Graphics.Animation.IsHighlightVisible = False
    
    #Add objects to the chain
    chain2.Objects.Add("Facility/{}".format(facName))
    chain2.Objects.Add("Constellation/SensorConst")
    
    
    #Get complete chain access data
    compAcc = chain.DataProviders.Item("Complete Access").QueryInterface(STKObjects.IAgDataPrvInterval).Exec(scenario2.StartTime,scenario2.StopTime)
    el = compAcc.DataSets.ElementNames
    numRows = compAcc.DataSets.RowCount
    maxOutage = []
    
    #Save out the report to a text file    
    with open("{}CompleteChainAccess.txt".format(facName),"w") as dataFile:
        dataFile.write("{},{},{},{}\n".format(el[0],el[1],el[2],el[3]))
        for row in range(numRows):
            rowData = compAcc.DataSets.GetRow(row)
            dataFile.write("{},{},{},{}\n".format(rowData[0],rowData[1],rowData[2],rowData[3]))            
            
    dataFile.close()
    
    #Get max outage time for each chain, print to console and save to file
    with open("MaxOutageData.txt", "a") as outageFile:
        if numRows == 1:
            outageFile.write("{},NA,NA,NA\n".format(facName))
            print("{}: No Outage".format(facName))
        else:
            #Get StartTimes and StopTimes as lists
            startTimes = list(compAcc.DataSets.GetDataSetByName("Start Time").GetValues())
            stopTimes = list(compAcc.DataSets.GetDataSetByName("Stop Time").GetValues())
            
            #convert to from strings to datetimes
            startDatetimes = np.array([dt.datetime.strptime(startTime[:-3], "%d %b %Y %H:%M:%S.%f") for startTime in startTimes])
            stopDatetimes = np.array([dt.datetime.strptime(stopTime[:-3], "%d %b %Y %H:%M:%S.%f") for stopTime in stopTimes])
            
            outages = startDatetimes[1:] - stopDatetimes[:-1]
            maxOutage = np.amax(outages).total_seconds()
            start = stopTimes[np.argmax(outages)]
            stop = startTimes[np.argmax(outages)+1]
            outageFile.write("{},{},{},{}\n".format(facName,maxOutage,start,stop))
            print("{}: {} seconds from {} until {}".format(facName, maxOutage, start, stop))

    
root.Rewind()
root.Save()
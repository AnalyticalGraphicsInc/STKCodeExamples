import sys
import time

try:
    from agi.stk12.stkdesktop import STKDesktop, STKDesktopApplication
    from agi.stk12.stkengine import STKEngine, STKEngineApplication
    from agi.stk12.stkobjects import *
    from agi.stk12.stkobjects.aviator import *
except:
    print("Failed to import stk modules. Make sure you have installed the STK Python API wheel \
        (agi.stk<..ver..>-py3-none-any.whl) from the STK Install bin directory.")
try:
    import numpy as np
except:
    print("**** Error: Failed to import one of the required modules (numpy). \
        Make sure you have them installed. If you are using anaconda python, make sure you are running \
             from an anaconda command prompt.")
    sys.exit(1)



#--------------------------------------------Variables---------------------------------------#

satelliteNames = ['Satellite1', 'Satellite2']
placeNames = ['Place1', 'Place2', 'Place3', 'Place4', 'Place5', 'Place6', 'Place7']
algTimeStep = 30   # sec



#----------------------------------------------Defs------------------------------------------#

stk = STKDesktop.AttachToApplication()
stkRoot = stk.Root
stkRoot.UnitPreferences.SetCurrentUnit('DateFormat', 'EpSec')
scenario = stkRoot.CurrentScenario


def getPlaces(names: list):
    # Get all place objects 
    places = []

    for placeName in names:
        placeObj = scenario.Children.Item(placeName)
        places.append(placeObj)

    return places


def getSatellites(names: list):
    # Get satellite objects and sensors on named satellites 
    sensors = []
    satellites = []

    for satelliteName in names:
        satObj = scenario.Children.Item(satelliteName)
        satellites.append(satObj)
        sensObjColl = satObj.Children.GetElements(AgESTKObjectType.eSensor)
        sensObj = sensObjColl.Item(0)
        sensors.append(sensObj)
    
    return satellites, sensors


def computeRangeSatsToPlaces(satList: list, placeNames: list):

    allRanges = []
    # Get distance vector magnitude for each sat/place combo
    for sat in satList:
        rangesPerSat = []
        for placeName in placeNames:
            elems = sat.DataProviders.GetDataPrvTimeVarFromPath('Vectors(Inertial)/' + placeName).ExecElements(scenario.StartTime, scenario.StopTime, algTimeStep, ['Time', 'Magnitude'])
            times = np.array(elems.DataSets.GetDataSetByName('Time').GetValues())
            ranges = np.array(elems.DataSets.GetDataSetByName('Magnitude').GetValues())
            rangesPerSat.append(ranges)
        allRanges.append(rangesPerSat)
    return allRanges, times


def computeAccessSatsToPlaces(satList: list, placeList: list):
    # Compute access from all sats to all places and merge time intervals so that the final intervals for pointing are the satellite has access and is closest to the facility
    allAccesses = []
    for sat in satList:
        accessPerSat = []
        for place in placeList:
            access = sat.GetAccessToObject(place)
            access.ComputeAccess()
            accessPerSat.append(access)
        allAccesses.append(accessPerSat)
    return allAccesses


def setPointingForEachSensor(satList: list, placeList: list, placeNames: list, sensList: list, ranges, times):
    # Set pointing for sensors based on minimum range at a time, but also verify the satellite has access to the place at that time
    # Compute access to cross-reference time lists
    allAccesses = computeAccessSatsToPlaces(satList, placeList)

    i = 0

    for sat in satList:
        rangesForSat = np.vstack(ranges[i])
        rangesForSat = np.transpose(rangesForSat)

        # Creat dictionary headers
        pointingTimesForPlaceForSat = {}
        for z in range(len(placeList)):
            pointingTimesForPlaceForSat[z] = []
            
        epSec = 0
        for rangeSet in rangesForSat:
            smallToLargeInd = np.argsort(rangeSet)
            for placeIndex in smallToLargeInd:
                if allAccesses[i][placeIndex].Vgt.EventIntervalLists.Item('AccessIntervals').Occurred(epSec):
                    pointingTimesForPlaceForSat[placeIndex].append(epSec)
                    break

            epSec += algTimeStep       
        
        sens = AgSensor(sensList[i])
        sens.SetPointingType(AgESnPointing.eSnPtTargeted)
        pointing = AgSnPtTargeted(sens.Pointing)
        pointing.Targets.RemoveAll()

        for y in range(len(placeNames)):
            try: (AgSatellite(sat)).Vgt.EventIntervalLists.Remove('PointingTo' + placeNames[y])
            except: pass

            intervalsForPlace = []
            newPass = True
            stepsInPass = 1
            for j in range(len(pointingTimesForPlaceForSat[y])):
                if j == (len(pointingTimesForPlaceForSat[y]) - 1):
                    if stepsInPass == 1: 
                        intervalsForPlace.append(pointingTimesForPlaceForSat[y][j])
                    intervalsForPlace.append(pointingTimesForPlaceForSat[y][j])
                elif (pointingTimesForPlaceForSat[y][j + 1] - pointingTimesForPlaceForSat[y][j] == algTimeStep):
                    if newPass:
                        intervalsForPlace.append(pointingTimesForPlaceForSat[y][j])
                        newPass = False
                    stepsInPass += 1
                else: 
                    if stepsInPass == 1: 
                        intervalsForPlace.append(pointingTimesForPlaceForSat[y][j])
                    intervalsForPlace.append(pointingTimesForPlaceForSat[y][j])
                    newPass = True
                    stepsInPass = 1
                j += 1

            intervals = (AgSatellite(sat)).Vgt.EventIntervalLists.Factory.CreateEventIntervalListFixed('PointingTo' + placeNames[y], 'Interval for when this satellite will point the sensor at ' + placeNames[y])
            
            intervals.SetIntervals(intervalsForPlace)
            intervalsInList = intervals.FindIntervals().Intervals.Count
            pointing.Targets.Add('Place/' + placeNames[y])

            if (y == 0):
                pointing.EnableAccessTimes = False
                pointing.ScheduleTimes.RemoveAll()
            
            for u in range(intervalsInList):
                start = intervals.FindIntervals().Intervals.Item(u).Start
                stop = intervals.FindIntervals().Intervals.Item(u).Stop
                # Interval will fail to add if start = stop, so make this nearly instantaneous 
                if (start == stop): stop = start + 0.000001
                pointing.ScheduleTimes.Add(start, stop, 'Place/' + placeNames[y])

        i += 1



#------------------------------------------------------Main--------------------------------------------#

places = getPlaces(placeNames)
satellites, sensors = getSatellites(satelliteNames)
ranges, times = computeRangeSatsToPlaces(satellites, placeNames)
setPointingForEachSensor(satellites, places, placeNames, sensors, ranges, times)

runtime = (time.process_time())/60
print(str(runtime) + " min")


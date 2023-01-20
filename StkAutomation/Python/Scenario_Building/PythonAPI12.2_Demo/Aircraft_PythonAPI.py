# Most backcountry skiers carry transceivers with the following specifications, but these are short range and are only useful
# if one member of the group is not buried. In this case, we are assuming all members of the party are buried, meaning the signal
# is active but is not projecting far enough to be detected by ski patrol and an aircraft is the fastest way to get there. We are
# just modeling the transmitting part of the transceiver
# Specs (https://en.wikipedia.org/wiki/Avalanche_transceiver , https://www.etsi.org/deliver/etsi_en/300700_300799/30071801/02.01.00_20/en_30071801v020100a.pdf):
# Standard Transmit freq: 457 kHz
# Digital transmitter pattern: Dipole
# Power: 250 nW

import time
import os
import sys
from typing import Union, Tuple

try:
    from agi.stk12.stkdesktop import STKDesktop, STKDesktopApplication
    from agi.stk12.stkengine import STKEngine, STKEngineApplication
    from agi.stk12.stkobjects import *
    from agi.stk12.stkobjects.aviator import *
except:
    print(
        "Failed to import stk modules. Make sure you have installed the STK Python API wheel \
        (agi.stk<..ver..>-py3-none-any.whl) from the STK Install bin directory"
    )
try:
    import matplotlib.pyplot as plt
    import numpy as np
except:
    print(
        "**** Error: Failed to import one of the required modules (matplotlib, numpy). \
        Make sure you have them installed. If you are using anaconda python, make sure you are running \
             from an anaconda command prompt."
    )
    sys.exit(1)


# ---------------------------------------------------------------------------------------------------------------#
# -------------------------------------------------Variables-----------------------------------------------------#
# ---------------------------------------------------------------------------------------------------------------#

# Scenario Name
scenarioName = "PythonAPIDemoTelluride"

# Use Cesium OSM Buildings?
useCesiumBuildings = True
accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwZjlhZmM2Ni1iZTA3LTQxY2MtYmEzMi1jNWViYTBhZmVlMjYiLCJpZCI6MzE3NzUsInNjb3BlcyI6WyJhc2wiLCJhc3IiLCJhc3ciLCJnYyJdLCJpYXQiOjE1OTU5NTUwMTJ9.eNMQjsAk0dj0jdN4q8ZnaU7LRc8DIDFywEHNC9jtYhY"

# Lat and Lon in radians, altitude in m, altitude is Height Above Ground, terrain is used otherwise
# Tour Stop One, Bridal Veil Falls
bvfName = "BridalVeilViewpoint"
bvfLat = float(np.radians(37.9257785881))
bvfLon = float(np.radians(-107.7686464079))
bvfAlt = 0
bvfList = [bvfName, bvfLat, bvfLon, bvfAlt]

# Bridal Veil Falls Navigation Waypoint
bvfWName = "BVFWaypoint"
bvfWLat = float(np.radians(37.9326316074))
bvfWLon = float(np.radians(-107.7823345730))
bvfWAlt = 0
bvfWList = [bvfWName, bvfWLat, bvfWLon, bvfWAlt]

# Tour Stop Two, Lena Basin (location of avalanche)
lenaName = "LenaBasin"
lenaLat = float(np.radians(37.8828289963))
lenaLon = float(np.radians(-107.8278063819))
lenaAlt = 0
lenaList = [lenaName, lenaLat, lenaLon, lenaAlt]

# Lena Basin Inlet
lenaIName = "LenaInlet"
lenaILat = float(np.radians(37.9328744213))
lenaILon = float(np.radians(-107.8052059544))
lenaIAlt = 0
lenaIList = [lenaIName, lenaILat, lenaILon, lenaIAlt]

# Lena Basin Navigation Waypoint
lenaWName = "LenaWaypoint"
lenaWLat = float(np.radians(37.8879160751))
lenaWLon = float(np.radians(-107.8162632349))
lenaWAlt = 0
lenaWList = [lenaWName, lenaWLat, lenaWLon, lenaWAlt]

# Location of avalanche beacon
beacName = "Beacon"
beacLat = float(np.radians(37.881004))
beacLon = float(np.radians(-107.8266268830))
beacAlt = 0
beacList = [beacName, beacLat, beacLon, beacAlt]

# Location of Ski Patrol HQ
hqName = "SkiPatrolHQ"
hqLat = float(np.radians(37.9318144485))
hqLon = float(np.radians(-107.8330337421))
hqAlt = 6.47205  # m
hqList = [hqName, hqLat, hqLon, hqAlt]

# Location of Ski Patroller
patName = "SkiPatroller"
patLat = float(np.radians(37.8854332293))
patLon = float(np.radians(-107.8258837917))
patAlt = 0
patList = [patName, patLat, patLon, patAlt]

# Create data structures for place names and their locations
namesAndPlaces = [
    bvfList,
    bvfWList,
    lenaList,
    lenaIList,
    lenaWList,
    beacList,
    hqList,
    patList,
]

# Beacon transmitter specifications
emergXmtrFreq = 0.000457  # GHz
emergXmtrAntennaType = "Dipole"
emergXmtrPower = -66.0206  # dBW

# Cessna 206 Stats
aircraftObjectName = "TellurideAirTours"
acName = "Cessna 206"
wingArea = 16.3  # m^2
flapsArea = 6.96773  # m^2
speedbrakesArea = 1.39355  # m^2
maxAlt = 15700  # ft
maxMach = 0.6
maxEAS = 156  # nm/hr
maxStaticPowerSL = 300  # hp
props = 1
propDiam = 1.524  # m
aspectRatio = 7.4
fuelFlow = 243.371  # lb/hr
levelTurnRad = 200  # ft
maxLandingWeight = 2176  # lbs
emptyWeight = 2176  # lbs
fuelCapacity = 552  # lbs
fuelInitial = 552  # lbs
terfolMinAirspeed = 120  # nm/hr
terfolMaxAirspeed = 400  # nm/hr
terfolMaxEndurAirspeed = 230  # nm/hr
terfolMaxRangeAirspeed = 330  # nm/hr
terfolMaxPerfAirspeed = 365  # nm/hr

# Takeoff
takeoffClimbAngle = 3  # rad
departAboveRW = 200  # ft
rwAltOff = 5  # ft

# To Telluride and BVF
terfolPhaseOneAlt = 600  # ft
terfolPhaseTwoAlt = 500  # ft

# Bridal Veil Falls Holding
bvfHoldingAlt = 10200  # ft
bvfHoldingBearing = 310  # deg
bvfHoldingRange = 0  # nm
bvfHoldingWidth = 0.3949892009  # nm
bvfHoldingLength = 0.3291576674  # nm
bvfHoldingTurns = 6

# To Lena Basin
terfolPhaseThreeAltGen = 500  # ft
terfolPhaseThreeAltWaypoint = 300  # ft
terfolPhaseThreeAltBasin = 200  # ft

# Lena Basin Holding
basinHoldingAlt = 13050  # ft
basinHoldingBearing = 45  # deg
basinHoldingRange = 0  # nm
basinHoldingWidth = 0.2468682505  # nm
basinHoldingLength = 0.6583153348  # nm
basinHoldingTurns = 6

# To Telluride Regional Airport
terfolPhaseFourAltLenaWay = 250  # ft
terfolPhaseFourAltLenaInlet = 300  # ft
terfolPhaseFourAltToTel = 700  # ft

# Landing
landingApproachAltitude = 549.749  # ft
landingApproachRange = 2.31683  # nm
landingGlideslope = 3  # deg
landingRWOff = 13  # ft

# Airplane Emergency Receiver
rcvrFreqAC = 0.000457  # GHz
rcvrAntennaDiamAC = 1  # m
rcvrDesFreqAC = 0.000457  # GHz

# Ski Patrol HQ Walkie
hqWalkieFreq = 0.446  # GHz
hqWalkiePower = -3.0103  # dBW
hqWalkieDesFreq = 0.446  # GHz

# Ski Patroller Walkie
patrolWalkieFreq = 0.446  # GHz
patrolWalkieDesFreq = 0.446  # GHz

# Use 3D Model?
useModel = True
path = os.getcwd()
cessnaFilePath = path + "\\cessna_206.glb"


# ---------------------------------------------------------------------------------------------------------------#
# ----------------------------------------------------Defs-------------------------------------------------------#
# ---------------------------------------------------------------------------------------------------------------#


def initializeStk(
    StkEngine: bool = False, scenarioName: str = "PythonApi", scenarioPath: str = ""
) -> Union[
    Tuple[STKEngineApplication, AgStkObjectRoot],
    Tuple[STKDesktopApplication, AgStkObjectRoot],
]:
    """Return stk application and stk root
    A method to either start STK or STK Engine
    Optionally define a scenarioName or scenarioPath
    """
    if StkEngine:
        stk = STKEngine.StartApplication(noGraphics=True)
        stkRoot = stk.NewObjectRoot()
    else:
        stk = STKDesktop.StartApplication(visible=True, userControl=True)
        stkRoot = stk.Root
    if not scenarioPath:
        if stkRoot.CurrentScenario is None:
            stkRoot.NewScenario(scenarioName)
        else:
            stkRoot.CloseScenario()
            stkRoot.NewScenario(scenarioName)
    else:
        if stkRoot.CurrentScenario is not None:
            stkRoot.CloseScenario()
        try:
            stkRoot.Load(scenarioPath)
        except:
            print(f"Unable to load scenario: {scenarioPath}")
    setMetricUnits(stkRoot)
    return stk, stkRoot


def setMetricUnits(stkRoot: AgStkObjectRoot) -> None:
    """
    Set relavent units to meters, seconds, and radians
    """
    stkRoot.UnitPreferences.SetCurrentUnit("Distance", "m")
    stkRoot.UnitPreferences.SetCurrentUnit("Time", "sec")
    stkRoot.UnitPreferences.SetCurrentUnit("Angle", "rad")
    stkRoot.UnitPreferences.SetCurrentUnit("Latitude", "rad")
    stkRoot.UnitPreferences.SetCurrentUnit("Longitude", "rad")
    stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec")
    stkRoot.UnitPreferences.SetCurrentUnit("Duration", "sec")


def cesiumOSMBuildings(root, accessToken):
    cmd = f'VO * 3DTiles AddTileset ION "https://api.cesium.com" ASSETID 96188 ACCESSTOKEN {accessToken}'
    root.ExecuteCommand(cmd)


def insertPlaces(namesAndPlaces):
    stkObjectsPlaces = []
    i = 0
    for info in namesAndPlaces:
        name = info[0]
        lat = info[1]
        lon = info[2]
        hog = info[3]
        temp = []
        temp.append(name)
        temp.append(AgPlace(scenario.Children.New(AgESTKObjectType.ePlace, name)))
        temp[1].Position.AssignGeodetic(lat, lon, 0)
        temp[1].HeightAboveGround = hog
        i = i + 1

        stkObjectsPlaces.append(temp)

    return stkObjectsPlaces


# ---------------------------------------------------------------------------------------------------------------#
# ---------------------------------------------------Main--------------------------------------------------------#
# ---------------------------------------------------------------------------------------------------------------#
# Aircraft receives and must transfer to satellite due to mountainous terrain
# Only 15 minutes to find someone in an avalanche - plane must be in area (tourism)
# Python Variable iteration: time of transmitter/avalanche vs chance of mission failure (15 minutes to being dug out = 93%)
# https://utahavalanchecenter.org/education/faq#:~:text=Statistics%20show%20that%2093%20percent,don't%20have%20much%20time.
# AzEl Mask

# Grab Root and set units
stk, stkRoot = initializeStk(False, scenarioName)
scenario = stkRoot.CurrentScenario

# Set Terrain server for Analysis/AzEl
stkRoot.ExecuteCommand("TerrainServer / UseAgiServer Yes")
stkRoot.ExecuteCommand("Terrain * TerrainServer AzElMaskEnabled Yes")

# Declutter labels since terrain is mountainous
declutCmd = "VO * Declutter Enable On"
stkRoot.ExecuteCommand(declutCmd)

# Insert Cesium OSM Buildings if enabled
if useCesiumBuildings:
    cesiumOSMBuildings(stkRoot, accessToken)

# Insert Aviator aircraft so catalog is accessible
ac = AgAircraft(scenario.Children.New(AgESTKObjectType.eAircraft, aircraftObjectName))
ac.SetRouteType(AgEVePropagatorType.ePropagatorAviator)
avtr = AgAvtrPropagator(AgVePropagatorAviator(ac.Route).AvtrPropagator)

# Access Aviator catalog
catalog = avtr.AvtrCatalog
runways = catalog.RunwayCategory.ARINC424Runways

# Use ARINC424 runway file from STK install
runways.MasterDataFilepath = (
    "C:\\Program Files\\AGI\\STK 12\\Data\\Resources\\stktraining\\samples\\FAANFD18"
)

# Select Telluride Regional
telRgnlAvtr = runways.GetARINC424Item("TELLURIDE RGNL 09 27")

# Obtain Site Information for use in creating Place object
telRgnlVals = telRgnlAvtr.GetAllFieldsAndValues()

# Latitude is index 4, Longitude is index 5, altitude is index 6 (degrees and feet, respectively)
telRgnlLat = float(np.radians(float(telRgnlVals[4].replace("Latitude: ", ""))))
telRgnlLon = float(np.radians(float(telRgnlVals[5].replace("Longitude: ", ""))))
telRgnlAlt = (
    float(telRgnlVals[6].replace("Altitude: ", "")) / 3.28084
)  # Convert FT to meters

# Create Telluride Regional as a Place Object - not included in namesAndPlaces due to special origin
telRgnl = AgPlace(
    scenario.Children.New(AgESTKObjectType.ePlace, "TellurideRegionalAirport")
)
telRgnl.AltRef = AgEAltRefType.eWGS84
telRgnl.Position.AssignGeodetic(telRgnlLat, telRgnlLon, telRgnlAlt)

# Grab the town of Telluride from City Database - not included in namesAndPlaces due to special origin
telTownCmd = 'ImportFromDB * City "C:\\ProgramData\\AGI\\STK 12\\Databases\\City\\stkCityDb.cd" Class Place CityName Telluride'
stkRoot.ExecuteCommand(telTownCmd)

# Insert Waypoints and Places
namesAndObjects = insertPlaces(namesAndPlaces)

# Insert Transmitter on Beacon and set properties
beaconXmtr = AgTransmitter(
    (namesAndObjects[5])[1].Children.New(AgESTKObjectType.eTransmitter, "BeaconXmtr")
)
beaconXmtr.SetModel("Complex Transmitter Model")
complexBeacXmtr = AgTransmitterModelComplex(beaconXmtr.Model)
complexBeacXmtr.Frequency = emergXmtrFreq
complexBeacXmtr.Power = emergXmtrPower
beacAntenna = complexBeacXmtr.AntennaControl
beacAntenna.SetEmbeddedModel(emergXmtrAntennaType)

# Get Aviator Pieces
avtrMission = avtr.AvtrMission
phases = avtrMission.Phases

# Delete/Create Cessna 206 Model
acCat = catalog.AircraftCategory
if acCat.AircraftModels.Contains(acName) > 0:
    acCat.AircraftModels.RemoveChild(acName)
cessna = acCat.AircraftModels.AddAircraft(acName)
afwt = cessna.AdvFixedWingTool
afwt.WingArea = wingArea
afwt.MaxMach = maxMach
afwt.MaxEAS = maxEAS
afwt.FlapsArea = flapsArea
afwt.SpeedbrakesArea = speedbrakesArea
afwt.AeroModeAsSubsonic.GeometryModeAsBasic.SetAspectRatio(aspectRatio)
afwt.MaxAltitude = maxAlt
afwt.PowerplantStrategy = AgEAvtrAdvFixedWingPowerplantStrategy.eTurboprop
tProp = afwt.PowerplantModeAsTurboprop
tProp.MaxSeaLevelStaticPower = maxStaticPowerSL
tProp.PropellerCount = props
tProp.PropellerDiameter = propDiam
tProp.FuelFlow = fuelFlow
cessna.GetAsCatalogItem().Save()

afwt.CreateAllPerfModels(acName, True, True)
cessna.GetAsCatalogItem().Save()

# Set Acceleration Properties
accel = cessna.Acceleration.GetAdvAccelerationByName(acName)
accel.LevelTurns.SetLevelTurn(AgEAvtrTurnMode.eTurnModeRadius, levelTurnRad)
cessna.GetAsCatalogItem().Save()

# Set Default Configuration
defaultCon = cessna.DefaultConfiguration
defaultCon.EmptyWeight = emptyWeight
defaultCon.MaxLandingWeight = maxLandingWeight
fuelTank = defaultCon.GetStations().AddInternalFuelTank()
fuelTank.InitialFuelState = fuelInitial
fuelTank.Capacity = fuelCapacity
cessna.GetAsCatalogItem().Save()

# Add and Set Terrain Following Profile
terfol = AgAvtrAircraftTerrainFollowModel(
    cessna.TerrainFollow.GetAsCatalogItem().AddDefaultChild("Cessna 206")
)
terfol.UseAeroPropFuel = True
terfol.MinAirspeed = terfolMinAirspeed
terfol.MaxEnduranceAirspeed = terfolMaxEndurAirspeed
terfol.MaxRangeAirspeed = terfolMaxRangeAirspeed
terfol.MaxPerfAirspeed = terfolMaxPerfAirspeed
terfol.MaxAirspeed = terfolMaxAirspeed
cessna.GetAsCatalogItem().Save()

# Set Cessna as the vehicle to be used for the route
avtrMission.Vehicle = cessna

# Set 3d model of aircraft if enabled
if useModel:
    AgVOModelFile(ac.VO.Model.ModelData).Filename = cessnaFilePath
    modelCmd = f"VO */Aircraft/{aircraftObjectName} Model Use ModelFile"
    stkRoot.ExecuteCommand(modelCmd)

# Create Aircraft Route
# Takeoff
takeoff2Tel = phases[0]
takeoff2Tel.SetDefaultPerfModels()
takeoff2Tel.Name = "Takeoff to Telluride"
# Use Terrain Follow Performance Model
terfolPhaseOne = takeoff2Tel.GetPerformanceModelByType("TerrainFollow")
terfolPhaseOne.LinkToCatalog("Cessna 206")
takeoffProcs = takeoff2Tel.Procedures
takeoff = AgAvtrProcedureTakeoff(
    takeoffProcs.Add(
        AgEAvtrSiteType.eSiteRunwayFromCatalog, AgEAvtrProcedureType.eProcTakeoff
    )
)
AgAvtrSiteRunwayFromCatalog(takeoff.Site).SetCatalogRunway(telRgnlAvtr)
takeoff.RunwayHeadingOptions.RunwayMode = AgEAvtrRunwayHighLowEnd.eLowEnd
takeoffNorm = takeoff.ModeAsNormal
takeoffNorm.TakeoffClimbAngle = takeoffClimbAngle
takeoffNorm.DepartureAltitude = departAboveRW
takeoffNorm.RunwayAltitudeOffset = rwAltOff
takeoffNorm.UseRunwayTerrain = True

terfolToTel = AgAvtrProcedureTerrainFollow(
    takeoffProcs.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToTel.Site).ObjectName = "Place/Telluride"
terfolToTel.Site.Name = "To Telluride"
terfolToTel.AltitudeAGL = terfolPhaseOneAlt

# Fly to Bridal Veil Falls
toBVF = phases.Add()
toBVF.SetDefaultPerfModels()
toBVF.Name = "Bridal Veil Falls"
terfolPhaseTwo = toBVF.GetPerformanceModelByType("TerrainFollow")
terfolPhaseTwo.LinkToCatalog("Cessna 206")

terfolToBVFWay = AgAvtrProcedureTerrainFollow(
    toBVF.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToBVFWay.Site).ObjectName = "Place/BVFWaypoint"
terfolToBVFWay.Site.Name = "To BVF Waypoint"
terfolToBVFWay.AltitudeAGL = terfolPhaseTwoAlt

terfolToBVFView = AgAvtrProcedureTerrainFollow(
    toBVF.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToBVFView.Site).ObjectName = "Place/BridalVeilViewpoint"
terfolToBVFView.Site.Name = "To BVF Viewpoint"
terfolToBVFView.AltitudeAGL = terfolPhaseTwoAlt

bvfHolding = AgAvtrProcedureHoldingRacetrack(
    toBVF.Procedures.Add(
        AgEAvtrSiteType.eSiteEndOfPrevProcedure,
        AgEAvtrProcedureType.eProcHoldingRacetrack,
    )
)
bvfHolding.Site.Name = "BVF Tour Point"
bvfHolding.AltitudeOptions.UseDefaultCruiseAltitude = False
bvfHolding.AltitudeOptions.MSLAltitude = bvfHoldingAlt
bvfHolding.ProfileMode = AgEAvtrHoldingProfileMode.eClimbDescentOnStation
bvfHolding.Bearing = bvfHoldingBearing
bvfHolding.Range = bvfHoldingRange
bvfHolding.Width = bvfHoldingWidth
bvfHolding.Length = bvfHoldingLength
bvfHolding.Turns = bvfHoldingTurns
bvfHolding.HoldCruiseAirspeedOptions.CruiseSpeedType = (
    AgEAvtrCruiseSpeed.eMaxRangeAirspeed
)
turnDirCmd = (
    f"MissionModeler */Aircraft/{aircraftObjectName} Procedure 5 SetValue Direction 1"
)
stkRoot.ExecuteCommand(turnDirCmd)

# Fly to Lena Basin
toLena = phases.Add()
toLena.SetDefaultPerfModels()
toLena.Name = "Lena Basin"
terfolPhaseThree = toLena.GetPerformanceModelByType("TerrainFollow")
terfolPhaseThree.LinkToCatalog("Cessna 206")

terfolToBVFWayExit = AgAvtrProcedureTerrainFollow(
    toLena.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToBVFWayExit.Site).ObjectName = "Place/BVFWaypoint"
terfolToBVFWayExit.Site.Name = "To BVF Waypoint - Exit"
terfolToBVFWayExit.AltitudeAGL = terfolPhaseThreeAltGen

terfolToLenaInlet = AgAvtrProcedureTerrainFollow(
    toLena.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToLenaInlet.Site).ObjectName = "Place/LenaInlet"
terfolToLenaInlet.Site.Name = "To Lena Inlet"
terfolToLenaInlet.AltitudeAGL = terfolPhaseThreeAltGen

terfolToLenaWay = AgAvtrProcedureTerrainFollow(
    toLena.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToLenaWay.Site).ObjectName = "Place/LenaWaypoint"
terfolToLenaWay.Site.Name = "To Lena Waypoint"
terfolToLenaWay.AltitudeAGL = terfolPhaseThreeAltWaypoint

terfolToLenaBasin = AgAvtrProcedureTerrainFollow(
    toLena.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToLenaBasin.Site).ObjectName = "Place/LenaBasin"
terfolToLenaBasin.Site.Name = "To Lena Basin"
terfolToLenaBasin.AltitudeAGL = terfolPhaseThreeAltBasin

basinHolding = AgAvtrProcedureHoldingRacetrack(
    toLena.Procedures.Add(
        AgEAvtrSiteType.eSiteEndOfPrevProcedure,
        AgEAvtrProcedureType.eProcHoldingRacetrack,
    )
)
basinHolding.Site.Name = "Lena Tour Point"
basinHolding.AltitudeOptions.UseDefaultCruiseAltitude = False
basinHolding.AltitudeOptions.MSLAltitude = basinHoldingAlt
basinHolding.ProfileMode = AgEAvtrHoldingProfileMode.eClimbDescentOnStation
basinHolding.Bearing = basinHoldingBearing
basinHolding.Range = basinHoldingRange
basinHolding.Width = basinHoldingWidth
basinHolding.Length = basinHoldingLength
basinHolding.Turns = basinHoldingTurns
basinHolding.HoldCruiseAirspeedOptions.CruiseSpeedType = (
    AgEAvtrCruiseSpeed.eMaxRangeAirspeed
)
turnDirCmd = (
    f"MissionModeler */Aircraft/{aircraftObjectName} Procedure 10 SetValue Direction 0"
)
stkRoot.ExecuteCommand(turnDirCmd)

# Return to Telluride Rgnl
toTelRgnl = phases.Add()
toTelRgnl.SetDefaultPerfModels()
toTelRgnl.Name = "Return to Telluride Rgnl"
terfolPhaseFour = toTelRgnl.GetPerformanceModelByType("TerrainFollow")
terfolPhaseFour.LinkToCatalog("Cessna 206")

terfolToLenaWayExit = AgAvtrProcedureTerrainFollow(
    toTelRgnl.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToLenaWayExit.Site).ObjectName = "Place/LenaWaypoint"
terfolToLenaWayExit.Site.Name = "To Lena Waypoint - Exit"
terfolToLenaWayExit.AltitudeAGL = terfolPhaseFourAltLenaWay

terfolToLenaInletExit = AgAvtrProcedureTerrainFollow(
    toTelRgnl.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToLenaInletExit.Site).ObjectName = "Place/LenaInlet"
terfolToLenaInletExit.Site.Name = "To Lena Inlet - Exit"
terfolToLenaInletExit.AltitudeAGL = terfolPhaseFourAltLenaInlet

terfolToTelExit = AgAvtrProcedureTerrainFollow(
    toTelRgnl.Procedures.Add(
        AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcTerrainFollowing
    )
)
AgAvtrSiteSTKStaticObject(terfolToTelExit.Site).ObjectName = "Place/Telluride"
terfolToTelExit.Site.Name = "To Telluride - Exit"
terfolToTelExit.AltitudeAGL = terfolPhaseFourAltToTel

landing = AgAvtrProcedureLanding(
    toTelRgnl.Procedures.Add(
        AgEAvtrSiteType.eSiteRunwayFromCatalog, AgEAvtrProcedureType.eProcLanding
    )
)
AgAvtrSiteRunwayFromCatalog(landing.Site).SetCatalogRunway(telRgnlAvtr)
landing.ApproachMode = AgEAvtrApproachMode.eStandardInstrumentApproach
landing.RunwayHeadingOptions.RunwayMode = AgEAvtrRunwayHighLowEnd.eHighEnd
landingSIA = landing.ModeAsStandardInstrumentApproach
landingSIA.ApproachAltitude = landingApproachAltitude
landingSIA.ApproachFixRange = landingApproachRange
landingSIA.Glideslope = landingGlideslope
landingSIA.RunwayAltitudeOffset = landingRWOff
landing.EnrouteCruiseAirspeedOptions.CruiseSpeedType = (
    AgEAvtrCruiseSpeed.eMaxRangeAirspeed
)

avtr.Propagate()

# Add Aircraft Receiver
emergencyRcvr = AgReceiver(ac.Children.New(AgESTKObjectType.eReceiver, "EmergencyRcvr"))
emergencyRcvr.SetModel("Complex Receiver Model")
complexEmergencyRcvr = AgReceiverModelComplex(emergencyRcvr.Model)
complexEmergencyRcvr.AutoTrackFrequency = False
complexEmergencyRcvr.Frequency = rcvrFreqAC
complexEmergencyRcvr.AntennaControl.SetEmbeddedModel("Gaussian")
rcvrAntennaAC = AgAntennaModelGaussian(
    complexEmergencyRcvr.AntennaControl.EmbeddedModel
)
rcvrAntennaAC.DesignFrequency = rcvrDesFreqAC
rcvrAntennaAC.Diameter = rcvrAntennaDiamAC

# Add Aircraft Transmitter
emergencyXmtr = AgTransmitter(
    ac.Children.New(AgESTKObjectType.eTransmitter, "EmergencyXmtr")
)

# Add Ski Patrol HQ Walkie
hqWalkie = AgTransmitter(
    namesAndObjects[6][1].Children.New(AgESTKObjectType.eTransmitter, "WalkieHQ")
)
hqWalkie.SetModel("Complex Transmitter Model")
complexXmtrWalkie = AgTransmitterModelComplex(hqWalkie.Model)
complexXmtrWalkie.Frequency = hqWalkieFreq
complexXmtrWalkie.Power = hqWalkiePower
complexXmtrWalkie.AntennaControl.SetEmbeddedModel("Pencil Beam")
xmtrAntennaWalkie = AgAntennaModelPencilBeam(
    complexXmtrWalkie.AntennaControl.EmbeddedModel
)
xmtrAntennaWalkie.DesignFrequency = hqWalkieDesFreq

# Add Ski Patroller Walkie
patrolWalkie = AgReceiver(
    namesAndObjects[7][1].Children.New(AgESTKObjectType.eReceiver, "WalkiePatrol")
)
patrolWalkie.SetModel("Complex Receiver Model")
complexRcvrWalkie = AgReceiverModelComplex(patrolWalkie.Model)
complexRcvrWalkie.AutoTrackFrequency = False
complexRcvrWalkie.Frequency = patrolWalkieFreq
complexRcvrWalkie.AntennaControl.SetEmbeddedModel("Pencil Beam")
rcvrAntennaWalkie = AgAntennaModelPencilBeam(
    complexRcvrWalkie.AntennaControl.EmbeddedModel
)
rcvrAntennaWalkie.DesignFrequency = patrolWalkieDesFreq

# Add Az-El Mask to Ski Patrol HQ
AgPlace(namesAndObjects[6][1]).SetAzElMask(AgEAzElMaskType.eTerrainData, 0)
AgPlace(namesAndObjects[6][1]).AccessConstraints.AddConstraint(
    AgEAccessConstraints.eCstrAzElMask
)

# Add Az-El Mask to Ski Patroller
AgPlace(namesAndObjects[7][1]).SetAzElMask(AgEAzElMaskType.eTerrainData, 0)
AgPlace(namesAndObjects[7][1]).AccessConstraints.AddConstraint(
    AgEAccessConstraints.eCstrAzElMask
)

# Compute access between walkies
walkieAccess = hqWalkie.GetAccessToObject(patrolWalkie)
walkieAccess.ComputeAccess()

runtime = (time.process_time()) / 60
print(str(runtime) + " min")

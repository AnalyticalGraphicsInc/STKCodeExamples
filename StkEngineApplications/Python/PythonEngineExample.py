import os
import platform
import time

from agi.stk12.stkobjects import *
from agi.stk12.stkutil import *

startTime = time.time()

"""
SET TO TRUE TO USE ENGINE, FALSE TO USE GUI
"""
if platform.system() == "Linux":
    # Only STK Engine is available on Linux
    useStkEngine = True
else:
    # Change to true to run engine on Windows
    useStkEngine = False
############################################################################
# Scenario Setup
############################################################################

if useStkEngine:
    from agi.stk12.stkengine import STKEngine

    # Launch STK Engine with NoGraphics mode
    print("Launching STK Engine...")
    stk = STKEngine.StartApplication(noGraphics=True)

    # Create root object
    stkRoot = stk.NewObjectRoot()

else:
    from agi.stk12.stkdesktop import STKDesktop

    # Launch GUI
    print("Launching STK...")
    stk = STKDesktop.StartApplication(visible=True, userControl=True)

    # Get root object
    stkRoot = stk.Root

# Set date format
stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")

# Create new scenario
print("Creating scenario...")
stkRoot.NewScenario("PythonEngineExample")
scenario = stkRoot.CurrentScenario

# Set time period
scenario.SetTimePeriod("1 Aug 2020 16:00:00", "2 Aug 2020 16:00:00")
if useStkEngine == False:
    # Graphics calls are not available when running STK Engine in NoGraphics mode
    stkRoot.Rewind()

totalTime = time.time() - startTime
splitTime = time.time()
print(
    "--- Scenario creation: {a:4.3f} sec\t\tTotal time: {b:4.3f} sec ---".format(
        a=totalTime, b=totalTime
    )
)

############################################################################
# Simple Access
############################################################################

# Create satellite
satellite = scenario.Children.New(AgESTKObjectType.eSatellite, "MySatellite")

# Get propagator
satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody)
propagator = satellite.Propagator

# Get orbit state
orbitState = propagator.InitialState.Representation
orbitStateClassical = orbitState.ConvertTo(AgEOrbitStateType.eOrbitStateClassical)

# Set SMA and eccentricity
orbitStateClassical.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis
sizeShape = orbitStateClassical.SizeShape
sizeShape.Eccentricity = 0
sizeShape.SemiMajorAxis = 8000

# Set inclination and argument of perigee
orientation = orbitStateClassical.Orientation
orientation.Inclination = 25
orientation.ArgOfPerigee = 0

# Set RAAN
orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN
raan = orientation.AscNode
raan.Value = 0

# Set true anomaly
orbitStateClassical.LocationType = AgEClassicalLocation.eLocationTrueAnomaly
trueAnomaly = orbitStateClassical.Location
trueAnomaly.Value = 0

# Assign orbit state and propagate satellite
orbitState.Assign(orbitStateClassical)
propagator.Propagate()

# Create faciliy
facility = scenario.Children.New(AgESTKObjectType.eFacility, "MyFacility")

# Set position
facility.Position.AssignGeodetic(28.62, -80.62, 0.03)

# Compute access between satellite and facility
print("\nComputing access...")
access = satellite.GetAccessToObject(facility)
access.ComputeAccess()

# Get access interval data
stkRoot.UnitPreferences.SetCurrentUnit("Time", "Min")
accessDataProvider = access.DataProviders.GetDataPrvIntervalFromPath("Access Data")
elements = ["Start Time", "Stop Time", "Duration"]
accessResults = accessDataProvider.ExecElements(
    scenario.StartTime, scenario.StopTime, elements
)

startTimes = accessResults.DataSets.GetDataSetByName("Start Time").GetValues()
stopTimes = accessResults.DataSets.GetDataSetByName("Stop Time").GetValues()
durations = accessResults.DataSets.GetDataSetByName("Duration").GetValues()

# Print data to console
print("\nAccess Intervals")
print(
    "{a:<29s}  {b:<29s}  {c:<14s}".format(
        a="Start Time", b="Stop Time", c="Duration (min)"
    )
)
for i in range(len(startTimes)):
    print(
        "{a:<29s}  {b:<29s}  {c:<4.2f}".format(
            a=startTimes[i], b=stopTimes[i], c=durations[i]
        )
    )

print("\nThe maximum access duration is {a:4.2f} minutes.".format(a=max(durations)))

# Print computation time
totalTime = time.time() - startTime
sectionTime = time.time() - splitTime
splitTime = time.time()
print(
    "--- Access computation: {a:4.3f} sec\t\tTotal time: {b:4.3f} sec ---".format(
        a=sectionTime, b=totalTime
    )
)

############################################################################
# Constellations and Chains
############################################################################

# Remove initial satellite
satellite.Unload()

# Create constellation object
constellation = scenario.Children.New(
    AgESTKObjectType.eConstellation, "SatConstellation"
)

# Insert the constellation of Satellites
numOrbitPlanes = 4
numSatsPerPlane = 8

stkRoot.BeginUpdate()
for orbitPlaneNum, RAAN in enumerate(
    range(0, 180, 180 // numOrbitPlanes), 1
):  # RAAN in degrees

    for satNum, trueAnomaly in enumerate(
        range(0, 360, 360 // numSatsPerPlane), 1
    ):  # trueAnomaly in degrees

        # Insert satellite
        satellite = scenario.Children.New(
            AgESTKObjectType.eSatellite, f"Sat{orbitPlaneNum}{satNum}"
        )

        # Select Propagator
        satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody)

        # Set initial state
        twoBodyPropagator = satellite.Propagator
        keplarian = twoBodyPropagator.InitialState.Representation.ConvertTo(
            AgEOrbitStateType.eOrbitStateClassical.eOrbitStateClassical
        )

        keplarian.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis
        keplarian.SizeShape.SemiMajorAxis = 8200  # km
        keplarian.SizeShape.Eccentricity = 0

        keplarian.Orientation.Inclination = 60  # degrees
        keplarian.Orientation.ArgOfPerigee = 0  # degrees
        keplarian.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN
        keplarian.Orientation.AscNode.Value = RAAN  # degrees

        keplarian.LocationType = AgEClassicalLocation.eLocationTrueAnomaly
        keplarian.Location.Value = trueAnomaly + (360 // numSatsPerPlane / 2) * (
            orbitPlaneNum % 2
        )  # Stagger true anomalies (degrees) for every other orbital plane

        # Propagate
        satellite.Propagator.InitialState.Representation.Assign(keplarian)
        satellite.Propagator.Propagate()

        # Add to constellation object
        constellation.Objects.AddObject(satellite)

stkRoot.EndUpdate()
# Create chain
chain = scenario.Children.New(AgESTKObjectType.eChain, "Chain")

# Add satellite constellation and facility
chain.Objects.AddObject(constellation)
chain.Objects.AddObject(facility)

# Compute chain
chain.ComputeAccess()

# Find satellite with most access time
chainDataProvider = chain.DataProviders.GetDataPrvIntervalFromPath("Object Access")
chainResults = chainDataProvider.Exec(scenario.StartTime, scenario.StopTime)

objectList = []
durationList = []

# Loop through all satellite access intervals
for intervalNum in range(chainResults.Intervals.Count - 1):

    # Get interval
    interval = chainResults.Intervals[intervalNum]

    # Get data for interval
    objectName = interval.DataSets.GetDataSetByName("Strand Name").GetValues()[0]
    durations = interval.DataSets.GetDataSetByName("Duration").GetValues()

    # Add data to list
    objectList.append(objectName)
    durationList.append(sum(durations))

# Find object with longest total duration
index = durationList.index(max(durationList))
print(
    "\n{a:s} has the longest total duration: {b:4.2f} minutes.".format(
        a=objectList[index], b=durationList[index]
    )
)

# Print computation time
totalTime = time.time() - startTime
sectionTime = time.time() - splitTime
splitTime = time.time()
print(
    "--- Chain computation: {a:4.2f} sec\t\tTotal time: {b:4.2f} sec ---".format(
        a=sectionTime, b=totalTime
    )
)

############################################################################
# Coverage
############################################################################

# Create coverage definition
coverageDefinition = scenario.Children.New(
    AgESTKObjectType.eCoverageDefinition, "CoverageDefinition"
)

# Set grid bounds type
grid = coverageDefinition.Grid
grid.BoundsType = AgECvBounds.eBoundsCustomRegions

# Add US shapefile to bounds
bounds = coverageDefinition.Grid.Bounds

if platform.system() == "Linux":
    install_path = os.getenv("STK_INSTALL_DIR")
else:
    import winreg

    registry = winreg.ConnectRegistry(None, winreg.HKEY_LOCAL_MACHINE)
    key = winreg.OpenKey(registry, r"Software\AGI\STK\12.0")
    install_path = winreg.QueryValueEx(key, "InstallHome")

bounds.RegionFiles.Add(
    os.path.join(
        install_path[0],
        r"Data/Shapefiles/Countries/United_States_of_America\United_States_of_America.shp",
    )
)

# Set resolution
grid.ResolutionType = AgECvResolution.eResolutionDistance
resolution = grid.Resolution
resolution.Distance = 75

# Add constellation as asset
coverageDefinition.AssetList.Add("Constellation/SatConstellation")
coverageDefinition.ComputeAccesses()

# Create figure of merit
figureOfMerit = coverageDefinition.Children.New(
    AgESTKObjectType.eFigureOfMerit, "FigureOfMerit"
)

# Set the definition and compute type
figureOfMerit.SetDefinitionType(AgEFmDefinitionType.eFmAccessDuration)
definition = figureOfMerit.Definition
definition.SetComputeType(AgEFmCompute.eAverage)

fomDataProvider = figureOfMerit.DataProviders.GetDataPrvFixedFromPath("Overall Value")
fomResults = fomDataProvider.Exec()

minAccessDuration = fomResults.DataSets.GetDataSetByName("Minimum").GetValues()[0]
maxAccessDuration = fomResults.DataSets.GetDataSetByName("Maximum").GetValues()[0]
avgAccessDuration = fomResults.DataSets.GetDataSetByName("Average").GetValues()[0]

# Computation time
totalTime = time.time() - startTime
sectionTime = time.time() - splitTime

# Print data to console
print("\nThe minimum coverage duration is {a:4.2f} min.".format(a=minAccessDuration))
print("The maximum coverage duration is {a:4.2f} min.".format(a=maxAccessDuration))
print("The average coverage duration is {a:4.2f} min.".format(a=avgAccessDuration))
print(
    "--- Coverage computation: {a:0.3f} sec\t\tTotal time: {b:0.3f} sec ---".format(
        a=sectionTime, b=totalTime
    )
)

stkRoot.CloseScenario()
stk.ShutDown()

print("\nClosed STK successfully.")

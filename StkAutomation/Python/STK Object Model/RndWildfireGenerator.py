# Max Housner (mhousner@agi.com)
# Random Global Wildfire Generation Script
# June 25, 2020

# GLOBAL VARIABLES ------------------------------------------------------
EXISTING_SCENARIO = 0           # 1 if scenario already open 0 for new scenario
NUM_WILDFIRES_ATLEAST = 3       # At least this many wildfires will be generated
NUM_WILDFIRES_GENERATED = 10    # Generate this many wildfires before computing access
GRID_RESOLUTION = 1             # Coverage Grid resolution in degrees
WILDFIRE_SIZE = 1000            # Radius of wildfire EOIR sphere (meters)
WILDFIRE_TEMP = 1000            # EOIR Temperature of wildfire (K)
# -----------------------------------------------------------------------

from comtypes.client import CreateObject
from comtypes.client import GetActiveObject
from comtypes.gen import STKUtil
from comtypes.gen import STKObjects

# Existing/New scenario
if (EXISTING_SCENARIO == 0):
    uiApplication = CreateObject("STK12.Application")
    root = uiApplication.Personality2
    root.NewScenario("Hawtboyz_RndWildfire")
else:
    uiApplication = GetActiveObject("STK12.Application")
    root = uiApplication.Personality2

uiApplication.Visible = True
uiApplication.UserControl = True
scenario = root.CurrentScenario
scenario = scenario.QueryInterface(STKObjects.IAgScenario)

# Create a coverage object over all land
coverage = root.CurrentScenario.Children.New(7, 'WorldLand_Cov')
coverage = coverage.QueryInterface(STKObjects.IAgCoverageDefinition)

# Define the bounds using the world land coverage shapefile
coverage.Grid.BoundsType = 0
bounds = coverage.Grid.Bounds
bounds = bounds.QueryInterface(STKObjects.IAgCvBoundsCustomRegions)
bounds.RegionFiles.Add(r'C:\Program Files\AGI\STK 12\Data\Shapefiles\Land\WorldLand_Cov.shp')

# Define the Grid Resolution
coverage.Grid.ResolutionType = 2
res = coverage.Grid.Resolution
res = res.QueryInterface(STKObjects.IAgCvResolutionLatLon)
res.LatLon = GRID_RESOLUTION

# Set advanced settings
coverage.Advanced.AutoRecompute = False

# Randomly generate wildfires using Python's random library
import random
j = 0

while coverage.AssetList.Count < NUM_WILDFIRES_ATLEAST:
    for i in range(j, j + NUM_WILDFIRES_GENERATED):
        j += 1

        # Create the Aircraft object and set EOIR settings
        aircraft = root.CurrentScenario.Children.New(1, 'Wildfire' + str(i))
        aircraft = aircraft.QueryInterface(STKObjects.IAgAircraft)
        root.ExecuteCommand('EOIR */Aircraft/Wildfire' + str(i) + ' Shape Type Sphere ' + str(WILDFIRE_SIZE) + ' BodyTemperature ' + str(WILDFIRE_TEMP))
        root.ExecuteCommand('Graphics */Aircraft/Wildfire' + str(i) + ' SetColor 6')

        # Set random Lat Lon points and generate
        route = aircraft.Route
        route = route.QueryInterface(STKObjects.IAgVePropagatorGreatArc)
        route.Method = 2
        route.SetAltitudeRefType(1)
    
        randLat = (random.random() - 0.46154) * 130
        randLon = random.random() * 360
    
        waypoint = route.Waypoints.Add()
        waypoint.Latitude = randLat
        waypoint.Longitude = randLon
        waypoint.Altitude = 0.2
        waypoint.Time = scenario.StartTime

        waypoint2 = route.Waypoints.Add()
        waypoint2.Latitude = randLat
        waypoint2.Longitude = randLon
        waypoint2.Altitude = 0.2
        waypoint2.Time = scenario.StopTime

        route.Propagate()

        # Add airplane to the coverage AssetList
        coverage.AssetList.Add('Aircraft/Wildfire' + str(i))

    # Compute Accesses
    coverage.ComputeAccesses()

    # Generate Cov By Asset Report
    covObj = coverage.QueryInterface(STKObjects.IAgStkObject)
    covByAsset = covObj.DataProviders.Item('Coverage By Asset')
    covByAsset = covByAsset.QueryInterface(STKObjects.IAgDataPrvFixed)
    covData = covByAsset.Exec()

    # Get the Asset names and minimum % coverage
    assetNames = covData.DataSets.GetDataSetByName('Asset Name').GetValues()
    minPercentCov = covData.DataSets.GetDataSetByName('Minimum % Coverage').GetValues()

    # Remove objects that are not above land
    for c in range (0, len(assetNames)):
        if (minPercentCov[c] == 0.0):
            coverage.AssetList.Remove('Aircraft/' + assetNames[c])
            root.CurrentScenario.Children.Unload(1, assetNames[c])

# Remove the coverage object
root.CurrentScenario.Children.Unload(7, 'WorldLand_Cov')
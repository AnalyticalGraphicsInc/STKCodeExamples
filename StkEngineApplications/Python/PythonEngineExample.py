import time
startTime = time.time()
from comtypes.client import CreateObject
from comtypes.gen import STKObjects, STKUtil

"""
SET TO TRUE TO USE ENGINE, FALSE TO USE GUI
"""
useStkEngine = True

############################################################################
# Scenario Setup
############################################################################

if (useStkEngine):
    # Launch STK Engine
    print("Launching STK Engine...")
    stkxApp = CreateObject("STKX12.Application")
    
    # Disable graphics. The NoGraphics property must be set to true before the root object is created.
    stkxApp.NoGraphics = True
    
    # Create root object
    stkRoot = CreateObject('AgStkObjects12.AgStkObjectRoot')

else:
    # Launch GUI
    print("Launching STK...")
    uiApp = CreateObject("STK12.Application")
    uiApp.Visible = True
    uiApp.UserControl = True
    
    # Get root object
    stkRoot = uiApp.Personality2

# Set date format  
stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")

# Create new scenario
print("Creating scenario...")
stkRoot.NewScenario('PythonEngineExample')
scenario = stkRoot.CurrentScenario
scenario2 = scenario.QueryInterface(STKObjects.IAgScenario)

# Set time period
scenario2.SetTimePeriod("1 Aug 2020 16:00:00", "2 Aug 2020 16:00:00")
totalTime = time.time() - startTime
splitTime = time.time()
print("--- Scenario creation: {a:4.3f} sec\t\tTotal time: {b:4.3f} sec ---".format(a=totalTime, b=totalTime))

############################################################################
# Simple Access
############################################################################

# Create satellite
satellite = scenario.Children.New(18, "MySatellite")
satellite2 = satellite.QueryInterface(STKObjects.IAgSatellite)

# Get propagator
propagator = satellite2.Propagator
propagator2 = propagator.QueryInterface(STKObjects.IAgVePropagatorTwoBody)

# Get orbit state
orbitState = propagator2.InitialState.Representation
orbitStateClassical = orbitState.ConvertTo(STKUtil.eOrbitStateClassical).QueryInterface(STKObjects.IAgOrbitStateClassical)

# Set SMA and eccentricity
orbitStateClassical.SizeShapeType = STKObjects.eSizeShapeSemimajorAxis
sizeShape = orbitStateClassical.SizeShape
sizeShape2 = sizeShape.QueryInterface(STKObjects.IAgClassicalSizeShapeSemimajorAxis)
sizeShape2.Eccentricity = 0
sizeShape2.SemiMajorAxis = 8000

# Set inclination and argument of perigee
orientation = orbitStateClassical.Orientation
orientation.Inclination = 25
orientation.ArgOfPerigee = 0

# Set RAAN
orientation.AscNodeType = STKObjects.eAscNodeRAAN
raan = orientation.AscNode.QueryInterface(STKObjects.IAgOrientationAscNodeRAAN)
raan.Value = 0

# Set true anomaly
orbitStateClassical.LocationType = STKObjects.eLocationTrueAnomaly
trueAnomaly = orbitStateClassical.Location.QueryInterface(STKObjects.IAgClassicalLocationTrueAnomaly)
trueAnomaly.Value = 0

# Assign orbit state and propagate satellite
orbitState.Assign(orbitStateClassical)
propagator2.Propagate()

# Create faciliy
facility = scenario.Children.New(STKObjects.eFacility, "MyFacility")
facility2 = facility.QueryInterface(STKObjects.IAgFacility)

# Set position
facility2.Position.AssignGeodetic(28.62, -80.62, 0.03) 

# Compute access between satellite and facility
print("\nComputing access...")
access = satellite.GetAccessToObject(facility)
access.ComputeAccess()

# Get access interval data
stkRoot.UnitPreferences.SetCurrentUnit("Time", "Min")
accessDataProvider = access.DataProviders.GetDataPrvIntervalFromPath("Access Data")
elements = ["Start Time", "Stop Time", "Duration"]
accessResults = accessDataProvider.ExecElements(scenario2.StartTime, scenario2.StopTime, elements)

startTimes = accessResults.DataSets.GetDataSetByName("Start Time").GetValues()
stopTimes = accessResults.DataSets.GetDataSetByName("Stop Time").GetValues()
durations = accessResults.DataSets.GetDataSetByName("Duration").GetValues()

# Print data to console
print("\nAccess Intervals")
print("{a:<29s}  {b:<29s}  {c:<14s}".format(a="Start Time", b="Stop Time", c="Duration (min)"))
for i in range(len(startTimes)):
    print("{a:<29s}  {b:<29s}  {c:<4.2f}".format(a=startTimes[i], b=stopTimes[i], c=durations[i]))

print("\nThe maximum access duration is {a:4.2f} minutes.".format(a=max(durations)))

# Print computation time
totalTime = time.time() - startTime
sectionTime = time.time() - splitTime
splitTime = time.time()
print("--- Access computation: {a:4.3f} sec\t\tTotal time: {b:4.3f} sec ---".format(a=sectionTime, b=totalTime))

############################################################################
# Constellations and Chains
############################################################################

# Remove initial satellite
satellite.Unload()

# Create constellation object
constellation = scenario.Children.New(STKObjects.eConstellation, "SatConstellation")
constellation2 = constellation.QueryInterface(STKObjects.IAgConstellation)

# Insert the constellation of Satellites
numOrbitPlanes = 4
numSatsPerPlane = 8

for orbitPlaneNum, RAAN in enumerate(range(0,180,180//numOrbitPlanes),1): #RAAN in degrees

    for satNum, trueAnomaly in enumerate(range(0,360,360//numSatsPerPlane), 1): #trueAnomaly in degrees
        
        # Insert satellite
        satellite = scenario.Children.New(STKObjects.eSatellite, f"Sat{orbitPlaneNum}{satNum}")
        satellite2 = satellite.QueryInterface(STKObjects.IAgSatellite)
                
        # Select Propagator
        satellite2.SetPropagatorType(STKObjects.ePropagatorTwoBody)
        
        # Set initial state
        twoBodyPropagator = satellite2.Propagator.QueryInterface(STKObjects.IAgVePropagatorTwoBody)
        keplarian = twoBodyPropagator.InitialState.Representation.ConvertTo(STKUtil.eOrbitStateClassical).QueryInterface(STKObjects.IAgOrbitStateClassical)
        
        keplarian.SizeShapeType = STKObjects.eSizeShapeSemimajorAxis
        keplarian.SizeShape.QueryInterface(STKObjects.IAgClassicalSizeShapeSemimajorAxis).SemiMajorAxis = 8200 #km
        keplarian.SizeShape.QueryInterface(STKObjects.IAgClassicalSizeShapeSemimajorAxis).Eccentricity = 0

        keplarian.Orientation.Inclination = 60 #degrees
        keplarian.Orientation.ArgOfPerigee = 0 #degrees
        keplarian.Orientation.AscNodeType = STKObjects.eAscNodeRAAN
        keplarian.Orientation.AscNode.QueryInterface(STKObjects.IAgOrientationAscNodeRAAN).Value = RAAN  #degrees
        
        keplarian.LocationType = STKObjects.eLocationTrueAnomaly
        keplarian.Location.QueryInterface(STKObjects.IAgClassicalLocationTrueAnomaly).Value = trueAnomaly + (360//numSatsPerPlane/2)*(orbitPlaneNum%2)  #Stagger true anomalies (degrees) for every other orbital plane       
        
        # Propagate
        satellite2.Propagator.QueryInterface(STKObjects.IAgVePropagatorTwoBody).InitialState.Representation.Assign(keplarian)
        satellite2.Propagator.QueryInterface(STKObjects.IAgVePropagatorTwoBody).Propagate()
        
        # Add to constellation object
        constellation2.Objects.AddObject(satellite)

# Create chain
chain = scenario.Children.New(STKObjects.eChain, "Chain")
chain2 = chain.QueryInterface(STKObjects.IAgChain)

# Add satellite constellation and facility
chain2.Objects.AddObject(constellation)
chain2.Objects.AddObject(facility)

# Compute chain
chain2.ComputeAccess()

# Find satellite with most access time
chainDataProvider = chain.DataProviders.GetDataPrvIntervalFromPath("Object Access")
chainResults = chainDataProvider.Exec(scenario2.StartTime, scenario2.StopTime)

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
print("\n{a:s} has the longest total duration: {b:4.2f} minutes.".format(a=objectList[index], b=durationList[index]))

# Print computation time
totalTime = time.time() - startTime
sectionTime = time.time() - splitTime
splitTime = time.time()
print("--- Chain computation: {a:4.2f} sec\t\tTotal time: {b:4.2f} sec ---".format(a=sectionTime, b=totalTime))
    
############################################################################
# Coverage
############################################################################

# Create coverage definition
coverageDef = scenario.Children.New(STKObjects.eCoverageDefinition, "CoverageDefinition")
coverageDef2 = coverageDef.QueryInterface(STKObjects.IAgCoverageDefinition)

# Set grid bounds type
grid = coverageDef2.Grid
grid.BoundsType = STKObjects.eBoundsCustomRegions

# Add US shapefile to bounds
bounds = coverageDef2.Grid.Bounds
bounds2 = bounds.QueryInterface(STKObjects.IAgCvBoundsCustomRegions)
bounds2.RegionFiles.Add("C:\\Program Files\\AGI\\STK 12\\Data\\Shapefiles\\Countries\\United_States\\United_States.shp")

# Set resolution
grid.ResolutionType = STKObjects.eResolutionDistance
resolution = grid.Resolution
resolution2 = resolution.QueryInterface(STKObjects.IAgCvResolutionDistance)
resolution2.Distance = 75

# Add constellation as asset
coverageDef2.AssetList.Add("Constellation/SatConstellation")
coverageDef2.ComputeAccesses()

# Create figure of merit
figureOfMerit = coverageDef.Children.New(STKObjects.eFigureOfMerit, "FigureOfMert")
figureOfMerit2 = figureOfMerit.QueryInterface(STKObjects.IAgFigureOfMerit)

# Set the definition and compute type
figureOfMerit2.SetDefinitionType(STKObjects.eFmAccessDuration)
definition = figureOfMerit2.Definition
definition2 = definition.QueryInterface(STKObjects.IAgFmDefCompute)
definition2.SetComputeType(STKObjects.eAverage)

fomDataProvider = figureOfMerit.DataProviders.GetDataPrvFixedFromPath("Overall Value")
fomResults = fomDataProvider.Exec()

minAccessDuration = fomResults.DataSets.GetDataSetByName("Minimum").GetValues()[0]
maxAccessDuration = fomResults.DataSets.GetDataSetByName("Maximum").GetValues()[0]
avgAccessDuration = fomResults.DataSets.GetDataSetByName("Average").GetValues()[0]

print("\nThe minimum coverage duration is {a:4.2f} min.".format(a=minAccessDuration))
print("The maximum coverage duration is {a:4.2f} min.".format(a=maxAccessDuration))
print("The average coverage duration is {a:4.2f} min.".format(a=avgAccessDuration))

# Print computation time
totalTime = time.time() - startTime
sectionTime = time.time() - splitTime
print("--- Coverage computation: {a:4.2f} sec\t\tTotal time: {b:4.2f} sec ---".format(a=sectionTime, b=totalTime))

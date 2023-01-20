import sys

import win32com.client
from win32com.client import GetActiveObject

### Quick tip: If you would like to use a more detailed carrier ship
### download the CVN-72 model from support.agi.com/3d-models and
### place it into the following location before running the script:
### <STK Install Directory>\STKData\VO\Models\Sea

############################################################################
# Setup and Create the Scenario
############################################################################

print("...Opening the application")

try:
    # Grab an existing instance of STK
    stkUiApplication = win32com.client.GetActiveObject("STK12.Application")
    stkRoot = stkUiApplication.Personality2
    checkempty = stkRoot.Children.Count
    if checkempty == 0:
        # If a Scenario is not open, create a new scenario
        stkUiApplication.Visible = 1
        stkUiApplication.UserControl = 1
        stkRoot.NewScenario("Aviator_Carrier_Landing_Example")
    else:
        # If a Scenario is open, prompt the user to accept closing it or not
        inputText = input(
            "Close the current scenario? Press enter to continue. If you have not saved, your progress will be lost."
        )
        if inputText == "":
            stkRoot.CurrentScenario.Unload()
            stkUiApplication.Visible = True
            stkUiApplication.UserControl = 1
            stkRoot.NewScenario("Aviator_Carrier_Landing_Example")
        else:
            sys.exit()
except Exception:
    # STK is not running, launch new instance of STK 12 and grab it
    stkUiApplication = win32com.client.Dispatch("STK12.Application")
    stkUiApplication.Visible = 1
    stkUiApplication.UserControl = 1
    stkRoot = stkUiApplication.Personality2
    stkRoot.NewScenario("Aviator_Carrier_Landing_Example")

print("...Creating new scenario")

# Set scenario time interval
scenario = stkRoot.CurrentScenario
scenario.SetTimePeriod("20 Jan 2020 17:00:00.000", "+2 hours")  # times are UTCG

# Reset animation time to new scenario start time
stkRoot.Rewind()

# Set scenario global reference to MSL
scenario.VO.SurfaceReference = 1  # 1 = AgESurfaceReference.eMeanSeaLevel

# Maximize application window
stkRoot.ExecuteCommand("Application / Raise")
stkRoot.ExecuteCommand("Application / Maximize")

# Maximize 3D window
stkRoot.ExecuteCommand("Window3D * Maximize")


############################################################################
# Create Facility to Represent the Oceana Naval Air Station (KNTU)
############################################################################

print("...Adding Oceana Naval Air Station")

facilityKntu = scenario.Children.New(
    8, "OCEANA_NAS__APOLLO_SOUCEK_FIELD"
)  # 8 = AgESTKObjectType.eFacility

# Set facility postiion
facilityKntu.UseTerrain = True
facilityKntu.Position.AssignGeodetic(
    36.822744, -76.031892, 0.0
)  # setting alt to zero will place it on terrain

# Set facility color
facilityKntu.Graphics.Color = 16777215  # white


############################################################################
# Create Ship to Represent the USS Abraham Lincoln (CVN-72)
############################################################################

print("...Adding CVN-72 carrier ship")

shipCvn72 = scenario.Children.New(21, "CVN-72")  # 21 = AgESTKObjectType.eShip

# Set route properties
shipCvn72.SetRouteType(9)  # 9 = AgEVePropagatorType.ePropagatorGreatArc
shipCvn72.Route.SetAltitudeRefType(1)  # 1 = AgEVeAltitudeRef.eWayPtAltRefTerrain
shipCvn72.Route.AltitudeRef.Granularity = 1  # km

# Set waypoints
waypoint1 = shipCvn72.Route.Waypoints.Add()
waypoint1.Latitude = 36.64988281  # deg
waypoint1.Longitude = -75.11230361  # deg
waypoint1.Speed = 0.01543333  # km/s
waypoint1.Altitude = 0  # km
waypoint2 = shipCvn72.Route.Waypoints.Add()
waypoint2.Latitude = 36.63713768  # deg
waypoint2.Longitude = -74.87339587  # deg
waypoint2.Speed = 0.01543333  # km/s
waypoint2.Altitude = 0  # km
waypoint3 = shipCvn72.Route.Waypoints.Add()
waypoint3.Latitude = 36.65454874  # deg
waypoint3.Longitude = -75.29117133  # deg
waypoint3.Speed = 0.01543333  # km/s
waypoint3.Altitude = 0  # km

# Set display properties
shipCvn72.Graphics.Attributes.Color = 16776960  # cyan
shipCvn72.Graphics.Attributes.Line.Width = 2  # medium thickness. 2 = AgELineWidth.e3

# Set ship model
try:
    # Insert CVN-72 model if user has added to the STK install folder
    shipCvn72.VO.Model.ModelData.Filename = (
        "STKData\\VO\\Models\\Sea\\cvn-72\\cvn-72.mdl"
    )
except:
    # Insert default carrier model from STK install folder
    shipCvn72.VO.Model.ModelData.Filename = (
        "STKData\\VO\\Models\\Sea\\aircraft-carrier.mdl"
    )
    shipCvn72.VO.Offsets.Translational.Enable = True
    shipCvn72.VO.Offsets.Translational.Z = 0.02
    # km

# Propagate ship
shipCvn72.Route.Propagate()

# Position 3D window near ship
stkRoot.ExecuteCommand("VO * ViewFromTo Normal From Ship/CVN-72")  # zoom to ship
stkRoot.ExecuteCommand("VO * ViewerPosition 20 115 150000")  # set view position


############################################################################
# Create Lead Hornet Aircraft to Perform Carrier Landing
############################################################################

print("...Creating lead hornet aircraft")

aircraftHornetLead = scenario.Children.New(
    1, "Hornet_Flight_Lead"
)  # 1 = AgESTKObjectType.eAircraft

# Set propagator to Aviator
aircraftHornetLead.SetRouteType(15)  # 15 = AgEVePropagatorType.ePropagatorAviator

# Grab the Aviator Propagator
avtrPropHornetLead = aircraftHornetLead.Route.AvtrPropagator

# Grab the Aviator catalog. This handle can be used for later aircraft too.
catalog = avtrPropHornetLead.AvtrCatalog

# Grab the aircraft models from the catalog
acModels = catalog.AircraftCategory.AircraftModels

# If a copy of the Basic Fighter aircraft model already exists, remove it
if acModels.Contains("Basic Fighter"):
    basicFighter = acModels.GetAircraft("Basic Fighter")
else:
    print("...Basic Fighter aircraft model cannot be found.")

# Grab the mission
avtrMissionHornetLead = avtrPropHornetLead.AvtrMission

# Set the aircraft model to Basic Fighter Copy
avtrMissionHornetLead.Vehicle = basicFighter

# From the mission grab the phase collection
phasesHornetLead = avtrMissionHornetLead.Phases

# Get the first phase
phaseHornetLead = phasesHornetLead.Item(0)

# Get the procedure collection
proceduresHornetLead = phaseHornetLead.Procedures

# Set display properties
aircraftHornetLead.Graphics.Attributes.Color = 65535  # yellow
aircraftHornetLead.Graphics.Attributes.Line.Width = (
    2  # medium thickness. 2 = AgELineWidth.e3
)

##  Get the runways from the catalog

# Get the runway category
runwayCategory = avtrPropHornetLead.AvtrCatalog.RunwayCategory

# Set the ARINC runways to look at the installed sample
installDir = stkRoot.ExecuteCommand("GetDirectory / STKHome").Item(0)
runwayCategory.ARINC424Runways.MasterDataFilepath = (
    installDir + "Data\\Resources\\stktraining\\samples\\FAANFD18"
)

# Get the list of runways
runwaysARINC424 = runwayCategory.ARINC424Runways
runwayList = runwaysARINC424.ChildNames

# Grab Oceana NAS from runways
runwayNameOceana = "OCEANA NAS /APOLLO SOUCEK FIEL 05R 23L"
if runwaysARINC424.Contains(runwayNameOceana):
    oceana = runwaysARINC424.GetARINC424Item(runwayNameOceana)
else:
    print("...Runway " + runwayNameOceana + " does not exist in catalog.")


### Add a Takeoff Procedure

print("...Lead hornet - adding takeoff procedure")

# Add a takeoff procedure from a runway
takeoffHornetLead = proceduresHornetLead.Add(
    6, 22
)  # 6 = AgEAvtrSiteType.eSiteRunway, 22 = AgEAvtrProcedureType.eProcTakeoff

## Set the site properties

# Get the site
oceanaRunway = takeoffHornetLead.Site

# Copy the Oceana runway
oceanaRunway.CopyFromCatalog(oceana)
oceanaRunway.Name = runwayNameOceana

## Set the procedure properties

# Get the runway heading options
runwayOptionsHornetLead = takeoffHornetLead.RunwayHeadingOptions

# Set it to low end
runwayOptionsHornetLead.RunwayMode = 1  # 1 = AgEAvtrRunwayHighLowEnd.eLowEnd

# Set the takeoff to normal
takeoffHornetLead.TakeoffMode = 0  # 0 = AgEAvtrTakeoffMode.eTakeoffNormal

# Get the interface for a normal takeoff
normalTakeoffHornetLead = takeoffHornetLead.ModeAsNormal

# Get the angle and terrain option
normalTakeoffHornetLead.TakeoffClimbAngle = 3  # deg
normalTakeoffHornetLead.DepartureAltitude = 500  # ft
normalTakeoffHornetLead.RunwayAltitudeOffset = 0  # ft
normalTakeoffHornetLead.UseRunwayTerrain = True


### Add an Enroute Procedure to Begin Approach to Ship

print("...Lead hornet - adding enroute procedure to approach ship")

enrouteHornetLead = proceduresHornetLead.Add(
    9, 8
)  # 9 = AgEAvtrSiteType.eSiteSTKObjectWaypoint, 8 = AgEAvtrProcedureType.eProcEnroute

## Set the site properties
enrouteHornetLeadSite = enrouteHornetLead.Site

# Link to ship object
enrouteHornetLeadSite.ObjectName = "Ship/CVN-72"

# Set waypoint time to scenario start time
enrouteHornetLeadSite.WaypointTime = scenario.StartTime

# Set Offset mode and bearing/range values
enrouteHornetLeadSite.OffsetMode = (
    3  # 3 = AgEAvtrSTKObjectWaypointOffsetMode.eOffsetRelativeBearingRange
)
enrouteHornetLeadSite.Bearing = 180  # deg
enrouteHornetLeadSite.Range = 40  # nm

## Set the procedure properties

# Set the altitude options
enrouteHornetLead.AltitudeMSLOptions.UseDefaultCruiseAltitude = False
enrouteHornetLead.AltitudeMSLOptions.MSLAltitude = 20000  # ft

# Set the navigation options
enrouteHornetLead.NavigationOptions.NavMode = (
    2  # 2 = AgEAvtrPointToPointMode.eArriveOnCourse
)
enrouteHornetLead.NavigationOptions.ArriveOnCourse = 135  # deg


### Add a 2nd Enroute Procedure to "Enter the Stack"

print("...Lead hornet - adding enroute procedure to enter stack")

enroute2HornetLead = proceduresHornetLead.Add(
    9, 8
)  # 9 = AgEAvtrSiteType.eSiteSTKObjectWaypoint, 8 = AgEAvtrProcedureType.eProcEnroute

## Set the site properties
enroute2HornetLeadSite = enroute2HornetLead.Site

# Link to ship object
enroute2HornetLeadSite.ObjectName = "Ship/CVN-72"

# Set waypoint time to scenario start time
enroute2HornetLeadSite.WaypointTime = "20 Jan 2020 17:09:06.858"  # UTCG

# Set Offset mode and bearing/range values
enroute2HornetLeadSite.OffsetMode = (
    3  # 3 = AgEAvtrSTKObjectWaypointOffsetMode.eOffsetRelativeBearingRange
)
enroute2HornetLeadSite.Bearing = 180  # deg
enroute2HornetLeadSite.Range = 10  # nm

## Set the procedure properties

# Set the procedure name
enroute2HornetLead.Name = "Enter the Stack"

# Set the altitude options
enroute2HornetLead.AltitudeMSLOptions.UseDefaultCruiseAltitude = False
enroute2HornetLead.AltitudeMSLOptions.MSLAltitude = 10000  # ft

# Set the navigation options
enroute2HornetLead.NavigationOptions.NavMode = (
    1  # 1 = AgEAvtrPointToPointMode.eArriveOnCourseForNext
)

# Set the enroute cruise airspeed options
enroute2HornetLead.EnrouteCruiseAirspeedOptions.CruiseSpeedType = (
    1  # 1 = AgEAvtrCruiseSpeed.eMaxEnduranceAirspeed
)


### Create a New Mission Phase for StationKeeping

phase2HornetLead = avtrMissionHornetLead.Phases.Add()
phase2HornetLead.Name = "StationKeeping"

# Get procedures for new phase
procedures2HornetLead = phase2HornetLead.Procedures


### Add a Basic Maneuver Procedure to "Enter Case 1 Marshall"

print("...Lead hornet - adding maneuever to enter Case I Marshall")

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuverHornetLead = procedures2HornetLead.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

## Set the procedure properties

# Set procedure name
basicManeuverHornetLead.Name = "Case I Marshall"

## Set the horizontal/navigation strategy
basicManeuverHornetLead.NavigationStrategyType = "Stationkeeping"

# Get the navigation interface
stationkeepingNavHornetLead = basicManeuverHornetLead.Navigation

# Set stationkeeping target
stationkeepingNavHornetLead.TargetName = "Ship/CVN-72"

# Set station options
stationkeepingNavHornetLead.RelBearing = -90  # deg
stationkeepingNavHornetLead.RelRange = 2.7  # nm
stationkeepingNavHornetLead.DesiredRadius = 2.5  # nm
stationkeepingNavHornetLead.TurnDirection = 0  # 0 = AgEAvtrTurnDirection.eTurnLeft

# Set stop condition options
stationkeepingNavHornetLead.StopCondition = (
    1  # 1 = AgEAvtrStationkeepingStopCondition.eStopAfterTurnCount
)
stationkeepingNavHornetLead.StopAfterTurnCount = 5
stationkeepingNavHornetLead.UseRelativeCourse = True
stationkeepingNavHornetLead.StopCourse = -180  # deg

## Set the vertical/profile strategy
basicManeuverHornetLead.ProfileStrategyType = "Autopilot - Vertical Plane"

# Get the profile interface
autoProfileHornetLead = basicManeuverHornetLead.Profile

# Set the altitude options
autoProfileHornetLead.AltitudeMode = (
    1  # 1 = AgEAvtrAutopilotAltitudeMode.eAutopilotSpecifyAltitude
)
autoProfileHornetLead.AbsoluteAltitude = 2000  # ft
autoProfileHornetLead.AltitudeControlMode = (
    0  # 0 = AgEAvtrAutopilotAltitudeControlMode.eAutopilotAltitudeRate
)
autoProfileHornetLead.ControlAltitudeRateValue = 2000  # ft/min
autoProfileHornetLead.ControlLimitMode = 1  # 1 = AgEAvtrPerfModelOverride.eOverride
autoProfileHornetLead.MaxPitchRate = 10  # deg/s
autoProfileHornetLead.DampingRatio = 2

# Set the airspeed options
autoProfileHornetLead.AirspeedOptions.AirspeedMode = (
    3  # 3 = AgEAvtrBasicManeuverAirspeedMode.eMaintainMaxEnduranceAirspeed
)
autoProfileHornetLead.AirspeedOptions.MinSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)
autoProfileHornetLead.AirspeedOptions.MaxSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)

## Set the attitude/performance/fuel options
basicManeuverHornetLead.FlightMode = 3  # 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuverHornetLead.FuelFlowType = (
    1  # 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise
)

## Set the basic stop conditions
basicManeuverHornetLead.UseStopFuelState = True
basicManeuverHornetLead.StopFuelState = 2000  # lb
basicManeuverHornetLead.UseMaxTimeOfFlight = False
basicManeuverHornetLead.UseMaxDownrange = True
basicManeuverHornetLead.MaxDownrange = 500  # nm

basicManeuverHornetLead.AltitudeLimitMode = (
    0  # 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
)
basicManeuverHornetLead.TerrainImpactMode = (
    2  # 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue
)


### Add a Basic Maneuver to "Enter Break"

print("...Lead hornet - adding maneuever to enter break")

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuver2HornetLead = procedures2HornetLead.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

# Set the site name
basicManeuver2HornetLead.Site.Name = "Mother"

## Set the procedure properties

# Set procedure name
basicManeuver2HornetLead.Name = "Enter Break"

## Set the horizontal/navigation strategy
basicManeuver2HornetLead.NavigationStrategyType = "Relative Course"

# Get the navigation interface
relCourse = basicManeuver2HornetLead.Navigation

# Set the target
relCourse.TargetName = "Ship/CVN-72"

# Set relative or true course option
relCourse.UseRelativeCourse = True
relCourse.Course = 0  # deg

# Set the anchor offset
relCourse.InTrack = 1  # nm
relCourse.CrossTrack = 0  # nm

# Set other options
relCourse.UseApproachTurnMode = True

# Set closure mode
relCourse.ClosureMode = 2  # 2 = AgEAvtrClosureMode.eHOBS
relCourse.DownrangeOffset = 0  # nm
relCourse.HOBSMaxAngle = 90  # deg

## Set the vertical/profile strategy
basicManeuver2HornetLead.ProfileStrategyType = "Autopilot - Vertical Plane"

# Get the profile interface
autoProfile2HornetLead = basicManeuver2HornetLead.Profile

# Set the altitude options
autoProfile2HornetLead.AltitudeMode = (
    1  # 1 = AgEAvtrAutopilotAltitudeMode.eAutopilotSpecifyAltitude
)
autoProfile2HornetLead.AbsoluteAltitude = 800  # ft
autoProfile2HornetLead.AltitudeControlMode = (
    0  # 0 = AgEAvtrAutopilotAltitudeControlMode.eAutopilotAltitudeRate
)
autoProfile2HornetLead.ControlAltitudeRateValue = 2000  # ft/min
autoProfile2HornetLead.ControlLimitMode = 1  # 1 = AgEAvtrPerfModelOverride.eOverride
autoProfile2HornetLead.MaxPitchRate = 10  # deg/s
autoProfile2HornetLead.DampingRatio = 2

# Set the airspeed options
autoProfile2HornetLead.AirspeedOptions.AirspeedMode = (
    1  # 1 = AgEAvtrBasicManeuverAirspeedMode.eMaintainSpecifiedAirspeed
)
autoProfile2HornetLead.AirspeedOptions.SpecifiedAirspeedType = (
    2  # 2 = AgEAvtrAirspeedType.eCAS
)
autoProfile2HornetLead.AirspeedOptions.SpecifiedAirspeed = 350  # nm/hr
autoProfile2HornetLead.AirspeedOptions.MinSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)
autoProfile2HornetLead.AirspeedOptions.MaxSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)

## Set the attitude/performance/fuel options
basicManeuver2HornetLead.FlightMode = 3  # 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuver2HornetLead.FuelFlowType = (
    1  # 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise
)

## Set the basic stop conditions
basicManeuver2HornetLead.UseStopFuelState = True
basicManeuver2HornetLead.StopFuelState = 0  # lb
basicManeuver2HornetLead.UseMaxTimeOfFlight = False
basicManeuver2HornetLead.UseMaxDownrange = True
basicManeuver2HornetLead.MaxDownrange = 100  # nm

basicManeuver2HornetLead.AltitudeLimitMode = (
    0  # 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
)
basicManeuver2HornetLead.TerrainImpactMode = (
    2  # 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue
)


### Add a Basic Maneuver Procedure to "Break"

print("...Lead hornet - adding maneuever to break")

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuver3HornetLead = procedures2HornetLead.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

## Set the procedure properties

# Set procedure name
basicManeuver3HornetLead.Name = "Break"

## Set the horizontal/navigation strategy
basicManeuver3HornetLead.NavigationStrategyType = "Relative Course"

# Get the navigation interface
relCourse2 = basicManeuver3HornetLead.Navigation

# Set the target
relCourse2.TargetName = "Ship/CVN-72"

# Set relative or true course option
relCourse2.UseRelativeCourse = True
relCourse2.Course = 180  # deg

# Set the anchor offset
relCourse2.InTrack = 0  # nm
relCourse2.CrossTrack = -1.3  # nm

# Set other options
relCourse2.UseApproachTurnMode = True

# Set maneuver factor
relCourse2.ManeuverFactor = 1.00  # aggressive

# Set control limit
relCourse2.SetControlLimit(
    2, 29.9725
)  # 2 = AgEAvtrBasicManeuverStrategyNavControlLimit.eNavMaxTurnRate, deg/s

# Set closure mode
relCourse2.ClosureMode = 2  # 2 = AgEAvtrClosureMode.eHOBS
relCourse2.DownrangeOffset = 0  # nm
relCourse2.HOBSMaxAngle = 90  # deg

## Set the vertical/profile strategy
basicManeuver3HornetLead.ProfileStrategyType = "Autopilot - Vertical Plane"

# Get the profile interface
autoProfile3HornetLead = basicManeuver3HornetLead.Profile

# Set the altitude options
autoProfile3HornetLead.AltitudeMode = 1
autoProfile3HornetLead.AbsoluteAltitude = 600  # ft
autoProfile3HornetLead.AltitudeControlMode = (
    0  # 0 = AgEAvtrAutopilotAltitudeControlMode.eAutopilotAltitudeRate
)
autoProfile3HornetLead.ControlAltitudeRateValue = 2000  # ft/min
autoProfile3HornetLead.ControlLimitMode = 1
autoProfile3HornetLead.MaxPitchRate = 10  # deg/s
autoProfile3HornetLead.DampingRatio = 2

# Set the airspeed options
autoProfile3HornetLead.AirspeedOptions.AirspeedMode = (
    1  # 1 = AgEAvtrBasicManeuverAirspeedMode.eMaintainSpecifiedAirspeed
)
autoProfile3HornetLead.AirspeedOptions.SpecifiedAirspeedType = (
    2  # 2 = AgEAvtrAirspeedType.eCAS
)
autoProfile3HornetLead.AirspeedOptions.SpecifiedAirspeed = 145  # nm/hr
autoProfile3HornetLead.AirspeedOptions.SpecifiedAccelDecelMode = (
    1  # 1 = AgEAvtrPerfModelOverride.eOverride
)
autoProfile3HornetLead.AirspeedOptions.SpecifiedAccelDecelG = 0.3  # G's
autoProfile3HornetLead.AirspeedOptions.MinSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)
autoProfile3HornetLead.AirspeedOptions.MaxSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)

## Set the attitude/performance/fuel options
basicManeuver3HornetLead.FlightMode = 3  # 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuver3HornetLead.FuelFlowType = (
    1  # 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise
)

## Set the basic stop conditions
basicManeuver3HornetLead.UseStopFuelState = True
basicManeuver3HornetLead.StopFuelState = 0  # lb
basicManeuver3HornetLead.UseMaxTimeOfFlight = False
basicManeuver3HornetLead.UseMaxDownrange = True
basicManeuver3HornetLead.MaxDownrange = 50  # nm

basicManeuver3HornetLead.AltitudeLimitMode = (
    0  # 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
)
basicManeuver3HornetLead.TerrainImpactMode = (
    2  # 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue
)


### Add a Basic Maneuver Procedure to "Recover" (Land on Ship)

print("...Lead hornet - adding maneuever to recover")

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuver4HornetLead = procedures2HornetLead.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

## Set the procedure properties

# Set procedure name
basicManeuver4HornetLead.Name = "Recover"

## Set the horizontal/navigation strategy
basicManeuver4HornetLead.NavigationStrategyType = "Relative Course"

# Get the navigation interface
relCourse3 = basicManeuver4HornetLead.Navigation

# Set the target
relCourse3.TargetName = "Ship/CVN-72"

# Set relative or true course option
relCourse3.UseRelativeCourse = True
relCourse3.Course = -9.0  # deg

# Set the anchor offset
stkRoot.UnitPreferences.Item("AviatorDistance").SetCurrentUnit("ft")
relCourse3.InTrack = -850  # ft
relCourse3.CrossTrack = 75  # ft
stkRoot.UnitPreferences.Item("AviatorDistance").SetCurrentUnit("nm")

# Set other options
relCourse3.UseApproachTurnMode = True

# Set closure mode
relCourse3.ClosureMode = 2  # 2 = AgEAvtrClosureMode.eHOBS
relCourse3.DownrangeOffset = 0.1  # nm
relCourse3.HOBSMaxAngle = 90  # deg

## Set the vertical/profile strategy
basicManeuver4HornetLead.ProfileStrategyType = "Relative Flight Path Angle"

# Get the profile interface
relFpa = basicManeuver4HornetLead.Profile

# Set FPA and anchor alt offset
relFpa.FPA = -3.5  # deg
relFpa.AnchorAltOffset = 100  # ft

# Set control limit
relFpa.SetControlLimit(1, 10)  # 1 = AgEAvtrProfileControlLimit.eProfilePitchRate, deg/s

# Set airspeed options
relFpa.AirspeedOptions.AirspeedMode = (
    0  # 0 = AgEAvtrBasicManeuverAirspeedMode.eMaintainCurrentAirspeed
)
relFpa.AirspeedOptions.MaintainAirspeedType = 3  # 3 = AgEAvtrAirspeedType.eTAS
relFpa.AirspeedOptions.MinSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)
relFpa.AirspeedOptions.MaxSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)

## Set the attitude/performance/fuel options
basicManeuver4HornetLead.FlightMode = 3  # 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuver4HornetLead.FuelFlowType = (
    1  # 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise
)

## Set the basic stop conditions
basicManeuver4HornetLead.UseStopFuelState = True
basicManeuver4HornetLead.StopFuelState = 0  # lb
basicManeuver4HornetLead.UseMaxTimeOfFlight = False
basicManeuver4HornetLead.UseMaxDownrange = True
basicManeuver4HornetLead.MaxDownrange = 50  # nm

basicManeuver4HornetLead.AltitudeLimitMode = (
    0  # 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
)
basicManeuver4HornetLead.TerrainImpactMode = (
    2  # 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue
)

# Propagate aircraft
avtrPropHornetLead.Propagate()


############################################################################
# Create Wingman Hornet Aircraft to Fly Formation with Lead
############################################################################

print("...Creating wingman hornet aircraft")

aircraftHornetWing = scenario.Children.New(
    1, "Hornet_Flight_Wing"
)  # 1 = AgESTKObjectType.eAircraft

# Set propagator to Aviator
aircraftHornetWing.SetRouteType(15)  # 15 = AgEVePropagatorType.ePropagatorAviator

# Grab the Aviator Propagator
avtrPropHornetWing = aircraftHornetWing.Route.AvtrPropagator

# Grab the mission
avtrMissionHornetWing = avtrPropHornetWing.AvtrMission

# Set the aircraft model to Basic Fighter Copy
avtrMissionHornetWing.Vehicle = basicFighter

# From the mission grab the phase collection
phasesHornetWing = avtrMissionHornetWing.Phases

# Get the first phase
phaseHornetWing = phasesHornetWing.Item(0)

# Get the procedure collection
proceduresHornetWing = phaseHornetWing.Procedures

# Set display properties
aircraftHornetWing.Graphics.Attributes.Color = 16724991  # magenta
aircraftHornetWing.Graphics.Attributes.Line.Width = 2  # medium thickness


### Add an Enroute Procedure to Begin Flying to Lead

# Aircraft starts at a waypoint south of the lead aircraft, already flying.

print("...Wing hornet - adding enroute procedure to approach Lead hornet")

enrouteHornetWing = proceduresHornetWing.Add(
    15, 8
)  # 15 = AgEAvtrSiteType.eSiteWaypoint, 8 = AgEAvtrProcedureType.eProcEnroute

## Set the site properties
enrouteHornetWingSite = enrouteHornetWing.Site
enrouteHornetWingSite.Name = "Waypoint"
enrouteHornetWingSite.Latitude = 36.3174  # deg
enrouteHornetWingSite.Longitude = -75.4974  # deg

## Set the procedure properties

# Set the altitude options
enrouteHornetWing.AltitudeMSLOptions.UseDefaultCruiseAltitude = True

# Set the navigation options
enrouteHornetWing.NavigationOptions.NavMode = (
    2  # 2 = AgEAvtrPointToPointMode.eArriveOnCourse
)
enrouteHornetWing.NavigationOptions.ArriveOnCourse = 340.691  # deg


### Add a Basic Maneuver Procedure to "Intercept Leader"

print("...Wing hornet - adding maneuver to intercept Lead hornet")

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuverHornetWing = proceduresHornetWing.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

## Set the procedure properties

# Set procedure name
basicManeuverHornetWing.Name = "Intercept Leader"

# Set the horizontal/navigation strategy
basicManeuverHornetWing.NavigationStrategyType = "Relative Bearing"

# Get the navigation interface
relBearing = basicManeuverHornetWing.Navigation

# Set the target
relBearing.TargetName = "Aircraft/Hornet_Flight_Lead"

# Set relative bearing values
relBearing.RelBearing = -20
# deg
relBearing.MinRange = 15
# nm

# Set control limits
relBearing.SetControlLimit(0, 0)
# 0 = AgEAvtrBasicManeuverStrategyNavControlLimit.eNavUseAccelPerfModel

## Set the vertical/profile strategy
basicManeuverHornetWing.ProfileStrategyType = "Cruise Profile"

# Get the profile interface
cruiseProfileHornetWing = basicManeuverHornetWing.Profile

# Set the reference frame
cruiseProfileHornetWing.ReferenceFrame = 0
# 0 = AgEAvtrBasicManeuverRefFrame.eEarthFrame

# Set the altitude options
cruiseProfileHornetWing.RequestedAltitude = 18000
# ft

# Set cruise airspeed
cruiseProfileHornetWing.CruiseAirspeedOptions.CruiseSpeedType = 2
# 2 = AgEAvtrCruiseSpeed.eMaxRangeAirspeed

## Set the attitude/performance/fuel options
basicManeuverHornetWing.FlightMode = 3
# 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuverHornetWing.FuelFlowType = 1
# 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise

## Set the basic stop conditions
basicManeuverHornetWing.UseStopFuelState = True
basicManeuverHornetWing.StopFuelState = 0.0
basicManeuverHornetWing.UseMaxTimeOfFlight = False
basicManeuverHornetWing.UseMaxDownrange = False

basicManeuverHornetWing.AltitudeLimitMode = 0
# 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
basicManeuverHornetWing.TerrainImpactMode = 2
# 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue


### Add a Basic Maneuver Procedure to "Fly Formation to Marshall"

print("...Wing hornet - adding maneuver to fly in formation to Marshall")

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuverHornetWing = proceduresHornetWing.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

## Set the procedure properties

# Set procedure name
basicManeuverHornetWing.Name = "Fly Formation to Marshall"

## Set the horizontal/navigation strategy
basicManeuverHornetWing.NavigationStrategyType = "Rendezvous/Formation"

# Get the navigation interface
rendezvousForm = basicManeuverHornetWing.Navigation

# Set the cooperative target
rendezvousForm.TargetName = "Aircraft/Hornet_Flight_Lead"

# Set the position options
rendezvousForm.RelativeBearing = 135  # deg
rendezvousForm.RelativeRange = 0.25  # nm
rendezvousForm.AltitudeSplit = 100  # ft

# Set the maneuver factor
rendezvousForm.ManeuverFactor = 0.8

# Enable counter turn logic
rendezvousForm.UseCounterTurnLogic = True

# Set the collision avoidance logic
rendezvousForm.SetCPA(True, 152.4)  # nm

# Set the airspeed control options
rendezvousForm.MaxSpeedAdvantage = 75  # nm/hr

# Set the rendezvous stop condition
rendezvousForm.StopCondition = (
    2  # 2 = AgEAvtrRendezvousStopCondition.eStopAfterTargetCurrentPhase
)

## Set the vertical/profile strategy
# Profile settings are copied from the Navigation settings when using 'Rendezvous/Formation' as the nav mode.

## Set the attitude/performance/fuel options
basicManeuverHornetWing.FlightMode = 3  # 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuverHornetWing.FuelFlowType = (
    1  # 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise
)

## Set the basic stop conditions
basicManeuverHornetWing.UseStopFuelState = False
basicManeuverHornetWing.UseMaxTimeOfFlight = False
basicManeuverHornetWing.UseMaxDownrange = True
basicManeuverHornetWing.MaxDownrange = 500  # nm

basicManeuverHornetWing.AltitudeLimitMode = (
    0  # 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
)
basicManeuverHornetWing.TerrainImpactMode = (
    2  # 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue
)


### Add a Basic Maneuver Procedure to "Split - Marshall - 3 Kft"

print(
    "...Wing hornet - adding maneuver to fly Marshall at 3 Kft split from Lead hornet"
)

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuver2HornetWing = proceduresHornetWing.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

## Set the procedure properties

# Set procedure name
basicManeuver2HornetWing.Name = "Split - Marshall - 3 Kft"

## Set the horizontal/navigation strategy
basicManeuver2HornetWing.NavigationStrategyType = "Stationkeeping"

# Get the navigation interface
stationkeepingNavHornetWing = basicManeuver2HornetWing.Navigation

# Set stationkeeping target
stationkeepingNavHornetWing.TargetName = "Ship/CVN-72"

# Set station options
stationkeepingNavHornetWing.RelBearing = -90  # deg
stationkeepingNavHornetWing.RelRange = 2.7  # nm
stationkeepingNavHornetWing.DesiredRadius = 2.5  # nm
stationkeepingNavHornetWing.TurnDirection = 0  # 0 = AgEAvtrTurnDirection.eTurnLeft

# Set stop condition options
stationkeepingNavHornetWing.StopCondition = (
    1  # 1 = AgEAvtrStationkeepingStopCondition.eStopAfterTurnCount
)
stationkeepingNavHornetWing.StopAfterTurnCount = 5
stationkeepingNavHornetWing.UseRelativeCourse = True
stationkeepingNavHornetWing.StopCourse = -180  # deg

## Set the vertical/profile strategy
basicManeuver2HornetWing.ProfileStrategyType = "Autopilot - Vertical Plane"

# Get the profile interface
autoProfileHornetWing = basicManeuver2HornetWing.Profile

# Set the altitude options
autoProfileHornetWing.AltitudeMode = (
    1  # 1 = AgEAvtrAutopilotAltitudeMode.eAutopilotSpecifyAltitude
)
autoProfileHornetWing.AbsoluteAltitude = 3000  # ft
autoProfileHornetWing.AltitudeControlMode = 0
autoProfileHornetWing.ControlAltitudeRateValue = 2000  # ft/min
autoProfileHornetWing.ControlLimitMode = 1
autoProfileHornetWing.MaxPitchRate = 10  # deg/s
autoProfileHornetWing.DampingRatio = 2

# Set the airspeed options
autoProfileHornetWing.AirspeedOptions.AirspeedMode = 3
autoProfileHornetWing.AirspeedOptions.MinSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)
autoProfileHornetWing.AirspeedOptions.MaxSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)

## Set the attitude/performance/fuel options
basicManeuver2HornetWing.FlightMode = 3  # 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuver2HornetWing.FuelFlowType = (
    1  # 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise
)

## Set the basic stop conditions
basicManeuver2HornetWing.UseStopFuelState = False
basicManeuver2HornetWing.UseMaxTimeOfFlight = False
basicManeuver2HornetWing.UseMaxDownrange = True
basicManeuver2HornetWing.MaxDownrange = 500  # nm

basicManeuver2HornetWing.AltitudeLimitMode = (
    0  # 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
)
basicManeuver2HornetWing.TerrainImpactMode = (
    2  # 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue
)


### Add a Basic Maneuver Procedure to "Marshall - Step Down - 2 Kft"

print("...Wing hornet - adding maneuver to fly Marshall stepped down to 2 Kft")

# Add a Basic Maneuver procedure from the end of the previous procedure
basicManeuver3HornetWing = proceduresHornetWing.Add(
    1, 5
)  # 1 = AgEAvtrSiteType.eSiteEndOfPrevProcedure, 5 = AgEAvtrProcedureType.eProcBasicManeuver

## Set the procedure properties

# Set procedure name
basicManeuver3HornetWing.Name = "Marshall - Step Down - 2 Kft"

## Set the horizontal/navigation strategy
basicManeuver3HornetWing.NavigationStrategyType = "Stationkeeping"

# Get the navigation interface
stationkeeping2NavHornetWing = basicManeuver3HornetWing.Navigation

# Set stationkeeping target
stationkeeping2NavHornetWing.TargetName = "Ship/CVN-72"

# Set station options
stationkeeping2NavHornetWing.RelBearing = -90  # deg
stationkeeping2NavHornetWing.RelRange = 2.7  # nm
stationkeeping2NavHornetWing.DesiredRadius = 2.5  # nm
stationkeeping2NavHornetWing.TurnDirection = 0  # 0 = AgEAvtrTurnDirection.eTurnLeft

# Set stop condition options
stationkeeping2NavHornetWing.StopCondition = (
    1  # 1 = AgEAvtrStationkeepingStopCondition.eStopAfterTurnCount
)
stationkeeping2NavHornetWing.StopAfterTurnCount = 1
stationkeeping2NavHornetWing.UseRelativeCourse = True
stationkeeping2NavHornetWing.StopCourse = -180  # deg

## Set the vertical/profile strategy
basicManeuver3HornetWing.ProfileStrategyType = "Autopilot - Vertical Plane"

# Get the profile interface
autoProfile2HornetWing = basicManeuver3HornetWing.Profile

# Set the altitude options
autoProfile2HornetWing.AltitudeMode = 1
autoProfile2HornetWing.AbsoluteAltitude = 2000  # ft
autoProfile2HornetWing.AltitudeControlMode = (
    0  # 0 = AgEAvtrAutopilotAltitudeControlMode.eAutopilotAltitudeRate
)
autoProfile2HornetWing.ControlAltitudeRateValue = 2000  # ft/min
autoProfile2HornetWing.ControlLimitMode = 1  # 1 = AgEAvtrPerfModelOverride.eOverride
autoProfile2HornetWing.MaxPitchRate = 10  # deg/s
autoProfile2HornetWing.DampingRatio = 2

# Set the airspeed options
autoProfile2HornetWing.AirspeedOptions.AirspeedMode = (
    3  # 3 = AgEAvtrBasicManeuverAirspeedMode.eMaintainMaxEnduranceAirspeed
)
autoProfile2HornetWing.AirspeedOptions.MinSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)
autoProfile2HornetWing.AirspeedOptions.MaxSpeedLimits = (
    0  # 0 = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eConstrainIfViolated
)

## Set the attitude/performance/fuel options
basicManeuver3HornetWing.FlightMode = 3  # 3 = AgEAvtrPhaseOfFlight.eFlightPhaseCruise
basicManeuver3HornetWing.FuelFlowType = (
    1  # 1 = AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowCruise
)

## Set the basic stop conditions
basicManeuver3HornetWing.UseStopFuelState = True
basicManeuver3HornetWing.StopFuelState = 0  # lb
basicManeuver3HornetWing.UseMaxTimeOfFlight = False
basicManeuver3HornetWing.UseMaxDownrange = True
basicManeuver3HornetWing.MaxDownrange = 500  # nm

basicManeuver3HornetWing.AltitudeLimitMode = (
    0  # 0 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitError
)
basicManeuver3HornetWing.TerrainImpactMode = (
    2  # 2 = AgEAvtrBasicManeuverAltitudeLimit.eBasicManeuverAltLimitContinue
)

# Propagate aircraft
avtrPropHornetWing.Propagate()

### End of Script
print("...Scenario done!")

# This script will build out a baseline Aviator Two Stage Launch. Once the baseline 
# trajectory is built, it will run a trade study varying inputs like True Course Hint, 
# control FPA dot, and weight to determine which configuration/trajectory inputs bring
# a given amount of payload to orbit. This serves as an example of how you can script a 
# launch vehicle trajectory generation and run trade studies. To take this script further,
# you could refine inputs to match your needs and optimize set inputs to attempt and find
# right configuration for your mission.
#
# As used throughout this tutorial, the term “missile” refers to a generic high-speed ballistic object and does not refer to a weapon or defense article
#
# Created by: Tommy Myers (tmyers@agi.com)
# Date Created: 5/6/2025
# STK Version used: STK 12.10

# Import main library
# from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkengine import STKEngine
from agi.stk12.stkobjects import *
from agi.stk12.stkobjects.aviator import *
from agi.stk12.vgt import *
import csv
import pandas as pd # Will need to install if you do not have 
import os
import openpyxl # Will need to install if you do not have 

# Create directory for ephemeris files
current_directory = os.getcwd()
folder_path = current_directory + '\\Ephemeris'
try:
    os.makedirs(folder_path)
    print(f"Folder '{folder_path}' created successfully.")
except FileExistsError:
    print(f"Folder '{folder_path}' already exists.")
except Exception as e:
    print(f"An error occurred: {e}")

scenario_folder = current_directory + '\\LaunchVehicleAnalysis'
try:
    os.makedirs(scenario_folder)
    print(f"Folder '{scenario_folder}' created successfully.")
except FileExistsError:
    print(f"Folder '{scenario_folder}' already exists.")
except Exception as e:
    print(f"An error occurred: {e}")

# init results and input arrays
perigeeResults = []
apogeeResults = []
payloadResults = []
inclinationResults = []
fuelStateInputs = []
launchAzimuthInputs = []
fpaDotInputs = []
payloadInputs = []
orbitValid = []

stk = STKEngine.StartApplication(noGraphics=False) # optionally, noGraphics = True

# Get the IAgStkObjectRoot interface
root = stk.NewObjectRoot()

# Create new scenario
root.NewScenario("LaunchVehicleAnalysis")

# IAgStkObjectRoot root: STK Object Model root
scenario = root.CurrentScenario

# Set unit pref to EpSec
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec')

# Create baseline objects
lc39 = scenario.Children.New(AgESTKObjectType.ePlace, 'LC-39A')
stage1 = scenario.Children.New(AgESTKObjectType.eAircraft, 'Stage1')
stage2 = scenario.Children.New(AgESTKObjectType.eAircraft, 'Stage2')
sat = scenario.Children.New(AgESTKObjectType.eSatellite, 'Orbit')

# Assign to Kennedy Space Center
lc39.Position.AssignGeodetic(28.6084,-80.6043,0)

# Set Stage 1 to an Avtr AC
stage1.SetRouteType(AgEVePropagatorType.ePropagatorAviator)

# Connect to Aviator 
route1 = stage1.Route
avtr1 = route1.AvtrPropagator
mission1 = avtr1.AvtrMission
config1 = mission1.Configuration
phases1 = mission1.Phases
phase1 = phases1[0]
procedures1 = phase1.Procedures
catalog1 = avtr1.AvtrCatalog

perfName1 = 'Stage1LV'

# Looks to see if there is already a performance model with that name, if it exists, it removes it
try: 
    testifexists = catalog1.AircraftCategory.MissileModels.GetMissile(perfName1)
    testifexists.Remove()
    print('Model removed\n')
except:
    print('No model found\n')

# Creates the Stage One Performance Model 
stage1PerfModel = catalog1.AircraftCategory.MissileModels.AddMissile(perfName1)
stage1PerfModel.AttitudeTransitions.RollRate = 10
stage1PerfModel.Aerodynamics.ModeAsSimple.SRef = 10
stage1PerfModel.Propulsion.ModeAsSimple.MaxThrust = 1710000
stage1PerfModel.Propulsion.ModeAsSimple.FuelFlow = 22406000
stage1PerfModel.DefaultConfiguration.MaxLandingWeight = 100000000
stage1PerfModel.DefaultConfiguration.EmptyWeight = 315000
stations1 = stage1PerfModel.DefaultConfiguration.GetStations()
fuelTank1 = stations1.GetInternalFuelTankByName('Internal Fuel Tank')
fuelTank1.Capacity = 905900
fuelTank1.InitialFuelState = 905900

# Sets the performance Model
model1 = catalog1.AircraftCategory.MissileModels.GetMissile(perfName1)
mission1.Vehicle = model1

# Launch Procedure
launch1 : IAgAvtrProcedureLaunch
launch1 = procedures1.Add(AgEAvtrSiteType.eSiteSTKStaticObject, AgEAvtrProcedureType.eProcLaunch)
launchsite1 = launch1.Site
launchsite1.ObjectName = 'Place/LC-39A'
launch1.DirectionVecName = 'LC-39A Zenith(Detic)'
launch1.AttitudeMode = AgEAvtrLaunchAttitudeMode.eLaunchAlignDirectionVector
launch1.SpecifyLaunchAirspeed = 1
launch1.AccelG = 2
launch1.TrueCourseHint = 45
launch1.SetAirspeed(AgEAvtrAirspeedType.eCAS, 250)

# Pitch 63 FPA 10 km DR
pitch3D1 : IAgAvtrBasicManeuverStrategyPitch3D
pitch1 = procedures1.Add(AgEAvtrSiteType.eSiteEndOfPrevProcedure, AgEAvtrProcedureType.eProcBasicManeuver)
pitch1.NavigationStrategyType = 'Pitch3D'
pitch3D1 = pitch1.Navigation
pitch3D1.CommandFPA = 63
pitch3D1.StopWhenFPAAchieved = 0
pitch3D1.AirspeedOptions.AirspeedMode = AgEAvtrBasicManeuverAirspeedMode.eAccelDecelAeroProp
pitch3D1.AirspeedOptions.Throttle = 90
pitch3D1.AirspeedOptions.MinSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
pitch3D1.AirspeedOptions.MaxSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
pitch1.MaxDownrange = 5.39957  # 10 km
pitch1.UseMaxTimeOfFlight = 0
pitch1.FuelFlowType - AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowAeroProp

# Pitch 55 FPA 40 km DR
pitch3D2 : IAgAvtrBasicManeuverStrategyPitch3D
pitch2 = procedures1.Add(AgEAvtrSiteType.eSiteEndOfPrevProcedure, AgEAvtrProcedureType.eProcBasicManeuver)
pitch2.NavigationStrategyType = 'Pitch3D'
pitch3D2 = pitch2.Navigation
pitch3D2.CommandFPA = 55
pitch3D2.ControlFPADot = 0.15
pitch3D2.StopWhenFPAAchieved = 0
pitch3D2.AirspeedOptions.AirspeedMode = AgEAvtrBasicManeuverAirspeedMode.eAccelDecelAeroProp
pitch3D2.AirspeedOptions.Throttle = 100
pitch3D2.AirspeedOptions.MinSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
pitch3D2.AirspeedOptions.MaxSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
pitch2.MaxDownrange = 21.5983 #nm = 40 km
pitch2.UseMaxTimeOfFlight = 0
pitch2.FuelFlowType - AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowAeroProp

# Continue Zero G until MECO
ballistic3D1 : IAgAvtrBasicManeuverStrategyBallistic3D
zeroGMECO = procedures1.Add(AgEAvtrSiteType.eSiteEndOfPrevProcedure, AgEAvtrProcedureType.eProcBasicManeuver)
zeroGMECO.NavigationStrategyType = 'Ballistic3D'
ballistic3D1 = zeroGMECO.Navigation
ballistic3D1.AirspeedOptions.AirspeedMode = AgEAvtrBasicManeuverAirspeedMode.eAccelDecelAeroProp
ballistic3D1.AirspeedOptions.Throttle = 100
ballistic3D1.AirspeedOptions.MinSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
ballistic3D1.AirspeedOptions.MaxSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
zeroGMECO.UseMaxDownrange = 0
zeroGMECO.UseMaxTimeOfFlight = 0
zeroGMECO.FuelFlowType - AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowAeroProp

# Coast 3 seconds
coastMan : IAgAvtrBasicManeuverStrategyBallistic3D
coast1 = procedures1.Add(AgEAvtrSiteType.eSiteEndOfPrevProcedure, AgEAvtrProcedureType.eProcBasicManeuver)
coast1.NavigationStrategyType = 'Ballistic3D'
coastMan = coast1.Navigation
coastMan.AirspeedOptions.AirspeedMode = AgEAvtrBasicManeuverAirspeedMode.eAccelDecelUnderGravity
coastMan.AirspeedOptions.MinSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
coastMan.AirspeedOptions.MaxSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
coast1.UseMaxDownrange = 0
coast1.MaxTimeOfFlight = '00:00:03.000'
coast1.UseStopFuelState = 0

# Sets 3D model to preinstalled LV glb
pathToGlb = r'C:\Program Files\AGI\STK 12\STKData\VO\Models\Space\launchvehicle.glb'
glbCmd1 = r'MissionModeler */Aircraft/Stage1 Aircraft SetValue Model3D "' + pathToGlb + '"'
root.ExecuteCommand(glbCmd1)

avtr1.Propagate()

## STAGE 2 ##
stage2.SetRouteType(AgEVePropagatorType.ePropagatorAviator)

# Connect to Aviator 
route2 = stage2.Route
avtr2 = route2.AvtrPropagator
mission2 = avtr2.AvtrMission
config2 = mission2.Configuration
phases2 = mission2.Phases
phase2 = phases2[0]
procedures2 = phase2.Procedures
catalog2 = avtr2.AvtrCatalog

perfName2 = 'Stage2LV'

# Looks to see if there is already a performance model with that name, if it exists, it removes it
try: 
    testifexists = catalog2.AircraftCategory.MissileModels.GetMissile(perfName2)
    testifexists.Remove()
    print('Model removed\n')
except:
    print('No model found\n')

# Creates the Stage Two Performance Model 
stage2PerfModel = catalog2.AircraftCategory.MissileModels.AddMissile(perfName2)
stage2PerfModel.AttitudeTransitions.RollRate = 10
stage2PerfModel.Aerodynamics.ModeAsSimple.SRef = 10
stage2PerfModel.Propulsion.ModeAsSimple.MaxThrust = 285000
stage2PerfModel.Propulsion.ModeAsSimple.FuelFlow = 3260130
stage2PerfModel.DefaultConfiguration.MaxLandingWeight = 100000000
stage2PerfModel.DefaultConfiguration.EmptyWeight = 39100
stations2 = stage2PerfModel.DefaultConfiguration.GetStations()
fuelTank2 = stations2.GetInternalFuelTankByName('Internal Fuel Tank')
fuelTank2.Capacity = 237000
fuelTank2.InitialFuelState = 237000

# Sets the performance model
model2 = catalog2.AircraftCategory.MissileModels.GetMissile(perfName2)
mission2.Vehicle = model2

# Launch Procedure
launch2 : IAgAvtrProcedureLaunch
launch2 = procedures2.Add(AgEAvtrSiteType.eSiteSTKVehicle, AgEAvtrProcedureType.eProcLaunch)
launchsite2 = launch2.Site
launchsite2.ObjectName = 'Aircraft/Stage1'
launch2.LaunchTime = stage1.Vgt.Events.GetItemByName('EphemerisStopTime').FindOccurrence().Epoch
launch2.DirectionVecName = '1stStage FlightPath.X'
launch2.AttitudeMode = AgEAvtrLaunchAttitudeMode.eLaunchAlignDirectionVector
launch2.AttitudeMode = AgEAvtrLaunchAttitudeMode.eLaunchHoldParentAttitude
launch2.SpecifyLaunchAirspeed = 0

# Coast 3 seconds
coastMan2 : IAgAvtrBasicManeuverStrategyBallistic3D
coast2 = procedures2.Add(AgEAvtrSiteType.eSiteEndOfPrevProcedure, AgEAvtrProcedureType.eProcBasicManeuver)
coast2.NavigationStrategyType = 'Ballistic3D'
coastMan2 = coast2.Navigation
coastMan2.AirspeedOptions.AirspeedMode = AgEAvtrBasicManeuverAirspeedMode.eAccelDecelUnderGravity
coastMan2.AirspeedOptions.MinSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
coastMan2.AirspeedOptions.MaxSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
coast2.UseMaxDownrange = 0
coast2.MaxTimeOfFlight = '00:00:03.000'
coast2.UseStopFuelState = 0

# Slow Pitch Over
slow3dPitch : IAgAvtrBasicManeuverStrategyPitch3D
slowPitch = procedures2.Add(AgEAvtrSiteType.eSiteEndOfPrevProcedure, AgEAvtrProcedureType.eProcBasicManeuver)
slowPitch.NavigationStrategyType = 'Pitch3D'
slow3dPitch = slowPitch.Navigation
slow3dPitch.CommandFPA = 0
slow3dPitch.ControlFPADot = 0.3
slow3dPitch.StopWhenFPAAchieved = 0
slow3dPitch.AirspeedOptions.AirspeedMode = AgEAvtrBasicManeuverAirspeedMode.eAccelDecelAeroProp
slow3dPitch.AirspeedOptions.Throttle = 100
slow3dPitch.AirspeedOptions.MinSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
slow3dPitch.AirspeedOptions.MaxSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
slowPitch.UseMaxDownrange = 0
slowPitch.MaxTimeOfFlight = '00:04:00.000'
slowPitch.FuelFlowType - AgEAvtrBasicManeuverFuelFlowType.eBasicManeuverFuelFlowAeroProp

# Orbit Injection
orbitInjMan : IAgAvtrBasicManeuverStrategyBallistic3D
orbitInj = procedures2.Add(AgEAvtrSiteType.eSiteEndOfPrevProcedure, AgEAvtrProcedureType.eProcBasicManeuver)
orbitInj.NavigationStrategyType = 'Ballistic3D'
orbitInjMan = orbitInj.Navigation
orbitInjMan.AirspeedOptions.AirspeedMode = AgEAvtrBasicManeuverAirspeedMode.eAccelDecelAeroProp
orbitInjMan.AirspeedOptions.MinSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
orbitInjMan.AirspeedOptions.MaxSpeedLimits = AgEAvtrBasicManeuverStrategyAirspeedPerfLimits.eIgnoreIfViolated
orbitInjMan.AirspeedOptions.Throttle = 100
orbitInj.UseMaxDownrange = 0
orbitInj.UseMaxTimeOfFlight = 0

# Sets second stage 3D model
glbCmd2 = r'MissionModeler */Aircraft/Stage2 Aircraft SetValue Model3D "' + pathToGlb + '"'
root.ExecuteCommand(glbCmd2)
avtr2.Propagate()

# Gets MISSION specific fuel tank
stationsMission2 = config2.GetStations()
fuelTankMission2 = stationsMission2.GetInternalFuelTankByName('Internal Fuel Tank')

## BEGIN TRADE STUDY ##

# Initial weights, step size, and number of runs
initEmptyWeight2 = 39100
initFuelState2 = 237000
fuelStep = 1000
fuelRuns = 3

# True course hint initial, step size, and runs
azInit = 45
azStep = 5
azRuns = 3

# Control FPA Dot intial, step size, and runs
fpaInit = 0.3
fpaStep = 0.2
fpaRuns = 3

# counter for all of the runs
count = 1

# For loop to loop through and trade fuel for empty weight
for i in range (fuelRuns):

    emptyWeight2 = initEmptyWeight2 + fuelStep * i
    fuelState2 = initFuelState2 - fuelStep * i
    config2.EmptyWeight = emptyWeight2
    fuelTankMission2.InitialFuelState = fuelState2
    fuelTankMission2.Capacity = fuelState2
    payload = emptyWeight2 - 9100

    # For loop to loop through different true course hints
    for j in range(azRuns):
        phase1 = mission1.Phases.Item(0)
        launch1 = phase1.Procedures.Item(0)
        launchAzimuth = azInit + azStep*j
        launch1.TrueCourseHint = launchAzimuth

        # For loop to loop through different control FPA dots
        for k in range(fpaRuns):
            phase2 = mission2.Phases.Item(0)
            pitchOver = phase2.Procedures.Item(2)
            FPADot = fpaInit + fpaStep*k
            pitchOver.Navigation.ControlFPADot = FPADot

            avtr1.Propagate()

            # Sets the seconds launch time to be the ephemeris end of the first stage
            launch2.LaunchTime = stage1.Vgt.Events.GetItemByName('EphemerisStopTime').FindOccurrence().Epoch
            avtr2.Propagate()

            countstring = str(count)

            # Exports the second stage as an ephemeris file and sets the orbit's intial state as the ephemeris last point
            stage2.ExportTools.GetEphemerisStkExportTool().Export(folder_path + '//2ndStage' + countstring + '.e')
            initStateCmd = r'InitialState */Satellite/Orbit Import STKFile "'+ folder_path + r'\2ndStage' + countstring + '.e" EpochSelect LastPoint'
            root.ExecuteCommand(initStateCmd)
            sat.Propagator.Propagate()
            sat.ExportTools.GetEphemerisStkExportTool().Export(folder_path + '//Orbit' + countstring + '.e')
            
            # Grabs outputs for the excel table
            dp = sat.DataProviders.Item('Classical Elements').Group.Item('ICRF').Exec(scenario.StartTime, scenario.StopTime, 60)
            Time = dp.DataSets.GetDataSetByName('Time').GetValues()
            apogeeAlt = dp.DataSets.GetDataSetByName('Apogee Altitude').GetValues()
            perigeeAlt = dp.DataSets.GetDataSetByName('Perigee Altitude').GetValues()
            inclination = dp.DataSets.GetDataSetByName('Inclination').GetValues()

            # Adds results to array
            perigeeResults.append(perigeeAlt[0])
            apogeeResults.append(apogeeAlt[0])
            inclinationResults.append(inclination[0])
            fuelStateInputs.append(fuelState2)
            launchAzimuthInputs.append(launchAzimuth)
            fpaDotInputs.append(FPADot)
            payloadInputs.append(payload)

            # Prints results of each run
            print('run ' + countstring + ' resulted in ' + str(perigeeAlt[0]) + 
                  ' km perigee, ' + str(apogeeAlt[0]) + ' km apogee, and '+ str(inclination[0]) + 
                  ' inclination with a fuel state of ' + str(fuelState2) + ' lbs ' + 'using a Launch Azimuth of ' + 
                  str(launchAzimuth) + ' and an FPADot of ' + str(FPADot) + ' carrying ' + str(payload) + ' lbs of payload\n')
            # Checks if a valid orbit Valid = not going below 100 km
            if perigeeAlt[0] < 100:
                orbitValid.append(False)
            else:
                orbitValid.append(True)
            count += 1

combinedPath = folder_path + '\\test.csv'

### write csv for satellite collection
f = open(combinedPath, "w")
f.write("Version		 1.0\n\n")
f.write("ContentType EphemerisFile\n\n")
f.write("Begin Columns\n")
f.write("Name, Filename\n")
for y in range (fuelRuns * azRuns * fpaRuns):
    z = y+1
    f.write("Sat" + str(z) + ", Orbit" + str(z) + ".e\n")

f.write("End Columns\n")
f.close()

satcol = scenario.Children.New(AgESTKObjectType.eSatelliteCollection, 'Results')
satcolCmd = 'Collection */SatelliteCollection/Results Define Custom Propagator "J2Perturbation" File ' + combinedPath + ' EphemerisDirectory "' + folder_path + '"'
root.ExecuteCommand(satcolCmd)
satcolGraphicsCmd = 'Graphics */SatelliteCollection/Results SubsetGfx Subset AllSatellites ShowOrbits On'
root.ExecuteCommand(satcolGraphicsCmd)

# Create a dictionary where keys are column names and values are the lists
data = {'Payload': payloadInputs, 'Fuel Carried Stage 2': fuelStateInputs, 'FPA Dot': fpaDotInputs, 'Launch Azimuth': launchAzimuthInputs, 'Perigee': perigeeResults, 'Agogee': apogeeResults, 'Inclination': inclinationResults, 'Orbit Valid?': orbitValid}

# Create a Pandas DataFrame from the dictionary
df = pd.DataFrame(data)

# Write the DataFrame to an Excel file
df.to_excel(folder_path +'\\LVAnalysis.xlsx', index=False) # index=False prevents writing the index

print("Excel file created successfully.")

# Save Scenario
saveCmd = r'SaveAs / * "' + scenario_folder + r'\LaunchVehicleAnalysis"'
root.ExecuteCommand(saveCmd)
print('Scenario saved\nEnd of Script')
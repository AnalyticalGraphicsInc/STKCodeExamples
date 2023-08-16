# Import main library
from comtypes.client import CreateObject

### Start New Instance ###
stk = CreateObject('STK12.Application')

# Get the IAgStkObjectRoot Interface
root = stk.Personality2
from comtypes.gen import STKObjects
from comtypes.gen import STKUtil
from comtypes.gen import AgSTKVgtLib
import math

# Get the scenario object
root.NewScenario('GEOGridExample')

scenario = root.CurrentScenario
scenarioObj = scenario.QueryInterface(STKObjects.IAgScenario)

# Create sample satellites and sensors

sat1 = scenario.Children.New(STKObjects.eSatellite,'Sat1')
root.ExecuteCommand('OrbitWizard */Satellite/Sat1 Geosynchronous Inclination 0 SubsatellitePoint 120.0')

sat2 = scenario.Children.New(STKObjects.eSatellite,'Sat2')
root.ExecuteCommand('OrbitWizard */Satellite/Sat2 Geosynchronous Inclination 0 SubsatellitePoint 0.0')

sat3 = scenario.Children.New(STKObjects.eSatellite,'Sat3')
root.ExecuteCommand('OrbitWizard */Satellite/Sat3 Geosynchronous Inclination 0 SubsatellitePoint -120.0')

sens1 = sat1.Children.New(STKObjects.eSensor,'Sens1')
sens1Obj = sens1.QueryInterface(STKObjects.IAgSensor)
sens1Obj.SetPatternType(STKObjects.eSnRectangular)
sens1Pattern = sens1Obj.Pattern.QueryInterface(STKObjects.IAgSnRectangularPattern)
sens1Pattern.HorizontalHalfAngle = 5
sens1Pattern.VerticalHalfAngle = 5
sens1Pointing = sens1Obj.Pointing.QueryInterface(STKObjects.IAgSnPtFixed)
sens1Pointing.Orientation.AssignAzEl(4,0,0)

sens2 = sat2.Children.New(STKObjects.eSensor,'Sens2')
sens2Obj = sens2.QueryInterface(STKObjects.IAgSensor)
sens2Obj.SetPatternType(STKObjects.eSnRectangular)
sens2Pattern = sens2Obj.Pattern.QueryInterface(STKObjects.IAgSnRectangularPattern)
sens2Pattern.HorizontalHalfAngle = 5
sens2Pattern.VerticalHalfAngle = 5
sens2Pointing = sens2Obj.Pointing.QueryInterface(STKObjects.IAgSnPtFixed)
sens2Pointing.Orientation.AssignAzEl(4,0,0)

sens3 = sat3.Children.New(STKObjects.eSensor,'Sens3')
sens3Obj = sens3.QueryInterface(STKObjects.IAgSensor)
sens3Obj.SetPatternType(STKObjects.eSnRectangular)
sens3Pattern = sens3Obj.Pattern.QueryInterface(STKObjects.IAgSnRectangularPattern)
sens3Pattern.HorizontalHalfAngle = 5
sens3Pattern.VerticalHalfAngle = 5
sens3Pointing = sens3Obj.Pointing.QueryInterface(STKObjects.IAgSnPtFixed)
sens3Pointing.Orientation.AssignAzEl(4,0,0)

# Create initial volume grid
earth = root.CentralBodies.Item('Earth').QueryInterface(STKObjects.IAgStkCentralBody)
root.ExecuteCommand("SpatialTool * CentralBody/Earth Create \"Volume Grid\" GEOGrid \"Cartographic\" Latitude FixedNumberSteps Minimum -90 Maximum 90 NumSteps 20 Longitude FixedNumberSteps Minimum 0 Maximum 360 NumSteps 36 Altitude FixedNumberSteps Minimum 34000000 Maximum 37000000 NumSteps 10")
geogrid = earth.Vgt.VolumeGrids.GetItemByName('GEOGrid')

# Create volumetric constraint
condition = earth.Vgt.Volumes.Factory.CreateVolumeCombined('MultiSensorViz','Description here').QueryInterface(AgSTKVgtLib.IAgCrdnVolumeCombined)
condition.CombineOperation = AgSTKVgtLib.eCrdnVolumeCombinedOperationTypeOR

sensor1Condition = sens1.Vgt.Volumes.Item('Visibility')
sensor2Condition = sens2.Vgt.Volumes.Item('Visibility')
sensor3Condition = sens3.Vgt.Volumes.Item('Visibility')

condition.SetAllConditions([sensor1Condition, sensor2Condition, sensor3Condition])

# Create constrained grid
constrainedGrid = earth.Vgt.VolumeGrids.Factory.CreateVolumeGridConstrained('ConstrainedGrid','Description here').QueryInterface(AgSTKVgtLib.IAgCrdnVolumeGridConstrained)
constrainedGrid.Constraint = condition
constrainedGrid.ReferenceGrid = geogrid

# Create volumetric object
volumetric = scenario.Children.New(STKObjects.eVolumetric,'VolumetricCalc')
volumetricObj = volumetric.QueryInterface(STKObjects.IAgVolumetric)
volumeGridDef = volumetricObj.VolumeGridDefinition.QueryInterface(STKObjects.IAgVmGridSpatialCalculation)
volumeGridDef.VolumeGrid = 'CentralBody/Earth ConstrainedGrid'

volumetricObj.Compute()

# Pull out volumetric data
dataProvider = volumetric.DataProviders.Item('Satisfaction Volume').QueryInterface(STKObjects.IAgDataPrvTimeVar)
data = dataProvider.Exec(scenarioObj.StartTime,scenarioObj.StopTime,60)
array = data.DataSets.ToArray()

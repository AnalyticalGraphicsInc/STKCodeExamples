# Import main library
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.vgt import *

### Start New Instance ###
stk = STKDesktop.StartApplication(visible=True)
#stk = STKDesktop.AttachToApplication()

# Get the IAgStkObjectRoot Interface
root = stk.Root

# Get the scenario object
root.NewScenario('GEOGridExample')

scenario = root.CurrentScenario

# Create sample satellites and sensors

sat1 = IAgSatellite(scenario.Children.New(AgESTKObjectType.eSatellite,'Sat1'))
root.ExecuteCommand('OrbitWizard */Satellite/Sat1 Geosynchronous Inclination 0 SubsatellitePoint 120.0')

sat2 = IAgSatellite(scenario.Children.New(AgESTKObjectType.eSatellite,'Sat2'))
root.ExecuteCommand('OrbitWizard */Satellite/Sat2 Geosynchronous Inclination 0 SubsatellitePoint 0.0')

sat3 = IAgSatellite(scenario.Children.New(AgESTKObjectType.eSatellite,'Sat3'))
root.ExecuteCommand('OrbitWizard */Satellite/Sat3 Geosynchronous Inclination 0 SubsatellitePoint -120.0')

sens1 = IAgSensor(IAgStkObject(sat1).Children.New(AgESTKObjectType.eSensor,'Sens1'))
sens1.SetPatternType(AgESnPattern.eSnRectangular)
IAgSnRectangularPattern(sens1.Pattern).HorizontalHalfAngle = 5
IAgSnRectangularPattern(sens1.Pattern).VerticalHalfAngle = 5
IAgSnPtFixed(sens1.Pointing).Orientation.AssignAzEl(4,0,0)

sens2 = IAgSensor(IAgStkObject(sat2).Children.New(AgESTKObjectType.eSensor,'Sens2'))
sens2.SetPatternType(AgESnPattern.eSnRectangular)
IAgSnRectangularPattern(sens2.Pattern).HorizontalHalfAngle = 5
IAgSnRectangularPattern(sens2.Pattern).VerticalHalfAngle = 5
IAgSnPtFixed(sens2.Pointing).Orientation.AssignAzEl(4,0,0)

sens3 = IAgSensor(IAgStkObject(sat3).Children.New(AgESTKObjectType.eSensor,'Sens3'))
sens3.SetPatternType(AgESnPattern.eSnRectangular)
IAgSnRectangularPattern(sens3.Pattern).HorizontalHalfAngle = 5
IAgSnRectangularPattern(sens3.Pattern).VerticalHalfAngle = 5
IAgSnPtFixed(sens3.Pointing).Orientation.AssignAzEl(4,0,0)

# Create initial volume grid
earth = IAgStkCentralBody(root.CentralBodies.Item('Earth'))
geogrid = IAgCrdnVolumeGridLatLonAlt(earth.Vgt.VolumeGrids.Factory.CreateVolumeGridLatLonAlt('GEOGrid','Description here'))
latmin = root.ConversionUtility.NewQuantity('Angle','deg',-5)
latmax = root.ConversionUtility.NewQuantity('Angle','deg',5)
lonmin = root.ConversionUtility.NewQuantity('Angle','deg',0)
lonmax = root.ConversionUtility.NewQuantity('Angle','deg',360)
altmin = root.ConversionUtility.NewQuantity('Distance','km',34000)
altmax = root.ConversionUtility.NewQuantity('Distance','km',37000)
geogrid.AltitudeCoordinates.SetGridValuesFixedNumberOfStepsEx(altmin,altmax,10)
geogrid.LatitudeCoordinates.SetGridValuesFixedNumberOfStepsEx(latmin,latmax,11)
geogrid.LongitudeCoordinates.SetGridValuesFixedNumberOfStepsEx(lonmin,lonmax,60)

# Create volumetric constraint
condition = IAgCrdnVolumeCombined(earth.Vgt.Volumes.Factory.CreateVolumeCombined('MultiSensorViz','Description here'))
condition.CombineOperation = AgECrdnVolumeCombinedOperationType.eCrdnVolumeCombinedOperationTypeOR

sensor1Condition = IAgStkObject(sens1).Vgt.Volumes.Item('Visibility')
sensor2Condition = IAgStkObject(sens2).Vgt.Volumes.Item('Visibility')
sensor3Condition = IAgStkObject(sens3).Vgt.Volumes.Item('Visibility')

condition.SetAllConditions([sensor1Condition, sensor2Condition, sensor3Condition])

# Create constrained grid
constrainedGrid = IAgCrdnVolumeGridConstrained(earth.Vgt.VolumeGrids.Factory.CreateVolumeGridConstrained('ConstrainedGrid','Description here'))
constrainedGrid.Constraint = condition
constrainedGrid.ReferenceGrid = geogrid

# Create volumetric object
volumetric = IAgVolumetric(scenario.Children.New(AgESTKObjectType.eVolumetric,'VolumetricCalc'))
IAgVmGridSpatialCalculation(volumetric.VolumeGridDefinition).VolumeGrid = 'CentralBody/Earth ConstrainedGrid'

volumetric.Compute()

# Pull out volumetric data
data = IAgStkObject(volumetric).DataProviders.Item('Satisfaction Volume').Exec(scenario.StartTime,scenario.StopTime,60)
array = data.DataSets.ToArray()

uiapp = actxserver('STK12.Application');
root = uiapp.Personality2;
uiapp.visible = 1;    

%create a new scenario and specify the time
try
    root.CloseScenario();
    root.NewScenario('SimulinkAttitudeControl');        
catch
    root.NewScenario('SimulinkAttitudeControl');        
end
   
%set the unit preferences
root.UnitPreferences.SetCurrentUnit('DateFormat', 'UTCG');

root.CurrentScenario.SetTimePeriod('30 Jul 2013 20:45:00.000', '31 Jul 2013 10:00:00.000');
root.CurrentScenario.Epoch = '30 Jul 2013 20:45:00.000';

%create the target objects
target1 = root.CurrentScenario.Children.New('eTarget', 'Boston');
target1.Position.AssignGeodetic(42.3586, -71.0567, 0.0);
target2 = root.CurrentScenario.Children.New('eTarget', 'WashingtonDC');
target2.Position.AssignGeodetic(38.8904, -77.032, 0.0);
target3 = root.CurrentScenario.Children.New('eTarget', 'NewYork');
target3.Position.AssignGeodetic(40.7143, -74.006, 0.0);
target4 = root.CurrentScenario.Children.New('eTarget', 'Charleston');
target4.Position.AssignGeodetic(32.7812, -79.9316, 0.0);
facility1 = root.CurrentScenario.Children.New('eFacility', 'CapeCanaveral');
facility1.Position.AssignGeodetic(28.5, -80.57, 0.0);

%create the perfect pointing satellite 
perfectPointingSat = root.CurrentScenario.Children.New('eSatellite','PerfectPointing');
perfectPointingSat.SetPropagatorType('ePropagatorTwoBody');
perfectPointingSat.Propagator.InitialState.Representation.AssignClassical('eCoordinateSystemICRF', 7059.14, 0, 98, 0, -2, 0);
perfectPointingSat.Propagator.EphemerisInterval.SetExplicitInterval('30 Jul 2013 12:00:00.000', '31 Jul 2013 10:00:00.000');
perfectPointingSat.Propagator.InitialState.OrbitEpoch = '30 Jul 2013 12:00:00.000';
perfectPointingSat.Propagator.Propagate;

%add a sensor to the perfect satellite to represent the camera fov
perfectSensor = perfectPointingSat.Children.New('eSensor', 'perfectCamera');
perfectSensor.SetPatternType('eSnRectangular');
pattern = perfectSensor.Pattern;
pattern.HorizontalHalfAngle = 2;
pattern.VerticalHalfAngle = 2;

%create the axes for downlink to Cape Canaveral
targetVector = perfectPointingSat.Vgt.Vectors.Factory.CreateDisplacementVector('ToCapeCanaveral', perfectPointingSat.Vgt.Points.Item('Center'), facility1.Vgt.Points.Item('Center'));
targetAxes = perfectPointingSat.Vgt.Axes.Factory.Create('TargetCC', '', 'eCrdnAxesTypeAlignedAndConstrained');
targetAxes.AlignmentReferenceVector.SetVector(targetVector);
targetAxes.ConstraintReferenceVector.SetVector(perfectPointingSat.Vgt.Vectors.Item('Velocity'));

%set up the pointing for the perfect pointing satellite
perfectPointingSat.SetAttitudeType('eAttitudeStandard')

root.ExecuteCommand('AttitudeSegment */Satellite/PerfectPointing Add Profile SunPointing "30 Jul 2013 12:01:00.000" SunNadir Offset 0.0');
root.ExecuteCommand('AttitudeSegment */Satellite/PerfectPointing Add Profile Nadir "30 Jul 2013 20:46:00.000" NadirECIVel Offset 0.0');
root.ExecuteCommand('AttitudeSegment */Satellite/PerfectPointing Add Profile Slew "30 Jul 2013 20:46:59.900" FixedTimeSlew Smooth On');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target On');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Times UseAccess Off');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Add Target/Boston');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Times Add Target/Boston "30 Jul 2013 20:47:00.000" "30 Jul 2013 20:48:15.000"');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Add Target/WashingtonDC');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Times Add Target/WashingtonDC "30 Jul 2013 20:48:15.000" "30 Jul 2013 20:49:30.000"');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Add Target/NewYork');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Times Add Target/NewYork "30 Jul 2013 20:49:30.000" "30 Jul 2013 20:50:30.000"');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Add Target/Charleston');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Times Add Target/Charleston "30 Jul 2013 20:50:30.000" "30 Jul 2013 20:51:30.000"');
root.ExecuteCommand('SetAttitude */Satellite/PerfectPointing Target Slew Mode FixedTime SlewTime 0.1');
root.ExecuteCommand('AttitudeSegment */Satellite/PerfectPointing Add Profile Slew2 "30 Jul 2013 20:51:30.000" FixedTimeSlew Smooth On');
root.ExecuteCommand('AttitudeSegment */Satellite/PerfectPointing Add Profile SunPointing2 "30 Jul 2013 20:51:30.100" SunNadir Offset 0.0');
root.ExecuteCommand('AttitudeSegment */Satellite/PerfectPointing Add Profile TargetCC "30 Jul 2013 20:52:30.100" Fixed Quat 0 0 0 1 "Satellite/PerfectPointing TargetCC"');
root.ExecuteCommand('AttitudeSegment */Satellite/PerfectPointing Add Profile Nadir "30 Jul 2013 20:55:00.000" NadirECIVel Offset 0.0');
perfectPointingSat.Propagator.Propagate;

%create the realtime sat and configure it's orbit
realSat = root.CurrentScenario.Children.New('eSatellite','Simulink');
realSat.SetPropagatorType('ePropagatorTwoBody');
realSat.Propagator.InitialState.Representation.AssignClassical('eCoordinateSystemICRF', 7059.14, 0, 98, 0, -2, 0);
realSat.Propagator.InitialState.OrbitEpoch = '30 Jul 2013 12:00:00.000';
realSat.SetAttitudeType('eAttitudeRealTime');
realAttitude = realSat.Attitude;
realAttitude.LookAheadMethod = 'eHold';
realSat.Propagator.Propagate;

%add a sensor to the simulink satellite to represent the camera fov
sensor = realSat.Children.New('eSensor', 'Camera');
sensor.SetPatternType('eSnRectangular');
pattern = sensor.Pattern;
pattern.HorizontalHalfAngle = 2;
pattern.VerticalHalfAngle = 2;

%Body Axes for the perfect pointing satellite
perfectBodyAxes = perfectPointingSat.Vgt.Axes.Item('Body');
voBodyAxes = perfectPointingSat.VO.Vector.RefCrdns.GetCrdnByName('eAxesElem', perfectBodyAxes.QualifiedPath)
voBodyAxes.Visible = 1;
perfectPointingSat.VO.Vector.VectorSizeScale = 0.7

%Sun Vector for the perfect pointing satellite
sunVector = perfectPointingSat.Vgt.Vectors.Item('Sun');
voSunVector = perfectPointingSat.VO.Vector.RefCrdns.GetCrdnByName('eVectorElem', sunVector.QualifiedPath)
voSunVector.Visible = 1;

%Body Axes for the simulink satellite
bodyAxes = realSat.Vgt.Axes.Item('Body');
voBodyAxes = realSat.VO.Vector.RefCrdns.GetCrdnByName('eAxesElem', bodyAxes.QualifiedPath)
voBodyAxes.Visible = 1;
voBodyAxes.Color = 255255255;
realSat.VO.Vector.VectorSizeScale = 0.7

%reset the time
root.Rewind;

%set the unit preferences
root.UnitPreferences.SetCurrentUnit('DateFormat', 'EpSec');
root.UnitPreferences.SetCurrentUnit('AngleUnit', 'rad');

stkParameters = cell(5,1);
stkParameters{1} = uiapp;
stkParameters{2} = root;
stkParameters{3} = realSat;
stkParameters{4} = perfectPointingSat;
stkParameters{5} = perfectBodyAxes;

%assign the structure we'll use later to the UserData
set_param(gcb,'UserData', stkParameters);  
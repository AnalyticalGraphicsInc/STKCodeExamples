%% AttitudeUsingAScheduledVector
% Description: Sets up a scheduled vector in STK and builds all of the
% conditions
% Author: Austin Claybrook
% Date Created: 9/28/18
% Date Last Modified: 05/21/2021 by Alexander Lam
% Outputs: Builds the AWB components and sets up the attitude in STK
% Instructions: Open STK, Create a satellite, Adjust the inputs, click Run
% Note: Works for STK 12.0 and onward
%
% The satellite will slew the Body X to the pointingAng defined below when 
% the lat rate is positive and the satellite is above the aboveDeg 
% parameter set below. 

clear
clc

%% Inputs
satName = 'Satellite3'; % Satellite name in STK
aboveDeg = 20; % Latitude to start changing the attitude
pointingAng = 225; % Pointing angle relative to zenith, about the orbit normal
slewDur = 120; % Slew duration [sec]
slewMinDur = 10; % Minimum time spent in off schedule [sec]

%% Code
% Connect to Running Instance of STK
app = actxGetRunningServer('STK12.application');

% Handles
root = app.Personality2;
satPath = ['*/Satellite/',satName];
sat = root.GetObjectFromPath(satPath);
cs = sat.Vgt.CalcScalars;
csFac = cs.Factory;
con = sat.Vgt.Conditions;
conFac = con.Factory;
vec = sat.Vgt.Vectors;
vecFac = vec.Factory;

% Create Components
try
    lat = csFac.Create('Lat','Description','eCrdnCalcScalarTypeDataElement');
    latRate = csFac.Create('LatRate','Description','eCrdnCalcScalarTypeDataElement');
    AboveAng = conFac.Create('AboveAng','Description','eCrdnConditionTypeScalarBounds');
    Increasing = conFac.Create('Increasing','Description','eCrdnConditionTypeScalarBounds');
catch
    lat = cs.Item('Lat');
    latRate = cs.Item('LatRate');
    AboveAng = con.Item('AboveAng');
    Increasing = con.Item('Increasing');
end

% Set Properties For the Components
lat.SetWithGroup('LLR State','Fixed','Lat');
latRate.SetWithGroup('LLR State','Fixed','Lat Rate');
AboveAng.Scalar = lat;
% minVal = root.ConversionUtility.NewQuantity('Angle', 'deg', aboveDeg);
minVal = root.ConversionUtility.NewQuantity('Latitude', 'deg', aboveDeg);
AboveAng.SetMinimum(minVal)
Increasing.Scalar = latRate;
try
    root.ExecuteCommand(['CalculationTool * Satellite/',satName,' Create "Condition" AboveDegAndIncreasing "Combined"']);
catch
end
root.ExecuteCommand(['CalculationTool * Satellite/',satName,' Modify "Condition" AboveDegAndIncreasing "Combined" Conditions Add 1 "Satellite/',satName,' AboveAng Condition"']);
root.ExecuteCommand(['CalculationTool * Satellite/',satName,' Modify "Condition" AboveDegAndIncreasing "Combined" Conditions Add 1 "Satellite/',satName,' Increasing Condition"']);
root.ExecuteCommand(['CalculationTool * Satellite/',satName,' Modify "Condition" AboveDegAndIncreasing "Combined" RemoveCondition 1']);
root.ExecuteCommand(['CalculationTool * Satellite/',satName,' Modify "Condition" AboveDegAndIncreasing "Combined" RemoveCondition 1']);

try
    root.ExecuteCommand(['TimeTool * Satellite/',satName,' Create "Interval List" NotAboveDegAndIncreasing "Merged"']);
catch
end

% Create an Interval List when the conditions are not met
root.ExecuteCommand(['TimeTool * Satellite/',satName,' Modify "Interval List" NotAboveDegAndIncreasing "Merged" Operation AND']);
root.ExecuteCommand(['TimeTool * Satellite/',satName,' Modify "Interval List" NotAboveDegAndIncreasing "Merged" Intervals Add 1 "Satellite/',satName,' AvailabilityIntervals Interval List"']);
root.ExecuteCommand(['TimeTool * Satellite/',satName,' Modify "Interval List" NotAboveDegAndIncreasing "Merged" Intervals Add 1 "Satellite/',satName,' AboveDegAndIncreasing.SatisfactionIntervals Interval List"']);
root.ExecuteCommand(['TimeTool * Satellite/',satName,' Modify "Interval List" NotAboveDegAndIncreasing "Merged" RemoveInterval 1']);
root.ExecuteCommand(['TimeTool * Satellite/',satName,' Modify "Interval List" NotAboveDegAndIncreasing "Merged" RemoveInterval 1']);
root.ExecuteCommand(['TimeTool * Satellite/',satName,' Modify "Interval List" NotAboveDegAndIncreasing "Merged" Operation MINUS']);

% Create the Pointing Vector
try
    root.ExecuteCommand(['VectorTool * Satellite/',satName,' Create Vector PointingVec "Fixed in Axes"']);
catch
end
root.ExecuteCommand(['VectorTool * Satellite/',satName,' Modify Vector PointingVec "Fixed in Axes" Spherical ',num2str(pointingAng),' 0 1 "Satellite/',satName,' RIC"']);

% Create Scheduled Vector
try
    root.ExecuteCommand(['VectorTool * Satellite/',satName,' Create Vector AttitudeVec "Scheduled"']);
catch
end
root.ExecuteCommand(['VectorTool * Satellite/',satName,' Modify Vector AttitudeVec "Scheduled" Schedule "Satellite/',satName,' NotAboveDegAndIncreasing Interval List"']);
root.ExecuteCommand(['VectorTool * Satellite/',satName,' Modify Vector AttitudeVec "Scheduled" OffSchedule "Satellite/',satName,' PointingVec"']);
root.ExecuteCommand(['VectorTool * Satellite/',satName,' Modify Vector AttitudeVec "Scheduled" OnSchedule "Satellite/',satName,' Nadir(Detic)"']);
root.ExecuteCommand(['VectorTool * Satellite/',satName,' Modify Vector AttitudeVec "Scheduled" SlewWindowDuration ',num2str(slewDur)]);
root.ExecuteCommand(['VectorTool * Satellite/',satName,' Modify Vector AttitudeVec "Scheduled" SlewMinOffSchedule ',num2str(slewMinDur)]);

% Set the Attitude
sat.Attitude.Basic.SetProfileType('eProfileAlignedAndConstrained');
sat.Attitude.Basic.Profile.AlignedVector.Body.AssignXYZ(1,1,0); % Do this, so the attitude vectors are not pointing in the same direction
sat.Attitude.Basic.Profile.ConstrainedVector.Body.AssignXYZ(0,1,1); % Do this, so the attitude vectors are not pointing in the same direction
sat.Attitude.Basic.Profile.AlignedVector.Body.AssignXYZ(0,0,1); % Assign Desired values
sat.Attitude.Basic.Profile.ConstrainedVector.Body.AssignXYZ(1,0,0); % Assign Desired values
sat.Attitude.Basic.Profile.AlignedVector.ReferenceVector = ['Satellite/',satName,' Orbit_Normal'];
sat.Attitude.Basic.Profile.ConstrainedVector.ReferenceVector = ['Satellite/',satName,' AttitudeVec'];

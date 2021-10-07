function [dcm,quaternion] = getDCM(objectName,axesName,referenceAxes,time)
% ex. getSTKDCM('Satellite1','Body','CentralBody/Earth J2000','15 Apr 2021 18:00:30.000')
%% Get Active STK Scenario
% Change depending on your version
app = actxGetRunningServer('STK12.application');
%app = actxGetRunningServer('STK11.application');

% Grab the root and scenario objects
root = app.Personality2;
scenario = root.CurrentScenario;

%% Grab Object
object = scenario.Children.Item(objectName);

%% Grab Data Provider
dp = object.DataProviders.Item('Axes Choose Axes').Group.Item(axesName);
dp.PreData = referenceAxes;
quatData = dp.ExecSingleElementsArray({time},{'q1';'q2';'q3';'q4'});
quaternion = cell2mat([quatData.GetArray(int32(0)) quatData.GetArray(int32(1)) quatData.GetArray(int32(2)) quatData.GetArray(int32(3))]);
q0 = quaternion(1);
q1 = quaternion(2);
q2 = quaternion(3);
q3 = quaternion(4);

%% Compute DCM
dcm = [ q0^2+q1^2-q2^2-q3^2 2*(q1*q2+q0*q3) 2*(q1*q3-q0*q2);...
        2*(q1*q2-q0*q3) q0^2-q1^2+q2^2-q3^2 2*(q2*q3+q0*q1);...
        2*(q1*q3+q0*q2) 2*(q2*q3-q0*q1) q0^2-q1^2-q2^2+q3^2];
end
clear all
clc

format long g

% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK12.application');

% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;

% Get handle for scenario
scenario = root.CurrentScenario;

% Change time units to Epoch Seconds
root.UnitPreferences.SetCurrentUnit('DateFormat','EpSec');

% Compute handle for satellite
sat = root.GetObjectFromPath('/Satellite/Satellite1');

% Apply first attitude profile using SetAttitude Connect command and
% subsequent profiles using the AddAttitude command
root.ExecuteCommand('SetAttitude */Satellite/Satellite1 File Filename "C:\Users\<username>\Desktop\<filename>.a"')
root.ExecuteCommand('SetAttitude */Satellite/Satellite1 Profile InertFix Euler 10.0 20.0 30.0 321')
root.ExecuteCommand('AddAttitude */Satellite/Satellite1 Profile "14 May 2019 20:00:00.000" FixedTimeSlew Smooth On')
root.ExecuteCommand('AddAttitude */Satellite/Satellite1 Profile "14 May 2019 21:00:00.000" SunNadir 0')
root.ExecuteCommand('SetAttitude */Satellite/Satellite1 Target ADD Facility/Facility1')

%Clear a profile
root.ExecuteCommand('SetAttitude */Satellite/Satellite1 ClearData Profile FixedTimeSlew');




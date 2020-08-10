clear all; close all; clc

%% Variables

STKVersion = '12'; % Version of STK you are using, input as a string
startTime = '01 Jul 2010 18:00:00.000';
stopTime = '02 Jul 2010 18:00:00.000';
launchLat = 15; % Lat where missile launches from
launchLon = 0; % Lon where missile launches from
impactLat = 80; % Lat where missile impacts
impactLon = -120; % Lon where missile impacts
stepValue = 60; % in seconds, used for step value of pulling out Data Providers

%% Script

% Start a new STK scenario named 'Example_Scenario' with start and stop
% times

app = actxserver(append('STK',STKVersion,'.Application'));
root = app.Personality2;
scenario = root.Children.New('eScenario','Example_Scenario');
scenario.SetTimePeriod(startTime,stopTime);
scenario.StartTime = startTime;
scenario.StopTime = stopTime;

% Reset animation time
root.ExecuteCommand('Animate * Reset');

% Add a new missile to the scenario
if scenario.Children.Contains('eMissile','Missile')
    missile = scenario.Children.Item('Missile');
else
    missile = scenario.Children.New('eMissile','Missile');
end

% Create the trajectory for the missile and then propagate
Traj = missile.Trajectory;
Traj.Launch.Lat = launchLat;
Traj.Launch.Lon = launchLon;
Traj.ImpactLocation.SetImpactType(0);
Traj.ImpactLocation.Impact.Lat = impactLat;
Traj.ImpactLocation.Impact.Lon = impactLon;
missile.Trajectory.Propagate();

% Go into the Ground Range and Fixed folders for the Data Provider
MissileDP = missile.DataProviders.Item('Ground Range').Group.Item('Fixed');

% Execute the Time Varying command with Start, Stop, and Step Values
missileResults = MissileDP.Exec(scenario.StartTime,scenario.StopTime,stepValue);

% Pull out Ground Range and Alt
GroundRange = cell2mat(missileResults.DataSets.GetDataSetByName('Ground Range').GetValues());
Alt = cell2mat(missileResults.DataSets.GetDataSetByName('Alt').GetValues());

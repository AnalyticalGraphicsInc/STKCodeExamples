% This is a standalone script that demonstrates the use of sensor pointing
% and retrieval of AER data using the Object Model. A new STK scenario is
% created with 2 aircraft and a sensor on the first aircraft. The sensor is
% set to target the second aircraft, and access is computed. Time, Az, and
% El data of the access is loaded into Matlab matrices.

close all
clear all
clc

%Initialize 
%Establish the connection
try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
catch
    % STK is not running, launch new instance
    % Launch a new instance of STK12 and grab it
    uiapp = actxserver('STK12.application');
end

%get the root from the personality
%it has two... get the second, its the newer STK Object Model Interface as
%documented in the STK Help
root = uiapp.Personality2;

% set visible to true (show STK GUI)
uiapp.visible = 1;

%%From the STK Object Root you can command every aspect of the STK GUI
%close current scenario or open new one
try
    root.CloseScenario();
    root.NewScenario('Test');
catch
    root.NewScenario('Test');
end

scen = root.CurrentScenario;
scen.SetTimePeriod('01 May 2012 12:00:00.000', '02 May 2012 12:00:00.000');
root.Rewind;

%Create main aircraft and it's route, plus one target aircraft
ac1 = root.CurrentScenario.Children.New('eAircraft', 'ac1');
ac1.SetRouteType('ePropagatorGreatArc');
ac1Prop = ac1.Route;
wpt1 = ac1Prop.Waypoints.Add();
wpt1.Latitude = 0;
wpt1.Longitude = 10;
wpt1.Altitude = 3;
wpt2 = ac1Prop.Waypoints.Add();
wpt2.Latitude = 5;
wpt2.Longitude = 25;
wpt2.Altitude = 3;
ac1Prop.Propagate()

%aircraft 2
ac2 = root.CurrentScenario.Children.New('eAircraft', 'ac2');
ac2.SetRouteType('ePropagatorGreatArc');
ac2Prop = ac2.Route;
wpt1 = ac2Prop.Waypoints.Add();
wpt1.Latitude = 0.5;
wpt1.Longitude = 10.2;
wpt1.Altitude = 3;
wpt2 = ac2Prop.Waypoints.Add();
wpt2.Latitude = 5.5;
wpt2.Longitude = 25.2;
wpt2.Altitude = 3;
ac2Prop.Propagate

%add a sensor to the main aircraft
tsen1 = ac1.Children.New('eSensor', 'Targeted');
pattern1 = tsen1.Pattern;
pattern1.ConeAngle = 5;
tsen1.SetPointingType('eSnPtTargeted');

%set up the pointing properties for the sensor
pointing1 = tsen1.Pointing;
pointing1.Targets.AddObject(ac2);

%% Generate AER data
%Set up the access object from ac1 to ac2
access = ac1.GetAccessToObject(ac2);
access.ComputeAccess;

% setup AER and get Az, El
accessAER = access.DataProviders.Item('AER Data').Group.Item('BodyFixed').Exec(scen.StartTime, scen.StopTime,60);
AERTimes = cell2mat(accessAER.Interval.Item(0).DataSets.GetDataSetByName('Time').GetValues);
Az = cell2mat(accessAER.Interval.Item(0).DataSets.GetDataSetByName('Azimuth').GetValues);
El = cell2mat(accessAER.Interval.Item(0).DataSets.GetDataSetByName('Elevation').GetValues);
for i = 1:1:accessAER.Interval.Count-1
    AERTimes = [AERTimes; cell2mat(accessAER.Interval.Item(i).DataSets.GetDataSetByName('Time').GetValues)];
    Az = [Az; cell2mat(accessAER.Interval.Item(i).DataSets.GetDataSetByName('Azimuth').GetValues)];
    El = [El; cell2mat(accessAER.Interval.Item(i).DataSets.GetDataSetByName('Elevation').GetValues)];
end
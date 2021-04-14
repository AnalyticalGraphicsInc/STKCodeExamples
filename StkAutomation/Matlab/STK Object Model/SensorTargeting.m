close all;
clear;
clc;

%% Initiate STK

% Create an instance of STK
uiApplication = actxserver('STK12.Application');
uiApplication.Visible = 1;

% Get the IAgStkObjectRoot interface
root = uiApplication.Personality2;

%% Scenario Setup

% Create scenario
root.NewScenario('SensorTargeting');
scenario = root.CurrentScenario;

% IAgScenario scenario: Scenario object
scenario.SetTimePeriod('17 Apr 2020 16:00:00.000', '18 Apr 2020 16:00:00.000');
root.Rewind();

%% Create the Satellite

satellite = root.CurrentScenario.Children.New('eSatellite', 'MySatellite');
keplerian = satellite.Propagator.InitialState.Representation.ConvertTo('eOrbitStateClassical'); % Use the Classical Element interface
keplerian.SizeShapeType = 'eSizeShapeSemimajorAxis';  % Makes sure Semimajor Axis, Eccentricity, and Inclination are used
keplerian.LocationType = 'eLocationTrueAnomaly'; % Makes sure True Anomaly is being used
keplerian.Orientation.AscNodeType = 'eAscNodeRAAN'; % Use RAAN for data entry

% Assign the perigee and apogee altitude values:
keplerian.SizeShape.SemiMajorAxis = 6878.14;      % km
keplerian.SizeShape.Eccentricity = 0;

% Assign the other desired orbital parameters:
keplerian.Orientation.Inclination = 45;         % deg
keplerian.Orientation.ArgOfPerigee = 0;        % deg
keplerian.Orientation.AscNode.Value = -60;       % deg
keplerian.Location.Value = 0;                 % deg

% Apply the changes made to the satellite's state and propagate:
satellite.Propagator.InitialState.Representation.Assign(keplerian);
satellite.Propagator.Propagate();
 
%% Create Facilities
latitudes = [44.2794; 36.1695; 48.2521];
longitudes = [-89.8195; -117.596; -55.7671];

for i = 1:length(latitudes)
   facility = scenario.Children.New('eFacility', ['Facility', num2str(i)]);
   % Set color to yellow
   facility.Graphics.Color = 65535;
   
   % IAgFacility facility: Facility Object
   facility.Position.AssignGeodetic(latitudes(i), longitudes(i), 0);
   % Set altitude to height of terrain
   facility.UseTerrain = true;
end

%% Create and Modify the Sensor
sensor = satellite.Children.New('eSensor', 'MySensor');
sensor.CommonTasks.SetPatternSimpleConic(10, 0.1); % Sets a Cone Angle of 10 degrees and an angular resolution of 0.1 degrees

%% Set Up Sensor Targeting

% Sets the sensor pointing type to Targeted which is index (5)
sensor.SetPointingType(5);

% Get handle to the Targets Interface
TargetPointing = sensor.Pointing.Targets;

% Use the Add method to add each facility into the pointing targets
for i = 1:3
TargetPointing.Add(['Facility/Facility', num2str(i)]);
end

%% Indicate Specific Target Times

% Disable the option to use access times for targeting intervals. By
% default access times are used to determine pointing times for each target
sensor.Pointing.EnableAccessTimes = 0;

% Remove all previous target intervals
sensor.Pointing.ScheduleTimes.RemoveAll();

% Set the Schedule Times for each target for the sensor.
TargetTimes = sensor.Pointing.ScheduleTimes;
TargetTimes.Add('17 Apr 2020 16:10:00.000', '17 Apr 2020 16:15:00.000', 'Facility/Facility2');
TargetTimes.Add('17 Apr 2020 16:15:00.000', '17 Apr 2020 16:21:00.000', 'Facility/Facility1');
TargetTimes.Add('17 Apr 2020 16:21:00.000', '17 Apr 2020 16:27:00.000', 'Facility/Facility3');
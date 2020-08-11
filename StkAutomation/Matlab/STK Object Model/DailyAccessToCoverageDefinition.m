%% Setup
clear all; close all; clc;

app=actxserver('STK11.application')
app.UserControl=1
root=app.personality2


%Define scenario name, time interval, and reset animation
scenario=root.Children.New('eScenario','SateliiteCoverage');
scenario.SetTimePeriod('Today','+1days');
root.ExecuteCommand('Animate * Reset');

%% Define Satellite
Sat=scenario.Children.New('eSatellite','MySat');
keplerian = Sat.Propagator.InitialState.Representation.ConvertTo('eOrbitStateClassical'); % Use the Classical Element interface
keplerian.SizeShapeType = 'eSizeShapeAltitude';  % Changes from Ecc/Inc to Perigee/Apogee Altitude
keplerian.LocationType = 'eLocationTrueAnomaly'; % Makes sure True Anomaly is being used
keplerian.Orientation.AscNodeType = 'eAscNodeLAN'; % Use LAN instead of RAAN for data entry

% Assign the perigee and apogee altitude values:
keplerian.SizeShape.PerigeeAltitude = 500;      % km
keplerian.SizeShape.ApogeeAltitude = 600;       % km

% Assign the other desired orbital parameters:
keplerian.Orientation.Inclination = 45;         % deg
keplerian.Orientation.ArgOfPerigee = 0;        % deg
keplerian.Orientation.AscNode.Value = 0;       % deg
keplerian.Location.Value = 0;                 % deg

% Apply the changes made to the satellite's state and propagate:
Sat.Propagator.InitialState.Representation.Assign(keplerian);
Sat.Propagator.Propagate;

%% Define Coverage Definition and FOM
coverage = scenario.Children.New('eCoverageDefinition', 'MyCoverage');
coverage.Grid.BoundsType = 'eBoundsLatLonRegion';

coverage.Grid.Bounds.MinLatitude = -20; %deg lat
coverage.Grid.Bounds.MaxLatitude = 20; %deg lat
coverage.Grid.Bounds.MinLongitude = -20; %deg long
coverage.Grid.Bounds.MaxLongitude = 20; %deg long
resolution = coverage.Grid.Resolution;
resolution.LatLon=0.5;
coverage.AssetList.Add('Satellite/MySat');
coverage.ComputeAccesses;

fom=coverage.Children.New('eFigureOfMerit','NumDailyAccesses');
fom.SetDefinitionType('eFmNumberOfAccesses');
fom.Definition.SetComputeType('eMaxPerDay');

%% Pull FOM values

fomDP = fom.DataProviders.Item('Time Value By Point');
fomDP.PreData = '8 Nov 2018 05:00:00.000';
fomDP2 =fomDP.Exec;
pointLatitude = fomDP2.DataSets.GetDataSetByName('Latitude').GetValues;
pointLongitude = fomDP2.DataSets.GetDataSetByName('Longitude').GetValues;
pointFOMValue = fomDP2.DataSets.GetDataSetByName('FOM Value').GetValues;



%% Create scenario, add facility with sensor, add area target, add aircraft, add satellite
%  Compute access from facility to satellite, pull times of closest range for each access interval
%  Values stored in timeArray and magnitudeArray
% tic/toc displays elapsed time for comparison between Graphics and NoGraphics

clearvars;
clear all;
clc;
 
%%

tic

%% With Graphics
STKApplication = actxserver('STK12.application');
root = STKApplication.Personality2;

%% NoGraphics mode
% Before instantiating AgStkObjectRoot an instance of AgSTKXApplication or an STK X control must be created.
% This also requires a STKX license to be present
% STKXApplication = actxserver('STKX12.application');
% STKXApplication.NoGraphics = true;
% root = actxserver('AgStkObjects12.AgStkObjectRoot');

%% Create new scenario 
root.NewScenario('NoGraphicsModeTest'); 
scenario = root.CurrentScenario; 
% set time units 
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
root.ExecuteCommand('SetUnits / EpSec');
% Get units 
root.UnitPreferences.SetCurrentUnit('DateFormat','EpSec');
root.UnitPreferences.GetCurrentUnitAbbrv('DateFormat');
%root.ExecuteCommand('Animate * Reset');

%% Create Facility Object 
facility = scenario.Children.New('eFacility','MyFacility');
facility = root.GetObjectFromPath('*/Facility/MyFacility');

facility.Position.AssignPlanetodetic(40,-80,0);

%% Create Sensor and define Type 
sensor = facility.Children.New('eSensor','MySensor');
sensor.SetPatternType('eSnComplexConic');
sensor.CommonTasks.SetPatternComplexConic(50,90,0,90); 
% add constraint to sensor 
rangeConstraint = sensor.AccessConstraints.AddConstraint('eCstrRange');
rangeConstraint.EnableMax = true;
rangeConstraint. Max = 40; 

%% Create area target 
area = root.CurrentScenario.Children.New('eAreaTarget','MyArea');
area.AreaType = 'eEllipse'; 
ellipse = area.AreaTypeData; 
ellipse.SemiMajorAxis = 100; 
area.Position.AssignGeodetic(40,-80,0);

%% Create vehicle (aircraft) using GreatArc propagator 
aircraft = root.CurrentScenario.Children.New('eAircraft','MyAircraft');
aircraft.SetRouteType('ePropagatorGreatArc'); 
greatArcPropagator = aircraft.Route; 
% Create Aircraft Route 
waypoint1 = greatArcPropagator.Waypoints.Add();
waypoint1.Latitude = 39;
waypoint1.Longitude = -79;
waypoint1.Altitude = 10;

waypoint2 = greatArcPropagator.Waypoints.Add();
waypoint2.Latitude = 40;
waypoint2.Longitude = -80;
waypoint2.Altitude = 10; 
 
waypoint3 = greatArcPropagator.Waypoints.Add();
waypoint3.Latitude = 41;
waypoint3.Longitude = -81;
waypoint3.Altitude = 10;

greatArcPropagator.Propagate(); 
 
%% Create vehicle (satellite) using SPG4 propagator 
satellite = root.CurrentScenario.Children.New('eSatellite','MySatellite');
% satellite.SetPropagatorType('ePropagatorSGP4');
% propagator = satellite.Propagator; 
% propagator.CommonTasks.AddSegsFromOnlineSource('25544');
% propagator.Propagate();

% alternative code to set orbit with Keplerian elements
keplerian = satellite.Propagator.InitialState.Representation.ConvertTo('eOrbitStateClassical'); % Use the Classical Element interface
keplerian.SizeShapeType = 'eSizeShapeAltitude';  % Changes from Ecc/Inc to Perigee/Apogee Altitude
keplerian.LocationType = 'eLocationTrueAnomaly'; % Makes sure True Anomaly is being used
keplerian.Orientation.AscNodeType = 'eAscNodeLAN'; % Use LAN instead of RAAN for data entry

% Assign the perigee and apogee altitude values:
keplerian.SizeShape.PerigeeAltitude = 600;      % km
keplerian.SizeShape.ApogeeAltitude = 600;       % km

% Assign the other desired orbital parameters:
keplerian.Orientation.Inclination = 40;         % deg
keplerian.Orientation.ArgOfPerigee = 0;        % deg
keplerian.Orientation.AscNode.Value = 0;       % deg
keplerian.Location.Value = 0;                 % deg

% Apply the changes made to the satellite's state and propagate:
satellite.Propagator.InitialState.Representation.Assign(keplerian);
satellite.Propagator.Propagate;

% add access constraints to object 
lightingConstraint = satellite.AccessConstraints.AddConstraint('eCstrLighting');
lightingConstraint.Condition = 'eDirectSun';

%% Create and calculate access 
access = facility.GetAccessToObject(satellite);
access.ComputeAccess()

accessIntervals = access.ComputedAccessIntervalTimes;
accecssDataProvider = access.DataProviders.Item('AER Data').Group.Item('Default');
dataProviderElements = {'Time';'Azimuth';'Elevation';'Range'};

for i = 1:accessIntervals.Count    
    [start,stop] = accessIntervals.GetInterval(i-1);
    dataProviderResult = accecssDataProvider.ExecElements(start,stop,1,dataProviderElements);
    timeValues{i} = cell2mat(dataProviderResult.DataSets.GetDataSetByName('Time').GetValues); 
    azimuthValues{i} = cell2mat(dataProviderResult.DataSets.GetDataSetByName('Azimuth').GetValues); 
    elevationValues{i} = cell2mat(dataProviderResult.DataSets.GetDataSetByName('Elevation').GetValues); 
    rangeValues{i} = cell2mat(dataProviderResult.DataSets.GetDataSetByName('Range').GetValues);     
end 
 
% Create vector between objects 
vector = facility.Vgt.Vectors.Factory.Create('FromTo','Vector description','eCrdnVectorTypeDisplacement');
vector.Destination.SetPoint(satellite.Vgt.Points.Item('Center'));

% Visualize the vector 
% boresightVector = facility.VO.Vector.RefCrdns.Add('eVectorElem','Facility/MyFacility FromTo Vector');
% facility.VO.Vector.VectorSizeScale = 4.0; 
 
%% Get built in Calculation object from Analysis Workbench 
parameterSets = access.Vgt.ParameterSets.Item('From-To-AER(Body)');
% Get magnitude vector 
magnitude = parameterSets.EmbeddedComponents.Item('From-To-AER(Body).Cartesian.Magnitude');
% Get times of the minimum value for each access interval
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
minTimes = parameterSets.EmbeddedComponents.Item('From-To-AER(Body).Cartesian.Magnitude.TimesOfLocalMin');
timeArray = cell2mat(minTimes.FindTimes().Times);

for i = 1:size(timeArray,1)
    result = magnitude.Evaluate(cell2mat(timeArray(i)));
    magnitudeArray(i) = result.Value;
end 

toc
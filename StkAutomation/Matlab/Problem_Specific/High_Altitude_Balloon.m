%% High Altitude Balloon Modelling in STK

%% Summary
% This script takes all of the information that is associated with high
% altitude balloon definitions and creates random waypoints within the allotted
% variation range. Then the script creates a aircraft object in STK,
% writes these resulting waypoints for the aircraft's path, and propagates
% the route.

%% Outputs
% Results              =    A (Number_of_Waypoints x 4) matrix where       
%                           Results(i,1): Waypoint Latitude
%                           Results(i,2): Waypoint Longitude
%                           Results(i,3): Waypoint Altitude (Converted to
%                           kilometers)
%                           Results(i,4): Waypoint Speed (Converted to km/s)

%% Initialize 
% This section establishes a connection with an STK scenario.

close all;
clear all;
clc;

% Create an instance of STK %%%%%

uiApplication = actxserver('STK12.application');
uiApplication.Visible = 1;

%Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;

% Create a Scenario of STK %%%%%

scenario = root.Children.New('eScenario','High_Altitude_Balloon');

%% Global Variables

% ***** For Range_LA and Range_LO go to http://www.nhc.noaa.gov/gccalc.shtml
%
% Range_LA                     =    How many degrees of Latitude the
%                                             balloons can vary from the point of interest
% Range_LO                     =    How many degrees of Longitude the
%                                             balloons can vary from the point of interest
% Max_Speed_Change     =   An integer for the maximum change in speed of winds aloft that will cause the
%                                            balloons to move. Units in nautical miles per hour (knots)
% Min_Speed                   =    An integer for the lowest speed that the winds
%                                             can propagate the balloons. Units in nautical miles per hour (knots)
% First_Waypoint             =    A 2 x 1 matrix where x(1) is the Latitude and x(w) is the Longitude
%                                             of the first waypoint. This is the point of interest.
% Number_of_Waypoints  =    A single integer for the number of waypoints desired
% Starting_Altitude           =    A single integer for the disired altitude of
%                                            the balloon in feet (ft).
% Max_Altitude_Change   =    An integer for the maximum change in altitude. Expressed as an
%                                              even thousand foot (1000, 2000, 3000,95000...etc)
% Balloon_Name               =    A string with the balloon name.

Range_LA                      =       1.0; % Degrees (Approximately 60 Nautical Miles)
Range_LO                      =       1.0; % Degrees (Approximately 60 Nautical Miles)
Max_Speed_Change       =        20; % Knots
Min_Speed                     =         1; % Knots
First_Waypoint                =    [2.0, -50]; % [Latitude, Longitude]
Number_of_Waypoints           =        60; 
Starting_Altitude             =     70000; % Feet
Max_Altitude_Change     =      5000; % Feet
Balloon_Name                =   'Balloon_1';

%% Calculations

% Conversion Factors
  Feet_to_Km = 0.3048/1000;
  Knots_to_Km_per_Second = 1.852/3600;
  
% Random +/- matrix creation for Latitude
u = rand(Number_of_Waypoints, 1);
ind = u >= 0.5;
u(ind) = 1;
u(~ind) = -1;

% Random +/- matrix creation for Longitude
v = rand(Number_of_Waypoints, 1);
ind = v >= 0.5;
v(ind) = 1;
v(~ind) = -1;

% Random +/- matrix creation for Altitude
x = rand(Number_of_Waypoints, 1);
ind = x >= 0.5;
x(ind) = 1;
x(~ind) = -1;

% Random +/- matrix creation for Speed
y = rand(Number_of_Waypoints,1);
ind = y >= 0.5;
y(ind) = 1;
y(~ind) = -1;

% Divide The Maximum Altitude Change by 1000 to find how many thousand feet
% are allowed
Altitude_Difference = Max_Altitude_Change/1000;

% Write the Waypoints to the Results Matrix
for i=1:Number_of_Waypoints
    if i == 1
        Results(i,1) = First_Waypoint(1);
        Results(i,2) = First_Waypoint(2);
        Results(i,3) = (Starting_Altitude)*Feet_to_Km;
        Results(i,4) = (randi([Min_Speed,Max_Speed_Change]))*Knots_to_Km_per_Second;
    else
        Results(i,1) = First_Waypoint(1) + randi(100,1)/100*Range_LA*u(i);
        Results(i,2) = First_Waypoint(2) + randi(100,1)/100*Range_LO*v(i);
        Results(i,3) = (Starting_Altitude + randi(Altitude_Difference, 1)*1000*x(i))*Feet_to_Km;
        Results(i,4) = (randi([Min_Speed,Max_Speed_Change]))*Knots_to_Km_per_Second;
    end
end

%% Send Balloon Route Information to STK

% Create a New Aircraft

aircraft = root.CurrentScenario.Children.New('eAircraft', Balloon_Name);

% Set Aircraft Route 
% Set route to great arc, method and altitude reference

aircraft.SetRouteType('ePropagatorGreatArc');
route = aircraft.Route;
route.Method = 'eDetermineTimeAccFromVel';
route.SetAltitudeRefType('eWayPtAltRefMSL');

% Add Waypoints
for i = 1 : Number_of_Waypoints
    
    waypoint = route.Waypoints.Add();
    waypoint.Latitude = Results(i,1);
    waypoint.Longitude = Results(i,2);
    waypoint.Altitude = Results(i,3);  % km
    waypoint.Speed = Results(i,4);    % km/sec

end

route.Propagate;

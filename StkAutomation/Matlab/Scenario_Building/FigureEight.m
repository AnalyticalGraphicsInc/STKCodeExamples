%% FigureEight
% Author: Austin Claybrook
% Organization: Analytical Graphics Inc.
% Date Created: 2/12/19
% Last Modified: 6/1/20 by Cal Van Doren
% Description: Creates way points for a STK Object (which uses a GreatArc 
% propagator) in the shape of a figure eight.

%% Inputs
objPath = 'Ship/Ship1'; % Path to the object in the open STK Scenario
majorAxis = 2; % Distance between points on longer side [km]
minorAxis = 2; % Distance between points on shorter side [km]
lat = 42; % Latitude [degs]
lon = -128; % Longitude [degs]
maxTRFraction = 1; % Fraction of Max Turn Radius
speed = 27.78; % [km/hr]
revs = 3; % [numbers of revs]
Az = 0; % Orientation Angle, Measures CCW from East [deg]
alt = 0; % Altitude [km]

%% Code
% Ensure the Major Axis is the Major Axis
if minorAxis > majorAxis
    wOld = majorAxis;
    majorAxis = minorAxis;
    minorAxis = wOld;
end

% Compute the Approximate Max Turn Radius
kmPerDeg = 110.567; % approximate deg to km conversion [km/deg]
majorAxis = majorAxis/kmPerDeg;
minorAxis = minorAxis/kmPerDeg;
smallerSide = min([majorAxis,minorAxis])/2;
alpha = atan(max([majorAxis,minorAxis])/min([majorAxis,minorAxis]))/2;
maxTR = tan(alpha)*smallerSide; % [km]
tr = maxTR*maxTRFraction*kmPerDeg; % [km]
speed = speed/3600; % Convert to [km/sec]

% Create Points
lonlat = [0,0; ...
         1/2*majorAxis,1/2*minorAxis; ...
         1/2*majorAxis,-1/2*minorAxis; ...
         -1/2*majorAxis,1/2*minorAxis; ...
         -1/2*majorAxis,-1/2*minorAxis; ...
         0,0];
lonlat = lonlat';     
rotM = [cosd(Az) -sind(Az);sind(Az) cosd(Az);];
for i = 1:length(lonlat(1,:))
    lonlat(:,i) = rotM*lonlat(:,i);
end
lonlat(1,:) = lonlat(1,:)/cosd(lat);
lonlat(1,:) = lonlat(1,:)+lon;
lonlat(2,:) = lonlat(2,:)+lat;

% Repeat Points
lonlatOld = lonlat;
lonlat = [];
rev = 1;
while rev <= revs
    lonlat = [lonlat,lonlatOld(:,1:end-1)];
    rev = rev+1;
end
lonlat = [lonlat,lonlatOld(:,end)];

% Add Waypoints
lla = [lonlat',alt*ones(length(lonlat'),1)];
app = actxGetRunningServer('STK12.Application');
root = app.Personality2;
[objType,objName] = strtok(objPath,'/');
objName = objName(2:end);
if root.CurrentScenario.Children.Contains(['e',objType],objName)
    obj = root.GetObjectFromPath(objPath);
else
    obj = root.CurrentScenario.Children.New(['e',objType],objName);
end
obj.SetRouteType('ePropagatorGreatArc');
obj.Route.Waypoints.RemoveAll();
obj.Route.DefaultTurnRadius = tr;
for i = 1:length(lla)
    wp = obj.Route.Waypoints.Add();
    wp.Latitude = lla(i,2);
    wp.Longitude = lla(i,1);
    wp.Altitude = lla(i,3);
    wp.TurnRadius = tr;
    wp.Speed = speed;
end
obj.Route.Propagate();
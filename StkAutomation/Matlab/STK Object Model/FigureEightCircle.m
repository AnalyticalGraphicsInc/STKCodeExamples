%% FigureEightCircle
% Author: Austin Claybrook
% Organization: Analytical Graphics Inc.
% Date Created: 2/12/19
% Last Modified: 2/18/19 by Austin Claybrook
% Description: Creates an ephemeris file for a STK Object (which uses a GreatArc 
% propagator), in the shape of a figure eight which is composed of two circles.

%% Inputs
fileName = 'FigureEightCircle.e'; % Ephemeris file name
objPath = 'Ship/Ship2'; % ObjectType/ObjectName
r = 0.5; % Circle Radius [km]
lat = 42; % Latitude [deg]
lon = -128; % Longitude [deg]
speed = 27.78; % Speed [km/hr]
revs = 3; % Numbers of revs [revs]
Az = 0; % Orientation Angle, Measures CCW from East [deg]
alt = 0; % Altitude [km]
res = pi/3600; % Resolution of ephemeris points [rad]

%% Code
% Convert Variables
kmPerDeg = 110.567; % Approximate km to deg conversion [km/deg]
speed = speed/3600; % Convert to [km/sec]
dis = 2*(2*pi*r); % Distance per rec [km]
tPerRev = dis/speed; % Time per rev [sec]
r = r/kmPerDeg; % Convert to [deg]

% Create Points
th1 = -pi:res:0;
lonlat1 = [r * cos(th1) + r;-r * sin(th1)];
th2 = 0-res:-res:-pi;
lonlat2 = [r * cos(th2) + r; r * sin(th2)];
th3 = res:res:pi;
lonlat3 = [r * cos(th3) - r; r * sin(th3)];
th4 = -pi+res:res:0;
lonlat4 = [r * cos(th4) - r; r * sin(th4)];
th = [th1,th2,th3,th4];
lonlat = [lonlat1,lonlat2,lonlat3,lonlat4];
rotM = [cosd(Az) -sind(Az);sind(Az) cosd(Az);];
for i = 1:length(lonlat(1,:))
    lonlat(:,i) = rotM*lonlat(:,i);
end
lonlat(1,:) = lonlat(1,:)/(cosd(lat));
lonlat(2,:) = lonlat(2,:);
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
ts = linspace(0,tPerRev*revs,length(lonlat));

% Create Ephemeris File
eph = [ts',lonlat',alt*ones(length(ts),1)];
eph(:,[2 3]) = eph(:,[3 2]);
fid = fopen(fileName,'w');
fprintf(fid,'stk.v.5.0\n');
fprintf(fid,'BEGIN Ephemeris\n');
fprintf(fid,['NumberOfEphemerisPoints ',num2str(length(eph)),'\n']);
fprintf(fid,'InterpolationMethod     GreatArc\n');
fprintf(fid,'InterpolationOrder      1\n');
fprintf(fid,'DistanceUnit			Kilometers\n');
fprintf(fid,'CentralBody             Earth\n');
fprintf(fid,'CoordinateSystem        Fixed\n');
fprintf(fid,'EphemerisLLATimePos\n');
fprintf(fid,'%10.10f %10.10f %10.10f %10.10f\n',eph');
fprintf(fid,'END Ephemeris\n');
fclose(fid);

% Upload Eph File
app = actxGetRunningServer('STK12.Application');
root = app.Personality2;
[objType,objName] = strtok(objPath,'/');
objName = objName(2:end);
if root.CurrentScenario.Children.Contains(['e',objType],objName)
    obj = root.GetObjectFromPath(objPath);
else
    obj = root.CurrentScenario.Children.New(['e',objType],objName);
end
obj.SetRouteType('ePropagatorStkExternal');
obj.Route.Filename = [pwd,'\',fileName];
obj.Route.Propagate();

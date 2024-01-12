%%This script requires an area target and uses the resolution of your area target. Aaron J - AGI

%Necessary Variables
clear all
clc

%{
Name of new coverage definition being made. (New Coverage Definition)
Name of the Area Target being used. (Customize Coverage Definition)
Grid Resolution Distance. (Customize Coverage Definition)(Tested to 1
meter)
Height of the building being modeled.
Number of levels that the height will be broken up into. (Tested to 500
levels)
STK Version.
Name of the file to be made.
Filepath of the folder where this script is.
%}

Covname = input('Name for the new coverage definition (String, "Name"): ');
Area = input('Name of the Area Target to be used (String, Case Sensitive, "Name"): ');
Areaname = 'AreaTarget/'+ Area;
GRD = input('Grid Resolution Distance (meters)(integer): ');
Height = input('Height (meters): ');
LevelsTot = input('Number of levels (Altitude Resolution)(integer): ');
stkverstr = input('STK Version (String, "stk.v.12.8.0"): ');
stkver = convertStringsToChars(stkverstr);
PointFile = input('Name of Point File to be created (String, "Test File"): ');
Path = input('File path where this script resides (String, "C:\\Users\\ajones\\Documents\\MATLAB"): ');
PathLoc = Path+'\'+PointFile;

%% Grab Current STK 12 Instance
% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK12.Application');

% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;

%%  Grab Current Open Scenario
scenario = root.CurrentScenario;
%% New Coverage Definition
coverage = scenario.Children.New('eCoverageDefinition',Covname);
%% Grab Area Target Altitude
AT = root.CurrentScenario.Children.Item(Area);
Geo = AT.Position.QueryPlanetodeticArray;
GeoAlt = Geo{3};

%% Customize Coverage Definition
coverage.Grid.BoundsType = 'eBoundsCustomBoundary';
covGrid = coverage.Grid;
bounds = covGrid.Bounds;
bounds.BoundaryObjects.Add(Areaname); %Use name of Area Target


advanced = coverage.Advanced;
advanced.AutoRecompute = false;

coverage.VO.Static.PointSize = 6;
covGrid.ResolutionType = 1;
Res = covGrid.Resolution;
Res.Distance = GRD/1000;       %Coverage Grid Resolution (km)
%% Grab Grid Point Data to Copy at Altitudes
Data = coverage.DataProviders.Item('Grid Point Locations');
dp = Data.Exec;
Lat = cell2mat(dp.DataSets.GetDataSetByName("Latitude").GetValues);
Lon = cell2mat(dp.DataSets.GetDataSetByName("Longitude").GetValues);
Alt = cell2mat(dp.DataSets.GetDataSetByName("Altitude").GetValues);
%% Grab Lattitude, Longitude, and Altitude
LLA = [Lat,Lon,Alt];
Len = size(LLA,1);
Wid = size(LLA,2);
%% Make Sure Altitude Starts at the Area Target Centroid
i = 1;
for i = 1:Len
    LLA(i,3) = GeoAlt*1000;
end
%% Create Levels by Altitude
%Height = 24; %meters
%LevelsTot = 500; %Number of levels, dividing into height, including ground level
Levels = LevelsTot - 1;
Change = Height/Levels; %Change in altitude per level

start = 1;
k = 0;
Lenun = Len;
LLAtemp = LLA;

while k < Levels
    c = start;
    u = 1;
    for c = start:Len
        LLAtemp(u,3) = LLA(c,3)+Change;
        u = u+1;
    end
    start = Len+1;
    Len = Len+Lenun;
    LLA = [LLA;LLAtemp];
    k = k+1;
end
%% Create Point File
%stkver = 'stk.v.12.8.0';
fileID = fopen(PointFile,'w');

fprintf(fileID, [stkver,'\n\n']);
fprintf(fileID, 'Begin PointList\n\n');

g = 1;
for g = 1:Len
    LLAstr = num2str(LLA(g,:),9);
    fprintf(fileID, LLAstr);
    fprintf(fileID, '\n');
end

fprintf(fileID, '\n\n');
fprintf(fileID, 'End PointList');

fclose(fileID);
%% Upload File for Coverage Definition
coverage.PointDefinition.PointLocationMethod = 1;
coverage.PointDefinition.PointFileList.Add(PathLoc);
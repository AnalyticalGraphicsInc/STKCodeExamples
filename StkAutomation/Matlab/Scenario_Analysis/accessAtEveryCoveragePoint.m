%This script will iterate through every point in a coverage definition and
%find the lat, lon, and C/N between the transmitter and a dummy facility/receiver on a coverage definition
%point. It will write this information to a file

%Written by Valerie Lesser

%******Change the object names to get this code to work*****
close all; clear all; clc
format long  g

% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK12.application');

% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;

% Get handle for scenario
scenario = root.CurrentScenario;

%*****Change these object names to match the scenario*****%
%This will find the coverage defintion and facility within the scenario
CovDef = root.GetObjectFromPath("CoverageDefinition/CoverageDefinition1");
fac = root.GetObjectFromPath("Facility/Facility1");
satellite = root.GetObjectFromPath("Satellite/Satellite1");
transmitter = root.GetObjectFromPath("Satellite/Satellite1/Transmitter/Transmitter1");
receiver = root.GetObjectFromPath('Facility/Facility1/Receiver/Receiver1');
fom = root.GetObjectFromPath('CoverageDefinition/CoverageDefinition1/FigureOfMerit/FigureOfMerit1');

% Get access by object path
access = transmitter.GetAccess('Facility/Facility1/Receiver/Receiver1');
% Compute access
access.ComputeAccess();

%Get CovDef Grid Point Data Providers
GridPointsDP = CovDef.DataProviders.GetDataPrvFixedFromPath("Grid Point Locations");
%Execute Data Provider to retrieve data
GridPointsResult = GridPointsDP.Exec;
%Get values and save data to local variables
lat_temp = GridPointsResult.DataSets.GetDataSetByName("Latitude").GetValues;
lon_temp = GridPointsResult.DataSets.GetDataSetByName("Longitude").GetValues;
alt_temp = GridPointsResult.DataSets.GetDataSetByName("Altitude").GetValues;
%Format the local variables so Matlab can use them
lat =cell2mat(lat_temp);
lon = cell2mat(lon_temp);
alt = cell2mat(alt_temp);

fileID = fopen('LatLonCN.txt','w');

for i=1:length(lat)
    %Position the dummy facility to the geodetic positions of the coverage
    %definition
    fac.Position.AssignGeodetic(lat(i), lon(i), alt(i));
    fac.UseTerrain = true;
    fac.HeightAboveGround = 0;
    fac.Graphics.LabelVisible = false;
    fac.Graphics.Color = 255;
    CovDef.ComputeAccesses();
    
    dp = fom.DataProviders.GetDataPrvFixedFromPath('Value By Point').Exec();
    dpLat = cell2mat(dp.DataSets.GetDataSetByName('Latitude').GetValues);
    dpLong = cell2mat(dp.DataSets.GetDataSetByName('Longitude').GetValues);
    dpFOM = cell2mat(dp.DataSets.GetDataSetByName('FOM Value').GetValues);
    
    LinkInfo = access.DataProviders.Item('Link Information').Exec(root.CurrentScenario.StartTime, root.CurrentScenario.StopTime, 10);
    LinkInfoCNo = cell2mat(LinkInfo.DataSets.GetDataSetByName('C/N').GetValues);
    fprintf(fileID, '%0.6f %0.6f %0.6f\n', dpLat(i), dpLong(i), LinkInfoCNo(1));
         
end
fclose(fileID);


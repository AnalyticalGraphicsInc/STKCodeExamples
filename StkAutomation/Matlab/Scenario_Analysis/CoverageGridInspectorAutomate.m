function [ results ] = CoverageGridInspectorAutomate( CoverageDefinitionName, varargin )
%CoverageGridInspectorAutomate This function will automate the STK Grid
%Inspector which allows you to get detailed results for every grid point in
%the coverage defintion or figure of merit
%
%   CoverageGridInspectorAutomate(CovDefName) returns a 1 by N struct which includes
%   the latitude, longitude and data provider results for N grid points in
%   the coverage definition
%
%   Example
%       CoverageGridInspectorAutomate('CoverageDefinition1') 
%       returns 1xN struct array with fields:
%           Lat
%           Lon
%           Data
%   Example
%       CoverageGridInspectorAutomate('CoverageDefinition1','FigureOfMerit1') 
%       returns 1xN struct array with fields:
%           Lat
%           Lon
%           Data

    %See if 2 inputs were used in the function and use FOM results if true
    if nargin == 2
        hasFOM = true;
    else
        hasFOM = false;
    end
    
    uiapp = actxGetRunningServer('STK12.application');
    root = uiapp.Personality2;
    scen = root.CurrentScenario;
    root.UnitPreferences.SetCurrentUnit('DateFormat','EpSec');   %Convert DateTime to EpSec value   
    covPath = ['*/CoverageDefinition/' CoverageDefinitionName ];
    CovDef = root.GetObjectFromPath(covPath);
    
    GridPointsDP = CovDef.DataProviders.Item('Grid Point Locations');
    GridPointsResult = GridPointsDP.Exec;
    lat = GridPointsResult.DataSets.GetDataSetByName('Latitude').GetValues;
    lon = GridPointsResult.DataSets.GetDataSetByName('Longitude').GetValues;
    
    %Decides to ether use the Coverage or FigureOfMerit Grid Inspector
    %depending on the number of user inputs to the function
    if hasFOM
        FigOfMerit = root.GetObjectFromPath([ covPath '/FigureOfMerit/' varargin{1} ]);
        GridIn = FigOfMerit.GridInspector;
    else
        GridIn = CovDef.GridInspector;
    end

    for pt=1:length(lat)
        %select the point
        x = lat{pt};
        y = lon{pt};
        GridIn.SelectPoint(x,y);
    
        %Get Point Coverage Data
        %Execute Data Provider
        if hasFOM
            GridDP = GridIn.PointFOM;
            GridResult = GridDP.Exec(scen.StartTime, scen.StopTime, 60);     
        else
            GridDP = GridIn.PointCoverage;
            GridResult = GridDP.Exec(scen.StartTime, scen.StopTime);
        end

        %Push lat,lon to final struct
        results(pt).Lat = x;
        results(pt).Lon = y;
        
        temp = 0;
        %If datasets are available, get data
        if GridResult.DataSets.Count > 0
            if hasFOM
                GridTime = GridResult.DataSets.GetDataSetByName('Time').GetValues;
                GridFOM = GridResult.DataSets.GetDataSetByName('FOM Value').GetValues;
                GridNumAssets = GridResult.DataSets.GetDataSetByName('Number Of Assets').GetValues;
                %Loop through each access interval per grid point
                subData = struct;
                for i = 1:length(GridTime)
                    subData(i).Time = GridTime{i};
                    subData(i).FomValue = GridFOM{i};
                    subData(i).NumOfAssets = GridNumAssets{i};
                end   
            else
                GridAsset = GridResult.DataSets.GetDataSetByName('Asset Full Name').GetValues;
                GridStart = GridResult.DataSets.GetDataSetByName('Access Start').GetValues;
                GridStop = GridResult.DataSets.GetDataSetByName('Access End').GetValues;
                GridDuration = GridResult.DataSets.GetDataSetByName('Duration').GetValues;
                %Loop through each access interval per grid point
                subData = struct;
                for i = 1:length(GridStart)
                    subData(i).AssetName = GridAsset(i);
                    subData(i).StartTime = GridStart{i};
                    subData(i).StopTime = GridStop{i};
                    subData(i).Duration = GridDuration{i};
                end 
            end
            temp = subData;
        end 
        results(pt).Data = temp;
    end
    uiapp.release;
    clear uiapp root 
end


% NOTE:
% Most of this code uses the STK Object Model (COM).
% Lines beginning with root.ExecuteCommand use STK Connect Commands.

%% Create a new instance of STK
clear;
clc;


try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
    root = uiapp.Personality2;
    checkempty = root.Children.Count;
    if checkempty == 0
        %If a Scenario is not open, create a new scenario
        uiapp.visible = 1;
        root.NewScenario('PNT_and_Object_Coverage_Automation');
        scenario = root.CurrentScenario;
    else
        %If a Scenario is open, prompt the user to accept closing it or not
        rtn = questdlg({'Close the current scenario?',' ','(WARNING: If you have not saved your progress will be lost)'});
        if ~strcmp(rtn,'Yes')
            return
        else
            root.CurrentScenario.Unload
            uiapp.visible = 1;
            root.NewScenario('PNT_and_Object_Coverage_Automation');
            scenario = root.CurrentScenario;
        end
    end

catch
    % STK is not running, launch new instance
    % Launch a new instance of STK11 and grab it
    uiapp = actxserver('STK12.application');
    root = uiapp.Personality2;
    uiapp.visible = 1;
    root.NewScenario('PNT_and_Object_Coverage_Automation');
    scenario = root.CurrentScenario;
end

%% Specify to use Terrain Server for Az-El Masking
root.ExecuteCommand('Terrain * TerrainServer AzElMaskEnabled Yes');

%% Populate scenario with objects

% Facilities (Pseudolites)
pseudo1 = scenario.Children.New('eFacility','Pseudo1');
pseudo1.Position.AssignGeodetic(41.274,-112.108,0);
pseudo2 = scenario.Children.New('eFacility','Pseudo2');
pseudo2.Position.AssignGeodetic(41.253,-112.461,0);
pseudo3 = scenario.Children.New('eFacility','Pseudo3');
pseudo3.Position.AssignGeodetic(40.692,-113.788,0);
pseudo4 = scenario.Children.New('eFacility','Pseudo4');
pseudo4.Position.AssignGeodetic(41.675,-113.278,0);
pseudo5 = scenario.Children.New('eFacility','Pseudo5');
pseudo5.Position.AssignGeodetic(41.813,-112.755,0);
pseudo6 = scenario.Children.New('eFacility','Pseudo6');
pseudo6.Position.AssignGeodetic(41.387,-114.025,0);

facilityPseudolites = {pseudo1,pseudo2,pseudo3,pseudo4,pseudo5,pseudo6};
numFacilityPseudolites = length(facilityPseudolites);

% Aircraft (Pseudolites)
pseudo7 = scenario.Children.New('eAircraft','Pseudo7');
route1 = pseudo7.Route;
ptsArray1 = {42.073,-114.638,10.668,0.180;
            43.437,-114.134,10.668,0.180;
            42.599,-113.989,10.668,0.180;
            43.191,-111.336,10.668,0.180;
            40.044,-110.815,10.668,0.180};
route1.SetPointsSmoothRateAndPropagate(ptsArray1);
route1.Propagate;

pseudo8 = scenario.Children.New('eAircraft','Pseudo8');
route2 = pseudo8.Route;
ptsArray2 = {37.055,-114.848,10.668,0.180;
            39.521,-112.930,24.395,0.180;
            39.620,-111.242,10.668,0.180;
            40.017,-110.187,10.668,0.180;
            38.231,-112.515,10.668,0.180};
route2.SetPointsSmoothRateAndPropagate(ptsArray2);
route2.Propagate;

aircraftPseudolites = {pseudo7,pseudo8};
numAircraftPseudolites = length(aircraftPseudolites);

% Aircraft (Object of interest)
testaircraft = scenario.Children.New('eAircraft','TestAircraft');
route3 = testaircraft.Route;
ptsArray3 = {39.894,-114.510,10.668,0.140;
            40.684,-111.781,10.668,0.140;
            41.435,-112.065,10.668,0.140;
            41.781,-112.810,10.668,0.140;
            41.543,-113.374,10.668,0.140;
            40.282,-112.487,10.668,0.140};
route3.SetPointsSmoothRateAndPropagate(ptsArray3);
route3.Propagate;

%% Zoom camera near lake
root.ExecuteCommand('VO * ViewFromTo Normal From LLA');
root.ExecuteCommand('VO * ViewFromTo Advanced Position 41.20 -112.52 0');

%% Modify colors/labels/line widths of pseudolites

% Change color/label color for ground station pseudolites and offset labels
% NOTE: STK uses a decimal form for specifying color through Object Model.
for i = 1:numFacilityPseudolites
    facilityPseudolites{i}.Graphics.Color = 16777215;
    facilityPseudolites{i}.Graphics.LabelColor = 16777215;
    facilityPseudolites{i}.VO.Offsets.Label.Y = 5;
end

% Change color/line width for aircraft pseudolites
for j = 1:numAircraftPseudolites
    aircraftPseudolites{j}.Graphics.Attributes.Color = 16777215;
    aircraftPseudolites{j}.Graphics.Attributes.Line.Width = 2;
end

% Change color/line width for test aircraft
testaircraft.Graphics.Attributes.Color = 16776960;
testaircraft.Graphics.Attributes.Line.Width = 2;

%% Consider Terrain masking
for i = 1:numFacilityPseudolites
    % Set AzEl masking to use terrain data
    facilityPseudolites{i}.SetAzElMask('eTerrainData',0);
    % Enable Use Mask for Access Constraint under Basic->AzElMask
    facilityPseudolites{i}.AccessConstraints.AddNamedConstraint('AzElMask');
end

%% Create Constellation Object
constellation = scenario.Children.New('eConstellation','Constellation');

% Add facility and aircraft objects that were created above
for i = 1:numFacilityPseudolites
    constellation.Objects.AddObject(facilityPseudolites{i});
end
constellation.Objects.AddObject(pseudo7);
constellation.Objects.AddObject(pseudo8);

%% Create Chain Object to Visualize Access/No Access
chain = scenario.Children.New('eChain','Chain');
chain.Objects.AddObject(constellation);
chain.Objects.AddObject(testaircraft);
chain.ComputeAccess;
chain.Graphics.Animation.Color = 16724991;

%% Compute Object Coverage to Aircraft using Dilution of Precision (DOP)
objCoverage = testaircraft.ObjectCoverage;
objCoverage.Assets.Add('Constellation/Constellation');
objCoverage.Compute;

% Set FOM Properties
objCoverageFOM = objCoverage.FOM;
objCoverageFOM.SetDefinitionType('eFmDilutionofPrecision');
objCoverageFOM.Definition.SetComputeType('eAverage');
objCoverageFOM.Definition.SetMethod('eGDOP');
objCoverageFOM.Definition.SetType('eOverDetermined');
objCoverageFOM.Definition.TimeStep = 30;    % seconds

%% Set Contours
contours = objCoverage.FOM.Graphics.Static.Contours;
contours.IsVisible = true;
contours.ContourType = 'eSmoothFill';
contours.ColorMethod = 'eColorRamp';
contours.LevelAttributes.RemoveAll;
contours.LevelAttributes.AddLevelRange(1,20,1);  % Start,Stop,Step

contours.RampColor.StartColor = 16711680;       % Blue
contours.RampColor.EndColor = 255;              % Red

root.ExecuteCommand('Graphics */Aircraft/TestAircraft FOMContours Static LineWidth 5');

%% Add Legend
contours.Legend.VOWindow.IsVisible = true;

%% Pull in Time and FOM Values from STK for DOP

% Change time to Epoch Secs (otherwise STK imports hour:min:sec format)
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Pull in data
rptElems = {'Time';'FOM Value'};
provider = objCoverage.DataProviders.Item('FOM by Time');
results = provider.ExecElements(scenario.StartTime,testaircraft.Route.EphemerisInterval.FindStopTime(),30,rptElems);
datasets = results.DataSets;
Time1 = cell2mat(datasets.GetDataSetByName('Time').GetValues);
FOM1 = cell2mat(datasets.GetDataSetByName('FOM Value').GetValues);

%% Plot DOP vs Time
figure
plot(Time1,FOM1,'LineWidth',2)
xlabel('Time (EpSec)','FontSize',14)
ylabel('GDOP','FontSize',14)
title('Geometric Dilution of Precision vs Time','FontSize',20)
grid

%% Compute Object Coverage to Aircraft using Navigation Accuracy (NavAcc)

% Set FOM Properties
objCoverageFOM.SetDefinitionType('eFmNavigationAccuracy');
objCoverageFOM.Definition.SetComputeType('eAverage');
objCoverageFOM.Definition.SetMethod('eGACC');
objCoverageFOM.Definition.SetType('eOverDetermined');
objCoverageFOM.Definition.TimeStep = 30;    % seconds

% Set Uncertainties for each Asset Range (meters)
station1unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(0);
station1unc.MethodType = 'eFmNAConstant'; % The default is already const. but this shows you the syntax
station1unc.Method.Value = 1;
station2unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(1);
station2unc.Method.Value = 1;
station3unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(2);
station3unc.Method.Value = 1;
station4unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(3);
station4unc.Method.Value = 1;
station5unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(4);
station5unc.Method.Value = 1;
station6unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(5);
station6unc.Method.Value = 1;
aircraft1unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(6);
aircraft1unc.Method.Value = 2;
aircraft2unc = objCoverageFOM.Definition.Uncertainties.AssetList.Item(7);
aircraft2unc.Method.Value = 2;

% Set Uncertainty for Receiver Range (meters)
objCoverageFOM.Definition.Uncertainties.ReceiverRange = 0.5;

%% Pull in Time and FOM Values from STK for NavAcc
rptElems = {'Time';'FOM Value'};
provider = objCoverage.DataProviders.Item('FOM by Time');
results = provider.ExecElements(scenario.StartTime,testaircraft.Route.EphemerisInterval.FindStopTime(),30,rptElems);
datasets = results.DataSets;
Time2 = cell2mat(datasets.GetDataSetByName('Time').GetValues);
FOM2 = cell2mat(datasets.GetDataSetByName('FOM Value').GetValues);

%% Plot NavAcc vs Time
figure
plot(Time2,FOM2,'LineWidth',2)
xlabel('Time (EpSec)','FontSize',14)
ylabel('Geometric Accuracy (m)','FontSize',14)
title('Geometric Navigation Accuracy vs Time','FontSize',20)
grid
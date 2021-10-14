clear all; close all;

%%%%%%%%%%%%%%%%%%%%%%%%%%
% STK Chains with MATLAB
%%%%%%%%%%%%%%%%%%%%%%%%%%

% New scenario
uiApp = actxserver('STK12.Application');
uiApp.Visible = true;
uiApp.UserControl = true;
root = uiApp.Personality2;
root.NewScenario('STK_Chain_Automation');

% Attach to open scenario
% uiApp = actxGetRunningServer('STK12.Application');
% root = uiApp.Personality2;

scenario = root.CurrentScenario;

% Insert constellation of Satellites
scenario.Children.New('eSatellite', 'WalkerTemp');

% Make a walker constellation 
cmd = 'Walker */Satellite/WalkerTemp Type Delta NumPlanes 2 NumSatsPerPlane 4 InterPlanePhaseIncrement 1 ColorByPlane Yes ConstellationName WalkerSats';
root.ExecuteCommand(cmd);

% Delete original satellite
scenario.Children.Unload('eSatellite', 'WalkerTemp');

% Insert Facility
fac = scenario.Children.New('eFacility', 'GroundSite');

% Insert Chain
chain = scenario.Children.New('eChain', 'Chain');

% Get objects panel in Chain
chainObjs = chain.Objects;

% Add constellation and facility to Chain, just like in the GUI
% Use the path of the object or the object in matlab
chainObjs.Add('Constellation/WalkerSats');
% chainObjs.Add(scenario.Children.Item('WalkerSats');
chainObjs.Add('Facility/GroundSite');
% chainObjs.AddObject(fac)

% Compute Chain Access
chain.ComputeAccess();

% Obtain full chain data providers
dataSets = chain.DataProviders.GetDataPrvTimeVarFromPath('Access AER Data').Exec(scenario.StartTime, scenario.StopTime, 60).DataSets;

% Get total number of data sets
numberDataSets = dataSets.Count;

% 8 providers per row since Access AER Data provider has 8 columns (check
% in GUI)
cols = 8;
rows = numberDataSets/cols;

% Invert due to cell matrix construction and referencing
vals = cell(cols, rows);

% Get all values from Access AER Data provider
for i = 1:numberDataSets - 0
    vals{i} = dataSets.Item(i - 1).GetValues();
end

% Make matrix look like the report from STK
vals = transpose(vals);

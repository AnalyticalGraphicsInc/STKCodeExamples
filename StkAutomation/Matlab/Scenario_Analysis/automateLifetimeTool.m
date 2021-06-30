%% automateLifetimeTool
% Description: Automates the satellite lifetime tool, generates a text file
% of the results, and shows an example of looping through paraemters to run
% a trade study
% 
% Instructions: Have STK open with a scenario containing the satellite that 
% you would like to perform that lifetime analysis on.  Ensure the satellite
% has been propagated 

%% Inputs

% Name of satellite:
SatName = 'CrashAndBurn';

% Define Lifetime Parameters:
Mass = 21; %kg
DragCoeff = 2; %unitless
DragArea = 7; %m^2
FluxSigmaLevel = 1; %unitless

%% Code

% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK12.application');

% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;

%Grab scenario object
scen = root.CurrentScenario;

%Change DateFormat dimension to epoch seconds
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

%Grab Satellite Object
SatPath = ['*/Satellite/',SatName];
sat = root.GetObjectFromPath(SatPath);

% Initialize text file for storing results 
fid = fopen('LifetimeData.txt','wt');

%Configure the lifetime settings and run analysis
CmdString = ['SetLifetime */Satellite/CrashAndBurn DragCoeff ', num2str(DragCoeff), ' Mass ', num2str(Mass), ' DragArea ', num2str(DragArea), ' FluxSigmaLevel ', num2str(FluxSigmaLevel)];
root.ExecuteCommand(CmdString);
result = root.ExecuteCommand('Lifetime */Satellite/CrashAndBurn');

% Retreiveslifetime results from ExecuteCommand and print to file:
for k = 0:(result.Count - 1)
    answer = result.Item(k);
    fprintf(fid,' %s \n', answer);
end

% Notify when the run is complete 
fprintf('-Run Complete-\n\n');

% Close the external file
fclose(fid);

%Extract lifetime data providers to arrays:
LifetimeDP = sat.DataProviders.Item('Lifetime').Exec(scen.StartTime,scen.StopTime,60);
time = cell2mat(LifetimeDP.DataSets.GetDataSetByName('Time').GetValues);
inc = cell2mat(LifetimeDP.DataSets.GetDataSetByName('Inclination').GetValues);
sma = cell2mat(LifetimeDP.DataSets.GetDataSetByName('Semi-major Axis').GetValues);

%% Optionally you can vary the lifetime tool parameters to run a trade study:

% Define a range for one or more lifetime parameters
Mass = 1000:50:1500; %kg

% Initialize a different text file for storing results 
fid = fopen('LifetimeDataTrade.txt','wt');

% Loop through the values of the mass and print results to file for each
% mass input
for ii = 1:length(Mass)
   CmdString = ['SetLifetime */Satellite/CrashAndBurn DragCoeff ', num2str(DragCoeff), ' Mass ', num2str(Mass(ii)), ' DragArea ', num2str(DragArea), ' FluxSigmaLevel ', num2str(FluxSigmaLevel)];
   root.ExecuteCommand(CmdString);
   result = root.ExecuteCommand('Lifetime */Satellite/CrashAndBurn');
   % Retreives lifetime results from ExecuteCommand:
   for jj = 0:(result.Count - 1)
       answer = ['Mass = ',num2str(Mass(ii)),'kg  ',result.Item(jj)];
       fprintf(fid,' %s \n', answer);
   end
end
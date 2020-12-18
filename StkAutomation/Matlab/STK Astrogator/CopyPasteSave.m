
%% Connect to STK 
app = actxGetRunningServer("STK12.Application");
root = app.Personality2;

%% Get File Path
scenario = root.CurrentScenario;
testFile = scenario.ScenarioFiles(1);
[path, ~, ~] = fileparts(string(testFile));

%% Build Commands
saveCommand = strcat('Save / * '," ", path);

satName = 'Satellite1';
newName = 'Satellite';
satsNumber = scenario.Children.GetElements('eSatellite').Count + 1;

tempNameCommand = strcat('Rename */Satellite/', satName, " ", 'original');
loadCommand = strcat('Load / */Satellite', " ",'"', path, "\", satName, '.sa','"');

newNameCommand = strcat('Rename */Satellite/', satName, " ", newName, string(satsNumber));
returnNameCommand = strcat('Rename */Satellite/', 'original', " ",satName);

%% Rename, Load, Change Name, Return Name
root.ExecuteCommand(saveCommand);
root.ExecuteCommand(tempNameCommand);
root.ExecuteCommand(loadCommand);

root.ExecuteCommand(newNameCommand);
root.ExecuteCommand(returnNameCommand);
root.ExecuteCommand(saveCommand);

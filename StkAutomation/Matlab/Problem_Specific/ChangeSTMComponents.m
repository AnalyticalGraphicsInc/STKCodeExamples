close all; clear; clc
format long  g

% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK12.application');

% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;

% Get handle for scenario
scenario = root.CurrentScenario;

% Get handle for satellite
satellitePath = '*/Satellite/MarsSat';
satellite = root.GetObjectFromPath(satellitePath);

% Duplicate and modify STM components for Mars
% Get the calc objs Folder
componentBrowser = scenario.ComponentDirectory.GetComponents('eComponentAstrogator').GetFolder('Calculation Objects');
% Get the cartesian STM Folder
stm = componentBrowser.GetFolder('Cartesian STM');

finalVariableList = ['PosX'; 'PosY'; 'PosZ'; 'VelX'; 'VelY'; 'VelZ'];
initialVariableList = ['PosX'; 'PosY'; 'PosZ'; 'VelX'; 'VelY'; 'VelZ'];

for ff = 1:length(finalVariableList)
    for ii = 1:length(initialVariableList)
        
        string = ['STM',finalVariableList(ff,:),initialVariableList(ii,:)];
        
        try 
            % This checks if it already exists
            stm.Item([string,'Mars']);
        catch
            % If it does not exist, create it below
            % Grab the STM component
            stmComponent = stm.Item(string);
            % Make a copy of the Model to Edit it
            stmComponentClone = stmComponent.CloneObject;
            
            % Grab a handle of the new STM Component and edit properties
            NewSTMComponent = stm.Item([string,'1']);
            NewSTMComponent.Name = [string,'Mars'];
            NewSTMComponent.CoordSystemName = 'CentralBody/Mars J2000';
        end
        
    end
end

% Change DateFormat dimension to epoch seconds to make the data easier to handle in
% MATLAB
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');


% Extract STM components at a single time
time = 0;  %EpSec
STMDP = satellite.DataProviders.Item('Astrogator Values').Group.Item('Cartesian STM').ExecSingle(time);

STMMatrix = zeros(6,6);
for ff = 1:length(finalVariableList)
    for ii = 1:length(initialVariableList)
        
        % Populate STM matrix with values
        string = ['STM',finalVariableList(ff,:),initialVariableList(ii,:),'Mars'];
        STMMatrix(ff,ii) = cell2mat(STMDP.DataSets.GetDataSetByName(string).GetValues);
        
    end
end

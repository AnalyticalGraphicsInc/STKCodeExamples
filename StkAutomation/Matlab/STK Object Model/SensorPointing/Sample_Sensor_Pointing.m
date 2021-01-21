%% Always start your MATLAB session fresh
clc
clear all

% % 
% % % Initialize 
try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK11.application');
    root = uiapp.Personality2;
    checkempty = root.Children.Count;

    % Create a new scenario and give it a 24 hour analysis time period
    root.NewScenario('SampleSatelliteSensorPointingScenario');
    scenario = root.CurrentScenario;
    scenario.SetTimePeriod('Today','+24 hrs');
    
catch
    % STK is not running; launch new instance
    uiapp = actxserver('STK11.application');
    root = uiapp.Personality2;
    uiapp.visible = 1;
    
    % Create a new scenario and give it a 24 hour analysis time period
    root.NewScenario('SampleSatelliteSensorPointingScenario');
    scenario = root.CurrentScenario;
    scenario.SetTimePeriod('Today','+24 hrs');
    
    % Alternatively, you can opt to load a previously saved scenario
    % TODO update this path to point to your starter scenario
    % root.LoadScenario("C:\Users\myusername\Documents\STK 11 (x64)\SampleSatelliteSensorPointingScenario\SampleSatelliteSensorPointingScenario.sc");
    % scenario = root.CurrentScenario;
end
 
%%
%
% Create a Walker constellation of satellites that we will use to then make
% pointed sensors
%

% make a seed satellite
satellite = scenario.Children.New('eSatellite', 'Satellite');
satellite.SetPropagatorType('ePropagatorJ4Perturbation');
satellite.Propagator.UseScenarioAnalysisTime;
satellite.Propagator.InitialState.OrbitEpoch.SetImplicitTime(scenario.AnalysisEpoch.ReferenceEvent);

satellite.Propagator.InitialState.Representation.AssignClassical('eCoordinateSystemICRF',6878.14,0.0,15.0,0.0,0,0);

satellite.Propagator.Propagate;

% use the Walker tool (command) to create 100 satellites for analysis
% see Connect documentation: https://help.agi.com/stkdevkit/index.htm#../Subsystems/connectCmds/Content/cmd_WalkerSatellites.htm
root.ExecuteCommand('Walker */Satellite/Satellite Type Delta NumPlanes 10 NumSatsPerPlane 10 InterPlanePhaseIncrement 1 ColorByPlane Yes ConstellationName SatelliteConst');

%%
%
% Read from matrix
% Add some pointed sensors
%
disp('Creating directional sensors on satellite constellation')

% make a constellation object for the Sensor objects
sensorConstellation = scenario.Children.New('eConstellation', 'SensorConst');

% this command is new to MATLAB 2020a
% TODO update this file path
%tableIn = readtable('C:\Users\myusername\Desktop\Sample_Sensor_Pointing_Matrix.csv','ReadVariableNames',true,'ReadRowNames',true);
tableIn = readtable('C:\Users\tstritch\Documents\GitHub\STKCodeExamples\StkAutomation\Matlab\STK Object Model\SensorPointing\Sample_Sensor_Pointing_Matrix.csv', 'ReadVariableNames',true,'ReadRowNames',true);


[m,n] = size(tableIn);
%row 1 has sat numbers in STK
%col 1 has sat numbers in STK
dataSection = cell2mat(table2cell(tableIn));
[row,col] = find(dataSection);

rowStop = length(row);
colStop = length(col);

cellNames = tableIn.Properties.VariableNames;
hbar = waitbar(0,'Creating directional sensors on satellite constellation...');
for i = 1:rowStop
    
    rowInd = row(i); 
    colInd = col(i); 
    %TODO update sensor naming convention, if desired
    firstSatName = cellNames{colInd};
    secondSatName = cellNames{rowInd};

    sensorName = strcat('SensorTo', cellNames{rowInd});
    
    sat1Path = strcat('Satellite/', firstSatName);
    sat2Path = strcat('*/Satellite/', secondSatName);
    sat2Short = strcat('Satellite/', secondSatName);
    
    satellite = root.GetObjectFromPath(sat1Path);

    if (satellite.Children.Contains('eSensor',sensorName))
        sensor = satellite.Children.Item(sensorName);
        sensorConstellation.Objects.AddObject(sensor);
    else
        sensor = satellite.Children.New('eSensor', sensorName);
        sensor.CommonTasks.SetPatternSimpleConic(1,0.1);
        sensor.CommonTasks.SetPointingTargetedTracking('eTrackModeTranspond', 'eBoresightRotate', sat2Path);
        
        % optional - disable the sensor graphics as it could be
        % overwhelming
        sensor.Graphics.InheritFromScenario = false;
        sensor.Graphics.IsObjectGraphicsVisible = false;

        % optional compute access
        % access = satellite.GetAccess(sat2Short);
        % access.ComputeAccess();

        % Put the satellite into the constellation
        sensorConstellation.Objects.AddObject(sensor);
    end
    waitbar(i/rowStop, hbar)
end
close(hbar)

root.ExecuteCommand('Animate * Reset');

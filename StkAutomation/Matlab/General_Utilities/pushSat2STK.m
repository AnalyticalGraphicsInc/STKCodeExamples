% Script connects to STK and inserts a satellite with a given attitude file
% (*.a) and ephemeris file (*.e) and propagates for the duration of the
% file

% To export an ephemeris and attitude file for 
close all; clear all; clc

%% User Defined variables
attFile = ''; % empty to generate file, absolute filepath for existing .a
ephFile = ''; % empty to generate file, absolute filepath for existing .e
satMdl = ''; % empty to keep default or absolute filepath to change

%% Create new/connect to running instance of STK
try
    % Attach to running scenario
    uiApp = actxGetRunningServer('STK12.Application');
    root = uiApp.Personality2;
    scenario = root.CurrentScenario;
    disp('connected to running scenario');
catch
    % New scenario
    uiApp = actxserver('STK12.Application');
    uiApp.Visible = true;
    uiApp.UserControl = true;
    root = uiApp.Personality2;
    root.NewScenario('Test_Scenario');
    scenario = root.CurrentScenario;
    disp('launched test scenario')
end

%% call function to generate .a and .e file if needed
[attFile, ephFile] = exportAttEphFiles(scenario);

%% Ensure .a and .e files cover same interval
[scStart_att, scEnd_att] = getScenarioTimes(attFile);
[scStart_eph, scEnd_eph] = getScenarioTimes(ephFile);

% Convert to Julian Date to compare
scStart_att_JD = root.ConversionUtility.ConvertDate('UTCG','JDate',scStart_att);
scStart_eph_JD = root.ConversionUtility.ConvertDate('UTCG','JDate',scStart_eph);

% ensure intervals match
if scStart_att_JD ~= scStart_eph_JD
    disp('Scenarios do not start at the same time. Please provide matching ephemeris/attitude files');
    return;
elseif scEnd_att ~= scEnd_eph
    disp('Scenarios do not end at the same time. Please provide matching ephemeris/attitude files');
    return;
else
    disp('Matching ephemeris and attitude file intervals. Pushing to STK...');
    scEnd = root.ConversionUtility.ConvertDate('JDate','UTCG',string(str2double(scStart_att_JD) + (scEnd_att/86400)));
end

%% Set scenario start and end times
scenario.SetTimePeriod(scStart_att,scEnd);

%% Create satellite and assign external attitude (.a) and ephemeris file (.e)
mySat = scenario.Children.New('eSatellite','externalSat');
mySat.Attitude.External.Load(attFile);
mySat.SetPropagatorType('ePropagatorStkExternal');
mySat.Propagator.Filename = ephFile;
mySat.Propagator.Propagate;

%% Set model file to non-default
if ~isempty(satMdl)
    mySat.VO.Model.ModelData.Filename = satMdl;
end

%% Function to get start/stop times for .a/.e file
% Function assumes satelliteFile is a *.a or *.e file properly formatted to
% be read into STK 
% Times written in Epoch Seconds
% Returns: scStart - UTCG [string]
%          scEnd - EpSec at end of file [float]

function [scStart, scEnd] = getScenarioTimes(satelliteFile)
    % open file
    fid = fopen(satelliteFile, 'r');
    
    % get scenario epoch
    textLine = fgetl(fid);
    lineCnt = 1;
    while ischar(textLine)
        textLine = fgetl(fid);
        if contains(textLine,'ScenarioEpoch')
            line = strsplit(textLine,'\t');
            scStart = line(2);
            scStart = scStart{1};
            break;
        end
        lineCnt = lineCnt + 1;
    end
    frewind(fid);
    
    % if attitude file, pull attitude data
    if contains(satelliteFile,'.a')
        data = textscan(fid,'%f %f %f %f %f', 'HeaderLines', 25, 'Delimiter', '\t');
        
    % if ephemeris file, pull pos/vel states
    elseif contains(satelliteFile,'.e')
        data = textscan(fid,'%f %f %f %f %f %f %f', 'HeaderLines', 26, 'Delimiter', '\t');
    
    % not an acceptable file format. Return
    else
        error('File type not acceptable. Please input *.a or *.e file');
    end
    
    scEnd = data{1}(end);
    fclose(fid);
    
end

% function to create a test satellite in order to export an attitude and
% ephemeris file that can be loaded in by the main script
function [attFileName, ephFileName] = exportAttEphFiles(scenario)
    % create and propagate satellite
    testSat = scenario.Children.New('eSatellite', 'testSat');
    testSat.Propagator.Propagate;
    
    % export attitude and ephemeris files
    attFileName = '.\testSat.a';
    ephFileName = '.\testSat.e';
    testSat.ExportTools.GetAttitudeExportTool.Export(attFileName)
    testSat.ExportTools.GetEphemerisStkExportTool.Export(ephFileName)
    
    % remove the satellite
    testSat.Unload;
    
end

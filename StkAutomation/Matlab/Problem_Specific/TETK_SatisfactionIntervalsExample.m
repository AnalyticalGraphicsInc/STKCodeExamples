% Creator: Jake Gremer, Analytical Graphics, Inc.

% This code allows you to import TSPI data files and create a flight segment .txt file based
% on intervals that satisfy TWO user input metric conditions, which is then loaded into
% TETK via Connect Commands to visualize.
% In this example, we are looking at the intervals during our Ownship's
% flight that meet a Cal Air Speed condition (from 200 to 300 knots)
% and a roll angle condition (from -50 to 0 degrees).
% 
% NOTE:
% %% Setup for .csv files using ISO-YD time %%%
% This code is taking a .csv TSPI file with a time format in ISO-YD and
% converting it to the correct format in order to import flight segments
% via .txt file.
% Will require time format manipulation for importing flight time segments.
% Required format for ingest to TETK: ddd:HH:mm:ss.sss

clear;
clc;
%% ****INPUTS****
% *---Section 1: Files Paths---*
dataFileDirectory = 'C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Ownship.csv'; % Insert path of TSPI data file
mappingTableDirectory = 'C:\Users\jgremer\Documents\STK 12\Config\TeDataMappingTable.tedm'; % Insert path of .tedm file
% *---Section 2: Analysis Object---*
analysisObject = 'Ownship1'; % Insert name of analysis object
% *---Section 3: Conditions---*
% Assign time
time = "Time (ISO-YD)"; % Insert name of time column header
% First data column of interest from .csv file
element1 = "Cal Air Speed"; % Insert name of data column header
min1 = 200; % minimum value - corresponding units to data column: knots
max1 = 300; % maximum value - corresponding units to data column: knots
% Second data column of interest from .csv file
element2 = "Roll"; % Insert name of data column header
min2 = -50; % minimum value - corresponding units to data column: degs
max2 = 0; % maximum value - corresponding units to data column: degs

%% Check for running application
% initial setup - optional
userArea = getenv('USERPROFILE');
scenarioName = 'TE_SatisfactionIntervals_Example'; % **INPUT** - name of scenario
scenarioFolder = fullfile(userArea,'Documents','STK 12',scenarioName); %path assembly for scenario folder
if exist(scenarioFolder, 'dir') %remove existing folder if it exists
    rmdir(scenarioFolder, 's')
end
mkdir(scenarioFolder); %new folder creation
setDefaultDirCommand = ['SetDefaultDir / ' '"' scenarioFolder '"']; %set working directory

%% try/catch
try
    % Grab existing instance of STK
    uiapplication = actxGetRunningServer('STK12.application');
    root = uiapplication.Personality2;
    checkempty = root.Children.Count;
    if checkempty == 0
        % If no scenario open,create new scenario
        uiapplication.visible = 1;
        root.ExecuteCommand(setDefaultDirCommand);
        root.NewScenario(scenarioName);
        scenario = root.CurrentScenario; 
    else
        % If scenario is open, save and close
        rtn = questdlg({'Close the current scenario?',' ','(WARNING: If you have not saved your progress will be lost)'});
        if ~strcmp(rtn,'Yes')
            return
        else
            root.CurrentScenario.Unload;
            uiapplication.visible = 1;
            root.ExecuteCommand(setDefaultDirCommand);
            root.NewScenario(scenarioName);
            scenario = root.CurrentScenario;
        end
    end 
catch
    % If not running, launch new instance of STK
    uiapplication = actxserver('STK12.application');
    root = uiapplication.Personality2;
    uiapplication.visible = 1;
    root.ExecuteCommand(setDefaultDirCommand);
    root.NewScenario(scenarioName);
    scenario = root.CurrentScenario;
end

%% Get path to STK install directory and user directory - optional step
installDirectory = root.ExecuteCommand('GetDirectory / STKHome').Item(0);
userDirectory = root.ExecuteCommand('GetDirectory / DefaultUser').Item(0);

%% Set up scenario/windows
disp('* Creating scenario settings *')
%Resets the animation to the start time
root.ExecuteCommand('Animate * Reset');
%Maximizes the 3D Graphics Window
root.ExecuteCommand('Window3D * Maximize');

%% Import ownship and necessary files
disp('* Importing Mappings and Ownship Data *')
% Import .tedm file - *point to .tedm file to use*
root.ExecuteCommand(['TE_Mapping * Import File "', mappingTableDirectory,'" ']);
% Import ownship - *point to .csv file to use*
root.ExecuteCommand(['TE_AnalysisObject * Add File "', dataFileDirectory,'" Name "', analysisObject,'" ']);
root.Save();

%% Read .csv file
disp('* Reading metrics and conditions *');
T = readtable(dataFileDirectory,'PreserveVariableNames',true);
T.Properties.VariableUnits = T{1,:};
T(1,:) = [];

%% Table conversions
% time array
time_t = T(:,strcmp(T.Properties.VariableNames, time));
time_a = table2array(time_t);
% assign correct columns
element1_str = T(:,strcmp(T.Properties.VariableNames, element1));
element2_str = T(:,strcmp(T.Properties.VariableNames, element2));
% convert to arrays
element1_a = table2array(element1_str);
element2_a = table2array(element2_str);
% convert to scalar
element1_scal = str2double(element1_a);
element2_scal = str2double(element2_a);

%% Determine data elements that align with both metric conditions
fprintf('* Calculating satisfaction intervals for %s and %s *\n',element1,element2);
% Check metric conditions
j = 1;
for i = 1:length(element1_scal)
    if ((min1 < element1_scal(i)) && (element1_scal(i) < max1) && (min2 < element2_scal(i)) && (element2_scal(i) < max2))
       %bin the cells
        parametersSatisfied{j,1} = time_a{i}; %time
        parametersSatisfied{j,2} = element1_scal(i); %value for parameter 1
        parametersSatisfied{j,3} = element2_scal(i); %value for parameter 2
        parametersSatisfied{j,4} = i; % time counter
        j = j + 1;
    end  
end
      
%% Grab time intervals that satisfy both metric conditions
% set first start time segment
timeSegments{1,1} = parametersSatisfied{1,1};
% set time segments
j = 2;
for i = 2:length(parametersSatisfied)-1
     if (parametersSatisfied{i,4} - parametersSatisfied{i-1,4} ~= 1) 
         timeSegments{j,1} = parametersSatisfied{i,1}; %start
         timeSegments{j-1,2} = parametersSatisfied{i-1,1}; %stop
         j = j + 1;
     end
end
% set last stop time segment
timeSegments{j-1,2} = parametersSatisfied{length(parametersSatisfied),1};

%% Reformat Time Segments to allow for ingestion into TETK (Replace "T" with ":" & Delete year)
for i = 1:length(timeSegments)  
     timeSegments{i,1} = strrep(timeSegments{i,1},'T',':');
     timeSegments{i,2} = strrep(timeSegments{i,2},'T',':');
     timeSegments{i,1} = strrep(timeSegments{i,1},'-',' ');
     timeSegments{i,2} = strrep(timeSegments{i,2},'-',' ');
     [x1,y1] = strtok(timeSegments{i,1});
     [x2,y2] = strtok(timeSegments{i,2});
     timeSegmentsFiltered{i,1} = strtok(y1);
     timeSegmentsFiltered{i,2} = strtok(y2);
end

%% Create time segments .txt file before reading in TETK
disp('* Creating satisfactionIntervals.txt file *');
timeSegmentFileName = 'satisfactionIntervals.txt';
fileID = fopen(timeSegmentFileName,'wt');
for i = 1:length(timeSegments)
    fprintf(fileID, '%s %s\n', timeSegmentsFiltered{i,1},timeSegmentsFiltered{i,2});   
end
fclose(fileID);
movefile(timeSegmentFileName, scenarioFolder);

%% Create time segments in TETK by ingesting .txt file
disp('* Creating flight segments from satisfactionIntervals.txt file *');
root.ExecuteCommand(['TE_SegmentDefinition * Import File "', scenarioFolder, '\', timeSegmentFileName,'" Ownship "', analysisObject,'" Subsystem "FlightSegment" Name "SatisfactionIntervals" ShowOnTimeline "On"']);
root.Save();
disp('* Complete *');

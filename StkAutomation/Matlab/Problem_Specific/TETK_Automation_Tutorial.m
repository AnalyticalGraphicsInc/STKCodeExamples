clc

%% This section will check for running applications of STK and create a new scenario based on 3 paths
disp('* Grabbing/creating instance of STK and creating a new scenario *')
tic

userArea = getenv('USERPROFILE'); %Get User Area
scenarioName = 'TE_MATLAB_Automation'; %Name to be used for scenario
scenarioFolder = fullfile(userArea,'Documents','STK 12',scenarioName) %Assemble path for scenario folder to be created
mkdir(scenarioFolder); %Create folder
setDefaultDirCommand = ['SetDefaultDir / ' '"' scenarioFolder '"'] %Set working directory. If not set TETK will save all files/folders in current working directory. 

%%
try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
    root = uiapp.Personality2;
    checkempty = root.Children.Count;
    if checkempty == 0
        %If a Scenario is not open, create a new scenario
        uiapp.visible = 1;
        root.ExecuteCommand(setDefaultDirCommand)
        root.NewScenario(scenarioName);
        scenario = root.CurrentScenario;
        %root.Save()
        
    else
        %If a Scenario is open, prompt the user to accept closing it or not
        rtn = questdlg({'Close the current scenario?',' ','(WARNING: If you have not saved your progress will be lost)'});
        if ~strcmp(rtn,'Yes')
            return
        else
            root.CurrentScenario.Unload
            uiapp.visible = 1;
            root.ExecuteCommand(setDefaultDirCommand)
            root.NewScenario(scenarioName);
            scenario = root.CurrentScenario;
            %root.Save()
            %ExecuteCommand(["SetDefaultDir /" '"' scenarioFolder '"'])
            %root.SaveScenario();
        end
    end

catch
    % STK is not running, launch new instance
    % Launch a new instance of STK12 and grab it
    uiapp = actxserver('STK12.application');
    root = uiapp.Personality2;
    uiapp.Visible = 1;
    uiapp.UserControl = 1;
    root.ExecuteCommand(setDefaultDirCommand)
    root.NewScenario(scenarioName);
    scenario = root.CurrentScenario;
    %root.Save()
    %ExecuteCommand(["SetDefaultDir /" '"' scenarioFolder '"']);
    %root.SaveScenario();
    
end

toc

%% Get the path to the STK install directory
installDirectory = root.ExecuteCommand('GetDirectory / STKHome').Item(0);
disp(installDirectory);

%% Set up windows
disp('* Creating scenario settings *')
%Resets the animation to the start time
root.ExecuteCommand('Animate * Reset');
%Maximizes the 3D Graphics Window
root.ExecuteCommand('Window3D * Maximize');
%root.ExecuteCommand('ConnectLog / On "C:\temp\connectlog.txt"');   %Creates a log of connect commands set to an area you have access to

%% This Section will import the ownship (AnalysisObject) and target TSPIs (AssociatedObjects)and track files (AdditionalData)

disp('* Importing Mappings and Ownship Data')
root.ExecuteCommand('TE_Mapping * Import File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\TrainingAutomation.tedm" ');
root.ExecuteCommand('TE_AnalysisObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Ownship.csv" Name "Ownship1" ');

%Import the TSPI data for our target objects as associated objects
disp('*Importing TSPI data for Targets')
root.ExecuteCommand('TE_AssociatedObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Target1.csv" Name "Target1_TSPI" Ownship "Ownship1" ');
root.ExecuteCommand('TE_AssociatedObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Target2.csv" Name "Target2_TSPI" Ownship "Ownship1" ');
root.ExecuteCommand('TE_AssociatedObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Ship.csv" Name "Ship_TSPI" Ownship "Ownship1" ');

%Import aditional data files that contain our subsystem measurements
disp('*Importing track files')
root.ExecuteCommand('TE_AdditionalData * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Air Tracks - Radar.csv" Ownship "Ownship1" ');
root.ExecuteCommand('TE_AdditionalData * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Air Tracks - Track Event.csv" Ownship "Ownship1" ');
root.ExecuteCommand('TE_AdditionalData * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Ship Tracks - RF.csv" Ownship "Ownship1" ');
%% 
 % An example of creating static ground targets that can be used as truth
 % objects for track comparisons.
disp('Creating ground targets and setting locations');
root.ExecuteCommand('NewMulti / */Target 4 Target1 Target2 Target3 Target4');
root.ExecuteCommand('SetPosition */Target/Target1 Geodetic 35.5429 -118.263 Terrain');
root.ExecuteCommand('SetPosition */Target/Target2 Geodetic 35.3837 -118.208 Terrain');
root.ExecuteCommand('SetPosition */Target/Target3 Geodetic 35.2823 -118.096 Terrain');
root.ExecuteCommand('SetPosition */Target/Target4 Geodetic 35.4346 -118.04 Terrain');
 
 %% Import flight segments from files
    % By setting the assets for a flight segment T&E will auto create
    % quicklooks for the assigned objects.  This gives the user access to
    % things like range, aspect, range rate, etc
 disp('Importing flight segments from files');
 root.ExecuteCommand('TE_SegmentDefinition * Import File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\RF runs.txt" Ownship "Ownship1" Subsystem "RF" ShowOnTimeline "On" Name "RfRuns" Assets "*/Aircraft/Ship_TSPI"');
 

 
 %% Manually Creating flight segments for RADAR Subsystem
%disp('Creating flight segments')
root.ExecuteCommand('Units_Set * Connect Date "UTCG"');
root.ExecuteCommand('TE_SegmentDefinition * Add Name "RADAR" StartTime "1 Mar 2017 20:56:16.000" StopTime "1 Mar 2017 20:58:30.000" Ownship "Ownship1" Subsystem "RADAR" ShowOnTimeline "On" Assets "*/Aircraft/Target1_TSPI,*/Aircraft/Target2_TSPI"');
root.ExecuteCommand('TE_SegmentDefinition * Add Name "RADAR" StartTime "1 Mar 2017 21:01:30.000" StopTime "1 Mar 2017 21:04:18.000" Ownship "Ownship1" Subsystem "RADAR" ShowOnTimeline "On" Assets "*/Aircraft/Target1_TSPI,*/Aircraft/Target2_TSPI"');
root.ExecuteCommand('TE_SegmentDefinition * Add Name "RADAR" StartTime "1 Mar 2017 21:10:39.075" StopTime "1 Mar 2017 21:13:09.349" Ownship "Ownship1" Subsystem "RADAR" ShowOnTimeline "On" Assets "*/Aircraft/Target1_TSPI,*/Aircraft/Target2_TSPI"');

%% Manually Creating flight segments for RF subsystem
%root.ExecuteCommand('TE_SegmentDefinition * Add Name "RF_Run1" StartTime "1 Mar 2017 20:21:43.000" StopTime "1 Mar 2017 20:25:43.000" Ownship "Ownship1" Subsystem "RF" ShowOnTimeline "On"');
%root.ExecuteCommand('TE_SegmentDefinition * Add Name "RF_Run1" StartTime "1 Mar 2017 20:34:45.000" StopTime "1 Mar 2017 20:39:37.000" Ownship "Ownship1" Subsystem "RF" ShowOnTimeline "On" Assets "*/Aircraft/Target1_TSPI"');
 
 %% Create Tracks from the Track File that are filtered by Track ID and constrained by a run  Need to clarify doc for using flight segments.  YOU WILL HAVE TO UPDATE THE MAPPING NAME TO A MAPPING THAT EXISTS ON YOUR SYSTEM

root.ExecuteCommand('TE_Track * Add Name "Target1Track" AnalysisObject "Ownship1" Mapping "Sample_Track" TrackIDs "1001" TimePath "Scenario/TE_Automation RADAR_Run1 EventInterval"'); %This is currently failing due to the naming bugs disabling
root.ExecuteCommand('TE_Track * Add Name "ShipTrack" AnalysisObject "Ownship1" Mapping "ShipTrack"'); 
 
% Promote Tracks and modify point properties
disp('Promoting Tracks to heavy objects for further analysis');
root.ExecuteCommand('TE_Track * Promote Name "ShipTrack"');

% Color some tracks by a parameter.
root.ExecuteCommand('TE_Track * Points Name "Target1Track" Show "On" ColordisplayType "dynamic" ColorContourType "Smooth" SetParameter "SlantRangeToTarget1_TSPI" Units "nmi" MinValue "34.5" MaxValue "60.5" MinColor "green" MaxColor "red"');

% Can't do track comparison with the aircraft tracks because they contain duplicate times and we can't filter through connect yet
disp('Performing Track Comparisons');
%$stkRoot->ExecuteCommand('TE_TrackComparison * Add Name "TrackCompare1" AnalysisObject "Ownship1" Track "Target1Track" TruthObject "Aircraft/Target1_TSPI" TruthPointing "StkObject" MeasuredObject "Aircraft/Target1Track_Measured" ReferenceSystem "Aircraft/Ownship1 NorthWestUp System" TimePath "Aircraft/Ownship1 AvailabilityTimeSpan EventInterval"');
root.ExecuteCommand('TE_TrackComparison * Add Name "TrackCompareShip" AnalysisObject "Ownship1" Track "ShipTrack" TruthObject "Aircraft/Ship_TSPI" TruthPointing "StkObject" MeasuredObject "Aircraft/ShipTrack_Measured" ReferenceSystem "Aircraft/Ownship1 NorthWestUp System" TimePath "Aircraft/Ownship1 AvailabilityTimeSpan EventInterval"');
 
%% Display some results from the track comparison
trackComparisonCSVCommand = ['TE_TrackComparison * Export Name "TrackCompareShip" File ' '"' scenarioFolder '\shipTrackCompare.csv"'];
root.ExecuteCommand(trackComparisonCSVCommand); %outputs the results table to a csv in the scenario directory

%Output Min, Max, Avg, Std Dev, and RMS for MOPs to command window
trackCompareShipResults = root.ExecuteCommand('TE_TrackComparisonCalculator_RM * Name "TrackCompareShip"');
for i = 0:trackCompareShipResults.Count-1
    result = trackCompareShipResults.Item(i);
    fprintf('%s \n',result);
end

%% Create some quick looks
disp('Creating quick Looks');
root.ExecuteCommand('TE_QuickLooks * Create From "Ownship1" To "Target1"');
root.ExecuteCommand('TE_QuickLooks * Create From "Ownship1" To "Target2"');

%% Change the properties of some quicklooks
% When modifying Quicklooks properties through connect the value used for the name parameter is the name
% of the Vector Geometry object that exist in Analysis Workbench. Some formatting Examples:
% Range vectors follow the format RangeVector_<From Object Name>_<To Object Name>.
% Angles follow the format <Angle Name><From or To><To Object Name>
% command examples follow
disp('Changing QuickLook Graphics');
root.ExecuteCommand('TE_QuickLooks * Set Ownship "Ownship1" Name "RangeVector_Ownship1_Target1_TSPI" Color "Red" Show "On" ShowValue "Off"');
root.ExecuteCommand('TE_QuickLooks * Set Ownship "Ownship1" Name "BodyAspectToTarget1_TSPI" Color "Yellow" Show "On" ShowValue "Off"');

%% Add sensor and axes required for sensor quick look then run sensor quicklook
disp('Adding a sensor and performing a Sensor Quick Look');
root.ExecuteCommand('New / */Aircraft/Ownship1/Sensor Sensor1');
root.ExecuteCommand('CalculationTool * Aircraft/Ownship1/Sensor/Sensor1 Create Axes Sensor1_NativeAxes_01 "Fixed in Axes"');
root.ExecuteCommand('TE_SensorQuickLooks * Create from "Sensor1" to "Target1_TSPI" Axes "Sensor1_NativeAxes_01" Frame "spherical"');
root.ExecuteCommand('TE_SensorQuickLooks * Graph from "Sensor1" to "Target1_TSPI" Name "AzEl" Frame "spherical"');
root.ExecuteCommand('TE_SensorQuickLooks * Vector from "Sensor1" to "Target1_TSPI" Name "Boresight" Axes "Sensor1_NativeAxes_01" Show "ON"');


%% Create A vector, create a graph then save an image of the graph.
disp('Creating a vector, a graph, and saving graph as an image');
root.ExecuteCommand('TE_Vector * Add Name "AirTracksVector" AnalysisObject "Ownship1" Mapping "AirTracksVector" Source "Air Tracks - Track Event.csv" Point Path "Center"');
root.ExecuteCommand('TE_Graph * Add Name "OwnshipLatVsLon" AnalysisObject "Ownship1" GraphXY PointSize "Medium" Segment Color "%155000100" Point "1.0" Data Element "Ownship.csv|Longitude, Ownship.csv|Latitude" Labels "Longitude, Latitude" Units "deg, deg" Time Constraint "RADAR_Run1" ');

graph1_png_path = fullfile(scenarioFolder,'OwnshipLatVsLon.png');
createGraphCommand = ['TE_Graph * Save Name "OwnshipLatVsLon" AnalysisObject "Ownship1" File ' '"' graph1_png_path '"'];
root.ExecuteCommand(createGraphCommand);

disp('DONE!')
 
%% Save all your changes
root.SaveScenario(); 

 
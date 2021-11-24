%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Realtime Satellite Propagation w/ MATLAB Clock
%
% Description: This script simulates a realtime satellite propagation where
% position/velocity/attitude data is coming from an external source. In
% this case the external source is a text file of states whereas the same
% process would be followed if data was streaming in from another source.
% The simulation runs off of a timer where MATLAB dictates the time step to
% STK.
%
% Programming Help: https://help.agi.com/stkdevkit/index.htm#automationTree/treeOver.htm?TocPath=_____1
% Connect Command Reference: https://help.agi.com/stkdevkit/index.htm#../Subsystems/connect/Content/theVeryTop.htm
% Object Model Reference: https://help.agi.com/stkdevkit/index.htm#automationTree/objModel.htm?TocPath=Using%2520Core%2520Libraries%257CSTK%2520Object%2520Model%257C_____0
% MATLAB Code Snippets: https://help.agi.com/stkdevkit/index.htm#stkObjects/ObjModMatlabCodeSamples.htm?Highlight=matlab
% RealTime Propagator: https://help.agi.com/stk/index.htm#stk/veh_propagator_realTime.htm
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


%% Housekeeping
close all
clear all
clc

%% Establish STK Connection
%Attempt to grab an open STK instance, if there are none then create a new
%instance and connect to it
try
    %Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
catch
    %STK is not running, launch new instance
    %Launch a new instance of STK and grab it
    uiapp = actxserver('STK12.application');
end
%Get the root from the personality it has two... get the second, its the 
%newer STK Object Model Interface as documented in the STK Help
root = uiapp.Personality2;
%Set visible to true (show STK GUI)
uiapp.visible = 1;

%% Create a New STK Scenario
%Try closing an open scenario and opening a new one named 'RealTimeTest', 
%if that fails then there isn't a scenario open so just open a new one
try
    root.CloseScenario();
    root.NewScenario('RealTimeTest');
catch
    root.NewScenario('RealTimeTest');
end
%Get the scenario root, its of type IAgScenario
%IAgScenario documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgScenario.html?Highlight=IAgScenario
scenObj = root.CurrentScenario;

%% Configure Scenario Time and Animation
%Set the object model to expect all dates in Local Gregorian
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('LCLG');
%Set the Connect module to expect all dates in Local Gregorian
root.ExecuteCommand('SetUnits / GregorianLOCAL');
%Get the system clock time and use that to set up the scenario's start and
%stop time.
tomorrow_date = datestr((now+1), 'dd mmm yyyy HH:MM:SS.FFF');
current_date = datestr((now), 'dd mmm yyyy HH:MM:SS.FFF');
%Set scenario epoch (reference for all relative times)
scenObj.Epoch = current_date;
%Set scenario start and stop times
scenObj.StopTime = tomorrow_date;
scenObj.StartTime = current_date;
%Set the scenario's animation properties to animate in realtime mode
%IAgScAnimcation documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgScAnimation.html?Highlight=IAgScAnimation
scAnimation = scenObj.Animation;
%AgEScTimeStepType types: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgScAnimation.html?Highlight=IAgScAnimation
scAnimation.AnimStepType = 'eScTimeStep';
 
%% Create Satellite Object
%Create a new satellite object named "Satellite1"
%IAgStkObject documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgStkObjectCollection.html?Highlight=IAgStkObjectCollection
%AgEStkObjectType types: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgStkObjectCollection.html?Highlight=IAgStkObjectCollection
satellite = scenObj.Children.New('eSatellite', 'Satellite1');

%% Configure Position Propagation
%Set realtime propagator and configure look ahead
%Realtime connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_RealTime.htm?Highlight=Realtime
root.ExecuteCommand('Realtime */Satellite/Satellite1 SetProp')
root.ExecuteCommand('Realtime */Satellite/Satellite1 SetLookAhead HoldCBIPosition 3600 60 3600')

%% Configure Attitude Propagation
%Set realtime attitude propagation without interpolation
%SetAttitude connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_SetAttitudeRealTime.htm?Highlight=setattitude
root.ExecuteCommand('SetAttitude */Satellite/Satellite1 RealTime Hold 3600 3600')

%% Rewind Animation
%reset the VO window to the scenario start time
root.Rewind

%% Configure 3D Graphics Data Displays
%Remove all data displays to be able to add more by name
satellite.VO.DataDisplay.RemoveAll();
%Add J2000 Position and Velocity data display
%IAgVODataDisplayElement documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVODataDisplayElement.html?Highlight=IAgVODataDisplayElement
positionDataDisplay = satellite.VO.DataDisplay.Add('J2000 Position Velocity');
positionDataDisplay.IsVisible = 1;
% Change font color to green check out rgb2stkColor.m here: https://github.com/AnalyticalGraphicsInc/STKCodeExamples/tree/master/StkAutomation/Matlab/General_Utilities
positionDataDisplay.FontColor = '000255000';
%Add Attitude Quaternions data display
attitudeDataDisplay = satellite.VO.DataDisplay.Add('Attitude Quaternions');
attitudeDataDisplay.IsVisible = 1;
% Change the y location to 200 pixels
attitudeDataDisplay.Y = 200;
% Change font color to white check out rgb2stkColor.m here: https://github.com/AnalyticalGraphicsInc/STKCodeExamples/tree/master/StkAutomation/Matlab/General_Utilities
attitudeDataDisplay.FontColor = '255255000';


%% Open Position/Attitude Data Files
%Open the file with all positional data (this wouldn't be necessary in a
%realworld application where you'd be getting the realtime data from an
%outside application)
posFile = fopen('SatellitePosition.txt','r');
fseek(posFile, 0, 'eof');
eof_byte = ftell(posFile);
fseek(posFile, 0, 'bof');
%Repeat the same thing for the attitude data
attFile = fopen('SatelliteAttitude.txt','r');
fseek(attFile, 0, 'eof');
eof_byte2 = ftell(attFile);
fseek(attFile, 0, 'bof');

%% Configure Simulation Execution
%Set up a matlab timer object.  The TimerFcn callback function will be
%called every one second, where it will read a line from the position and
%attitude files and send that data into STK.  It will also update the
%current animation time in STK.  Therefore MATLAB will completely control
%the STK clock.  
t = timer;
t.ExecutionMode = 'fixedRate';
t.BusyMode = 'drop';
t.Period = 0.1;
t.TimerFcn = {@satelliteUpdate, posFile, attFile, root};

%% Execute Simulation
%run stop(t) on the command line to kill the timer object
start(t)

%% Satellite Update Function (Executed Using Timer Object)
function satelliteUpdate(obj, event, posFile, attFile, root)

    %Get current animation time
    dateString = datestr(event.Data.time, 'dd mmm yyyy HH:MM:SS.FFF');

    %Grab next line in position data file
    data_line = fgetl(posFile);
    %Parse line by spaces
    spLine = regexp(data_line, '\s', 'split');
    %Break out each component in the data line
    epsec = spLine{1}; %Unused
    x_pos = spLine{2};
    y_pos = spLine{3};
    z_pos = spLine{4};
    x_vel = spLine{5};
    y_vel = spLine{6};
    z_vel = spLine{7};
    
    %Grab next line in attitude data file
    att_line = fgetl(attFile);
    %Parse line by spaces
    spLine = regexp(att_line, '\s', 'split');
    %Break out each component in the data line
    epsec = spLine{1}; %Unused
    q1 = spLine{2};
    q2 = spLine{3};
    q3 = spLine{4};
    q4 = spLine{5};

    %We'll use the timer update as the scenario Epoch Seconds.  Convert the
    %timer seconds into STK Local Gregorian
    matlabEpSec = root.ConversionUtility.ConvertDate('LCLG', 'EpSec', dateString);
    
    %Push position point to STK scenario
    %SetPosition connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_SetPositionVehicles.htm?Highlight=setposition
    root.ExecuteCommand(['SetPosition */Satellite/Satellite1 ECI "' dateString '" ' x_pos ' ' y_pos ' ' z_pos ' ' x_vel ' ' y_vel ' ' z_vel]);
    %Push attitude point to STK scenario
    %AddAttitude connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_AddAttitudeQuat.htm?Highlight=addattitude
    root.ExecuteCommand(['AddAttitude */Satellite/Satellite1 Quat "' dateString '" ' q1 ' ' q2 ' ' q3 ' ' q4]);
    %Set current time in STK scenario
    root.CurrentTime = str2double(matlabEpSec);

    %Output the time, quat, position to the command window
    fprintf(['Time: ' dateString '\nPosition: ' x_pos ' ' y_pos ' ' z_pos ...
        '\nAttitude: ' q1 ' ' q2 ' ' q3 ' ' q4 '\n']);
end
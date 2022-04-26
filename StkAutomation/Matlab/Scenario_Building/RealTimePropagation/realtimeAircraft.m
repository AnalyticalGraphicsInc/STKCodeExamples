%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Realtime Aircraft Propagation
%
% Description: This script simulates a realtime aircraft propagation where
% position/velocity/attitude data is coming from an external source. In
% this case the external source is a text file of states whereas the same
% process would be followed if data was streaming in from another source.
% The simulation runs off of a timer where MATLAB dictates the time step to
% STK.
%
% Programming Help: https://help.agi.com/stkdevkit/index.htm#automationTree/treeOver.htm
% Connect Command Reference: https://help.agi.com/stkdevkit/index.htm#../Subsystems/connect/Content/theVeryTop.htm
% Object Model Reference: https://help.agi.com/stkdevkit/index.htm#automationTree/objModel.htm
% MATLAB Code Snippets: https://help.agi.com/stkdevkit/index.htm#stkObjects/ObjModMatlabCodeSamples.htm
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
%IAgScenario documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgScenario.html
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
%IAgScAnimcation documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgScAnimation.html
scAnimation = scenObj.Animation;
%AgEScTimeStepType types: https://help.agi.com/stkdevkit/#DocX/STKObjects~Enumerations~AgEScTimeStepType_EN.html
scAnimation.AnimStepType = 'eScRealTime';

%% Create Aircraft Object
%create a new aicraft object named "Aircraft1"
aircraft = scenObj.Children.New('eAircraft', 'Aircraft1');
aircraft.VO.Model.ModelData.Filename = 'C:\Program Files\AGI\STK 12\STKData\VO\Models\Air\f-18c_hornet.mdl';

%% Configure Position Propagation
%Set realtime propagator and configure look ahead
%Realtime connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_RealTime.htm
root.ExecuteCommand('Realtime */Aircraft/Aircraft1 SetProp')
root.ExecuteCommand('Realtime */Aircraft/Aircraft1 SetLookAhead HoldCBFPosition 60 60 60')

%% Configure Attitude Propagation
%SetAttitude RealTime connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_SetAttitudeRealTime.htm
root.ExecuteCommand('SetAttitude */Aircraft/Aircraft1 RealTime Hold 60 60')
%SetAttitude DataReference connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_SetAttitudeDataReference.htm
root.ExecuteCommand('SetAttitude */Aircraft/Aircraft1 DataReference Fixed Quat 0 0 0 1 "CentralBody/Earth Fixed"');

%% Begin Animation
%Reset the VO window and then begin playing the animation (in realtime)
root.Rewind
root.PlayForward

%% Configure 3D Graphics Data Displays
%Remove all data displays to be able to add more by name
aircraft.VO.DataDisplay.RemoveAll();
%Add LLA Position data display
%IAgVODataDisplayElement documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVODataDisplayElement.html
positionDataDisplay = aircraft.VO.DataDisplay.Add('LLA Position');
positionDataDisplay.IsVisible = 1;
% Change font color to green check out rgb2stkColor.m here: https://github.com/AnalyticalGraphicsInc/STKCodeExamples/tree/master/StkAutomation/Matlab/General_Utilities
positionDataDisplay.FontColor = '000255000';
%Add Velocity Heading data display
velocityDataDisplay = aircraft.VO.DataDisplay.Add('Velocity Heading');
velocityDataDisplay.IsVisible = 1;
% Change the y location to 200 pixels
velocityDataDisplay.Y = 200;
% Change font color to white check out rgb2stkColor.m here: https://github.com/AnalyticalGraphicsInc/STKCodeExamples/tree/master/StkAutomation/Matlab/General_Utilities
velocityDataDisplay.FontColor = '255255000';

%% Open Position/Attitude Data Files
%Open the file with all positional data (this wouldn't be necessary in a
%realworld application where you'd be getting the realtime data from an
%outside application)
posFile = fopen('FighterPosition.txt','r');
fseek(posFile, 0, 'eof');
eof_byte = ftell(posFile);
fseek(posFile, 0, 'bof');
%Repeat the same thing for the attitude data
attFile = fopen('FighterAttitude.txt','r');
fseek(attFile, 0, 'eof');
eof_byte2 = ftell(attFile);
fseek(attFile, 0, 'bof');

%% Propagate with Real Time Data
%Loop through each line of the position and attitude files and pass the
%data into STK.  The data in the files was generated with a 1 second time
%step, so i put a pause command at the bottom of the loop to simulate that
%we actually receive the data every one second.  This is not precise.
primID = 1;
while ftell(posFile) < eof_byte
    
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

    %check what the current system clock time is, use this as the time
    %stamp for the data to be passed into STK
    curTime = datestr((now), 'dd mmm yyyy HH:MM:SS.FFF');
    %Push position point to STK scenario
    %SetPosition connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_SetPositionVehicles.htm
    root.ExecuteCommand(['SetPosition */Aircraft/Aircraft1 ECF "' curTime '" ' x_pos ' ' y_pos ' ' z_pos ' ' x_vel ' ' y_vel ' ' z_vel]);
    %Push attitude point to STK scenario  
    %AddAttitude connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_AddAttitudeQuat.htm
    root.ExecuteCommand(['AddAttitude */Aircraft/Aircraft1 Quat "' curTime '" ' q1 ' ' q2 ' ' q3 ' ' q4]);
    
    %output the time, quat, position to the matlab screen
    fprintf(['Time: ' curTime '\nPosition: ' x_pos ' ' y_pos ' ' z_pos ...
        '\nAttitude: ' q1 ' ' q2 ' ' q3 ' ' q4 '\n']);
    
    %Pause for 1 second
    pause(0.1)
end





%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Realtime Missile Propagation with Articulations
%
% Description: This script simulates a realtime missile propagation where
% position/velocity/attitude data is coming from an external source. 
% Articulations are also included to demonstrate real time staging. In
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

%set the object model to expect all dates in Local Gregorian
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
%set the Connect module to expect all dates in Local Gregorian
root.ExecuteCommand('SetUnits / EpSec');

%% Configure Scenario Time and Animation
%Set the scenario times manually so that the attitude data is properly synced
scenObj.Epoch = '22 May 2013 23:00:00.000';
scenObj.StartTime = '22 May 2013 23:00:00.000';
scenObj.StopTime = '23 May 2013 04:00:00.000';

%Set the scenario's animation properties to animate in x realtime mode
%IAgScAnimcation documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgScAnimation.html?Highlight=IAgScAnimation
scAnimation = scenObj.Animation;
%AgEScTimeStepType types: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgScAnimation.html?Highlight=IAgScAnimation
scAnimation.AnimStepType = 'eScXRealTime';

%% Rewind Animation
%Reset the VO window and then begin playing the animation (in realtime)
root.Rewind

%% Create Missile Object
%create a new missile object named "Missile1"
missile = scenObj.Children.New('eMissile', 'Missile1');
missile.VO.Model.ModelData.Filename = 'C:\Program Files\AGI\STK 12\STKData\VO\Models\Missiles\taepodong-2.mdl';

%% Configure Position Propagation
%Set realtime propagator and configure look ahead
%Realtime connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_RealTime.htm?Highlight=Realtime
root.ExecuteCommand('Realtime */Missile/Missile1 SetProp');
root.ExecuteCommand('Realtime */Missile/Missile1 SetLookAhead HoldCBFPosition 60 60 60');

%% Configure Attitude Propagation
%SetAttitude RealTime connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_SetAttitudeRealTime.htm?Highlight=setattitude
root.ExecuteCommand('SetAttitude */Missile/Missile1 RealTime Extrapolate 60 60');

%% Configure 3D Graphics Data Displays
%Remove all data displays to be able to add more by name
missile.VO.DataDisplay.RemoveAll();
%Add J2000 Position and Velocity data display
%IAgVODataDisplayElement documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVODataDisplayElement.html?Highlight=IAgVODataDisplayElement
positionDataDisplay = missile.VO.DataDisplay.Add('J2000 Position Velocity');
positionDataDisplay.IsVisible = 1;
% Change font color to green check out rgb2stkColor.m here: https://github.com/AnalyticalGraphicsInc/STKCodeExamples/tree/master/StkAutomation/Matlab/General_Utilities
positionDataDisplay.FontColor = '000255000';
%Add Attitude Quaternions data display
attitudeDataDisplay = missile.VO.DataDisplay.Add('Attitude Quaternions');
attitudeDataDisplay.IsVisible = 1;
% Change the y location to 200 pixels
attitudeDataDisplay.Y = 200;
% Change font color to white check out rgb2stkColor.m here: https://github.com/AnalyticalGraphicsInc/STKCodeExamples/tree/master/StkAutomation/Matlab/General_Utilities
attitudeDataDisplay.FontColor = '255255000';

%% Open Position/Attitude Data Files
%Open the file with all positional data (this wouldn't be necessary in a
%realworld application where you'd be getting the realtime data from an
%outside application)
posFile = fopen('MissilePosition.txt','r');
fseek(posFile, 0, 'eof');
eof_byte = ftell(posFile);
fseek(posFile, 0, 'bof');
%Repeat the same thing for the attitude data
attFile = fopen('MissileAttitude.txt','r');
fseek(attFile, 0, 'eof');
eof_byte2 = ftell(attFile);
fseek(attFile, 0, 'bof');

%% Begin Animation
root.PlayForward;

%% Propagate with Real Time Data
%Loop through each line of the position and attitude files and pass the
%data into STK.  The data in the files was generated with a 0.1 second time
%step, this seems to give a consistant stream of data which keeps up with
%real-time.  This is not precise.
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

    if str2double(epsec) == 0
        %send the first couple of articulation commands
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 2 Stage1Flame Size 0 1']);
    elseif str2double(epsec) == 30.0
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 2 Stage1Flame Size 1 0']);
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 2 Stage2Flame Size 0 1']);
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 25 Stage1 MoveX 0 -900']);
    elseif str2double(epsec) == 45.0
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 0.1 Stage1 Size 1 0']);
    elseif str2double(epsec) == 60.0
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 2 Stage2Flame Size 1 0']);
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 2 Stage3Flame Size 0 1']);
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 25 Stage2 MoveX 0 -900']);
    elseif str2double(epsec) == 75.0
        root.ExecuteCommand(['VO */Missile/Missile1 Articulate "' num2str(root.CurrentTime + 1) '" 0.1 Stage2 Size 1 0']);
    end
    
    %Check what the current system clock time is, use this as the time
    %stamp for the data to be passed into STK
    %SetPosition connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_SetPositionVehicles.htm?Highlight=setposition
    root.ExecuteCommand(['SetPosition */Missile/Missile1 ECF "' epsec '" ' x_pos ' ' y_pos ' ' z_pos ' ' x_vel ' ' y_vel ' ' z_vel]);
    %AddAttitude Quat connect command: https://help.agi.com/stk/index.htm#../Subsystems/connectCmds/Content/cmd_AddAttitudeQuat.htm?Highlight=addattitude
    root.ExecuteCommand(['AddAttitude */Missile/Missile1 Quat "' epsec '" ' q1 ' ' q2 ' ' q3 ' ' q4]);
    
    %output the time, quat, position to the matlab screen
    fprintf(['Time: ' epsec '\nPosition: ' x_pos ' ' y_pos ' ' z_pos ...
        '\nAttitude: ' q1 ' ' q2 ' ' q3 ' ' q4 '\n']);
    
    %Pause for .1 seconds
    pause(0.01)
end





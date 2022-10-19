%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Realtime Satellite Propagation
%
% Description: This script simulates a realtime satellite propagation where
% position/velocity/attitude data is coming from an external source. In
% this case the external source is a text file of states whereas the same
% process would be followed if data was streaming in from another source.
% The simulation runs on a fixed one second time step cadence.
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
%Set the object model to expect all distances in meters
root.UnitPreferences.Item('Distance').SetCurrentUnit('m');
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

%% Create Satellite Object
%Create a new satellite object named "Satellite1"
%IAgStkObject documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgStkObjectCollection.html
%AgEStkObjectType types: https://help.agi.com/stkdevkit/#DocX/STKObjects~Enumerations~AgESTKObjectType_EN.html
satellite = scenObj.Children.New('eSatellite', 'Satellite1');

%% Configure Position Propagation
%Set the satellite to expect real-time position and attitude data
%IAgSatellite documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgSatellite.html
%AgEVePropagatorType types: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~Enumerations~AgEVePropagatorType_EN.html
satellite.SetPropagatorType('ePropagatorRealtime');
%Use simple two body propagation for the look ahead time
%IAgVePropagatorRealtime documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVePropagatorRealtime.html
%AgELookAheadPropagator types: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~Enumerations~AgELookAheadPropagator_EN.html
satellite.Propagator.LookAheadPropagator = 'eLookAheadTwoBody';
%Set look ahead and look behind duration to 1 min (input in seconds)
%This setting also applies to attitude real time propagation
satellite.Propagator.Duration.LookAhead = 60.00;
satellite.Propagator.Duration.LookBehind = 60.00;
%Set propagation time step to 60 seconds
satellite.Propagator.TimeStep = 60.0;
%Initialize the real time propagator
satellite.Propagator.Propagate;

%% Configure Attitude Propagation
%Set attitude propagation type to real time
%AgEVeAttitude types: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~Enumerations~AgEVeAttitude_EN.html
satellite.SetAttitudeType('eAttitudeRealTime');
%Set look ahead method to hold at last attitude point
%AgEVeLookAheadMethod types: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~Enumerations~AgEVeLookAheadMethod_EN.html
satellite.Attitude.LookAheadMethod = 'eHold';

%% Begin Animation
%Reset the VO window and then begin playing the animation (in realtime)
root.Rewind
root.PlayForward

%% Configure 3D Graphics Data Displays
%Remove all data displays to be able to add more by name
satellite.VO.DataDisplay.RemoveAll();
%Add J2000 Position and Velocity data display
%IAgVODataDisplayElement documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVODataDisplayElement.html
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

%% Propagate with Real Time Data
%Loop through each line of the position and attitude files and pass the
%data into STK. The data in the files was generated with a 1 second time
%step, so there's a pause command at the bottom of the loop to simulate that
%we actually receive the data every one second.
primID = 1;
%Get reference to real time point builder
%IAgVeRealtimePointBuilder documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVeRealtimePointBuilder.html
pointBuilder = satellite.Propagator.PointBuilder;

%Loop until end of data file
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

    %Get system clock time to be used as input time for data point
    curTime = datestr((now), 'dd mmm yyyy HH:MM:SS.FFF');
    %Push position/velocity data point
    %IAgVeRealtimeCartesianPoints documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVeRealtimePointBuilder.html
    pointBuilder.ECI.Add(curTime,...
        str2double(x_pos), str2double(y_pos), str2double(z_pos),...
        str2double(x_vel), str2double(y_vel), str2double(z_vel));
    %Push attitude data point
    %IAgVeAttitudeRealTime documentation: https://help.agi.com/stkdevkit/index.htm#DocX/STKObjects~IAgVeAttitudeRealTime.html
    satellite.Attitude.AddQuaternion(curTime,...
        str2double(q1), str2double(q2), str2double(q3), str2double(q4));
    %Output the time, quat, position to the command window
    fprintf(['Time: ' curTime '\nPosition: ' x_pos ' ' y_pos ' ' z_pos ...
        '\nAttitude: ' q1 ' ' q2 ' ' q3 ' ' q4 '\n']);
    %Pause 1 second to simulate real time
    pause(0.1)
end
close all
clear all
clc

%Initialize 
%Establish the connection
try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
catch
    % STK is not running, launch new instance
    % Launch a new instance of STK and grab it
    uiapp = actxserver('STK12.application');
end

%get the root from the personality
%it has two... get the second, its the newer STK Object Model Interface as
%documented in the STK Help
root = uiapp.Personality2;

% set visible to true (show STK GUI)
uiapp.visible = 1;

%%From the STK Object Root you can command every aspect of the STK GUI

%close current scenario or open new one
try
    root.CloseScenario();
    root.NewScenario('RealTimeTest');
catch
    root.NewScenario('RealTimeTest');
end

%get the scenario root, its of type IAgScenario 
scenObj = root.CurrentScenario;

%set the object model to expect all dates in Local Gregorian, or clock time
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('LCLG');
%set the Connect module to expect all dates in Local Gregorian
root.ExecuteCommand(['SetUnits / GregorianLOCAL']);

%Get the system clock time and use that to set up the scenario's start and
%stop time.
tomorrow_date = datestr((now+1), 'dd mmm yyyy HH:MM:SS.FFF');
current_date = datestr((now), 'dd mmm yyyy HH:MM:SS.FFF');

scenObj.Epoch = current_date;
scenObj.StopTime = tomorrow_date;
scenObj.StartTime = current_date;

%set the scenario's animation properties to animate in simulated time
scAnimation = scenObj.Animation;
scAnimation.AnimStepType = 'eScTimeStep';

%reset the VO window to the scenario start time
root.Rewind
 
%create a new satellite object named "Satellite1"
satellite = scenObj.Children.New('eSatellite', 'Satellite1');

%set the satellite to expect realtime position and attitude data
root.ExecuteCommand('Realtime */Satellite/Satellite1 SetProp')
root.ExecuteCommand('Realtime */Satellite/Satellite1 SetLookAhead HoldCBIPosition 3600 60 3600')
root.ExecuteCommand('SetAttitude */Satellite/Satellite1 RealTime Hold 3600 3600')

%find the J2000 Position and Velocity data display, turn them on in 3D 
for i = 0:satellite.VO.DataDisplay.Count-1
    if (strcmp(satellite.VO.DataDisplay.Item(i).Name, 'J2000 Position Velocity'))
        posDD = satellite.VO.DataDisplay.Item(i);
        posDD.IsVisible = 1;
        posDD.FontColor = '000255000';
    elseif (strcmp(satellite.VO.DataDisplay.Item(i).Name, 'Attitude Quaternions'))
        attDD = satellite.VO.DataDisplay.Item(i);
        attDD.IsVisible = 1;
        attDD.Y = 180;
        attDD.FontColor = '255255000';
    end
end


%open the file with all positional data (this wouldn't be necessary in a
%realworld application where you'd be getting the realtime data from an
%outside application.
fid = fopen('SatellitePosition.txt','r');
fseek(fid, 0, 'eof');
eof_byte = ftell(fid);
fseek(fid, 0, 'bof');

%do the same thing for the attitude data
fid2 = fopen('SatelliteAttitude.txt','r');
fseek(fid2, 0, 'eof');
eof_byte2 = ftell(fid2);
fseek(fid2, 0, 'bof');

%Set up a matlab timer object.  The TimerFcn callback function will be
%called every one second, where it will read a line from the position and
%attitude files and send that data into STK.  It will also update the
%current animation time in STK.  Therefore MATLAB will completely control
%the STK clock.  
t = timer;
t.ExecutionMode = 'fixedRate';
t.BusyMode = 'drop';
t.Period = 0.1;
t.TimerFcn = {@satelliteUpdate, fid, fid2, root};

start(t)

%run stop(t) on the command line to kill the timer object
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

%set the object model to expect all dates in Local Gregorian
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('LCLG');
%set the object model to expect all distances in meters
root.UnitPreferences.Item('Distance').SetCurrentUnit('m');

%Get the system clock time and use that to set up the scenario's start and
%stop time.
tomorrow_date = datestr((now+1), 'dd mmm yyyy HH:MM:SS.FFF');
current_date = datestr((now), 'dd mmm yyyy HH:MM:SS.FFF');

scenObj.Epoch = current_date;
scenObj.StopTime = tomorrow_date;
scenObj.StartTime = current_date;

%set the scenario's animation properties to animate in realtime mode
scAnimation = scenObj.Animation;
scAnimation.AnimStepType = 'eScRealTime';
 
%create a new satellite object named "Satellite1"
satellite = scenObj.Children.New('eSatellite', 'Satellite1');

%set the satellite to expect realtime position and attitude data
satellite.SetPropagatorType('ePropagatorRealtime');
satellite.Propagator.LookAheadPropagator = 'eLookAheadTwoBody';
satellite.Propagator.Duration.LookAhead = 3600.00;
satellite.Propagator.Duration.LookBehind = 3600.00;
satellite.Propagator.TimeStep = 60.0;
satellite.Propagator.Propagate;

satellite.SetAttitudeType('eAttitudeRealTime');
satellite.Attitude.LookAheadMethod = 'eHold';
satellite.Attitude.Duration.LookAhead = 60.0;
satellite.Attitude.Duration.LookBehind = 60.0;

%reset the VO window and then begin playing the animation (in realtime)
root.Rewind
root.PlayForward

%find the J2000 Position and Velocity data display 
for i = 0:satellite.VO.DataDisplay.Count-1
    if (strcmp(satellite.VO.DataDisplay.Item(i).Name, 'J2000 Position Velocity'))
        posDD = satellite.VO.DataDisplay.Item(i);
        posDD.IsVisible = 1;
        posDD.FontColor = '000255000';
    elseif (strcmp(satellite.VO.DataDisplay.Item(i).Name, 'Attitude Quaternions'))
        attDD = satellite.VO.DataDisplay.Item(i);
        attDD.IsVisible = 1;
        attDD.Y = 200;
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

%loop through each line of the position and attitude files and pass the
%data into STK.  The data in the files was generated with a 1 second time
%step, so i put a pause command at the bottom of the loop to simulate that
%we actually receive the data every one second.  This is not precise.
primID = 1;
while ftell(fid) < eof_byte
    
    %call the functions to read the data lines from the file and pass back
    %the position and attitude data
    [epsec, x_pos, y_pos, z_pos, x_vel, y_vel, z_vel] = get_posvel_data(fid);
    [epsec, q1, q2, q3, q4] = get_attitude_data(fid2);

    %check what the current system clock time is, use this as the time
    %stamp for the data to be passed into STK
    curTime = datestr((now), 'dd mmm yyyy HH:MM:SS.FFF');
    satellite.Propagator.PointBuilder.ECI.Add(curTime,...
        str2double(x_pos), str2double(y_pos), str2double(z_pos),...
        str2double(x_vel), str2double(y_vel), str2double(z_vel));
    satellite.Attitude.AddQuaternion(curTime,...
        str2double(q1), str2double(q2), str2double(q3), str2double(q4));
    
    %output the time, quat, position to the matlab screen
    disp(sprintf(['Time: ' curTime '\nPosition: ' x_pos ' ' y_pos ' ' z_pos ...
        '\nAttitude: ' q1 ' ' q2 ' ' q3 ' ' q4 '\n']));
    pause(0.1)
end





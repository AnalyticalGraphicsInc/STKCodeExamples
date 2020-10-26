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
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
%set the Connect module to expect all dates in Local Gregorian
root.ExecuteCommand(['SetUnits / EpSec']);

%set the scenario times so that the attitude data is properly synced
scenObj.Epoch = '22 May 2013 23:00:00.000';
scenObj.StartTime = '22 May 2013 23:00:00.000';
scenObj.StopTime = '23 May 2013 04:00:00.000';

%set the scenario's animation properties to animate in realtime mode
scAnimation = scenObj.Animation;
scAnimation.AnimStepType = 'eScXRealTime';

%reset the VO window and then begin playing the animation (in realtime)
root.Rewind
 
%create a new missile object named "missile1"
missile = scenObj.Children.New('eMissile', 'Missile1');
missile.VO.Model.ModelData.Filename = 'C:\Program Files\AGI\STK 12\STKData\VO\Models\Missiles\taepodong-2.mdl';


%set the missile to expect realtime position and attitude data
root.ExecuteCommand('Realtime */Missile/Missile1 SetProp');
root.ExecuteCommand('Realtime */Missile/Missile1 SetLookAhead HoldCBFPosition 3600 60 3600');
root.ExecuteCommand('SetAttitude */Missile/Missile1 RealTime Extrapolate 3600 3600');

%find the J2000 Position and Velocity data display 
for i = 0:missile.VO.DataDisplay.Count-1
    if (strcmp(missile.VO.DataDisplay.Item(i).Name, 'J2000 Position Velocity'))
        posDD = missile.VO.DataDisplay.Item(i);
        posDD.IsVisible = 1;
        posDD.FontColor = '000255000';
    elseif (strcmp(missile.VO.DataDisplay.Item(i).Name, 'Attitude Quaternions'))
        attDD = missile.VO.DataDisplay.Item(i);
        attDD.IsVisible = 1;
        attDD.Y = 200;
        attDD.FontColor = '255255000';
    end
end

%open the file with all positional data (this wouldn't be necessary in a
%realworld application where you'd be getting the realtime data from an
%outside application.
fid = fopen('MissilePosition.txt','r');
fseek(fid, 0, 'eof');
eof_byte = ftell(fid);
fseek(fid, 0, 'bof');

%do the same thing for the attitude data
fid2 = fopen('MissileAttitude.txt','r');
fseek(fid2, 0, 'eof');
eof_byte2 = ftell(fid2);
fseek(fid2, 0, 'bof');

root.PlayForward;
%loop through each line of the position and attitude files and pass the
%data into STK.  The data in the files was generated with a 0.1 second time
%step, this seems to give a consistant stream of data which keeps up with
%real-time.  This is not precise.
while ftell(fid) < eof_byte
    
    %call the functions to read the data lines from the file and pass back
    %the position and attitude data
    [epsec, x_pos, y_pos, z_pos, x_vel, y_vel, z_vel] = get_posvel_data(fid);
    [epsec, q1, q2, q3, q4] = get_attitude_data(fid2);

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
    
    %check what the current system clock time is, use this as the time
    %stamp for the data to be passed into STK
    root.ExecuteCommand(['SetPosition */Missile/Missile1 ECF "' epsec '" ' x_pos ' ' y_pos ' ' z_pos ' ' x_vel ' ' y_vel ' ' z_vel]);
    root.ExecuteCommand(['AddAttitude */Missile/Missile1 Quat "' epsec '" ' q1 ' ' q2 ' ' q3 ' ' q4]);
    
    %output the time, quat, position to the matlab screen
    disp(sprintf(['Time: ' epsec '\nPosition: ' x_pos ' ' y_pos ' ' z_pos ...
        '\nAttitude: ' q1 ' ' q2 ' ' q3 ' ' q4 '\n']));
end





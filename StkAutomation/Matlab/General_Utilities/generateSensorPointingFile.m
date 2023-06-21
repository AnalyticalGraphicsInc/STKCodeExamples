function generateSensorPointingFile(sensorPath, inputFileName, stepSize)
%
% This function will generate an attitude pointing file (.sp) for the 
% specified sensor, using quaternions. For targeted sensors and/or sensors 
% with multiple access intervals, this script will create multiple pointing
% files for each interval, and name them according to their start and stop 
% times in EpSec.
%
% Created By: Max Housner (max.housner@ansys.com)
% Date Created: 05/16/2023
% Last Modified: 05/18/2023
%
% =============  INPUTS  =============
%
% sensorPath (char array): Specify object path of sensor
% inputFileName (char array): Specify filename for .sp file
% stepSize (int): Set the step size (sec) for the sensor pointing file
%
% EXAMPLE:
% generateSensorPointingFile('Satellite/LEOSat/Sensor/LEOsensor', 'LEOSensorAttitude', 60)

% =============  BEGIN FUNCTION  =============

% Initilize
% Grab an existing instance of STK
uiapp = actxGetRunningServer('STK12.application');
root = uiapp.Personality2;
scen = root.CurrentScenario;
% Set units to EpSec
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Get object handle
splitStr = split(sensorPath,'/');
objType = splitStr{1};
objName = splitStr{2};
sensorName = splitStr{4};
if scen.Children.Contains(['e',objType],objName)
    obj = root.GetObjectFromPath([objType,'/',objName]);
    if obj.Children.Contains('eSensor',sensorName)
        sensor = obj.Children.Item(sensorName);
    else
        disp(['No object ',sensorPath,' was found.'])
        return
    end
else
    disp(['No object ',objType,'/',objName,' was found.'])
    return
end

% Get information necessary for sensor pointing file ---------------------
bodyAxesDP = sensor.DataProviders.Item('Body Axes Orientation').Group.Item('Parent Body Axes').ExecElements(scen.StartTime, scen.StopTime, stepSize, {'Time';'q1';'q2';'q3';'q4'});

% Loop through each access interval if there are multiple
for i = 0:bodyAxesDP.Intervals.Count - 1
    % Gather the data from the data provider
    Time = cell2mat(bodyAxesDP.Interval.Item(i).DataSets.GetDataSetByName('Time').GetValues);
    q1 = cell2mat(bodyAxesDP.Interval.Item(i).DataSets.GetDataSetByName('q1').GetValues);
    q2 = cell2mat(bodyAxesDP.Interval.Item(i).DataSets.GetDataSetByName('q2').GetValues);
    q3 = cell2mat(bodyAxesDP.Interval.Item(i).DataSets.GetDataSetByName('q3').GetValues);
    q4 = cell2mat(bodyAxesDP.Interval.Item(i).DataSets.GetDataSetByName('q4').GetValues);
    if any(strcmpi(obj.ClassName,{'Target','Facility','Place'}))
        bodyAxes = [Time,q4,-q3,q2,-q1];
    else
        bodyAxes = [Time,q1,q2,q3,q4];
    end

    % Write sensor pointing file
    if bodyAxesDP.Intervals.Count > 1 || strcmpi(sensor.PointingType,'eSnPtTargeted')
        fileName = [inputFileName,'_',num2str(Time(1),'%.2f'),'_to_',num2str(Time(end),'%.2f'),'.sp'];
    else
        fileName = [inputFileName,'.sp'];
    end
    scDir = root.ExecuteCommand('GetDirectory / Scenario').Item(0);
    fid = fopen([scDir,fileName],'w');
    stkVer = root.ExecuteCommand('GetSTKVersion /').Item(0);
    fprintf(fid,['stk.v.',stkVer(6:9),'\n']);
    fprintf(fid,'BEGIN Attitude\n');
    fprintf(fid,['NumberOfAttitudePoints ',num2str(length(bodyAxes)),'\n']);
    fprintf(fid,'AttitudeTimeQuaternions\n');
    fprintf(fid,'%10.16f %10.16f %10.16f %10.16f %10.16f \n',bodyAxes');
    fprintf(fid,'\n');
    fprintf(fid,'END Ephemeris\n');
    fclose(fid);
    
    disp(['Sensor pointing file "',fileName,'" has been successfully created under ',scDir(1:end-1)]);

end

end


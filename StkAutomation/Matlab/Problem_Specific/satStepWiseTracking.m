function satStepWiseTracking(constName,targetPath,slewTime,totalSecHold,attStepSize)
%
% This function will take a satellite's attitude, and modify the tracking
% portions of the satellite's attitude to appear like they are stepwise for
% a single object. This will apply to all satellites specified in the
% constellation object.
% 
% NOTE: This script only that this modifies the satellite's attitude and 
% NOT the sensor's attitude. This is assuming the sensor is standard, nadir
% aligned. This only works for satellites in a constellation.
%
% Created By: Max Housner (max.housner@ansys.com)
% Date Created: 05/12/2023
%
%=============  INPUTS  =============
%
% constName (char array): Specify name of satellite constellation
% targetPath (char array): Specify object path of target
% slewTime (int): Set the time to slew to tracking (seconds) 
% totalSecHold (int): Set the total time the satellite holds on the target
%                   in the stepwise tracking before re-targeting (seconds)
% attStepSize (int): Set the step size for the attitude files
%
% EXAMPLE:
% satStepWiseTracking('TrackingConstellation','Aircraft/Target',30,120,10)


%=============  BEGIN FUNCTION  =============

% Initilize
% Grab an existing instance of STK
uiapp = actxGetRunningServer('STK12.application');
root = uiapp.Personality2;
% Set units to EpSec
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Create a folder wherever the script is for the attitude files
if ~exist(constName + "_Attitude_Files", 'dir')
    mkdir(constName + "_Attitude_Files")
end

% Get Constellation Object
const = root.CurrentScenario.Children.Item(constName);
numSats = const.Objects.Count;

% Get Target Object
target = root.GetObjectFromPath(targetPath);

% Loop through each Satellite in the constellation.
for num = 0:numSats-1
    % Get references to the satellite
    sat = const.Objects.Item(num).LinkedObject;
    satName = const.Objects.Item(num).Name;
    
    % Set the pointing to the Target
    sat.Attitude.Pointing.UseTargetPointing = true;
    if (~sat.Attitude.Pointing.Targets.Contains(targetPath))
        sat.Attitude.Pointing.Targets.Add(targetPath);
    end
    
    % Only modify satellites that have access to the target
    if sat.Attitude.Pointing.TargetTimes.AccessTimes.Count > 0
        % Modify slew time
        sat.Attitude.Pointing.TargetSlew.SetSlewModeType('eVeSlewModeFixedTime')
        sat.Attitude.Pointing.TargetSlew.SlewMode.SlewTime = slewTime;
    end
    
    % Export satellite attitude, modify, and load again -----------
    % Generate attitude file
    attExport = sat.ExportTools.GetAttitudeExportTool();
    attExport.TimePeriod.TimePeriodType = 'eExportToolTimePeriodSpecify';
    attExport.TimePeriod.Start = 0;
    attExport.TimePeriod.Stop = target.Route.StopTime;
    attExport.StepSize.StepSizeType = 'eExportToolStepSizeSpecify';
    attExport.StepSize.Value = attStepSize;
    attExport.Export(pwd + "\" + constName + "_Attitude_Files\" + satName + "_tracking.a");
    
    % Modify attitude file to be step wise
    % Read in file
    fid = fopen(pwd + "\" + constName + "_Attitude_Files\" + satName + "_tracking.a");
    C = textscan(fid, '%s', 'delimiter', '\n');
    fclose(fid);
    
    % Only modify satellites that have access to the target
    if sat.Attitude.Pointing.TargetTimes.AccessTimes.Count > 0
        for q = 0:sat.Attitude.Pointing.TargetTimes.AccessTimes.Count - 1
            
            % Get the lines in the attitude file to start and stop
            accessStartTime = sat.Attitude.Pointing.TargetTimes.AccessTimes.Item(q).StartTime;
            line_to_start = 26 + round(accessStartTime/attStepSize);
            accessStopTime = sat.Attitude.Pointing.TargetTimes.AccessTimes.Item(q).StopTime;
            line_to_end = 26 + ceil(accessStopTime/attStepSize);
            lines_to_skip = round(totalSecHold / attStepSize);
            
            % Loop through each line in attitude file
            for i = line_to_start:lines_to_skip:line_to_end
                holdAttitude = C{1,1}{i,1}(23:end);
                for j = 1:(lines_to_skip-1)
                    if i+j > (numel(C{1,1}) - 2)
                        break
                    end
                    C{1,1}{i+j,1}(23:end) = holdAttitude;
                end
            end
        end
    end
            
    % Create new attitude file
    fid = fopen(pwd + "\" + constName + "_Attitude_Files\" + satName + "_FINAL.a", 'w');
    for k=1:numel(C{1,1}) 
        fprintf(fid,'%s\r\n',C{1,1}{k,1}); 
    end
    fclose(fid);
    
    % Load attitude file back into satellite
    sat.Attitude.External.Load(pwd + "\" + constName + "_Attitude_Files\" + satName + "_FINAL.a")

end

end


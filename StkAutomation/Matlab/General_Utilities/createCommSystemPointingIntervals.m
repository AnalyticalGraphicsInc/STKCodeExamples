% This script was written for a CommSystem object that uses the receiver
% with minimum range.
% It creates an interval list file from this CommSystem object, that can be
% imported to a sensor's pointing properties so that the sensor's visual 
% pointing and the corresponding minimum range CommSystem link are aligned

% Attach to running instance of STK
uiapp = actxGetRunningServer('STK.application');
root = uiapp.Personality2;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Get the stop time of the scenario
stopTime = root.CurrentScenario.StopTime;
timeStep = 1;

% Extract time and receiver name from the CommSystem object
commSys = root.GetObjectFromPath('*/CommSystem/LandSat_CommSys_minRange');
dp = commSys.DataProviders.GetDataPrvTimeVarFromPath("Link Information");
dpElements = {'Time'; 'Rcvr Name'};
dpResult = dp.ExecElements(0, stopTime, timeStep, dpElements);
timeValues = cell2mat(dpResult.DataSets.GetDataSetByName('Time').GetValues);
rcvrName = (dpResult.DataSets.GetDataSetByName('Rcvr Name').GetValues);

str = strsplit(string(rcvrName(1)), '/');
satPath = "Satellite/" + str(2);

intrvlStartTime = timeValues(1);
prevTime = timeValues(1);
prevSatName = satPath(1);

%create Interval List
fileName = "LandSat_minRange_Pointing_Intervals.int";
scDir = root.ExecuteCommand('GetDirectory / Scenario').Item(0);
fid = fopen((scDir + fileName),'w');
stkVer = root.ExecuteCommand('GetSTKVersion /').Item(0);

fprintf(fid,['stk.v.',stkVer(6:9),'\n']);
fprintf(fid,'BEGIN IntervalList\n');
fprintf(fid,'\t\t DATEUNITABRV EpSec\n'); 
fprintf(fid,'BEGIN Intervals\n');

stop = length(timeValues);

for i = 2:timeStep:stop
    % Get the satellite path from the Rcvr Name string
    str = strsplit(string(rcvrName(i)), '/');

    currTime = timeValues(i);
    currSatName = "Satellite/" + str(2);
    disp(currTime)

    if (prevTime + timeStep ~= currTime) || (prevSatName ~= currSatName)
        %file format
        % intrvlStartTime already defined
        intrvlEndTime = prevTime;
        intrvlSatName = prevSatName;

        % print interval to file
        fprintf(fid,'\t\t "%10.16f" "%10.16f" %s \n',intrvlStartTime, intrvlEndTime, intrvlSatName);

        % reset start time
        intrvlStartTime = currTime;

    end

    if (i == stop)
        intrvlEndTime = currTime;
        intrvlSatName = currSatName;

        %print the last interval to file
        fprintf(fid,'\t\t "%10.16f" "%10.16f" %s \n',intrvlStartTime, intrvlEndTime, intrvlSatName);
    end

    prevTime = currTime;
    prevSatName = currSatName;
end

fprintf(fid,'END Intervals\n');
fprintf(fid,'END IntervalList\n');
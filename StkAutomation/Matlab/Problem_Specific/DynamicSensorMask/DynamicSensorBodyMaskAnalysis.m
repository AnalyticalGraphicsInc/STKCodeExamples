clear; clc;
%% Inputs
% Author: Austin Claybrook, Mo Syed, Jordan K.
% Last Modified: 10/24/19 by Austin Claybrook

% Description: Simulates a Dynamic Sensor Body AzElMask to Handle Moving Obscurations.
% Computes sensor body AzElMasks at the specified times. After the masks have
% been created, they can be reused for subsequent access calculations.
% Access is calculated to each of the specified objects from each specified
% sensor. Access is computed until the time of the next available mask and
% then the mask is updated and the process is repeated. In this way access 
% accounting for multiple masks can be consider. The switching of the
% AzElMasks is handled entirely by the script, but the resulting interval
% lists representing non-obscured access can be loaded back into STK.

% Set Start,Stop and Step times in EpSec
startTime = 1000; % sec
stopTime = 2200; % sec
maskTimeStep = 30; % This is how often a new mask gets calculated
maskTimes = startTime:maskTimeStep:stopTime;
maskTimes = [0:100:900,1000:30:2200,2300:100:3000]; % Can pass in more complex time steps to increase or decrease the sampling

% Sensors to Create Body Masks For
sensorPaths = FilterObjectsByType('Sensor'); % Grab all sensors
sensorPaths = {'/Satellite/ISS/Sensor/BackOfISS'};

% Obscuring Objects
obscurePaths = FilterObjectsByType('Satellite'); % Grab all satellites
% objPaths = {'Satellite/Dragon'};

% Objects to get Access To
objPaths = FilterObjectsByType('Place'); % Grab all places
% objPaths = {'Place/Venice','Place/Paris','Place/London'};

% Additional parameters
reuseExistingMaskFiles = true; % Reuse existing mask files with the same name. If false new mask files will always be created.
writeToFile = true; % Create interval file lists of the access times
createIntervals = true; % Create time components of the access times, stored on the sensor in Analysis Workbench
addAccessAsObjectLines = true; % Adds object lines using the non-obscured access intervals
disableAccessComputationsAndAnimateScenario = false; % Disables access computations. Useful to visualize the dynamic AzElMasks 
winRes = 512; % Resolution of bmsk files

%% Code

% Grab open STK scenario
app = actxGetRunningServer('STK12.application');
root = app.personality2;
root.Isolate();

% Change unit preferences og OM and Connect
root.UnitPreferences.SetCurrentUnit('DateFormat','EpSec');
root.ExecuteCommand('Units_Set * Connect Date EpSec');

% Get the scenario and epoch
scenario = root.CurrentScenario;
scenarioEpoch = scenario.Epoch;

% scenario.Animation.EnableAnimCycleTime = 1;
% scenario.Animation.AnimCycleType = 'eEndTime';

% Get handle to objects. Do this upfront to ensure all of the objects exist
for i = 1:length(sensorPaths)
    sensorList(i) = root.GetObjectFromPath(sensorPaths{i});
end
for i = 1:length(obscurePaths)
    obscureList(i) = root.GetObjectFromPath(obscurePaths{i});
end
for i = 1:length(objPaths)
    objList(i) = root.GetObjectFromPath(objPaths{i});
end

res = root.ExecuteCommand('GetDirectory / Scenario');
dir = res.Item(0);
folderPath = [dir,'\DynamicMaskFiles'];
if ~exist(folderPath, 'dir')
   mkdir(folderPath)
end
disp(['Files stored in ', folderPath])
disp(' ')

%%  Enter loop for each sensor
% Close timeline view
tlClosed = false;
ws = [];
for w = 0:app.Windows.Count-1
    if contains(app.Windows.Item(w).Caption,'Timeline') || (w == 0 && isempty(app.Windows.Item(w).Caption))
        ws = [ws,w];
    end
end
ws = ws(end:-1:1);
for w = ws
    app.Windows.Item(w).Close;
    tlClosed = true;
end

% Close Timelineview, this speeds things up
for sensornum = 1:length(sensorList)
    
    % Create a handle for the sensor being used
    sensor = sensorList(sensornum);
   
    % Create sensor body mask files
    for epoch = maskTimes(1:end-1)
        fileName = [folderPath '\' sensor.InstanceName 'Time' num2str(epoch) '.bmsk'];
        if ~exist(fileName, 'file')
            root.CurrentTime = maskTimes(i);
            tic
            GetSensorAzElMask(root,folderPath,epoch,sensor,obscureList,winRes);
            disp([sensor.InstanceName 'Time' num2str(epoch) '.bmsk Created. '])
            toc
            disp(' ')
        elseif reuseExistingMaskFiles ~= true
            root.CurrentTime = maskTimes(i);
            tic
            GetSensorAzElMask(root,folderPath,epoch,sensor,obscureList,winRes);
            disp([sensor.InstanceName 'Time' num2str(epoch) '.bmsk Created. '])
            toc
            disp(' ')
        end
    end

    try
        % Turn on constraints to use the body mask for each sensor
        sensor.AccessConstraints.AddConstraint('eCstrSensorAzElMask');
        sensor.Graphics.Projection.UseConstraints = 1;
        sensor.Graphics.Projection.EnableConstraint('SensorAzElMask');
    catch
    end

    
    % Run access each time a body mask is changed
    starts = -1*ones(1,length(objList));
    stops = -1*ones(1,length(objList));
    ndxs = ones(1,length(objList));

    for i = 1:length(maskTimes)-1
        
        % Updates Current Animation Time
        root.CurrentTime = maskTimes(i);

        % Update mask based on current time step
        SetSensorAzElMask(folderPath,maskTimes(i),sensor)
        
        if disableAccessComputationsAndAnimateScenario == false
            for num = 1:length(objList)
                % Get access to object
                access = sensor.GetAccessToObject(objList(num));
                % Return 1 for access or nothing for no access at the current time step
                accessDP = access.DataProviders.Item('Access Data').Exec(maskTimes(i),maskTimes(i+1));

                % If access found put into variables
                if accessDP.Interval.Count > 0
                    accessStartTimes = cell2mat(accessDP.DataSets.GetDataSetByName('Start Time').GetValues);
                    accessStopTimes = cell2mat(accessDP.DataSets.GetDataSetByName('Stop Time').GetValues);
                    count = length(accessStartTimes);
                    ndx = ndxs(num);
                    starts(ndx:ndx+count-1,num) = accessStartTimes;
                    stops(ndx:ndx+count-1,num) = accessStopTimes;
                    ndxs(num) = ndx + count;
                end

            end
            
        end
        
    end
    
    % Turn off constraints to use the body mask for each sensor
    sensor.AccessConstraints.RemoveConstraint('eCstrSensorAzElMask');
    sensor.Graphics.Projection.DisableConstraint('SensorAzElMask');
    sensor.Graphics.Projection.UseConstraints = 0;
    
    % Create Non-Obscured Intervals
    if disableAccessComputationsAndAnimateScenario == false
        
        for num = 1:length(objList)
            start = starts(:,num);
            stop = stops(:,num);
            start(start == 0) = []; 
            stop(stop == 0) = [];
            start(start == -1) = []; 
            stop(stop == -1) = []; 

            % Remove adjacent time intervals created when switching body masks
            stop(2:end+1) = stop;
            start(end+1) = 0;
            removeintervals = start ~= stop;
            start = start(removeintervals);
            stop = stop(removeintervals);
            start = start(1:end-1);
            stop = stop(2:end);
            data = [start, stop]';

            % Write to file
            if writeToFile == true
                stringOne = 'stk.v.11.0\nBEGIN IntervalList';
                % use the specifed scenario epoch instead
                % stringTwo = ['\n\nScenarioEpoch ',scenarioEpoch];
                stringTwo = '\n\nDATEUNITABRV EpSec';
                stringThree = '\n\nBegin Intervals\n\n';
                stringFour = '\nEND Intervals\n\nEND IntervalList';
                FileName = [folderPath,'\AzElIntervalList',sensor.InstanceName,'To',objList(num).InstanceName,'.int'];
                fileID = fopen(FileName,'w');
                fprintf(fileID,[stringOne  stringTwo stringThree]); 
                fprintf(fileID,'%1.3f %2.3f\r\n',data);
                fprintf(fileID,stringFour);
                fclose(fileID);
                disp(['AzElIntervalList',sensor.InstanceName,objList(num).InstanceName,'.int created.'])
            end

            % Creates interval lists available as components to assign to the
            % proper object
            if createIntervals == true
                listName = ['AzElIntervalList',sensor.InstanceName,'To', objList(num).InstanceName];
                sensorVGT = sensor.Vgt;
                intervalListFac = sensorVGT.EventIntervalLists.Factory;
                %Creates or grabs interval list component
                try
                timeIntervalList = intervalListFac.CreateEventIntervalListFixed(listName,'');
                catch
                timeIntervalList = sensorVGT.EventIntervalLists.Item(listName);    
                end
                %Organize times into cell array
                intervalCell = [];
                for accessNum = 1:length(start)
                    intervalCell = [intervalCell; num2cell(start(accessNum)); num2cell(stop(accessNum))];
                end
                %Stores values from above into the component
                if ~isempty(intervalCell)
                    timeIntervalList.SetIntervals(intervalCell);
                end
                
                % Add components to the timeline view. Try to remove and
                % readd if it was removed. Otherwise simply try to add,
                % or create a new timeline.
                truncPath = split(sensor.Path,'/');
                truncPath = strjoin(truncPath(end-3:end),'/');
                accessName = ['Access/',strrep(truncPath,'/','-'),'-To-', objList(num).ClassName,'-',objList(num).InstanceName];
                try
                    res = root.ExecuteCommand(['Timeline * TimeComponent Remove ContentView "ContentView1" "' accessName ' AccessIntervals Interval List"']);
                catch
                    try
                        root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "' accessName ' AccessIntervals Interval List"']);
                    catch
                        root.ExecuteCommand('Timeline * CreateWindow'); % Add timeline if necessary
                        root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "' accessName ' AccessIntervals Interval List"']);
                    end
                end
                if  res.IsSucceeded == 1
                    root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "' accessName ' AccessIntervals Interval List"']);
                end
                
                try
                    res = root.ExecuteCommand(['Timeline * TimeComponent Remove ContentView "ContentView1" "' truncPath ' ' listName ' Interval List"']);
                catch
                end
                root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "' truncPath ' ' listName ' Interval List"']);

                disp(['Time Component ',listName,' created.'])
                
                if addAccessAsObjectLines == true
                    % Remove old object lines
                    try
                        cmd = ['VO * ObjectLine Delete FromObj ',truncPath,' ToObj ',objList(num).ClassName,'/',objList(num).InstanceName];
                        root.ExecuteCommand(cmd);
                    catch
                    end
                    % Add in new object lines using the computed AzElMask intervals
                    color = dec2hex(sensor.Graphics.Color);
                    color = ['#',color([5:6 3:4 1:2])];
                    cmd = ['VO * ObjectLine Add FromObj ',truncPath,' ToObj ',objList(num).ClassName,'/',objList(num).InstanceName ' IntervalType UseIntervals LineWidth 5 Color ' color ' ImportIntervals "' truncPath ' ' listName ' Interval List"'];
                    root.ExecuteCommand(cmd);
                    
                end
            end
        
        end %end loop for each access object
    end 

end % end loop for each sensor

% Add back in Timeline if closed and refresh
if tlClosed == true
    root.ExecuteCommand('Timeline * CreateWindow');
end
root.ExecuteCommand('Timeline * Refresh WindowID 1');

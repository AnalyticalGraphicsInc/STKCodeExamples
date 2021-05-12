%Author: Mohammad Syed
%Organization: Analytical Graphics Inc.
%Date Created: 3/22/18
%Date Modified: 5/24/18 by Austin Claybrook

% Description: This is a script to select access from an object to
% multiple other objects based off of a specifed condition metric, such as
% minimum range or maximum elevation angle.
% 
% AFTER THE SCRIPT IS RUN
% Once the script generates the file, create a sensor on the object
% and assign its pointing type to Targeted and move over all the to objects.
% Change the target times to load in the created interval file.
% Remember to change the cone angle to something 
% small so only the singular object that meets your metric is selected
% 
% Inputs:
% fromObjectType - type of object to compute access from
% fromObjectName - name of object
% toObjectTypes - type of object to compute access to, it can be multiple types
% optionalToObjectName - optionally, only use objects that contain this string in their name
% fileName - the filename you want the data to be exported to (without the extension)
% conditionMetric - the conditional metric you want to extract (Range and
% Elevation are the only two metrics for now
% timeStep - this is how often the data is sampled from STK. By default it
% is at 1 second which does take some time to run, so change with caution
% extrema - this is what determines if you're looking for minimum or maximum

clc;
clear;

%Declare variables
fromObjectType = 'Place';
fromObjectName = 'Place1';
toObjectTypes = {'Satellite'}; %Place as many object types in here
optionalToObjectName = 'gps'; %Filter by objects containing this string. Only
                    %works if one object type in toOjbectTypes is specified.
fileName = 'maxElevation';
timeStep = 1;
conditionMetric = 'Elevation'; %Range or Elevation
extrema = 'Maximum';  %Minimum or Maximum

%Grabs running instance of STK
uiApplication = actxGetRunningServer('STK12.application');
root = uiApplication.Personality2;
scenario = root.CurrentScenario;

%Hides instance of STK
uiApplication.Visible = 1;

%Changes date unit
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

%Time array design
endTime = scenario.StopTime - scenario.StartTime;
timeArray = scenario.StartTime:timeStep:endTime;

%Gets ground site
fromObject = root.GetObjectFromPath(['*/' fromObjectType '/' fromObjectName]);

%Grab all instances of an object type
for i = 1:length(toObjectTypes)
    
    [paths] = FilterObjectsByType(toObjectTypes{i},optionalToObjectName);
    
    if i == 1
        toObjectPaths = paths;
    else
        toObjectPaths = [toObjectPaths,paths];
    end
end

%Sets array to 999999 or -999999 depending on whether min or max is set
if strcmp(extrema,'Maximum')

    dataArray = ones(length(timeArray),length(toObjectPaths)) * -999999;

elseif strcmp(extrema,'Minimum')

    dataArray = ones(length(timeArray),length(toObjectPaths)) * 999999;
    
end

%Gets access to the objects
for i = 1:length(toObjectPaths)

    obj(i) = root.GetObjectFromPath(toObjectPaths{i});
            
    access(i) = obj(i).GetAccessToObject(fromObject);
    access(i).ComputeAccess;
    accessDP(i) = access(i).DataProviders.Item('Access Data').Exec(scenario.StartTime, scenario.StopTime);
    aerDP(i) = access(i).DataProviders.Item('AER Data').Group.Item('BodyFixed').Exec(scenario.StartTime, scenario.StopTime,timeStep);
    
    %DP results return cell data types.  cell2mat
    
    if accessDP(i).DataSets.Count > 0
    
        accStart{i} = cell2mat(accessDP(i).DataSets.GetDataSetByName('Start Time').GetValues);
        accStop{i} = cell2mat(accessDP(i).DataSets.GetDataSetByName('Stop Time').GetValues);
        accDur{i} = cell2mat(accessDP(i).DataSets.GetDataSetByName('Duration').GetValues);

        aerTime{i} = aerDP(i).Interval.Item(cast(0,'int32')).DataSets.GetDataSetByName('Time').GetValues;
        aerMetric{i} = aerDP(i).Interval.Item(cast(0,'int32')).DataSets.GetDataSetByName(conditionMetric).GetValues; %Only works for AER Data Providers
        %You will need to customize the data provider for other metrics

        %Grabs data provider information and stores into cell arrays
         for j = 1:accessDP(i).Intervals.Count-1

            aerTime{i} = [Time{i}; aerDP.Interval.Item(cast(j,'int32')).DataSets.GetDataSetByName('Time').GetValues];
            aerMetric{i} = [aerMetric{i}; aerDP.Interval.Item(cast(j,'int32')).DataSets.GetDataSetByName('Elevation').GetValues];       
            accStart{i} = [accStart{i}; accessDP(i).Interval.Item(j).DataSets.GetDataSetByName('Start Time').GetValues];
            accStop{i} =  [accStop{i}; accessDP(i).Interval.Item(j).DataSets.GetDataSetByName('Stop Time').GetValues];
            accDur{i} =   [accDur{i}; accessDP(i).Interval.Item(j).DataSets.GetDataSetByName('Duration').GetValues];

         end

        %Stores data into array 
        A = timeArray;
        B = cell2mat(aerTime{i});
        B = B(2:end-1);
        
        %This stores the access times and aligns them correctly    
        idx = ismember(A', B, 'rows');
        c = 1:size(A, 2);
        matchPosition = c(idx); 

        dataMetric = cell2mat(aerMetric{i});

        %Stores data into table
         for k = 1:length(matchPosition)

             dataArray(matchPosition(k),i) = dataMetric(k);

         end

        %Cleans the access
         access(i).RemoveAccess();
    end
end

%Pull and compare data of each object
if strcmp(extrema,'Maximum')
    
    [metric,objPos] = max(dataArray');
   
    %Tells us which values are actual values
    metricLogic = metric ~= -999999;

elseif strcmp(extrema,'Minimum')
    
    [metric,objPos] = min(dataArray');
    
    %Tells us which values are actual values
    metricLogic = metric ~= 999999;
    
end

objPos = metricLogic .* objPos;

%Sets variables and cleans others
k = objPos';
j = 1;
start = [];
startUTCG = [];
finish = [];
finishUTCG = [];
objectNumber = [];
objectName = [];

%Organizes data by start and end time by the metric chosen
for i = 1:length(k)-1
    
    %Makes sure we only look for actual values since the values correspond
    %to an object
    if k(i) ~= 0       

        %This will always grab the first nonzero value;
        if isempty(start)

            start(j) = i;
            
        end
        
        %Checks to see if the value behind is different, then marks itself
        %as start
        if i >= 2
            if k(i) ~= k(i-1)

                start(j) = i;

            end
        end
        
        %Finds the last corresponding value as well as the object. Iterates
        %forward by one
        if k(i) ~= k(i+1)

            finish(j) = i; 
            objectNumber(j) = k(i);
            j = j + 1;
        end
        
        if i == length(k)-1 && isempty(finish)
            
            finish(j) = i + 1;
            objectNumber(j) = k(i);
            
        end
        
    end
    
end

%Grabs the corresponding object from STK referenced by objectNumber
for i = 1: length(objectNumber)
    
objectName{i} = [' ' obj(objectNumber(i)).ClassName '/' obj(objectNumber(i)).InstanceName];        
    
end

%Converts times from EpSec to UTCG
for i = 1:length(start)
    
    startUTCG{i} = ['"' root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(start(i))) '"'];
    finishUTCG{i} = [' "' root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(finish(i))) '"'];
    
end

%Brings back instance of STK
uiApplication.Visible = 1;

%Writes interval file
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');

stringOne = 'stk.v.11.0\nBEGIN IntervalList';

data = [startUTCG; finishUTCG; objectName];

stringTwo = '\n\nScenarioEpoch ';

scenarioEpoch = scenario.Epoch;

stringThree = '\nDateUnitAbrv UTCG\n\nBegin Intervals\n\n\t\t';

stringFour = '\nEND Intervals\n\nEND IntervalList';

fileID = fopen([fileName '.int'],'w');

fprintf(fileID,[stringOne  stringTwo scenarioEpoch stringThree]); 

fprintf(fileID,'%s %s %s \n\t\t',data{:});

fprintf(fileID,stringFour);

fclose(fileID);

disp('Complete!')
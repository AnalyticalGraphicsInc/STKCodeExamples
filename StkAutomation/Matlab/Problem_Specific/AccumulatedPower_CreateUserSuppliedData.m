clear; close;

%% Variables
% STK Scenario must be open and must have a series of Facility objects and 
% a satellite. 

% When the satellite has access to a facility the 
% 'connectedConsumptionRate' will be used for power computations. When the
% satellite does not have access to a facility, the 
% 'defaultConsumptionRate' will be used for power computations. The
% resulting annotation will simply display the string assigned to
% 'powerUnit'. The rates below are in units of Watts/accumTimeStep, where
% accumTimeStep is set on line 27.
connectedConsumptionRate = 4;  
defaultConsumptionRate = 0;

% If 'useAllFacs' is set to True, all facilities in the scenario will be
% considered for computation. If False, only the facilities specified in
% 'facsToUse' will be considered. 
useAllFacs = true;
facsToUse = ['Facility1', 'Facility2', 'Facility5', 'Facility 6'];

% Name of Satellite being used for computation
satName = 'Satellite1';

% Time step to be used for accumulation calculations
accumTimeStep = 1;   % sec

% Location to write external data file to
fileLoc = 'C:\\Users\\arogers\\Documents\\STK 12';


%% Get Access Data from STK 
app = actxGetRunningServer('STK12.Application');
root = app.Personality2;
scenario = root.CurrentScenario;
sat = scenario.Children.Item(satName);

if useAllFacs
    numObjects = scenario.Children.Count;
    k = 1;
    for i = 0:(numObjects - 1)
        obj = scenario.Children.Item(int32(i));
        if string(obj.ClassType) == "eFacility"
            facObjs(k) = obj;
            k = k + 1;
        end
    end
else
    numFacilities = length(facsToUse);
    for i = 1:numFacilities
        facObjs(i) = scenario.Children.Item(facsToUse(i));
    end
end

% Create chain access to quickly compute any intervals when the satellite 
% has access to any facility
try
    facConst = scenario.Children.New('eConstellation', 'FacilityConstellation');
catch
    toUnload = scenario.Children.Item('FacilityConstellation');
    toUnload.Unload();
    facConst = scenario.Children.New('eConstellation', 'FacilityConstellation');
end
for j = 1:length(facObjs)
    facConst.Objects.AddObject(facObjs(j));
end
try
    facToSatChain = scenario.Children.New('eChain', 'FacilitiesToSatellite');
catch
    toUnload = scenario.Children.Item('FacilitiesToSatellite');
    toUnload.Unload()
    facToSatChain = scenario.Children.New('eChain', 'FacilitiesToSatellite');
end
facToSatChain.Objects.AddObject(sat);
facToSatChain.Objects.AddObject(facConst);
facToSatChain.ComputeAccess();
root.UnitPreferences.SetCurrentUnit('Date', 'EpSec');

dataSets = facToSatChain.DataProviders.GetDataPrvIntervalFromPath('Interval List/CompleteChainAccessIntervals').ExecElements(scenario.StartTime, scenario.StopTime, {'Start Time'; 'Stop Time'}).DataSets;
% Get total number of data sets (access passes)
numberDataSets = dataSets.Count;

startStops = [];

starts = cell2mat(dataSets.Item(0).GetValues());
stops = cell2mat(dataSets.Item(1).GetValues());
for u = 1:length(starts)
    both = [starts(u), stops(u)];
    startStops = cat(2, startStops, both);
end

numTimeSteps = scenario.StopTime/accumTimeStep;
startStopNum = 1;
inAccess = false;
accumPower = 0;
for step = 1:int32(numTimeSteps)
    time = accumTimeStep * (step - 1);
    times(step) = time;
    if time > startStops(startStopNum)
        inAccess = ~inAccess;
        startStopNum = startStopNum + 1;
    end
    if inAccess
        powers(step) = accumPower + connectedConsumptionRate;
    else
        powers(step) = accumPower + defaultConsumptionRate;
    end
    accumPower = powers(step);
end

%% Create External Data File
root.UnitPreferences.SetCurrentUnit('Date', 'UTCG');
fileID = fopen(strcat(fileLoc, '\\ExternalPowerData.txt'), 'w');
fprintf(fileID, strcat('stk.v.4.3\nBegin DataGroup\n\tGroupName My Data\n\tNumberOfPoints\t', string(length(times))));
fprintf(fileID, strcat('\n\tReferenceEpoch\t', string(scenario.StartTime)));
fprintf(fileID, '\n\tBegin DataElement\n\t\tName\tAccumulated Power\n\t\tDimension\tPower\n\t\tFileUnitAbbr\tW\n\t\tInterpOrder\t1\n\tEnd DataElement\n\n');
fprintf(fileID, '\tBegin Data\n');
for pointNum = 1:length(times)
    fprintf(fileID, strcat('\t', string(times(pointNum)), '\t', string(powers(pointNum)), '\n'));
end
fprintf(fileID, '\tEnd Data\nEnd DataGroup');
fclose(fileID);

%% Make STK Use External Data File
cmd = string(strcat({'ExternalData */Chain/FacilitiesToSatellite ReadFile "'}, fileLoc, '\\ExternalPowerData.txt', {'" Save'}));
root.ExecuteCommand(cmd);

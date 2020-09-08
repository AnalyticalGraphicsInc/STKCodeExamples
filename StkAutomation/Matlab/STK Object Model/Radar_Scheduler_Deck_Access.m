% %Written By: Alexander Ridgeway
% %Date:       27 April 2016
% 
clear;
clc;
try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
catch
    % STK is not running, launch new instance
    % Launch a new instance of STK and grab it
    uiapp = actxserver('STK12.application');
end

%get the root from the personality
root = uiapp.Personality2;

if root.CurrentScenario.Children.Contains('eFacility', 'Radar')
    fac = root.GetObjectFromPath('/Facility/Radar');
    fac.Unload;
    fac = root.CurrentScenario.Children.New('eFacility','Radar');
else
    fac = root.CurrentScenario.Children.New('eFacility','Radar');
end
    fac = root.CurrentScenario.Children.New('eFacility','Radar');
catch
    fac = root.GetObjectFromPath('/Facility/Radar');
    fac.Unload;
    fac = root.CurrentScenario.Children.New('eFacility','Radar');
end

FacLat = 13.7355;
FacLon = 80.1847;
FacAlt = -0.0564551;
RangeMax = 1000;
Start = '1 Jan 2017 00:00:00.000';
Stop = '1 Jan 2017 00:30:00.000';


fac.UseTerrain = false;
fac.Position.AssignGeodetic(FacLat,FacLon,FacAlt);
sen = fac.Children.New('eSensor','Dome');
sen.CommonTasks.SetPatternRectangular(65,65);
const = sen.AccessConstraints;
range = const.AddConstraint(34); 
range.EnableMax = true;
range.Max = RangeMax;
 
Startdate = datetime(Start,'InputFormat','dd MMM yyyy HH:mm:SS.000');
Stopdate  = datetime(Stop,'InputFormat','dd MMM yyyy HH:mm:SS.000');
Duration = seconds(Stopdate-Startdate);

disp(['Facility Location'])
disp(['Latitude:  ',num2str(FacLat),' degrees'])
disp(['Longitude: ',num2str(FacLon),' degrees'])
disp(['Altitude:  ',num2str(FacAlt),' degrees'])
disp([' '])
disp(['Time of Analysis'])
disp(['Start:     ',Start,' UTC'])
disp(['Stop:      ',Stop, ' UTC'])
disp(['Duration:  ',num2str(Duration),' seconds'])
disp([' '])

disp('***Begin Deck Access***')
disp(datetime('now'))
cmd =['DeckAccess */Facility/Radar/Sensor/Dome "' Start '" "' Stop '" Satellite "C:\ProgramData\AGI\STK 11\Databases\Satellite\stkAllTLE.tce" SortObj OutReport AddSatellites 10000'];
root.ExecuteCommand(cmd);

disp('***Complete Deck Access ***')
disp(datetime('now'))

chain = scen.Children.New('eChain','DeckAccess');
try 
    constell = scen.Children.New('eConstellation','Deck_Sats');
catch
    constell = root.GetObjectFromPath('/Constellation/Deck_Sats');
    constell.Unload;
    constell = scen.Children.New('eConstellation','Deck_Sats');
end
chain.Objects.AddObject(constell);
chain.Objects.AddObject(sen);

% 
disp('***Begin Adding Satellites***')
disp(datetime('now'))

Names = scen.DataProviders.Item('Object Names').Exec;
Names = Names.DataSets.GetDataSetByName('Object Names').GetValues;
NumTemp = size(Names);
NumSize = NumTemp(1);

for i = 2:NumSize-1
    temps = cell2mat(Names(i));
    sizetemp = size(temps);
    if sizetemp(2) >=36 
        if temps(1:36) == '/Scenario/DeckAccess/Satellite/deck_';
        constell.Objects.Add(temps);
        end
    end
end
disp('***Completed Adding Satellites***')
disp(datetime('now'))



chain.SetTimePeriodType('eUserSpecifiedTimePeriod');
chainUserTimePeriod = chain.TimePeriod;
chainUserTimePeriod.SetTimePeriod(Start,Stop); % Set to scenario period

chain.ComputeAccess;

AERdata = chain.DataProviders.Item('Access AER Data');

% % % % TimeStart = chain.Vgt.Events.Item('ConsideredStartTime').FindOccurrence.Epoch;
% % % % TimeStop  = chain.Vgt.Events.Item('ConsideredStopTime').FindOccurrence.Epoch;



PLAN = cell(Duration+1,1);

disp('***Begin Writing Schedule***')
disp(datetime('now'))

for t = 0:Duration/10

    TIME = Startdate + 10*seconds(t);
    AERresults = AERdata.ExecSingle(char(datetime(TIME, 'Format', 'dd MMM yyyy HH:mm:ss.000')));

    count_int = AERresults.Intervals.Count - 1;
    
    tempstr = [char(datetime(TIME, 'Format', 'dd MMM yyyy HH:mm:ss.000')) ' UTC'];
    
    for i = 0:count_int

        intev = AERresults.Intervals.Item(i);

        Tm = cell2mat(intev.DataSets.GetDataSetByName('Time').GetValues);
        Az = cell2mat(intev.DataSets.GetDataSetByName('Azimuth').GetValues);
        El = cell2mat(intev.DataSets.GetDataSetByName('Elevation').GetValues);
        St = cell2mat(intev.DataSets.GetDataSetByName('Strand Name').GetValues);
        tempstr =  [tempstr ', SSN:' St(16:21) ' Az:' num2str(Az,'%3.3f') ' El:' num2str(El,'%3.3f')];

    end
    PLAN{t+1} = tempstr;
end

  filename = ['Schedule_' char(datetime(TIME, 'Format', 'MMddyyyy_HHmmss')) '.csv'];
  fid = fopen(filename,'w');
  fprintf(fid,'%s\n',PLAN{:,1});
  fclose(fid);


disp('***Completed Writing Schedule***')
disp(datetime('now'))


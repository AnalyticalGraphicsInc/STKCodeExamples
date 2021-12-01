clear all; close all;
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Acceleration History File Creator
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

% User-specific variables
satName = 'FiniteSat';
STKVersion = '12';
maneuverName = 'Maneuver';
accelTimeStep = 1; % sec
interpolationOrder = '1';

% Grab STK scenario and set date format to epoch seconds for file
uiapp = actxGetRunningServer(['STK',STKVersion,'.application']);
root = uiapp.Personality2;
scenario = root.CurrentScenario;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Get astg sat 
sat = scenario.Children.Item(satName);

% Get data from maneuver history data provider
data_sat = sat.DataProviders.Item('Astrogator Maneuver Ephemeris Block History').Group.Item('Maneuver');
data_out = data_sat.Exec(scenario.StartTime,scenario.StopTime,accelTimeStep);

Rpt_data(:,1) = cell2mat(data_out.DataSets.GetDataSetByName('Time').GetValues);
Rpt_data(:,2) = cell2mat(data_out.DataSets.GetDataSetByName('Thrust_Vector_X').GetValues);
Rpt_data(:,3) = cell2mat(data_out.DataSets.GetDataSetByName('Thrust_Vector_Y').GetValues);
Rpt_data(:,4) = cell2mat(data_out.DataSets.GetDataSetByName('Thrust_Vector_Z').GetValues);
Rpt_data(:,5) = cell2mat(data_out.DataSets.GetDataSetByName('Total_Mass').GetValues);
Rpt_data(:,6) = cell2mat(data_out.DataSets.GetDataSetByName('Total_Mass_Flow_Rate').GetValues);
eph_pnts = length(Rpt_data(:,1));

% Get epoch time in UTCG
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
t_start = char(scenario.StartTime);
t_epoch = datetime(t_start,'InputFormat','dd MMM yyyy HH:mm:ss.000')+seconds(Rpt_data(1,1));
Epoch = char(t_epoch);
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Write file header - sat name + maneuver name is used as filename
fileID = fopen([satName,'_',maneuverName,'.accelhist'],'w');
fprintf(fileID,'stkv4.3\n');
fprintf(fileID,'BEGIN AccelHistory\n');
fprintf(fileID,'NumberOfEphemerisPoints		%1.0f\n\n',eph_pnts);
fprintf(fileID,'ScenarioEpoch			%s\n\n',Epoch);  
fprintf(fileID,'CoordinateSystem		        J2000\n\n');
fprintf(fileID,['InterpolationOrder		        ',interpolationOrder,'\n\n']);
fprintf(fileID,'EPHEMERISTIMEACCMASSRATE\n');

% Write accel history
for i = 1: eph_pnts
    if Rpt_data(i,5)>0
        Out_accl(i,1) = Rpt_data(i,1)-Rpt_data(1,1);
        Out_accl(i,2) = Rpt_data(i,2)/Rpt_data(i,5);
        Out_accl(i,3) = Rpt_data(i,3)/Rpt_data(i,5);
        Out_accl(i,4) = Rpt_data(i,4)/Rpt_data(i,5);
    end
    fprintf(fileID,'%6.2f  %2.6f  %2.6f  %2.6f  %2.6f\n',Out_accl(i,1),Out_accl(i,2),Out_accl(i,3),Out_accl(i,4),Rpt_data(i,6));
    
end
fprintf(fileID,'End AccelHistory');
fclose(fileID);


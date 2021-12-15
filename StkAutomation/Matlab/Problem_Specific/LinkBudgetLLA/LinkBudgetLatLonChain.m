% This script takes the TransponderLB STK scenario and outputs the
% Eb/No, rain loss, and atm loss at the aircraft and facility's
% latitude and longitude. The facility is used as a 'dummy' object for the
% ground vehicle. The script works by moving the facility to each point in
% the ground vehicle's ephemeris file then outputting the LLA information
% of facility and aircraft. At each facility position, the aircraft flies
% its entire route and comm parameters are exported to an Excel
% spreadsheet.

% The scenario takes a long time to run, as the facility is stepping
% through each ground vehicle point and computing comm analysis to the
% aircraft.

tic

%% Define the scenario and set properties
clear; clc; close all;
% access current scenario
stk = actxGetRunningServer('STK12.application');
% access the root of the application
root = stk.personality2;
% get a handle to the just created scenario
scenario = root.CurrentScenario;

%% Set all necessary variables and constants
StepSize = 1; % sec
chain = root.GetObjectFromPath('Chain/Chain1');
facility = root.GetObjectFromPath('Facility/LBfacility');
aircraft = root.GetObjectFromPath('Aircraft/Aircraft1');
satellite = root.GetObjectFromPath('Satellite/100W');
tx1 = root.GetObjectFromPath('Aircraft/Aircraft1/Sensor/Sensor1/Transmitter/ULtransmitter');

%% Initialize excel sheet
% create file name
timestamp = datestr(now);   % need to remove colons from timestamp string
filename = ['Link Information ' timestamp([1:14, 16:17]) '.xlsx'];  

% write in labels
% PosLabels = {'Facility Position','','Aircraft Position'};
% DataLabels = {'Latitude (deg)','Longitude(deg)','Latitude (deg)','Longitude (deg)',...
%     'Eb/No1 (dB)','Eb/No2 (dB)','Eb/No Total 2 (dB)',...
%     'Rain Loss1 (dB)','Atm Loss1 (dB)','Rain Loss2 (dB)','Atm Loss2 (dB)'};
% % xlswrite('Link Information.xlsx',PosLabels);
% % xlswrite('Link Information.xlsx',DataLabels,1,'A2');
% xlswrite(filename,PosLabels);
% xlswrite(filename,DataLabels,1,'A2');

Label1 = {'UL EIRP (dBW)'};
Label2 = {'UL G/T (dB-K)'};
Label3 = {'DL EIRP (dBW)'};
Label4 = {'Facility Position','','Elevation Angle (deg)','Aircraft Position (deg)','','Elevation Angle (deg)','UL','','DL','','',...
    'T sys','T rain','Transponded Link','UL','','DL',''};
Label5 = {'Lat','Lon','Facility to Satellite','Lat','Lon','Aircraft to Satellite','Path Loss (dB)','Eb/No (dB)',...
    'Path Loss (dB)','Eb/No (dB)','G/T (dB-K)','(K)','(K)','Eb/No Total (dB)','Rain Loss (dB)','Atm Loss (dB)','Rain Loss (dB)','Atm Loss (dB)'};
Label6 = {'UL Frequency (GHz)'};
Label7 = {'DL Frequency (GHz)'};
Label8 = {'Data Rate (Mbps)'};  % C3
Label9 = {'Satellite Position (deg)';'Lat';'Lon'}; % E1, this is a column of labels
Label10 = {'Required Eb/No (dB)'}; % G1
Label10a = {'UAV Altitude (km)'}; % G2
Label11 = {'UL link availibility';'DL link availibility'}; % I1
Label12 = {'Delta G/T (dB-K)','UL Other Losses (dB)','DL Other Losses (dB)','Required Eb/No (dB)','Margin (dB)'}; % S5
    
xlswrite(filename,Label1,1,'A1');
xlswrite(filename,Label2,1,'A2');
xlswrite(filename,Label3,1,'A3');
xlswrite(filename,Label4,1,'A4');
xlswrite(filename,Label5,1,'A5');
xlswrite(filename,Label6,1,'C1');
xlswrite(filename,Label7,1,'C2');
xlswrite(filename,Label8,1,'C3'); % data goes into D3
xlswrite(filename,Label9,1,'E1'); % data goes into F2, F3
xlswrite(filename,Label10,1,'G1'); % data goes into H1
xlswrite(filename,Label10a,1,'G2'); % data goes into H2
xlswrite(filename,Label11,1,'I1'); % data goes into J1, J2
xlswrite(filename,Label12,1,'S5'); % append to far end of spreadsheet


%% Loop through facility locations and calculate link info
% read in data for facility locations
FacLocs = importdata('Ground Vehicle Conus Ephemeris 5 deg.e');
FacTime = FacLocs.data(:,1);
FacLat = FacLocs.data(:,2);
FacLon = FacLocs.data(:,3);
FacAlt = FacLocs.data(:,4);

% read in data for aircraft locations
AcLocs = importdata('Aircraft Conus Ephemeris 5 deg.e');
AcTime = AcLocs.data(:,1);
AcLat = AcLocs.data(:,2);
AcLon = AcLocs.data(:,3);
AcAlt = AcLocs.data(:,4);

% get data for UL EIRP, UL G/T, DL EIRP; write to Excel file
LinkInfo = chain.DataProviders.Item('Link Information').ExecElements(scenario.StartTime, scenario.StopTime, StepSize,...
        {'EIRP1';'g/T1';'EIRP2';'Rcvd. Frequency1';'Rcvd. Frequency2'});
up_eirp = cell2mat(LinkInfo.DataSets.GetDataSetByName('EIRP1').GetValues); % dBW
up_GoverT = cell2mat(LinkInfo.DataSets.GetDataSetByName('g/T1').GetValues); % dB-K
dn_eirp = cell2mat(LinkInfo.DataSets.GetDataSetByName('EIRP2').GetValues); % dBW
up_freq = cell2mat(LinkInfo.DataSets.GetDataSetByName('Rcvd. Frequency1').GetValues); % GHz
dn_freq = cell2mat(LinkInfo.DataSets.GetDataSetByName('Rcvd. Frequency2').GetValues); % GHz
xlswrite(filename,up_eirp(1),1,'B1');
xlswrite(filename,up_GoverT(1),1,'B2');
xlswrite(filename,dn_eirp(1),1,'B3');
xlswrite(filename,up_freq(1),1,'D1');
xlswrite(filename,dn_freq(1),1,'D2');

% get satelite position data and write to Excel file
satPos = satellite.DataProviders.Item('LLA State').Group.Item('Fixed')...
        .ExecElements(scenario.StartTime, scenario.StopTime, StepSize, {'Lat';'Lon'});
satLat = cell2mat(satPos.DataSets.GetDataSetByName('Lat').GetValues); % deg
satLon = cell2mat(satPos.DataSets.GetDataSetByName('Lon').GetValues); % deg  
xlswrite(filename,satLat(1),1,'F2');
xlswrite(filename,satLon(1),1,'F3');

% get data rate and write to Excel file
txProp = tx1.DataProviders.Item('Basic Properties').ExecElements({'Data Rate'});
dataRate = cell2mat(txProp.DataSets.GetDataSetByName('Data Rate').GetValues); % Mbps
xlswrite(filename,dataRate,1,'D3');

% get required Eb/No and write to Excel file
req_EbNo = 0; % placeholder, defined in receiver, how to extract value?
xlswrite(filename,req_EbNo,1,'H1');

%% Loop aircraft through once for video recording
input('Set up video recording, press enter once ready.');

% place facility at first location
facility.Position.AssignGeodetic(FacLat(1),FacLon(1),FacAlt(1));
% loop through aircraft locations and place aircraft there
EpSecEnd = AcTime(end);
UTCGEnd = root.ExecuteCommand(['Units_Convert * Date EpSec UTCG "' num2str(EpSecEnd) '"']);
root.ExecuteCommand(['SetAnimation * TimeStep 0.01 EndTime "' num2str(UTCGEnd.Item(0)) '"']);
root.ExecuteCommand('Animate * Start End');
input('Continue to second location, press enter.')

% place facility at second location
facility.Position.AssignGeodetic(FacLat(2),FacLon(2),FacAlt(2));
% loop through aircraft locations and place aircraft there
root.Rewind;
EpSecEnd = AcTime(end);
UTCGEnd = root.ExecuteCommand(['Units_Convert * Date EpSec UTCG "' num2str(EpSecEnd) '"']);
root.ExecuteCommand(['SetAnimation * TimeStep 0.01 EndTime "' num2str(UTCGEnd.Item(0)) '"']);
root.ExecuteCommand('Animate * Start End');

% finish recording
input('Stop video recording, press enter to complete data collection.');

%% Calculate data for spreadsheet
for i = 1:length(FacTime)
    % place facility at specified location
    facility.Position.AssignGeodetic(FacLat(i),FacLon(i),FacAlt(i));
    
    %% Output values to .csv file
    % write in facility location
%     row = 3 + length(FacTime)*(i-1);
    row = 6 + length(FacTime)*(i-1);
    xlswrite(filename,[FacLat(i),FacLon(i)],1,['A' num2str(row)]); 
    
    %% Loop through all aircraft locations and calculate link info
    % Grab necessary Link Information data and store to variables for Chain
    LinkInfo = chain.DataProviders.Item('Link Information').ExecElements(scenario.StartTime, scenario.StopTime, StepSize,...
        {'Time';'Eb/No1';'Eb/No2';'Eb/No Tot.2';'Atmos Loss1';'Rain Loss1';'Atmos Loss2';'Rain Loss2';...
        'Free Space Loss1';'Free Space Loss2';'g/T2';'Tequivalent2';'Train2';'Link Margin Tot.2'}); 
    
    Time = cell2mat(LinkInfo.DataSets.GetDataSetByName('Time').GetValues); % UTCG
    EbNo1 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Eb/No1').GetValues); % dB
    EbNo2 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Eb/No2').GetValues); % dB
    EbNotot2 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Eb/No Tot.2').GetValues); % dB
    AtmosLoss1 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Atmos Loss1').GetValues); % dB
    RainLoss1 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Rain Loss1').GetValues); % dB
    AtmosLoss2 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Atmos Loss2').GetValues); % dB
    RainLoss2 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Rain Loss2').GetValues); % dB
    
    % added Link Budget data (NOTE: should check that I picked the right data!!!!)
    PathLoss1 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Free Space Loss1').GetValues); % dB
    PathLoss2 = cell2mat(LinkInfo.DataSets.GetDataSetByName('Free Space Loss2').GetValues); % dB
    GoverT = cell2mat(LinkInfo.DataSets.GetDataSetByName('g/T2').GetValues); % dB-K
    Tsys = cell2mat(LinkInfo.DataSets.GetDataSetByName('Tequivalent2').GetValues); % K
    Train = cell2mat(LinkInfo.DataSets.GetDataSetByName('Train2').GetValues); % K
    Margin = cell2mat(LinkInfo.DataSets.GetDataSetByName('Link Margin Tot.2').GetValues); % dB  <---- currently outputs -9999 b/c required Eb/No is not set

    % compute accesses from facility-to-satellite and aircraft-to-satellite
    access1 = facility.GetAccessToObject(satellite);
    access1.ComputeAccess
    access2 = aircraft.GetAccessToObject(satellite);
    access2.ComputeAccess
    
    % get AER data
    FtoS = access1.DataProviders.Item('AER Data').Group.Item('Default');
    AtoS = access2.DataProviders.Item('AER Data').Group.Item('Default');
    AERInfoFacility = FtoS.ExecElements(scenario.StartTime, scenario.StopTime, StepSize, {'Elevation'});
    AERInfoAircraft = AtoS.ExecElements(scenario.StartTime, scenario.StopTime, StepSize, {'Elevation'});
    
    ElevAngle1 = cell2mat(AERInfoFacility.DataSets.GetDataSetByName('Elevation').GetValues); % deg
    ElevAngle2 = cell2mat(AERInfoAircraft.DataSets.GetDataSetByName('Elevation').GetValues); % deg
    
    ElevAngle1 = ElevAngle1(1:length(ElevAngle2));
    
    %% Grab LLA and store to variables for Aircraft1
    acLLA = aircraft.DataProviders.Item('LLA State').Group.Item('Fixed')...
        .ExecElements(scenario.StartTime, scenario.StopTime, StepSize, {'Lat';'Lon';'Alt'});
    acLat = cell2mat(acLLA.DataSets.GetDataSetByName('Lat').GetValues); % deg
    acLon = cell2mat(acLLA.DataSets.GetDataSetByName('Lon').GetValues); % deg
    acAlt = cell2mat(acLLA.DataSets.GetDataSetByName('Alt').GetValues); % km
    xlswrite(filename,acAlt(1),1,'H2');  % this is being rewritten each loop - works for now
    
    %% Other calculations for spreadsheet
    % delta G/T, link margin, other losses
    d_GoverT = GoverT - up_GoverT;
    req_EbNo = zeros(size(EbNotot2));      % placeholder
    up_otherLoss = zeros(size(EbNotot2));  % placeholder
    dn_otherLoss = zeros(size(EbNotot2));  % placeholder
    
%     xlswrite(filename,[acLat,acLon,EbNo1,EbNo2,EbNotot2,RainLoss1,AtmosLoss1,RainLoss2,AtmosLoss2],1,['C' num2str(row)]);
    xlswrite(filename,[ElevAngle1,acLat,acLon,ElevAngle2,PathLoss1,EbNo1,PathLoss2,EbNo2,GoverT,...
        Tsys,Train,EbNotot2,RainLoss1,AtmosLoss1,RainLoss2,AtmosLoss2,d_GoverT,up_otherLoss,dn_otherLoss,req_EbNo,Margin],1,['C' num2str(row)]);
    
    count = i
end

toc
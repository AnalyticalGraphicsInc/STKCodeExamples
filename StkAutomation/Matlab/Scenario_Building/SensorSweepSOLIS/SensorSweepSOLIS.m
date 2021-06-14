% Description: Creates a sweeping/raster scan over a list of area targets. 
% The scan speed, width and direction are controllable and will be inserted
% as a ground vehicle called 'Scan#'. A SOLIS target sequence file can also
% be created, which can be used to command a spacecraft's attitude to
% point along the scan pattern. SOLIS can be automatically run with the
% generated list of scans. A preconfigured Aviator aircraft can alternatively
% be used to generate the scan pattern. This script relies on GenerateScan.m
% and LatLonNewPoint.m to create the scan pattern.

%% Inputs, Only this section needs to be modified
% Scan Inputs:
scanNumberList = {'3','2','1'}; % Switches between scan patterns 1,2,3. If Aviator is used, the scan # also controls the scan width
scanSpeedList = [50,70,30]; % Speed of scan along the ground [km/s]
scanWidthList = [150,200,50]; % Pass width [km], If Aviator is used the values won't be used
scanDirList = {'TB','LR','TB'}; % 'LR' or 'TB', Either scan left to right or top to bottom
areaTargetList = {'Argentina','Brazil','Cuba'}; % Can be any area target already in the scenario. If not the area target is assumed to be a country and the script will try to load the country from the installed shp file
% Example of how to generate a list of repeat values:
% numScans = 3; % Number of scans
% scanNumberList = cell(1,numScans);
% scanNumberList(:) = {'1'}; % {'1','1','1'}
% scanSpeedList = 50*ones(1,numScans); % [50 50 50]
% scanResList = 200*ones(1,numScans); % [200 200 200]
% scanDirList = cell(1,numScans);
% scanDirList(:) = {'LR'}; % {'LR','LR','LR'}
% areaTargetList = cell(1,numScans);
% areaTargetList(:) = {'United_States'}; % {'United_States','United_States','United_States'}

% Additional Scan Options:
% The scan can either be generated from a bounding rectangle with equally
% spaced lat lon points or use constant East-West or North-South lat lon lines.
% The scan duration can be used instead of the scan speed, the scan can be
% at an altitude and targets can be inserted at the scan points.
useBoundingRectangle = true; % If true, use the bounding rectangle aligned with the long and short side of the area target.
insertScanPointsAsTargets = false; % Insert targets from the scan points
useFixedScanDur = false; % If true, the scan duration will be fixed and the scan speed will be overidden
fixedScanDur = 3600*ones(length(areaTargetList),1); % Duration of Scan [sec]
alt = 0; % Scan altitude [km]

% SOLIS Inputs:
% Creates a seqs file which can be executed in SOLIS to command a
% spacecraft to point along the scan pattern, note that the spacecraft will point
% along the scan pattern regardless of the spacecraft's location. The
% location of the seqs files needs to be in the seqs folder for the specific
% satellite in the scenario folder. The satellite must already exist and
% have all of the SOLIS folders already generated. During the scan either EigenSlew or 
% RenezSlew can be used. EigenSlew does not slow down at each scan point, 
% whereas RenezSlew does. The slew time between scans needs to be provided.
% SOLIS can be set to run automatically once the new seqs are generated.
createSOLISseqsFile = false; % Create a SOLIS target plan file
seqsLocation = 'C:\Users\aclaybrook\Documents\STK 11 (x64)\SOLISTPTest\Satellite1\seqs'; %  Will need to be put in the right directory for SOLIS to recognize the file
slewMode = 'EigenSlew'; % Either EigenSlew or RendezSlew
slewTimeBetweenScans = 120*ones(length(areaTargetList),1); % Used for the slew time between scans in the seqs file.
runSOLISseqs = false; % If true, the created SOLIS seqs file will be run. The seqsLocation must be in the ...\satelliteName\seqs folder.
% Can also create a target planner file. The XML format will need to be
% editted for your specific usecase.
createSOLIStpcFile = true; % Create a SOLIS target plan file
tpcLocation = 'C:\Users\aclaybrook\Documents\STK 11 (x64)\SOLISTPTest\Satellite1\tp';

% Replace the Scan Pattern with an Aviator Scan Pattern:
% The scan can be generated using a preconfigured Aviator aircraft which 
% follows a search pattern at a preconfigured scan width. The aviator turn
% around points are used to create the scan. Midpoints, turn out times, and
% a fixed number of points are also options.
useAviatorForScanPattern = false; % If true, the aviator search pattern is used to seed the scan. The scanWidth equivalent is defined in the aviator search pattern.
useMidpoints = false; % Add the mid points between each turn, used with Aviator search pattern only
useTurnoutTimes = false; % Add the exit of the turns, used with Aviator search pattern only
useFixedNumScanPoints = false; % If true a fixed number of equally places scan points are used instead of the turning points, used with Aviator search pattern only
fixedNumPoints = 50*ones(length(areaTargetList),1); % Number of points used for the scan if using a fixed number, used with Aviator search pattern only

%% Code
clc; % Clear the Command Window
computedScanDur = zeros(length(areaTargetList),1); % Store scan durations

% Connect to STK
app = actxGetRunningServer('STK12.application');
root = app.personality2;

% Set object model units but don't change the GUI
root.Isolate();
root.UnitPreferences.SetCurrentUnit('DateFormat','EpSec');
t1 = tic; % Time the script

% Create a scan for each area target in the list
for ii = 1:length(areaTargetList)
    
    % Get the names of the preconfigured objects in the scenario
    acName = ['Aviator_AT_Search',scanNumberList{ii}]; % Aircraft Name
    atName = ['AT',scanNumberList{ii}]; % Area Target that Aviator References
    scanName = ['Scan',scanNumberList{ii}]; % Ground Scan Name 

    % Import Area Target if it is a country and is needed, The Unload command is necessary to get rid
    % of extra area targets imported.
    if ~root.CurrentScenario.Children.Contains('eAreaTarget',areaTargetList{ii})
        disp(['Inserting AreaTarget: ',areaTargetList{ii}])
        tic
        cmd = ['GIS * Import "C:\Program Files\AGI\STK 11\Data\Shapefiles\Countries\',areaTargetList{ii},'\',areaTargetList{ii},'.shp" AreaTarget'];
        try
            root.ExecuteCommand(cmd);
            root.ExecuteCommand(['UnloadMulti / */AreaTarget/',areaTargetList{ii},'_*']);
            root.ExecuteCommand(['Graphics */AreaTarget/',areaTargetList{ii},' Show Off']);
        catch
            disp(['Shape File Not Found For: ',areaTargetList{ii}])
            return
        end
        toc
        disp(' ')
    end


    % Update Aviator Aircraft by updating AreaTarget points
    if useAviatorForScanPattern == true
        atCountry = root.GetObjectFromPath(['AreaTarget/',areaTargetList{ii}]);
        atAviator = root.GetObjectFromPath(['AreaTarget/',atName]);
        llAT = atCountry.AreaTypeData.ToArray;
        disp(['AreaTarget being Scanned: ',areaTargetList{ii}])
        disp(['Updating Aviator AreaTarget: ',atName])
        tic
        root.BeginUpdate();
        atAviator.AreaTypeData.RemoveAll();
        atAviator.CommonTasks.SetAreaTypePattern(llAT);
        toc
        disp(' ')
        disp(['Updating Aviator Scan: ',acName])
        tic
        root.EndUpdate();
        toc
        disp(' ')

        % Read Aviator Ephemeris
        disp(['Updating Ground Scan: ',scanName])
        tic
        ac = root.GetObjectFromPath(['Aircraft/',acName]);
        lladp = ac.DataProviders.Item('LLA State');
        fdp = lladp.Group.Item('Fixed');
        elems = {'Lat';'Lon'};
        objStart = ac.Vgt.Events.Item('EphemerisStartTime');
        objStart = objStart.FindOccurrence;
        objStart = objStart.Epoch;
        objStop = ac.Vgt.Events.Item('EphemerisStopTime');
        objStop = objStop.FindOccurrence;
        objStop = objStop.Epoch;
        if useFixedNumScanPoints == true
            timeRes = ((objStop-objStart)/(fixedNumPoints(ii)-1));
            res = fdp.ExecElements(objStart,objStop,timeRes,elems);
            lla = cell2mat(res.DataSets.ToArray);
        else
            % roll = ac.Vgt.CalcScalars.Item('Roll');
            % minmax = roll.EmbeddedComponents.Item(4); % 'TimesOfLocalMinMax'
            % res = minmax.FindTimes;
            % times = res.Times;
            
            % Get the times when the ac turns
            rolling = ac.Vgt.Conditions.Item('Rolling'); % when |roll angle| > 1 deg
            ct = rolling.EmbeddedComponents.Item(22); % InCrossingTimes
            res = ct.FindTimes;
            times = res.Times;
            objStop = objStop-1; % Move the stop time back 1 sec to make sure there is valid ephemeris
            % Add start and stop times to the list of rolling times
            times = [objStart;times;objStop];
            % Adds the midpoints between the turns
            if useMidpoints == true
                timesMat = cell2mat(times);
                timesMid = timesMat(1:end-1)+diff(timesMat)/2;
                timesMat = sort([timesMat;timesMid]);
                times = num2cell(timesMat);
            end
            % Adds the end of the exits of the turns as scan points
            if useTurnoutTimes== true
                ct = rolling.EmbeddedComponents.Item(23); % OutCrossingTimes
                res = ct.FindTimes;
                timesOut = res.Times;
                times = num2cell(sort(cell2mat([times;timesOut])));
            end
            res = fdp.ExecSingleElementsArray(times,elems);
%             lla = cell2mat([res.GetArray('Lat'), res.GetArray('Lon')]);
            lla = cell2mat([res.GetArray(int32(0)), res.GetArray(int32(1))]);
        end
        alts =ones(length(lla),1)*alt; % Add altitudes
        lla =  [lla,alts];
    else % Use the matlab function GenerateScan to generate the scan pattern
        disp(['AreaTarget being Scanned: ',areaTargetList{ii}])
        disp(['Generating and Updating Ground Scan: ',scanName])
        tic
        ll = GenerateScan(root,areaTargetList{ii},scanSpeedList(ii),scanWidthList(ii),scanDirList{ii},useBoundingRectangle);
        alts =ones(length(ll),1)*alt; % Add altitudes
        lla =  [ll,alts];
    end

    % Add the scan points to a ground scan 
    if root.CurrentScenario.Children.Contains('eGroundVehicle',scanName)
        scan = root.GetObjectFromPath(['GroundVehicle/',scanName]);
    else
        scan = root.CurrentScenario.Children.New('eGroundVehicle',scanName);
    end
    scan.SetRouteType('ePropagatorGreatArc');
    scan.Route.SetAltitudeRefType('eWayPtAltRefWGS84')
    scan.Route.Waypoints.RemoveAll();
    for i = 1:size(lla)
        wp = scan.Route.Waypoints.Add();
        wp.Latitude = lla(i,1);
        wp.Longitude = lla(i,2);
        wp.Altitude = lla(i,3);
        wp.TurnRadius = 0;
        wp.Speed = scanSpeedList(ii);
    end
    scan.Route.Propagate();
    scanData = cell2mat(scan.Route.Waypoints.ToArray());
    scanData = scanData(:,1:4);
    computedScanDur(ii) = (scanData(end,1)-scanData(1,1));

    % Override the scan speed with a fixed duration
    if useFixedScanDur == true
        ratio = computedScanDur(ii)/fixedScanDur(ii);
        scanSpeedList(ii) = scanSpeedList(ii)*ratio;
        for i = 1:size(lla)
            wp = scan.Route.Waypoints.Item(i-1);
            wp.Speed = scanSpeedList(ii);
        end
        scan.Route.Propagate();
        scanData = cell2mat(scan.Route.Waypoints.ToArray());
        scanData = scanData(:,1:4);
        computedScanDur(ii) = (scanData(end,1)-scanData(1,1));
    end

    disp([scanName, ' # of points: ',num2str(length(scanData))])
    disp([scanName, ' speed: ',num2str(scanSpeedList(ii)),' [km/s]'])
    disp([scanName, ' duration: ',num2str(computedScanDur(ii)),' [secs]'])
    toc
    disp(' ')

    % Insert Scan Points as Targets. If this is causing issues consider turning off terrain.
    if insertScanPointsAsTargets == true
        tic
        disp('Inserting Scan Targets')
        % Remove Old Targets
        try
            root.ExecuteCommand(['UnloadMulti / */Target/',scanName,'Target*']);
        catch
        end
        % Add in Targets
        targetNames = [];
        for i = 1:length(lla)
            targetNames = [targetNames,' ',scanName,'Target',num2str(i)];
        end
        root.ExecuteCommand(['NewMulti / */Target ',num2str(length(lla)),targetNames]);
        for i = 1:length(lla)
            target = root.GetObjectFromPath(['Target/',scanName,'Target',num2str(i)]);
            target.Position.AssignGeodetic(lla(i,1),lla(i,2),lla(i,3))
        end
        toc
        disp(' ')
    end

    % Create SOLIS seqs file
    if runSOLISseqs == true 
        createSOLISseqsFile = true;
    end
    if createSOLISseqsFile == true
        tic
        disp(['Writing seqs file: ',seqsLocation,'\',areaTargetList{ii},scanName,'.seqs'])
        fid = fopen([seqsLocation,'\',areaTargetList{ii},scanName,'.seqs'],'w');
        fprintf(fid,'<?xml version="1.0"?>\n');
        fprintf(fid,'<sequence version="1.0.3">\n');
        fprintf(fid,'  <comment text="--- TARGET PLANNER TIMELINE SUMMARY ---" />\n');
        fprintf(fid,['  <sc_cmd delta_time="0.1" mnemonic="EPH_SET_TGT">\n']);
        fprintf(fid,'    <parameter mnemonic="TARGET" value="GeneralTarget" />\n');
        fprintf(fid,'    <parameter mnemonic="ARGTYPE" value="LLA_deg_m" />\n');
        fprintf(fid,['    <parameter mnemonic="ARG0" value="',num2str(scanData(1,2)),'" />\n']);
        fprintf(fid,['    <parameter mnemonic="ARG1" value="',num2str(scanData(1,3)),'" />\n']);
        fprintf(fid,['    <parameter mnemonic="ARG2" value="',num2str(scanData(1,4)),'" />\n']);
        fprintf(fid,'  </sc_cmd>\n');
        fprintf(fid,['  <sc_cmd delta_time="0.1" mnemonic="MDC_SLEW_TRACK">\n']);
        fprintf(fid,['    <parameter mnemonic="SLEWTIME_S" value="',num2str(slewTimeBetweenScans(ii)-0.1),'" />\n']);
        fprintf(fid,'    <parameter mnemonic="SLEWMODE" value="RendezSlew" />\n');
        fprintf(fid,'    <parameter mnemonic="TRACKMODE" value="TargetTrack" />\n');
        fprintf(fid,'    <parameter mnemonic="CLOCKINGOPTION" value="TgtNormal" />\n');
        fprintf(fid,'  </sc_cmd>\n');

        for i = 1:size(scanData)-1
            if i ~= 1
                fprintf(fid,['  <sc_cmd delta_time="',num2str(scanData(i,1)-scanData(i-1,1)-0.1),'" mnemonic="EPH_SET_TGT">\n']);
            else
                fprintf(fid,['  <sc_cmd delta_time="',num2str(slewTimeBetweenScans(ii)),'" mnemonic="EPH_SET_TGT">\n']);
            end
            fprintf(fid,'    <parameter mnemonic="TARGET" value="GeneralTarget" />\n');
            fprintf(fid,'    <parameter mnemonic="ARGTYPE" value="LLA_deg_m" />\n');
            fprintf(fid,['    <parameter mnemonic="ARG0" value="',num2str(scanData(i+1,2)),'" />\n']);
            fprintf(fid,['    <parameter mnemonic="ARG1" value="',num2str(scanData(i+1,3)),'" />\n']);
            fprintf(fid,['    <parameter mnemonic="ARG2" value="',num2str(scanData(i+1,4)),'" />\n']);
            fprintf(fid,'  </sc_cmd>\n');

            fprintf(fid,['  <sc_cmd delta_time="0.1" mnemonic="MDC_SLEW_TRACK">\n']);
            fprintf(fid,['    <parameter mnemonic="SLEWTIME_S" value="',num2str(scanData(i+1,1)-scanData(i,1)-0.1),'" />\n']);
            fprintf(fid,['    <parameter mnemonic="SLEWMODE" value="',slewMode,'" />\n']);
            fprintf(fid,'    <parameter mnemonic="TRACKMODE" value="TargetTrack" />\n');
            fprintf(fid,'    <parameter mnemonic="CLOCKINGOPTION" value="TgtNormal" />\n');
            fprintf(fid,'  </sc_cmd>\n');
        end
        fprintf(fid,['  <sc_cmd delta_time="',num2str(scanData(i+1,1)-scanData(i,1)-0.1),'" mnemonic="MDC_GOTO_HOLD">\n']);
        fprintf(fid,'    <parameter mnemonic="HOLDMODE" value="Hold" />\n');
        fprintf(fid,'  </sc_cmd>\n');
        fprintf(fid,'</sequence>\n');
        fclose(fid);
        toc
        disp(' ')
    end

end

% Run SOLIS
if runSOLISseqs == true && createSOLISseqsFile == true
    clear STK
    % Make sure the necessary scripts are on the Matlab path
    P = genpath('C:\Program Files\AGI\STK 11\Solis\Scripts');
    addpath(P);
    STK = SOL_API_ConnectToRunningSTK(); % Connect to SOLIS
    disp(' ')
    % Get Satellite name from seqs path
    SatName = split(seqsLocation,'\');
    SatName = SatName{end-1};
    % Clear Sequence Execution List
    STK.SOL_Satellite = STK.SOL_Connect.GetSatellite(SatName); % Pick the specified satellite 
    STK.SOL_Satellite.SequenceSelection.ClearExecutionList()
    % Add seqs file to the Execution List
    for ii = 1:length(areaTargetList)
        scanName = ['Scan',scanNumberList{ii}]; % Ground Scan Name 
        try
            STK.SOL_Satellite.SequenceSelection.AddSequence([areaTargetList{ii},scanName],0,0)
        catch
            disp(' ')
            disp(['SOLIS Error Message: ',[areaTargetList{ii},scanName],' was not found'])
            disp('In SOLIS click the Sequence Editor page and then the Sequence Selection under the Mission Sequence Definition to reload the sequences')
            input('Press Enter in Matlab after the sequence appears in the Sequence Library','s');
            STK.SOL_Satellite.SequenceSelection.AddSequence([areaTargetList{ii},scanName],0,0)
            disp(' ')
        end
    end
    % Set the Runtime of SOLIS to the estimate scan duration
    buffer = 10; % Extra SOLIS run time in case the scans take longer than expected
    SOLISRunTime = (sum(computedScanDur)+slewTimeBetweenScans(ii)*length(computedScanDur)+buffer); % sum of scan durations + slew times between scans + buffer
    simStartTime = char(STK.SOL_Satellite.StartTime);
    simStartTime = datetime(simStartTime,'InputFormat','MM/dd/yyyy hh:mm:ss a');
    simEndTime = simStartTime + seconds(SOLISRunTime);
    STK.SOL_Satellite.EndTime = datestr(simEndTime,'mm/dd/yyyy hh:MM:ss AM');
    disp(['SOLIS Runtime: ',num2str(SOLISRunTime),' sec'])
    disp(' ')
    % Run SOLIS
    disp('Running SOLIS')
    tic
    config = STK.SOL_Satellite.GetConfiguration();
    config.Run();
    config.WaitForSimulationEnd(); % Wait until the Run finishes 
    toc
    disp(' ')
end

disp('Script Done')
t2 = toc(t1);
disp(['Total Run Time: ',num2str(t2)])

%%
if createSOLIStpcFile == true
    disp(['Writing tpc file: ',tpcLocation,'\ScanOrderAndSlewDur.tpc'])
    tic
    fid = fopen([tpcLocation,'\ScanOrderAndSlewDur.tpc'],'w');
    fprintf(fid,'<?xml version="1.0" encoding="utf-8"?>\n');
    fprintf(fid,'<TargetPlannerConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" BtwnTgtMinDwell="0" BeginTime="2019-08-16T19:00:00" EndTime="2019-08-16T21:00:00">\n');
    fprintf(fid,'  <BtwnTgtCmd SlewMode="RendezSlew" DestinationMode="Hold" ClockingOption="TgtNormal" />\n');
    fprintf(fid,'  <Targets>\n');
    for i = 1:length(areaTargetList)
        atCountry = root.GetObjectFromPath(['AreaTarget/',areaTargetList{i}]);
        centriod = cell2mat(atCountry.Position.QueryPlanetocentricArray());
        fprintf(fid,['    <Target Name="',areaTargetList{i},'" TargetConfigReference="',areaTargetList{i},'Scan" Priority="100">\n']);
        fprintf(fid,'      <Locations>\n');
        fprintf(fid,['        <Location Name="',areaTargetList{i},'" Latitude="',num2str(centriod(1)),'" Longitude="',num2str(centriod(2)),'" Altitude="',num2str(round(centriod(3),3)),'" HeightAboveGround="0" />\n']);
        fprintf(fid,'        <Location xsi:nil="true" />\n');
        fprintf(fid,'      </Locations>\n');
        fprintf(fid,'      <Constraints>\n');
        fprintf(fid,'        <Constraint ConstraintName="LineOfSight" ConstraintType="26" ExclIntvl="false" MaxRelMotion="0" MaxTimeStep="0" />\n');
        fprintf(fid,'      </Constraints>\n');
        fprintf(fid,'      <TargetType>Target</TargetType>\n');
        fprintf(fid,'    </Target>\n');
    end
    fprintf(fid,'  </Targets>\n');
    fprintf(fid,'  <TargetingConfigurations>\n');
    for i = 1:length(areaTargetList)
        fprintf(fid,['    <TargetingConfiguration Name="',areaTargetList{i},'Scan" TargetingType="Point" OnTargetSequenceDeltaTimeInit="0" OnTargetSequenceName="" OffTargetSequenceDeltaTimeInit="0" OffTargetSequenceName="" MaxSamples="1" MinimumDwellTime="',num2str(ceil(computedScanDur(i))),'" DesiredDwellTime="',num2str(ceil(computedScanDur(i))),'" ConstraintSourceReference="[No Constraints - Line of Sight]">\n']);
        fprintf(fid,'      <ModeCmd SlewMode="RendezSlew" DestinationMode="TargetTrack" ClockingOption="TgtNormal" />\n');
        fprintf(fid,'    </TargetingConfiguration>\n');
    end
    fprintf(fid,'  </TargetingConfigurations>\n');
    fprintf(fid,'</TargetPlannerConfiguration>\n');
    fclose(fid);
    toc
end


%% Old tpc and seqs files
% Creating SOLIS target planner file
% if createSOLIStpcFile == true
%     disp('Writing tpc file')
%     fid = fopen([tpcLocation,'\',country,scanName,'.tpc'],'w');
%     fprintf(fid,'<?xml version="1.0" encoding="utf-8"?>\n');
%     fprintf(fid,'<TargetPlannerConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" BtwnTgtMinDwell="0" BeginTime="2019-08-16T19:00:00" EndTime="2019-08-16T21:00:00">\n');
%     fprintf(fid,'  <BtwnTgtCmd SlewMode="RendezSlew" DestinationMode="Hold" ClockingOption="TgtNormal" />\n');
%     fprintf(fid,'  <Targets>\n');
%     for i = 1:length(lla)
% %         fprintf(fid,['    <Target Name="SearchTarget',num2str(i),'" TargetConfigReference="AT_Search" Priority="',num2str(i),'">\n']);
%         fprintf(fid,['    <Target Name="SearchTarget',num2str(i),'" TargetConfigReference="AT_Search" Priority="1">\n']);
%         fprintf(fid,'      <Locations>\n');
%         fprintf(fid,['        <Location Name="SearchTarget',num2str(i),'" Latitude="',num2str(lla(i,1)),'" Longitude="',num2str(lla(i,2)),'" Altitude="',num2str(lla(i,3)),'" HeightAboveGround="0" />\n']);
%         fprintf(fid,'        <Location xsi:nil="true" />\n');
%         fprintf(fid,'      </Locations>\n');
%         fprintf(fid,'      <Constraints>\n');
%         fprintf(fid,'        <Constraint ConstraintName="LineOfSight" ConstraintType="26" ExclIntvl="false" MaxRelMotion="0" MaxTimeStep="0" />\n');
%         fprintf(fid,'      </Constraints>\n');
%         fprintf(fid,'      <TargetType>Target</TargetType>\n');
%         fprintf(fid,'    </Target>\n');
%     end
%     fprintf(fid,'  </Targets>\n');
%     fprintf(fid,'  <TargetingConfigurations>\n');
%     fprintf(fid,'    <TargetingConfiguration Name="AT_Search" TargetingType="Point" OnTargetSequenceDeltaTimeInit="0" OnTargetSequenceName="" OffTargetSequenceDeltaTimeInit="0" OffTargetSequenceName="" MaxSamples="1" MinimumDwellTime="1" DesiredDwellTime="30" ConstraintSourceReference="[No Constraints - Line of Sight]">\n');
%     fprintf(fid,'      <ModeCmd SlewMode="RendezSlew" DestinationMode="TargetTrack" ClockingOption="TgtNormal" />\n');
%     fprintf(fid,'    </TargetingConfiguration>\n');
%     fprintf(fid,'  </TargetingConfigurations>\n');
%     fprintf(fid,'</TargetPlannerConfiguration>\n');
%     fclose(fid);
% end

%%

% % Creating SOLIS target planner file
% if createSOLISseqsFile == true
%     disp('Writing seqs file')
%     fid = fopen([seqsLocation,'\',country,scanName,'.seqs'],'w');
%     fprintf(fid,'<?xml version="1.0"?>\n');
%     fprintf(fid,'<sequence version="1.0.3">\n');
%     fprintf(fid,'  <comment text="--- TARGET PLANNER TIMELINE SUMMARY ---" />\n');
%     fprintf(fid,'  <sc_cmd delta_time="0.1" mnemonic="MDC_SLEW_TRACK">\n');
%     fprintf(fid,'    <parameter mnemonic="SLEWTIME_S" value="10" />\n');
%     fprintf(fid,'    <parameter mnemonic="SLEWMODE" value="RendezSlew" />\n');
%     fprintf(fid,'    <parameter mnemonic="TRACKMODE" value="TargetTrack" />\n');
%     fprintf(fid,'    <parameter mnemonic="CLOCKINGOPTION" value="InertialFixed" />\n');
%     fprintf(fid,'  </sc_cmd>\n');
%     for i = 1:length(scanData)-1
%         fprintf(fid,['  <sc_cmd delta_time="',num2str(scanData(i+1,1)-scanData(i,1)),'" mnemonic="EPH_SET_TGTSCAN">\n']);
%         fprintf(fid,'    <parameter mnemonic="TARGET" value="GeneralTarget" />\n');
%         fprintf(fid,['    <parameter mnemonic="GROUNDSPEED_KMPS" value="',num2str(scanSpeed),'" />\n']);
%         fprintf(fid,'    <parameter mnemonic="ARGTYPE" value="LLA_deg_m" />\n');
%         fprintf(fid,['    <parameter mnemonic="STARTARG0" value="',num2str(scanData(i,2)),'" />\n']);
%         fprintf(fid,['    <parameter mnemonic="STARTARG1" value="',num2str(scanData(i,3)),'" />\n']);
%         fprintf(fid,['    <parameter mnemonic="STARTARG2" value="',num2str(scanData(i,4)),'" />\n']);
%         fprintf(fid,['    <parameter mnemonic="ENDARG0" value="',num2str(scanData(i+1,2)),'" />\n']);
%         fprintf(fid,['    <parameter mnemonic="ENDARG1" value="',num2str(scanData(i+1,3)),'" />\n']);
%         fprintf(fid,['    <parameter mnemonic="ENDARG2" value="',num2str(scanData(i+1,4)),'" />\n']);
%         fprintf(fid,'    <parameter mnemonic="SCANINTERPTYPE" value="LatLonAlt" />\n');
%         fprintf(fid,'  </sc_cmd>\n');
%         fprintf(fid,'  <sc_cmd delta_time="0.1" mnemonic="MDC_SLEW_TRACK">\n');
%         fprintf(fid,'    <parameter mnemonic="SLEWTIME_S" value="10" />\n');
%         fprintf(fid,'    <parameter mnemonic="SLEWMODE" value="RendezSlew" />\n');
%         fprintf(fid,'    <parameter mnemonic="TRACKMODE" value="TargetTrack" />\n');
%         fprintf(fid,'    <parameter mnemonic="CLOCKINGOPTION" value="InertialFixed" />\n');
%         fprintf(fid,'  </sc_cmd>\n');
%     end
%     fprintf(fid,'  <sc_cmd delta_time="30" mnemonic="MDC_GOTO_HOLD">\n');
%     fprintf(fid,'    <parameter mnemonic="HOLDMODE" value="Hold" />\n');
%     fprintf(fid,'  </sc_cmd>\n');
%     fprintf(fid,'</sequence>\n');
%     fclose(fid);
% end
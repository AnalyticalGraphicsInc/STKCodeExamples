function [] = multiSegmentAttitude(satName,facName, prePassTime, SlewLength)
% prePassTime: Pre operational time in seconds
% SlewLength: Slew time in seconds
%% Get Active STK Scenario
% Change depending on your version
app = actxGetRunningServer('STK12.application');
%app = actxGetRunningServer('STK11.application');

% Grab the root and scenario objects
root = app.Personality2;
scenario = root.CurrentScenario;

%% Grab Object Handles
satellite = scenario.Children.Item(satName);
facility = scenario.Children.Item(facName);

%% Create 10 min Sunlight Constraint
% Remove the interval lists if they already exist
try
    satellite.Vgt.EventIntervalLists.Remove('offsetSunlight');
    satellite.Vgt.EventIntervalLists.Remove('sunConstraint');
end

% Grab sunlight intervals
sunlightIntervals = satellite.Vgt.EventIntervalLists.Item('LightingIntervals.Sunlight');

% Offset sunlight intervals by pre pass time and slew length
offsetSunlight = satellite.Vgt.EventIntervalLists.Factory.CreateEventIntervalListTimeOffset('offsetSunlight','Intermediate array to offset sunlight times by 10 min');
offsetSunlight.ReferenceIntervals = sunlightIntervals;
offsetSunlight.TimeOffset = prePassTime + SlewLength;

% Create constraint interval list as times where both the offset and
% orginal sunlight intervals are satisfied
sunConstraint = satellite.Vgt.EventIntervalLists.Factory.CreateEventIntervalListMerged('sunConstraint','Shortened lighting intervals to be used as temporal constraint on satellite');
sunConstraint.MergeOperation = 'eCrdnEventListMergeOperationAND';
sunConstraint.SetIntervalListA(offsetSunlight);
sunConstraint.SetIntervalListB(sunlightIntervals);

% Add temporal constraint to satellite
temporalConstraint = satellite.AccessConstraints.AddConstraint('eCstrIntervals');
connectString = append('SetConstraint */Satellite/',satName,' Intervals Include TimeComponent "Satellite/',satName,' sunConstraint Interval List"');
root.ExecuteCommand(connectString);

%% Compute Access
% Set time units to Epoch Seconds for a continuous time scale
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Compute access
access = facility.GetAccessToObject(satellite);
access.ComputeAccess();

% Grab operational window start and stop times
accessData = access.DataProviders.Item('Access Data').Exec(scenario.StartTime,scenario.StopTime);
try
    operationalStart = round(cell2mat(accessData.DataSets.GetDataSetByName('Start Time').GetValues()));
    operationalStop = round(cell2mat(accessData.DataSets.GetDataSetByName('Stop Time').GetValues()));
catch
    fprintf("No Access Intervals Found -- Returning");
    return
end
        
% Define pre operational interval start times based on operational start times
preOpStart = operationalStart - prePassTime - SlewLength;
PreOpSlewStart = preOpStart - SlewLength;
OpSlewStart = operationalStart - SlewLength;

%% Create Pre Operational Interval List
% Remove the interval lists if they already exist
try
    access.Vgt.EventIntervalLists.Remove('offsetAccess');
    access.Vgt.EventIntervalLists.Remove('slewOffsetAccess');
    access.Vgt.EventIntervalLists.Remove('preOpIntervals');
end
accessIntervals = access.Vgt.EventIntervalLists.Item('AccessIntervals');
offsetAccess = access.Vgt.EventIntervalLists.Factory.CreateEventIntervalListTimeOffset('offsetAccess','Negative offset of access times');
offsetAccess.ReferenceIntervals = accessIntervals;
offsetAccess.TimeOffset = -(SlewLength + prePassTime);
slewOffsetAccess = access.Vgt.EventIntervalLists.Factory.CreateEventIntervalListTimeOffset('slewOffsetAccess','Negative offset of access times just slew');
slewOffsetAccess.ReferenceIntervals = accessIntervals;
slewOffsetAccess.TimeOffset = -(SlewLength);
preOpIntervals = access.Vgt.EventIntervalLists.Factory.CreateEventIntervalListMerged('preOpIntervals','List of preOp Intervals');
preOpIntervals.MergeOperation = 'eCrdnEventListMergeOperationMINUS';
preOpIntervals.SetIntervalListA(offsetAccess);
preOpIntervals.SetIntervalListB(slewOffsetAccess);

%% Set Up Attitude Profile
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');

% Clear attitude profile
connectString = append('SetAttitude */Satellite/',satName,' ClearData AllProfiles');
root.ExecuteCommand(connectString);

% Account for the first preOp time not being the start of the scenario
if preOpStart(1) <= str2num(root.ConversionUtility.ConvertDate('UTCG','EpSec',scenario.StartTime))
    connectString = append('SetAttitude */Satellite/',satName,' Profile AlignConstrain Axis 0 1 0 "Satellite/',satName,' Sun" Axis 0 0 1 "Satellite/',satName,' Nadir(Detic)"');
    root.ExecuteCommand(connectString);
    skip_first = 1;
else %if preOpStart(1) ~= 0
    connectString = append('SetAttitude */Satellite/',satName,' Profile AlignConstrain Axis 1 0 0 "Satellite/',satName,' Velocity" Axis 0 0 1 "Satellite/',satName,' Nadir(Detic)"');
    root.ExecuteCommand(connectString);
    skip_first = 0;
    
end

% Fill in remaining attitude segments
for i = 1:length(preOpStart)
    % PreOp segment
    if ~(i == 1 && skip_first)
        % PreOp Fixed Time Slew
        PreOpSlewTime = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(PreOpSlewStart(i)));
        connectString = append('AddAttitude */Satellite/', satName,' Profile Slew "',PreOpSlewTime,'" FixedTimeSlew Smooth On "');
        root.ExecuteCommand(connectString);
        
        
        preOpTime = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(preOpStart(i)));
        connectString = append('AddAttitude */Satellite/',satName,' Profile PreOp',num2str(i),' "',preOpTime,'" AlignConstrain Axis 0 1 0 "Satellite/',satName,' Sun" Axis 0 0 1 "Satellite/',satName,' Nadir(Detic)"');
        root.ExecuteCommand(connectString);
    end
    % Pre-Op Slew
    OpSlewTime = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(OpSlewStart(i)));
    connectString = append('AddAttitude */Satellite/', satName,' Profile Slew "',OpSlewTime,'" FixedTimeSlew Smooth On "');
    root.ExecuteCommand(connectString);
    
    % Operational segment
    operationalTime = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(operationalStart(i)));
    connectString = append('AddAttitude */Satellite/',satName,' Profile Operational',num2str(i),' "',operationalTime,'" AlignConstrain Axis 1 0 0 "Satellite/',satName,' ',facName,'" Axis 0 0 1 "Satellite/',satName,' Nadir(Detic)"');
    root.ExecuteCommand(connectString);
    
    % Pre-Non-Ops Slew
    PostOpsSlewTime = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(operationalStop(i)));
    connectString = append('AddAttitude */Satellite/', satName,' Profile Slew "',PostOpsSlewTime,'" FixedTimeSlew Smooth On "');
    root.ExecuteCommand(connectString);
    
    % Non-Operational segment
    nonoperationalTime = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(operationalStop(i)+SlewLength));
    connectString = append('AddAttitude */Satellite/',satName,' Profile NonOperational',num2str(i),' "',nonoperationalTime,'" AlignConstrain Axis 1 0 0 "Satellite/',satName,' Velocity" Axis 0 0 1 "Satellite/',satName,' Nadir(Detic)"');
    root.ExecuteCommand(connectString);
end

%% Add Text Annotations
try
    root.ExecuteCommand('VO * Annotation Delete PreOp Text');
    root.ExecuteCommand('VO * Annotation Delete Operational Text');
end
root.ExecuteCommand(append('VO * Annotation Add PreOp Text String "Pre-Operational" Coord pixel Position 5 5 0 Color yellow FontStyle Large Interval Import "Access/Facility-',facName,'-To-Satellite-',satName,' preOpIntervals"'));
root.ExecuteCommand(append('VO * Annotation Add Operational Text String "Operational" Coord pixel Position 5 5 0 Color green FontStyle Large Interval Import "Access/Facility-',facName,'-To-Satellite-',satName,' AccessIntervals"'));

%% Display Vectors
vector = satellite.VO.Vector;
try
    sunvector = vector.RefCrdns.GetCrdnByName('eVectorElem',append('Satellite/',satName,' Sun Vector'));
    sunvector.Visible = true;
    sunvector.Color = 65535;
    sunvector.LabelVisible = true;
end
try
    tovector = vector.RefCrdns.Add('eVectorElem',append('Satellite/',satName,' ',facName,' Vector'));
    tovector.Visible = true;
    tovector.LabelVisible = true;
end
try
    bodyaxes = vector.RefCrdns.GetCrdnByName('eAxesElem',append('Satellite/',satName,' Body Axes'));
    bodyaxes.Visible = true;
    bodyaxes.LabelVisible = true;
end
end
%% visualizeDeltaVVectors
% Adds delta V vectors along the trajectory of an Astrogator satellite. The
% delta Vs come from the maneuver summary report. The maneuver location 
% and vector are also created in Analysis Workbench.
% 
% Instructions:
% Open a STK scenario with a propagated Astrogator satellite which has
% maneuvers. In the Input section of the Matlab code, update the satellite
% name, desired coordinate system to display the dV Vectors and graphics 
% options. Then click Run in Matlab. The code will need to be rerun
% to update the maneuvers if the trajectory changes.
% 
% Notes:
% When selecting the 'displayAxes', ensure both a coordinate axes and a
% coordinate system exist in Analysis Workbench with the same name.
% 
% For finite maneuvers the delta V vector is placed at the beginning
% of the maneuver and appears after the maneuver occurs. Change
% Line 193 to put the dV vector at the start of the burn:
% ephDP = sat.DataProviders.Item('Astrogator Maneuver Ephemeris Block Initial');
% 
% Ensure the spacecraft does not run out of fuel during a maneuver.

%% Inputs
% Required Inputs
satName = 'TearDrop';% Satellite Name
displayAxes = 'Satellite/Target RIC';% Coordinate Axes and System To display the dV vectors
% displayAxes = 'CentralBody/Earth Fixed';
% displayAxes = 'Satellite/Satellite2 RIC';

% Graphics Options
showAfterOccurs = 1; % 0 or 1, Show dV after it occurs = 1, Show dV always = 0
pointSize = 6; % 0 to 10, Size of the dV point
vectorScale = 3.9; % 0 to 10, Size of the dV vector
showPoint = 1; % 0 or 1, Show point = 1, Don't show = 0
showVec = 1; % 0 or 1, Show vector = 1, Don't show = 0
showPointLabel = 1; % 0 or 1, Show point label = 1, Don't show = 0
showVecLabel = 0; % 0 or 1, Show vector label = 1, Don't show = 0
showVecMag  = 1; % 0 or 1, Show dV magnitude [m/sec] = 1, Don't show = 0
useDetailThreshold = 0; % 0 or 1, Use display distance thresholds = 1, Don't use = 0
usePointColor = 1; % 0 or 1, Use the pointColor listed below = 1, Use default colors = 0
useVectorColor = 1; % 0 or 1, Use the vectorColor listed below = 1, Use default colors = 0
pointColor = '00FF00'; % Hexadecimal RGB value, i.e. Cyan = 00FFFF, Light Green = 00FF00, Yellow = FFFF00, Red = FF0000
vectorColor = '00FF00'; % Hexadecimal RGB value

%% Code
% Create an instance of STK and grab the root
app = actxGetRunningServer('STK11.application');
app.Visible = 1;
root = app.Personality2;

% Switch to Epoch Seconds Before Pulling Data
currentUnit = root.UnitPreferences.Item('DateFormat').CurrentUnit.Abbrv;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Grab the needed handles
sat = root.GetObjectFromPath(['*/Satellite/',satName]);
scen = root.CurrentScenario;
prop = sat.Propagator;
MCS = prop.MainSequence;
vec = sat.Vgt.Vectors;
point = sat.Vgt.Points;

% Get the start and stop time of the satellite
StartTime = MCS.Item(0).InitialState.Epoch; % First Segment
StopTime = MCS.Item(MCS.Count-1).FinalState.Epoch; % Last segment

% Grab the maneuver locations and vectors from the data providers
ephDP = sat.DataProviders.Item('Astrogator Maneuver Ephemeris Block Final');
manDP = ephDP.Group.Item('Maneuver');
% Create the Calc Object components for the dVs in custom frames as needed.
if strcmpi(displayAxes,'CentralBody/Earth Inertial')
    DP = manDP.ExecElementsNativeTimes(StartTime, StopTime, {'Time';'Inertial_DeltaV_Magnitude';'Inertial_DeltaVx';'Inertial_DeltaVy';'Inertial_DeltaVz'});
else
    astroCompFol = scen.ComponentDirectory.GetComponents('eComponentAstrogator');
    calcObjFol = astroCompFol.GetFolder('Calculation Objects');
    manFol = calcObjFol.GetFolder('Maneuver');
    try
        dVx = manFol.Item(['Inertial_DeltaVx ',displayAxes]);
        dVy = manFol.Item(['Inertial_DeltaVy ',displayAxes]);
        dVz = manFol.Item(['Inertial_DeltaVz ',displayAxes]);
    catch
        dVx = manFol.DuplicateComponent('Inertial_DeltaVx',['Inertial_DeltaVx ',displayAxes]);
        dVy = manFol.DuplicateComponent('Inertial_DeltaVy',['Inertial_DeltaVy ',displayAxes]);
        dVz = manFol.DuplicateComponent('Inertial_DeltaVz',['Inertial_DeltaVz ',displayAxes]);
    end
    dVx.CoordAxesName = displayAxes;
    dVy.CoordAxesName = displayAxes;
    dVz.CoordAxesName = displayAxes;
    dVxName = strrep(['Inertial_DeltaVx ',displayAxes],' ','_');
    dVyName = strrep(['Inertial_DeltaVy ',displayAxes],' ','_');
    dVzName = strrep(['Inertial_DeltaVz ',displayAxes],' ','_');
    DP = manDP.ExecElementsNativeTimes(StartTime, StopTime, {'Time';'Inertial_DeltaV_Magnitude';dVxName;dVyName;dVzName});
end
dVData = cell2mat(DP.DataSets.ToArray);
n = length(dVData)/5;
dVData = reshape(dVData,5,n);
dVData(2:5,:) = dVData(2:5,:)*1000; % Convert to m/s from km/s

% Create the Calc Object components for the maneuver start location in the custom frames as needed.
ephDP = sat.DataProviders.Item('Astrogator Maneuver Ephemeris Block Initial');
cartDP = ephDP.Group.Item('Cartesian Elems');
if strcmpi(displayAxes,'CentralBody/Earth Inertial')
    xyzDP = cartDP.ExecElementsNativeTimes(StartTime, StopTime, {'X';'Y';'Z'});
else
    cartElemsFol = calcObjFol.GetFolder('Cartesian Elems');
    try
        X = cartElemsFol.Item(['X ',displayAxes]);
        Y = cartElemsFol.Item(['Y ',displayAxes]);
        Z = cartElemsFol.Item(['Z ',displayAxes]);
    catch
        X = cartElemsFol.DuplicateComponent('X',['X ',displayAxes]);
        Y = cartElemsFol.DuplicateComponent('Y',['Y ',displayAxes]);
        Z = cartElemsFol.DuplicateComponent('Z',['Z ',displayAxes]);
    end
    X.CoordSystemName = displayAxes;
    Y.CoordSystemName = displayAxes;
    Z.CoordSystemName = displayAxes;
    XName = strrep(['X ',displayAxes],' ','_');
    YName = strrep(['Y ',displayAxes],' ','_');
    ZName = strrep(['Z ',displayAxes],' ','_');
    xyzDP = cartDP.ExecElementsNativeTimes(StartTime, StopTime, {XName;YName;ZName});
end
xyz = cell2mat(xyzDP.DataSets.ToArray);
xyz = reshape(xyz,3,n);

% Remove any empty maneuvers
Ndx = dVData(1,:) ~= 0;
dVData = dVData(:,Ndx);
xyz = xyz(:,Ndx);
[~,n] = size(dVData);

% For each maneuver create an AWB dV vector and point
for i = 1:n

        % Create or reference a dV vector and the location for the end of the maneuver 
        try
            dVVec = vec.Factory.Create(['dVVec',num2str(i)],'dV vector of a maneuver','eCrdnVectorTypeFixedInAxes');
            dVLoc = point.Factory.Create(['dV',num2str(i)],'Location for the end of the maneuver','eCrdnPointTypeFixedInSystem');
        catch
            dVVec = vec.Item(['dVVec',num2str(i)]);
            dVLoc = point.Item(['dV',num2str(i)]);
        end
        
        % Assign the dV components and the reference frame
        dVVec.ReferenceAxes.SetPath(displayAxes);
        dVVec.Direction.AssignXYZ(dVData(3,i),dVData(4,i),dVData(5,i));
        dVLoc.Reference.SetPath(displayAxes);
        dVLoc.FixedPoint.AssignCartesian(xyz(1,i),xyz(2,i),xyz(3,i));
        
        % Move the vectors and points to 3D Graphics
        try
            dVVO = sat.VO.Vector.RefCrdns.Add('eVectorElem',['Satellite/',sat.InstanceName,' ','dVVec',num2str(i),' Vector']);
            pointVO = sat.VO.Vector.RefCrdns.Add('ePointElem',['Satellite/',sat.InstanceName,' ','dV',num2str(i),' Point']);
        catch
            dVVO = sat.VO.Vector.RefCrdns.GetCrdnByName('eVectorElem',['Satellite/',sat.InstanceName,' ','dVVec',num2str(i),' Vector']);
            pointVO = sat.VO.Vector.RefCrdns.GetCrdnByName('ePointElem',['Satellite/',sat.InstanceName,' ','dV',num2str(i),' Point']);

        end
        
        % Modify Graphics Properties
        dVVO.Visible = showVec;
        if showVec == 1
            % Draw the dV vector at the correct point
            dVVO.DrawAtPoint = 1;
            dVVO.Point = ['Satellite/',satName,' ','dV',num2str(i),' Point'];
            % Various graphics properties
            dVVO.MagnitudeVisible = showVecMag;
            dVVO.LabelVisible = showVecLabel;
            if useVectorColor == 1
               dVVO.Color = hex2dec([vectorColor(5:6),vectorColor(3:4),vectorColor(1:2)]);
            end
            if showAfterOccurs == 1
                dVVO.SetDisplayStatusType('eUseIntervals');
                dVVO.DisplayTimesData.RemoveAll();
                dVVO.DisplayTimesData.Add(dVData(1,i),StopTime);
            else
                dVVO.SetDisplayStatusType('eAlwaysOn');
            end
        end

        pointVO.Visible = showPoint;
        if showPoint == 1
            % Various graphics properties
            pointVO.Size = pointSize;
            pointVO.LabelVisible = showPointLabel;
            if usePointColor == 1
               pointVO.Color = hex2dec([pointColor(5:6),pointColor(3:4),pointColor(1:2)]);
            end
            % Show Always or only after the maneuvers occur
            if showAfterOccurs == 1
                pointVO.SetDisplayStatusType('eUseIntervals');
                pointVO.DisplayTimesData.RemoveAll();
                pointVO.DisplayTimesData.Add(dVData(1,i),StopTime);
            else
                pointVO.SetDisplayStatusType('eAlwaysOn');
            end
        end
       
end

% Set vector scale and detail thresholds
sat.VO.Vector.VectorSizeScale = vectorScale;
sat.VO.Model.DetailThreshold.EnableDetailThreshold = useDetailThreshold;

% Switch back to the default Date Format
root.UnitPreferences.Item('DateFormat').SetCurrentUnit(currentUnit);
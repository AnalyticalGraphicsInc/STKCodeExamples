%% createEphemerisFile
% Description: Creates an ephemeris file for an STK Object in an Analysis
% Workbench Coordinate System. The script can also automatically create a
% new object using the new ephemeris file.

% Instructions: Have STK open, update the Input section of this script,
% then click Run in Matlab

% Examples objects and coordinate systems:
% objPath = 'Aircraft/Aircraft1';
% objPath = 'Missile/Missile1';
% objPath = 'LaunchVehicle/LaunchVehicle1';

% coordSys = 'CentralBody/Earth ICRF';
% coordSys = 'CentralBody/Moon L2';
% coordSys = 'Satellite/Satellite2 RIC';

%% Inputs
% ObjectType/ObjectName
objPath = 'Aircraft/Aircraft1';

% Coordinate System in AWB
coordSys = 'CentralBody/Earth Fixed';

% File name of the ephemeris file. Saved in the Scenario Directory
autoName = true; % true or false, Ex: 'Satellite Satellite1 CentralBody Earth ICRF.e'
fileName = 'CustomCoordSysEph.e'; % Used if autoName is set to false

% Step Size
useEphemerisSteps = true; % true or false, Use ephemeris time steps
timeStep = 60; % [sec] Used if useEphemerisSteps is set to false

% Create a new object from the new ephemeris file
createNewObjectwEph = true; % true or false

% Display the ephemeris object's trajectory in the specified coordinate system
% Only applies for a Satellite, Missile or Launch Vehicle
displayInCoordSys  = true; % true or false

%% Code
% Create an instance of STK and grab the root
app = actxGetRunningServer('STK12.application');
app.Visible = 1;
root = app.Personality2;

% Switch to Epoch Seconds Before Pulling Data
currentUnit = root.UnitPreferences.Item('DateFormat').CurrentUnit.Abbrv;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% Grab the needed handles
[objType,objName] = strtok(objPath,'/');
objName = objName(2:end);
scen = root.CurrentScenario;
if scen.Children.Contains(['e',objType],objName)
    obj = root.GetObjectFromPath(objPath);
else
    disp(['No object ',objPath,' was found.'])
    return
end

% Get the start and stop time of the object
if strcmpi(obj.ClassName,'Satellite')
    if strcmpi(obj.PropagatorType,'ePropagatorAstrogator')
        MCS = obj.Propagator.MainSequence;
        StartTime = MCS.Item(0).InitialState.Epoch; % First segment start time
        StopTime = MCS.Item(MCS.Count-1).FinalState.Epoch; % Last segment stop time
    else
        StartTime = obj.Propagator.StartTime;
        StopTime = obj.Propagator.StopTime;
    end
elseif any(strcmpi(obj.ClassName,{'Missile','LaunchVehicle'}))
        StartTime = obj.Trajectory.StartTime;
        StopTime = obj.Trajectory.StopTime;
else
    if strcmpi(obj.RouteType,'ePropagatorAviator')
        StartTime = obj.Vgt.Events.Item('EphemerisStartTime').FindOccurrence.Epoch; % First segment start time
        StopTime = obj.Vgt.Events.Item('EphemerisStopTime').FindOccurrence.Epoch; % Last segment stop time
    else
        StartTime = obj.Route.StartTime;
        StopTime = obj.Route.StopTime;
    end
end

% Grab the position and velocity over time from the data providers
pcsDP = obj.DataProviders.Item('Points Choose System');
cenDP = pcsDP.Group.Item('Center');
cenDP.PreData = coordSys;
if useEphemerisSteps == true
    DP = cenDP.ExecElementsNativeTimes(StartTime, StopTime+1, {'Time';'x';'y';'z';'Velocity x';'Velocity y';'Velocity z'});
else
    DP = cenDP.ExecElements(StartTime, StopTime, timeStep,{'Time';'x';'y';'z';'Velocity x';'Velocity y';'Velocity z'});
end
Eph = cell2mat(DP.DataSets.ToArray);
    
% Write the Ephemeris file
if autoName == true
    fileName = [strrep(objPath,'/',' '),' ',strrep(coordSys,'/',' '),'.e']; % Ex: 'Satellite Satellite1 CentralBody Earth Inertial.e'
end
scDir = root.ExecuteCommand('GetDirectory / Scenario');
fid = fopen([scDir.Item(0),'\',fileName],'w');
fprintf(fid,'stk.v.11.0\n');
fprintf(fid,'BEGIN Ephemeris\n');
fprintf(fid,['\tNumberOfEphemerisPoints		 ',num2str(length(Eph)),'\n']);
scenStartTimeUTCG = root.ConversionUtility.ConvertDate('EpSec','UTCG',num2str(scen.StartTime));
fprintf(fid,['\tScenarioEpoch		 ',scenStartTimeUTCG,'\n']);
if any(strcmpi(obj.ClassName,{'Missile','LaunchVehicle'}))
    fprintf(fid,'\tInterpolationMethod		 Lagrange\n');
    fprintf(fid,'\tInterpolationSamplesM1		 5\n');

elseif strcmpi(obj.ClassName,'Satellite')
    if strcmpi(obj.PropagatorType,'ePropagatorAstrogator')
        fprintf(fid,'\tInterpolationSamplesM1		 7\n');
    else
        fprintf(fid,'\tInterpolationSamplesM1		 5\n');
    end
else
    if strcmpi(obj.RouteType,'ePropagatorAviator')
        fprintf(fid,'\tInterpolationMethod		 Hermite\n');
    elseif strcmpi(coordSys,'CentralBody/Earth Fixed')
        fprintf(fid,'\tInterpolationMethod		 GreatArc\n');
    end
    fprintf(fid,'\tInterpolationSamplesM1		 1\n');
end
fprintf(fid,'\tDistanceUnit		 Kilometers\n');
fprintf(fid,'\tCentralBody		 Earth\n');
[typeObj,sys] = strtok(coordSys,' ');
fprintf(fid,['\tCoordinateSystem        Custom',sys,' ',typeObj,'\n']);
fprintf(fid,'\tEphemerisTimePosVel\n');
fprintf(fid,'\n');
fprintf(fid,'%10.16f %10.16f %10.16f %10.16f %10.16f %10.16f %10.16f \n',Eph');
fprintf(fid,'\n');
fprintf(fid,'END Ephemeris\n');
fclose(fid);

% Switch back to the default Date Format
root.UnitPreferences.Item('DateFormat').SetCurrentUnit(currentUnit);

% Create new object with ephemeris file
if createNewObjectwEph == true
    if root.CurrentScenario.Children.Contains(['e',objType],[objName,'_Eph'])
        objEph = root.GetObjectFromPath([objPath,'_Eph']);
    else
        objEph = root.CurrentScenario.Children.New(['e',objType],[objName,'_Eph']);
    end
    if strcmpi(obj.ClassName,'Satellite')
        objEph.SetPropagatorType('ePropagatorStkExternal');
        objEph.Propagator.Filename = [scDir.Item(0),'\',fileName];
        objEph.Propagator.Propagate();
    elseif any(strcmpi(obj.ClassName,{'Missile','LaunchVehicle'}))
        objEph.SetTrajectoryType('ePropagatorStkExternal');
        objEph.Trajectory.Filename = [scDir.Item(0),'\',fileName];
        objEph.Trajectory.Propagate();
    else
        objEph.SetRouteType('ePropagatorStkExternal');
        objEph.Route.Filename = [scDir.Item(0),'\',fileName];
        objEph.Route.Propagate();
    end
    
    % Display the object in the specified coordinate system
    if displayInCoordSys == true
        if strcmpi(obj.ClassName,'Satellite')
            objEph.VO.OrbitSystems.InertialByWindow.IsVisible = false;
            if ~objEph.VO.OrbitSystems.Contains([coordSys, ' System'])
                objEph.VO.OrbitSystems.Add([coordSys, ' System'])
            end
        elseif any(strcmpi(obj.ClassName,{'Missile','LaunchVehicle'}))
            objEph.VO.TrajectorySystems.Item(0).IsVisible = false;
            if ~objEph.VO.TrajectorySystems.Contains([coordSys, ' System'])
                objEph.VO.TrajectorySystems.Add([coordSys, ' System']);
            end
        end
    end
end


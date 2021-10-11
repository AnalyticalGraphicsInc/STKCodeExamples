close all
clear all
clc
format long g 

%%VARIABLES
attitudeFilePath = 'C:\Users\alam\Desktop\outputAttitude.a';
yawMin = -20.0;
yawMax = 20.0;

%Establish the connection
try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
catch
    % STK is not running, launch new instance
    % Launch a new instance of STK12 and grab it
    uiapp = actxserver('STK12.application');
end

%get the root from the personality
%it has two... get the second, its the newer STK Object Model Interface as
%documented in the STK Help
root = uiapp.Personality2;
% set visible to true (show STK GUI)
uiapp.visible = 1;

%%From the STK Object Root you can command every aspect of the STK GUI
%close current scenario or open new one
try
    root.CloseScenario();
    root.NewScenario('SatelliteAttitudePointing');
catch
    root.NewScenario('SatelliteAttitudePointing');
end

%set units to utcg before setting scenario time period and animation period
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');

%set scenario time period and animation period
root.CurrentScenario.SetTimePeriod('1 Jan 2013 12:00:00.000', '2 Jan 2013 12:00:00.000');

%create satellite and fac
satObj = root.CurrentScenario.Children.New('eSatellite', 'Satellite1');
facObj = root.CurrentScenario.Children.New('eFacility', 'GroundControlStation');

%propagate sat...
satObj.Propagator.InitialState.Representation.AssignClassical('eCoordinateSystemJ2000', 7183.551236, 0.001, 98.78, 87.0, 0, 0);
satObj.Propagator.Propagate;

%position facility
facObj.Position.AssignPlanetodetic(1.294624456164, 103.778554795279, 0.0);

%add a sensor to the facility that targets the satellite.  Then constrain
%the sensor to only view the satellite when it is below 45 degrees above
%the horizon
uplinkSensor = facObj.Children.New('eSensor', 'ElvContraint');
uplinkSensor.SetPatternType('eSnSimpleConic');
uplinkSensor.Pattern.ConeAngle = 5.0;
elvConstraint = uplinkSensor.AccessConstraints.AddConstraint('eCstrElevationAngle');
elvConstraint.EnableMax = 1;
elvConstraint.Max = 45.0;

%set the facility sensor to target the satellite
uplinkSensor.SetPointingType('eSnPtTargeted');
uplinkSensor.Pointing.Targets.AddObject(satObj);

%add a nadir pointing sensor to the satellite
nadirSensor = satObj.Children.New('eSensor', 'NadirPointing');
nadirSensor.SetPatternType('eSnSimpleConic');
nadirSensor.Pattern.ConeAngle = 5.0;

%set the sensor type to rectangular, define the half angles
starTracker = satObj.Children.New('eSensor', 'StarTracker');
starTracker.SetPatternType('eSnRectangular')
starTracker.Pattern.HorizontalHalfAngle = 45;
starTracker.Pattern.VerticalHalfAngle = 45;
starTracker.CommonTasks.SetPointingFixedAzEl(0, -30.0, 'eAzElAboutBoresightRotate');  

%Add the sun planet object
sunObj = root.CurrentScenario.Children.New('ePlanet', 'Sun');

%set up target pointing for the satellite
satObj.Attitude.Pointing.UseTargetPointing = 1;
satObj.Attitude.Pointing.Targets.Add([facObj.ClassName '/' facObj.InstanceName]);

%create some chains to analyze when our satellite views the ground station,
%and when our star tracker views the sun.
commChain = root.CurrentScenario.Children.New('eChain', 'CommIntervals');
commChain.Objects.AddObject(uplinkSensor);
commChain.Objects.AddObject(satObj);

sunConstraintViolated = root.CurrentScenario.Children.New('eChain', 'sunConstraintViolated');
sunConstraintViolated.Objects.AddObject(starTracker);
sunConstraintViolated.Objects.AddObject(sunObj);

root.Rewind;

%%Create a custom VGT point for the facility
%get the vgt root
vgtRoot = root.Vgt;

%get the IAgCrdnProvider for the facility
facVGT = facObj.Vgt;

%get the vgt root for the satellite
satVGT = satObj.Vgt;

%turn on the body vectors and the VVLH vectors
satVVLH = satVGT.Axes.Item('VVLH');
satBody = satVGT.Axes.Item('Body');
try
    satBodyVO = satObj.VO.vector.RefCrdns.Add('eAxesElem', satVVLH.QualifiedPath);
catch
    satBodyVO = satObj.VO.vector.RefCrdns.GetCrdnByName('eAxesElem', satVVLH.QualifiedPath);
end
try
    satVVLHVO = satObj.VO.vector.RefCrdns.Add('eAxesElem', satBody.QualifiedPath);
catch
    satVVLHVO = satObj.VO.Vector.RefCrdns.GetCrdnByName('eAxesElem', satBody.QualifiedPath);
end
satBodyVO.Visible = true;
satBodyVO.Color = 255255255;
satVVLHVO.Visible = true;
satVVLHVO.Color = 255;

%get the vgt root for the chain objects
commChainVGT = commChain.Vgt;
sunConstrVGT = sunConstraintViolated.Vgt;
commCCA = commChainVGT.EventIntervalLists.Item('CompleteChainAccessIntervals');
sunConstrCCA = sunConstrVGT.EventIntervalLists.Item('CompleteChainAccessIntervals');

%create a new interval list under the satellite object, that is the merged
%interval list showing us when we are downlinking but also pointing the
%star tracker towards the sun
starTrackerConstraintIntervalList = satVGT.EventIntervalLists.Factory.CreateEventIntervalListMerged('StarTrackerConstraintViolated', '');
starTrackerConstraintIntervalList.SetIntervalListA(commCCA);
starTrackerConstraintIntervalList.SetIntervalListB(sunConstrCCA);
starTrackerConstraintIntervalList.MergeOperation = 'eCrdnEventListMergeOperationAND';

%set units to utcg before setting scenario time period and animation period
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

%get the attitude for the satellite over the course of the mission.  We
%need to find when the Yaw value exceeds the user specified amount
vvlhDP = satObj.DataProviders.Item('Body Axes Orientation:YPR 321').Group.Item('VVLH');
yprElems = {'Time'; 'Yaw'; 'Pitch'; 'Roll'};
satStartTime = satObj.Propagator.EphemerisInterval.FindStartTime;
satStopTime = satObj.Propagator.EphemerisInterval.FindStopTime;
yprResults = vvlhDP.ExecElements(satStartTime, satStopTime, 1.0, yprElems);
time = cell2mat(yprResults.DataSets.GetDataSetByName('Time').GetValues);
yaw = cell2mat(yprResults.DataSets.GetDataSetByName('Yaw').GetValues);
pitch = cell2mat(yprResults.DataSets.GetDataSetByName('Pitch').GetValues);
roll = cell2mat(yprResults.DataSets.GetDataSetByName('Roll').GetValues);

%set units to utcg before setting scenario time period and animation period
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
scenarioStartTimeUTCG = root.CurrentScenario.AnalysisEpoch.TimeInstant;

%set up the output attitude file header
fid = fopen(attitudeFilePath, 'w+');
fprintf(fid, '%s\n\n', 'stk.v.5.0');
fprintf(fid, '%s\n\n', 'BEGIN Attitude');
fprintf(fid, '%s%s\n', 'ScenarioEpoch           ', scenarioStartTimeUTCG);
fprintf(fid, '%s%s\n', 'NumberOfAttitudePoints  ', num2str(size(time, 1)));
fprintf(fid, '%s\n', 'BlockingFactor          20');
fprintf(fid, '%s\n', 'InterpolationOrder      1');
fprintf(fid, '%s\n', 'CentralBody             Earth');
fprintf(fid, '%s%s\n', 'CoordinateAxes          Custom VVLH Satellite/', satObj.InstanceName);
fprintf(fid, '%s\n\n', 'Sequence                321');
fprintf(fid, '%s\n\n', 'AttitudeTimeYPRAngles');

%Add all of the attitude data to the file, constraining yaw values as
%specified by the user
for i=1:length(time)
    iYaw = yaw(i);
    if(iYaw < yawMin)
        iYaw = yawMin;
    elseif(iYaw > yawMax)
        iYaw = yawMax;
    end
    %fprintf(fid, '%s\t%s\t%s\t%s\n', num2str(time(i)), num2str(iYaw), num2str(pitch(i)), num2str(roll(i)));
    fprintf(fid, '%s\t%s\t%s\t%s\n', num2str(time(i)), num2str(iYaw), num2str(pitch(i)), num2str(roll(i)));
end

%close the attitude file
fprintf(fid, '\n%s', 'END Attitude');
fclose(fid);

%clone the satellite object and add the attitude file to the cloned object
newSatObj = satObj.CopyObject([satObj.InstanceName '_Constrained']);
newSatObj.Attitude.Pointing.UseTargetPointing = false;
newSatObj.Attitude.External.Load(attitudeFilePath);

%turn off the visualization of the original satellite
satObj.Graphics.Attributes.IsVisible = 0;

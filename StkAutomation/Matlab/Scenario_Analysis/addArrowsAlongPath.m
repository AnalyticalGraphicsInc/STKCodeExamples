function addArrowsAlongPath(objectPath, numArrows, arrowLegLength_mi, arrowLegAngle, arrowLineWidth, clearExistingPrimitives, colorInput)

% This function enables you to draw equally-spaced arrows along
% the route of an object to represent its direction of travel. 3D graphics
% primitives are used to draw the arrows.
% 
% NOTE: This script only works with Earth-bound objects (i.e. aircraft,
% ground vehicles, ships, and missiles)
%
%%%%%%%%%%  INPUTS  %%%%%%%%%%
%
% objectPath (char array): Specify object path of vehicle 
% numArrows (int): Set number of equally-spaced arrows along the path
% arrowLegLength_mi (int/double): Set leg length for each arrow in miles
% arrowLegAngle (int/double): Set angle between arrow leg and neg. direction vector
% arrowLineWidth (int): Set arrow line width (1-10)
% clearExistingPrimitives (bool): Do you want to clear any existing primitives?
% [optional] colorInput (1x6 char array): RGB hex color code for the arrows
% --->  When colorInput is left blank the function will use the existing
%       color of the object
%
% EXAMPLE:
% addDirectionalArrows('Aircraft/testAircraft',10,20,45,3,true)
% addDirectionalArrows('Aircraft/testAircraft',10,20,45,3,true,'FFFF00')

%%%%%%%%%%  BEGIN FUNCTION  %%%%%%%%%%

% Grab an existing instance of STK
uiApplication = actxGetRunningServer('STK12.application');
root = uiApplication.Personality2;

% Set unit preferences
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
root.UnitPreferences.Item('Distance').SetCurrentUnit('mi');

% Get a handle on object
object = root.GetObjectFromPath(objectPath);

% Get stop time of object and max distance object travels
objectVgt = object.Vgt;
objectStopTime = objectVgt.Events.Item('EphemerisStopTime').FindOccurrence().Epoch;
objectMaxDistance = objectVgt.CalcScalars.Item('DistanceAlongTrajectory.Max').Evaluate(objectStopTime).Value;

% Determine distance spacing for arrows
distanceSpacing = objectMaxDistance/(numArrows+1);

% Create array of distances along path to place arrows
distanceThresholds_mi = (distanceSpacing:distanceSpacing:distanceSpacing*numArrows)';
distanceThresholds_meters = distanceThresholds_mi*1609.34;

% Determine times where arrows should be placed using a Condition Set
conditionSetName = 'distanceThresholds';
% Delete condition set if it already exists
if objectVgt.ConditionSets.Contains(conditionSetName)
    objectVgt.ConditionSets.Remove(conditionSetName);
end
% Create a distance thresholds Condition Set in AWB
root.ExecuteCommand(['CalculationTool * ', objectPath, ' Create "Condition Set" ', conditionSetName, ' "Scalar Thresholds" Scalar "', objectPath, ' DistanceAlongTrajectory"']);
distanceThresholdsAwb = objectVgt.ConditionSets.Item(conditionSetName);
distanceThresholdsAwb.IncludeBelowLowestThreshold = 1;
distanceThresholdsAwb.IncludeAboveHighestThreshold = 0;
thresholdLabels = cell(numArrows,1);
for i = 1:numArrows
    thresholdLabels{i} = (['Distance',num2str(i)]);
end
distanceThresholdsAwb.SetThresholdsAndLabels(num2cell(distanceThresholds_meters),thresholdLabels); % despite set units, only seems to input values in meters

% Get times when distance thresholds are crossed
distanceThresholdsAwbIntervals = objectVgt.EventIntervalCollections.Item([conditionSetName, '.SatisfactionIntervals']);
intervalCollections = distanceThresholdsAwbIntervals.FindIntervalCollection().IntervalCollections;
times = zeros(numArrows,1);
for j = 0:numArrows-1
    times(j+1) = intervalCollections.Item(j).Item(0).Stop;
end

% Create two arrow endpoints using AWB
% Delete points if they already exist
if objectVgt.Points.Contains('ArrowPointLeftSide')
    objectVgt.Points.Remove('ArrowPointLeftSide');
end
if objectVgt.Points.Contains('ArrowPointRightSide')
    objectVgt.Points.Remove('ArrowPointRightSide');
end
% Create new points
leftSidePoint = objectVgt.Points.Factory.Create('ArrowPointLeftSide', 'Left side end point for drawing a directional arrow along the objects path.', 'eCrdnPointTypeFixedInSystem');
rightSidePoint = objectVgt.Points.Factory.Create('ArrowPointRightSide', 'Right side end point for drawing a directional arrow along the objects path.', 'eCrdnPointTypeFixedInSystem');
leftSidePoint.FixedPoint.AssignSpherical(0,-180+arrowLegAngle,arrowLegLength_mi);
rightSidePoint.FixedPoint.AssignSpherical(0,180-arrowLegAngle,arrowLegLength_mi);

% Get position of arrow endpoints at threshold times
elems = { 'Detic Latitude'; 'Detic Longitude'; 'Detic Altitude' };
leftPointDp = object.DataProviders.Item('Points(Fixed)').Group.Item('ArrowPointLeftSide').ExecSingleElementsArray(num2cell(times),elems);
rightPointDp = object.DataProviders.Item('Points(Fixed)').Group.Item('ArrowPointRightSide').ExecSingleElementsArray(num2cell(times),elems);
leftPointLat = cell2mat(leftPointDp.GetArray(cast(0,'int32')));
leftPointLon = cell2mat(leftPointDp.GetArray(cast(1,'int32')));
leftPointAlt = cell2mat(leftPointDp.GetArray(cast(2,'int32')));
rightPointLat = cell2mat(rightPointDp.GetArray(cast(0,'int32')));
rightPointLon = cell2mat(rightPointDp.GetArray(cast(1,'int32')));
rightPointAlt = cell2mat(rightPointDp.GetArray(cast(2,'int32')));

% Determine position of object at threshold times
latitude_AwbScalar = objectVgt.CalcScalars.Item('Trajectory(Cartographic).Detic.LLA.Latitude');
longitude_AwbScalar = objectVgt.CalcScalars.Item('Trajectory(Cartographic).Detic.LLA.Longitude');
altitude_AwbScalar = objectVgt.CalcScalars.Item('Trajectory(Cartographic).Detic.LLA.Altitude');
latitude_AwbQuickEvalArray = latitude_AwbScalar.QuickEvaluateArray(num2cell(times)); % returns a cell array of cells. Each cell contains two elements
longitude_AwbQuickEvalArray = longitude_AwbScalar.QuickEvaluateArray(num2cell(times)); % returns a cell array of cells. Each cell contains two elements
altitude_AwbQuickEvalArray = altitude_AwbScalar.QuickEvaluateArray(num2cell(times)); % returns a cell array of cells. Each cell contains two elements

% Extract LLA values for object
objectLat = cellfun(@(x)x{2},latitude_AwbQuickEvalArray);
objectLon = cellfun(@(x)x{2},longitude_AwbQuickEvalArray);
objectAlt = cellfun(@(x)x{2},altitude_AwbQuickEvalArray);

% Use Connect command to draw arrows (persists with scenario reopen)
if nargin < 7
    objectColor = object.Graphics.Attributes.Color;
    % STK returns color decimal value as if colors were specified as BGR.
    % Primitive Connect command below expects it in RGB. Need to flip.
    stkFlippedHexColor = FlipStkColorCode(objectColor);
else
    stkFlippedHexColor = colorInput;
end
if clearExistingPrimitives == true
    root.ExecuteCommand('VO * Primitive Delete ID All');
end
for k = 1:numArrows
    root.ExecuteCommand(['VO_R * Primitive Add ID Auto Type Line Color #', stkFlippedHexColor,' LineWidth ', num2str(arrowLineWidth),' DrawType Space Points 3 LLA ', num2str(leftPointLat(k)),' ', num2str(leftPointLon(k)),' ', num2str(leftPointAlt(k)*1609.34),' ', num2str(objectLat(k)), ' ', num2str(objectLon(k)),' ', num2str(objectAlt(k)*1609.34),' ', num2str(rightPointLat(k)),' ', num2str(rightPointLon(k)),' ', num2str(rightPointAlt(k)*1609.34),'']);
end

end

function stkFlippedHexColor = FlipStkColorCode( stkColorCode )
    % Convert the color decimal code to hex:
    hex = dec2hex(stkColorCode,6);
    
    % Rearrange to the appropriate order:
    B = hex(1:2);
    G = hex(3:4);
    R = hex(5:6);
    
    % Return the flipped hex vector:  
    stkFlippedHexColor = [R G B];
end

% Inputs
sensPath = 'Satellite/Satellite1/Sensor/Sensor1';   % Must be an EOIR sensor
bandIndex = 0;  % Band 1
evalTime = '8 Dec 2022 19:00:00.000';

% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK12.Application');

% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2;

% Get relevant objects
scenario = root.CurrentScenario;
sensor = root.GetObjectFromPath(sensPath);

% Gather info EOIR sensor pixel info
numColumns = sensor.Pattern.Bands.Item(int32(bandIndex)).HorizontalPixels;
numRows = sensor.Pattern.Bands.Item(int32(bandIndex)).VerticalPixels;

% Create and configure dummy sensor
parentObj = sensor.Parent;
dummySensor = parentObj.Children.New('eSensor', 'dummySensor');
dummySensor.SetPatternType('eSnRectangular');
% Note that horizontal/vertical definitions are switched between EOIR and
% rectangular sensor patterns
dummySensor.Pattern.HorizontalHalfAngle = sensor.Pattern.Bands.Item(int32(bandIndex)).VerticalHalfAngle;
dummySensor.Pattern.VerticalHalfAngle = sensor.Pattern.Bands.Item(int32(bandIndex)).HorizontalHalfAngle;

% Get sensor pattern intersections
elems = {'Latitude'; 'Longitude'; 'Altitude'};
patternDPResult = dummySensor.DataProviders.Item('Pattern Intersection Corners').ExecSingleElements(evalTime, elems);
patternCorners = cell2mat(patternDPResult.DataSets.ToArray());

% Create points for 3 pattern corners
point1 = dummySensor.Vgt.Points.Factory.CreatePointFixedOnCentralBody('intersect1', '', patternCorners(1, 2), patternCorners(1, 1), patternCorners(1, 3), 'eCrdnReferenceShapeEllipsoid');
point1.Position.AssignGeodetic(patternCorners(1, 1), patternCorners(1, 2), patternCorners(1, 3));

point2 = dummySensor.Vgt.Points.Factory.CreatePointFixedOnCentralBody('intersect2', '', patternCorners(2, 2), patternCorners(2, 1), patternCorners(2, 3), 'eCrdnReferenceShapeEllipsoid');
point2.Position.AssignGeodetic(patternCorners(2, 1), patternCorners(2, 2), patternCorners(2, 3));

point3 = dummySensor.Vgt.Points.Factory.CreatePointFixedOnCentralBody('intersect3', '', patternCorners(3, 2), patternCorners(3, 1), patternCorners(3, 3), 'eCrdnReferenceShapeEllipsoid');
point3.Position.AssignGeodetic(patternCorners(3, 1), patternCorners(3, 2), patternCorners(3, 3));

% Creat displacement vectors for each point
sensCenter = dummySensor.Vgt.Points.Item('Center');

vec1 = dummySensor.Vgt.Vectors.Factory.CreateDisplacementVector('vec1', sensCenter, point1);
vec1.Origin.SetPoint(sensCenter);
vec1.Destination.SetPoint(point1);

vec2 = dummySensor.Vgt.Vectors.Factory.CreateDisplacementVector('vec2', sensCenter, point2);
vec2.Origin.SetPoint(sensCenter);
vec2.Destination.SetPoint(point2);

vec3 = dummySensor.Vgt.Vectors.Factory.CreateDisplacementVector('vec3', sensCenter, point3);
vec3.Origin.SetPoint(sensCenter);
vec3.Destination.SetPoint(point3);

% Get unit vectors components with respect to body
elems = {'x/Magnitude'; 'y/Magnitude'; 'z/Magnitude'};
vec1DPResult = dummySensor.DataProviders.Item('Vectors(Body)').Group.Item('vec1').ExecSingleElements(evalTime, elems);
vec1Unit = cell2mat(vec1DPResult.DataSets.ToArray());

vec2DPResult = dummySensor.DataProviders.Item('Vectors(Body)').Group.Item('vec2').ExecSingleElements(evalTime, elems);
vec2Unit = cell2mat(vec2DPResult.DataSets.ToArray());

vec3DPResult = dummySensor.DataProviders.Item('Vectors(Body)').Group.Item('vec3').ExecSingleElements(evalTime, elems);
vec3Unit = cell2mat(vec3DPResult.DataSets.ToArray());

% Find y component difference between vecs 2 and 3 for horizontal pixels
% and define angular distance between each pixel
colSpan = vec3Unit(2) - vec2Unit(2);
colStep = colSpan/numColumns;

% Find x component difference btween vecs 1 and 2 for vertical pixels and
% define angular distance between each pixel
rowSpan = vec2Unit(1) - vec1Unit(1);
rowStep = rowSpan/numRows;

% z is constant
z = vec1Unit(3);

% Set up vector to iterate over and find CB intersection
iterVec = dummySensor.Vgt.Vectors.Factory.Create('iterVector', '', 'eCrdnVectorTypeFixedInAxes');
iterVecIntersect = dummySensor.Vgt.Points.Factory.Create('iterVecIntersect', '', 'eCrdnPointTypeCentralBodyIntersect');
iterVecIntersect.DirectionVector = iterVec;

% Optional, if you want visualize progress in the 3D graphics window
% dummySensor.VO.Vector.RefCrdns.Add('ePointElem', 'Satellite/Satellite1/Sensor/dummySensor iterVecIntersect');

% Intialize loop to iterate from left to right, top to bottom
pixelLatLon = {};
elems = {'Detic Latitude'; 'Detic Longitude'};
rowCount = 1;
for x = vec2Unit(1)-rowStep/2:-rowStep:vec1Unit(1)
    disp(['Processing row ' int2str(rowCount) '...']);
    row = {};
    for y = vec2Unit(2)+colStep/2:colStep:vec3Unit(2)
        % Compute lat/lon for pixel in row
        iterVec.Direction.AssignXYZ(x, y, z);
        intersectDPResults = dummySensor.DataProviders.Item('Points(Fixed)').Group.Item('iterVecIntersect').ExecSingleElements(evalTime, elems);
        row = [row cell2mat(intersectDPResults.DataSets.ToArray())];
    end
    rowCount = rowCount + 1;
    pixelLatLon = [pixelLatLon; row];
end

% Clean up
dummySensor.Unload();
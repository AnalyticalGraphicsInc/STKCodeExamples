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

% Gather info EOIR sensor pixel and FOV info
numColumns = sensor.Pattern.Bands.Item(int32(bandIndex)).HorizontalPixels;
numRows = sensor.Pattern.Bands.Item(int32(bandIndex)).VerticalPixels;
vertHA = sensor.Pattern.Bands.Item(int32(bandIndex)).VerticalHalfAngle;
horzHA = sensor.Pattern.Bands.Item(int32(bandIndex)).HorizontalHalfAngle;

% Create vectors representing each side of the sensor FOV
sensBodyAxes = sensor.Vgt.Axes.Item('Body');

try
    leftVec = sensor.Vgt.Vectors.Factory.Create('leftVec', '', 'eCrdnVectorTypeFixedInAxes');
catch
    leftVec = sensor.Vgt.Vectors.Item('leftVec');
end
leftVec.Direction.AssignEuler(horzHA, 0, 'e12');
leftVec.ReferenceAxes.SetAxes(sensBodyAxes);

try
    rightVec = sensor.Vgt.Vectors.Factory.Create('rightVec', '', 'eCrdnVectorTypeFixedInAxes');
catch
    rightVec = sensor.Vgt.Vectors.Item('rightVec');
end
rightVec.Direction.AssignEuler(-horzHA, 0, 'e12');
rightVec.ReferenceAxes.SetAxes(sensBodyAxes);

try
    topVec = sensor.Vgt.Vectors.Factory.Create('topVec', '', 'eCrdnVectorTypeFixedInAxes');
catch
    topVec = sensor.Vgt.Vectors.Item('topVec');
end
topVec.Direction.AssignEuler(0, vertHA, 'e12');
topVec.ReferenceAxes.SetAxes(sensBodyAxes);

try
    bottomVec = sensor.Vgt.Vectors.Factory.Create('bottomVec', '', 'eCrdnVectorTypeFixedInAxes');
catch
    bottomVec = sensor.Vgt.Vectors.Item('bottomVec');
end
bottomVec.Direction.AssignEuler(0, -vertHA, 'e12');
bottomVec.ReferenceAxes.SetAxes(sensBodyAxes);

% Create planes representing each edge of sensor FOV
sensCenter = sensor.Vgt.Points.Item('Center');
sensBodyX = sensBodyAxes.EmbeddedComponents.Item('Body.X');
sensBodyY = sensBodyAxes.EmbeddedComponents.Item('Body.Y');

try
    leftPlane = sensor.Vgt.Planes.Factory.Create('leftPlane', '', 'eCrdnPlaneTypeTwoVector');
catch
    leftPlane = sensor.Vgt.Planes.Item('leftPlane');
end
leftPlane.ReferencePoint.SetPoint(sensCenter);
leftPlane.ReferenceVector.SetVector(leftVec);
leftPlane.Vector2.SetVector(sensBodyX);

try
    rightPlane = sensor.Vgt.Planes.Factory.Create('rightPlane', '', 'eCrdnPlaneTypeTwoVector');
catch
    rightPlane = sensor.Vgt.Planes.Item('rightPlane');
end
rightPlane.ReferencePoint.SetPoint(sensCenter);
rightPlane.ReferenceVector.SetVector(rightVec);
rightPlane.Vector2.SetVector(sensBodyX);

try
    topPlane = sensor.Vgt.Planes.Factory.Create('topPlane', '', 'eCrdnPlaneTypeTwoVector');
catch
    topPlane = sensor.Vgt.Planes.Item('topPlane');
end
topPlane.ReferencePoint.SetPoint(sensCenter);
topPlane.ReferenceVector.SetVector(topVec);
topPlane.Vector2.SetVector(sensBodyY);

try
    bottomPlane = sensor.Vgt.Planes.Factory.Create('bottomPlane', '', 'eCrdnPlaneTypeTwoVector');
catch
    bottomPlane = sensor.Vgt.Planes.Item('bottomPlane');
end
bottomPlane.ReferencePoint.SetPoint(sensCenter);
bottomPlane.ReferenceVector.SetVector(bottomVec);
bottomPlane.Vector2.SetVector(sensBodyY);

% Creat plane intersection vectors for three corners
try
    bottomLeftVec = sensor.Vgt.Vectors.Factory.Create('bottomLeftVec', '', 'eCrdnVectorTypeTwoPlanesIntersection');
catch
    bottomLeftVec = sensor.Vgt.Vectors.Item('bottomLeftVec');
end
bottomLeftVec.PlaneA.SetPlane(leftPlane);
bottomLeftVec.PlaneB.SetPlane(bottomPlane);

try
    topLeftVec = sensor.Vgt.Vectors.Factory.Create('topLeftVec', '', 'eCrdnVectorTypeTwoPlanesIntersection');
catch
    topLeftVec = sensor.Vgt.Vectors.Item('topLeftVec');
end
topLeftVec.PlaneA.SetPlane(leftPlane);
topLeftVec.PlaneB.SetPlane(topPlane);

try
    topRightVec = sensor.Vgt.Vectors.Factory.Create('topRightVec', '', 'eCrdnVectorTypeTwoPlanesIntersection');
catch
    topRightVec = sensor.Vgt.Vectors.Item('topRightVec');
end
topRightVec.PlaneA.SetPlane(rightPlane);
topRightVec.PlaneB.SetPlane(topPlane);

% Get unit vectors components with respect to body
elems = {'x/Magnitude'; 'y/Magnitude'; 'z/Magnitude'};
bottomLeftVecDPResult = sensor.DataProviders.Item('Vectors(Body)').Group.Item('bottomLeftVec').ExecSingleElements(evalTime, elems);
bottomleftUnitVec = cell2mat(bottomLeftVecDPResult.DataSets.ToArray());

topLeftVecDPResult = sensor.DataProviders.Item('Vectors(Body)').Group.Item('topLeftVec').ExecSingleElements(evalTime, elems);
topLeftUnitVec = cell2mat(topLeftVecDPResult.DataSets.ToArray());

topRightVecDPResult = sensor.DataProviders.Item('Vectors(Body)').Group.Item('topRightVec').ExecSingleElements(evalTime, elems);
topRightUnitVec = cell2mat(topRightVecDPResult.DataSets.ToArray());

% Find y component difference between vecs 2 and 3 for horizontal pixels
% and define angular distance between each pixel
colSpan = topRightUnitVec(2) - topLeftUnitVec(2);
colStep = colSpan/numColumns;

% Find x component difference btween vecs 1 and 2 for vertical pixels and
% define angular distance between each pixel
rowSpan = topLeftUnitVec(1) - bottomleftUnitVec(1);
rowStep = rowSpan/numRows;

% z is constant
z = bottomleftUnitVec(3);

% Set up vector to iterate over
try
    iterVec = sensor.Vgt.Vectors.Factory.Create('iterVec', '', 'eCrdnVectorTypeFixedInAxes');
catch
    iterVec = sensor.Vgt.Vectors.Item('iterVec');
end

% Set up CB intersection point for iterVec
try
    intersectPoint = sensor.Vgt.Points.Factory.Create('intersectPoint', '', 'eCrdnPointTypeCentralBodyIntersect');
catch
    intersectPoint = sensor.Vgt.Points.Item('intersectPoint');
end
intersectPoint.DirectionVector = iterVec;

% Set up plane normal to iterVec
try
    normalPlane = sensor.Vgt.Planes.Factory.Create('normalPlane', '', 'eCrdnPlaneTypeNormal');
catch
    normalPlane = sensor.Vgt.Planes.Item('normalPlane');
end
normalPlane.NormalVector.SetVector(iterVec);
normalPlane.ReferencePoint.SetPoint(root.CentralBodies.Earth.Vgt.Points.Item('Center'));

% Find intersection of iterVec with plane for min altitude point
try
    planePoint = sensor.Vgt.Points.Factory.Create('planePoint', '', 'eCrdnPointTypePlaneIntersection');
catch
    planePoint = sensor.Vgt.Points.Item('planePoint');
end
planePoint.DirectionVector.SetVector(iterVec);
planePoint.OriginPoint.SetPoint(sensCenter);
planePoint.ReferencePlane.SetPlane(normalPlane);

% Optional, if you want visualize progress in the 3D graphics window
sensor.VO.Vector.RefCrdns.Add('eVectorElem', 'Satellite/Satellite1/Sensor/Sensor1 iterVec');

% Intialize loop to iterate from left to right, top to bottom
pixelLatLon = {};
elems = {'Detic Latitude'; 'Detic Longitude'; 'Detic Altitude'};
rowCount = 1;
for x = topLeftUnitVec(1)-rowStep/2:-rowStep:bottomleftUnitVec(1)
    disp(['Processing row ' int2str(rowCount) '...']);
    row = {};
    for y = topLeftUnitVec(2)+colStep/2:colStep:topRightUnitVec(2)
        % Compute lat/lon for pixel in row
        iterVec.Direction.AssignXYZ(x, y, z);
        intersectDPResults = sensor.DataProviders.Item('Points(Fixed)').Group.Item('intersectPoint').ExecSingleElements(evalTime, elems);
        intersectLatLon = cell2mat(intersectDPResults.DataSets.ToArray());
        if ~isempty(intersectLatLon)
            % CB intersection exists
            row = [row intersectLatLon];
        else
            % CB intersection does not exist, so use lat/lon that minimizes
            % altitude instead
            intersectDPResults = sensor.DataProviders.Item('Points(Fixed)').Group.Item('planePoint').ExecSingleElements(evalTime, elems);
            row = [row cell2mat(intersectDPResults.DataSets.ToArray())];
        end
    end
    rowCount = rowCount + 1;
    pixelLatLon = [pixelLatLon; row];
end

disp('Done!');
disp(pixelLatLon);
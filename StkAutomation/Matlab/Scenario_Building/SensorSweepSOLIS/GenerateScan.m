function ll = GenerateScan(root,atName,scanSpeed,scanRes,scanDir,useBoundingRectangle)
% Example inputs:
% root = app.personality2; % app = actxGetRunningServer('STK12.application');
% atName = 'United_States';
% scanSpeed = 20;% km/s
% scanRes = 300; % km
% scanDir = 'LR'; % LR or TB, left to right or top to bottom

% outputs: A list of lat lon points for the scan
if nargin == 5
    useBoundingRectangle = true;
end

%% Code

at = root.GetObjectFromPath(['AreaTarget/',atName]);

% Get Boundary Points and Rectangle
if useBoundingRectangle == true % Bounding rectangle orientation will not coinside with east-west and north-south. May have issues with very high latitude targets
    br = at.DataProviders.Item('Bounding Rectangle');
    brcp = br.Group.Item('Corner Points');
    elems = {'Geodetic-Lat';'Geodetic-Lon'};
    corners = brcp.ExecElements(elems);
    corners = cell2mat(corners.DataSets.ToArray);
    % Put corners in order. bl -> tl -> tr -> br -> bl
    [~,ind] = sort(corners(:,1));
    corners = corners(ind,:);
    [~,ind] = sort(corners(1:2,2));
    corners(1:2,:) = corners(ind,:);
    [~,ind] = sort(corners(1:2,2));
    corners(1:2,:) = corners(ind,:);
    [~,ind] = sort(corners(3:4,2));
    corners(3:4,:) = corners(ind+2,:);
    corners = corners([1,3,4,2],:);
    corners(end+1,:) = corners(1,:);
    
    % Measure distance using sides along a great arc
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(1,1)),' ',num2str(corners(1,2)),' ',num2str(corners(2,1)),' ',num2str(corners(2,2))];
    blTotl = root.ExecuteCommand(cmd);    
    blTotl = str2num(blTotl.Item(0))/1000;
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(2,1)),' ',num2str(corners(2,2)),' ',num2str(corners(3,1)),' ',num2str(corners(3,2))];
    tlTotr = root.ExecuteCommand(cmd);    
    tlTotr = str2num(tlTotr.Item(0))/1000;
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(3,1)),' ',num2str(corners(3,2)),' ',num2str(corners(4,1)),' ',num2str(corners(4,2))];
    trTobr = root.ExecuteCommand(cmd);    
    trTobr = str2num(trTobr.Item(0))/1000;
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(4,1)),' ',num2str(corners(4,2)),' ',num2str(corners(5,1)),' ',num2str(corners(5,2))];
    brTobl = root.ExecuteCommand(cmd);    
    brTobl = str2num(brTobl.Item(0))/1000;
    dists = [blTotl,tlTotr ,trTobr,brTobl];

    % Determine pass number and side length
    passNum = ceil(dists/scanRes);
    TBpass = max(passNum([2,4]));
    LRpass = max(passNum([1,3]));

    % Get equally spaced lat/lon points along the bounding rectangle sides
    % Left Side
    lat1 = corners(1,1);
    lon1 = corners(1,2);
    lat2 = corners(2,1);
    lon2 = corners(2,2);
    [b] = Bearing(lat1,lon1,lat2,lon2);
    scanRes = max(dists([1,3])/LRpass);
    latsL = zeros(LRpass,1);
    lonsL = zeros(LRpass,1);
    for i = 1:length(latsL)
        [latsL(i),lonsL(i)] = LatLonNewPoint(lat1,lon1,scanRes*(i-0.5),b); % the 0.5*scanRes is to move the scan passes off of the edge
    end

    % Top Side
    lat1 = corners(2,1);
    lon1 = corners(2,2);
    lat2 = corners(3,1);
    lon2 = corners(3,2);
    [b] = Bearing(lat1,lon1,lat2,lon2);
    scanRes = max(dists([2,4])/TBpass);
    latsT = zeros(TBpass,1);
    lonsT = zeros(TBpass,1);
    for i = 1:length(latsT)
        [latsT(i),lonsT(i)] = LatLonNewPoint(lat1,lon1,scanRes*(i-0.5),b);
    end

    % Right Side
    lat1 = corners(3,1);
    lon1 = corners(3,2);
    lat2 = corners(4,1);
    lon2 = corners(4,2);
    [b] = Bearing(lat1,lon1,lat2,lon2);
    scanRes = max(dists([1,3])/LRpass);
    latsR = zeros(LRpass,1);
    lonsR = zeros(LRpass,1);
    for i = 1:length(latsR)
        [latsR(i),lonsR(i)] = LatLonNewPoint(lat1,lon1,scanRes*(i-0.5),b);
    end

    % Bottom Side
    lat1 = corners(4,1);
    lon1 = corners(4,2);
    lat2 = corners(5,1);
    lon2 = corners(5,2);
    [b] = Bearing(lat1,lon1,lat2,lon2);
    scanRes = max(dists([2,4])/TBpass);
    latsB = zeros(TBpass,1);
    lonsB = zeros(TBpass,1);
    for i = 1:length(latsB)
        [latsB(i),lonsB(i)] = LatLonNewPoint(lat1,lon1,scanRes*(i-0.5),b);
    end

else % use the min and max of the bouning points, this will coinside with east-west and north-south, but it may miss sections of the area target for large area targets or high latitude targets
    bp = at.DataProviders.Item('Boundary Points');
    elems = {'Geodetic-Lat';'Geodetic-Lon'};
    points = bp.ExecElements(elems);
    points = cell2mat(points.DataSets.ToArray);
    % Minimum of the Bounding Points
    corners = [];
    padding = 2; % Sometimes need lat/lon padding to not missed edges of the area target
    corners(1,1) = min(points(:,1))-padding;
    corners(1,2) = min(points(:,2))-padding;
    corners(2,1) = max(points(:,1))+padding;
    corners(2,2) = min(points(:,2))-padding;
    corners(3,1) = max(points(:,1))+padding;
    corners(3,2) = max(points(:,2))+padding;
    corners(4,1) = min(points(:,1))-padding;
    corners(4,2) = max(points(:,2))+padding;
    corners(end+1,:) = corners(1,:);
    
    % Measure distance using sides along a great arc
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(1,1)),' ',num2str(corners(1,2)),' ',num2str(corners(2,1)),' ',num2str(corners(2,2))];
    blTotl = root.ExecuteCommand(cmd);    
    blTotl = str2num(blTotl.Item(0))/1000;
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(2,1)),' ',num2str(corners(2,2)),' ',num2str(corners(3,1)),' ',num2str(corners(3,2))];
    tlTotr = root.ExecuteCommand(cmd);    
    tlTotr = str2num(tlTotr.Item(0))/1000;
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(3,1)),' ',num2str(corners(3,2)),' ',num2str(corners(4,1)),' ',num2str(corners(4,2))];
    trTobr = root.ExecuteCommand(cmd);    
    trTobr = str2num(trTobr.Item(0))/1000;
    cmd = ['MeasureSurfaceDistance * ',num2str(corners(4,1)),' ',num2str(corners(4,2)),' ',num2str(corners(5,1)),' ',num2str(corners(5,2))];
    brTobl = root.ExecuteCommand(cmd);    
    brTobl = str2num(brTobl.Item(0))/1000;
    dists = [blTotl,tlTotr ,trTobr,brTobl];

    % Determine pass number and side length
    passNum = ceil(dists/scanRes);
    TBpass = max(passNum([2,4]));
    LRpass = max(passNum([1,3]));
    
    % Left,Top,Right and Bottom lat/lons, not equally necessarily spaced in distance
    latsL = linspace(corners(1,1),corners(2,1),LRpass);
    lonsL = linspace(corners(1,2),corners(2,2),LRpass);
    latsT = linspace(corners(2,1),corners(3,1),TBpass);
    lonsT = linspace(corners(2,2),corners(3,2),TBpass);
    latsR = linspace(corners(3,1),corners(4,1),LRpass);
    lonsR = linspace(corners(3,2),corners(4,2),LRpass);
    latsB = linspace(corners(4,1),corners(5,1),TBpass);
    lonsB = linspace(corners(4,2),corners(5,2),TBpass);
end

% Reverse the direction of the right and bottom
latsR = latsR(end:-1:1);
lonsR = lonsR(end:-1:1);
latsB = latsB(end:-1:1);
lonsB = lonsB(end:-1:1);

% Pick LR or TB lla points, rearange to get the desired search pattern
if strcmp(scanDir,'LR')
    ll = zeros(length(latsL)*2,3);
    indL = sort([4:4:length(latsL)*2,1:4:length(latsL)*2]);
    indR = sort([3:4:length(latsL)*2,2:4:length(latsL)*2]);
    ll(indL,1) = latsL;
    ll(indL,2) = lonsL;
    ll(indR ,1) = latsR;
    ll(indR ,2) = lonsR;
else 
    ll = zeros(length(latsT)*2,3);
    indB = sort([4:4:length(latsT)*2,1:4:length(latsT)*2]);
    indT = sort([3:4:length(latsT)*2,2:4:length(latsT)*2]);
    ll(indB,1) = latsB;
    ll(indB,2) = lonsB;
    ll(indT ,1) = latsT;
    ll(indT ,2) = lonsT;
end 


% Add in larger scan as a 1st guess. This will be refined by access times
root.BeginUpdate();
% Add the scan points to the ground scan
scanName = 'ScanGuess';
if root.CurrentScenario.Children.Contains('eGroundVehicle',scanName)
    scan = root.GetObjectFromPath(['GroundVehicle/',scanName]);
else
    scan = root.CurrentScenario.Children.New('eGroundVehicle',scanName);
end

scan.SetRouteType('ePropagatorGreatArc');
scan.Route.SetAltitudeRefType('eWayPtAltRefWGS84')
scan.Route.Waypoints.RemoveAll();
% lla(:,2) = wrapTo180(lla(:,2));
for i = 1:size(ll)
    wp = scan.Route.Waypoints.Add();
    wp.Latitude = ll(i,1);
    wp.Longitude = ll(i,2);
    wp.Altitude = ll(i,3); % currently zeros for altitude
    wp.TurnRadius = 0;
    wp.Speed = scanSpeed;
end
scan.Route.Propagate();

% if at.AccessConstraints.IsConstraintActive('eCstrElevationAngle')
%     at.AccessConstraints.RemoveConstraint('eCstrElevationAngle')
% end
% el = at.AccessConstraints.AddConstraint('eCstrElevationAngle');
% el.Angle = 90;

% Refine Scan based on Access Times
acc = scan.GetAccessToObject(at);
acc.ComputeAccess();
accDP = acc.DataProviders.Item('Access Data');
accesses = accDP.ExecElements(root.CurrentScenario.StartTime,root.CurrentScenario.StopTime,{'Start Time';'Stop Time'});
accesses = accesses.DataSets.ToArray;
accesses2 = cell(length(accesses)*2,1);
accesses2(1:2:end) = accesses(:,1);
accesses2(2:2:end) = accesses(:,2);
llaDP = scan.DataProviders.Item('LLA State').Group.Item('Fixed');
newlla = llaDP.ExecSingleElementsArray(accesses2,{'Lat';'Lon'});
newLat = cell2mat( newlla.GetArray(int32(0)));
newLon = cell2mat( newlla.GetArray(int32(1)));
acc.RemoveAccess();

ll = [newLat,newLon];

% % Points are added in as part of SensorSweepwSOLIS.m 
% scan.Route.Waypoints.RemoveAll();
% for i = 1:size(ll)
%     wp = scan.Route.Waypoints.Add();
%     wp.Latitude = ll(i,1);
%     wp.Longitude = ll(i,2);
% %     wp.Altitude = ll(i,3);
%     wp.TurnRadius = 0;
%     wp.Speed = scanSpeed;
% end
% scan.Route.Propagate();

% at.AccessConstraints.RemoveConstraint('eCstrElevationAngle');
scan.Unload();
root.EndUpdate();

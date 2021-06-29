function ClosestGrazingAngle(objPath,vectorName,AddVisually)
% Author: Austin Claybrook
% Organization: Analytical Graphics Inc.
% Date Created: 01/25/19
% Last Modified: 05/28/21 by Alexander Lam
% Description: Finds the closest* Earth horizon/limb/edge/grazing angle
% from a specifed vector/boresight. 
% Notes: *The direction of the closest edge is assumed to be in the same
% plane as the Nadir vector and vector of interest. This assumption is
% true for a perfectly spherical Earth, but may be slightly different when
% accounting for Earth's oblateness.
% *If Nadir is selected as the vector of interest, the closest edge of the
% Earth is assumed to be Northward.

%% Inputs:
% objPath: Object path in STK [string]
% vectorName: Name of the vector in AWB [string]
% AddVisually: Turn on the AWB components?(optional) false or true [boolean]

% Outputs:
% Analysis Workbench components: the rotation axis to the closest edge, the
% grazing point on the edge, the grazing vector and the grazing angle from
% the specified vector.

% Example inputs:
% ClosestGrazingAngle('Satellite/Satellite1','Body.X')
% ClosestGrazingAngle('Satellite/Satellite1/Sensor/Sensor1','Boresight')
% ClosestGrazingAngle('Satellite/Satellite1/Sensor/Sensor1','Boresight',true)
% ClosestGrazingAngle('Satellite/Satellite1/Sensor/Sensor1/Transmitter/Transmitter1','Boresight')


%% Check to see if AddVisually was defined
if ~exist('AddVisually','var')
    AddVisually = false;
end

%% Connect to current STK application, root and scenario
app = actxGetRunningServer('STK12.application');
app.UserControl = 1;
root = app.Personality2;

%% Grab the VGT interfaces 
obj = root.GetObjectFromPath(objPath);
objVGT = obj.Vgt;
vec = objVGT.Vectors;
vecFac = vec.Factory;
vecOfInterest = vec.Item(vectorName); 
point = objVGT.Points;
pointFac = point.Factory;
ang = objVGT.Angles;
angFac = ang.Factory;

%% Rotation Axis
if vec.Contains('Rotation_Axis')  
    rotAxis = vec.Item('Rotation_Axis');
else
    rotAxis = vecFac.Create('Rotation_Axis','Rotation Axis','eCrdnVectorTypeCrossProduct');
end   
rotAxis.To.SetVector(vecOfInterest);

% Set the Nadir vector 
if strcmp(obj.Parent.ClassName,'Scenario')
    if contains(vectorName,'Nadir')
        north = vec.Item('North');
        rotAxis.From.SetVector(vecOfInterest);
        rotAxis.To.SetVector(north);
    else
        nadir = vec.Item('Nadir(Centric)');
        rotAxis.From.SetVector(nadir);
    end
elseif strcmp(obj.Parent.Parent.ClassName,'Scenario')
    parent = obj.Parent;
    pvec = parent.Vgt.Vectors;
    nadir = pvec.Item('Nadir(Centric)');
    rotAxis.From.SetVector(nadir);
else
    parent = obj.Parent.Parent;
    pvec = parent.Vgt.Vectors;
    nadir = pvec.Item('Nadir(Centric)');
    rotAxis.From.SetVector(nadir);
end

%% Create the grazing point
if  point.Contains('ClosestGrazingPoint')
    grazingPoint = point.Item('ClosestGrazingPoint');
else
    grazingPoint = pointFac.Create('ClosestGrazingPoint','Approximately the Closest Grazing Point','eCrdnPointTypeGrazing');
end

if contains(vectorName,'Nadir')
    grazingPoint.DirectionVector.SetVector(north);
else
    grazingPoint.DirectionVector.SetVector(vecOfInterest);
end

%% Create the grazing vector
if vec.Contains('ClosestGrazingVector')
    grazingVec = vec.Item('ClosestGrazingVector');
else
    grazingVec = vecFac.Create('ClosestGrazingVector','To Grazing Point','eCrdnVectorTypeDisplacement');
end
center = point.Item('Center');
grazingVec.Destination.SetPoint(grazingPoint)
grazingVec.Origin.SetPoint(center)

%% Create the grazing angle from Nadir(Detic)
if ang.Contains('ClosestGrazingAngle')
    grazingAngle = ang.Item('ClosestGrazingAngle');
else
    grazingAngle = angFac.Create('ClosestGrazingAngle','Approximately the Closest Grazing Angle','eCrdnAngleTypeDihedralAngle');
end

grazingAngle.FromVector.SetVector(vecOfInterest);
grazingAngle.ToVector.SetVector(grazingVec);
grazingAngle.PoleAbout.SetVector(rotAxis);
grazingAngle.SignedAngle = 1;

%% Create the grazing angle as a calculation scalar
cs = objVGT.CalcScalars;
csFac = cs.Factory;
if cs.Contains('ClosestGrazingAngle')
    grazingValCS = cs.Item('ClosestGrazingAngle');
else
    grazingValCS = csFac.Create('ClosestGrazingAngle','Grazing Angle Along Direction','eCrdnCalcScalarTypeAngle');
end
grazingValCS.InputAngle = grazingAngle;

%% Turn on Visually
if AddVisually
    try
        vecOfInterestVO = obj.VO.Vector.RefCrdns.Add('eVectorElem',vecOfInterest.QualifiedPath);
        vecOfInterestVO.Visible = 1;
        vecOfInterestVO.Color = 65280;
    end
    try
        grazingVecVO = obj.VO.Vector.RefCrdns.Add('eVectorElem',grazingVec.QualifiedPath);
        grazingVecVO.Visible = 1;
        grazingVecVO.Color = 65280;
    end
    try
        grazingPointVO = obj.VO.Vector.RefCrdns.Add('ePointElem',grazingPoint.QualifiedPath);
        grazingPointVO.Visible = 1;
        grazingPointVO.Color = 65280;
    end
    try
        grazingAngleVO = obj.VO.Vector.RefCrdns.Add('eAngleElem',grazingAngle.QualifiedPath);
        grazingAngleVO.Visible = 1;
        grazingAngleVO.LabelVisible = 0;
        grazingAngleVO.AngleValueVisible = 1;
        grazingAngleVO.Color = 65280;
    end
end

%% Evaluate the angle at an instant
% epochInstant = '25 Jan 2018 05:00:00.000';
% value = grazingValCS.Evaluate(epochInstant);
% value.Value

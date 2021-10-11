function CreateGrazingAngle(objectPath,angleCCWFromNorth)
    % Author: Austin Claybrook
    % Organization: Analytical Graphics Inc.
    % Date Created: 06/27/18
    % Last Modified: 02/23/21 by Noah Ingwersen
%     Description: Finds the Earth horizon grazing angle along a given
%     direction from an object. In this case, a bearing angle rotated CCW from 
%     North is used to specify the direction.
%     
%     % Inputs:
%     objectPath: Object path in STK [string]
%     angleCCWFromNorth: Angle CCW from North (Westward) to specify the
%     reference direction [double]
%     
%     Outputs:
%     Analysis Workbench components: a reference direction vector, the grazing
%     point, the vector pointing to the grazing point, the grazing angle
%     relative to Nadir(detic), and a calc scalar for the grazing angle
    
    % Example input:
    % GrazingAngle('Satellite/Satellite1', -45)
    % GrazingAngle('Aircraft/Aircraft1', 20)
    
    %% Connect to current STK application, root and scenario
    uiApplication = actxGetRunningServer('STK12.application');
    uiApplication.UserControl = 1;
    stkRoot = uiApplication.Personality2;
    
    %% Grab the VGT intferface for a desired objellite
    object = stkRoot.GetObjectFromPath(objectPath);
    objectVgt = object.Vgt;
    
    %% Create a vector rotated CCW relative to North
    vectorComponents = [cosd(angleCCWFromNorth), sind(angleCCWFromNorth); -sind(angleCCWFromNorth), cosd(angleCCWFromNorth);]*[1;0];
    axes = objectVgt.Axes;
    northEastDownAxes = axes.Item('NorthEastDown');
    vectors = objectVgt.Vectors;
    vectorFactory = vectors.Factory;
    try
        customVector = vectorFactory.Create([num2str(angleCCWFromNorth), 'CCWFromNorth'], 'Rotated from North', 'eCrdnVectorTypeFixedInAxes');
    catch
        customVector = vectors.Item([num2str(angleCCWFromNorth), 'CCWFromNorth']);
    end
    customVector.ReferenceAxes.SetAxes(northEastDownAxes);
    customVector.Direction.AssignXYZ(vectorComponents(1), vectorComponents(2), 0);
    
    %% Create the grazing point
    point = objectVgt.Points;
    pointFactory = point.Factory;
    try
        customPoint = pointFactory.Create(['GrazingPoint', num2str(angleCCWFromNorth), 'CCWFromNorth'], 'Grazing Point Along Direction', 'eCrdnPointTypeGrazing');
    catch
        customPoint = point.Item(['GrazingPoint', num2str(angleCCWFromNorth), 'CCWFromNorth']);
    end
    customPoint.DirectionVector.SetVector(customVector);
    
    %% Create the grazing vector
    try
        toPoint = vectorFactory.Create(['ToGrazingPoint', num2str(angleCCWFromNorth), 'CCWFromNorth'], 'To Grazing Point', 'eCrdnVectorTypeDisplacement');
    catch
        toPoint = vectors.Item(['ToGrazingPoint', num2str(angleCCWFromNorth), 'CCWFromNorth']);
    end
    
    center = point.Item('Center');
    toPoint.Destination.SetPoint(customPoint);
    toPoint.Origin.SetPoint(center);
    
    %% Create the grazing angle from Nadir(Detic)
    angles = objectVgt.Angles;
    angleFactory = angles.Factory;
    
    try
        customAngle = angleFactory.Create(['GrazingAngle', num2str(angleCCWFromNorth), 'CCWFromNorth'], 'Grazing Angle Along Direction', 'eCrdnAngleTypeBetweenVectors');
    catch
        customAngle = angles.Item(['GrazingAngle', num2str(angleCCWFromNorth), 'CCWFromNorth']);
    end
    
    nadirVector = vectors.Item('Nadir(Detic)');
    customAngle.FromVector.SetVector(nadirVector);
    customAngle.ToVector.SetVector(toPoint);
    
    %% Create the grazing angle as a calculation scalar
    calcScalars = objectVgt.CalcScalars;
    calcScalarFactory = calcScalars.Factory;
    
    try
        customCalcScalar = calcScalarFactory.Create(['GrazingAngle', num2str(angleCCWFromNorth), 'CCWFromNorth'], 'Grazing Angle Along Direction', 'eCrdnCalcScalarTypeAngle');
    catch
        customCalcScalar = calcScalars.Item(['GrazingAngle', num2str(angleCCWFromNorth), 'CCWFromNorth']);
    end
    
    customCalcScalar.InputAngle = customAngle;
    
    %% Evaluate the angle at an instant
    % epochInstant = '1 Jan 2018 00:00:00.000';
    % value = customCS.Evaluate(epochInstant);
    % value.Value
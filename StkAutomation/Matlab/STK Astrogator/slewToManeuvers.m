%% slewToManeuvers
% When an Astrogator satellite maneuvers by default the manuever will adjust
% the attitude to align with the thrust direction, but it will not slew. 
% This script allows the satellite to slew into and out of the maneuver.
% Inputs

% Which Satellite
satName = 'Satellite1';

% Where to point when thrusting
onScheduleAlignedVector = ['Satellite/',satName,' TotalThrust'];
onScheduleConstrainedVector = ['CentralBody/Earth EclipticNormal'];
bodyDirAligned = [0,0,1]; % [x,y,z] components for the body aligned vector

% Where to point when not thrusting
offScheduleAlignedVector  = ['Satellite/',satName,' Nadir(Detic)'];
offScheduleConstrainedVector = ['Satellite/',satName,' Sun'];
bodyDirConstrained = [1,0,0]; % [x,y,z] components for the body constrained vector

% Slew options
slewDur = 60; % [s]

% Minimum thrust threshold. (Don't enter 0 or else numerical issues may cause problems)
thrustingThreshold = 0.000001; % [N] 

%% Code

% Connect to the satellite
app = actxGetRunningServer('STK11.Application');
root = app.Personality2;
sc = root.CurrentScenario;
sat = root.GetObjectFromPath(['Satellite/',satName]);

% Create the thrust calc scalar
cs = sat.Vgt.CalcScalars;
if cs.Contains('Thrust')
    thrustcs = cs.Item('Thrust');
else
    thrustcs = cs.Factory.Create('Thrust','Thrust Mag','eCrdnCalcScalarTypeVectorMagnitude');
end
thrustcs.InputVector = sat.Vgt.Vectors.Item('TotalThrust');

% Create the thrusting condition
con = sat.Vgt.Conditions;
if con.Contains('Thrusting')
    thrustcon = con.Item('Thrusting');
else
    thrustcon = con.Factory.Create('Thrusting','Is Thrusting?','eCrdnConditionTypeScalarBounds');
end
thrustcon.Scalar = sat.Vgt.CalcScalars.Item('Thrust');
quantity = root.ConversionUtility.NewQuantity('Force','N',thrustingThreshold);
thrustcon.SetMinimum(quantity);

% Create the pointing vectors
vec = sat.Vgt.Vectors;
if ~vec.Contains('AlignedDir')
    cmd = ['VectorTool * Satellite/',satName,' Create Vector AlignedDir "Scheduled"'];
    root.ExecuteCommand(cmd);
end
cmd = ['VectorTool * Satellite/',satName,' Modify Vector AlignedDir "Scheduled" Schedule "Satellite/',satName,' Thrusting.SatisfactionIntervals Interval List"'];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector AlignedDir "Scheduled" OnSchedule "',onScheduleAlignedVector,'"'];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector AlignedDir "Scheduled" OffSchedule "',offScheduleAlignedVector,'"'];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector AlignedDir "Scheduled" SlewWindowDuration ',num2str(slewDur)];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector AlignedDir "Scheduled" SlewTiming FavorOffSchedule'];
root.ExecuteCommand(cmd);

if ~vec.Contains('ConstrainedDir')
    cmd = ['VectorTool * Satellite/',satName,' Create Vector ConstrainedDir "Scheduled"'];
    root.ExecuteCommand(cmd);
end
cmd = ['VectorTool * Satellite/',satName,' Modify Vector ConstrainedDir "Scheduled" Schedule "Satellite/',satName,' Thrusting.SatisfactionIntervals Interval List"'];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector ConstrainedDir "Scheduled" OnSchedule "',onScheduleConstrainedVector,'"'];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector ConstrainedDir "Scheduled" OffSchedule "',offScheduleConstrainedVector,'"'];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector ConstrainedDir "Scheduled" SlewWindowDuration ',num2str(slewDur)];
root.ExecuteCommand(cmd);
cmd = ['VectorTool * Satellite/',satName,' Modify Vector ConstrainedDir "Scheduled" SlewTiming FavorOffSchedule'];
root.ExecuteCommand(cmd);

% Set attitude
sat.Attitude.Basic.SetProfileType('eProfileAlignedAndConstrained');
sat.Attitude.Basic.Profile.AlignedVector.ReferenceVector = ['Satellite/',satName,' AlignedDir'];
sat.Attitude.Basic.Profile.ConstrainedVector.ReferenceVector = ['Satellite/',satName,' ConstrainedDir'];
[x,y,z] = sat.Attitude.Basic.Profile.AlignedVector.Body.QueryXYZ;
xyz = [x,y,z];
% Check to make sure there are no co-aligned vectors
if sum(xyz == bodyDirConstrained) ~= 3
    sat.Attitude.Basic.Profile.AlignedVector.Body.AssignXYZ(bodyDirAligned(1),bodyDirAligned(2),bodyDirAligned(3))
    sat.Attitude.Basic.Profile.ConstrainedVector.Body.AssignXYZ(bodyDirConstrained(1),bodyDirConstrained(2),bodyDirConstrained(3))
else
    sat.Attitude.Basic.Profile.ConstrainedVector.Body.AssignXYZ(bodyDirConstrained(1),bodyDirConstrained(2),bodyDirConstrained(3))
    sat.Attitude.Basic.Profile.AlignedVector.Body.AssignXYZ(bodyDirAligned(1),bodyDirAligned(2),bodyDirAligned(3))
end

MCS = sat.Propagator.MainSequence;
maneuvers = FilterSegmentsByType(MCS,'Maneuver',[]);
for i = 1:length(maneuvers)
    maneuvers(i).Maneuver.AttitudeControl.CustomFunction = 'eVAEnablePageDefintion';
end

clear all; close all;
%%--------------------------------------------------------------------------
% Astrogator API Demo
%--------------------------------------------------------------------------

% Here we'll be demonstrating the Astrogator Object Model API and using it
% to build a LEO to GEO transfer and then a combined inclination and apogee
% raise maneuver once at GEO. This tutorial is meant to review only the
% Astrogator API - general STK scripting details are not presented here but
% help can be found at https://help.agi.com/stkdevkit

% Action is required by the reader in any lines that are commented and
% contain the words "ACTION REQUIRED"

%% Open a new STK scenario 

% Begin by creating a new STK scenario
uiApp = actxserver('STK12.Application');

% Make the UI visible and allow for user control after the connection from
% MATLAB is broken
uiApp.Visible = true;
uiApp.UserControl = true;

% Grab handle to STK program root
root = uiApp.Personality2;

% Create a new STK scenario
root.NewScenario('LEOToGEOWithMATLAB');

% Grab handle to newly created scenario
scenario = root.CurrentScenario;

%% Create an Astrogator Satellite

% Satellite Name
satName = 'AstgSat';

% Create a new satellite child object under the parent scenario object
sat = scenario.Children.New('eSatellite', satName);

% Set the propagator of the satellite to Astrogator
sat.SetPropagatorType('ePropagatorAstrogator');

% Grab a handle to the satellite propagator (Astrogator in this case)
astg = sat.Propagator;

% Grab a handle to the Mission Control Sequence (MCS)
mcs = astg.MainSequence;

%% Modify the First Propagate Segment

% ACTION REQUIRED
% Grab a handle to the propagate segment and add a stopping condition
% propSeg1 = 

% Create a new descending node stopping condition
propSeg1.StoppingConditions.Add('DescendingNode');

%% Add a new Target Sequence with Maneuver and Propagate Segments

% Add a new Target Sequence segment and save the handle
tgtSeq1 = mcs.Insert('eVASegmentTypeTargetSequence', 'Target Apoapsis', '-');

% Insert an impulsive maneuver in the target sequence
dV1 = tgtSeq1.Segments.Insert('eVASegmentTypeManeuver', 'dV1', '-');

% Set the attitude/maneuver direction type to thrust vector
dV1.Maneuver.SetAttitudeControlType('eVAAttitudeControlThrustVector');

% Grab a handle to the attitude control/maneuver direction interface
dV1Att = dV1.Maneuver.AttitudeControl;

% Define direction and magnitude values for the first maneuver in a Hohmann
% Transfer - in VNC frame
dV1x = 0; % m/s
dV1y = 0; % m/s
dV1z = 0; % m/s

% Assign values to maneuver
dV1Att.AssignCartesian(dV1x, dV1y, dV1z);

% Insert a propagate segment for the transfer orbit in the target sequence
propSeg2 = tgtSeq1.Segments.Insert('eVASegmentTypePropagate', 'PropToApoapsis', '-');

% Change the propagate segment color - STK uses decimal color coding
propSeg2Color = 8632576;
propSeg2.Properties.Color = propSeg2Color;

% ACTION REQUIRED
% Add an apoapsis stopping condition to the new propagate segment
% 

%% Add Another New Target Sequence with Propagate and Maneuver Segments

% Insert a second target sequence after the first
tgtSeq2 = mcs.Insert('eVASegmentTypeTargetSequence', 'Target Circularization', '-');

% ACTION REQUIRED 
% Add an impulsive maneuver to the new target sequence
% dV2 = 

% ACTION REQUIRED
% Set the attitude type to full thrust vector
% 

% Grab a handle to the attitude control/maneuver direction interface
dV2Att = dV1.Maneuver.AttitudeControl;

% Define direction and magnitude values for the second maneuver in a Hohmann
% Transfer - in VNC frame
dV2x = 0; % m/s
dV2y = 0; % m/s
dV2z = 0; % m/s

% ACTION REQUIRED
% Assign magnitude values to maneuver
% 

% Add a propagate segment to the end of the new target sequence
propSeg3 = tgtSeq2.Segments.Insert('eVASegmentTypePropagate', 'Prop1Day', '-');

% Grab a handle to the default duration stopping condition in the propagate
% segment that was just added
propSeg3Dur = propSeg3.StoppingConditions.Item('Duration');

% Change the trip value of duration to 1 day
propSeg3Dur.Properties.Trip = 86400; % sec

% Change the color of the third propagate segment
propSeg3Color = 16775168;
propSeg3.Properties.Color = propSeg3Color;

%% Configure the First Target Sequence

% Enable the control parameters in the maneuver segment (maneuver magnitude
% in velocity direction - just boosting into transfer orbit)
dV1.EnableControlParameter('eVAControlManeuverImpulsiveCartesianX');

% Add result for the propagate segment in the first target sequence
propSeg2.Results.Add('Spherical Elems/R Mag'); 

% Grab a handle to the differential corrector
dc1 = tgtSeq1.Profiles.Item('Differential Corrector');

% Enable the control parameter we added for the maneuver's magnitude in the
% cartesian X direction
dc1.ControlParameters.Item(0).Enable = true;

% Enable the R Mag result and set a desired magnitude
rmag = dc1.Results.Item(0);
rmag.Enable = true;
rmag.DesiredValue = 42164; % km, radius in spherical coordinates

%% Configure the Second Target Sequence

% ACTION REQUIRED
% Enable the control parameters for the manevuer (circularize and also
% change inclination to 0 degrees)
%
%
%

% Add results to the maneuver segment (not the propagate segment)
dV2.Results.Add('Keplerian Elems/Eccentricity');
dV2.Results.Add('Keplerian Elems/Inclination');
dV2.Results.Add('Spherical Elems/Flight Path Angle');

% Enable the control parameters for the maneuver in the corrector profile
dc2 = tgtSeq2.Profiles.Item('Differential Corrector');
dc2.ControlParameters.GetControlByPaths('dV2', 'ImpulsiveMnvr.Cartesian.X').Enable = true;
dc2.ControlParameters.GetControlByPaths('dV2', 'ImpulsiveMnvr.Cartesian.Y').Enable = true;
dc2.ControlParameters.GetControlByPaths('dV2', 'ImpulsiveMnvr.Cartesian.Z').Enable = true;

% Enable results and change tolerances (we want the value of all three of 
% these to be 0, which is the default value, but they have varying tolerances) 
ecc = dc2.Results.GetResultByPaths('dV2', 'Eccentricity');
inc = dc2.Results.GetResultByPaths('dV2', 'Inclination');
fpa = dc2.Results.GetResultByPaths('dV2', 'Flight Path Angle');

% ACTION REQUIRED
% Enable results and set tolerances to 0.001, 0.001, and 0.0001,
% respectively



%% Run Active Profiles for Each Target Sequence with No Pop-Ups

% Change target sequence setting to Run Active Profiles
tgtSeq1.Action = 'eVATargetSeqActionRunActiveProfiles';
tgtSeq2.Action = 'eVATargetSeqActionRunActiveProfiles';

% Turn off pop-ups from targeters during MCS run
tgtSeq1.Profiles.Item(0).EnableDisplayStatus = false;
tgtSeq2.Profiles.Item(0).EnableDisplayStatus = false; 

% ACTION REQUIRED
% Run the MCS
% 

% ACTION REQUIRED
% Apply the targeting profile changes
% 

% Clear Targeting Graphics
astg.ClearDWCGraphics();

% Show the lead and trail of each sequence
sat.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType('eDataAll');
sat.VO.Pass.TrackData.PassData.Orbit.SetTrailDataType('eDataAll');



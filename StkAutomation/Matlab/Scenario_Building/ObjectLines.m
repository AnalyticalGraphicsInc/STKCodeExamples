% First, manually create a scenario and setup a facility and satellite.
% Enter their names below in satelliteName and facilityName. 
% Change the final animation time;

stk = actxGetRunningServer('STK12.application');
root = stk.Personality2;
scenario = root.CurrentScenario;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');

% List of times in seconds relative to finalTime;
times = [0, -10, -20, -30, -40, -50, -60, -70, -80, -90, -100, 10,  20,  30,  40,  50,  60,  70,  80,  90,  100];

% Specify the final animation time here;
finalTime = '1 Jul 2021 17:00:00.000';

% Name of main satellite;
satelliteName = 'Satellite1';

% Name of facility;
facilityName = 'Facility1';

mainSatellite = root.GetObjectFromPath(['*/Satellite/',satelliteName]);
mainEpoch = mainSatellite.Propagator.InitialState.OrbitEpoch.TimeInstant;
mainColor = mainSatellite.Graphics.Attributes.Color;

for i = times
    satellite = mainSatellite.CopyObject([num2str(i), 's']);
    satellite.Propagator.InitialState.OrbitEpoch.SetExplicitTime(mainEpoch - i); 
    satellite.Propagator.Propagate;
    satellite.Graphics.PassData.GroundTrack.SetLeadDataType('eDataNone');
    satellite.Graphics.PassData.GroundTrack.SetTrailSameAsLead();
    satellite.Graphics.PassData.Orbit.SetLeadDataType('eDataNone');
    satellite.Graphics.PassData.Orbit.SetTrailSameAsLead();
    root.ExecuteCommand(['VO * ObjectLine Add FromObj Satellite/', num2str(i),'s ToObj Facility/', facilityName,' Color #', dec2hex(mainColor)]);
end

root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
scenario.Animation.StartTime = finalTime;
root.AnimationOptions
root.Rewind;
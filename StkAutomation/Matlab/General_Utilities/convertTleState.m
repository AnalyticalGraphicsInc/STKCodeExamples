function [  ] = convertTleState( tleSatelliteName, newPropagator )
%convertTleState This function will take a satellite propagated with SGP4
%and create a new satellite with the intital state of the TLE using the
%propagator specified 
%
% NOTE: The strings input must use single quotes and not double quotes to
% adhere to the requirements for SetPropagatorType
%
%Available propagator values:
%   'ePropagatorHPOP'
%   'ePropagatorJ2Perturbation'
%   'ePropagatorJ4Perturbation'
%   'ePropagatorTwoBody'
%
%   convertTleState(SatelliteName, propagator) creates a new satellite from
%   the 'tleSatelliteName' TLE initial state using the 'propagator' type
%
%   Example
%       convertTleState('Satellite1', 'ePropagatorTwoBody') 
    uiapp = actxGetRunningServer('STK12.application');
    root = uiapp.Personality2;
    scen = root.CurrentScenario;
    root.UnitPreferences.SetCurrentUnit('DateFormat','EpSec');   %Convert DateTime to EpSec value   
    satPath = append('*/Satellite/',tleSatelliteName);
    try
        sat = root.GetObjectFromPath(satPath);
        if strcmp(sat.PropagatorType, 'ePropagatorSGP4')
            if any(strcmp(newPropagator, {'ePropagatorHPOP', 'ePropagatorJ2Perturbation', 'ePropagatorJ4Perturbation', 'ePropagatorTwoBody'}))
                cartesianPosdp = sat.DataProviders.Item('Cartesian Position').Group.Item('ICRF');
                posResult = cartesianPosdp.ExecSingle(sat.Propagator.EphemerisInterval.GetStartEpoch.TimeInstant);
                x = posResult.DataSets.GetDataSetByName('x').GetValues;
                y = posResult.DataSets.GetDataSetByName('y').GetValues;
                z = posResult.DataSets.GetDataSetByName('z').GetValues;

                cartesianVeldp = sat.DataProviders.Item('Cartesian Velocity').Group.Item('ICRF');
                velResult = cartesianVeldp.ExecSingle(sat.Propagator.EphemerisInterval.GetStartEpoch.TimeInstant);
                vx = velResult.DataSets.GetDataSetByName('x').GetValues;
                vy = velResult.DataSets.GetDataSetByName('y').GetValues;
                vz = velResult.DataSets.GetDataSetByName('z').GetValues;

                newSat = scen.Children.New('eSatellite', [sat.InstanceName '_New']);
                newSat.SetPropagatorType(newPropagator);
                newSat.Propagator.InitialState.OrbitEpoch.SetExplicitTime(sat.Propagator.EphemerisInterval.GetStartEpoch.TimeInstant);
                newSat.Propagator.InitialState.Representation.AssignCartesian('eCoordinateSystemICRF', x{1}, y{1}, z{1}, vx{1}, vy{1}, vz{1});
                newSat.Propagator.Propagate;
            else
                disp([newPropagator ' is not a valid propagator type. Choose from ePropagatorHPOP, ePropagatorJ2Perturbation, ePropagatorJ4Perturbation or ePropagatorTwoBody']);
            end
        else
            disp('The choosen satellite is not based on a TLE');
        end
    catch
       disp([tleSatelliteName ' satellite was not found.']); 
    end
    uiapp.release;
    clear uiapp root 
end


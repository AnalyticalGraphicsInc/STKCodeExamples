% Propagates an Astrogator satellite for each TLE of a spacecraft. A target
% is inserted if the satellite completely decays.

%% Inputs
% Import TLEs between the timeframe
analysisStartTime = '25 Jan 2015 00:00:00.000';
analysisStopTime = '28 Jan 2015 00:00:00.000';

% Spacecraft Config
sscNumber = '26953'; % QuickBird2
Cd = 2.0; % Coefficent of Drag
Cr = 1.0;  % Coefficent of SRP
dragArea = 4.864; % [m^2]
srpArea = 4.864; % [m^2]
dryMass =  951; % [kg]

% Propagator Config
maximumPropagation = 30; % Max Propagation Time [days]
propagatorName = 'Earth HPOP Default v10'; % Propagator to Use

%% Start STK and create scenario
% Connect to STK or start STK
try
    app = actxGetRunningServer('STK12.application'); 
    app.Visible = 1;
    root = app.Personality2;
    root.Isolate;
catch
    app = actxserver('STK12.application'); 
    app.Visible = 1;
    root = app.Personality2;
    root.Isolate;
    root.NewScenario('Deorbit_History');

end

root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
scenario = root.CurrentScenario;
scenario.StartTime = analysisStartTime;
scenario.StopTime = analysisStopTime;
root.Rewind;

%Insert the desired satellite and propagate it for the analysis duration
if ~scenario.Children.Contains('eSatellite',['SGP4-' sscNumber])
    satelliteTLE = scenario.Children.New('eSatellite',['SGP4-' sscNumber]);
    satelliteTLE.SetPropagatorType('ePropagatorSGP4');
    propagator = satelliteTLE.Propagator;
    propagator.EphemerisInterval.SetExplicitInterval(analysisStartTime, analysisStopTime);
    try
        propagator.CommonTasks.AddSegsFromOnlineSource(sscNumber); 
        propagator.AutoUpdateEnabled = true;
    catch
        propagator.CommonTasks.AddSegsFromFile(sscNumber,'C:\ProgramData\AGI\STK 12\Databases\Satellite\stkAllTLE.tce');
    end
    propagator.Propagate;
else
    satelliteTLE = scenario.Children.Item(['SGP4-' sscNumber]);
    propagator = satelliteTLE.Propagator;
    propagator.EphemerisInterval.SetExplicitInterval(analysisStartTime, analysisStopTime);
    propagator.Propagate;
end


% Grab each TLE epoch from the satellite

segments = propagator.Segments.Count;
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
for i = 0:segments - 1
    epoch = propagator.Segments.Item(i).SwitchTime;
    epochUTCG = strsplit(strrep(strrep(epoch,' ','_'),':','_'),'.');
    epochsUTCG{i+1} = epochUTCG{1};
end
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
for i = 0:segments - 1
    epoch = propagator.Segments.Item(i).SwitchTime;
    epochs(i+1) = epoch;
end
% For each epoch create a new astrogator satellite and propagate until
% ground impact or max duration
deltaTime = 1e-3;  % seconds

for i = 1:segments
    if epochs(i) >= 0 && (propagator.EphemerisInterval.FindStopTime()-epochs(i)) > 0
        satellitePositionDataProvide = satelliteTLE.DataProviders.Item('Cartesian Position').Group.Item('ICRF').ExecSingle(epochs(i)+deltaTime);
        satelliteXPosition = cell2mat(satellitePositionDataProvide.DataSets.GetDataSetByName('x').GetValues);
        satellliteYPosition = cell2mat(satellitePositionDataProvide.DataSets.GetDataSetByName('y').GetValues);
        satelliteZPosition = cell2mat(satellitePositionDataProvide.DataSets.GetDataSetByName('z').GetValues);
        satelliteVelocityDataProvider = satelliteTLE.DataProviders.Item('Cartesian Velocity').Group.Item('ICRF').ExecSingle(epochs(i)+deltaTime);
        satelliteXVelocity = cell2mat(satelliteVelocityDataProvider.DataSets.GetDataSetByName('x').GetValues);
        satelliteYVelocity = cell2mat(satelliteVelocityDataProvider.DataSets.GetDataSetByName('y').GetValues);
        satrlliteZVelocity = cell2mat(satelliteVelocityDataProvider.DataSets.GetDataSetByName('z').GetValues);
        % Include the TLE epoch in the object names
        name = ['TLE_',epochsUTCG{i}];
        % Create Astrogator satellite
        if ~scenario.Children.Contains('eSatellite',name)
            satellite = scenario.Children.New('eSatellite',name);
        else
            satellite = scenario.Children.Item(name);
        end
        satellite.SetPropagatorType('ePropagatorAstrogator');
        driver = satellite.Propagator;
        driver.Options.DrawTrajectoryIn3D = false;
        
        initialState = driver.MainSequence.Item(0);
        initialState.InitialState.Cd = Cd;
        initialState.InitialState.Cr = Cr;
        initialState.InitialState.DragArea = dragArea;
        initialState.InitialState.SRPArea = srpArea;
        initialState.InitialState.DryMass = dryMass;
        initialState.InitialState.FuelMass = 0;
        initialState = driver.MainSequence.Item(0);
        initialState.SetElementType('eVAElementTypeCartesian');
        initialState.InitialState.Epoch = epochs(i)+deltaTime;
        cartesian = initialState.InitialState.Element;
        cartesian.X = satelliteXPosition;
        cartesian.Y = satellliteYPosition;
        cartesian.Z = satelliteZPosition;
        cartesian.Vx = satelliteXVelocity;
        cartesian.Vy = satelliteYVelocity;
        cartesian.Vz = satrlliteZVelocity;
        propagate = driver.MainSequence.Item(1);
        propagate.PropagatorName = propagatorName;
        try
            propagate.StoppingConditions.Remove('Altitude');
        catch
        end
        propagate.StoppingConditions.Add('Altitude');
        propagate.StoppingConditions.Item('Duration').Properties.Trip = maximumPropagation*86400;
        propagate.StoppingConditions.Item('Altitude').Properties.Trip = 0;
        
        % Propagate
        driver.RunMCS;
        satellite.Propagator.ClearDWCGraphics
        % Add an ephemeris span interval to the timeline
        try
            root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "Satellite/' satellite.InstanceName  ' EphemerisTimeSpan Interval"']);
        catch
            root.ExecuteCommand('Timeline * CreateWindow'); % Add timeline if necessary
            root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "Satellite/' satellite.InstanceName  ' EphemerisTimeSpan Interval"']);
        end
        % Get the time of impact
        stopTime = satellite.Propagator.MainSequence.Item(1).FinalState.Epoch;
        if abs(stopTime -(epochs(i)+maximumPropagation*86400)) > 1 % more than 1 sec off the max duration
            % Get the impact latitude and longitude
            satelliteEndLLA = satellite.DataProviders.Item('LLA State').Group.Item('Fixed').ExecSingle(stopTime - deltaTime);
            latitude = cell2mat(satelliteEndLLA.DataSets.GetDataSetByName('Lat').GetValues);
            longitude = cell2mat(satelliteEndLLA.DataSets.GetDataSetByName('Lon').GetValues);
            % Create Target
            if ~scenario.Children.Contains('eTarget',name)
                target = scenario.Children.New('eTarget', name);
            else
                target = root.GetObjectFromPath(['Target/',name]);
            end
            target.Position.AssignGeodetic(latitude,longitude,0);
            target.Graphics.Color = 255;
        else
            disp([name,' Did not decay'])
        end
    end
end
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
% Refresh the Timeline
root.ExecuteCommand('Timeline * Refresh WindowID 1');

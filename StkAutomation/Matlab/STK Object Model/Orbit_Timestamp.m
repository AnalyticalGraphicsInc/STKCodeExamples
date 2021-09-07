clear
clc
%% User Inputs
tick_time = 360; %s

% Uncomment to apply timestamps to all satellites. Comment out to choose satellites
% all_sats = 1;


% Uncomment to apply timestamps to certain satellites. Comment out for all satellites
sat_names = {'Satellite7', 'Satellite2'};
all_sats = 0;


%% Scenario Set up

% Connect to STK
uiapp = actxGetRunningServer('STK11.application');
root = uiapp.Personality2;
sc = root.CurrentScenario;

% Get start and stop times in UTCG format
root.UnitPreferences.SetCurrentUnit('DateFormat', 'UTCG');
start_UTCG = root.ConversionUtility.NewDate('UTCG', sc.StartTime);
stop_UTCG = root.ConversionUtility.NewDate('UTCG', sc.StopTime);

% Set units to EpSec and km
root.UnitPreferences.SetCurrentUnit('DateFormat', 'EpSec');
root.ExecuteCommand('Units_SetConnect / Date "EpochSeconds"');
root.ExecuteCommand('Units_SetConnect / Distance "Kilometers"');
%% Annotation Creation

% Get all satellites
if all_sats == 1
    sats = sc.Children.GetElements('eSatellite');
end

% Get start and stop time in EpSec
stop = sc.StopTime;
start = sc.StartTime;

% Counter for number of annotations
annotation_count = 1;

% Get number of satellites based off user input
switch all_sats
    case 1
        sat_count = sats.Count;
        
    case 0
        sat_count = length(sat_names);
end


% Loop through sats
for i = 1:sat_count
    
    % Get initial date and end of scenario date
    new_date = start_UTCG;
    last_date = stop_UTCG;
    count = 2;
    
    % Get satellite handle based on user input
    switch all_sats
        case 1
           thisSat = sats.Item(int32(i-1));
           
        case 0
            if root.ObjectExists(['Satellite/', sat_names{i}]) == 1
                
                thisSat = root.GetObjectFromPath(['Satellite/', sat_names{i}]);
            else
                
                fprintf(' A satellite with the name %s does not exist. Program will exit\n', sat_names{i});
                return
            end
    end
    
    % Switch to fixed frame, enable orbit ticks
    thisSat.VO.OrbitSystems.InertialByWindow.IsVisible = 0;
    thisSat.VO.OrbitSystems.FixedByWindow.IsVisible = 1;
    
    thisSat.VO.Pass.TickMarks.Orbit.IsVisible = 1;
    thisSat.VO.Pass.TickMarks.TimeBetweenTicks = tick_time;
    
    % Get satellite period
    orbit = thisSat.Propagator.InitialState.Representation.ConvertTo('eOrbitStateClassical');
    orbit.SizeShapeType = 'eSizeShapePeriod';
    period = orbit.SizeShape.Period;
    
    % Set initial orbit interval
    interval_start = 0;
    interval_stop = period;
    
    % Get LLAs
    dp = thisSat.DataProviders.GetDataPrvTimeVarFromPath('LLA State/Fixed');
    results = dp.Exec(start, stop, tick_time);
    
    lat = cell2mat(results.DataSets.GetDataSetByName('Lat').GetValues);
    lon = cell2mat(results.DataSets.GetDataSetByName('Lon').GetValues);
    alt = cell2mat(results.DataSets.GetDataSetByName('Alt').GetValues);
    
    % Create initial annotation
    string = ['VO * Annotation Add orbit_tick', num2str(annotation_count)];
    string = [string, ' Text String "', new_date.Format('UTCG'), '" Coord latlon '];
    string = [string, 'Position ', num2str(lon(1)), ' ', num2str(lat(1)), ' ',num2str(alt(1))];
    string = [string, ' Interval Add 1 "', num2str(interval_start), '" "', num2str(interval_stop), '"'];
    root.ExecuteCommand(string);
    annotation_count = annotation_count + 1;
    
    % Add tick time to date
    new_date = new_date.Add('sec', tick_time);
    
    % Calculate number of ticks per orbit, number of orbits for scenario
    ticks = floor(period/tick_time);
    remainder = mod(round(period/tick_time, 8), ticks);
    tick_check = remainder;
    tick_check_counter = 1;
    orbits = floor(stop/period);
    
    % Check if scenario length is a multiple of satellite period
    if mod(stop/period,orbits) < 0.01
        orbits = orbits - 1;
    end
    
    % Loop through all ticks for all orbits
    for j = 1:orbits
        for k = 1:ticks
            
            % Create annotation to display only for orbit interval
            string = ['VO * Annotation Add orbit_tick', num2str(annotation_count)];
            string = [string, ' Text String "', new_date.Format('UTCG'), '" Coord latlon '];
            string = [string, 'Position ', num2str(lon(count)), ' ', num2str(lat(count)), ' ',num2str(alt(count))];
            string = [string, ' Interval Add 1 "', num2str(interval_start), '" "', num2str(interval_stop), '"'];
            root.ExecuteCommand(string);
            new_date = new_date.Add('sec', tick_time);
            
            
            count = count + 1;
            annotation_count = annotation_count + 1;
            
        end
        
        % Remainder of orbit without tick eventually results in extra tick
        % for one orbit
        ticks = floor(period/tick_time);
        tick_check = tick_check + remainder;
        
        if tick_check >= tick_check_counter
            ticks = ticks + 1;
            tick_check_counter = tick_check_counter + 1;
        end
        
        % Set the start of next orbit interval to stop of previous. Find end of
        % next orbit interval
        interval_start = interval_stop;
        interval_stop = interval_stop + period;
    end
    
    % Last orbit in scenario ends at the end of scenario instead of
    % continuation of previous orbit
    count = length(lat);
    interval_stop = stop;
    
    % Check if period is multiple of tick time
    if mod(ceil(period/tick_time),period/tick_time) < 1e-8
        ticks = ceil(period/tick_time) + 1;
        
    else
        
        ticks = ceil(period/tick_time);
    end
    
    % Create annotations for final orbit
    for k = 1:ticks
        
        string = ['VO * Annotation Add orbit_tick', num2str(annotation_count)];
        string = [string, ' Text String "', last_date.Format('UTCG'), '" Coord latlon '];
        string = [string, 'Position ', num2str(lon(count)), ' ', num2str(lat(count)), ' ',num2str(alt(count))];
        string = [string, ' Interval Add 1 "', num2str(interval_start), '" "', num2str(interval_stop), '"'];
        root.ExecuteCommand(string);
        last_date = last_date.Subtract('sec', tick_time);
        
        count = count - 1;
        annotation_count = annotation_count + 1;
        
    end
    
end




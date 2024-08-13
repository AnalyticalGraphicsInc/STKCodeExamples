function [minimumDuration, maximumDuration, totalDuration] = coverageDurationPerDay(chainName)
    % Coverage Duration Per Day
    % Author: Kyle Gallaher
    % 
    % Description: Using a prebuilt, running scenario of STK, this function
    % receives the name of a chain and returns the minimum, maximum, and total
    % access duration per day.
    %
    % Instructions: Create a scenario containing a chain object that includes
    % the objects you want to compute access between. This function expects
    % the scenario time to start and stop at 00:00:00.000 UTCG.
    %
    % Inputs:
    % chainName: The name of the chain in the form of a string
    
    % Get running instance of STK
    uiApp = actxGetRunningServer('STK12.Application');
    root = uiApp.Personality2;
    
    % Convert DateTime to EpSec values
    root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
    
    % Get start and stop time of the scenario
    scenario = root.CurrentScenario;
    startTime = scenario.StartTime;
    stopTime = scenario.StopTime;
    
    % Determine the amount of days the scenario is ran. In the case of a
    % non-integer, round() is used.
    days = round((stopTime-startTime)/(24*60*60));
    
    % Grabs chain object from scenario
    areaChain = root.CurrentScenario.Children.Item(chainName);
    
    % Set start of the day to the start of the scenario
    dayStart = startTime;

    % Loop through Access Data data provider to determine the minimum,
    % maximum, and total access duration per day.
    for i = 1:days
        %Set end of the day
        dayEnd = dayStart + 24*60*60;

        % Pull data from data provider
        dp = areaChain.DataProviders.Item("Access Data").Exec(dayStart, dayEnd);

        % Loop through intervals in the data set
        for j = 1:dp.Intervals.Count
            minDuration(j) = min(cell2mat(dp.Interval.Item(cast(j-1, 'int32')).DataSets.GetDataSetByName("Duration").GetValues()));
            maxDuration(j) = max(cell2mat(dp.Interval.Item(cast(j-1, 'int32')).DataSets.GetDataSetByName("Duration").GetValues()));
            duration(j) = sum(cell2mat(dp.Interval.Item(cast(j-1, 'int32')).DataSets.GetDataSetByName("Duration").GetValues()));
        end

        % Record the desired data
        minimumDuration(i) = min(minDuration);
        maximumDuration(i) = max(maxDuration);
        totalDuration(i) = sum(duration);

        % Clear arrays for the next loop
        duration = [];
        minDuration = [];
        maxDuration = [];

        % Set the start of the next day as the end of the previous day
        dayStart = dayEnd;
    end
end
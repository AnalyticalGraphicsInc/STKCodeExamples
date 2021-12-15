function [ ] = intervalListFromTimeArray( stkObjectPath, timeArrayName, timeOffset )
%intervalListFromTimeArray Provide a Time Array on an STK Object with an
%offset time in seconds to create a new Time Interval List based off the
%Time Array
%
%   Example
%       intervalListFromTimeArray('*/Satellite/Satellite1', 'SampleTimeArray', 600)
    uiapp = actxGetRunningServer('STK.application');
    root = uiapp.Personality2;
    root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
    
    object = root.GetObjectFromPath(stkObjectPath);
    timeArray = object.Vgt.EventArrays.Item(timeArrayName);
    times = timeArray.FindTimes;
    timesArray = times.Times;
    %Create Temp Time Instant 
    timeComp = object.Vgt.Events.Factory.CreateEventEpoch('Time','');
    %Create Temp Time Interval
    timeIntFactory = object.Vgt.EventIntervals.Factory;
    timeInterval = timeIntFactory.CreateEventIntervalFixedDuration('TimeInterval','');
    %Store Interval Times
    intervals = {};
    
    for i = 1:length(timesArray)
        timeComp.Epoch = timesArray{i};
        timeInterval.ReferenceTimeInstant = timeComp;
        timeInterval.StartOffset = -1 * timeOffset;
        timeInterval.StopOffset = timeOffset;
        result = timeInterval.FindInterval;
        intervals{end+1} = result.Interval.Start;
        intervals{end+1} = result.Interval.Stop;
    end
    
    %Create Time Interval List
    try
        %Remove Temp Components
        object.Vgt.Events.Remove('Time');
        object.Vgt.EventIntervals.Remove('TimeInterval');
        
        intervalList = object.Vgt.EventIntervalLists.Factory.CreateEventIntervalListFixed(['IntListFrom',timeArrayName,'_TimeArray'],'');
        intervalList.SetIntervals(intervals');
    catch
        error('The IntervalList component already exists.');
    end
end


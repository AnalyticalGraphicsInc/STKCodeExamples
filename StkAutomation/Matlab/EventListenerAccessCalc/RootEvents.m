function RootEvents(varargin)

place = size(varargin);
spot = place(2);

switch (varargin{spot})
    
    case 'OnAnimUpdate'
        disp('Animation is updating')
    case 'OnScenarioNew'
        disp('New Scenario Created')
    case 'OnScenarioLoad'
        disp('Scenario Loaded')
    case 'OnScenarioClose'
        disp('Scenario Closed')
    case 'OnScenarioSave'
        disp('Scenario Saved')
    case 'OnLogMessage'
        disp('New Message in Log')
    case 'OnStkObjectAdded'
        disp('New Object Added')
    case 'OnStkObjectDeleted'
        disp('Object Deleted')
    case 'OnStkObjectRenamed'
        disp('Object Name Changed')
    case 'OnAnimationRewind'
        disp('Animation Reset')
    case 'OnAnimationPause'
        disp('Animation is Paused')
    case 'OnAnimationPlayback'
        disp('Animation Playback Event')
end
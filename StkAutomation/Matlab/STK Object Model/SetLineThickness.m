% Attach to Scenario
app = actxGetRunningServer('STK11.Application');
root = app.Personality2;

% Inputs
[objPaths] = FilterObjectsByType('Satellite');
width = 'e3'; % Default is e1
res = 60*30; % sec

% Options
setLineWidth = 0;
setRes = 0;
setOrbitPass = 0;
setGroundPass = 0;
setDetailThresholds = 0;
removeTimeBeforeZero = 1;

%% Execute
% Turn of Ground Tracks
sc = root.CurrentScenario;
sc.Graphics.GndTracksVisible = 0;
sc.Graphics.GndMarkersVisible = 0;

% Adjust Settings For Each Object
for j = 1:length(objPaths)
    obj = root.GetObjectFromPath(objPaths{j});
    
    % Set Line Width, And Turn of GroundTracks
    if setLineWidth == 1
        obj.Graphics.Attributes.Default.Inherit = 1;
        obj.Graphics.Attributes.Default.Line.Width = width;
        numOfIntervals = obj.Graphics.Attributes.Intervals.Count;

        for i = 0:numOfIntervals-1
            interval = obj.Graphics.Attributes.Intervals.Item(i);
            interval.GfxAttributes.Line.Width = width; 
        end
    end
    
    
    if removeTimeBeforeZero == 1
        zeroUTCG = root.ConversionUtility.ConvertDate('EpSec','UTCG','0');
        interval = obj.Graphics.Attributes.Intervals.Item(0);
        StartEP = str2num(root.ConversionUtility.ConvertDate('UTCG','EpSec',interval.StartTime));
        StopEP = str2num(root.ConversionUtility.ConvertDate('UTCG','EpSec',interval.StopTime));
        if StartEP < 0 && StopEP > 0
            intNew = obj.Graphics.Attributes.Intervals.Add(zeroUTCG,interval.StopTime);
            interval.GfxAttributes.Inherit = false;
            interval.GfxAttributes.IsOrbitVisible = false;
            interval.GfxAttributes.IsGroundTrackVisible = false;
            interval.StopTime = zeroUTCG;
            intNew.GfxAttributes.Color = interval.GfxAttributes.Color;
        end
    end
    
    % Set Orbit Resolution
    if setRes == 1
        obj.Graphics.Resolution.Orbit = res;
    end
    
    % Turn off Ground tracks
    if setGroundPass == 1
        obj.Graphics.PassData.GroundTrack.SetLeadDataType('eDataNone');
        obj.Graphics.PassData.GroundTrack.SetTrailSameAsLead;
    end
    
    % Show only previous orbit tracks
    if setOrbitPass == 1
        obj.Graphics.PassData.Orbit.SetLeadDataType('eDataNone')
        obj.Graphics.PassData.Orbit.SetTrailDataType('eDataAll');
    end
    

    if setDetailThresholds == 1
        obj.VO.Model.DetailThreshold.All = 0.7;
        obj.VO.Model.DetailThreshold.ModelLabel = 2; % [km]
        obj.VO.Model.DetailThreshold.MarkerLabel = 1e6; % [km]
        obj.VO.Model.DetailThreshold.Marker = 1e6; % [km]
        obj.VO.Model.DetailThreshold.Point = 1e12; % [km]
    end
end
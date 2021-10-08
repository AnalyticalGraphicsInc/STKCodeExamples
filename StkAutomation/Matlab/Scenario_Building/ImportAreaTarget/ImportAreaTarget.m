function [ ] = ImportAreaTarget( filepath, areaTargetName )
%ImportAreaTarget Attaches to an open instance of STK12 and imports
%position data from Excel spreadsheet. Units are assumed to be degrees and
%meters with a header row in the Excel file LAT and LON
%
% Example: ImportAreaTarget('AT.xlsx')

    uiapp = actxGetRunningServer('STK12.application');
    root = uiapp.Personality2;
    scen = root.CurrentScenario;

    root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
    root.UnitPreferences.Item('Latitude').SetCurrentUnit('deg');
    root.UnitPreferences.Item('Longitude').SetCurrentUnit('deg');
    root.UnitPreferences.SetCurrentUnit('Distance','m');

    data = readtable(filepath);
    
    areaTarget = scen.Children.New('eAreaTarget', areaTargetName);
    areaTarget.AreaType = 'ePattern';
    patterns = areaTarget.AreaTypeData;
    root.BeginUpdate();
    for i = 1:height(data)
        patterns.Add(data.LAT(i), data.LON(i));
    end
    root.EndUpdate();
    areaTarget.AutoCentroid = true;
end
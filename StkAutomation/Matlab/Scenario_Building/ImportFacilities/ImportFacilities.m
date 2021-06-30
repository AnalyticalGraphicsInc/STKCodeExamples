function [ ] = ImportFacilities( filepath )
% ImportFacilities Attaches to an open instance of STK12 and imports
% position data from Excel spreadsheet. Units are assumed to be degrees and
% meters with a header row in the Excel file for ID, LAT, LON, ALT
% 
% Example: ImportFacilities('GroundSites.xlsx')

    % Grab a running instance of STK
    uiapp = actxGetRunningServer('STK12.application');
    root = uiapp.Personality2;
    % Grab the current scenario
    scen = root.CurrentScenario;

    % Change the date to Epoch Seconds
    root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
    % Change the latitude and longitude to degrees
    root.UnitPreferences.Item('Latitude').SetCurrentUnit('deg');
    root.UnitPreferences.Item('Longitude').SetCurrentUnit('deg');
    % Change the distance to meters
    root.UnitPreferences.SetCurrentUnit('Distance','m');

    % Matlab command to read the excel sheet as a table
    data = readtable(filepath);

    % Iterate through each row
    for i = 1:height(data)
        % Grab the name of the facility
        facName = data.ID{i};
        
        % There cannot be two objects with the same name in STK so if there
        % is already a facility with the same name, delete it.
        if scen.Children.Contains('eFacility', facName)
            obj = scen.Children.Item(facName);
            obj.Unload;
        end
        
        % Create the facility with the name listed in the excel sheet
        fac = scen.Children.New('eFacility', facName);
        % Choose ot not use terrain
        fac.UseTerrain = false;
        % Set the latitude, longitude, and altitude
        fac.Position.AssignGeodetic(data.LAT(i), data.LON(i), data.ALT(i));
    end
end
def ImportFacilities(STKVersion, filepath):
    # ImportFacilities attaches to an open instance of STK and imports position data
    # from an Excel spreadsheet. Inputs include STK whole number version as an
    # integer and Excel file path. Units are assumed to be degrees and meters with a
    # header row in the Excel file for ID, LAT, LON, ALT. This function requires the
    # pandas Python library.
    #
    # Example: ImportFacilities(12, 'GroundSites.xlsx')
    
    import pandas as pd
    from comtypes.client import GetActiveObject
    
    # Grab a running instance of STK
    uiApplication = GetActiveObject(f'STK{STKVersion}.Application')
    root = uiApplication.Personality2

    from comtypes.gen import STKObjects
    
    # Grab current scenario
    scenario = root.CurrentScenario
    uiApplication.Visible = True
    uiApplication.UserControl = True
    scenario2 = scenario.QueryInterface(STKObjects.IAgScenario)
    
    # Change the latitude and longitude to degrees
    root.UnitPreferences.Item('Latitude').SetCurrentUnit('deg')
    root.UnitPreferences.Item('Longitude').SetCurrentUnit('deg')
    
    # Change the distance to meters
    root.UnitPreferences.SetCurrentUnit('Distance','m')
    
    # Use pandas to read in excel sheet as a dataframe
    df = pd.read_excel(filepath)
    
    # Iterate through each row
    for i, row in df.iterrows():
        facName = row['ID']
        lat = row['LAT']
        lon = row['LON']
        alt = row['ALT']
        type(facName)
        
        # There cannot be two objects with the same name in STK, so
        # if there is already a facility with the same name, delete it.
        if scenario.Children.Contains(STKObjects.eFacility, facName):
            obj = scenario.Children.Item(facName)
            obj.Unload()
            
        # Create the facility with the name listed in the excel sheet
        fac = scenario.Children.New(STKObjects.eFacility, facName)
        fac2 = fac.QueryInterface(STKObjects.IAgFacility)
        
        # Choose to not use terrain
        fac2.UseTerrain = False
        
        # Set the latitude, longitude, and altitude
        fac2.Position.AssignGeodetic(row['LAT'], row['LON'], row['ALT'])


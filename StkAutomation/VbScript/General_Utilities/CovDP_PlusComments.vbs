'Create AGI Application object
Dim uiApp 
'Connect to the running instance of STK 12
Set uiApp = GetObject(,"STK12.Application")

Dim root
'Get the IAgSTKObjectRoot interface (the STK 12 COM Object Model)
Set root = uiapp.Personality2

Dim scen
'Create a handle for the Scenario Object
Set scen = root.CurrentScenario
'Set units to UTCG (better for reports)
root.UnitPreferences("DateFormat").SetCurrentUnit("UTCG")

Dim CovDef, coverageDP, coverageResult, GridIn
'Get handle on Coverage Definition Object (change path as appropriate)
Set CovDef = root.GetObjectFromPath("*/CoverageDefinition/CoverageDefinition1")

'Get "Partial Coverage" data provider - you can substitute any data provider here
Set coverageDP = CovDef.DataProviders("Partial Coverage")
'Execute the data providers over the scenario interval - scen.StartTime to scen.StopTime
Set coverageResult = coverageDP.Exec(scen.StartTime, scen.StopTime)
msgbox coverageResult.DataSets.Count
'Get the start and stop times 
'	- you can choose any data set from the current data provider
covStartUTCG = coverageResult.DataSets.GetDataSetByName("Global Coverage Start").GetValues
covEndUTCG = coverageResult.DataSets.GetDataSetByName("Global Coverage End").GetValues


Dim GridPointsDP, GridPointsResult
'Here's a second example of retrieving a set of data
'Get CovDef Grid Point Data Providers
Set GridPointsDP = CovDef.DataProviders("Grid Point Locations")
'Execute Data Provider to retrieve data
Set GridPointsResult = GridPointsDP.Exec
'Get values and save data to local variables
lat = GridPointsResult.DataSets.GetDataSetByName("Latitude").GetValues
lon = GridPointsResult.DataSets.GetDataSetByName("Longitude").GetValues

msgbox lat(1) & lon(1)

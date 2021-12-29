%FOM Data Provider
%scenario should not be open prior to running script
uiapp = actxserver('STK12.application');
root = uiapp.Personality2;
%Change file path
ScenarioPath= 'C:\Users\alemme\Documents\STK 12\Scenarios\FOM_TimeValueByPoint\FOM_TimeValueByPoint.sc';
root.LoadScenario(ScenarioPath)
%Change Object path
FOMPath = '*/CoverageDefinition/CoverageDefinition1/FigureOfMerit/FigureOfMerit1';
oFom = root.GetObjectFromPath(FOMPath);
oFom.InstanceName
%Change time of interest
dataPrv = oFom.DataProviders.Item('Time Value By Point');
dataPrv.PreData = '16 Dec 2021 17:00:00.000';

%Don't forget to compute the access before trying to obtain the values
root.ExecuteCommand('Cov */CoverageDefinition/CoverageDefinition1 Access Compute');
%Get the elements of interest from the Data Provider 
%       - dataPrv is expecting to receive a Cell type array {xx;xx;xx...}
result = ExecElements(dataPrv,{'Latitude'; 'Longitude'; 'FOM Value'});

%Pull out the individual data sets
Lats =result.DataSets(1).GetDataSetByName('Latitude');
Longs = result.DataSets(1).GetDataSetByName('Longitude');
FOMVal = result.DataSets(1).GetDataSetByName('FOM Value');

%Get the values of the datasets
LatArray = Lats.GetValues();
LongArray = Longs.GetValues(); 
FOMValArray = FOMVal.GetValues();

[LatArray LongArray FOMValArray]

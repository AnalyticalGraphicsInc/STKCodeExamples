%Initialize 
uiApplication = actxGetRunningServer('STK12.application');
root = uiApplication.Personality2;
uiApplication.visible = 1;
scenario = root.CurrentScenario;

%Create a new satellite object named "LeoSat"
satellite = scenario.Children.New('eSatellite', 'LeoSat');
satellite.Propagator.Propagate;

%Add Temporal Constraint on LeoSat Satellite
localtime = satellite.AccessConstraints.AddConstraint('eCstrLocalTime');
localtime.Min = '1:00:00.000';
localtime.Max = '12:25:00.000';

%Create a new Coverage Definition & assign LeoSat as the Asset
covDef = scenario.Children.New('eCoverageDefinition','CovDef');
covDef.AssetList.Add(satellite.Path);
covDef.ComputeAccesses();

%Create a new Figure of Merit, type = Revisit Time
fom = covDef.Children.New('eFigureofMerit','Fom');
fom.SetDefinitionType('eFmRevisitTime');
fom.Definition.Satisfaction.EnableSatisfaction = true;

%Pulls the Percent Satisfied as a value
SatData_1 = fom.DataProviders.GetDataPrvFixedFromPath('Static Satisfaction');
Result_1 = SatData_1.Exec();
Percent_1 = cell2mat(Result_1.DataSets.GetDataSetByName('Percent Satisfied').GetValues)

%Gets Grid Inspector Tool Data
%This allows you to put in a Lat/Lon for the grid inspector 
gridInspector = fom.GridInspector;
Lat = 42.1429;
Lon = 4.00000;
gridInspector.SelectPoint(Lat,Lon);

%Outputs the same message as in the Grid Inspector
gridInspector.Message

%This is the value for Start Time from Execute Single (ans_Interval)
%The next is value for Duration of the Revisit Time (ans_Duration)
pointFOM = gridInspector.PointFOM;
pointFOMResult = pointFOM.Exec(scenario.StartTime,scenario.StopTime,60);
answer = pointFOMResult.DataSets.GetRow(0); %If row index is out of range adjust the temporal constrains of LeoSat.
ans_Interval = cell2mat(answer(1))
ans_Duration = cell2mat(answer(2)) 
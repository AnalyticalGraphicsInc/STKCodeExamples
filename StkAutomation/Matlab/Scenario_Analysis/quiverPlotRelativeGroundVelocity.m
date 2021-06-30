%Grab a running instance of STK12
uiapp = actxGetRunningServer('STK12.application');
root = uiapp.Personality2;
uiapp.visible = 1;
scenario = root.CurrentScenario;

% Enter the time you would like to generate the vector field
timeForPlot = '12 Mar 2018 17:00:00.000';
% Enter the name of the coverage definition
covDef = root.GetObjectFromPath('CoverageDefinition/CoverageDefinition1');

% Grab handles to the Figures of Merit
Xrate = covDef.Children.Item('Xvelocity');
Yrate = covDef.Children.Item('Yvelocity');
RelVel = covDef.Children.Item('RelativeVelocity');

% Get handles to the Data Providers
XrateDP = Xrate.DataProviders.GetDataPrvFixedFromPath('Time Value By Point');
YrateDP = Yrate.DataProviders.GetDataPrvFixedFromPath('Time Value By Point');
RelVelDP = RelVel.DataProviders.GetDataPrvFixedFromPath('Time Value By Point');

% Assigning PreData
XrateDP.PreData = timeForPlot;
YrateDP.PreData = timeForPlot;
RelVelDP.PreData = timeForPlot;

% Compute results
Xresult = XrateDP.Exec();
Yresult = YrateDP.Exec();
RelVelresult = RelVelDP.Exec();

% Get results in Matrix format
x = cell2mat(Xresult.DataSets.GetDataSetByName('Longitude').GetValues);
y = cell2mat(Xresult.DataSets.GetDataSetByName('Latitude').GetValues);
u = cell2mat(Xresult.DataSets.GetDataSetByName('FOM Value').GetValues);
v = cell2mat(Yresult.DataSets.GetDataSetByName('FOM Value').GetValues);

% Quiver plot
quiver(x, y, u, v)
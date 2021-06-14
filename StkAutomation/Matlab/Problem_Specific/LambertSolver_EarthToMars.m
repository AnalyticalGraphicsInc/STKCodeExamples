%% Simple OM Script to compute DeltaV required for an Earth to Mars transit via Lambert Solver

% Get reference to running STK instance
uiApplication = actxGetRunningServer('STK12.application');
% Get our IAgStkObjectRoot interface
root = uiApplication.Personality2; 
 
% create  a custom lambert solver
root.ExecuteCommand("ComponentBrowser */ Duplicate ""Design Tools"" ""Lambert Solver"" EarthToMars");
 
% set the mode
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars LambertToolMode ""Specify initial and final central bodies""");

% set the central body
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars CentralBody Sun");
 
% set the propagator
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars Propagator Sun_Point_Mass");
 
% earth centre is the departing place
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars Departure.CentralBody Earth");
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars Departure.RadiusScaleFactor 0");
 
% mars centre is the arriving place
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars Arrival.CentralBody Mars");
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars Arrival.RadiusScaleFactor 0");

% set the departure epoch
root.ExecuteCommand("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars InitEpoch ""15 Jun 2021 00:00:00.000"" UTCG");

% set the ToF
root.ExecuteCommand(strcat("ComponentBrowser */ SetValue ""Design Tools"" EarthToMars MinimumTOF 210 day"));

% compute
root.ExecuteCommand("ComponentBrowser */ LambertCompute ""Design Tools"" EarthToMars");

% get the result from the Lambert solver
deltaVTRes = root.ExecuteCommand("ComponentBrowser_RM */ GetValue ""Design Tools"" EarthToMars LambertResult.DeltaVT");
result = strsplit(deltaVTRes.Item(0));
deltaVT = str2double(result(3));
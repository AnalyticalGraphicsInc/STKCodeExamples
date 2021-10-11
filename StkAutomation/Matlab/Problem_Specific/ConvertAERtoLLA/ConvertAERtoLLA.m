%Written By: Alexander Ridgeway
%Date:       14 April 2016

clear;
clc;

try
    % Grab an existing instance of STK
    uiapp = actxGetRunningServer('STK12.application');
catch
    % STK is not running, launch new instance
    % Launch a new instance of STK and grab it
    uiapp = actxserver('STK12.application');
end

%get the root from the personality
root = uiapp.Personality2;
% set visible to true (show STK GUI)
uiapp.visible = 1;
%%From the STK Object Root you can command every aspect of the STK GUI
%close current scenario or open new one
try
    root.CloseScenario();
    root.NewScenario('LaunchVehicle');
catch
    root.NewScenario('LaunchVehicle');
end
%get the scenario root, its of type IAgScenario 
scen = root.CurrentScenario;

%Read the data into a Table > then convert to cell.
%Trying to keep accuracy of data points 
%Format of CSV is 
%EpochSeconds,Latitude,Longitude,Altitude for a simple example.
%Use Matlab skills to convert data OR change how these values are handled
root.ExecuteCommand('SetUnits / Km Epsec');
TAER = readtable('MOTR-To-GSLV-AER.csv');
TAER = table2cell(TAER);

%Initialize the Time-L-L-A, which is unused but could be utilized later
sizes = size(TAER);
TLLA= zeros(sizes(1),4);

%Create the Radar Location (Delete if it exists)
%Setting the Position of the actual Radar (fixed location)
try
    fac = root.CurrentScenario.Children.New('eFacility','Radar');
catch
    fac = root.GetObjectFromPath('/Facility/Radar');
    fac.Unload;
    fac = root.CurrentScenario.Children.New('eFacility','Radar');
end
fac.UseTerrain = false;
fac.Position.AssignGeodetic(13.7355,80.1847,-0.0564551);

%Create the DataVehicle (Delete if it exists)
%Using an 'aircraft' but could use a Ground Vehicle or Ship
%All three have a GreatArc propagator which allows for simple data
%interpolation and maintains the order of these points. Its also easy to
%remove a few data points.
try
    vehicle = root.CurrentScenario.Children.New('eAircraft','DataVehicle');
catch
    vehicle = root.GetObjectFromPath('/Aircraft/DataVehicle');
    vehicle.Unload;
    vehicle = root.CurrentScenario.Children.New('eAircraft','DataVehicle');
end
% Making the Launch Vehicle look more like a Launch Vehicle
% To change the model from an aircraft to Launch Vehicle
% To turn vapor trails on for Tank:
% To set attributes for the display of the vapor trails:
% To use a model attach point:
root.ExecuteCommand('VO */Aircraft/DataVehicle Model File "C:\Program Files\AGI\STK 11\STKData\VO\Models\Space\launchvehicle.dae"');
root.ExecuteCommand('VO */Aircraft/DataVehicle VaporTrail Show On');
root.ExecuteCommand('VO */Aircraft/DataVehicle VaporTrail MaxPuffs 1000 Density 50 Radius .02');
root.ExecuteCommand('VO */Aircraft/DataVehicle VaporTrail UseAttachPoint On AttachPoint "Stage1-SmokeNode"');

%This is the main loop here.
%First step is to create/modifies a Point in Analysis Workbench
%Next step is to run a report on that point's LLA value
%Finally step is to add the LLA position to the vehicle's route.
root.ExecuteCommand('BatchGraphics * On');%Command can potentially speed up the visualization 
for j = 1: sizes(1)

    %Create/Modify Point
    if j ==1 
        cmd = ['VectorTool * Facility/Radar Create Point Point_1 "Fixed in System" Spherical ' ...
        num2str(TAER{j,2},'%3.8f') ' -' num2str(TAER{j,3},'%3.8f') ' ' num2str(TAER{j,4},'%5.6f') ' "Facility/Radar Body"'];    
    else
        cmd = ['VectorTool * Facility/Radar Modify Point Point_1 "Fixed in System" Spherical ' ...
        num2str(TAER{j,2},'%3.8f') ' -' num2str(TAER{j,3},'%3.8f') ' ' num2str(TAER{j,4},'%5.6f') ' "Facility/Radar Body"'];    
    end
    root.ExecuteCommand(cmd);
    
    %Retrieves the point's location in LLA
    point = fac.DataProviders.Item('Points(Fixed)').Group.Item('Point_1').ExecSingle(scen.StartTime);
    lat = cell2mat(point.DataSets.GetDataSetByName('Detic Latitude').GetValues);
    lon = cell2mat(point.DataSets.GetDataSetByName('Detic Longitude').GetValues);
    alt = cell2mat(point.DataSets.GetDataSetByName('Detic Altitude').GetValues);
    
    %Not required but good to have a compiled list of all points in LLA
    TLLA(j,1)=TAER{j,1};
    TLLA(j,2)=lat;
    TLLA(j,3)=lon;
    TLLA(j,4)=alt;
    
    %Adding this point as a Waypoint to the vehicle
    cmd1 = ['AddWaypoint */Aircraft/DataVehicle DetVelFromTime ',  num2str(lat,'%3.8f') , ' ' ,num2str(lon,'%3.8f') , ' ' , num2str(alt,'%8.8f') , ' ' , num2str(TAER{j,1},'%3.8f')];
    root.ExecuteCommand(cmd1);
end 
root.ExecuteCommand('BatchGraphics * Off'); 

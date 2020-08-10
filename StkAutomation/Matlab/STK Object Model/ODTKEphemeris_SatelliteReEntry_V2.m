% Author: Alex Ridgeway
% Other Source Material: Austin Claybrook
% Organization: Analytical Graphics Inc.
% Date Created: 06/22/2020
% Description: Performs Re-Entry Analysis with a gaussian distribution of
% error analysis. Uses ephemeris with Covariance from ODTK's Output to
% propagate the satellite until a given altitude. 
% A target is inserted if the satellite completely decays.
% Script is writen to show example of automation of STK for decay analysis

%% Inputs into the Run: a. Ephemeris, b. Physical Properties and c. Run Info

% % % a. Ephemeris file from ODTK: 
%Requires EphemerisTimePosVel and CovarianceTimePosVel in STK format
%Commonly created in ODTK. 
disp('Select Ephemeris File for Re-Entry Calculation')
disp('Press Enter...')
pause;
[efile,epath] = uigetfile('*.e');
if isequal(efile,0)
   disp('User selected Cancel');
    return
else
%   disp(['User selected ',epath,efile]);
end


% % % b. Spacecraft Physical Parameters
disp('Enter some information about the Physical Properties of the Satellite')
disp('Press Enter...')
prompt = {'Coefficent of Drag','Coefficent of SRP','Drag Area(m^2)','SRP Area(m^2)', 'Dry Mass (kg)', 'Uncertainty (%)'};
dlgtitle = 'Input';
dims = [1 1 1 1 1 1];
definput = {'2.0','1.0','10','10','1000','10'};
answer = inputdlg(prompt,dlgtitle,dims,definput);
Cd = str2double(answer{1}); % Coefficent of Drag
Cr = str2double(answer{2});  % Coefficent of SRP
dragArea = str2double(answer{3}); % [m^2]
srpArea = str2double(answer{4}); % [m^2]
dryMass =  str2double(answer{5}); % [kg]
UNCERT_pct = str2double(answer{6})/100; %Percetage of uncertainty in the physical parameters.



% % % c. Inputs about the number of runs
disp('Enter some information for the Re-Entry runs')
disp('Press Enter...')
prompt = {'Enter the number of total runs to vary inputs (integer):','Enter maximum propagation time (days):','Enter decay altitude (km):'};
dlgtitle = 'Input';
dims = [1 1 1];
definput = {'1','15','80'};
answer = inputdlg(prompt,dlgtitle,dims,definput);

%error handling (any integer greater than 1/0 is accepted also catches NaN for String Inputs)
NumRuns = round(str2double(answer{1}));
if NumRuns >= 1
else
    NumRuns = 1;
end
maxProp=round(str2double(answer{2}));
if maxProp >= 1
else
    maxProp = 15;
end
ReEntryAlt =str2double(answer{3});
if ReEntryAlt >= 0
else
    ReEntryAlt = 80;
end


%% Connect to STK or Start new STK instance
disp('Connecting to STK...');
try
    app = actxGetRunningServer('STK11.application'); 
    app.Visible = 1;
    root=app.Personality2;
    root.Isolate;
catch
    app = actxserver('STK11.application'); 
    app.Visible = 1;
    root=app.Personality2;
    root.Isolate;
    root.NewScenario('DecayScenario');
end
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');



%% LOAD the Reference Satelite (from ODTK's Epehemeris)
try 
    root.CurrentScenario.Children.New('eSatellite','RefSat'); 
end
refsat = root.GetObjectFromPath('Satellite/RefSat');
refsat.SetPropagatorType('ePropagatorStkExternal')
%refsat.Propagator.Filename = 'C:\STK\WORKING\ReEntry_Decay\PosVelPcovVcov.e';
refsat.Propagator.Filename =[epath,efile];
refsat.Propagator.Propagate;
predictTime = refsat.Propagator.StopTime;
root.CurrentScenario.StartTime= predictTime;
root.CurrentScenario.StopTime= predictTime + maxProp*24*3600;

try   
    satCovDP = refsat.DataProviders.Item('Pos Vel Projected Covariance').Group.Item('RIC').ExecSingle(0);
    satcx = cell2mat(satCovDP.DataSets.GetDataSetByName('Sigma X').GetValues);
    satcy = cell2mat(satCovDP.DataSets.GetDataSetByName('Sigma Y').GetValues);
    satcz = cell2mat(satCovDP.DataSets.GetDataSetByName('Sigma Z').GetValues);
    satcvx = cell2mat(satCovDP.DataSets.GetDataSetByName('Sigma Xd').GetValues);
    satcvy = cell2mat(satCovDP.DataSets.GetDataSetByName('Sigma Yd').GetValues);
    satcvz = cell2mat(satCovDP.DataSets.GetDataSetByName('Sigma Zd').GetValues);
catch
    disp('Needs Position and Velocity Covariance in the Ephemeris file')
    disp('Please regenerate Ephemeris with Pos and Vel Covariance')
    return
end

%% LOADS a different HPOP model. NRL model has lower altitude modelling 
        

astgComponents = root.CurrentScenario.ComponentDirectory.GetComponents('eComponentAstrogator');
propComponents = astgComponents.GetFolder('Propagators');

try
propComponents.LoadComponent('C:\STK\WORKING\ReEntry_Decay\NRL_Drag.propagator');
catch
    disp('Could not find propagator file at C:\STK\WORKING\ReEntry_Decay\NRL_Drag.propagator')
    disp('Press place NRL_Drag.propagator into correct directory THEN press Enter...')
    return;
end

%% Loop to create all of the re-entry predictions.
a=1; %Starting loop for re-entry locations
b=1; %Starting loop for Non-decay events

for i =1:NumRuns
    
    % Create Astrogator satellite
    name = ['ReEntry_',num2str(i)]; 
    if ~root.CurrentScenario.Children.Contains('eSatellite',name)
        sat = root.CurrentScenario.Children.New('eSatellite',name);
    else
        sat = root.CurrentScenario.Children.Item(name);
    end
    sat.SetPropagatorType('ePropagatorAstrogator');
    driver = sat.Propagator;
    driver.Options.DrawTrajectoryIn3D = false;

    %Add Uncertainty
    if i > 1
        Cd_t = Cd + randn()*UNCERT_pct*Cd; % Coefficent of Drag
        Cr_t = Cr + randn()*UNCERT_pct*Cr;  % Coefficent of SRP
        dragArea_t = dragArea+ randn()*UNCERT_pct*dragArea; % [m^2]
        srpArea_t  = srpArea + randn()*UNCERT_pct*srpArea; % [m^2]
        dryMass_t  = dryMass + randn()*UNCERT_pct*dryMass; % [kg]

        satx_t=randn()*satcx;
        saty_t=randn()*satcy;
        satz_t=randn()*satcz;
        satvx_t=randn()*satcvx;
        satvy_t=randn()*satcvy;
        satvz_t=randn()*satcvz;
    else
        Cd_t = Cd ; % Coefficent of Drag
        Cr_t = Cr ;  % Coefficent of SRP
        dragArea_t = dragArea; % [m^2]
        srpArea_t  = srpArea ; % [m^2]
        dryMass_t  = dryMass ; % [kg]

        satx_t=0;
        saty_t=0;
        satz_t=0;
        satvx_t=0;
        satvy_t=0;
        satvz_t=0;
    end
            
    driver.MainSequence.RemoveAll;
    
    
    % Sets the Initial State and Physical Properties of one satellite
    initState = driver.MainSequence.Insert('eVaSegmentTypeInitialState','InitialState','-');           
    initState.InitialState.Cd = Cd_t;
    initState.InitialState.Cr = Cr_t;
    initState.InitialState.DragArea = dragArea_t;
    initState.InitialState.SRPArea = srpArea_t;
    initState.InitialState.DryMass = dryMass_t;
    initState.InitialState.FuelMass = 0;

    initState.SetElementType('eVAElementTypeCartesian');
    initState.CoordSystemName = 'Satellite/RefSat RIC';
    initState.InitialState.Epoch = 0;
    cart = initState.InitialState.Element;
    cart.X = satx_t;
    cart.Y = saty_t;
    cart.Z = satz_t;
    cart.Vx = 0; %satvx_t;
    cart.Vy = 0; %satvy_t;
    cart.Vz = 0; %satvz_t;     

    % Creates a Propagation segment to stop at Max Prop time or specified Altitude 
    propagate = driver.MainSequence.Insert('eVaSegmentTypePropagate','Propagate','-');                 
    propagate.StoppingConditions.Add('Altitude');
    propagate.StoppingConditions.Item('Duration').Properties.Trip = maxProp*86400; %sec
    propagate.StoppingConditions.Item('Altitude').Properties.Trip = ReEntryAlt; % km 
    propagate.PropagatorName = 'NRL Drag';
    
    % Propagate
    driver.RunMCS;
    
    %Get Results of Final State (decay or no decay)
    stopTime = sat.Propagator.MainSequence.Item(1).FinalState.Epoch;
    satEndLL = sat.DataProviders.Item('LLA State').Group.Item('Fixed').ExecSingle(stopTime-0.001);
    lat = cell2mat(satEndLL.DataSets.GetDataSetByName('Lat').GetValues);
    lon = cell2mat(satEndLL.DataSets.GetDataSetByName('Lon').GetValues);
    alt = cell2mat(satEndLL.DataSets.GetDataSetByName('Alt').GetValues);
    
    %Get two different Stop Time formats, EpochSeconds and UTCG. 
    endtime = sat.Propagator.MainSequence.Item(1).FinalState.Epoch; %in EpochSeconds
    root.UnitPreferences.Item('DateFormat').SetCurrentUnit('UTCG');
    displaytime = sat.Propagator.MainSequence.Item(1).FinalState.Epoch; %in UTCG
    
    %Creates a Timeline View to see when expected times occur.
    try
        root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "Satellite/' sat.InstanceName  ' EphemerisTimeSpan Interval"']);
    catch
        root.ExecuteCommand('Timeline * CreateWindow'); % Add timeline if necessary
        root.ExecuteCommand(['Timeline * TimeComponent Add ContentView "ContentView1" "Satellite/' sat.InstanceName  ' EphemerisTimeSpan Interval"']);
    end
    
    % Create Target at each of the re-entry locations and Save RESULTS.
    if alt <= ReEntryAlt+1; %little buffer of kms just incase 80.1 is achiever
        if ~root.CurrentScenario.Children.Contains('eTarget',name)
            target = root.CurrentScenario.Children.New('eTarget', name);
        else
            target = root.GetObjectFromPath(['Target/',name]);
        end
        target.Position.AssignGeodetic(lat,lon,alt);
        target.Graphics.Color = 255;

        disp([name,' decay around Lat:', num2str(lat), ', Lon:' , num2str(lon) , ', at Time:',displaytime, 'UTCG'])
        root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
        Results_LLA_data(a,:) = [lat lon alt endtime];
        %Results_LLA_time(a,:) = [displaytime];
        Results_Inputs(a,:) = [Cd_t Cr_t dragArea_t srpArea_t dryMass_t 0 satx_t saty_t satz_t satvx_t satvy_t satvz_t];             
        a=a+1;
    else
        disp([name,' Did not decay before :', displaytime])
        Result_NoDecay(b,:) = [Cd_t Cr_t dragArea_t srpArea_t dryMass_t 0 satx_t saty_t satz_t satvx_t satvy_t satvz_t]; 
        b=b+1;
    end
    
    %Back to normal Epoch Seconds. 
    root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec');
end

%% Results: 
disp(['Number of Decay Locations found:' num2str(a-1)])
disp(['Number of No-Decay:' num2str(b-1)])
disp(['Primary Results stored in the STK Scenario'])
disp(['Additional Results stored in the Workspace'])

Hr_Time = Results_LLA_data(:,4)/3600;
disp([num2str(mean(Hr_Time)),': hrs Mean Decay Time']);
disp([num2str(std(Hr_Time)),': hrs Standard Deviation']);


% Visualize Better
root.ExecuteCommand('Graphics * Label Show Off');
root.ExecuteCommand('VO */Satellite/* Model Show Off');
root.ExecuteCommand('VO */Satellite/* OrbitSystem Modify System "InertialByWindow" Show Off');
% root.ExecuteCommand('Graphics */Satellite/* Pass2D OrbitLead None OrbitTrail Time 120');
% root.ExecuteCommand('Graphics */Satellite/* Basic LineWidth 3');
% root.ExecuteCommand('VO */Satellite/* Pass3D Inherit On');
% root.ExecuteCommand('Graphics */Satellite/* Pass2D Resolution MaxOrbit 1');
% root.ExecuteCommand('VO */Satellite/* OrbitSystem Modify System "FixedByWindow" Show On');




% Author: Alex Ridgeway
% Organization: Analytical Graphics Inc.
% Date Created: 05/07/2019
% Last Updated: 08/12/2021 by Alex Lam
% Description: Performs a single object breakup for a debris object. Models
% 50 random debris objects generated using a gaussian model.


%% Initial config
uiapp = actxGetRunningServer('STK12.application');
root = uiapp.Personality2;
uiapp.visible = 1;

t1 = datestr(now,'dd mmm yyyy HH:MM:SS.000');
t2 = datestr(now+(1/864),'dd mmm yyyy HH:MM:SS.000');   % 100 min

scen = root.CurrentScenario;
root.CurrentScenario.SetTimePeriod(t1,t2); 
root.CurrentScenario.Epoch = t1; 
root.Rewind();

%Trys to find input object called Deb_. If no object exists, It creates one
try  root.GetObjectFromPath('Satellite/Deb_') 
catch 
    name = 'Deb_';
    satellite = scen.Children.New('eSatellite',name);
    satellite.SetPropagatorType('ePropagatorAstrogator');
    driver = satellite.Propagator;
    driver.MainSequence.RemoveAll();
    init = driver.Mainsequence.Insert('eVASegmentTypeInitialState','Initial State','-');
    init.InitialState.Epoch = scen.StartTime;
    init.SetElementType('eVAElementTypeCartesian');
 
    cart = init.Element;
    cart.X=-6630.82;
    cart.Y=-793.524;
    cart.Z=0.0;
    cart.Vx=0.849709;
    cart.Vy=-7.10032;
    cart.Vz=-3.03541 ;
    init.SpacecraftParameters.DragArea= 20;
    init.SpacecraftParameters.DryMass= 500;
end

%% Generate Debris
% Copy original satellite and create debris satellites
for i =1:50
    %Create a Copy of the Source Satellite
    try 
            root.ExecuteCommand(strcat('Copy / */Satellite/Deb_ Name Deb_',int2str(i)));
    catch
            root.ExecuteCommand(strcat('Unload / */Satellite/Deb_',int2str(i)));
            root.ExecuteCommand(strcat('Copy / */Satellite/Deb_ Name Deb_',int2str(i)));
    end
    name = strcat('Deb_',int2str(i));
    deb = root.GetObjectFromPath(strcat('Satellite/Deb_',int2str(i)));
    astro = deb.Propagator;
    
    %Adding a Maneuver
    man = astro.MainSequence.Insert('eVaSegmentTypeManeuver','Collision','-');
    man.SetManeuverType('eVAManeuverTypeImpulsive');
    impulsive = man.Maneuver;
    impulsive.SetAttitudeControlType('eVAAttitudeControlThrustVector');
    thrustVector = impulsive.AttitudeControl;
    thrustVector.ThrustAxesName = strcat('Satellite/',name,' VNC(Earth)');

    %Here is the MODEL for Debris breakup. It adds a DeltaV to simulate
    %breakup Defaul maneuver entry is Km/s, adding just 1m/s (1km/1000=1m) 
    thrustVector.AssignCartesian(randn()/1000,randn()/1000,randn()/1000);
   
    %Prop 100 min
    prop = astro.MainSequence.Insert('eVaSegmentTypePropagate','Prop100','-');
    prop.StoppingConditions.Item('Duration').Properties.Trip = 6000; %seconds
    prop.StoppingConditions.Add('Altitude');
    prop.StoppingConditions.Item('Altitude').Properties.Trip = 0;

    %Run MCS
    astro.RunMCS();	    
end

%% Propagate Original Sat
deb = root.GetObjectFromPath('Satellite/Deb_');
astro = deb.Propagator;

%Propagate
prop = astro.MainSequence.Insert('eVaSegmentTypePropagate','Prop100','-');
prop.StoppingConditions.Item('Duration').Properties.Trip = 6000; %seconds
prop.StoppingConditions.Add('Altitude');
prop.StoppingConditions.Item('Altitude').Properties.Trip = 0;

% Run MCS
astro.RunMCS();    

%% Visualization in the VVLH Frame
root.ExecuteCommand('Astrogator */Satellite/Deb* ClearDWCGraphics');
root.ExecuteCommand('Graphics */Satellite/Deb* Basic LineWidth 3');
root.ExecuteCommand('Graphics */Satellite/Deb* Pass2D OrbitLead None OrbitTrail Time 1200');
root.ExecuteCommand('Graphics */Satellite/Deb* Pass2D Resolution MaxOrbit 1');
root.ExecuteCommand('Graphics */Satellite/Deb* Label Show Off');
root.ExecuteCommand('VO */Satellite/Deb* Pass3D OrbitLead None OrbitTrail Time 1200');
root.ExecuteCommand('VO */Satellite/Deb* Model Show Off');
root.ExecuteCommand('VO */Satellite/Deb* OrbitSystem Modify System "InertialByWindow" Show Off');
root.ExecuteCommand('VO */Satellite/Deb* OrbitSystem Modify System "FixedByWindow" Show Off');
root.ExecuteCommand('VO */Satellite/Deb_* OrbitSystem Add System "Satellite/Deb_ VVLH"');
root.ExecuteCommand('VO */Satellite/Deb_* OrbitSystem Modify System "Satellite/Deb_ VVLH" color cyan');

%% Optional, change visualization to Fixed Frame
%root.ExecuteCommand('VO */Satellite/* OrbitSystem Modify System "FixedByWindow" Show On');
%root.ExecuteCommand('VO */Satellite/Deb_* OrbitSystem Modify System "Satellite/Deb_ VVLH" Show Off');




%% MonteCarlo
% Author: Austin Claybrook
% Organization: Analytical Graphics, Inc.
% Date Created: 03/04/19
% Last Modified: 03/12/19 by Austin Claybrook

% % Description:
% An example of setting up a Monte Carlo analysis in STK. This example
% perturbs a satellite's initial state with a Gaussian or uniform
% distribution. At each iteration the perturbations and the RIC state 
% relative to the nominal satellite are recorded. Please feel free to
% modify this snippet as necessary for your particular analysis,
% alternatively, you can perform more detailed analyses with STK Analyzer. 

% Instructions:
% Open a STK scenario with a satellite. Update the fields in the Input section
% with the desired values.

% Notes:
% The RIC data is stored in a variable called "results".
% The perturbed states are stored in a variable called "Xs".
% The perturbations are stored in a variable called "XPerts".
% The script will need to be modified to work for Astrogator satellites.

%% Inputs
% Existing satellite path in STK
satName = 'Satellite1';

% State Representation To Perturb
stateRepresenation = 'Cartesian'; % 'Keplerian' or 'Cartesian'

% Number of Perturbations
numOfPert = 25;

% Perturbations Method
pertMethod = 'Normal'; % 'Normal' or 'Uniform'

% Perturbation Size: 1-sigma values for 'Normal' and +-1 scale bounds for 'Uniform'
% Units are [km] and [deg] for 'Keplerian'. Units are [km] and [km/sec] for 'Cartesian'
% Set values to 0 if you do not want to perturb.
XSigma(1) = 10; % a or x
XSigma(2) = 10; % e or y
XSigma(3) = 10; % i or z
XSigma(4) = 0.01; % AoP or Vx
XSigma(5) = 0.01; % RAAN or Vy
XSigma(6) = 0.01; % TA or Vz

% Optional Plotting of Perturbed States
plotPerturbedStates = true; % true or false

%% Code 
% Connects to an existing instance of STK
app = actxGetRunningServer('STK12.application');
root = app.Personality2;
scenario = root.CurrentScenario;
curUnit = root.UnitPreferences.Item('DateFormat').CurrentUnit.Abbrv; % Pull current date format
root.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec'); % Switch to date format to EpSec

% Create a perturbed satellite
satNameP = [satName,'Perturbed'];
if scenario.Children.Contains('eSatellite',satNameP)
    satellite = root.GetObjectFromPath(['Satellite/',satNameP]);
    satellite.Unload;
    satRef = root.GetObjectFromPath(['Satellite/',satName]);
    satellite = scenario.Children.CopyObject(satRef,satNameP);
else
    satRef = root.GetObjectFromPath(['Satellite/',satName]);
    satellite = scenario.Children.CopyObject(satRef,satNameP);
end

% Pull existing state
X = zeros(6,1);
if strcmpi(stateRepresenation,'Keplerian')
    state = satellite.Propagator.InitialState.Representation.ConvertTo('eOrbitStateClassical');
    X(1) = state.SizeShape.SemiMajorAxis;
    X(2) = state.SizeShape.Eccentricity;
    X(3) = state.Orientation.Inclination;
    X(4) = state.Orientation.ArgOfPerigee;
    X(5) = state.Orientation.AscNode.Value;
    X(6) = state.Location.Value;
    pertList = {'a','e','i','AoP','RAAN','TA'};
elseif strcmpi(stateRepresenation,'Cartesian')
    state = satellite.Propagator.InitialState.Representation.ConvertTo('eOrbitStateCartesian');
    X(1) = state.XPosition;
    X(2) = state.YPosition;
    X(3) = state.ZPosition;
    X(4) = state.XVelocity;
    X(5) = state.YVelocity;
    X(6) = state.ZVelocity;
    pertList = {'x','y','z','Vx','Vy','Vz'};
end

% Perturb the state
XPerts = zeros(6,numOfPert); % Store Perturbations
Xs = zeros(6,numOfPert); % Store Perturbed States
results = cell(1,numOfPert); % Store results
for z = 1:numOfPert
    
    % Reassign Nominal State
    if strcmpi(stateRepresenation,'Keplerian')
        state.SizeShape.SemiMajorAxis = X(1);
        state.SizeShape.Eccentricity = X(2);
        state.Orientation.Inclination = X(3);
        state.Orientation.ArgOfPerigee = X(4);
        state.Orientation.AscNode.Value = X(5);
        state.Location.Value = X(6);
    elseif strcmpi(stateRepresenation,'Cartesian')
        state.XPosition = X(1);
        state.YPosition = X(2);
        state.ZPosition = X(3);
        state.XVelocity = X(4);
        state.YVelocity = X(5);
        state.ZVelocity = X(6);
    end

    % Pertubations
    if strcmpi(pertMethod,'Normal')
        % 1 sigma values
        XPert = XSigma'.*randn([6,1]); % Perturbation State Vector
    elseif strcmpi(pertMethod,'Uniform')
        % +- 1 perturbation value
        XPert = XSigma'.*2.*(rand([6,1])-0.5); % Perturbation State Vector
    end
    
    % Assign Perturbations
    XPerturbed = X;
    for j = 1:length(pertList)
        switch pertList{j}
            case 'a'
                XPerturbed(1) = X(1)+XPert(1);
                state.SizeShape.SemiMajorAxis = XPerturbed(1);
            case 'e'
                XPerturbed(2) = abs(X(2)+XPert(2));
                state.SizeShape.Eccentricity = XPerturbed(2);
            case 'i'
                XPerturbed(3) = abs(wrapTo180(X(3)+XPert(3)));
                state.Orientation.Inclination = XPerturbed(3);
            case 'AoP'
                XPerturbed(4) = wrapTo360(X(4)+XPert(4));
                state.Orientation.ArgOfPerigee = XPerturbed(4);
            case 'RAAN'
                XPerturbed(5) = wrapTo360(X(5)+XPert(5));
                state.Orientation.AscNode.Value = XPerturbed(5);
            case 'TA'
                XPerturbed(6) = wrapTo360(X(6)+XPert(6));
                state.Location.Value = XPerturbed(6);
            case 'x'
                XPerturbed(1) = X(1)+XPert(1);
                state.XPosition = XPerturbed(1);
            case 'y'
                XPerturbed(2) = X(2)+XPert(2);
                state.YPosition = XPerturbed(2);
            case 'z'
                XPerturbed(3) = X(3)+XPert(3);
                state.ZPosition = XPerturbed(3);
            case 'Vx'
                XPerturbed(4) = X(4)+XPert(4);
                state.XVelocity = XPerturbed(4);
            case 'Vy'
                XPerturbed(5) = X(5)+XPert(5);
                state.YVelocity = XPerturbed(5);
            case 'Vz'
                XPerturbed(6) = X(6)+XPert(6);
                state.ZVelocity = XPerturbed(6);
        end
    end
    XPerts(:,z) = XPert;
    Xs(:,z) = XPerturbed;
    
    % Evaluate Perturbation
    satellite.Propagator.InitialState.Representation.Assign(state);
    satellite.Propagator.Propagate;

    % Store RIC values from nominal trajectory
    rmDP = satellite.DataProviders.Item('Relative Motion');
    rmDP.PreData = ['Satellite/',satName];
    timeStep = 60;
    res = rmDP.Exec(scenario.StartTime,scenario.StopTime,timeStep);
    t = res.DataSets.GetDataSetByName('Time').GetValues;
    r = res.DataSets.GetDataSetByName('Radial').GetValues;
    i = res.DataSets.GetDataSetByName('In-Track').GetValues;
    c = res.DataSets.GetDataSetByName('Cross-Track').GetValues;
    results(z) = {cell2mat([t,r,i,c])}; % Results stored in a cell array
end
root.UnitPreferences.Item('DateFormat').SetCurrentUnit(curUnit); % Revert to original date format

% Plot Perturbed Values
if plotPerturbedStates
    close all;
    for j = 1:length(pertList)
        switch pertList{j}
            case 'a'
                figure(1)
                plot(1:numOfPert,Xs(1,:),'k.')
                xlabel('Iterations')
                ylabel('a [km]')
                legend(['1\sigma = ',num2str(std(XPerts(1,:)))])
                title('a vs Iterations')
            case 'e'
                figure(2)
                plot(1:numOfPert,Xs(2,:),'k.')
                xlabel('Iterations')
                ylabel('e')
                legend(['1\sigma = ',num2str(std(Xs(2,:)))])
                title('e vs Iterations')
            case 'i'
                figure(3)
                plot(1:numOfPert,Xs(3,:),'k.')
                xlabel('Iterations')
                ylabel('i [deg]')
                legend(['1\sigma = ',num2str(std(XPerts(3,:)))])
                title('i vs Iterations')
            case 'AoP'
                figure(4)
                plot(1:numOfPert,Xs(4,:),'k.')
                xlabel('Iterations')
                ylabel('AoP [deg]')
                legend(['1\sigma = ',num2str(std(XPerts(4,:)))])
                title('AoP vs Iterations')
            case 'RAAN'
                figure(5)
                plot(1:numOfPert,Xs(5,:),'k.')
                xlabel('Iterations')
                ylabel('RAAN [deg]')
                legend(['1\sigma = ',num2str(std(XPerts(5,:)))])
                title('RAAN vs Iterations')
            case 'TA'
                figure(6)
                plot(1:numOfPert,Xs(6,:),'k.')
                xlabel('Iterations')
                ylabel('TA [deg]')
                legend(['1\sigma = ',num2str(std(XPerts(6,:)))])
                title('TA vs Iterations')
            case 'x'
                figure(1)
                plot(1:numOfPert,Xs(1,:),'k.')
                xlabel('Iterations')
                ylabel('x [km]')
                legend(['1\sigma = ',num2str(std(Xs(1,:)))])
                title('x vs Iterations')
            case 'y'
                figure(2)
                plot(1:numOfPert,Xs(2,:),'k.')
                xlabel('Iterations')
                ylabel('y [km]')
                legend(['1\sigma = ',num2str(std(Xs(2,:)))])
                title('y vs Iterations')
            case 'z'
                figure(3)
                plot(1:numOfPert,Xs(3,:),'k.')
                xlabel('Iterations')
                ylabel('z [km]')
                legend(['1\sigma = ',num2str(std(Xs(3,:)))])
                title('z vs Iterations')
            case 'Vx'
                figure(4)
                plot(1:numOfPert,Xs(4,:),'k.')
                xlabel('Iterations')
                ylabel('Vx [km/sec]')
                legend(['1\sigma = ',num2str(std(Xs(4,:)))])
                title('Vx vs Iterations')
            case 'Vy'
                figure(5)
                plot(1:numOfPert,Xs(5,:),'k.')
                xlabel('Iterations')
                ylabel('Vy [km]')
                legend(['1\sigma = ',num2str(std(Xs(5,:)))])
                title('Vy vs Iterations')
            case 'Vz'
                figure(6)
                plot(1:numOfPert,Xs(6,:),'k.')
                xlabel('Iterations')
                ylabel('Vz [km]')
                legend(['1\sigma = ',num2str(std(Xs(6,:)))])
                title('Vz vs Iterations')
        end
    end
end
% % %Written By: Alexander Ridgeway
% % %Date:       10 July 2017
% % 

% %     REQUIRES Scenario with  
% %     A: Satellite called Satellite1
% %     B: AdvCAT called AdvCAT1
% %     C: The AdvCAT needs to be set up with Satellite1 as Primary and 
% %        any TLE database as the Secondary

% Grab an existing instance of STK
uiapp = actxGetRunningServer('STK12.application');
%get the root from the personality
%%From the STK Object Root you can command every aspect of the STK GUI
root = uiapp.Personality2;
% set visible to true (show STK GUI)
uiapp.visible = 1;

%get the scenario root, its of type IAgScenario 
scenario = root.CurrentScenario;
%get the Satellite Object
sat = root.GetObjectFromPath('/Satellite/Satellite1');

TotalCounts = zeros(1,10); % This will tel us how many Conjunctions
x_semX = 7000.0; %Place Holder
x_eccY = 0.0;
x_incL = 28.5; 
x_argP = 0.0;
x_raaN = 0.0;
x_truE = 0.0;


%Parameteric Study
%i goes 1 to 10 (10 levels)
%Semi Major Axis is changed by i*100 (step of 100km)
for i = 1:10
    x_semX = 7000+i*100;
    sat.Propagator.InitialState.Representation.AssignClassical...
        ('eCoordinateSystemICRF',x_semX,x_eccY,x_incL,x_argP,x_raaN,x_truE);
    sat.Propagator.Propagate;
    Report(i) = root.ExecuteCommand('ACATEvents_RM */AdvCAT/AdvCAT1');
    TotalCounts(i) = Report(i).Count;
    disp([x_semX TotalCounts(i)])
end


close all 
clear all 
clc 
  
% Create a new instance of STK12 
uiapp = actxserver('STK12.Application'); 
uiapp.visible = 1; 
  
% Get the object model root for STK12, IAgStkObjectRoot 
root = uiapp.Personality2; 
  
% Create a new scenario named SimpleSTK 
root.NewScenario('SimpleSTK'); 
  
% Create a satellite named TestSat, propagate with default properties 
satObj = root.CurrentScenario.Children.New('eSatellite', 'TestSat'); 
satObj.Propagator.Propagate; 
  
% Store the uiapp and root objects in the UserData property for the block 
stkParams = cell(3,1); 
stkParams{1} = uiapp; 
stkParams{2} = root; 
stkParams{3} = satObj; 
  
set_param(gcb, 'UserData', stkParams); 
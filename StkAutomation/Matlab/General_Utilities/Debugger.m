%%%

% Open Running Scenario and Get Object References

% Description: This script grabs a running instance of STK and assigns each
% object within to a MATLAB variable with the object name

% Cal Van Doren 10/13/2020

%%%

%% Housekeeping
clear all; close all; clc

%% Get Active STK Scenario
% Change depending on your version
app = actxGetRunningServer('STK12.application');
%app = actxGetRunningServer('STK11.application');

% Grab the root and scenario objects
root = app.Personality2;
scenario = root.CurrentScenario;

%% Get Object Count
count = scenario.Children.Count;

%% Loop To Create Variables Pointing to Objects
for i = 0:count-1 
   objName = scenario.Children.Item(int32(i)).InstanceName;
   tempObj = scenario.Children.Item(int32(i));
   eval([objName '= scenario.Children.Item(int32(i))']);
   if tempObj.Children.Count > 0
      for j = 0:tempObj.Children.Count-1
         subObjName = tempObj.Children.Item(int32(j)).InstanceName;
         tempSubObj = tempObj.Children.Item(int32(j));
         eval([subObjName '= tempObj.Children.Item(int32(j))']);
         if tempSubObj.Children.Count > 0
            for k = 0:tempSubObj.Children.Count-1
               subsubObjName = tempSubObj.Children.Item(int32(k)).InstanceName;
               eval([subsubObjName '= tempSubObj.Children.Item(int32(k))']);
            end
         end
      end
   end
end

clear tempObj tempSubObj subObjName objName i j count;
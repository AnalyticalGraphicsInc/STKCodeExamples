%Company: Analytical Graphics, Inc.
%Writer: Mo syed
%Date: 5/21/2019


%This script allows you to export many vgt components on one object and
%inserts them on another. Through the GUI, you can only do one at a time


awbComponentFolder = 'AWB'; %Folder where all the components will be
%stored
exportObjectName = 'Ship/Ship1'; %Object type and name
importObjectName = 'Satellite/Satellite1'; %Object type and name


%----------------------------STK Code--------------------------------------

%Connect to instance
app = actxGetRunningServer('STK12.application');
root = app.Personality2;
scenario = root.CurrentScenario;

%Grabs objects
objectToOutput = root.GetObjectFromPath(exportObjectName);
objectToInput = root.GetObjectFromPath(importObjectName);

%Gets path to the scenario
pathCommand = root.ExecuteCommand('GetDirectory / Scenario');
scenarioPath = pathCommand.Item(0);

%Creates folder to store .awb files and checks if it already exists
try
    mkdir([scenarioPath '/' awbComponentFolder]);
end

%Gets path to new folder
awbComponentPath = [scenarioPath '/' awbComponentFolder];

%Stores all vgt types
vgtType(1) = objectToOutput.Vgt.Points;
vgtType(2) = objectToOutput.Vgt.Axes;
vgtType(3) = objectToOutput.Vgt.Systems;
vgtType(4) = objectToOutput.Vgt.Planes;
vgtType(5) = objectToOutput.Vgt.Vectors;
vgtType(6) = objectToOutput.Vgt.Angles;

%Export AWB Components to the designated folder
for i = 1: length(vgtType)
    
    for j = 0 :vgtType(i).Count-1

    currentComponent = vgtType(i).Item(j);
    currentComponent.Export([awbComponentPath '/' currentComponent.Name '.awb'],'');

    end

end

%Input awb components to desired object
for i = 1: length(vgtType)
    
    for j = 0 :vgtType(i).Count-1

    currentComponent = vgtType(i).Item(j);
    objectToInput.Vgt.Import([awbComponentPath '/' currentComponent.Name '.awb']);

    end

end


disp('Components Transferred')




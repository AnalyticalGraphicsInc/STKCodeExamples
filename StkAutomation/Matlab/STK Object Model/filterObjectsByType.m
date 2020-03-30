%% filterObjectsByType
function [objPaths] = filterObjectsByType(objType,name)
% Description: Grabs all objects of a specifed type and returns their file
% path. Optionally the objects can also be filtered by name.

%% Inputs:
% objType: Object Type in STK, i.e. Satellite, Transmitter, etc. [string]
% name: Optionally Filter by Objects Containing this Name [string]

% Example 1: Find all satellites in the scenario
% FilterObjectsByType('Satellite', '')

% Example 2: Find all satellites in the scenario that start with "gps"
% FilterObjectsByType('Satellite', 'gps')


%% Code
% Attatch to an Existing Instance of STK
uiApplication = actxGetRunningServer('STK11.application');
root = uiApplication.Personality2;

% Write Objects in the Scenario to an XML File
xml = root.AllInstanceNamesToXML;
fileID = fopen('ObjectNames.xml','w');
fprintf(fileID,'%s',xml);
fclose(fileID);

% Grab All Objects in the XML of a User Specified Type
xmlDoc = xmlread('ObjectNames.xml');
allListItems = xmlDoc.getElementsByTagName('object');
objPaths = {};
k = 1;

% Grab All Objects of the Specified Type and Name
for i = 1:allListItems.getLength
    % Grab the Specifed Object Type
    ListItem = allListItems.item(i-1);
    if strcmp(char(ListItem.getAttribute('class')),char(objType)) == 1
        temp = strsplit(char(ListItem.getAttribute('path')),'/');
        % Store Objects
        if nargin == 2 && contains(temp{end}, name)
            objPaths{k} = char(ListItem.getAttribute('path'));
            k = k + 1;
        elseif nargin == 1
            objPaths{k} = char(ListItem.getAttribute('path'));
            k = k + 1;
        end
    end
end
end
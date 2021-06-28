function makeEllipticalSensor()
    % Author: Austin Claybrook
    % Organization: AGI, an Ansys company
    % Date Created: 10/12/2018
    % Last Modified:
    % Description: This script creates an elliptical sensor for a given
    % semi-major axis half angle, semi-minor axis half angle, and a
    % rotation angle fromhorizontal. It prompt the user for these values,
    % and the script will place the completed .pattern file on the desktop.
    % The .pattern file can then by loaded into STK by setting the sensor
    % type to "Custom" and loading the file from the desktop.
    
    %Get user inputs for shape/orientation of elliptical sensor.
    desktop = getSpecialFolder('Desktop');
    semiMajorAngle = input('Enter half angle of Semi-Major Axis (in Degrees): ');
    semiMinorAngle = input('Enter half angle of Semi-Minor Axis (in Degrees): ');
    rotationAngle = input('Enter rotation angle of semi-major axis from horizontal (in degrees): ');
    
    %Convert Semi-Major/Minor half angles to distances
    semiMajorDistance = tand(semiMajorAngle);
    semiMinorDistance = tand(semiMinorAngle);
    
    %Convert Semi-Major/Minor distances to polar ellipse values.
    %polar r relates to the complement of elevation 
    azRad = 0:pi/180:2*pi;
    r = sqrt(1 ./ ((cos(azRad) ./ semiMajorDistance) .^ 2 + (sin(azRad) ./ semiMinorDistance) .^ 2));
    coElDeg = atand(r);
    elDeg = 90 - coElDeg;
    
    %process values to be in azimuth/elevation pairs
    azDeg = azRad * 180 / pi;
    azDegRot = mod(azDeg - 90 + rotationAngle, 361); 
    result = [azDegRot.', elDeg.'];
    len = size(result);
    
    %print the .pattern file to the desktop
    file = fopen([desktop.char, '\EllipticalSensor', num2str(semiMajorAngle), '-', num2str(semiMinorAngle), '-', num2str(rotationAngle), '.pattern'], 'w');
    fprintf(file, 'stk.v.4.3\n');
    fprintf(file, 'NumberPoints %3d\n', len(1));
    fprintf(file, 'AzElMaskData\n');
    fprintf(file, '%7.4f %7.4f\n', result.');
    fprintf(file, 'EndPatternData');
    fclose(file);
end


% This function came from a code example in the Matlab help
% https://www.mathworks.com/help/matlab/matlab_external/read-special-system-folder-path.html
function result = getSpecialFolder(arg)
% Returns the special system folders such as "Desktop", "MyMusic" etc.
% arg can be any one of the enum element mentioned in this link
% http://msdn.microsoft.com/en-us/library/
% system.environment.specialfolder.aspx
% e.g. 
%       >> getSpecialFolder('Desktop')
%
%       ans = 
%       C:\Users\jsmith\Desktop
 
% Get the type of SpecialFolder enum, this is a nested enum type.
specialFolderType = System.Type.GetType(...
    'System.Environment+SpecialFolder');
% Get a list of all SpecialFolder enum values 
folders = System.Enum.GetValues(specialFolderType);
enumArg = [];
 
% Find the matching enum value requested by the user
for i = 1:folders.Length
    if (strcmp(char(folders(i)), arg))
        enumArg = folders(i);
    break
    end
end
 
% Validate
if(isempty(enumArg))
    error('Invalid Argument')
end
 
% Call GetFolderPath method and return the result
result = System.Environment.GetFolderPath(enumArg);
end
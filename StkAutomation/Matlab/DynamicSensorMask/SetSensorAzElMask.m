function SetSensorAzElMask(folderPath,time,sensor)

% Set the Sensor AzElMask file
timeStr = num2str(time);
string = [folderPath '\' sensor.InstanceName 'Time' timeStr '.bmsk'];
sensor.SetAzElMaskFile(string)

end


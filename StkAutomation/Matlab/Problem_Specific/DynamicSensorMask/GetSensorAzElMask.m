function GetSensorAzElMask(root,folderPath, time, sensor, obscureList,winRes)

% Get truncated paths
timeStr = num2str(time);
sensorTruncPath = [sensor.ClassName '/' sensor.InstanceName];
parent = sensor.Parent;
parentTruncPath = [parent.ClassName '/' parent.InstanceName];
sensorPath = ['*/' parentTruncPath '/' sensorTruncPath];

% Format AzElMaskTool
cmdStr = ['VO ' sensorPath ' AzElMaskTool '];
cmdTime = [cmdStr 'Time "' timeStr '"'];
root.ExecuteCommand(cmdTime);
for i = 1:length(obscureList)
    obscuringpath = [obscureList(i).ClassName '/' obscureList(i).InstanceName];
    cmdObj = [cmdStr 'Object On ' obscuringpath];
    root.ExecuteCommand(cmdObj);
end
cmdFile = [cmdStr 'File "' folderPath '\' sensor.InstanceName 'Time' timeStr '.bmsk"'];
root.ExecuteCommand(cmdFile);
cmdWin = [cmdStr 'WindowDim ' num2str(winRes)];
root.ExecuteCommand(cmdWin);

% Compute AzElMask
cmdCompute = [cmdStr 'Compute CBObscure Off'];
root.ExecuteCommand(cmdCompute);

end


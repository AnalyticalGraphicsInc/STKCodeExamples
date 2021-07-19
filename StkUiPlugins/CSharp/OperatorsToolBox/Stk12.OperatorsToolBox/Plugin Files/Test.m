app=actxGetRunningServer('STK12.application');
app.UserControl=1;
root=app.personality2;
scenario=root.CurrentScenario;
root.ExecuteCommand("Animate * Reset")
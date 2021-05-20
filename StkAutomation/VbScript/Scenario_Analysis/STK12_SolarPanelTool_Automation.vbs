
Dim app
Set app = CreateObject("STK12.Application")

set root = app.Personality2

Dim res
Dim start_time
Dim stop_time

start_time = "19 Feb 2013 17:00:00.000"
stop_time = "19 Feb 2013 21:00:00.000"

Set res = root.ExecuteCommand("New / Scenario SolarPanelTool")

Set res = root.ExecuteCommand("SetAnalysisTimePeriod * """ & start_time & """ """ & stop_time & "")
Set res = root.ExecuteCommand("SetAnimation * StartAndCurrentTime UseAnalysisStartTime")

Set res = root.ExecuteCommand("New / */Satellite Satellite1")

Set res = root.ExecuteCommand("SetState */Satellite/Satellite1 Classical J2Perturbation """ & start_time & """  """ & stop_time & """ 60 J2000 """ & start_time & """ 7163000.137079 0.0 28.5 0.0 139.7299 360.0")
'Set res = root.ExecuteCommand("ImportFromDB * Satellite AGIServer Propagate On TimePeriod UseScenarioInterval SSCNumber 25544")
Set res = root.ExecuteCommand("SetAttitude */Satellite/Satellite1 Profile SpinNadir 1.3 0 """ & start_time & "")

Set res = root.ExecuteCommand("VO */Satellite/Satellite1 SolarPanel Visualization Radius On 5 AddGroup Panels")
Set res = root.ExecuteCommand("VO */Satellite/Satellite1 SolarPanel Compute """ & start_time & """ """ & stop_time & """ 60")

MsgBox("Finished Computing")

Dim WshShell, strCurDir
Set WshShell = CreateObject("WScript.Shell")
strCurDir    = WshShell.CurrentDirectory

Set res = root.ExecuteCommand("ReportCreate */Satellite/Satellite1 Type Save Style ""Solar Panel Power"" TimePeriod """ & start_time & """ """ & stop_time & """ File """ & strCurDir & "\SPT_Report.txt" & """ TimeStep 60")
Set res = root.ExecuteCommand("GraphCreate */Satellite/Satellite1 Type Save Style ""Solar Panel Power"" TimePeriod """ & start_time & """ """ & stop_time & """ File """ & strCurDir & "\SPT_Graph.bmp" & """ TimeStep 60")

Set app = Nothing
Set root = Nothing
Set res = Nothing
Set WshShell = Nothing

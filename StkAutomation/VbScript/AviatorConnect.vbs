' This VBS script will open a new instance of STK, create a an aircraft, use Aviator to create the different procedures 
' during the route, and finally calculate inter-visibility (called Access) between the aircraft and a ground target.  
' This script uses Connect commands to create the aircraft and calculate the inter-visibility.

' How to use:
' Attached to this article is a *.vbs file with the below code. Download the file, or copy the code into a text editor and save as a *.vbs file.
' Double-click on the *.vbs file to  run, it should not require any additional setup. As written, this .vbs example requires STKv12 with the Integration and Aviator license.


'Connect to the STK API

Set uiApp = CreateObject("STK12.Application")
uiApp.Visible = True
uiApp.UserControl = True
Set stk = uiApp.personality2
stk.ExecuteCommand("New / Scenario AviatorConnectExample")

'Create an aircraft route with a default UAV performance model

stk.ExecuteCommand("New / */Aircraft Aircraft1")
stk.ExecuteCommand("SetPropagator */Aircraft/Aircraft1 MissionModeler")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Aircraft Copy ""Basic UAV""")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Aircraft Choose ""Basic UAV Copy""")

'Defining the first waypoint (Takeoff)

stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Procedure Add AsFirst SiteType Runway ProcedureType ""Takeoff""")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 1 SetValue Latitude 0 deg")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 1 SetValue Longitude 0 deg")

'Define the second waypoint (Enroute)

stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Procedure Add After 1 SiteType Waypoint ProcedureType ""Enroute""")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 2 SetValue Latitude 10 deg")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 2 SetValue Longitude 10 deg")

'Define the third waypoint (Enroute)

stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Procedure Add After 2 SiteType Waypoint ProcedureType ""Enroute""")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 3 SetValue Latitude 10 deg")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 3 SetValue Longitude 0 deg")

'Define the fourth waypoint (Landing)

stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Procedure Add After 3 SiteType Runway ProcedureType ""Landing""")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 4 SetValue Latitude 0 deg")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 4 SetValue Longitude 0 deg")

stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 ConfigureAll")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 CalculateAll")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 SendNtfUpdate")

'Create X number of targets

stk.ExecuteCommand("New / Target t1")
stk.ExecuteCommand("New / Target t2")

'Set the position of the targets

stk.ExecuteCommand("SetPosition */Target/t1 Geodetic 2 0 0")
stk.ExecuteCommand("SetPosition */Target/t2 Geodetic 0 2 0")

'Compute Access and generate graph/reports
stk.ExecuteCommand("GraphCreate */Aircraft/Aircraft1 Type Display Style ""Access"" AccessObject */Target/t1 TimePeriod UseAccessTimes TimeStep 60")



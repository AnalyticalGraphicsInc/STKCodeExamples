'open a file for reading and writing the files
dim objoutfile
dim otf,ofso
dim filename
dim strnextline
dim latitude_deg, longitude_deg, count
set ofso = createobject("scripting.filesystemobject")

'Connect to the STK API and the current running scenario
dim app
set app = getobject(,"STK12.Application")
set stk = app.personality2

'Open the waypoints file - change path for your own use
set objfile = ofso.opentextfile("C:\temp\waypoints.txt")
count = 1

msgbox("loading waypoints file from C:\temp\waypoints.txt")

'Read all the waypoints 
do until objfile.atendofstream

	strnextline = objfile.readline

	latitude_deg = cdbl(trim(left(strnextline,6))) 
	longitude_deg = cdbl(trim(mid(strnextline,8,6)))

	if count = 1 then
		
		if Not stk.ObjectExists("*/Aircraft/Aircraft1") then
			
			'Create an aircraft route with a default UAV performance model
			stk.ExecuteCommand("New / */Aircraft Aircraft1")
			stk.ExecuteCommand("SetPropagator */Aircraft/Aircraft1 MissionModeler")
			stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Aircraft Choose ""Basic UAV""")
			
		end if
		
		'Defining the first waypoint
		stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Procedure Add AsFirst SiteType Waypoint ProcedureType ""Enroute""")
		stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 1 SetValue Latitude " + cstr(latitude_deg) + " deg")
		stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site 1 SetValue Longitude " + cstr(longitude_deg) + " deg")

	end if

	'Add a new waypoint after the previous waypoint

	stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Procedure Add After " + cstr(count) + " SiteType Waypoint ProcedureType ""Enroute""")

	'Loop onto the next waypoint

	count = count + 1

	'Set the new position of the waypoint

	stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site " + cstr(count) + " SetValue Latitude " + cstr(latitude_deg) + " deg")
	stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 Site " + cstr(count) + " SetValue Longitude " + cstr(longitude_deg) + " deg")

loop

stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 ConfigureAll")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 CalculateAll")
stk.ExecuteCommand("MissionModeler */Aircraft/Aircraft1 SendNtfUpdate")

msgbox("Finished creating Aircraft")
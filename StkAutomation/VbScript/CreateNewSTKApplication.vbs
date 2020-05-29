Dim app

Set app = CreateObject("STK11.Application")

set root = app.Personality2


Dim res

Set res = root.ExecuteCommand("New / Scenario s1")

Set res = root.ExecuteCommand("New / Facility f1")

msgbox("finished")


Dim uiApp, root, scen
'   Create AGI Application object
Set uiApp = GetObject(,"STK12.Application")
Set root = uiApp.Personality2
Set scen = root.CurrentScenario
on error resume next
set cmdResult = root.ExecuteCommand("VO_R * MapID")
test = Split(cmdResult(0), vblf)
window = ubound(test) -1


root.ExecuteCommand("Window3D * CreateWindow Type Normal Title GeoView0")
root.ExecuteCommand("Window3D * CreateWindow Type Normal Title GeoView90")
root.ExecuteCommand("Window3D * CreateWindow Type Normal Title GeoView180")
root.ExecuteCommand("Window3D * CreateWindow Type Normal Title GeoView270")
root.ExecuteCommand("Window3D * Maximize WindowID 2")
set results = root.ExecuteCommand("Window3D_R * Size WindowID " & Cstr(window + 1))
sizeParts = Split(results(0), " ")
windowWidth = Cint(sizeParts(0)) / 4
windowWidth = CInt(windowWidth)
windowHeight = CInt(sizeParts(1))

root.ExecuteCommand("Window3D * Iconify  WindowID 2")
root.ExecuteCommand("Window3D * Raise  WindowID 2")
root.ExecuteCommand("Window3D * Size " & windowWidth & " " & windowWidth & "  WindowID " & Cstr(window + 1))
root.ExecuteCommand("Window3D * Size " & windowWidth & " " & windowWidth & " WindowID " & Cstr(window + 2))
root.ExecuteCommand("Window3D * Size " & windowWidth & " " & windowWidth & " WindowID " & Cstr(window + 3))
root.ExecuteCommand("Window3D * Size " & windowWidth & " " & windowWidth & " WindowID " & Cstr(window + 4))
root.ExecuteCommand("VO * Celestial Stars Show Off  WindowID " & Cstr(window + 1))
root.ExecuteCommand("VO * Celestial Stars Show Off  WindowID " & Cstr(window + 2))
root.ExecuteCommand("VO * Celestial Stars Show Off  WindowID " & Cstr(window + 3))
root.ExecuteCommand("VO * Celestial Stars Show Off  WindowID " & Cstr(window + 4))
root.ExecuteCommand("Window3D * SetFrame None WindowID " & Cstr(window + 1))
root.ExecuteCommand("Window3D * SetFrame None WindowID " & Cstr(window + 2))
root.ExecuteCommand("Window3D * SetFrame None WindowID " & Cstr(window + 3))
root.ExecuteCommand("Window3D * SetFrame None WindowID " & Cstr(window + 4))
root.ExecuteCommand("Window3D * Place 0 0 WindowID " & Cstr(window + 1))
root.ExecuteCommand("Window3D * Place " & Cstr(windowWidth) & " 0 WindowID " & Cstr(window + 2))
root.ExecuteCommand("Window3D * Place " & Cstr(2*windowWidth) & " 0 WindowID " & Cstr(window + 3))
root.ExecuteCommand("Window3D * Place " & Cstr(3*windowWidth) & " 0 WindowID " & Cstr(window + 4))
root.ExecuteCommand("Window3D * ViewVolume FieldOfView  90 WindowID " & Cstr(window + 1))
root.ExecuteCommand("Window3D * ViewVolume FieldOfView  90 WindowID " & Cstr(window + 2))
root.ExecuteCommand("Window3D * ViewVolume FieldOfView  90 WindowID " & Cstr(window + 3))
root.ExecuteCommand("Window3D * ViewVolume FieldOfView  90 WindowID " & Cstr(window + 4))
root.ExecuteCommand("Window3D * ViewVolume MinVisibleDist 35000000 WindowID " & Cstr(window + 1))
root.ExecuteCommand("Window3D * ViewVolume MinVisibleDist 35000000 WindowID " & Cstr(window + 2))
root.ExecuteCommand("Window3D * ViewVolume MinVisibleDist 35000000 WindowID " & Cstr(window + 3))
root.ExecuteCommand("Window3D * ViewVolume MinVisibleDist 35000000 WindowID " & Cstr(window + 4))
root.ExecuteCommand("VO * ViewAlongDirection From ""CentralBody/Earth Center Point"" Direction ""CentralBody/Earth Fixed.X Vector"" WindowID " & Cstr(window + 4)) 
root.ExecuteCommand("VO * ViewAlongDirection From ""CentralBody/Earth Center Point"" Direction ""CentralBody/Earth Fixed.Y Vector"" WindowID " & Cstr(window + 3)) 

root.ExecuteCommand("VectorTool * Earth Create Vector ViewNegativeX ""Fixed in Axes""")
root.ExecuteCommand("VectorTool * Earth Modify Vector ViewNegativeX ""Fixed in Axes"" Cartesian -1 0 0 ""CentralBody/Earth Fixed""")
root.ExecuteCommand("VectorTool * Earth Create Vector ViewNegativeY ""Fixed in Axes""")
root.ExecuteCommand("VectorTool * Earth Modify Vector ViewNegativeY ""Fixed in Axes"" Cartesian 0 -1 0 ""CentralBody/Earth Fixed""")
root.ExecuteCommand("VO * ViewAlongDirection From ""CentralBody/Earth Center Point"" Direction ""CentralBody/Earth ViewNegativeX Vector"" WindowID " & Cstr(window + 2)) 
root.ExecuteCommand("VO * ViewAlongDirection From ""CentralBody/Earth Center Point"" Direction ""CentralBody/Earth ViewNegativeY Vector"" WindowID " & Cstr(window + 1)) 
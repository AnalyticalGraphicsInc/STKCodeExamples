from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.stkutil import *
from agi.stk12.vgt import *
from agi.stk12.stkobjects.astrogator import *
from agi.stk12.stkobjects.aviator import *
from agi.stk12.graphics import *
from agi.stk12.utilities.colors import Color, Colors

# Get reference to running STK instance
stk = STKDesktop.AttachToApplication()

# Get the IAgStkObjectRoot interface
root = stk.Root
scenario = root.CurrentScenario

#Check For Objects
aircraft_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eAircraft)
satellite_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite)
ship_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eShip)
missile_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eMissile)
ground_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eGroundVehicle)
line_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eLineTarget)
planet_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.ePlanet)
sensor_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSensor)
area_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eAreaTarget)
launch_list = root.CurrentScenario.Children.GetElements(AgESTKObjectType.eLaunchVehicle)


# Get Variables
wins = root.ExecuteCommand("VO_R * MapID").Item(0)
windownumber = 1 #Which window are you changing?
fieldofview = 70 #What field of view do you want? Ed: 70
windowwidth = 1280 #Ed
windowheight = 720 #Ed
Lighting = "Ed Custom" #Ed Custom or STK Defaults or High Contrast
Celestial = "Bright Star" #Bright Star (Ed), STK Defaults, Hipparcos, No Stars
Clouds = "Off" #Off (Ed) or On
CameraInertia = "Off" #Off (Ed) or On
UnconstrainedRotation = "Off" #Off (Ed) or on
UseFonts = "Arial" #Arial (Ed) or STK Defaults
ImageCache = 512 #512 (Ed) or 64 (STK Default)
ShowTime = "Epoch Seconds" #Epoch Seconds (Ed) or STK defaults
LineWidths = 2 #2 (Ed), 1, or 3

# Variable Sets
windowid = str(windownumber)
fov = str(fieldofview)
width = str(windowwidth)
height = str(windowheight)
Cache = str(ImageCache)
Line = str(LineWidths)

smallFont = root.CurrentScenario.VO.SmallFont.Name
medFont   = root.CurrentScenario.VO.MediumFont.Name
largeFont = root.CurrentScenario.VO.LargeFont.Name
smallPt   = root.CurrentScenario.VO.SmallFont.PtSize
medPt     = root.CurrentScenario.VO.MediumFont.PtSize
largePt   = root.CurrentScenario.VO.LargeFont.PtSize



# Set Field of View
root.ExecuteCommand("Window3D * ViewVolume FieldOfview " + fov + " WindowID " + windowid)


#Set Window Size
root.ExecuteCommand("Window3D * InnerSize " + width + " " + height + " WindowID " + windowid)


#Smooth lighting
root.ExecuteCommand("VO * Lighting UsePerPixelLighting On WindowID " + windowid)

if Lighting == "STK Defaults":
	root.ExecuteCommand("VO * Lighting Show On Ambient 25 Sun 500 NightLights 500 Positional On Object On ObjAmbient 25 ObjSun 100 SolarIntensity On WindowID " + windowid)	
if Lighting == "Ed Custom":
	root.ExecuteCommand("VO * Lighting Show On Ambient 20 Sun 500 NightLights 500 Positional On Object On ObjAmbient 20 ObjSun 80 SolarIntensity On WindowID " + windowid)	
if Lighting == "High Contrast":
	root.ExecuteCommand("VO * Lighting Show On Ambient 5 Sun 500 NightLights 500 Positional On Object On ObjAmbient 5 ObjSun 95 SolarIntensity On WindowID " + windowid)
	

#Celestial
if Celestial == "STK Defaults":
	root.ExecuteCommand("VO * Celestial Stars Show On ShowPts On ShowTx Off WindowID " + windowid)
	root.ExecuteCommand("VO * Celestial Stars CollectionName \"ScenarioCollection\" BrightMag -1.0 MediumMag 7.0 DimMag 12.0 BrightPtSize 5 MediumPtSize 1 DimPtSize .1 BrightTranslucency 0 MediumTranslucency 25 DimTranslucency 80 ColorizeStars Off WindowID " + windowid)
if Celestial == "Bright Star":
	root.ExecuteCommand("VO * Celestial Stars Show On ShowPts On ShowTx Off WindowID " + windowid)
	root.ExecuteCommand("VO * Celestial Stars CollectionName \"Bright Star v5 Mag 6\" BrightMag -1.44 MediumMag 3.0 DimMag 6.0 BrightPtSize 4 MediumPtSize 2 DimPtSize 1 BrightTranslucency 0 MediumTranslucency 0 DimTranslucency 0 ColorizeStars On WindowID " + windowid)
if Celestial == "Hipparcos":
	root.ExecuteCommand("VO * Celestial Stars Show On ShowPts On ShowTx Off WindowID " + windowid)
	root.ExecuteCommand("VO * Celestial Stars CollectionName \"Hipparcos 2 Mag 6\" BrightMag -1.46 MediumMag 3.0 DimMag 6.0 BrightPtSize 4 MediumPtSize 2 DimPtSize 1 BrightTranslucency 0 MediumTranslucency 0 DimTranslucency 0 ColorizeStars On WindowID " + windowid)
if Celestial == "No Stars":
	root.ExecuteCommand("VO * Celestial Stars Show Off WindowID " + windowid)
	

#Clouds
root.ExecuteCommand("VO * Attributes Clouds " + Clouds + " " + windowid)


#Camera Inertia
root.ExecuteCommand("VO * View Mouse Inertia " + CameraInertia + " WindowID " + windowid)


#Unconstrained Rotation
root.ExecuteCommand("VO * View Parameters UseUpAxis " + UnconstrainedRotation + " WindowID " + windowid)


#Use Fonts
if UseFonts == "Arial":
	root.ExecuteCommand("VO * Fonts Small Face \"" + UseFonts + "\" Size 18 Bold On")
	root.ExecuteCommand("VO * Fonts Medium Face \"" + UseFonts + "\" Size 24 Bold On")
	root.ExecuteCommand("VO * Fonts Large Face \"" + UseFonts + "\" Size 36 Bold On")
if UseFonts == "STK Defaults":
	root.ExecuteCommand("VO * Fonts Small Face \"" + smallFont + "\" Size " + smallPt + " Bold Off")
	root.ExecuteCommand("VO * Fonts Medium Face \"" + medFont  + "\" Size " + medPt + " Bold Off")
	root.ExecuteCommand("VO * Fonts Large Face \"" + largeFont + "\" Size " + largePt + " Bold Off")


#Image Cache
root.ExecuteCommand("VO * TerrainAndImagery Data ChunkImageCacheSize " + Cache)


#Use Epoch Seconds
if ShowTime == "Epoch Seconds":
	root.ExecuteCommand("Units_Set * GUI Date EpSec")


#Line Thicknesses
if line_list.Count > 0:
    root.ExecuteCommand("Graphics */LineTarget/* LineWidth " + Line)
if sensor_list.Count > 0:
    root.ExecuteCommand("Graphics */Sensor/* LineWidth " + Line)
if area_list.Count > 0:
    root.ExecuteCommand("Graphics */AreaTarget/* LineWidth " + Line)
if planet_list.Count > 0:
    root.ExecuteCommand("Graphics */Planet/* LineWidth " + Line)
 
#Vehicles
if satellite_list.Count > 0:
    root.ExecuteCommand("Graphics */Satellite/* Basic LineWidth " + Line)
if aircraft_list.Count > 0:
    root.ExecuteCommand("Graphics */Aircraft/* Basic LineWidth " + Line)
if ship_list.Count > 0:
    root.ExecuteCommand("Graphics */Ship/* Basic LineWidth " + Line)
if missile_list.Count > 0:
    root.ExecuteCommand("Graphics */Missile/* Basic LineWidth " + Line)
if launch_list.Count > 0:
    root.ExecuteCommand("Graphics */LaunchVehicle/* Basic LineWidth " + Line)
if ground_list.Count > 0:
    root.ExecuteCommand("Graphics */GroundVehicle/* Basic LineWidth " + Line)
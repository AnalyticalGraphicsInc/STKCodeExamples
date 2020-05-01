# Get reference to running STK instance using win32com
from win32com.client import GetActiveObject
uiApplication = GetActiveObject('STK12.Application')


# Get our IAgStkObjectRoot interface
root = uiApplication.Personality2 

print(root.CurrentScenario.InstanceName) 
print("Total number of objects: {0}".format(len(root.CurrentScenario.Children)))

for stkObject in root.CurrentScenario.Children:
    print(stkObject.ClassName + " " + stkObject.InstanceName)
    
    if stkObject.ClassName == "Facility":
        positionArray = stkObject.Position.QueryPlanetocentric()
        print('Position: {0:3.2f} {1:3.2f}'.format(positionArray[0], positionArray[1]))
    
    #if stkObject.ClassName == "AdvCAT":
    #if stkObject.ClassName == "Aircraft":
    #if stkObject.ClassName == "Area Target":
    #if stkObject.ClassName == "Attitude Coverage":
    #if stkObject.ClassName == "Chain":
    #if stkObject.ClassName == "CommSystem":
    #if stkObject.ClassName == "Constellation":
    #if stkObject.ClassName == "Coverage Definition":
    #if stkObject.ClassName == "Facility":
    #if stkObject.ClassName == "Ground Vehicle":
    #if stkObject.ClassName == "Launch Vehicle":
    #if stkObject.ClassName == "Line Target":
    #if stkObject.ClassName == "MTO (Multi-Track Object)":
    #if stkObject.ClassName == "Missile":
    #if stkObject.ClassName == "Missile System":
    #if stkObject.ClassName == "Planet":
    #if stkObject.ClassName == "Radar":
    #if stkObject.ClassName == "Receiver":
    #if stkObject.ClassName == "Satellite":
    #if stkObject.ClassName == "Scenario":
    #if stkObject.ClassName == "Sensor":
    #if stkObject.ClassName == "Ship":
    #if stkObject.ClassName == "Star":
    #if stkObject.ClassName == "Target":
    #if stkObject.ClassName == "Transmitter":
    #if stkObject.ClassName == "Figure of Merit":
    #if stkObject.ClassName == "Root":
    #if stkObject.ClassName == "Access":
    #if stkObject.ClassName == "Coverage":
    #if stkObject.ClassName == "Attitude Figure of Merit":
    #if stkObject.ClassName == "Submarine":
    #if stkObject.ClassName == "Antenna":
    #if stkObject.ClassName == "Place":
    #if stkObject.ClassName == "Volumetric":
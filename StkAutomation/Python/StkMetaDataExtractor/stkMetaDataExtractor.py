import json
import sys

import win32com.client
# Start new instance of STK
uiApplication = win32com.client.Dispatch('STK12.Application')
uiApplication.Visible = False


# Get our IAgStkObjectRoot interface
root = uiApplication.Personality2 

if sys.argv[1].endswith('.vdf'):
    root.LoadVDF(sys.argv[1], '')
elif sys.argv[1].endswith('.sc'):
    root.LoadScenario(sys.argv[1])
else:
    print(sys.argv[1] + ' is not a recognized scenario file')
    exit

print(root.CurrentScenario.InstanceName) 
print("Total number of objects: {0}".format(len(root.CurrentScenario.Children)))

data = {}


data['ObjectsByType'] = {}
data['ObjectsByType']['Scenario'] = {'Name': root.CurrentScenario.InstanceName,'Class':root.CurrentScenario.ClassName}

for stkObject in root.CurrentScenario.Children:
    print(stkObject.ClassName + " " + stkObject.InstanceName)  
    props = {'Name': stkObject.InstanceName,'Class':stkObject.ClassName}

    if stkObject.ClassName not in data['ObjectsByType']:
        data['ObjectsByType'][stkObject.ClassName] = {}
    


    if stkObject.ClassName == "Facility":
        positionArray = stkObject.Position.QueryPlanetocentric()
        props['Position'] = {'latitude' : positionArray[0], 'longitude':positionArray[1], 'altitude': positionArray[2]}
        print('Position: {0:3.2f} {1:3.2f}'.format(positionArray[0], positionArray[1]))

    data['ObjectsByType'][stkObject.ClassName].append(props)    
    data['ObjectsByType'][stkObject.ClassName]['Counter'] = data['ObjectsByType'][stkObject.ClassName].Count()

print("Writing to " + sys.argv[2])
with open(sys.argv[2], 'w') as outfile:
    json.dump(data, outfile)

    
    #if stkObject.ClassName == "Aircraft":
    #if stkObject.ClassName == "AdvCAT":
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
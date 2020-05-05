import json
import sys

def SummarizeStkObject(stkObject):
    print("Summarizing " + stkObject.Path)
    # Create the Properties object to hold object information, specifically Name and Class
    props = {'Name': stkObject.InstanceName,'Class':stkObject.ClassName}
    
    # Capture any metadata provided on an object
    for key in stkObject.Metadata.Keys:
        props[key] = stkObject.Metadata[key]

    # For each type of object, allow for unique information to be captured
    if stkObject.ClassName == "Facility":
        positionArray = stkObject.Position.QueryPlanetocentric()
        props['Position'] = {'latitude' : positionArray[0], 'longitude':positionArray[1], 'altitude': positionArray[2]}
        #print('Position: {0:3.2f} {1:3.2f}'.format(positionArray[0], positionArray[1]))

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

    # Determine if it is necessary to iterate through the potential child objects
    if stkObject.HasChildren:
        props['Children'] = {} 
        
        for stkChild in stkObject.Children:
            if stkChild.ClassName not in props['Children']:
                props['Children'][stkChild.ClassName] = []
            # Summarize the child objects
            props['Children'][stkChild.ClassName].append(SummarizeStkObject(stkChild))
    # Return the summary
    return props

# Create a new instance of STK Engine (STK X)
import comtypes
from comtypes.client import CreateObject
stkXApplication = CreateObject('STKX12.Application')
stkXApplication.NoGraphics = True
root = CreateObject('AgStkObjects12.AgStkObjectRoot')

# Determine if the scenario to be loaded is a vdf or sc file and laod as appropriate
if sys.argv[1].endswith('.vdf'):
    root.LoadVDF(sys.argv[1], '')
elif sys.argv[1].endswith('.sc'):
    root.LoadScenario(sys.argv[1])
else:
    print(sys.argv[1] + ' is not a recognized scenario file')
    exit

# Get the summary of the scenario
summary = SummarizeStkObject(root.CurrentScenario)

print("Writing to " + sys.argv[2])
with open(sys.argv[2], 'w') as outfile:
    json.dump(summary, outfile)

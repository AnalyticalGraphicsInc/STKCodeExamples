import os
import sys


def dict2xml(d, root_node=None):
    wrap = False if not root_node or isinstance(d, list) else True
    root = "objects" if root_node is None else root_node
    root_singular = root[:-1] if "s" == root[-1] and root_node is None else root
    xml = ""
    children = []

    if isinstance(d, dict):
        for key, value in dict.items(d):
            if isinstance(value, dict):
                children.append(dict2xml(value, key))
            elif isinstance(value, list):
                children.append(dict2xml(value, key))
            else:
                xml = xml + " " + key + '="' + str(value) + '"'
    else:
        for value in d:
            children.append(dict2xml(value, root_singular))

    end_tag = ">" if 0 < len(children) else "/>"

    if wrap or isinstance(d, dict):
        xml = "<" + root + xml + end_tag

    if 0 < len(children):
        for child in children:
            xml = xml + child

        if wrap or isinstance(d, dict):
            xml = xml + "</" + root + ">"

    return xml


def SummarizeStkObject(stkObject):
    # Create the Properties object to hold object information, specifically Name and Class
    props = {"Name": stkObject.InstanceName}

    if stkObject.ShortDescription:
        props["ShortDescription"] = stkObject.ShortDescription
    if stkObject.LongDescription:
        props["LongDescription"] = stkObject.LongDescription

    # Capture any metadata provided on an object
    for key in stkObject.Metadata.Keys:
        props[key] = stkObject.Metadata[key]

    # For each type of object, allow for unique information to be captured
    if (
        stkObject.ClassName == "Facility"
        or stkObject.ClassName == "Target"
        or stkObject.ClassName == "Place"
    ):
        positionArray = stkObject.Position.QueryPlanetocentric()
        props["Latitude"] = positionArray[0]
        props["Longitude"] = positionArray[1]
        props["Altitude"] = positionArray[2]

    elif stkObject.ClassName == "Sensor":
        patternType = stkObject.PatternType
        patternTypes = [
            "ComplexConic",
            "Custom",
            "PHalfPower",
            "Rectangular",
            "SAR",
            "SimpleConic",
            "EOIR",
        ]
        props["PatternType"] = patternTypes[patternType]

    elif stkObject.ClassName == "Antenna":
        antennaType = stkObject.Model.Name
        props["AntennaType"] = antennaType

    elif stkObject.ClassName == "Radar":
        radarType = stkObject.Model.Name
        props["RadarType"] = radarType

    elif stkObject.ClassName == "Receiver":
        receiverType = stkObject.Model.Name
        props["ReceiverType"] = receiverType

    elif stkObject.ClassName == "Transmitter":
        transmitterType = stkObject.Model.Name
        props["TransmitterType"] = transmitterType

    elif stkObject.ClassName == "Satellite":
        possiblePropagator = stkObject.PropagatorSupportedTypes
        for propagatorType in possiblePropagator:
            if propagatorType[0] == stkObject.PropagatorType:
                props["PropagatorType"] = propagatorType[1]

    elif stkObject.ClassName == "AreaTarget":
        centroidPosition = stkObject.Position.QueryPlanetocentricArray()
        props["CentroidLatitude"] = centroidPosition[0]
        props["CentroidLongitude"] = centroidPosition[1]
        props["CentroidAltitude"] = centroidPosition[2]

    elif stkObject.ClassName == "CoverageDefinition":
        gridClasses = [
            "eGridClassUnknown",
            "eGridClassAircraft",
            "eGridClassFacility",
            "eGridClassRadar",
            "eGridClassReceiver",
            "eGridClassSatellite",
            "eGridClassSubmarine",
            "eGridClassTarget",
            "eGridClassTransmitter",
            "eGridClassGroundVehicle",
            "eGridClassShip",
            "eGridClassPlace",
            "eGridClassSensor",
        ]
        gridClass = stkObject.PointDefinition.GridClass
        props["GridClass"] = gridClass
        props["GridClassName"] = gridClasses[gridClass + 1]

    elif stkObject.ClassName == "Missile":
        possibleTrajectory = stkObject.TrajectorySupportedTypes
        usedTrajectory = stkObject.TrajectoryType
        for trajectory in possibleTrajectory:
            if trajectory[0] == usedTrajectory:
                props["TrajectoryType"] = trajectory[1]

    elif stkObject.ClassName == "Planet":
        centralBody = stkObject.PositionSourceData.CentralBody
        props["CentralBody"] = centralBody

    elif stkObject.ClassName == "Chain":
        chainobjects = stkObject.Objects
        props["NumberOfLinks"] = chainobjects.Count

    elif stkObject.ClassName == "Constellation":
        constellationobjects = stkObject.Objects
        props["NumberOfItems"] = constellationobjects.Count

    # Determine if it is necessary to iterate through the potential child objects
    if stkObject.HasChildren:
        props["Children"] = {}

        for stkChild in stkObject.Children:
            if stkChild.ClassName not in props["Children"]:
                props["Children"][stkChild.ClassName] = []
            # Summarize the child objects
            try:
                childSummary = SummarizeStkObject(stkChild)
                props["Children"][stkChild.ClassName].append(childSummary)
            except Exception:
                print(
                    "ERROR: An exception occurred summarizing " + stkObject.InstanceName
                )

    # Return the summary
    return props


inputScenarioPath = sys.argv[1]
outputSummaryDirectory = sys.argv[2]
print("Launching STK")
# Get reference to running STK instance using win32com
from win32com.client import Dispatch

uiApplication = Dispatch("STK12.Application")
uiApplication.Visible = False

# Get our IAgStkObjectRoot interface
root = uiApplication.Personality2

print("Loading scenario: " + inputScenarioPath)
# Determine if the scenario to be loaded is a vdf or sc file and laod as appropriate
if inputScenarioPath.endswith(".vdf"):
    root.LoadVDF(inputScenarioPath, "")
elif inputScenarioPath.endswith(".sc"):
    root.LoadScenario(inputScenarioPath)
else:
    print("ERROR: " + inputScenarioPath + " is not a recognized scenario file")
    exit

# Export a CZML preview from STK
outputCzmlPath = os.path.join(
    outputSummaryDirectory, root.CurrentScenario.InstanceName + ".czml"
)
cmd = 'ExportCZML * "' + outputCzmlPath + '" http://assets.agi.com/models/'
try:
    print("Exporting CZML to " + outputCzmlPath)
    root.ExecuteCommand(cmd)
except Exception:
    print("ERROR: An exception occurred generating CZML")

# Generating XML
# Get the summary of the scenario
try:
    print("Summarizing STK scenario")
    summary = SummarizeStkObject(root.CurrentScenario)

    outputXmlSummaryPath = os.path.join(
        outputSummaryDirectory, root.CurrentScenario.InstanceName + ".xml"
    )

    print("Writing XML summary to " + outputXmlSummaryPath)
    xmlSummary = dict2xml(summary)
    with open(outputXmlSummaryPath, "w") as f:
        f.write(xmlSummary)
except Exception:
    print("ERROR: An exception occurred generating XML")

# Generating Json
# outputJsonSummaryPath = os.path.join(outputSummaryDirectory, root.CurrentScenario.InstanceName + ".json")
# with open(outputJsonSummaryPath, 'w') as outfile:
#     json.dump(summary, outfile)

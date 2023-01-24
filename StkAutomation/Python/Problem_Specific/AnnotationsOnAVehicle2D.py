#
# Add Custom Annotations for a Vehicle at Locations that Correspond to Desired Times
#
# This Script Adds 2D and 3D Annotations - ensure that a scenario is open
#
# Only edit Main
#

# Definitions


def Annotations(
    STKVersion, ObjectName, ObjectType, accuracy, Events, EventNames, EventColors
):

    numEvents = len(Events)

    from comtypes.client import GetActiveObject

    uiApplication = GetActiveObject("STK{}.Application".format(STKVersion))
    root = uiApplication.Personality2

    from comtypes.gen import STKObjects

    scenario = root.CurrentScenario
    uiApplication.Visible = True
    uiApplication.UserControl = True
    scenarioObj = scenario.QueryInterface(STKObjects.IAgScenario)

    # This is specifically for the object type Launch Vehicle, but this can be changed to the
    # desired object
    Object = scenario.Children.Item(ObjectName)
    ObjectType = "STKObjects.IAg{}".format(ObjectType)
    # ObjectObj = Object.QueryInterface(eval(ObjectType))

    # Retrieve lat/lon/alt of object at desired times
    LLA = Object.DataProviders("LLA State")
    LLA2 = LLA.QueryInterface(STKObjects.IAgDataProviderGroup)
    LLAFixed = LLA2.Group.Item("Fixed")
    LLATV = LLAFixed.QueryInterface(STKObjects.IAgDataPrvTimeVar)
    LLAAlmost = LLATV.Exec(scenarioObj.StartTime, scenarioObj.StopTime, accuracy)
    Times = list(LLAAlmost.DataSets.Item(0).GetValues())
    Lon = list(LLAAlmost.DataSets.Item(1).GetValues())
    Lat = list(LLAAlmost.DataSets.Item(2).GetValues())
    Alt = list(LLAAlmost.DataSets.Item(3).GetValues())

    # clear old annotations
    root.ExecuteCommand("MapAnnotation * Delete All")
    root.ExecuteCommand("VO * Annotation Delete AllAnnotations Text")

    # for every event, grab lat, lon, alt and put annotation in STK
    for i in range(numEvents):
        # grab index that corresponds to time of event
        ind = Times.index(Events[i])

        Latitude = str(Lat[ind])
        Longitude = str(Lon[ind])
        Altitude = str(Alt[ind])

        # put annotation in 2D graphics
        cmd = (
            "MapAnnotation * Add "
            + str(i + 1)
            + ' Text String "'
            + EventNames[i]
            + '" Color '
            + EventColors[i]
            + " Position "
            + Latitude
            + " "
            + Longitude
        )

        root.ExecuteCommand(cmd)

        # put annotation in 3D graphics
        cmd2 = (
            "VO * Annotation Add "
            + str(i + 1)
            + ' Text String "'
            + EventNames[i]
            + '" Coord LatLon Position '
            + Latitude
            + " "
            + Longitude
            + Altitude
            + " Color "
            + EventColors[i]
        )

        root.ExecuteCommand(cmd2)


# Main

# List version of STK (whole number), name of object
STKVersion = "12"
ObjectName = "Satellite1"

# object type - available options are:
# LaunchVehicle  Missile  GroundVehicle  Aircraft  Satellite  Ship
ObjectType = "Satellite"

# Time Step - round the times used to this level of accuracy
# Lower time step will increase accuracy but could increase run time
accuracy = 0.1

# Enter Times of Events here - Round to accuracy above, but keep 9 sig figs
# Use format shown below (UTCG) and add more events if desired
Events = [
    "6 Jul 2020 16:00:00.000000000",
    "6 Jul 2020 16:01:55.900000000",
    "6 Jul 2020 16:02:27.400000000",
    "6 Jul 2020 16:03:50.400000000",
    "6 Jul 2020 16:03:51.400000000",
    "6 Jul 2020 16:03:52.400000000",
    "6 Jul 2020 16:03:53.400000000",
    "6 Jul 2020 16:03:54.400000000",
    "6 Jul 2020 16:03:55.400000000",
    "6 Jul 2020 16:03:56.400000000",
    "6 Jul 2020 16:03:57.400000000",
    "6 Jul 2020 16:03:58.400000000",
    "6 Jul 2020 16:07:00.000000000",
    "6 Jul 2020 16:10:00.000000000",
    "6 Jul 2020 16:09:00.000000000",
]

# List name for each event (in chronological order)
EventNames = [
    "Event 1",
    "Event 2",
    "Event 3",
    "Event 4",
    "Event 5",
    "Event 6",
    "Event 7",
    "Event 8",
    "Event 9",
    "Event 10",
    "Event 11",
    "Event 12",
    "Event 13",
    "Event 14",
    "Event 15",
]

# list colors for each event (in chronological order)
# to use RGB color values, use strings in this form: "%RRRGGGBBB"
EventColors = [
    "yellow",
    "green",
    "red",
    "cyan",
    "cyan",
    "cyan",
    "cyan",
    "cyan",
    "cyan",
    "cyan",
    "cyan",
    "cyan",
    "red",
    "yellow",
    "purple",
]

Annotations(
    STKVersion, ObjectName, ObjectType, accuracy, Events, EventNames, EventColors
)

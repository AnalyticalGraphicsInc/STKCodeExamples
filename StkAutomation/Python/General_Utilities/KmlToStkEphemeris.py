"""
Created on Thu Aug 22 2019 11:18:32

@author: Noah Ingwersen

This script reads KML flight log files from ForeFlight (or other flight software) and converts them to
ephemeris files to be used by STK.

Dependencies:
 - Beautiful Soup 4
 - lxml
"""

# Imports
from bs4 import BeautifulSoup

# ---- User Inputs  ------------------------------------------#

kmlFile = r"C:\temp\ForeFlightExampleFile.kml"

# ------------------------------------------------------------#

# Open the kml file and parse as xml
kml = open(kmlFile, "r")
kmlParser = BeautifulSoup(kml, "xml")

# Find coordinates and times
unformattedCoordinates = kmlParser.findAll("gx:coord")
unformattedTimes = kmlParser.findAll("when")

# Start ephemeris file
ephemerisFile = open(kmlFile.split(".")[0] + ".e", "w+")
ephemerisFile.write("stk.v.10.0\n")
ephemerisFile.write("BEGIN Ephemeris\n")
ephemerisFile.write("TimeFormat ISO-YMD\n")
ephemerisFile.write("InterpolationMethod GreatArcMSL\n")
ephemerisFile.write("InterpolationSamplesM1 1\n")
ephemerisFile.write(
    "NumberOfEphemerisPoints " + str(len(unformattedCoordinates)) + "\n"
)
ephemerisFile.write("EphemerisMSLLLATimePos\n")
ephemerisFile.write("\n")

# Create a line of Time(ISO-YMD), Latitude, Longitude and Altitude (LLA) for each point
allTimeAndLLA = []
for i in range(len(unformattedCoordinates)):
    time = unformattedTimes[i]
    coordinates = unformattedCoordinates[i]
    LLA = coordinates.string.split(" ")
    timeAndLLA = [time.string.replace("Z", "")] + LLA
    # Write that line to the ephemeris file
    ephemerisFile.write(
        "\t{0} {1} {2} {3}\n".format(
            timeAndLLA[0], timeAndLLA[2], timeAndLLA[1], timeAndLLA[3]
        )
    )
    allTimeAndLLA.append(timeAndLLA)

# Finish the ephemeris file
ephemerisFile.write("\nEND Ephemeris")
ephemerisFile.close()

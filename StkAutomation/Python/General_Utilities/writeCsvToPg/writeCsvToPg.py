# -*- coding: utf-8 -*-
"""
This is a standalone script that writes a csv with columns Time in UTCG, Lat,
Lon, and Alt to a great arc propagator file (.pg). Inputs of vehicle ID and
full csv path are prompted from the user. Output is a .pg file in the same
directory that can be imported into any STK object with a great arc propagator.
"""
from agi.stk12.stkengine import STKEngine
from agi.stk12.stkutil import *
import csv

# Set up STK Engine for conversion utility to EpSec
stk = STKEngine.StartApplication(noGraphics=True)
root = stk.NewObjectRoot()
converter = root.ConversionUtility
epoch = converter.ConvertDate('EpSec', 'UTCG', '0')

# Example vehicle ID: GV1
vehicleID = input('Input vehicle ID: ')
# Example path: C:\LLA.csv
csvPath = input('Input full csv path: ')

# Read input file
file = open(csvPath)
inputData = list(csv.reader(file))
file.close()

# Create output file
outputPath = csvPath.rsplit('\\', 1)[0]
ephem = open(f'{outputPath}\\{vehicleID}.pg', 'w')

# Write header
numPoints = len(inputData) - 1
header = ['stk.v.5.0\n',
          'BEGIN GreatArc\n',
          'Method DetVelFromTime\n',
          f'TimeOfFirstWaypoint {epoch}\n',
          'ArcGranularity 0.01745\n',
          'AltRef Terrain\n',
          f'NumberOfWaypoints {numPoints}\n',
          'BEGIN Waypoints\n']
ephem.writelines(header)

# Convert time to EpSec and write data points
line_count = 0
for row in inputData:
    if line_count != 0:
        epSecTime = converter.ConvertDate('UTCG', 'EpSec', row[0])
        ephem.write(f'{epSecTime}\t{row[1]}\t{row[2]}\t0\t0\t0\n')
    line_count += 1

# Write footer
footer = ['END Waypoints\n',
          'END GreatArc\n']
ephem.writelines(footer)

# Close out
ephem.close()
stk.ShutDown()
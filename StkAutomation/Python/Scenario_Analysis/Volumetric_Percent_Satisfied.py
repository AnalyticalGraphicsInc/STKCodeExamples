"""
Created by: Noah Ingwersen
Last Updated: 12/7/2021
"""

# Imports
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
import numpy as np

######## USER INPUTS ################
volumetricName = "Volumetric1"
satisfactionValue = 1

# Set stopTime to "None" to compute a single point in time.
# Set startTime and stopTime to "None" to use scenario interval
startTime = None  # UTCG
stopTime = "8 Dec 2021 16:00:00.000"  # UTCG
# stopTime = None
timeStep = 7250  # sec

includeStopTime = True

######### ANALYSIS ###################
# Connect to STK
stk = STKDesktop.AttachToApplication()
root = stk.Root
scenario = AgScenario(root.CurrentScenario)

# Set date units to EpSec for easier looping
root.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec")

# Determine analysis times
if stopTime == None:
    if startTime == None:
        startTime = scenario.StartTime
        stopTime = scenario.StopTime
    else:
        startTime = float(
            root.ConversionUtility.ConvertDate("UTCG", "EpSec", startTime)
        )
        stopTime = startTime
        # Analysis time array would be empty if includeStopTime is False
        includeStopTime = True
else:
    startTime = float(root.ConversionUtility.ConvertDate("UTCG", "EpSec", startTime))
    stopTime = float(root.ConversionUtility.ConvertDate("UTCG", "EpSec", stopTime))

analysisTimes = np.arange(startTime, stopTime, timeStep)
if includeStopTime:
    analysisTimes = np.append(analysisTimes, stopTime)

# Compute volumetric
volumetricPath = "Volumetric/" + volumetricName
volumetric = AgVolumetric(root.GetObjectFromPath(volumetricPath))
volumetric.Compute()

# Get volumetric's volume
volumeDataProvider = volumetric.DataProviders.GetDataPrvTimeVarFromPath(
    "Satisfaction Volume"
)
volumeResults = volumeDataProvider.Exec(scenario.StartTime, scenario.StartTime, 1)
volume = (volumeResults.DataSets.GetDataSetByName("Satisfied Volume").GetValues())[0]

allPercentSatisfied = []
allSatisfiedVolume = []
for time in analysisTimes:
    # Get volumetric values
    valueDataProvider = volumetric.DataProviders.GetDataPrvFixedFromPath(
        "Volumetric Values at Time"
    )
    # Pre data time must be UTCG string
    valueDataProvider.PreData = root.ConversionUtility.ConvertDate(
        "EpSec", "UTCG", str(time)
    )
    results = valueDataProvider.Exec()

    data = results.DataSets.GetDataSetByName("Value").GetValues()

    # Find points that are satisfied based on user input value
    satisfiedPoints = [
        float(point) for point in data if float(point) >= satisfactionValue
    ]
    percentSatisfied = len(satisfiedPoints) / len(data)
    satisfiedVolume = volume * percentSatisfied

    print(f'Time: {root.ConversionUtility.ConvertDate("EpSec", "UTCG", str(time))}')
    print(f"Percent Satisfied: {(percentSatisfied * 100):.2f}%")
    print(f"Satisfied Volume {satisfiedVolume:.2f} km^3\n")

    allPercentSatisfied.append(percentSatisfied)
    allSatisfiedVolume.append(satisfiedVolume)

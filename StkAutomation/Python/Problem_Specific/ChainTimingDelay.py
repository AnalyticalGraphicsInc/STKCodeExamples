"""
Complete chain access in STK means that each link in the chain simultaneously has access.
This script allows a maximum delay between target/asset access and the rest of the chain.
This is useful for scenarios such as an imaging system with onboard storage, where a target
can be imaged, then downlinked at a later time specified by the maximum delay.
"""

import os
from dataclasses import dataclass

from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *

# Globals
# Specify type for intellisense
STK = None
ROOT: AgStkObjectRoot = None
SCENARIO: AgScenario = None

"""""" """""" """""" """
   USER INPUTS   
""" """""" """""" """"""
# Assets and target can be a single object or constellation
ASSET_PATH = "Satellite/ImageSat/Sensor/Imager"
TARGET_PATH = "Place/Target"
MAX_DELAY = 600  # sec
# Set to true to create copies of any objects before they are modified
PRESERVE_OBJECTS = True


"""""" """""" """""" """
   CLASSES
""" """""" """""" """"""


@dataclass
class AccessInterval:
    """
    Contains the start and stop time of a single access interval with associated units.
    """

    start: float
    stop: float
    unit: str


class Strand:
    """
    Represents a single access link (strand) in a chain between two unique objects.
    """

    # Enums
    eToObject = 0
    eFromObject = 1
    eBoth = 2

    def __init__(
        self,
        fromObject: IAgStkObject,
        toObject: IAgStkObject,
        intervals: list[AccessInterval],
    ):
        self.fromObject = fromObject
        self.toObject = toObject
        self.intervals = intervals

    def writeIntervalFile(self, filepath: str, delay: float = 0):
        """
        Writes an interval file (.int) adding an optional delay (in sec) to the end of each access interval
        """
        with open(filepath, "w") as file:
            file.write(
                "stk.v.8.0\n"
                "BEGIN IntervalList\n"
                "\tDateUnitAbrv UTCG\n"
                "BEGIN Intervals\n"
            )

            for interval in self.intervals:
                startTime = ROOT.ConversionUtility.ConvertDate(
                    interval.unit, "UTCG", str(interval.start)
                )
                stopTime = ROOT.ConversionUtility.ConvertDate(
                    interval.unit, "UTCG", str(interval.stop + delay)
                )
                file.write(f'\t"{startTime}" "{stopTime}"\n')

            file.write("END Intervals\n" "END IntervalList")

    def removeConstraints(self, stkObject: int, preserveObjects: bool):
        """
        Removes the line of sight and field of view (if applicable) constraints for the given object
        """
        # Add objects chosen to list
        objects: list[IAgStkObject] = []
        if stkObject == self.eFromObject or stkObject == self.eBoth:
            objects.append(self.fromObject)
        if stkObject == self.eToObject or stkObject == self.eBoth:
            objects.append(self.toObject)

        for obj in objects:
            if preserveObjects:
                # Copy the object before we make any changes. Other strands may contain this object,
                # so it may have already been duplicated
                if not ROOT.ObjectExists(f"{obj.Path}_Original"):
                    obj.CopyObject(f"{obj.InstanceName}_Original")

            obj.AccessConstraints.RemoveConstraint(
                AgEAccessConstraints.eCstrLineOfSight
            )

            try:
                obj.AccessConstraints.RemoveConstraint(
                    AgEAccessConstraints.eCstrFieldOfView
                )
            except:
                # FOV constraint doesn't exist for this object
                pass

    def applyIntervalFile(self, filename: str, stkObject: IAgStkObject):
        """
        Set an interval constraint to constrain the object to accesses in a file
        """
        intervalConstraint: AgAccessCnstrIntervals = (
            stkObject.AccessConstraints.AddConstraint(
                AgEAccessConstraints.eCstrIntervals
            )
        )
        intervalConstraint.ActionType = AgEActionType.eActionInclude
        intervalConstraint.Intervals.LoadIntervals(filename)


"""""" """""" """""" """
   FUNCTIONS
""" """""" """""" """"""


def main(preserveObjects: bool = False):
    """
    Entry point for the script. Set preserveObjects=True to duplicate any objects before they are modifed
    """
    global STK, ROOT, SCENARIO
    STK = STKDesktop.AttachToApplication()
    ROOT = STK.Root
    SCENARIO = ROOT.CurrentScenario

    accesses = computeAccesses()
    for strand in accesses:
        extendAccesses(strand, preserveObjects)


def computeAccesses() -> list[Strand]:
    """
    Creates a chain to compute access between 1 or more targets and assets, returns all access intervals sorted by strand.
    """
    # Create chain to compute access, in case asset or target is a constellation
    try:
        ROOT.GetObjectFromPath("Chain/AssetToTarget").Unload()
    except:
        pass

    chain: AgChain = SCENARIO.Children.New(AgESTKObjectType.eChain, "AssetToTarget")
    chain.Objects.Add(ASSET_PATH)
    chain.Objects.Add(TARGET_PATH)
    chain.ComputeAccess()

    accessIntervals = getAccessIntervals(chain)
    chain.Unload()

    return accessIntervals


def getAccessIntervals(chain: AgChain) -> list[Strand]:
    """
    Create unique strands from Strand Access data provider and sort access intervals
    """
    # Get each individual strand for the entire scenario
    ROOT.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec")
    strandDataProvider = chain.DataProviders.GetDataPrvIntervalFromPath("Strand Access")
    strandAccessResults = strandDataProvider.Exec(SCENARIO.StartTime, SCENARIO.StopTime)

    strandNames = strandAccessResults.DataSets.GetDataSetByName(
        "Strand Name (Long)"
    ).GetValues()
    startTimes = strandAccessResults.DataSets.GetDataSetByName("Start Time").GetValues()
    stopTimes = strandAccessResults.DataSets.GetDataSetByName("Stop Time").GetValues()

    # Add unique strands to a list
    strands: list[Strand] = []
    unit = ROOT.UnitPreferences.GetCurrentUnitAbbrv("DateFormat")
    for i, strandName in enumerate(strandNames):
        splitPaths = strandName.split(" to ")
        object1 = ROOT.GetObjectFromPath(splitPaths[0])
        object2 = ROOT.GetObjectFromPath(splitPaths[1])

        startTime = startTimes[i]
        stopTime = stopTimes[i]
        interval = AccessInterval(startTime, stopTime, unit)

        # If the strand already exists, add the access interval to it
        strandExists = False
        for strand in strands:
            if strand.fromObject == object1 and strand.toObject == object2:
                strandExists = True
                strand.intervals.append(interval)

        # Otherwise create a new strand object
        if not strandExists:
            strands.append(Strand(object1, object2, [interval]))

    return strands


def extendAccesses(strand: Strand, preserveObjects: bool):
    """
    Remove standard access constraints and use an interval file to ensure that the accesses end after original access stop plus user delay
    """
    scenarioDirectory = ROOT.ExecuteCommand("GetDirectory / Scenario")[0]
    filepath = os.path.join(
        scenarioDirectory,
        f"{strand.fromObject.InstanceName}-to-{strand.toObject.InstanceName}Intervals.int",
    )
    strand.removeConstraints(strand.eBoth, preserveObjects)
    strand.writeIntervalFile(filepath, delay=MAX_DELAY)
    strand.applyIntervalFile(filepath, strand.toObject)


if __name__ == "__main__":
    main(preserveObjects=PRESERVE_OBJECTS)

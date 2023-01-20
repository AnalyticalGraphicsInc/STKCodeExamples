"""-----------------------------------------------------------------------------------------------------------"""
"""             Script to utilize Access Time per orbit allotment ("Duty Cycle Allottment")                   """
"""-----------------------------------------------------------------------------------------------------------"""
"""                                                                                                           """
""" This script utilizes a Duty Cycle allotment per orbit to determine access time to area targets. The script"""
""" can utilize a named "special" target as a primary target and will split the remaining time evenly amongst """
""" the remaining targets.                                                                                    """
"""                                                                                                           """
"""-----------------------------------------------------------------------------------------------------------"""

try:
    from agi.stk12.stkdesktop import STKDesktop
    from agi.stk12.stkobjects import *
    from agi.stk12.vgt import *
except:
    print(
        "Failed to import stk modules. Make sure you have installed the STK Python API wheel \
        (agi.stk<..ver..>-py3-none-any.whl) from the STK Install bin directory"
    )


# ---------------------------------------------------------------------------------------------------------------#
# -------------------------------------------------Constants-----------------------------------------------------#
# ---------------------------------------------------------------------------------------------------------------#

# Scenario Info - Satellites are treated as independent entities - no collaboration on targeting
satNames = ["Satellite1", "Satellite2"]
sensorNames = ["Sensor1", "Sensor2"]

# Prioritize Targets (list targets in array with index that matches index of satellite that should target it)
prioritize = True
primaryTargetNames = ["AreaTarget3", "AreaTarget7"]

# Duty Cycle Allotted Time per satellite per orbit (min) - organize in same fashion as primary targets
dutyCycleTimes = [7, 7]


# ---------------------------------------------------------------------------------------------------------------#
# ---------------------------------------------------Main--------------------------------------------------------#
# ---------------------------------------------------------------------------------------------------------------#

# Get scenario
stk = STKDesktop.AttachToApplication()
stkRoot = stk.Root
scenario = stkRoot.CurrentScenario

# Change object model units to minutes to compute times
stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec")

for i in range(len(satNames)):

    dutyCycleTime = dutyCycleTimes[i] * 60

    # Get Satellite object
    sat = AgSatellite(scenario.Children.Item(satNames[i]))

    # Get Sensor object
    sens = sat.Children.Item(sensorNames[i])

    # Turn off temporal constraints previously made
    if (AgSensor(sens)).AccessConstraints.IsConstraintActive(
        AgEAccessConstraints.eCstrIntervals
    ):
        (AgSensor(sens)).AccessConstraints.RemoveConstraint(
            AgEAccessConstraints.eCstrIntervals
        )

    # Get Area Targets objects and their names
    areaTargets = scenario.Children.GetElements(AgESTKObjectType.eAreaTarget)
    ATs = []

    for areaTarget in areaTargets:
        ATs.append(areaTarget)

    ATNames = []

    for AT in ATs:
        ATNames.append(AgStkObject(AT).InstanceName)

    # If a primary target is enabled, specify its index
    if prioritize:
        primaryIndex = ATNames.index(primaryTargetNames[i])

    # Get inital Access between the sensor and each Area Target
    accesses = []
    for ATName in ATNames:
        accesses.append(sens.GetAccess(f"AreaTarget/{ATName}"))

    # Get times of crossing the ascending node using VGT (denotes separate orbits)
    satTimeArrays = sat.Vgt.EventArrays
    # Delete component if previously made
    try:
        satTimeArrays.Remove("AscendingNodeCrossing")
    except:
        pass
    # Create condition component
    satTAFact = satTimeArrays.Factory
    ascNodeCndtnVGT = sat.Vgt.Conditions.Item("AboveAscendingNode")
    ascNodeCrossVGT = AgCrdnEventArrayConditionCrossings(
        satTAFact.CreateEventArrayConditionCrossings(
            "AscendingNodeCrossing", "Time of satellite crossing ascending node, UTC."
        )
    )
    ascNodeCrossVGT.Condition = ascNodeCndtnVGT
    ascNodeCrossVGT.SatisfactionCrossing = (
        AgECrdnSatisfactionCrossing.eCrdnSatisfactionCrossingIn
    )
    # Get times
    ascNodeCrosses = ascNodeCrossVGT.FindTimes().Times

    # Create time interval for each orbit
    satTimeInstants = sat.Vgt.Events
    satStartTime = satTimeInstants.Item("EphemerisStartTime").FindOccurrence().Epoch
    satStopTime = satTimeInstants.Item("EphemerisStopTime").FindOccurrence().Epoch
    satIntervals = sat.Vgt.EventIntervals
    satIntervalFact = satIntervals.Factory
    i = 0
    orbitInts = []
    for ascNodeCross in ascNodeCrosses:
        try:
            satIntervals.Remove(f"Cross{i}")
        except:
            pass
        orbitInt = AgCrdnEventIntervalFixed(
            satIntervalFact.CreateEventIntervalFixed(
                f"Cross{i}", f"Interval for Revolution {i}"
            )
        )
        if i == 0:
            orbitInt.SetInterval(satStartTime, ascNodeCross)
        else:
            orbitInt.SetInterval(ascNodeCrosses[i - 1], ascNodeCross)
        orbitInts.append(orbitInt)
        i = i + 1
        if i == len(ascNodeCrosses):
            try:
                satIntervals.Remove(f"Cross_Last")
            except:
                pass
            lastOrbitInt = AgCrdnEventIntervalFixed(
                satIntervalFact.CreateEventIntervalFixed(
                    f"Cross_Last", f"Interval for Last Rev"
                )
            )
            lastOrbitInt.SetInterval(ascNodeCross, satStopTime)
            orbitInts.append(lastOrbitInt)

    # For each orbit interval and each Area Target, create a combined interval list that requires that access be had and that it is during that orbit interval
    satIntervalLists = sat.Vgt.EventIntervalLists
    satILFact = satIntervalLists.Factory
    dutyCycleInts = []
    j = 0
    for orbit in orbitInts:
        dcTime = dutyCycleTime
        ATperOrbit = []
        accessIntsInOrbit = []
        priorityInts = []
        k = 0
        for access in accesses:
            try:
                satIntervalLists.Remove(f"Orbit-{j}_{ATNames[k]}")
            except:
                pass
            accessInt = access.Vgt.EventIntervalLists.Item("AccessIntervals")
            merged = AgCrdnEventIntervalListMerged(
                satILFact.CreateEventIntervalListMerged(
                    f"Orbit-{j}_{ATNames[k]}",
                    f"Overlap between Orbit Interval {j} and Area Target {ATNames[k]}",
                )
            )
            merged.MergeOperation = (
                AgECrdnEventListMergeOperation.eCrdnEventListMergeOperationAND
            )
            merged.SetIntervalListA(accessInt)
            merged.SetIntervalB(orbit)

            numInts = AgCrdnEventIntervalList(merged).FindIntervals().Intervals.Count
            if numInts == 0:
                ATperOrbit.append(None)
            else:
                ATperOrbit.append(merged)
            k = k + 1

        # Grab every access interval in this orbit as a separate interval
        y = 0
        for ATIntList in ATperOrbit:
            intsLeft = True
            if prioritize:
                if y == primaryIndex:
                    z = 0
                    while intsLeft:
                        try:
                            priorityInts.append(
                                AgCrdnEventIntervalList(ATIntList)
                                .FindIntervals()
                                .Intervals.Item(z)
                            )
                            z = z + 1
                        except:
                            intsLeft = False
            x = 0
            while intsLeft:
                try:
                    accessIntsInOrbit.append(
                        (AgCrdnEventIntervalList(ATIntList))
                        .FindIntervals()
                        .Intervals.Item(x)
                    )
                    x = x + 1
                except:
                    intsLeft = False
            y = y + 1

        # Create intervals for this orbit to add to sensor's availability times (penultimate representation of Duty Cycle)
        numIntsForOrbit = len(accessIntsInOrbit) + len(priorityInts)
        for priorityInt in priorityInts:
            start = float(priorityInt.Start)
            stop = float(priorityInt.Stop)
            minutesInPass = stop - start
            if dcTime > minutesInPass:
                dutyCycleInts.append(priorityInt)
                dcTime = dcTime - minutesInPass
            else:
                try:
                    satIntervals.Remove("dcIntPriorityLast")
                except:
                    pass
                addInt = AgCrdnEventIntervalFixed(
                    satIntervalFact.CreateEventIntervalFixed(
                        "dcIntPriorityLast",
                        "Last interval in orbit due to DC Time and Priority target time.",
                    )
                )
                addInt.SetInterval(start, start + dcTime)
                dutyCycleInts.append(addInt)
                dcTime = 0

        # Divide duty cycle time left in revolution by number of access left
        if len(accessIntsInOrbit) == 0:
            pass
        else:
            timePerPassInOrbit = dcTime / len(accessIntsInOrbit)
            passNo = 1
            for normInt in accessIntsInOrbit:
                start = float(normInt.Start)
                stop = float(normInt.Stop)
                minutesInPass = stop - start
                if timePerPassInOrbit > minutesInPass:
                    dutyCycleInts.append(normInt)
                else:
                    try:
                        satIntervals.Remove(f"IntforPass{passNo}")
                    except:
                        pass
                    addInt = AgCrdnEventIntervalFixed(
                        satIntervalFact.CreateEventIntervalFixed(
                            f"IntforPass{passNo}",
                            "Int for Pass shortened by remaining duty cycle time",
                        )
                    )
                    addInt.SetInterval(start, start + timePerPassInOrbit)
                    dutyCycleInts.append(addInt)
                passNo = passNo + 1

        j = j + 1

    # Add each interval created as a temporal constraint for the sensor
    intConstraint = AgAccessCnstrIntervals(
        (AgSensor(sens)).AccessConstraints.AddConstraint(
            AgEAccessConstraints.eCstrIntervals
        )
    )
    intConstraint.ActionType = AgEActionType.eActionInclude
    intervalAdder = intConstraint.Intervals
    intervalAdder.RemoveAll()

    for intCnstr in dutyCycleInts:
        try:
            # If equal the interval is too short to be distinguished due to rounding (10e-7 accuracy)
            if intCnstr.Start == intCnstr.Stop:
                pass
            else:
                intervalAdder.Add(intCnstr.Start, intCnstr.Stop)
        except:
            # If equal the interval is too short to be distinguished due to rounding (10e-7 accuracy)
            if intCnstr.StartTime == intCnstr.StopTime:
                pass
            else:
                intervalAdder.Add(intCnstr.StartTime, intCnstr.StopTime)

    for ATName in ATNames:
        sens.GetAccess(f"AreaTarget/{ATName}")

# -*- coding: utf-8 -*-

# convertTleState This function will take all satellites in a scenario
# propagated with SGP4 and create a new satellite with the intital state of
# the TLE using the propagator specified

from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import AgESTKObjectType, AgEVePropagatorType

# Available Inputs: HPOP, J2, J4, TwoBody
targetPropagator = "HPOP"

# Find appropriate propagator enum
if targetPropagator == "HPOP":
    newPropagator = 0
elif targetPropagator == "J2":
    newPropagator = 1
elif targetPropagator == "J4":
    newPropagator = 2
else:
    newPropagator = 7

# Get reference to running STK instance, get root and scenario interfaces
stk = STKDesktop.AttachToApplication()
root = stk.Root
scenario = root.CurrentScenario

# Get all satellites in the scenario
sats = scenario.Children.GetElements(AgESTKObjectType.eSatellite)

for i in range(sats.Count):
    sat = sats.Item(i)
    if sat.PropagatorType == AgEVePropagatorType.ePropagatorSGP4:  # SGP4
        # Get ICRF Cartesian Position
        cartesianPosdp = sat.DataProviders.Item("Cartesian Position").Group.Item("ICRF")
        posResult = cartesianPosdp.ExecSingle(
            sat.Propagator.EphemerisInterval.GetStartEpoch().TimeInstant
        )
        x = posResult.DataSets.GetDataSetByName("x").GetValues()
        y = posResult.DataSets.GetDataSetByName("y").GetValues()
        z = posResult.DataSets.GetDataSetByName("z").GetValues()

        # Get ICRF Cartesian Velocity
        cartesianVeldp = sat.DataProviders.Item("Cartesian Velocity").Group.Item("ICRF")
        velResult = cartesianVeldp.ExecSingle(
            sat.Propagator.EphemerisInterval.GetStartEpoch().TimeInstant
        )
        vx = velResult.DataSets.GetDataSetByName("x").GetValues()
        vy = velResult.DataSets.GetDataSetByName("y").GetValues()
        vz = velResult.DataSets.GetDataSetByName("z").GetValues()

        # Create and load initial state to new satellite
        newSat = scenario.Children.New(
            AgESTKObjectType.eSatellite, sat.InstanceName + "_New"
        )
        newSat.SetPropagatorType(newPropagator)
        newSat.Propagator.InitialState.OrbitEpoch.SetExplicitTime(
            sat.Propagator.EphemerisInterval.GetStartEpoch().TimeInstant
        )
        newSat.Propagator.InitialState.Representation.AssignCartesian(
            11, x[0], y[0], z[0], vx[0], vy[0], vz[0]
        )  # ICRF

        newSat.Propagator.Propagate()

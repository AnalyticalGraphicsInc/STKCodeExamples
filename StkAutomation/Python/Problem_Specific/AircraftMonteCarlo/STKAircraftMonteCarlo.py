"""
MonteCarlo with STK

Description: This script sets the state of an aircraft in STK and computes the resulting
link budget.

Output: CSV with C/N
"""

import csv
import MonteCarloStateGenerator as mc
import SatelliteLookUpTable as tbl

# STK Imports
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.vgt import *

# STK Inputs
# SCENARIO_TIME_UTCG = "22 Aug 2025 18:30:00.000"  # UTCG
# SCENARIO_TIME_EPSEC = 0  # EpSec

# Monte Carlo Inputs
NUM_TRIALS = 100
PRINT_TO_CSV = True
OUTPUT_CSV = "output.csv"
TABLE_CSV = "satLookUpTable.csv"
SAVE_LOOKUP_CSV = True  # If running script for first time, set to True


def start_stk(scenarioPath):
    started_stk = False
    try:
        print("Trying to connect to STK")
        stk = STKDesktop.AttachToApplication()
        root = stk.Root
        checkEmpty = root.Children.Count
        if checkEmpty > 0:
            stk.Visible = True
            stk.UserControl = True
            scenario = AgScenario(root.CurrentScenario)
            return scenario, root, stk, started_stk
        else:
            pass
            print("Scenario path = " + scenarioPath)
            print("Error: scenario is null.")
            return None, root, stk, started_stk
    except Exception as e:
        print("Loading scenario...")
        stk = STKDesktop.StartApplication(visible=True, userControl=True)
        root = stk.Root
        root.LoadScenario(scenarioPath)
        scenario = root.CurrentScenario
        started_stk = True
        print(e)
        return scenario, root, stk, started_stk


def shutdown_stk(root, stk, scenarioPath, scenario):
    root.SaveScenario()
    root.CloseScenario()
    stk.Quit()
    stk.ShutDown()

    del root
    del stk


def set_aircraft_state(route, orientation, time, lat, lon, roll, pitch, heading, alt):
    # clear any waypoints already in the route
    route.Waypoints.RemoveAll()

    # create two waypoints with the same coordinates so that there is a flight path
    waypoint1 = route.Waypoints.Add()
    waypoint1.Latitude = float(lat)
    waypoint1.Longitude = float(lon)
    waypoint1.Altitude = float(alt)
    waypoint1.Time = float(time)

    waypoint2 = route.Waypoints.Add()
    waypoint2.Latitude = float(lat)
    waypoint2.Longitude = float(lon)
    waypoint2.Altitude = float(alt)
    waypoint2.Time = float(time + 1)

    route.Propagate()

    orientation.AssignYPRAngles(
        2, float(roll), float(pitch), float(heading)  # RPY sequence
    )
    return


def compute_link_budget(access, time):
    access.ClearAccess()

    # Compute C/N at the current scenario timestep with access object
    access.ComputeAccess()
    accessDP = access.DataProviders.Item("Link Information").ExecSingle(time)
    C_N = accessDP.DataSets.GetDataSetByName("C/N").GetValues()[0]
    return C_N


def get_satellite_object_from_chain(root, time):
    # compute Iridium-TF chain for time input
    chain = root.GetObjectFromPath("/Chain/Iridium-TF")
    chain.SetTimePeriodType(2)  # eUserSpecific
    chainUserTimePeriod = chain.TimePeriod
    chainUserTimePeriod.TimeInterval.SetExplicitInterval(time, time + 1)
    chain.ClearAccess()
    optimal = chain.OptimalStrandOpts
    optimal.Compute = True
    optimal.Type = 0  # eChOptStrandMetricDistance
    chain.ComputeAccess()

    strandDP = chain.DataProviders.Item("Optimal Strand at Time")
    elems = ["Strand Name"]
    result = strandDP.ExecSingleElements(time, elems)
    strand_name = result.DataSets.Item(0).GetValues()

    sat_name = strand_name[0].split(" to ")[0]
    return sat_name


def get_satellite_from_table(epsec):
    # input is date time in EpSec between 0 and 172800 sec (48 hrs)
    return tbl.get_value_from_table(epsec)


def main():
    # Start STK
    # UPDATE SCENARIO PATH
    scenarioPath = "C:\\Users\\nahmed\\OneDrive - ANSYS, Inc\\Documents\\STK_ODTK 13\\AircraftMonteCarlo\\AircraftMonteCarlo.sc"
    scenario, root, stk, started_stk = start_stk(scenarioPath)
    aircraft = root.GetObjectFromPath("/Aircraft/TestFlight")
    aircraft.SetRouteType(9)  # sets aircraft to use Great Arc propagator
    route = aircraft.Route
    route.SetAltitudeRefType(0)  # sets aircraft to use MSL altitude reference

    # Set units
    root.UnitPreferences.Item("DateFormat").SetCurrentUnit("EpSec")
    root.UnitPreferences.Item("Ratio").SetCurrentUnit("dB")
    root.UnitPreferences.Item("Distance").SetCurrentUnit("ft")

    aircraft.SetAttitudeType(2)  # sets aircraft to use Standard attitude profile
    attitude = aircraft.Attitude.Basic
    attitude.SetProfileType(6)  # fixed in axes attitude profile
    orientation = attitude.Profile.Orientation

    # NEXT - vary the time step of the satellite, have a random  time selected
    # satName = "IRIDIUM_171_43929"
    if SAVE_LOOKUP_CSV:
        tbl.save_lookup_to_csv(scenario, root)
        tbl.load_lookup_from_csv(TABLE_CSV)
    else:
        tbl.load_lookup_from_csv(TABLE_CSV)

    # get aircraft receiver
    receiver = root.GetObjectFromPath("/Aircraft/TestFlight/Receiver/TF_Rx")

    # Get states from the Monte Carlo script
    print("getting monte carlo states...")
    states = mc.run_monte_carlo(NUM_TRIALS)

    c_n_list = []
    time_list = []

    # Set aircraft lat, lon, roll, pitch, heading, altitude to monte carlo state
    for i in range(NUM_TRIALS):
        print("trial #" + str(i))
        scenario_time = mc.sample_time()
        print("new time... " + str(scenario_time))
        time_list.append(scenario_time)

        set_aircraft_state(
            route,
            orientation,
            scenario_time,
            states["lat"][i],
            states["lon"][i],
            states["roll"][i],
            states["pitch"][i],
            states["heading"][i],
            states["altitude"][i],
        )

        print("aircraft state set")

        # satName = get_satellite_object_from_chain(root, scenario_time)
        satName = get_satellite_from_table(scenario_time)
        print(satName)
        satellite = root.GetObjectFromPath("/Satellite/" + satName)
        transmitter = satellite.Children.GetItemByIndex(1)
        access = transmitter.GetAccessToObject(receiver)

        C_N = compute_link_budget(access, scenario_time)
        c_n_list.append(C_N)

    if PRINT_TO_CSV:
        with open(OUTPUT_CSV, "w", newline="") as f:
            writer = csv.writer(f)
            print("writing to file...")

            # Header row
            writer.writerow(
                ["time (EpSec)", "lat", "lon", "pitch", "heading", "altitude", "C/N"]
            )
            for i in range(NUM_TRIALS):

                writer.writerow(
                    [
                        time_list[i],
                        states["lat"][i],
                        states["lon"][i],
                        states["roll"][i],
                        states["pitch"][i],
                        states["heading"][i],
                        states["altitude"][i],
                        c_n_list[i],
                    ]
                )

    print(f"Saved states to {OUTPUT_CSV}")

    return


if __name__ == "__main__":
    main()

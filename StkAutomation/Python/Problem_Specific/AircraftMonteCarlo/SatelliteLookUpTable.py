"""
SatelliteLookUpTable

Input: Time (UTCG) within a 48 hour window
Output: Satellite name from the Iridium constellation
"""

import csv

# STK Imports
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.vgt import *

SAVE_LOOKUP_CSV = False
TABLE_CSV = "satLookUpTable.csv"

satellite_lookup_table = {}


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
        print("Creating a new scenario")
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
    # STKDesktop.ReleaseAll() #comment these if it doesnt work

    del root
    del stk
    # del STKDesktop #comment these if it doesnt work


def get_value_from_table(epsec):
    print("in lookup fnc...")
    if not (0 <= epsec <= 172800):
        raise Exception("Time must be between 0 ad 172800 seconds")
    times = sorted(satellite_lookup_table.keys())
    closest_time = min(times, key=lambda t: abs(t - epsec))

    print(epsec)
    print(closest_time)
    print("from table: " + str(satellite_lookup_table[closest_time]))

    # interpolate between time values
    return str(satellite_lookup_table[closest_time])


def load_lookup_from_csv(filepath):
    with open(filepath, "r", newline="") as f:
        reader = csv.reader(f)
        next(reader)  # skip header
        for row in reader:
            satellite_lookup_table[float(row[0])] = row[1]
    return


def save_lookup_to_csv(scenario, root):
    chain = root.GetObjectFromPath("/Chain/FlightArea-Iridium")
    chain.ClearAccess()

    # optimal chain will find the closest satellite by range to the area target
    optimal = chain.OptimalStrandOpts
    optimal.Compute = True
    optimal.Type = 0  # eChOptStrandMetricDistance
    chain.ComputeAccess()

    # Get the closest satellite's name
    strandDP = chain.DataProviders.Item("Optimal Strand at Time")
    results = strandDP.Exec(scenario.StartTime, scenario.StopTime, 1)
    times = results.DataSets.GetDataSetByName("Time").GetValues()  # in UTCG
    strand_names = results.DataSets.GetDataSetByName("Strand Name").GetValues()
    with open(TABLE_CSV, "w", newline="") as f:
        writer = csv.writer(f)
        print("writing file...")

        # Header row
        writer.writerow(["Time (EpSec)", "Satellite Name"])

        # save name to table
        for t, s in zip(times, strand_names):
            sat_name = s.split(" to ")[1]
            satellite_lookup_table[t] = [sat_name]
            writer.writerow([t, sat_name])
    return


def main():
    scenarioPath = "C:\\Users\\nahmed\\OneDrive - ANSYS, Inc\\Documents\\STK_ODTK 13\\AircraftMonteCarlo\\AircraftMonteCarlo.sc"
    scenario, root, stk, started_stk = start_stk(scenarioPath)
    root.UnitPreferences.Item("DateFormat").SetCurrentUnit("EpSec")
    # flightArea = root.GetObjectFromPath("/AreaTarget/FlightArea")

    if SAVE_LOOKUP_CSV:
        save_lookup_to_csv(
            scenario, root
        )  # uncomment if the table doesn't already exist
    else:
        load_lookup_from_csv(TABLE_CSV)

    satName = get_value_from_table(28917.32)


# shutdown_stk(root, stk, scenarioPath, scenario)

if __name__ == "__main__":
    main()

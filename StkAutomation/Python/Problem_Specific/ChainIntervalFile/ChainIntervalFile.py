"""
Created on Wed Feb 18 2026

@author: Nuha Delago

This script takes an Optimal Strand by Time report and generates an interval file for targeted sensor pointing
"""

import os

# EDIT - change to the directory this script is saved in
os.chdir(
    r"C:\Users\nahmed\OneDrive - ANSYS, Inc\Documents\GitHub\STKCodeExamples\StkAutomation\Python\Problem_Specific"
)


def main():
    report = open(
        "Optimal_Strands_by_Time.txt", "r"
    )  # EDIT - make sure the file name is the same as the text file you saved from STK
    report.readline()  # this skip first header line
    lines = report.readlines()

    interval_file = open(
        "IntervalFile.int", "w"
    )  # EDIT - this will create IntervalFile.int in the directory specified on line 13
    interval_file.write(
        "stk.v.13.0\nBEGIN IntervalList\n\t\t\tDATEUNITABRV UTCG\nBEGIN Intervals\n"
    )

    current_time = None
    active_facility = None
    active_start_time = None

    intervals = []

    for i, line in enumerate(lines):
        if ":" in line:
            # get start time
            current_time = line
            # continue
        if " to " in line:
            strand_info = line
            parts = strand_info.split()  # split into words
            facility_name = parts[2]  # facility name would be the third word
            if active_facility == None:
                active_facility = facility_name
                active_start_time = current_time
                continue
            if (
                facility_name == active_facility
            ):  # still the same facility, so keep going
                continue
            else:
                previous_stop_time = current_time
                intervals.append(
                    (active_facility, active_start_time, previous_stop_time)
                )

                # Start a new interval
                active_facility = facility_name
                active_start_time = current_time

            # if active_facility is not None:
        if (
            i == len(lines) - 2
        ):  # The 2 is because there is an empty line at the end of the file
            intervals.append(
                (active_facility, active_start_time, current_time)
            )  # close out the final end time

    for facility, start, stop in intervals:
        # strip any new line characters that might be in the start, stop and facility strings
        line = f'\t\t\t"{start.strip()}" "{stop.strip()}" Facility/{facility.strip()}\n'
        interval_file.write(line)

    interval_file.write("END Intervals\nEND IntervalList\n")

    report.close()
    interval_file.close()


if __name__ == "__main__":
    main()

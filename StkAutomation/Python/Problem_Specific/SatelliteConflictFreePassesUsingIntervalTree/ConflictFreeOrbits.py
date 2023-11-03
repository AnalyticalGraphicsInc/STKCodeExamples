# Import basic utilities
import datetime as dt
import numpy as np
import os

# users need to install the python api
# STK library imports
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *
from agi.stk12.stkutil import *
from agi.stk12.stkx import *
from agi.stk12.utilities.colors import Color, Colors

# Defines an interval object to be used to store data for each access interval from the access computation
class Interval(object):
    obj = ""
    start = -1
    end = -1
    def __init__(self, obj, start, end) -> None:
        self.obj = obj  # The name of the object accessed by the facility in this interval
        self.start = start
        self.end = end

# I utilized an interval tree to store data to allow for more efficient computations of conflict due to our large dataset (rather than O(n^2) runtime to find all conflicts of an interval, it runs in O(nlogn)).
# For more information about interval trees, refer to https://www.geeksforgeeks.org/interval-tree/
# In the following section, I define the functions and classes to implement my interval tree data structure

# -------------------- Interval Tree ---------------------
# Defines a node to be used to store our intervals in an interval tree.
class Node:
    def __init__(self, interval, max):
        self.interval = interval    # Stores an interval object as a property
        self.max = max  # The maximum "end time" value within the subtree containing the current node and its descendants
        self.left = None    # Reference to left child of node in tree
        self.right = None   # Reference to right child of node in tree

# Function to insert nodes into the interval tree
def insert(root, interval):
    if root == None:
        return Node(interval, interval.end)
 
    if interval.start < root.interval.start:
        root.left = insert(root.left, interval)
    else:
        root.right = insert(root.right, interval)
 
    if root.max < interval.end:
        root.max = interval.end
 
    return root

# Function to return if the passed interval has any conflict with any other interval -> return True if conflict exists. Runs in O(logn) time.
def isConflicted(root, interval):
    if root == None:
        return False
    # NOTE: The 180 refers to 180 seconds = 3 min in between each access. Determine how much time you would like to constrain between accesses by different satellites.
    if((interval.start > root.interval.start-180 and interval.start < root.interval.end+180 or (interval.end > root.interval.start-180 and interval.end < root.interval.end+180)) and interval.obj != root.interval.obj):
        return True
    elif(root.left != None and root.left.max > interval.start):
        return isConflicted(root.left, interval)
    else:
        return isConflicted(root.right, interval)
# ----------------- End of Interval Tree ---------------------


# Runs through all of our intervals (linear time traversal) and calls isConflicted to mark if conflicted or not. If not conflicted, add to answers, which stores all non-conflicted intervals
def remove_conflicts(sats):
    tree_root = None
    for sat in sats:
        for i in range(len(sat)):
            tree_root = insert(tree_root, sat[i])

    answers = {}
    for sat in sats:
        while(len(sat) > 0):
            interval = sat.pop()
            if(not isConflicted(tree_root, interval)):
                if(not interval.obj in answers):
                    answers[interval.obj] = [interval]
                else:
                    answers[interval.obj].append(interval)
    
    return answers

# Merge any intervals that are part of the same pass in linear time. We define our assumptions below, but we assume accesses are computed in temporal order.
def merge_same_passes(answers):
    for satlist in answers.values():
        i = 0
        while(i < len(satlist)-1):
            if(satlist[i].end > satlist[i+1].start-2700): # NOTE: For LEO satellites: We assume that if two intervals by the same satellite are within 45 minutes of each other, they must be part of same pass (impossible as it is to be otherwise)
                satlist[i].end = satlist[i+1].end
                satlist.pop(i+1)
            else:
                i += 1
    
    return answers

# Get the pass number of the valid pass interval
def get_pass_nums(answers, sc):
    for satname in answers:
        satlist = answers[satname]
        satellite_obj = sc.Children.GetItemByName(satname)
        for interval in satlist:
            passData = satellite_obj.DataProviders.Item('Passes')
            passResults = passData.Exec(interval.start, interval.end)
            newpassnum = passResults.DataSets.GetDataSetByName('Pass Number').GetValues()[0]
            interval.passnum = newpassnum

    for s in answers.values():
        for a in s:
            print(f"{a.obj}:\tPass {a.passnum}\t{a.start}\t{a.end}")
    
    return answers

# Writes the answers into an output.txt file (which it will create if one does not exist in the same directory as this script).
def create_file(answers):
    file = open("output.txt", "w")

    for satobj in answers:
        file.write(f"{satobj}:\n")
        satlist = answers[satobj]
        for interval in satlist:
            file.write(f"Pass {interval.passnum}   \t{interval.start}\t{interval.end}\n")
        file.write("\n")
    print("Done creating file.")

# Connects to STK using the STK Object Model with Python, computes accesses directly in the script and pulls the resulting data into my data structure that stores all of the access data for use in my algorithm.
def get_accesses(sats):
    uiApp = STKDesktop.StartApplication(visible=True)
    uiApp.Visible = True
    uiApp.UserControl = True

    stkRoot = uiApp.Root
    # TODO: REPLACE PATH VALUE WITH THE PATH TO YOUR SCENARIO -------------------------------------
    path = "" # REPLACE
    stkRoot.Load(path)
    stkRoot.UnitPreferences.Item('DateFormat').SetCurrentUnit('EpSec')
    sc = stkRoot.CurrentScenario
    print("Getting Accesses")
    # TODO: REPLACE "Facility1" NAME WITH THE NAME OF YOUR FACILITY OBJECT ----------------------------------
    fac1 = sc.Children.GetItemByName('Facility1') # REPLACE
    satnum = 0
    for sat in sc.Children.GetElements(18):
        satname = sat.InstanceName
        oSat = IAgStkObject(sat)
        access = fac1.GetAccessToObject(oSat)
        access.ComputeAccess()

        accessData = access.DataProviders.Item('Access Data')
        results = accessData.Exec(sc.StartTime,sc.StopTime)

        accessStart = results.DataSets.GetDataSetByName('Start Time').GetValues()
        accessStop = results.DataSets.GetDataSetByName('Stop Time').GetValues()

        intervals = []
        i = len(accessStart)-1
        while(i >= 0):
            intervals.append(Interval(satname, accessStart[i], accessStop[i]))
            i-=1
        sats.insert(satnum, intervals)
        satnum += 1
    
    answers = remove_conflicts(sats)
    answers = merge_same_passes(answers)
    answers = get_pass_nums(answers, sc)
    create_file(answers)
            

# Simply by calling the script, main should run, and the pipeline will run automatically to generate the output file.
def main():

    sats = []
    get_accesses(sats)


if __name__ == "__main__":
    main()
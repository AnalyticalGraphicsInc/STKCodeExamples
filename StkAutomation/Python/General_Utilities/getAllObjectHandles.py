# Import main library
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *

stk = STKDesktop.AttachToApplication()

# Get the IAgStkObjectRoot interface and scenario object
root = stk.Root
scenario = root.CurrentScenario

count = scenario.Children.Count

for i in range(count):
    objName = scenario.Children.Item(i).InstanceName
    tempObj = scenario.Children.Item(i)
    exec(objName+" = scenario.Children.Item(i)")
    if (tempObj.Children.Count > 0):
        for j in range(tempObj.Children.Count):
            subObjName = tempObj.Children.Item(j).InstanceName
            tempSubObj = tempObj.Children.Item(j)
            exec(subObjName+" = tempObj.Children.Item(j)")
            if (tempSubObj.Children.Count > 0):
                for k in range(tempSubObj.Children.Count):
                    subSubObjName = tempSubObj.Children.Item(k).InstanceName
                    exec(subSubObjName+" = tempSubObj.Children.Item(k)")
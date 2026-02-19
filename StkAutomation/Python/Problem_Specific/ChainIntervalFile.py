'''
filename

Input:
Output:
'''

# STK Imports
from agi.stk13.stkdesktop import STKDesktop
from agi.stk13.stkobjects import *
from agi.stk13.vgt import *

def start_stk(scenarioPath):
    started_stk = False
    try:
        print('Trying to connect to STK')
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
            print('Scenario path = ' + scenarioPath)
            print('Error: scenario is null.')
            return None, root, stk, started_stk
    except Exception as e:
        print('Creating a new scenario')
        stk = STKDesktop.StartApplication(visible=True, userControl=True)
        root = stk.Root
        root.LoadScenario(scenarioPath)
        scenario = root.CurrentScenario
        started_stk = True
        print(e)
        return scenario, root, stk, started_stk

def shutdown_stk(root, stk, scenarioPath, scenario)
    root.SaveScenario()
    root.CloseScenario()
    stk.Quit()
    stk.ShutDown()
    #STKDesktop.ReleaseAll() #comment these if it doesnt work

    del root
    del stk
    #del STKDesktop #comment these if it doesnt work
def main():
    scenarioPath = 'stkfilepath'
    scenario, root, stk, started_stk = start_stk(scenarioPath)
    #shutdown_stk(root, stk, scenarioPath, scenario)

if __name__ == '__main__':
    main()
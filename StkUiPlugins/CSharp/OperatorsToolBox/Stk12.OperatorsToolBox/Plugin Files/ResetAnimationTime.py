from comtypes.client import GetActiveObject
import sys

print(sys.argv)
uiApplication = GetActiveObject('STK12.Application')
root = uiApplication.Personality2
root.ExecuteCommand("Animate * Reset")
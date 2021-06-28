### this tool will combine multiple ephemeris files into one
### author: jens ramrath, agi
### date: 18 may 2021

### INIT
ephemerisDir = 'C:\\temp\\1month\\eFiles'
combinedPath = 'C:\\temp\\1month\\Combined.e'


class EphemerisPoint:
    def __init__(self, epSec, x, y, z, epoch):
        self.Epoch = epoch
        self.EpSec = epSec
        self.X = x
        self.Y = y
        self.Z = z


### RUN
# start new instance of STK
import comtypes
from comtypes.client import CreateObject
from comtypes.gen import STKObjects
from comtypes.gen import AgSTKVgtLib
app = CreateObject('STK12.Application')
app.Visible = True
app.UserControl= True
root = app.Personality2

# build scenario
root.NewScenario('CombineEphemeris')
oSc = root.CurrentScenario
sc = oSc.QueryInterface(STKObjects.IAgScenario)
root.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")


oSat = oSc.Children.New(STKObjects.eSatellite,'Combine')
sat = oSat.QueryInterface(STKObjects.IAgSatellite)
sat.SetPropagatorType(STKObjects.ePropagatorStkExternal)

import os
# loop through all .e files
firstSat = True
allPoints = []
for file in os.listdir(ephemerisDir):
    if file.endswith(".e"):
        
        prop = sat.Propagator.QueryInterface(STKObjects.IAgVePropagatorStkExternal)
        prop.FileName = os.path.join(ephemerisDir, file)
        prop.Propagate()
        print(os.path.join(ephemerisDir, file))

        # set scenario time for the first one
        if firstSat:
            sc.SetTimePeriod(prop.StartTime, prop.StopTime)
            firstSat = False
            prop.Propagate()
        
        # report out ephemeris
        root.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec")
        dp = oSat.DataProviders.GetDataPrvTimeVarFromPath('Cartesian Position/ICRF').QueryInterface(STKObjects.IAgDataPrvTimeVar)
        results = dp.ExecNativeTimes(prop.StartTime, prop.StopTime)
        t = results.DataSets.GetDataSetByName('Time').GetValues()
        x = results.DataSets.GetDataSetByName('x').GetValues()
        y = results.DataSets.GetDataSetByName('y').GetValues()
        z = results.DataSets.GetDataSetByName('z').GetValues()

        root.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")

        # write to array
        for ptCounter in range(len(t)):
            thisPt = EphemerisPoint(t[ptCounter], x[ptCounter], y[ptCounter], z[ptCounter], prop.EphemerisStartEpoch.TimeInstant)
            allPoints += [thisPt]


# sort array
allPoints.sort(key=lambda x: x.EpSec, reverse=False)


### write combined ephemeris file
f = open(combinedPath, "w")
f.write("stk.v.10.0\n")
f.write("BEGIN Ephemeris\n")
f.write("InterpolationMethod    Lagrange\n")
f.write("InterpolationOrder  	5\n")
f.write("ScenarioEpoch " + allPoints[0].Epoch + "\n")
f.write("CentralBody            Earth\n")
f.write("CoordinateSystem       ICRF\n")
f.write("DistanceUnit           Kilometers\n")
f.write("EphemerisTimePos\n")

for thisPoint in allPoints:
    f.write("   " + str(thisPoint.EpSec) + " " + str(thisPoint.X) + " " + str(thisPoint.Y) + " " + str(thisPoint.Z) + "\n")


f.write("END Ephemeris\n")
f.close()


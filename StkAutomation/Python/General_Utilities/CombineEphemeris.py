import os

from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import *


ephemerisDir = r"C:\temp\1month\eFiles"
combinedPath = r"C:\temp\1month\Combined.e"


class EphemerisPoint:
    def __init__(self, epSec, x, y, z, epoch):
        self.Epoch = epoch
        self.EpSec = epSec
        self.X = x
        self.Y = y
        self.Z = z

# RUN
        
# start new instance of STK
stk = STKDesktop.StartApplication(visible=True)
root = stk.Root


# build scenario
root.NewScenario("CombineEphemeris")
sc = root.CurrentScenario
root.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")


sat = sc.Children.New(AgESTKObjectType.eSatellite, "Combine")
sat.SetPropagatorType(AgEVePropagatorType.ePropagatorStkExternal)

# loop through all .e files
firstSat = True
allPoints = []
for file in os.listdir(ephemerisDir):
    if file.endswith(".e"):

        sat.Propagator.Filename = os.path.join(ephemerisDir, file)
        sat.Propagator.Propagate()
        
        print(os.path.join(ephemerisDir, file))

        # set scenario time for the first one
        if firstSat:
            sc.SetTimePeriod(sat.Propagator.StartTime, sat.Propagator.StopTime)
            firstSat = False
            sat.Propagator.Propagate()

        # report out ephemeris
        root.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec")
        dp = sat.DataProviders.GetDataPrvTimeVarFromPath("Cartesian Position/ICRF")
        results = dp.ExecNativeTimes(sat.Propagator.StartTime, sat.Propagator.StopTime)
        t = results.DataSets.GetDataSetByName("Time").GetValues()
        x = results.DataSets.GetDataSetByName("x").GetValues()
        y = results.DataSets.GetDataSetByName("y").GetValues()
        z = results.DataSets.GetDataSetByName("z").GetValues()

        root.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")

        # write to array
        for ptCounter in range(len(t)):
            thisPt = EphemerisPoint(
                t[ptCounter],
                x[ptCounter],
                y[ptCounter],
                z[ptCounter],
                sat.Propagator.EphemerisStartEpoch.TimeInstant,
            )
            
            # only add to array if we don't have a point at this time already
            if not thisPt.EpSec in [data.EpSec for data in allPoints]:
                allPoints += [thisPt]


# sort array
allPoints.sort(key=lambda x: x.EpSec, reverse=False)


# write combined ephemeris file
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
    f.write(
        "   "
        + str(thisPoint.EpSec)
        + " "
        + str(thisPoint.X)
        + " "
        + str(thisPoint.Y)
        + " "
        + str(thisPoint.Z)
        + "\n"
    )


f.write("END Ephemeris\n")
f.close()

root.CloseScenario()
stk.ShutDown()

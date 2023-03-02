from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import AgDrResult, AgScenario, AgStkObjectRoot
from agi.stk12.vgt import (
    AgCrdnCalcScalarFile,
    AgCrdnConditionScalarBounds,
    AgCrdnEventIntervalListCondition,
    AgECrdnConditionThresholdOption,
)

# Inputs
sensorPath = "*/Satellite/Satellite1/Sensor/Sensor1"
step = 60
saveDir = "C:/sunObscurationScalar.csc"

# Attach to running STK
stk = STKDesktop.AttachToApplication()

# Get the IAgStkObjectRoot interface and scenario object, set DateFormat units to Epoch days
root = AgStkObjectRoot(stk.Root)
scenario = AgScenario(root.CurrentScenario)

# Set up and run sensor obscuration tool on the sun
cmd = (
    "VO "
    + sensorPath
    + " Obscuration CentralBody Sun On DontRenderNormalWindows On DeleteData"
)
root.ExecuteCommand(cmd)
cmd = (
    "VO "
    + sensorPath
    + ' Obscuration Compute "'
    + scenario.StartTime
    + '" "'
    + scenario.StopTime
    + '" '
    + str(step)
)
root.ExecuteCommand(cmd)

# Pull times and percent obscuration
sensor = root.GetObjectFromPath(sensorPath)
obsResult = AgDrResult(
    sensor.DataProviders.Item("Obscuration").ExecElements(
        scenario.StartTime, scenario.StopTime, step, ["Time", "Percent Obscured"]
    )
)
time = obsResult.DataSets.GetDataSetByName("Time").GetValues()
pct = obsResult.DataSets.GetDataSetByName("Percent Obscured").GetValues()

# Write data provider results to calc scalar file
with open(saveDir, "w") as f:
    header = [
        "stk.v.11.0\n",
        "BEGIN Data\n",
        "TimeFormat UTCG\n",
        "UnitType Distance\n",
        "ValueUnit km\n",
        "NumberOfIntervals 1\n",
        "BEGIN Interval\n",
        "NumberOfPoints " + str(len(time)) + "\n",
        "BEGIN TimeValues\n",
    ]
    f.writelines(header)
    for i in range(len(time)):
        f.write(time[i] + " " + str(pct[i]) + "\n")

# Import calc scalar file to STK
try:
    obsScalar = AgCrdnCalcScalarFile(
        sensor.Vgt.CalcScalars.Factory.CreateCalcScalarFile(
            "sunObscuration", "", saveDir
        )
    )
except Exception:
    obsScalar = AgCrdnCalcScalarFile(sensor.Vgt.CalcScalars.Item("sunObscuration"))
    obsScalar.Filename = saveDir

# Create condition for sun obscuration - if obscuration is greater than 0, then sun is in FOV
try:
    obsCondition = AgCrdnConditionScalarBounds(
        sensor.Vgt.Conditions.Factory.CreateConditionScalarBounds("ifSunObscured", "")
    )
except Exception:
    obsCondition = AgCrdnConditionScalarBounds(
        sensor.Vgt.Conditions.Item("ifSunObscured")
    )
obsCondition.Scalar = obsScalar
obsCondition.Operation = (
    AgECrdnConditionThresholdOption.eCrdnConditionThresholdOptionAboveMin
)
obsCondition.SetMinimum(root.ConversionUtility.NewQuantity("Distance", "km", 0))

# Create satisfaction interval list for when sun is in FOV
try:
    obsIntervals = AgCrdnEventIntervalListCondition(
        sensor.Vgt.EventIntervalLists.Factory.CreateEventIntervalListCondition(
            "sunObscurationTimes", ""
        )
    )
except Exception:
    obsIntervals = AgCrdnEventIntervalListCondition(
        sensor.Vgt.EventIntervalLists.Item("sunObscurationTimes")
    )
obsIntervals.Condition = obsCondition

# Print times when sun is in FOV
obsIntervalsResult = obsIntervals.FindIntervals().Intervals
print("Sun Obscuration Times\n----------------------------------")
for i in range(obsIntervalsResult.Count):
    print(obsIntervalsResult.Item(i).Start + " to " + obsIntervalsResult.Item(i).Stop)

# Create displacement vector to the sun
sunCenter = root.CentralBodies.Sun.Vgt.Points.Item("Center")
sensorCenter = sensor.Vgt.Points.Item("Center")
try:
    sunVec = sensor.Vgt.Vectors.Factory.CreateDisplacementVector(
        "toSun", sensorCenter, sunCenter
    )
except Exception:
    sunVec = sensor.Vgt.Vectors.Item("toSun")

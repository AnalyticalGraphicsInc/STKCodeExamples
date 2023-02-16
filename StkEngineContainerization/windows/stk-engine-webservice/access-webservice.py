from agi.stk12.stkengine import STKEngine
from agi.stk12.stkobjects.stkobjects import (
    AgESTKObjectType,
    IAgSatellite,
    IAgVePropagatorTwoBody,
)
from flask import Flask, request

app = Flask(__name__)

stk = STKEngine.StartApplication(noGraphics=True)
root = stk.NewObjectRoot()
root.UnitPreferences.SetCurrentUnit("DateFormat", "ISO-LTZ")
root.NewScenario("access-webservice")
scenario = root.CurrentScenario
mySatelliteObject = scenario.Children.New(AgESTKObjectType.eSatellite, "MySatellite")
myPlaceObject = scenario.Children.New(AgESTKObjectType.ePlace, "MyPlace")

mySatellite = IAgSatellite(mySatelliteObject)
mySatellitePropagator = IAgVePropagatorTwoBody(mySatellite.Propagator)


@app.route("/access")
def access_service():
    args = request.args
    startTime = args["startTime"]
    stopTime = args["stopTime"]
    scenario.AnalysisInterval.SetStartAndStopTimes(startTime, stopTime)

    mySatellitePropagator.InitialState.OrbitEpoch.SetExplicitTime(startTime)
    mySatellitePropagator.Propagate()

    access = mySatelliteObject.GetAccessToObject(myPlaceObject)
    access.ComputeAccess()

    intvllist = []
    accessIntervals = access.ComputedAccessIntervalTimes.ToArray(0, -1)
    for intvl in accessIntervals:
        intvllist.append({"start": intvl[0], "stop": intvl[1]})

    result = {"accessIntervals": intvllist}
    return result

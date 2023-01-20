from agiparallel.client import *
from agiparallel.constants import TaskProperties
from agiparallel.infrastructure.TaskEnvironment import TaskEnvironment
from agi.stk12.stkengine import STKEngine
from agi.stk12.stkobjects.stkobjects import (
    AgESTKObjectType,
    IAgSatellite,
    IAgVePropagatorTwoBody,
)
from datetime import datetime, timedelta, timezone
import json, os, uuid


def main():
    today = datetime.now(timezone.utc)
    tomorrow = today + timedelta(days=1)
    timeIntervals = [
        (today.isoformat(), tomorrow.isoformat()),
        ("2022-01-01T00:00:00.000+00:00", "2022-01-02T00:00:00.000+00:00"),
    ]

    coordinatorHostname = os.getenv("COORDINATOR_HOSTNAME", default="localhost")
    coordinatorPort = os.getenv("COORDINATOR_PORT", default="9090")

    with ClusterJobScheduler(coordinatorHostname, int(coordinatorPort)) as client:
        client.connect()
        job = client.create_job()
        job.set_task_environment(StkTaskEnvironment())
        for interval in timeIntervals:
            job.add_task(ComputeTask(*interval))
        job.submit()
        job.wait_until_done()

    for i in range(len(timeIntervals)):
        print()
        print(f"Access intervals for analysis interval {timeIntervals[i]}:")
        print(json.dumps(job.tasks[i].result, indent=4))


class StkTaskEnvironment(TaskEnvironment):
    def __init__(self):
        self.unique_id = uuid.UUID("6DDE57D8-49F3-4343-9E2F-4047247C8B41")

    def setup(self):

        self.app = STKEngine.StartApplication(noGraphics=True)
        self.root = self.app.NewObjectRoot()
        self.root.UnitPreferences.SetCurrentUnit("DateFormat", "ISO-LTZ")
        self.root.NewScenario("Example")
        self.scenario = self.root.CurrentScenario
        self.mySatelliteObject = self.scenario.Children.New(
            AgESTKObjectType.eSatellite, "MySatellite"
        )
        self.myPlaceObject = self.scenario.Children.New(
            AgESTKObjectType.ePlace, "MyPlace"
        )

        self.mySatellite = IAgSatellite(self.mySatelliteObject)
        self.mySatellitePropagator = IAgVePropagatorTwoBody(self.mySatellite.Propagator)

    def teardown(self):
        if self.root.CurrentScenario is not None:
            self.root.CloseScenario()
        self.app.ShutDown()


class ComputeTask:
    def __init__(self, startTime, stopTime):
        self.startTime = startTime
        self.stopTime = stopTime

    def execute(self):
        log = self.get_property(TaskProperties.LOGGER)

        env = self.get_property(TaskProperties.ENVIRONMENT)
        if not env:
            raise Exception("could not get task environment!")

        env.scenario.AnalysisInterval.SetStartAndStopTimes(
            self.startTime, self.stopTime
        )
        env.mySatellitePropagator.InitialState.OrbitEpoch.SetExplicitTime(
            self.startTime
        )
        env.mySatellitePropagator.Propagate()

        access = env.mySatelliteObject.GetAccessToObject(env.myPlaceObject)
        access.ComputeAccess()

        accessIntervals = access.ComputedAccessIntervalTimes.ToArray(0, -1)
        intvllist = [
            {"start": interval[0], "stop": interval[1]} for interval in accessIntervals
        ]

        self.result = {"accessIntervals": intvllist}


if __name__ == "__main__":
    main()

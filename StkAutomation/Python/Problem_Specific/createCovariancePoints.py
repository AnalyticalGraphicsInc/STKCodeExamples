import numpy as np
from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkobjects import (
    AgEGeometricElemType,
    AgELeadTrailData,
    AgESTKObjectType,
    AgEVePropagatorType,
)
from agi.stk12.vgt import AgECrdnPointType

# Inputs
satName = "Satellite1"
sigmaLevel = 3
timeStep = 60

#


def createPointsFromCovariance(
    root, satName, timeStep=60, sigmaLevel=3, createPoints=False, createSats=False
):

    # Use Body Frame
    frame = "ICRF"

    # Get sat
    satPath = "Satellite/{}".format(satName)
    sat = root.GetObjectFromPath(satPath)

    # Get Scenario Path
    cmd = "GetDirectory / Scenario"
    res = root.ExecuteCommand(cmd)
    scenarioPath = res.Item(0)

    # Get covariance
    covDP = sat.DataProviders.Item("Position Covariance in Axes")
    covDpICRF = covDP.Group.Item(frame)
    covDpICRF.PreData = str(sigmaLevel)
    events = sat.Vgt.Events
    startTime = events.Item("EphemerisStartTime").FindOccurrence().Epoch
    stopTime = events.Item("EphemerisStopTime").FindOccurrence().Epoch
    elems = [
        "Time",
        "Major Sigma",
        "Intermediate Sigma",
        "Minor Sigma",
        "Major Sigma Dir x",
        "Major Sigma Dir y",
        "Major Sigma Dir z",
        "Minor Sigma Dir x",
        "Minor Sigma Dir y",
        "Minor Sigma Dir z",
    ]
    res = covDpICRF.ExecElements(startTime, stopTime, timeStep, elems)
    covData = np.asarray(res.DataSets.ToArray())

    # Organize Data, scale vectors
    times = covData[:, 0]
    times = np.round(times, 3)
    majorSigma = covData[:, 1].astype(float)
    intermediateSigma = covData[:, 2].astype(float)
    minorSigma = covData[:, 3].astype(float)
    majorDir = covData[:, 4:7].astype(float)
    minorDir = covData[:, 7:10].astype(float)
    intermediateDir = np.asarray(
        [np.cross(minorDir[ii], majorDir[ii]) for ii in range(len(covData[:]))]
    )
    scaledMajorDir = majorDir * majorSigma.reshape(len(majorSigma), 1)
    scaledIntermediateDir = intermediateDir * intermediateSigma.reshape(
        len(intermediateSigma), 1
    )
    scaledMinorDir = minorDir * minorSigma.reshape(len(minorSigma), 1)

    # # Get sat ephemeris
    cartPos = sat.DataProviders.Item("Cartesian Position")
    cartPos = cartPos.Group.Item(frame)
    posData = cartPos.Exec(startTime, stopTime, timeStep)  # Time x, y, z
    posData = np.asarray(posData.DataSets.ToArray())
    pos = posData[:, 1:4].astype(float)

    cartVel = sat.DataProviders.Item("Cartesian Velocity")
    cartVel = cartVel.Group.Item(frame)
    velData = cartVel.Exec(startTime, stopTime, timeStep)  # Time, Vx, Vy, Vz
    velData = np.asarray(velData.DataSets.ToArray())

    # Create all 6 directions
    # Sat pos +- major,int,minor vecs
    majorPos = pos + scaledMajorDir
    majorNeg = pos - scaledMajorDir
    intermediatePos = pos + scaledIntermediateDir
    intermediateNeg = pos - scaledIntermediateDir
    minorPos = pos + scaledMinorDir
    minorNeg = pos - scaledMinorDir
    # majorPos = scaledMajorDir
    # majorNeg = -scaledMajorDir
    # intermediatePos = scaledIntermediateDir
    # intermediateNeg = -scaledIntermediateDir
    # minorPos = scaledMinorDir
    # minorNeg = -scaledMinorDir

    ephFiles = [
        majorPos,
        majorNeg,
        intermediatePos,
        intermediateNeg,
        minorPos,
        minorNeg,
    ]
    names = [
        "majorPos.e",
        "majorNeg.e",
        "intermediatePos.e",
        "intermediateNeg.e",
        "minorPos.e",
        "minorNeg.e",
    ]
    startTimeUTCG = root.ConversionUtility.ConvertDate("EpSec", "UTCG", str(startTime))
    for ii, ephFile in enumerate(ephFiles):
        with open(scenarioPath + names[ii], "w") as f:

            f.write("stk.v.12.0\n")
            f.write("BEGIN Ephemeris\n")
            f.write("InterpolationMethod    Lagrange\n")
            f.write("InterpolationOrder  	5\n")
            f.write("ScenarioEpoch {}\n".format(startTimeUTCG))
            f.write("CentralBody            Earth\n")
            # f.write("CoordinateSystem       Custom {} Satellite/{}\n".format(frame,satName))
            f.write("CoordinateSystem       {}\n".format(frame))
            f.write("DistanceUnit           Kilometers\n")
            f.write("EphemerisTimePos\n")

            for jj, line in enumerate(majorPos):
                f.write(
                    "   {} {} {} {}\n".format(
                        times[jj], ephFile[jj, 0], ephFile[jj, 1], ephFile[jj, 2]
                    )
                )
                # f.write("   {} {} {} {} {} {} {}\n".format(times[jj],ephFile[jj,0],ephFile[jj,1],ephFile[jj,2],vel[jj,0],vel[jj,1],vel[jj,2]))

            f.write("END Ephemeris\n")

    if createPoints:
        pointsFactory = sat.Vgt.Points.Factory
        sat.VO.Vector.RefCrdns.RemoveAll()  # Clear graphics
        for ii, name in enumerate(names):
            name = name.split(".")[0]
            ephFile = ephFiles[ii]
            if sat.Vgt.Points.Contains(name):
                sat.Vgt.Points.Remove(name)
            pointsFactory.Create(
                name, "Point On the Covariance", AgECrdnPointType.eCrdnPointTypeFile
            )
            point = sat.Vgt.Points.Item(name)
            point.Filename = scenarioPath + name + ".e"
            sat.VO.Vector.RefCrdns.Add(
                AgEGeometricElemType.ePointElem,
                "Satellite/{} {} Point".format(satName, point.Name),
            )  # Add to VO

    if createSats:
        scenario = root.CurrentScenario
        for ii, name in enumerate(names):
            name = name.split(".")[0]
            if not scenario.Children.Contains(AgESTKObjectType.eSatellite, name):
                scenario.Children.New(AgESTKObjectType.eSatellite, name)
            satii = scenario.Children.Item(name)
            satii.SetPropagatorType(AgEVePropagatorType.ePropagatorStkExternal)
            satii.Propagator.Filename = scenarioPath + name + ".e"
            # Clear graphics
            satii.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(
                AgELeadTrailData.eDataNone
            )
            satii.VO.Pass.TrackData.PassData.Orbit.SetTrailDataType(
                AgELeadTrailData.eDataNone
            )
            satii.Propagator.Propagate()


stk = STKDesktop.AttachToApplication()
root = stk.Root
root.Isolate()
root.UnitPreferences.Item("DateFormat").SetCurrentUnit("EpSec")
createPointsFromCovariance(
    root,
    satName,
    timeStep=60,
    sigmaLevel=sigmaLevel,
    createPoints=True,
    createSats=True,
)

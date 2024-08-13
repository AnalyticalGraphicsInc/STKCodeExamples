# satellite creation part of the LoadCDM script 

from agi.stk12.stkdesktop import STKDesktop
from agi.stk12.stkutil import *
from agi.stk12.stkobjects import *
import ParseCdm

def AddSatellite(segment, root, epoch):

    sat = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, segment.UniqueName)
    sat.SetPropagatorType(AgEVePropagatorType.ePropagatorHPOP)
    prop = sat.Propagator

    # set units to ISO-YMD
    previousUnits = root.UnitPreferences.GetCurrentUnitAbbrv("DateFormat")
    root.UnitPreferences.SetCurrentUnit("DateFormat", "ISO-YMD")

    prop.InitialState.OrbitEpoch.SetExplicitTime(epoch)
    root.UnitPreferences.SetCurrentUnit("DateFormat", previousUnits)

    # initial position / velocity
    prop.InitialState.Representation.AssignCartesian(AgECoordinateSystem.eCoordinateSystemFixed,
                                                        float(segment.StateVector.X_Value),
                                                        float(segment.StateVector.Y_Value),
                                                        float(segment.StateVector.Z_Value),
                                                        float(segment.StateVector.X_DOT_Value),
                                                        float(segment.StateVector.Y_DOT_Value),
                                                        float(segment.StateVector.Z_DOT_Value))

    prop.Propagate()

    # covariance
    # check if there is actually non-zero covariance
    if float(segment.Covariance.CR_R_Value) > 0:
        prop.Covariance.ComputeCovariance = True
        prop.Covariance.Frame = AgEVeFrame.eLVLH
        prop.Covariance.PositionVelocity[0].X = float(segment.Covariance.CR_R_Value)

        prop.Covariance.PositionVelocity[1].X = float(segment.Covariance.CT_R_Value)
        prop.Covariance.PositionVelocity[1].Y = float(segment.Covariance.CT_T_Value)

        prop.Covariance.PositionVelocity[2].X = float(segment.Covariance.CN_R_Value)
        prop.Covariance.PositionVelocity[2].Y = float(segment.Covariance.CN_T_Value)
        prop.Covariance.PositionVelocity[2].Z = float(segment.Covariance.CN_N_Value)

        prop.Covariance.PositionVelocity[3].X = float(segment.Covariance.CRDot_R_Value)
        prop.Covariance.PositionVelocity[3].Y = float(segment.Covariance.CRDot_T_Value)
        prop.Covariance.PositionVelocity[3].Z = float(segment.Covariance.CRDot_N_Value)
        prop.Covariance.PositionVelocity[3].Vx = float(segment.Covariance.CRDot_RDot_Value)

        prop.Covariance.PositionVelocity[4].X = float(segment.Covariance.CTDot_R_Value)
        prop.Covariance.PositionVelocity[4].Y = float(segment.Covariance.CTDot_T_Value)
        prop.Covariance.PositionVelocity[4].Z = float(segment.Covariance.CTDot_N_Value)
        prop.Covariance.PositionVelocity[4].Vx = float(segment.Covariance.CTDot_RDot_Value)
        prop.Covariance.PositionVelocity[4].Vy = float(segment.Covariance.CTDot_TDot_Value)

        prop.Covariance.PositionVelocity[5].X = float(segment.Covariance.CNDot_R_Value)
        prop.Covariance.PositionVelocity[5].Y = float(segment.Covariance.CNDot_T_Value)
        prop.Covariance.PositionVelocity[5].Z = float(segment.Covariance.CNDot_N_Value)
        prop.Covariance.PositionVelocity[5].Vx = float(segment.Covariance.CNDot_RDot_Value)
        prop.Covariance.PositionVelocity[5].Vy = float(segment.Covariance.CNDot_TDot_Value)
        prop.Covariance.PositionVelocity[5].Vz = float(segment.Covariance.CNDot_NDot_Value)


        prop.Propagate()

    return sat
import typing
import numpy as np
from agi.stk12.plugins.accessconstraintplugin import IAgAccessConstraintPlugin, AgEAccessConstraintObjectType, AgEAccessConstraintDependencyFlags, AgEAccessApparentPositionType
from agi.stk12.plugins.utplugin               import IAgUtPluginConfig, AgEUtLogMsgType, AgEUtFrame
from agi.stk12.plugins.attrautomation         import AgEAttrAddFlags
from agi.stk12.plugins.stkplugin              import AgStkPluginSite

# This plugin determines the lighting condition of the background of an access object.
# The range constraint can be dynamically set if the background is Earth and lit by direct sun,
# background is Earth and unlit (penumbra or umbra), or if the background is deep space.
# Other CBs are not considered. The plugin returns 1 if the range constraint for the specified
# background is satisifed, and 0 if not.

class CAgAccessConstraintPlugin(object):
    def __init__(self):
        self.scope = None
        self.site = None
        self.DayRange = 100000000
        self.NightRange = 100000000
        self.SpaceRange = 100000000
        self.a = 6378137
        self.b = 6378137
        self.c = 6356752.31424
        self.sunRadius = 695700000
    
    @property
    def DisplayName(self) -> str:
        '''
        Triggered when the plugin is being registered. This is the name of the constraint used by STK.
        The DisplayName property may alternatively be defined as a member attribute (see the STK Python API Programmer's Guide).
        '''
        return 'COM_PY_RangeLightingConstraint'

    def Register(self, result:"IAgAccessConstraintPluginResultRegister") -> None:
        '''
        Triggered after application start-up, in order to register the constraint for specific STK object pairs for which this constraint is applicable.
        '''
        result.BaseObjectType = AgEAccessConstraintObjectType.eSensor
        result.BaseDependency = AgEAccessConstraintDependencyFlags.eDependencyRelativePosVel + AgEAccessConstraintDependencyFlags.eDependencyPosVel + AgEAccessConstraintDependencyFlags.eDependencyRelSun
        result.Dimension = "Unitless"
        result.MinValue = 0.5
        result.MaxValue = 1.0
        result.TargetDependency = AgEAccessConstraintDependencyFlags.eDependencyRelativePosVel + AgEAccessConstraintDependencyFlags.eDependencyPosVel + AgEAccessConstraintDependencyFlags.eDependencyRelSun
        result.AddTarget(AgEAccessConstraintObjectType.eSatellite)
        result.AddTarget(AgEAccessConstraintObjectType.eAircraft)
        result.Register()

        result.Message(AgEUtLogMsgType.eUtLogMsgInfo, f'{self.DisplayName}: Register(Sensor to Satellite/Aircraft)')


    def Init(self, site:"IAgUtPluginSite") -> bool:
        '''
        Triggered just before the first computational event trigger.
        '''
        self.site = AgStkPluginSite(site)
        return True

    def PreCompute(self, result:"IAgAccessConstraintPluginResultPreCompute") -> bool:
        '''
        Triggered prior to the calls to the Evaluate method, to allow for any required initialization.
        '''
        return True

    def Evaluate(self, result:"IAgAccessConstraintPluginResultEval", baseData:"IAgAccessConstraintPluginObjectData", targetData:"IAgAccessConstraintPluginObjectData") -> bool:
        '''
        Triggered when the plugin is evaluated for an access constraint value
        '''
        # Compute Earth intersection point
        posVec = np.array(baseData.Position_Array(AgEUtFrame.eUtFrameFixed))
        losVec = np.array(baseData.RelativePosition_Array(AgEAccessApparentPositionType.eLightPathApparentPosition, AgEUtFrame.eUtFrameFixed))
        scaleFactor = np.linalg.norm(posVec) / np.linalg.norm(losVec)
        losVecScaled = losVec * scaleFactor
        D = np.diag([1/self.a**2, 1/self.b**2, 1/self.c**2])
        coeffA = np.dot(np.dot(losVecScaled, D), losVecScaled)
        coeffB = np.dot(np.dot(posVec, D), losVecScaled)
        coeffC = np.dot(np.dot(posVec, D), posVec)-1
        discriminant = coeffB**2 - coeffA*coeffC
        range = targetData.Range(AgEAccessApparentPositionType.eLightPathApparentPosition)
        taus = [(-coeffB + np.sqrt(discriminant))/coeffA, (-coeffB - np.sqrt(discriminant))/coeffA]
        if discriminant < 0:
            # No intersection exists, we are looking at deep space
            result.Value = float((range < self.SpaceRange))
        else:
            # Intersection exists, determine lighting condition
            tau = min(n for n in taus if n>0)
            earthIntersect = posVec + losVecScaled * tau
            sens2SunVec = np.array(baseData.ApparentSunPosition_Array(AgEUtFrame.eUtFrameFixed))
            intersect2SunVec = sens2SunVec + posVec - earthIntersect
            intersect2SunVecHat = intersect2SunVec / np.linalg.norm(intersect2SunVec)
            intersectNormalHat = np.dot(D,earthIntersect)/np.linalg.norm(np.dot(D,earthIntersect))
            sunDistance = np.linalg.norm(intersect2SunVec)
            sunDiskAngle = self.sunRadius/sunDistance
            sunPosAngle = np.arctan2(np.dot(intersect2SunVecHat, intersectNormalHat), np.linalg.norm(np.cross(intersect2SunVecHat, intersectNormalHat)))
            if sunPosAngle > -sunDiskAngle:
                result.Value = float(range < self.DayRange)
            else:
                result.Value = float(range < self.NightRange)
        return True

    def PostCompute(self, result:"IAgAccessConstraintPluginResultPostCompute") -> bool:
        '''
        Triggered after the calls to the Evaluate method, to allow for any required clean up.
        '''
        return True

    def Free(self) -> None:
        '''
        Triggered just before the plugin is destroyed.
        '''
        del(self.scope)
        del(self.site)
        del(self.DayRange)
        del(self.NightRange)
        del(self.a)
        del(self.b)
        del(self.c)
        
    def GetPluginConfig(self, pAttrBuilder:"IAgAttrBuilder") -> typing.Any:
        '''
        Get an attribute container of the configuration settings.
        '''
        if self.scope is None:
            self.scope = pAttrBuilder.NewScope()
            pAttrBuilder.AddQuantityMinDispatchProperty2(self.scope, "DayRange", "Range of sensor when background is lit Earth", "DayRange", "Distance", "km", "m", 0.0, AgEAttrAddFlags.eAddFlagNone)
            pAttrBuilder.AddQuantityMinDispatchProperty2(self.scope, "NightRange", "Range of sensor when background is unlit Earth", "NightRange", "Distance", "km", "m", 0.0, AgEAttrAddFlags.eAddFlagNone)
            pAttrBuilder.AddQuantityMinDispatchProperty2(self.scope, "SpaceRange", "Range of sensor when background is space", "SpaceRange", "Distance", "km", "m", 0.0, AgEAttrAddFlags.eAddFlagNone)
        return self.scope
        
    def VerifyPluginConfig(self, pPluginCfgResult:"IAgUtPluginConfigVerifyResult") -> None:
        '''
        Verify the Plugin Config
        '''
        pass
# This implements the Venus GRAM in STK under the GRAM common framework.
# This plugin was most recently tested with GRAM 2.1.0.
# This plugin requires the built GRAMpy module to interface with the GRAM suite.
# A copy of the GRAM Suite can be obtained through the NASA Technology Transfer Program.

import typing, math
from agi.stk12.plugins.utplugin         import AgEUtLogMsgType
from agi.stk12.plugins.attrautomation   import AgEAttrAddFlags
from agi.stk12.plugins.stkplugin        import AgStkPluginSite
from agi.stk12.vgt                      import AgECrdnSmartEpochState
from GRAMpy                             import gram

class CAgAsDensityModelPlugin(object):
    def __init__(self):
        # Debug flags
        self.DebugMode = False
        self.PluginEnabled = True
        self.MessageInterval = 1000
        self.MsgCntr = -1
        self.IntegSteps = 0

        # INPUTS
        self.CentralBodyName = 'Venus'
        self.LowestValidAltKm = 0
        self.SpicePath = r'\SPICE'
        self.InitialRandomSeed = 1001
        self.DensityPerturbationScale = 1.0
        self.MinimumRelativeStepSize = 0.0
        self.FastModeOn = 0
        self.DensityType = 'Mean'

        # STK specific, do not change
        self.Scope = None
        self.Site = None
        self.Root = None
        self.Density = None
        self.DisplayName = 'VenusGRAM Suite Atmosphere Plugin'
        self.OutputLowAltMsg = True
        self.ComputingTemperature = True
        self.ComputingPressure = True
      
        # GRAM specific, do not change
        self.venus = None
        self.position = None
    
    def Init(self, Site:"IAgUtPluginSite") -> bool:
        self.Site = AgStkPluginSite(Site)
        self.Root = self.Site.StkRootObject

        # Parse scenario epoch
        self.Root.UnitPreferences.SetCurrentUnit('DateFormat', 'YYYY:MM:DD') # YYYY:MM:DD:HH:MM:SS.sss Time
        smartEpoch = self.Root.CurrentScenario.AnalysisEpoch
        if smartEpoch.State == AgECrdnSmartEpochState.eCrdnSmartEpochStateExplicit:
            epoch = smartEpoch.TimeInstant
        else:
            epoch = smartEpoch.ReferenceEvent.FindOccurrence().Epoch
        epochSplit = epoch.split(':')
        self.Root.UnitPreferences.SetCurrentUnit('DateFormat', 'UTCG')
        
        # Set up GRAM
        inputParameters = gram.VenusInputParameters()
        inputParameters.timeFrame = gram.PET
        inputParameters.spicePath = self.SpicePath
        inputParameters.initialRandomSeed = self.InitialRandomSeed
        inputParameters.densityPerturbationScale = self.DensityPerturbationScale
        inputParameters.minRelativeStepSize = self.MinimumRelativeStepSize
        inputParameters.fastModeOn = self.FastModeOn
        self.venus = gram.VenusAtmosphere()
        self.venus.setInputParameters(inputParameters)

        # Configure GRAM start time and position
        self.position = gram.Position()
        ttime = gram.GramTime()
        ttime.setStartTime(int(epochSplit[0]), int(epochSplit[1]), int(epochSplit[2]), int(epochSplit[3]), int(epochSplit[4]), float(epochSplit[5]), gram.UTC, gram.PET)
        self.venus.setStartTime(ttime)

        if self.DebugMode:
            self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, "VenusGRAMPlugin:Init()")
            if self.PluginEnabled:
                self.Site.Message(AgEUtLogMsgType.eUtLogMsgInfo, "VenusGRAMPlugin:Init() Enabled")
            else:
                self.Site.Message(AgEUtLogMsgType.eUtLogMsgInfo, "VenusGRAMPlugin:Init() Disabled because Enabled flag is False")
        if not self.PluginEnabled:
            self.Site.Message(AgEUtLogMsgType.eUtLogMsgAlarm, "VenusGRAMPlugin:Init() Disabled because Enabled flag is False")
        return self.PluginEnabled

    def Register(self, Result:"IAgAsDensityModelResultRegister"):
        if self.DebugMode:
            Result.Message(AgEUtLogMsgType.eUtLogMsgDebug, "VenusGRAMPlugin:Register()")
    
    def Free(self) -> bool:
        self.Site = None
        return True

    # PreCompute is a member of the optional IAgAsDensityModelPluginExtended interface
    def PreCompute(self, Result:"IAgAsDensityModelResult") -> bool:
        self.IntegSteps = 0
        self.MsgCntr = -1
        if self.PluginEnabled:
            self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, "PreCompute() called.")

            if self.DebugMode:
                self.TestResultInterface("PreCompute()", Result)
            
            return True
        return False

    # PreNextStep is a member of the optional IAgAsDensityModelPluginExtended interface
    def PreNextStep(self, Result:"IAgAsDensityModelResult") -> bool:
        if self.PluginEnabled:
            RptCnt = self.MessageInterval
            if self.DebugMode:
                if self.IntegSteps % RptCnt == 0:
                    self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, f'PreNextStep(): Integration Step: {self.IntegSteps}')
                if self.IntegSteps == 0:
                    self.TestResultInterface("PreNextStep()", Result)
            
            self.IntegSteps += 1

            return True
        return False

    # PostCompute is a member of the optional IAgAsDensityModelPluginExtended interface
    def PostCompute(self, Result:"IAgAsDensityModelResult") -> bool:
        if self.PluginEnabled:
            self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, "PostCompute() called.")
            if self.DebugMode:
                self.TestResultInterface("PostCompute()", Result)

            return True
        return False
    
    def Evaluate(self, ResultEval:"IAgAsDensityModelResultEval") -> bool:
        self.MsgCntr += 1
        if self.PluginEnabled:
            self.PluginEnabled = self.SetDensity(ResultEval)
            if self.DebugMode and self.MsgCntr == 0:
                self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, "Evaluate() called.")
                self.TestResultEvalInterface("Evaluate()", ResultEval)

        return self.PluginEnabled
    
    def CentralBody(self) -> str:
        return self.CentralBodyName
    
    def ComputesTemperature(self) -> bool:
        return self.ComputingTemperature
    
    def ComputesPressure(self) -> bool:
        return self.ComputingPressure
    
    def UsesAugmentedSpaceWeather(self) -> bool:
        return False
    
    def GetLowestValidAltitude(self) -> bool:
        return self.LowestValidAltKm
    
    def OverrideAtmFluxLags(self, FluxLags: "IAgAsDensityModelPLuginAtmFluxLagsConfig") -> bool:
        return True

    def SetDensity(self, ResultEval:"IAgAsDensityModelResultEval") -> bool:
        # Query STK for time and LLA
        stkTime = float(ResultEval.DateString('EpSec'))
        LLA = ResultEval.LatLonAlt_Array()
        LatitudeDeg = math.degrees(LLA[0])
        LongitudeDeg = math.degrees(LLA[1])
        AltitudeKm = LLA[2] / 1000

        # Validate altitude
        if AltitudeKm < self.LowestValidAltKm:
            if self.OutputLowAltMsg:
                msg = f'setDensity: altitude {AltitudeKm:1.6f} is less than minimum valid altitude ( {self.LowestValidAltKm} km ). Keeping density constant below this height.'
                self.Site.Message(AgEUtLogMsgType.eUtLogMsgWarning, f'{msg}')
                self.OutputLowAltMsg = False
            AltitudeKm = self.LowestValidAltKm

        if self.DebugMode:
            if self.MsgCntr % self.MessageInterval == 0:
                msg = f'setDensity: time = {stkTime:02.6f} EpSec'
                self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, f'{msg}')
                msg = f'setDensity: lat = {LatitudeDeg:1.6f}, lon = {LongitudeDeg:1.6f}, alt = {AltitudeKm:1.6f}'
                self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, f'{msg}')

        # Set GRAM position
        self.position.elapsedTime = stkTime        
        self.position.latitude = LatitudeDeg
        self.position.longitude = LongitudeDeg
        self.position.height = AltitudeKm
        self.venus.setPosition(self.position)

        # Update GRAM atmosphere
        self.venus.update()
        atmos = self.venus.getAtmosphereState()
        if self.DensityType == 'Mean':
            self.Density = atmos.density
        elif self.DensityType == 'Low':
            self.Density = atmos.lowDensity
        elif self.DensityType == 'High':
            self.Density = atmos.highDensity
        else:
            self.Density = atmos.perturbedDensity

        # Push result back to STK
        ResultEval.SetDensity(self.Density)
        ResultEval.SetPressure(atmos.pressure)
        ResultEval.SetTemperature(atmos.temperature)

        if self.DebugMode:
            if self.MsgCntr % self.MessageInterval == 0:
                msg = f'setDensity: density {self.Density}'
                self.Site.Message(AgEUtLogMsgType.eUtLogMsgDebug, f'{msg}')        
        return True

    def TestResultInterface(self, Name:"str", Result:"IAgAsDensityModelResult"):
        if not self.DebugMode:
            return
        return

    def TestResultEvalInterface(self, Name:"str", ResultEval:"IAgAsDensityModelResultEval"):
        if not self.DebugMode:
            return
        return

    def GetPluginConfig(self, pAttrBuilder:"IAgAttrBuilder") -> typing.Any:
        if self.Scope is None:
            self.Scope = pAttrBuilder.NewScope()
            pAttrBuilder.AddStringDispatchProperty( self.Scope, "CentralBodyName", "CentralBodyName", "CentralBodyName", AgEAttrAddFlags.eAddFlagReadOnly)
            pAttrBuilder.AddStringDispatchProperty( self.Scope, "SpicePath", "SpicePath", "SpicePath", AgEAttrAddFlags.eAddFlagNone )
            pAttrBuilder.AddIntMinMaxDispatchProperty( self.Scope, "InitialRandomSeed", "InitialRandomSeed", "InitialRandomSeed", 1, 29999, AgEAttrAddFlags.eAddFlagNone )
            pAttrBuilder.AddDoubleMinMaxDispatchProperty( self.Scope, "DensityPerturbationScale", "DensityPerturbationScale", "DensityPerturbationScale", 0.0, 2.0, AgEAttrAddFlags.eAddFlagNone )
            pAttrBuilder.AddDoubleMinMaxDispatchProperty( self.Scope, "MinimumRelativeStepSize", "MinimumRelativeStepSize", "MinimumRelativeStepSize", 0.0, 1.0, AgEAttrAddFlags.eAddFlagNone )
            pAttrBuilder.AddBoolDispatchProperty( self.Scope, "FastModeOn", "FastModeOn", "FastModeOn", AgEAttrAddFlags.eAddFlagNone )
            pAttrBuilder.AddChoicesDispatchProperty( self.Scope, "DensityType", "Density Type", "DensityType", ["Low", "Mean", "High", "Perturbed"] )
        return self.Scope
        
    def VerifyPluginConfig(self, pPluginCfgResult:"IAgUtPluginConfigVerifyResult") -> bool:
        pPluginCfgResult.Result = True
        pPluginCfgResult.Message = "Ok"
        return True
        

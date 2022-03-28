'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================

'==========================================
' AgEUtFrame Enumeration
'==========================================
Dim eUtFrameInertial, eUtFrameFixed, eUtFrameLVLH, eUtFrameNTC, eUtFrameICRF, eUtFrameJ2000
eUtFrameInertial          = 0
eUtFrameFixed             = 1
eUtFrameLVLH              = 2
eUtFrameNTC               = 3
eUtFrameICRF              = 4
eUtFrameJ2000             = 5

'==========================================
' AgEUtTimeScale Enumeration
'==========================================
Dim eUTC, eTAI, eTDT, eUT1, eSTKEpochSec, eTDB, eGPS
eUTC             = 0
eTAI             = 1
eTDT             = 2
eUT1             = 3
eSTKEpochSec     = 4
eTDB             = 5
eGPS             = 6

'==========================================
' AgELogMsgType Enumeration
'==========================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm
eLogMsgDebug         = 0
eLogMsgInfo          = 1
eLogMsgForceInfo     = 2
eLogMsgWarning       = 3
eLogMsgAlarm         = 4

'===========================================
' AgEAttrAddFlags Enumeration
'===========================================
Dim eFlagNone, eFlagTransparent, eFlagHidden, eFlagTransient, eFlagReadOnly, eFlagFixed
eFlagNone            = 0
eFlagTransparent     = 2
eFlagHidden          = 4
eFlagTransient       = 8
eFlagReadOnly        = 16
eFlagFixed           = 32

'==========================================
' AgEAsEOMFuncPluginEventTypes Enumeration
'==========================================
Dim eEventTypesPrePropagate, eEventTypesPreNextStep, eEventTypesEvaluate, eEventTypesPostPropagate

eEventTypesPrePropagate = 0
eEventTypesPreNextStep = 1
eEventTypesEvaluate = 2
eEventTypesPostPropagate = 3

'==========================================
' State Values Enumeration
'==========================================
dim eEOMFuncPluginInputStateValuesPosY
eEOMFuncPluginInputStateValuesPosY = 1
dim eEOMFuncPluginInputStateValuesPosZ
eEOMFuncPluginInputStateValuesPosZ = 2
dim eEOMFuncPluginInputStateValuesVelX
eEOMFuncPluginInputStateValuesVelX = 3
dim eEOMFuncPluginInputStateValuesVelY
eEOMFuncPluginInputStateValuesVelY = 4
dim eEOMFuncPluginInputStateValuesVelZ
eEOMFuncPluginInputStateValuesVelZ = 5
dim eEOMFuncPluginInputStateValuesPosCBFX
eEOMFuncPluginInputStateValuesPosCBFX = 6
dim eEOMFuncPluginInputStateValuesPosCBFY
eEOMFuncPluginInputStateValuesPosCBFY = 7
dim eEOMFuncPluginInputStateValuesPosCBFZ
eEOMFuncPluginInputStateValuesPosCBFZ = 8
dim eEOMFuncPluginInputStateValuesVelCBFX
eEOMFuncPluginInputStateValuesVelCBFX = 9
dim eEOMFuncPluginInputStateValuesVelCBFY
eEOMFuncPluginInputStateValuesVelCBFY = 10
dim eEOMFuncPluginInputStateValuesVelCBFZ
eEOMFuncPluginInputStateValuesVelCBFZ = 11
dim eEOMFuncPluginInputStateValuesCBIVelInCBFX
eEOMFuncPluginInputStateValuesCBIVelInCBFX = 12
dim eEOMFuncPluginInputStateValuesCBIVelInCBFY
eEOMFuncPluginInputStateValuesCBIVelInCBFY = 13
dim eEOMFuncPluginInputStateValuesCBIVelInCBFZ
eEOMFuncPluginInputStateValuesCBIVelInCBFZ = 14
dim eEOMFuncPluginInputStateValuesQuat1
eEOMFuncPluginInputStateValuesQuat1 = 15
dim eEOMFuncPluginInputStateValuesQuat2
eEOMFuncPluginInputStateValuesQuat2 = 16
dim eEOMFuncPluginInputStateValuesQuat3
eEOMFuncPluginInputStateValuesQuat3 = 17
dim eEOMFuncPluginInputStateValuesQuat4
eEOMFuncPluginInputStateValuesQuat4 = 18
dim eEOMFuncPluginInputStateValuesCBIToCBF00
eEOMFuncPluginInputStateValuesCBIToCBF00 = 19
dim eEOMFuncPluginInputStateValuesCBIToCBF01
eEOMFuncPluginInputStateValuesCBIToCBF01 = 20
dim eEOMFuncPluginInputStateValuesCBIToCBF02
eEOMFuncPluginInputStateValuesCBIToCBF02 = 21
dim eEOMFuncPluginInputStateValuesCBIToCBF10
eEOMFuncPluginInputStateValuesCBIToCBF10 = 22
dim eEOMFuncPluginInputStateValuesCBIToCBF11
eEOMFuncPluginInputStateValuesCBIToCBF11 = 23
dim eEOMFuncPluginInputStateValuesCBIToCBF12
eEOMFuncPluginInputStateValuesCBIToCBF12 = 24
dim eEOMFuncPluginInputStateValuesCBIToCBF20
eEOMFuncPluginInputStateValuesCBIToCBF20 = 25
dim eEOMFuncPluginInputStateValuesCBIToCBF21
eEOMFuncPluginInputStateValuesCBIToCBF21 = 26
dim eEOMFuncPluginInputStateValuesCBIToCBF22
eEOMFuncPluginInputStateValuesCBIToCBF22 = 27
dim eEOMFuncPluginInputStateValuesAngVelCBFX
eEOMFuncPluginInputStateValuesAngVelCBFX = 28
dim eEOMFuncPluginInputStateValuesAngVelCBFY
eEOMFuncPluginInputStateValuesAngVelCBFY = 29
dim eEOMFuncPluginInputStateValuesAngVelCBFZ
eEOMFuncPluginInputStateValuesAngVelCBFZ = 30
dim eEOMFuncPluginInputStateValuesAltitude
eEOMFuncPluginInputStateValuesAltitude = 31
dim eEOMFuncPluginInputStateValuesLatitude
eEOMFuncPluginInputStateValuesLatitude = 32
dim eEOMFuncPluginInputStateValuesLongitude
eEOMFuncPluginInputStateValuesLongitude = 33
dim eEOMFuncPluginInputStateValuesTotalMass
eEOMFuncPluginInputStateValuesTotalMass = 34
dim eEOMFuncPluginInputStateValuesDryMass
eEOMFuncPluginInputStateValuesDryMass = 35
dim eEOMFuncPluginInputStateValuesFuelMass
eEOMFuncPluginInputStateValuesFuelMass = 36
dim eEOMFuncPluginInputStateValuesCd
eEOMFuncPluginInputStateValuesCd = 37
dim eEOMFuncPluginInputStateValuesDragArea
eEOMFuncPluginInputStateValuesDragArea = 38
dim eEOMFuncPluginInputStateValuesAtmosphericDensity
eEOMFuncPluginInputStateValuesAtmosphericDensity = 39
dim eEOMFuncPluginInputStateValuesAtmosphericAltitude
eEOMFuncPluginInputStateValuesAtmosphericAltitude = 40
dim eEOMFuncPluginInputStateValuesCr
eEOMFuncPluginInputStateValuesCr = 41
dim eEOMFuncPluginInputStateValuesSRPArea
eEOMFuncPluginInputStateValuesSRPArea = 42
dim eEOMFuncPluginInputStateValuesKr1
eEOMFuncPluginInputStateValuesKr1 = 43
dim eEOMFuncPluginInputStateValuesKr2
eEOMFuncPluginInputStateValuesKr2 = 44
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFX
eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFX = 45
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFY
eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFY = 46
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFZ
eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFZ = 47
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFX
eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFX = 48
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFY
eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFY = 49
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFZ
eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFZ = 50
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosX
eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosX = 51
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosY
eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosY = 52
dim eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosZ
eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosZ = 53
dim eEOMFuncPluginInputStateValuesApparentSunPosCBFX
eEOMFuncPluginInputStateValuesApparentSunPosCBFX = 54
dim eEOMFuncPluginInputStateValuesApparentSunPosCBFY
eEOMFuncPluginInputStateValuesApparentSunPosCBFY = 55
dim eEOMFuncPluginInputStateValuesApparentSunPosCBFZ
eEOMFuncPluginInputStateValuesApparentSunPosCBFZ = 56
dim eEOMFuncPluginInputStateValuesApparentSatPosCBFX
eEOMFuncPluginInputStateValuesApparentSatPosCBFX = 57
dim eEOMFuncPluginInputStateValuesApparentSatPosCBFY
eEOMFuncPluginInputStateValuesApparentSatPosCBFY = 58
dim eEOMFuncPluginInputStateValuesApparentSatPosCBFZ
eEOMFuncPluginInputStateValuesApparentSatPosCBFZ = 59
dim eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosX
eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosX = 60
dim eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosY
eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosY = 61
dim eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosZ
eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosZ = 62
dim eEOMFuncPluginInputStateValuesTrueSunPosCBFX
eEOMFuncPluginInputStateValuesTrueSunPosCBFX = 63
dim eEOMFuncPluginInputStateValuesTrueSunPosCBFY
eEOMFuncPluginInputStateValuesTrueSunPosCBFY = 64
dim eEOMFuncPluginInputStateValuesTrueSunPosCBFZ
eEOMFuncPluginInputStateValuesTrueSunPosCBFZ = 65
dim eEOMFuncPluginInputStateValuesTrueSatPosCBFX
eEOMFuncPluginInputStateValuesTrueSatPosCBFX = 66
dim eEOMFuncPluginInputStateValuesTrueSatPosCBFY
eEOMFuncPluginInputStateValuesTrueSatPosCBFY = 67
dim eEOMFuncPluginInputStateValuesTrueSatPosCBFZ
eEOMFuncPluginInputStateValuesTrueSatPosCBFZ = 68
dim eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosX
eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosX = 69
dim eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosY
eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosY = 70
dim eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosZ
eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosZ = 71
dim eEOMFuncPluginInputStateValuesSolarIntensity
eEOMFuncPluginInputStateValuesSolarIntensity = 72
dim eEOMFuncPluginInputStateValuesRadPressureCoefficient
eEOMFuncPluginInputStateValuesRadPressureCoefficient = 73
dim eEOMFuncPluginInputStateValuesRadPressureArea
eEOMFuncPluginInputStateValuesRadPressureArea = 74
dim eEOMFuncPluginInputStateValuesMassFlowRate
eEOMFuncPluginInputStateValuesMassFlowRate = 75
dim eEOMFuncPluginInputStateValuesTankPressure
eEOMFuncPluginInputStateValuesTankPressure = 76
dim eEOMFuncPluginInputStateValuesTankTemperature
eEOMFuncPluginInputStateValuesTankTemperature = 77
dim eEOMFuncPluginInputStateValuesFuelDensity
eEOMFuncPluginInputStateValuesFuelDensity = 78
dim eEOMFuncPluginInputStateValuesThrustX
eEOMFuncPluginInputStateValuesThrustX = 79
dim eEOMFuncPluginInputStateValuesThrustY
eEOMFuncPluginInputStateValuesThrustY = 80
dim eEOMFuncPluginInputStateValuesThrustZ
eEOMFuncPluginInputStateValuesThrustZ = 81
dim eEOMFuncPluginInputStateValuesDeltaV
eEOMFuncPluginInputStateValuesDeltaV = 82
dim eEOMFuncPluginInputStateValuesGravityAccelX
eEOMFuncPluginInputStateValuesGravityAccelX = 83
dim eEOMFuncPluginInputStateValuesGravityAccelY
eEOMFuncPluginInputStateValuesGravityAccelY = 84
dim eEOMFuncPluginInputStateValuesGravityAccelZ
eEOMFuncPluginInputStateValuesGravityAccelZ = 85
dim eEOMFuncPluginInputStateValuesTwoBodyAccelX
eEOMFuncPluginInputStateValuesTwoBodyAccelX = 86
dim eEOMFuncPluginInputStateValuesTwoBodyAccelY
eEOMFuncPluginInputStateValuesTwoBodyAccelY = 87
dim eEOMFuncPluginInputStateValuesTwoBodyAccelZ
eEOMFuncPluginInputStateValuesTwoBodyAccelZ = 88
dim eEOMFuncPluginInputStateValuesGravityPertAccelX
eEOMFuncPluginInputStateValuesGravityPertAccelX = 89
dim eEOMFuncPluginInputStateValuesGravityPertAccelY
eEOMFuncPluginInputStateValuesGravityPertAccelY = 90
dim eEOMFuncPluginInputStateValuesGravityPertAccelZ
eEOMFuncPluginInputStateValuesGravityPertAccelZ = 91
dim eEOMFuncPluginInputStateValuesSolidTidesAccelX
eEOMFuncPluginInputStateValuesSolidTidesAccelX = 92
dim eEOMFuncPluginInputStateValuesSolidTidesAccelY
eEOMFuncPluginInputStateValuesSolidTidesAccelY = 93
dim eEOMFuncPluginInputStateValuesSolidTidesAccelZ
eEOMFuncPluginInputStateValuesSolidTidesAccelZ = 94
dim eEOMFuncPluginInputStateValuesOceanTidesAccelX
eEOMFuncPluginInputStateValuesOceanTidesAccelX = 95
dim eEOMFuncPluginInputStateValuesOceanTidesAccelY
eEOMFuncPluginInputStateValuesOceanTidesAccelY = 96
dim eEOMFuncPluginInputStateValuesOceanTidesAccelZ
eEOMFuncPluginInputStateValuesOceanTidesAccelZ = 97
dim eEOMFuncPluginInputStateValuesDragAccelX
eEOMFuncPluginInputStateValuesDragAccelX = 98
dim eEOMFuncPluginInputStateValuesDragAccelY
eEOMFuncPluginInputStateValuesDragAccelY = 99
dim eEOMFuncPluginInputStateValuesDragAccelZ
eEOMFuncPluginInputStateValuesDragAccelZ = 100
dim eEOMFuncPluginInputStateValuesThirdBodyAccelX
eEOMFuncPluginInputStateValuesThirdBodyAccelX = 101
dim eEOMFuncPluginInputStateValuesThirdBodyAccelY
eEOMFuncPluginInputStateValuesThirdBodyAccelY = 102
dim eEOMFuncPluginInputStateValuesThirdBodyAccelZ
eEOMFuncPluginInputStateValuesThirdBodyAccelZ = 103
dim eEOMFuncPluginInputStateValuesSRPAccelX
eEOMFuncPluginInputStateValuesSRPAccelX = 104
dim eEOMFuncPluginInputStateValuesSRPAccelY
eEOMFuncPluginInputStateValuesSRPAccelY = 105
dim eEOMFuncPluginInputStateValuesSRPAccelZ
eEOMFuncPluginInputStateValuesSRPAccelZ = 106
dim eEOMFuncPluginInputStateValuesNoShadowSRPAccelX
eEOMFuncPluginInputStateValuesNoShadowSRPAccelX = 107
dim eEOMFuncPluginInputStateValuesNoShadowSRPAccelY
eEOMFuncPluginInputStateValuesNoShadowSRPAccelY = 108
dim eEOMFuncPluginInputStateValuesNoShadowSRPAccelZ
eEOMFuncPluginInputStateValuesNoShadowSRPAccelZ = 109
dim eEOMFuncPluginInputStateValuesGenRelAccelX
eEOMFuncPluginInputStateValuesGenRelAccelX = 110
dim eEOMFuncPluginInputStateValuesGenRelAccelY
eEOMFuncPluginInputStateValuesGenRelAccelY = 111
dim eEOMFuncPluginInputStateValuesGenRelAccelZ
eEOMFuncPluginInputStateValuesGenRelAccelZ = 112
dim eEOMFuncPluginInputStateValuesAlbedoAccelX
eEOMFuncPluginInputStateValuesAlbedoAccelX = 113
dim eEOMFuncPluginInputStateValuesAlbedoAccelY
eEOMFuncPluginInputStateValuesAlbedoAccelY = 114
dim eEOMFuncPluginInputStateValuesAlbedoAccelZ
eEOMFuncPluginInputStateValuesAlbedoAccelZ = 115
dim eEOMFuncPluginInputStateValuesThermalPressureAccelX
eEOMFuncPluginInputStateValuesThermalPressureAccelX = 116
dim eEOMFuncPluginInputStateValuesThermalPressureAccelY
eEOMFuncPluginInputStateValuesThermalPressureAccelY = 117
dim eEOMFuncPluginInputStateValuesThermalPressureAccelZ
eEOMFuncPluginInputStateValuesThermalPressureAccelZ = 118
dim eEOMFuncPluginInputStateValuesAddedAccelX
eEOMFuncPluginInputStateValuesAddedAccelX = 119
dim eEOMFuncPluginInputStateValuesAddedAccelY
eEOMFuncPluginInputStateValuesAddedAccelY = 120
dim eEOMFuncPluginInputStateValuesAddedAccelZ
eEOMFuncPluginInputStateValuesAddedAccelZ = 121
dim eEOMFuncPluginInputStateValuesStateTransPosXPosX
eEOMFuncPluginInputStateValuesStateTransPosXPosX = 122
dim eEOMFuncPluginInputStateValuesStateTransPosXPosY
eEOMFuncPluginInputStateValuesStateTransPosXPosY = 123
dim eEOMFuncPluginInputStateValuesStateTransPosXPosZ
eEOMFuncPluginInputStateValuesStateTransPosXPosZ = 124
dim eEOMFuncPluginInputStateValuesStateTransPosXVelX
eEOMFuncPluginInputStateValuesStateTransPosXVelX = 125
dim eEOMFuncPluginInputStateValuesStateTransPosXVelY
eEOMFuncPluginInputStateValuesStateTransPosXVelY = 126
dim eEOMFuncPluginInputStateValuesStateTransPosXVelZ
eEOMFuncPluginInputStateValuesStateTransPosXVelZ = 127
dim eEOMFuncPluginInputStateValuesStateTransPosYPosX
eEOMFuncPluginInputStateValuesStateTransPosYPosX = 128
dim eEOMFuncPluginInputStateValuesStateTransPosYPosY
eEOMFuncPluginInputStateValuesStateTransPosYPosY = 129
dim eEOMFuncPluginInputStateValuesStateTransPosYPosZ
eEOMFuncPluginInputStateValuesStateTransPosYPosZ = 130
dim eEOMFuncPluginInputStateValuesStateTransPosYVelX
eEOMFuncPluginInputStateValuesStateTransPosYVelX = 131
dim eEOMFuncPluginInputStateValuesStateTransPosYVelY
eEOMFuncPluginInputStateValuesStateTransPosYVelY = 132
dim eEOMFuncPluginInputStateValuesStateTransPosYVelZ
eEOMFuncPluginInputStateValuesStateTransPosYVelZ = 133
dim eEOMFuncPluginInputStateValuesStateTransPosZPosX
eEOMFuncPluginInputStateValuesStateTransPosZPosX = 134
dim eEOMFuncPluginInputStateValuesStateTransPosZPosY
eEOMFuncPluginInputStateValuesStateTransPosZPosY = 135
dim eEOMFuncPluginInputStateValuesStateTransPosZPosZ
eEOMFuncPluginInputStateValuesStateTransPosZPosZ = 136
dim eEOMFuncPluginInputStateValuesStateTransPosZVelX
eEOMFuncPluginInputStateValuesStateTransPosZVelX = 137
dim eEOMFuncPluginInputStateValuesStateTransPosZVelY
eEOMFuncPluginInputStateValuesStateTransPosZVelY = 138
dim eEOMFuncPluginInputStateValuesStateTransPosZVelZ
eEOMFuncPluginInputStateValuesStateTransPosZVelZ = 139
dim eEOMFuncPluginInputStateValuesStateTransVelXPosX
eEOMFuncPluginInputStateValuesStateTransVelXPosX = 140
dim eEOMFuncPluginInputStateValuesStateTransVelXPosY
eEOMFuncPluginInputStateValuesStateTransVelXPosY = 141
dim eEOMFuncPluginInputStateValuesStateTransVelXPosZ
eEOMFuncPluginInputStateValuesStateTransVelXPosZ = 142
dim eEOMFuncPluginInputStateValuesStateTransVelXVelX
eEOMFuncPluginInputStateValuesStateTransVelXVelX = 143
dim eEOMFuncPluginInputStateValuesStateTransVelXVelY
eEOMFuncPluginInputStateValuesStateTransVelXVelY = 144
dim eEOMFuncPluginInputStateValuesStateTransVelXVelZ
eEOMFuncPluginInputStateValuesStateTransVelXVelZ = 145
dim eEOMFuncPluginInputStateValuesStateTransVelYPosX
eEOMFuncPluginInputStateValuesStateTransVelYPosX = 146
dim eEOMFuncPluginInputStateValuesStateTransVelYPosY
eEOMFuncPluginInputStateValuesStateTransVelYPosY = 147
dim eEOMFuncPluginInputStateValuesStateTransVelYPosZ
eEOMFuncPluginInputStateValuesStateTransVelYPosZ = 148
dim eEOMFuncPluginInputStateValuesStateTransVelYVelX
eEOMFuncPluginInputStateValuesStateTransVelYVelX = 149
dim eEOMFuncPluginInputStateValuesStateTransVelYVelY
eEOMFuncPluginInputStateValuesStateTransVelYVelY = 150
dim eEOMFuncPluginInputStateValuesStateTransVelYVelZ
eEOMFuncPluginInputStateValuesStateTransVelYVelZ = 151
dim eEOMFuncPluginInputStateValuesStateTransVelZPosX
eEOMFuncPluginInputStateValuesStateTransVelZPosX = 152
dim eEOMFuncPluginInputStateValuesStateTransVelZPosY
eEOMFuncPluginInputStateValuesStateTransVelZPosY = 153
dim eEOMFuncPluginInputStateValuesStateTransVelZPosZ
eEOMFuncPluginInputStateValuesStateTransVelZPosZ = 154
dim eEOMFuncPluginInputStateValuesStateTransVelZVelX
eEOMFuncPluginInputStateValuesStateTransVelZVelX = 155
dim eEOMFuncPluginInputStateValuesStateTransVelZVelY
eEOMFuncPluginInputStateValuesStateTransVelZVelY = 156
dim eEOMFuncPluginInputStateValuesStateTransVelZVelZ
eEOMFuncPluginInputStateValuesStateTransVelZVelZ = 157

'================================
' Global Variables
'================================
Dim m_AgUtPluginSite
Dim m_AgAttrScope
Dim m_CrdnPluginProvider
Dim m_CrdnConfiguredAxes

Set m_AgUtPluginSite 	   = Nothing
Set m_AgAttrScope 		   = Nothing
Set m_CrdnPluginProvider   = Nothing
Set m_CrdnConfiguredAxes   = Nothing

Dim m_DeltaVAxes

m_DeltaVAxes = "VNC(Earth)"

dim m_thrustXIndex
dim m_thrustYIndex
dim m_thrustZIndex
dim m_massIndex
dim m_effectiveImpulseIndex
dim m_integratedDeltaVxIndex
dim m_integratedDeltaVyIndex
dim m_integratedDeltaVzIndex

m_thrustXIndex = 0
m_thrustYIndex = 0
m_thrustZIndex = 0
m_massIndex = 0
m_effectiveImpulseIndex = 0
m_integratedDeltaVxIndex = 0
m_integratedDeltaVyIndex = 0
m_integratedDeltaVzIndex = 0

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
   
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		' Create an attribute for the delta-V axes, so it appears on the panel.
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "DeltaVAxes", "Axes in which to integrate delta-V", "DeltaVAxes", 0)
		

	End If

	Set GetPluginConfig = m_AgAttrScope

End Function  

'===========================
' VerifyPluginConfig method
'===========================
Function VerifyPluginConfig(AgUtPluginConfigVerifyResult)
   
    Dim Result
    Dim Message

	Result = true
	Message = "Ok"

	AgUtPluginConfigVerifyResult.Result  = Result
	AgUtPluginConfigVerifyResult.Message = Message

End Function  

'======================
' Init Method
'======================
Function Init( AgUtPluginSite )

    Dim ret
    ret = false

	Set m_AgUtPluginSite = AgUtPluginSite
	
	If( Not m_AgUtPluginSite is Nothing ) Then
	
		Set m_CrdnPluginProvider 	= m_AgUtPluginSite.VectorToolProvider
		
		If(Not m_CrdnPluginProvider is Nothing) Then

            ' we'll use this to rotate from inertial to the specified axes
			Set m_CrdnConfiguredAxes  = m_CrdnPluginProvider.ConfigureAxes( "Inertial", "CentralBody/Earth", m_DeltaVAxes, "")
			
			If (Not m_CrdnConfiguredAxes is Nothing) Then
			
			    ret = true
			    
			End If
			
		End If			
		
	End If
	
    Init = ret

End Function
 

'======================
' Register Method
'======================
Function Register( AgAsEOMFuncPluginRegisterHandler )

    ' plugin needs the thrust vector and the mass
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesThrustX)
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesThrustY)
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesThrustZ)
    
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesTotalMass)

    
    ' plugin gives the derivative of effective impulse and integrated delta-V
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("EffectiveImpulse")
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVx")
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVy")
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVz")

    ' plugin only needs to be called on evaluate
    Call AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPrePropagate)
    Call AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPreNextStep)
    Call AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPostPropagate)
    
    Register = true

End Function


'======================
' SetIndices Function
'======================
Function SetIndices( AgAsEOMFuncPluginSetIndicesHandler )

    ' get the indices for the input variables
    m_thrustXIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesThrustX)
    m_thrustYIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesThrustY)
    m_thrustZIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesThrustZ)
    m_massIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesTotalMass)
    
    ' get the indices for the derivatives we will output
    m_effectiveImpulseIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("EffectiveImpulse")
    m_integratedDeltaVxIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVx")
    m_integratedDeltaVyIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVy")
    m_integratedDeltaVzIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVz")

    SetIndices = true

End Function

'=================
' Calc Method
'=================
Function Calc( eventType, AgAsEOMFuncPluginStateVector )

    ' get the current thrust values, and give back the derivatives of
    ' effective impulse and the integrated delta V components

    ' get thrust
    Dim thrustX, thrustY, thrustZ
    thrustX = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustXIndex)
    thrustY = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustYIndex)
    thrustZ = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustZIndex)

    ' get mass
    Dim mass
    mass = AgAsEOMFuncPluginStateVector.GetInputValue(m_massIndex)
    

    ' derivative of effective impulse is the total thrust magnitude
    Dim thrustMag
    thrustMag = Sqr(thrustX*thrustX + thrustY*thrustY + thrustZ*thrustZ)
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_effectiveImpulseIndex, thrustMag)
    
    ' rotate thrust vector to desired integration frame for integrated delta-V
    Dim thrustArray
    thrustArray = m_CrdnConfiguredAxes.TransformComponents_Array(AgAsEOMFuncPluginStateVector, thrustX, thrustY, thrustZ)    
    ' the derivative of each integrated delta-V component is that component of thrust acceleration 
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVxIndex, thrustArray(0) / mass)
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVyIndex, thrustArray(1) / mass)
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVzIndex, thrustArray(2) / mass)

    Calc = true
    
End Function

'===========================================================
' Free Method
'===========================================================
Sub Free()

	If( Not m_AgUtPluginSite is Nothing ) Then
	
		Set m_AgUtPluginSite 		= Nothing
		Set m_CrdnPluginProvider   	= Nothing
		Set m_CrdnConfiguredAxes 	= Nothing

	End If

End Sub

'============================================================
' DeltaVAxes property
'============================================================
Function GetDeltaVAxes()

       GetDeltaVAxes = m_DeltaVAxes

End Function

Function SetDeltaVAxes(axes)

       m_DeltaVAxes = axes

End Function


'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================

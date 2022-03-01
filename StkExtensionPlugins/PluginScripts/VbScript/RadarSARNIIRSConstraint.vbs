	' ****************************************************************************
	' *                                                                          *
	' * Custom Radar SAR constraint plugin                                       *
	' * Please update within the START & END of the User areas only              *
	' *                                                                          *
	' ****************************************************************************

Option explicit

'flag so that the first computed output is displayed
Dim Verbose
Verbose = -1

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_RadarSARNIIRSConstraint(argArray)
	Dim retVal

	If IsEmpty(argArray(0)) Then
		retVal = VB_RadarSARNIIRSConstraint_compute(argArray)
		
	ElseIf argArray(0) = "register" Then
		retVal = VB_RadarSARNIIRSConstraint_register()

	ElseIf argArray(0) = "compute" Then
		retVal = VB_RadarSARNIIRSConstraint_compute(argArray)
		
	Else
		' bad call
		retVal = Empty
		
	End If

	VB_RadarSARNIIRSConstraint = retVal

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_RadarSARNIIRSConstraint_register()

	ReDim argStr(47)
	' ****************************************************************************
	' STK parameter interface definition
	' All STK (input to the script & the script output) parameters are connected
	' ****************************************************************************
			
	' Outputs	
	argStr(0) ="ArgumentType = Output; Name = RadarSARPluginConstraintValue; ArgumentName = RadarSARPluginConstraintValue"

	' Inputs
	argStr(1)="ArgumentType = Input; Name = DateUTC; ArgumentName = DateUTC; Type = Value"
	argStr(2)="ArgumentType = Input; Name = EpochSec; ArgumentName = EpochSec; Type = Value"
	argStr(3)="ArgumentType = Input; Name = CbName; ArgumentName = CbName; Type = Value"
	argStr(4)="ArgumentType = Input; Name = RadarPath; ArgumentName = RadarPath; Type = Value"
	argStr(5)="ArgumentType = Input; Name = TargetPath; ArgumentName = TargetPath; Type = Value"

	argStr(6)="ArgumentType = Input; Name = RadarTransmitPosCBF; ArgumentName = RadarTransmitPosCBF; Type = Value"
	argStr(7)="ArgumentType = Input; Name = RadarTransmitAttitudeQuat; ArgumentName = RadarTransmitAttitudeQuat; Type = Value"
	argStr(8)="ArgumentType = Input; Name = RadarReceivePosCBF; ArgumentName = RadarReceivePosCBF; Type = Value"
	argStr(9)="ArgumentType = Input; Name = RadarReceiveAttitudeQuat; ArgumentName = RadarReceiveAttitudeQuat; Type = Value"
	argStr(10)="ArgumentType = Input; Name = TargetPosCBF; ArgumentName = TargetPosCBF; Type = Value"
	argStr(11)="ArgumentType = Input; Name = TargetAttitudeQuat; ArgumentName = TargetAttitudeQuat; Type = Value"
	
	argStr(12)="ArgumentType = Input; Name = RadarTransmitterToTargetVecBF; ArgumentName = RadarTransmitterToTargetVecBF; Type = Value"
	argStr(13)="ArgumentType = Input; Name = RadarReceiverToTargetVecBF; ArgumentName = RadarReceiverToTargetVecBF; Type = Value"
	argStr(14)="ArgumentType = Input; Name = TargetToRadarReceiverVecBF; ArgumentName = TargetToRadarReceiverVecBF; Type = Value"
	argStr(15)="ArgumentType = Input; Name = RadarTransmitterToTargetRange; ArgumentName = RadarTransmitterToTargetRange; Type = Value"
	argStr(16)="ArgumentType = Input; Name = RadarReceiverToTargetRange; ArgumentName = RadarReceiverToTargetRange; Type = Value"
	
	argStr(17)="ArgumentType = Input; Name = AngleRate; ArgumentName = AngleRate; Type = Value"
	argStr(18)="ArgumentType = Input; Name = ConeAngle; ArgumentName = ConeAngle; Type = Value"
	argStr(19)="ArgumentType = Input; Name = TransmitPropagationTime; ArgumentName = TransmitePropagationTime; Type = Value"
	argStr(20)="ArgumentType = Input; Name = ReceivePropagationTime; ArgumentName = ReceivePropagationTime; Type = Value"
	argStr(21)="ArgumentType = Input; Name = TransmitRangeRate; ArgumentName = TransmitRangeRate; Type = Value"
	argStr(22)="ArgumentType = Input; Name = ReceiveRangeRate; ArgumentName = ReceiveRangeRate; Type = Value"
	argStr(23)="ArgumentType = Input; Name = RadarSpeed; ArgumentName = RadarSpeed; Type = Value"
	argStr(24)="ArgumentType = Input; Name = RefractedElevationAngle; ArgumentName = RefractedElevationAngle; Type = Value"
	argStr(25)="ArgumentType = Input; Name = RadarTransmitFrequency; ArgumentName = RadarTransmitFrequency; Type = Value"
	argStr(26)="ArgumentType = Input; Name = DopplerShiftedFrequencyAtTarget; ArgumentName = DopplerShiftedFrequencyAtTarget; Type = Value"
	argStr(27)="ArgumentType = Input; Name = DopplerShiftedFrequencyAtRadarReceiver; ArgumentName = DopplerShiftedFrequencyAtRadarReceiver; Type = Value"

	argStr(28)="ArgumentType = Input; Name = SARTimeAzimuthResolution; ArgumentName = SARTimeAzimuthResolution; Type = Value"
	argStr(29)="ArgumentType = Input; Name = SARMaxSceneWidth; ArgumentName = SARMaxSceneWidth; Type = Value"
	argStr(30)="ArgumentType = Input; Name = SARIntegrationTime; ArgumentName = SARIntegrationTime; Type = Value"
	argStr(31)="ArgumentType = Input; Name = SARAzimuthResolution; ArgumentName = SARAzimuthResolution; Type = Value"
	argStr(32)="ArgumentType = Input; Name = SARRangeResolution; ArgumentName = SARRangeResolution; Type = Value"
	argStr(33)="ArgumentType = Input; Name = SARAreaRate; ArgumentName = SARAreaRate; Type = Value"
	argStr(34)="ArgumentType = Input; Name = SARSNR; ArgumentName = SARSNR; Type = Value"
	argStr(35)="ArgumentType = Input; Name = SARSCR; ArgumentName = SARSCR; Type = Value"
	argStr(36)="ArgumentType = Input; Name = SARCNR; ArgumentName = SARCNR; Type = Value"
	argStr(37)="ArgumentType = Input; Name = SARPTCR; ArgumentName = SARPTCR; Type = Value"

	argStr(38)="ArgumentType = Input; Name = SAREffectiveNoiseBackScatter; ArgumentName = SAREffectiveNoiseBackScatter; Type = Value"
	argStr(39)="ArgumentType = Input; Name = SARNoiseBandwidth; ArgumentName = SARNoiseBandwidth; Type = Value"
	argStr(40)="ArgumentType = Input; Name = SARNoisePower; ArgumentName = SARNoisePower; Type = Value"

	argStr(41)="ArgumentType = Input; Name = SARSNRUnderJamming; ArgumentName = SARSNRUnderJamming; Type = Value"
	argStr(42)="ArgumentType = Input; Name = SARSCRUnderJamming; ArgumentName = SARSCRUnderJamming; Type = Value"
	argStr(43)="ArgumentType = Input; Name = SARCNRUnderJamming; ArgumentName = SARCNRUnderJamming; Type = Value"
	argStr(44)="ArgumentType = Input; Name = SARJOverS; ArgumentName = SARJOverS; Type = Value"
	argStr(45)="ArgumentType = Input; Name = SARJammingPower; ArgumentName = SARJammingPower; Type = Value"
	
	VB_RadarSARNIIRSConstraint_register = argStr

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_RadarSARNIIRSConstraint_compute(argArray)

	' ****************************************************************************
	' Declaration of local variables used for STK parameters assinment
	' ****************************************************************************

	' Radar and Target CBF positions and attitudes
	Dim RadarXmtrPosCBF, rdrXmPosX, rdrXmPosY, rdrXmPosZ
	Dim RadarXmtrAttQuat, rdrXmAttq1,rdrXmAttq2,rdrXmAttq3,rdrXmAttq4
	Dim RadarRcvrPosCBF, rdrRcPosX, rdrRcPosY, rdrRcPosZ
	Dim RadarRcvrAttQuat, rdrRcAttq1,rdrRcAttq2,rdrRcAttq3,rdrRcAttq4
	Dim TargetPos, tgtPosX, tgtPosY, tgtPosZ
	Dim TargetAttQuat, tgtAttq1,tgtAttq2,tgtAttq3,tgtAttq4
	
	' Radar - targer vectors
	Dim RadarXmtrTargetVecBF, rdrXmTgtX, rdrXmTgtY, rdrXmTgtZ
	Dim RadarRcvrTargetVecBF, rdrRcTgtX,rdrRcTgtY,rdrRcTgtZ
	Dim TargetRadarRcvrVecBF, tgtRdrRcX,tgtRdrRcY,tgtRdrRcZ
	
	' Radar - target ranges
	Dim rdrXmTgtRange, rdrRcTgtRange
	
	' Radar geomtery data 
	Dim AngleRate, ConeAngle, TransmitPropTime, ReceivePropTime, TransmitRangeRate, ReceiveRangeRate
	Dim RadarSpeed, RefracElevationAngle
	
	' Radar frequency data
	Dim RdrFreq, FreqAtTarget, FrequencyAtRdrRcvr
	
	' Radar SAR data
	Dim SARTimeAziRes, SARMaxSceneWidth, SARIntTime, SARAziRes, SARRangeRes, SARAreaRate, SARSNR, SARSCR, SARCNR, SARPTCR
	Dim	SAREffNoiseBackScatter, SARNoiseBw, SARNoisePower
	Dim SARSNRJam, SARSCRJam, SARCNRJam, SARJOverS, SARJamPower
	
	' ****************************************************************************
	' START of user data area, please update within the user areas only
	' Model specific variables
	' ****************************************************************************
	
	Dim range, DopplerShift
	Dim lamda, f, D, Q
	Dim m2In, mic2m
	Dim sinAngle, GSD, NIIRS, a, b, RER, Hgm, G
	Dim SARSNRLin, dlog10
	
	' ****************************************************************************
	' Model specific data
	' END of user data area, please update within the user areas only
	' ****************************************************************************
	
	Redim retVal(1)
	
	' ****************************************************************************
	' STK parameters assinment to local variables
	' ****************************************************************************

	'RadarTransmitPosCBF
	RadarXmtrPosCBF  = argArray(6)
	rdrXmPosX = RadarXmtrPosCBF(0)
	rdrXmPosY = RadarXmtrPosCBF(1)
	rdrXmPosZ = RadarXmtrPosCBF(2)
	'RadarTransmitAttitudeQuat
	RadarXmtrAttQuat  = argArray(7)
	rdrXmAttq1 = RadarXmtrAttQuat(0)
	rdrXmAttq2 = RadarXmtrAttQuat(1)
	rdrXmAttq3 = RadarXmtrAttQuat(2)
	rdrXmAttq4 = RadarXmtrAttQuat(3)

	'RadarReceivePosCBF
	RadarRcvrPosCBF  = argArray(8)
	rdrRcPosX = RadarRcvrPosCBF(0)
	rdrRcPosY = RadarRcvrPosCBF(1)
	rdrRcPosZ = RadarRcvrPosCBF(2)
	'RadarTransmitAttitudeQuat
	RadarRcvrAttQuat  = argArray(9)
	rdrRcAttq1 = RadarRcvrAttQuat(0)
	rdrRcAttq2 = RadarRcvrAttQuat(1)
	rdrRcAttq3 = RadarRcvrAttQuat(2)
	rdrRcAttq4 = RadarRcvrAttQuat(3)
	
	'TargetPosCBF
	TargetPos  = argArray(10)
	tgtPosX = TargetPos(0)
	tgtPosY = TargetPos(1)
	tgtPosZ = TargetPos(2)
	'TargetAttitudeQuat
	TargetAttQuat  = argArray(11)
	tgtAttq1 = TargetAttQuat(0)
	tgtAttq2 = TargetAttQuat(1)
	tgtAttq3 = TargetAttQuat(2)
	tgtAttq4 = TargetAttQuat(3)
	
	'RadarTransmitterToTargetVecBF
	RadarXmtrTargetVecBF  = argArray(12)
	rdrXmTgtX = RadarXmtrTargetVecBF(0)
	rdrXmTgtY = RadarXmtrTargetVecBF(1)
	rdrXmTgtZ = RadarXmtrTargetVecBF(2)
	'RadarReceiverToTargetVecBF
	RadarRcvrTargetVecBF  = argArray(13)
	rdrRcTgtX = RadarRcvrTargetVecBF(0)
	rdrRcTgtY = RadarRcvrTargetVecBF(1)
	rdrRcTgtZ = RadarRcvrTargetVecBF(2)
	'TargetToRadarReceiverVecBF
	TargetRadarRcvrVecBF  = argArray(14)
	tgtRdrRcX = TargetRadarRcvrVecBF(0)
	tgtRdrRcY = TargetRadarRcvrVecBF(1)
	tgtRdrRcZ = TargetRadarRcvrVecBF(2)
	
	'RadarTransmitterToTargetRange
	rdrXmTgtRange = argArray(15)
	'RadarReceiverToTargetRange
	rdrRcTgtRange = argArray(16)
	
	'AngleRate
	AngleRate = argArray(17)
	'ConeAngle
	ConeAngle = argArray(18)
	'TransmitPropagationTime
	TransmitPropTime = argArray(19)
	'ReceivePropagationTime
	ReceivePropTime = argArray(20)
	'TransmitRangeRate
	TransmitRangeRate = argArray(21)
	'ReceiveRangeRate
	ReceiveRangeRate = argArray(22)
	'RadarSpeed
	RadarSpeed = argArray(23)
	'RefractedElevationAngle
	RefracElevationAngle = argArray(24)
	'RadarTransmitFrequency
	RdrFreq = argArray(25)
	'DopplerShiftedFrequencyAtTarget
	FreqAtTarget = argArray(26)
	'DopplerShiftedFrequencyAtRadarReceiver
	FrequencyAtRdrRcvr = argArray(27)
	
	'SARTimeAzimuthResolution
	SARTimeAziRes = argArray(28)
	'SARMaxSceneWidth
	SARMaxSceneWidth = argArray(29)
	'SARIntegrationTime
	SARIntTime = argArray(30)
	'SARAzimuthResolution
	SARAziRes = argArray(31)
	'SARRangeResolution
	SARRangeRes = argArray(32)
	'SARAreaRate
	SARAreaRate = argArray(33)
	'SARSNR
	SARSNR = argArray(34)
	'SARSCR
	SARSCR = argArray(35)
	'SARCNR
	SARCNR = argArray(36)
	'SARPTCR
	SARPTCR = argArray(37)
	
	'SAREffectiveNoiseBackScatter
	SAREffNoiseBackScatter = argArray(38)
	'SARNoiseBandwidth
	SARNoiseBw = argArray(39)
	'SARNoisePower
	SARNoisePower = argArray(40)

	'SARSNRUnderJamming
	SARSNRJam = argArray(41)
	'SARSCRUnderJamming
	SARSCRJam = argArray(42)
	'SARCNRUnderJamming
	SARCNRJam = argArray(43)
	'SARJOverS
	SARJOverS = argArray(44)
	'SARJammingPower
	SARJamPower = argArray(45)

	' ****************************************************************************
	' START of user model area, please update within the user areas only
	' Model specific equations
	' ****************************************************************************
	
	' range & DopplerShift computed for testing only.  These are not used by the model
	range = Sqr((rdrXmPosX-tgtPosX)^2 + (rdrXmPosY-tgtPosY)^2 + (rdrXmPosZ-tgtPosZ)^2) / 1000
	DopplerShift = FreqAtTarget - RdrFreq 
	
	m2In = 39.37007874015876
	mic2m = 1.0e-6
	' lamda = 299792458.0 / FreqAtTarget
	lamda = 0.65
	D = 10
	Q = 2
	sinAngle = rdrXmTgtZ / rdrXmTgtRange
	GSD = rdrXmTgtRange * lamda * mic2m / (Q * D * Sqr(sinAngle))
	a = 3.32
	b = 1.559
	RER = 0.9
	Hgm = 1.0
	G = 1.0
	dlog10 = Log(10.0)
	
	SARSNRLin = 10^(SARSNR/10.0)
	
	NIIRS = 10.251 - a * Log(GSD * m2In)/dlog10 + b * Log(RER)/dlog10 - 0.656 * Hgm - (0.344 * G / SARSNRLin)

	retVal(0) = NIIRS
	
	' ****************************************************************************
	' END of user model area, please update within the user areas only
	' ****************************************************************************
	
	VB_RadarSARNIIRSConstraint_compute = retVal
	
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	' Display results for first function call only
	If Verbose < 0 Then
		MsgBox "Return Value = " & retVal(0)
		Verbose = 1
	End If
	
End Function
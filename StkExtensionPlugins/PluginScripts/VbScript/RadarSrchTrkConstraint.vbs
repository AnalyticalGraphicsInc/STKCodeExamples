' Custom Radar Search/Track constraint plugin 

Option explicit

'flag so that the first computed output is displayed
Dim Verbose
Verbose = -1

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_RadarSrchTrkConstraint(argArray)
	Dim retVal

	If IsEmpty(argArray(0)) Then
		retVal = VB_RadarSrchTrkConstraint_compute(argArray)
		
	ElseIf argArray(0) = "register" Then
		retVal = VB_RadarSrchTrkConstraint_register()

	ElseIf argArray(0) = "compute" Then
		retVal = VB_RadarSrchTrkConstraint_compute(argArray)
		
	Else
		' bad call
		retVal = Empty
		
	End If

	VB_RadarSrchTrkConstraint = retVal

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_RadarSrchTrkConstraint_register()

	ReDim argStr(58)
			
	' Outputs	
	argStr(0) ="ArgumentType = Output; Name = RadarSearchTrackPluginConstraintValue; ArgumentName = RadarSearchTrackPluginConstraintValue"

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

	argStr(28)="ArgumentType = Input; Name = ST_SinglePulseSNR; ArgumentName = ST_SinglePulseSNR; Type = Value"
	argStr(29)="ArgumentType = Input; Name = ST_IntegratedSNR; ArgumentName = ST_IntegratedSNR; Type = Value"
	argStr(30)="ArgumentType = Input; Name = ST_NumIntegratedPulses; ArgumentName = ST_NumIntegratedPulses; Type = Value"
	argStr(31)="ArgumentType = Input; Name = ST_IntegrationTime; ArgumentName = ST_IntegrationTime; Type = Value"
	argStr(32)="ArgumentType = Input; Name = ST_DwellTime; ArgumentName = ST_DwellTime; Type = Value"
	argStr(33)="ArgumentType = Input; Name = ST_TargetRange; ArgumentName = ST_TargetRange; Type = Value"
	argStr(34)="ArgumentType = Input; Name = ST_TargetVelocity; ArgumentName = ST_TargetVelocity; Type = Value"
	argStr(35)="ArgumentType = Input; Name = ST_TargetMLCVelocity; ArgumentName = ST_TargetMLCVelocity; Type = Value"
	argStr(36)="ArgumentType = Input; Name = ST_TargetInClearDopplerZone; ArgumentName = ST_TargetInClearDopplerZone; Type = Value"
	argStr(37)="ArgumentType = Input; Name = ST_TargetInMLCFilter; ArgumentName = ST_TargetInMLCFilter; Type = Value"
	argStr(38)="ArgumentType = Input; Name = ST_TargetInSLCFilter; ArgumentName = ST_TargetInSLCFilter; Type = Value"

	argStr(39)="ArgumentType = Input; Name = ST_UnambigRangeFlag; ArgumentName = ST_UnambigRangeFlag; Type = Value"
	argStr(40)="ArgumentType = Input; Name = ST_UnambigVelFlag; ArgumentName = ST_UnambigVelFlag; Type = Value"
	
	argStr(41)="ArgumentType = Input; Name = ST_NoiseBandwidth; ArgumentName = ST_NoiseBandwidth; Type = Value"
	argStr(42)="ArgumentType = Input; Name = ST_NoisePower; ArgumentName = ST_NoisePower; Type = Value"

	argStr(43)="ArgumentType = Input; Name = ST_SinglePulseProbDetection; ArgumentName = ST_SinglePulseProbDetection; Type = Value"
	argStr(44)="ArgumentType = Input; Name = ST_IntegratedProbDetection; ArgumentName = ST_IntegratedProbDetection; Type = Value"
	argStr(45)="ArgumentType = Input; Name = ST_NonCFARDetectThreshold; ArgumentName = ST_NonCFARDetectThreshold; Type = Value"
	argStr(46)="ArgumentType = Input; Name = ST_CFARThresholdMultiplier; ArgumentName = ST_CFARThresholdMultiplier; Type = Value"
	
	argStr(47)="ArgumentType = Input; Name = ST_SinglePulseSNRUnderJamming; ArgumentName = ST_SinglePulseSNRUnderJamming; Type = Value"
	argStr(48)="ArgumentType = Input; Name = ST_IntegratedSNRUnderJamming; ArgumentName = ST_IntegratedSNRUnderJamming; Type = Value"
	argStr(49)="ArgumentType = Input; Name = ST_SinglePulseProbDetectionUnderJamming; ArgumentName = ST_SinglePulseProbDetectionUnderJamming; Type = Value"
	argStr(50)="ArgumentType = Input; Name = ST_IntegratedProbDetectionUnderJamming; ArgumentName = ST_IntegratedProbDetectionUnderJamming; Type = Value"
	argStr(51)="ArgumentType = Input; Name = ST_IntegrationTimeUnderJamming; ArgumentName = ST_IntegrationTimeUnderJamming; Type = Value"
	argStr(52)="ArgumentType = Input; Name = ST_NumIntegratedPulsesUnderJamming; ArgumentName = ST_NumIntegratedPulsesUnderJamming; Type = Value"
	argStr(53)="ArgumentType = Input; Name = ST_DwellTimeUnderJamming; ArgumentName = ST_DwellTimeUnderJamming; Type = Value"
	argStr(54)="ArgumentType = Input; Name = ST_JOverS; ArgumentName = ST_JOverS; Type = Value"
	argStr(55)="ArgumentType = Input; Name = ST_IntegratedJOverS; ArgumentName = ST_IntegratedJOverS; Type = Value"
	argStr(56)="ArgumentType = Input; Name = ST_JammingPower; ArgumentName = ST_JammingPower; Type = Value"
	
	VB_RadarSrchTrkConstraint_register = argStr

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_RadarSrchTrkConstraint_compute(argArray)
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
	
	' Radar Saerch/Track data
	Dim SinglePulseSNR, IntegSNR, IntegTime, DwellTime
	Dim TargetRange, TargetVel, TargetMLCVel, TargetInMLCFilter, TargetInSLCFilter
	Dim	NoiseBandwidth, NoisePower
	Dim SinglePulseProbDet, IntegProbDet, NonCFARDetectThreshold, CFARThresholdMultiplier
	Dim SinglePulseSNRJam, IntegSNRJam, SinglePulseProbDetJam, IntegProbDetJam, IntegTimeJam
	Dim DwellTimeJam, JOverS, IntegJOverS, JamPower
	
	' Radar Search/Track flags
	Dim NumIntegPulses, TargetInClearDopplerZone, UnambigRangeFlag, UnambigVelFlag, NumIntegPulsesJam
	
	Dim range, DopplerShift
	
	Redim retVal(1)

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
	
	'ST_SinglePulseSNR
	SinglePulseSNR = argArray(28)
	'ST_IntegratedSNR
	IntegSNR = argArray(29)
	'ST_NumIntegratedPulses
	NumIntegPulses = argArray(30)
	'ST_IntegrationTime
	IntegTime= argArray(31)
	'ST_DwellTime
	DwellTime = argArray(32)
	'ST_TargetRange
	TargetRange = argArray(33)
	'ST_TargetVelocity
	TargetVel = argArray(34)
	'ST_TargetMLCVelocity
	TargetMLCVel = argArray(35)
	'ST_TargetInClearDopplerZone
	TargetInClearDopplerZone = argArray(36)
	'ST_TargetInMLCFilter
	TargetInMLCFilter = argArray(37)
	'ST_TargetInSLCFilter
	TargetInSLCFilter = argArray(38)
	'ST_UnambigRangeFlag
	UnambigRangeFlag = argArray(39)
	'ST_UnambigVelFlag
	UnambigVelFlag = argArray(40)
	'ST_NoiseBandwidth
	NoiseBandwidth = argArray(41)
	'ST_NoisePower
	NoisePower = argArray(42)

	'ST_SinglePulseProbDetection
	SinglePulseProbDet = argArray(43)
	'ST_IntegartedProbDetection
	IntegProbDet = argArray(44)
	'ST_NonCFARDetectThreshold
	NonCFARDetectThreshold = argArray(45)
	'ST_CFARThresholdMultiplier
	CFARThresholdMultiplier = argArray(46)
	
	'ST_SinglePulseSNRUnderJamming
	SinglePulseSNRJam = argArray(47)
	'ST_IntegratedSNRUnderJamming
	IntegSNRJam = argArray(48)
	'ST_SinglePulseProbDetectionUnderJamming
	SinglePulseProbDetJam = argArray(49)
	'ST_IntegratedProbDetectionUnderJamming
	IntegProbDetJam = argArray(50)
	'ST_IntegrationTimeUnderJamming
	IntegTimeJam = argArray(51)
	'ST_NumIntegratedPulsesUnderJamming
	NumIntegPulsesJam = argArray(52)
	'ST_DwellTimeUnderJamming
	DwellTimeJam = argArray(53)
	'ST_JOverS
	JOverS = argArray(54)
	'ST_IntegratedJOverS
	IntegJOverS = argArray(55)
	'ST_JammingPower
	JamPower = argArray(56)

	range = Sqr((rdrXmPosX-tgtPosX)^2 + (rdrXmPosY-tgtPosY)^2 + (rdrXmPosZ-tgtPosZ)^2) / 1000
	
	DopplerShift = FreqAtTarget - RdrFreq 

	retVal(0) = IntegSNR
	VB_RadarSrchTrkConstraint_compute = retVal
	
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	' Display results for first function call only
	If Verbose < 0 Then
		MsgBox "Return Value = " & retVal(0)
		Verbose = 1
	End If
	
End Function
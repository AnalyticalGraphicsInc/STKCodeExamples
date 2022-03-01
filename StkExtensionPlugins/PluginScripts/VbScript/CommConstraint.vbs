' Custom multibeam antenna beam selection strategy plugin 

Option explicit

'flag so that the first computed output is displayed
Dim Verbose
Verbose = -1

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_CommConstraint(argArray)
	Dim retVal

	If IsEmpty(argArray(0)) Then
		retVal = VB_CommConstraint_compute(argArray)
		
	ElseIf argArray(0) = "register" Then
		retVal = VB_CommConstraint_register()

	ElseIf argArray(0) = "compute" Then
		retVal = VB_CommConstraint_compute(argArray)
		
	Else
		' bad call
		retVal = Empty
		
	End If

	VB_CommConstraint = retVal

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_CommConstraint_register()

	ReDim argStr(27)
			
	' Outputs	
	argStr(0) ="ArgumentType = Output; Name = PluginConstraintValue; ArgumentName = PluginConstraintValue"

	' Inputs
	argStr(1)="ArgumentType = Input; Name = DateUTC; ArgumentName = DateUTC; Type = Value"
	argStr(2)="ArgumentType = Input; Name = EpochSec; ArgumentName = EpochSec; Type = Value"
	argStr(3)="ArgumentType = Input; Name = CbName; ArgumentName = CbName; Type = Value"
	argStr(4)="ArgumentType = Input; Name = ReceiverPath; ArgumentName = ReceiverPath; Type = Value"
	argStr(5)="ArgumentType = Input; Name = TransmitterPath; ArgumentName = TransmitterPath; Type = Value"

	argStr(6)="ArgumentType = Input; Name = RcvrPosCBF; ArgumentName = RcvrPosCBF; Type = Value"
	argStr(7)="ArgumentType = Input; Name = RcvrAttitude; ArgumentName = RcvrAttitude; Type = Value"
	argStr(8)="ArgumentType = Input; Name = XmtrPosCBF; ArgumentName = XmtrPosCBF; Type = Value"
	argStr(9)="ArgumentType = Input; Name = XmtrAttitude; ArgumentName = XmtrAttitude; Type = Value"
	argStr(10)="ArgumentType = Input; Name = ReceivedFrequency; ArgumentName = ReceivedFrequency; Type = Value"

	argStr(11)="ArgumentType = Input; Name = DataRate; ArgumentName = DataRate; Type = Value"
	argStr(12)="ArgumentType = Input; Name = Bandwidth; ArgumentName = Bandwidth; Type = Value"
	argStr(13)="ArgumentType = Input; Name = CDMAGainValue; ArgumentName = CDMAGainValue; Type = Value"
	argStr(14)="ArgumentType = Input; Name = ReceiverGain; ArgumentName = ReceiverGain; Type = Value"
	argStr(15)="ArgumentType = Input; Name = PolEfficiency; ArgumentName = PolEfficiency; Type = Value"


	argStr(16)="ArgumentType = Input; Name = PolRelativeAngle; ArgumentName = PolRelativeAngle; Type = Value"
	argStr(17)="ArgumentType = Input; Name = RIP; ArgumentName = RIP; Type = Value"
	argStr(18)="ArgumentType = Input; Name = FluxDensity; ArgumentName = FluxDensity; Type = Value"
	argStr(19)="ArgumentType = Input; Name = GOverT; ArgumentName = GOverT; Type = Value"
	argStr(20)="ArgumentType = Input; Name = CarrierPower; ArgumentName = CarrierPower; Type = Value"

	argStr(21)="ArgumentType = Input; Name = BandwidthOverlap; ArgumentName = BandwidthOverlap; Type = Value"
	argStr(22)="ArgumentType = Input; Name = CNo; ArgumentName = CNo; Type = Value"
	argStr(23)="ArgumentType = Input; Name = CNR; ArgumentName = CNR; Type = Value"
	argStr(24)="ArgumentType = Input; Name = EbNo; ArgumentName = EbNo; Type = Value"
	argStr(25)="ArgumentType = Input; Name = BER; ArgumentName = BER; Type = Value"
	
	argStr(26)="ArgumentType = Input; Name = DateUTC; ArgumentName = dateStr" 'Type = ????

	VB_CommConstraint_register = argStr

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_CommConstraint_compute(argArray)
	Dim xmtrPos, xmtrPosX, xmtrPosY, xmtrPosZ
	Dim rcvrPos, rcvrPosX, rcvrPosY, rcvrPosZ
	Dim range, ftpp, Bn, PW, CNo, SNR
	Redim retVal(1)

	'XmtrPosCBF
	xmtrPos  = argArray(8)
	xmtrPosX = xmtrPos(0)
	xmtrPosY = xmtrPos(1)
	xmtrPosZ = xmtrPos(2)

	'RcvrPosCBF
	rcvrPos  = argArray(6)
	rcvrPosX = rcvrPos(0)
	rcvrPosY = rcvrPos(1)
	rcvrPosZ = rcvrPos(2)

	range = Sqr((xmtrPosX-rcvrPosX)^2 + (xmtrPosY-rcvrPosY)^2 + (xmtrPosZ-rcvrPosZ)^2) / 1000
	
	' Example Transmission of 625/50 television by INTELSAT
		
	' ftpp = peak-to-peak frequency deviation (Hz)
		ftpp = 15.0e6
	' Bn = Noise Bandwidth at Receiver (Hz)
		Bn = 5.0e6
	' PW = improvement factor due to pre-emphasis and de-emphasis and weighting factor (dB)
		PW = 13.2
	
		CNo = argArray(22)
		
	' SNR = 3/2 * (fttp/Bn)^2 * (1/Bn) * pw * C/No
		
	SNR = 10*Log(1.5*ftpp*ftpp/(Bn*Bn*Bn))/Log(10) + PW + CNo

	retVal(0) = SNR
	VB_CommConstraint_compute = retVal
	
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	' Display results for first function call only
	If Verbose < 0 Then
		MsgBox "Return Value = " & retVal(0)
		Verbose = 1
	End If
	
End Function
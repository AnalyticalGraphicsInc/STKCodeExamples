' Script to check for LOS access in urban environments
'
' How to use:
' 	Requires Urban Propagation Extension license - compute comm link using TPGEODESIC atmospheric absorption model
'	Turn off all other losses (e.g. rain, clouds/fog, tropospheric scintillation, custom)
'	Place dummy 0dBW simple transmitter model on coverage asset (EIRP must be 0dBW)
'	Use simple receiver models for coverage grid template objects
'	Use this script as a comm access constraint on the reciever template for the grid points
' How it works:
' 	Uprop returns approximately zero for atmospheric loss when it determines LOS is available
'	Script computes free space path loss and subtracts it from RIP
'	With 0dBW transmitter difference should be approx zero since atmospheric loss is near zero when LOS exists
'	Function returns 100 when LOS available and 0 otherwise.

Option explicit

'flag so that the first computed output is displayed
Dim Verbose
Verbose = -1

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_UProp_LOS_Check(argArray)
	Dim retVal

	If IsEmpty(argArray(0)) Then
		retVal = VB_UProp_LOS_Check_compute(argArray)
		
	ElseIf argArray(0) = "register" Then
		retVal = VB_UProp_LOS_Check_register()

	ElseIf argArray(0) = "compute" Then
		retVal = VB_UProp_LOS_Check_compute(argArray)
		
	Else
		' bad call
		retVal = Empty
		
	End If

	VB_UProp_LOS_Check = retVal

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_UProp_LOS_Check_register()

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

	VB_UProp_LOS_Check_register = argStr

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_UProp_LOS_Check_compute(argArray)
	Dim xmtrPos, xmtrPosX, xmtrPosY, xmtrPosZ
	Dim rcvrPos, rcvrPosX, rcvrPosY, rcvrPosZ
	Dim range, fspl, pi, c, ReceivedFrequency, RIP, UpropCheck, EpochSec
	Redim retVal(1)
	
	'```````````````````````  OLD LOGIC CODE  ````````````````````````````
	' When grid points are inside buildings, TPGEODESIC returns RIP = 0 W; use this as logic trap

	RIP = argArray(17)
	'msgbox(RIP)
	'If RIP <= 0 Then
	'	retVal(0) = 0.0

	'Else
		'RIP = 10*Log(argArray(17))/Log(10)

	'``````````````````````````````````````````````````````````````````````
	' Now the plugin interface returns values in db vs. watts so i can remove the above check and conversion

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

		' Range in meters
		range = Sqr((xmtrPosX-rcvrPosX)^2 + (xmtrPosY-rcvrPosY)^2 + (xmtrPosZ-rcvrPosZ)^2)
	
		pi = 3.141592653589
		c = 2.99792458e8

		ReceivedFrequency = argArray(10)

		' Free Space Path Loss
		fspl = -20*Log(4*pi*range*ReceivedFrequency/c)/Log(10)

		' If RIP - fspl approximately zero, then no atmospheric loss reported from Uprop which means LOS available

		UpropCheck = Abs(RIP - fspl)
		
		If UpropCheck < 0.1 Then
			retVal(0) = 100.0
		Else
			retVal(0) = 0.0
		End If

	'End If

	VB_UProp_LOS_Check_compute = retVal
	
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		
	' Display results for first LOS access found
	'If Verbose < 0 Then
	'	If retVal(0) = 100 Then
	'		EpochSec = argArray(2)
	'		MsgBox "Time = " & EpochSec &"; Return Value = " & retVal(0) & "; UpropCheck = " & UpropCheck
	'		Verbose = 1
	'	End If
	'End If
	
End Function
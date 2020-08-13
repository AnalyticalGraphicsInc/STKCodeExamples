
Dim VB_TransmitterModel_globalVar
Dim VB_TransmitterModel_Inputs
Dim VB_TransmitterModel_Outputs

'==========================================================================
' VB_TransmitterModel() fctn
'==========================================================================
Function VB_TransmitterModel ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_TransmitterModel_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_TransmitterModel_globalVar = -1

		retVal = VB_TransmitterModel_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_TransmitterModel_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_TransmitterModel = retVal

End Function

Function VB_TransmitterModel_register()

'		Output Parameters
	ReDim descripStr(3), argStr(20)

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Frequency"
	descripStr(2)="ArgumentName = Frequency"
	argStr(0) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Power"
	descripStr(2)="ArgumentName = Power"
	argStr(1) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Gain"
	descripStr(2)="ArgumentName = Gain"
	argStr(2) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = DataRate"
	descripStr(2)="ArgumentName = DataRate"
	argStr(3) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Bandwidth"
	descripStr(2)="ArgumentName = Bandwidth"
	argStr(4) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Modulation"
	descripStr(2)="ArgumentName = Modulation"
	argStr(5) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PostTransmitLoss"
	descripStr(2)="ArgumentName = PostTransmitLoss"
	argStr(6) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PolType"
	descripStr(2)="ArgumentName = PolType"
	argStr(7) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PolRefAxis"
	descripStr(2)="ArgumentName = PolRefAxis"
	argStr(8) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PolTiltAngle"
	descripStr(2)="ArgumentName = PolTiltAngle"
	argStr(9) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PolAxialRatio"
	descripStr(2)="ArgumentName = PolAxialRatio"
	argStr(10) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = UseCDMASpreadGain"
	descripStr(2)="ArgumentName = UseCDMASpreadGain"
	argStr(11) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = CDMAGain"
	descripStr(2)="ArgumentName = CDMAGain"
	argStr(12) = descripStr

'		Input Parameters
	ReDim descripStr(4)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = DateUTC"
	descripStr(2)="ArgumentName = DateUTC"
	descripStr(3)="Type = Value"
	argStr(13) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = CbName"
	descripStr(2)="ArgumentName = CbName"
	descripStr(3)="Type = Value"
	argStr(14) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = XmtrPosCBF"
	descripStr(2)="ArgumentName = XmtrPosCBF"
	descripStr(3)="Type = Value"
	argStr(15) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = XmtrAttitude"
	descripStr(2)="ArgumentName = XmtrAttitude"
	descripStr(3)="Type = Value"
	argStr(16) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = RcvrPosCBF"
	descripStr(2)="ArgumentName = RcvrPosCBF"
	descripStr(3)="Type = Value"
	argStr(17) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = RcvrAttitude"
	descripStr(2)="ArgumentName = RcvrAttitude"
	descripStr(3)="Type = Value"
	argStr(18) = descripStr


  	argStr(19) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

	VB_TransmitterModel_register = argStr

End Function


Function VB_TransmitterModel_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_TransmitterModel_globalVar < 0 Then

		Set VB_TransmitterModel_Inputs = g_GetPluginArrayInterface("VB_TransmitterModel_Inputs")

		outStr = VB_TransmitterModel_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_TransmitterModel_Outputs = g_GetPluginArrayInterface("VB_TransmitterModel_Outputs")

		outStr = VB_TransmitterModel_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_TransmitterModel_globalVar = 1

		MsgBox inputData(VB_TransmitterModel_Inputs.dateStr)

	End If

'   DUMMY Model for testing script only
'   Model parameters for Comm Transmitter 
'	.
'
'	Note: This example is for fixed parameters, but these can vary at each time step

	Redim returnValue(13)
	Dim Frequency, Power, Gain, DataRate, Bandwidth, Modulation, PostTransmitLoss
	Dim PolType, PolRefAxis, PolTiltAngle, PolAxialRatio
	Dim UseCDMASpreadGain, CDMAGain

	Dim xmtrXYZ, xmtrX, xmtrY, xmtrZ, rcXYZ, rcX, rcY, rcZ
	Dim xmAtti, xmA, xmB, XmC, xmD, rcAtti, rcA, rcB, rcC, rcD

	xmtrXYZ = inputData(VB_TransmitterModel_Inputs.XmtrPosCBF)
	xmtrX   = xmtrXYZ(0)
	xmtrY   = xmtrXYZ(1)
	xmtrZ   = xmtrXYZ(2)

	xmAtti  = inputData(VB_TransmitterModel_Inputs.XmtrAttitude)
	xmA     = xmAtti(0)
	xmB     = xmAtti(1)
	xmC     = xmAtti(2)
	xmD     = xmAtti(3)
	
	rcXYZ = inputData(VB_TransmitterModel_Inputs.RcvrPosCBF)
	rcX   = rcXYZ(0)
	rcY   = rcXYZ(1)
	rcZ   = rcXYZ(2)

	rcAtti  = inputData(VB_TransmitterModel_Inputs.RcvrAttitude)
	rcA     = rcAtti(0)
	rcB     = rcAtti(1)
	rcC     = rcAtti(2)
	rcD     = rcAtti(3)

'	range = Sqr((xmtrX-rcX)^2 + (xmtrY-rcY)^2 + (xmtrZ-rcZ)^2)

	returnValue(VB_TransmitterModel_Outputs.Frequency)        = 1.93e14
	returnValue(VB_TransmitterModel_Outputs.Power)            = -6.99
	returnValue(VB_TransmitterModel_Outputs.Gain)             = 93.45
	returnValue(VB_TransmitterModel_Outputs.DataRate)         = 20.0e6
	returnValue(VB_TransmitterModel_Outputs.Bandwidth)        = 1e6
	returnValue(VB_TransmitterModel_Outputs.Modulation)       = "BPSK"
	returnValue(VB_TransmitterModel_Outputs.PostTransmitLoss) = -1.55
	returnValue(VB_TransmitterModel_Outputs.PolType)          = 0
	returnValue(VB_TransmitterModel_Outputs.PolRefAxis)       = 0
	returnValue(VB_TransmitterModel_Outputs.PolTiltAngle)     = 0.0
	returnValue(VB_TransmitterModel_Outputs.PolAxialRatio)    = 1.0
	returnValue(VB_TransmitterModel_Outputs.UseCDMASpreadGain)= 0
	returnValue(VB_TransmitterModel_Outputs.CDMAGain)         = 0.0

	VB_TransmitterModel_compute = returnValue

End Function



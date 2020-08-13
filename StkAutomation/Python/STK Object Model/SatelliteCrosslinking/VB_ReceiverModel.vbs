
Dim VB_ReceiverModel_globalVar
Dim VB_ReceiverModel_Inputs
Dim VB_ReceiverModel_Outputs

'==========================================================================
' VB_ReceiverModel() fctn
'==========================================================================
Function VB_ReceiverModel ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_ReceiverModel_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_ReceiverModel_globalVar = -1

		retVal = VB_ReceiverModel_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_ReceiverModel_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_ReceiverModel = retVal

End Function

Function VB_ReceiverModel_register()

'		Output Parameters
	ReDim descripStr(3), argStr(22)

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Frequency"
	descripStr(2)="ArgumentName = Frequency"
	argStr(0) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Bandwidth"
	descripStr(2)="ArgumentName = Bandwidth"
	argStr(1) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Gain"
	descripStr(2)="ArgumentName = Gain"
	argStr(2) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PreReceiveLoss"
	descripStr(2)="ArgumentName = PreReceiveLoss"
	argStr(3) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PreDemodLoss"
	descripStr(2)="ArgumentName = PreDemodLoss"
	argStr(4) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = UseRainModel"
	descripStr(2)="ArgumentName = UseRainModel"
	argStr(5) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RainOutagePercent"
	descripStr(2)="ArgumentName = RainOutagePercent"
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
	descripStr(1)="Name = ReceiverNoiseFigure"
	descripStr(2)="ArgumentName = ReceiverNoiseFigure"
	argStr(11) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = CableLoss"
	descripStr(2)="ArgumentName = CableLoss"
	argStr(12) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = CableNoiseTemp"
	descripStr(2)="ArgumentName = CableNoiseTemp"
	argStr(13) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = AntennaNoiseTemp"
	descripStr(2)="ArgumentName = AntennaNoiseTemp"
	argStr(14) = descripStr

'		Input Parameters
	ReDim descripStr(4)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = DateUTC"
	descripStr(2)="ArgumentName = DateUTC"
	descripStr(3)="Type = Value"
	argStr(15) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = CbName"
	descripStr(2)="ArgumentName = CbName"
	descripStr(3)="Type = Value"
	argStr(16) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = XmtrPosCBF"
	descripStr(2)="ArgumentName = XmtrPosCBF"
	descripStr(3)="Type = Value"
	argStr(17) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = XmtrAttitude"
	descripStr(2)="ArgumentName = XmtrAttitude"
	descripStr(3)="Type = Value"
	argStr(18) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = RcvrPosCBF"
	descripStr(2)="ArgumentName = RcvrPosCBF"
	descripStr(3)="Type = Value"
	argStr(19) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = RcvrAttitude"
	descripStr(2)="ArgumentName = RcvrAttitude"
	descripStr(3)="Type = Value"
	argStr(20) = descripStr


  	argStr(21) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

	VB_ReceiverModel_register = argStr

End Function


Function VB_ReceiverModel_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_ReceiverModel_globalVar < 0 Then

		Set VB_ReceiverModel_Inputs = g_GetPluginArrayInterface("VB_ReceiverModel_Inputs")

		outStr = VB_ReceiverModel_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_ReceiverModel_Outputs = g_GetPluginArrayInterface("VB_ReceiverModel_Outputs")

		outStr = VB_ReceiverModel_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_ReceiverModel_globalVar = 1

		MsgBox inputData(VB_ReceiverModel_Inputs.dateStr)

	End If

'   EXAMPLE Model for testing script only
'   Model parameters for Comm Trasnmitter 
'	.
'	Note: This example is for fixed parameters, but these can vary at each time step
'
'   USER MODEL AREA
	Redim returnValue(15)

'	PLEASE DO NOT CHANGE ANYTHING ABOVE THIS LINE

	Dim Frequency, Bandwidth, Gain, PreReceiveLoss,  PreDemodLoss, UseRainModel, RainOutagePercent
	Dim PolType, PolRefAxis, PolTiltAngle, PolAxialRatio
	Dim ReceiverNoiseFigure, CableLoss, CableNoiseTemp, AntennaNoiseTemp

	Dim xmtrXYZ, xmtrX, xmtrY, xmtrZ, rcXYZ, rcX, rcY, rcZ
	Dim xmAtti, xmA, xmB, XmC, xmD, rcAtti, rcA, rcB, rcC, rcD

	xmtrXYZ = inputData(VB_ReceiverModel_Inputs.XmtrPosCBF)
	xmtrX   = xmtrXYZ(0)
	xmtrY   = xmtrXYZ(1)
	xmtrZ   = xmtrXYZ(2)

	xmAtti  = inputData(VB_ReceiverModel_Inputs.XmtrAttitude)
	xmA     = xmAtti(0)
	xmB     = xmAtti(1)
	xmC     = xmAtti(2)
	xmD     = xmAtti(3)
	
	rcXYZ = inputData(VB_ReceiverModel_Inputs.RcvrPosCBF)
	rcX   = rcXYZ(0)
	rcY   = rcXYZ(1)
	rcZ   = rcXYZ(2)

	rcAtti  = inputData(VB_ReceiverModel_Inputs.RcvrAttitude)
	rcA     = rcAtti(0)
	rcB     = rcAtti(1)
	rcC     = rcAtti(2)
	rcD     = rcAtti(3)

'	range = Sqr((xmtrX-rcX)^2 + (xmtrY-rcY)^2 + (xmtrZ-rcZ)^2)

	returnValue(VB_ReceiverModel_Outputs.Frequency)           = 1.93e14
	returnValue(VB_ReceiverModel_Outputs.Bandwidth)           = 1e9
	returnValue(VB_ReceiverModel_Outputs.Gain)                = 96.17
	returnValue(VB_ReceiverModel_Outputs.PreReceiveLoss)      = -2.3
	returnValue(VB_ReceiverModel_Outputs.PreDemodLoss)        = -0.86
	returnValue(VB_ReceiverModel_Outputs.UseRainModel)        = 0
	returnValue(VB_ReceiverModel_Outputs.RainOutagePercent)   = 0
	returnValue(VB_ReceiverModel_Outputs.PolType)             = 0
	returnValue(VB_ReceiverModel_Outputs.PolRefAxis)          = 0
	returnValue(VB_ReceiverModel_Outputs.PolTiltAngle)        = 0.0
	returnValue(VB_ReceiverModel_Outputs.PolAxialRatio)       = 1.0
	returnValue(VB_ReceiverModel_Outputs.ReceiverNoiseFigure) = 0.25
	returnValue(VB_ReceiverModel_Outputs.CableLoss)           = 0.5
	returnValue(VB_ReceiverModel_Outputs.CableNoiseTemp)      = 290
	returnValue(VB_ReceiverModel_Outputs.AntennaNoiseTemp)    = 50.0

'   END OF USER MODEL AREA

	VB_ReceiverModel_compute = returnValue

End Function



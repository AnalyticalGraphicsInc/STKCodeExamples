' All names must match the name of the file and each other, i.e. "VB_TransmitterModel_TimeDynamic"
' Ctrl+F and replace all, then Save As with the chosen name
Dim VB_TransmitterModel_TimeDynamic_globalVar
Dim VB_TransmitterModel_TimeDynamic_Inputs
Dim VB_TransmitterModel_TimeDynamic_Outputs

'==========================================================================
' VB_TransmitterModel_TimeDynamic() fctn
'==========================================================================
Function VB_TransmitterModel_TimeDynamic( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_TransmitterModel_TimeDynamic_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_TransmitterModel_TimeDynamic_globalVar = -1

		retVal = VB_TransmitterModel_TimeDynamic_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_TransmitterModel_TimeDynamic_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_TransmitterModel_TimeDynamic = retVal

End Function

Function VB_TransmitterModel_TimeDynamic_register()

'		Output Parameters
	ReDim descripStr(3), argStr(15)

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
	descripStr(1)="Name = EpochSec"
	descripStr(2)="ArgumentName = EpochSec"
	descripStr(3)="Type = Value"
	argstr(13) = descripStr

	argStr(14) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

	VB_TransmitterModel_TimeDynamic_register = argStr

End Function


Function VB_TransmitterModel_TimeDynamic_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_TransmitterModel_TimeDynamic_globalVar < 0 Then

		Set VB_TransmitterModel_TimeDynamic_Inputs = g_GetPluginArrayInterface("VB_TransmitterModel_TimeDynamic_Inputs")

		outStr = VB_TransmitterModel_TimeDynamic_Inputs.Describe()

		Set VB_TransmitterModel_TimeDynamic_Outputs = g_GetPluginArrayInterface("VB_TransmitterModel_TimeDynamic_Outputs")

		outStr = VB_TransmitterModel_TimeDynamic_Outputs.Describe()

		VB_TransmitterModel_TimeDynamic_globalVar = 1

	End If

'   Model parameters for Comm Transmitter 

	Redim returnValue(13)
    Dim epSecParent, time
    Dim xmtrFreqCenter, xmtrBandwidthSpec, sweepRate, xmtrPower
    
    ' GHz, TimeDynamic specs
    xmtrFreqCenter = 30
    xmtrBandwidthSpec = 1
	xmtrPower = 92

    ' GHz/Sec
    sweepRate = 0.01

	time = inputData(VB_TransmitterModel_TimeDynamic_Inputs.EpochSec)
	'time = 250

    ' Start at bottom of range
    lowerBW = xmtrFreqCenter - (0.5 * xmtrBandwidthSpec)
    upperBW = xmtrFreqCenter + (0.5 * xmtrBandwidthSpec)
    tempFreq = lowerBW + (time * sweepRate)
    secondsPerSweep = xmtrBandwidthSpec/sweepRate
    howManySweeps = time/(secondsPerSweep * 1.0)
    ' If this, rounded down and plus 1, is odd, then we're sweeeping up to the max bandwidth
    upOrDown = Int(howManySweeps) + 1
    secsIntoSweep = time - (Int(howManySweeps) * secondsPerSweep)
    If upOrDown Mod 2 > 0 Then
        ' Is odd, sweeping up
        freq = lowerBW + (sweepRate * secsIntoSweep)
    Else
        ' Sweeping down
        freq = upperBW - (sweepRate * secsIntoSweep)
    End If

	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.Frequency)        = freq * 1000000000        'Hz
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.Power)            = xmtrPower                'dB
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.Gain)             = 0.0      			         'dBi
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.DataRate)         = 12.0e6			         'bits/sec
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.Bandwidth)        = 1.0				         'Hz
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.Modulation)       = "FSK"
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.PostTransmitLoss) = 0                        'dB
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.PolType)          = 2					     ' 2 is RHC, options are here: https://help.agi.com/stk/index.htm#../Subsystems/pluginScripts/Content/commPoints.htm#poltype
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.PolRefAxis)       = 0                    ' 0 is X
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.PolTiltAngle)     = 0.0
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.PolAxialRatio)    = 0.0
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.UseCDMASpreadGain)= 0						 
	returnValue(VB_TransmitterModel_TimeDynamic_Outputs.CDMAGain)         = 0.0

	VB_TransmitterModel_TimeDynamic_compute = returnValue

End Function


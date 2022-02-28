'#########################################################################################
' EXAMPLE VBS BASED SCRIPT MODULATOR PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER MODULATION MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
' If you change the file name then the function names below
' must be edited to match the file name
'#########################################################################################

Dim VB_DynamicDemodulator_globalVar
Dim VB_DynamicDemodulator_Inputs
Dim VB_DynamicDemodulator_Outputs

'==========================================================================
' VB_DynamicDemodulator() fctn
'==========================================================================
Function VB_DynamicDemodulator ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_DynamicDemodulator_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_DynamicDemodulator_globalVar = -1

		retVal = VB_DynamicDemodulator_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_DynamicDemodulator_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_DynamicDemodulator = retVal

End Function

Function VB_DynamicDemodulator_register()

    Dim ac
	ReDim descripStr(3), argStr(13)

    ac = 0

    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = OutBER"
    descripStr(2)="ArgumentName = OutBER"
    argStr(ac) = descripStr


	ReDim descripStr(4)

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = DateUTC"
	descripStr(2)="ArgumentName = DateUTC"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = CbName"
	descripStr(2)="ArgumentName = CbName"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = ObjectPath"
	descripStr(2)="ArgumentName = ObjectPath"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = EpochSec"
	descripStr(2)="ArgumentName = EpochSec"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = ObjectPosLLA"
	descripStr(2)="ArgumentName = ObjectPosLLA"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = RFFreq"
	descripStr(2)="ArgumentName = RFFreq"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = DataRate"
	descripStr(2)="ArgumentName = DataRate"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = SpectrumLimitLo"
	descripStr(2)="ArgumentName = SpectrumLimitLo"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = SpectrumLimitHi"
	descripStr(2)="ArgumentName = SpectrumLimitHi"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = SignalModulationName"
	descripStr(2)="ArgumentName = SignalModulationName"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
		
    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = SignalEbNo"
	descripStr(2)="ArgumentName = SignalEbNo"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
			
	
	
    'MsgBox  ac
	VB_DynamicDemodulator_register = argStr

End Function


Function VB_DynamicDemodulator_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_DynamicDemodulator_globalVar < 0 Then

		Set VB_DynamicDemodulator_Inputs = g_GetPluginArrayInterface("VB_DynamicDemodulator_Inputs")

		outStr = VB_DynamicDemodulator_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_DynamicDemodulator_Outputs = g_GetPluginArrayInterface("VB_DynamicDemodulator_Outputs")

		outStr = VB_DynamicDemodulator_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_DynamicDemodulator_globalVar = 1

		'MsgBox inputData(VB_DynamicDemodulator_Inputs.dateUTC)

	End If
	
	' Dim input parameters
    Dim EpochSec
    Dim DataRate
	Dim RFFreq
	Dim ObjPosLLA
	
    ' Initialize Input values
	EpochSec      = inputData(VB_DynamicDemodulator_Inputs.EpochSec)
	RFFreq = inputData(VB_DynamicDemodulator_Inputs.RFFreq)
	ObjPosLLA = inputData(VB_DynamicDemodulator_Inputs.ObjectPosLLA)
	DataRate      = inputData(VB_DynamicDemodulator_Inputs.DataRate)
	SignalModulationName = inputData(VB_DynamicDemodulator_Inputs.SignalModulationName)
	

	'############################################################################################
	' USER PLUGIN RF MODULATION MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR RF MODULATION COMPUTATION MODEL
	' All input and out paramters have been mapped to the variables below
	'############################################################################################
	' NOTE: the outputs that are returned MUST be in the same order as registered
	' All frequency and frequency step value units are Hertz.
	'	Script inputs
	'		DateUTC : Time			string
	'		Central Body Name	    string
	'		ObjectPath   			string
	'		EpochSec        (secs)  double  
    '       RFFreq    (Hz)   double  
	'		ObjPosLLA (Rad,Rad,m)	double(3)
    '       DataRate         (bps)  double
	'       SignalModulationName    string (max 32 characters )
	'
	'	Script outputs
    '       OutBER                            double

	Redim returnValue(2)  ' Size should be equivalent to number of outputs being returned

    ' Dim STK expected output parameters
	Dim OutBER
 
    ' Dim temporaries used for this particular example
	Dim waveformSelector

    ' Initialize Output values
	' NOTE: Your doppler resolution will be limited to FreqStepSize, so be sure to set number of 
	'       NumPSDPts to achieve adequate doppler resolution.
    OutBER           = 0.5
	
	objPosLat = ObjPosLLA(0)
	objPosLon = ObjPosLLA(1)
	objPosAlt = ObjPosLLA(2)

	waveformSelector = Int(EpochSec) Mod 3
	
	If ( waveformSelector = 0) Then
		OutBER = 0.5
	ElseIf ( waveformSelector = 1) Then
		OutBER = 1.1111111e-1
	Else
	     If (StrComp(SignalModulationName, "BPSK") = 0) Then
		     OutBER = 2.222222e-2
	     ElseIf (StrComp(SignalModulationName, "OQPSK") = 0) Then
		     OutBER = 3.333333e-3
	     ElseIf (StrComp(SignalModulationName, "MyOQPSK") = 0) Then
		     OutBER = 4.444444e-4
	     Else
		     OutBER = 5.555555e-5
	     End If
    End If



 	 
	' Modulation MODEL END
	' #####################################################
	returnValue(VB_DynamicDemodulator_Outputs.OutBER)           = OutBER

	'############################################################################################
	' END OF USER MODEL AREA	
	'############################################################################################

	VB_DynamicDemodulator_compute = returnValue

End Function

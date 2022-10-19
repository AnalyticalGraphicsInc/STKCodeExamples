'#########################################################################################
' EXAMPLE VBS BASED SCRIPT MODULATOR PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER MODULATION MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
' If you change the file name then the function names below
' must be edited to match the file name
'#########################################################################################

Dim VB_DynamicModulator_CustomPSD_globalVar
Dim VB_DynamicModulator_CustomPSD_Inputs
Dim VB_DynamicModulator_CustomPSD_Outputs

'==========================================================================
' VB_DynamicModulator_CustomPSD() fctn
'==========================================================================
Function VB_DynamicModulator_CustomPSD ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_DynamicModulator_CustomPSD_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_DynamicModulator_CustomPSD_globalVar = -1

		retVal = VB_DynamicModulator_CustomPSD_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_DynamicModulator_CustomPSD_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_DynamicModulator_CustomPSD = retVal

End Function

Function VB_DynamicModulator_CustomPSD_register()

    Dim ac
	ReDim descripStr(3), argStr(18)

    ac = 0
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = IsDynamic"
	descripStr(2)="ArgumentName = IsDynamic"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = ModulationName"
	descripStr(2)="ArgumentName = ModulationName"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = SpectrumLimitLow"
	descripStr(2)="ArgumentName = SpectrumLimitLow"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = SpectrumLimitHi"
	descripStr(2)="ArgumentName = SpectrumLimitHi"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = UsePSD"
	descripStr(2)="ArgumentName = UsePSD"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = NumPSDPoints"
	descripStr(2)="ArgumentName = NumPSDPoints"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PSDData"
	descripStr(2)="ArgumentName = PSDData"
	argStr(ac) = descripStr

    ac = ac + 1
    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = FreqStepSize"
    descripStr(2)="ArgumentName = FreqStepSize"
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
	descripStr(1)="Name = RFCarrierFreq"
	descripStr(2)="ArgumentName = RFCarrierFreq"
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
	descripStr(1)="Name = DataRate"
	descripStr(2)="ArgumentName = DataRate"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    'MsgBox  ac
	VB_DynamicModulator_CustomPSD_register = argStr

End Function


Function VB_DynamicModulator_CustomPSD_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_DynamicModulator_CustomPSD_globalVar < 0 Then

		Set VB_DynamicModulator_CustomPSD_Inputs = g_GetPluginArrayInterface("VB_DynamicModulator_CustomPSD_Inputs")

		outStr = VB_DynamicModulator_CustomPSD_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_DynamicModulator_CustomPSD_Outputs = g_GetPluginArrayInterface("VB_DynamicModulator_CustomPSD_Outputs")

		outStr = VB_DynamicModulator_CustomPSD_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_DynamicModulator_CustomPSD_globalVar = 1

		MsgBox inputData(VB_DynamicModulator_CustomPSD_Inputs.dateUTC)

	End If
	Redim returnValue(10)  ' Size should be equivalent to number of outputs being returned
	
	' Dim input parameters
    Dim DateUTC, CbName, ObjectPath, EpochSec, DataRate, RFCarrierFreq, ObjPosLLA
	
    ' Initialize Input values
	DateUTC       = inputData(VB_DynamicModulator_CustomPSD_Inputs.DateUTC)
	CbName        = inputData(VB_DynamicModulator_CustomPSD_Inputs.CbName)
	ObjectPath    = inputData(VB_DynamicModulator_CustomPSD_Inputs.ObjectPath)
	EpochSec      = inputData(VB_DynamicModulator_CustomPSD_Inputs.EpochSec)
	RFCarrierFreq = inputData(VB_DynamicModulator_CustomPSD_Inputs.RFCarrierFreq)
	ObjPosLLA     = inputData(VB_DynamicModulator_CustomPSD_Inputs.ObjectPosLLA)
	DataRate      = inputData(VB_DynamicModulator_CustomPSD_Inputs.DataRate)
	
    ' Dim STK expected output parameters
	Dim IsDynamic, ModulationName, SpectrumLimitLow, SpectrumLimitHi
	Dim UsePSD, NumPSDPoints, PsdData (100000, 1), FreqStepSize
 	
	
	'############################################################################################
	' USER PLUGIN RF MODULATOR MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR RF MODULATOR COMPUTATION MODEL
	'
	' This sample demonstrates how to dynamically create a custom Power Spectral Density.  If 
	' Use Signal PSD is not enabled within STK, the magnitude will be assumed unity across the
	' band as defined by SpectrumLimitLow amd SpectrumLimitHi.   If one does not wants to specify 
	' the power spectral density, one should use the other interface (see 
	' VB_DynamicModulator_IdealPSD.vbs) example.
	'
	' All input and out paramters have been mapped to variables described below.
	'############################################################################################
	' NOTE: the outputs that are returned MUST be in the same order as registered
	' If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	' will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	' at every timestep.
	'
	' All frequency and frequency step value units are Hertz.
	' PSD values are in dB.
	'
	' Script input variables available to user:
	'		DateUTC    - Date in UTC.                                            string
	'		CbName	   - Name of the transmitter's central body.                 string
	'		ObjectPath - Path of the object, i.e. objects fully qualified name.  string
	'		EpochSec   - Current simulation epoch seconds.                       double  
    '       RFCarrierFreq - RF carrier frequency, in Hz.                         double  
	'		ObjPosLLA     - Objects position in LLA (Rad,Rad,m).                 double(3)
    '       DataRate      - Information data rate of the transmitter, in bps.    double
	'
	' Script outputs which must be filled in by the user:
	'       IsDynamic            - Indicates if script is time-dynamic (see above).   int
	'       ModulationName       - Modulation name, for demodulator selection.        string ( max 32 characters )
	'		SpectrumLimitLow - Lower band limit of output spectrum in Hz and
    '                          relative to RF carrier frequency (-100GHz to 0).   double
	'		SpectrumLimitHi  - Upper band limit of output spectrum in Hz and 
	'                          relative to RF carrier frequency (0 to 100GHz).    double 
	'       UsePSD           - Use signal PSD indicator, 0 or 1.                  int
    '       NumPSDPoints     - Number of PSD points being returned (max 100,000)  int
	'       PsdData          - Psd values.                                        double array [ NumPSDPoints x 1 ]
    '       FreqStepSize     - Frequency step size of Psd data, in Hz             double

	

    ' Dim temporaries used for this particular example
    Dim I, waveformSelector
 	Dim objPosLat, objPosLon, objPosAlt, chipsPerBit, modulationEfficiency, codeRate, spreadingFactor
	
    ' Initialize Output values
	' NOTE: Your doppler resolution will be limited to FreqStepSize, so be sure to 
	'       set NumPSDPts to achieve adequate doppler resolution.
	IsDynamic        = 1 
	ModulationName   = "BPSK"   ' Only used by STK for proper demodulation
	modulationEfficiency = 2.0  
	codeRate         = 1.0
    chipsPerBit      = 1
	SpectrumLimitLow = -(DataRate * modulationEfficiency / codeRate * chipsPerBit)/2.0
	SpectrumLimitHi  = -SpectrumLimitLow
    UsePSD           = 1
    NumPSDPoints     = 50001

	FreqStepSize     = (SpectrumLimitHi - SpectrumLimitLow)/NumPSDPoints
	objPosLat = ObjPosLLA(0)
	objPosLon = ObjPosLLA(1)
	objPosAlt = ObjPosLLA(2)

	waveformSelector = Int(EpochSec) Mod 4
	
    ' First waveform is...
	' DataRate Hz wide w/ peak magnitude of 0dB at carrier frequency and
    ' -30 dB down at edges.
	If ( waveformSelector = 0) Then
		For I = 0 To NumPSDPoints/2 Step 1
			PsdData (I, 0) = -30 + I*(30/NumPSDPoints*2)
			PsdData (NumPSDPoints-I, 0) = -30 + I*(30/NumPSDPoints*2)
		Next
    ' Second waveform is...
	' DataRate Hz wide w/ peak magnitude of 0dB at carrier frequency and
    ' -10 dB down at edges.
	ElseIf ( waveformSelector = 1) Then
		For I = 0 To NumPSDPoints/2 Step 1
			PsdData (I, 0) = -10 + I*(10/NumPSDPoints*2)
			PsdData (NumPSDPoints-I, 0) = -10 + I*(10/NumPSDPoints*2)
		Next		
    ' Third waveform is...
	' similar to second waveform but is 4 times more bandwidth efficient
	ElseIf (waveformSelector = 2) Then
	    modulationEfficiency = 0.25
    	SpectrumLimitLow = -(DataRate * modulationEfficiency / codeRate * chipsPerBit)/2.0
	    SpectrumLimitHi  = -SpectrumLimitLow
	    FreqStepSize     = (SpectrumLimitHi - SpectrumLimitLow)/NumPSDPoints	
	
		For I = 0 To NumPSDPoints/2 Step 1
			PsdData (I, 0) = -10 + I*(10/NumPSDPoints*2)
			PsdData (NumPSDPoints-I, 0) = -10 + I*(10/NumPSDPoints*2)
		Next
    ' Fourth waveform is...
	' similar to third waveform but 2x wider than 3rd
		Else
	    modulationEfficiency = 0.25
		chipsPerBit = 2
    	SpectrumLimitLow = -(DataRate * modulationEfficiency / codeRate * chipsPerBit)/2.0
	    SpectrumLimitHi  = -SpectrumLimitLow
	    FreqStepSize     = (SpectrumLimitHi - SpectrumLimitLow)/NumPSDPoints	
	
		For I = 0 To NumPSDPoints/2 Step 1
			PsdData (I, 0) = -10 + I*(10/NumPSDPoints*2)
			PsdData (NumPSDPoints-I, 0) = -10 + I*(10/NumPSDPoints*2)
		Next	
	End If
	
 	' Modulator MODEL END
	
	
	' #####################################################
	returnValue(VB_DynamicModulator_CustomPSD_Outputs.IsDynamic)            = IsDynamic
    returnValue(VB_DynamicModulator_CustomPSD_Outputs.ModulationName)       = ModulationName
	returnValue(VB_DynamicModulator_CustomPSD_Outputs.SpectrumLimitLow)     = SpectrumLimitLow
	returnValue(VB_DynamicModulator_CustomPSD_Outputs.SpectrumLimitHi)      = SpectrumLimitHi
	returnValue(VB_DynamicModulator_CustomPSD_Outputs.UsePSD)               = UsePSD
	returnValue(VB_DynamicModulator_CustomPSD_Outputs.NumPSDPoints)         = NumPSDPoints
	returnValue(VB_DynamicModulator_CustomPSD_Outputs.PSDData)              = PsdData
	returnValue(VB_DynamicModulator_CustomPSD_Outputs.FreqStepSize)         = FreqStepSize

	'############################################################################################
	' END OF USER MODEL AREA	
	'############################################################################################

	VB_DynamicModulator_CustomPSD_compute = returnValue

End Function

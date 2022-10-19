'#########################################################################################
' EXAMPLE VBS BASED SCRIPT MODULATOR PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER MODULATION MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
' If you change the file name then the function names below
' must be edited to match the file name
'#########################################################################################

Dim VB_DynamicModulator_IdealPSD_globalVar
Dim VB_DynamicModulator_IdealPSD_Inputs
Dim VB_DynamicModulator_IdealPSD_Outputs

'==========================================================================
' VB_DynamicModulator_IdealPSD() fctn
'==========================================================================
Function VB_DynamicModulator_IdealPSD ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_DynamicModulator_IdealPSD_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_DynamicModulator_IdealPSD_globalVar = -1

		retVal = VB_DynamicModulator_IdealPSD_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_DynamicModulator_IdealPSD_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_DynamicModulator_IdealPSD = retVal

End Function

Function VB_DynamicModulator_IdealPSD_register()

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
	descripStr(1)="Name = ModulationEfficiency"
	descripStr(2)="ArgumentName = ModulationEfficiency"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = CodeRate"
	descripStr(2)="ArgumentName = CodeRate"
	argStr(ac) = descripStr
	
    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = PSDShape"
	descripStr(2)="ArgumentName = PSDShape"
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
	descripStr(1)="Name = ChipsPerBit"
	descripStr(2)="ArgumentName = ChipsPerBit"
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
	VB_DynamicModulator_IdealPSD_register = argStr

End Function


Function VB_DynamicModulator_IdealPSD_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_DynamicModulator_IdealPSD_globalVar < 0 Then

		Set VB_DynamicModulator_IdealPSD_Inputs = g_GetPluginArrayInterface("VB_DynamicModulator_IdealPSD_Inputs")

		outStr = VB_DynamicModulator_IdealPSD_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_DynamicModulator_IdealPSD_Outputs = g_GetPluginArrayInterface("VB_DynamicModulator_IdealPSD_Outputs")

		outStr = VB_DynamicModulator_IdealPSD_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_DynamicModulator_IdealPSD_globalVar = 1


	End If
	Redim returnValue(10)  ' Size should be equivalent to number of outputs being returned
	
	' Dim input parameters
    Dim DateUTC, CbName, ObjectPath, EpochSec, DataRate, RFCarrierFreq, ObjPosLLA
	
    ' Initialize Input values
	DateUTC       = inputData(VB_DynamicModulator_IdealPSD_Inputs.DateUTC)
	CbName        = inputData(VB_DynamicModulator_IdealPSD_Inputs.CbName)
	ObjectPath    = inputData(VB_DynamicModulator_IdealPSD_Inputs.ObjectPath)
	EpochSec      = inputData(VB_DynamicModulator_IdealPSD_Inputs.EpochSec)
	RFCarrierFreq = inputData(VB_DynamicModulator_IdealPSD_Inputs.RFCarrierFreq)
	ObjPosLLA     = inputData(VB_DynamicModulator_IdealPSD_Inputs.ObjectPosLLA)
	DataRate      = inputData(VB_DynamicModulator_IdealPSD_Inputs.DataRate)
	
    ' Dim STK expected output parameters
	Dim IsDynamic, ModulationName,  ModulationEfficiency, CodeRate, SpectrumLimitLow, SpectrumLimitHi
	Dim UsePSD, ChipsPerBit
 	
	
	'############################################################################################
	' USER PLUGIN RF MODULATOR MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR RF MODULATOR COMPUTATION MODEL
	'
	' This sample demonstrates how to dynamically select different modulations and/or encoding while 
	' leaving STK compute theoretical PSD.  If one wants to specify a custome PSD one should use
	' the other interface (see VB_DynamicModulator_CustomPSD.vbs) example.
	'
	' All input and out paramters have been mapped to variables described below.
	' This is an example of how an modulator script can be used to dynamically change either
    ' a modulator or a particular encoding while allowing STK to compute a theoretical PSD.
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
	'       IsDynamic        - Indicates if script is time-dynamic (see above).   int
	'       ModulationName   - Modulation name, for demodulator selection.        string ( max 32 characters )
	'     	ModulationEfficiency - Efficiency of the modulation, Hz/bits per sec.     double  
	'       CodeRate         - Ratio of uncoded to coded bits (i.e. 1/2 Code Rate is 0.5) double  
    '       PSDShape         - A PSD shape which STK will use to generate the Power
    '                          Spectral Density if Use Signal PSD is enabled.
	'		SpectrumLimitLow - Lower band limit which to compute a PSD over in Hz
    '                          and relative to RF carrier frequency (-100GHz to 0). double
	'		SpectrumLimitHi  - Upper band limit which to compute a PSD over in Hz and 
	'                          relative to RF carrier frequency (0 to 100GHz).    double 
	'       UsePSD           - Use signal PSD indicator, 0 or 1.                  int
    '       ChipsPerBit      - Number of chips per bit.                           int

	
	
    ' Dim temporaries used for this particular example
    Dim I, waveformSelector
	Dim objPosLat, objPosLon, objPosAlt 

	
    ' Initialize Output values
	IsDynamic        = 1 
	ModulationName   = "BPSK" ' Only used by STK for proper demodulation
	PSDShape         = "BPSK"
	ModulationEfficiency = 2.0  
	CodeRate         = 1.0
    ChipsPerBit      = 1
	SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0
	SpectrumLimitHi  = -SpectrumLimitLow
    UsePSD           = 0

	objPosLat = ObjPosLLA(0)
	objPosLon = ObjPosLLA(1)
	objPosAlt = ObjPosLLA(2)

	waveformSelector = Int(EpochSec) Mod 4
	
    ' First waveform is...
    ' QAM256
	If ( waveformSelector = 0) Then
		ModulationName   = "QAM256"
		PSDShape         = "QAM256"
		ModulationEfficiency = 0.25
		CodeRate = 1.0
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0
		SpectrumLimitHi  = -SpectrumLimitLow
    ' Second waveform is...
    ' QAM16
	ElseIf ( waveformSelector = 1) Then
		ModulationName   = "QAM16" 
		PSDShape         = "QAM16"
		ModulationEfficiency = 0.5
		CodeRate = 1.0
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0
		SpectrumLimitHi  = -SpectrumLimitLow
    ' Third waveform is...
    ' 8PSK
	ElseIf (waveformSelector = 2) Then
		ModulationName   = "8PSK" 
		PSDShape         = "8PSK"
		ModulationEfficiency =  0.6666666666
		CodeRate = 1.0
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0
		SpectrumLimitHi  = -SpectrumLimitLow
    ' Fourth waveform is...
    ' BPSK
		Else
		ModulationName   = "BPSK" 
        PSDShape         = "BPSK"
		ModulationEfficiency = 2.0
		CodeRate = 1.0
		SpectrumLimitLow = -(DataRate * ModulationEfficiency / CodeRate * ChipsPerBit)/2.0
		SpectrumLimitHi  = -SpectrumLimitLow
	End If
	
 	' Modulator MODEL END
	
	
	' #####################################################
	returnValue(VB_DynamicModulator_IdealPSD_Outputs.IsDynamic)            = IsDynamic
    returnValue(VB_DynamicModulator_IdealPSD_Outputs.ModulationName)       = ModulationName
	returnValue(VB_DynamicModulator_IdealPSD_Outputs.ModulationEfficiency) = ModulationEfficiency
	returnValue(VB_DynamicModulator_IdealPSD_Outputs.CodeRate)             = CodeRate
	returnValue(VB_DynamicModulator_IdealPSD_Outputs.PSDShape)             = PSDShape
	returnValue(VB_DynamicModulator_IdealPSD_Outputs.SpectrumLimitLow)     = SpectrumLimitLow
	returnValue(VB_DynamicModulator_IdealPSD_Outputs.SpectrumLimitHi)      = SpectrumLimitHi
	returnValue(VB_DynamicModulator_IdealPSD_Outputs.UsePSD)               = UsePSD
    returnValue(VB_DynamicModulator_IdealPSD_Outputs.ChipsPerBit)          = ChipsPerBit

	'############################################################################################
	' END OF USER MODEL AREA	
	'############################################################################################

	VB_DynamicModulator_IdealPSD_compute = returnValue

End Function

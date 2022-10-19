'#########################################################################################
' SAPMLE FOR VBS BASED CUSTOM RF FILTER PLUGIN SCRIPT PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER FILTER MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
' If you change the file name then the function names below
' must be edited to match the file name
'#########################################################################################

' VB_DynamicFilter

Dim VB_DynamicFilter_globalVar
Dim VB_DynamicFilter_Inputs
Dim VB_DynamicFilter_Outputs


'==========================================================================
' VB_DynamicFilter() fctn
'==========================================================================
Function VB_DynamicFilter ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_DynamicFilter_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_DynamicFilter_globalVar = -1

		retVal = VB_DynamicFilter_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_DynamicFilter_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_DynamicFilter = retVal

End Function

Function VB_DynamicFilter_register()

        Dim ac
	ReDim descripStr(3), argStr(13)

        ac = 0
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = IsDynamic"
	descripStr(2)="ArgumentName = IsDynamic"
	argStr(ac) = descripStr

        ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = LowerBandlimit"
	descripStr(2)="ArgumentName = LowerBandlimit"
	argStr(ac) = descripStr

        ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = UpperBandlimit"
	descripStr(2)="ArgumentName = UpperBandlimit"
	argStr(ac) = descripStr

        ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = NumPoints"
	descripStr(2)="ArgumentName = NumPoints"
	argStr(ac) = descripStr

        ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Attenuation"
	descripStr(2)="ArgumentName = Attenuation"
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
	descripStr(1)="Name = CenterFreq"
	descripStr(2)="ArgumentName = CenterFreq"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

        ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = FreqStepSize"
	descripStr(2)="ArgumentName = FreqStepSize"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

   'MsgBox  ac

	VB_DynamicFilter_register = argStr

End Function


Function VB_DynamicFilter_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_DynamicFilter_globalVar < 0 Then

		Set VB_DynamicFilter_Inputs = g_GetPluginArrayInterface("VB_DynamicFilter_Inputs")

		outStr = VB_DynamicFilter_Inputs.Describe()

		displayDialog outStr , 800

		Set VB_DynamicFilter_Outputs = g_GetPluginArrayInterface("VB_DynamicFilter_Outputs")

		outStr = VB_DynamicFilter_Outputs.Describe()

		displayDialog outStr , 800

		VB_DynamicFilter_globalVar = 1

		'MsgBox inputData(VB_DynamicFilter_Inputs.DateUTC)

	End If
	Redim returnValue(6)  ' Size should be equivalent to number of outputs being returned

	' Dim input parameters
    Dim DateUTC, CbName, ObjectPath, EpochSec, ObjectPosLLA, CenterFreq, FreqStepSize

	    ' Initialize Input values
	DateUTC       = inputData(VB_DynamicFilter_Inputs.DateUTC)
	CbName        = inputData(VB_DynamicFilter_Inputs.CbName)
	ObjectPath    = inputData(VB_DynamicFilter_Inputs.ObjectPath)
    EpochSec      = inputData(VB_DynamicFilter_Inputs.EpochSec)
	ObjectPosLLA  = inputData(VB_DynamicFilter_Inputs.ObjectPosLLA)
	CenterFreq    = inputData(VB_DynamicFilter_Inputs.CenterFreq)
	FreqStepSize  = inputData(VB_DynamicFilter_Inputs.FreqStepSize)

    ' Dim STK expected output parameters
	Dim IsDynamic, LowerBandlimit, UpperBandlimit, NumPoints, Attenuation (100000,1)


	'############################################################################################
	' USER PLUGIN RF FILTER MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR RF FILTER COMPUTATION MODEL
	' All input and out paramters have been mapped to variables described below.
	'############################################################################################
	' NOTE: the outputs that are returned MUST be in the same order as registered
	' If IsDynamic is set to 0 (false), this script will only be called once and the same outputs
	' will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called
	' at every timestep.
    '
	' All frequency and frequency step value units are Hertz.
	' Attenuation values are in dB.
	' Time is in Seconds.
	' Lat, Long are in radians and Altitude is in Meters
    '
	' Script input variables available to user:
	'		DateUTC      - Date in UTC.                                              string
	'		CbName	     - Name of the transmitter's central body.                   string
	'		ObjectPath   - Path of the object, i.e. objects fully qualified name.    string
	'		EpochSec     - Current simulation epoch seconds.                         double
	'		ObjectPosLLA - Objects position in LLA (Rad,Rad,m).                      double(3)
	'       CenterFreq   - Center frequency of the filter, in Hz.                    double
    '       FreqStepSize - Frequency step size of the attenuation values, in Hz.   double
	'
	' Script outputs which must be filled in by the user:
	'       IsDynamic      - Indicates if script is time-dynamic (see above).        int
	'		LowerBandlimit - Lower band limit of the filter attenuation points
    '                        in Hz and relative to CenterFreq (-100GHz to 0).        double
	'		UpperBandlimit - Upper band limit of the filter attenuation points
    '                        in Hz and relative to CenterFreq (0 to 100GHz).         double
    '       NumPoints   - Number of attenuation points being returned (max 100,000). int
	'       Attenuation - Filter's attenuation values, in dB (0 to -3000 dB).        double array [ NumPoints x 1 ]


	'  Dim temporaries used for this particular example
    Dim I, filterSelector

    ' Initialize Output values
	IsDynamic = 1
  	FreqStepSize = 10000

	filterSelector = Int(EpochSec) Mod 3

    ' First filter is...
	' 60 MHz wide w/ zero attenuation at center frequency
	' and -60 dB down at edges
	If (filterSelector = 0) Then
	   LowerBandlimit = -30e6
       UpperBandlimit = 30e6
	' Second filter is...
	' 30 MHz wide w/ zero attenuation at center frequency
	' and -60 dB down at edges
	ElseIf (filterSelector = 1) Then
	   LowerBandlimit = -15e6
       UpperBandlimit = 15e6
	' Third filter is...
	' 10 MHz wide w/ zero attenuation at center frequency
	' and -60 dB down at edges
	ElseIf (filterSelector = 2) Then
	   LowerBandlimit = -5e6
       UpperBandlimit = 5e6
	End If

	NumPoints    = 1+(UpperBandlimit - LowerBandlimit)/FreqStepSize

	' Make sure we don't blow out the fixed array size
    If (NumPoints) > 100000 Then
        NumPoints = 100000
        FreqStepSize = (UpperBandlimit - LowerBandlimit)/(NumPoints - 1)
    End If

    For I = 0 To (NumPoints-1)/2 Step 1
	  attenDB = -60 + I*2*60/(NumPoints-1)
	  Attenuation (I, 0) = attenDB
	  Attenuation (NumPoints-1-I, 0) = attenDB
	Next


	' FILTER MODEL END
	' #####################################################

	returnValue(VB_DynamicFilter_Outputs.IsDynamic)       = IsDynamic
	returnValue(VB_DynamicFilter_Outputs.LowerBandlimit)  = LowerBandlimit
	returnValue(VB_DynamicFilter_Outputs.UpperBandlimit)  = UpperBandlimit
	returnValue(VB_DynamicFilter_Outputs.NumPoints)       = NumPoints
	returnValue(VB_DynamicFilter_Outputs.Attenuation)     = Attenuation

	'############################################################################################
	' END OF USER MODEL AREA
	'############################################################################################

	VB_DynamicFilter_compute = returnValue

End Function

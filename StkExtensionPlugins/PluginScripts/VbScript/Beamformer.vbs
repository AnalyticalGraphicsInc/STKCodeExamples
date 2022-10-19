'#########################################################################################
' EXAMPLE VBS BASED SCRIPT "BEAMFORMER" PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER "DIRECTION PROVIDER" MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
' If you change the file name then the function names below
' must be edited to match the file name
'#########################################################################################

Dim VB_Beamformer_globalVar
Dim VB_Beamformer_Inputs
Dim VB_Beamformer_Outputs

'==========================================================================
' VB_Beamformer() fctn
'==========================================================================
Function VB_Beamformer ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_Beamformer_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_Beamformer_globalVar = -1

		retVal = VB_Beamformer_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_Beamformer_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_Beamformer = retVal

End Function

Function VB_Beamformer_register()

    Dim ac
	ReDim descripStr(3), argStr(9)

    ac = 0
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = IsDynamic"
	descripStr(2)="ArgumentName = IsDynamic"
	argStr(ac) = descripStr
 
    ac = ac + 1
    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = Weights"
    descripStr(2)="ArgumentName = Weights"
    argStr(ac) = descripStr
	
	ReDim descripStr(4)

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = EpochSec"
	descripStr(2)="ArgumentName = EpochSec"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

   ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = NumberOfElements"
	descripStr(2)="ArgumentName = NumberOfElements"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = DesignFrequency"
	descripStr(2)="ArgumentName = DesignFrequency"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
     ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = OperatingFrequency"
	descripStr(2)="ArgumentName = OperatingFrequency"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = NumberOfBeamDirections" ' currently only supporting 1 beam
	descripStr(2)="ArgumentName = NumberOfBeamDirections"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
	ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = BeamDirections"  ' currently only supporting 1 beam
	descripStr(2)="ArgumentName = BeamDirections"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = NumberOfNullDirections" ' max 100
	descripStr(2)="ArgumentName = NumberOfNullDirections" 
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
	ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = NullDirections"  ' max 100
	descripStr(2)="ArgumentName = NullDirections"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
    'MsgBox  ac
	VB_Beamformer_register = argStr

End Function


Function VB_Beamformer_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_Beamformer_globalVar < 0 Then

		Set VB_Beamformer_Inputs = g_GetPluginArrayInterface("VB_Beamformer_Inputs")

		outStr = VB_Beamformer_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_Beamformer_Outputs = g_GetPluginArrayInterface("VB_Beamformer_Outputs")

		outStr = VB_Beamformer_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_Beamformer_globalVar = 1

		'MsgBox inputData(VB_Beamformer_Inputs.EpochSec)

	End If
	Redim returnValue(2)  ' Size should be equivalent to number of outputs being returned
	
	' Dim input parameters
    Dim EpochSec, NumberOfElements, DesignFrequency, OperatingFrequency, NumberOfBeamDirections, BeamDirections, NumberOfNullDirections, NullDirections
	
    ' Initialize Input values
	EpochSec         = inputData(VB_Beamformer_Inputs.EpochSec)
	NumberOfElements = inputData(VB_Beamformer_Inputs.NumberOfElements)
	DesignFrequency = inputData(VB_Beamformer_Inputs.DesignFrequency)
	OperatingFrequency = inputData(VB_Beamformer_Inputs.OperatingFrequency)
	NumberOfBeamDirections   = inputData(VB_Beamformer_Inputs.NumberOfBeamDirections)
	BeamDirections       = inputData(VB_Beamformer_Inputs.BeamDirections)
	NumberOfNullDirections   = inputData(VB_Beamformer_Inputs.NumberOfNullDirections)
	Nullirections       = inputData(VB_Beamformer_Inputs.NullDirections)
	
    ' Dim STK expected output parameters
	Dim IsDynamic
	Dim Weights
 	
	
	'############################################################################################
	' All input and out paramters have been mapped to variables described below.
	'############################################################################################
	' NOTE: the outputs that are returned MUST be in the same order as registered
	' If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	' will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	' at every timestep.
	'
	' All weights are to be complex numbers(see STK help).
	'
	' Script input variables available to user:
	'		EpochSec   - Current simulation epoch seconds.                         double  
	'		NumberOfElements - Number of enabled antenna elements in the array.    int
	'		DesignFrequency - Design frequency of the antenna array (Hz).          double
	'		OperatingFrequency - Current operating frequency of the antenna (Hz).  double
    '       NumberOfBeamDirections -  The number of items in the BeamDirections 
	'                          input field described below. 	                   int
	'		BeamDirections     - Array of Az/El values (rad/rad) representing 
    '                          the direction "entities", where "entities" are  
    '                          defined by the specific selected Direction Provider. 
    '                          Currently an array length of one is supported.      double(1,2)
    '       NumberOfNullDirections -  The number of items in the NullDirections 
	'                          input field described below. 	                   int
	'		NullDirections     - Array of Az/El values (rad/rad) representing the 
    '                          direction "entities", where "entities" are defined  
    '                          by the specific selected beam-steering Direction Prvdr. 
	'                          Currenly an array length of one is supported.      double(1,2)
	'
	' Script outputs which must be filled in by the user:
	'       IsDynamic      -   Indicates if script is time-dynamic (see above).    int
	'       Weights        -   Complex values for each element.  Format is 
	'                          linear array of real/imaginary interleaved values.
	'############################################################################################
	' USER PLUGIN BEAM DIRECTION PROVIDER MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR DIRECTION PROVIDER COMPUTATION MODEL
	'
	' This sample demonstrates how to dynamically return weights.  Implements a static "deck" of
        ' three weight sets to be applied at specific time steps. This is a very simplistic example
	' to demostrate how to dynamically return weights.
	' 
	'
	

    ' Dim temporaries used for this particular example
 
    ' Initialize Output values
	IsDynamic        = 1 

	If EpochSec < 600 Then
	   Weights = Array(-0.0727559,0.233901, 0.0164913,0.215751 ,-0.0603389,0.244863, 0.153553,0.128345, 0.184669,0.227678, -0.0637141,0.202648, 0.205931,0.0567225, 0.160496,0.177382, 0.147236,-0.0999877, 0.313338,-1.85741e-013, 0.147236,0.0999877, 0.160496,-0.177382, 0.205931,-0.0567225, -0.0637141,-0.202648, 0.184669,-0.227678, 0.153553,-0.128345, -0.0603389,-0.244863, 0.0164913,-0.215751, -0.0727559,-0.233901) 
        ElseIf (EpochSec >= 600) And (EpochSec < 1200) Then
	   Weights = Array(-0.0384998,0.242632, 0.116201,0.175782, -0.0728582,0.22732, 0.199626,-0.0118431, 0.180923,0.201406, -0.124046,0.194012, 0.217225,-0.00904073, 0.127373,0.194714, 0.0867985,-0.184947, 0.281027,1.38456e-015, 0.0867985,0.184947, 0.127373,-0.194714, 0.217225,0.00904073, -0.124046,-0.194012, 0.180923,-0.201406, 0.199626,0.0118431, -0.0728582,-0.22732, 0.116201,-0.175782, -0.0384998,-0.242632) 
	Else
	   Weights = Array(-0.0160897,0.235592, 0.195985,0.120679, -0.0770078,0.204931, 0.186028,-0.139551, 0.158855,0.171406, -0.147394,0.179345, 0.217917,-0.0586559, 0.102059,0.196255, 0.0284807,-0.231605, 0.232606,-7.91854e-014, 0.0284807,0.231605, 0.102059,-0.196255, 0.217917,0.0586559, -0.147394,-0.179345, 0.158855,-0.171406, 0.186028,0.139551, -0.0770078,-0.204931, 0.195985,-0.120679, -0.0160897,-0.235592) 
        End If
	
	'############################################################################################
	' END OF USER MODEL AREA	
	'############################################################################################
	

	returnValue(VB_Beamformer_Outputs.IsDynamic)   = IsDynamic
        returnValue(VB_Beamformer_Outputs.Weights)     = Weights
	VB_Beamformer_compute = returnValue

End Function

'#########################################################################################
' EXAMPLE VBS BASED SCRIPT "NULL DIRECTION PROVIDER" PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER "DIRECTION PROVIDER" MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
' If you change the file name then the function names below
' must be edited to match the file name
'#########################################################################################

Dim VB_NullDirectionProvider_globalVar
Dim VB_NullDirectionProvider_Inputs
Dim VB_NullDirectionProvider_Outputs

Dim gNullScanAzStepSize, gNullScanElStepSize, gNullScanMinAz, gNullScanMaxAz, gNullScanMinEl, gNullScanMaxEl, gNullScanAz, gNullScanEl, degToRad, radToDeg

degToRad = 0.01745329252
radToDeg = 57.29577951308
gNullScanAzStepSize = NulldegToRad*4
gNullScanElStepSize = NulldegToRad*4
gNullScanMinAz = -30*NulldegToRad
gNullScanMaxAz = 30*NulldegToRad
gNullScanMinEl = -30*NulldegToRad
gNullScanMaxEl = 30*NulldegToRad

gNullScanAz = gNullScanMinAz
gNullScanEl = gNullScanMinEl
	

'==========================================================================
' VB_NullDirectionProvider() fctn
'==========================================================================
Function VB_NullDirectionProvider ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_NullDirectionProvider_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_NullDirectionProvider_globalVar = -1

		retVal = VB_NullDirectionProvider_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_NullDirectionProvider_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_NullDirectionProvider = retVal

End Function

Function VB_NullDirectionProvider_register()

    Dim ac
	ReDim descripStr(3), argStr(18)

    ac = 0
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = IsDynamic"
	descripStr(2)="ArgumentName = IsDynamic"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = NumDirections"
	descripStr(2)="ArgumentName = NumDirections"
	argStr(ac) = descripStr
	
    ac = ac + 1
    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = Azimuths"
    descripStr(2)="ArgumentName = Azimuths"
    argStr(ac) = descripStr
    
    ac = ac + 1
    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = Elevations"
    descripStr(2)="ArgumentName = Elevations"
    argStr(ac) = descripStr
	
	
	ReDim descripStr(4)

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
	descripStr(1)="Name = PosLLA"
	descripStr(2)="ArgumentName = PosLLA"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

   ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = PosCBF"
	descripStr(2)="ArgumentName = PosCBF"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = NumberOfMembers"
	descripStr(2)="ArgumentName = NumberOfMembers"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = MemberPositionFormat"
	descripStr(2)="ArgumentName = MemberPositionFormat"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
    ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = MemberPositions"
	descripStr(2)="ArgumentName = MemberPositions"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
	ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = MemberFrequencies"
	descripStr(2)="ArgumentName = MemberFrequencies"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

	ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = MemberPwrs"
	descripStr(2)="ArgumentName = MemberPwrs"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr

	ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = MemberIds"
	descripStr(2)="ArgumentName = MemberIds"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
	ac = ac + 1
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = MemberCategories"
	descripStr(2)="ArgumentName = MemberCategories"
	descripStr(3)="Type = Value"
	argStr(ac) = descripStr
	
	
    'MsgBox  ac
	VB_NullDirectionProvider_register = argStr

End Function


Function VB_NullDirectionProvider_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_NullDirectionProvider_globalVar < 0 Then

		Set VB_NullDirectionProvider_Inputs = g_GetPluginArrayInterface("VB_NullDirectionProvider_Inputs")

		outStr = VB_NullDirectionProvider_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_NullDirectionProvider_Outputs = g_GetPluginArrayInterface("VB_NullDirectionProvider_Outputs")

		outStr = VB_NullDirectionProvider_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_NullDirectionProvider_globalVar = 1

		'MsgBox inputData(VB_NullDirectionProvider_Inputs.EpochSec)

	End If
	Redim returnValue(4)  ' Size should be equivalent to number of outputs being returned
	
	' Dim input parameters
    Dim ObjectPath, EpochSec, PosLLA, PosCBF, NumberOfMembers, MemberPositionFormat, MemberPositions, MemberFrequencies, MemberPwrs, MemberIds, MemberCategories
	
    ' Initialize Input values
	ObjectPath           = inputData(VB_NullDirectionProvider_Inputs.ObjectPath)
	EpochSec             = inputData(VB_NullDirectionProvider_Inputs.EpochSec)
	PosLLA               = inputData(VB_NullDirectionProvider_Inputs.PosLLA)
	PosCBF               = inputData(VB_NullDirectionProvider_Inputs.PosCBF)
	NumberOfMembers      = inputData(VB_NullDirectionProvider_Inputs.NumberOfMembers)
	MemberPositionFormat = inputData(VB_NullDirectionProvider_Inputs.MemberPositionFormat)
	MemberPositions      = inputData(VB_NullDirectionProvider_Inputs.MemberPositions)
	MemberFrequencies    = inputData(VB_NullDirectionProvider_Inputs.MemberFrequencies)
	MemberPwrs           = inputData(VB_NullDirectionProvider_Inputs.MemberPwrs)
	MemberIds            = inputData(VB_NullDirectionProvider_Inputs.MemberIds)	
	MemberCategories     = inputData(VB_NullDirectionProvider_Inputs.MemberCategories)
	
    ' Dim STK expected output parameters
	Dim IsDynamic, NumDirections
	Dim Azs(100), Els(100)
	Azs(0) = 0
	Els(0) = 0
 	
	
	'############################################################################################
	' All input and out paramters have been mapped to variables described below.
	'############################################################################################
	' NOTE: the outputs that are returned MUST be in the same order as registered
	' If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	' will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	' at every timestep.
	'
	' All directions specified as Azimuth and Elevation angles (see STK help) in degrees and 
	' relative to the entity's body coordinate system.
	'
	' Script input variables available to user:
	'		ObjectPath - Path of the object, i.e. objects fully qualified name.   string
	'		EpochSec   - Current simulation epoch seconds.                        double  
	'		PosLLA	   - Position the object in LLA.                              string
	'		PosCBF	   - Position the object in CBF.                              string
	'		NumberOfMembers - Number of members in view at this time step. Used
    '                         to define size of input field arrays.  Max 100
	'                         WARNING: Always check this field since, for efficency, 
	'                                  STK may provide old data for 
	'                                  other fields and should be considered stale
    '                                  if this field is 0.                       	 int
    '       MemberPositionFormat - Defines if memberPositions array will be a
	'                              relative position (to antenna) in Theta/Phi/Range 
	'                             (rad/rad/m) or X/Y/Z (m/m/m)                       int  
	'		MemberPositions      - Member positions in format specified by
	'                              MemberPositionFormat.                          double(3)
    '       MemberFrequencies   -  Member frequencies (-1 for non-RF members)     double(100)
    '       MemberPwrs          -  Member eirp (-3000dBW for non-emitter members) double(100)
    '       MemberIds           -  Member ids, 0-based as listed in antenna.         int(100)
	'       MemberCategories    -  Member object category (Aircraft, Facility, etc.) int(100)
	'
	' Script outputs which must be filled in by the user:
	'       IsDynamic           - Indicates if script is time-dynamic (see above).   int
	'       NumDirections       - Currently, beam only supports 1 direction           int
    '       Azimuths            - Az in antenna's coordinate system (rad)           double(100)
    '       Elevations          - El in antenna's coordinate system (rad)           double(100)
    '
	'############################################################################################
	' USER PLUGIN BEAM DIRECTION PROVIDER MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR DIRECTION PROVIDER COMPUTATION MODEL
	'
	' This sample demonstrates how to dynamically return nulling directions.  This script looks 
	' at each member to determine it's category (i.e. Aircraft, Facility, Satellite, etc.) to 
	' determine if it should null the member.  If the member is not an aircraft, it will return
	' the member's direction in order to be nulled.
	' This is just a simplistic example to demonstrate how to dynamically return null directions.
	
    ' Dim temporaries used for this particular example
	
    ' Initialize Output values
	IsDynamic        = 1 

	' If any object is in radar range, use track mode determine who to track
	NumDirections = 0
	For i = 0 To NumberOfMembers - 1
	   If MemberCategories(i) <> 1 Then  ' If it's not an aircraft...treat it as a jammer and null it
	      Azs(NumDirections) = MemberPositions(3*i)
	      Els(NumDirections) = MemberPositions(3*i+1)
		  NumDirections = NumDirections+1 
	   End If
	 Next  
    
    '############################################################################################
    ' END OF USER MODEL AREA    
    '############################################################################################    
	
	returnValue(VB_NullDirectionProvider_Outputs.IsDynamic)      = IsDynamic
    returnValue(VB_NullDirectionProvider_Outputs.NumDirections)  = NumDirections
	returnValue(VB_NullDirectionProvider_Outputs.Azimuths)       = Azs
	returnValue(VB_NullDirectionProvider_Outputs.Elevations)     = Els

	'############################################################################################
	' END OF USER MODEL AREA	
	'############################################################################################

	VB_NullDirectionProvider_compute = returnValue

End Function

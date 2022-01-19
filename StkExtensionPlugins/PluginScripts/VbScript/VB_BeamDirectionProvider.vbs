'#########################################################################################
' EXAMPLE VBS BASED SCRIPT "BEAM DIRECTION PROVIDER" PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER "BEAM DIRECTION PROVIDER" MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
' If you change the file name then the function names below
' must be edited to match the file name
'#########################################################################################

Dim VB_BeamDirectionProvider_globalVar
Dim VB_BeamDirectionProvider_Inputs
Dim VB_BeamDirectionProvider_Outputs

' Dim some useful constants for conversions used in this particular example...
Dim degToRad, radToDeg, piOver2
piOver2 = 1.5707963268
degToRad = 0.01745329252
radToDeg = 57.29577951308
	
' Dim globals used for this particular example.  Globals will retain their state across each time step script call...
 Dim gScanAzStepSize, gScanElStepSize, gScanMinAz, gScanMaxAz, gScanMinEl, gScanMaxEl, gScanAz, gScanEl  
 gScanAzStepSize = 10*degToRad  ' Az scan step size
 gScanElStepSize = 10*degToRad  ' El scan step size
 gScanMinAz = -30*degToRad     ' Az min scan angle
 gScanMaxAz = 30*degToRad      ' Az max scan angle
 gScanMinEl = -40*degToRad     ' El min scan angle
 gScanMaxEl = 40*degToRad       ' El max scan angle
 
 gScanAz = gScanMinAz
 gScanEl = gScanMinEl	

	
'==========================================================================
' VB_BeamDirectionProvider() fctn
'==========================================================================
Function VB_BeamDirectionProvider ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_BeamDirectionProvider_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_BeamDirectionProvider_globalVar = -1

		retVal = VB_BeamDirectionProvider_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_BeamDirectionProvider_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_BeamDirectionProvider = retVal

End Function

Function VB_BeamDirectionProvider_register()

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
	descripStr(1)="Name = MemberPositionFormat"
	descripStr(2)="ArgumentName = MemberPositionFormat"
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
	VB_BeamDirectionProvider_register = argStr

End Function


Function VB_BeamDirectionProvider_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_BeamDirectionProvider_globalVar < 0 Then

		Set VB_BeamDirectionProvider_Inputs = g_GetPluginArrayInterface("VB_BeamDirectionProvider_Inputs")

		outStr = VB_BeamDirectionProvider_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_BeamDirectionProvider_Outputs = g_GetPluginArrayInterface("VB_BeamDirectionProvider_Outputs")

		outStr = VB_BeamDirectionProvider_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_BeamDirectionProvider_globalVar = 1

	End If
	Redim returnValue(4)  ' Size should be equivalent to number of outputs being returned
	
	' Dim input parameters
    Dim ObjectPath, EpochSec, PosLLA, PosCBF, NumberOfMembers, MemberPositionFormat, MemberPositions, MemberFrequencies, MemberPwrs, MemberIds, MemberCategories
	
    ' Initialize Input values
	ObjectPath           = inputData(VB_BeamDirectionProvider_Inputs.ObjectPath)
	EpochSec             = inputData(VB_BeamDirectionProvider_Inputs.EpochSec)
	PosLLA               = inputData(VB_BeamDirectionProvider_Inputs.PosLLA)
	PosCBF               = inputData(VB_BeamDirectionProvider_Inputs.PosCBF)
	NumberOfMembers      = inputData(VB_BeamDirectionProvider_Inputs.NumberOfMembers)
	MemberPositionFormat = inputData(VB_BeamDirectionProvider_Inputs.MemberPositionFormat)
	MemberPositions      = inputData(VB_BeamDirectionProvider_Inputs.MemberPositions)
	MemberFrequencies    = inputData(VB_BeamDirectionProvider_Inputs.MemberFrequencies)
	MemberPwrs           = inputData(VB_BeamDirectionProvider_Inputs.MemberPwrs)
	MemberIds            = inputData(VB_BeamDirectionProvider_Inputs.MemberIds)	
	MemberCategories     = inputData(VB_BeamDirectionProvider_Inputs.MemberCategories)
	
    ' Dim STK expected output parameters
	Dim IsDynamic, NumDirections
	Dim Azs(100), Els(100)
	Azs(0) = 0
	Els(0) = 0
	
	'############################################################################################
	' All input and output paramters have been mapped to variables described below.
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
	'       NumDirections       - Number of directions being returned                int
    '       Azimuths            - Az in antenna's coordinate system (rad)           double(100)
    '       Elevations          - El in antenna's coordinate system (rad)           double(100)
    '
	'############################################################################################
	' USER PLUGIN BEAM DIRECTION PROVIDER MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR DIRECTION PROVIDER COMPUTATION MODEL
	'
	' This simple sample demonstrates how to dynamically return beam directions.  This script 
	' defines the antenna's field of regard (FOR) and then scans the FOR.  If any aircraft fly   
	' within the FOR and within effective range it will switch to tracking mode.  It will switch 
	' it's target if another member becomes closer.  If all objects are out of the FOR, it will 
	' switch back to track mode. This is just a simplistic example to demonstrate how to 
	' dynamically return direction.
	
    'Dim temporaries used for this particular example
 	Dim objPosLat, objPosLon, objPosAlt, objAz, objEl, objRange, radarRange, minAz, minEl, minRange
 
    ' Initialize Output values
	IsDynamic     = 1 
 	NumDirections = 1

	radarRange = 162510.500 ' m
	' If any object is in radar range, use track mode determine who to track
	minRange = 1e300
	For i = 0 To NumberOfMembers - 1   
	   objAz = MemberPositions(3*i)
	   objEl = MemberPositions(3*i+1)
	   objRange = MemberPositions(3*i+2)
	   objCat = MemberCategories(i)
	   ' Track the closest object within range of the radar
	   If objRange < radarRange Then
	       If objRange < minRange Then 
		   ' Only target it if it's in front hemisphere
		       If objAz > -piOver2 And objAz < piOver2 Then
		          minAz = objAz
			      minEl = objEl
			      minRange = objRange
				  trkdCat = objCat
			   End If
		   End If
	   End If
	 Next

	' If nothing is inside radar range, continue scan mode
	
	' Define Az/El bins
	Dim AzSteps, ElSteps, TotalBins, TimeStep, TimeMatrix(9,7), count, temp, EpochSecInteger, TimeBinAz, TimeBinEl
	
	If minRange = 1e300 Then
		AzSteps = (gScanMaxAz - gScanMinAz) / gScanAzStepSize
		ElSteps = (gScanMaxEl - gScanMinEl) / gScanElStepSize
		TotalBins = (ElSteps + 1)*(AzSteps + 1)
		TimeStep = 1/(TotalBins-1)
		
		'TimeMatrix = zeros(ElSteps + 1,AzSteps + 1)
		count = 0
		For I = 1 To ElSteps+1 Step 1
			For J = 1 To AzSteps+1 Step 1
				TimeMatrix(I,J) = TimeStep*count
				count = count + 1
			Next
		Next
		
		temp = 0
		EpochSecInteger = Int(EpochSec)
		'MsgBox(EpochSec)
		For I = 1 To ElSteps+1 Step 1
			For J = 1 To AzSteps+1 Step 1
				If (EpochSec-EpochSecInteger) <= TimeMatrix(I,J) Then
					TimeBinAz = I
					TimeBinEl = J
					
					'MsgBox(TimeBinAz)
					'MsgBox(TimeBinEl)
					
					temp = 1
					Exit For
				End If
			Next
			If temp = 1 Then
				Exit For
			End If
		Next

	Azs(0) = gScanMinAz + gScanAzStepSize*(TimeBinAz-1)
	Els(0) = gScanMinEl + gScanElStepSize*(TimeBinEl-1)
	
	'MsgBox(gScanMinAz + gScanAzStepSize*(TimeBinAz-1))
	'MsgBox(gScanMinEl + gScanElStepSize*(TimeBinEl-1))
	
		'gScanAz = gScanAz + gScanAzStepSize
        'gBeamEl = gBeamEl + gElStepSize
		
	    'If EpochSec <= 0.00001 Then		  
	    '  gScanAz = gScanMinAz
	    '  gScanEl = gScanMinEl
	    'End If
    	'Azs(0) = gScanAz
	    'Els(0) = gScanEl 
		
		' Check for end of scan pattern and reset to begin scan pattern
		'If gScanAz > gScanMaxAz Then
		'	gScanEl = gScanEl + gScanElStepSize
		'	gScanAz = gScanMinAz
		'End If	
		
		'If gScanEl > gScanMaxEl Then
		'	gScanElStepSize = gScanElStepSize
		'	gScanEl = gScanMinEl
		'End If

    Else
	    Azs(0) = minAz
		Els(0) = minEl
    End If

	
    '############################################################################################
    ' END OF USER MODEL AREA    
    '############################################################################################    
	
	returnValue(VB_BeamDirectionProvider_Outputs.IsDynamic)      = IsDynamic
    returnValue(VB_BeamDirectionProvider_Outputs.NumDirections)  = NumDirections
	returnValue(VB_BeamDirectionProvider_Outputs.Azimuths)       = Azs
	returnValue(VB_BeamDirectionProvider_Outputs.Elevations)     = Els


	VB_BeamDirectionProvider_compute = returnValue

End Function

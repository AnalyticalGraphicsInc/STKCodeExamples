' Constraint Plugin component NIIRS_Constraint
' Author:     Tom Johnson (converted to VBS by Noah Ingwersen)
' Company:    Analytical Graphics, Inc.
' Copyright:  None.  Modify and distribute at will
'
' Description:
'
' This constraint calculates a modified form of the NIIRS image quality
' metric.  It's modified in that it doesn't calculate all the terms since
' I didn't have enough information to do so.  The first step is to calculate
' the Ground Sample Distance (GSD) for a sensor
' viewing another object.  This version of the GSD equation is parameterized
' in terms of Q, the optical ratio. Reference document is 
' “Image Quality and lamdaFN/p For Remote Sensing Systems,” Robert D. Fiete, 
' Optical Engineering, Vol. 38 No 7, July 1999
'
'                  SR * lamda
'       GSD =  --------------------------
'              Q * D * sqrt( sin(elev)  )
'
' where
'
'       SR = slant range (meters) from STK
'
'       lamda = wavelenth of the sensor you are observing with
'
'       Q     = optical ratio (unitless)
'
'       D     = optical diameter (meters)
'
'       elev  = elevation angle between the ground object and the sensor (from STK)
'
' The assumption is that the user provides lamda, Q, and D within the script and
' STK does the rest.  GSD is returned in meters.  Then we pass it into the NIIRS
' equation and return the NIIRS value.


Option explicit


'==========================================================================
' Constants
'==========================================================================
Dim micron2meters, meters2inches, lamda, D, Q

' Conversions
micron2meters = 1.0e-6
meters2inches = 39.37007874015876

' Sensor wavelenth (microns)
lamda = 0.65  ' 700 nanometers = 0.7 microns

' Optical diameter (meters)
D = 10

' Optical ratio (unitless). 
'
'                f
'   Q = lamda * ---
'                D
'       ------------
'             pp
'
'   where
'
'   f = focal length (meters)
'
'   pp = pixel pitch (meters)

Q = 2.0



Dim G_NIIRS_Constraint_Verbose
G_NIIRS_Constraint_Verbose = -1

'==========================================================================
' NIIRS_Constraint_GetConstraintDisplayName() fctn
'==========================================================================
Function NIIRS_Constraint_GetConstraintDisplayName ()
	NIIRS_Constraint_GetConstraintDisplayName = "NIIRS_Constraint"
End Function

'==========================================================================
' NIIRS_Constraint_GetAccessList() fctn
'==========================================================================

Dim G_NIIRS_Constraint_AllClasses
G_NIIRS_Constraint_AllClasses = "Facility,Place,Target"

Function NIIRS_Constraint_GetAccessList ( stateData )
	Dim strReturnValue
	Dim strBaseClass
	strBaseClass = stateData(1)

	'MsgBox "GetAccessList- BaseClass: " & strBaseClass, vbOKOnly, "NIIRS_Constraint"

	If strBaseClass = "Sensor" Then
		strReturnValue = G_NIIRS_Constraint_AllClasses
	End IF

	NIIRS_Constraint_GetAccessList = strReturnValue
End Function

'==========================================================================
' NIIRS_Constraint fctn
'==========================================================================
Function NIIRS_Constraint ( stateData )

	Dim retVal

	If VarType(stateData(0)) = vbEmpty Then

		retVal = NIIRS_Constraint_EvalFunc_compute(stateData)

	ElseIf stateData(0) = "register" Then

		G_NIIRS_Constraint_Verbose = -1

		retVal = NIIRS_Constraint_EvalFunc_register()

	ElseIf stateData(0) = "GetAccessList" Then

		retVal = NIIRS_Constraint_GetAccessList( stateData )

	ElseIf stateData(0) = "GetConstraintDisplayName" Then

		retVal = NIIRS_Constraint_GetConstraintDisplayName()

	Else

		'error: do nothing

	End If

	NIIRS_Constraint = retVal

End Function

Function NIIRS_Constraint_EvalFunc_register()

	ReDim descripStr(3), argStr(13)

	' Outputs

	argStr(0) = "ArgumentType = Output ; ArgumentName = Status ; Name = Status " 

	argStr(1) = "ArgumentType = Output ; ArgumentName = Result ; Name = Result " 

	' Inputs
	
	' Request the slant range and relative position vector of the sensor as seen
    ' from the ground site.  Relative position vector is requested in the topocentric
    ' frame so that we can easily calculate the elevation angle.

	argStr(2) = "ArgumentType = Input ; ArgumentName = range ; Name = range "
 
	argStr(3) = "ArgumentType = Input ; ArgumentName = toRelPos ; Name = toRelPosition ; RefName = TopoCentric "

	NIIRS_Constraint_EvalFunc_register = argStr

End Function


Function NIIRS_Constraint_EvalFunc_compute( stateData)
	
	' Grab the input data, note: argument 0 is the calling mode
	Dim range, relPos
	range = stateData(1)
	relPos = stateData(2)
	
	Dim NIIRS, sinAngle, GSD
	
	' Check for negative elevation angle (e.g. z component of relative
    ' position vector is negative.)  If so, bail out gracefully.
	If relPos(2) < 0.0 Then
	
		NIIRS = -1
		
	Else
	
		' Calculate sin of the elevation angle.
		sinAngle = relPos(2) / range
		
		' Now calculate the ground sample distance (GSD). 
		GSD = range * lamda * micron2meters / (Q * D * Sqr(sinAngle))
		
		' Now calculate the NIIRS value.  Don't forget that VB Script doesn't 
        ' natively support log10, so have to work in terms of natural logs.
        ' Terms after the RER are truncated since I don't have any values
        ' to use for them.
		
		' NIIRS coefficients
		Dim a, b, RER
		a   = 3.32
        b   = 1.559
        RER = 0.9
		
		NIIRS = 10.251 - a * log(GSD * meters2inches)/log(10) + b * log(RER)/log(10)
		
	End If
	
	
	Dim outStr

	If G_NIIRS_Constraint_Verbose < 0 Then

		' This section will output the values of the data on the first call
		
		outStr = "Slant Range: " & range & " m" & vbNewLine & vbNewLine
		outStr = outStr & "RelPosition X: " & relPos(0) & " m" & vbNewLine
		outStr = outStr & "RelPosition Y: " & relPos(1) & " m" & vbNewLine
		outStr = outStr & "RelPosition Z: " & relPos(2) & " m" & vbNewLine & vbNewLine
		outStr = outStr & "GSD: " & GSD & " m" & vbNewLine
		outStr = outStr & "NIIRS Value: " & NIIRS
		'MsgBox outStr

		G_NIIRS_Constraint_Verbose = 1

	End If


	
	' Send return to STK
	
	Redim returnValue(3)

	'returnValue(0) = " MESSAGE: [Info] NIIRS_Constraint- Everything is fine;  CONTROL: OK"
	returnValue(0) = "Okay"  'Status

	returnValue(1) = NIIRS   'Result: return NIIRS value

	NIIRS_Constraint_EvalFunc_compute = returnValue

End Function

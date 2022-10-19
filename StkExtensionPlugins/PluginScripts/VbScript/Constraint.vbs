' Constraint Plugin component VB_Constraint
' Author:     Guy Smith
' Company:    Analytical Graphics, Inc.
' Description: 
'  This function is called by the AGI Constraint Script Plugin software and is not intended 
'   for general use. This function takes a structure as input that contains
'   a 'method' string parameter that is utilized to determine what the purpose
'   of the call was. Four methods are supported. ( 'compute', 'register', 'GetAccessList',
'   'GetConstraintDisplayName' ) Refer to the Constraint Script Plugin documentation 
'   for an in-depth explaination of the functionality.
'
'  This file provides an example of the capabilities of the Constraint Script Plugin
'   functionality. The simple Constraint that is implemented by this script associates itself
'   with the Facility class and configures itself to return the the Constraint iteration 
'   step Epoch time (in STK Epoch seconds) passed in on every iteration.

' Notes:
' 1)  This source file is loaded into a general script engine namespace. Therefore,
'      any function names and global variables must be named appropriately or other
'      script source files could corrupt the data or call the functions. It is
'      possible to take advantage of the common namespace as a means to communicate
'      between other scripts/plugins, but, you must design the scripts to do this.
'      A good method to avoid namespace collision is to preface the functions and
'      variables with the name of the file, as was done in this example.
' 2)  Using MsgBox calls in this script is a good way to debug the functionality,
'      but will interupt STK, since the MsgBox must be Acknowledged before control
'      is passed back to the STK process.

' Copyright 2002, Analytical Graphics Incorporated


Option explicit

'MsgBox "VB_Constraint.vbs loaded..."


Dim G_VB_Constraint_Verbose

G_VB_Constraint_Verbose = -1

'==========================================================================
' VB_Constraint_GetConstraintDisplayName() fctn
'==========================================================================
Function VB_Constraint_GetConstraintDisplayName ()
	VB_Constraint_GetConstraintDisplayName = "VBPluginConstraint"
End Function

'==========================================================================
' VB_Constraint_GetAccessList() fctn
'==========================================================================

Dim G_VB_Constraint_AllClasses
G_VB_Constraint_AllClasses = "Aircraft,AreaTarget,Facility,GroundVehicle," _
       & "LaunchVehicle,Missile,Planet,Radar,Satellite,Ship,Star,Target"

Function VB_Constraint_GetAccessList ( stateData )
	Dim strReturnValue
	Dim strBaseClass
	strBaseClass = stateData(1)

	'MsgBox "GetAccessList- BaseClass: " & strBaseClass, vbOKOnly, "VB_Constraint"

	If strBaseClass = "Facility" Then
		strReturnValue = G_VB_Constraint_AllClasses
	End IF

	VB_Constraint_GetAccessList = strReturnValue
End Function

'==========================================================================
' VB_Constraint fctn
'==========================================================================
Function VB_Constraint ( stateData )

	Dim retVal

	If VarType(stateData(0)) = vbEmpty Then

		retVal = VB_Constraint_EvalFunc_compute(stateData)

	ElseIf stateData(0) = "register" Then

		G_VB_Constraint_Verbose = -1

		retVal = VB_Constraint_EvalFunc_register()

	ElseIf stateData(0) = "GetAccessList" Then

		retVal = VB_Constraint_GetAccessList( stateData )

	ElseIf stateData(0) = "GetConstraintDisplayName" Then

		retVal = VB_Constraint_GetConstraintDisplayName()

	Else

		'error: do nothing

	End If

	VB_Constraint = retVal

End Function

Function VB_Constraint_EvalFunc_register()

	ReDim descripStr(3), argStr(13)

	' Outputs

	argStr(0) = "ArgumentType = Output ; ArgumentName = Status ; Name = Status " 

	argStr(1) = "ArgumentType = Output ; ArgumentName = Result ; Name = Result " 

	' Inputs

	argStr(2) = "ArgumentType = Input ; ArgumentName = Epoch ; Name = Epoch " 

	argStr(3) = "ArgumentType = Input ; ArgumentName = fromPos ; Name = fromPosition ; RefName = Fixed " 

	argStr(4) = "ArgumentType = Input ; ArgumentName = fromVel ; Name = fromVelocity ; RefName = Inertial " 

	argStr(5) = "ArgumentType = Input ; ArgumentName = fromQuat ; Name = fromQuaternion; RefName = Fixed "

	argStr(6) = "ArgumentType = Input ; ArgumentName = fromObj ; Name = fromObjectPath " 
 
	argStr(7) = "ArgumentType = Input ; ArgumentName = toPos ; Name = toPosition ; RefName = Fixed " 

	argStr(8) = "ArgumentType = Input ; ArgumentName = toVel ; Name = toVelocity ; RefName = Inertial " 

	argStr(9) = "ArgumentType = Input ; ArgumentName = toQuat ; Name = toQuaternion; RefName = Fixed "

	argStr(10) = "ArgumentType = Input ; ArgumentName = toObj ; Name = toObjectPath " 

	' Get an input from the Vector Geometry Tool

      ReDim descripStr(8)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="ArgumentName = toEarthFromMoonInSunFixed"
	descripStr(2)="Type = Vector"
	descripStr(3)="Name = Earth"
	descripStr(4)="Source = CentralBody/Moon"
	descripStr(5)="RefSource = CentralBody/Sun"
	descripStr(6)="RefName = Fixed"
	descripStr(7)="Derivative = Yes"

	argStr(11) = descripStr

	VB_Constraint_EvalFunc_register = argStr

End Function


Function VB_Constraint_EvalFunc_compute( stateData)

	Dim outStr, len, ii,jj, tempArray

	outStr = ""

	If G_VB_Constraint_Verbose < 0 Then

		' This section will output the values of the data on the first call

		len = UBound(stateData)

		For ii = 0 To len

			If TypeName(stateData(ii)) = "Variant()" Then

				tempArray = stateData(ii)

				For jj = 0 To UBound(tempArray)

					outStr = outStr & "Val " & ii & "(" & jj & ") = " & tempArray(jj) & vbNewline

				Next

			Else

				outStr = outStr & "Val " & ii & " = " & stateData(ii) & vbNewline

			End If

		Next

		MsgBox outStr

		G_VB_Constraint_Verbose = 1

	End If


	Redim returnValue(3)

	returnValue(0) = " MESSAGE: [Info] VB_Constraint- Everything is fine;  CONTROL: OK"  'Status

	returnValue(1) = stateData(1)   'Result: return current epoch

	VB_Constraint_EvalFunc_compute = returnValue

End Function

'MsgBox AgCallFunctionOneArgReturnOne( "VB_Constraint", "GetConstraintDisplayName", "dummy" )
'MsgBox AgCallFunctionOneArgReturnOne( "VB_Constraint", "GetAccessList", "Facility" )
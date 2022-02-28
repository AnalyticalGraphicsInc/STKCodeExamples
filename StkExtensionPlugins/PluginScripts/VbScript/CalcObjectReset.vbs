
Dim VB_CalcObjectReset_compute_init
Dim VB_CalcObjectReset_Inputs
Dim VB_CalcObjectReset_Outputs

VB_CalcObjectReset_compute_init = -1

'==========================================================================
' VB_CalcObjectReset() fctn
'==========================================================================
Function VB_CalcObjectReset ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_CalcObjectReset_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_CalcObjectReset_compute_init = -1

		retVal = VB_CalcObjectReset_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_CalcObjectReset_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_CalcObjectReset = retVal

End Function

Function VB_CalcObjectReset_register()

	ReDim argStr(1)

	argStr(0) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

	VB_CalcObjectReset_register = argStr

End Function


Function VB_CalcObjectReset_compute( stateData )

	' NOTE: stateData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_CalcObjectReset_compute_init < 0 Then

		Set VB_CalcObjectReset_Inputs = g_GetPluginArrayInterface("VB_CalcObjectReset_Inputs")

		outStr = VB_CalcObjectReset_Inputs.Describe()
		
		MsgBox outStr

		VB_CalcObjectReset_compute_init = 1

	End If

	' No returned data

	outStr = ""

	outStr = outStr & "date is " & stateData(VB_CalcObjectReset_Inputs.dateStr) & vbNewline

	MsgBox outStr


End Function




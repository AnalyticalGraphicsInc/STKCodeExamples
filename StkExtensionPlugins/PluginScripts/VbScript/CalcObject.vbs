
Dim VB_CalcObject_compute_init
Dim VB_CalcObject_Inputs
Dim VB_CalcObject_Outputs

VB_CalcObject_compute_init = -1

'==========================================================================
' VB_CalcObject() fctn
'==========================================================================
Function VB_CalcObject ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_CalcObject_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_CalcObject_compute_init = -1

		retVal = VB_CalcObject_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_CalcObject_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_CalcObject = retVal

End Function

Function VB_CalcObject_register()

	ReDim argStr(3)

	argStr(0) = "ArgumentType = Output ; ArgumentName = Value ; Name = Value"

	argStr(1) = "ArgumentType = Input ; ArgumentName = Inc ; Name = Inclination ; Type = CalcObject"

	argStr(2) = "ArgumentType = Input ; ArgumentName = RightAsc ; Name = RAAN ; Type = CalcObject"

	VB_CalcObject_register = argStr

End Function


Function VB_CalcObject_compute( stateData )

	' NOTE: stateData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_CalcObject_compute_init < 0 Then

		Set VB_CalcObject_Inputs = g_GetPluginArrayInterface("VB_CalcObject_Inputs")

		outStr = VB_CalcObject_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_CalcObject_Outputs = g_GetPluginArrayInterface("VB_CalcObject_Outputs")

		outStr = VB_CalcObject_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_CalcObject_compute_init = 1

		outStr = "Inc = " & stateData(VB_CalcObject_Inputs.Inc) & ", RightAsc = "
		outStr = outStr & stateData(VB_CalcObject_Inputs.RightAsc) 

		MsgBox outStr

	End If

	Dim inc, raan

	inc = stateData(VB_CalcObject_Inputs.Inc)
	raan = stateData(VB_CalcObject_Inputs.RightAsc)

	Redim returnValue(1)

	returnValue(VB_CalcObject_Outputs.Value) = sin(inc)*sin(raan)

	VB_CalcObject_compute = returnValue

End Function



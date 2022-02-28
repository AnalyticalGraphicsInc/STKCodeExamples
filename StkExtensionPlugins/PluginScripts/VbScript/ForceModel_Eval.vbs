
Dim VB_ForceModel_Eval_compute_init
Dim VB_ForceModel_Eval_Inputs
Dim VB_ForceModel_Eval_Outputs

VB_ForceModel_Eval_compute_init = -1

'==========================================================================
' VB_ForceModel_Eval() fctn
'==========================================================================
Function VB_ForceModel_Eval ( argArray )

	Dim retVal, upper

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_ForceModel_Eval_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_ForceModel_Eval_compute_init = -1

		retVal = VB_ForceModel_Eval_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_ForceModel_Eval_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_ForceModel_Eval = retVal

End Function

Function VB_ForceModel_Eval_register()

	ReDim argStr(4)

	argStr(0) = "ArgumentType = Output ; ArgumentName = status ; Name = Status"

	argStr(1) = "ArgumentType = Output ; ArgumentName = accel ; Name = Acceleration ; RefName = CbiLVLH"

	argStr(2) = "ArgumentType = Input ; ArgumentName = Vel ; Name = Velocity ; RefName = Inertial"

	argStr(3) = "ArgumentType = Input ; ArgumentName = Date ; Name = DateUTC "

	VB_ForceModel_Eval_register = argStr

End Function


Function VB_ForceModel_Eval_compute( stateData )

	' NOTE: stateData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_ForceModel_Eval_compute_init < 0 Then

		Set VB_ForceModel_Eval_Inputs = g_GetPluginArrayInterface("VB_ForceModel_Eval_Inputs")

		outStr = VB_ForceModel_Eval_Inputs.Describe()
		
		MsgBox outStr

		Set VB_ForceModel_Eval_Outputs = g_GetPluginArrayInterface("VB_ForceModel_Eval_Outputs")

		outStr = VB_ForceModel_Eval_Outputs.Describe()
		
		MsgBox outStr

		VB_ForceModel_Eval_compute_init = 1

	End If

	Dim cbiVel, cbiSpeed, factor

	factor = 0.000001
	cbiVel = stateData(VB_ForceModel_Eval_Inputs.Vel)

	cbiSpeed = sqr(cbiVel(0)*cbiVel(0)+cbiVel(1)*cbiVel(1)+cbiVel(2)*cbiVel(2))

	Redim returnValue(2)
	Redim cartVecValue(3)

	returnValue(VB_ForceModel_Eval_Outputs.status) = "Still Okay"

	cartVecValue(0) = 0.
	cartVecValue(1) = factor*cbiSpeed
	cartVecValue(2) = 0.

	returnValue(VB_ForceModel_Eval_Outputs.accel) = cartVecValue

	VB_ForceModel_Eval_compute = returnValue

End Function


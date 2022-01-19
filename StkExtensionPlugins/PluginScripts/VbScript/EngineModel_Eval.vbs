
Dim VB_EngineModel_Eval_compute_init
Dim VB_EngineModel_Eval_Inputs
Dim VB_EngineModel_Eval_Outputs
Dim VB_EngineModel_Eval_Count
Dim VB_EngineModel_Eval_OutputCount

VB_EngineModel_Eval_compute_init = -1

VB_EngineModel_Eval_OutputCount = 4000

'
' NOTE: the variable VB_EngineModel_SegStart_refEpoch is assumed to be defined.
'		It should be defined by the VB_EngineModel_SegStart script which should
'		be used as the SegStart Function for this engine model that uses this Eval function
'

'==========================================================================
' VB_EngineModel_Eval() fctn
'==========================================================================
Function VB_EngineModel_Eval ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_EngineModel_Eval_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_EngineModel_Eval_compute_init = -1

		retVal = VB_EngineModel_Eval_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_EngineModel_Eval_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If


	VB_EngineModel_Eval = retVal

End Function

Function VB_EngineModel_Eval_register()

	ReDim argStr(6)

	argStr(0) = "ArgumentType = Output; Name = Status; ArgumentName = status"

	argStr(1) = "ArgumentType = Output; Name = Thrust; ArgumentName = thrust"

	argStr(2) = "ArgumentType = Output; Name = MassFlowRate; ArgumentName = flowrate"

	argStr(3) = "ArgumentType = Input; Name = Epoch; ArgumentName = Epoch"

	argStr(4) =  "ArgumentType = Input; Type = CalcObject; Name = Inclination; ArgumentName = Incl"

	argStr(5) = "ArgumentType = Input; Name = FuelMass; ArgumentName = fuel"

	VB_EngineModel_Eval_register = argStr

End Function


Function VB_EngineModel_Eval_compute( stateData )

	Dim outStr

	outStr = ""

	If VB_EngineModel_Eval_compute_init < 0 Then

		Set VB_EngineModel_Eval_Inputs = g_GetPluginArrayInterface("VB_EngineModel_Eval_Inputs")

		outStr = VB_EngineModel_Eval_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_EngineModel_Eval_Outputs = g_GetPluginArrayInterface("VB_EngineModel_Eval_Outputs")

		outStr = VB_EngineModel_Eval_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_EngineModel_Eval_compute_init = 1

		VB_EngineModel_Eval_refEpoch =  stateData(VB_EngineModel_Eval_Inputs.Epoch)

		VB_EngineModel_Eval_Count = VB_EngineModel_Eval_OutputCount

	End If

	Dim epoch, deltaT, msg

	msg = "Okay"

	epoch = stateData(VB_EngineModel_Eval_Inputs.Epoch)

	If Not VarType(VB_EngineModel_SegStart_refEpoch) = vbEmpty Then

		deltaT = epoch - VB_EngineModel_SegStart_refEpoch 
	
	Else

		deltaT = epoch
		msg = "ERROR"

	End If

	Redim returnValue(3)
	
	returnValue(VB_EngineModel_Eval_Outputs.status) = msg

	returnValue(VB_EngineModel_Eval_Outputs.thrust) = 0.0003*deltaT	' thrust increases with time!

	returnValue(VB_EngineModel_Eval_Outputs.flowrate) = 0.001		' constant flow rate

	' The code below outputs the values every so often

	VB_EngineModel_Eval_Count = 	VB_EngineModel_Eval_Count + 1

	If VB_EngineModel_Eval_Count > VB_EngineModel_Eval_OutputCount Then

		outStr = ""
		outStr = outStr & "Epoch " & epoch & vbNewline
		outStr = outStr & "Fuel " & stateData(VB_EngineModel_Eval_Inputs.fuel) & vbNewline
		outStr = outStr & "Thrust " & returnValue(VB_EngineModel_Eval_Outputs.thrust) & vbNewline

		displayDialog outStr , 800

		VB_EngineModel_Eval_Count = 0 
	End If

	VB_EngineModel_Eval_compute = returnValue

End Function


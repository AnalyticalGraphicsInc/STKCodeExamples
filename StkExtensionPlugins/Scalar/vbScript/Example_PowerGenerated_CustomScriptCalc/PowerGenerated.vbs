
Dim PowerGenerated_compute_init
Dim PowerGenerated_Inputs
Dim PowerGenerated_Outputs

PositiveDataStorage_compute_init = -1

'==========================================================================
' PositiveDataStorage() fctn
'==========================================================================
Function PowerGenerated(argArray)

	Dim retVal
	If IsEmpty(argArray(0)) Then
		retVal = PowerGenerated_compute( argArray )
	ElseIf argArray(0) = "register" Then
		PowerGenerated_compute_init = -1
		retVal = PowerGenerated_register()
	ElseIf argArray(0) = "compute" Then
		retVal = PowerGenerated_compute( argArray )
	Else
		retVal = Empty
	End If
	PowerGenerated = retVal

End Function

Function PowerGenerated_register()

	ReDim argStr(2)
	argStr(0) = "ArgumentType = Output ; Name = Scalar ; ArgumentName = value"
	argStr(1) = "ArgumentType = Input ; Type = Scalar ; Source = Satellite/Satellite1 ; Name = SunIncidence ; ArgumentName = SunIncidence"
	PowerGenerated_register = argStr

End Function

Function PowerGenerated_compute(stateData)

	If PowerGenerated_compute_init < 0 Then
		Set PowerGenerated_Inputs = g_GetPluginArrayInterface("PowerGenerated_Inputs")
		Set PowerGenerated_Outputs = g_GetPluginArrayInterface("PowerGenerated_Outputs")
		PowerGenerated_compute_init = 1
	End If

	Redim returnValue(1)
	returnValue(PowerGenerated_Outputs.value) = CustomScalar(stateData(PowerGenerated_Inputs.SunIncidence))
	PowerGenerated_compute = returnValue

End Function

Function CustomScalar(SunIncidence)
	If SunIncidence < (90*3.1415/180) Then
		CustomScalar = 1380*cos(SunIncidence)
	Else
		CustomScalar = 0
	End If
End Function

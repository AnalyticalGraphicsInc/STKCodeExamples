
Dim VB_EngineModel_SegStart_refEpoch

VB_EngineModel_SegStart_refEpoch = 0

'==========================================================================
' VB_EngineModel_SegStart() fctn
'==========================================================================
Function VB_EngineModel_SegStart( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_EngineModel_SegStart_compute( argArray )

	ElseIf argArray(0) = "register" Then

		retVal = VB_EngineModel_SegStart_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_EngineModel_SegStart_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_EngineModel_SegStart = retVal

End Function

Function VB_EngineModel_SegStart_register()

	ReDim retVal(1)

	retVal(0) = "ArgumentType = Input; Name = Epoch; ArgumentName = Epoch"

	VB_EngineModel_SegStart_register = retVal

End Function

Function VB_EngineModel_SegStart_compute(argArray)

	Dim outStr

	' no outputs - just remembers the refEpoch 

	VB_EngineModel_SegStart_refEpoch = argArray(1)

	outStr = "VB_EngineModel_SegStart called." & vbNewline
	outStr = outStr & "RefEpoch = " & VB_EngineModel_SegStart_refEpoch & vbNewline

	MsgBox outStr

End Function
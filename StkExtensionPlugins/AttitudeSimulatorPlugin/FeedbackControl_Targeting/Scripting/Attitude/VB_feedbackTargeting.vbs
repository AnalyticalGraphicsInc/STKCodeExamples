Dim VB_feedbackTargeting_init
Dim VB_feedbackTargeting_Inputs
Dim VB_feedbackTargeting_Outputs

'==========================================================================
' VB_feedbackTargeting() fctn
'==========================================================================
Function VB_feedbackTargeting ( argArray )
	Dim retVal

	If IsEmpty(argArray(0)) Then
		' do compute
		retVal = VB_feedbackTargeting_compute( argArray )
	ElseIf argArray(0) = "register" Then
		'do register 
		VB_feedbackTargeting_init = -1
		retVal = VB_feedbackTargeting_register()
	ElseIf argArray(0) = "compute" Then
		' do compute
		retVal = VB_feedbackTargeting_compute( argArray )
	Else
		' bad call
		retVal = Empty
	End If

	VB_feedbackTargeting = retVal
End Function

'REGISTER SECTION
Function VB_feedbackTargeting_register()

	ReDim argStr(5)

	' Outputs
	argStr(0) = "ArgumentType = Output ; Type = Parameter ; ArgumentName = Torque ; Name = Torque ; BasicType = Vector "

	' Inputs
	argStr(1) = "ArgumentType = Input ; ArgumentName = time ; Name = Epoch "
	argStr(2) = "ArgumentType = Input ; ArgumentName = att ; Type = Attitude ; Derivative = Yes "
	argStr(3) = "ArgumentType = Input ; ArgumentName = erratt ; Type = Attitude ; RefName = Body ; RefSource = Satellite/PerfectPointing "
	argStr(4) = "ArgumentType = Input ; ArgumentName = IMtx ; Type = Inertia ; Name = Inertia "
				   
	VB_feedbackTargeting_register = argStr

End Function

'COMPUTE SECTION
Function VB_feedbackTargeting_compute( stateData )

	If VB_feedbackTargeting_init < 0 Then

		' get interface classes if register was called
		Set VB_feedbackTargeting_Inputs = g_GetPluginArrayInterface("VB_feedbackTargeting_Inputs")
		Set VB_feedbackTargeting_Outputs = g_GetPluginArrayInterface("VB_feedbackTargeting_Outputs")

		VB_feedbackTargeting_init = 1

	End If
	
	' get input values
	Dim att, erratt, IMtx

	att = stateData(VB_feedbackTargeting_Inputs.att)
	erratt = stateData(VB_feedbackTargeting_Inputs.erratt)
	IMtx = stateData(VB_feedbackTargeting_Inputs.IMtx) 'oddly, this is returned as a 9x1 array instead of a 3x3

	' create return array of correct size
	Redim returnValue(1)

	' vec that is returned is an array of 3 doubles: allocate it
	Redim torque(3)

	'create the gain values
	Dim k, c
	k = 2.0
	c = 0.8
    
	'Apply a feedbackTargeting control law, vbscript does not make this easy...
	Redim temp(3)
	temp(0) = ( k * erratt(0) * erratt(3) ) + ( c * att(4) )
	temp(1) = ( k * erratt(1) * erratt(3) ) + ( c * att(5) )
	temp(2) = ( k * erratt(2) * erratt(3) ) + ( c * att(6) )

	torque(0) = -1 * IMtx(0) * temp(0) + -1 * IMtx(3) * temp(1) + -1 * IMtx(6) * temp(2)
	torque(1) = -1 * IMtx(1) * temp(0) + -1 * IMtx(4) * temp(1) + -1 * IMtx(7) * temp(2)
	torque(2) = -1 * IMtx(2) * temp(0) + -1 * IMtx(5) * temp(1) + -1 * IMtx(8) * temp(2)	
	
	returnValue(VB_feedbackTargeting_Outputs.Torque) = torque
	VB_feedbackTargeting_compute = returnValue

End Function


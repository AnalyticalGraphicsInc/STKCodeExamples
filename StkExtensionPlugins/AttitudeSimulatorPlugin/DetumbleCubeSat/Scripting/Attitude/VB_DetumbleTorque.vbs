Dim VB_DetumbleTorque_init
Dim VB_DetumbleTorque_Inputs
Dim VB_DetumbleTorque_Outputs

'==========================================================================
' VB_DetumbleTorque() fctn
'==========================================================================
Function VB_DetumbleTorque ( argArray )
	Dim retVal

	If IsEmpty(argArray(0)) Then
		' do compute
		retVal = VB_DetumbleTorque_compute( argArray )
	ElseIf argArray(0) = "register" Then
		'do register 
		VB_DetumbleTorque_init = -1
		retVal = VB_DetumbleTorque_register()
	ElseIf argArray(0) = "compute" Then
		' do compute
		retVal = VB_DetumbleTorque_compute( argArray )
	Else
		' bad call
		retVal = Empty
	End If

	VB_DetumbleTorque = retVal
End Function

'REGISTER SECTION
Function VB_DetumbleTorque_register()

	ReDim argStr(3)

	' Outputs
	argStr(0) = "ArgumentType = Output ; Type = Parameter ; ArgumentName = Torque ; Name = Torque ; BasicType = Vector "

	' Inputs
	argStr(1) = "ArgumentType = Input ; ArgumentName = time ; Name = Epoch "
	argStr(2) = "ArgumentType = Input ; ArgumentName = MagFieldIGRF ; Name = MagField(IGRF) ; Type = Vector ; RefType = Attitude ; Derivative = Yes "
                   
	VB_DetumbleTorque_register = argStr

End Function

'COMPUTE SECTION
Function VB_DetumbleTorque_compute( stateData )

	If VB_DetumbleTorque_init < 0 Then

		' get interface classes if register was called
		Set VB_DetumbleTorque_Inputs = g_GetPluginArrayInterface("VB_DetumbleTorque_Inputs")
		Set VB_DetumbleTorque_Outputs = g_GetPluginArrayInterface("VB_DetumbleTorque_Outputs")

		VB_DetumbleTorque_init = 1

	End If

	' get input values
	Dim epoch, magFieldVec

	epoch = stateData(VB_DetumbleTorque_Inputs.time)
	magFieldVec = stateData(VB_DetumbleTorque_Inputs.MagFieldIGRF)

	' create return array of correct size
	Redim returnValue(1)

	' vec that is returned is an array of 3 doubles: allocate it
	Redim torque(3)
	
	' create the magField vector, magField derivative vector, and cross product vector
	Redim magField(3)
	Redim magFieldDot(3)
	Dim crossProduct(3)
	
	magField(0) = magFieldVec(0)
	magField(1) = magFieldVec(1)
	magField(2) = magFieldVec(2)
	magFieldDot(0) = magFieldVec(3)
	magFieldDot(1) = magFieldVec(4)
	magFieldDot(2) = magFieldVec(5)
        
	' cross product formula... a = b x c , MagDotCrossMag = magDot x mag
	crossProduct(0) = magFieldDot(1) * magField(2) - magFieldDot(2) * magField(1) ' ax = by*cz - bz*cy
	crossProduct(1) = magFieldDot(2) * magField(0) - magFieldDot(0) * magField(2) ' ay = bz*cx - bx*cz
	crossProduct(2) = magFieldDot(0) * magField(1) - magFieldDot(1) * magField(0) ' az = bx*cy - by*cx
    
	' apply some negative value to counter the change relative to the magnetic field 
	torque(0) = -4000 * crossProduct(0)
	torque(1) = -4000 * crossProduct(1)
	torque(2) = -4000 * crossProduct(2)
	
	returnValue(VB_DetumbleTorque_Outputs.Torque) = torque
	VB_DetumbleTorque_compute = returnValue

End Function


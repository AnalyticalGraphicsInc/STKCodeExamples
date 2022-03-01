
Dim VB_CustomVector_init
Dim VB_CustomVector_Inputs
Dim VB_CustomVector_Outputs

'==========================================================================
' VB_CustomVector() fctn
'==========================================================================
Function VB_CustomVector ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_CustomVector_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_CustomVector_init = -1

		retVal = VB_CustomVector_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_CustomVector_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If


	VB_CustomVector = retVal

End Function

Function VB_CustomVector_register()

	ReDim argStr(7)

	' Outputs

	argStr(0) = "ArgumentType = Output ; ArgumentName = vec ; Name = Vector "

	' Inputs

	argStr(1) = "ArgumentType = Input ; ArgumentName = time ; Name = Epoch "

	argStr(2) = "ArgumentType = Input ; ArgumentName = apoVec ; Name = Apoapsis ; "
	argStr(2) = argStr(2) & "Type = Vector ; RefName = Body"

	Redim descripStr(5)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="ArgumentName = bodyAxes"
	descripStr(2)="Name = Body"
	descripStr(3)="Type = Axes"
	descripStr(4)="RefName = TopoCentric"

	argStr(3) = descripStr
	
	Redim descripStr(4)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="ArgumentName = sunMoonAngle"
	descripStr(2)="Name = SunMoon"
	descripStr(3)="Type = Angle"

	argStr(4) = descripStr

    	Redim descripStr(7)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = Center"
	descripStr(2)="ArgumentName = moonPnt"
	descripStr(3)="Type = Point"
	descripStr(4)="Source = CentralBody/Moon"
	descripStr(5)="RefName = Inertial"
	descripStr(6)="RefSource  = CentralBody/Sun"

	argStr(5) = descripStr

	argStr(6) = "ArgumentType = Input ; ArgumentName = bodySys ; Name = Body ; Type = CrdnSystem ; "
	argStr(6) = argStr(6) & "RefName = Fixed ; RefSource  = CentralBody/Earth "

	VB_CustomVector_register = argStr

End Function


Function VB_CustomVector_compute( stateData )

	Dim outStr

	outStr = ""

	If VB_CustomVector_init < 0 Then

		' get interface classes if register was called

		Set VB_CustomVector_Inputs = g_GetPluginArrayInterface("VB_CustomVector_Inputs")

		' Output a MsgBox of the inputs/outputs

		outStr = VB_CustomVector_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_CustomVector_Outputs = g_GetPluginArrayInterface("VB_CustomVector_Outputs")

		' Output a MsgBox of the inputs/outputs

		outStr = VB_CustomVector_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_CustomVector_init = 1

	End If

	' get input values

	Dim epoch, apo, bAxes, smAngle, mnPnt, bSys

	epoch = stateData(VB_CustomVector_Inputs.time)
	apo = stateData(VB_CustomVector_Inputs.apoVec)
	bAxes = stateData(VB_CustomVector_Inputs.bodyAxes)
	smAngle = stateData(VB_CustomVector_Inputs.sunMoonAngle)
	mnPnt = stateData(VB_CustomVector_Inputs.moonPnt)
	bSys = stateData(VB_CustomVector_Inputs.bodySys)

	' Output values first time thru

	If VB_CustomVector_init = 1 Then

		VB_CustomVector_init = 10

		outStr = "time = " & epoch & vbNewline
		outStr = outStr & "apoVec = " & apo(0) & ", " & apo(1) & ", " & apo(2) & vbNewline
		outStr = outStr & "bodyAxes = " & bAxes(0) & ", " & bAxes(1) & ", " & bAxes(2) 
		outStr = outStr & ", " & bAxes(3) & vbNewline
		outStr = outStr & "sunMoonAngle = " & smAngle & vbNewline
		outStr = outStr & "moonPnt = " & mnPnt(0) & ", " & mnPnt(1) & ", " & mnPnt(2) & vbNewline
		outStr = outStr & "bodySys(vec) = " & bSys(0) & ", " & bSys(1) & ", " & bSys(2) & vbNewline
		outStr = outStr & "bodySys(quat) = " & bSys(3) & ", " & bSys(4) & ", " & bSys(5) 
		outStr = outStr & ", " & bSys(6) & vbNewline

		displayDialog outStr , 800

	End If

	' create return array of correct size
	Redim returnValue(1)

	' vec that is returned is an array of 3 doubles: allocate it
	Redim vector(3)

	' set output values

	vector(0) = apo(0)
	vector(1) = apo(1)
	vector(2) = apo(2)

	returnValue(VB_CustomVector_Outputs.vec) = vector

	VB_CustomVector_compute = returnValue

End Function


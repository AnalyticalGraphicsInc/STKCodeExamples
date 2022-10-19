
Dim VB_RadarCrossSection_globalVar
Dim VB_RadarCrossSection_Inputs
Dim VB_RadarCrossSection_Outputs

'==========================================================================
' VB_RadarCrossSection() fctn
'==========================================================================
Function VB_RadarCrossSection ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_RadarCrossSection_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_RadarCrossSection_globalVar = -1

		retVal = VB_RadarCrossSection_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_RadarCrossSection_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_RadarCrossSection = retVal

End Function

Function VB_RadarCrossSection_register()

'		Output Parameters
	ReDim descripStr(3), argStr(18)

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixReal00"
	descripStr(2)="ArgumentName = RCSMatrixReal00"
	argStr(0) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixImg00"
	descripStr(2)="ArgumentName = RCSMatrixImg00"
	argStr(1) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixReal01"
	descripStr(2)="ArgumentName = RCSMatrixReal01"
	argStr(2) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixImg01"
	descripStr(2)="ArgumentName = RCSMatrixImg01"
	argStr(3) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixReal10"
	descripStr(2)="ArgumentName = RCSMatrixReal10"
	argStr(4) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixImg10"
	descripStr(2)="ArgumentName = RCSMatrixImg10"
	argStr(5) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixReal11"
	descripStr(2)="ArgumentName = RCSMatrixReal11"
	argStr(6) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = RCSMatrixImg11"
	descripStr(2)="ArgumentName = RCSMatrixImg11"
	argStr(7) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = ScatterMatrixBasis"
	descripStr(2)="ArgumentName = ScatterMatrixBasis"
	argStr(8) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = IsDynamic"
	descripStr(2)="ArgumentName = IsDynamic"
	argStr(9) = descripStr
	
'		Input Parameters
	ReDim descripStr(4)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = EpochSec"
	descripStr(2)="ArgumentName = EpochSec"
	descripStr(3)="Type = Value"
	argStr(10) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = Frequency"
	descripStr(2)="ArgumentName = Frequency"
	descripStr(3)="Type = Value"
	argStr(11) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = IncidentRho"
	descripStr(2)="ArgumentName = IncidentRho"
	descripStr(3)="Type = Value"
	argStr(12) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = IncidentTheta"
	descripStr(2)="ArgumentName = IncidentTheta"
	descripStr(3)="Type = Value"
	argStr(13) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = ReflectedRho"
	descripStr(2)="ArgumentName = ReflectedRho"
	descripStr(3)="Type = Value"
	argStr(14) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = ReflectedTheta"
	descripStr(2)="ArgumentName = ReflectedTheta"
	descripStr(3)="Type = Value"
	argStr(15) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = IncidentBodyFixedVector"
	descripStr(2)="ArgumentName = IncidentBodyFixedVector"
	descripStr(3)="Type = Value"
	argStr(16) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = ReflectedBodyFixedVector"
	descripStr(2)="ArgumentName = ReflectedBodyFixedVector"
	descripStr(3)="Type = Value"
	argStr(17) = descripStr

	VB_RadarCrossSection_register = argStr

End Function


Function VB_RadarCrossSection_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_RadarCrossSection_globalVar < 0 Then

		Set VB_RadarCrossSection_Inputs = g_GetPluginArrayInterface("VB_RadarCrossSection_Inputs")

		outStr = VB_RadarCrossSection_Inputs.Describe()
		
		displayDialog outStr , 800

		Set VB_RadarCrossSection_Outputs = g_GetPluginArrayInterface("VB_RadarCrossSection_Outputs")

		outStr = VB_RadarCrossSection_Outputs.Describe()
		
		displayDialog outStr , 800

		VB_RadarCrossSection_globalVar = 1

	End If

'   EXAMPLE Model for testing script only
'   Model parameters for Comm Trasnmitter 
'	.
'	Note: This example is for fixed parameters, but these can vary at each time step
'
'   USER MODEL AREA
	Redim returnValue(9)

'	PLEASE DO NOT CHANGE ANYTHING ABOVE THIS LINE

	Dim epSec, freq, incRho, incTheta, refRho, refTheta, incBodyVec, refBodyVec
	
	epSec = inputData(VB_RadarCrossSection_Inputs.EpochSec)
	freq = inputData(VB_RadarCrossSection_Inputs.Frequency)
	incRho = inputData(VB_RadarCrossSection_Inputs.IncidentRho)
	incTheta = inputData(VB_RadarCrossSection_Inputs.IncidentTheta)
	refRho = inputData(VB_RadarCrossSection_Inputs.ReflectedRho)
	refTheta = inputData(VB_RadarCrossSection_Inputs.ReflectedTheta)
	incBodyVec = inputData(VB_RadarCrossSection_Inputs.IncidentBodyFixedVector)
	refBodyVec = inputData(VB_RadarCrossSection_Inputs.ReflectedBodyFixedVector)


	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixReal00)    = 0.0
	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixImg00)     = 0.0
	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixReal01)    = 0.0
	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixImg01)     = -1.0
	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixReal10)    = 1.0
	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixImg10)     = 0.0
	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixReal11)    = 0.0
	returnValue(VB_RadarCrossSection_Outputs.RCSMatrixImg11)     = 0.0
	returnValue(VB_RadarCrossSection_Outputs.ScatterMatrixBasis) = 0
	returnValue(VB_RadarCrossSection_Outputs.IsDynamic)          = 0


'   END OF USER MODEL AREA

	VB_RadarCrossSection_compute = returnValue

End Function



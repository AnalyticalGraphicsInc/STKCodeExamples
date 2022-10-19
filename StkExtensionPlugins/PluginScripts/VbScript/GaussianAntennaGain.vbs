'#########################################################################################
' SAPMLE FOR PERL BASED CUSTOM ANTENNA GAIN PLUGIN SCRIPT PROVIDED BY THE USER
' PLEASE ADD YOUR MODEL IN THE USER ANTENNA GAIN MODEL AREA BELOW.
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
'#########################################################################################

' VB_GaussianAntennaGain

Dim VB_GaussianAntennaGain_globalVar
Dim VB_GaussianAntennaGain_Inputs
Dim VB_GaussianAntennaGain_Outputs

'==========================================================================
' VB_GaussianAntennaGain() fctn
'==========================================================================
Function VB_GaussianAntennaGain ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_GaussianAntennaGain_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_GaussianAntennaGain_globalVar = -1

		retVal = VB_GaussianAntennaGain_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_GaussianAntennaGain_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_GaussianAntennaGain = retVal

End Function

Function VB_GaussianAntennaGain_register()

	ReDim descripStr(3), argStr(13)

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = AntennaGain"
	descripStr(2)="ArgumentName = AntennaGain"
	argStr(0) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = Beamwidth"
	descripStr(2)="ArgumentName = Beamwidth"
	argStr(1) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = AntennaMaxGain"
	descripStr(2)="ArgumentName = AntennaMaxGain"
	argStr(2) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = IntegratedGain"
	descripStr(2)="ArgumentName = IntegratedGain"
	argStr(3) = descripStr

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = AntennaCoordSystem"
	descripStr(2)="ArgumentName = AntennaCoordSystem"
	argStr(4) = descripStr
	
	ReDim descripStr(4)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = DateUTC"
	descripStr(2)="ArgumentName = DateUTC"
	descripStr(3)="Type = Value"
	argStr(5) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = CbName"
	descripStr(2)="ArgumentName = CbName"
	descripStr(3)="Type = Value"
	argStr(6) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = Frequency"
	descripStr(2)="ArgumentName = Frequency"
	descripStr(3)="Type = Value"
	argStr(7) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = AzimuthAngle"
	descripStr(2)="ArgumentName = AzimuthAngle"
	descripStr(3)="Type = Value"
	argStr(8) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = ElevationAngle"
	descripStr(2)="ArgumentName = ElevationAngle"
	descripStr(3)="Type = Value"
	argStr(9) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = AntennaPosLLA"
	descripStr(2)="ArgumentName = AntennaPosLLA"
	descripStr(3)="Type = Value"
	argStr(10) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = AntennaCoordSystem"
	descripStr(2)="ArgumentName = AntennaCoordSystem"
	descripStr(3)="Type = Value"
	argStr(11) = descripStr


  	argStr(12) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

	VB_GaussianAntennaGain_register = argStr

End Function


Function VB_GaussianAntennaGain_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_GaussianAntennaGain_globalVar < 0 Then

		Set VB_GaussianAntennaGain_Inputs = g_GetPluginArrayInterface("VB_GaussianAntennaGain_Inputs")

		outStr = VB_GaussianAntennaGain_Inputs.Describe()
		
		'displayDialog outStr , 800

		Set VB_GaussianAntennaGain_Outputs = g_GetPluginArrayInterface("VB_GaussianAntennaGain_Outputs")

		outStr = VB_GaussianAntennaGain_Outputs.Describe()
		
		'displayDialog outStr , 800

		VB_GaussianAntennaGain_globalVar = 1

		'MsgBox inputData(VB_GaussianAntennaGain_Inputs.dateStr)

	End If

	'############################################################################################
	' USER ANTENNA GAIN MODEL AREA.
	' PLEASE REPLACE THE CODE BELOW WITH YOUR ANTENNA GAIN COMPUTATION MODEL
	'############################################################################################
	' NOTE: the outputs that are returned MUST be in the same order as registered

	' AntennaGain (dB), gain of the antenna at time and in the Azi-Elev direction off the boresight.
	' Beamwidth (Rad) is the 3-dB beamwith of the antenna.
	' AntennaMaxGain (dB) is the maximum ( possibly boresight gain of the antenna)
	' IntegratedGain of the antenna (range 0-1) used for antenna Noise computation.
	
	'   Test Model Gaussian Antenna
	'	Script inputs
	'		Date, Time					string
	'		Central Body Name			string
	'		Frequency(Hz)				double
	'		Azimuth Angle(Rad)			double
	'		Elevation Angle(Rad)		double
	'		AntennaPosLLA(Rad,Rad,m)	double(3)
	'
	Redim returnValue(4)
	Dim x, thetab, gmax, gain, expParm

	Dim lambda, eff, dia, freq, el, az
	Dim antPos, antPosLat, antPosLon, antPosAlt

	eff = 0.55
	dia = 1.0
	freq = inputData(VB_GaussianAntennaGain_Inputs.Frequency)
	el   = inputData(VB_GaussianAntennaGain_Inputs.ElevationAngle)
	az   = inputData(VB_GaussianAntennaGain_Inputs.AzimuthAngle)

	antPos    = inputData(VB_GaussianAntennaGain_Inputs.AntennaPosLLA)
	antPosLat = antPos(0)
	antPosLon = antPos(1)
	antPosAlt = antPos(2)

	lambda = 299792458.0 / freq
	thetab = lambda / (dia * Sqr(eff))
	x = 3.141592 * dia / lambda
	x = x * x
	gmax = eff * x
	expParm = -2.76 * el * el / (thetab * thetab)
	If expParm < -700 Then
	   expParm = -700
	End If
	gain = gmax * Exp(expParm)

	returnValue(VB_GaussianAntennaGain_Outputs.AntennaGain)     = 10.0*Log(gain)/Log(10.0)
	returnValue(VB_GaussianAntennaGain_Outputs.Beamwidth)       = thetab
	returnValue(VB_GaussianAntennaGain_Outputs.AntennaMaxGain)  = 10.0*Log(gmax)/Log(10.0)
	returnValue(VB_GaussianAntennaGain_Outputs.IntegratedGain)  = 0.5
	
	'AntennaCoordSystem return 0 for Polar and 1 for Rectangular
	returnValue(VB_GaussianAntennaGain_Outputs.AntennaCoordSystem)  = 0

	'############################################################################################
	' END OF USER MODEL AREA	
	'############################################################################################

	VB_GaussianAntennaGain_compute = returnValue

End Function

'#########################################################################################
' SAMPLE STK ANTENNA GAIN PLUGIN TO MODEL PHASED ARRAY GAIN (WRITTEN IN VBSCRIPT)
' TO MODIFY/REPLACE THE SIMPLE COSINE LOSS MODEL, EDIT CODE IN THE -USER GAIN MODEL AREA-
' DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
'#########################################################################################

' VB_PhasedArrayAntGain

Dim VB_PhasedArrayAntGain_compute_init
Dim VB_PhasedArrayAntGain_globalVar
Dim VB_PhasedArrayAntGain_Inputs
Dim VB_PhasedArrayAntGain_Outputs

VB_PhasedArrayAntGain_compute_init = -1
'==========================================================================
' VB_PhasedArrayAntGain() fctn
'==========================================================================
Function VB_PhasedArrayAntGain ( argArray )

	Dim retVal

	If IsEmpty(argArray(0)) Then

		' do compute

		retVal = VB_PhasedArrayAntGain_compute( argArray )

	ElseIf argArray(0) = "register" Then

		VB_PhasedArrayAntGain_globalVar = -1
		VB_PhasedArrayAntGain_compute_init = -1

		retVal = VB_PhasedArrayAntGain_register()

	ElseIf argArray(0) = "compute" Then

		' do compute

		retVal = VB_PhasedArrayAntGain_compute( argArray )

	Else

		' bad call

		retVal = Empty

	End If

	VB_PhasedArrayAntGain = retVal

End Function

Function VB_PhasedArrayAntGain_register()

'******************************************************************
'******************************************************************
'************************Output Parameters*************************
'******************************************************************
'******************************************************************

	' Note: these output parameters are defined by the STK antenna gain plugin interface

	ReDim descripStr(4), argStr(15)

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

	descripStr(0)="ArgumentType = Output"
	descripStr(1)="Name = DynamicGain"
	descripStr(2)="ArgumentName = DynamicGain"
	argStr(5) = descripStr

'******************************************************************
'******************************************************************
'*************************Input Parameters*************************
'******************************************************************
'******************************************************************

	' Note: these input parameters are defined by the STK antenna gain plugin interface

	ReDim descripStr(4)

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = DateUTC"
	descripStr(2)="ArgumentName = DateUTC"
	descripStr(3)="Type = Value"
	argStr(6) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = CbName"
	descripStr(2)="ArgumentName = CbName"
	descripStr(3)="Type = Value"
	argStr(7) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = Frequency"
	descripStr(2)="ArgumentName = Frequency"
	descripStr(3)="Type = Value"
	argStr(8) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = AzimuthAngle"
	descripStr(2)="ArgumentName = AzimuthAngle"
	descripStr(3)="Type = Value"
	argStr(9) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = ElevationAngle"
	descripStr(2)="ArgumentName = ElevationAngle"
	descripStr(3)="Type = Value"
	argStr(10) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = AntennaPosLLA"
	descripStr(2)="ArgumentName = AntennaPosLLA"
	descripStr(3)="Type = Value"
	argStr(11) = descripStr

	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = AntennaCoordSystem"
	descripStr(2)="ArgumentName = AntennaCoordSystem"
	descripStr(3)="Type = Value"
	argStr(12) = descripStr
	
	descripStr(0)="ArgumentType = Input"
	descripStr(1)="Name = EpochSec"
	descripStr(2)="ArgumentName = EpochSec"
	descripStr(3)="Type = Value"
	argStr(13) = descripStr


  	argStr(14) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

	VB_PhasedArrayAntGain_register = argStr

End Function


Function VB_PhasedArrayAntGain_compute( inputData )

	' NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

	Dim outStr

	outStr = ""

	If VB_PhasedArrayAntGain_globalVar < 0 Then

		Set VB_PhasedArrayAntGain_Inputs = g_GetPluginArrayInterface("VB_PhasedArrayAntGain_Inputs")

		outStr = VB_PhasedArrayAntGain_Inputs.Describe()
		
		'displayDialog outStr , 800

		Set VB_PhasedArrayAntGain_Outputs = g_GetPluginArrayInterface("VB_PhasedArrayAntGain_Outputs")

		outStr = VB_PhasedArrayAntGain_Outputs.Describe()
		
		'displayDialog outStr , 800

		VB_PhasedArrayAntGain_globalVar = 1

		'MsgBox inputData(VB_PhasedArrayAntGain_Inputs.dateStr)

	End If
	
'******************************************************************
'******************************************************************
'************************Algorithm*********************************
'******************************************************************
'******************************************************************

	'############################################################################################
	' USER GAIN MODEL AREA -- START
	'############################################################################################
	'
	' TO MODIFY/REPLACE THE SIMPLE COSINE LOSS MODEL, EDIT CODE IN THIS SECTION OF THE SCRIPT!!!

	' DESCRIPTION OF SIMPLE COSINE LOSS MODEL
	' ----------------------------------------
	'
	' Overview:
	' Simple approximation of mainbeam line-of-sight gain assuming ideal cosine scan loss behavior
	' over a limited scan range (e.g. 60 degrees). Estimates first-order gain reduction from 
	' distortion and spreading introduced as mainbeam is scanned away from the array's normal 
	' vector. The model is intended as a simple symmetrical planar phased array model for link
	' budgets and radar analysis.
	'
	' Applicability:
	' As shipped, this plugin script is intended for use in communication link and radar anlaysis
	' invloving two rf objects with a constant system noise temperature.  This plugin as shipped 
	' is NOT appropriate for use with a calculated system temperature or in an STK CommSystem 
	' analysis with constellations of tranmitters, receivers and interferers. This is because 
	' integrated gain, non-symmetrical arrays, mainbeam shape, sidelobe structure, adaptive nulling, and
	' other gain pattern details are not modeled. Nor will this plug-in provide dynamic gain graphics
	' in the STK 2-D or 3-D windows. If such capabilities are of interest to you, please ask AGI
	' Support about additional phased array modeling options for STK.  You may contact us
	' by phone at 1.800.924.7244 or via email at support@agi.com.
	'
	' Assumptions:
	' 1) Planar array of ideal elements --> gain reduction proportional to projected aperture area
	' 2) Half-wavelength or less element spacing --> minimal gain reduction from grating lobes
	' 3) Symmetrical element layout --> equivalent scan loss behavior over all line-of-site azimuths
	' 4) Scan limited --> beyond a certain scan angle (e.g. 60 deg), no useful gain provided
	'
	' Mathematics:
	' A maximum gain value representing the broadside on-axis mainbeam (i.e. line-of-sight along 
	' array's normal vector) is reduced by multipliying by the cosine of the scan angle, where 
	' scan angle is defined as the angle between the array normal vector and the line-of-sight 
	' to the intended communication node or radar target. 
	'
	' The user is presented two options to model the maximum gain:
	' 1) a user-specified hardcoded value
	' 2) a calculation based on user-specified circular aperture area, aperture efficiency, and
	' operating frequency. The user selects the model they wish to use by setting the gainModel
	' script parameter.
	' As shipped, the script is set to use a hardcoded maximum gain of 41 dB.
	' A placeholder is also provided for the user to introduce their own maximum gain calculation.
	'
	' END DESCRIPTION
	'

	'Overview of inputs and variable types provided by the STK antenna gain plug-in interface
	'   Test Model Phased Array Antenna
	'	Script inputs
	'		Date, Time				string
	'		Central Body Name			string
	'		Frequency(Hz)				double
	'		Azimuth Angle(Rad)			double
	'		Elevation Angle(Rad)			double
	'		AntennaPosLLA(Deg,Deg,m)		double(3)
	'		AntennaCoordSystem			integer 
	'							(0: az-el-polar,
	'							 1: az-el-rectangular,
	'							 2: az-el-other)
	'		EpochSec (seconds)			double
	'
	
	'Declaring and Initializing inputs, constants and other variables
	
	Redim returnValue(6)
	Dim Pi, date, cb, freq
	Dim antPos, antPosLat, antPosLon, antPosAlt, antCoordSys
	Dim az, el, EpochSec
	Dim AntennaGain, scanLoss, maxScan
	Dim gainModel, gmax, designFreq, area, eff, lambda
	Dim sineOfElevation, alphaPrimary, betaPrimary, alphaSecondary, betaSecondary, alphabeta, arcsinab

	Pi = 4.0 * Atn(1)
	
	'Get input values (those not used by simple cosine loss model are commented out)

	'date = inputData(VB_PhasedArrayAntGain_Inputs.DateUTC)
	'cb = inputData(VB_PhasedArrayAntGain_Inputs.CbName)
	freq = inputData(VB_PhasedArrayAntGain_Inputs.Frequency)
	az  = inputData(VB_PhasedArrayAntGain_Inputs.AzimuthAngle)
	el = inputData(VB_PhasedArrayAntGain_Inputs.ElevationAngle)
	'antPos    = inputData(VB_PhasedArrayAntGain_Inputs.AntennaPosLLA)
	'antPosLat = antPos(0)
	'antPosLon = antPos(1)
	'antPosAlt = antPos(2)	
	'antCoordSys = inputData(VB_PhasedArrayAntGain_Inputs.AntennaCoordSystem)
	'EpochSec = inputData(VB_PhasedArrayAntGain_Inputs.EpochSec)


	' Initialize return gain value and internally calculated scan angle value
	AntennaGain = -999999.9
    	scanLoss    = 0.0
    	
    	'Maximum Allowable Scan Range - User defined
	'-------------------------------------------
	'Scan angles larger than this hardcoded limit will result in zero gain returned to STK
	'For the simple cosine loss model, max scan set to 60 degrees (Pi/3)
    	maxScan = Pi/3

    	'Maximum Gain Model
	'------------------
	'Represents broadside on-axis mainbeam boresight gain (i.e. boresight along array's
	'normal vector). As shipped, the script is set to use a hardcoded maximum gain of 
	'41 dB (equivalent to 1 meter parabolic at 14.5 GHz and 55% efficiency). The user may
	'change this harcoded value or use the calculation-based model provided by setting
	'the gainModel script parameter and providing the appropriate inputs. A placeholder is
	'also provided for the user to introduce their own max gain calculation.

	gainModel = 0
	
	If (gainModel = 0) Then
	  'Hardcoded maximum gain value--user can set value as desired
    	  gmax = 41.0

	Elseif (gainModel = 1) Then
	  'Max gain calculated based on circular aperture area, aperture efficiency, and operating frequency.
	  'This model is useful when the array will be used over a variety of operating frequencies.
	  'Assumes half-wavelength or less element spacing!!!
	  'Reference: Handbook of Electrical Engineering Calculations, Phadke, 1999

	  'USER-SPECIFIED DESIGN FREQUENCY IN HZ -- used to enforce half-wavelength or less 
	  'element spacing assumption (operating frequencies above design frequency will result
	  'in a return value of -999999.9 dB gain)
	  designFreq = 14500000000.

	  'USER-SPECIFIED APERTURE AREA IN SQUARE-METERS
	  area = 3.141593

	  'USER-SPECIFIED APERTURE EFFICIENCY (UNITLESS)
	  eff = 0.6 

	  'Calculate max gain in dB
	  If (freq <= designFreq) Then
	    lambda = 299792458.0 / freq
	    gmax = eff*4*Pi*area/(lambda*lambda)
	    gmax = 10*Log(gmax)/Log(10.0)	  

	  Else
	    gmax = -999999.9

	  End If

	Else
	  'Placeholder for user-supplied maximum on-axis gain calculation.
	  gmax = 0.0

	End If
	

	'Scan Angle Computation
	'----------------------
	
	'Computing Scan Loss and Antenna Gain under the condition of less than or equal to Maximum Allowable Scan
	'Otherwise Antenna Gain = -999999.9 dB

	If (el <= maxScan) Then

          ' When not using a hardcoded max gain, only compute antenna gain if operating frequency
	  ' is less than design frequency that way the same error/null value (-999999.9 dB) is
	  ' always returned from the plugin.

	    If (gainModel = 0) Then
	      scanLoss = Cos(el)
	      scanLoss = 10.0*Log(scanLoss)/Log(10.0)
	      AntennaGain = gmax + scanLoss

	    Elseif (gainModel = 1) Then
	      If (freq <= designFreq) Then	
	        scanLoss = Cos(el)
	        scanLoss = 10.0*Log(scanLoss)/Log(10.0)
	        AntennaGain = gmax + scanLoss
	      End If

	    Else
	      'Placeholder for use with user-supplied maximum on-axis gain calculation.

	    End If

	End If
	

	'Return of Outputs to STK
	'------------------------

	' NOTE: Simple cosine loss model only returns AntennaGain and AntennaMaxGain
	' NOTE: Simple cosine loss model does not compute integrated gain and thus should only be used
	'       with a constant system noise temperature when used in Rx and Radar objects.

	' NOTE: All outputs MUST be returned and in the same order that they were registered.
	'       STK will interpret return values with the units shown below.

	' AntennaGain (dB), gain of the antenna at time and in the Azi-Elev direction off the boresight.
	' Beamwidth (Rad) is the 3-dB beamwidth of the antenna.
	' AntennaMaxGain (dB) is the maximum ( possibly boresight gain of the antenna)
	' IntegratedGain of the antenna (range 0-1) used for antenna Noise computation.
	' Dynamic Gain is a flag (value = 0 or 1) depending on whether the graphics get updated at each timestep.

	returnValue(VB_PhasedArrayAntGain_Outputs.AntennaGain)     = AntennaGain
	returnValue(VB_PhasedArrayAntGain_Outputs.Beamwidth)       = 0
	returnValue(VB_PhasedArrayAntGain_Outputs.AntennaMaxGain)  = gmax
	returnValue(VB_PhasedArrayAntGain_Outputs.IntegratedGain)  = 0
	returnValue(VB_PhasedArrayAntGain_Outputs.AntennaCoordSystem)  = 0
	returnValue(VB_PhasedArrayAntGain_Outputs.DynamicGain)     = 0

	'############################################################################################
	' USER GAIN MODEL AREA -- END
	'############################################################################################

	VB_PhasedArrayAntGain_compute = returnValue

End Function

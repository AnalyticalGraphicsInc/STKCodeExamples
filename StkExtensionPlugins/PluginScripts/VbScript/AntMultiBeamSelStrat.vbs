' Custom multibeam antenna beam selection strategy plugin 

Option explicit

'flag so that the first computed output is displayed
Dim Verbose
Verbose = -1

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_AntMultiBeamSelStrat (argArray)
	Dim retVal

	If IsEmpty(argArray(0)) Then
		retVal = VB_AntMultiBeamSelStrat_compute(argArray)
		
	ElseIf argArray(0) = "register" Then
		retVal = VB_AntMultiBeamSelStrat_register()

	ElseIf argArray(0) = "compute" Then
		retVal = VB_AntMultiBeamSelStrat_compute(argArray)
		
	Else
		' bad call
		retVal = Empty
		
	End If

	VB_AntMultiBeamSelStrat = retVal

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_AntMultiBeamSelStrat_register()

	ReDim argStr(10)
			
	' Outputs
	argStr(0) = "ArgumentType = Output; Name = BeamNumber; ArgumentName = BeamNumber"

	' Inputs
	argStr(1) = "ArgumentType = Input; Name = DateUTC; ArgumentName = DateUTC"
	argStr(2) = "ArgumentType = Input; Name = EpochSec; ArgumentName = EpochSec"
	argStr(3) = "ArgumentType = Input; Name = CbName; ArgumentName = CbName"
	argStr(4) = "ArgumentType = Input; Name = AntennaPosLLA; ArgumentName = AntennaPosLLA"
	argStr(5) = "ArgumentType = Input; Name = NumberOfBeams; ArgumentName = NumberOfBeams"
	argStr(6) = "ArgumentType = Input; Name = BeamIDsArray; ArgumentName = BeamIDsArray"
	argStr(7) = "ArgumentType = Input; Name = Frequency; ArgumentName = Frequency"
	argStr(8) = "ArgumentType = Input; Name = Power; ArgumentName = Power"
	argStr(9) = "ArgumentType = Input; Name = IsActive; ArgumentName = IsActive"

	VB_AntMultiBeamSelStrat_register = argStr


End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_AntMultiBeamSelStrat_compute(argArray)
	
	Dim retVal(1)
	Dim DateUTC, CbName, AntennaPosLLA, NumberOfBeams, BeamIDsArray, Frequency, Power
	Dim EpSec, AntennaPosLat, AntennaPosLon, AntennaPosAlt
	Dim i, lambda, IsActive
	
	DateUTC = argArray(1)
	EpSec = argArray(2)
	CbName = argArray(3)
	AntennaPosLLA = argArray(4)
	NumberOfBeams = argArray(5)	
	BeamIDsArray = argArray(6)
	Frequency = argArray(7)
	Power = argArray(8)
        IsActive = argArray(9)
	
	AntennaPosLat = AntennaPosLLA(0)
	AntennaPosLon = AntennaPosLLA(1)
	AntennaPosAlt = AntennaPosLLA(2)

	lambda = 299792458.0 / Frequency(0)

	retVal(0) = 1
	For i = 0 To NumberOfBeams - 1
	   If IsActive(i) = 1 Then
	      retVal(0) = i + 1
	      Exit For
	   End If
	Next

	' Populate the output array and return the result
	VB_AntMultiBeamSelStrat_compute = retVal
	
	' Display results for first function call only
	If Verbose < 0 Then
		MsgBox "Return Value = " & retVal(0)
		Verbose = 1
	End If
	
End Function
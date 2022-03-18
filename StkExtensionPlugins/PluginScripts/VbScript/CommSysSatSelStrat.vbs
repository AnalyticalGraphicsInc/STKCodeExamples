' Custom multibeam antenna beam selection strategy plugin 

Option explicit

'flag so that the first computed output is displayed
Dim Verbose
Verbose = -1

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_CommSysSatSelStrat(argArray)
	Dim retVal

	If IsEmpty(argArray(0)) Then
		retVal = VB_CommSysSatSelStrat_compute(argArray)
		
	ElseIf argArray(0) = "register" Then
		retVal = VB_CommSysSatSelStrat_register()

	ElseIf argArray(0) = "compute" Then
		retVal = VB_CommSysSatSelStrat_compute(argArray)
		
	Else
		' bad call
		retVal = Empty
		
	End If

	VB_CommSysSatSelStrat = retVal

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_CommSysSatSelStrat_register()

	ReDim argStr(21)
			
	' Outputs
	argStr(0) = "ArgumentType = Output; Name = SatSelMeritValue; ArgumentName = SatSelMeritValue"

	' Inputs
	argStr(1) = "ArgumentType = Input; Name = DateUTC; ArgumentName = DateUTC"
	argStr(2) = "ArgumentType = Input; Name = EpochSec; ArgumentName = EpochSec"
	argStr(3) = "ArgumentType = Input; Name = CbName; ArgumentName = CbName"
	argStr(4) = "ArgumentType = Input; Name = CommSysPath; ArgumentName = CommSysPath"
	argStr(5) = "ArgumentType = Input; Name = FromIndex; ArgumentName = FromIndex"
	
	argStr(6) = "ArgumentType = Input; Name = NumberOfFromObjects; ArgumentName = NumberOfFromObjects"
	argStr(7) = "ArgumentType = Input; Name = FromObjectsIDArray; ArgumentName = FromObjectsIDArray"
	argStr(8) = "ArgumentType = Input; Name = FromObjectIsStatic; ArgumentName = FromObjectIsStatic"
	argStr(9) = "ArgumentType = Input; Name = FromObjectPosCBFArray; ArgumentName = FromObjectPosCBFArray"
	argStr(10) = "ArgumentType = Input; Name = FromObjectPosLLAArray; ArgumentName = FromObjectPosLLAArray"
	
	argStr(11) = "ArgumentType = Input; Name = FromToRelPosArray; ArgumentName = FromToRelPosArray"
	argStr(12) = "ArgumentType = Input; Name = FromObjectAttitudeArray; ArgumentName = FromObjectAttitudeArray"
	argStr(13) = "ArgumentType = Input; Name = ToIndex; ArgumentName = ToIndex"
	argStr(14) = "ArgumentType = Input; Name = NumberOfToObjects; ArgumentName = NumberOfToObjects"
	argStr(15) = "ArgumentType = Input; Name = ToObjectsIDArray; ArgumentName = ToObjectsIDArray"
	
	argStr(16) = "ArgumentType = Input; Name = ToObjectIsStatic; ArgumentName = ToObjectIsStatic"
	argStr(17) = "ArgumentType = Input; Name = ToObjectPosCBFArray; ArgumentName = ToObjectPosCBFArray"
	argStr(18) = "ArgumentType = Input; Name = ToObjectPosLLAArray; ArgumentName = ToObjectPosLLAArray"
	argStr(19) = "ArgumentType = Input; Name = ToFromRelPosArray; ArgumentName = ToFromRelPosArray"
	argStr(20) = "ArgumentType = Input; Name = ToObjectAttitudeArray; ArgumentName = ToObjectAttitudeArray"
	
	VB_CommSysSatSelStrat_register = argStr

End Function

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Function VB_CommSysSatSelStrat_compute(argArray)
	
	Dim retVal(1)
	Dim DateUTC, EpochSec, CbName, CommSysPath, FromIndex
	Dim NumberOfFromObjects, FromObjectsIDArray, FromObjectIsStatic, FromObjectPosCBFArray, FromObjectPosLLAArray
	Dim FromToRelPosArray, FromObjectAttitudeArray, ToIndex, NumberOfToObjects, ToObjectsIDArray
	Dim ToObjectIsStatic, ToObjectPosCBFArray, ToObjectPosLLAArray, ToFromRelPosArray, ToObjectAttitudeArray
	
	Dim fromX, fromY, fromZ
	Dim toX, toY, toZ
	Dim rX, rY, rZ
	Dim m, n, range
	
	DateUTC = argArray(1)
	EpochSec = argArray(2)
	CbName = argArray(3)
	CommSysPath = argArray(4)
	FromIndex = argArray(5)
	
	NumberOfFromObjects = argArray(6)
	FromObjectsIDArray = argArray(7)
	FromObjectIsStatic = argArray(8)
	FromObjectPosCBFArray = argArray(9)
	FromObjectPosLLAArray = argArray(10)
	
	FromToRelPosArray = argArray(11)
	FromObjectAttitudeArray = argArray(12)
	ToIndex = argArray(13)
	NumberOfToObjects = argArray(14)
	ToObjectsIDArray = argArray(15)
	
	ToObjectIsStatic = argArray(16)
	ToObjectPosCBFArray = argArray(17)
	ToObjectPosLLAArray = argArray(18)
	ToFromRelPosArray = argArray(19)
	ToObjectAttitudeArray = argArray(20)
	
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	m = 3 * FromIndex
	n = 3 * ToIndex

	fromX = FromObjectPosCBFArray(m)
	toX = ToObjectPosCBFArray(n)
	
	m = m + 1
	n = n + 1
	fromY = FromObjectPosCBFArray(m)
	toY = ToObjectPosCBFArray(n)
	
	m = m + 1
	n = n + 1
	fromZ = FromObjectPosCBFArray(m)
	toZ = ToObjectPosCBFArray(n)

	rX = fromX - toX
	rY = fromY - toY
	rZ = fromZ - toZ
	
	range = sqr(rX^2 + rY^2 + rZ^2)
	
	' Populate the output array and return the result
	retVal(0) = range
	VB_CommSysSatSelStrat_compute = retVal
	
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	
	' Display results for first function call only
	If Verbose < 0 Then
		MsgBox "Return Value = " & retVal(0)
		Verbose = 1
	End If
	
End Function
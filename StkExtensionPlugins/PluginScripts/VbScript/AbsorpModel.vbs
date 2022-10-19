
Dim VB_AbsorpModel_globalVar
Dim VB_AbsorpModel_Inputs
Dim VB_AbsorpModel_Outputs

'==========================================================================
' VB_AbsorpModel() fctn
'==========================================================================
Function VB_AbsorpModel ( argArray )

    Dim retVal

    If IsEmpty(argArray(0)) Then

        'do compute

        retVal = VB_AbsorpModel_compute( argArray )

    ElseIf argArray(0) = "register" Then

        VB_AbsorpModel_globalVar = -1

        retVal = VB_AbsorpModel_register()

    ElseIf argArray(0) = "compute" Then

        'do compute

        retVal = VB_AbsorpModel_compute( argArray )

    Else

        'bad call

        retVal = Empty

    End If

    VB_AbsorpModel = retVal

End Function

Function VB_AbsorpModel_register()

    ReDim descripStr(3), argStr(8)

    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = AbsorpLoss"
    descripStr(2)="ArgumentName = AbsorpLoss"
    argStr(0) = descripStr

    ReDim descripStr(3)

    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = NoiseTemp"
    descripStr(2)="ArgumentName = NoiseTemp"
    argStr(1) = descripStr

    ReDim descripStr(4)

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = DateUTC"
    descripStr(2)="ArgumentName = DateUTC"
    descripStr(3)="Type = Value"
    argStr(2) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = Frequency"
    descripStr(2)="ArgumentName = Frequency"
    descripStr(3)="Type = Value"
    argStr(3) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = CbName"
    descripStr(2)="ArgumentName = CbName"
    descripStr(3)="Type = Value"
    argStr(4) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = XmtrPosCBF"
    descripStr(2)="ArgumentName = XmtrPosCBF"
    descripStr(3)="Type = Value"
    argStr(5) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = RcvrPosCBF"
    descripStr(2)="ArgumentName = RcvrPosCBF"
    descripStr(3)="Type = Value"
    argStr(6) = descripStr

    argStr(7) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

    VB_AbsorpModel_register = argStr

End Function

Function VB_AbsorpModel_compute( inputData )

    'NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

    Dim outStr

    outStr = ""

    If VB_AbsorpModel_globalVar < 0 Then

        Set VB_AbsorpModel_Inputs = g_GetPluginArrayInterface("VB_AbsorpModel_Inputs")

        outStr = VB_AbsorpModel_Inputs.Describe()

        displayDialog outStr , 800

        Set VB_AbsorpModel_Outputs = g_GetPluginArrayInterface("VB_AbsorpModel_Outputs")

        outStr = VB_AbsorpModel_Outputs.Describe()

        displayDialog outStr , 800

        VB_AbsorpModel_globalVar = 1

        MsgBox inputData(VB_AbsorpModel_Inputs.dateStr)

    End If

    'Model for testing
    'Absorption Loss is about 10% of the free space loss (in dBs) and must be less than one.
    'NoiseTemp is the noise temprature in Kelvin.
    '
    'NOTE:  Return Loss is in Linear Scale, STK will convert to dBs

    Redim returnValue(2)
    Dim freq
    Dim fromXYZ, fromX, fromY, fromZ, toXYZ, toX, toY, toZ
    Dim range, loss, freeSpace

    freq  = inputData(VB_AbsorpModel_Inputs.Frequency)

    fromXYZ = inputData(VB_AbsorpModel_Inputs.XmtrPosCBF)
    fromX   = fromXYZ(0)
    fromY   = fromXYZ(1)
    fromZ   = fromXYZ(2)

    toXYZ = inputData(VB_AbsorpModel_Inputs.RcvrPosCBF)
    toX   = toXYZ(0)
    toY   = toXYZ(1)
    toZ   = toXYZ(2)

    range = Sqr((fromX-toX)^2 + (fromY-toY)^2 + (fromZ-toZ)^2)

    freeSpace = (4 * 3.141592 * range * freq) / 299792458.0
    loss = 10^(Log10(freeSpace * freeSpace)/10)

    returnValue(VB_AbsorpModel_Outputs.AbsorpLoss) = 1.0/loss
    returnValue(VB_AbsorpModel_Outputs.NoiseTemp)  = 273.15 * (1 - 1.0/loss)

    VB_AbsorpModel_compute = returnValue
    
End Function

Function Log10(x)

    Log10 = Log(x) / Log(10.0)
    
End Function


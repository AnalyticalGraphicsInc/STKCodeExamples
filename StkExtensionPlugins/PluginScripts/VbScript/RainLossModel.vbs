
Dim VB_RainLossModel_globalVar
Dim VB_RainLossModel_Inputs
Dim VB_RainLossModel_Outputs

'==========================================================================
' VB_RainLossModel() fctn
'==========================================================================
Function VB_RainLossModel ( argArray )

    Dim retVal

    If IsEmpty(argArray(0)) Then

        'do compute
        retVal = VB_RainLossModel_compute( argArray )

    ElseIf argArray(0) = "register" Then

        VB_RainLossModel_globalVar = -1
        retVal = VB_RainLossModel_register()

    ElseIf argArray(0) = "compute" Then

        'do compute
        retVal = VB_RainLossModel_compute( argArray )

    Else

        'bad call
        retVal = Empty

    End If

    VB_RainLossModel = retVal

End Function

Function VB_RainLossModel_register()

    ReDim descripStr(3), argStr(9)

    descripStr(0)="ArgumentType = Output"
    descripStr(1)="Name = RainLoss"
    descripStr(2)="ArgumentName = RainLoss"
    argStr(0) = descripStr

    ReDim descripStr(4)

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = DateUTC"
    descripStr(2)="ArgumentName = DateUTC"
    descripStr(3)="Type = Value"
    argStr(1) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = Frequency"
    descripStr(2)="ArgumentName = Frequency"
    descripStr(3)="Type = Value"
    argStr(2) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = CbName"
    descripStr(2)="ArgumentName = CbName"
    descripStr(3)="Type = Value"
    argStr(3) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = ElevAngle"
    descripStr(2)="ArgumentName = ElevAngle"
    descripStr(3)="Type = Value"
    argStr(4) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = OutagePercentage"
    descripStr(2)="ArgumentName = OutagePercentage"
    descripStr(3)="Type = Value"
    argStr(5) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = RcvrPosLLA"
    descripStr(2)="ArgumentName = RcvrPosLLA"
    descripStr(3)="Type = Value"
    argStr(6) = descripStr

    descripStr(0)="ArgumentType = Input"
    descripStr(1)="Name = XmtrPosLLA"
    descripStr(2)="ArgumentName = XmtrPosLLA"
    descripStr(3)="Type = Value"
    argStr(7) = descripStr

    argStr(8) = "ArgumentType = Input ; Name = DateUTC ; ArgumentName = dateStr"

    VB_RainLossModel_register = argStr

End Function

Function VB_RainLossModel_compute( inputData )

    'NOTE: inputData(0) is the call Mode, which is either Empty or 'compute'

    Dim outStr

    outStr = ""

    If VB_RainLossModel_globalVar < 0 Then

        Set VB_RainLossModel_Inputs = g_GetPluginArrayInterface("VB_RainLossModel_Inputs")

        outStr = VB_RainLossModel_Inputs.Describe()

        displayDialog outStr , 800

        Set VB_RainLossModel_Outputs = g_GetPluginArrayInterface("VB_RainLossModel_Outputs")

        outStr = VB_RainLossModel_Outputs.Describe()

        displayDialog outStr , 800

        VB_RainLossModel_globalVar = 1

        MsgBox inputData(VB_RainLossModel_Inputs.dateStr)

    End If
    
    '**************************************
    'USE MODEL AREA
    'PLEASE DO NOT CHANGE ANYTHING ABOVE THIS LINE
    '**************************************

    'Compute the Test Model
    'NOTE:  Loss should be returned as a positive dB value.

    Redim returnValue(1)
    Dim freq, elev, outPcnt
    Dim rcvrLLA, rcvrLat, rcvrLon, rcvrAlt
    Dim xmtrLLA, xmtrLat, xmtrLon, xmtrAlt
    Dim loss

    freq  = inputData(VB_RainLossModel_Inputs.Frequency)
    elev  = inputData(VB_RainLossModel_Inputs.ElevAngle)
    outPcnt = inputData(VB_RainLossModel_Inputs.OutagePercentage)

    rcvrLLA = inputData(VB_RainLossModel_Inputs.RcvrPosLLA)
    rcvrLat = rcvrLLA (0)
    rcvrLon = rcvrLLA (1)
    rcvrAlt = rcvrLLA (2)

    xmtrLLA = inputData(VB_RainLossModel_Inputs.XmtrPosLLA)
    xmtrLat = xmtrLLA (0)
    xmtrLon = xmtrLLA (1)
    xmtrAlt = xmtrLLA (2)

    loss  = 1/20

    returnValue(VB_RainLossModel_Outputs.RainLoss) = loss

    'END OF USER MODEL AREA
    'DO NOT CHANGE ANYTHING BELOW THIS LINE
    '**************************************

    VB_RainLossModel_compute = returnValue

End Function

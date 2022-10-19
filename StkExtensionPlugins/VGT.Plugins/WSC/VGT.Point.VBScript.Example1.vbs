'=====================================================
'  Copyright 2012, Analytical Graphics, Inc.          
'=====================================================

'==========================================
' AgEUtFrame Enumeration
'==========================================
Dim eUtFrameInertial, eUtFrameFixed, eUtFrameLVLH, eUtFrameNTC, eUtFrameICRF, eUtFrameJ2000
eUtFrameInertial          = 0
eUtFrameFixed             = 1
eUtFrameLVLH              = 2
eUtFrameNTC               = 3
eUtFrameICRF              = 4
eUtFrameJ2000             = 5

'==========================================
' AgEUtTimeScale Enumeration
'==========================================
Dim eUTC, eTAI, eTDT, eUT1, eSTKEpochSec, eTDB, eGPS
eUTC             = 0
eTAI             = 1
eTDT             = 2
eUT1             = 3
eSTKEpochSec     = 4
eTDB             = 5
eGPS             = 6

'==========================================
' AgELogMsgType Enumeration
'==========================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm
eLogMsgDebug         = 0
eLogMsgInfo          = 1
eLogMsgForceInfo     = 2
eLogMsgWarning       = 3
eLogMsgAlarm         = 4

'===========================================
' AgEAttrAddFlags Enumeration
'===========================================
Dim eFlagNone, eFlagTransparent, eFlagHidden, eFlagTransient, eFlagReadOnly, eFlagFixed
eFlagNone            = 0
eFlagTransparent     = 2
eFlagHidden          = 4
eFlagTransient       = 8
eFlagReadOnly        = 16
eFlagFixed           = 32


'==========================================
' Declare Global Variables
'==========================================
Dim m_AgUtPluginSite, m_StkRootObject, m_AgAttrScope, m_DisplayName, m_MyDouble, m_MyString
Set m_AgUtPluginSite     = Nothing
Set m_StkRootObject      = Nothing
Set m_AgAttrScope        = Nothing
m_DisplayName        = "VGT.Point.VBScript.Example1"
m_MyDouble           = 10.123
m_MyString           = "test"

Dim m_objectConfiguredPoint
m_objectConfiguredPoint = null

Sub Message(logMsgType, msg)

    If (NOT m_AgUtPluginSite is Nothing) Then
        Call m_AgUtPluginSite.Message( logMsgType, m_DisplayName & ": " & msg )
    End If

End Sub

Function Init( AgUtPluginSite )

    Set m_AgUtPluginSite = AgUtPluginSite

    Call Message(eLogMsgInfo, "Init() Entered")

    If (NOT m_AgUtPluginSite is Nothing) Then
        ' Get a pointer to the STK Object Model root object
        Set m_StkRootObject = m_AgUtPluginSite.StkRootObject
    End If

    Call Message(eLogMsgInfo, "Init() Exited")

    Init = true

End Function


Sub Register( Result )

    Dim oPath

    objPath = "[" & Result.ObjectPath & "]"

    Result.ShortDescription = "Test short Desc: Component created using " & m_DisplayName + " " & objPath
    Result.LongDescription = "Test long Desc: Component created using " & m_DisplayName + " " & objPath

    Call Result.SetRefSystem("Body", "<MyObject>")

End Sub

Function Reset( Result )

    Call Message(eLogMsgInfo, "Reset() Entered:")

    Set m_CalcToolProvider = Result.CalcToolProvider
    Set m_VectorToolProvider = Result.VectorToolProvider

    Set m_objectConfiguredPoint = m_VectorToolProvider.ConfigurePoint("SubPoint(Detic)", "<MyObject>", "Body", "<MyObject>")

    Call Message(eLogMsgInfo, "Reset() Exited:")

    Reset = True

End Function

Function Evaluate( Result )

    If (NOT m_objectConfiguredPoint is Nothing) Then
        Dim currentValueArray
        Dim x, y, z

        currentValueArray = m_objectConfiguredPoint.CurrentValue_Array(Result)
        x = currentValueArray(0)
        y = currentValueArray(1)
        z = currentValueArray(2)
        
        ' For this example, the point is the detic point divided by two.
        Call Result.SetPosition(x / 2, y / 2, z / 2)
    End If

    Evaluate = true

End Function

Sub Free()

    Call Message(eLogMsgInfo, "Free()")

    Set m_AgUtPluginSite = Nothing
    Set m_StkRootObject = Nothing
    Set m_CalcToolProvider = Nothing
    Set m_VectorToolProvider = Nothing
    Set m_objectConfiguredPoint = Nothing

End Sub

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

    Call Message(eLogMsgInfo, "GetPluginConfig() Entered")

    If (m_AgAttrScope is Nothing) Then
        Set m_AgAttrScope = AgAttrBuilder.NewScope()
        Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "MyString", "A string", "MyString", eFlagReadOnly )
        Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "MyDouble", "A double", "MyDouble", eFlagNone )
    End If

    Set GetPluginConfig = m_AgAttrScope

End Function


'===========================
' VerifyPluginConfig method
'===========================
Sub VerifyPluginConfig( VerifyResult )

    Set Result = true
    Set Message = "Ok"

    VerifyResult.Result  = Result
    VerifyResult.Message = Message

End Sub

'==================
' MyString property
'==================
Function GetMyString()

    GetMyString = m_MyString

End Function

Sub SetMyString( val )

    m_MyString = val

End Sub

'==================
' MyDouble property
'==================
Function GetMyDouble()

    GetMyDouble = m_MyDouble

End Function

Sub SetMyDouble( val )

    m_MyDouble = val

End Sub

'=====================================================
'  Copyright 2012, Analytical Graphics, Inc.          
'=====================================================

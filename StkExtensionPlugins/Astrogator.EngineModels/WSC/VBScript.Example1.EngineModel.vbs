'======================================================
'  Copyright 2005-2011, Analytical Graphics, Inc.          
' =====================================================

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

'================================
' Declare Global Variables
'================================
Dim m_AgUtPluginSite
Dim m_AgAttrScope
Dim m_InitTime
Dim m_gatorPrv
Dim m_argOfLat

Set m_AgUtPluginSite = Nothing
Set m_AgAttrScope	 = Nothing
Set m_gatorPrv = Nothing
Set m_argOfLat = Nothing

'======================================
' Declare Global 'Attribute' Variables
'======================================
Dim m_Name
Dim m_T0
Dim m_T1
Dim m_T2
Dim m_Ts
Dim m_Tc
Dim m_Isp

m_Name	= "VBScript.Example1.EngineModel.wsc"   
m_T0	= 0
m_T1	= 0.0001
m_T2	= 0.0000001
m_Ts	= 0
m_Tc	= 0
m_Isp   = 3000

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
		'MsgBox "here1"
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		'MsgBox "here2"
		
		'===========================
		' General Plugin attributes
		'===========================
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias", "Name", eFlagReadOnly )
		'MsgBox "here3"
		'===========================
		' Propagation related
		'===========================
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "T0", "Initial Thrust", "T0", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "T1", "Linear Thrust Coefficient", "T1", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "T2", "Quadratic Thrust Coefficient", "T2", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Ts", "Sine Thrust Coefficient", "TS", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Tc", "Cosine Thrust Coefficient", "TC", 0 )
		'MsgBox "here4"
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Isp", "Specific Impulse", "ISP", 0 )
		'MsgBox "here5"
	End If
	'MsgBox "here6"

	'If m_AgAttrScope is Nothing Then
	'	MsgBox "Uhhh Oh"
	'End If
	Set GetPluginConfig = m_AgAttrScope

End Function  

'===========================
' VerifyPluginConfig method
'===========================
Function VerifyPluginConfig(AgUtPluginConfigVerifyResult)
   
    Dim Result
    Dim Message

	Result = true
	Message = "Ok"

	'TODO:  Add Yaw and Pitch Maximum and Minimum Checks here

	AgUtPluginConfigVerifyResult.Result  = Result
	AgUtPluginConfigVerifyResult.Message = Message

End Function  

'===========================
' Message method
'===========================
Sub Message( msgType, msg )
   
	If( Not m_AgUtPluginSite is Nothing) then
	   	
		Call m_AgUtPluginSite.Message( msgType, msg )

	End If
   	
End Sub

'======================
' Init Method
'======================
Function Init( AgUtPluginSite )

	Set m_AgUtPluginSite = AgUtPluginSite
	
	if(NOT m_AgUtPluginSite is Nothing) then
	
		Set m_gatorPrv = m_AgUtPluginSite.GatorProvider
        	
        if(NOT m_gatorPrv is Nothing) then

        	Set m_argOfLat = m_gatorPrv.ConfigureCalcObject("Argument_of_Latitude")
        	
        end if
        
    end if
    
    if(m_argOfLat is Nothing) then
		Init = 0
	else
		Init = 1
	end if
    
End Function

'======================
' PrePropagate Method
'======================
Function PrePropagate( AgGatorPluginResultState )

	If( Not AgGatorPluginResultState is Nothing) then

		Dim dcArray
		dcArray = AgGatorPluginResultState.DayCount_Array( eStkEpochSec )
		If(IsArray(dcArray) and UBound(dcArray) > 0) Then
 			m_InitTime = dcArray(0) * 86400.0 + dcArray(1)
 		End If
 		
	End If

	PrePropagate = 1

End Function

'======================
' PreNextStep Function
'======================
Function PreNextStep( AgGatorPluginResultState )

	PreNextStep = 1
	
End Function

'=================
' Evaluate Method
'=================
Function Evaluate( AgGatorPluginResultEvalEngineModel )

	If( Not AgGatorPluginResultEvalEngineModel is Nothing) then
         
			Dim time
			Dim Thrust
			Dim deltaT
			Dim argOfLat
			Dim dcArray
			time = m_InitTime
			
			dcArray = AgGatorPluginResultEvalEngineModel.DayCount_Array( eStkEpochSec )
			If(IsArray(dcArray) and UBound(dcArray) > 0) Then
 				time = dcArray(0) * 86400.0 + dcArray(1)
 			End If
			
			deltaT = time - m_InitTime
			
			argOfLat = m_argOfLat.Evaluate(AgGatorPluginResultEvalEngineModel)
			
			Thrust = m_T0 + m_T1 * deltaT + m_T2 * deltaT^2 + m_Ts * sin(argOfLat) + m_Tc * cos(argOfLat)
					
			Call AgGatorPluginResultEvalEngineModel.SetThrustAndIsp(Thrust, m_Isp)
			
		End If
         
        Evaluate = 1

End Function

'=================
' Free Method
'=================
Sub Free()

	' do nothing
        
End Sub
    
'==================
' Name property
'==================
Function GetName()

	GetName = m_Name

End function

'==================
' T0 property
'==================
Function GetT0()

       GetT0 = m_T0

End Function

Function SetT0( thrust )

       m_T0 = thrust

End Function

'==================
' T1 property
'==================
Function GetT1()

       GetT1 = m_T1

End Function

Function SetT1( thrust )

       m_T1 = thrust

End Function

'==================
' T2 property
'==================
Function GetT2()

       GetT2 = m_T2

End Function

Function SetT2( thrust )

       m_T2 = thrust

End Function

'==================
' TS property
'==================
Function GetTS()

       GetTS = m_Ts

End Function

Function SetTS( thrust )

       m_Ts = thrust

End Function

'==================
' TC property
'==================
Function GetTC()

       GetTC = m_Tc

End Function

Function SetTC( thrust )

       m_Tc = thrust

End Function

'==================
' ISP property
'==================
Function GetISP()

       GetISP = m_Isp

End Function

Function SetISP( isp )

       m_Isp = isp

End Function

'======================================================
'  Copyright 2005-2011, Analytical Graphics, Inc.          
' =====================================================
'======================================================
'  Copyright 2012, Analytical Graphics, Inc.          
' =====================================================

'===========================================
' AgEAttrAddFlags Enumeration
'===========================================
Dim eFlagNone, eFlagTransparent, eFlagHidden, eFlagTransient, eFlagReadOnly, eFlagFixed

eFlagNone			= 0
eFlagTransparent	= 2
eFlagHidden			= 4
eFlagTransient		= 8  
eFlagReadOnly		= 16
eFlagFixed			= 32

'==================================
' Log Msg Type Enumeration
'==================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm

eLogMsgDebug	 	= 0
eLogMsgInfo 		= 1
eLogMsgForceInfo 	= 2
eLogMsgWarning 		= 3
eLogMsgAlarm 		= 4

'==========================================
' AgETimeScale
'==========================================
Dim eSTKEpochSec
eSTKEpochSec = 4

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
Dim m_Thrust
Dim m_ThrustRate
Dim m_MassFlowRate

m_Name	= "VBScript.ConstantThrustRate.EngineModel.wsc"   
m_Thrust	= 1000 'N
m_ThrustRate	= 5 'N/s
m_MassFlowRate  = 10 'kg/sec

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
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "Thrust", "Initial Thrust", "Thrust", "ForceUnit", "Newtons", "Newtons", 0 )
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "ThrustRate", "Thrust Rate (per sec)", "ThrustRate", "ForceUnit", "Newtons", "Newtons", 0 )
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "MassFlowRate", "Mass Flow Rate (kg/sec)", "MassFlowRate", "MassUnit", "Kilograms", "Kilograms", 0 )
	End If

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

	Init = 1
    
End Function

'======================
' PrePropagate Method
'======================
Function PrePropagate( AgGatorPluginResultEvalEngineModel )

	If( Not AgGatorPluginResultEvalEngineModel is Nothing) then

		Dim dcArray
		dcArray = AgGatorPluginResultEvalEngineModel.DayCount_Array( eStkEpochSec )
		If(IsArray(dcArray) and UBound(dcArray) > 0) Then
 			m_InitTime = dcArray(0) * 86400.0 + dcArray(1)
 		End If
 		
	End If

	PrePropagate = 1

End Function

'======================
' PreNextStep Function
'======================
Function PreNextStep( AgGatorPluginResultEvalEngineModel )

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
			Dim dcArray
			time = m_InitTime
			
			dcArray = AgGatorPluginResultEvalEngineModel.DayCount_Array( eStkEpochSec )
			If(IsArray(dcArray) and UBound(dcArray) > 0) Then
 				time = dcArray(0) * 86400.0 + dcArray(1)
 			End If
			
			deltaT = time - m_InitTime
						
			Thrust = m_Thrust + m_ThrustRate * deltaT
					
			Call AgGatorPluginResultEvalEngineModel.SetThrustAndMassFlowRate(Thrust, m_MassFlowRate)
			
		End If
         
        Evaluate = 1

End Function

'======================
' PostPropagate Method
'======================
Function PostPropagate(resultInterface)
      
	PostPropagate = 0
       	
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
' Thrust property
'==================
Function GetThrust()

       GetThrust = m_Thrust

End Function

Function SetThrust( thrust )

       m_Thrust = thrust

End Function

'==================
' ThrustRate property
'==================
Function GetThrustRate()

       GetThrustRate = m_ThrustRate

End Function

Function SetThrustRate( thrustRate )

       m_ThrustRate = thrustRate

End Function

'==================
' MassFlowRate property
'==================
Function GetMassFlowRate()

       GetMassFlowRate = m_MassFlowRate

End Function

Function SetMassFlowRate( massFlowRate )

       m_MassFlowRate = massFlowRate

End Function

'======================================================
'  Copyright 2012, Analytical Graphics, Inc.          
' =====================================================
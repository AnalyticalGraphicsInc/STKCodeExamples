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
Dim m_CrdnPluginProvider
Dim m_CrdnConfiguredAxes
Dim m_AgAttrScope
Dim m_InitTime
Dim m_gatorPrv
Dim m_argOfLat

Set m_AgUtPluginSite = Nothing
Set m_AgAttrScope	 = Nothing

'======================================
' Declare Global 'Attribute' Variables
'======================================
Dim m_Name
Dim m_yaw
Dim m_yawRate
Dim m_pitch
Dim m_pitchRate
Dim m_roll
Dim m_rollRate
Dim m_refAxes

'Set default values for this plugins user inputs
m_Name		= "VBScript.ConstantRates.AttitudeController.wsc"   
m_yaw		= 0.0
m_yawRate	= 0.0
m_pitch		= 0.0
m_pitchRate	= 0.0
m_roll		= 0.0
m_rollRate	= 0.0
m_refAxes = "VNC"

'=======================
' GetPluginConfig method - this will create the custom user inputs for this plugin which show up in STK
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
   
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		'===========================
		' General Plugin attributes
		'===========================
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias", "Name", eFlagReadOnly )
				
		'===========================
		' Propagation related
		'===========================
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "Yaw", "Initial Yaw Offset", "Yaw", "AngleUnit", "Radians", "Radians", 0 )
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "YawRate", "Yaw Rate (per sec)", "YawRate", "AngleUnit", "Radians", "Radians", 0 )
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "Pitch", "Initial Pitch Offset", "Pitch", "AngleUnit", "Radians", "Radians", 0 )
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "PitchRate", "Pitch Rate (per sec)", "PitchRate", "AngleUnit", "Radians", "Radians", 0 )
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "Roll", "Initial Roll Offset", "Roll", "AngleUnit", "Radians", "Radians", 0 )
		Call AgAttrBuilder.AddQuantityDispatchProperty2  ( m_AgAttrScope, "RollRate", "Roll Rate (per sec)", "RollRate", "AngleUnit", "Radians", "Radians", 0 )
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "RefAxes", "Reference Axes", "RefAxes", 0 )

	End If

	Set GetPluginConfig = m_AgAttrScope

End Function  

'===========================
' VerifyPluginConfig method - can be used to verify that inputs are valid, but not really used here
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
' Message method - helper function so other parts of the plugin can easily send something to the STK Message Viewer
'===========================
Sub Message( msgType, msg )
   
	If( Not m_AgUtPluginSite is Nothing) then
	   	
		Call m_AgUtPluginSite.Message( msgType, msg )

	End If
   	
End Sub

'======================
' Init Method - called at the beginning of the MCS evaluation
'======================
Function Init( AgUtPluginSite )

	Init = 1
            
End Function

'======================
' PrePropagate Method - called before running the propagate (or finite maneuver) segment which uses this plugin
'======================
Function PrePropagate( AgGatorPluginResultAttCtrl )

	If( Not AgGatorPluginResultAttCtrl is Nothing) then

		'Set the reference axes that will be used to define the attitude rotations
		Message eLogMsgInfo, "m_refAxes: " & m_refAxes
		Call AgGatorPluginResultAttCtrl.SetRefAxes(m_refAxes)

		'Set a global variable which holds the initial time (in EpSec) of the start of the segment. Will be used as a reference in the Evaluate method.
		Dim dcArray
		dcArray = AgGatorPluginResultAttCtrl.DayCount_Array( eStkEpochSec )
		If(IsArray(dcArray) and UBound(dcArray) > 0) Then
			m_InitTime = dcArray(0) * 86400.0 + dcArray(1)
		End If

		'dateArray = AgGatorPluginResultAttCtrl.Date_Array(0)	
		'Message eLogMsgInfo, "Burn start time: " & dateArray(0) & "-" & dateArray(1) & "-" & dateArray(2) & " " & dateArray(3) & ":" & dateArray(4) & ":" & dateArray(5)
		'Message eLogMsgInfo, "Burn start time (EpSec): " & m_InitTime
	End If

	PrePropagate = 1

End Function

'======================
' PreNextStep Function - called before each propagator step
'======================
Function PreNextStep( AgGatorPluginResultAttCtrl )

	PreNextStep = 1
	
End Function

'=================
' Evaluate Method - called during each force model evaluation
'=================
Function Evaluate( AgGatorPluginResultAttCtrl )

	If( Not AgGatorPluginResultAttCtrl is Nothing) then
         
			Dim time
			Dim yawAngle
			Dim pitchAngle
			Dim rollAngle			
			Dim deltaT
			Dim dcArray
			Dim dateArray
			
			time = m_InitTime
			
			'Get the current time at this evaluation
			dcArray = AgGatorPluginResultAttCtrl.DayCount_Array( eStkEpochSec )
			If(IsArray(dcArray) and UBound(dcArray) > 0) Then
 				time = dcArray(0) * 86400.0 + dcArray(1)
 			End If
						
			deltaT = time - m_InitTime
			yawAngle = m_yaw + m_yawRate * deltaT
			pitchAngle = m_pitch + m_pitchRate * deltaT
			rollAngle = m_roll + m_rollRate * deltaT
			Call AgGatorPluginResultAttCtrl.EulerRotate( 321, yawAngle, pitchAngle, rollAngle )
			
	End If
         
        Evaluate = 1

End Function

'======================
' PostPropagate Method - called after the propagation has finished
'======================
Function PostPropagate(resultInterface)
      
	PostPropagate = 0
       	
End Function

'=================
' Free Method - called after the entire MCS finishes running
'=================
Sub Free()

	' do nothing
        
End Sub
    

'******************
'Begin Property functions which allow STK to interact with the custom user inputs in this plugin
'******************

'==================
' Name property
'==================
Function GetName()

	GetName = m_Name

End function

'==================
' Pitch property
'==================
Function GetYaw()

       GetYaw = m_yaw

End Function

Function SetYaw( yaw )

       m_yaw = yaw

End Function

'==================
' PitchRate property
'==================
Function GetYawRate()

       GetYawRate = m_yawRate

End Function

Function SetYawRate( yawRate )

       m_yawRate = yawRate

End Function

'==================
' Pitch property
'==================
Function GetPitch()

       GetPitch = m_pitch

End Function

Function SetPitch( pitch )

       m_pitch = pitch

End Function

'==================
' PitchRate property
'==================
Function GetPitchRate()

       GetPitchRate = m_pitchRate

End Function

Function SetPitchRate( pitchRate )

       m_pitchRate = pitchRate

End Function

'==================
' Pitch property
'==================
Function GetRoll()

       GetRoll = m_roll

End Function

Function SetRoll( roll )

       m_roll = roll

End Function

'==================
' PitchRate property
'==================
Function GetRollRate()

       GetRollRate = m_rollRate

End Function

Function SetRollRate( rollRate )

       m_rollRate = rollRate

End Function

'==================
' RefAxes property
'==================
Function GetRefAxes()

       GetRefAxes = m_refAxes

End Function

Function SetRefAxes( refAxes )

       m_refAxes = refAxes

End Function



'======================================================
'  Copyright 2012, Analytical Graphics, Inc.          
' =====================================================
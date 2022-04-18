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

'==================================
' AgEEulerSequence Enumeration
'==================================
Dim e121, e123, e131, e132, e212, e213, e231, e232, e312, e313, e321, e323

e121 = 121
e123 = 123
e131 = 131
e132 = 132
e212 = 212
e213 = 213
e231 = 231
e232 = 232
e312 = 312
e313 = 313
e321 = 321
e323 = 323

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
Dim m_y0
Dim m_y1
Dim m_y2
Dim m_ys
Dim m_yc
Dim m_p0
Dim m_p1
Dim m_p2
Dim m_ps
Dim m_pc

m_Name	= "VBScript.Example1.AttitudeController.wsc"   
m_y0	= 0
m_y1	= 0.0001
m_y2	= 0.0000001
m_ys	= 0
m_yc	= 0
m_p0	= 0
m_p1	= 0.0002
m_p2	= 0.00000001
m_ps	= 0
m_pc	= 0


'=======================
' GetPluginConfig method
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
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Y0", "Initial Yaw", "Y0", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Y1", "Linear Yaw Coefficient", "Y1", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Y2", "Quadratic Yaw Coefficient", "Y2", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Ys", "Sine Yaw Coefficient", "YS", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Yc", "Cosine Yaw Coefficient", "YC", 0 )
				
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "P0", "Initial Pitch", "P0", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "P1", "Linear Pitch Coefficient", "P1", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "P2", "Quadratic Pitch Coefficient", "P2", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Ps", "Sine Pitch Coefficient", "PS", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Pc", "Cosine Pitch Coefficient", "PC", 0 )

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
Function PrePropagate( AgGatorPluginResultAttCtrl )

	If( Not AgGatorPluginResultAttCtrl is Nothing) then

			Call AgGatorPluginResultAttCtrl.SetRefAxes("Satellite VNC(Earth)")
			
			Dim dcArray
			
			dcArray = AgGatorPluginResultAttCtrl.DayCount_Array( eStkEpochSec )
			If(IsArray(dcArray) and UBound(dcArray) > 0) Then
 				m_InitTime = dcArray(0) * 86400.0 + dcArray(1)
 			End If

	End If

	PrePropagate = 1

End Function

'======================
' PreNextStep Function
'======================
Function PreNextStep( AgGatorPluginResultAttCtrl )

	PreNextStep = 1
	
End Function

'=================
' Evaluate Method
'=================
Function Evaluate( AgGatorPluginResultAttCtrl )

	If( Not AgGatorPluginResultAttCtrl is Nothing) then
         
			Dim time
			Dim yawAngle
			Dim pitchAngle
			Dim deltaT
			Dim argOfLat
			Dim dcArray
			
			time = m_InitTime
			
			dcArray = AgGatorPluginResultAttCtrl.DayCount_Array( eStkEpochSec )
			If(IsArray(dcArray) and UBound(dcArray) > 0) Then
 				time = dcArray(0) * 86400.0 + dcArray(1)
 			End If
						
			deltaT = time - m_InitTime
			
			argOfLat = m_argOfLat.Evaluate(AgGatorPluginResultAttCtrl)
			
			yawAngle = m_y0 + m_y1 * deltaT + m_y2 * deltaT^2 + m_ys * sin(argOfLat) + m_yc * cos(argOfLat)
			
			pitchAngle = m_p0 + m_p1 * deltaT + m_p2 * deltaT^2 + m_ps * sin(argOfLat) + m_pc * cos(argOfLat)
			
			Call AgGatorPluginResultAttCtrl.EulerRotate( e321, yawAngle, pitchAngle, 0 )
			
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
' Y0 property
'==================
Function GetY0()

       GetY0 = m_y0

End Function

Function SetY0( yaw )

       m_y0 = yaw

End Function

'==================
' Y1 property
'==================
Function GetY1()

       GetY1 = m_y1

End Function

Function SetY1( yaw )

       m_y1 = yaw

End Function

'==================
' Y2 property
'==================
Function GetY2()

       GetY2 = m_y2

End Function

Function SetY2( yaw )

       m_y2 = yaw

End Function

'==================
' YS property
'==================
Function GetYS()

       GetYS = m_ys

End Function

Function SetYS( yaw )

       m_ys = yaw

End Function

'==================
' YC property
'==================
Function GetYC()

       GetYC = m_yc

End Function

Function SetYC( yaw )

       m_yc = yaw

End Function

'==================
' P0 property
'==================
Function GetP0()

       GetP0 = m_p0

End Function

Function SetP0( pitch )

       m_p0 = pitch

End Function

'==================
' P1 property
'==================
Function GetP1()

       GetP1 = m_p1

End Function

Function SetP1( pitch )

       m_p1 = pitch

End Function

'==================
' P2 property
'==================
Function GetP2()

       GetP2 = m_p2

End Function

Function SetP2( pitch )

       m_p2 = pitch

End Function

'==================
' PS property
'==================
Function GetPS()

       GetPS = m_ps

End Function

Function SetPS( pitch )

       m_ps = pitch

End Function

'==================
' PC property
'==================
Function GetPC()

       GetPC = m_pc

End Function

Function SetPC( pitch )

       m_pc = pitch

End Function

'======================================================
'  Copyright 2005-2011, Analytical Graphics, Inc.          
' =====================================================
'======================================================
'  Copyright 2006, Analytical Graphics, Inc.          
' =====================================================

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
Dim m_AgAttrScope

Set m_AgAttrScope	 = Nothing

'======================================
' Declare Global 'Attribute' Variables
'======================================
Dim m_objectName
Dim m_controlName
Dim m_currentValue
Dim m_initialValue
Dim m_step
Dim m_dimension
Dim m_internalUnit
Dim m_active
Dim m_type

m_objectName = ""
m_controlName = ""
m_currentValue = 0.0
m_initialValue = 0.0
m_step = 100.0
m_active = false

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		'===========================
		' General Plugin attributes
		'===========================
		Call AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "Active", "True if used by algorithm", "IsActive", eFlagNone )
		
		' note: m_dimension and m_internalUnits are set by the plugin point before
		' this method is called
		if (m_dimension = "") Then
			' dimensionless
			Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "Step", "Step when searching for bounds", "Step", eFlagNone )
		elseif (m_dimension = "DateFormat") then
			' the step is in timeUnits (seconds) if the control is a date
			Call AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "Step", "Step when searching for bounds", "Step", "Time", "Seconds", "Seconds", eFlagNone )
		else
			' attr quantity, use internal units that were given
			Call AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "Step", "Step when searching for bounds", "Step", m_dimension, m_internalUnit, m_internalUnit, eFlagNone )
		End If
		
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

'=======================
' ObjectName property
'=======================
Function GetObjectName()

       GetObjectName = m_objectName

End Function

Function SetObjectName( val )

       m_objectName = val

End Function

'=======================
' ControlName property
'=======================
Function GetControlName()

       GetControlName = m_controlName

End Function

Function SetControlName( val )

       m_controlName = val

End Function

'=======================
' ControlType property
'=======================
Function GetControlType()

       GetControlType = m_type

End Function

Function SetControlType( val )

       m_type = val

End Function

'=======================
' CurrentValue property
'=======================
Function GetCurrentValue()

       GetCurrentValue = m_currentValue

End Function

Function SetCurrentValue( val )

       m_currentValue = val

End Function

'=======================
' InitialValue property
'=======================
Function GetInitialValue()

       GetInitialValue = m_initialValue

End Function

Function SetInitialValue( val )

       m_initialValue = val

End Function

'=======================
' Dimension property
'=======================
Function GetDimension()

       GetDimension = m_dimension

End Function

Function SetDimension( val )

	   if (m_dimension <> val) then
			' reset the attr scope so a new one will be made with the new dimension the next time
			' it is needed
			set m_AgAttrScope = Nothing
	   end if

       m_dimension = val

End Function

'=======================
' InternalUnit property
'=======================
Function GetInternalUnit()

       GetInternalUnit = m_internalUnit

End Function

Function SetInternalUnit( val )

       m_internalUnit = val

End Function

'=======================
' Step property
'=======================
Function GetStep()

       GetStep = m_step

End Function

Function SetStep( val )

       m_step = val

End Function

'=======================
' IsActive property
'=======================
Function IsActive()

       IsActive = m_active

End Function

Function SetActive( val )

       m_active = val

End Function

'======================================================
'  Copyright 2006, Analytical Graphics, Inc.          
' =====================================================
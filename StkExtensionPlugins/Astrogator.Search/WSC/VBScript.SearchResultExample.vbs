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
Dim m_resultName
Dim m_currentValue
Dim m_valid
Dim m_desired
Dim m_tolerance
Dim m_dimension
Dim m_internalUnit
Dim m_active

m_objectName = ""
m_resultName = ""
m_currentValue = 0.0
m_desired = 0.0
m_tolerance = 0.01
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
		if (m_dimension = "") then
			' dimensionless - desired value and tolerance are just attr doubles
			Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "DesiredValue", "Desired Value", "DesiredValue", eFlagNone )
			Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "Tolerance", "Tolerance", "Tolerance", eFlagNone )
		elseif (m_dimension = "DateFormat") then
			' date, set desired value as an attrDate and tolerance as an attrQuantity in seconds
			Call AgAttrBuilder.AddDateDispatchProperty ( m_AgAttrScope, "DesiredValue", "Desired Value", "DesiredValue", eFlagNone )
			Call AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "Tolerance", "Tolerance", "Tolerance", "Time", "Seconds", "Seconds", eFlagNone )
		else
			' attr quantity, use internal units that were given
			Call AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "DesiredValue", "Desired Value", "DesiredValue", m_dimension, m_internalUnit, m_internalUnit, eFlagNone )
			Call AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "Tolerance", "Tolerance", "Tolerance", m_dimension, m_internalUnit, m_internalUnit, eFlagNone )
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

'==================
' Name property
'==================
Function GetName()

	GetName = m_Name

End function

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
' ResultName property
'=======================
Function GetResultName()

       GetResultName = m_resultName

End Function

Function SetResultName( val )

       m_resultName = val

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
' IsValid property
'=======================
Function GetIsValid()

       GetIsValid = m_valid

End Function

Function SetIsValid( val )

       m_valid = val

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
			set m_AgAttrScope = nothing
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
' DesiredValue property
'=======================
Function GetDesiredValue()

       GetDesiredValue = m_desired

End Function

Function SetDesiredValue( val )

       m_desired = val

End Function

'=======================
' Tolerance property
'=======================
Function GetTolerance()

       GetTolerance = m_tolerance

End Function

Function SetTolerance( val )

       m_tolerance = val

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
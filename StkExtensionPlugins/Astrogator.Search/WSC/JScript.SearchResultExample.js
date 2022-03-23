//=====================================================
//  Copyright 2006-2007, Analytical Graphics, Inc.          
//=====================================================

//==========================================
// AgELogMsgType Enumeration
//==========================================
var eLogMsgDebug	 	= 0;
var eLogMsgInfo 		= 1;
var eLogMsgForceInfo 	= 2;
var eLogMsgWarning 		= 3;
var eLogMsgAlarm 		= 4;

//===========================================
// AgEAttrAddFlags Enumeration
//===========================================
var eFlagNone			= 0;
var eFlagTransparent	= 2;
var eFlagHidden			= 4;
var eFlagTransient		= 8; 
var eFlagReadOnly		= 16;
var eFlagFixed			= 32;

//==========================================
// Declare Global Variables
//==========================================
var m_AgAttrScope			= null;

//======================================
// Declare Global 'Attribute' Variables
//======================================
var m_objectName;
var m_resultName;
var m_currentValue = 0.0;
var m_valid = false;
var m_desired = 0.0;
var m_tolerance = 0.01;
var m_active = false;
var m_dimension;
var m_internalUnit;

//=======================
// GetPluginConfig method
//=======================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();
		
		//===========================
		// General Plugin attributes
		//===========================
		AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "Active", "True if used by algorithm", "IsActive", eFlagNone );

		// note: m_dimension and m_internalUnits are set by the plugin point before
		// this method is called
		if (m_dimension == "")
		{
			// dimensionless - desired value and tolerance are just attr doubles
			AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "DesiredValue", "Desired Value", "DesiredValue", eFlagNone );
			AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "Tolerance", "Tolerance", "Tolerance", eFlagNone );
		}
		else if (m_dimension == "DateFormat")
		{
			// date, set desired value as an attrDate and tolerance as an attrQuantity in seconds
			AgAttrBuilder.AddDateDispatchProperty ( m_AgAttrScope, "DesiredValue", "Desired Value", "DesiredValue", eFlagNone );
			AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "Tolerance", "Tolerance", "Tolerance", "Time", "Seconds", "Seconds", eFlagNone );
		}
		else
		{
			// attr quantity, use internal units that were given
			AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "DesiredValue", "Desired Value", "DesiredValue", m_dimension, m_internalUnit, m_internalUnit, eFlagNone );
			AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "Tolerance", "Tolerance", "Tolerance", m_dimension, m_internalUnit, m_internalUnit, eFlagNone );
		}
	}

	return m_AgAttrScope;
}

//===========================
// VerifyPluginConfig method
//===========================
function VerifyPluginConfig(AgUtPluginConfigVerifyResult)
{   
	var Result = true;
	var Message = "Ok";

	AgUtPluginConfigVerifyResult.Result  = Result;
	AgUtPluginConfigVerifyResult.Message = Message;
}  

//===========================
// Message method
//===========================
function Message( msgType, msg )
{
	if( m_AgUtPluginSite != null)
	{   	
		m_AgUtPluginSite.Message( msgType, msg );
	}
}

//==================
// ObjectName property
//==================
function GetObjectName()
{
	return m_objectName;
}

function SetObjectName( val )
{
	m_objectName = val;
}

//==================
// ResultName property
//==================
function GetResultName()
{
	return m_resultName;
}

function SetResultName( val )
{
	m_resultName = val;
}

//==================
// Current Value property
//==================
function GetCurrentValue()
{
	return m_currentValue;
}

function SetCurrentValue( val )
{
	m_currentValue = val;
}

//==================
// IsValid property
//==================
function GetIsValid()
{
	return m_valid;
}

function SetIsValid( val )
{
	m_valid = val;
}

//==================
// Dimension property
//==================
function GetDimension()
{
	return m_dimension;
}

function SetDimension( val )
{
	if (val != m_dimension)
	{
		// reset the attr scope so a new one will be made with the new dimension the next time
		// it is needed
		m_AgAttrScope = null;
	}

	m_dimension = val;
}

//==================
// InternalUnit property
//==================
function GetInternalUnit()
{
	return m_internalUnit;
}

function SetInternalUnit( val )
{
	m_internalUnit = val
}

//==================
// Desired property
//==================
function GetDesiredValue()
{
	return m_desired;
}

function SetDesiredValue( val )
{
	m_desired = val;
}

//==================
// Tolerance property
//==================
function GetTolerance()
{
	return m_tolerance;
}

function SetTolerance( val )
{
	m_tolerance = val;
}

//==================
// IsActive property
//==================
function IsActive()
{
	return m_active;
}

function SetActive( val )
{
	m_active = val;
}

//=====================================================
//  Copyright 2006-2007, Analytical Graphics, Inc.          
//=====================================================
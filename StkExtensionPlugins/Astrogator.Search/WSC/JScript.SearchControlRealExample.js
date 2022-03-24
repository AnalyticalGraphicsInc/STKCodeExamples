//=====================================================
//  Copyright 2006-2008, Analytical Graphics, Inc.          
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
var m_controlName;
var m_currentValue = 0.0;
var m_initialValue = 0.0;
var m_step = 100.0;
var m_active = false;
var m_dimension;
var m_internalUnit;
var m_type;

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
			// dimensionless
			AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "Step", "Step when searching for bounds", "Step", eFlagNone );
		}
		else if (m_dimension == "DateFormat")
		{
			// the step is in timeUnits (seconds) if the control is a date
			AgAttrBuilder.AddQuantityDispatchProperty2 ( m_AgAttrScope, "Step", "Step when searching for bounds", "Time", "Step", "Seconds", "Seconds", eFlagNone );
		}
		else
		{
            // attr quantity, use internal units that were given
            AgAttrBuilder.AddQuantityDispatchProperty2 (m_AgAttrScope, "Step", "Step when searching for bounds", "Step", m_dimension, m_internalUnit, m_internalUnit, eFlagNone );
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
// ControlName property
//==================
function GetControlName()
{
	return m_controlName;
}

function SetControlName( val )
{
	m_controlName = val;
}

//==================
// ControlType property
//==================
function GetControlType()
{
	return m_type;
}

function SetControlType( val )
{
	m_type = val;
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
// Initial Value property
//==================
function GetInitialValue()
{
	return m_initialValue;
}

function SetInitialValue( val )
{
	m_initialValue = val;
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

	m_dimension = val
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
// Step property
//==================
function GetStep()
{
	return m_step;
}

function SetStep( val )
{
	m_step = val;
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
//  Copyright 2006-2008, Analytical Graphics, Inc.          
//=====================================================
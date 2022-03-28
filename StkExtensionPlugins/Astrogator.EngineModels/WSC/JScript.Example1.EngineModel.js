//=====================================================
//  Copyright 2005-2011, Analytical Graphics, Inc.          
//=====================================================

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
// AgELogMsgType Enumeration
//==========================================
var eLogMsgDebug	 	= 0;
var eLogMsgInfo 		= 1;
var eLogMsgForceInfo 	= 2;
var eLogMsgWarning 		= 3;
var eLogMsgAlarm 		= 4;

//==========================================
// AgEUtTimeScale Enumeration
//==========================================
var eUTC 			= 0;
var eTAI 			= 1;
var eTDT 			= 2;
var eUT1 			= 3;
var eSTKEpochSec 	= 4;
var eTDB 			= 5;
var eGPS 			= 6;

//==========================================
// Declare Global Variables
//==========================================
var m_AgUtPluginSite		= null;
var m_AgAttrScope			= null;
var m_InitTime				= 0.0;

//======================================
// Declare Global 'Attribute' Variables
//======================================
var m_Name	= "JScript.Example1.EngineModel.wsc"   
var m_T0	= 0;
var m_T1	= 0.0001;
var m_T2	= 0.0000001;
var m_Ts	= 0;
var m_Tc	= 0;
var m_Isp	= 3000;
var m_gatorPrv = null;
var m_argOfLat = null;

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
		AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias", "Name", eFlagReadOnly );
				
		//===========================
		// Propagation related
		//===========================
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "T0", "Initial Thrust", "T0", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "T1", "Linear Thrust Coefficient", "T1", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "T2", "Quadratic Thrust Coefficient", "T2", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Ts", "Sine Thrust Coefficient", "TS", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Tc", "Cosine Thrust Coefficient", "TC", 0 );

		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Isp", "Specific Impulse", "ISP", 0 );			
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

	//TODO:  Add Yaw and Pitch Maximum and Minimum Checks here

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

//======================
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;
	
	if (m_AgUtPluginSite != null)
	{
		m_gatorPrv = m_AgUtPluginSite.GatorProvider;
		
		if (m_gatorPrv != null)
		{
			m_argOfLat = m_gatorPrv.ConfigureCalcObject("Argument_of_Latitude");
			
			if (m_argOfLat != null)
			{
				return true;
			}
		}
	}
	
	return false;
}

//======================
// PrePropagate Method
//======================
function PrePropagate(AgGatorPluginResultState)
{
    if (AgGatorPluginResultState != null)
	{
	    var vbArrayTimes = AgGatorPluginResultState.DayCount_Array(eSTKEpochSec);
		var jsArrayTimes = vbArrayTimes.toArray();
		
		m_InitTime = jsArrayTimes[0] * 86400.0 + jsArrayTimes[1];
	}

	return true;
}

//======================
// PreNextStep Function
//======================
function PreNextStep(AgGatorPluginResultState)
{
	return true;
}

//=================
// Evaluate Method
//=================
function Evaluate( AgGatorPluginResultEvalEngineModel )
{
	if( AgGatorPluginResultEvalEngineModel != null )
	{
		var time		= 0.0;
		var thrust		= 0.0;
		var deltaT		= 0.0;
		var argOfLat	= 0.0;
		
		var vbArrayTimes = AgGatorPluginResultEvalEngineModel.DayCount_Array( eSTKEpochSec );
		var jsArrayTimes = vbArrayTimes.toArray();
		
		time = jsArrayTimes[0] * 86400.0 + jsArrayTimes[1];
						
		deltaT = time - m_InitTime;
		
		argOfLat = m_argOfLat.Evaluate(AgGatorPluginResultEvalEngineModel);
			
		thrust = m_T0 + ( m_T1 * deltaT ) + ( m_T2 * deltaT * deltaT ) + ( m_Ts * Math.sin(argOfLat) ) + ( m_Tc * Math.cos(argOfLat) );
					
		AgGatorPluginResultEvalEngineModel.SetThrustAndIsp( thrust , m_Isp);
	}

	return true;
}

//=================
// Free Method
//=================
function Free()
{
	// do nothing
}
    
//==================
// Name property
//==================
function GetName()
{
	return m_Name;
}

//==================
// T0 property
//==================
function GetT0()
{
	return m_T0;
}

function SetT0( val )
{
	m_T0 = val;
}

//=================
// T1 property
//==================
function GetT1()
{
	return m_T1;
}

function SetT1( val )
{
	m_T1 = val;
}

//==================
// T2 property
//==================
function GetT2()
{
	return m_T2;
}

function SetT2( val )
{
	m_T2 = val;
}

//==================
// TS property
//==================
function GetTS()
{
	return m_Ts;
}

function SetTS( val )
{
	m_Ts = val;
}

//==================
// TC property
//==================
function GetTC()
{
	return m_Tc;
}

function SetTC( val )
{
	m_Tc = val;
}

//==================
// Isp property
//==================
function GetISP()
{
	return m_Isp;
}

function SetISP( val )
{
	m_Isp = val;
}

//=====================================================
//  Copyright 2005-2011, Analytical Graphics, Inc.          
//=====================================================
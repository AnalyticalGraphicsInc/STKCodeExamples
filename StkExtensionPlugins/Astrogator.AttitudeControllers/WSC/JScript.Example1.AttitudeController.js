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

//==================================
// AgEEulerSequence Enumeration
//==================================
var e121 = 121;
var e123 = 123;
var e131 = 131;
var e132 = 132;
var e212 = 212;
var e213 = 213;
var e231 = 231;
var e232 = 232;
var e312 = 312;
var e313 = 313;
var e321 = 321;
var e323 = 323;

//==========================================
// AgETimeScale
//==========================================
var eSTKEpochSec		= 4;

//==========================================
// Declare Global Variables
//==========================================
var m_AgUtPluginSite		= null;
var m_AgAttrScope			= null;
var m_InitTime				= 0.0;

//======================================
// Declare Global 'Attribute' Variables
//======================================
var m_Name	= "JScript.Example1.AttitudeController.wsc"   
var m_y0	= 0;
var m_y1	= 0.0001;
var m_y2	= 0.0000001;
var m_ys	= 0;
var m_yc	= 0;
var m_p0	= 0;
var m_p1	= 0.0002;
var m_p2	= 0.00000001;
var m_ps	= 0;
var m_pc	= 0;
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
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Y0", "Initial Yaw", "Y0", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Y1", "Linear Yaw Coefficient", "Y1", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Y2", "Quadratic Yaw Coefficient", "Y2", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Ys", "Sine Yaw Coefficient", "YS", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Yc", "Cosine Yaw Coefficient", "YC", 0 );
				
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "P0", "Initial Pitch", "P0", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "P1", "Linear Pitch Coefficient", "P1", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "P2", "Quadratic Pitch Coefficient", "P2", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Ps", "Sine Pitch Coefficient", "PS", 0 );
		AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "Pc", "Cosine Pitch Coefficient", "PC", 0 );
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
function PrePropagate( AgGatorPluginResultAttCtrl )
{
	if( AgGatorPluginResultAttCtrl != null )
	{
			AgGatorPluginResultAttCtrl.SetRefAxes("Satellite VNC(Earth)");
			
			var vbArrayTimes = AgGatorPluginResultAttCtrl.DayCount_Array( eSTKEpochSec );
			var jsArrayTimes = vbArrayTimes.toArray();
		
			m_InitTime = jsArrayTimes[0] * 86400.0 + jsArrayTimes[1];
	}

	return true;
}

//======================
// PreNextStep Function
//======================
function PreNextStep( AgGatorPluginResultAttCtrl )
{
	return true;
}

//=================
// Evaluate Method
//=================
function Evaluate( AgGatorPluginResultAttCtrl )
{
	if( AgGatorPluginResultAttCtrl != null )
	{
		var time		= 0.0;
		var yawAngle	= 0.0;
		var pitchAngle	= 0.0;
		var deltaT		= 0.0;
		var argOfLat	= 0.0;
			
		var vbArrayTimes = AgGatorPluginResultAttCtrl.DayCount_Array( eSTKEpochSec );
		var jsArrayTimes = vbArrayTimes.toArray();
		
		time = jsArrayTimes[0] * 86400.0 + jsArrayTimes[1];
			
		deltaT = time - m_InitTime;
		
		argOfLat = m_argOfLat.Evaluate(AgGatorPluginResultAttCtrl);
			
		yawAngle = m_y0 + ( m_y1 * deltaT ) + ( m_y2 * deltaT * deltaT ) + ( m_ys * Math.sin(argOfLat) ) + ( m_yc * Math.cos(argOfLat) );
			
		pitchAngle = m_p0 + ( m_p1 * deltaT ) + ( m_p2 * deltaT * deltaT ) + ( m_ps * Math.sin(argOfLat) ) + ( m_pc * Math.cos(argOfLat) );
			
		AgGatorPluginResultAttCtrl.EulerRotate( e321, yawAngle, pitchAngle, 0 );
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
// Y0 property
//==================
function GetY0()
{
	return m_y0;
}

function SetY0( yaw )
{
	m_y0 = yaw;
}

//=================
// Y1 property
//==================
function GetY1()
{
	return m_y1;
}

function SetY1( yaw )
{
	m_y1 = yaw;
}

//==================
// Y2 property
//==================
function GetY2()
{
	return m_y2;
}

function SetY2( yaw )
{
	m_y2 = yaw;
}

//==================
// YS property
//==================
function GetYS()
{
	return m_ys;
}

function SetYS( yaw )
{
	m_ys = yaw;
}

//==================
// YC property
//==================
function GetYC()
{
	return m_yc;
}

function SetYC( yaw )
{
	m_yc = yaw;
}

//==================
// P0 property
//==================
function GetP0()
{
	return m_p0;
}

function SetP0( pitch )
{
	m_p0 = pitch;
}

//==================
// P1 property
//==================
function GetP1()
{
	return m_p1;
}

function SetP1( pitch )
{
	m_p1 = pitch;
}

//==================
// P2 property
//==================
function GetP2()
{
	return m_p2;
}

function SetP2( pitch )
{
	m_p2 = pitch;
}

//==================
// PS property
//==================
function GetPS()
{
	return m_ps;
}

function SetPS( pitch )
{
	m_ps = pitch;
}

//==================
// PC property
//==================
function GetPC()
{
	return m_pc;
}

function SetPC( pitch )
{
	m_pc = pitch;
}

//=====================================================
//  Copyright 2005-2011, Analytical Graphics, Inc.          
//=====================================================
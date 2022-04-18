//=====================================================
//  Copyright 2018-2019, Analytical Graphics, Inc.          
//=====================================================

//=====================================================
// This example models an exponential atmospheric
// density consistent with STK's exponential models.
//=====================================================

//==========================================
// Log Msg Type Enumeration
//==========================================
var eLogMsgDebug	 	= 0;
var eLogMsgInfo 		= 1;
var eLogMsgForceInfo 	= 2;
var eLogMsgWarning 		= 3;
var eLogMsgAlarm 		= 4;

//==========================================
// AgEAttrAddFlags Enumeration
//==========================================
var eFlagNone			= 0;
var eFlagTransparent	= 2;
var eFlagHidden			= 4;
var eFlagTransient		= 8;  
var eFlagReadOnly		= 16;
var eFlagFixed = 32;

//==========================================
// Declare Global Variables
//==========================================
var m_AgUtPluginSite		= null;
var m_AgAttrScope			= null;

var m_MsgCntr				= -1;
var m_Enabled				= true;
var m_DebugMode				= false;
var m_MsgInterval			= 500;

var m_cbName				= "Earth";
var m_refDen				= 1.784e-11;        // kg/m^3
var m_refAlt				= 300.0* 1000.0;	// meters;
var m_scaleAlt				= 20.0 * 1000.0;	// meters
var m_lowestValidAlt		= 90.0 * 1000.0;	// meters
var m_Density				= -1;
var m_computesTemp			= false;
var m_computesPressure		= false;
var m_userIndex				= 0;
var m_MaxScaleHeights		= 7.0;
var m_maxHeightMsg			= 0;
var m_lowAltMsg				= 0;

//==========================================
// Message handling
//==========================================
function Message ( severity, msg )
{
	if ( m_AgUtPluginSite )
	{
		m_AgUtPluginSite.Message( severity, msg );
	}
}

function DebugMsg ( msg )
{
	if ( m_DebugMode )
	{
		if ( m_MsgCntr % m_MsgInterval == 0 )
		{
			Message( eLogMsgDebug, msg );
		}
	}
}

//========================
// GetPluginConfig method
//========================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();

		// This plugin's specific attrs
		AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "CentralBodyName", "CentralBody Name", "CentralBodyName", eFlagNone);
		AgAttrBuilder.AddDoubleDispatchProperty( m_AgAttrScope, "RefDensity", "Reference Density kg/m^3", "RefDensity", eFlagNone);
		AgAttrBuilder.AddQuantityMinDispatchProperty2( m_AgAttrScope, "RefAltitude", "Reference Altitude", "RefAltitude", "DistanceUnit", "Kilometers", "Meters", 0.0, eFlagNone);
		AgAttrBuilder.AddQuantityMinDispatchProperty2( m_AgAttrScope, "ScaleAltitude", "Scale Altitude", "ScaleAltitude", "DistanceUnit", "Kilometers", "Meters", 0.0, eFlagNone);
		AgAttrBuilder.AddQuantityMinDispatchProperty2( m_AgAttrScope, "LowestValidAlt", "Lowest Valid Altitude", "LowestValidAltitude", "DistanceUnit", "Kilometers", "Meters", 0.0, eFlagNone);
		AgAttrBuilder.AddIntDispatchProperty( m_AgAttrScope, "MaxScaleHeights", "Max number of scale heights to allow", "MaxScaleHeights", 0);

		// General plugin attrs
		AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "PluginEnabled", "If the plugin is enabled or has experienced an error", "Enabled", eFlagNone);
		AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "DebugMode", "Turn debug messages on or off", "DebugMode", eFlagNone);

		// Messaging attr
		AgAttrBuilder.AddIntDispatchProperty( m_AgAttrScope, "MessageInterval", "The interval at which to send messages during propagation in Debug mode", "MsgInterval", eFlagNone);
	}

	return m_AgAttrScope;
}  

//===========================
// VerifyPluginConfig method
//===========================
function VerifyPluginConfig( AgUtPluginConfigVerifyResult )
{
    var Result = true;
    var Message = "Ok";

	AgUtPluginConfigVerifyResult.Result  = Result;
	AgUtPluginConfigVerifyResult.Message = Message;
}

//======================
// Register Method
//======================
function Register( AgAsDensityModelResultRegister ) // find out what 'input' is
{
	if( AgAsDensityModelResultRegister )
	{
		if( m_DebugMode == true )
		{
			AgAsDensityModelResultRegister.Message( eLogMsgInfo, "Register() called" );
		}
	}
} 

//======================
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;
	
	if( m_AgUtPluginSite )
	{
		if ( m_DebugMode == true )
		{
			if ( m_Enabled == true )
			{
				Message( eLogMsgInfo, "Init(): Enabled" );
			}
			else
			{
				Message( eLogMsgInfo, "Init(): Disabled because Enabled flag is false" );
			}
		}
		else if(m_Enabled == false)
		{
			Message( eLogMsgAlarm, "Init(): Disabled because Enabled flag is false" );
		}
	}
	
	m_maxHeightMsg = 0;
	m_lowAltMsg = 0;
	m_MsgCntr = -1;
   
    return m_Enabled;
}

//======================
// Evaluate Method
//======================
function Evaluate( AgAsDensityModelResultEval )
{
	m_MsgCntr++;

	if(m_Enabled == true && AgAsDensityModelResultEval )
	{
		m_Enabled = setDensity( AgAsDensityModelResultEval );
	}
	return m_Enabled;
}

//======================
// Local setDensity Method
//======================
function setDensity( AgAsDensityModelResultEval )
{
	AgAsDensityModelResultEval.SetDensity( 0.0 );
	
	var enabled = false;
	var altitude = AgAsDensityModelResultEval.Altitude;
	if( altitude )
	{
		if(altitude < m_lowestValidAlt)
		{
			if(m_lowAltMsg == 0)
			{
				var altKm3 = altitude / 1000.0;
				var altTrunc3 = altKm3.toFixed(3);
				var lowAltKm = m_lowestValidAlt / 1000.0;
				var lowestValidAltTrunc = lowAltKm.toFixed(3);
				var msg3 = "setDensity: altitude " + altTrunc3 + " is less than minimum valid altitude ( " + lowestValidAltTrunc + " km). Keeping density constant below this height.";
				Message( eLogMsgWarning, msg3 );
				
				m_lowAltMsg = 1;
			}
			
			altitude = m_lowestValidAlt;
		}
		
		var diffAlt = m_refAlt - altitude;
		var expArg = diffAlt / m_scaleAlt;
		
		if( m_DebugMode == true )
		{
			var msg = "setDensity: alt= " + altitude + ", expArg = " + expArg;
			DebugMsg(msg);
		}
		
		enabled = true;
		
		if(expArg < -700)
		{
			m_Density = 0.0;
		}
		else 
		{
			if(expArg > m_MaxScaleHeights)
			{
				if(m_maxHeightMsg == 0)
				{
					var altKm = altitude / 1000.0;
					var altTrunc = altKm.toFixed(3);
					var expArgTrunc = expArg.toFixed(3);
					var msg = "setDensity: scaleHeight " + expArgTrunc + " exceeds maximum allowed ( " + m_MaxScaleHeights + "), alt= " + altTrunc + " km. Keeping density constant at maxScaleHeight.";
					Message( eLogMsgAlarm, msg );
					
					m_maxHeightMsg = 1;
					
					// demonstrate we can get at flux file values, if a flux file is being used
					try
					{
						// may not be using a file - throws in that case
						var vbFluxArray = AgAsDensityModelResultEval.CurrentAtmFlux_Array();
						var jsFluxArray = vbFluxArray.toArray();
						var ii;
						var msg2 = "";
						for(ii=0; ii<jsFluxArray.length; ++ii)
						{
							msg2 = msg2 + jsFluxArray[ii] + ", ";
						}
						Message( eLogMsgDebug, msg2 );
					}
					catch(err)
					{
						Message( eLogMsgAlarm, err.message );
					}
				}
				
				expArg = m_MaxScaleHeights;
			}
			m_Density = m_refDen * Math.exp(expArg);
		}
		if( m_Density )
		{
			AgAsDensityModelResultEval.SetDensity( m_Density );
		}
	}
	
	return enabled;
}

//===========================================================
// Free Method
//===========================================================
function Free()
{
	if( m_AgUtPluginSite != null )
	{	
		m_AgUtPluginSite = null;
	}
	
	return true;
}

// ============================================================
//  Computes Temperature property
// ============================================================
function ComputesTemperature()
{
	return m_computesTemp;
}

// ============================================================
//  Computes Pressure property
// ============================================================
function ComputesPressure()
{
	return m_computesPressure;
}

// ============================================================
//  New methods
// ============================================================
function CentralBody()
{
	return m_cbName;
}
function UsesAugmentedSpaceWeather()
{
	return false;
}
function GetLowestValidAltitude()
{
	return m_lowestValidAlt;	// meters
}

function AtmFluxLags()
{
	// this fctn uses [in/out] parameters not supported by IDispatch implemented by JScript, VBScript, Perl
	return;
}
function AugmentedAtmFluxLags()
{
	// this fctn uses [in/out] parameters not supported by IDispatch implemented by JScript, VBScript, Perl
	return;
}

// ============================================================
//  Enabled property
// ============================================================
function GetEnabled()
{
	return m_Enabled;
}

function SetEnabled( input )
{
	m_Enabled = input;
}

// ======================================================
//  MsgStatus property
// ======================================================
function GetDebugMode()
{
	return m_DebugMode;
}

function SetDebugMode( input )
{
    m_DebugMode = input;
}

// =======================================================
//  EvalMsgInterval property
// =======================================================
function GetMsgInterval()
{
	return m_MsgInterval;
}

function SetMsgInterval( input )
{
	m_MsgInterval = input;
}

// =======================================================
//  RefDen property
// =======================================================
function GetRefDensity()
{
	return m_refDen;
}

function SetRefDensity( input )
{
	m_refDen = input;
}

// =======================================================
//  RefAlt property
// =======================================================
function GetRefAltitude()
{
	return m_refAlt;
}

function SetRefAltitude( input )
{
	m_refAlt = input;
}

// =======================================================
//  MaxScaleHeights property
// =======================================================
function GetMaxScaleHeights()
{
	return m_MaxScaleHeights;
}

function SetMaxScaleHeights( input )
{
	m_MaxScaleHeights = input;
}

// =======================================================
//  ScaleAlt property
// =======================================================
function GetScaleAltitude()
{
	return m_scaleAlt;
}

function SetScaleAltitude( input )
{
	m_scaleAlt = input;
}

// =======================================================
//  LowestValidAltitude property
// =======================================================
function GetLowestValidAlt()
{
	return m_lowestValidAlt;
}

function SetLowestValidAlt( input )
{
	m_lowestValidAlt = input;
}

// =======================================================
//  CentralBodyName property
// =======================================================
function GetCentralBodyName()
{
	return m_cbName;
}

function SetCentralBodyName( input )
{
	m_cbName = input;
}

//=====================================================
//  Copyright 2018-2019, Analytical Graphics, Inc.
//=====================================================

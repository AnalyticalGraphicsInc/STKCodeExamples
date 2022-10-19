//=====================================================
//  Copyright YYYY, YOUR COMPANY NAME GOES HERE         
//=====================================================

//////////////////////////////////////////////////////
////
//// Begin Enumeration Declarations
////

//==========================================
// AgEUtFrame Enumeration
//==========================================
var eUtFrameInertial 		= 0;
var eUtFrameFixed 			= 1;
var eUtFrameLVLH 			= 2;
var eUtFrameNTC 			= 3;
var eUtFrameICRF 			= 4;
var eUtFrameJ2000 			= 5;

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
// AgEUtSunPosType Enumeration
//==========================================
var eApparentToTrueCB 	= 0;
var eApparent 			= 1;
var eTrue 				= 2;
var eSRP 				= 3;

//=================================
// AgEAccelType Enumeration
//=================================

var eTotalAccel 			= 0;
var eTwoBodyAccel 			= 1;
var eGravityAccel 			= 2;
var ePerturbedGravityAccel 	= 3;
var eSolidTidesAccel 		= 4;
var eOceanTidesAccel 		= 5;
var eDragAccel 				= 6;
var eSRPAccel 				= 7;
var eThirdBodyAccel 		= 8;
var eGenRelativityAccel 	= 9;
var eAddedAccel 			= 10;
var eAlbedoAccel 			= 11;
var eThermalRadiationPressureAccel	= 12;

// =================================
//  AgEForceModelType Enumeration
// =================================

var eGravityModel 			= 0;
var eSolidTidesModel		= 1;
var eOceanTidesModel 		= 2;
var eDragModel				= 3;
var eSRPModel		 		= 4;
var eThirdBodyModel 		= 5;
var eGenRelativityModel 	= 6;
var eAlbedoModel 			= 7;
var eThermalRadiationPressureModel 	= 8;
var eDensityModel 			= 9;

////
//// End Enumeration Declarations
////
//////////////////////////////////////////////////////

//////////////////////////////////////////////////////
////
//// Begin Plugin Variable Declarations
////

var m_AgUtPluginSite	= null;
var m_AgAttrScope		= null;

var m_Name				= "MyPlugin.Hpop.JScript.wsc";
var m_MyProperty		= 0.0;

////
//// End Plugin Variable Declarations
////
//////////////////////////////////////////////////////

//========================
// GetPluginConfig method
//========================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();
				
		// Register all plugin parameters here, using AgAttrBuilder
		
		// Each parameter has a Type (double, bool, integer, filename, string, etc.)
		
		// Example: registration of MyProperty as a double
		
		AgAttrBuilder.AddDoubleDispatchProperty( 
			m_AgAttrScope, 
			"MyProperty", 
			"A description of MyProperty", 
			"MyProperty",
			eAddFlagNone );
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

    // Perform checks of the ranges of the plugin parameter data
    
    // If there is an error in the settings, then set Result to false
    // and provide a Message to be communicated back to the user
    // indicating the problem

	AgUtPluginConfigVerifyResult.Result  = Result;
	AgUtPluginConfigVerifyResult.Message = Message;
} 

//======================
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;

	var retVal = true;

	// Implement any Init activities here
	// If the plugin cannot initialize (say, because it cannot open a file it needs)
	// then it can return false and the plugin won't be be used
	
	// retVal = false;

    return retVal;
} 

//======================
// PrePropagate Method
//======================
function PrePropagate( AgAsHpopPluginResult )
{
	// Implement any PrePropogate activities here
	
	return true;
}


//======================
// PreNextStep Function
//======================
function PreNextStep( AgAsHpopPluginResult )
{
	var retVal = true;

	// Implement any PreNextStep activities here

	// If you have none, you can tell the application not to even call this function
	// by returning false instead of true

	// retVal = false;

	return retVal;
}

//=================
// Evaluate Method
//=================
function Evaluate( AgAsHpopPluginResultEval )
{
	var retVal = true;

	// Implement any Evaluate activities here

	// If you have none, you can tell the application not to even call this function
	// by returning false instead of true

	// retVal = false;
	
	return retVal;
}

//=================
// PostEvaluate Method
//=================
function PostEvaluate( AgAsHpopPluginResultPostEval )
{
	var retVal = true;
	
	// Implement any PostEvaluate activities here

	// If you have none, you can tell the application not to even call this function
	// by returning false instead of true

	// retVal = false;
		
	return retVal;
}

//========================================================
// PostPropagate Method
//========================================================
function PostPropagate( AgAsHpopPluginResult)
{
	// Implement any PostPropogate activities here
		
	return true;
}


//===========================================================
// Free Method
//===========================================================
function Free()
{
	m_AgUtPluginSite  = null;
}

//=============================================================
// Name Property
//=============================================================
function GetName()
{
	return m_Name;
}

function SetName( name )
{
	m_Name = name;
}

//=======================================================
// MyProperty property
//=======================================================
function GetMyProperty()
{
	return m_MyProperty;
}

function SetMyProperty( value )
{
	m_MyProperty = value;
} 
//=====================================================
//  Copyright YYYY, YOUR COMPANY NAME GOES HERE          
//=====================================================

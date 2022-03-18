//=====================================================
//  Copyright 2012, Analytical Graphics, Inc.          
//=====================================================

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
// Declare Global Variables
//==========================================
var m_Site			= null;
var m_StkRootObject = null;
var m_AgAttrScope	= null;
var m_DisplayName   = "VGT.Vector.JScript.Example1";
var m_MyDouble      = 10.123;
var m_MyString      = "test";

var m_CalcToolProvider = null;
var m_VectorToolProvider = null;

var m_moonConfiguredVector = null;
var m_sunConfiguredVector = null;

function Message(logMsgType, msg)
{
    if(m_Site != null)
    {
        m_Site.Message( logMsgType, m_DisplayName + ": " + msg );
    }
}

function Init(AgUtPluginSite)
{
    m_Site = AgUtPluginSite;

    Message(eLogMsgInfo, "Init() Entered");

    if (m_Site != null) {
        // Get a pointer to the STK Object Model root object
        m_StkRootObject = m_Site.StkRootObject;
    }

    Message(eLogMsgInfo, "Init() Exited");

    return true;
}

function Register( Result )
{
    var objPath = "[" + Result.ObjectPath + "]";
    Message(eLogMsgInfo, objPath + "Register() Entered");

    Result.ShortDescription = m_DisplayName + " Example";
    Result.LongDescription = m_DisplayName + " Example";

    Result.SetRefAxes("ICRF", "CentralBody/Earth");

    Message(eLogMsgInfo, objPath + "Register() Exited");
}

function Reset( Result )
{
    var objPath = "[" + Result.ObjectPath + "]";
    Message(eLogMsgInfo, objPath + "Reset() Entered");

    m_CalcToolProvider = Result.CalcToolProvider;
    m_VectorToolProvider = Result.VectorToolProvider;

    m_moonConfiguredVector = m_VectorToolProvider.ConfigureVector("Moon", "<MyObject>", "ICRF", "<MyObject>");
    m_sunConfiguredVector = m_VectorToolProvider.ConfigureVector("Sun", "<MyObject>", "ICRF", "<MyObject>");

    Message(eLogMsgInfo, objPath + "Reset() Exited");

    return true;
}

function Evaluate( Result )
{
    if (m_moonConfiguredVector != null && m_sunConfiguredVector != null)
    {
        var moonVectorArray = m_moonConfiguredVector.CurrentValue_Array(Result).toArray();
        var sunVectorArray = m_sunConfiguredVector.CurrentValue_Array(Result).toArray();

        var x1 = moonVectorArray[0];
        var y1 = moonVectorArray[1];
        var z1 = moonVectorArray[2];

        var x2 = sunVectorArray[0];
        var y2 = sunVectorArray[1];
        var z2 = sunVectorArray[2];

        // For this example, the vector is the average vector of the sun and the moon.
        Result.SetVectorComponents((x2 + x1) / 2, (y2 + y1) / 2, (z2 + z1) / 2);
    }

    return true;
}

function Free()
{
    Message( eLogMsgInfo, "Free()" );
	
    m_Site = null;
    m_StkRootObject = null;
    m_CalcToolProvider = null;
    m_VectorToolProvider = null;
    m_moonConfiguredVector = null;
    m_sunConfiguredVector = null;
}

//=======================
// GetPluginConfig method
//=======================
function GetPluginConfig(AgAttrBuilder)
{
    Message(eLogMsgInfo, "GetPluginConfig() Entered");

    if (m_AgAttrScope == null) {
        m_AgAttrScope = AgAttrBuilder.NewScope();

        //===========================
        // General Plugin attributes
        //===========================
        AgAttrBuilder.AddStringDispatchProperty(m_AgAttrScope, "MyString", "A string", "MyString", eFlagReadOnly);
        AgAttrBuilder.AddDoubleDispatchProperty(m_AgAttrScope, "MyDouble", "A double", "MyDouble", eFlagNone);
    }

    Message(eLogMsgInfo, "GetPluginConfig() Exited");

    return m_AgAttrScope;
}

//===========================
// VerifyPluginConfig method
//===========================
function VerifyPluginConfig(VerifyResult)
{
    Message(eLogMsgInfo, "VerifyPluginConfig() Entered");

    var Result = true;
    var Message = "Ok";

    VerifyResult.Result = Result;
    VerifyResult.Message = Message;

    Message(eLogMsgInfo, "VerifyPluginConfig() Exited");
}

//==================
// MyString property
//==================
function GetMyString()
{
    return m_MyString;
}

function SetMyString(val)
{
    m_MyString = val;
}

//==================
// MyDouble property
//==================
function GetMyDouble()
{
    return m_MyDouble;
}

function SetMyDouble(val)
{
    m_MyDouble = val;
}


//=====================================================
//  Copyright 2012, Analytical Graphics, Inc.          
//=====================================================

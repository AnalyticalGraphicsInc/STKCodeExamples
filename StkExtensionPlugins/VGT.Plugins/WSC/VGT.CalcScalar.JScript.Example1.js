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
var m_AgUtPluginSite            = null;
var m_StkRootObject = null;
var m_AgAttrScope = null;
var m_CalcToolProvider = null;
var m_VectorToolProvider = null;
var m_DisplayName   = "VGT.CalcScalar.JScript.Example1";
var m_MyDouble      = 10.123;
var m_MyString      = "test";

function Message(logMsgType, msg)
{
    if(m_AgUtPluginSite != null)
    {
        m_AgUtPluginSite.Message( logMsgType, m_DisplayName + ": " + msg );
    }
}

function Init(AgUtPluginSite)
{
    m_AgUtPluginSite = AgUtPluginSite;

    Message(eLogMsgInfo, "Init() Entered");

    if (m_AgUtPluginSite != null) {
        // Get a pointer to the STK Object Model root object
        m_StkRootObject = m_AgUtPluginSite.StkRootObject;
    }

    Message(eLogMsgInfo, "Init() Exited");

    return true;
}

function Register( Result )
{
    Message(eLogMsgInfo, m_DisplayName + ".Register() Entered:");

    var objPath = "[" + Result.ObjectPath + "]";

    Result.ShortDescription = "Test short Desc: Component created using " + m_DisplayName + " " + objPath;
    Result.LongDescription = "Test long Desc: Component created using " + m_DisplayName + " " + objPath;

    var oPath = Result.ObjectPath;
    var parentPath = Result.ParentPath;
    var grandParentPath = Result.GrandParentPath;

    Message(eLogMsgInfo, objPath + " Register() [objPath=" + oPath + "] [parentPath=" + parentPath + "] [grandParentPath=" + grandParentPath + "]");

    Message(eLogMsgInfo, m_DisplayName + ".Register() Exited:");
}

function Reset( Result )
{
    var objPath = "[" + Result.ObjectPath + "]";

    Message(eLogMsgInfo, m_DisplayName + ".Reset() Entered:");

    m_CalcToolProvider = Result.CalcToolProvider;
    m_VectorToolProvider = Result.VectorToolProvider;

    m_ObjectTrajectoryCatesianX = m_CalcToolProvider.GetCalcScalar("Trajectory(CBF).Cartesian.X", "<MyObject>");
    m_ObjectTrajectoryCatesianZ = m_CalcToolProvider.GetCalcScalar("Trajectory(CBF).Cartesian.Z", "<MyObject>");

    Message(eLogMsgInfo, m_DisplayName + ".Reset() Exited:");

    return true;
}

function Evaluate( Result )
{
    if (m_ObjectTrajectoryCatesianX != null && m_ObjectTrajectoryCatesianZ != null)
    {
        var xArray = m_ObjectTrajectoryCatesianX.CurrentValue_Array(Result);
        var zArray = m_ObjectTrajectoryCatesianZ.CurrentValue_Array(Result);

        var x = xArray.toArray()[0];
        var z = zArray.toArray()[0];

        Result.SetValue(x + z);
    }

    return true;
}

function Free()
{
    Message(eLogMsgInfo, m_DisplayName + ".Free()");

    m_CalcToolProvider = null;
    m_VectorToolProvider = null;
    m_ObjectTrajectoryCatesianX = null;
    m_ObjectTrajectoryCatesianZ = null;
	m_AgUtPluginSite = null;
}

//=======================
// GetPluginConfig method
//=======================
function GetPluginConfig( AgAttrBuilder )
{
    //Message(eLogMsgInfo, "GetPluginConfig() Entered");

    if( m_AgAttrScope == null )
    {
        m_AgAttrScope = AgAttrBuilder.NewScope();
        
        //===========================
        // General Plugin attributes
        //===========================
        AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "MyString", "A string", "MyString", eFlagReadOnly );
        AgAttrBuilder.AddDoubleDispatchProperty  ( m_AgAttrScope, "MyDouble", "A double", "MyDouble", eFlagNone );
    }

    //Message(eLogMsgInfo, "GetPluginConfig() Exited");

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

    VerifyResult.Result  = Result;
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

function SetMyString( val )
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

function SetMyDouble( val )
{
    m_MyDouble = val;
}

//=====================================================
//  Copyright 2012, Analytical Graphics, Inc.          
//=====================================================

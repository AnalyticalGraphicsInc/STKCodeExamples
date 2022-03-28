//=====================================================
//  Copyright 2005, Analytical Graphics, Inc.          
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

//==========================================
// Declare Global Variables
//==========================================
var m_AgUtPluginSite		= null;
var m_AgStkPluginSite		= null;
var m_AgAttrScope			= null;
var m_CrdnPluginProvider	= null;
var m_CrdnConfiguredVector	= null;

//======================================
// Declare Global 'Attribute' Variables
//======================================
var m_Name					= "JScript.Example1.Hpop.wsc";
var m_Enabled				= true;
var m_VectorName			= "Periapsis";
var m_SRPArea				= 0.0;
var m_SrpIsOn				= false;

var m_AccelRefFrame			= 3;

var m_AccelX				= 0.0;
var m_AccelY				= 0.07;
var m_AccelZ				= 0.0;

var m_MsgStatus				= false;
var m_EvalMsgInterval		= 5000;
var m_PostEvalMsgInterval	= 5000;
var m_PreNextMsgInterval	= 1000;

var m_PreNextCntr			= 0;
var m_EvalCntr				= 0;
var m_PostEvalCntr			= 0;


//========================
// GetPluginConfig method
//========================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();
		
		//===========================
		// General Plugin attributes
		//===========================
		AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias",                 "Name",       0 );
		AgAttrBuilder.AddBoolDispatchProperty  ( m_AgAttrScope, "PluginEnabled",     "If the plugin is enabled or has experience an error", "Enabled",    0 );
		AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "VectorName", "Vector Name that affects the srp area",              "VectorName", 0 );
				
		//===========================
		// Propagation related
		//===========================
		AgAttrBuilder.AddChoicesDispatchProperty( m_AgAttrScope, "AccelRefFrame", "Acceleration Reference Frame",    "AccelRefFrame", GetAccelRefFrameChoices() );
		AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "AccelX",         "Acceleration in the X direction", "AccelX",        0 );
		AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "AccelY",         "Acceleration in the Y direction", "AccelY",        0 );
		AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "AccelZ",         "Acceleration in the Z direction", "AccelZ",        0 );
				
		//==============================
		// Messaging related attributes
		//==============================
		AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "UsePropagationMessages",     "Send messages to the message window during propagation",                               "MsgStatus",           0 );
		AgAttrBuilder.AddIntDispatchProperty ( m_AgAttrScope, "EvaluateMessageInterval",  "The interval at which to send messages from the Evaluate method during propagation", "EvalMsgInterval",     0 );
        AgAttrBuilder.AddIntDispatchProperty ( m_AgAttrScope, "PostEvaluateMessageInterval",  "The interval at which to send messages from the PostEvaluate method during propagation", "PostEvalMsgInterval",     0 );
		AgAttrBuilder.AddIntDispatchProperty ( m_AgAttrScope, "PreNextStepMessageInterval", "The interval at which to send messages from the PreNextStep method during propagation", "PreNextMsgInterval", 0 );
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

    if( !( m_AccelX <= 10 && m_AccelX >= -10 ) )
    {
    	Result  = false;
    	Message = "AccelX was not within the range of -10 to +10 meters per second squared";
    }
    else if( !( m_AccelY <= 10 && m_AccelY >= -10 ) )
    {
    	Result  = false;
    	Message = "AccelY was not within the range of -10 to +10 meters per second squared";
	}
	else if( !( m_AccelZ <= 10 && m_AccelZ >= -10 ) )
	{
    	Result  = false;
    	Message = "AccelZ was not within the range of -10 to +10 meters per second squared";
	}

	AgUtPluginConfigVerifyResult.Result  = Result;
	AgUtPluginConfigVerifyResult.Message = Message;
} 

//======================
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;
	
	if( m_AgUtPluginSite != null )
	{
		if( m_Enabled == true )
		{
			// if called by STK, get Vector Tool provider to do SRP Area computation
			
			var siteName = m_AgUtPluginSite.SiteName;
			
			if(siteName == "IAgStkPluginSite" || siteName == "IAgGatorPluginSite")
			{
				m_CrdnPluginProvider 	= m_AgUtPluginSite.VectorToolProvider;
				
				if(m_CrdnPluginProvider != null)
				{
					m_CrdnConfiguredVector  = m_CrdnPluginProvider.ConfigureVector( m_VectorName, "", "J2000", "CentralBody/Earth");
				}
				
				if( m_MsgStatus == true )
				{
					m_AgUtPluginSite.Message( eLogMsgDebug, "Init():" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelRefFrame( " + GetAccelRefFrame() + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelX( " + m_AccelX + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelY( " + m_AccelY + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): AccelZ( " + m_AccelZ + " )" );
				}
				
				if( m_CrdnConfiguredVector == null )
				{
					m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Could not obtain " + m_VectorName );
					m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Turning off the computation of SRP Area" );
				}
			}
			else 
			{
				m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): " + siteName + " does not provide VectorToolProvider" );
				m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Turning off the computation of SRP Area" );
			}
		}
		else
		{
			m_AgUtPluginSite.Message( eLogMsgDebug, "Init(): Disabled" );
		}
	}
    
    return m_Enabled;
} 

//======================
// PrePropagate Method
//======================
function PrePropagate( AgAsHpopPluginResult )
{
	if( m_AgUtPluginSite != null )
	{
		if( m_Enabled == true )
		{
			if( AgAsHpopPluginResult != null )
			{
				var WholeDays_Index   = 0;
				var SecsIntoDay_Index = 1;
				var Year_Index        = 2;
				var DayOfYear_Index   = 3;
                var Month_Index       = 4;
                var DayOfMonth_Index  = 5
				var Hours_Index       = 6;
				var Minutes_Index     = 7;
				var Seconds_Index     = 8;
			
				var vbArrayTimes = AgAsHpopPluginResult.RefEpochElements_Array( eUTC );
				var jsArrayTimes = vbArrayTimes.toArray();
				
				m_SrpIsOn = AgAsHpopPluginResult.IsForceModelOn( eSRPModel );
				
				if(m_SrpIsOn)
				{
					m_SRPArea = AgAsHpopPluginResult.SRPArea;
				}
			
				if( m_MsgStatus == true )
				{
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate():" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch WholeDays( " + jsArrayTimes[WholeDays_Index] + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch SecsIntoDay( " + jsArrayTimes[SecsIntoDay_Index] + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch Year( " + jsArrayTimes[Year_Index] + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch DayOfYear( " + jsArrayTimes[DayOfYear_Index] + " )" );
                    m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch Month( " + jsArrayTimes[Month_Index] + " )");
                    m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch DayOfMonth( " + jsArrayTimes[DayOfMonth_Index] + " )");
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch Hours( " + jsArrayTimes[Hours_Index] + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch Minutes( " + jsArrayTimes[Minutes_Index] + " )" );
					m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Epoch Seconds( " + jsArrayTimes[Seconds_Index] + " )" );
				}			
			}			
		}
		else
		{
			if( m_MsgStatus == true )
			{	
				m_AgUtPluginSite.Message( eLogMsgDebug, "PrePropagate(): Disabled" );
			}
		}
	}
	
	return m_Enabled;
}


//======================
// PreNextStep Function
//======================
function PreNextStep( AgAsHpopPluginResult )
{
	m_PreNextCntr = m_PreNextCntr + 1;

	if( m_AgUtPluginSite != null )
	{
		if( m_Enabled == true )
		{
			if( m_MsgStatus == true )
			{
				if( m_PreNextCntr % m_PreNextMsgInterval == 0 )
				{
					m_AgUtPluginSite.Message( eLogMsgDebug, "PreNextStep( " + m_PreNextCntr + " ):" );
				}
			}
		}
		else
		{
			if( m_MsgStatus == true )
			{
				m_AgUtPluginSite.Message( eLogMsgDebug, "PreNextStep(): Disabled" );			
			}
		}
	}

	return m_Enabled;
}

//=================
// Evaluate Method
//=================
function Evaluate( AgAsHpopPluginResultEval )
{
	m_EvalCntr = m_EvalCntr + 1;
	
	if( m_AgUtPluginSite != null )
	{	
		if( m_Enabled == true )
		{
			EvaluateSRPArea( AgAsHpopPluginResultEval );

			AgAsHpopPluginResultEval.AddAcceleration( m_AccelRefFrame, m_AccelX, m_AccelY, m_AccelZ );

			if( m_MsgStatus == true )
			{
				if( m_EvalCntr % m_EvalMsgInterval == 0 )
				{
					m_AgUtPluginSite.Message( eLogMsgDebug, "Evaluate( " + m_EvalCntr + " ):" );
				}
			}
		}
		else
		{
			if( m_MsgStatus == true )
			{
				m_AgUtPluginSite.Message( eLogMsgDebug, "Evalate(): Disabled" );
			}
		}
	}
	
	return m_Enabled;
}

function EvaluateSRPArea( ResultEval )
{
	var Result = true;
	
	if(!m_SrpIsOn)
	{
		return Result;
	}
	
	// NOTE: m_CrdnConfiguredVector may be null if plugin not run from STK
	if( m_CrdnConfiguredVector == null )
	{
		// just return true
		return Result;
	}
	
	var PosX_Index		= 0;
	var PosY_Index		= 1;
	var PosZ_Index		= 2;
	var VelX_Index		= 3;
	var VelY_Index		= 4;
	var VelZ_Index		= 5;
	
	var VecX_Index		= 0;
	var VecY_Index		= 1;
	var VecZ_Index		= 2;

	var VecPosDotProd	= 0.0;
	var VecMag			= 0.0;
	var PosMag			= 0.0;
	var Theta			= 0.0;

	if( ResultEval != null )
	{
		var vbPosVelArray = ResultEval.PosVel_Array( eUtFrameInertial );
		var jsPosVelArray = vbPosVelArray.toArray();
				
		var vbVecArray = m_CrdnConfiguredVector.CurrentValue_Array( ResultEval );
		var jsVecArray = vbVecArray.toArray();
		
		VecPosDotProd 	= ( jsVecArray[VecX_Index] * jsPosVelArray[PosX_Index] ) + ( jsVecArray[VecY_Index] * jsPosVelArray[PosY_Index] ) + ( jsVecArray[VecZ_Index] * jsPosVelArray[PosZ_Index] );
		VecMag			= Math.sqrt( Math.pow(jsVecArray[VecX_Index],2) + Math.pow(jsVecArray[VecY_Index],2) + Math.pow(jsVecArray[VecZ_Index],2) );
		PosMag			= Math.sqrt( Math.pow(jsPosVelArray[PosX_Index],2) + Math.pow(jsPosVelArray[PosY_Index],2) + Math.pow(jsPosVelArray[PosZ_Index],2) );
		Theta			= Math.acos( VecPosDotProd / ( VecMag * PosMag ) );
			
		// SRP must be on else this may be a run-time error
		ResultEval.SRPArea = ( m_SRPArea / 4.0 ) * ( 3 - Math.sin( Theta ) );
		
		if( m_AgUtPluginSite != null && m_MsgStatus == true )
		{
			if( m_EvalCntr % m_EvalMsgInterval == 0 )
			{
				var ThetaDeg = Theta * 57.2957795130823208767;
				
				m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " + m_EvalCntr + " ): VecX( " + jsVecArray[VecX_Index] + " ), VecY( " + jsVecArray[VecY_Index] + " ), VecZ( " + jsVecArray[VecZ_Index] + " ) meters/sec" );
				m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " + m_EvalCntr + " ): PosX(" + jsPosVelArray[PosX_Index] + " ), PosY( " + jsPosVelArray[PosY_Index] + " ), PosZ( " + jsPosVelArray[PosZ_Index] + " ) meters" );
				m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " + m_EvalCntr + " ): VelX(" + jsPosVelArray[VelX_Index] + " ), VelY( " + jsPosVelArray[VelY_Index] + " ), VelZ(" + jsPosVelArray[VelZ_Index] + " ) meters/sec" );
				m_AgUtPluginSite.Message( eLogMsgDebug, "EvaluateSRPArea( " + m_EvalCntr + " ): SRPArea(" + ResultEval.SRPArea + " m^2), Theta( " + ThetaDeg + " deg)" );
			}
		}
	}
	else
	{
		if( AgUtPluginSite != null && m_MsgStatus == true )
		{
			m_AgUtPluginSite.Message( eLogMsgWarning, "Crdn Configured Vector or Result Eval was null" );
		}
	}
}

//=================
// PostEvaluate Method
//=================
function PostEvaluate( AgAsHpopPluginResultPostEval )
{
	m_PostEvalCntr = m_PostEvalCntr + 1;
	
	if( m_AgUtPluginSite != null )
	{	
		if( m_Enabled == true )
		{
			if( m_MsgStatus == true )
			{
				if( m_PostEvalCntr % m_PostEvalMsgInterval == 0 )
				{
					var		reportFrame = eUtFrameNTC;
					var		frameName = "NTC";

					var		accelType = eSRPAccel;
					var		AltInKm;

					AltInKm = AgAsHpopPluginResultPostEval.Altitude*0.001;
					
					m_AgUtPluginSite.Message( eLogMsgDebug, 
						"PostEvaluate( " + m_PostEvalCntr + " ):" +  " ): SRPArea (" +
						AgAsHpopPluginResultPostEval.SRPArea+" m^2), Altitude ("+AltInKm+" km)" );

					var vbAccelArray = AgAsHpopPluginResultPostEval.GetAcceleration_Array( accelType, reportFrame);
					var jsAccelArray = vbAccelArray.toArray();
					
					m_AgUtPluginSite.Message( eLogMsgDebug, 
						"PostEvaluate( " + m_PostEvalCntr + " ):" + " ): SRPAccel (" +
						frameName+") is ("+jsAccelArray[0]+", "+jsAccelArray[1]+", "+
						jsAccelArray[2]+") meters/secs^2" );

					// report out the added acceleration in NTC components
					accelType = eAddedAccel;
					
					vbAccelArray = AgAsHpopPluginResultPostEval.GetAcceleration_Array( accelType, reportFrame);
					jsAccelArray = vbAccelArray.toArray();

					m_AgUtPluginSite.Message( eLogMsgDebug, 
						"PostEvaluate( " + m_PostEvalCntr + " ):" + " ): ThrustAccel (" +
						frameName+") is ("+jsAccelArray[0]+", "+jsAccelArray[1]+", "+
						jsAccelArray[2]+") meters/secs^2" );
				}
			}
		}
		else
		{
			if( m_MsgStatus == true )
			{
				m_AgUtPluginSite.Message( eLogMsgDebug, "PostEvalate(): Disabled" );
			}
		}
	}
	
	return m_Enabled;
}

//========================================================
// PostPropagate Method
//========================================================
function PostPropagate( AgAsHpopPluginResult)
{
	if( m_AgUtPluginSite != null )
	{	
		if( m_Enabled == true )
		{
			if( m_MsgStatus == true )
			{
				m_AgUtPluginSite.Message( eLogMsgDebug, "PostPropagate():" );
			}			
		}
		else
		{
			if( m_MsgStatus == true )
			{
				m_AgUtPluginSite.Message( eLogMsgDebug, "PostPropagate(): Disabled" );
			}
		}
	}
	
	return m_Enabled;
}


//===========================================================
// Free Method
//===========================================================
function Free()
{
	if( m_AgUtPluginSite != null )
	{	
		if( m_MsgStatus == true )
		{
			m_AgUtPluginSite.Message( eLogMsgDebug, "Free():" );
			m_AgUtPluginSite.Message( eLogMsgDebug, "Free(): PreNextCntr( " + m_PreNextCntr + " )" );
			m_AgUtPluginSite.Message( eLogMsgDebug, "Free(): EvalCntr( " + m_EvalCntr + " )" );
			m_AgUtPluginSite.Message( eLogMsgDebug, "Free(): PostEvalCntr( " + m_PostEvalCntr + " )" );
		}
		
		m_AgUtPluginSite 		= null
		m_CrdnPluginProvider   	= null
		m_CrdnConfiguredVector 	= null
	}
}

//=============================================================
// Name Method
//=============================================================
function GetName()
{
	return m_Name;
}

function SetName( name )
{
	m_Name = name;
}

//============================================================
// Enabled property
//============================================================
function GetEnabled()
{
	return m_Enabled;
}

function SetEnabled( enabled )
{
	m_Enabled = enabled;
}

//============================================================
// VectorName property
//============================================================
function GetVectorName()
{
	return m_VectorName;
}

function SetVectorName( vectorname )
{
	m_VectorName = vectorname;
}

//=============================================================
// AccelRefFrame property
// NOTE:  Do to the below comment on the next property of
// GetAccelRefFrameChoices, I did not want to have multiple
// Ref Frame Arrays defined, especially one in VBScript in the
// WSC file and one in this JScript file.  So I just made the
// below property of GetAccelRefFrame and SetAccelRefFrame use
// the VBScript method to create the Ref Frame Safe Array and
// just convert it to a JScript Array to check for which frame.
// "Was that clear?  As mud?" Contact Tech support for better
// explanation...and they will contact me ;-)
//=============================================================
function GetAccelRefFrame()
{       
	var vbRefFramesArray = CreateRefFrameChoicesArray();
	var jsRefFramesArray = vbRefFramesArray.toArray();
	return jsRefFramesArray[ m_AccelRefFrame ];
}

function SetAccelRefFrame( accelrefframe )
{
	var vbRefFramesArray = CreateRefFrameChoicesArray();
	var jsRefFramesArray = vbRefFramesArray.toArray();
	
	if( jsRefFramesArray[0] == accelrefframe )
	{
		m_AccelRefFrame = 0;
	}
	else if( jsRefFramesArray[1] == accelrefframe )
	{	
		m_AccelRefFrame = 1;
	}
	else if( jsRefFramesArray[2] == accelrefframe )
	{
		m_AccelRefFrame = 2;
	}
	else if( jsRefFramesArray[3] == accelrefframe )
	{
		m_AccelRefFrame = 3;
	}
}

//=================================================================
// AccelRefFrameChoices property
// NOTE: JScript's Array are not of type VARIANT SAFEARRAY,
// thus you must use the VBArray object to access the
// VARIANT SAFEARRAY version of a JScript array.
//
// So must write and use a VBScript function to create
// a VBScript array.
//
// See this blog for details...(written by developer of JScript)
// http://blogs.msdn.com/ericlippert/archive/2003/09/22/53061.aspx
//=================================================================
function GetAccelRefFrameChoices()
{
	return CreateRefFrameChoicesArray();
}

//==========================================================
// AccelX property
//==========================================================
function GetAccelX()
{
	return m_AccelX;
}

function SetAccelX( accelx )
{
	m_AccelX = accelx;
}

//=========================================================
// AccelY property
//=========================================================
function GetAccelY()
{
	return m_AccelY;
}

function SetAccelY( accely )
{
	m_AccelY = accely;
}

//=========================================================
// AccelZ property
//=========================================================
function GetAccelZ()
{
	return m_AccelZ;
}

function SetAccelZ( accelz )
{
	m_AccelZ = accelz;
}

//======================================================
// MsgStatus property
//======================================================
function GetMsgStatus()
{
	return m_MsgStatus;
}

function SetMsgStatus( msgstatus )
{
       m_MsgStatus = msgstatus;
}

//=======================================================
// EvalMsgInterval property
//=======================================================
function GetEvalMsgInterval()
{
	return m_EvalMsgInterval;
}

function SetEvalMsgInterval( evalmsginterval )
{
	m_EvalMsgInterval = evalmsginterval;
}

//=======================================================
// PostEvalMsgInterval property
//=======================================================
function GetPostEvalMsgInterval()
{
	return m_PostEvalMsgInterval;
}

function SetPostEvalMsgInterval( postevalmsginterval )
{
	m_PostEvalMsgInterval = postevalmsginterval;
}

//=======================================================
// PreNextMsgInterval property
//=======================================================
function GetPreNextMsgInterval()
{
	return m_PreNextMsgInterval;
}

function SetPreNextMsgInterval( prenextmsginterval )
{
	m_PreNextMsgInterval = prenextmsginterval;
} 
//=====================================================
//  Copyright 2005, Analytical Graphics, Inc.          
//=====================================================

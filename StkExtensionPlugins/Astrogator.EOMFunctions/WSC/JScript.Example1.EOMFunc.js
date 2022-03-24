//=====================================================
//  Copyright 2009, Analytical Graphics, Inc.          
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
// AgEAsEOMFuncPluginEventTypes Enumeration
//==========================================
var eEventTypesPrePropagate = 0;
var eEventTypesPreNextStep = 1;
var eEventTypesEvaluate = 2;
var eEventTypesPostPropagate = 3;

//==========================================
// AgEAsEOMFuncPluginInputStateValues Enumeration
//==========================================
var eEOMFuncPluginInputStateValuesPosX = 0;
var eEOMFuncPluginInputStateValuesPosY = 1;
var eEOMFuncPluginInputStateValuesPosZ = 2;
var eEOMFuncPluginInputStateValuesVelX = 3;
var eEOMFuncPluginInputStateValuesVelY = 4;
var eEOMFuncPluginInputStateValuesVelZ = 5;
var eEOMFuncPluginInputStateValuesPosCBFX = 6;
var eEOMFuncPluginInputStateValuesPosCBFY = 7;
var eEOMFuncPluginInputStateValuesPosCBFZ = 8;
var eEOMFuncPluginInputStateValuesVelCBFX = 9;
var eEOMFuncPluginInputStateValuesVelCBFY = 10;
var eEOMFuncPluginInputStateValuesVelCBFZ = 11;
var eEOMFuncPluginInputStateValuesCBIVelInCBFX = 12;
var eEOMFuncPluginInputStateValuesCBIVelInCBFY = 13;
var eEOMFuncPluginInputStateValuesCBIVelInCBFZ = 14;
var eEOMFuncPluginInputStateValuesQuat1 = 15;
var eEOMFuncPluginInputStateValuesQuat2 = 16;
var eEOMFuncPluginInputStateValuesQuat3 = 17;
var eEOMFuncPluginInputStateValuesQuat4 = 18;
var eEOMFuncPluginInputStateValuesCBIToCBF00 = 19;
var eEOMFuncPluginInputStateValuesCBIToCBF01 = 20;
var eEOMFuncPluginInputStateValuesCBIToCBF02 = 21;
var eEOMFuncPluginInputStateValuesCBIToCBF10 = 22;
var eEOMFuncPluginInputStateValuesCBIToCBF11 = 23;
var eEOMFuncPluginInputStateValuesCBIToCBF12 = 24;
var eEOMFuncPluginInputStateValuesCBIToCBF20 = 25;
var eEOMFuncPluginInputStateValuesCBIToCBF21 = 26;
var eEOMFuncPluginInputStateValuesCBIToCBF22 = 27;
var eEOMFuncPluginInputStateValuesAngVelCBFX = 28;
var eEOMFuncPluginInputStateValuesAngVelCBFY = 29;
var eEOMFuncPluginInputStateValuesAngVelCBFZ = 30;
var eEOMFuncPluginInputStateValuesAltitude = 31;
var eEOMFuncPluginInputStateValuesLatitude = 32;
var eEOMFuncPluginInputStateValuesLongitude = 33;
var eEOMFuncPluginInputStateValuesTotalMass = 34;
var eEOMFuncPluginInputStateValuesDryMass = 35;
var eEOMFuncPluginInputStateValuesFuelMass = 36;
var eEOMFuncPluginInputStateValuesCd = 37;
var eEOMFuncPluginInputStateValuesDragArea = 38;
var eEOMFuncPluginInputStateValuesAtmosphericDensity = 39;
var eEOMFuncPluginInputStateValuesAtmosphericAltitude = 40;
var eEOMFuncPluginInputStateValuesCr = 41;
var eEOMFuncPluginInputStateValuesSRPArea = 42;
var eEOMFuncPluginInputStateValuesKr1 = 43;
var eEOMFuncPluginInputStateValuesKr2 = 44;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFX = 45;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFY = 46;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFZ = 47;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFX = 48;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFY = 49;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFZ = 50;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosX = 51;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosY = 52;
var eEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosZ = 53;
var eEOMFuncPluginInputStateValuesApparentSunPosCBFX = 54;
var eEOMFuncPluginInputStateValuesApparentSunPosCBFY = 55;
var eEOMFuncPluginInputStateValuesApparentSunPosCBFZ = 56;
var eEOMFuncPluginInputStateValuesApparentSatPosCBFX = 57;
var eEOMFuncPluginInputStateValuesApparentSatPosCBFY = 58;
var eEOMFuncPluginInputStateValuesApparentSatPosCBFZ = 59;
var eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosX = 60;
var eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosY = 61;
var eEOMFuncPluginInputStateValuesApparentSatToSunCBIPosZ = 62;
var eEOMFuncPluginInputStateValuesTrueSunPosCBFX = 63;
var eEOMFuncPluginInputStateValuesTrueSunPosCBFY = 64;
var eEOMFuncPluginInputStateValuesTrueSunPosCBFZ = 65;
var eEOMFuncPluginInputStateValuesTrueSatPosCBFX = 66;
var eEOMFuncPluginInputStateValuesTrueSatPosCBFY = 67;
var eEOMFuncPluginInputStateValuesTrueSatPosCBFZ = 68;
var eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosX = 69;
var eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosY = 70;
var eEOMFuncPluginInputStateValuesTrueSatToSunCBIPosZ = 71;
var eEOMFuncPluginInputStateValuesSolarIntensity = 72;
var eEOMFuncPluginInputStateValuesRadPressureCoefficient = 73;
var eEOMFuncPluginInputStateValuesRadPressureArea = 74;
var eEOMFuncPluginInputStateValuesMassFlowRate = 75;
var eEOMFuncPluginInputStateValuesTankPressure = 76;
var eEOMFuncPluginInputStateValuesTankTemperature = 77;
var eEOMFuncPluginInputStateValuesFuelDensity = 78;
var eEOMFuncPluginInputStateValuesThrustX = 79;
var eEOMFuncPluginInputStateValuesThrustY = 80;
var eEOMFuncPluginInputStateValuesThrustZ = 81;
var eEOMFuncPluginInputStateValuesDeltaV = 82;
var eEOMFuncPluginInputStateValuesGravityAccelX = 83;
var eEOMFuncPluginInputStateValuesGravityAccelY = 84;
var eEOMFuncPluginInputStateValuesGravityAccelZ = 85;
var eEOMFuncPluginInputStateValuesTwoBodyAccelX = 86;
var eEOMFuncPluginInputStateValuesTwoBodyAccelY = 87;
var eEOMFuncPluginInputStateValuesTwoBodyAccelZ = 88;
var eEOMFuncPluginInputStateValuesGravityPertAccelX = 89;
var eEOMFuncPluginInputStateValuesGravityPertAccelY = 90;
var eEOMFuncPluginInputStateValuesGravityPertAccelZ = 91;
var eEOMFuncPluginInputStateValuesSolidTidesAccelX = 92;
var eEOMFuncPluginInputStateValuesSolidTidesAccelY = 93;
var eEOMFuncPluginInputStateValuesSolidTidesAccelZ = 94;
var eEOMFuncPluginInputStateValuesOceanTidesAccelX = 95;
var eEOMFuncPluginInputStateValuesOceanTidesAccelY = 96;
var eEOMFuncPluginInputStateValuesOceanTidesAccelZ = 97;
var eEOMFuncPluginInputStateValuesDragAccelX = 98;
var eEOMFuncPluginInputStateValuesDragAccelY = 99;
var eEOMFuncPluginInputStateValuesDragAccelZ = 100;
var eEOMFuncPluginInputStateValuesThirdBodyAccelX = 101;
var eEOMFuncPluginInputStateValuesThirdBodyAccelY = 102;
var eEOMFuncPluginInputStateValuesThirdBodyAccelZ = 103;
var eEOMFuncPluginInputStateValuesSRPAccelX = 104;
var eEOMFuncPluginInputStateValuesSRPAccelY = 105;
var eEOMFuncPluginInputStateValuesSRPAccelZ = 106;
var eEOMFuncPluginInputStateValuesNoShadowSRPAccelX = 107;
var eEOMFuncPluginInputStateValuesNoShadowSRPAccelY = 108;
var eEOMFuncPluginInputStateValuesNoShadowSRPAccelZ = 109;
var eEOMFuncPluginInputStateValuesGenRelAccelX = 110;
var eEOMFuncPluginInputStateValuesGenRelAccelY = 111;
var eEOMFuncPluginInputStateValuesGenRelAccelZ = 112;
var eEOMFuncPluginInputStateValuesAlbedoAccelX = 113;
var eEOMFuncPluginInputStateValuesAlbedoAccelY = 114;
var eEOMFuncPluginInputStateValuesAlbedoAccelZ = 115;
var eEOMFuncPluginInputStateValuesThermalPressureAccelX = 116;
var eEOMFuncPluginInputStateValuesThermalPressureAccelY = 117;
var eEOMFuncPluginInputStateValuesThermalPressureAccelZ = 118;
var eEOMFuncPluginInputStateValuesAddedAccelX = 119;
var eEOMFuncPluginInputStateValuesAddedAccelY = 120;
var eEOMFuncPluginInputStateValuesAddedAccelZ = 121;
var eEOMFuncPluginInputStateValuesStateTransPosXPosX = 122;
var eEOMFuncPluginInputStateValuesStateTransPosXPosY = 123;
var eEOMFuncPluginInputStateValuesStateTransPosXPosZ = 124;
var eEOMFuncPluginInputStateValuesStateTransPosXVelX = 125;
var eEOMFuncPluginInputStateValuesStateTransPosXVelY = 126;
var eEOMFuncPluginInputStateValuesStateTransPosXVelZ = 127;
var eEOMFuncPluginInputStateValuesStateTransPosYPosX = 128;
var eEOMFuncPluginInputStateValuesStateTransPosYPosY = 129;
var eEOMFuncPluginInputStateValuesStateTransPosYPosZ = 130;
var eEOMFuncPluginInputStateValuesStateTransPosYVelX = 131;
var eEOMFuncPluginInputStateValuesStateTransPosYVelY = 132;
var eEOMFuncPluginInputStateValuesStateTransPosYVelZ = 133;
var eEOMFuncPluginInputStateValuesStateTransPosZPosX = 134;
var eEOMFuncPluginInputStateValuesStateTransPosZPosY = 135;
var eEOMFuncPluginInputStateValuesStateTransPosZPosZ = 136;
var eEOMFuncPluginInputStateValuesStateTransPosZVelX = 137;
var eEOMFuncPluginInputStateValuesStateTransPosZVelY = 138;
var eEOMFuncPluginInputStateValuesStateTransPosZVelZ = 139;
var eEOMFuncPluginInputStateValuesStateTransVelXPosX = 140;
var eEOMFuncPluginInputStateValuesStateTransVelXPosY = 141;
var eEOMFuncPluginInputStateValuesStateTransVelXPosZ = 142;
var eEOMFuncPluginInputStateValuesStateTransVelXVelX = 143;
var eEOMFuncPluginInputStateValuesStateTransVelXVelY = 144;
var eEOMFuncPluginInputStateValuesStateTransVelXVelZ = 145;
var eEOMFuncPluginInputStateValuesStateTransVelYPosX = 146;
var eEOMFuncPluginInputStateValuesStateTransVelYPosY = 147;
var eEOMFuncPluginInputStateValuesStateTransVelYPosZ = 148;
var eEOMFuncPluginInputStateValuesStateTransVelYVelX = 149;
var eEOMFuncPluginInputStateValuesStateTransVelYVelY = 150;
var eEOMFuncPluginInputStateValuesStateTransVelYVelZ = 151;
var eEOMFuncPluginInputStateValuesStateTransVelZPosX = 152;
var eEOMFuncPluginInputStateValuesStateTransVelZPosY = 153;
var eEOMFuncPluginInputStateValuesStateTransVelZPosZ = 154;
var eEOMFuncPluginInputStateValuesStateTransVelZVelX = 155;
var eEOMFuncPluginInputStateValuesStateTransVelZVelY = 156;
var eEOMFuncPluginInputStateValuesStateTransVelZVelZ = 157;

//==========================================
// Declare Global Variables
//==========================================

// Axes in which the delta-v is integrated.  This value can be changed on the 
// propagator panel.
var m_DeltaVAxes = "VNC(Earth)";


var m_AgUtPluginSite		= null;
var m_CrdnPluginProvider	= null;
var m_CrdnConfiguredAxes    = null;
var m_AgAttrScope			= null;

var m_thrustXIndex = 0;
var m_thrustYIndex = 0;
var m_thrustZIndex = 0;
var m_massIndex = 0;

var m_effectiveImpulseIndex = 0;
var m_integratedDeltaVxIndex = 0;
var m_integratedDeltaVyIndex = 0;
var m_integratedDeltaVzIndex = 0;

//========================
// GetPluginConfig method
//========================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();
		
		// Create an attribute for the delta-V axes, so it appears on the panel.
		AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "DeltaVAxes", "Axes in which to integrate delta-V", "DeltaVAxes", 0);
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
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;
	
	if( m_AgUtPluginSite == null )
	{
	    return false;
	}
	
	m_CrdnPluginProvider 	= m_AgUtPluginSite.VectorToolProvider;
	
	if(m_CrdnPluginProvider != null)
	{
	    // we'll use this to rotate from inertial to the specified axes
		m_CrdnConfiguredAxes = m_CrdnPluginProvider.ConfigureAxes( "Inertial", "CentralBody/Earth", m_DeltaVAxes, "");
		
		if (m_CrdnConfiguredAxes != null)
		{
		    return true;
		}
	}
   
    return false;
} 

//======================
// Register Method
//======================
function Register( AgAsEOMFuncPluginRegisterHandler )
{
    // plugin needs the thrust vector and the mass
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesThrustX);
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesThrustY);
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesThrustZ);
    
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(eEOMFuncPluginInputStateValuesTotalMass);

    
    // plugin gives the derivative of effective impulse and integrated delta-V
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("EffectiveImpulse");
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVx");
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVy");
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVz");

    // plugin only needs to be called on evaluate
    AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPrePropagate);
    AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPreNextStep);
    AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPostPropagate);
    
    return true;
}


//======================
// SetIndices Function
//======================
function SetIndices( AgAsEOMFuncPluginSetIndicesHandler )
{
    // get the indices for the input variables
    m_thrustXIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesThrustX);
    m_thrustYIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesThrustY);
    m_thrustZIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesThrustZ);
    m_massIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(eEOMFuncPluginInputStateValuesTotalMass);
    
    // get the indices for the derivatives we will output
    m_effectiveImpulseIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("EffectiveImpulse");
    m_integratedDeltaVxIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVx");
    m_integratedDeltaVyIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVy");
    m_integratedDeltaVzIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVz");

    return true;
}

//=================
// Calc Method
//=================
function Calc( eventType, AgAsEOMFuncPluginStateVector )
{
    // get the current thrust values, and give back the derivatives of
    // effective impulse and the integrated delta V components

    // get thrust
    var thrustX = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustXIndex);
    var thrustY = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustYIndex);
    var thrustZ = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustZIndex);

    // get mass
    var mass = AgAsEOMFuncPluginStateVector.GetInputValue(m_massIndex);
    

    // derivative of effective impulse is the total thrust magnitude
    var thrustMag = Math.sqrt(thrustX*thrustX + thrustY*thrustY + thrustZ*thrustZ);
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_effectiveImpulseIndex, thrustMag);
    
    // rotate thrust vector to desired integration frame for integrated delta-V
    var thrustVBArray = m_CrdnConfiguredAxes.TransformComponents_Array(AgAsEOMFuncPluginStateVector, thrustX, thrustY, thrustZ);
    var thrustArray = thrustVBArray.toArray();
    
    // the derivative of each integrated delta-V component is that component of thrust acceleration 
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVxIndex, thrustArray[0] / mass);
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVyIndex, thrustArray[1] / mass);
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVzIndex, thrustArray[2] / mass);

    return true;
}


//===========================================================
// Free Method
//===========================================================
function Free()
{
	if( m_AgUtPluginSite != null )
	{	
		m_AgUtPluginSite 		= null
	}
	
	return true;
}

//============================================================
// DeltaVAxes property
//============================================================
function GetDeltaVAxes()
{
	return m_DeltaVAxes;
}

function SetDeltaVAxes( axes )
{
	m_DeltaVAxes = axes;
}



//=====================================================
//  Copyright 2009, Analytical Graphics, Inc.          
//=====================================================

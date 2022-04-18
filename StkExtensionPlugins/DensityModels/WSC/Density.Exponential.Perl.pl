#-----------------------------------------------------------------------------------
#
# The Density Model being modeled is that of an exponential atmosphere, 
# like the Exponential Atmosphere model
#
#-----------------------------------------------------------------------------------

# This script requires Perl 5.8.0 or higher

require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;

# ==========================================
#  Log Msg Type Enumeration
# ==========================================
use constant eLogMsgDebug	 	=> 0;
use constant eLogMsgInfo 		=> 1;
use constant eLogMsgForceInfo 	=> 2;
use constant eLogMsgWarning 	=> 3;
use constant eLogMsgAlarm 		=> 4;

# ==========================================
#  AgEAttrAddFlags Enumeration
# ==========================================
use constant eFlagNone			=> 0;
use constant eFlagTransparent	=> 2;
use constant eFlagHidden		=> 4;
use constant eFlagTransient		=> 8;  
use constant eFlagReadOnly		=> 16;
use constant eFlagFixed			=> 32;

use constant false	=> 0;
use constant true	=> 1;

# ==========================================
#  Declare Global Variables
# ==========================================
my $m_AgUtPluginSite	= undef;
my $m_AgAttrScope		= undef;

my $m_MsgCntr			= -1;
my $m_Enabled			= true;
my $m_DebugMode			= false;
my $m_MsgInterval		= 500;

my $m_cbName			= "Earth";
my $m_refDen			= 1.784e-11;        # kg/m^3
my $m_refAlt 			= 300.0* 1000.0;	# meters;
my $m_scaleAlt 			= 20.0 * 1000.0;    # meters
my $m_lowestValidAlt	= 90.0 * 1000.0;	# meters
my $m_Density 			= -1;
my $m_computesTemp		= false;
my $m_computesPressure  = false;
my $m_UserIndex			= 0;
my $m_MaxScaleHeights   = 7.0;
my $m_maxHeightMsg		= 0;
my $m_lowAltMsg			= 0;

my $dirpath = 'C:\Temp';
my $m_DebugFile = $dirpath.'\Exponential_Debug.txt';

sub Message
{
	my $severity = $_[0];
	my $msg = $_[1];
	
	if( defined($m_AgUtPluginSite) )
	{
		$m_AgUtPluginSite->Message( $severity, "$msg" );
	}
}

sub DebugMsg
{
	my $msg = $_[0];
	
	if($m_DebugMode)
	{
		if($m_MsgCntr % $m_MsgInterval == 0)
		{
			Message(eLogMsgDebug, "$msg");
		}
	}
}

# ========================
#  GetPluginConfig method
# ========================
sub GetPluginConfig
{
	my $AgAttrBuilder = $_[0];
	
	if( !defined($m_AgAttrScope) )
	{
		$m_AgAttrScope = $AgAttrBuilder->NewScope();
		
		$AgAttrBuilder->AddStringDispatchProperty ( $m_AgAttrScope, 
			"CentralBodyName", 
			"CentralBody Name", 
			"CentralBodyName", eFlagNone );
			
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, 
			"RefDensity", 
			"Reference Density", 
			"RefDensity", eFlagNone );
			
		$AgAttrBuilder->AddQuantityMinDispatchProperty2 ( $m_AgAttrScope, 
			"RefAltitude", 
			"Reference Altitude", 
			"RefAltitude", 
			"DistanceUnit", "Kilometers", "Meters", 0.0, eFlagNone);
			
		$AgAttrBuilder->AddQuantityMinDispatchProperty2 ( $m_AgAttrScope, 
			"ScaleAltitude", 
			"Scale Altitude", 
			"ScaleAltitude", 
			"DistanceUnit", "Kilometers", "Meters", 0.0, eFlagNone);
			
		$AgAttrBuilder->AddQuantityMinDispatchProperty2 ( $m_AgAttrScope, 
			"LowestValidAlt", 
			"Lowest Valid Altitude", 
			"LowestValidAltitude", 
			"DistanceUnit", "Kilometers", "Meters", 0.0, eFlagNone);

		$AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, 
			"MaxScaleHeights", 
			"Max number of scale heights to allow", 
			"MaxScaleHeights", eFlagNone);
			
		# ===========================
		#  General Plugin attributes
		# ===========================
		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, 
			"PluginEnabled",
			"If the plugin is enabled or has experienced an error", 
			"Enabled",    
			eFlagNone );

		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, 
			"DebugMode",
			"Turn debug messages on or off", 
			"DebugMode",    
			eFlagNone );

		$AgAttrBuilder->AddFileDispatchProperty  ( $m_AgAttrScope, 
			"DebugFile",
			"Debug output file", 
			"DebugFile", "", "*.txt",   
			eFlagNone );
		
		# ==============================
		#  Messaging related attributes
		# ==============================
		$AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, 
			"MessageInterval", 
			"The interval at which to send messages during propagation in Debug mode", 
			"MsgInterval",     
			eFlagNone );
 	}

	return $m_AgAttrScope;
}  

# ===========================
#  VerifyPluginConfig method
# ===========================
sub VerifyPluginConfig
{
	my $AgUtPluginConfigVerifyResult = $_[0];
	
    my $Result = true;
    my $Message = "Ok";
    
	$AgUtPluginConfigVerifyResult->{Result}  = $Result;
	$AgUtPluginConfigVerifyResult->{Message} = $Message;
} 

# ======================
#  Register Method
# ======================
sub Register
{
	my $Result = $_[0];
	if( defined($Result) )
	{		 
		if( $m_DebugMode == true )
		{	
			$Result->Message( eLogMsgInfo, "Register() called" );
		}
	}
}

# ======================
#  Init Method
# ======================
sub Init
{
	my $AgUtPluginSite = $_[0];
	
	$m_AgUtPluginSite = $AgUtPluginSite;
	
	if( defined($m_AgUtPluginSite) )
	{		 
		if( $m_DebugMode == true )
		{
			if( $m_Enabled == true )
			{
				Message( eLogMsgInfo, "Init(): Enabled" );
			}
			else
			{
				Message( eLogMsgInfo, "Init(): Disabled because Enabled flag is False" );
			}
		}
		elsif($m_Enabled == false)
		{
			Message( eLogMsgAlarm, "Init(): Disabled because Enabled flag is False" );
		}
	}

    $m_maxHeightMsg = 0;
	$m_lowAltMsg = 0;
	$m_MsgCntr = -1;
	
    return $m_Enabled;
} 

# =================
#  Evaluate Method
# =================
sub Evaluate
{
	my $Result = $_[0];	# This is an IAgAsDensityModelResultEval interface
	
	$m_MsgCntr++;

	if($m_Enabled == true && defined($Result) )
	{
		$m_Enabled = setDensity($Result);
	}
	return $m_Enabled;
}


sub setDensity
{
	my $Result = $_[0];
	my $enabled = false;
	my $altitude = $Result->{Altitude};
	if(defined($altitude))
	{
		if($altitude < $m_lowestValidAlt)
		{
			if($m_lowAltMsg == 0)
			{
				my $altTrunc3 = sprintf "%.3f", $altitude / 1000.0;
				my $lowestValidAltTrunc = sprintf "%.3f", $m_lowestValidAlt / 1000.0;
				my $msg3 = "setDensity: altitude ".$altTrunc3." is less than minimum valid altitude ( ".$lowestValidAltTrunc." km). Keeping density constant below this height.";
				Message( eLogMsgWarning, $msg3 );
				
				$m_lowAltMsg = 1;
			}
			
			$altitude = $m_lowestValidAlt;
		}
		
		my $diffAlt = $m_refAlt - $altitude;
		my $expArg = $diffAlt / $m_scaleAlt;
		
		if( $m_DebugMode == true )
		{
			my $msg = "setDensity: alt= ".$altitude.", expArg = ".$expArg;
			DebugMsg($msg);
		}
		
		$enabled = true;
		
		if($expArg < -700)
		{
			$m_Density = 0.0;
		}
		else 
		{
			if($expArg > $m_MaxScaleHeights)
			{
				if($m_maxHeightMsg == 0)
				{
					my $altTrunc = sprintf "%.3f", $altitude / 1000.0;
					my $expArgTrunc = sprintf "%.3f", $expArg;
					my $msg = "setDensity: scaleHeight ".$expArgTrunc." exceeds maximum allowed ( ".$m_MaxScaleHeights."), alt= ".$altTrunc." km. Keeping density constant at maxScaleHeight.";
					Message( eLogMsgAlarm, $msg );
					
					$m_maxHeightMsg = 1;
				}
				
				$expArg = $m_MaxScaleHeights;
			}
			$m_Density = $m_refDen * exp($expArg);
		}
		if(defined($m_Density))
		{
			$Result->SetDensity($m_Density);
		}
	}
	
	return $enabled;	
}

# ===========================================================
#  Free Method
# ===========================================================
sub Free
{
	if( $m_DebugMode == true )
	{
		Message( eLogMsgDebug, "Free(): MsgCntr( $m_MsgCntr )" );
	}

	$m_AgUtPluginSite 		= undef;
}

# ============================================================
#  Computes Temperature property
# ============================================================
sub ComputesTemperature
{
	return $m_computesTemp;
}

# ============================================================
#  Computes Pressure property
# ============================================================
sub ComputesPressure
{
	return $m_computesPressure;
}


# ============================================================
#  ...
# ============================================================
sub CentralBody
{
	return $m_cbName;
}
sub UsesAugmentedSpaceWeather
{
	return false;
}
sub GetLowestValidAltitude
{
	return $m_lowestValidAlt;	# meters
}

sub AtmFluxLags
{
	# this fctn uses [in/out] parameters not supported by IDispatch implemented by JScript, VBScript, Perl
	return;
}
sub AugmentedAtmFluxLags
{
	# this fctn uses [in/out] parameters not supported by IDispatch implemented by JScript, VBScript, Perl
	return;
}

# ============================================================
#  Enabled property
# ============================================================
sub GetEnabled
{
	return $m_Enabled;
}

sub SetEnabled
{
	$m_Enabled = $_[0];
}

# ======================================================
#  MsgStatus property
# ======================================================
sub GetDebugMode
{
	return $m_DebugMode;
}

sub SetDebugMode
{
    $m_DebugMode = $_[0];
}


# ======================================================
#  DebugFile property
# ======================================================
sub GetDebugFile
{
	return $m_DebugFile;
}

sub SetDebugFile
{
    $m_DebugFile = $_[0];
}

# =======================================================
#  EvalMsgInterval property
# =======================================================
sub GetMsgInterval
{
	return $m_MsgInterval;
}

sub SetMsgInterval
{
	$m_MsgInterval = $_[0];
}

# =======================================================
#  RefDen property
# =======================================================
sub GetRefDensity
{
	return $m_refDen;
}

sub SetRefDensity
{
	$m_refDen = $_[0];
}

# =======================================================
#  RefAlt property
# =======================================================
sub GetRefAltitude
{
	return $m_refAlt;
}

sub SetRefAltitude
{
	$m_refAlt = $_[0];
}

# =======================================================
#  MaxScaleHeights property
# =======================================================
sub GetMaxScaleHeights
{
	return $m_MaxScaleHeights;
}

sub SetMaxScaleHeights
{
	$m_MaxScaleHeights = $_[0];
}

# =======================================================
#  ScaleAlt property
# =======================================================
sub GetScaleAltitude
{
	return $m_scaleAlt;
}

sub SetScaleAltitude
{
	$m_scaleAlt = $_[0];
}

# =======================================================
#  LowestValidAltitude property
# =======================================================
sub GetLowestValidAlt
{
	return $m_lowestValidAlt;
}

sub SetLowestValidAlt
{
	$m_lowestValidAlt = $_[0];
}

# =======================================================
#  CentralBodyName property
# =======================================================
sub GetCentralBodyName
{
	return $m_cbName;
}

sub SetCentralBodyName
{
	$m_cbName = $_[0];
}

#=====================================================
#  Copyright 2018-2019, Analytical Graphics, Inc.
#=====================================================
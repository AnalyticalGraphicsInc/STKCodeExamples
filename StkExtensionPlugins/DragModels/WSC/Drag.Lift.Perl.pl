# =====================================================
#   Copyright 2010, Analytical Graphics, Inc.          
# =====================================================

#-----------------------------------------------------------------------------------
#
# The atmospheric reflectance being modeled has components of lift and drag, 
# Drag produces an acceleration in the relative wind direction.
# Lift produces an acceleration in a direction perpendicular to drag.
#
#-----------------------------------------------------------------------------------

# This script requires Perl 5.8.0 or higher

require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;

# ==========================================
#  Reference Frames Enumeration
# ==========================================
use constant eInertial 			=> 0;
use constant eFixed 			=> 1;
use constant eLVLH 			=> 2;
use constant eNTC 			=> 3;
use constant eBodyFrame 		=> 4;

my $numFrames = 5;
my %refFrameNamesHash;
$refFrameNamesHash{0} = "Inertial";
$refFrameNamesHash{1} = "Fixed";
$refFrameNamesHash{2} = "LVLH";
$refFrameNamesHash{3} = "NTC";
$refFrameNamesHash{4} = "Body";

my @refFrameNamesArray;

my $i;
for($i = 0; $i < $numFrames; $i++)
{
	push @refFrameNamesArray, "$refFrameNamesHash{$i}";
}

# ==========================================
#  Time Scale Enumeration
# ==========================================
use constant eUTC 			=> 0;
use constant eTAI 			=> 1;
use constant eTDT 			=> 2;
use constant eUT1 			=> 3;
use constant eSTKEpochSec 		=> 4;
use constant eTDB 			=> 5;
use constant eGPS 			=> 6;

# ==========================================
#  Log Msg Type Enumeration
# ==========================================
use constant eLogMsgDebug	 	=> 0;
use constant eLogMsgInfo 		=> 1;
use constant eLogMsgForceInfo 		=> 2;
use constant eLogMsgWarning 		=> 3;
use constant eLogMsgAlarm 		=> 4;

# ==========================================
#  AgEAttrAddFlags Enumeration
# ==========================================
use constant eFlagNone			=> 0;
use constant eFlagTransparent		=> 2;
use constant eFlagHidden		=> 4;
use constant eFlagTransient		=> 8;  
use constant eFlagReadOnly		=> 16;
use constant eFlagFixed			=> 32;

use constant PI => 3.1415926535897932384;
use constant false	=> 0;
use constant true	=> 1;

# ==========================================
#  Declare Global Variables
# ==========================================
my $m_AgUtPluginSite	= undef;
my $m_AgAttrScope		= undef;
my $m_CDIndex			= -1;
my $m_CLIndex			= -1;
my $m_DragArea			= 20.0;
my $m_LiftArea			= 20.0;
my $m_CD                	= 2.02;
my $m_CL                	= 0.1;

my $m_MsgCntr			= -1;
my $m_Enabled			= true;
my $m_DebugMode			= true;
my $m_MsgInterval		= 500;

my $dirpath = 'C:\Temp';
my $m_DebugFile = $dirpath.'\DragLift_Debug.txt';

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

sub crossProduct
{
	my $aArrayRef = $_[0];
	my $bArrayRef = $_[1];

	my @answer;

	push @answer, $aArrayRef->[1]*$bArrayRef->[2] - $bArrayRef->[1]*$aArrayRef->[2];
	push @answer, $aArrayRef->[2]*$bArrayRef->[0] - $bArrayRef->[2]*$aArrayRef->[0];
	push @answer, $aArrayRef->[0]*$bArrayRef->[1] - $bArrayRef->[0]*$aArrayRef->[1];

	return \@answer;
} 

sub dotProduct
{
	my $aArrayRef = $_[0];
	my $bArrayRef = $_[1];

	my $answer;

	$answer = $aArrayRef->[0]*$bArrayRef->[0] + $aArrayRef->[1]*$bArrayRef->[1] + $aArrayRef->[2]*$bArrayRef->[2];

	return $answer;
} 

sub normalize
{
	my $arrayRef = $_[0];
	
	my $magnitude = dotProduct($arrayRef, $arrayRef);
	$magnitude = sqrt($magnitude);
	
	if($magnitude > 0.0)
	{
		$arrayRef->[0] /= $magnitude;
		$arrayRef->[1] /= $magnitude;
		$arrayRef->[2] /= $magnitude;
	}
	
	return $magnitude;
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
		
		#
		# "DragCoefficient" was added for STK as there is no way to
		# modify CD via GUI, but should be removed when using with ODTK
		# See BUG53691.
		#
		#$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, 
		#	"DragCoefficient", 
		#	"Drag Coefficient", 
		#	"DragCoefficient",     
		#	eFlagNone );
		#
		#$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, 
		#	"LiftCoefficient", 
		#	"Lift Coefficient", 
		#	"LiftCoefficient",     
		#	eFlagNone );

		$AgAttrBuilder->AddQuantityDispatchProperty2 ( $m_AgAttrScope,
			"DragArea", 
			"Drag Area",
			"DragArea",
            "Area",
			"m^2",
			"m^2",
			eFlagNone );
			
		$AgAttrBuilder->AddQuantityDispatchProperty2 ( $m_AgAttrScope,
			"LiftArea", 
			"Lift Area",
			"LiftArea",
            "Area"
			"m^2",
			"m^2",
			eFlagNone );


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
			my $myMsg = "Register() called, cd = " . $m_CD;
			#$Result->Message( eLogMsgInfo, "Register() called" );
			#$Result->Message( eLogMsgInfo, $myMsg );
		}
		
		$m_CDIndex = $Result->RegisterParameter("CD", $m_CD, 0.0, 20.0, "Unitless");
		
		if($m_CDIndex > -1)
		{
			if( $m_DebugMode == true )
			{
				$Result->Message( eLogMsgInfo, "Registered CD as Unitless parameter");
			}
		}
		else
		{
			$Result->Message( eLogMsgAlarm, "Unable to register CD as Unitless parameter");
		}
		
		$m_CLIndex = $Result->RegisterParameter("CL", $m_CL, -20.0, 20.0, "Unitless");
		
		if($m_CLIndex > -1)
		{
			if( $m_DebugMode == true )
			{
				$Result->Message( eLogMsgInfo, "Registered CL as Unitless parameter");
			}
		}
		else
		{
			$Result->Message( eLogMsgAlarm, "Unable to register CL as Unitless parameter");
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
	
    return $m_Enabled;
} 

# ======================
#  PreCompute Method
# ======================
sub PreCompute
{
	my $Result = $_[0];
	
	if( $m_Enabled == true )
	{
		if( defined($Result) )
		{
			
			if( $m_DebugMode == true )
			{
				Message(eLogMsgDebug, "PreCompute() called" );

				# Open debug file and write header
				open(DEBUGFILE,">$m_DebugFile");

			}
		}			
	}
	elsif( $m_DebugMode == true )
	{	
		Message( eLogMsgDebug, "PreCompute(): Disabled" );
	}
	
	return $m_Enabled;
}

# =================
#  Evaluate Method
# =================
sub Evaluate
{
	my $Result = $_[0];
	
	$m_MsgCntr++;

	if($m_Enabled == true && defined($Result) )
	{
		my $cd = 0.0;
		my $cl = 0.0;
		
		if($m_CDIndex > -1)
		{
			$cd = $Result->ParameterValue($m_CDIndex);
		}
		
		if($m_CLIndex > -1)
		{
			$cl = $Result->ParameterValue($m_CLIndex);
		}
		
		setDragLiftReflectanceInFixed($Result, $cd, $cl);
		
	}

	return $m_Enabled;
}

sub setDragLiftReflectanceInFixed
{
	my $Result = $_[0];
	my $cd = $_[1];
	my $cl = $_[2];

	my $enabled = false;

	my $incidentVecFixed = $Result->IncidentDirection_Array(eFixed);

    	my @posParts;
    	push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;
    	push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;
    	push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;

    	my @velParts;
    	push @velParts, 0.0; push @velParts, 0.0; push @velParts, 0.0;
    	push @velParts, 0.0; push @velParts, 0.0; push @velParts, 0.0;
    	push @velParts, 0.0; push @velParts, 0.0; push @velParts, 0.0;

	my $k = 0;


	if(defined($incidentVecFixed))
	{
		
		# particle reflectance is positive along the incidence vector
		# and the incidence vector is opposite the fixed velocity. 

		my $x = $m_DragArea * $incidentVecFixed->[0];
		my $y = $m_DragArea * $incidentVecFixed->[1];
		my $z = $m_DragArea * $incidentVecFixed->[2];
		
		if($m_CDIndex > -1)
		{
		    printf DEBUGFILE "CD partials: %20.15e, %20.15e, %20.15e\n", $x, $y, $z;
			$Result->SetReflectanceParamPartials($m_CDIndex, eFixed, $x, $y, $z);
		}

		$x *= $cd;
		$y *= $cd;
		$z *= $cd;

		# Compute the direction of Lift
		my $pvFixedArray = $Result->PosVel_Array(eFixed);
		my $crossTrackArray = crossProduct($pvFixedArray, $incidentVecFixed);
		my $dummy = normalize($crossTrackArray);
		my $liftDirFixed = crossProduct($crossTrackArray, $incidentVecFixed);
		
		my $xL = $m_LiftArea * $liftDirFixed->[0];
		my $yL = $m_LiftArea * $liftDirFixed->[1];
		my $zL = $m_LiftArea * $liftDirFixed->[2];
		
		if($m_CLIndex > -1)
		{
		    printf DEBUGFILE "PV: %20.15e, %20.15e, %20.15e\n", $pvFixedArray->[0], $pvFixedArray->[1], $pvFixedArray->[2];
		    printf DEBUGFILE "Lift dir: %20.15e, %20.15e, %20.15e\n", $liftDirFixed->[0], $liftDirFixed->[1], $liftDirFixed->[2];
		    printf DEBUGFILE "Xtrack dir: %20.15e, %20.15e, %20.15e\n", $crossTrackArray->[0], $crossTrackArray->[1], $crossTrackArray->[2];
		    printf DEBUGFILE "Lift area: %20.15e\n", $m_LiftArea;
		    printf DEBUGFILE "CL partials: %20.15e, %20.15e, %20.15e\n", $xL, $yL, $zL;
		    printf DEBUGFILE "CL index: %2d\n", $m_CLIndex;
			$Result->SetReflectanceParamPartials($m_CLIndex, eFixed, $xL, $yL, $zL);
		}
		
		$xL *= $cl;
		$yL *= $cl;
		$zL *= $cl;
		
		$x += $xL;
		$y += $yL;
		$z += $zL;
		
	    #printf DEBUGFILE "Cd: %10.3f\nIncidence: %20.15e %20.15e %20.15e \n", 
        #	$cd, $incidentVecFixed->[0], $incidentVecFixed->[1], $incidentVecFixed->[2];
	    #printf DEBUGFILE "Cl: %10.3f\nLift: %20.15e %20.15e %20.15e \n", 
        #	$cd, $liftDirFixed->[0], $liftDirFixed->[1], $liftDirFixed->[2];
               
		$Result->SetReflectance(eFixed, $x, $y, $z);

		printf DEBUGFILE "reflectance: %20.15e, %20.15e, %20.15e\n", $x, $y, $z;

		my $incidentDirPosPartialsArrayFixed = $Result->IncidentDirectionCompPosPartials_Array(eFixed);

		if(defined($incidentDirPosPartialsArrayFixed))
		{
			#writeMatrix($incidentDirPosPartialsArrayFixed, "IncFixedPPs");

			# Position partials

		    	my $BCoeff = $cd * $m_DragArea;
			
			for ($k=0; $k<9; $k++)
			{
				$posParts[$k] = $BCoeff * $incidentDirPosPartialsArrayFixed->[$k] ;
			}

			$Result->SetReflectanceCompPosPartials( eFixed,
					$posParts[0], $posParts[1], $posParts[2],
					$posParts[3], $posParts[4], $posParts[5],
					$posParts[6], $posParts[7], $posParts[8]);

			#writeMatrix(\@posParts, "OutFixedPPs");

			# Velocity partials
			my $incidentDirVelPartialsArrayFixed = $Result->IncidentDirectionCompVelPartials_Array(eFixed);

			if(defined($incidentDirVelPartialsArrayFixed))
			{
				#writeMatrix($incidentDirVelPartialsArrayFixed, "IncFixedVPs");

				for ($k=0; $k<9; $k++)
				{
					$velParts[$k] = $BCoeff * $incidentDirVelPartialsArrayFixed->[$k] ;
				}

				$Result->SetReflectanceCompVelPartials( eFixed,
					$velParts[0], $velParts[1], $velParts[2],
					$velParts[3], $velParts[4], $velParts[5],
					$velParts[6], $velParts[7], $velParts[8]);

				#writeMatrix(\@velParts, "OutFixedVPs");
			}

			my $tempStr = "$m_MsgCntr> $cd $cl: ($x, $y, $z)";
			DebugMsg($tempStr);
			
			$enabled = true;
		}
	}
	
	return $enabled;
}


# ========================================================
#  PostCompute Method
# ========================================================
sub PostCompute
{
	my $Result = $_[0];
	
	if( $m_DebugMode == true )
	{
		if( $m_Enabled == true )
		{
			Message( eLogMsgDebug, "PostCompute(): called" );
			# now close debug file
            	close(DEBUGFILE);
		}
		else
		{
			Message( eLogMsgDebug, "PostCompute(): Disabled" );
		}
	}
	
	return $m_Enabled;
}


# ===========================================================
#  Free Method
# ===========================================================
sub Free()
{
	if( $m_DebugMode == true )
	{
		Message( eLogMsgDebug, "Free(): MsgCntr( $m_MsgCntr )" );
	}

	$m_AgUtPluginSite 		= undef;
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
#  DragArea property
# =======================================================
sub GetDragArea
{
	return $m_DragArea;
}

sub SetDragArea
{
	$m_DragArea = $_[0];
	printf DEBUGFILE "Drag area set: %20.15e\n", $m_DragArea;
}

# =======================================================
#  DragCoeff property
# =======================================================
sub GetDragCoefficient
{
	return $m_CD;
}

sub SetDragCoefficient
{
	$m_CD = $_[0];
}

# =======================================================
#  LiftArea property
# =======================================================
sub GetLiftArea
{
	return $m_LiftArea;
}

sub SetLiftArea
{
	$m_LiftArea = $_[0];
	printf DEBUGFILE "Lift area set: %20.15e\n", $m_LiftArea;
}

# =======================================================
#  LiftCoeff property
# =======================================================
sub GetLiftCoefficient
{
	return $m_CL;
}

sub SetLiftCoefficient
{
	$m_CL = $_[0];
}



sub writeMatrix{
	my $aVectorRef = $_[0];
	my $vecName = $_[1];
	printf DEBUGFILE "%10s   %20.15e   %20.15e   %20.15e\n", $vecName, $aVectorRef->[0], $aVectorRef->[1], $aVectorRef->[2];
	printf DEBUGFILE "             %20.15e   %20.15e   %20.15e\n",     $aVectorRef->[3], $aVectorRef->[4], $aVectorRef->[5];
	printf DEBUGFILE "             %20.15e   %20.15e   %20.15e\n",     $aVectorRef->[6], $aVectorRef->[7], $aVectorRef->[8];
}

# =====================================================
#   Copyright 2010, Analytical Graphics, Inc.          
# =====================================================

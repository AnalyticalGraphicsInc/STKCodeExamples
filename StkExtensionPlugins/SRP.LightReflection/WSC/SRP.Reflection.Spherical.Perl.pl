
#-----------------------------------------------------------------------------------
#
# The light reflectance being modeled is that of a sphere, 
# exactly like the Spherical model
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
use constant eInertial 		=> 0;
use constant eFixed 		=> 1;
use constant eLVLH 			=> 2;
use constant eNTC 			=> 3;
use constant eBodyFrame 	=> 4;

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
use constant eSTKEpochSec 	=> 4;
use constant eTDB 			=> 5;
use constant eGPS 			=> 6;

# ==========================================
#  Log Msg Type Enumeration
# ==========================================
use constant eLogMsgDebug	 	=> 0;
use constant eLogMsgInfo 		=> 1;
use constant eLogMsgForceInfo 	=> 2;
use constant eLogMsgWarning 	=> 3;
use constant eLogMsgAlarm 		=> 4;

# ==========================================
#  Sun Position Enumeration
# ==========================================
use constant eApparentToTrueCB 	=> 0;
use constant eApparent 			=> 1;
use constant eTrue 				=> 2;
use constant eSRP 				=> 3;

# ==========================================
#  AgEAttrAddFlags Enumeration
# ==========================================
use constant eFlagNone			=> 0;
use constant eFlagTransparent	=> 2;
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
my $m_CrIndex			= -1;
my $m_SrpArea			= 20.0;
my $m_RefFrame 			= eInertial;

my $m_MsgCntr			= -1;
my $m_Enabled			= true;
my $m_DebugMode			= true;
my $m_MsgInterval		= 500;

my $dirpath = 'C:\Temp\Sph';
my $Debug_out = $dirpath.'\Spherical_Debug.txt';



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

sub acos
{
	my $arg = $_[0];
	
	# deal with nuemrical roundoff issues
	
	my $temp = 1.0 - $arg*$arg;
	
	if($temp < 0.0)
	{
		$temp = 0.0;
	}
	elsif($temp > 1.0)
	{
		$temp = 1.0;
	}
	
	my $val = atan2(sqrt($temp), $arg);
	
	return $val;
	
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
	
	my $dot = $aArrayRef->[0] * $bArrayRef->[0] +
			  $aArrayRef->[1] * $bArrayRef->[1] +
			  $aArrayRef->[2] * $bArrayRef->[2];
			  
	return $dot;
}

sub scaleVector
{
	my $factor = $_[0];
	my $arrayRef = $_[1];
	
	my @newArray;
	push @newArray, $arrayRef->[0] * $factor;
	push @newArray, $arrayRef->[1] * $factor;
	push @newArray, $arrayRef->[2] * $factor;
	
	return \@newArray;
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

sub addToVector
{
	my $sumVectorRef = $_[0];
	my $addVectorRef = $_[1];
	
	$sumVectorRef->[0] += $addVectorRef->[0];
	$sumVectorRef->[1] += $addVectorRef->[1];
	$sumVectorRef->[2] += $addVectorRef->[2];
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
		
		 $AgAttrBuilder->AddQuantityDispatchProperty2 ( $m_AgAttrScope, "SRPArea", 
		 						"SRP Area", "SRPARea", 
		 						"Area", "m^2", "m^2", eFlagNone );
		 						
		 $AgAttrBuilder->AddChoicesDispatchProperty( $m_AgAttrScope, 
				 "RefFrame", 
				 "Reference Frame",    
				 "RefFrame", 
				 \@refFrameNamesArray );
										
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
		
		$m_CrIndex = $Result->RegisterParameter("Cr", 1.67, 1.0, 2.0, "Unitless");
		
		if($m_CrIndex > -1)
		{
			if( $m_DebugMode == true )
			{
				$Result->Message( eLogMsgInfo, "Registered Cr as Unitless parameter");
			}
		}
		else
		{
			$Result->Message( eLogMsgAlarm, "Unable to register Cr as Unitless parameter");
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
				open(DEBUGFILE,">$Debug_out");

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
		my $cr = 0.0;
		
		if($m_CrIndex > -1)
		{
			$cr = $Result->ParameterValue($m_CrIndex);
		}
		
		if($m_RefFrame == eBodyFrame)
		{
			$m_Enabled = setSphericalReflectanceInBody($Result, $cr);
		}
		else
		{
			$m_Enabled = setSphericalReflectanceUsingFrame($Result, $cr, $m_RefFrame);
		}
	}

	return $m_Enabled;
}

sub setSphericalReflectanceInBody
{
	my $Result = $_[0];
	my $cr = $_[1];

	my $enabled = false;

	my $incidentVecArrayRef = $Result->IncidentDirectionInBody_Array();
		
	if(defined($incidentVecArrayRef))
	{			
		# reflectance is positive along the incident direction
		my $x = $m_SrpArea * $incidentVecArrayRef->[0];
		my $y = $m_SrpArea * $incidentVecArrayRef->[1];
		my $z = $m_SrpArea * $incidentVecArrayRef->[2];

		if($m_CrIndex > -1)
		{
			$Result->SetReflectanceInBodyParamPartials($m_CrIndex, $x, $y, $z);
		}

		$x *= $cr;
		$y *= $cr;
		$z *= $cr;

		$Result->SetReflectanceInBody($x, $y, $z);

		my $incidentDirPosPartialsArrayRef = $Result->IncidentDirectionBodyCompPosPartials_Array();

		if(defined($incidentDirPosPartialsArrayRef))
		{
			my @posPartials;
			my $reflectanceMag = $cr*$m_SrpArea;

			$posPartials[0][0] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[0];
			$posPartials[0][1] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[1];
			$posPartials[0][2] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[2];

			$posPartials[1][0] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[3];
			$posPartials[1][1] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[4];
			$posPartials[1][2] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[5];

			$posPartials[2][0] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[6];
			$posPartials[2][1] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[7];
			$posPartials[2][2] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[8];

			$Result->SetReflectanceBodyCompPosPartials( 
						$posPartials[0][0], $posPartials[0][1], $posPartials[0][2],
						$posPartials[1][0], $posPartials[1][1], $posPartials[1][2],
						$posPartials[2][0], $posPartials[2][1], $posPartials[2][2]);

			# VelPartials are zero in inertial - we'll set this anyway to test it
			
			my $doVelPartials = 1;
			
			if($doVelPartials)
			{
				my $incidentDirVelPartialsArrayRef = $Result->IncidentDirectionBodyCompVelPartials_Array();
				
				if(defined($incidentDirVelPartialsArrayRef))
				{
					my @velPartials;
					
					$velPartials[0][0] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[0];
					$velPartials[0][1] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[1];
					$velPartials[0][2] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[2];

					$velPartials[1][0] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[3];
					$velPartials[1][1] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[4];
					$velPartials[1][2] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[5];

					$velPartials[2][0] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[6];
					$velPartials[2][1] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[7];
					$velPartials[2][2] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[8];

					$Result->SetReflectanceBodyCompVelPartials( 
						$velPartials[0][0], $velPartials[0][1], $velPartials[0][2],
						$velPartials[1][0], $velPartials[1][1], $velPartials[1][2],
						$velPartials[2][0], $velPartials[2][1], $velPartials[2][2]);
					
				}
			}

			my $tempStr = "$m_MsgCntr> $cr : frame = Body : ($x, $y, $z)";
			DebugMsg($tempStr);
			
			$enabled = true;
		}
	}
	
	return $enabled;
}

sub setSphericalReflectanceUsingFrame
{
	my $Result = $_[0];
	my $cr = $_[1];
	my $frame = $_[2];

	my $enabled = false;

	my $incidentVecArrayRef = $Result->IncidentDirection_Array($frame);

	if(defined($incidentVecArrayRef))
	{
		# reflectance is positive along the incident direction
		my $x = $m_SrpArea * $incidentVecArrayRef->[0];
		my $y = $m_SrpArea * $incidentVecArrayRef->[1];
		my $z = $m_SrpArea * $incidentVecArrayRef->[2];

		
	    printf DEBUGFILE "%10.3f  %10.3f  %17.10f %17.10f %17.10f  \n", 
                $cr, $m_SrpArea, $incidentVecArrayRef->[0], $incidentVecArrayRef->[1], $incidentVecArrayRef->[2];
               

		if($m_CrIndex > -1)
		{
			$Result->SetReflectanceParamPartials($m_CrIndex, $frame, $x, $y, $z);
		}

		$x *= $cr;
		$y *= $cr;
		$z *= $cr;

		$Result->SetReflectance($frame, $x, $y, $z);

		my $incidentDirPosPartialsArrayRef = $Result->IncidentDirectionCompPosPartials_Array($frame);

		if(defined($incidentDirPosPartialsArrayRef))
		{
			my @posPartials;
			my $reflectanceMag = $cr*$m_SrpArea;

			$posPartials[0][0] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[0];
			$posPartials[0][1] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[1];
			$posPartials[0][2] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[2];

			$posPartials[1][0] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[3];
			$posPartials[1][1] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[4];
			$posPartials[1][2] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[5];

			$posPartials[2][0] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[6];
			$posPartials[2][1] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[7];
			$posPartials[2][2] = $reflectanceMag * $incidentDirPosPartialsArrayRef->[8];

			$Result->SetReflectanceCompPosPartials($frame, 
						$posPartials[0][0], $posPartials[0][1], $posPartials[0][2],
						$posPartials[1][0], $posPartials[1][1], $posPartials[1][2],
						$posPartials[2][0], $posPartials[2][1], $posPartials[2][2]);

			# VelPartials are zero in inertial - we'll set this anyway to test it
			
			my $doVelPartials = 1;
			
			if($doVelPartials)
			{
				my $incidentDirVelPartialsArrayRef = $Result->IncidentDirectionCompVelPartials_Array($frame);
				
				if(defined($incidentDirVelPartialsArrayRef))
				{
					my @velPartials;
					
					$velPartials[0][0] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[0];
					$velPartials[0][1] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[1];
					$velPartials[0][2] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[2];

					$velPartials[1][0] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[3];
					$velPartials[1][1] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[4];
					$velPartials[1][2] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[5];

					$velPartials[2][0] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[6];
					$velPartials[2][1] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[7];
					$velPartials[2][2] = $reflectanceMag * $incidentDirVelPartialsArrayRef->[8];

					$Result->SetReflectanceCompVelPartials($frame, 
						$velPartials[0][0], $velPartials[0][1], $velPartials[0][2],
						$velPartials[1][0], $velPartials[1][1], $velPartials[1][2],
						$velPartials[2][0], $velPartials[2][1], $velPartials[2][2]);
					
				}
			}

			my $tempStr = "$m_MsgCntr> $cr : frame = $frame : ($x, $y, $z)";
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
#  SRPArea property
# =======================================================
sub GetSRPArea
{
	return $m_SrpArea;
}

sub SetSRPArea
{
	$m_SrpArea = $_[0];
}

# =====================================================
#   Ref Frame
# =====================================================
sub GetRefFrame
{       
	return $refFrameNamesArray[ $m_RefFrame ];
}

sub SetRefFrame
{
	my $refframe = $_[0];
		
	my $i; my $found = false;
	
	for($i=0; $i < $numFrames; $i++)
	{
		if("$refFrameNamesArray[$i]" eq "$refframe")
		{
			$m_RefFrame = $i;
			$found = true;
			last;
		}
	}
	
	if($found == false)
	{
		Win32:MsgBox "SetRefFrame : Could not find $refframe";
	}
}

sub GetRefFrameChoices
{
	return @refFrameNamesArray;
}

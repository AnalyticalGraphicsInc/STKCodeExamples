# =====================================================
#   Copyright 2005, Analytical Graphics, Inc.          
# =====================================================

# Must be running under Perl 5.6.1 for support of the Win32:OLE package.

require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;

# ==========================================
#  Reference Frames Enumeration
# ==========================================
use constant eUtFrameInertial 		=> 0;
use constant eUtFrameFixed 			=> 1;
use constant eUtFrameLVLH 			=> 2;
use constant eUtFrameNTC 			=> 3;

my $numFrames = 4;
my %refFrameNamesHash;
$refFrameNamesHash{0} = "eUtFrameInertial";
$refFrameNamesHash{1} = "eUtFrameFixed";
$refFrameNamesHash{2} = "eUtFrameLVLH";
$refFrameNamesHash{3} = "eUtFrameNTC";

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

# =+========================================
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

# =================================
#  Accel Type Enumeration
# =================================

use constant eTotalAccel 			=> 0;
use constant eTwoBodyAccel 			=> 1;
use constant eGravityAccel 			=> 2;
use constant ePerturbedGravityAccel => 3;
use constant eSolidTidesAccel 		=> 4;
use constant eOceanTidesAccel 		=> 5;
use constant eDragAccel 			=> 6;
use constant eSRPAccel 				=> 7;
use constant eThirdBodyAccel 		=> 8;
use constant eGenRelativityAccel 	=> 9;
use constant eAddedAccel 			=> 10;

# =================================
#  ForceModel Type Enumeration
# =================================

use constant eGravityModel 			=> 0;
use constant eSolidTidesModel		=> 1;
use constant eOceanTidesModel 		=> 2;
use constant eDragModel				=> 3;
use constant eSRPModel		 		=> 4;
use constant eThirdBodyModel 		=> 5;
use constant eGenRelativityModel 	=> 6;

# ==========================================
#  AgEAttrAddFlags Enumeration
# ==========================================
use constant eFlagNone			=> 0;
use constant eFlagTransparent	=> 2;
use constant eFlagHidden		=> 4;
use constant eFlagTransient		=> 8;  
use constant eFlagReadOnly		=> 16;
use constant eFlagFixed			=> 32;

# ==========================================
#  Declare Global Variables
# ==========================================
my $m_AgUtPluginSite		= undef;
my $m_AgStkPluginSite		= undef;
my $m_AgAttrScope			= undef;
my $m_CrdnPluginProvider	= undef;
my $m_CrdnConfiguredVector	= undef;

use constant true 	=> 1;
use constant false 	=> 0;

# ======================================
#  Declare Global 'Attribute' Variables
# ======================================
my $m_Name					= "Perl.Example1.Hpop.wsc";
my $m_Enabled				= true;
my $m_VectorName			= "Periapsis";
my $m_SRPArea				= 0.0;
my $m_SrpIsOn				= false;

my $m_AccelRefFrame			= 3;

my $m_AccelX				= 0.0;
my $m_AccelY				= 0.07;
my $m_AccelZ				= 0.0;

my $m_MsgStatus				= false;
my $m_EvalMsgInterval		= 5000;
my $m_PostEvalMsgInterval	= 5000;
my $m_PreNextMsgInterval	= 1000;

my $m_PreNextCntr			= 0;
my $m_EvalCntr				= 0;
my $m_PostEvalCntr			= 0;

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


# ========================
#  GetPluginConfig method
# ========================
sub GetPluginConfig
{
	my $AgAttrBuilder = $_[0];
	
	if( !defined($m_AgAttrScope) )
	{
		$m_AgAttrScope = $AgAttrBuilder->NewScope();
		
		# ===========================
		#  General Plugin attributes
		# ===========================
		$AgAttrBuilder->AddStringDispatchProperty( $m_AgAttrScope, "PluginName", "Human readable plugin name or alias",                 "Name",       0 );
		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, "PluginEnabled",     "If the plugin is enabled or has experience an error", "Enabled",    0 );
		$AgAttrBuilder->AddStringDispatchProperty( $m_AgAttrScope, "VectorName", "Vector Name that affects the srp area",              "VectorName", 0 );
				
		# ===========================
		#  Propagation related
		# ===========================
		$AgAttrBuilder->AddChoicesDispatchProperty( $m_AgAttrScope, "AccelRefFrame", "Acceleration Reference Frame",    "AccelRefFrame", \@refFrameNamesArray );
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, "AccelX",         "Acceleration in the X direction", "AccelX",        0 );
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, "AccelY",         "Acceleration in the Y direction", "AccelY",        0 );
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, "AccelZ",         "Acceleration in the Z direction", "AccelZ",        0 );
				
		# ==============================
		#  Messaging related attributes
		# ==============================
		$AgAttrBuilder->AddBoolDispatchProperty( $m_AgAttrScope, "UsePropagationMessages",     "Send messages to the message window during propagation",                               "MsgStatus",           0 );
		$AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, "EvaluateMessageInterval",  "The interval at which to send messages from the Evaluate method during propagation", "EvalMsgInterval",     0 );
        $AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, "PostEvaluateMessageInterval",  "The interval at which to send messages from the PostEvaluate method during propagation", "PostEvalMsgInterval",     0 );
		$AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, "PreNextStepMessageInterval", "The interval at which to send messages from the PreNextStep method during propagation", "PreNextMsgInterval", 0 );
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

    if( !( $m_AccelX <= 10 && $m_AccelX >= -10 ) )
    {
    	$Result  = false;
    	$Message = "AccelX was not within the range of -10 to +10 meters per second squared";
    }
    elsif( !( $m_AccelY <= 10 && $m_AccelY >= -10 ) )
    {
    	$Result  = false;
    	$Message = "AccelY was not within the range of -10 to +10 meters per second squared";
	}
	elsif( !( $m_AccelZ <= 10 && $m_AccelZ >= -10 ) )
	{
    	$Result  = false;
    	$Message = "AccelZ was not within the range of -10 to +10 meters per second squared";
	}

	$AgUtPluginConfigVerifyResult->{Result}  = $Result;
	$AgUtPluginConfigVerifyResult->{Message} = $Message;
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
		if( $m_Enabled == true )
		{
			# if called by STK, get Vector Tool provider to do SRP Area computation
			
			my $siteName = $m_AgUtPluginSite->{SiteName};
			
			if($siteName eq "IAgStkPluginSite" || $siteName eq "IAgGatorPluginSite")
			{
				$m_CrdnPluginProvider 	= $m_AgUtPluginSite->{VectorToolProvider};
				
				if( defined($m_CrdnPluginProvider) )
				{
					$m_CrdnConfiguredVector  = $m_CrdnPluginProvider->ConfigureVector( $m_VectorName, "", "J2000", "CentralBody/Earth");
				}
		
				if( $m_MsgStatus == true )
				{
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Init():" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): AccelRefFrame( " . GetAccelRefFrame() . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): AccelX( " . $m_AccelX . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): AccelY( " . $m_AccelY . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): AccelZ( " . $m_AccelZ . " )" );
				}
				
				if( !defined($m_CrdnConfiguredVector) )
				{
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): Could not obtain $m_VectorName" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): Turning off the computation of SRP Area" );
				}
				
				my $scenDir = $m_AgUtPluginSite->ScenarioDirectory();
				$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): ScenDir = $scenDir" );
				
				my $installDir = $m_AgUtPluginSite->InstallDirectory();
				$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): InstallDir = $installDir" );
				
				my $configDir = $m_AgUtPluginSite->ConfigDirectory();
				$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): ConfigDir = $configDir" );
			}
			else
			{
				$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): $siteName does not provide VectorToolProvider" );
				$m_AgUtPluginSite->Message( eLogMsgDebug, "Init(): Turning off the computation of SRP Area" );
			}
		}
		else
		{
			$m_AgUtPluginSite->Message( eLogMsgDebug, "$m_Name.Init(): Disabled because Enabled flag is False" );
		}
	}
    
    return $m_Enabled;
} 

# ======================
#  PrePropagate Method
# ======================
sub PrePropagate
{
	my $AgAsHpopPluginResult = $_[0];
	
	if( defined($m_AgUtPluginSite) )
	{
		if( $m_Enabled == true )
		{
			if( defined($AgAsHpopPluginResult) )
			{
				my $WholeDays_Index   = 0;
				my $SecsIntoDay_Index = 1;
				my $Year_Index        = 2;
				my $DayOfYear_Index   = 3;
				my $Month_Index       = 4;
                my $DayOfMonth_Index   = 5;
				my $Hours_Index       = 6;
				my $Minutes_Index     = 7;
				my $Seconds_Index     = 8;
			
				my $arrayRef = $AgAsHpopPluginResult->RefEpochElements_Array( eUTC );
				
				$m_SrpIsOn = $AgAsHpopPluginResult->IsForceModelOn( eSRPModel );
				
				if($m_SrpIsOn)
				{
					$m_SRPArea = $AgAsHpopPluginResult->{SRPArea};
				}
			
				if( $m_MsgStatus == true )
				{
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate():" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch WholeDays( " . $arrayRef->[$WholeDays_Index] . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch SecsIntoDay( " . $arrayRef->[$SecsIntoDay_Index] . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch Year( " . $arrayRef->[$Year_Index] . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch DayOfYear( " . $arrayRef->[$DayOfYear_Index] . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch Month( " . $arrayRef->[$Month_Index] . " )" );
                    $m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch DayOfMonth( " . $arrayRef->[$DayOfMonth_Index] . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch Hours( " . $arrayRef->[$Hours_Index] . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch Minutes( " . $arrayRef->[$Minutes_Index] . " )" );
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Epoch Seconds( " . $arrayRef->[$Seconds_Index] . " )" );
				}			
			}			
		}
		else
		{
			if( $m_MsgStatus == true )
			{	
				$m_AgUtPluginSite->Message( eLogMsgDebug, "PrePropagate(): Disabled" );
			}
		}
	}
	
	return $m_Enabled;
}


# ======================
#  PreNextStep Function
# ======================
sub PreNextStep
{
	my $AgAsHpopPluginResult = $_[0];
	
	$m_PreNextCntr++;

	if( defined($m_AgUtPluginSite) )
	{
		if( $m_Enabled == true )
		{
			if( $m_MsgStatus == true )
			{
				if( ($m_PreNextCntr % $m_PreNextMsgInterval) == 0 )
				{
					$m_AgUtPluginSite->Message( eLogMsgDebug, "PreNextStep( $m_PreNextCntr ):" );
				}
			}
		}
		else
		{
			if( $m_MsgStatus == true )
			{
				$m_AgUtPluginSite->Message( eLogMsgDebug, "PreNextStep(): Disabled" );			
			}
		}
	}

	return $m_Enabled;
}

# =================
#  Evaluate Method
# =================
sub Evaluate
{
	my $AgAsHpopPluginResultEval = $_[0];
	
	$m_EvalCntr++;
	
	if( defined($m_AgUtPluginSite) )
	{	
		if( $m_Enabled == true )
		{
			EvaluateSRPArea( $AgAsHpopPluginResultEval );

			$AgAsHpopPluginResultEval->AddAcceleration( $m_AccelRefFrame, $m_AccelX, $m_AccelY, $m_AccelZ );

			if( $m_MsgStatus == true )
			{
				if( $m_EvalCntr % $m_EvalMsgInterval == 0 )
				{
					$m_AgUtPluginSite->Message( eLogMsgDebug, "Evaluate( " . $m_EvalCntr . " ):" );
				}
			}
		}
		else
		{
			if( $m_MsgStatus == true )
			{
				$m_AgUtPluginSite->Message( eLogMsgDebug, "Evalate(): Disabled" );
			}
		}
	}
	
	return $m_Enabled;
}

sub EvaluateSRPArea
{
	my $ResultEval = $_[0];
	
	my $Result = true;
	
	if(!$m_SrpIsOn)
	{
		return $Result;
	}
	
	# NOTE: $m_CrdnConfiguredVector may be null if plugin not run from STK
	if( !defined($m_CrdnConfiguredVector) )
	{
		# just return true
		return $Result;
	}
	
	my $PosX_Index		= 0;
	my $PosY_Index		= 1;
	my $PosZ_Index		= 2;
	my $VelX_Index		= 3;
	my $VelY_Index		= 4;
	my $VelZ_Index		= 5;
	
	my $VecX_Index		= 0;
	my $VecY_Index		= 1;
	my $VecZ_Index		= 2;

	my $VecPosDotProd	= 0.0;
	my $VecMag			= 0.0;
	my $PosMag			= 0.0;
	my $Theta			= 0.0;

	if( defined($ResultEval) )
	{
		my $posVelArrayRef = $ResultEval->PosVel_Array( eUtFrameInertial );
				
		my $vecArrayRef = $m_CrdnConfiguredVector->CurrentValue_Array( $ResultEval );
			
		$VecPosDotProd 	= ( $vecArrayRef->[$VecX_Index] * $posVelArrayRef->[$PosX_Index] ) + 
						  ( $vecArrayRef->[$VecY_Index] * $posVelArrayRef->[$PosY_Index] ) + 
						  ( $vecArrayRef->[$VecZ_Index] * $posVelArrayRef->[$PosZ_Index] );
		$VecMag			= sqrt( $vecArrayRef->[$VecX_Index] * $vecArrayRef->[$VecX_Index] + 
								$vecArrayRef->[$VecY_Index] * $vecArrayRef->[$VecY_Index] +
								$vecArrayRef->[$VecZ_Index] * $vecArrayRef->[$VecZ_Index] );
		$PosMag			= sqrt( $vecArrayRef->[$PosX_Index] * $vecArrayRef->[$PosX_Index] + 
								$vecArrayRef->[$PosY_Index] * $vecArrayRef->[$PosY_Index] +
								$vecArrayRef->[$PosZ_Index] * $vecArrayRef->[$PosZ_Index] );
		$Theta			= acos( $VecPosDotProd / ( $VecMag * $PosMag ) );
			
		# SRP must be on else this may be a run-time error
		$ResultEval->{SRPArea} = ( $m_SRPArea / 4.0 ) * ( 3 - sin( $Theta ) );
		
		if( defined($m_AgUtPluginSite) && $m_MsgStatus == true )
		{
			if( $m_EvalCntr % $m_EvalMsgInterval == 0 )
			{
				my $ThetaDeg = $Theta * 57.2957795130823208767;
				
				$m_AgUtPluginSite->Message( eLogMsgDebug, "EvaluateSRPArea( " . $m_EvalCntr . " ): VecX( " . $vecArrayRef->[$VecX_Index] . " ), VecY( " . $vecArrayRef->[$VecY_Index] . " ), VecZ( " . $vecArrayRef->[$VecZ_Index] . " ) meters/sec" );
				$m_AgUtPluginSite->Message( eLogMsgDebug, "EvaluateSRPArea( " . $m_EvalCntr . " ): PosX(" . $posVelArrayRef->[$PosX_Index] . " ), PosY( " . $posVelArrayRef->[$PosY_Index] . " ), PosZ( " . $posVelArrayRef->[$PosZ_Index] . " ) meters" );
				$m_AgUtPluginSite->Message( eLogMsgDebug, "EvaluateSRPArea( " . $m_EvalCntr . " ): VelX(" . $posVelArrayRef->[$VelX_Index] . " ), VelY( " . $posVelArrayRef->[$VelY_Index] . " ), VelZ(" . $posVelArrayRef->[$VelZ_Index] . " ) meters/sec" );
				$m_AgUtPluginSite->Message( eLogMsgDebug, "EvaluateSRPArea( " . $m_EvalCntr . " ): SRPArea(" . $ResultEval->{SRPArea} . " m^2), Theta( " . $ThetaDeg . " deg)" );
			}
		}
	}
	else
	{
		if( defined($m_AgUtPluginSite) && $m_MsgStatus == true )
		{
			$m_AgUtPluginSite->Message( eLogMsgWarning, "Result Eval was undef" );
		}
	}
}

# =================
#  PostEvaluate Method
# =================
sub PostEvaluate
{
	my $AgAsHpopPluginResultPostEval = $_[0];
	
	$m_PostEvalCntr++;
	
	if( defined($m_AgUtPluginSite) && defined($AgAsHpopPluginResultPostEval) )
	{	
		if( $m_Enabled == true )
		{
			if( $m_MsgStatus == true )
			{
				if( $m_PostEvalCntr % $m_PostEvalMsgInterval == 0 )
				{
					my $reportFrame = eUtFrameNTC;
					my $frameName = "NTC";

					my $accelType = eSRPAccel;
					my $AltInKm;

					$AltInKm = $AgAsHpopPluginResultPostEval->{Altitude}*0.001;
					
					$m_AgUtPluginSite->Message( eLogMsgDebug, 
						"PostEvaluate( " . $m_PostEvalCntr . " ):" .  " ): SRPArea (" .
						$AgAsHpopPluginResultPostEval->{SRPArea}." m^2), Altitude ($AltInKm km)" );

					my $accelArrayRef = $AgAsHpopPluginResultPostEval->GetAcceleration_Array( $accelType, $reportFrame);
					
					$m_AgUtPluginSite->Message( eLogMsgDebug, 
						"PostEvaluate( " . $m_PostEvalCntr . " ):" . " ): SRPAccel (" .
						$frameName.") is (".$accelArrayRef->[0].", ".$accelArrayRef->[1].", ".
						$accelArrayRef->[2].") meters/secs^2" );

					#  report out the added acceleration in NTC components
					$accelType = eAddedAccel;
					
					$accelArrayRef = $AgAsHpopPluginResultPostEval->GetAcceleration_Array( $accelType, $reportFrame);

					$m_AgUtPluginSite->Message( eLogMsgDebug, 
						"PostEvaluate( " . $m_PostEvalCntr . " ):" . " ): ThrustAccel (" .
						$frameName.") is (".$accelArrayRef->[0].", ".$accelArrayRef->[1].", ".
						$accelArrayRef->[2].") meters/secs^2" );
				}
			}
		}
		else
		{
			if( $m_MsgStatus == true )
			{
				$m_AgUtPluginSite->Message( eLogMsgDebug, "PostEvalate(): Disabled" );
			}
		}
	}
	
	return $m_Enabled;
}

# ========================================================
#  PostPropagate Method
# ========================================================
sub PostPropagate
{
	my $AgAsHpopPluginResult = $_[0];
	
	if( defined($m_AgUtPluginSite) )
	{	
		if( $m_Enabled == true )
		{
			if( $m_MsgStatus == true )
			{
				$m_AgUtPluginSite->Message( eLogMsgDebug, "PostPropagate():" );
			}			
		}
		else
		{
			if( $m_MsgStatus == true )
			{
				$m_AgUtPluginSite->Message( eLogMsgDebug, "PostPropagate(): Disabled" );
			}
		}
	}
	
	return $m_Enabled;
}


# ===========================================================
#  Free Method
# ===========================================================
sub Free()
{
	if( defined($m_AgUtPluginSite) )
	{	
		if( $m_MsgStatus == true )
		{
			$m_AgUtPluginSite->Message( eLogMsgDebug, "Free():" );
			$m_AgUtPluginSite->Message( eLogMsgDebug, "Free(): PreNextCntr( " . $m_PreNextCntr . " )" );
			$m_AgUtPluginSite->Message( eLogMsgDebug, "Free(): EvalCntr( " . $m_EvalCntr . " )" );
			$m_AgUtPluginSite->Message( eLogMsgDebug, "Free(): PosEvalCntr( " . $m_PostEvalCntr . " )" );
		}
		
		$m_AgUtPluginSite 		= undef;
		$m_CrdnPluginProvider   = undef;
		$m_CrdnConfiguredVector = undef;
	}
}

# =============================================================
#  Name Method
# =============================================================
sub GetName
{
	return $m_Name;
}

sub SetName
{
	$m_Name = $_[0];
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

# ============================================================
#  VectorName property
# ============================================================
sub GetVectorName
{
	return $m_VectorName;
}

sub SetVectorName
{
	$m_VectorName = $_[0];
}

sub GetAccelRefFrame
{       
	return $refFrameNamesArray[ $m_AccelRefFrame ];
}

sub SetAccelRefFrame
{
	my $accelrefframe = $_[0];
	
	my $i;
	
	for($i=0; $i < $numFrames; $i++)
	{
		if($refFrameNamesArray[$i] eq $accelrefframe)
		{
			$m_AccelRefFrame = $i;
			last;
		}
	}
}

sub GetAccelRefFrameChoices
{
	return @refFrameNamesArray;
}

# ==========================================================
#  AccelX property
# ==========================================================
sub GetAccelX
{
	return $m_AccelX;
}

sub SetAccelX
{
	$m_AccelX = $_[0];
}

# =========================================================
#  AccelY property
# =========================================================
sub GetAccelY
{
	return $m_AccelY;
}

sub SetAccelY
{
	$m_AccelY = $_[0];
}

# =========================================================
#  AccelZ property
# =========================================================
sub GetAccelZ
{
	return $m_AccelZ;
}

sub SetAccelZ
{
	$m_AccelZ = $_[0];
}

# ======================================================
#  MsgStatus property
# ======================================================
sub GetMsgStatus
{
	return $m_MsgStatus;
}

sub SetMsgStatus
{
    $m_MsgStatus = $_[0];
}

# =======================================================
#  EvalMsgInterval property
# =======================================================
sub GetEvalMsgInterval
{
	return $m_EvalMsgInterval;
}

sub SetEvalMsgInterval
{
	$m_EvalMsgInterval = $_[0];
}

# =======================================================
#  PostEvalMsgInterval property
# =======================================================
sub GetPostEvalMsgInterval
{
	return $m_PostEvalMsgInterval;
}

sub SetPostEvalMsgInterval
{
	$m_PostEvalMsgInterval = $_[0];
}

# =======================================================
#  PreNextMsgInterval property
# =======================================================
sub GetPreNextMsgInterval
{
	return $m_PreNextMsgInterval;
}

sub SetPreNextMsgInterval
{
	$m_PreNextMsgInterval = $_[0];
} 
# =====================================================
#   Copyright 2005, Analytical Graphics, Inc.          
# =====================================================

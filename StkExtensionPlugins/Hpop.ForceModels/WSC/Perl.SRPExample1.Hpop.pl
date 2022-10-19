# =====================================================
#   Copyright 2005, Analytical Graphics, Inc.          
# =====================================================

#-----------------------------------------------------------------------------------
# Intended use:	Computes SRP acceleration for a TDRS spacecraft in orbit using
#				the Pechenick model as described in "TDRS Solar Pressure Model
#				For Filtering", Pechenick, K. and Hujsak, R., Applied
#				Technology Associates Of Delaware, Inc., Dec 1987.
#
#				The intended use is such that CR = 1.0 should be an
#				approximately correct filter input.
#
#				The model computes diffuse and specular reflection 
#				contributions. Contributions are even made in directions
#				orthogonal to the sunlight vector (called "sailing" for solar 
#				pressure).
#
#  Caveat Emptor: The TDRS satellite is geosynchronous, with the solar panel 
#				extending on a mast from the crosstrack surface of the 
#				satellite. Additionally, there is a solar sail, a AW C-band 
#				antenna, and a SGL antenna that are nominally earth-pointing.
#				This model takes advantage of these facts in several
#				places. Any modification for other satellites should be 
#				careful of the assumption.
#
#	Special Note:  ODTK option for "diffuse reflecting sphere" should NOT 
#				be used because it multiplies the CR by 1 + 4./9.
#-----------------------------------------------------------------------------------
#	This model uses coefficients of specular and diffuse reflection for flat 
#		plates and is designed to facilitate ODTK adaptability. This adds some
#		complexity that a typical STK/HPOP user might not implement
#
#	1. The satellite should be configured to compute the SRP acceleration 
#	   so that all necessary SRP-related variables are computed
#
#	2. The entire SRP contribution to the acceleration, as computed by HPOP 
#	   internally, will be subtracted out
#
#	3. The SRP acceleration contribution, as computed by the Pechenick model,
#	   will then be added
#
#--------------------SOLAR PRESSURE DETAIL-----------------------------------------
#
#	The standard solar pressure model for a sphere is of the form
#		accel(sphere) = CR * X1 * k
#           X1 = A/M * Illum * Irrad / c
#		where CR = 1.0 for a perfectly reflective sphere
#				A = Area, M = Mass, 
#				Illum = illumination factor (0 <= Illum <= 1 )
#				Irrad = irradiance in Watts/Meter^2 
#					  = solar flux = Luminosity/(4*pi*distance_sunFromSat^2),
#				c = speed of light
#				k = unit vector from sun to satellite
#	For the Pechenick model there are three acceleration directions: 
#				along k
#				along k x ( k x N ) [N is normal to solar panel surface
#									 and arises from solar panels]
#				along k x ( k x M ) [M is radial is arises from some earth
#									 pointing sub-structures]
#	and the model has the form:
#		accel = CR * ( a1 * k + a2 * k x ( k x N ) + a3 * k x ( k x M ) ) 
#						* Illum * Irrad /c/M
#
#-----------------------------------------------------------------------------------

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
my $m_AgAttrScope			= undef;

my $m_speedOfLight = 299792458.0;
my $m_spacecraftMass = 1764.17;
my $m_busRadius = 1.157;		# effective radius in meters (R_tilde_b)
my $m_SA_AntennaRadius = 0.945;	# effective radius in meters (R_tidle_E)
my $m_solarPanelArrayArea = 29.518;	# meters^2 (A_P_1)
my $m_otherArea = 6.454;	# meters^2 (A_P_2)

my $m_busTerm;
my $m_SA_Term;
my $m_BP1Term;
my $m_BP2Term;
my @m_sunlightSRP = (); 

my $m_MsgCntr		= -1;
my $m_EvalMsgsOn	= 0;
my $m_EvalMsgCount	= 0;
my $m_EvalMsgMax 	= 1;

use constant PI => 3.1415926535897932384;

use constant false	=> 0;
use constant true	=> 1;

# ======================================
#  Declare Global 'Attribute' Variables
# ======================================
my $m_Name					= "Hpop.FrcMdl.Srp.Perl.Example1";
my $m_Enabled				= true;
my $m_DebugMode				= false;
my $m_MsgInterval			= 500;
my $m_diffuseReflectivity	= 0.75;		# 0 <= value <= (1-$m_specularReflectivity)
my $m_specularReflectivity	= 0.25;		# 0 <= value <= 1.0

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
	
	if($m_DebugMode && $m_EvalMsgsOn)
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


sub computeSRP
{
	my $illum = $_[0];
	my $cr = $_[1];
	my $solarFlux = $_[2];
	my $posVelArrayRef = $_[3];
	my $satToSunArrayRef = $_[4];
	my $sunlightSrpArrayRef = $_[5];
	my $sailingSrpArrayRef = $_[6];
	
	#
	# Begin computation of SRP using the Pechenick model
	#

	my @velArray; 
	push @velArray, $posVelArrayRef->[3];
	push @velArray, $posVelArrayRef->[4];
	push @velArray, $posVelArrayRef->[5];

	my $wArrayRef = crossProduct($posVelArrayRef, \@velArray);
	normalize($wArrayRef);		# unit cross track vector (w_hat)

	normalize($posVelArrayRef);	# 1st 3 elements are unit radial vector (u_hat)

	my $kArrayRef = $satToSunArrayRef;
	my $distance_sunToSat = normalize($kArrayRef);

	$kArrayRef->[0] *= -1.0;	# unit vector from Sun to Sat (k_hat)
	$kArrayRef->[1] *= -1.0;
	$kArrayRef->[2] *= -1.0;

	#DebugMsg("DistSun $distance_sunToSat");
	#DebugMsg("K : ($kArrayRef->[0], $kArrayRef->[1], $kArrayRef->[2])");

	my $cArrayRef = crossProduct($wArrayRef, $kArrayRef);
	normalize($cArrayRef);		# unit vector in orbit plane in plane of solar array (c_hat)

	my $nArrayRef = crossProduct($wArrayRef, $cArrayRef);
	normalize($nArrayRef);		# unit vector in orbit plane normal to solar panel face towards sun
								# (solar panel faces sun_to_sat direction)

	#DebugMsg("N : ($nArrayRef->[0], $nArrayRef->[1], $nArrayRef->[2])");

	my $dotP =  dotProduct($posVelArrayRef, $kArrayRef);

	if($dotP >= 0.0)
	{
		$dotP = -1.0;
	}
	else
	{
		$dotP = 1.0;
	}

	my @mArray;	# unit radial or anti-radial  (m_hat)
	push @mArray, $dotP * $posVelArrayRef->[0];
	push @mArray, $dotP * $posVelArrayRef->[1];
	push @mArray, $dotP * $posVelArrayRef->[2];

	#DebugMsg("M : ($mArray[0], $mArray[1], $mArray[2])");

	my $k_n_ArrayRef = crossProduct($kArrayRef, $nArrayRef);
	my $k_k_n_ArrayRef = crossProduct($kArrayRef, $k_n_ArrayRef);

	#DebugMsg("KKN : ($k_k_n_ArrayRef->[0], $k_k_n_ArrayRef->[1], $k_k_n_ArrayRef->[2])");

	my $k_m_ArrayRef = crossProduct($kArrayRef, \@mArray);
	my $k_k_m_ArrayRef = crossProduct($kArrayRef, $k_m_ArrayRef);

	#DebugMsg("KKM : ($k_k_m_ArrayRef->[0], $k_k_m_ArrayRef->[1], $k_k_m_ArrayRef->[2])");

	# compute angles

	my $cosAlpha = -1.0 * dotProduct($nArrayRef, $kArrayRef);	# n_hat and k_hat are almost anti-aligned
	my $cos2Alpha = 2.0*$cosAlpha*$cosAlpha - 1.0;

	my $cosAlphaStar = -1.0 * dotProduct(\@mArray, $kArrayRef);
	my $cos2AlphaStar = 2.0*$cosAlphaStar*$cosAlphaStar - 1.0;

	#DebugMsg("Cos(alpha) = $cosAlpha, Cos(alphaStar) = $cosAlphaStar");

	# compute some aux qtys

	# NOTE: we are using the formulas here with the app providing Luminosity (thru the solar flux value),
	#		Mass, Cr, spacecraft mass, speed of light. Thus, the value for B_tilde_P_i and C_tilde
	#		from the paper will be computed on the fly, rather than being assumed constant
	#
	#		Also, there is a typo in formula (3.1) on page 5: in that formula, r 
	#		means magnitude(sun_to_Sat_vector)
	
	# NOTE: Cr is applied only to the k direction, not kkn nor kkm

	my $C_Term = $cr * $illum * $solarFlux * ($m_busTerm + 2*$m_SA_Term);
	my $B_P_1_Term = $illum * $solarFlux * $m_BP1Term;
	my $B_P_2_Term = $illum * $solarFlux * $m_BP2Term;

	# compute contributions

	my $C_Term_Contrib_ArrayRef = scaleVector($C_Term, $kArrayRef);

	my $tempVal = $cr * $B_P_1_Term * $cosAlpha * ( (3.0 + 2.0*$cosAlpha) + 
								$m_specularReflectivity * (3.0*$cos2Alpha - 2.0*$cosAlpha) );

	my $B_P_1_Term_Contrib_k_ArrayRef = scaleVector($tempVal, $kArrayRef);

	$tempVal = $B_P_1_Term * $cosAlpha * (2.0 + $m_specularReflectivity * (6.0*$cosAlpha - 2.0) );

	my $B_P_1_Term_Contrib_kkn_ArrayRef = scaleVector($tempVal, $k_k_n_ArrayRef);

	$tempVal = $cr * $B_P_2_Term * $cosAlphaStar * ( (3.0 + 2.0*$cosAlphaStar) + 
								 $m_specularReflectivity * (3.0*$cos2AlphaStar - 2.0*$cosAlphaStar) );

	my $B_P_2_Term_Contrib_k_ArrayRef = scaleVector($tempVal, $kArrayRef);

	$tempVal = $B_P_2_Term * $cosAlphaStar * (2.0 + $m_specularReflectivity * (6.0*$cosAlphaStar - 2.0) );

	my $B_P_2_Term_Contrib_kkm_ArrayRef = scaleVector($tempVal, $k_k_m_ArrayRef);

	# add all contributions

	my @sailContrib; push @sailContrib, 0.0; push @sailContrib, 0.0; push @sailContrib, 0.0;

	addToVector(\@sailContrib, $B_P_1_Term_Contrib_kkn_ArrayRef);
	addToVector(\@sailContrib, $B_P_2_Term_Contrib_kkm_ArrayRef);

	DebugMsg("Sailing SRP = ($sailContrib[0], $sailContrib[1], $sailContrib[2])");
	
	@{$sailingSrpArrayRef} = @sailContrib;

	my @sunlightContrib;	push @sunlightContrib, 0.0; push @sunlightContrib, 0.0; push @sunlightContrib, 0.0;

	addToVector(\@sunlightContrib, $C_Term_Contrib_ArrayRef);
	addToVector(\@sunlightContrib, $B_P_1_Term_Contrib_k_ArrayRef);
	addToVector(\@sunlightContrib, $B_P_2_Term_Contrib_k_ArrayRef);

	DebugMsg("Sunlight SRP = ($sunlightContrib[0], $sunlightContrib[1], $sunlightContrib[2])");
		
	@{$sunlightSrpArrayRef} = @sunlightContrib;
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
		$AgAttrBuilder->AddStringDispatchProperty( $m_AgAttrScope, "PluginName", "Human readable plugin name or alias",                 "Name",       eFlagNone );
		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, "PluginEnabled",     "If the plugin is enabled or has experienced an error", "Enabled",    eFlagNone );
		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, "DebugMode",     "Turn debug messages on or off", "DebugMode",    eFlagNone );
			
		# ==============================
		#  Reflectivity related attributes
		# ==============================
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, "Reflectivity_Specular", "Specular reflectivity coefficient", "SpecularReflectivity",        eFlagNone );
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, "Reflectivity_Diffuse", "Diffuse reflectivity coefficient", "DiffuseReflectivity",        eFlagNone );
				
		# ==============================
		#  Messaging related attributes
		# ==============================
		$AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, "MessageInterval",  "The interval at which to send messages during propagation in Debug mode", "MsgInterval",     eFlagNone );
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
    
    if($m_specularReflectivity < 0.0 || $m_specularReflectivity > 1.0)
    {
		$Result = false;
		$Message = "Specular Reflectivity must be between 0.0 and 1.0";
	}
	elsif($m_diffuseReflectivity < 0.0 || $m_diffuseReflectivity > (1.0 - $m_specularReflectivity))
	{
		$Result = false;
		$Message = "Diffuse Reflectivity must be between 0.0 and (1.0 - Specular)";
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
		#Win32::MsgBox("<Enabled ,Debug> = $m_Enabled, $m_DebugMode", 0);
		 
		if( $m_DebugMode == true )
		{
			if( $m_Enabled == true )
			{
				Message( eLogMsgInfo, "$m_Name.Init(): Enabled" );
			}
			else
			{
				Message( eLogMsgInfo, "$m_Name.Init(): Disabled because Enabled flag is False" );
			}
		}
		elsif($m_Enabled == false)
		{
			Message( eLogMsgAlarm, "$m_Name.Init(): Disabled because Enabled flag is False" );
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
	
	if( $m_Enabled == true )
	{
		if( defined($AgAsHpopPluginResult) )
		{
			# Insure that SRP is On
			my $srpIsOn = $AgAsHpopPluginResult->IsForceModelOn(eSRPModel);

			if(!$srpIsOn)
			{
				$m_Enabled = false;

				Message( eLogMsgAlarm, "$m_Name.PrePropagate(): SRP must be ON for this plugin to work but is currently OFF." );
				Message( eLogMsgAlarm, "$m_Name.PrePropagate(): Turning OFF all methods for $m_Name");

				return $m_Enabled;
			}
			
			# compute terms that don't change over time
			
			$m_speedOfLight = $AgAsHpopPluginResult->{LightSpeed};
			$m_spacecraftMass = $AgAsHpopPluginResult->{TotalMass};
			
			my $tempVal = $m_spacecraftMass * $m_speedOfLight;
			
			if( $m_DebugMode == true )
			{
				Message(eLogMsgDebug, "$m_Name.PrePropagate(): Mass = $m_spacecraftMass");
				Message(eLogMsgDebug, "$m_Name.PrePropagate(): c = $m_speedOfLight");
			}
			
			$m_busTerm = PI * $m_busRadius * $m_busRadius * 
								(1.0 + 4.0/9.0 * $m_diffuseReflectivity) / $tempVal;
								
			$m_SA_Term = PI * $m_SA_AntennaRadius * $m_SA_AntennaRadius * 
								(1.0 + 4.0/9.0 * $m_diffuseReflectivity) / $tempVal;
									
			$m_BP1Term = $m_solarPanelArrayArea / (3.0 * $tempVal);
			$m_BP2Term = $m_otherArea / (3.0 * $tempVal);
			
			if( $m_DebugMode == true )
			{
				Message(eLogMsgDebug, "$m_Name.PrePropagate(): BP1Term = $m_BP1Term" );
				Message(eLogMsgDebug, "$m_Name.PrePropagate(): BP2Term = $m_BP2Term" );
				Message(eLogMsgDebug, "$m_Name.PrePropagate(): Bus_Term = $m_busTerm" );
				Message(eLogMsgDebug, "$m_Name.PrePropagate(): SA_Term = $m_SA_Term" );
			}
		}			
	}
	elsif( $m_DebugMode == true )
	{	
		Message( eLogMsgDebug, "$m_Name.PrePropagate(): Disabled" );
	}
	
	return $m_Enabled;
}


# ======================
#  PreNextStep Function
# ======================
sub PreNextStep
{
	my $AgAsHpopPluginResult = $_[0];
	
	$m_MsgCntr++;
	
	if( $m_Enabled == true )
	{
		if( $m_DebugMode == true )
		{
			if( ($m_MsgCntr % $m_MsgInterval) == 0 )
			{
				my $deltaT = "unknown";
				
				if( defined($AgAsHpopPluginResult) )
				{
					$deltaT = $AgAsHpopPluginResult->TimeSinceRefEpoch();
				}
					
				Message( eLogMsgDebug, "$m_Name.PreNextStep( $m_MsgCntr ): Time since Ref Epoch = $deltaT secs" );
			}
		}
	}
	elsif( $m_DebugMode == true )
	{
		Message( eLogMsgDebug, "$m_Name.PreNextStep(): Disabled" );			
	}
	
	$m_EvalMsgsOn = 1;
	$m_EvalMsgCount = 0;

	return $m_Enabled;
}

# =================
#  Evaluate Method
# =================
sub Evaluate
{
	my $Result = $_[0];
	
	if($m_Enabled == true && defined($Result) )
	{
		# if illumination is zero, there isn't any contribution anyway, so do nothing
				
		my $illum = $Result->{SolarIntensity};

		if($illum == 0.0)
		{
			return $m_Enabled;
		}
		
		my $cr = $Result->{Cr};
		my $solarFlux = $Result->{SolarFlux};		# L /(4 * pi * R_sun^2)
		my $posVelArrayRef = $Result->PosVel_Array(eUtFrameInertial);
		my $satToSunArrayRef = $Result->SunPosition_Array(eSRP, eUtFrameInertial);	
		
		@m_sunlightSRP = ();	# this is a global
		my @sailingSRP = (); 
		
		computeSRP($illum, $cr, $solarFlux, $posVelArrayRef, $satToSunArrayRef, \@m_sunlightSRP, \@sailingSRP);
		
		# For OD to be able to estimate Cr, we need HPOP to compute SRP itself (i.e., the sunlight portion)
		# but of course we want HPOP to compute the value that we just computed
		#
		# THUS, for the sunlight portion, we'll modify the SRPArea to make this happen
		
		my $magnitude = dotProduct(\@m_sunlightSRP, \@m_sunlightSRP);
		$magnitude = sqrt($magnitude);
				
		$Result->{SRPArea} = $magnitude / ($cr * $solarFlux * $illum / ($m_spacecraftMass * $m_speedOfLight));
		
		# add sailing SRP contribution
				
		$Result->AddAcceleration(eUtFrameInertial, $sailingSRP[0], $sailingSRP[1], $sailingSRP[2]);
	}

	return $m_Enabled;
}

# =================
#  PostEvaluate Method
# =================
sub PostEvaluate
{
	my $Result = $_[0];
	
	if( $m_Enabled == true && $m_DebugMode == true && defined($Result) )
	{
		# Check result
		
		# get SRP acceleration
				
		my $srpAccelArrayRef = $Result->GetAcceleration_Array(eSRPAccel, eUtFrameInertial);
				
		DebugMsg("HPOP computed Sunlight SRP = ($srpAccelArrayRef->[0], $srpAccelArrayRef->[1], $srpAccelArrayRef->[2])");
		
		$srpAccelArrayRef->[0] *= -1.0;
		$srpAccelArrayRef->[1] *= -1.0;
		$srpAccelArrayRef->[2] *= -1.0;
		
		addToVector($srpAccelArrayRef, \@m_sunlightSRP);
		
		DebugMsg("Difference in Sunlight SRP = ($srpAccelArrayRef->[0], $srpAccelArrayRef->[1], $srpAccelArrayRef->[2])");
				
		my $cr = $Result->{Cr};
		my $dragArea = $Result->{DragArea};
		my $solarFlux = $Result->{SolarFlux};
		my $illum = $Result->{SolarIntensity};
		
		DebugMsg("Area = $dragArea, Flux = $solarFlux, Cr = $cr,  Mass = $m_spacecraftMass, Illum = $illum");
	}
	
	$m_EvalMsgCount++;

	if($m_EvalMsgCount % $m_EvalMsgMax == 0)
	{
		# don't output on every call here  - this gets called alot and would output too many messages!
		$m_EvalMsgsOn = 0;
	}	
	
	return $m_Enabled;
}

# ========================================================
#  PostPropagate Method
# ========================================================
sub PostPropagate
{
	my $AgAsHpopPluginResult = $_[0];
	
	if( $m_DebugMode == true )
	{
		if( $m_Enabled == true )
		{
			Message( eLogMsgDebug, "$m_Name.PostPropagate(): Enabled" );
		}
		else
		{
			Message( eLogMsgDebug, "$m_Name.PostPropagate(): Disabled" );
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
		Message( eLogMsgDebug, "$m_Name.Free(): MsgCntr( $m_MsgCntr )" );
	}

	$m_AgUtPluginSite 		= undef;
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

# ======================================================
#  DiffuseReflectivity roperty
# ======================================================
sub GetDiffuseReflectivity
{
	return $m_diffuseReflectivity;
}

sub SetDiffuseReflectivity
{
	$m_diffuseReflectivity = $_[0];
}

# ======================================================
#  DiffuseReflectivity roperty
# ======================================================
sub GetSpecularReflectivity
{
	return $m_specularReflectivity;
}

sub SetSpecularReflectivity
{
	$m_specularReflectivity = $_[0];
}

# =====================================================
#   Copyright 2005, Analytical Graphics, Inc.          
# =====================================================

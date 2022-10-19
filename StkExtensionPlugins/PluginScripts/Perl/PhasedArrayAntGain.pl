##########################################################################################
# SAMPLE STK ANTENNA GAIN PLUGIN TO MODEL PHASED ARRAY GAIN (WRITTEN IN PERL)
# TO MODIFY/REPLACE THE SIMPLE COSINE LOSS MODEL, EDIT CODE IN THE -USER GAIN MODEL AREA-
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_PhasedArrayAntGain

# declare some global variables

my $Perl_PhasedArrayAntGain_compute_init = -1;
my $Perl_PhasedArrayAntGain_Inputs, $Perl_PhasedArrayAntGain_Outputs;
my %Perl_PhasedArrayAntGain_Outputs_ArgHash;

sub Perl_PhasedArrayAntGain
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_PhasedArrayAntGain_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_PhasedArrayAntGain_compute_init = -1;

		@retVal = Perl_PhasedArrayAntGain_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_PhasedArrayAntGain_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_PhasedArrayAntGain_register
{
	my @argStr;
	
#******************************************************************
#******************************************************************
#************************Output Parameters*************************
#******************************************************************
#******************************************************************

	push @argStr, "ArgumentType = Output; Name = AntennaGain;    ArgumentName = AntennaGain";
	push @argStr, "ArgumentType = Output; Name = Beamwidth;      ArgumentName = Beamwidth";
	push @argStr, "ArgumentType = Output; Name = AntennaMaxGain; ArgumentName = AntennaMaxGain";
	push @argStr, "ArgumentType = Output; Name = IntegratedGain; ArgumentName = IntegratedGain";
	push @argStr, "ArgumentType = Output; Name = AntennaCoordSystem; ArgumentName = AntennaCoordSystem";
	push @argStr, "ArgumentType = Output; Name = DynamicGain; ArgumentName = DynamicGain";
	
#******************************************************************
#******************************************************************
#*************************Input Parameters*************************
#******************************************************************
#******************************************************************
	

	push @argStr, "ArgumentType = Input;  Name = DateUTC;             ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = CbName;              ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = Frequency;           ArgumentName = Frequency";
	push @argStr, "ArgumentType = Input;  Name = AzimuthAngle;        ArgumentName = AzimuthAngle";
	push @argStr, "ArgumentType = Input;  Name = ElevationAngle;      ArgumentName = ElevationAngle";
	push @argStr, "ArgumentType = Input;  Name = AntennaPosLLA;       ArgumentName = AntennaPosLLA";
	push @argStr, "ArgumentType = Input;  Name = AntennaCoordSystem;  ArgumentName = AntennaCoordSystem";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;            ArgumentName = EpochSec";


	return @argStr; 
}

sub Perl_PhasedArrayAntGain_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is Date and is a String. Strings are simply scalars in Perl so the assignment is easy:	
	my $date = $origArray[1];

	# next argument is CbName and is a String. 	
	my $CbName = $origArray[2];

	# next argument is Frequency (Hz) and is a double. doubles are simply scalars in Perl so the assignment is easy:	
	my $freq = $origArray[3];

	# next argument is AzimuthAngle (Rad) and is a double.
	# This is in Antenna Coordinates (Y-Axis) and is measured off the boresight.	
	my $az = $origArray[4];

	# next argument is ElevationAgle (rad) and is a double.
	# This is in Antenna Coordinates (X-axis) and is measured off the boresight.
	my $el = $origArray[5];

	# next argument is named AntennaPosLLA and is antenna position in (Lat, Long, Alt) which is of type Double:3 
	# meaning it is an array of 3 doubles, latitude (Rad), logitude (Rad), altitude (meters) are components of position. 
	# Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $origArray[6], one must de-reference it as an array as shown below

	my @antPosLLAArray = @{$origArray[6]};

	# next argument is AntennaCoordSystem and is an integer.
	# This is a flag for Antenna Coordinate System 
	# (0 for Azimuth-Elevation Polar, 1 for Azimuth-Elevation Rectangular & 2 for Azimuth-Elevation Other).
	my $coordSys = $origArray[7];
	
	# next argument is EpochSec (seconds) and is a double. doubles are simply scalars in Perl so the assignment is easy:	
	my $epsec = $origArray[8];
	

	if($Perl_PhasedArrayAntGain_compute_init < 0)
	{
		$Perl_PhasedArrayAntGain_compute_init = 1; 

		#STKUtil::printOut " CustomGainPlugin:init: 0: $origArray[0], 1: $origArray[1], 2: $origArray[2], 3: $origArray[3], 4: $origArray[4], 5: $origArray[5], 6: $@antPosLLAArray[0], 7: $@antPosLLAArray[1], 8: $@antPosLLAArray[2], 9: $origArray[7]";

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_PhasedArrayAntGain_Inputs = $g_PluginArrayInterfaceHash{'Perl_PhasedArrayAntGain'}{'Inputs'};
		$Perl_PhasedArrayAntGain_Outputs = $g_PluginArrayInterfaceHash{'Perl_PhasedArrayAntGain'}{'Outputs'};

		%Perl_PhasedArrayAntGain_Outputs_ArgHash = $Perl_PhasedArrayAntGain_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_PhasedArrayAntGain_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered

	# AntennaGain (dB), gain of the antenna at time and in the Azi-Elev direction off the boresight.
	# Beamwidth (Rad) is the 3-dB beamwith of the antenna.
	# AntennaMaxGain (dB) is the maximum ( possibly boresight gain of the antenna)
	# IntegratedGain of the antenna (range 0-1) used for antenna Noise computation.
	# AntennaCoordSystem is the Coordinate System used for the antenna: 
	#  (0 for Azimuth-Elevation Polar, 1 for Azimuth-Elevation Rectangular & 2 for Azimuth-Elevation Other)
	# DynamicGain is a flag that enables 3D visualization of the dynamic antenna pattern.
	
	#############################################################################################
	# USER ANTENNA GAIN MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR ANTENNA GAIN COMPUTATION MODEL
	#############################################################################################

	# TO MODIFY/REPLACE THE SIMPLE COSINE LOSS MODEL, EDIT CODE IN THIS SECTION OF THE SCRIPT!!!
	
		# DESCRIPTION OF SIMPLE COSINE LOSS MODEL
		# ----------------------------------------
		#
		# Overview:
		# Simple approximation of mainbeam line-of-sight gain assuming ideal cosine scan loss behavior
		# over a limited scan range (e.g. 60 degrees). Estimates first-order gain reduction from 
		# distortion and spreading introduced as mainbeam is scanned away from the array's normal 
		# vector. The model is intended as a simple symmetrical planar phased array model for link
		# budgets and radar analysis.
		#
		# Applicability:
		# As shipped, this plugin script is intended for use in communication link and radar anlaysis
		# invloving two rf objects with a constant system noise temperature.  This plugin as shipped 
		# is NOT appropriate for use with a calculated system temperature or in an STK CommSystem 
		# analysis with constellations of tranmitters, receivers and interferers. This is because 
		# integrated gain, non-symmetrical arrays, mainbeam shape, sidelobe structure, adaptive nulling, and
		# other gain pattern details are not modeled. Nor will this plug-in provide dynamic gain graphics
		# in the STK 2-D or 3-D windows. If such capabilities are of interest to you, please ask AGI
		# Support about additional phased array modeling options for STK.  You may contact us
		# by phone at 1.800.924.7244 or via email at support@agi.com.
		#
		# Assumptions:
		# 1) Planar array of ideal elements --> gain reduction proportional to projected aperture area
		# 2) Half-wavelength or less element spacing --> minimal gain reduction from grating lobes
		# 3) Symmetrical element layout --> equivalent scan loss behavior over all line-of-site azimuths
		# 4) Scan limited --> beyond a certain scan angle (e.g. 60 deg), no useful gain provided
		#
		# Mathematics:
		# A maximum gain value representing the broadside on-axis mainbeam (i.e. line-of-sight along 
		# array's normal vector) is reduced by multipliying by the cosine of the scan angle, where 
		# scan angle is defined as the angle between the array normal vector and the line-of-sight 
		# to the intended communication node or radar target. 
		#
		# The user is presented two options to model the maximum gain:
		# 1) a user-specified hardcoded value
		# 2) a calculation based on user-specified circular aperture area, aperture efficiency, and
		# operating frequency. The user selects the model they wish to use by setting the gainModel
		# script parameter.
		# As shipped, the script is set to use a hardcoded maximum gain of 41 dB.
		#  A placeholder is also provided for the user to introduce their own maximum gain calculation.
		#
		# END DESCRIPTION
		#
	
		# Overview of inputs and variable types provided by the STK antenna gain plug-in interface
		#   Test Model Phased Array Antenna
		#	Script inputs
		#		Date, Time				string
		#		Central Body Name			string
		#		Frequency(Hz)				double
		#		Azimuth Angle(Rad)			double
		#		Elevation Angle(Rad)			double
		#		AntennaPosLLA(Deg,Deg,m)		double(3)
		#		AntennaCoordSystem			integer 
		#							(0: az-el-polar,
		#							 1: az-el-rectangular,
		#							 2: az-el-other)
		#		EpochSec (seconds)			double
		#
		
		# Declaring and Initializing inputs, constants and other variables
	
	# compute the Test Model : Phased Array Antenna Pattern
	my $antPos, $antPosLat, $antPosLon, $antPosAlt, $antCoordSys;
	my $AntennaGain, $scanloss, $maxscan;
	my $gainModel, $gmax, $designFreq, $area, $eff, $lambda;
	my $sineOfElevation, $alphaPrimary, $betaPrimary, $alphaSecondary, $betaSecondary, $alphabeta, $arcsinab;
	
 	my $pi = 4*atan2(1,1);
 
 	# Get array input values (those not used by simple cosine loss model are commented out)
 	
	# $antPosLat = @antPosLLAArray[0];
	# $antPosLon = @antPosLLAArray[1];
	# $antPosAlt = @antPosLLAArray[2];
	
	# Initialize return gain value and internally calculated scan angle value
	my $AntennaGain = -999999.9;
	my $scanLoss    = 0.0;
	    	
	# Maximum Allowable Scan Range - User defined
	# -------------------------------------------
	# Scan angles larger than this hardcoded limit will result in zero gain returned to STK
	# For the simple cosine loss model, max scan set to 60 degrees (Pi/3)
	$maxScan = $pi/3;
	
	# Maximum Gain Model
	# ------------------
	# Represents broadside on-axis mainbeam boresight gain (i.e. boresight along array's
	# normal vector). As shipped, the script is set to use a hardcoded maximum gain of 
	# 41 dB (equivalent to 1 meter parabolic at 14.5 GHz and 55% efficiency). The user may
	# change this harcoded value or use the calculation-based model provided by setting
	# the gainModel script parameter and providing the appropriate inputs. A placeholder is
	# also provided for the user to introduce their own max gain calculation.

	$gainModel = 0;
	
	if ($gainModel == 0)
	# Hardcoded maximum gain value--user can set value as desired
	{
		$gmax = 41.0;
	}
	elsif ($gainModel == 1)
	# Max gain calculated based on circular aperture area, aperture efficiency, and operating frequency.
	# This model is useful when the array will be used over a variety of operating frequencies.
	# Assumes half-wavelength or less element spacing!!!
	# Reference: Handbook of Electrical Engineering Calculations, Phadke, 1999
	{
	# USER-SPECIFIED DESIGN FREQUENCY IN HZ -- used to enforce half-wavelength or less 
	# element spacing assumption (operating frequencies above design frequency will result
	# in a return value of -999999.9 dB gain)
		$designFreq = 14500000000;	
	
	# USER-SPECIFIED APERTURE AREA IN SQUARE-METERS
		$area = $pi;
	
	# USER-SPECIFIED APERTURE EFFICIENCY (UNITLESS)
		$eff = 0.6;	
	
	# Calculate max gain in dB
		if($freq <= $designFreq)
		{
			$lambda = 299792458.0 / $freq;
			$gmax = $eff * 4 * $pi * $area/($lambda * $lambda);
			$gmax = 10.0*log($gmax)/log(10.0);
		}
		else
		{
			$gmax = -999999.9;
		}
	}
	
	else 
	{
	# Placeholder for user-supplied maximum on-axis gain calculation.
		$gmax = 0.0;
	}
	
	# Scan Angle Computation
	# ----------------------
		
	# Computing Scan Loss and Antenna Gain under the condition of less than or equal to Maximum Allowable Scan
	# Otherwise Antenna Gain = -999999.9 dB

	if ($el <= $maxScan)
	{
	# When not using a hardcoded max gain, only compute antenna gain if operating frequency
	# is less than design frequency that way the same error/null value (-999999.9 dB) is
	# always returned from the plugin.
		
		
		if ($gainModel == 0)
		{
			$scanLoss = cos($el);
			$scanLoss = 10.0*log($scanLoss)/log(10.0);
			$AntennaGain = $gmax + $scanLoss;
		}
		elsif ($gainModel == 1)
		
		{
			if($freq <= $designFreq)
			{
			$scanLoss = cos($el);
			$scanLoss = 10.0*log($scanLoss)/log(10.0);
			$AntennaGain = $gmax + $scanLoss;
			}
		}
		else
		{
		# Placeholder for use with user-supplied maximum on-axis gain calculation.
		}
	}

	# Return of Outputs to STK
	# ------------------------

	# NOTE: Simple cosine loss model only returns AntennaGain and AntennaMaxGain
	# NOTE: Simple cosine loss model does not compute integrated gain and thus should only be used
	#       with a constant system noise temperature when used in Rx and Radar objects.

	#  NOTE: All outputs MUST be returned and in the same order that they were registered.
	#       STK will interpret return values with the units shown below.

	# AntennaGain (dB), gain of the antenna at time and in the Azi-Elev direction off the boresight.
	# Beamwidth (Rad) is the 3-dB beamwidth of the antenna.
	# AntennaMaxGain (dB) is the maximum ( possibly boresight gain of the antenna)
	# IntegratedGain of the antenna (range 0-1) used for antenna Noise computation.
	# Dynamic Gain is a flag (value = 0 or 1) depending on whether the graphics get updated at each timestep.

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $AntennaGain;
	$returnArray[1] =  0;
	$returnArray[2] =  $gmax;
	$returnArray[3] =   0;
	$returnArray[4] =   0;
	$returnArray[5] =   0;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################

	return @returnArray;
}

sub Perl_PhasedArrayAntGain_showArgs
{
	my @argStrArray;

	#STKUtil::printOut "Doing Perl_PhasedArrayAntGain_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_PhasedArrayAntGain_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_PhasedArrayAntGain_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_PhasedArrayAntGain_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_PhasedArrayAntGain_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_PhasedArrayAntGain_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_PhasedArrayAntGain_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;

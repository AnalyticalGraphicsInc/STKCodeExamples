##########################################################################################
# SAPMLE FOR PERL BASED CUSTOM ANTENNA GAIN PLUGIN SCRIPT PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER ANTENNA GAIN MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_GaussianAntennaGain

# declare some global variables

my $Perl_GaussianAntennaGain_compute_init = -1;
my $Perl_GaussianAntennaGain_Inputs, $Perl_GaussianAntennaGain_Outputs;
my %Perl_GaussianAntennaGain_Outputs_ArgHash;

sub Perl_GaussianAntennaGain
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_GaussianAntennaGain_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_GaussianAntennaGain_compute_init = -1;

		@retVal = Perl_GaussianAntennaGain_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_GaussianAntennaGain_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_GaussianAntennaGain_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = AntennaGain;    ArgumentName = AntennaGain";
	push @argStr, "ArgumentType = Output; Name = Beamwidth;      ArgumentName = Beamwidth";
	push @argStr, "ArgumentType = Output; Name = AntennaMaxGain; ArgumentName = AntennaMaxGain";
	push @argStr, "ArgumentType = Output; Name = IntegratedGain; ArgumentName = IntegratedGain";
	push @argStr, "ArgumentType = Output; Name = AntennaCoordSystem;  ArgumentName = AntennaCoordSystem";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;             ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = CbName;              ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = Frequency;           ArgumentName = Frequency";
	push @argStr, "ArgumentType = Input;  Name = AzimuthAngle;        ArgumentName = AzimuthAngle";
	push @argStr, "ArgumentType = Input;  Name = ElevationAngle;      ArgumentName = ElevationAngle";
	push @argStr, "ArgumentType = Input;  Name = AntennaPosLLA;       ArgumentName = AntennaPosLLA";
	push @argStr, "ArgumentType = Input;  Name = AntennaCoordSystem;  ArgumentName = AntennaCoordSystem";


	return @argStr; 
}

sub Perl_GaussianAntennaGain_compute
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
	# This is a flag for Antenna Coordinate System (0 for Azimuth-Elevation Polar & 1 for Azimuth-Elevation Rectangular).
	my $coordSys = $origArray[7];
	

	if($Perl_GaussianAntennaGain_compute_init < 0)
	{
		$Perl_GaussianAntennaGain_compute_init = 1; 

		STKUtil::printOut " CustomGainPlugin:init: 0: $origArray[0], 1: $origArray[1], 2: $origArray[2], 3: $origArray[3], 4: $origArray[4], 5: $origArray[5], 6: $@antPosLLAArray[0], 7: $@antPosLLAArray[1], 8: $@antPosLLAArray[2], 9: $origArray[7]";

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_GaussianAntennaGain_Inputs = $g_PluginArrayInterfaceHash{'Perl_GaussianAntennaGain'}{'Inputs'};
		$Perl_GaussianAntennaGain_Outputs = $g_PluginArrayInterfaceHash{'Perl_GaussianAntennaGain'}{'Outputs'};

		%Perl_GaussianAntennaGain_Outputs_ArgHash = $Perl_GaussianAntennaGain_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_GaussianAntennaGain_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered

	# AntennaGain (dB), gain of the antenna at time and in the Azi-Elev direction off the boresight.
	# Beamwidth (Rad) is the 3-dB beamwith of the antenna.
	# AntennaMaxGain (dB) is the maximum ( possibly boresight gain of the antenna)
	# IntegratedGain of the antenna (range 0-1) used for antenna Noise computation.
	
	#############################################################################################
	# USER ANTENNA GAIN MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR ANTENNA GAIN COMPUTATION MODEL
	#############################################################################################

	# compute the Test Model : Gaussian Antenna Gain Pattern
	my $x, $thetab, $gmax, $gain, $expParm;
	my $lambda, $eff, $dia;
	my $antPos, $antPosLat, $antPosLon, $antPosalt;

	# set Anttena size and efficiency
	my $eff = 0.55;
	my $dia = 1.0;

	$antPosLat = @antPosLLAArray[0];
	$antPosLon = @antPosLLAArray[1];
	$antPosAlt = @antPosLLAArray[2];
	
	$lambda = 299792458.0 / $freq;
	$thetab = $lambda / ($dia * sqrt($eff));
	$x = 3.141592 * $dia / $lambda;
	$x = $x ** 2;
	
	$gmax = $eff * $x;
	$expParm = -2.76 * $el * $el / ($thetab * $thetab);
	if($expParm < -700)
	{
		$expParm = -700;
	}
	$gain = $gmax * exp($expParm);
	$gain = 10.0*log($gain)/log(10.0);

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] = $gain;
	$returnArray[1] = $thetab;
	$returnArray[2] = 10.0*log($gmax)/log(10.0);
	$returnArray[3] = 0.5;
	
	#AntennaCoordSystem return 0 for Polar and 1 for Rectangular
	$returnArray[4] = 0;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################

	return @returnArray;
}

sub Perl_GaussianAntennaGain_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_GaussianAntennaGain_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_GaussianAntennaGain_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_GaussianAntennaGain_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_GaussianAntennaGain_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_GaussianAntennaGain_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_GaussianAntennaGain_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_GaussianAntennaGain_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
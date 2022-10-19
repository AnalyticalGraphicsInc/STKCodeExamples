##########################################################################################
# SAMPLE FOR PERL BASED RADAR SAR NIIRS CONSTRAINT PLUGIN SCRIPT
# SCRIPT PROVIDED BY THE USER.
# PLEASE ADD YOUR CONSTRAINT CODE IN THE USER RADAR SEARCH/TRACK CONSTRAINT PLUGIN AREA BELOW.
# DO NOT CHANGE ANYTHING ABOVE OR BELOW THAT AREA IN THE SCRIPT
##########################################################################################

# Perl_RadarCrossSection

use STKUtil;

# declare some global variables

my $Perl_RadarCrossSection_compute_init = -1;
my $Perl_RadarCrossSection_Inputs, $Perl_RadarCrossSection_Outputs;
my %Perl_RadarCrossSection_Outputs_ArgHash;

sub Perl_RadarCrossSection
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_RadarCrossSection_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_RadarCrossSection_compute_init = -1;

		@retVal = Perl_RadarCrossSection_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_RadarCrossSection_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_RadarCrossSection_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = RCSMatrixReal00; ArgumentName = RCSMatrixReal00";
	push @argStr, "ArgumentType = Output; Name = RCSMatrixImg00; ArgumentName = RCSMatrixImg00";
	push @argStr, "ArgumentType = Output; Name = RCSMatrixReal01; ArgumentName = RCSMatrixReal01";
	push @argStr, "ArgumentType = Output; Name = RCSMatrixImg01; ArgumentName = RCSMatrixImg01";
	push @argStr, "ArgumentType = Output; Name = RCSMatrixReal10; ArgumentName = RCSMatrixReal10";
	push @argStr, "ArgumentType = Output; Name = RCSMatrixImg10; ArgumentName = RCSMatrixImg10";
	push @argStr, "ArgumentType = Output; Name = RCSMatrixReal11; ArgumentName = RCSMatrixReal11";
	push @argStr, "ArgumentType = Output; Name = RCSMatrixImg11; ArgumentName = RCSMatrixImg11";
	push @argStr, "ArgumentType = Output; Name = ScatterMatrixBasis; ArgumentName = ScatterMatrixBasis";
	push @argStr, "ArgumentType = Output; Name = IsDynamic; ArgumentName = IsDynamic";

	push @argStr, "ArgumentType = Input; Name = EpochSec; ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input; Name = Frequency; ArgumentName = Frequency";
	push @argStr, "ArgumentType = Input; Name = IncidentRho; ArgumentName = IncidentRho";
	push @argStr, "ArgumentType = Input; Name = IncidentTheta; ArgumentName = IncidentTheta";
	push @argStr, "ArgumentType = Input; Name = ReflectedRho; ArgumentName = ReflectedRho";
	push @argStr, "ArgumentType = Input; Name = ReflectedTheta; ArgumentName = ReflectedTheta";
	push @argStr, "ArgumentType = Input; Name = IncidentBodyFixedVector; ArgumentName = IncidentBodyFixedVector";
	push @argStr, "ArgumentType = Input; Name = ReflectedBodyFixedVector; ArgumentName = ReflectedBodyFixedVector";

	return @argStr; 
}

sub Perl_RadarCrossSection_compute
{
	# the inputs here are in the order of the requested Inputs, as registered

	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is the simulation time and is a Double. 
	
	my $EpochSec = $origArray[1];

	# next argument is the incident rho angle and is a Double. 
	
	my $IncidentRho = $origArray[2];
	
	# next argument is the incident theta angle and is a Double. 
	
	my $IncidentTheta = $origArray[3];
	
	# next argument is the reflected rho angle and is a Double. 
	
	my $ReflectedRho = $origArray[4];
	
	# next argument is the reflected theta angle and is a Double. 
	
	my $ReflectedTheta = $origArray[5];

	# next argument is the incident body fixed vector and is type (CartVec3) Array and is double-3. 

	my @IncidentBodyFixedVector = @{$origArray[6]};

	# next argument is the incident body fixed vector and is type (CartVec3) Array and is double-3. 

	my @ReflectedBodyFixedVector = @{$origArray[7]};
	
			
	if($Perl_RadarCrossSection_compute_init < 0)
	{
		$Perl_RadarCrossSection_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_RadarCrossSection_Inputs = $g_PluginArrayInterfaceHash{'Perl_RadarCrossSection'}{'Inputs'};
		$Perl_RadarCrossSection_Outputs = $g_PluginArrayInterfaceHash{'Perl_RadarCrossSection'}{'Outputs'};

		%Perl_RadarCrossSection_Outputs_ArgHash = $Perl_RadarCrossSection_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_RadarCrossSection_showArgs();
	}


	#############################################################################################
	# NOTE: the outputs that are returned MUST be in the same order as registered	

	# this defines the return array
	my @returnArray = ();
	
	$returnArray[0] = 0.0;
	$returnArray[1] = 0.0;
	$returnArray[2] = 0.0;
	$returnArray[3] = -1.0;
	$returnArray[4] = 1.0;
	$returnArray[5] = 0.0;
	$returnArray[6] = 0.0;
	$returnArray[7] = 0.0;
	$returnArray[8] = 0;
	$returnArray[9] = 0;
	
	return @returnArray;
}

sub Perl_RadarCrossSection_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_RadarCrossSection_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_RadarCrossSection_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_RadarCrossSection_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_RadarCrossSection_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_RadarCrossSection_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_RadarCrossSection_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_RadarCrossSection_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
##########################################################################################
# SAPMLE FOR PERL BASED CUSTOM MULTIBEAM ANTENNA BEAM SELECTION STRATEGY PLUGIN 
# SCRIPT PROVIDED BY THE USER.
# PLEASE ADD YOUR STRATEGY IN THE USER BEAM SELECTION PLUGIN AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_AntMultiBeamSelStrat

use STKUtil;

# declare some global variables

my $Perl_AntMultiBeamSelStrat_compute_init = -1;
my $Perl_AntMultiBeamSelStrat_Inputs, $Perl_AntMultiBeamSelStrat_Outputs;
my %Perl_AntMultiBeamSelStrat_Outputs_ArgHash;

sub Perl_AntMultiBeamSelStrat
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_AntMultiBeamSelStrat_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_AntMultiBeamSelStrat_compute_init = -1;

		@retVal = Perl_AntMultiBeamSelStrat_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_AntMultiBeamSelStrat_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_AntMultiBeamSelStrat_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = BeamNumber;          ArgumentName = BeamNumber";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;             ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;            ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = CbName;              ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = AntennaPosLLA;       ArgumentName = AntennaPosLLA";
	push @argStr, "ArgumentType = Input;  Name = NumberOfBeams;       ArgumentName = NumberOfBeams";
	push @argStr, "ArgumentType = Input;  Name = BeamIDsArray;        ArgumentName = BeamIDsArray";
	push @argStr, "ArgumentType = Input;  Name = Frequency;           ArgumentName = Frequency";
	push @argStr, "ArgumentType = Input;  Name = Power;               ArgumentName = Power";
	push @argStr, "ArgumentType = Input;  Name = IsActive;            ArgumentName = IsActive";


	return @argStr; 
}

sub Perl_AntMultiBeamSelStrat_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is Date/Time and is a String. Strings are simply scalars in Perl so the assignment is easy:	
	my $date = $origArray[1];

	# next argument is epoch seconds as an integer
	my $epSec = $origArray[2];

	# next argument is CbName and is a String. 	
	my $CbName = $origArray[3];

	# next argument is named AntennaPosLLA and is antenna position in (Lat, Long, Alt) which is of type Double:3 
	# meaning it is an array of 3 doubles, latitude (Rad), logitude (Rad), altitude (meters) are components of position. 
	# Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $origArray[4], one must de-reference it as an array as shown below
	my @antPosLLAArray = @{$origArray[4]};

	# next argument is NumberOfBeams and is a integer.  This is the total number od beams in the antenna
	# This is the total number of beams (in STK beam numbers start from 1 to n).
      # BeamNumber returned by this script must be in the range 1 - n.	
	my $numBeams = $origArray[5];

	# next argument is BeamIDsArray (Char) and is an array of characters of size(NumberOfBeams * BeamIDLength).
      # Currently the BeamIDLength is = 64 characters.  The array is orgnised as one row per BeamID * NumberofBeams (rows).
      # The 1-D aray needs to be parsed into 2-D arrray.
	my $beamIDs = @{$origArray[6]};

	# next argument is the Frequency (Hz) Array and is a double. doubles are simply scalars in Perl so the assignment is easy:
      # The PERL array range is [0] to [NumberOfBeams-1]
	my @freqArray = @{$origArray[7]};

	# next argument is Power (dBw) Array and is a double. doubles are simply scalars in Perl so the assignment is easy:
      # The PERL array range is [0] to [NumberOfBeams-1]
	my @powerArray = @{$origArray[8]}; 
	
	# next argument is the IsActive Array and is a integer.  A value of 1 indicates the beam is active, while 0 is inactive.
	my @activeFlagArray = @{$origArray[9]};

	if($Perl_AntMultiBeamSelStrat_compute_init < 0)
	{
		$Perl_AntMultiBeamSelStrat_compute_init = 1; 

		STKUtil::printOut " MultiBeamSelectionStrategyPlugin:init: 0: $origArray[0], 1: $origArray[1], 2: $origArray[2],  3: $antPosLLAArray[0], 4: $antPosLLAArray[1], 5: $antPosLLAArray[2], 6: $origArray[4], 7: $origArray[5], 8: $freqArray[0], 9: $powerArray[0]";

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_AntMultiBeamSelStrat_Inputs = $g_PluginArrayInterfaceHash{'Perl_AntMultiBeamSelStrat'}{'Inputs'};
		$Perl_AntMultiBeamSelStrat_Outputs = $g_PluginArrayInterfaceHash{'Perl_AntMultiBeamSelStrat'}{'Outputs'};

		%Perl_AntMultiBeamSelStrat_Outputs_ArgHash = $Perl_AntMultiBeamSelStrat_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_AntMultiBeamSelStrat_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered

	# BeamNumber , Beam Number (in the range 1- n) of the multibeam antenna to be used for Link Analysis at this time instant.
	
	#############################################################################################
	# USER ANTENNA BEAM SELECTION STRATEGY PLUGIN AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR BEAM SELECTION STRATEGY
	# Please do NOT change anything above this area
	#############################################################################################

	# compute the Test STRATEGY : Beam selection strategy example

	my $lambda;

	# numBeams id the total number of beams and also the length of the freq & power arrays
	
	$antPosLat = $antPosLLAArray[0];
	$antPosLon = $antPosLLAArray[1];
	$antPosAlt = $antPosLLAArray[2];
	
	$lambda = 299792458.0 / $freqArray[0];

 	my $selectedBeam = 1;
 	for($i = 0; $i < $numBeams; $i++)
 	{
    	   if($activeFlagArray[$i] == 1)
    	   {
    	      $selectedBeam = $i + 1;
    	      last;
    	   }
 	}
 	
	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $selectedBeam;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	# Please do NOT change anything below this area
	#############################################################################################

	return @returnArray;
}

sub Perl_AntMultiBeamSelStrat_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_AntMultiBeamSelStrat_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_AntMultiBeamSelStrat_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_AntMultiBeamSelStrat_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_AntMultiBeamSelStrat_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_AntMultiBeamSelStrat_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_AntMultiBeamSelStrat_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_AntMultiBeamSelStrat_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
##########################################################################################
# SAMPLE STK ANTENNA GAIN PLUGIN TO MODEL BEAM FORMER FOR PHASED ARRAY ANTENNA (WRITTEN IN PERL)
# TO MODIFY/REPLACE THE BEAM DIRECTION PROVIDER MODEL, EDIT CODE IN THE -USER GAIN MODEL AREA-
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_Beamformer

# declare some global variables

# my $Perl_Beamformer_compute_init = -1;
my $Perl_Beamformer_Inputs, $Perl_Beamformer_Outputs;
my %Perl_Beamformer_Outputs_ArgHash;

sub Perl_Beamformer
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_Beamformer_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_Beamformer_compute_init = -1;

		@retVal = Perl_Beamformer_register();

	}
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_Beamformer_compute(@inputData);
	}
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_Beamformer_register
{
	my @argStr;
	
	#******************************************************************
	#******************************************************************
	#************************Output Parameters*************************
	#******************************************************************
	#******************************************************************

	push @argStr, "ArgumentType = Output; Name = IsDynamic; ArgumentName = IsDynamic";
	push @argStr, "ArgumentType = Output; Name = Weights; ArgumentName = Weights";

	#******************************************************************
	#******************************************************************
	#*************************Input Parameters*************************
	#******************************************************************
	#******************************************************************

	push @argStr, "ArgumentType = Input;  Name = EpochSec; ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = NumberOfElements; ArgumentName = NumberOfElements";
	push @argStr, "ArgumentType = Input;  Name = DesignFrequency; ArgumentName = DesignFrequency";
	push @argStr, "ArgumentType = Input;  Name = OperatingFrequency; ArgumentName = OperatingFrequency";
	push @argStr, "ArgumentType = Input;  Name = NumberOfBeamDirections; ArgumentName = NumberOfBeamDirections";
	push @argStr, "ArgumentType = Input;  Name = BeamDirections; ArgumentName = BeamDirections";
	push @argStr, "ArgumentType = Input;  Name = NumberOfNullDirections; ArgumentName = NumberOfNullDirections";
	push @argStr, "ArgumentType = Input;  Name = NullDirections; ArgumentName = NullDirections";

	return @argStr; 
}

sub Perl_Beamformer_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is EpochSec (seconds) and is a Double. Doubles are simply scalars in Perl so the assignment is easy:	
	my $EpochSec = $origArray[1];
	
	# next argument is Number Of Elements and is an integer.
	my $NumberOfElements = $origArray[2];
	
	# next argument is Design Frequency and is an integer.
	my $DesignFrequency = $origArray[3];

	# next argument is Operating Frequency and is an integer.
	my $OperatingFrequency = $origArray[4];	
	
	# next argument is Number Of Beam Directions and is an integer.
	my $NumberOfBeamDirections = $origArray[5];
	
	# next argument is named BeamDirections and is of type Double.
	# Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $origArray[4], one must de-reference it as an array as shown below
	
	my @BeamDirections = @{$origArray[6]};
	
	# next argument is Number Of Null Directions and is an integer.
	my $NumberOfNullDirections = $origArray[7];
	
	# next argument is named NullDirections and is of type Double.
	
	my @NullDirections = @{$origArray[8]};

	if($Perl_Beamformer_compute_init < 0)
	{
		$Perl_Beamformer_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_Beamformer_Inputs = $g_PluginArrayInterfaceHash{'Perl_Beamformer'}{'Inputs'};
		$Perl_Beamformer_Outputs = $g_PluginArrayInterfaceHash{'Perl_Beamformer'}{'Outputs'};

		%Perl_Beamformer_Outputs_ArgHash = $Perl_Beamformer_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		#Perl_Beamformer_showArgs();
	}
	
    # Declare outputs
	my @Weights;
	my $IsDynamic = 1;

	# NOTE: the outputs that are returned MUST be in the same order as registered
	
	############################################################################################
	# USER PLUGIN BEAMFORMER MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR BEAMFORMER COMPUTATION MODEL
	#
	# This sample demonstrates how to dynamically return weights.  Implements a static "deck" of
    # three weight sets to be applied at specific time steps. This is a very simplistic example
	# to demostrate how to dynamically return weights.
	# 
	#
	# All input and out paramters have been mapped to variables described below.
	############################################################################################
	# NOTE: the outputs that are returned MUST be in the same order as registered
	# If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	# will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	# at every timestep.
	#
	# All weights are to be complex numbers(see STK help).
	#
	# Script input variables available to user:
	#		EpochSec   - Current simulation epoch seconds.                         double  
	#		NumberOfElements - Number of enabled antenna elements in the array.    int
	#		DesignFrequency - Design frequency of the antenna array (Hz).          double
	#		OperatingFrequency - Current operating frequency of the antenna (Hz).  double
    #       NumberOfBeamDirections -  The number of items in the BeamDirections 
	#                          input field described below. 	                   int
	#		BeamDirections     - Array of Az/El values (rad/rad) representing 
    #                          the direction "entities", where "entities" are  
    #                          defined by the specific selected Direction Provider. 
    #                          Currently an array length of one is supported.      double(1,2)
    #       NumberOfNullDirections -  The number of items in the NullDirections 
	#                          input field described below. 	                   int
	#		NullDirections     - Array of Az/El values (rad/rad) representing the 
    #                          direction "entities", where "entities" are defined  
    #                          by the specific selected beam-steering Direction Prvdr. 
	#                          Currenly an array length of one is supported.      double(1,2)
	#
	# Script outputs which must be filled in by the user:
	#       IsDynamic      -   Indicates if script is time-dynamic (see above).    int
	#       Weights        -   Complex values for each element.  Format is 
	#                          linear array of real/imaginary interleaved values.
		
	# Dim temporaries used for this particular example

	# Initialize Output values
	$IsDynamic = 1;

	if ($EpochSec < 600) 
	{
	   @Weights = (-0.0727559,0.233901, 0.0164913,0.215751 ,-0.0603389,0.244863, 0.153553,0.128345, 0.184669,0.227678, -0.0637141,0.202648, 0.205931,0.0567225, 0.160496,0.177382, 0.147236,-0.0999877, 0.313338,-1.85741e-013, 0.147236,0.0999877, 0.160496,-0.177382, 0.205931,-0.0567225, -0.0637141,-0.202648, 0.184669,-0.227678, 0.153553,-0.128345, -0.0603389,-0.244863, 0.0164913,-0.215751, -0.0727559,-0.233901); 
	}
    elsif ($EpochSec >= 600 && $EpochSec < 1200)
	{
	   @Weights = (-0.0384998,0.242632, 0.116201,0.175782, -0.0728582,0.22732, 0.199626,-0.0118431, 0.180923,0.201406, -0.124046,0.194012, 0.217225,-0.00904073, 0.127373,0.194714, 0.0867985,-0.184947, 0.281027,1.38456e-015, 0.0867985,0.184947, 0.127373,-0.194714, 0.217225,0.00904073, -0.124046,-0.194012, 0.180923,-0.201406, 0.199626,0.0118431, -0.0728582,-0.22732, 0.116201,-0.175782, -0.0384998,-0.242632); 
	}
	else
	{
	   @Weights = (-0.0160897,0.235592, 0.195985,0.120679, -0.0770078,0.204931, 0.186028,-0.139551, 0.158855,0.171406, -0.147394,0.179345, 0.217917,-0.0586559, 0.102059,0.196255, 0.0284807,-0.231605, 0.232606,-7.91854e-014, 0.0284807,0.231605, 0.102059,-0.196255, 0.217917,0.0586559, -0.147394,-0.179345, 0.158855,-0.171406, 0.186028,0.139551, -0.0770078,-0.204931, 0.195985,-0.120679, -0.0160897,-0.235592); 
    }



	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################
	
	  
	# this defines the return array	
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW
	$returnArray[0] =  $IsDynamic;
	$returnArray[1] =  \@Weights;

	STKUtil::formatOutputArray(\@returnArray);
	return @returnArray;
}

sub Perl_Beamformer_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_Beamformer_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_Beamformer_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_Beamformer_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_Beamformer_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_Beamformer_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_Beamformer_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_Beamformer_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;

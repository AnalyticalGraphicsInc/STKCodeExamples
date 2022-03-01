##########################################################################################
# SAMPLE STK ANTENNA GAIN PLUGIN TO MODEL NULL DIRECTION PROVIDER FOR PHASED ARRAY ANTENNA (WRITTEN IN PERL)
# TO MODIFY/REPLACE THE NULL DIRECTION PROVIDER MODEL, EDIT CODE IN THE -USER GAIN MODEL AREA-
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_NullDirectionProvider

# declare some global variables

# my $Perl_NullDirectionProvider_compute_init = -1;
my $Perl_NullDirectionProvider_Inputs, $Perl_NullDirectionProvider_Outputs;
my %Perl_NullDirectionProvider_Outputs_ArgHash;

# Declare some useful constants for conversions used in this particular example...
my $NulldegToRad, $radToDeg;

$NulldegToRad = 0.01745329252;
$radToDeg = 57.29577951308;

# Declare globals used for this particular example.  Globals will retain their state across each
# time step script call...
my $gNullScanAzStepSize, $gNullScanElStepSize, $gNullScanMinAz, $gNullScanMaxAz, $gNullScanMinEl, $gNullScanMaxEl, $gNullScanAz, $gNullScanEl;

$gNullScanAzStepSize = NulldegToRad*4;
$gNullScanElStepSize = NulldegToRad*4;
$gNullScanMinAz = -30*NulldegToRad;
$gNullScanMaxAz = 30*NulldegToRad;
$gNullScanMinEl = -30*NulldegToRad;
$gNullScanMaxEl = 30*NulldegToRad;

$gNullScanAz = gNullScanMinAz;
$gNullScanEl = gNullScanMinEl;

sub Perl_NullDirectionProvider
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_NullDirectionProvider_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_NullDirectionProvider_compute_init = -1;

		@retVal = Perl_NullDirectionProvider_register();

	}
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_NullDirectionProvider_compute(@inputData);
	}
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_NullDirectionProvider_register
{
	my @argStr;
	
	#******************************************************************
	#******************************************************************
	#************************Output Parameters*************************
	#******************************************************************
	#******************************************************************

	push @argStr, "ArgumentType = Output; Name = IsDynamic; ArgumentName = IsDynamic";
	push @argStr, "ArgumentType = Output; Name = NumDirections; ArgumentName = NumDirections";
	push @argStr, "ArgumentType = Output; Name = Azimuths; ArgumentName = Azimuths";
	push @argStr, "ArgumentType = Output; Name = Elevations; ArgumentName = Elevations";
	
	#******************************************************************
	#******************************************************************
	#*************************Input Parameters*************************
	#******************************************************************
	#******************************************************************

	push @argStr, "ArgumentType = Input;  Name = ObjectPath; ArgumentName = ObjectPath";
	push @argStr, "ArgumentType = Input;  Name = EpochSec; ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = PosLLA; ArgumentName = PosLLA";
	push @argStr, "ArgumentType = Input;  Name = PosCBF; ArgumentName = PosCBF";
	push @argStr, "ArgumentType = Input;  Name = MemberPositionFormat; ArgumentName = MemberPositionFormat";
	push @argStr, "ArgumentType = Input;  Name = NumberOfMembers; ArgumentName = NumberOfMembers";
	push @argStr, "ArgumentType = Input;  Name = MemberPositions; ArgumentName = MemberPositions";
	push @argStr, "ArgumentType = Input;  Name = MemberFrequencies; ArgumentName = MemberFrequencies";
	push @argStr, "ArgumentType = Input;  Name = MemberPwrs; ArgumentName = MemberPwrs";
	push @argStr, "ArgumentType = Input;  Name = MemberIds; ArgumentName = MemberIds";
	push @argStr, "ArgumentType = Input;  Name = MemberCategories; ArgumentName = MemberCategories";

	return @argStr; 
}

sub Perl_NullDirectionProvider_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is Object Path and is a String. Strings are simply scalars in Perl so the assignment is easy:	
	my $ObjectPath = $origArray[1];
	
	# next argument is EpochSec (seconds) and is a Double. Doubles are simply scalars in Perl so the assignment is easy:	
	my $EpochSec = $origArray[2];

	# next argument is Position in LLA and is a String.	
	my $PosLLA = $origArray[3];

	# next argument is Position in XYZ and is a String.	
	my $PosCBF = $origArray[4];

	# next argument is Member Position Format and is an integer.
	my $MemberPositionFormat = $origArray[5];
	
	# next argument is Number of Members and is an integer.
	my $NumberOfMembers = $origArray[6];
	
	# next argument is named MemberPositions and is of type Double:3 meaning it is an array of 3 doubles. 
	# Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $origArray[7], one must de-reference it as an array as shown below
	
	my @MemberPositions = @{$origArray[7]};
	
	# next argument is named MemberFrequencies and is of type Double:100 meaning it is an array of 100 doubles. 
	
	my @MemberFrequencies = @{$origArray[8]};
	
	# next argument is Member Powers (EIRP) and is of type Double:100 meaning it is an array of 100 doubles. 
	
	my @MemberPwrs = @{$origArray[9]};
	
	# next argument is Member IDs and is of type Integer:100 meaning it is an array of 100 integers. 
	
	my @MemberIds = @{$origArray[10]};

	# next argument is Member Categories and is of type Integer:100 meaning it is an array of 100 integers. 
	
	my @MemberCategories = @{$origArray[11]};

	if($Perl_NullDirectionProvider_compute_init < 0)
	{
		$Perl_NullDirectionProvider_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_NullDirectionProvider_Inputs = $g_PluginArrayInterfaceHash{'Perl_NullDirectionProvider'}{'Inputs'};
		$Perl_NullDirectionProvider_Outputs = $g_PluginArrayInterfaceHash{'Perl_NullDirectionProvider'}{'Outputs'};

		%Perl_NullDirectionProvider_Outputs_ArgHash = $Perl_NullDirectionProvider_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		#Perl_NullDirectionProvider_showArgs();
	}
	
	my $IsDynamic     = 1; 
 	my $NumDirections = 0;
	my @Azs;
	my @Els;
	$Azs[99] = 0;  # Force array capacity to be 100 in size.
	$Azs[99] = 0;
	$Els[99] = 0;
	$Els[99] = 0;

	

	# NOTE: the outputs that are returned MUST be in the same order as registered
	
	############################################################################################
	# USER PLUGIN NULL DIRECTION PROVIDER MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR DIRECTION PROVIDER COMPUTATION MODEL
	#
	# This sample demonstrates how to dynamically return nulling directions.  This script looks 
	# at each member to determine it's category (i.e. Aircraft, Facility, Satellite, etc.) to 
	# determine if it should null the member.  If the member is not an aircraft, it will return
	# the member's direction in order to be nulled.
	# This is just a simplistic example to demonstrate how to dynamically return null directions.
	#
	# All input and out paramters have been mapped to variables described below.
	############################################################################################
	# NOTE: the outputs that are returned MUST be in the same order as registered
	# If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	# will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	# at every timestep.
	#
	# All directions specified as Azimuth and Elevation angles (see STK help) in degrees and 
	# relative to the entity's body coordinate system.
	#
	# Script input variables available to user:
	#		ObjectPath - Path of the object, i.e. objects fully qualified name.   string
	#		EpochSec   - Current simulation epoch seconds.                        double  
	#		PosLLA	   - Position the object in LLA.                              string
	#		PosCBF	   - Position the object in CBF.                              string
	#		NumberOfMembers - Number of members in view at this time step. Used
    #                         to define size of input field arrays.  Max 100
	#                         WARNING: Always check this field since, for efficency, 
	#                                  STK may provide old data for 
	#                                  other fields and should be considered stale
    #                                  if this field is 0.                       	 int
    #       MemberPositionFormat - Defines if memberPositions array will be 
	#                              relative position in Theta/Phi/Range (rad/rad/m)
	#                              or X/Y/Z (m/m/m)                               int  
	#		MemberPositions      - Member positions in format specified by
	#                              MemberPositionFormat.                          double(3)
    #       MemberFrequencies   -  Member frequencies (-1 for non-RF members)     double(100)
    #       MemberPwrs          -  Member eirp (-3000dBW for non-emitter members) double(100)
    #       MemberIds           -  Member ids, 0-based as listed in antenna.         int(100)
	#       MemberCategories    -  Member object category (Aircraft, Facility, etc.) int(100)
	#
	# Script outputs which must be filled in by the user:
	#       IsDynamic           - Indicates if script is time-dynamic (see above).   int
	#       NumDirections       - Currently, beam only supports 1 direction           int
	#		Azimuths            - Az in antenna's coordinate system (rad)           double(100)
	#       Elevations          - El in antenna's coordinate system (rad)           double(100)		############################################################################################
		
	# Initialize Output values
	$IsDynamic = 1;
	$NumDirections = 0;
	
	for ($i = 0; $i < $NumberOfMembers; $i++)
	{
	   # If it's not an aircraft...treat it as a jammer and null it
	   if ($MemberCategories[i] != 1)
		{
	      $Azs[$i] = $MemberPositions[3*i];
	      $Els[$i] = $MemberPositions[3*i+1];
		  $NumDirections = $NumDirections+1;
	    }
	 } 
	 

	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################
	
	
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW
	$returnArray[0] =  $IsDynamic;
	$returnArray[1] =  $NumDirections;
	$returnArray[2] =  \@Azs;
	$returnArray[3] =  \@Els;
	

	STKUtil::formatOutputArray(\@returnArray);
	return @returnArray;
}

sub Perl_NullDirectionProvider_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_NullDirectionProvider_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_NullDirectionProvider_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_NullDirectionProvider_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_NullDirectionProvider_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_NullDirectionProvider_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_NullDirectionProvider_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_NullDirectionProvider_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;

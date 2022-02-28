##########################################################################################
# SAPMLE FOR PERL BASED COMM SYSTEM SATELLITE SELECTION STRATEGY PLUGIN 
# SCRIPT PROVIDED BY THE USER.
# PLEASE ADD YOUR STRATEGY CODE IN THE USER SATELLITE SELECTION PLUGIN AREA BELOW.
# DO NOT CHANGE ANYTHING ABOVE OR BELOW THAT AREA IN THE SCRIPT
##########################################################################################

# Perl_CommSysSatSelStrat

use STKUtil;

# declare some global variables

my $Perl_CommSysSatSelStrat_compute_init = -1;
my $Perl_CommSysSatSelStrat_Inputs, $Perl_CommSysSatSelStrat_Outputs;
my %Perl_CommSysSatSelStrat_Outputs_ArgHash;

sub Perl_CommSysSatSelStrat
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_CommSysSatSelStrat_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_CommSysSatSelStrat_compute_init = -1;

		@retVal = Perl_CommSysSatSelStrat_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_CommSysSatSelStrat_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_CommSysSatSelStrat_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = SatSelMeritValue;         ArgumentName = SatSelMeritValue";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;                  ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;                 ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = CbName;                   ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = CommSysPath;              ArgumentName = CommSysPath";

	push @argStr, "ArgumentType = Input;  Name = FromIndex;                ArgumentName = FromIndex";
	push @argStr, "ArgumentType = Input;  Name = NumberOfFromObjects;      ArgumentName = NumberOfFromObjects";
	push @argStr, "ArgumentType = Input;  Name = FromObjectsIDArray;       ArgumentName = FromObjectsIDArray";
	push @argStr, "ArgumentType = Input;  Name = FromObjectIsStatic;       ArgumentName = FromObjectIsStatic";
	push @argStr, "ArgumentType = Input;  Name = FromObjectPosCBFArray;    ArgumentName = FromObjectPosCBFArray";
	push @argStr, "ArgumentType = Input;  Name = FromObjectPosLLAArray;    ArgumentName = FromObjectPosLLAArray";
	push @argStr, "ArgumentType = Input;  Name = FromToRelPosArray;        ArgumentName = FromToRelPosArray";
	push @argStr, "ArgumentType = Input;  Name = FromObjectAttitudeArray;  ArgumentName = FromObjectAttitudeArray";

	push @argStr, "ArgumentType = Input;  Name = ToIndex;                  ArgumentName = ToIndex";
	push @argStr, "ArgumentType = Input;  Name = NumberOfToObjects;        ArgumentName = NumberOfToObjects";
	push @argStr, "ArgumentType = Input;  Name = ToObjectsIDArray;         ArgumentName = ToObjectsIDArray";
	push @argStr, "ArgumentType = Input;  Name = ToObjectIsStatic;         ArgumentName = ToObjectIsStatic";
	push @argStr, "ArgumentType = Input;  Name = ToObjectPosCBFArray;      ArgumentName = ToObjectPosCBFArray";
	push @argStr, "ArgumentType = Input;  Name = ToObjectPosLLAArray;      ArgumentName = ToObjectPosLLAArray";
	push @argStr, "ArgumentType = Input;  Name = ToFromRelPosArray;        ArgumentName = ToFromRelPosArray";
	push @argStr, "ArgumentType = Input;  Name = ToObjectAttitudeArray;    ArgumentName = ToObjectAttitudeArray";


	return @argStr; 
}

sub Perl_CommSysSatSelStrat_compute
{
	# the inputs here are in the order of the requested Inputs, as registered

	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is Date/Time and is a String. Strings are simply scalars in Perl so the assignment is easy:	

	my $date = $origArray[1];

	# next argument is CbName and is a Double. 
	
	my $EpochSec = $origArray[2];

	# next argument is CbName and is a String.
 	
	my $CbName = $origArray[3];

	# next argument is CommSysPath and is a String.
 	
	my $CommSysPath = $origArray[4];

	# next argument is named FromIndex and is type Int. This is the current index from array of From objects in STK
	# This is the From Object being linked (in STK numbers start from 0 to (n-1) ).

	my $FromIndex = $origArray[5];

	# next argument is named NumberOfFromObjects and is type Int. This is the total number of From objects in STK
	# This is the total number of From Objects (in STK numbers start from 0 to (n-1) ).

	my $NumberOfFromObjects = $origArray[6];

	# NOTE: All from object value arrays are of length NumberOfFromObjects.

	# next argument is FromObjectsIDArray (Char) and is an array of Characters of size(NumberOfFromObjects * ObjectIDLength).
      # Currently the ObjectIDLength is = 64 characters.  The array is orgnised as one row per ObjectIDLength* NumberOfFromObjects (rows).
      # The 1-D aray needs to be parsed into 2-D arrray.
	# actual array out of the argument $origArray[6], one must de-reference it as an array as shown below

	my @FromObjectsIDArray = @{$origArray[7]};

	# next argument is FromObjectIsStatic(Bool) and is an array of Boolean values 

	my @FromObjStaticArray = @{$origArray[8]};

	# next argument is the FromObjectPosCBFArray (CartVec3) Array and is double-3. 
	# The array is orgnised as one row per FromObjectPosCBFArray (double-3)* NumberOfFromObjects (rows).
      # The PERL array range is [0] to [NumberOfFromObjects -1]

	my @FromObjectPosCBFArray = @{$origArray[9]};


	# next argument is the FromObjectPosLLAArray(CartVec3) Array and is double-3. 
	# The array is orgnised as one row per FromObjectPosLLAArray(double-3)* NumberOfFromObjects (rows).
      # The PERL array range is [0] to [NumberOfFromObjects -1]

	my @FromObjectPosLLAArray= @{$origArray[10]};

	# next argument is the FromToRelPosArray(CartVec3) Array and is double-3. 
	# The array is orgnised as one row per FromToRelPosArray(double-3)* NumberOfFromObjects (rows).
      # The PERL array range is [0] to [NumberOfFromObjects -1]

	my @FromToRelPosArray= @{$origArray[11]};

	# next argument is the FromObjectAttitudeArray(Quaternion) Array and is double-4. 
	# The array is orgnised as one row per FromObjectAttitudeArray(double-4)* NumberOfFromObjects (rows).
      # The PERL array range is [0] to [NumberOfFromObjects -1]

	my @FromObjectAttitudeArray= @{$origArray[12]};




	# next argument is named ToIndex and is type Int. This is the current index from array of To objects in STK
	# This is the To Object being linked (in STK numbers start from 0 to (n-1) ).

	my $ToIndex = $origArray[13];

	# next argument is named NumberOfToObjects and is type Int. This is the total number of To objects in STK
	# This is the total number of To Objects (in STK numbers start from 0 to (n-1) ).

	my $NumberOfToObjects = $origArray[14];

	# NOTE: All To object value arrays are of length NumberOfToObjects.

	# next argument is ToObjectsIDArray (Char) and is an array of Characters of size(NumberOfToObjects * ObjectIDLength).
      # Currently the ObjectIDLength is = 64 characters.  The array is orgnised as one row per ObjectIDLength* NumberOfToObjects (rows).
      # The 1-D aray needs to be parsed into 2-D arrray.
	# actual array out of the argument $origArray[5], one must de-reference it as an array as shown below

	my @ToObjectsIDArray = @{$origArray[15]};

	# next argument is ToObjectIsStatic(Bool) and is an array of Boolean values 

	my @ToObjStaticArray = @{$origArray[16]};

	# next argument is the ToObjectPosCBFArray (CartVec3) Array and is double-3. 
	# The array is orgnised as one row per ToObjectPosCBFArray (double-3)* NumberOfToObjects (rows).
      # The PERL array range is [0] to [NumberOfToObjects -1]

	my @ToObjectPosCBFArray = @{$origArray[17]};


	# next argument is the ToObjectPosLLAArray(CartVec3) Array and is double-3. 
	# The array is orgnised as one row per ToObjectPosLLAArray(double-3)* NumberOfToObjects (rows).
      # The PERL array range is [0] to [NumberOfToObjects -1]

	my @ToObjectPosLLAArray= @{$origArray[18]};

	# next argument is the ToToRelPosArray(CartVec3) Array and is double-3. 
	# The array is orgnised as one row per ToToRelPosArray(double-3)* NumberOfToObjects (rows).
      # The PERL array range is [0] to [NumberOfToObjects -1]

	my @ToToRelPosArray= @{$origArray[19]};

	# next argument is the ToObjectAttitudeArray(Quaternion) Array and is double-4. 
	# The array is orgnised as one row per ToObjectAttitudeArray(double-4)* NumberOfToObjects (rows).
      # The PERL array range is [0] to [NumberOfToObjects -1]

	my @ToObjectAttitudeArray= @{$origArray[20]};

	if($Perl_CommSysSatSelStrat_compute_init < 0)
	{
		$Perl_CommSysSatSelStrat_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_CommSysSatSelStrat_Inputs = $g_PluginArrayInterfaceHash{'Perl_CommSysSatSelStrat'}{'Inputs'};
		$Perl_CommSysSatSelStrat_Outputs = $g_PluginArrayInterfaceHash{'Perl_CommSysSatSelStrat'}{'Outputs'};

		%Perl_CommSysSatSelStrat_Outputs_ArgHash = $Perl_CommSysSatSelStrat_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_CommSysSatSelStrat_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered

	# SatSelMeritValue , is a number returned by the script to the Link pair at this time instant.
	# STK sorts the list of possible links in a descending MeritValue order.  The scale is open ended.
	# The link with the highest value will be analysed first at the current time instant.
	
	#############################################################################################
	# USER SATELLITE SELECTION STRATEGY PLUGIN AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR SATELLITE SELECTION STRATEGY
	# Please do NOT change anything above this area
	#############################################################################################

	# compute the Test STRATEGY : Satellite selection strategy example

	my $time = $EpochSec;
	my $fromNum = $FromIndex, $toNum = $ToIndex;
	my $ i, j;


	# NumberOfFromObjects is total number of from objects and also the length of the From value arrays
	# NumberOfToObjects is total number of to objects and also the length of the To value arrays
	
	$fromX = @FromObjectPosCBFArray[3 * $fromNum];
	$fromY = @FromObjectPosCBFArray[3 * $fromNum + 1];
	$fromZ = @FromObjectPosCBFArray[3 * $fromNum + 2];
	
	$toX = @ToObjectPosCBFArray[3 * $toNum];
	$toY = @ToObjectPosCBFArray[3 * $toNum + 1];
	$toZ = @ToObjectPosCBFArray[3 * $toNum + 2];

	$rX = $fromX - $toX;
	$rY = $fromY - $toY;
	$rZ = $fromZ - $toZ;

	$range = sqrt ($rX * $rX + $rY * $rY + $rZ * $rZ);

	# STKUtil::printOut "0: $fromX, 1: $fromY, 2: $fromZ, 3: $toX, 4: $toY, 5: $toZ, 6: $range, 7: $fromNum, 8: $toNum";

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $range;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	# Please do NOT change anything below this area
	#############################################################################################

	return @returnArray;
}

sub Perl_CommSysSatSelStrat_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_CommSysSatSelStrat_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_CommSysSatSelStrat_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_CommSysSatSelStrat_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_CommSysSatSelStrat_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_CommSysSatSelStrat_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_CommSysSatSelStrat_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_CommSysSatSelStrat_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
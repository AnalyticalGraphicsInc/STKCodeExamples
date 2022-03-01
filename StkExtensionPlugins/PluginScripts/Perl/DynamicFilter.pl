##########################################################################################
# EXAMPLE PERL BASED SCRIPT MODULATOR PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER MODULATION MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
# If you change the file name then the function names below
# must be edited to match the file name
##########################################################################################

# Perl_DynamicFilter

# declare some global variablesfunction

my $Perl_DynamicFilter_compute_init = -1;
my $Perl_DynamicFilter_Inputs, $Perl_DynamicFilter_Outputs;
my %Perl_DynamicFilter_Outputs_ArgHash;

sub Perl_DynamicFilter
{
# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_DynamicFilter_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_DynamicFilter_compute_init = -1;

		@retVal = Perl_DynamicFilter_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_DynamicFilter_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_DynamicFilter_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = IsDynamic;ArgumentName = IsDynamic";
	push @argStr, "ArgumentType = Output; Name = LowerBandlimit;ArgumentName = LowerBandlimit";
	push @argStr, "ArgumentType = Output; Name = UpperBandlimit;ArgumentName = UpperBandlimit";
	push @argStr, "ArgumentType = Output; Name = NumPoints;ArgumentName = NumPoints";
	push @argStr, "ArgumentType = Output; Name = Attenuation;ArgumentName = Attenuation";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = CbName;ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = ObjectPath;ArgumentName = ObjectPath";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = ObjectPosLLA;ArgumentName = ObjectPosLLA";
	push @argStr, "ArgumentType = Input;  Name = CenterFreq;ArgumentName = CenterFreq";
	push @argStr, "ArgumentType = Input;  Name = FreqStepSize;ArgumentName = FreqStepSize";
	
	return @argStr; 
}


sub Perl_DynamicFilter_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is Date and is a String. Strings are simply scalars in Perl so the assignment is easy:	
	my $date = $origArray[1];

	# next argument is CbName and is a String. 	
	my $CbName = $origArray[2];
	
	# next argument is ObjectPath and is a String.	
	my $ObjectPath = $origArray[3];
	
	# next argument is Time and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $time = $origArray[4];
	
	# next argument is named ObjectPosLLA and is object position in (Lat, Long, Alt) which is of type Double:3 
	# meaning it is an array of 3 doubles, latitude (Rad), logitude (Rad), altitude (meters) are components of position. 
	# Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $origArray[6], one must de-reference it as an array as shown below
	my @ObjectPosLLA = @{$origArray[5]};
		
	# next argument is CenterFreq and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $CenterFreq = $origArray[6];
		
	# next argument is FreqStepSize and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $FreqStepSize = $origArray[7];
	
	
	if($Perl_DynamicFilter_compute_init < 0)
	{
		$Perl_DynamicFilter_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_DynamicFilter_Inputs = $g_PluginArrayInterfaceHash{'Perl_DynamicFilter'}{'Inputs'};
		$Perl_DynamicFilter_Outputs = $g_PluginArrayInterfaceHash{'Perl_DynamicFilter'}{'Outputs'};

		%Perl_DynamicFilter_Outputs_ArgHash = $Perl_DynamicFilter_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_DynamicFilter_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered
	
	#############################################################################################
	# USER DynamicFilter MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR DynamicFilter MODEL
	#############################################################################################

	# compute the Test Model : DynamicFilter Model
	
	#	Expected Output Parameters
	my $IsDynamic, $LowerBandlimit, $UpperBandlimit, $NumPoints, @Attenuation;
	$Attenuation[99999] = -3000;  # Force array capacity to be 100000 in size.
	
	#	Variables for this example
	my $I, $filterSelector, $attenDB;
	my $objPosLat, $objPosLon, $objPosAlt; 

	#	Initialize Output values
	$IsDynamic        = 1;
	#$FreqStepSize = 10000;
	
	$objPosLat = @ObjectPosLLA[0];
	$objPosLon = @ObjectPosLLA[1];
	$objPosAlt = @ObjectPosLLA[2];
	
	$filterSelector = int($time) % 3;

	# First filter is...
	# 60 MHz wide w/ zero attenuation at center frequency
	# and -60 dB down at edges
	if($filterSelector == 0)
	{
	   $LowerBandlimit = -30e6;
       $UpperBandlimit = 30e6;
	}
	# Second filter is...
	# 30 MHz wide w/ zero attenuation at center frequency
	# and -60 dB down at edges
	elsif($filterSelector == 1)
	{
	   $LowerBandlimit = -15e6;
       $UpperBandlimit = 15e6;
	}
	# Third filter is...
	# 10 MHz wide w/ zero attenuation at center frequency
	# and -60 dB down at edges
	elsif($filterSelector == 2)
	{
	   $LowerBandlimit = -5e6;
       $UpperBandlimit = 5e6;
	}

	$NumPoints = 1+($UpperBandlimit - $LowerBandlimit)/$FreqStepSize;

	# Make sure we don't blow out the fixed array size
    if($NumPoints > 100000)
	{
        $NumPoints = 100000;
        $FreqStepSize = ($UpperBandlimit - $LowerBandlimit)/($NumPoints - 1);
	}

	for ($I = 0; $I <($NumPoints)/2; $I++)
	{
		$attenDB = -60 + $I*2*60/($NumPoints-1);
		$Attenuation[$I] = $attenDB;
	    $Attenuation[$NumPoints-1-$I] = $attenDB;
	}
	#$Attenuation[$I] = $attenDB = -60 + $I*2*60/($NumPoints);
	
	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $IsDynamic;
	$returnArray[1] =  $LowerBandlimit;
	$returnArray[2] =  $UpperBandlimit;
	$returnArray[3] =  $NumPoints;
	$returnArray[4] =  \@Attenuation;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################

	STKUtil::formatOutputArray(\@returnArray);
	return @returnArray;
}


sub Perl_DynamicFilter_showArgs
{
	my @argStrArray;

	#STKUtil::printOut "Doing Perl_DynamicFilter_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicFilter_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_DynamicFilter_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicFilter_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicFilter_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_DynamicFilter_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicFilter_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
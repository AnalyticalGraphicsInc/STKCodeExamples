##########################################################################################
# EXAMPLE PERL BASED SCRIPT MODULATOR PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER MODULATION MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
# If you change the file name then the function names below
# must be edited to match the file name
##########################################################################################

# Perl_DynamicDemodulator

# declare some global variablesfunction

my $Perl_DynamicDemodulator_compute_init = -1;
my $Perl_DynamicDemodulator_Inputs, $Perl_DynamicDemodulator_Outputs;
my %Perl_DynamicDemodulator_Outputs_ArgHash;

sub Perl_DynamicDemodulator
{
# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_DynamicDemodulator_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_DynamicDemodulator_compute_init = -1;

		@retVal = Perl_DynamicDemodulator_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_DynamicDemodulator_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_DynamicDemodulator_register
{
	my @argStr;
	
	push @argStr, "ArgumentType = Output; Name = OutBER;ArgumentName = OutBER";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = CbName;ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = ObjectPath;ArgumentName = ObjectPath";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = RFFreq;ArgumentName = RFFreq";
	push @argStr, "ArgumentType = Input;  Name = ObjectPosLLA;ArgumentName = ObjectPosLLA";
	push @argStr, "ArgumentType = Input;  Name = DataRate;ArgumentName = DataRate";
	push @argStr, "ArgumentType = Input; Name = SpectrumLimitLo;ArgumentName = SpectrumLimitLo";
	push @argStr, "ArgumentType = Input; Name = SpectrumLimitHi;ArgumentName = SpectrumLimitHi";
	push @argStr, "ArgumentType = Input; Name = SignalModulationName;ArgumentName = SignalModulationName";
	push @argStr, "ArgumentType = Input; Name = SignalEbNo;ArgumentName = SignalEbNo";
	
	return @argStr; 
}


sub Perl_DynamicDemodulator_compute
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
		
	# next argument is RFCarrierFreq and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $RFFreq = $origArray[5];
	
	# next argument is named ObjectPosLLA and is object position in (Lat, Long, Alt) which is of type Double:3 
	# meaning it is an array of 3 doubles, latitude (Rad), logitude (Rad), altitude (meters) are components of position. 
	# Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $origArray[6], one must de-reference it as an array as shown below
	my @ObjectPosLLA = @{$origArray[6]};
	
	# next argument is DataRate and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $DataRate = $origArray[7];
	
	# next argument is SpectrumLimitLow and is a value. Values are simply scalars in Perl so the assignment is easy:
	my $SpectrumLimitLo = $origArray[8];
	
	# next argument is SpectrumLimitHi and is a value. Values are simply scalars in Perl so the assignment is easy:
	my $SpectrumLimitHi = $origArray[9];
	
	# next argument is SignalModulationName and is a String. Strings are simply scalars in Perl so the assignment is easy:	
	my $SignalModulationName = $origArray[10];
	
	# next argument is SignalEbNo and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $SignalEbNo = $origArray[11];
	
	if($Perl_DynamicDemodulator_compute_init < 0)
	{
		$Perl_DynamicDemodulator_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_DynamicDemodulator_Inputs = $g_PluginArrayInterfaceHash{'Perl_DynamicDemodulator'}{'Inputs'};
		$Perl_DynamicDemodulator_Outputs = $g_PluginArrayInterfaceHash{'Perl_DynamicDemodulator'}{'Outputs'};

		%Perl_DynamicDemodulator_Outputs_ArgHash = $Perl_DynamicDemodulator_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_DynamicDemodulator_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered
	
	#############################################################################################
	# USER DynamicDemodulator MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR DynamicDemodulator MODEL
	#############################################################################################

	# compute the Test Model : DynamicDemodulator Model
	
	#	Expected Output Parameters
	my $OutBER;
	
	#	Variables for this example
	my $waveformSelector;
	my $objPosLat, $objPosLon, $objPosAlt; 

	#	Initialize Output values
	# NOTE: Your doppler resolution will be limited to FreqStepSize, so be sure to set number of 
	#      NumPSDPts to achieve adequate doppler resolution.
    $OutBER           = 0.5;
	
	$objPosLat = @ObjPosLLA[0];
	$objPosLon = @ObjPosLLA[1];
	$objPosAlt = @ObjPosLLA[2];
	
	$waveformSelector = int($time) % 3;
	
	if($waveformSelector == 0)
	{
		$OutBER = 0.5;
	}
	elsif ( $waveformSelector == 1)
	{
		$OutBER = 1.1111111e-1;
	}
	else
	{
	     if($SignalModulationName eq "BPSK")
		 {
		     $OutBER = 2.222222e-2;
	     }
		 elsif($SignalModulationName eq "OQPSK")
		 {
		     $OutBER = 3.333333e-3;
		 }
	     elsif($SignalModulationName eq "MyOQPSK")
		 {
		     $OutBER = 4.444444e-4;
		 }
	     else
		 {
		     $OutBER = 5.555555e-5;
	     }
	}

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $OutBER;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################

	return @returnArray;
}


sub Perl_DynamicDemodulator_showArgs
{
	my @argStrArray;

	#STKUtil::printOut "Doing Perl_DynamicDemodulator_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicDemodulator_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_DynamicDemodulator_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicDemodulator_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicDemodulator_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_DynamicDemodulator_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicDemodulator_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
##########################################################################################
# EXAMPLE PERL BASED SCRIPT MODULATOR PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER MODULATION MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
# If you change the file name then the function names below
# must be edited to match the file name
##########################################################################################

# Perl_DynamicModulator_IdealPSD

# declare some global variablesfunction

my $Perl_DynamicModulator_IdealPSD_compute_init = -1;
my $Perl_DynamicModulator_IdealPSD_Inputs, $Perl_DynamicModulator_IdealPSD_Outputs;
my %Perl_DynamicModulator_IdealPSD_Outputs_ArgHash;

sub Perl_DynamicModulator_IdealPSD
{
# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_DynamicModulator_IdealPSD_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_DynamicModulator_IdealPSD_compute_init = -1;

		@retVal = Perl_DynamicModulator_IdealPSD_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_DynamicModulator_IdealPSD_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_DynamicModulator_IdealPSD_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = IsDynamic;ArgumentName = IsDynamic";
	push @argStr, "ArgumentType = Output; Name = ModulationName;ArgumentName = ModulationName";
	push @argStr, "ArgumentType = Output; Name = ModulationEfficiency;ArgumentName = ModulationEfficiency";
	push @argStr, "ArgumentType = Output; Name = CodeRate;ArgumentName = CodeRate";
	push @argStr, "ArgumentType = Output; Name = PSDShape;ArgumentName = PSDShape";
	push @argStr, "ArgumentType = Output; Name = SpectrumLimitLow;ArgumentName = SpectrumLimitLow";
	push @argStr, "ArgumentType = Output; Name = SpectrumLimitHi;ArgumentName = SpectrumLimitHi";
	push @argStr, "ArgumentType = Output; Name = UsePSD;ArgumentName = UsePSD";
	push @argStr, "ArgumentType = Output; Name = ChipsPerBit;ArgumentName = ChipsPerBit";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = CbName;ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = ObjectPath;ArgumentName = ObjectPath";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = RFCarrierFreq;ArgumentName = RFCarrierFreq";
	push @argStr, "ArgumentType = Input;  Name = ObjectPosLLA;ArgumentName = ObjectPosLLA";
	push @argStr, "ArgumentType = Input;  Name = DataRate;ArgumentName = DataRate";
	
	return @argStr; 
}


sub Perl_DynamicModulator_IdealPSD_compute
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
	my $RFCarrierFreq = $origArray[5];
	
	# next argument is named ObjectPosLLA and is object position in (Lat, Long, Alt) which is of type Double:3 
	# meaning it is an array of 3 doubles, latitude (Rad), logitude (Rad), altitude (meters) are components of position. 
	# Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $origArray[6], one must de-reference it as an array as shown below
	my @ObjectPosLLA = @{$origArray[6]};
	
	# next argument is DataRate and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $DataRate = $origArray[7];
	
	
	if($Perl_DynamicModulator_IdealPSD_compute_init < 0)
	{
		$Perl_DynamicModulator_IdealPSD_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_DynamicModulator_IdealPSD_Inputs = $g_PluginArrayInterfaceHash{'Perl_DynamicModulator_IdealPSD'}{'Inputs'};
		$Perl_DynamicModulator_IdealPSD_Outputs = $g_PluginArrayInterfaceHash{'Perl_DynamicModulator_IdealPSD'}{'Outputs'};

		%Perl_DynamicModulator_IdealPSD_Outputs_ArgHash = $Perl_DynamicModulator_IdealPSD_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_DynamicModulator_IdealPSD_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered
	
	#############################################################################################
	# USER DynamicModulator_IdealPSD MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR DynamicModulator_IdealPSD MODEL
	#############################################################################################

	# compute the Test Model : DynamicModulator_IdealPSD Model
	
	#	Expected Output Parameters
	my $IsDynamic, $ModulationName,  $ModulationEfficiency, $CodeRate, $SpectrumLimitLow, $SpectrumLimitHi;
	my $UsePSD, $ChipsPerBit;
	
	#	Variables for this example
	my $I, $waveformSelector;
	my $objPosLat, $objPosLon, $objPosAlt; 

	#	Initialize Output values
	$IsDynamic        = 1;
	$ModulationName   = "BPSK"; # Only used by STK for proper demodulation
	$PSDShape         = "BPSK";
	$ModulationEfficiency = 2.0;  
	$CodeRate         = 1.0;
    $ChipsPerBit      = 1;
	$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
	$SpectrumLimitHi  = -$SpectrumLimitLow;
    $UsePSD           = 0;
	
	$objPosLat = @ObjPosLLA[0];
	$objPosLon = @ObjPosLLA[1];
	$objPosAlt = @ObjPosLLA[2];
	
	$waveformSelector = int($time) % 4;
	
	# First waveform is...
    # QAM256
	if($waveformSelector == 0)
	{
		$ModulationName   = "QAM256";
		$PSDShape         = "QAM256";
		$ModulationEfficiency = 0.25;
		$CodeRate = 1.0;
		$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
		$SpectrumLimitHi  = -$SpectrumLimitLow;
	}	
    # Second waveform is...
    # QAM16
	elsif ( $waveformSelector == 1)
	{
		$ModulationName   = "QAM16";
		$PSDShape         = "QAM16";
		$ModulationEfficiency = 0.5;
		$CodeRate = 1.0;
		$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
		$SpectrumLimitHi  = -$SpectrumLimitLow;
	}
    # Third waveform is...
    # 8PSK
	elsif ($waveformSelector == 2)
	{
		$ModulationName   = "8PSK";
		$PSDShape         = "8PSK";
		$ModulationEfficiency =  0.6666666666;
		$CodeRate = 1.0;
		$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
		$SpectrumLimitHi  = -$SpectrumLimitLow;
	}
    # Fourth waveform is...
    # BPSK
	else
	{
		$ModulationName   = "BPSK"; 
        $PSDShape         = "BPSK";
		$ModulationEfficiency = 2.0;
		$CodeRate = 1.0;
		$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
		$SpectrumLimitHi  = -$SpectrumLimitLow;
	}

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $IsDynamic;
	$returnArray[1] =  $ModulationName;
	$returnArray[2] =  $ModulationEfficiency;
	$returnArray[3] =  $CodeRate;
	$returnArray[4] =  $PSDShape;
	$returnArray[5] =  $SpectrumLimitLow;
	$returnArray[6] =  $SpectrumLimitHi;
	$returnArray[7] =  $UsePSD;
	$returnArray[8] =  $ChipsPerBit;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################

	return @returnArray;
}


sub Perl_DynamicModulator_IdealPSD_showArgs
{
	my @argStrArray;

	#STKUtil::printOut "Doing Perl_DynamicModulator_IdealPSD_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicModulator_IdealPSD_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_DynamicModulator_IdealPSD_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicModulator_IdealPSD_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicModulator_IdealPSD_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_DynamicModulator_IdealPSD_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicModulator_IdealPSD_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
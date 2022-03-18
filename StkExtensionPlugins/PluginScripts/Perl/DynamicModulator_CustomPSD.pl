##########################################################################################
# EXAMPLE PERL BASED SCRIPT MODULATOR PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER MODULATION MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
# If you change the file name then the function names below
# must be edited to match the file name
##########################################################################################

# Perl_DynamicModulator_CustomPSD

# declare some global variablesfunction

my $Perl_DynamicModulator_CustomPSD_compute_init = -1;
my $Perl_DynamicModulator_CustomPSD_Inputs, $Perl_DynamicModulator_CustomPSD_Outputs;
my %Perl_DynamicModulator_CustomPSD_Outputs_ArgHash;

sub Perl_DynamicModulator_CustomPSD
{
# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_DynamicModulator_CustomPSD_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_DynamicModulator_CustomPSD_compute_init = -1;

		@retVal = Perl_DynamicModulator_CustomPSD_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_DynamicModulator_CustomPSD_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_DynamicModulator_CustomPSD_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = IsDynamic;ArgumentName = IsDynamic";
	push @argStr, "ArgumentType = Output; Name = ModulationName;ArgumentName = ModulationName";
	push @argStr, "ArgumentType = Output; Name = SpectrumLimitLow;ArgumentName = SpectrumLimitLow";
	push @argStr, "ArgumentType = Output; Name = SpectrumLimitHi;ArgumentName = SpectrumLimitHi";
	push @argStr, "ArgumentType = Output; Name = UsePSD;ArgumentName = UsePSD";
	push @argStr, "ArgumentType = Output; Name = NumPSDPoints;ArgumentName = NumPSDPoints";
	push @argStr, "ArgumentType = Output; Name = FreqStepSize;ArgumentName = FreqStepSize";
	push @argStr, "ArgumentType = Output; Name = PSDData;ArgumentName = PSDData";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = CbName;ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = ObjectPath;ArgumentName = ObjectPath";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = RFCarrierFreq;ArgumentName = RFCarrierFreq";
	push @argStr, "ArgumentType = Input;  Name = ObjectPosLLA;ArgumentName = ObjectPosLLA";
	push @argStr, "ArgumentType = Input;  Name = DataRate;ArgumentName = DataRate";
	
	return @argStr; 
}


sub Perl_DynamicModulator_CustomPSD_compute
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
	
	
	if($Perl_DynamicModulator_CustomPSD_compute_init < 0)
	{
		$Perl_DynamicModulator_CustomPSD_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_DynamicModulator_CustomPSD_Inputs = $g_PluginArrayInterfaceHash{'Perl_DynamicModulator_CustomPSD'}{'Inputs'};
		$Perl_DynamicModulator_CustomPSD_Outputs = $g_PluginArrayInterfaceHash{'Perl_DynamicModulator_CustomPSD'}{'Outputs'};

		%Perl_DynamicModulator_CustomPSD_Outputs_ArgHash = $Perl_DynamicModulator_CustomPSD_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_DynamicModulator_CustomPSD_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered
	
	#############################################################################################
	# USER DynamicModulator_CustomPSD MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR DynamicModulator_CustomPSD MODEL
	#
	# This sample demonstrates how to dynamically create a custom Power Spectral Density.  If 
	# Use Signal PSD is not enabled within STK, the magnitude will be assumed unity across the
	# band as defined by SpectrumLimitLow amd SpectrumLimitHi.   If one does not wants to specify 
	# the power spectral density, one should use the other interface (see 
	# Perl_DynamicModulator_IdealPSD.pl) example.
	#
	# All input and out paramters have been mapped to variables described below.
	#############################################################################################
	# NOTE: the outputs that are returned MUST be in the same order as registered
	# If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
	# will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
	# at every timestep.
	#
	# All frequency and frequency step value units are Hertz.
	# PSD values are in dB.
	#
	# Script input variables available to user:
	#		DateUTC    - Date in UTC.                                            string
	#		CbName	   - Name of the transmitter's central body.                 string
	#		ObjectPath - Path of the object, i.e. objects fully qualified name.  string
	#		EpochSec   - Current simulation epoch seconds.                       double  
    #       RFCarrierFreq - RF carrier frequency, in Hz.                         double  
	#		ObjPosLLA     - Objects position in LLA (Rad,Rad,m).                 double(3)
    #       DataRate      - Information data rate of the transmitter, in bps.    double
	#
	# Script outputs which must be filled in by the user:
	#       IsDynamic            - Indicates if script is time-dynamic (see above).   int
	#       ModulationName       - Modulation name, for demodulator selection.        string ( max 32 characters )
	#		SpectrumLimitLow - Lower band limit of output spectrum in Hz and
    #                          relative to RF carrier frequency (-100GHz to 0).   double
	#		SpectrumLimitHi  - Upper band limit of output spectrum in Hz and 
	#                          relative to RF carrier frequency (0 to 100GHz).    double 
	#       UsePSD           - Use signal PSD indicator, 0 or 1.                  int
    #       NumPSDPoints     - Number of PSD points being returned (max 100,000)  int
	#       PsdData          - Psd values.                                        double array [ NumPSDPoints x 1 ]
    #       FreqStepSize     - Frequency step size of Psd data, in Hz             double	
	#############################################################################################

	# compute the Test Model : DynamicModulator_CustomPSD Model
	
	#	Expected Output Parameters
	my $IsDynamic, $ModulationName, $SpectrumLimitLow, $SpectrumLimitHi;
	my $UsePSD, $NumPSDPoints, @PSDData, $FreqStepSize;
	$PSDData[99999] = -3000;  # Force array capacity to be 100000 in size.

		
	#	Variables for this example
	my $I, $waveformSelector;
	my $objPosLat, $objPosLon, $objPosAlt, $ChipsPerBit, $ModulationEfficiency, $CodeRate, $SpreadingFactor; 

	#	Initialize Output values
	$IsDynamic        = 1;
	$ModulationName   = "BPSK"; # Only used by STK for proper demodulation
	$ModulationEfficiency = 2.0;  
	$CodeRate         = 1.0;
    $ChipsPerBit      = 1;
	$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
	$SpectrumLimitHi  = -$SpectrumLimitLow;
    $UsePSD           = 1;
	$NumPSDPoints	  = 50001;
	
	$FreqStepSize     = ($SpectrumLimitHi - $SpectrumLimitLow)/$NumPSDPoints;
	$objPosLat = @ObjPosLLA[0];
	$objPosLon = @ObjPosLLA[1];
	$objPosAlt = @ObjPosLLA[2];
	
	$waveformSelector = int($time) % 4;
	
	# First waveform is...
	# DataRate Hz wide w/ peak magnitude of 0dB at carrier frequency and
    # -30 dB down at edges.
	if($waveformSelector == 0)
	{
		for ($I = 0; $I < int($NumPSDPoints/2); $I++)
		{
			$PSDData [$I] = -30 + $I*(30/$NumPSDPoints*2);
			$PSDData [$NumPSDPoints-$I] = -30 + $I*(30/$NumPSDPoints*2);
		}
    }
	# Second waveform is...
	# DataRate Hz wide w/ peak magnitude of 0dB at carrier frequency and
    # -10 dB down at edges.
	elsif ($waveformSelector == 1)
	{
		for ($I = 0; $I < int($NumPSDPoints/2); $I++)
		{
			$PSDData [$I] = -10 + $I*(10/$NumPSDPoints*2);
			$PSDData [$NumPSDPoints-$I] = -10 + $I*(10/$NumPSDPoints*2);
		}
	}
    # Third waveform is...
	# similar to second waveform but is 4 times more bandwidth efficient
	elsif ($waveformSelector == 2)
	{
	    $ModulationEfficiency = 0.25;
    	$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
	    $SpectrumLimitHi  = -$SpectrumLimitLow;
	    $FreqStepSize     = ($SpectrumLimitHi - $SpectrumLimitLow)/$NumPSDPoints;
	
		for ($I = 0; $I < int($NumPSDPoints/2); $I++)
		{
			$PSDData [$I] = -10 + $I*(10/$NumPSDPoints*2);
			$PSDData [$NumPSDPoints-$I] = -10 + $I*(10/$NumPSDPoints*2);
		}
	}
    # Fourth waveform is...
	# similar to third waveform but 2x wider than 3rd
	else
	{
	    $ModulationEfficiency = 0.25;
		$ChipsPerBit = 2;
    	$SpectrumLimitLow = -($DataRate * $ModulationEfficiency / $CodeRate * $ChipsPerBit)/2.0;
	    $SpectrumLimitHi  = -$SpectrumLimitLow;
	    $FreqStepSize     = ($SpectrumLimitHi - $SpectrumLimitLow)/$NumPSDPoints;
	
		for ($I = 0; $I < int($NumPSDPoints/2); $I++)
		{
			$PSDData [$I] = -10 + $I*(10/$NumPSDPoints*2);
			$PSDData [$NumPSDPoints-$I] = -10 + $I*(10/$NumPSDPoints*2);
		}
	}
	
	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $IsDynamic;
	$returnArray[1] =  $ModulationName;
	$returnArray[2] =  $SpectrumLimitLow;
	$returnArray[3] =  $SpectrumLimitHi;
	$returnArray[4] =  $UsePSD;
	$returnArray[5] =  $NumPSDPoints;
	$returnArray[6] =  $FreqStepSize;
	$returnArray[7] =  \@PSDData;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################


    STKUtil::formatOutputArray(\@returnArray);
	return @returnArray;
}


sub Perl_DynamicModulator_CustomPSD_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_DynamicModulator_CustomPSD_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicModulator_CustomPSD_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_DynamicModulator_CustomPSD_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicModulator_CustomPSD_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_DynamicModulator_CustomPSD_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_DynamicModulator_CustomPSD_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_DynamicModulator_CustomPSD_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	#STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
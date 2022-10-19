##########################################################################################
# SAPMLE FOR PERL BASED COMM CONSTRAINT PLUGIN SCRIPT
# SCRIPT PROVIDED BY THE USER.
# PLEASE ADD YOUR CONSTRAINT CODE IN THE USER COMM CONSTRAINT PLUGIN AREA BELOW.
# DO NOT CHANGE ANYTHING ABOVE OR BELOW THAT AREA IN THE SCRIPT
##########################################################################################

# Perl_CommConstraint

use STKUtil;

# declare some global variables

my $Perl_CommConstraint_compute_init = -1;
my $Perl_CommConstraint_Inputs, $Perl_CommConstraint_Outputs;
my %Perl_CommConstraint_Outputs_ArgHash;

sub Perl_CommConstraint
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_CommConstraint_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_CommConstraint_compute_init = -1;

		@retVal = Perl_CommConstraint_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_CommConstraint_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_CommConstraint_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = PluginConstraintValue;    ArgumentName = PluginConstraintValue";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;                  ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;                 ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = CbName;                   ArgumentName = CbName";

	push @argStr, "ArgumentType = Input;  Name = ReceiverPath;             ArgumentName = ReceiverPath";
	push @argStr, "ArgumentType = Input;  Name = TransmitterPath;          ArgumentName = TransmitterPath";
	push @argStr, "ArgumentType = Input;  Name = RcvrPosCBF;               ArgumentName = RcvrPosCBF";
	push @argStr, "ArgumentType = Input;  Name = RcvrAttitude;             ArgumentName = RcvrAttitude";
	push @argStr, "ArgumentType = Input;  Name = XmtrPosCBF;               ArgumentName = XmtrPosCBF";
	push @argStr, "ArgumentType = Input;  Name = XmtrAttitude;             ArgumentName = XmtrAttitude";

	push @argStr, "ArgumentType = Input;  Name = ReceivedFrequency;        ArgumentName = ReceivedFrequency";
	push @argStr, "ArgumentType = Input;  Name = DataRate;                 ArgumentName = DataRate";
	push @argStr, "ArgumentType = Input;  Name = Bandwidth;                ArgumentName = Bandwidth";
	push @argStr, "ArgumentType = Input;  Name = CDMAGainValue;            ArgumentName = CDMAGainValue";
	push @argStr, "ArgumentType = Input;  Name = ReceiverGain;             ArgumentName = ReceiverGain";

	push @argStr, "ArgumentType = Input;  Name = PolEfficiency;            ArgumentName = PolEfficiency";
	push @argStr, "ArgumentType = Input;  Name = PolRelativeAngle;         ArgumentName = PolRelativeAngle";
	push @argStr, "ArgumentType = Input;  Name = RIP;                      ArgumentName = RIP";
	push @argStr, "ArgumentType = Input;  Name = FluxDensity;              ArgumentName = FluxDensity";
	push @argStr, "ArgumentType = Input;  Name = GOverT;                   ArgumentName = GOverT";
	push @argStr, "ArgumentType = Input;  Name = CarrierPower;             ArgumentName = CarrierPower";
	push @argStr, "ArgumentType = Input;  Name = BandwidthOverlap;         ArgumentName = BandwidthOverlap";
	push @argStr, "ArgumentType = Input;  Name = CNo;                      ArgumentName = CNo";
	push @argStr, "ArgumentType = Input;  Name = CNR;                      ArgumentName = CNR";
	push @argStr, "ArgumentType = Input;  Name = EbNo;                     ArgumentName = EbNo";
	push @argStr, "ArgumentType = Input;  Name = BER;                      ArgumentName = BER";


	return @argStr; 
}

sub Perl_CommConstraint_compute
{
	# the inputs here are in the order of the requested Inputs, as registered

	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is Date/Time and is a String. Strings are simply scalars in Perl so the assignment is easy:	

	my $date = $origArray[1];

	# next argument is the simulation time and is a Double. 
	
	my $EpochSec = $origArray[2];

	# next argument is CbName and is a String.
 	
	my $CbName = $origArray[3];

	# next argument is ReceiverPath and is a String.
 	
	my $ReceiverPath = $origArray[4];

	# next argument is TransmitterPath and is a String.
 	
	my $TransmitterPath = $origArray[5];

	# next argument is named RcvrPosCBF and is type (CartVec3) Array and is double-3. 

	my @RcvrPosCBF = @{$origArray[6]};

	# next argument is named RcvrAttitude and is type (Quaternion) Array and is double-4. 

	my @RcvrAttitude = @{$origArray[7]};

	# next argument is named XmtrPosCBF and is type (CartVec3) Array and is double-3. 

	my @XmtrPosCBF = @{$origArray[8]};

	# next argument is named XmtrAttitude and is type (Quaternion) Array and is double-4. 

	my @XmtrAttitude = @{$origArray[9]};



	# next argument is ReceivedFrequency and is a double. The value units are Hertz.

	my $ReceivedFrequency = $origArray[10];

	# next argument is DataRate and is a double. The value units are Bits per Second.

	my $DataRate = $origArray[11];

	# next argument is Bandwidth and is a double. The value units are Hertz.

	my $Bandwidth = $origArray[12];

	# next argument is CDMAGainValue and is a double. The value is in dB.

	my $CDMAGainValue = $origArray[13];

	# next argument is ReceiverGain and is a double. The value is in dB.

	my $ReceiverGain = $origArray[14];



	# next argument is PolEfficiency and is a double. The value range is 0 - 1.

	my $PolEfficiency = $origArray[15];

	# next argument is PolRelativeAngle and is a double. The value units are radians.

	my $PolRelativeAngle = $origArray[16];

	# next argument is RIP and is a double. The value units are dB(W/m^2).

	my $RIP = $origArray[17];

	# next argument is FluxDensity and is a double. The value units are dB(W/m^2 Hz).

	my $FluxDensity = $origArray[18];

	# next argument is GOverT and is a double. The value units are dB/K.

	my $GOverT = $origArray[19];

	# next argument is CarrierPower and is a double. The value units are dBW.

	my $CarrierPower = $origArray[20];

	# next argument is BandwidthOverlap and is a double. The value range is 0 - 1.

	my $BandwidthOverlap = $origArray[21];

	# next argument is CNo and is a double. The value is in dB.

	my $CNo = $origArray[22];

	# next argument is CNR and is a double. The value is in dB.

	my $CNR = $origArray[23];

	# next argument is EbNo and is a double. The value is in dB.

	my $EbNo = $origArray[24];

	# next argument is BER and is a double.

	my $BER = $origArray[25];





	if($Perl_CommConstraint_compute_init < 0)
	{
		$Perl_CommConstraint_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_CommConstraint_Inputs = $g_PluginArrayInterfaceHash{'Perl_CommConstraint'}{'Inputs'};
		$Perl_CommConstraint_Outputs = $g_PluginArrayInterfaceHash{'Perl_CommConstraint'}{'Outputs'};

		%Perl_CommConstraint_Outputs_ArgHash = $Perl_CommConstraint_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_CommConstraint_showArgs();
	}


	#############################################################################################
	# NOTE: the outputs that are returned MUST be in the same order as registered

	# Constraint Value , is a value returned by the script to STK Access at this time instant.
	# STK applies the MIN & MAX constrint criteria and the Exclude Interval flag (if checked)
	# to this value to determine access availability. 
	
	#############################################################################################
	# USER COMM ONCSTRAINT PLUGIN AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR CONTSTRAINT CRITERION
	# Please do NOT change anything above this area
	#############################################################################################

	# compute the Test CONSTRAINT VALUE : Comm Constraint plugin Script example

	my $time = $EpochSec;

	my $ i, j;


	# NumberOfFromObjects is total number of from objects and also the length of the From value arrays
	# NumberOfToObjects is total number of to objects and also the length of the To value arrays
	
	$fromX = @XmtrPosCBF[0];
	$fromY = @XmtrPosCBF[1];
	$fromZ = @XmtrPosCBF[2];
	
	$toX = @RcvrPosCBF[0];
	$toY = @RcvrPosCBF[1];
	$toZ = @RcvrPosCBF[2];

	$rX = $fromX - $toX;
	$rY = $fromY - $toY;
	$rZ = $fromZ - $toZ;

	$range = sqrt ($rX * $rX + $rY * $rY + $rZ * $rZ) / 1000.0;

	# STKUtil::printOut "0: $fromX, 1: $fromY, 2: $fromZ, 3: $toX, 4: $toY, 5: $toZ, 6: $rX, 7: $rY, 8: $rZ, 9: $range";

	# Example Transmission of 625/50 television by INTELSAT
	
	# ftpp = peak-to-peak frequency deviation (Hz)
	$ftpp = 15.0e6;
	
	# Bn = Noise Bandwidth at Receiver (Hz)
	$Bn = 5.0e6;
	
	# PW = improvement factor due to pre-emphasis and de-emphasis and weighting factor (dB)
	$PW = 13.2;
		
	$CNo = $origArray[22];
			
	# SNR = 3/2 * (fttp/Bn)^2 * (1/Bn) * pw * C/No
			
	$SNR = 10.0*log(1.5*$ftpp*$ftpp/($Bn*$Bn*$Bn))/log(10.0) + $PW + $CNo;

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $SNR;
	
	#############################################################################################
	# END OF USER CONSTRAINT MODEL AREA	
	# Please do NOT change anything below this area
	#############################################################################################

	return @returnArray;
}

sub Perl_CommConstraint_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_CommConstraint_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_CommConstraint_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_CommConstraint_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_CommConstraint_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_CommConstraint_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_CommConstraint_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_CommConstraint_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
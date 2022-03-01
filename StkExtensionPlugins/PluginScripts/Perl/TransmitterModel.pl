##########################################################################################
# SAMPLE FOR PERL BASED CUSTOM TRANSMITTER MODEL PLUGIN SCRIPT PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER TRANSMITTER MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_TransmitterModel

# declare some global variablesfunction

my $Perl_TransmitterModel_compute_init = -1;
my $Perl_TransmitterModel_Inputs, $Perl_TransmitterModel_Outputs;
my %Perl_TransmitterModel_Outputs_ArgHash;

sub Perl_TransmitterModel
{
# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_TransmitterModel_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_TransmitterModel_compute_init = -1;

		@retVal = Perl_TransmitterModel_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_TransmitterModel_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_TransmitterModel_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = Frequency;         ArgumentName = Frequency";
	push @argStr, "ArgumentType = Output; Name = Power;             ArgumentName = Power";
	push @argStr, "ArgumentType = Output; Name = Gain;              ArgumentName = Gain";
	push @argStr, "ArgumentType = Output; Name = DataRate;          ArgumentName = DataRate";
	push @argStr, "ArgumentType = Output; Name = Bandwidth;         ArgumentName = Bandwidth";
	push @argStr, "ArgumentType = Output; Name = Modulation;        ArgumentName = Modulation";
	push @argStr, "ArgumentType = Output; Name = PostTransmitLoss;  ArgumentName = PostTransmitLoss";
	push @argStr, "ArgumentType = Output; Name = PolType;           ArgumentName = PolType";
	push @argStr, "ArgumentType = Output; Name = PolRefAxis;        ArgumentName = PolRefAxis";
	push @argStr, "ArgumentType = Output; Name = PolTiltAngle;      ArgumentName = PolTiltAngle";
	push @argStr, "ArgumentType = Output; Name = PolAxialRatio;     ArgumentName = PolAxialRatio";
	push @argStr, "ArgumentType = Output; Name = UseCDMASpreadGain; ArgumentName = UseCDMASpreadGain";
	push @argStr, "ArgumentType = Output; Name = CDMAGain;          ArgumentName = CDMAGain";

	push @argStr, "ArgumentType = Input;  Name = EpochSec;          ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = DateUTC;           ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = CbName;            ArgumentName = CbName";
	push @argStr, "ArgumentType = Input;  Name = XmtrPosCBF;        ArgumentName = XmtrPosCBF";
	push @argStr, "ArgumentType = Input;  Name = XmtrAttitude;      ArgumentName = XmtrAttitude";
	push @argStr, "ArgumentType = Input;  Name = RcvrPosCBF;        ArgumentName = RcvrPosCBF";
	push @argStr, "ArgumentType = Input;  Name = RcvrAttitude;      ArgumentName = RcvrAttitude";
	
	return @argStr; 
}


sub Perl_TransmitterModel_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @origArray = @_;

	# $origArray[0] is the calling mode

	# next argument is Time and is a value. Values are simply scalars in Perl so the assignment is easy:	
	my $time = $origArray[1];

	# next argument is Date and is a String. Strings are simply scalars in Perl so the assignment is easy:	
	my $date = $origArray[2];

	# next argument is CbName and is a String. 	
	my $CbName = $origArray[3];
	
	# next argument is named XmtrPosCBF and is type (CartVec3) Array and is double-3. 
		
	my @XmtrPosCBF = @{$origArray[4]};
		
	# next argument is named XmtrAttitude and is type (Quaternion) Array and is double-4. 
		
	my @XmtrAttitude = @{$origArray[5]};
	
	# next argument is named RcvrPosCBF and is type (CartVec3) Array and is double-3.
	
        my @RcvrPosCBF = @{$origArray[6]};
	
	# next argument is named RcvrAttitude and is type (Quaternion) Array and is double-4. 
	
	my @RcvrAttitude = @{$origArray[7]};
	
	if($Perl_TransmitterModel_compute_init < 0)
	{
		$Perl_TransmitterModel_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_TransmitterModel_Inputs = $g_PluginArrayInterfaceHash{'Perl_TransmitterModel'}{'Inputs'};
		$Perl_TransmitterModel_Outputs = $g_PluginArrayInterfaceHash{'Perl_TransmitterModel'}{'Outputs'};

		%Perl_TransmitterModel_Outputs_ArgHash = $Perl_TransmitterModel_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_TransmitterModel_showArgs();
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered
	
	#############################################################################################
	# USER TRANSMITTER MODEL AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR TRANSMITTER MODEL
	#############################################################################################

	# compute the Test Model : Transmitter Model
	

	$fromX = @XmtrPosCBF[0];
	$fromY = @XmtrPosCBF[1];
	$fromZ = @XmtrPosCBF[2];
	
	$toX = @RcvrPosCBF[0];
	$toY = @RcvrPosCBF[1];
	$toZ = @RcvrPosCBF[2];

	$rX = $fromX - $toX;
	$rY = $fromY - $toY;
	$rZ = $fromZ - $toZ;
	
	$xmA = @XmtrAttitude[0];
	$xmB = @XmtrAttitude[1];
	$xmC = @XmtrAttitude[2];
	$xmD = @XmtrAttitude[3];
	
	$rcA = @RcvrAttitude[0];
	$rcB = @RcvrAttitude[1];
	$rcC = @RcvrAttitude[2];
	$rcD = @RcvrAttitude[3];
	
	$range = sqrt ($rX * $rX + $rY * $rY + $rZ * $rZ);

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  12.3e9;
	$returnArray[1] =  35;
	$returnArray[2] =  30;
	$returnArray[3] =  12.0e6;
	$returnArray[4] =  25.0e6;
	$returnArray[5] =  "BPSK";
	$returnArray[6] =  -2.3;
	$returnArray[7] =  0;
	$returnArray[8] =  0;
	$returnArray[9] =  0.0;
	$returnArray[10] = 1.0;
	$returnArray[11] = 0;
	$returnArray[12] = 0.0;
	
	#############################################################################################
	# END OF USER MODEL AREA	
	#############################################################################################

	return @returnArray;
}


sub Perl_TransmitterModel_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_TransmitterModel_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_TransmitterModel_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_TransmitterModel_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_TransmitterModel_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_TransmitterModel_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_TransmitterModel_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_TransmitterModel_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
##########################################################################################
# SAMPLE FOR PERL BASED RADAR SAR NIIRS CONSTRAINT PLUGIN SCRIPT
# SCRIPT PROVIDED BY THE USER.
# PLEASE ADD YOUR CONSTRAINT CODE IN THE USER RADAR SEARCH/TRACK CONSTRAINT PLUGIN AREA BELOW.
# DO NOT CHANGE ANYTHING ABOVE OR BELOW THAT AREA IN THE SCRIPT
##########################################################################################

# Perl_RadarSARNIIRSConstraint

use STKUtil;

# declare some global variables

my $Perl_RadarSARNIIRSConstraint_compute_init = -1;
my $Perl_RadarSARNIIRSConstraint_Inputs, $Perl_RadarSARNIIRSConstraint_Outputs;
my %Perl_RadarSARNIIRSConstraint_Outputs_ArgHash;

sub Perl_RadarSARNIIRSConstraint
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_RadarSARNIIRSConstraint_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_RadarSARNIIRSConstraint_compute_init = -1;

		@retVal = Perl_RadarSARNIIRSConstraint_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_RadarSARNIIRSConstraint_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_RadarSARNIIRSConstraint_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = RadarSARPluginConstraintValue;    ArgumentName = RadarSARPluginConstraintValue";

	push @argStr, "ArgumentType = Input;  Name = DateUTC;                  ArgumentName = DateUTC";
	push @argStr, "ArgumentType = Input;  Name = EpochSec;                 ArgumentName = EpochSec";
	push @argStr, "ArgumentType = Input;  Name = CbName;                   ArgumentName = CbName";

	push @argStr, "ArgumentType = Input;  Name = RadarPath;                ArgumentName = RadarPath";
	push @argStr, "ArgumentType = Input;  Name = TargetPath;               ArgumentName = TargetPath";
	push @argStr, "ArgumentType = Input;  Name = RadarTransmitPosCBF;      ArgumentName = RadarTransmitPosCBF";
	push @argStr, "ArgumentType = Input;  Name = RadarTransmitAttitudeQuat;             ArgumentName = RadarTransmitAttitudeQuat";
	push @argStr, "ArgumentType = Input;  Name = RadarReceivePosCBF;                    ArgumentName = RadarReceivePosCBF";
	push @argStr, "ArgumentType = Input;  Name = RadarReceiveAttitudeQuat;              ArgumentName = RadarReceiveAttitudeQuat";
	push @argStr, "ArgumentType = Input;  Name = TargetPosCBF;                    	    ArgumentName = TargetPosCBF";
	push @argStr, "ArgumentType = Input;  Name = TargetAttitudeQuat;                    ArgumentName = TargetAttitudeQuat";


	push @argStr, "ArgumentType = Input;  Name = RadarTransmitterToTargetVecBF;        ArgumentName = RadarTransmitterToTargetVecBF";
	push @argStr, "ArgumentType = Input;  Name = RadarReceiverToTargetVecBF;           ArgumentName = RadarReceiverToTargetVecBF";
	push @argStr, "ArgumentType = Input;  Name = TargetToRadarReceiverVecBF;           ArgumentName = TargetToRadarReceiverVecBF";
	push @argStr, "ArgumentType = Input;  Name = RadarTransmitterToTargetRange;        ArgumentName = RadarTransmitterToTargetRange";
	push @argStr, "ArgumentType = Input;  Name = RadarReceiverToTargetRange;           ArgumentName = RadarReceiverToTargetRange";

	push @argStr, "ArgumentType = Input;  Name = AngleRate;                ArgumentName = AngleRate";
	push @argStr, "ArgumentType = Input;  Name = ConeAngle;                ArgumentName = ConeAngle";
	push @argStr, "ArgumentType = Input;  Name = TransmitPropagationTime;  ArgumentName = TransmitPropagationTime";
	push @argStr, "ArgumentType = Input;  Name = ReceivePropagationTime;   ArgumentName = ReceivePropagationTime";
	push @argStr, "ArgumentType = Input;  Name = TransmitRangeRate;        ArgumentName = TransmitRangeRate";
	push @argStr, "ArgumentType = Input;  Name = ReceiveRangeRate;         ArgumentName = ReceiveRangeRate";
	push @argStr, "ArgumentType = Input;  Name = RadarSpeed;               ArgumentName = RadarSpeed";
	push @argStr, "ArgumentType = Input;  Name = RefractedElevationAngle;  ArgumentName = RefractedElevationAngle";
	push @argStr, "ArgumentType = Input;  Name = RadarTransmitFrequency;   ArgumentName = RadarTransmitFrequency";
	push @argStr, "ArgumentType = Input;  Name = DopplerShiftedFrequencyAtTarget;          ArgumentName = DopplerShiftedFrequencyAtTarget";
	push @argStr, "ArgumentType = Input;  Name = DopplerShiftedFrequencyAtRadarReceiver;   ArgumentName = DopplerShiftedFrequencyAtRadarReceiver";

	push @argStr, "ArgumentType = Input;  Name = SARTimeAzimuthResolution; ArgumentName = SARTimeAzimuthResolution";
	push @argStr, "ArgumentType = Input;  Name = SARMaxSceneWidth;         ArgumentName = SARMaxSceneWidth";
	push @argStr, "ArgumentType = Input;  Name = SARIntegrationTime;       ArgumentName = SARIntegrationTime";
	push @argStr, "ArgumentType = Input;  Name = SARAzimuthResolution;     ArgumentName = SARAzimuthResolution";
	push @argStr, "ArgumentType = Input;  Name = SARRangeResolution;       ArgumentName = SARRangeResolution";
	push @argStr, "ArgumentType = Input;  Name = SARAreaRate;              ArgumentName = SARAreaRate";
	push @argStr, "ArgumentType = Input;  Name = SARSNR;	               ArgumentName = SARSNR";
	push @argStr, "ArgumentType = Input;  Name = SARSCR;                   ArgumentName = SARSCR";
	push @argStr, "ArgumentType = Input;  Name = SARCNR;                   ArgumentName = SARCNR";
	push @argStr, "ArgumentType = Input;  Name = SARPTCR;                  ArgumentName = SARPTCR";
	
	push @argStr, "ArgumentType = Input; Name = SAREffectiveNoiseBackScatter; ArgumentName = SAREffectiveNoiseBackScatter";
	push @argStr, "ArgumentType = Input; Name = SARNoiseBandwidth; ArgumentName = SARNoiseBandwidth";
	push @argStr, "ArgumentType = Input; Name = SARNoisePower; ArgumentName = SARNoisePower";
		
	push @argStr, "ArgumentType = Input; Name = SARSNRUnderJamming; ArgumentName = SARSNRUnderJamming";
	push @argStr, "ArgumentType = Input; Name = SARSCRUnderJamming; ArgumentName = SARSCRUnderJamming";
	push @argStr, "ArgumentType = Input; Name = SARCNRUnderJamming; ArgumentName = SARCNRUnderJamming";
	push @argStr, "ArgumentType = Input; Name = SARJOverS; ArgumentName = SARJOverS";
	push @argStr, "ArgumentType = Input; Name = SARJammingPower; ArgumentName = SARJammingPower";


	return @argStr; 
}

sub Perl_RadarSARNIIRSConstraint_compute
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

	# next argument is RadarPath and is a String.
 	
	my $RadarPath = $origArray[4];

	# next argument is TargetPath and is a String.
 	
	my $TargetPath = $origArray[5];

	# next argument is named RadarTransmitPosCBF and is type (CartVec3) Array and is double-3. 

	my @RadarTransmitPosCBF = @{$origArray[6]};

	# next argument is named RadarTransmitAttitudeQuat and is type (Quaternion) Array and is double-4. 

	my @RadarTransmitAttitudeQuat = @{$origArray[7]};

	# next argument is named RadarReceivePosCBF and is type (CartVec3) Array and is double-3. 

	my @RadarReceivePosCBF = @{$origArray[8]};

	# next argument is named RadarReceiveAttitudeQuat and is type (Quaternion) Array and is double-4. 

	my @RadarReceiveAttitudeQuat = @{$origArray[9]};
	
	# next argument is named TargetPosCBF and is type (CartVec3) Array and is double-3. 
	
	my @TargetPosCBF = @{$origArray[10]};
	
	# next argument is named TargetAttitudeQuat and is type (Quaternion) Array and is double-4. 
	
	my @TargetAttitudeQuat = @{$origArray[11]};
	
	
	
	# next argument is named RadarTransmitterToTargetVecBF and is type (CartVec3) Array and is double-3. 
		
	my @RadarTransmitterToTargetVecBF = @{$origArray[12]};
	
	# next argument is named RadarReceiverToTargetVecBF and is type (CartVec3) Array and is double-3. 
		
	my @RadarReceiverToTargetVecBF = @{$origArray[13]};
	
	# next argument is named TargetToRadarReceiverVecBF and is type (CartVec3) Array and is double-3. 
		
	my @TargetToRadarReceiverVecBF = @{$origArray[14]};

	# next argument is RadarTransmitterToTargetRange and is a double. The value units are meters.

	my $RadarTransmitterToTargetRange = $origArray[15];

	# next argument is RadarReceiverToTargetRange and is a double. The value units are meters.

	my $RadarReceiverToTargetRange = $origArray[16];
	

	# next argument is AngleRate and is a double. The value units are Radians per second.

	my $AngleRate = $origArray[17];

	# next argument is ConeAngle and is a double. The value units are Radians.

	my $ConeAngle = $origArray[18];

	# next argument is TransmitPropagationTime and is a double. The value units are seconds.

	my $TransmitPropagationTime = $origArray[19];
	
	# next argument is ReceivePropagationTime and is a double. The value units are seconds.
	
	my $ReceivePropagationTime = $origArray[20];
	
	# next argument is TransmitRangeRate and is a double. The value is in meters per second.
	
	my $TransmitRangeRate = $origArray[21];
	
	# next argument is ReceiveRangeRate and is a double. The value is in meters per second.
	
	my $ReceiveRangeRate = $origArray[22];

	# next argument is RadarSpeed and is a double. The value is in meters per second.

	my $RadarSpeed = $origArray[23];

	# next argument is RefractedElevationAngle and is a double. The value units are radians.

	my $RefractedElevationAngle = $origArray[24];

	# next argument is RadarTransmitFrequency and is a double. The value units are Hz.

	my $RadarTransmitFrequency = $origArray[25];

	# next argument is DopplerShiftedFrequencyAtTarget and is a double. The value units are Hz.

	my $DopplerShiftedFrequencyAtTarget = $origArray[26];

	# next argument is DopplerShiftedFrequencyAtRadarReceiver and is a double. The value units are Hz.

	my $DopplerShiftedFrequencyAtRadarReceiver = $origArray[27];
	
	

	# next argument is SARTimeAzimuthResolution and is a double. The value units are m-sec.

	my $SARTimeAzimuthResolution = $origArray[28];

	# next argument is SARMaxSceneWidth and is a double. The value units are meters.

	my $SARMaxSceneWidth = $origArray[29];

	# next argument is SARIntegrationTime and is a double. The value is in seconds.

	my $SARIntegrationTime = $origArray[30];

	# next argument is SARAzimuthResolution and is a double. The value is in meters.

	my $SARAzimuthResolution = $origArray[31];

	# next argument is SARRangeResolution and is a double. The value is in meters.

	my $SARRangeResolution = $origArray[32];

	# next argument is SARAreaRate and is a double. The value is in meters^2 per second.

	my $SARAreaRate = $origArray[33];
	
	# next argument is SARSNR and is a double. The value is in dB.

	my $SARSNR = $origArray[34];
	
	# next argument is SARSCR and is a double. The value is in dB.
	
	my $SARSCR = $origArray[35];
		
	# next argument is SARCNR and is a double. The value is in dB.
	
	my $SARCNR = $origArray[36];
	
	# next argument is SARPTCR and is a double. The value is in dB.
		
	my $SARPTCR = $origArray[37];



	# next argument is SAREffectiveNoiseBackScatter and is a double. The value is in dBW.
	
	my $SAREffectiveNoiseBackScatter = $origArray[38];
	
	
	
	# next argument is SARNoiseBandwidth and is a double. The value units are in Hz.
		
	my $SARNoiseBandwidth = $origArray[39];
		
	# next argument is SARNoisePower and is a double. The value units are in dBW.
			
	my $SARNoisePower = $origArray[40];
	
	
	
	# next argument is SARSNRUnderJamming and is a double. The value units are in dB.
			
	my $SARSNRUnderJamming = $origArray[41];
			
	# next argument is SARSCRUnderJamming and is a double. The value units are in dB.
				
	my $SARSCRUnderJamming = $origArray[42];
			
	# next argument is SARCNRUnderJamming and is a double. The value units are in dB.
				
	my $SARCNRUnderJamming = $origArray[43];
				
	
				
	# next argument is SARJOverS and is a double. The values are in dB.
					
	my $SARJOverS = $origArray[44];
	
	# next argument is SARJammingPower and is a double. The values are in dBW.
				
	my $SARJammingPower = $origArray[45];
			


	if($Perl_RadarSARNIIRSConstraint_compute_init < 0)
	{
		$Perl_RadarSARNIIRSConstraint_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_RadarSARNIIRSConstraint_Inputs = $g_PluginArrayInterfaceHash{'Perl_RadarSARNIIRSConstraint'}{'Inputs'};
		$Perl_RadarSARNIIRSConstraint_Outputs = $g_PluginArrayInterfaceHash{'Perl_RadarSARNIIRSConstraint'}{'Outputs'};

		%Perl_RadarSARNIIRSConstraint_Outputs_ArgHash = $Perl_RadarSARNIIRSConstraint_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_RadarSARNIIRSConstraint_showArgs();
	}


	#############################################################################################
	# NOTE: the outputs that are returned MUST be in the same order as registered

	# Constraint Value , is a value returned by the script to STK Access at this time instant.
	# STK applies the MIN & MAX constrint criteria and the Exclude Interval flag (if checked)
	# to this value to determine access availability. 
	
	#############################################################################################
	# USER RADAR SAR NIIRS CONSTRAINT PLUGIN AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR CONSTRAINT CRITERION
	# Please do NOT change anything above this area
	#############################################################################################

	# compute the Test CONSTRAINT VALUE : RadarSARNIIRS Constraint plugin Script example

	# range & DopplerShift computed for testing only.  These are not used by the model.
	
	my $time = $EpochSec;
	
	my $ i, j;
	
	$fromX = @RadarXmtrPosCBF[0];
	$fromY = @RadarXmtrPosCBF[1];
	$fromZ = @RadarXmtrPosCBF[2];
	
	$toX = @TargetPosCBF[0];
	$toY = @TargetPosCBF[1];
	$toZ = @TargetPosCBF[2];
	
	$rdrXmTgtX = @RadarTransmitterToTargetVecBF[0];
	$rdrXmTgtY = @RadarTransmitterToTargetVecBF[1];
	$rdrXmTgtZ = @RadarTransmitterToTargetVecBF[2];
	
	$FreqAtTarget = $DopplerShiftedFrequencyAtTarget;
	$RdrFreq = $RadarTransmitFrequency;
	
	$rdrXmTgtRange = $RadarTransmitterToTargetRange;

	$rX = $fromX - $toX;
	$rY = $fromY - $toY;
	$rZ = $fromZ - $toZ;

	$range = sqrt ($rX * $rX + $rY * $rY + $rZ * $rZ) / 1000.0;
	$DopplerShift = $FreqAtTarget - $RdrFreq;
	
	$m2In = 39.37007874015876;
	$mic2m = 10**(-6);
	# $lamda = 299792458.0 / $FreqAtTarget;
	$lamda = 0.65;
	$D = 10;
	$Q = 2;
	$sinAngle = $rdrXmTgtZ / $rdrXmTgtRange;
	$GSD = $rdrXmTgtRange * $lamda * $mic2m / ($Q * $D * sqrt($sinAngle));
	$a = 3.32;
	$b = 1.559;
	$RER = 0.9;
	$Hgm = 1.0;
	$G = 1.0;
	$dlog10 = log(10.0);
		
	$SARSNRLin = 10**($SARSNR/10.0);
		
	$NIIRS = 10.251 - $a * log($GSD * $m2In)/$dlog10 + $b * log($RER)/$dlog10 - 0.656 * $Hgm - (0.344 * $G / $SARSNRLin);

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $NIIRS;
	
	#############################################################################################
	# END OF USER CONSTRAINT MODEL AREA	
	# Please do NOT change anything below this area
	#############################################################################################

	return @returnArray;
}

sub Perl_RadarSARNIIRSConstraint_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_RadarSARNIIRSConstraint_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_RadarSARNIIRSConstraint_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_RadarSARNIIRSConstraint_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_RadarSARNIIRSConstraint_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_RadarSARNIIRSConstraint_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_RadarSARNIIRSConstraint_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_RadarSARNIIRSConstraint_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
##########################################################################################
# SAPMLE FOR PERL BASED RADAR SEARCH/TRACK CONSTRAINT PLUGIN SCRIPT
# SCRIPT PROVIDED BY THE USER.
# PLEASE ADD YOUR CONSTRAINT CODE IN THE USER RADAR SEARCH/TRACK CONSTRAINT PLUGIN AREA BELOW.
# DO NOT CHANGE ANYTHING ABOVE OR BELOW THAT AREA IN THE SCRIPT
##########################################################################################

# Perl_RadarSrchTrkConstraint

use STKUtil;

# declare some global variables

my $Perl_RadarSrchTrkConstraint_compute_init = -1;
my $Perl_RadarSrchTrkConstraint_Inputs, $Perl_RadarSrchTrkConstraint_Outputs;
my %Perl_RadarSrchTrkConstraint_Outputs_ArgHash;

sub Perl_RadarSrchTrkConstraint
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @inputData = @{$_[0]};
 
	my @retVal;

	if ( !defined($inputData[0]) )
	{
		# do compute

		@retVal = Perl_RadarSrchTrkConstraint_compute(@inputData);

	}
	elsif ( $inputData[0] eq 'register' )
	{
		$Perl_RadarSrchTrkConstraint_compute_init = -1;

		@retVal = Perl_RadarSrchTrkConstraint_register();

      }
	elsif ( $inputData[0] eq 'compute' )
	{
		@retVal = Perl_RadarSrchTrkConstraint_compute(@inputData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_RadarSrchTrkConstraint_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = RadarSearchTrackPluginConstraintValue;    ArgumentName = RadarSearchTrackPluginConstraintValue";

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

	push @argStr, "ArgumentType = Input;  Name = ST_SinglePulseSNR;        ArgumentName = ST_SinglePulseSNR";
	push @argStr, "ArgumentType = Input;  Name = ST_IntegratedSNR;         ArgumentName = ST_IntegratedSNR";
	push @argStr, "ArgumentType = Input;  Name = ST_NumIntegratedPulses;   ArgumentName = ST_NumIntegratedPulses";
	push @argStr, "ArgumentType = Input;  Name = ST_IntegrationTime;       ArgumentName = ST_IntegrationTime";
	push @argStr, "ArgumentType = Input;  Name = ST_DwellTime;             ArgumentName = ST_DwellTime";
	push @argStr, "ArgumentType = Input;  Name = ST_TargetRange;           ArgumentName = ST_TargetRange";
	push @argStr, "ArgumentType = Input;  Name = ST_TargetVelocity;        ArgumentName = ST_TargetVelocity";
	push @argStr, "ArgumentType = Input;  Name = ST_TargetMLCVelocity;     ArgumentName = ST_TargetMLCVelocity";
	push @argStr, "ArgumentType = Input;  Name = ST_TargetInClearDopplerZone;   ArgumentName = ST_TargetInClearDopplerZone";
	push @argStr, "ArgumentType = Input;  Name = ST_TargetInMLCFilter;          ArgumentName = ST_TargetInMLCFilter";
	push @argStr, "ArgumentType = Input;  Name = ST_TargetInSLCFilter;          ArgumentName = ST_TargetInSLCFilter";
	
	push @argStr, "ArgumentType = Input; Name = ST_UnambigRangeFlag; ArgumentName = ST_UnambigRangeFlag";
	push @argStr, "ArgumentType = Input; Name = ST_UnambigVelFlag; ArgumentName = ST_UnambigVelFlag";
		
	push @argStr, "ArgumentType = Input; Name = ST_NoiseBandwidth; ArgumentName = ST_NoiseBandwidth";
	push @argStr, "ArgumentType = Input; Name = ST_NoisePower; ArgumentName = ST_NoisePower";
	
	push @argStr, "ArgumentType = Input; Name = ST_SinglePulseProbDetection; ArgumentName = ST_SinglePulseProbDetection";
	push @argStr, "ArgumentType = Input; Name = ST_IntegratedProbDetection; ArgumentName = ST_IntegratedProbDetection";
	push @argStr, "ArgumentType = Input; Name = ST_NonCFARDetectThreshold; ArgumentName = ST_NonCFARDetectThreshold";
	push @argStr, "ArgumentType = Input; Name = ST_CFARThresholdMultiplier; ArgumentName = ST_CFARThresholdMultiplier";
		
	push @argStr, "ArgumentType = Input; Name = ST_SinglePulseSNRUnderJamming; ArgumentName = ST_SinglePulseSNRUnderJamming";
	push @argStr, "ArgumentType = Input; Name = ST_IntegratedSNRUnderJamming; ArgumentName = ST_IntegratedSNRUnderJamming";
	push @argStr, "ArgumentType = Input; Name = ST_SinglePulseProbDetectionUnderJamming; ArgumentName = ST_SinglePulseProbDetectionUnderJamming";
	push @argStr, "ArgumentType = Input; Name = ST_IntegratedProbDetectionUnderJamming; ArgumentName = ST_IntegratedProbDetectionUnderJamming";
	push @argStr, "ArgumentType = Input; Name = ST_IntegrationTimeUnderJamming; ArgumentName = ST_IntegrationTimeUnderJamming";
	push @argStr, "ArgumentType = Input; Name = ST_NumIntegratedPulsesUnderJamming; ArgumentName = ST_NumIntegratedPulsesUnderJamming";
	push @argStr, "ArgumentType = Input; Name = ST_DwellTimeUnderJamming; ArgumentName = ST_DwellTimeUnderJamming";
	push @argStr, "ArgumentType = Input; Name = ST_JOverS; ArgumentName = ST_JOverS";
	push @argStr,"ArgumentType = Input; Name = ST_IntegratedJOverS; ArgumentName = ST_IntegratedJOverS";
	push @argStr, "ArgumentType = Input; Name = ST_JammingPower; ArgumentName = ST_JammingPower";


	return @argStr; 
}

sub Perl_RadarSrchTrkConstraint_compute
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
	
	

	# next argument is ST_SinglePulseSNR and is a double. The value units are dB.

	my $ST_SinglePulseSNR = $origArray[28];

	# next argument is ST_IntegratedSNR and is a double. The value units are dB.

	my $ST_IntegratedSNR = $origArray[29];

	# next argument is ST_NumIntegratedPulses and is a double. The value is unitless.

	my $ST_NumIntegratedPulses = $origArray[30];

	# next argument is ST_IntegrationTime and is a double. The value is in seconds.

	my $ST_IntegrationTime = $origArray[31];

	# next argument is ST_DwellTime and is a double. The value is in seconds.

	my $ST_DwellTime = $origArray[32];

	# next argument is ST_TargetRange and is a double. The value is in meters.

	my $ST_TargetRange = $origArray[33];

	# next argument is ST_TargetVelocity and is a double. The value is in meters per second.

	my $ST_TargetVelocity = $origArray[34];
	
	# next argument is ST_TargetMLCVelocity and is a double. The value is in meters per second.

	my $ST_TargetMLCVelocity = $origArray[35];
	
	# next argument is ST_TargetInClearDopplerZone and is a double.
	
	my $ST_TargetInClearDopplerZone = $origArray[36];
		
	# next argument is ST_TargetInMLCFilter and is a double.
	
	my $ST_TargetInMLCFilter = $origArray[37];
	
	# next argument is ST_TargetInSLCFilter and is a double.
		
	my $ST_TargetInSLCFilter = $origArray[38];



	# next argument is ST_UnambigRangeFlag and is a double. The value is set at 0 or 1.
	
	my $ST_UnambigRangeFlag = $origArray[39];
	
	# next argument is ST_UnambigVelFlag and is a double. The value is set at 0 or 1.
		
	my $ST_UnambigVelFlag = $origArray[40];
	
	# next argument is ST_NoiseBandwidth and is a double. The value units are in Hz.
		
	my $ST_NoiseBandwidth = $origArray[41];
		
	# next argument is ST_NoisePower and is a double. The value units are in dBW.
			
	my $ST_NoisePower = $origArray[42];
	
	
	# next argument is ST_SinglePulseProbDetection and is a double. The value ranges between 0 and 1.
		
	my $ST_SinglePulseProbDetection = $origArray[43];
		
	# next argument is ST_IntegratedProbDetection and is a double. The value ranges between 0 and 1.
			
	my $ST_IntegratedProbDetection = $origArray[44];
		
	# next argument is ST_NonCFARDetectThreshold and is a double. The value units are in dB.
			
	my $ST_NonCFARDetectThreshold = $origArray[45];
			
	# next argument is ST_CFARThresholdMultiplier and is a double. The values are unitless.
				
	my $ST_CFARThresholdMultiplier = $origArray[46];
	
	
	
	# next argument is ST_SinglePulseSNRUnderJamming and is a double. The value units are in dB.
			
	my $ST_SinglePulseSNRUnderJamming = $origArray[47];
			
	# next argument is ST_IntegratedSNRUnderJamming and is a double. The value units are in dB.
				
	my $ST_IntegratedSNRUnderJamming = $origArray[48];
			
	# next argument is ST_SinglePulseProbDetectionUnderJamming and is a double. The value ranges between 0 and 1.
				
	my $ST_SinglePulseProbDetectionUnderJamming = $origArray[49];
				
	# next argument is ST_IntegratedProbDetectionUnderJamming and is a double. The value ranges between 0 and 1.
					
	my $ST_IntegratedProbDetectionUnderJamming = $origArray[50];
	
	
	# next argument is ST_IntegrationTimeUnderJamming and is a double. The value units are in seconds.
			
	my $ST_IntegrationTimeUnderJamming = $origArray[51];
			
	# next argument is ST_NumIntegratedPulsesUnderJamming and is a double. The values are unitless.
				
	my $ST_NumIntegratedPulsesUnderJamming = $origArray[52];
			
	# next argument is ST_DwellTimeUnderJamming and is a double. The value units are in seconds.
				
	my $ST_DwellTimeUnderJamming = $origArray[53];
				
	# next argument is ST_JOverS and is a double. The values are in dB.
					
	my $ST_JOverS = $origArray[54];
	
	# next argument is ST_IntegratedJOverS and is a double. The values are in dB.
			
	my $ST_IntegratedJOverS = $origArray[55];
			
	# next argument is ST_JammingPower and is a double. The values are in dBW.
				
	my $ST_JammingPower = $origArray[56];
			
		




	if($Perl_RadarSrchTrkConstraint_compute_init < 0)
	{
		$Perl_RadarSrchTrkConstraint_compute_init = 1; 

		# The following hashes have been created automatically after this script has registered its inputs and outputs.
		# Each hash contains information about the arguments for this script. The hashes have been created as a
		# user convenience, for those users wanting to know, during the running of the script, what the inputs
		# and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
		# is unneeded and can be removed.

		$Perl_RadarSrchTrkConstraint_Inputs = $g_PluginArrayInterfaceHash{'Perl_RadarSrchTrkConstraint'}{'Inputs'};
		$Perl_RadarSrchTrkConstraint_Outputs = $g_PluginArrayInterfaceHash{'Perl_RadarSrchTrkConstraint'}{'Outputs'};

		%Perl_RadarSrchTrkConstraint_Outputs_ArgHash = $Perl_RadarSrchTrkConstraint_Outputs->getArgumentHash();

		# comment out the line below if you don't want to see the inputs and outputs each time the script is run
		Perl_RadarSrchTrkConstraint_showArgs();
	}


	#############################################################################################
	# NOTE: the outputs that are returned MUST be in the same order as registered

	# Constraint Value , is a value returned by the script to STK Access at this time instant.
	# STK applies the MIN & MAX constrint criteria and the Exclude Interval flag (if checked)
	# to this value to determine access availability. 
	
	#############################################################################################
	# USER RADAR SEARCH/TRACK CONSTRAINT PLUGIN AREA.
	# PLEASE REPLACE THE CODE BELOW WITH YOUR CONSTRAINT CRITERION
	# Please do NOT change anything above this area
	#############################################################################################

	# compute the Test CONSTRAINT VALUE : RadarSrchTrk Constraint plugin Script example

	my $time = $EpochSec;

	my $ i, j;


	# NumberOfFromObjects is total number of from objects and also the length of the From value arrays
	# NumberOfToObjects is total number of to objects and also the length of the To value arrays
	
	$fromX = @RadarXmtrPosCBF[0];
	$fromY = @RadarXmtrPosCBF[1];
	$fromZ = @RadarXmtrPosCBF[2];
	
	$toX = @TargetPosCBF[0];
	$toY = @TargetPosCBF[1];
	$toZ = @TargetPosCBF[2];

	$rX = $fromX - $toX;
	$rY = $fromY - $toY;
	$rZ = $fromZ - $toZ;

	$range = sqrt ($rX * $rX + $rY * $rY + $rZ * $rZ) / 1000.0;

	# this defines the return array
	my @returnArray = ();

	# RETURN YOUR RESULTS BELOW

	$returnArray[0] =  $ST_IntegratedSNR;
	
	#############################################################################################
	# END OF USER CONSTRAINT MODEL AREA	
	# Please do NOT change anything below this area
	#############################################################################################

	return @returnArray;
}

sub Perl_RadarSrchTrkConstraint_showArgs
{
	my @argStrArray;

	STKUtil::printOut "Doing Perl_RadarSrchTrkConstraint_compute_init\n";

	@argStrArray = ();

	push @argStrArray, $Perl_RadarSrchTrkConstraint_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

	# the first arg on input is the calling mode

	push @argStrArray, "0 : this is the calling mode\n";

	my @args = $Perl_RadarSrchTrkConstraint_Inputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_RadarSrchTrkConstraint_Inputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

	@argStrArray = ();

	push @argStrArray, $Perl_RadarSrchTrkConstraint_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

	my @args = $Perl_RadarSrchTrkConstraint_Outputs->getArgumentArray();

	# to see description args

	my $index, $descrip;

	foreach $arg (@args)
	{
		($index, $descrip) = $Perl_RadarSrchTrkConstraint_Outputs->getArgument($arg);

		push @argStrArray, "$index : $arg = $descrip\n";
	}

	STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
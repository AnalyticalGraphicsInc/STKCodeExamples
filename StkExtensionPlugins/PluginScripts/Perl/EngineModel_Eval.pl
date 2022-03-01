# Perl_EngineModel_Eval

use STKUtil qw(getInputArray printOut);

# declare some global variables

my $Perl_EngineModel_Eval_compute_init = -1;

my $Perl_EngineModel_Eval_refEpoch = 0.0;

# NOTE: this script is intended to be used with a script called at SegmentStart
#       which defines the epoch at which the segment begins in $Perl_EngineModel_SegStart_refEpoch

use vars qw($Perl_EngineModel_SegStart_refEpoch);

sub Perl_EngineModel_Eval
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @stateData = STKUtil::getInputArray(@_);
 
	my @retVal;

	if ( !defined($stateData[0]) )
	{
		# do compute

		@retVal = Perl_EngineModel_Eval_compute(@stateData);

	}
	elsif ( $stateData[0] eq 'register' )
	{
		$Perl_EngineModel_Eval_compute_init = -1;

		@retVal = Perl_EngineModel_Eval_register();

      }
	elsif ( $stateData[0] eq 'compute' )
	{
		@retVal = Perl_EngineModel_Eval_compute(@stateData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;}

sub Perl_EngineModel_Eval_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = Status; ArgumentName = status";

	push @argStr, "ArgumentType = Output; Name = Thrust; ArgumentName = thrust";

	push @argStr, "ArgumentType = Output; Name = MassFlowRate; ArgumentName = flowrate";

	push @argStr, "ArgumentType = Input; Name = Epoch; ArgumentName = Epoch";

	push @argStr, "ArgumentType = Input; Type = CalcObject; Name = Inclination; ArgumentName = Incl";

	return @argStr; 
}

sub Perl_EngineModel_Eval_compute
{
	my @origArray = @_;

	# $origArray[0] is the calling mode
	my $epoch = $origArray[1];
	my $incl = $origArray[2];

	if(defined($Perl_EngineModel_SegStart_refEpoch))
	{
		$Perl_EngineModel_Eval_refEpoch = $Perl_EngineModel_SegStart_refEpoch;
	}
	else
	{
		$Perl_EngineModel_Eval_refEpoch = 0.0;
	}

	my @returnArray;

	push @returnArray, "DON'T STOP";

	my $deltaT = $epoch - $Perl_EngineModel_Eval_refEpoch ;
	my $thrust = 0.0003*$deltaT;
	
	push @returnArray, $thrust;		# thrust increase with time!

	my $massflowrate = 0.001;

	push @returnArray, $massflowrate;	# constant flow rate

	return @returnArray;
}

1;
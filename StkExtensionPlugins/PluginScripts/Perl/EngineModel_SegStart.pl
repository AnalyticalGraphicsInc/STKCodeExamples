# Perl_EngineModel_SegStart

use STKUtil qw(getInputArray printOut);

# declare some global variables

use vars qw($Perl_EngineModel_SegStart_refEpoch);

my $Perl_EngineModel_SegStart_compute_init = -1;

sub Perl_EngineModel_SegStart
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @stateData = STKUtil::getInputArray(@_);
 
	my @retVal;

	if ( !defined($stateData[0]) )
	{
		# do compute

		@retVal = Perl_EngineModel_SegStart_compute(@stateData);

	}
	elsif ( $stateData[0] eq 'register' )
	{
		$Perl_EngineModel_SegStart_compute_init = -1;

		@retVal = Perl_EngineModel_SegStart_register();

      }
	elsif ( $stateData[0] eq 'compute' )
	{
		@retVal = Perl_EngineModel_SegStart_compute(@stateData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_EngineModel_SegStart_register
{
	my @argStr;

	push @argStr, "ArgumentType = Input; Name = Epoch; ArgumentName = Epoch";

	return @argStr; 
}

sub Perl_EngineModel_SegStart_compute
{
	my @origArray = @_;

	# $origArray[0] is the calling mode

	$Perl_EngineModel_SegStart_refEpoch = $origArray[1];

	STKUtil::printOut "Ref Epoch = $Perl_EngineModel_SegStart_refEpoch\n";
}

# MUST end with a 1;

1;
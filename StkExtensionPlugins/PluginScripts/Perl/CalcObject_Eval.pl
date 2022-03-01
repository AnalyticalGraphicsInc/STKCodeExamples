# Perl_CalcObject_Eval

use STKUtil qw(getInputArray printOut);

sub Perl_CalcObject_Eval
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @stateData = STKUtil::getInputArray(@_);
 
	my @retVal;

	if ( !defined($stateData[0]) )
	{
		# do compute

		@retVal = Perl_CalcObject_Eval_compute(@stateData);

	}
	elsif ( $stateData[0] eq 'register' )
	{
		@retVal = Perl_CalcObject_Eval_register();
      }
	elsif ( $stateData[0] eq 'compute' )
	{
		@retVal = Perl_CalcObject_Eval_compute(@stateData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_CalcObject_Eval_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = Value; ArgumentName = Value";

	push @argStr, "ArgumentType = Input; Type = CalcObject; Name = Inclination; ArgumentName = Inc";

	push @argStr, "ArgumentType = Input; Type = CalcObject; Name = RAAN; ArgumentName = RightAsc";

	return @argStr; 
}

sub Perl_CalcObject_Eval_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @origArray = @_;

      my @returnArray;

	# $origArray[0] is the calling mode
	my $Inc = $origArray[1];
      my $RightAsc = $origArray[2];

	my $value = sin($Inc)*sin($RightAsc);

	# NOTE: the outputs that are returned MUST be in the same order as registered

	push @returnArray, $value;

      return @returnArray;
}

# MUST end Perl script file with a non-zero integer

1;

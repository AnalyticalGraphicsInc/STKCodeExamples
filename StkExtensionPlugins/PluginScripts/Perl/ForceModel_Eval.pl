# Perl_ForceModel_Eval

# declare some global variables

use STKUtil qw(getInputArray printOut);

my $Perl_ForceModel_Eval_compute_init = -1;

sub Perl_ForceModel_Eval
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @stateData = STKUtil::getInputArray(@_);
 
	my @retVal;

	if ( !defined($stateData[0]) )
	{
		# do compute

		@retVal = Perl_ForceModel_Eval_compute(@stateData);

	}
	elsif ( $stateData[0] eq 'register' )
	{
		$Perl_ForceModel_Eval_compute_init = -1;

		@retVal = Perl_ForceModel_Eval_register();

      }
	elsif ( $stateData[0] eq 'compute' )
	{
		@retVal = Perl_ForceModel_Eval_compute(@stateData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_ForceModel_Eval_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output; Name = Status; ArgumentName = status";

	push @argStr, "ArgumentType = Output; Name = Acceleration; ArgumentName = accel; RefName = CbiLVLH";

	push @argStr, "ArgumentType = Input; Name = Velocity; RefName = Inertial; ArgumentName = Vel";

	push @argStr, "ArgumentType = Input; Name = DateUTC; ArgumentName = Date";

	return @argStr; 
}


sub Perl_ForceModel_Eval_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @inArray = @_;

	# $inArray[0] is the calling mode

	# next argument is named Vel and is a Velocity which is of type Double:3 meaning it is an array of 3 doubles
	# (ie. the x, y, z components of the Velocity vector). Arrays in Perl are passed by reference, so to get the
	# actual array out of the argument $inArray[1], one must de-reference it as an array as shown below

	my @velArray = @{$inArray[1]};
	
	# next argument is Date and is a String. Strings are simply scalars in Perl so the assignment is easy:	
	my $date = $inArray[2];

	if($Perl_ForceModel_Eval_compute_init < 0)
	{
		$Perl_ForceModel_Eval_compute_init = 1; 
	
        STKUtil::printOut " ForceModelEval_compute\n1: @{$inArray[1]}, 2: $inArray[2]";
	}

	# NOTE: the outputs that are returned MUST be in the same order as registered

	# compute the acceleration: here it is a "reverse" drag, being proportional to the inertial speed
	my $factor = 0.000001;
	my $cbiSpeed = sqrt($velArray[0]*$velArray[0]+$velArray[1]*$velArray[1]+$velArray[2]*$velArray[2]);

	# accel with be the acceleration in the CbiLVLH frame
	my @accel;

	push @accel, 0.0;				# x-component: radial
	push @accel, $factor*$cbiSpeed;	# y-component: inTrack (perpendicular to radial, but in-plane)
	push @accel, 0.0;				# z-component: crossTrack (perpendicular to plane created by position, velocity)

	# this defines the return array
	my @returnArray = ();

	$returnArray[0] = "Don't stop";
	$returnArray[1] =  \@accel;

		
	STKUtil::formatOutputArray(\@returnArray);
	return @returnArray;
}

# MUST end Perl script file with a non-zero integer

1;
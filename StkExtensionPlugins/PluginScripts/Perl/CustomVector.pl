# Perl_CustomVector

# declare some global variables

use STKUtil qw (getInputArray printOut);

my $Perl_CustomVector_compute_init = -1;

sub Perl_CustomVector
{
	# the inputs to the the script arise as a reference to an array
	# the STKUtil::getInputArray function is used to get at the array itself

	my @stateData = STKUtil::getInputArray(@_);
 
	my @retVal;

	if ( !defined($stateData[0]) )
	{
		# do compute

		@retVal = Perl_CustomVector_compute(@stateData);

	}
	elsif ( $stateData[0] eq 'register' )
	{
		$Perl_CustomVector_compute_init = -1;

		@retVal = Perl_CustomVector_register();

      }
	elsif ( $stateData[0] eq 'compute' )
	{
		@retVal = Perl_CustomVector_compute(@stateData);
      }
	else
	{
		# error: do nothing
	}

	# MUST return a reference to an array, as shown below

	return \@retVal;
}

sub Perl_CustomVector_register
{
	my @argStr;

	push @argStr, "ArgumentType = Output ; ArgumentName = vec ; Name = Vector";

	push @argStr, "ArgumentType = Input ; ArgumentName = time ; Name = Epoch";

	push @argStr, "ArgumentType = Input ; ArgumentName = apoVec ; Type = Vector ; Name = Apoapsis ; " .
			  "RefName = Body ";

	push @argStr, "ArgumentType = Input ; ArgumentName = bodyAxes ; Type = Axes ; Name = Body ; RefName = TopoCentric ";

	push @argStr, "ArgumentType = Input ; ArgumentName = sunMoonAngle ; Type = Angle ; Name = SunMoon ";

	push @argStr, "ArgumentType = Input ; ArgumentName = moonPnt ; Type = Point ; Name = Center ; " .
			  "Source = CentralBody/Moon ; RefName = Inertial ; RefSource = CentralBody/Sun ";

	push @argStr, "ArgumentType = Input ; ArgumentName = bodySys ; Type = CrdnSystem ; Name = Body ; " .
			  "RefName = Fixed ; RefSource = CentralBody/Earth ";

	return @argStr; 
}

sub Perl_CustomVector_compute
{
	# the inputs here are in the order of the requested Inputs, as registered
	my @inArray = @_;

	# extract inputs

	# $inArray[0] is the calling mode
	my $epoch = $inArray[1];
	my @apo = @{$inArray[2]};
	my @bAxes = @{$inArray[3]};
	my $smAngle = $inArray[4];
	my @mnPnt = @{$inArray[5]};
	my @bSys = @{$inArray[6]};

	if($Perl_CustomVector_compute_init < 0)
	{
		$Perl_CustomVector_compute_init = 1;

		my @infoArray=();

		push @infoArray, "time = $epoch\n";
		push @infoArray, "apoVec = $apo[0], $apo[1], $apo[2]\n";
		push @infoArray, "bodyAxes = $bAxes[0], $bAxes[1], $bAxes[2], $bAxes[3]\n";
		push @infoArray, "sunMoonAngle = $smAngle\n";
		push @infoArray, "moonPnt = $mnPnt[0], $mnPnt[1], $mnPnt[2]\n";
		push @infoArray, "bodySys(vec) = $bSys[0], $bSys[1], $bSys[2]\n";
		push @infoArray, "bodySys(quat) = $bSys[3], $bSys[4], $bSys[5], $bSys[6]\n";

		STKUtil::printOut @infoArray;
	}
	
	# NOTE: the outputs that are returned MUST be in the same order as registered

	# define vector 

 	my @vector =();

	push @vector, $apo[0];		
	push @vector, $apo[1];	
	push @vector, $apo[2];		

	# this defines the return array
	my @returnArray = ();

	$returnArray[0] = \@vector;

	return @returnArray;
}

# MUST end Perl script file with a non-zero integer

1;
# Constraint Plugin component Perl_Constraint
# Author:     Guy Smith
# Company:    Analytical Graphics, Inc.
# Description: Built as an example for the Constraint Script plugin point.
#  This function is called by the AGI Constraint Script Plugin software and is not intended 
#   for general use. This function takes a structure as input that contains
#   a 'method' string parameter that is utilized to determine what the purpose
#   of the call was. Four methods are supported. ( 'compute', 'register', 'GetAccessList',
#   'GetConstraintDisplayName' ) Refer to the Constraint Script Plugin documentation 
#   for an in-depth explaination of the functionality.
#
#  This file provides an example of the capabilities of the Constraint Script Plugin
#   functionality. The simple Constraint that is implemented by this script associates itself
#   with the Facility class and configures itself to return the the Constraint iteration 
#   step Epoch time (in STK Epoch seconds) passed in on every iteration.

# Notes:
# 1)  This source file is loaded into a general script engine namespace. Therefore,
#      any function names and global variables must be named appropriately or other
#      script source files could corrupt the data or call the functions. It is
#      possible to take advantage of the common namespace as a means to communicate
#      between other scripts/plugins, but, you must design the scripts to do this.
#      A good method to avoid namespace collision is to preface the functions and
#      variables with the name of the file, as was done in this example.
# 2)  Using STKUtil::printOut calls in this script is a good way to debug the functionality,
#      but on PC will interupt STK, since the MsgBox must be Acknowledged before control
#      is passed back to the STK process.

# Copyright 2002, Analytical Graphics Incorporated


use STKUtil qw(getInputArray printOut);

my $Perl_Constraint_compute_init = -1;

#==========================================================================
# Perl_Constraint_GetConstraintDisplayName() fctn
#==========================================================================
sub Perl_Constraint_GetConstraintDisplayName
{
	#STKUtil::printOut ( "Perl_Constraint_GetConstraintDisplayName called, Returning 'PerlPluginConstraint'\n");

	my @retArray = ();
	my $strReturnValue = "PerlPluginConstraint";
	push @retArray, $strReturnValue;

	return @retArray;
}

#==========================================================================
# Perl_Constraint_GetAccessList() fctn
#==========================================================================
sub Perl_Constraint_GetAccessList
{
	my $callmode = shift;
	my $strBaseClass = shift;

	my @retArray = ();
	my $strReturnValue = '';

	if(defined($strBaseClass))
	{
		if ($strBaseClass eq "Facility")
		{
			#STKUtil::printOut ( "Perl_Constraint_GetAccessList Facility Identified\n" );

			$strReturnValue = 'Aircraft,AreaTarget,Facility,GroundVehicle,LaunchVehicle,Missile,Planet,Radar,Satellite,Ship,Star,Target';
		}
	
		push @retArray, $strReturnValue;
	}

	return @retArray;
}

#==========================================================================
# Perl_Constraint fctn
#==========================================================================
sub Perl_Constraint 
{
	my @methodData = STKUtil::getInputArray(@_);
		
      my @retVal = ();

	#STKUtil::printOut ( "Perl_Constraint largest index: " . $#methodData . "\n");

	if ( !defined($methodData[0]) ) # No Function name means compute
	{
		@retVal = Perl_Constraint_compute(@methodData);
	}
	elsif ( $methodData[0] eq 'register' )
	{
		$Perl_Constraint_compute_init = -1;

		@retVal = Perl_Constraint_register();
      }
	elsif ( $methodData[0] eq 'GetAccessList' )
	{
		@retVal = Perl_Constraint_GetAccessList(@methodData);
      }
	elsif ( $methodData[0] eq 'GetConstraintDisplayName' )
	{
		@retVal = Perl_Constraint_GetConstraintDisplayName();
      }
	else
	{
		# error: do nothing
	}

	return \@retVal;
}

sub Perl_Constraint_register
{
	my @argStr = ();

	# outputs

	push @argStr, "ArgumentType = Output; Name = Status; ArgumentName = Status";

	push @argStr, "ArgumentType = Output; Name = Result; ArgumentName = Result";

	# inputs

	push @argStr, "ArgumentType = Input; Name = Epoch; ArgumentName = Epoch";

	push @argStr, "ArgumentType = Input; Name = fromPosition; RefName = Fixed; ArgumentName = fromPos";

	push @argStr, "ArgumentType = Input; Name = fromVelocity; RefName = Inertial; ArgumentName = fromVel";

	push @argStr, "ArgumentType = Input; Name = fromQuaternion; RefName = Fixed; ArgumentName = fromQuat";

	push @argStr, "ArgumentType = Input; Name = toPosition; RefName = Fixed; ArgumentName = toPos";

	push @argStr, "ArgumentType = Input; Name = toVelocity; RefName = Inertial; ArgumentName = toVel";

	push @argStr, "ArgumentType = Input; Name = toQuaternion; RefName = Fixed; ArgumentName = toQuat";

	push @argStr, "ArgumentType = Input; Name = fromObjectPath; ArgumentName = fromObj";

	push @argStr, "ArgumentType = Input; Name = toObjectPath; ArgumentName = toObj";

	# get an input from Vector Geometry Tool

	push @argStr, "ArgumentType = Input; ArgumentName = toEarthFromMoonInSunFixed ; Type = Vector; " .
			  "Name = Earth; Source = CentralBody/Moon; RefSource = CentralBody/Sun; " .
			  "RefName = Fixed; Derivative = Yes";

	return @argStr;
}


sub Perl_Constraint_compute
{
      my @stateData = @_;

	# note: argument 0 is the calling mode

	if($Perl_Constraint_compute_init < 0)
	{
		$Perl_Constraint_compute_init = 1;

		my @fromPosition = ();

		@fromPosition = @{$stateData[2]};

		my $strMessage = "Perl_Constraint_compute:\n".
						"    fromPosition X: " . $fromPosition[0] . "\n".
						"    fromPosition Y: " . $fromPosition[1] . "\n".
						"    fromPosition Z: " . $fromPosition[2] . "\n";

		STKUtil::printOut($strMessage);

		$strMessage = "Perl_Constraint_compute:\nepoch: $stateData[1]\n";

		STKUtil::printOut($strMessage);
	}

	# Allocate space for the returned array
	my @returnValue = ();
	
#   Status
#	push @returnValue, " MESSAGE:[alarm] I had an error;  CONTROL: Stop";
#	push @returnValue, " MESSAGE: [Info] Perl Perl_Constraint- Everything is fine;  CONTROL: OK";

	push @returnValue, "Okay";		# Status

	push @returnValue, $stateData[1];	# Result: return current epoch

	return @returnValue;
}

# MUST end Perl script file with a non-zero integer

1;

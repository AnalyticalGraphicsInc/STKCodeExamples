#
# this is the temp file used by STK.
#
$inputfile = shift;

#
# read the file into memory
#
open (IN, "$inputfile");
@data = <IN>;
close IN;

#
# find stkSatDbAll.sd
#
$sd = $ENV{'ProgramData'} . "\\AGI\\STK 10\\Databases\\Satellite\\stkSatDbAll.sd";

#
# open .sd file, create a hash of tle-common names
#
if (-e $sd){
	open (SD, $sd);
	foreach (<SD>){
		$satSD{substr($_,0,5)} = substr($_,5,15);
		}
	close (SD);
	}

else {
	$sd = "";
	}

#
# open the file for writing (this is what STK will display)
#
open (OUTPUT, ">$inputfile");

if ($sd){
	print OUTPUT "SSC numbers have been replaced with common names from\n$sd file\n\n";
	}
else{
	print OUTPUT "Unable to find %ProgramData%\\AGI\\STK 10\\Databases\\Satellite\\stkSatDbAll.sd file.\n\n";
	}

foreach (@data){
	if ($sd){
		s/       Start Time/                 Start Time/;
		s/^-----    -/---------------    -/;
		s/^(\d{5})/$satSD{$1}/;
		}
	print OUTPUT "$_";
	}

#
# close the output file
#
close OUTPUT;

#
# when the script ends, STK will disply whatever is in the $inputfile
#
exit;
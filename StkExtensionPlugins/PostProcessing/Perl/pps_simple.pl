#
# this is the temp file used by STK
#
$inputfile = shift;

#
# open/read/close the file
#
open (INPUT, "$inputfile");
@inData = <INPUT>;
close INPUT;

#
# open the new file (overwite input file) for writing
#
open (OUTPUT, ">$inputfile");
$count = 1;
foreach (@inData){
	# do something with the data
	print OUTPUT $count++ . " " . $_;
	}

#
# close the output file
#
close OUTPUT;

#
# when the script ends, STK will disply whatever is in the $inputfile
#
exit;
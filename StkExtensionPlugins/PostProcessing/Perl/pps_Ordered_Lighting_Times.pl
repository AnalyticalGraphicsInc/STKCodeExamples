#
# this is the temp file used by STK.
#
$inputfile = $ARGV[0];

#
# open/read/close the file
#
open (INPUT, "$inputfile");
@inData = <INPUT>;
close INPUT;

#
# the first line of the report is a time stamp - I want to save this.
#
$timestamp = shift(@inData);
$timestamp =~ s/^\s+//;
chomp ($timestamp);

#
# loop through the data . . . one line at a time
#
$condition = "";
$object = "";
foreach $line (@inData) {
	# remove the 'end of line' (newline)
	chomp ($line);
	
	# if the line is empty, do nothing
	if (! $line){
		next;
		}
	
	# this is to get the Satellite's name
	elsif ($line =~ /Satellite-(.*):  Lighting/) {
		$object = $1;
		next;
		}
	
	# these next three tests pulls out the 'condition'
	elsif ($line =~ /Sunlight Times/){
		$condition = "Sunlight";
		next;
		}
	elsif ($line =~ /Penumbra Times/){
		$condition = "Penumbra";
		next;
		}
	elsif ($line =~ /Umbra Times/){
		$condition = "Umbra   ";
		next;
		}
	
	# split the line on '2 or more' spaces
	@lineData = split (/\s\s+/,$line);
	
	
	# only lines which have '5' elements (max array index of 4) will contain 'real' data 
	if ($#lineData != 4){
		next;
		}
	else {
		# write the useful data to an element of the @outData array - make sure 'epoch sec' is first
		push (@outData, $lineData[1] . "\t" . $lineData[2] . "\t" . $lineData[3] . "\t" . $condition . "\t" . $lineData[4]);
		}	
	}

#
# this puts the data in time order (which is why I included 
# the start time in epoch secs in the custom report style
#
@outData = sort { $a <=> $b } @outData;

#
# open the new file (same as input file) for writing
#
open (OUTPUT, ">$inputfile");

# write all data to OUTPUT
print OUTPUT $timestamp . "\n";
print OUTPUT $object . "\n";
#print OUTPUT "StartTime                     EndTime                       Condition   Duration\n";
#print OUTPUT "--------------------------------------------------------------------------------\n";
printf OUTPUT "\n%-30s%-30s%-20sDuration\n", "StartTime", "EndTime", "Condition" ;
for (1..88){
	print OUTPUT "-";
	}
print OUTPUT "\n";

foreach $line (@outData){
	@lineData = split (/\t/,$line);
	#print OUTPUT $lineData[1] . "\t" . $lineData[2] . "\t" . $lineData[3] . "\t" . $lineData[4] . "\n";
	
	printf OUTPUT "%-30s%-30s%-20s$lineData[4]\n", $lineData[1], $lineData[2], $lineData[3];
	
	
	}

#
# close the output file
#
close OUTPUT;

#
# when the script ends, STK will disply whatever is in the $inputfile
#
exit;
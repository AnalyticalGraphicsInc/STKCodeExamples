use Time::Local;
use Win32;
	
my %month = ('Jan', '0',
	  'Feb', '1',
	  'Mar', '2',
	  'Apr', '3',
	  'May', '4',
	  'Jun', '5',
	  'Jul', '6',		  
	  'Aug', '7',		  
	  'Sep', '8',		  
	  'Oct', '9',
	  'Nov', '10',		  
	  'Dec', '11');

#
# this is the temp file used by STK.
#
$inputfile = shift;

#
# open/read/close the file
#
open (INPUT, "$inputfile");
@inData = <INPUT>;
close INPUT;

my @data;
my $dataEpoch;
my $cur_time;
my $ref_time;
my $alt;

foreach (@inData){
	#
	# remove leading white space
	#
	s/^\s+//;

	#
	# find relevant data: datetime(Gregorian format) lat long alt (integer or decimal values)
	#

	if ($_ =~ /^(\d{0,2}) (\w{3}) (\d{4}) (\d{2}):(\d{2}):(\d{2}\.{0,1}\d*)\s+(\-?\d+\.{0,1}\d*)\s+(\-?\d+\.{0,1}\d*)\s+(\-*\d*\.{0,1}\d*)/){
	#           (  day  ) (month) (year ) ( hh  ) ( mm  ) (      sec      )   (      lat       )   (      lon       )   (      alt       )
	
		if (! $dataEpoch){
			$dataEpoch = $1 . " " . $2 . " " . $3 . " " . $4 . ":" . $5 . ":" . $6;
			$ref_time = timegm (0, $5,$4,$1,$month{$2},$3) + $6;
			}			

		$alt = $9;
		if (! $alt){
			$alt = 0;
			}

		if (($7 <= 90) && ($8 <= 360) && ($7 >= -90) && ($8 >= -360)){ # ensure valid lat/long points
			$cur_time = (timegm (0, $5,$4,$1,$month{$2},$3) + $6) - $ref_time;
			push (@data, "$cur_time \t " . $7 . " \t " . $8 . " \t " . $alt);
			}
		elsif ($#data > -1){
			last;
			}
		}

	elsif ($#data > -1){
		last;
		}
	}

if ($#data > 0){
	#
	# open the new file (same as input file) for writing
	#
	open (OUTPUT, ">$inputfile");
	
	print OUTPUT "stk.v.4.2.1";
	print OUTPUT "\nBEGIN Ephemeris";
	print OUTPUT "\nNumberOfEphemerisPoints " . ($#data+1);
	print OUTPUT "\nInterpolationOrder 5";
	print OUTPUT "\nScenarioEpoch $dataEpoch";
	print OUTPUT "\nEphemerisLLATimePos\n";
	print OUTPUT "\n" . join("\n", @data);
	print OUTPUT "\n\nEND Ephemeris";
	close OUTPUT;
	}

else {
	use Win32;
	Win32::MsgBox("No ephemeris data found. Report not post-processed." ,0, "$0");
	}

exit;

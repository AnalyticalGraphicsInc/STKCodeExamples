use Date::Calc qw( Delta_DHMS check_date check_time);

#
# this is the temp file used by STK.
#
$inputfile = shift;

%month_to_num = (
	  'Jan',  '1', 
	  'Feb',  '2', 
	  'Mar',  '3', 
	  'Apr',  '4', 
	  'May',  '5', 
	  'Jun',  '6', 
	  'Jul',  '7', 
	  'Aug',  '8', 
	  'Sep',  '9', 
	  'Oct', '10',
	  'Nov', '11',
	  'Dec', '12',
	  );

#
# open/read/close the file
#
open (INPUT, "$inputfile");
@inData = <INPUT>;
close INPUT;


#
# open the new file (same as input file) for writing
#
open (OUTPUT, ">$inputfile");

print OUTPUT "stk.v.7.0\n";
print OUTPUT "BEGIN GreatArc\n";
print OUTPUT "Method DetVelFromTime\n";
$time_of_first_waypoint = get_time_of_first_waypoint();
print OUTPUT "TimeOfFirstWaypoint " . $time_of_first_waypoint . "\n";
print OUTPUT "NumberOfWaypoints " . get_num_of_points() . "\n";
print OUTPUT "BEGIN Waypoints\n";

($ed, $em, $ey, $eh, $emin, $es) = split (/[\s+:]/,$time_of_first_waypoint);


foreach (@inData){
	chomp;
	($d, $m, $y, $time, $lat, $lon, $alt) = split (/\s+/);
	($h, $min, $s) = split (/:/,$time);
	
	if ((check_date($y,$month_to_num{$m},$d))&& (check_time($h,$min,$s))){
		($D,$H,$M,$S) = Delta_DHMS($ey,$month_to_num{$em},$ed, $eh,$emin,$es, $y,$month_to_num{$m},$d, $h,$min,$s); 
		$sec = ((((($D * 2 ) * $H) * 60 ) + $M) * 60 ) + ($s - $es);
		print OUTPUT "\n\t$sec \t $lat \t $lon \t $alt \t 0 \t 0";
		}
	}

print OUTPUT "\n\nEND Waypoints\n";
print OUTPUT "END GreatArc\n";

#
# close the output file
#
close OUTPUT;

#
# when the script ends, STK will disply whatever is in the $inputfile
#
exit;

sub get_time_of_first_waypoint {
	my ($time, $count, $d, $m, $y, $h, $min, $s);
	$count = 0;
	while (! $time){
		($d, $m, $y, $h, $min, $s) = split (/[\s:]/,$inData[$count++]);
		if ((check_date($y,$month_to_num{$m},$d))&& (check_time($h,$min,$s))){
			$time = "$d $m $y $h:$min:$s";
			}
		}
	return ($time);
	}

sub get_num_of_points {
	my ($count, $d, $m, $y, $h, $min, $s, $numpoints);
	$count = 0;
	$numpoints = 0;
	foreach (@inData){
		($d, $m, $y, $h, $min, $s) = split (/[\s:]/,$inData[$count++]);
		if ((check_date($y,$month_to_num{$m},$d))&& (check_time($h,$min,$s))){
			$numpoints++;
			}
		}
	return ($numpoints);
	}
##########################################################################################
# SAMPLE STK ANTENNA GAIN PLUGIN TO MODEL BEAM DIRECTION PROVIDER FOR PHASED ARRAY ANTENNA (WRITTEN IN PERL)
# TO MODIFY/REPLACE THE BEAM DIRECTION PROVIDER MODEL, EDIT CODE IN THE -USER GAIN MODEL AREA-
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_BeamDirectionProvider

# declare some global variables

# my $Perl_BeamDirectionProvider_compute_init = -1;
my $Perl_BeamDirectionProvider_Inputs, $Perl_BeamDirectionProvider_Outputs;
my %Perl_BeamDirectionProvider_Outputs_ArgHash;

# Declare some useful constants for conversions used in this particular example...
my $degToRad, $radToDeg, $piOver2;
$piOver2 = 1.5707963268;
$degToRad = 0.01745329252;
$radToDeg = 57.29577951308;

# Declare globals used for this particular example.  Globals will retain their state across each
# time step script call...
my $gScanAzStepSize, $gScanElStepSize, $gScanMinAz, $gScanMaxAz, $gScanMinEl, $gScanMaxEl, $gScanAz, $gScanEl;
$gScanAzStepSize = 4*$degToRad;
$gScanElStepSize = 5*$degToRad;
$gScanMinAz = -30*$degToRad;
$gScanMaxAz = 30*$degToRad;
$gScanMinEl = -20*$degToRad;
$gScanMaxEl = 5*$degToRad;

$gScanAz = $gScanMinAz;
$gScanEl = $gScanMinEl;

sub Perl_BeamDirectionProvider
{
    # the inputs to the the script arise as a reference to an array
    # the STKUtil::getInputArray function is used to get at the array itself

    my @inputData = @{$_[0]};
 
    my @retVal;

    if ( !defined($inputData[0]) )
    {
        # do compute

        @retVal = Perl_BeamDirectionProvider_compute(@inputData);

    }
    elsif ( $inputData[0] eq 'register' )
    {
        $Perl_BeamDirectionProvider_compute_init = -1;

        @retVal = Perl_BeamDirectionProvider_register();

    }
    elsif ( $inputData[0] eq 'compute' )
    {
        @retVal = Perl_BeamDirectionProvider_compute(@inputData);
    }
    else
    {
        # error: do nothing
    }

    # MUST return a reference to an array, as shown below

    return \@retVal;
}

sub Perl_BeamDirectionProvider_register
{
    my @argStr;
    
    #******************************************************************
    #******************************************************************
    #************************Output Parameters*************************
    #******************************************************************
    #******************************************************************

    push @argStr, "ArgumentType = Output; Name = IsDynamic; ArgumentName = IsDynamic";
    push @argStr, "ArgumentType = Output; Name = NumDirections; ArgumentName = NumDirections";
    push @argStr, "ArgumentType = Output; Name = Azimuths; ArgumentName = Azimuths";
    push @argStr, "ArgumentType = Output; Name = Elevations; ArgumentName = Elevations";
    
    #******************************************************************
    #******************************************************************
    #*************************Input Parameters*************************
    #******************************************************************
    #******************************************************************

    push @argStr, "ArgumentType = Input;  Name = ObjectPath; ArgumentName = ObjectPath";
    push @argStr, "ArgumentType = Input;  Name = EpochSec; ArgumentName = EpochSec";
    push @argStr, "ArgumentType = Input;  Name = PosLLA; ArgumentName = PosLLA";
    push @argStr, "ArgumentType = Input;  Name = PosCBF; ArgumentName = PosCBF";
    push @argStr, "ArgumentType = Input;  Name = MemberPositionFormat; ArgumentName = MemberPositionFormat";
    push @argStr, "ArgumentType = Input;  Name = NumberOfMembers; ArgumentName = NumberOfMembers";
    push @argStr, "ArgumentType = Input;  Name = MemberPositions; ArgumentName = MemberPositions";
    push @argStr, "ArgumentType = Input;  Name = MemberFrequencies; ArgumentName = MemberFrequencies";
    push @argStr, "ArgumentType = Input;  Name = MemberPwrs; ArgumentName = MemberPwrs";
    push @argStr, "ArgumentType = Input;  Name = MemberIds; ArgumentName = MemberIds";
    push @argStr, "ArgumentType = Input;  Name = MemberCategories; ArgumentName = MemberCategories";

    return @argStr; 
}

sub Perl_BeamDirectionProvider_compute
{
    # the inputs here are in the order of the requested Inputs, as registered
    my @origArray = @_;

    # $origArray[0] is the calling mode

    # next argument is Object Path and is a String. Strings are simply scalars in Perl so the assignment is easy:    
    my $ObjectPath = $origArray[1];
    
    # next argument is EpochSec (seconds) and is a Double. Doubles are simply scalars in Perl so the assignment is easy:    
    my $EpochSec = $origArray[2];

    # next argument is Position in LLA and is a String.    
    my $PosLLA = $origArray[3];

    # next argument is Position in XYZ and is a String.    
    my $PosCBF = $origArray[4];

    # next argument is Member Position Format and is an integer.
    my $MemberPositionFormat = $origArray[5];
    
    # next argument is Number of Members and is an integer.
    my $NumberOfMembers = $origArray[6];
    
    # next argument is named MemberPositions and is of type Double:3 meaning it is an array of 3 doubles. 
    # Arrays in Perl are passed by reference, so to get the
    # actual array out of the argument $origArray[7], one must de-reference it as an array as shown below
    
    my @MemberPositions = @{$origArray[7]};
    
    # next argument is named MemberFrequencies and is of type Double:100 meaning it is an array of 100 doubles. 
    
    my @MemberFrequencies = @{$origArray[8]};
    
    # next argument is Member Powers (EIRP) and is of type Double:100 meaning it is an array of 100 doubles. 
    
    my @MemberPwrs = @{$origArray[9]};
    
    # next argument is Member IDs and is of type Integer:100 meaning it is an array of 100 integers. 
    
    my @MemberIds = @{$origArray[10]};

    # next argument is Member Categories and is of type Integer:100 meaning it is an array of 100 integers. 
    
    my @MemberCategories = @{$origArray[11]};

    if($Perl_BeamDirectionProvider_compute_init < 0)
    {
        $Perl_BeamDirectionProvider_compute_init = 1; 

        # The following hashes have been created automatically after this script has registered its inputs and outputs.
        # Each hash contains information about the arguments for this script. The hashes have been created as a
        # user convenience, for those users wanting to know, during the running of the script, what the inputs
        # and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
        # is unneeded and can be removed.

        $Perl_BeamDirectionProvider_Inputs = $g_PluginArrayInterfaceHash{'Perl_BeamDirectionProvider'}{'Inputs'};
        $Perl_BeamDirectionProvider_Outputs = $g_PluginArrayInterfaceHash{'Perl_BeamDirectionProvider'}{'Outputs'};

        %Perl_BeamDirectionProvider_Outputs_ArgHash = $Perl_BeamDirectionProvider_Outputs->getArgumentHash();

        # comment out the line below if you don't want to see the inputs and outputs each time the script is run
        #Perl_BeamDirectionProvider_showArgs();
    }

    my $IsDynamic     = 1; 
     my $NumDirections = 0;
        my @Azs;
    my @Els;
    $Azs[99] = 0;  # Force array capacity to be 100 in size.
    $Azs[99] = 0;
    $Els[99] = 0;
    $Els[99] = 0;    

    # NOTE: the outputs that are returned MUST be in the same order as registered
    
    ############################################################################################
    # All input and out parameters have been mapped to variables described below.
    ############################################################################################
    # NOTE: the outputs that are returned MUST be in the same order as registered
    # If IsDynamic is set to 0 (false), this script will only be called once and the same outputs 
    # will be used for every timestep.  Setting IsDynamic to 1 (true), this script will be called 
    # at every timestep.
    #
    # All directions specified as Azimuth and Elevation angles (see STK help) in degrees and 
    # relative to the entity's body coordinate system.
    #
    # Script input variables available to user:
    #    ObjectPath - Path of the object, i.e. objects fully qualified name.   string
    #    EpochSec   - Current simulation epoch seconds.                        double  
    #    PosLLA       - Position the object in LLA.                              string
    #    PosCBF       - Position the object in CBF.                              string
    #    NumberOfMembers - Number of members in view at this time step. Used
    #                         to define size of input field arrays.  Max 100
    #                         WARNING: Always check this field since, for efficency, 
    #                                  STK may provide old data for 
    #                                  other fields and should be considered stale
    #                                  if this field is 0.                            int
    #       MemberPositionFormat - Defines if memberPositions array will be a
    #                              relative position (to antenna) in Theta/Phi/Range 
    #                             (rad/rad/m) or X/Y/Z (m/m/m)                       int  
    #       MemberPositions      - Member positions in format specified by
    #       MemberPositionFormat.                          double(3)
    #       MemberFrequencies   -  Member frequencies (-1 for non-RF members)     double(100)
    #       MemberPwrs          -  Member eirp (-3000dBW for non-emitter members) double(100)
    #       MemberIds           -  Member ids, 0-based as listed in antenna.         int(100)
    #       MemberCategories    -  Member object category (Aircraft, Facility, etc.) int(100)
    #
    # Script outputs which must be filled in by the user:
    #       IsDynamic           - Indicates if script is time-dynamic (see above).   int
    #       NumDirections       - Number of directions being returned                int
    #       Directions          - Az/El in antenna's coordinate system (rad/rad)   double(2)
    #
    #############################################################################################
    # USER PLUGIN BEAM DIRECTION PROVIDER MODEL AREA.
    # PLEASE REPLACE THE CODE BELOW WITH YOUR DIRECTION PROVIDER COMPUTATION MODEL
    #
    # This simple sample demonstrates how to dynamically return beam directions.  This script 
    # defines the antenna's field of regard (FOR) and then scans the FOR.  If any aircraft fly   
    # within the FOR and within effective range it will switch to tracking mode.  It will switch 
    # it's target if another member becomes closer.  If all objects are out of the FOR, it will 
    # switch back to track mode. This is just a simplistic example to demonstrate how to 
    # dynamically return direction.
        
    # Dim temporaries used for this particular example
    my $objAz, $objEl, $objRange, $radarRange, $minAz, $minEl, $minRange;

    # Initialize Output values
    $IsDynamic     = 1; 
    $NumDirections = 1;

    $radarRange = 100000;
    # If any object is in radar range, use track mode determine who to track
    my $minRange = 1e+300;
    for ($i = 0; $i <= $NumberOfMembers - 1; $i++)
    {
        $objAz = $MemberPositions[3*$i];
        $objEl = $MemberPositions[3*$i+1];
        $objRange = $MemberPositions[3*$i+2];
        # Track the closest object within range of the radar
        if ($objRange < $radarRange) 
        {
            if ($objRange < $minRange) 
            {
                # Only target it if it's in front hemisphere
                if (($objAz > -$piOver2) && ($objAz < $piOver2))
                {
                    $minAz = $objAz;
                    $minEl = $objEl;
                    $minRange = $objRange;
                }
            }
        }
    }
    # If nothing is inside radar range, continue scan mode
    if ($minRange == 1e+300) 
    {
        $gScanAz = $gScanAz + $gScanAzStepSize;
        $gScanAz = $gScanAz + $gScanAzStepSize;
        # gBeamEl = gBeamEl + gElStepSize
        if ($EpochSec <= 0.00001) 
        {
            $gScanAz = $gScanMinAz;
            $gScanEl = $gScanMinEl;
        }
    
        $Azs[0] = $gScanAz;
        $Els[0] = $gScanEl;

        # Check for end of scan pattern and reset to begin scan pattern
        if ($gScanAz > $gScanMaxAz)
        {
            $gScanEl = $gScanEl + $gScanElStepSize;
            $gScanAz = $gScanMinAz;
        }

        if ($gScanEl > $gScanMaxEl) 
        {
            $gScanElStepSize = $gScanElStepSize;
            $gScanEl = $gScanMinEl;
        }
    }
    else
    {
        $Azs[0] = $minAz;
        $Els[0] = $minEl;
                
    }

    #############################################################################################
    # END OF USER MODEL AREA    
    #############################################################################################
    
    # this defines the return array    
    my @returnArray = ();

    # RETURN YOUR RESULTS BELOW
    $returnArray[0] =  $IsDynamic;
    $returnArray[1] =  $NumDirections;
    $returnArray[2] =  \@Azs;
    $returnArray[3] =  \@Els;
    
    STKUtil::formatOutputArray(\@returnArray);
    return @returnArray;
}

sub Perl_BeamDirectionProvider_showArgs
{
    my @argStrArray;

    STKUtil::printOut "Doing Perl_BeamDirectionProvider_compute_init\n";

    @argStrArray = ();

    push @argStrArray, $Perl_BeamDirectionProvider_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

    # the first arg on input is the calling mode

    push @argStrArray, "0 : this is the calling mode\n";

    my @args = $Perl_BeamDirectionProvider_Inputs->getArgumentArray();

    # to see description args

    my $index, $descrip;

    foreach $arg (@args)
    {
        ($index, $descrip) = $Perl_BeamDirectionProvider_Inputs->getArgument($arg);

        push @argStrArray, "$index : $arg = $descrip\n";
    }

    STKUtil::printOut @argStrArray;

    @argStrArray = ();

    push @argStrArray, $Perl_BeamDirectionProvider_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

    my @args = $Perl_BeamDirectionProvider_Outputs->getArgumentArray();

    # to see description args

    my $index, $descrip;

    foreach $arg (@args)
    {
        ($index, $descrip) = $Perl_BeamDirectionProvider_Outputs->getArgument($arg);

        push @argStrArray, "$index : $arg = $descrip\n";
    }

    STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;

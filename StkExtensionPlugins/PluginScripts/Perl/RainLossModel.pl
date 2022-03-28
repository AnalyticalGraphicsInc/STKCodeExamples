##########################################################################################
# SAMPLE FOR PERL BASED RAIN LOSS PLUGIN SCRIPT PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER  MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_RainLossModel

# declare some global variables

my $Perl_RainLossModel_compute_init = -1;
my $Perl_RainLossModel_Inputs, $Perl_RainLossModel_Outputs;
my %Perl_RainLossModel_Outputs_ArgHash;

sub Perl_RainLossModel
{
    # The inputs to the the script arise as a reference to an array
    # the STKUtil::getInputArray function is used to get at the array itself

    my @inputData = @{$_[0]};

    my @retVal;

    if ( !defined($inputData[0]) )
    {
        # do compute
        @retVal = Perl_RainLossModel_compute(@inputData);
    }
    elsif ( $inputData[0] eq 'register' )
    {
        $Perl_RainLossModel_compute_init = -1;
        @retVal = Perl_RainLossModel_register();
    }
    elsif ( $inputData[0] eq 'compute' )
    {
        @retVal = Perl_RainLossModel_compute(@inputData);
    }
    else
    {
        # error: do nothing
    }

    # MUST return a reference to an array, as shown below

    return \@retVal;
}

sub Perl_RainLossModel_register
{
    my @argStr;

    push @argStr, "ArgumentType = Output; Name = RainLoss;     ArgumentName = RainLoss";

    push @argStr, "ArgumentType = Input;  Name = DateUTC;             ArgumentName = DateUTC";
    push @argStr, "ArgumentType = Input;  Name = Frequency;           ArgumentName = Frequency";
    push @argStr, "ArgumentType = Input;  Name = CbName;              ArgumentName = CbName";
    push @argStr, "ArgumentType = Input;  Name = ElevAngle;           ArgumentName = ElevAngle";
    push @argStr, "ArgumentType = Input;  Name = OutagePercentage;    ArgumentName = OutagePercentage";
    push @argStr, "ArgumentType = Input;  Name = RcvrPosLLA;          ArgumentName = RcvrPosLLA";
    push @argStr, "ArgumentType = Input;  Name = XmtrPosLLA;          ArgumentName = XmtrPosLLA";

    return @argStr; 
}

sub Perl_RainLossModel_compute
{
    # the inputs here are in the order of the requested Inputs, as registered
    my @origArray = @_;

    # $origArray[0] is the calling mode

    # next argument is Date and is a String. Strings are simply scalars in Perl so the assignment is easy:
    my $date = $origArray[1];

    # next argument is Frequency (Hz) and is a double. doubles are simply scalars in Perl so the assignment is easy:
    my $freq = $origArray[2];

    # next argument is CbName and is a String.
    my $CbName = $origArray[3];

    # next argument is ElevAngle (degrees) and is a double.
    my $ElevAngle = $origArray[4];

    # next argument is OutagePercentage and is a double.
    my $OutagePercentage = $origArray[5];

    # next arguments are named RcvrPosLLA and XmtrPosLLA and are the receiver and transmitter positions, respectively, which is of type Double:3 
    # meaning it is an array of 3 doubles, Lat (deg), Lon (deg), Alt (m) are components of position. 
    # Arrays in Perl are passed by reference, so to get the
    # actual array out of the argument $origArray[6], one must de-reference it as an array as shown below

    my @rcvrPosArray = @{$origArray[6]};
    my @xmtrPosArray = @{$origArray[9]};

    if($Perl_RainLossModel_compute_init < 0)
    {
        $Perl_RainLossModel_compute_init = 1; 

        STKUtil::printOut " RainLossPlugin:init: 0: $origArray[0], 1: $origArray[1], 2: $origArray[2], 3: $origArray[3], 4: $origArray[4], 5: $origArray[5], 6: $@rcvrPosArray[0], 7: $@rcvrPosArray[1], 8: $@rcvrPosArray[2], 9: $@xmtrPosArray[0], 10: $@xmtrPosArray[1], 11: $@xmtrPosArray[2]";

        # The following hashes have been created automatically after this script has registered its inputs and outputs.
        # Each hash contains information about the arguments for this script. The hashes have been created as a
        # user convenience, for those users wanting to know, during the running of the script, what the inputs
        # and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
        # is unneeded and can be removed.

        $Perl_RainLossModel_Inputs = $g_PluginArrayInterfaceHash{'Perl_RainLossModel'}{'Inputs'};
        $Perl_RainLossModel_Outputs = $g_PluginArrayInterfaceHash{'Perl_RainLossModel'}{'Outputs'};

        %Perl_RainLossModel_Outputs_ArgHash = $Perl_RainLossModel_Outputs->getArgumentHash();

        # comment out the line below if you don't want to see the inputs and outputs each time the script is run
        Perl_RainLossModel_showArgs();
    }

    #############################################################################################
    # USER RAIN LOSS MODEL AREA.
    # PLEASE REPLACE THE CODE BELOW WITH YOUR LOSS COMPUTATION MODEL
    #############################################################################################

    # NOTE: the outputs that are returned MUST be in the same order as registered

    # Compute the Test Model
    # NOTE:  Loss should be returned as a positive dB value.

    my $rcvrPosLat, $rcvrPosLon, $rcvrPosalt;
    my $xmtrPosLat, $xmtrPosLon, $xmtrPosalt;

    $rcvrPosLat = @rcvrPosArray[0];
    $rcvrPosLon = @rcvrPosArray[1];
    $rcvrPosalt = @rcvrPosArray[2];
    $xmtrPosLat = @xmtrPosArray[0];
    $xmtrPosLon = @xmtrPosArray[1];
    $xmtrPosalt = @xmtrPosArray[2];

    my $Loss  = 1/20;

    # this defines the return array
    my @returnArray = ();

    # RETURN YOUR RESULTS BELOW

    $returnArray[0] =  $Loss;

    #############################################################################################
    # END OF USER MODEL AREA	
    #############################################################################################

    return @returnArray;
}

sub Perl_RainLossModel_showArgs
{
    my @argStrArray;

    STKUtil::printOut "Doing Perl_RainLossModel_compute_init\n";

    @argStrArray = ();

    push @argStrArray, $Perl_RainLossModel_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

    # the first arg on input is the calling mode

    push @argStrArray, "0 : this is the calling mode\n";

    my @args = $Perl_RainLossModel_Inputs->getArgumentArray();

    # to see description args

    my $index, $descrip;

    foreach $arg (@args)
    {
        ($index, $descrip) = $Perl_RainLossModel_Inputs->getArgument($arg);

        push @argStrArray, "$index : $arg = $descrip\n";
    }

    STKUtil::printOut @argStrArray;

    @argStrArray = ();

    push @argStrArray, $Perl_RainLossModel_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

    my @args = $Perl_RainLossModel_Outputs->getArgumentArray();

    # to see description args

    my $index, $descrip;

    foreach $arg (@args)
    {
        ($index, $descrip) = $Perl_RainLossModel_Outputs->getArgument($arg);

        push @argStrArray, "$index : $arg = $descrip\n";
    }

    STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
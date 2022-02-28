##########################################################################################
# SAPMLE FOR PERL BASED ATMOSPHERIC ABSORPTION LOSS PLUGIN SCRIPT PROVIDED BY THE USER
# PLEASE ADD YOUR MODEL IN THE USER  MODEL AREA BELOW.
# DO NOT CHANGE ANYTHING ELSE IN THE SCRIPT
##########################################################################################

# Perl_AbsorpModel

# declare some global variables

my $Perl_AbsorpModel_compute_init = -1;
my $Perl_AbsorpModel_Inputs, $Perl_AbsorpModel_Outputs;
my %Perl_AbsorpModel_Outputs_ArgHash;

sub Perl_AbsorpModel
{
    #The inputs to the the script arise as a reference to an array
    #the STKUtil::getInputArray function is used to get at the array itself

    my @inputData = @{$_[0]};

    my @retVal;

    if ( !defined($inputData[0]) )
    {
        #do compute

        @retVal = Perl_AbsorpModel_compute(@inputData);
    }
    elsif ( $inputData[0] eq 'register' )
    {
        $Perl_AbsorpModel_compute_init = -1;

        @retVal = Perl_AbsorpModel_register();
    }
    elsif ( $inputData[0] eq 'compute' )
    {
        @retVal = Perl_AbsorpModel_compute(@inputData);
    }
    else
    {
        # error: do nothing
    }

    #MUST return a reference to an array, as shown below

    return \@retVal;
}

sub Perl_AbsorpModel_register
{
    my @argStr;

    push @argStr, "ArgumentType = Output; Name = AbsorpLoss;     ArgumentName = AbsorpLoss";
    push @argStr, "ArgumentType = Output; Name = NoiseTemp;      ArgumentName = NoiseTemp";

    push @argStr, "ArgumentType = Input;  Name = DateUTC;             ArgumentName = DateUTC";
    push @argStr, "ArgumentType = Input;  Name = Frequency;           ArgumentName = Frequency";
    push @argStr, "ArgumentType = Input;  Name = CbName;              ArgumentName = CbName";
    push @argStr, "ArgumentType = Input;  Name = XmtrPosCBF;          ArgumentName = XmtrPosCBF";
    push @argStr, "ArgumentType = Input;  Name = RcvrPosCBF;          ArgumentName = RcvrPosCBF";

    return @argStr; 
}

sub Perl_AbsorpModel_compute
{
    #the inputs here are in the order of the requested Inputs, as registered
    my @origArray = @_;

    #$origArray[0] is the calling mode

    #next argument is Date and is a String. Strings are simply scalars in Perl so the assignment is easy:	
    my $date = $origArray[1];

    #next argument is Frequency (Hz) and is a double. doubles are simply scalars in Perl so the assignment is easy:	
    my $freq = $origArray[2];

    #next argument is CbName and is a String. 	
    my $CbName = $origArray[3];

    #next argument is named XmtrPosCBF and is the transmitter position, which is of type Double:3 
    #meaning it is an array of 3 doubles, X (m), Y (m), Z (m) are components of position. 
    #Arrays in Perl are passed by reference, so to get the
    #actual array out of the argument $origArray[4], one must de-reference it as an array as shown below

    #next argument is named RcvrPosCBF and is the receiver position, which is of type Double:3 
    #meaning it is an array of 3 doubles, X (m), Y (m), Z (m) are components of position. 
    #Arrays in Perl are passed by reference, so to get the
    #actual array out of the argument $origArray[5], one must de-reference it as an array as shown below

    my @xmtrPosArray = @{$origArray[4]};
    my @rcvrPosArray = @{$origArray[5]};

    if($Perl_AbsorpModel_compute_init < 0)
    {
        $Perl_AbsorpModel_compute_init = 1; 

        STKUtil::printOut " AbsorpLossPlugin:init: 0: $origArray[0], 1: $origArray[1], 2: $origArray[2], 3: $origArray[3], 4: $@xmtrPosArray[0], 5: $@xmtrPosArray[1], 6: $@xmtrPosArray[2], 7: $@rcvrPosArray[0], 8: $@rcvrPosArray[1], 9: $@rcvrPosArray[2]";

        #The following hashes have been created automatically after this script has registered its inputs and outputs.
        #Each hash contains information about the arguments for this script. The hashes have been created as a
        #user convenience, for those users wanting to know, during the running of the script, what the inputs
        #and outputs are. In many cases, the script write doesn't care, in which case this entire if-block
        #is unneeded and can be removed.

        $Perl_AbsorpModel_Inputs = $g_PluginArrayInterfaceHash{'Perl_AbsorpModel'}{'Inputs'};
        $Perl_AbsorpModel_Outputs = $g_PluginArrayInterfaceHash{'Perl_AbsorpModel'}{'Outputs'};

        %Perl_AbsorpModel_Outputs_ArgHash = $Perl_AbsorpModel_Outputs->getArgumentHash();

        #comment out the line below if you don't want to see the inputs and outputs each time the script is run
        Perl_AbsorpModel_showArgs();
    }

    #############################################################################################
    # USER ABSORPTION LOSS MODEL AREA.
    # PLEASE REPLACE THE CODE BELOW WITH YOUR LOSS COMPUTATION MODEL
    #############################################################################################

    # NOTE: the outputs that are returned MUST be in the same order as registered

    #Model for testing
    #Absorption Loss is about 10% of the free space loss (in dBs) and must be less than one.
    #NoiseTemp is the noise temprature in Kelvin.
    #
    #NOTE:  Return Loss is in Linear Scale, STK will convert to dBs

    my $xmtrPosLat, $xmtrPosLon, $xmtrPosalt;
    my $rcvrPosLat, $rcvrPosLon, $rcvrPosalt;

    $xmtrX = @xmtrPosArray[0];
    $xmtrY = @xmtrPosArray[1];
    $xmtrZ = @xmtrPosArray[2];

    $rcvrX = @rcvrPosArray[0];
    $rcvrY = @rcvrPosArray[1];
    $rcvrZ = @rcvrPosArray[2];

    my $xD = $xmtrX-$rcvrX;
    my $yD = $xmtrY-$rcvrY;
    my $zD = $xmtrZ-$rcvrZ;

    my $Range = sqrt($xD * $xD + $yD * $yD + $zD * $zD);
    my $freeSpace = (4 * 3.141592 * $Range * $freq) / 299792458.0;
    my $Loss = 10**(log10($freeSpace * $freeSpace)/10);

    # this defines the return array
    my @returnArray = ();

    # RETURN YOUR RESULTS BELOW

    $returnArray[0] =  1.0/$Loss;
    $returnArray[1] =  273.15 * (1 - 1.0/$Loss);
    
    #############################################################################################
    # END OF USER MODEL AREA	
    #############################################################################################

    return @returnArray;
}

sub log10
{
    my $x = shift;
    return log($x)/log(10);
}

sub Perl_AbsorpModel_showArgs
{
    my @argStrArray;

    STKUtil::printOut "Doing Perl_AbsorpModel_compute_init\n";

    @argStrArray = ();

    push @argStrArray, $Perl_AbsorpModel_Inputs->{'FunctionName'}->{'Name'} . " Inputs \n";

    #the first arg on input is the calling mode

    push @argStrArray, "0 : this is the calling mode\n";

    my @args = $Perl_AbsorpModel_Inputs->getArgumentArray();

    #to see description args

    my $index, $descrip;

    foreach $arg (@args)
    {
        ($index, $descrip) = $Perl_AbsorpModel_Inputs->getArgument($arg);

        push @argStrArray, "$index : $arg = $descrip\n";
    }

    STKUtil::printOut @argStrArray;

    @argStrArray = ();

    push @argStrArray, $Perl_AbsorpModel_Outputs->{'FunctionName'}->{'Name'} . " Outputs \n";

    my @args = $Perl_AbsorpModel_Outputs->getArgumentArray();

    #to see description args

    my $index, $descrip;

    foreach $arg (@args)
    {
        ($index, $descrip) = $Perl_AbsorpModel_Outputs->getArgument($arg);

        push @argStrArray, "$index : $arg = $descrip\n";
    }

    STKUtil::printOut @argStrArray;

}

# MUST end Perl script file with a non-zero integer

1;
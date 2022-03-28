# =====================================================
#   Copyright 2012, Analytical Graphics, Inc.          
# =====================================================

# Must be running under at least Perl 5.8.0 for support of the Win32:OLE package.

require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;

#############################################################
# Reference Frames Enumeration
#############################################################
use constant eUtFrameInertial          => 0;
use constant eUtFrameFixed             => 1;
use constant eUtFrameLVLH              => 2;
use constant eUtFrameNTC               => 3;

#############################################################
# Time Scale Enumeration
#############################################################
use constant eUTC             => 0;
use constant eTAI             => 1;
use constant eTDT             => 2;
use constant eUT1             => 3;
use constant eSTKEpochSec     => 4;
use constant eTDB             => 5;
use constant eGPS             => 6;

#############################################################
# Log Msg Type Enumeration
#############################################################
use constant eLogMsgDebug         => 0;
use constant eLogMsgInfo          => 1;
use constant eLogMsgForceInfo     => 2;
use constant eLogMsgWarning       => 3;
use constant eLogMsgAlarm         => 4;

#############################################################
# AgEAttrAddFlags Enumeration
#############################################################
use constant eFlagNone            => 0;
use constant eFlagTransparent     => 2;
use constant eFlagHidden          => 4;
use constant eFlagTransient       => 8;
use constant eFlagReadOnly        => 16;
use constant eFlagFixed           => 32;

# ==========================================
#  True False constants
# ==========================================
use constant true 	=> 1;
use constant false 	=> 0;

#############################################################
# Declare Global Variables
#############################################################
my $m_AgUtPluginSite     = undef;
my $m_StkRootObject      = undef;
my $m_AgAttrScope        = undef;
my $m_CalcToolProvider   = undef;
my $m_VectorToolProvider = undef;
my $m_DisplayName        = "VGT.Point.Perl.Example1";
my $m_MyDouble           = 10.123;
my $m_MyString           = "test";

my $m_objectConfiguredPoint = undef;

sub Message
{
    if (defined($m_AgUtPluginSite))
    {
        $m_AgUtPluginSite->Message( $_[0], $m_DisplayName . ": " . $_[1] );
    }
}

sub Init
{
    my $AgUtPluginSite = $_[0];
    $m_AgUtPluginSite = $AgUtPluginSite;

    my $retVal = true;

    if ( defined($m_AgUtPluginSite) )
    {
        # Get a pointer to the STK Object Model root object
        $m_StkRootObject = $m_AgUtPluginSite->StkRootObject;
    }

    return $retVal;
}


sub Register
{
    my $Result = $_[0];
    my $objPath = "[" . $Result->ObjectPath . "]";

    $Result->{ShortDescription} = "Test short Desc: Component created using " . $m_DisplayName . " " . $objPath;
    $Result->{LongDescription} = "Test long Desc: Component created using " . $m_DisplayName . " " . $objPath;

    $Result->SetRefSystem("Body", "<MyObject>");
}

sub Reset
{
    my $Result = $_[0];
    Message(eLogMsgInfo, "Reset() Entered:");

    my $retVal = true;

    $m_CalcToolProvider = $Result->CalcToolProvider;
    $m_VectorToolProvider = $Result->VectorToolProvider;

    $m_objectConfiguredPoint = $m_VectorToolProvider->ConfigurePoint("SubPoint(Detic)", "<MyObject>", "Body", "<MyObject>");

    Message(eLogMsgInfo, "Reset() Exited:");

    return $retVal;
}

sub Evaluate
{
    my $Result = $_[0];

    my $retVal = true;

    if (defined($m_objectConfiguredPoint))
    {
        my $currentValueArray = $m_objectConfiguredPoint->CurrentValue_Array($Result);
        my $x = @{$currentValueArray}[0];
        my $y = @{$currentValueArray}[1];
        my $z = @{$currentValueArray}[2];

        # For this example, the point is the detic point divided by two.
        $Result->SetPosition($x / 2, $y / 2, $z / 2);
    }

    return $retVal;
}

sub Free
{
    Message(eLogMsgInfo, "Free()");

    $m_AgUtPluginSite = undef;
    $m_StkRootObject = undef;
    $m_CalcToolProvider = undef;
    $m_VectorToolProvider = undef;
    $m_objectConfiguredPoint = undef;
}

#=======================
# GetPluginConfig method
#=======================
sub GetPluginConfig
{
    my $AgAttrBuilder = $_[0];

    if (!defined($m_AgAttrScope))
    {
        $m_AgAttrScope = $AgAttrBuilder->NewScope();
        $AgAttrBuilder->AddStringDispatchProperty( $m_AgAttrScope, "MyString", "A string", "MyString", eFlagReadOnly );
        $AgAttrBuilder->AddDoubleDispatchProperty( $m_AgAttrScope, "MyDouble", "A double", "MyDouble", eFlagNone );
    }

    return $m_AgAttrScope;
}

#===========================
# VerifyPluginConfig method
#===========================
sub VerifyPluginConfig
{
    my $VerifyResult = $_[0];

    my $Result = true;
    my $Message = "Ok";

    $VerifyResult->{Result}  = $Result;
    $VerifyResult->{Message} = $Message;
}

#==================
# MyString property
#==================
sub GetMyString
{
    return $m_MyString;
}

sub SetMyString
{
    $m_MyString = $_[0];
}

#==================
# MyDouble property
#==================
sub GetMyDouble
{
    return $m_MyDouble;
}

sub SetMyDouble
{
    $m_MyDouble = $_[0];
}

#=====================================================
#  Copyright 2012, Analytical Graphics, Inc.          
#=====================================================

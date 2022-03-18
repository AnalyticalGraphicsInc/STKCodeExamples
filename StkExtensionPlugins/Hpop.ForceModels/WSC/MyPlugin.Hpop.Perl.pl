# =====================================================
#   Copyright YYYY, YOUR COMPANY NAME GOES HERE           
# =====================================================

# Must be running under at least Perl 5.6.1 for support of the Win32:OLE package.

require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;

#############################################################
####
#### Begin Enumeration Declarations
####

# ==========================================
#  Reference Frames Enumeration
# ==========================================
use constant eUtFrameInertial 		=> 0;
use constant eUtFrameFixed 			=> 1;
use constant eUtFrameLVLH 			=> 2;
use constant eUtFrameNTC 			=> 3;

my $numFrames = 4;
my %refFrameNamesHash;
$refFrameNamesHash{0} = "eUtFrameInertial";
$refFrameNamesHash{1} = "eUtFrameFixed";
$refFrameNamesHash{2} = "eUtFrameLVLH";
$refFrameNamesHash{3} = "eUtFrameNTC";

my @refFrameNamesArray;

my $i;
for($i = 0; $i < $numFrames; $i++)
{
	push @refFrameNamesArray, "$refFrameNamesHash{$i}";
}


# ==========================================
#  Time Scale Enumeration
# ==========================================
use constant eUTC 			=> 0;
use constant eTAI 			=> 1;
use constant eTDT 			=> 2;
use constant eUT1 			=> 3;
use constant eSTKEpochSec 	=> 4;
use constant eTDB 			=> 5;
use constant eGPS 			=> 6;

# =+========================================
#  Log Msg Type Enumeration
# ==========================================
use constant eLogMsgDebug	 	=> 0;
use constant eLogMsgInfo 		=> 1;
use constant eLogMsgForceInfo 	=> 2;
use constant eLogMsgWarning 	=> 3;
use constant eLogMsgAlarm 		=> 4;

# ==========================================
#  Sun Position Enumeration
# ==========================================
use constant eApparentToTrueCB 	=> 0;
use constant eApparent 			=> 1;
use constant eTrue 				=> 2;
use constant eSRP 				=> 3;

# =================================
#  Accel Type Enumeration
# =================================

use constant eTotalAccel 			=> 0;
use constant eTwoBodyAccel 			=> 1;
use constant eGravityAccel 			=> 2;
use constant ePerturbedGravityAccel => 3;
use constant eSolidTidesAccel 		=> 4;
use constant eOceanTidesAccel 		=> 5;
use constant eDragAccel 			=> 6;
use constant eSRPAccel 				=> 7;
use constant eThirdBodyAccel 		=> 8;
use constant eGenRelativityAccel 	=> 9;
use constant eAddedAccel 			=> 10;

# =================================
#  ForceModel Type Enumeration
# =================================

use constant eGravityModel 			=> 0;
use constant eSolidTidesModel		=> 1;
use constant eOceanTidesModel 		=> 2;
use constant eDragModel				=> 3;
use constant eSRPModel		 		=> 4;
use constant eThirdBodyModel 		=> 5;
use constant eGenRelativityModel 	=> 6;

# ==========================================
#  AgEAttrAddFlags Enumeration
# ==========================================
use constant eFlagNone			=> 0;
use constant eFlagTransparent	=> 2;
use constant eFlagHidden		=> 4;
use constant eFlagTransient		=> 8;  
use constant eFlagReadOnly		=> 16;
use constant eFlagFixed			=> 32;

# ==========================================
#  True False constants
# ==========================================
use constant true 	=> 1;
use constant false 	=> 0;

####
#### End Enumeration Declarations
####
#############################################################

#############################################################
####
#### Begin Plugin Variable Declarations
####

my $m_AgUtPluginSite		= undef;
my $m_AgAttrScope			= undef;

my $m_Name					= "MyPlugin.Hpop.Perl.wsc";
my $m_MyProperty			= 0.0;

####
#### Plugin Variable Declarations
####
#############################################################

# ========================
#  GetPluginConfig method
# ========================
sub GetPluginConfig
{
	my $AgAttrBuilder = $_[0];
	
	if( !defined($m_AgAttrScope) )
	{
		$m_AgAttrScope = $AgAttrBuilder->NewScope();
		
		# Register all plugin parameters here, using AgAttrBuilder
				
		# Each parameter has a Type (double, bool, integer, filename, string, etc.)
				
		# Example: registration of MyProperty as a double

		$AgAttrBuilder->AddDoubleDispatchProperty ( 
			$m_AgAttrScope, 
			"MyProperty",         
			"A description of MyProperty", 
			"MyProperty",
			eFlagNone );
	}

	return $m_AgAttrScope;
}  

# ===========================
#  VerifyPluginConfig method
# ===========================
sub VerifyPluginConfig
{
	my $AgUtPluginConfigVerifyResult = $_[0];
	
    my $Result = true;
    my $Message = "Ok";

    # Perform checks of the ranges of the plugin parameter data
	    
	# If there is an error in the settings, then set Result to false
	# and provide a Message to be communicated back to the user
    # indicating the problem

	$AgUtPluginConfigVerifyResult->{Result}  = $Result;
	$AgUtPluginConfigVerifyResult->{Message} = $Message;
} 

# ======================
#  Init Method
# ======================
sub Init
{
	my $AgUtPluginSite = $_[0];
	
	$m_AgUtPluginSite = $AgUtPluginSite;
	
	my $retVal = true;
	
	# Implement any Init activities here
	# If the plugin cannot initialize (say, because it cannot open a file it needs)
	# then it can return false and the plugin won't be be used
		
	# $retVal = false;
    
    return $retVal;
} 

# ======================
#  PrePropagate Method
# ======================
sub PrePropagate
{
	# Implement any PrePropogate activities here
		
	return true;
}


# ======================
#  PreNextStep Function
# ======================
sub PreNextStep
{
	my $retVal = true;
	
	# Implement any PreNextStep activities here

	# If you have none, you can tell the application not to even call this function
	# by returning false instead of true

	# $retVal = false;
	
	return $retVal;
}

# =================
#  Evaluate Method
# =================
sub Evaluate
{
	my $retVal = true;

	# Implement any Evaluate activities here

	# If you have none, you can tell the application not to even call this function
	# by returning false instead of true

	# $retVal = false;
		
	return $retVal;
}


# =================
#  PostEvaluate Method
# =================
sub PostEvaluate
{
	my $retVal = true;

	# Implement any PostEvaluate activities here

	# If you have none, you can tell the application not to even call this function
	# by returning false instead of true

	# $retVal = false;
			
	return $retVal;
}

# ========================================================
#  PostPropagate Method
# ========================================================
sub PostPropagate
{
	# Implement any PostPropogate activities here
			
	return true;
}

# ===========================================================
#  Free Method
# ===========================================================
sub Free()
{
	$m_AgUtPluginSite = undef;
}

# =============================================================
#  Name Method
# =============================================================
sub GetName
{
	return $m_Name;
}

sub SetName
{
	$m_Name = $_[0];
}

# ============================================================
#  MyProperty property
# ============================================================
sub GetMyProperty
{
	return $m_MyProperty;
}

sub SetMyProperty
{
	$m_MyProperty = $_[0];
}

# =====================================================
#   Copyright YYYY, YOUR COMPANY NAME GOES HERE        
# =====================================================

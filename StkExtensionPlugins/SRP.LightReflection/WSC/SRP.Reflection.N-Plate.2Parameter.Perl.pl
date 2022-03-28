#########################################################################
#                                                                       #
#  2 Plate Reflectance Model.                                           # 
#                                                                       #
#  Computes Reflectance and Partials in Body Coordinates                #
#                                                                       # 
#  This model solves for 2 scale parameters, one for each plate.        #
#  No attempt has been made to generalize the code to support           #
#  a N-plate model.                                                     #
#                                                                       #
#                                                                       #
#  Note: Turning on the DebugMode will output accelerations to the file #
#        given by the DebugFile attribute. This attribute               #
#        only opens an existing file; it does not create a new file.    #
#        So to output Debug data this file needs to be created before   #
#        running the plugin. The default filename is                    #
#           'C:\Temp\Nplate_2Parameter_SRP_Reflectance_Debug.txt'       #
#########################################################################


# This script requires Perl 5.8.0 or higher

#  This first section of code (before any of the subroutines or methods) is where all
#  the global variables are initialized
require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;


# ==========================================
#  Reference Frames Enumeration
# ==========================================
use constant eInertial 		=> 0;
use constant eFixed 		=> 1;
use constant eLVLH 			=> 2;
use constant eNTC 			=> 3;
use constant eBodyFrame		=> 4;

my $numFrames = 4;
my %refFrameNamesHash;
$refFrameNamesHash{0} = "eInertial";
$refFrameNamesHash{1} = "eFixed";
$refFrameNamesHash{2} = "eLVLH";
$refFrameNamesHash{3} = "eNTC";
$refFrameNamesHash{4} = "eBody";


my @refFrameNamesArray;

my $i;
for($i = 0; $i < $numFrames; $i++)
{
	push @refFrameNamesArray, "$refFrameNamesHash{$i}";
}

my %TFhash;
$TFhash{0} = "False";
$TFhash{1} = "True";

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

# ==========================================
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
#  Declare Global Variables
# ==========================================
my $m_AgUtPluginSite		= undef;
my $m_AgAttrScope			= undef;

my $m_speedOfLight   = 299792458.0;
my $m_spacecraftMass = 6800.0;
my $m_solarFlux;
my @m_sunlightSRP    = (); 
my @m_sailingSRP     = ();
my @SRP_b            = ();


my $m_MsgCntr		= 0;
my $m_EvalMsgsOn	= 1;
my $m_EvalMsgCount	= 0;
my $m_EvalMsgMax 	= 1;

### Parameter Indicies ####
my $m_CrIndex1    = -1;
my $m_CrIndex2    = -1;


use constant PI     => 3.1415926535897932384;
use constant false	=> 0;
use constant true	=> 1;

# ======================================
#  Declare Global 'Attribute' Variables
# ======================================
#  These variables are the default values for the Plugin attributes 
#  that are displayed in ODTK.  The subroutines at the end of this
#  code are used to pass the values of these attributes back and forth.
my $m_Name					= "SRP.Reflection.N-Plate.2Parameter.Perl";
my $m_Enabled				= true;
my $m_DebugMode				= false;
my $m_MsgInterval			= 1;
my $m_diffuseReflectivity	= 0.03;		# 0 <= value <= (1-$m_specularReflectivity)
my $m_specularReflectivity	= 0.57;		# 0 <= value <= 1.0
my $m_ErrorModel            = true;
my $m_Sun_Roll              = 0;
my $m_Sun_Pitch             = 0;
my $m_SrpArea			    = 20.0;

my $m_dcEpochRef;

########################
# Create path variable for Debug file
########################
my $m_DebugFileName = 'C:\Temp\Nplate_2Parameter_SRP_Reflectance_Debug.txt';


sub Message{
	my $severity = $_[0];
	my $msg = $_[1];
	
	if( defined($m_AgUtPluginSite) )
	{
		$m_AgUtPluginSite->Message( $severity, "$msg" );
	}
}

sub DebugMsg{
	my $msg = $_[0];
	
	#if($m_DebugMode && $m_EvalMsgsOn)
	if($m_DebugMode)		
	{
		if($m_MsgCntr % $m_MsgInterval == 0)
		{
			Message(eLogMsgDebug, "$msg");
		}
	}
}

sub acos{
	my $arg = $_[0];
	
	# deal with nuemrical roundoff issues
	
	my $temp = 1.0 - $arg*$arg;
	
	if($temp < 0.0)
	{
		$temp = 0.0;
	}
	elsif($temp > 1.0)
	{
		$temp = 1.0;
	}
	
	my $val = atan2(sqrt($temp), $arg);
	
	return $val;
	
}

sub asin{
    my $temp = sqrt(1 - $_[0] * $_[0]);
    my $val  = atan2($_[0],$temp);

    return $val;
}

sub crossProduct{
	my $aArrayRef = $_[0];
	my $bArrayRef = $_[1];

	my @answer;

	push @answer, $aArrayRef->[1]*$bArrayRef->[2] - $bArrayRef->[1]*$aArrayRef->[2];
	push @answer, $aArrayRef->[2]*$bArrayRef->[0] - $bArrayRef->[2]*$aArrayRef->[0];
	push @answer, $aArrayRef->[0]*$bArrayRef->[1] - $bArrayRef->[0]*$aArrayRef->[1];

	return \@answer;
} 

sub dotProduct{
	my $aArrayRef = $_[0];
	my $bArrayRef = $_[1];
	
	my $dot = $aArrayRef->[0] * $bArrayRef->[0] +
			  $aArrayRef->[1] * $bArrayRef->[1] +
			  $aArrayRef->[2] * $bArrayRef->[2];
			  
	return $dot;
}

sub scaleVector{
	my $factor = $_[0];
	my $arrayRef = $_[1];
	
	my @newArray;
	push @newArray, $arrayRef->[0] * $factor;
	push @newArray, $arrayRef->[1] * $factor;
	push @newArray, $arrayRef->[2] * $factor;
	
	return \@newArray;
}

sub normalize{
	my $arrayRef = $_[0];
	
	my $magnitude = dotProduct($arrayRef, $arrayRef);
	$magnitude = sqrt($magnitude);
	
	if($magnitude > 0.0)
	{
		$arrayRef->[0] /= $magnitude;
		$arrayRef->[1] /= $magnitude;
		$arrayRef->[2] /= $magnitude;
	}
	
	return $magnitude;
}

sub addToVector{
	my $sumVectorRef = $_[0];
	my $addVectorRef = $_[1];
	
	$sumVectorRef->[0] += $addVectorRef->[0];
	$sumVectorRef->[1] += $addVectorRef->[1];
	$sumVectorRef->[2] += $addVectorRef->[2];
}

sub RotateVector{
    my $col1ref = $_[0];
    my $col2ref = $_[1];
    my $col3ref = $_[2];
    my $vecref  = $_[3];
    
    my @r_vec; push @r_vec, 0; push @r_vec, 0; push @r_vec, 0; 
    
    my $i = 0;
    for ($i=0; $i<=2; $i++){
    $r_vec[$i] = $col1ref->[$i]*$vecref->[0] + $col2ref->[$i]*$vecref->[1] + $col3ref->[$i]*$vecref->[2];
    }
    
    return @r_vec;
}	

sub Transpose{
    my $col1ref  = $_[0];
    my $col2ref  = $_[1];
    my $col3ref  = $_[2];
    my $Tcol1ref = $_[3];
    my $Tcol2ref = $_[4];
    my $Tcol3ref = $_[5];
    
    $Tcol1ref->[0] = $col1ref->[0]; 
    $Tcol1ref->[1] = $col2ref->[0]; 
    $Tcol1ref->[2] = $col3ref->[0];
    
    $Tcol2ref->[0] = $col1ref->[1]; 
    $Tcol2ref->[1] = $col2ref->[1]; 
    $Tcol2ref->[2] = $col3ref->[1];
    
    $Tcol3ref->[0] = $col1ref->[2]; 
    $Tcol3ref->[1] = $col2ref->[2]; 
    $Tcol3ref->[2] = $col3ref->[2];
    
}

sub RotationMatrix{
    my $RotNum   = $_[0];
    my $Col_1ref = $_[1];
    my $Col_2ref = $_[2];
    my $Col_3ref = $_[3];
    my $angle    = $_[4];
    
    if ($RotNum == 1) {
        # Generate Rotation 1 Matrix
        $Col_1ref->[0] = 1;  $Col_2ref->[0] =            0;  $Col_3ref->[0] =           0;
        $Col_1ref->[1] = 0;  $Col_2ref->[1] =  cos($angle);  $Col_3ref->[1] = sin($angle);
        $Col_1ref->[2] = 0;  $Col_2ref->[2] = -sin($angle);  $Col_3ref->[2] = cos($angle);
            
    } elsif ($RotNum == 2) {
        # Generate Rotation 2 Matrix
        $Col_1ref->[0] = cos($angle);  $Col_2ref->[0] = 0;  $Col_3ref->[0] = -sin($angle);
        $Col_1ref->[1] =           0;  $Col_2ref->[1] = 1;  $Col_3ref->[1] =            0;
        $Col_1ref->[2] = sin($angle);  $Col_2ref->[2] = 0;  $Col_3ref->[2] =  cos($angle);
    
    } elsif ($RotNum == 3) {
        # Generate Rotation 3 Matrix
        $Col_1ref->[0] =  cos($angle);  $Col_2ref->[0] = sin($angle);  $Col_3ref->[0] = 0;
        $Col_1ref->[1] = -sin($angle);  $Col_2ref->[1] = cos($angle);  $Col_3ref->[1] = 0;
        $Col_1ref->[2] =            0;  $Col_2ref->[2] =           0;  $Col_3ref->[2] = 1;
    } else {
        # throw error
        Message( eLogMsgDebug, "Error: Choose a Rotation number between 1 and 3.");
    }

}

sub computeSRP{
	my $Result = $_[0];
	my $illum = $_[1];
	my $cr1 = $_[2];
	my $cr2 = $_[3];	
	my $incidentVecArrayRef = $_[4];   #Incident Vector onto Body (Body)
	my $sunlightSrpArrayRef = $_[5];
    my $sailingSrpArrayRef  = $_[6];

    
	#
	# Begin computation of SRP using an N-plate model
	#
    # initialize constants
    my $Nplate        = 2;
    my $R             = 149597870000;  # 1 AU in meters
   
    # initialize Arrays (areas of plates, and normal vectors in body frame)
    my @Cr;
    push @Cr, $cr1;
    push @Cr, $cr2;
    
    my @A; 
    # push @A, 69.6391;   # m^2   (original value)
    # push @A, 81.9058;   # m^2   (original value)
    push @A, 66.8535;   # m^2
    push @A, 78.6296;   # m^2
    #                 Plate 1            Plate 2
    my @Nx; push @Nx, -0.1045; push @Nx, -0.3592;  
    my @Ny; push @Ny,  0.0;    push @Ny,  0.0;
    my @Nz; push @Nz, -0.9945; push @Nz, -0.9333;

    my @sailContrib;     push @sailContrib, 0.0; push @sailContrib, 0.0; push @sailContrib, 0.0;
    my @sunlightContrib; push @sunlightContrib, 0.0; push @sunlightContrib, 0.0; push @sunlightContrib, 0.0;
    
    my @posParts;
    push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;
    push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;
    push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;
    
    my $j = 0;
    my $index = $m_CrIndex1;

    @SRP_b = ();      push @SRP_b, 0.0; push @SRP_b, 0.0; push @SRP_b, 0.0;  
    
    #all calcs in the for loop below are in the body frame
    for ($j = 0; $j <= $Nplate-1; $j++) {
        #take dot product of sun vector and normal vector for plate
        #(no need to divide by magnitudes, since both are unit vectors)
        my $Coeff = -$Cr[$j]*$illum;
        
        my $dot = -($incidentVecArrayRef->[0] * $Nx[$j] + $incidentVecArrayRef->[1] * $Ny[$j] + $incidentVecArrayRef->[2] * $Nz[$j]);

        my $Nx_comp = 2*($m_diffuseReflectivity/3 + $m_specularReflectivity*$dot)*$Nx[$j];
        my $Ny_comp = 2*($m_diffuseReflectivity/3 + $m_specularReflectivity*$dot)*$Ny[$j];
        my $Nz_comp = 2*($m_diffuseReflectivity/3 + $m_specularReflectivity*$dot)*$Nz[$j];
    
        my $Sx_comp = -(1 - $m_specularReflectivity)*$incidentVecArrayRef->[0];
        my $Sy_comp = -(1 - $m_specularReflectivity)*$incidentVecArrayRef->[1];
        my $Sz_comp = -(1 - $m_specularReflectivity)*$incidentVecArrayRef->[2];
    
        my $Sunlight_x = $Coeff * $A[$j] * abs($dot)  * ($Nx_comp + $Sx_comp);
        my $Sunlight_y = $Coeff * $A[$j] * abs($dot)  * ($Ny_comp + $Sy_comp);
        my $Sunlight_z = $Coeff * $A[$j] * abs($dot)  * ($Nz_comp + $Sz_comp);  
        
        my $sign = -1;
        if ($dot < 0)
        {
			$sign = +1;
		}		
		
		my $W = $Coeff * $A[$j];
		my $x = $W * ($Nx_comp + $Sx_comp);
		my $y = $W * ($Ny_comp + $Sy_comp);
		my $z = $W * ($Nz_comp + $Sz_comp);
		
		$posParts[0] += $sign * $x * $Nx[$j];
		$posParts[1] += $sign * $x * $Nx[$j];
		$posParts[2] += $sign * $x * $Nx[$j];
		$posParts[3] += $sign * $y * $Ny[$j];
		$posParts[4] += $sign * $y * $Ny[$j];
		$posParts[5] += $sign * $y * $Ny[$j];
		$posParts[6] += $sign * $z * $Nz[$j];
		$posParts[7] += $sign * $z * $Nz[$j];
		$posParts[8] += $sign * $z * $Nz[$j];	
	
		
		if ($index > -1) 
		{
			
			my $dSdCrX = $Sunlight_x/$Cr[$j];
			my $dSdCrY = $Sunlight_y/$Cr[$j];
			my $dSdCrZ = $Sunlight_z/$Cr[$j];					
			$Result->SetReflectanceInBodyParamPartials($index, $dSdCrX, $dSdCrY, $dSdCrZ);
		}	   

        $SRP_b[0] += $Sunlight_x;
        $SRP_b[1] += $Sunlight_y;
        $SRP_b[2] += $Sunlight_z; 
        
        $index = $m_CrIndex2;     
    } # end of loop over plates
    
    
    #  get sun-pitch and sun-roll angles  for display 
    
    my @S; 
    push @S, -$incidentVecArrayRef->[0];
    push @S, -$incidentVecArrayRef->[1];
    push @S, -$incidentVecArrayRef->[2];
    my $S_mag = normalize(\@S);

    $m_Sun_Roll  = (asin(-$S[1]))*(180/(PI));
    if ($S[2] > 0) {
        $m_Sun_Pitch = (atan2(-$S[0],$S[2]))*(180/(PI)) ;
    } elsif ($S[2] < 0) {
        if ($S[0] > 0) {
            $m_Sun_Pitch = (atan2(-$S[0],$S[2]) - (PI)*(-1))*(180/(PI));
        } elsif ($S[0] < 0) {
            $m_Sun_Pitch = (atan2(-$S[0],$S[2]) - (PI)*(1))*(180/(PI));
        } else {
            $m_Sun_Pitch = (atan2(-$S[0],$S[2]))*(180/(PI));
        }
    } else {
        $m_Sun_Pitch = 0;
    }
    
      
    #now get sunlight contribution       
       
    my $SRP_sunlight_mag = dotProduct(\@SRP_b, $incidentVecArrayRef);
    $sunlightContrib[0] = $incidentVecArrayRef->[0]*$SRP_sunlight_mag;
    $sunlightContrib[1] = $incidentVecArrayRef->[1]*$SRP_sunlight_mag;
    $sunlightContrib[2] = $incidentVecArrayRef->[2]*$SRP_sunlight_mag;
    
    #now get sailing contribution
    $sailContrib[0] = $SRP_b[0] - $sunlightContrib[0];
    $sailContrib[1] = $SRP_b[1] - $sunlightContrib[1];
    $sailContrib[2] = $SRP_b[2] - $sunlightContrib[2];
	    
	#add all contributions 
	@{$sailingSrpArrayRef}  = @sailContrib;
	@{$sunlightSrpArrayRef} = @sunlightContrib;
	
	$Result->SetReflectanceInBody($SRP_b[0], $SRP_b[1], $SRP_b[2]);
		
	####### Position Partials #####
	my $incidentDirPosPartialsArrayRef = $Result->IncidentDirectionBodyCompPosPartials_Array();

	if(defined($incidentDirPosPartialsArrayRef))
	{
		my @posPartials;
		
		$posPartials[0][0] = $posParts[0] * $incidentDirPosPartialsArrayRef->[0];
		$posPartials[0][1] = $posParts[1] * $incidentDirPosPartialsArrayRef->[1];
		$posPartials[0][2] = $posParts[2] * $incidentDirPosPartialsArrayRef->[2];

		$posPartials[1][0] = $posParts[3] * $incidentDirPosPartialsArrayRef->[3];
		$posPartials[1][1] = $posParts[4] * $incidentDirPosPartialsArrayRef->[4];
		$posPartials[1][2] = $posParts[5] * $incidentDirPosPartialsArrayRef->[5];

		$posPartials[2][0] = $posParts[6] * $incidentDirPosPartialsArrayRef->[6];
		$posPartials[2][1] = $posParts[7] * $incidentDirPosPartialsArrayRef->[7];
		$posPartials[2][2] = $posParts[8] * $incidentDirPosPartialsArrayRef->[8];

		$Result->SetReflectanceBodyCompPosPartials( 
					$posPartials[0][0], $posPartials[0][1], $posPartials[0][2],
					$posPartials[1][0], $posPartials[1][1], $posPartials[1][2],
					$posPartials[2][0], $posPartials[2][1], $posPartials[2][2]);
	}


	
	####### Velocity Partials zero?
	my @velPartials;
	$velPartials[0][0] = 0.0;
	$velPartials[0][1] = 0.0;
	$velPartials[0][2] = 0.0;
	
	$velPartials[1][0] = 0.0;
	$velPartials[1][1] = 0.0;
	$velPartials[1][2] = 0.0;
	
	$velPartials[2][0] = 0.0;
	$velPartials[2][1] = 0.0;
	$velPartials[2][2] = 0.0;

	$Result->SetReflectanceBodyCompVelPartials(eBodyFrame, 
		$velPartials[0][0], $velPartials[0][1], $velPartials[0][2],
		$velPartials[1][0], $velPartials[1][1], $velPartials[1][2],
		$velPartials[2][0], $velPartials[2][1], $velPartials[2][2]);
	
	$m_Enabled = defined($Result);

}
		

# ========================
#  GetPluginConfig method
# ========================
sub GetPluginConfig{
	my $AgAttrBuilder = $_[0];
	
	if( !defined($m_AgAttrScope) )
	{
		$m_AgAttrScope = $AgAttrBuilder->NewScope();
		
		# ===========================
		#  General Plugin attributes
		# ===========================
		$AgAttrBuilder->AddStringDispatchProperty(
		   $m_AgAttrScope,"PluginName","Human readable plugin name or alias","Name",eFlagNone);
		
		$AgAttrBuilder->AddBoolDispatchProperty(
		   $m_AgAttrScope,"PluginEnabled","If the plugin is enabled or has experienced an error","Enabled",eFlagNone);		
           
		# ==============================
		#  Reflectivity related attributes
		# ==============================
		$AgAttrBuilder->AddDoubleDispatchProperty(
		   $m_AgAttrScope,"Reflectivity_Specular","Specular reflectivity coefficient","SpecularReflectivity",eFlagNone);
		   
		$AgAttrBuilder->AddDoubleDispatchProperty(
		   $m_AgAttrScope,"Reflectivity_Diffuse","Diffuse reflectivity coefficient","DiffuseReflectivity",eFlagNone);
				
		# ==============================
		#  Messaging related attributes
		# ==============================
      	$AgAttrBuilder->AddBoolDispatchProperty(
		   $m_AgAttrScope,"DebugMode","Turn debug messages on or off","DebugMode",eFlagNone);
           
		$AgAttrBuilder->AddIntDispatchProperty(
		   $m_AgAttrScope,"MessageInterval","The interval at which to send messages during propagation in Debug mode","MsgInterval",eFlagNone);
		
		   
		$AgAttrBuilder->AddFileDispatchProperty(
		   $m_AgAttrScope,"DebugFileName","OutputDebugFileName","DebugFileName",
		   ".txt","*.txt",eFlagNone);
		 
 	}

	return $m_AgAttrScope;
}  

# ===========================
#  VerifyPluginConfig method
# ===========================
sub VerifyPluginConfig{
	my $AgUtPluginConfigVerifyResult = $_[0];
	
    my $Result = true;
    my $Message = "Ok";
    
    if($m_specularReflectivity < 0.0 || $m_specularReflectivity > 1.0)
    {
		$Result = false;
		$Message = "Specular Reflectivity must be between 0.0 and 1.0";
	}
	elsif($m_diffuseReflectivity < 0.0 || $m_diffuseReflectivity > (1.0 - $m_specularReflectivity))
	{
		$Result = false;
		$Message = "Diffuse Reflectivity must be between 0.0 and (1.0 - Specular)";
	}

	$AgUtPluginConfigVerifyResult->{Result}  = $Result;
	$AgUtPluginConfigVerifyResult->{Message} = $Message;
} 

# ======================
#  Init Method
# ======================
sub Init{
	my $AgUtPluginSite = $_[0];
	
	$m_AgUtPluginSite = $AgUtPluginSite;
	
	if( defined($m_AgUtPluginSite) )
	{
		#Win32::MsgBox("<Enabled ,Debug> = $m_Enabled, $m_DebugMode", 0);
		 
		if( $m_DebugMode == true )
		{
			if( $m_Enabled == true )
			{
				Message( eLogMsgInfo, "$m_Name.Init(): Enabled" );
			}
			else
			{
				Message( eLogMsgInfo, "$m_Name.Init(): Disabled because Enabled flag is False" );
			}
		}
		elsif($m_Enabled == false)
		{
			Message( eLogMsgAlarm, "$m_Name.Init(): Disabled because Enabled flag is False" );
		}
	}
	
    return $m_Enabled;
} 

# ======================
#  PreCompute Method
# ======================
# 
sub PreCompute{
	my $AgAsHpopPluginResult = $_[0];
	
    if( $m_Enabled == true )
	{
		if( defined($AgAsHpopPluginResult) )
		{			
			# compute terms that don't change over time - do not think we need this
			$m_speedOfLight = $AgAsHpopPluginResult->{LightSpeed};	       
			
			
			if( $m_DebugMode == true )
			{
				Message(eLogMsgDebug, "$m_Name.PreCompute(): Enabled");
	            
				# Open debug file and write header
				open(DEBUGFILE,">$m_DebugFileName");
				print DEBUGFILE "SRP Debug File \n";
				print DEBUGFILE "Plugin SRP Model: $m_Name \n";
				print DEBUGFILE "Message Interval: $m_MsgInterval \n";
					
				my $dateArrayRef = $AgAsHpopPluginResult->Date_Array(eUTC);
				
				printf DEBUGFILE "Epoch: %4d.%03d.%02d:%02d:%06.3f UTCG \n",
				     $dateArrayRef->[0], $dateArrayRef->[1], 
				     $dateArrayRef->[3], $dateArrayRef->[4], $dateArrayRef->[5]; 
				     
				$m_dcEpochRef = $AgAsHpopPluginResult->DayCount_Array(eUTC);
				     
				print DEBUGFILE  "\n";
				print DEBUGFILE "                                                    Acceleration in body frame\n";
				print DEBUGFILE " Elapsed Time      SunPitch   SunRoll           X                     Y                  Z\n";
				print DEBUGFILE "     (sec)           (deg)     (deg)         (m/sec^2)            ( m/sec^2)         ( m/sec^2)\n";
											
			}					
		}
		elsif( $m_DebugMode == true )
		{	
			Message( eLogMsgDebug, "$m_Name.PreCompute(): Disabled" );	
		}
	}
	return $m_Enabled;
}

# =================
#  Evaluate Method
# =================
#  This method is called at every integration step over the propagation.
#  This method calls the computeSRP subroutine (above)
sub Evaluate{
	my $Result = $_[0];
	
	if($m_Enabled == true && defined($Result) )
	{
		$m_spacecraftMass = $Result->{TotalMass};
		
		# if illumination is zero, there isn't any contribution anyway, so do nothing
				
		my $illum = $Result->{SolarIntensity};

		if($illum == 0.0)
		{
			return $m_Enabled;
		}
			
		$m_solarFlux = $Result->{SolarFlux};		# L /(4 * pi * R_sun^2)				
	
		my $cr1 = 0;		
		if($m_CrIndex1 > -1)
		{
			$cr1 = $Result->ParameterValue($m_CrIndex1);
		}
		
		
		my $cr2 = 0;		
		if($m_CrIndex2 > -1)
		{
			$cr2 = $Result->ParameterValue($m_CrIndex2);
		}				
	   		
		@m_sunlightSRP = ();	# these are globals
		@m_sailingSRP  = ();
	
		my $incidentVecArrayRef = $Result->IncidentDirectionInBody_Array();			
	       
		computeSRP($Result, $illum, $cr1, $cr2,
		           $incidentVecArrayRef, \@m_sunlightSRP, \@m_sailingSRP);	
		           
	    if( $m_Enabled != true  )
		{				
			DebugMsg("enabled fail")
		}
			
		if( $m_Enabled == true && $m_DebugMode == true && defined($Result) )
		{		
			
			if( ($m_MsgCntr % $m_MsgInterval) == 0)
			{				
				my $conv = $m_solarFlux / ($m_speedOfLight * $m_spacecraftMass);
				my $bx   = $conv * ($m_sunlightSRP[0] + $m_sailingSRP[0]);
				my $by   = $conv * ($m_sunlightSRP[1] + $m_sailingSRP[1]);
				my $bz   = $conv * ($m_sunlightSRP[2] + $m_sailingSRP[2]);  
				
				     
				my $dcRef = $Result->DayCount_Array(eUTC);  
				my $secs  = ($dcRef->[0] - $m_dcEpochRef->[0])*86400 + ($dcRef->[1] - $m_dcEpochRef->[1]);  

		                			
				printf DEBUGFILE "%12.3f      %9.3f  %9.3f    %17.10e   %17.10e   %17.10e  \n",
				     $secs, $m_Sun_Pitch, $m_Sun_Roll, $bx, $by, $bz;                
		
			}
			
			$m_MsgCntr++;
			
			
		}   
	}

	return $m_Enabled;
}

# ========================================================
#  PostCompute Method
# ========================================================
sub PostCompute
{
	my $Result = $_[0];
	
	if( $m_DebugMode == true )
	{
		if( $m_Enabled == true )
		{
			Message( eLogMsgDebug, "PostCompute(): called" );
			
			# now close debug file
            close(DEBUGFILE);
		}
		else
		{
			Message( eLogMsgDebug, "PostCompute(): Disabled" );
		}
	}
	
	return $m_Enabled;
}


# ===========================================================
#  Free Method
# ===========================================================
sub Free(){
	if( $m_DebugMode == true )
	{
		if( $m_Enabled == true )
		{
			Message( eLogMsgDebug, "$m_Name.PostPropagate(): Enabled" );
 		}
		else
		{
			Message( eLogMsgDebug, "$m_Name.PostPropagate(): Disabled" );
		}
		
		Message( eLogMsgDebug, "$m_Name.Free(): MsgCntr( $m_MsgCntr )" );
	}

	$m_AgUtPluginSite 		= undef;
}

# The properties listed below have subroutines that pass Plugin data to and from ODTK
# =============================================================
#  Name Property
# =============================================================
sub GetName{
	return $m_Name;
}

sub SetName{
	$m_Name = $_[0];
}

# ============================================================
#  Enabled property
# ============================================================
sub GetEnabled{
	return $m_Enabled;
}

sub SetEnabled{
	$m_Enabled = $_[0];
}

# ======================================================
#  MsgStatus property
# ======================================================
sub GetDebugMode{
	return $m_DebugMode;
}

sub SetDebugMode{
    $m_DebugMode = $_[0];
}

# =======================================================
#  EvalMsgInterval property
# =======================================================
sub GetMsgInterval{
	return $m_MsgInterval;
}

sub SetMsgInterval{
	$m_MsgInterval = $_[0];
}

# ============================================================
#  DebugFileName property
# ============================================================
sub GetDebugFileName{
	return $m_DebugFileName;
}

sub SetDebugFileName{
	$m_DebugFileName = $_[0];
}

# ======================================================
#  DiffuseReflectivity property
# ======================================================
sub GetDiffuseReflectivity{
	return $m_diffuseReflectivity;
}

sub SetDiffuseReflectivity{
	$m_diffuseReflectivity = $_[0];
}

# ======================================================
#  SpecularReflectivity property
# ======================================================
sub GetSpecularReflectivity{
	return $m_specularReflectivity;
}

sub SetSpecularReflectivity{
	$m_specularReflectivity = $_[0];
}

# ======================
#  Register Method
# ======================
sub Register
{
	my $Result = $_[0];
		
	if( defined($Result) )
	{		 
		if( $m_DebugMode == true )
		{
			$Result->Message( eLogMsgInfo, "Register() called" );
		}
		
		$m_CrIndex1 = $Result->RegisterParameter("Cr1", 1.67, 1.0, 2.0, "Unitless");
		
		if($m_CrIndex1 > -1)
		{			
			if($m_CrIndex1 > -1)
			{
				if( $m_DebugMode == true )
				{
					$Result->Message( eLogMsgInfo, "Registered Cr1 as Unitless parameter");
				}
			}
			
			$m_CrIndex2 = $Result->RegisterParameter("Cr2", 1.75, 1.0, 2.0, "Unitless");
			if($m_CrIndex2 > -1)
			{
				if( $m_DebugMode == true )
				{
					$Result->Message( eLogMsgInfo, "Registered Cr2 as Unitless parameter");
				}
			}
			else
			{
				$Result->Message( eLogMsgAlarm, "Unable to register Cr2 as Unitless parameter");
			}
		}
		else
		{
			$Result->Message( eLogMsgAlarm, "Unable to register Cr1 as Unitless parameter");
		}
	}
}

# ======================================================
#  Error Model Status property
# ======================================================
sub GetErrorModel{
	return $m_ErrorModel;
}

sub SetErrorModel{
    $m_ErrorModel = $_[0];
}

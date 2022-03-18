# =====================================================
#   Copyright 2010, Analytical Graphics, Inc.          
# =====================================================

#########################################################################
#                                                                       #
#  N plate drag model.  This model uses a 6-sided cube, 20 m^2 per side #
#                                                                       #
#  Computes Reflectance and Partials in Body Coordinates                #
#                                                                       # 
#  This model solves for 1 scale parameter                              #
#                                                                       #
#                                                                       #
#  Note: Turning on the DebugMode will output accelerations to the file #
#        given by the DebugFile attribute. This attribute               #
#        only opens an existing file; it does not create a new file.    #
#        So to output Debug data this file needs to be created before   #
#        running the plugin. The default filename is                    #
#           'C:\Temp\Drag_Plugin_Debug.txt'                             #
#########################################################################


# This script requires Perl 5.8.0 or higher

#  This first section of code (before any of the subroutines or methods) is where all
#  the global variables are initialized
require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;
use IO::Handle;


# ==========================================
#  Reference Frames Enumeration
# ==========================================
use constant eInertial 		=> 0;
use constant eFixed 		=> 1;
use constant eLVLH 			=> 2;
use constant eNTC 			=> 3;
use constant eBodyFrame		=> 4;

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
#  AgEAttrAddFlags Enumeration
# ==========================================
use constant eFlagNone			=> 0;
use constant eFlagTransparent	=> 2;
use constant eFlagHidden		=> 4;
use constant eFlagTransient		=> 8;  
use constant eFlagReadOnly		=> 16;
use constant eFlagFixed			=> 32;

use constant PI => 3.1415926535897932384;
use constant false	=> 0;
use constant true	=> 1;

# ==========================================
#  Declare Global Variables
# ==========================================
#  These variables are the default values for the Plugin attributes 
#  that are displayed in ODTK.  The subroutines at the end of this
#  code are used to pass the values of these attributes back and forth.

my $m_Name				= "Drag.N-Plate_Cube.Perl";
my $m_AgUtPluginSite	= undef;
my $m_AgAttrScope		= undef;
my $m_CDIndex			= -1;
my $m_CD                = 2.02;

my $m_MsgCntr			= -1;
my $m_Enabled			= true;
my $m_DebugMode			= true;
my $m_MsgInterval		= 500;

my $dirpath = 'C:\Temp';
my $m_DebugFile = $dirpath.'\Drag_Plugin_Debug.txt';

my $m_dcEpochRef;
my $lastSec = -202020;

# Body frame accelerations
my $bx;
my $by;
my $bz;

# Global variables relating to modeling of satellite shape

my $Nplate = 6; # Box model

# Area of each plate - in this example they are all the same
my @Area;

# Define each unit vector by modeling our satellite as a 6-sided box
my @Nx;
my @Ny;
my @Nz;

sub Message
{
	my $severity = $_[0];
	my $msg = $_[1];
	
	if( defined($m_AgUtPluginSite) )
	{
		$m_AgUtPluginSite->Message( $severity, "$msg" );
	}
}

sub DebugMsg
{
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

sub writeVec{
	my $aVectorRef = $_[0];
	my $vecName = $_[1];
	printf DEBUGFILE "%10s   %20.15e   %20.15e   %20.15e\n", $vecName,
		$aVectorRef->[0], $aVectorRef->[1], $aVectorRef->[2];
}

sub writeMatrix{
	my $aVectorRef = $_[0];
	my $vecName = $_[1];
	printf DEBUGFILE "%10s   %20.15e   %20.15e   %20.15e\n", $vecName, $aVectorRef->[0], $aVectorRef->[1], $aVectorRef->[2];
	printf DEBUGFILE "             %20.15e   %20.15e   %20.15e\n",     $aVectorRef->[3], $aVectorRef->[4], $aVectorRef->[5];
	printf DEBUGFILE "             %20.15e   %20.15e   %20.15e\n",     $aVectorRef->[6], $aVectorRef->[7], $aVectorRef->[8];
}

sub outerProduct{
    #first vector is columns, 2nd vector is rows
    #
    # Result is:
    #
    #  ax*bx  ay*bx  az*bx
    #  ax*by  ay*by  az*by
    #  ax*bz  ay*bz  az*bz
    
	my $aVectorRef = $_[0];
	my $bVectorRef = $_[1];
	my $cVectorRef = $_[2];
	
	$cVectorRef->[0] = $aVectorRef->[0] * $bVectorRef->[0];
	$cVectorRef->[1] = $aVectorRef->[1] * $bVectorRef->[0];
	$cVectorRef->[2] = $aVectorRef->[2] * $bVectorRef->[0];
	$cVectorRef->[3] = $aVectorRef->[0] * $bVectorRef->[1];
	$cVectorRef->[4] = $aVectorRef->[1] * $bVectorRef->[1];
	$cVectorRef->[5] = $aVectorRef->[2] * $bVectorRef->[1];
	$cVectorRef->[6] = $aVectorRef->[0] * $bVectorRef->[2];
	$cVectorRef->[7] = $aVectorRef->[1] * $bVectorRef->[2];
	$cVectorRef->[8] = $aVectorRef->[2] * $bVectorRef->[2];
}

sub vecTimesMatrix{
    #
    # Input vector a:
    # ax,ay,az
    #
    # Input matrix b:
    # bxx bxy bxz
    # byx byy byz
    # bzx bzy bzz
    #
    # Result is vector:
    #
    #  ax*bxx+ay*byx+az*bzx
    #  ax*bxy+ay*byy+az*byz
    #  ax*bxz+ay*byz+az*bzz
    
	my $aVectorRef = $_[0];
	my $bVectorRef = $_[1];
	my $cVector = $_[2];
	
	#printf DEBUGFILE "vecXmat: a[0] = %20.15e\n", $aVectorRef->[0];
	#printf DEBUGFILE "vecXmat: b[0] = %20.15e\n", $bVectorRef->[0];
	#printf DEBUGFILE "vecXmat: c[0] = %20.15e\n", $cVector->[0];

	$cVector->[0] =    $aVectorRef->[0] * $bVectorRef->[0] +
					   $aVectorRef->[1] * $bVectorRef->[3] +
					   $aVectorRef->[2] * $bVectorRef->[6];

	$cVector->[1] =    $aVectorRef->[0] * $bVectorRef->[1] +
					   $aVectorRef->[1] * $bVectorRef->[4] +
					   $aVectorRef->[2] * $bVectorRef->[7];

	$cVector->[2] =    $aVectorRef->[0] * $bVectorRef->[2] +
					   $aVectorRef->[1] * $bVectorRef->[5] +
					   $aVectorRef->[2] * $bVectorRef->[8];
}

sub vecTimesMatrix2{
    #
    # Input vector a:
    # ax,ay,az
    #
    # Input matrix b:
    # bxx bxy bxz
    # byx byy byz
    # bzx bzy bzz
    #
    # Result is vector:
    #
    #  ax*bxx+ay*byx+az*bzx
    #  ax*bxy+ay*byy+az*byz
    #  ax*bxz+ay*byz+az*bzz
    
	my $aVectorRef = $_[0];
	my $bVectorRef = $_[1];
	my $cVector    = $_[2];
	
	#printf DEBUGFILE "vecXmat2: a[0] = %20.15e\n", $aVectorRef->[0];
	#printf DEBUGFILE "vecXmat2: b[0] = %20.15e\n", $bVectorRef->[0];
	#printf DEBUGFILE "vecXmat2: c[0] = %20.15e\n", $cVector->[0];

	$cVector->[0] =  $aVectorRef->[0] * $bVectorRef->[0] +
					 $aVectorRef->[1] * $bVectorRef->[3] +
					 $aVectorRef->[2] * $bVectorRef->[6];

	$cVector->[1] =  $aVectorRef->[0] * $bVectorRef->[1] +
					 $aVectorRef->[1] * $bVectorRef->[4] +
					 $aVectorRef->[2] * $bVectorRef->[7];

	$cVector->[2] =  $aVectorRef->[0] * $bVectorRef->[2] +
					 $aVectorRef->[1] * $bVectorRef->[5] +
					 $aVectorRef->[2] * $bVectorRef->[8];
}

sub computeDrag{
	my $Result = $_[0];
	my $cd = $_[1];
	my $incidentVecArrayRef = $_[2];   #Incident Vector onto Body

	#writeVec($incidentVecArrayRef, "Incident");

	# Copy incident vector to local vel vector; vel vector is in direction of motion,
	# opposite of returned vector which is in direction of force on satellite
	my @velVector;
	push @velVector, $incidentVecArrayRef->[0] * -1;
	push @velVector, $incidentVecArrayRef->[1] * -1;
	push @velVector, $incidentVecArrayRef->[2] * -1;

    my @posParts;
    push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;
    push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;
    push @posParts, 0.0; push @posParts, 0.0; push @posParts, 0.0;

    my @velParts;
    push @velParts, 0.0; push @velParts, 0.0; push @velParts, 0.0;
    push @velParts, 0.0; push @velParts, 0.0; push @velParts, 0.0;
    push @velParts, 0.0; push @velParts, 0.0; push @velParts, 0.0;

    my $x = 0;
    my $y = 0;
    my $z = 0;

    my $j = 0;
    my $k = 0;

	my $incidentDirPosPartialsArrayRef = $Result->IncidentDirectionBodyCompPosPartials_Array();
	my $incidentDirVelPartialsArrayRef = $Result->IncidentDirectionBodyCompVelPartials_Array();

    #all calcs in the for loop below are in the body frame
    for ($j = 0; $j <= $Nplate-1; $j++)
    {
        #take dot product of velocity vector and normal vector for each plate
        #(no need to divide by magnitudes, since both are unit vectors)

        my $dot = $velVector[0] * $Nx[$j] + $velVector[1] * $Ny[$j] + $velVector[2] * $Nz[$j];

		#printf DEBUGFILE "Plate, dot prod: %d  %20.15e\n", $j+1, $dot;

		if ($dot > 0.0)
		{
			#printf DEBUGFILE "Plate, dot prod: %d  %20.15e\n", $j+1, $dot;

			# Subtraction puts returned reflectance in direction of force, opposite velocity
			$x -= $Area[$j] * $dot * $Nx[$j];
			$y -= $Area[$j] * $dot * $Ny[$j];
			$z -= $Area[$j] * $dot * $Nz[$j];

			#printf DEBUGFILE "areas: %20.15e, %20.15e, %20.15e\n", $x, $y, $z;

			my $mult = $Area[$j] * $cd;
			
			#printf DEBUGFILE "multiplier: %20.15e, dot prod: %20.15e\n", $mult, $dot;
			
			my @nhat = ($Nx[$j], $Ny[$j], $Nz[$j]);

			# Add each plate's contribution to incidence position & velocity partials

			if(defined($incidentDirPosPartialsArrayRef))
			{
				my @pos1 = (0,0,0); 			# nhat * incident partials
				my @pos2 = (0,0,0,0,0,0,0,0,0); # pos1 * nhat

				#writeVec(\@nhat, "nhat");
				#writeMatrix($incidentDirPosPartialsArrayRef, "IncidentPs");

				vecTimesMatrix2(\@nhat, $incidentDirPosPartialsArrayRef, \@pos1);

				#writeVec(\@pos1, "pos1");

				outerProduct(\@pos1, \@nhat, \@pos2);

				#writeMatrix(\@pos2, "pos2");

				for ($k=0; $k<9; $k++)
				{
					$posParts[$k] += $mult * $pos2[$k];
				}
				#writeMatrix(\@posParts, "posParts1");
			}

			if(defined($incidentDirVelPartialsArrayRef))
			{
				my @vel1 = (0,0,0); 			# nhat * incident partials
				my @vel2 = (0,0,0,0,0,0,0,0,0); # vel1 * nhat
				vecTimesMatrix2(\@nhat, $incidentDirVelPartialsArrayRef, \@vel1);
				outerProduct(\@vel1, \@nhat, \@vel2);

				for ($k=0; $k<9; $k++)
				{
					$velParts[$k] += $mult * $vel2[$k];
				}
				#writeMatrix(\@velParts, "velParts1");
			}
		}
    } # end of loop over plates
    
	if($m_CDIndex > -1)
	{
	    #printf DEBUGFILE "CD partials: %20.15e, %20.15e, %20.15e\n", $x, $y, $z;
		$Result->SetReflectanceInBodyParamPartials($m_CDIndex, $x, $y, $z);
	}

	$bx = $x * $cd;
	$by = $y * $cd;
	$bz = $z * $cd;

	$Result->SetReflectanceInBody($bx, $by, $bz);
	
#	printf DEBUGFILE "Cd: %10.3f\nIncidence: %20.15e %20.15e %20.15e \n", 
#		$cd, $incidentVecArrayRef->[0], $incidentVecArrayRef->[1], $incidentVecArrayRef->[2];
               
	printf DEBUGFILE "reflectance: %20.15e, %20.15e, %20.15e\n", $bx, $by, $bz;


	# Set the Position Partials
	if(defined($incidentDirPosPartialsArrayRef))
	{
		#writeMatrix($incidentDirPosPartialsArrayRef, "IncBodyPPs");

		$Result->SetReflectanceBodyCompPosPartials(
					$posParts[0], $posParts[1], $posParts[2],
					$posParts[3], $posParts[4], $posParts[5],
					$posParts[6], $posParts[7], $posParts[8]);

		#writeMatrix(\@posParts, "OutBodyPPs");
	}

	# Set the Velocity Partials
	if(defined($incidentDirVelPartialsArrayRef))
	{
		#writeMatrix($incidentDirVelPartialsArrayRef, "IncBodyVPs");

		$Result->SetReflectanceBodyCompVelPartials(
					$velParts[0], $velParts[1], $velParts[2],
					$velParts[3], $velParts[4], $velParts[5],
					$velParts[6], $velParts[7], $velParts[8]);

		#writeMatrix(\@velParts, "OutBodyVPs");
	}

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
		
		#
		# "DragCoefficient" was added for STK as there is no way to
		# modify CD via GUI, but should be removed when using with ODTK
		# See BUG53691.
		#
#		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, 
#			"DragCoefficient", 
#			"Drag Coefficient", 
#			"DragCoefficient",     
#			eFlagNone );

		# ===========================
		#  General Plugin attributes
		# ===========================
		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, 
			"PluginEnabled",
			"If the plugin is enabled or has experienced an error", 
			"Enabled",    
			eFlagNone );

		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, 
			"DebugMode",
			"Turn debug messages on or off", 
			"DebugMode",    
			eFlagNone );

		$AgAttrBuilder->AddFileDispatchProperty  ( $m_AgAttrScope, 
			"DebugFile",
			"Debug output file", 
			"DebugFile", "", "*.txt",   
			eFlagNone );

		# ==============================
		#  Messaging related attributes
		# ==============================
		$AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, 
			"MessageInterval", 
			"The interval at which to send messages during propagation in Debug mode", 
			"MsgInterval",     
			eFlagNone );
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
	my $AgAsHpopPluginResult = $_[0];	# This is an IAgAsDragModelResult interface
	
    if( $m_Enabled == true )
	{
		if( defined($AgAsHpopPluginResult) )
		{
			# Assemble area of each plate and plate normal vectors in the body frame

			# Box Model (6-sided cube)

			@Area = (); # reset array
			push @Area, 20;   # m^2
			push @Area, 20;   # m^2
			push @Area, 20;   # m^2
			push @Area, 20;   # m^2
			push @Area, 20;   # m^2
			push @Area, 20;   # m^2

			@Nx = (); # reset array
			@Ny = (); # reset array
			@Nz = (); # reset array

			# Imagine X is facing you, Y points to the right, and Z points up.
			# Plate 1 - front face, +X
			push @Nx, 1;
			push @Ny, 0;
			push @Nz, 0;
			# Plate 2 - right side, +Y
			push @Nx, 0;
			push @Ny, 1;
			push @Nz, 0;
			# Plate 3 - top face, +Z
			push @Nx, 0;
			push @Ny, 0;
			push @Nz, 1;
			# Plate 4 - back face, -X
			push @Nx, -1;
			push @Ny, 0;
			push @Nz, 0;
			# Plate 5 - left side, -Y
			push @Nx, 0;
			push @Ny, -1;
			push @Nz, 0;
			# Plate 6 - bottom, -Z
			push @Nx, 0;
			push @Ny, 0;
			push @Nz, -1;

			if( $m_DebugMode == true )
			{
				Message(eLogMsgDebug, "$m_Name.PreCompute(): Enabled");

				# Open debug file and write header
				open(DEBUGFILE,">$m_DebugFile");
				#print DEBUGFILE "My N-Plate Drag Debug File \n";
				#print DEBUGFILE "Plugin Model: $m_Name \n";
				#print DEBUGFILE "Message Interval: $m_MsgInterval \n";

				my $dateArrayRef = $AgAsHpopPluginResult->Date_Array(eUTC);

				#printf DEBUGFILE "Epoch: %4d.%03d.%02d:%02d:%06.3f UTCG \n",
				#     $dateArrayRef->[0], $dateArrayRef->[1], 
				#     $dateArrayRef->[3], $dateArrayRef->[4], $dateArrayRef->[5]; 

				$m_dcEpochRef = $AgAsHpopPluginResult->DayCount_Array(eUTC);
				$lastSec = -202020;

				#print DEBUGFILE  "\n";
				#print DEBUGFILE "                                Acceleration in body frame\n";
				#print DEBUGFILE " Elapsed Time           X                     Y                  Z\n";
				#print DEBUGFILE "     (sec)          (m/sec^2)            ( m/sec^2)         ( m/sec^2)\n";
											
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
	my $Result = $_[0];	# This is an IAgAsDragModelResultEval interface
	
	my $cd = 0;

	if($m_Enabled == true && defined($Result) )
	{
		if($m_CDIndex > -1)
		{
			$cd = $Result->ParameterValue($m_CDIndex);
		}
	
		my $incidentVecArrayRef = $Result->IncidentDirectionInBody_Array();

		computeDrag($Result, $cd, $incidentVecArrayRef);	
		           
	    if( $m_Enabled != true  )
		{				
			DebugMsg("enabled fail")
		}
			
		if( $m_Enabled == true && $m_DebugMode == true && defined($Result) )
		{		
			
			if( ($m_MsgCntr % $m_MsgInterval) == 0)
			{
				my $dcRef = $Result->DayCount_Array(eUTC);  
				my $secs  = ($dcRef->[0] - $m_dcEpochRef->[0])*86400 + ($dcRef->[1] - $m_dcEpochRef->[1]);  

				#printf DEBUGFILE "%12.3f      %17.10e   %17.10e   %17.10e  \n",
				#    $secs, $bx, $by, $bz;

				#printf DEBUGFILE "incidentVecArrayRef: %17.10e  %17.10e  %17.10e\n",
				#	$incidentVecArrayRef->[0], $incidentVecArrayRef->[1], $incidentVecArrayRef->[2];                
		
				DEBUGFILE->autoflush(1);
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
	my $Result = $_[0];	# This is an IAgAsDragModelResult interface
	
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

# =======================================================
#  DragCoeff property
# =======================================================
sub GetDragCoefficient
{
	return $m_CD;
}

sub SetDragCoefficient
{
	$m_CD = $_[0];
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

# ======================================================
#  DebugFile property
# ======================================================
sub GetDebugFile
{
	return $m_DebugFile;
}

sub SetDebugFile
{
    $m_DebugFile = $_[0];
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

# ======================
#  Register Method
# ======================
sub Register
{
	my $Result = $_[0];	# This is an IAgAsDragModelResultRegister interface
		
	if( defined($Result) )
	{		 
		if( $m_DebugMode == true )
		{
			my $myMsg = "Register() called, cd = " . $m_CD;
			#$Result->Message( eLogMsgInfo, "Register() called" );
			$Result->Message( eLogMsgInfo, $myMsg );
		}
		
		$m_CDIndex = $Result->RegisterParameter("CD", $m_CD, 0.0, 20.0, "Unitless");
		
		if($m_CDIndex > -1)
		{
			if( $m_DebugMode == true )
			{
				$Result->Message( eLogMsgInfo, "Registered CD as Unitless parameter");
			}
		}
		else
		{
			$Result->Message( eLogMsgAlarm, "Unable to register CD as Unitless parameter");
		}
	}
}

use strict;
use warnings;
use Win32::OLE;
use XML::Parser;
use Data::Dumper;
use File::HomeDir;
use File::Spec::Functions;

#************************************************************************************************************
#Create a folder in user area to save scenario in
#************************************************************************************************************
my $docsFolder = File::HomeDir->my_documents;
my $scenarioFolder = $docsFolder . "\\STK 12\\TE_Automation_Tutorial";
unless (-d $scenarioFolder or mkdir $scenarioFolder) {
    die "unable to create $scenarioFolder";
}

#Use existing STK instance if already running, otherwise start new
my $STKapp;
if (! ($STKapp = Win32::OLE->GetActiveObject('STK12.Application'))){
    $STKapp = Win32::OLE->new('STK12.Application');
}

$STKapp->{Visible} = 1;
$STKapp->{UserControl} = 1;    # a zero will cause STK to exit when script exits
my $stkRoot=$STKapp->{Personality2};

#Create the scenario and import the ownship
my $cmd = "New / Scenario TE_Automation";
#print($cmd);
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

# Tell STK to put all the files in this directory
# Important for T&E or the generated .e will not end up with the scenario.
$cmd = 'SetDefaultDir / "' .$scenarioFolder . '"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

# Save now to make sure we can write to this new directory
$cmd = 'save / * "' .$scenarioFolder . '/"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);


#************************************************************************************************************
#Import all of the mappings that are going to be required for this scenario
#************************************************************************************************************
print("* Importing Mappings and Ownship Data\n");
$cmd = 'TE_Mapping * Import File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\TrainingAutomation.tedm" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#************************************************************************************************************
#Import our Ownship
#************************************************************************************************************
$cmd = 'TE_AnalysisObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Ownship.csv" Name "Ownship1" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#************************************************************************************************************
#Import the TSPI data for our target objects as associated objects
#************************************************************************************************************
print("* Importing TSPI data for Targets\n");
$cmd = 'TE_AssociatedObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Target1.csv" Name "Target1_TSPI" Ownship "Ownship1" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_AssociatedObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Target2.csv" Name "Target2_TSPI" Ownship "Ownship1" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_AssociatedObject * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Ship.csv" Name "Ship_TSPI" Ownship "Ownship1" ObjectType "Ship" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#************************************************************************************************************
#Import aditional data files that contain our subsystem measurements
#************************************************************************************************************
print("* Importing track files\n");
$cmd = 'TE_AdditionalData * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Air Tracks - Radar.csv" Ownship "Ownship1" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_AdditionalData * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Air Tracks - Track Event.csv" Ownship "Ownship1" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_AdditionalData * Add File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\Ship Tracks - RF.csv" Ownship "Ownship1" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'ConnectLog / On "C:\temp\connectlog.txt"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

print("* Creating ground targets and setting locations\n");
$cmd = 'NewMulti / */Target 4 Target1 Target2 Target3 Target4';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'SetPosition */Target/Target1 Geodetic 35.5429 -118.263 Terrain';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'SetPosition */Target/Target2 Geodetic 35.3837 -118.208 Terrain';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'SetPosition */Target/Target3 Geodetic 35.2823 -118.096 Terrain';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'SetPosition */Target/Target4 Geodetic 35.4346 -118.04 Terrain';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#************************************************************************************************************
#Creating flight segments that correspond for each system tested
#************************************************************************************************************
print("* Creating flight segments\n");
$cmd = 'TE_SegmentDefinition * Add Name "RADAR" StartTime "1 Mar 2017 20:56:16.000" StopTime "1 Mar 2017 20:58:30.000" Ownship "Ownship1" Subsystem "RADAR" ShowOnTimeline "On" Assets "*/Aircraft/Target1_TSPI,*/Aircraft/Target2_TSPI"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_SegmentDefinition * Add Name "RADAR" StartTime "1 Mar 2017 21:01:30.000" StopTime "1 Mar 2017 21:04:18.000" Ownship "Ownship1" Subsystem "RADAR" ShowOnTimeline "On" Assets "*/Aircraft/Target1_TSPI,*/Aircraft/Target2_TSPI"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_SegmentDefinition * Add Name "RADAR" StartTime "1 Mar 2017 21:10:39.075" StopTime "1 Mar 2017 21:13:09.349" Ownship "Ownship1" Subsystem "RADAR" ShowOnTimeline "On" Assets "*/Aircraft/Target1_TSPI,*/Aircraft/Target2_TSPI"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
print("* Importing flight segments from file\n");
$cmd = 'TE_SegmentDefinition * Import File "C:\Program Files\AGI\STK 12\Help\TeTraining\Training files\RF runs.txt" Ownship "Ownship1" Subsystem "RF" ShowOnTimeline "On" Name "RfRuns"'; #An example of importing from a runs file
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#************************************************************************************************************
#Creating Tracks, Track Promotion, and Track comparison
#************************************************************************************************************
$cmd = 'TE_Track * Add Name "Target1Track" AnalysisObject "Ownship1" Mapping "Sample_Track" TrackIDs "1001" TimePath "Scenario/TE_Automation RADAR_Run1 EventInterval"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_Track * Add Name "ShipTrack" AnalysisObject "Ownship1" Mapping "ShipTrack"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

print("* Promoting Tracks to heavy objects for further analysis\n");
$cmd = 'TE_Track * Promote Name "ShipTrack"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_Track * Points Name "Target1Track" Show "On" ColordisplayType "dynamic" ColorContourType "Smooth" SetParameter "SlantRangeToTarget1_TSPI" Units "nmi" MinValue "34.5" MaxValue "60.5" MinColor "green" MaxColor "red"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

print("* Performing Track Comparisons\n");
$cmd = 'TE_TrackComparison * Add Name "TrackCompareShip" AnalysisObject "Ownship1" Track "ShipTrack" TruthObject "Ship/Ship_TSPI" TruthPointing "StkObject" MeasuredObject "Aircraft/ShipTrack_Measured" ReferenceSystem "Aircraft/Ownship1 NorthWestUp System" TimePath "Aircraft/Ownship1 AvailabilityTimeSpan EventInterval"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#Don't have acces to install location so set appropriate directory
$cmd = 'TE_TrackComparison * Export Name "TrackCompareShip" File "'.$scenarioFolder.'\shipTrackCompare.csv"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#Track ID of four limits to one line of output as an example, if not specified all lines will be returned
$cmd = 'TE_TrackTraceability_RM * Name  "ShipTrack" ID "4"';
my $trace = $stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
PrintTrace($trace);

print("* Creating Quick Looks\n");
$cmd = 'TE_QuickLooks * Create From "Ownship1" To "Target1"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_QuickLooks * Angle Ownship "Ownship1" Target "Target1" Name "BodyAzimuth" Show "ON"';       # or NorthAzimuth
$cmd = 'TE_QuickLooks * Angle Ownship "Ownship1" Target "Target1" Name "BodyElevation" Show "ON"';     # or NorthElevation
$cmd = 'TE_QuickLooks * Vector Ownship "Ownship1" Target "Target1" Name "Range" Show "ON"';            # or North or BodyX
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'TE_QuickLooks * Create From "Ownship1" To "Target2"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
$cmd = 'TE_QuickLooks * Plane Ownship "Ownship1" Target "Target2" Name "BodyXY" Show "ON"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#************************************************************************************************************
# Add sensor and axes required for sensor quick look then run sensor quicklook
#************************************************************************************************************
print ("* Adding a sensor and performing a Sensor Quick Look\n");
$cmd = 'New / */Aircraft/Ownship1/Sensor Sensor1';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'CalculationTool * Aircraft/Ownship1/Sensor/Sensor1 Create Axes Sensor1_NativeAxes_01 "Fixed in Axes"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'TE_SensorQuickLooks * Create from "Sensor1" to "Target1_TSPI" Axes "Sensor1_NativeAxes_01" Frame "spherical"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'TE_SensorQuickLooks * Graph from "Sensor1" to "Target1_TSPI" Name "AzEl" Frame "spherical"';  # Overlay "ON"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'TE_SensorQuickLooks * Vector from "Sensor1" to "Target1_TSPI" Name "Boresight" Axes "Sensor1_NativeAxes_01" Show "ON"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

#************************************************************************************************************
#Create A vector, create a graph then save an image of the graph.
#************************************************************************************************************
print("* Creating a vector, a graph, and saving graph as an image\n");
$cmd = 'TE_Vector * Add Name "AirTracksVector" AnalysisObject "Ownship1" Mapping "AirTracksVector" Source "Air Tracks - Track Event.csv" Point Path "Center"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'TE_Graph * Add Name "OwnshipLatVsLon" AnalysisObject "Ownship1" GraphXY PointSize "Medium" Segment Color "%155000100" Point "1.0" Data Element "Ownship.csv|Longitude, Ownship.csv|Latitude" Labels "Longitude, Latitude" Units "deg, deg" Time Constraint "RADAR_Run1" ';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'TE_Graph * Save Name "OwnshipLatVsLon" AnalysisObject "Ownship1" File "'.$scenarioFolder.'\OwnshipLatVsLon.png"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);

$cmd = 'save / * "' .$scenarioFolder . '/"';
$stkRoot->ExecuteCommand($cmd);
Assert_Pass($cmd);
print("DONE!\n");

#$STKapp->Quit();

sub Assert_Pass
{
    my ($cmd) = $_[0];

    my $err = Win32::OLE->LastError();
    if ($err != "0") {
        print "failure command: ", $cmd, "\n";
        return 0;
    }
    return 1;
}

sub PrintTrace
{
    my ($res) = $_[0];
    my $inv = $res->Invoke("Item", 0);
    print $inv, "\n";
}



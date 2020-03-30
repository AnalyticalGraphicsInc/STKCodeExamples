'Nifty Scripting Tricks

'1. Close all graphs open in StaticProductBuilder: ----------------------------------------------------

'// -------- JavaScript:
	for( i=0 ; i<uiApp.windows.count ; i++ )
	{
	if( uiApp.windows(i).caption.match(/Graph Viewer/) )
	{
	uiApp.windows(i).Close()
	}
	}

'// --------- VBSCript:
	dim uiApp
	set uiApp = GetObject(, "ODTK.Application")
				:
				:
				:
	for each w in uiApp.windows
		if InStr(w.caption,"Graph Viewer") = 1 then
			w.Close
		end if
	next


'2. To add facility to tracking list -----------------------------------------------------------------
'
' to add a facility to a Filter.TrackerList
'
	dim xx
	set xx = ODTK.Scenario1.Filter1.TrackerList
	msgbox xx.size
	xx.insert(ODTK.Scenario1.Filter1.TrackerList.Choices(1))  

'3. Add to tracker list -------------------------------------------------------------------------------

	fac = "TrackingSystem1.Australia_177W_1st" set trkList = ODTK.Scenario(0).Filter(0).TrackerList
	set tlc = trkList.Choices
	msgbox tlc.size
	for a = 0 to tlc.size - 1
		if tlc(a) = fac then
			trkList.insert(tlc(a))
		end if
	next

'4. Loading Station position --------------------------------------------------------------------------
'In 5.0 this works:

	dim xxx, qqq, yyy
	set xxx = ODTK.Scenario1.TrackingSystem1
	set qqq = xxx.Children.Facility
	for each x in qqq
		set yyy = x.Position.Dereference.ToCartesian()
		yyy.cartesian.X.set xx, "m"
		yyy.cartesian.Y.set yy, "m"
		yyy.cartesian.Z.set zz, "m"
		x.Position = yyy
	next
	
'In 3.0, 4.0, and 5.0 this works:

	dim xxx, qqq, yyy
	set xxx = ODTK.Scenario1.TrackingSystem1
	set qqq = xxx.Children.Facility
	for each x in qqq
		set yyy = x.Position.ToCartesian()
		yyy.x = 10000.
		x.Position = yyy
	next

'This seems to work:

	set x = scen.GPS.PRN06.OrbitState.ToCartesian()
	x.CoordFrame = "Fixed"
	scen.GPS.PRN06.OrbitState = x

'The problem was that CoordFrame is not hooked to a function and just stores an integer in memory when you change it.
'You need to trigger a call to one of the conversion functions that read it do all necessary math.

'5. Retrieve the type of finite maneuver --------------------------------------------------------------

	("msgbox ODTK.Scenario(0).Satellite("FinManSat").ForceModel.FiniteManeuvers(0).name" does not work)

	msgbox ODTK.Scenario(0).Satellite("FinManSat").ForceModel.FiniteManeuvers(0).GetProp("name")

'6. Reset minimum elevation ---------------------------------------------------------------------------

	fac.MinElevation.Min.Set -90,"deg"
	fac.MinElevation.Set -90,"deg"

'7. Add an impulsive maneuver -------------------------------------------------------------------------

	dim iii
	iii = 0
	
	set yyy = ODTK.Scenario(0).Satellite(0).ForceModel
	set xxx = ODTK.Scenario(0).Satellite(0).ForceModel.InstantManeuvers

	set t = xxx.Choices(3)
	xxx.push_back t
	yyy.InstantManeuvers(iii).Epoch = od.NewDate(startTime, "UTCG")                 
	yyy.InstantManeuvers(iii).Name = cstr( "AutoMan" & iii )
	yyy.InstantManeuvers(iii).DeltaV.DeltaVX = rThrust
	yyy.InstantManeuvers(iii).DeltaV.DeltaVY = iThrust
	yyy.InstantManeuvers(iii).DeltaV.DeltaVZ = cThrust
	yyy.InstantManeuvers(iii).Uncertainty.Type = "ByComponent"
	yyy.InstantManeuvers(iii).Uncertainty.XSigma = rSigma
	yyy.InstantManeuvers(iii).Uncertainty.YSigma = iSigma
	yyy.InstantManeuvers(iii).Uncertainty.ZSigma = cSigma

'8. Add a finite maneuver -----------------------------------------------------------------------------

	dim iii
	iii = 0		' index into maneuvers starts with zero
	
	set xxx = ODTK.Scenario(0).Satellite(0).ForceModel
	set t = ODTK.Scenario(0).Satellite(0).ForceModel.FiniteManeuvers.Choices(1)
	
	xxx.FiniteManeuvers.push_back t

	xxx.FiniteManeuvers(iii).Time.StartTime = od.NewDate(startTime, "ModJDate")

'	if the date is given in UTCG as a character string (e.g. 6 June 2005 00:00:00.000) then use the line:
'	xxx.FiniteManeuvers(iii).Time.StartTime = od.NewDate(startTime, "UTCG")
                 
	xxx.FiniteManeuvers(iii).Time.StopMode = "TimeSpan"
	xxx.FiniteManeuvers(iii).Time.TimeSpan = tSpan
	xxx.FiniteManeuvers(iii).Name = mnvrID 
	xxx.FiniteManeuvers(iii).Thrust.ThrustX = rThrust
	xxx.FiniteManeuvers(iii).Thrust.ThrustY = iThrust
	xxx.FiniteManeuvers(iii).Thrust.ThrustZ = cThrust
	xxx.FiniteManeuvers(iii).Mass.Isp = 360

'	to use permanent maneuver states, assign the name of the permanent maneuver:
'	xxx.FiniteManeuvers(iii).StateAssociation = "XIPS_N1"

'9. Add to Custom Tracking Schedule entry: ------------------------------------------------------------

	set sim = ODTK.Scenario(0).Simulator(0)
	sim.CustomTrackingIntervals.Enabled = true
	Set list = sim.CustomTrackingIntervals.Schedule
	list.clear()
	for each fac in rcvrs.Children.Facility
		Set newElem = list.NewElem ()
	
		newElem.Enabled = true
		newElem.Satellites = "All Satellites"
		newElem.Trackers   = "Specific Tracker"
	
		newElem.SelectedTrackingStrand = cstr("* - " + sat1.name + " - " + sat2.name + " - " + rcvrs.name+"."+fac.name)

' 		if there are 73 inclusion intervals:

		for ii = 0 to 72
			set startTime = tt.AddTime(ODTK.NewQuantity(ii*deltime,"min"))
			set endTime = startTime.AddTime(ODTK.NewQuantity(interval,"min"))
'
'			add a new interval to existing element
'
			Set newInt = newElem.InclusionIntervals.NewElem()
			newInt.Enabled = true
			newInt.start = startTime
			newInt.stop  = endTime
			newElem.InclusionIntervals.insert(newInt)
				
		next

		list.push_back(newElem)
	
	next ' each receiver
'9a. To set a specific satellite: ---------------------------------------------------------------------

		newElem.Satellites = "Specific Satellite"

		For Each satObj In ODTK.Children.Scenario(0).Children.Satellite
				If satObj.name = sat Then
					newElem.SelectedSatellite = satObj
				End If
		Next
'9b. To add filter custom data editing schedule items

		set t1 = ODTK.NewDate("13 Jan 2012 19:00:00.000","UTCG")
		dim fil
		set fil = ODTK.Scenario(0).Filter(0)
		fil.CustomDataEditing.Enabled = false
		set list = fil.CustomDataEditing.Schedule

		set newElem = list.NewElem()
		list.push_back(newElem)

		set newInt = list(0).Intervals.NewElem()

		newInt.Enabled = true
		newInt.start = t1
		newInt.stop = t2

		list(0).Intervals.insert(ints)


'10. ' copy orbit from one sat to another -------------------------------------------------------------
 
ODTK.Scenario(0).Satellite("LS_Satellite1").OrbitState = ODTK.Scenario(0).Satellite("Copy_of_LS_Satellite1").OrbitState

msgbox "done"

'11. Set tracker measurement statisitcs ---------------------------------------------------------------

' set measurement statistics
dim ii, xxx, yyy, zzz, x, y, z, nn
set xxx = ODTK.Scenario(0).Children.TrackingSystem
for each x in xxx
   set yyy = x.Children.Facility
   for each y in yyy
      if(y.name = "BE_NW") then
      nn = y.MeasurementStatistics.size -1
        for ii = 0 to nn
	if(y.MeasurementStatistics(ii).Type.Type = "Range" ) then
		y.MeasurementStatistics(ii).Type.WhiteNoiseSigma = 5  ' 5 m
		y.MeasurementStatistics(ii).Type.BiasSigma = 50       ' 50 m
		y.MeasurementStatistics(ii).Type.Bias = 0             ' 0 m
		y.MeasurementStatistics(ii).Type.BiasHalfLife = 28800 ' 20 days
	end if
	if(y.MeasurementStatistics(ii).Type.Type = "Doppler" ) then
		y.MeasurementStatistics(ii).Type.WhiteNoiseSigma = 5  ' 5 cm/sec
		y.MeasurementStatistics(ii).Type.BiasSigma = 50       ' 50 cm/sec
		y.MeasurementStatistics(ii).Type.Bias = 0             ' 0 cm/sec
		y.MeasurementStatistics(ii).Type.BiasHalfLife = 28800 ' 20 days
	end if
	if(y.MeasurementStatistics(ii).Type.Type = "Azimuth" ) then
		y.MeasurementStatistics(ii).Type.WhiteNoiseSigma = .005 ' .005 deg
		y.MeasurementStatistics(ii).Type.BiasSigma = .010       ' .01 deg
		y.MeasurementStatistics(ii).Type.Bias = 0               ' 0 deg
		y.MeasurementStatistics(ii).Type.BiasHalfLife = 28800   ' 20 days
	end if
	if(y.MeasurementStatistics(ii).Type.Type = "Elevation" ) then
		y.MeasurementStatistics(ii).Type.WhiteNoiseSigma = .005 ' .005 deg
		y.MeasurementStatistics(ii).Type.BiasSigma = .010       ' .01 deg
		y.MeasurementStatistics(ii).Type.Bias = 0               ' 0 deg
		y.MeasurementStatistics(ii).Type.BiasHalfLife = 28800   ' 20 days
	end if
        next
      end if
   next
next
'msgbox "done"

'12. load LS Stage - first stage without drag - second stage with drag --------------------------------

dim ns
ODTK.Scenario(0).Satellite("Target").LeastSquares("LeastSquares1").Stages.clear()

set ns = ODTK.Scenario(0).Satellite("Target").LeastSquares("LeastSquares1").Stages.NewElem()
ns.StartTime = ODTK.NewDate( "1 Jul 2008 00:00:00","UTCG")
ns.StopTime = ODTK.NewDate( "2 Jul 2008 00:00:00","UTCG")
ns.EstimateBCoeff = false
ns.DataFrequency = ODTK.NewQuantity( 1, "sec" )
ns.MaxIterations = 20
ns.SigmaEdit = false
set unused = ODTK.Scenario(0).Satellite("Target").LeastSquares("LeastSquares1").Stages.push_back( ns )

set ns = ODTK.Scenario(0).Satellite("Target").LeastSquares("LeastSquares1").Stages.NewElem()
ns.StartTime = ODTK.NewDate( "1 Jul 2008 00:00:00","UTCG")
ns.StopTime = ODTK.NewDate( "2 Jul 2008 00:00:00","UTCG")
ns.EstimateBCoeff = true
ns.DataFrequency = ODTK.NewQuantity( 1, "sec" )
ns.MaxIterations = 20
ns.SigmaEdit = false
set unused = ODTK.Scenario(0).Satellite("Target").LeastSquares("LeastSquares1").Stages.push_back( ns )

'13. Convert  sat state to Kepler elements, get a few numbers and convert back ------------------------

set yy = Scen.Satellite("Target").OrbitState.ToKeplerian()
	smax = yy.SemiMajorAxis
	eccen = yy.Eccentricity
	Scen.Satellite("Target").OrbitState = yy	
set yy = Scen.Satellite("Target").OrbitState.ToCartesian()

'14. Refresh static product builder -------------------------------------------------------------------

set pb = ODTK.Application.ProductBuilder
set dp = pb.DataProducts
set e1 = dp.NewElem()
e1.Name = "Some stuff 2"
dp.push_back e1

pb.SaveDataProductList "C:\temp\test.dpl"

for each w in uiApp.windows
	if InStr(w.caption,"Static Product") = 1 then
	w.Close
end if

next
pb.LoadDataProductList "C:\temp\test.dpl"
pb.GenerateProduct(0) ' Does nothing but causes Static product builder to reload

'15. Loading obs file into scenario

'
' load tracking data file names for first N days
'
for index = 1 to num_days
zzz = cstr("C:\Documents and Settings\rhujsak\My Documents\ODTK 5 GPS\TrackingData\JanMeas\"&yy&"00"&Index&"0.05o")
  If (fso.FileExists(zzz)) Then
   set meas = yyy.newElem()
   meas.Enabled = true
   meas.Filename = zzz
   yyy.push_back(meas)
  else
   msgbox "no tracking data for "&yy& " on day "&index 
  end if
next

'16. Replace double quote with null

replace (stringContents, chr(34), "")

'17. Replace single quote with double quote

Const ForReading = 1
Const ForWriting = 2

Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.OpenTextFile("C:\Scripts\Test.txt", ForReading)

strText = objFile.ReadAll
objFile.Close

strOldText = Chr(39)	'single quote
strNewText = Chr(34)	'double quote

strNewText = Replace(strText, strOldText, strNewText)

Set objFile = objFSO.OpenTextFile("C:\Scripts\Test.txt", ForWriting)
objFile.WriteLine strNewText
objFile.Close

'18. List all files in a folder:

On Error Resume Next
  Dim fso, folder, files, NewsFile,sFolder
  
  Set fso = CreateObject("Scripting.FileSystemObject")
  sFolder = Wscript.Arguments.Item(0)
  If sFolder = "" Then
      Wscript.Echo "No Folder parameter was passed"
      Wscript.Quit
  End If
  Set NewFile = fso.CreateTextFile(sFolder&"\FileList.txt", True)
  Set folder = fso.GetFolder(sFolder)
  Set files = folder.Files
  
  For each folderIdx In files
    NewFile.WriteLine(folderIdx.Name)
  Next
  NewFile.Close
  
'19. List all files in folder (second version)

    Set fsoFolder = CreateObject("Scripting.FileSystemObject")
    Set folder = fsoFolder.GetFolder("C:\temp")
    Set files = folder.Files
    for each objFile in files
	    sName = objFile.Name
	    sCreated = objFile.DateCreated
	    sAccessed = objFile.DateLastAccessed
	    sModified = objFile.DateLastModified
	    sSize = objFile.Size
	    sType = objFile.Type
    	
    	
	    msgbox sName & " " & sCreated & " " & sAccessed & " "&
	    sModified & " " & sSize & " " & sType & "!"
    	
    next
    set files = nothing
    set folder = nothing
    set fsoFolder = nothing
    
' 20.   Kill all orphaned AgUiApplication.exe processes in Task Manager
'       Kill AgUiApplication.exe running in Task Manager
'           *** may need to be modified to recognize when "AgUiApplication.exe" 
'               is listed as "AGUIAP~<n>.exe", n=1,2,3,4 or some other abbreviation. ***
'
'       Google search on "ExecQuery Win32_Process" for JavaScript variant'
'
    Dim objProcess

    strComputer = "."
    set objWMIService = GetObject("winmgmts:{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")
    set colProcessList = objWMIService.ExecQuery("SELECT * FROM Win32_Process WHERE  Name = 'AgUiApplication.exe'")
    set colProcessList2 = objWMIService.ExecQuery("SELECT * FROM Win32_Process WHERE  Name = 'AGUIAP~1.exe'")
    set colProcessList3 = objWMIService.ExecQuery("SELECT * FROM Win32_Process WHERE  Name = 'AGUIAP~2.exe'")
    set colProcessList4 = objWMIService.ExecQuery("SELECT * FROM Win32_Process WHERE  Name = 'AGUIAP~3.exe'")

    for each objProcess in colProcessList
     objProcess.Terminate()
    next
    
    for each objProcess in colProcessList2
     objProcess.Terminate()
    next
    
    for each objProcess in colProcessList3
     objProcess.Terminate()
    next
    
    for each objProcess in colProcessList4
     objProcess.Terminate()
    next
'
' 21. Insert selected MeasTypes in filter or simulator
'
    fil.MeasTypes.clear()
    fil.MeasTypes.Insert( "1W Bistatic Range" )
    fil.MeasTypes.Insert( "P1 Pseudo-range" )
'
' 22. Set up orbit state, assume smax, etc are already loaded previously
'
set yy = Scen.Satellite("Target").OrbitState.ToKeplerian()
	yy.SemiMajorAxis = smax
	yy.Eccentricity = eccen
	yy.TrueArgofLatitude = targ
	yy.Inclination = incl
	yy.RAAN = raan
	yy.ArgofPerigee = argp
	Scen.Satellite("Target").OrbitState = yy	
'
' 23. load ODTK and set app properties
'
    dim uiApp
    dim ODTK
    set uiApp = CreateObject("ODTK.Application")
    set ODTK  = uiApp.personality

  	UiApp.visible = true
    uiApp.UserControl = True
'
' 24. Load Satellite list with 24 GPS Satellites
'
    dim i, yyy
    set xxx = ODTK.Scenario(0).GPSConstellation("GPSIII")
    for i = 0 to 23
    yyy = xxx.GPSSatellite(i).name 
    ODTK.Scenario(0).Filter("Filter1").SatelliteList.InsertByName  "GPSIII." & yyy 
    next
'
' 25. Load Reference Clocks for compoaite clock estimation
'
'   load all clocks (as an example)
'
'   ground clocks are <trackingsystem name>.<facility name>.<receiver name>
'   satellite clocks are <constellation name>.<satellite name>
'
    dim gname, satname, clockName
    gname = "GPSIII"
    for each sat in ODTK.Scenario(0).GPSConstellation(gname).Children.GPSSatellite
        satname = sat.name
        clockName = gname& "." & satName
        ODTK.Scenario(0).GPSConstellation(gname).SVEstimatedStates.ReferenceClocks.Insert clockName
    next
    for each trk in ODTK.Scenario(0).Children.TrackingSystem
        for each fac in trk.Children.Facility
            for each rcvr in fac.Children.GPSReceiver
                clockName = trk.name &"."& fac.name &"."& rcvr.name
                ODTK.Scenario(0).GPSConstellation(gname).SVEstimatedStates.ReferenceClocks.Insert clockName
            next
        next
    next
'
'   create STK scenario start, stop, and epoch times
'
	dim stkApp, STK
	set stkApp = CreateObject("STK.Application")
	set STK = stkApp.Personality
	
	StartTime = <dd mmm yyyy hh:mm:ss.sssssss>
	StopTime = <dd mmm yyyy hh:mm:ss.sssssss>
	
    cmdText = "New / Scenario " & SName
    STK.ExecuteCommand (cmdText)
	
    cmdText = "SetTimePeriod * "& chr(34)& StartTime &chr(34) &" "&  chr(34) & StopTime & chr(34)
    STK.ExecuteCommand (cmdText)
    
    cmdText = "SetEpoch * " & chr(34) & StartTime & chr(34)
    STK.ExecuteCommand (cmdText)
'--------------------------------------------------------------------------------------------------------
' 26. Change default coordinate system for each facility

    for each fac in ODTK.Scenario(0).TrackingSystem(0).Children.Facility
        fac.Position.DisplayCoordFlag = "Cartesian"
    next
    msgbox "done"
'--------------------------------------------------------------------------------------------------------
' 27. Change default coordinate system for each satellite

    For each sat in ODTK.Scenario(0).Children.Satellite
        sat.OrbitState.DisplayCoordFlag = "Keplerian"
    next
    msgbox "done"
----------------------------------------------------------------------------------------------------------
' 28. convert single line into multiple lines, split on ))

Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.OpenTextFile("C:\Documents and Settings\rhujsak\My Documents\TrackingData\SSN\TransmitB3\03Z12APR.TXT", 1)

strText = objFile.ReadAll
objFile.Close

strOldText = "))"	
strNewText = Chr(13)& Chr(10) & "))"	

strNewText = Replace(strText, strOldText, strNewText)

'   output file must exist

Set objFile = objFSO.OpenTextFile("C:\Documents and Settings\rhujsak\My Documents\TrackingData\SSN\TransmitB3\03Z12APR.TOBS", 2)
objFile.WriteLine strNewText
objFile.Close
'--------------------------------------------------------------------------------------------------------
' add an extension to every file name in a folder
'
strComputer = "."

Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\cimv2")

directory = "C:\Documents and Settings\rhujsak\My Documents\OD5.2\OrbitalScenarios\NSS9\NS9\Tracking Data\Tracking Data\RawAngles"

Set colFiles = objWMIService.ExecQuery _
    ("ASSOCIATORS OF {Win32_Directory.Name='" & directory & "'} Where " _
        & "ResultClass = CIM_DataFile")

For Each objFile In colFiles
    strExtension = objFile.Extension 
    strExtension = "rawAzEl"
    strNewName = objFile.Drive & objFile.Path & objFile.FileName & objFile.Extension & "." & strExtension
    errResult = objFile.Rename(strNewName)
Next
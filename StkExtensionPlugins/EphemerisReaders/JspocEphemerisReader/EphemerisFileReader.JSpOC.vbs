'======================================================
'  Copyright 2012, Analytical Graphics, Inc.          
' =====================================================

' this script will read in ephemeris in the following JSpOC format
' author: jens ramrath, agi
' date: 11 may 2017
' 
' CLASSIFICATION: UNCLASSIFIED
' 36508 CALIBRATION REFERENCE EPHEMERIS
' TIME OF LAST MODIFICATION TO FILE: 17 122 21 37 29.000
' EPHEMERIS FILE START TIME:         17 113 00 00 00.000
' EPHEMERIS FILE STOP TIME:          17 119 00 00 00.000
' EARTH MODEL: EGM-96
' COORDINATE SYSTEM: EARTH CENTERED ROTATING (ECR)
' 
'                       X(KM)          Y(KM)          Z(KM)
' YYDDDHHMMSS.SSS   XDOT(KM/SEC)   YDOT(KM/SEC)   ZDOT(KM/SEC)
' ---------------  -------------  -------------  ------------
' 17113000000.000   -6253.236965    2634.397410   -2080.465272
'                       2.314169      -0.137836      -7.165113
' 17113000100.000  -6102.040858    2620.226707   -2505.893872
'                       2.723752      -0.334965      -7.011165





'==================================
' Log Msg Type Enumeration
'==================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm
eLogMsgDebug        = 0
eLogMsgInfo         = 1
eLogMsgForceInfo    = 2
eLogMsgWarning      = 3
eLogMsgAlarm        = 4


'==========================================
' AgEAsEphemFileDistanceUnit
'==========================================
Dim eAsEphemFileDistanceUnitUnknown, eAsEphemFileDistanceUnitMeter, eAsEphemFileDistanceUnitKilometer, eAsEphemFileDistanceUnitKiloFeet, eAsEphemFileDistanceUnitFeet, eAsEphemFileDistanceUnitNautMile
eAsEphemFileDistanceUnitUnknown   = -1
eAsEphemFileDistanceUnitMeter     = 0
eAsEphemFileDistanceUnitKilometer = 1
eAsEphemFileDistanceUnitKiloFeet  = 2
eAsEphemFileDistanceUnitFeet      = 3
eAsEphemFileDistanceUnitNautMile  = 4

'==========================================
' AgEAsEphemFileTimeUnit
'==========================================
Dim eAsEphemFileTimeUnitUnknown, eAsEphemFileTimeUnitSecond, eAsEphemFileTimeUnitMinute, eAsEphemFileTimeUnitHour, eAsEphemFileTimeUnitDay
eAsEphemFileTimeUnitUnknown       = -1
eAsEphemFileTimeUnitSecond        = 0
eAsEphemFileTimeUnitMinute        = 1
eAsEphemFileTimeUnitHour          = 2
eAsEphemFileTimeUnitDay           = 3

'==========================================
' AgEAsEphemInterpolationMethod
'==========================================
Dim eAsEphemInterpolationMethodUnknown, eAsEphemInterpolationMethodLagrange, eAsEphemInterpolationMethodHermite, eAsEphemInterpolationMethodLagrangeVOP
eAsEphemInterpolationMethodUnknown     = -1
eAsEphemInterpolationMethodLagrange    = 0
eAsEphemInterpolationMethodHermite     = 1
eAsEphemInterpolationMethodLagrangeVOP = 2

'==========================================
' AgEAsCovRep
'==========================================
Dim eAsCovRepUnknown, eAsCovRepStandard, eAsCovRepRIC
eAsCovRepUnknown  = -1
eAsCovRepStandard = 0
eAsCovRepRIC      = 1

'================================
' Declare Global Variables
'================================
Dim m_AgUtPluginSite
Const m_extension = ".ecr"

'======================
' Init Function
'======================
Function Init( Site )

    Set m_AgUtPluginSite = Site
    Init = True

End Function

'======================
' Register Method
'======================
Sub Register( Result )

    If ( Not Result Is Nothing ) Then
        result.FormatID = "EphemerisFileReader.JSpOC"
        result.Name = "JSpOC ECF Ephemeris Reader"
        Call result.AddFileExtension ( m_extension )
    End If

End Sub

'======================
' ReadEphemeris Method
'======================
Sub ReadEphemeris( Result )
	' ##### READ HEADER #####
	' add later if it becomes necessary. For now assume fixed frame
	
	
	' ##### READ DATA #####
    Dim fileSystemObject, textReader, fileLines, fileLine, numPointsRead, elements
    Dim foundFirstLine
    Dim t, x, y, z, vx, vy, vz

    If ( Not Result is Nothing ) Then
		' SETTING
        Result.CentralBody = "Earth"
        Result.CoordinateSystem = "Fixed"
        Result.InterpolationMethod = eAsEphemInterpolationMethodLagrange
        Result.InterpolationOrder = 5
        Result.CovarianceRepresentation = eAsCovRepRIC

        Call Result.SetUnits( eAsEphemFileDistanceUnitKilometer, eAsEphemFileTimeUnitSecond )

        Set fileSystemObject = CreateObject("Scripting.FileSystemObject")
        Set textReader = fileSystemObject.OpenTextFile( Result.Filename )
        fileLines = Split(textReader.ReadAll(), vbLf)

        lineNumber = 0
        numPointsRead = 0
        foundFirstLine = False

	    Dim oRegExp1, oRegExp2, oMatches

		Set oRegExp1 = new RegExp
		oRegExp1.pattern = "([+-]?\d{11}\.\d{3})\s+([+-]?\d+\.\d+)\s+([+-]?\d+\.\d+)\s+([+-]?\d+\.\d+)\s*"
		
		
		Set oRegExp2 = new RegExp
		oRegExp2.pattern = "\s+([+-]?\d+\.\d+)\s+([+-]?\d+\.\d+)\s+([+-]?\d+\.\d+)\s*"

        ' LOOP THROUGH ALL LINES
        For Each thisLine in fileLines

			' look for first data line
			if foundFirstLine = False then
				set oMatches = oRegExp1.Execute(thisLine)

				if oMatches.Count = 1 then
					t = oMatches(0).submatches(0)
					x = oMatches(0).submatches(1)
					y = oMatches(0).submatches(2)
					z = oMatches(0).submatches(3)

					foundFirstLine = True
				end if
				
			else
				' look for 2nd line of datacmd
				set oMatches = oRegExp2.Execute(thisLine)

				if oMatches.Count = 1 then
					vx = oMatches(0).submatches(0)
					vy = oMatches(0).submatches(1)
					vz = oMatches(0).submatches(2)

				end if
				
				' convert time from YYDDDHHMMSS.SSS to ISO format
				dt = "20" & Left(t, 2) & "-" & Mid(t, 3, 3) & "T" & Mid(t, 6, 2) & ":" & Mid(t, 8, 2) & ":" & Right(t, 9)

                added = Result.AddEphemerisAtEpoch( "ISO-YD", dt, x, y, z, vx, vy, vz, Array() )
               	foundFirstLine = False
               	
            end if

        Next

        Call m_AgUtPluginSite.Message( eLogMsgInfo, "VBScript Data Report Reader read " & numPointsRead & " points" )

    End If

End Sub

'======================
' ReadMetaData Method
'======================
Sub ReadMetaData( Result )

    If ( Not Result is Nothing ) Then
        'Call Result.AddMetaData("My Metadata", "Custom")
    End If

End Sub

'=================
' Verify Method
'=================
Sub Verify( Result )

    Dim fileSystemObject, textReader, secondLine

    If ( Not Result is Nothing ) Then

        Set fileSystemObject = CreateObject("Scripting.FileSystemObject")

        If Not StrComp("." & fileSystemObject.GetExtensionName( Result.Filename ), m_extension, 1) = 0 Then
            Result.IsValid = False
            Result.Message = "File must have a .ecr extension"
            Return
        End If

        Set textReader = fileSystemObject.OpenTextFile( Result.Filename )
        textReader.ReadLine()
        secondLine = textReader.ReadLine()
        textReader.close()

'        If InStr(1, secondLine, "ECF Position & Velocity") = 0 Then
'            Result.IsValid = False
'            Result.Message = "File doesn't seem to be an ECF Position & Velocity Data Report"
'        End If

    End If

End Sub

'=================
' Free Method
'=================
Sub Free()

    Set m_AgUtPluginSite = Nothing

End Sub

'======================================================
'  Copyright 2012, Analytical Graphics, Inc.          
' =====================================================

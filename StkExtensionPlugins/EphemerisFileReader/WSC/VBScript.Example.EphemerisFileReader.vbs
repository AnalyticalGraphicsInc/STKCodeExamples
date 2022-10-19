'======================================================
'  Copyright 2012, Analytical Graphics, Inc.          
' =====================================================

'==========================================
' AgELogMsgType Enumeration
'==========================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm
eLogMsgDebug         = 0
eLogMsgInfo          = 1
eLogMsgForceInfo     = 2
eLogMsgWarning       = 3
eLogMsgAlarm         = 4

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
Const m_extension = ".txt"

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
        result.FormatID = "VBScript.Example.EphemerisFileReader"
        result.Name = "VBScript ECF Report Reader"
        Call result.AddFileExtension ( m_extension )
    End If

End Sub

'======================
' ReadEphemeris Method
'======================
Sub ReadEphemeris( Result )

    Dim fileSystemObject, textReader, fileLines, fileLine, numPointsRead, elements
    Dim headerRead, separatorRead, readThisLine, lineNumber, added
    Dim d, x, y, z, vx, vy, vz

    If ( Not Result is Nothing ) Then

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
        added = False

        ' Go through each line in the data report and parse it
        For Each fileLine in fileLines
            elements = Split(fileLine, "    ")
            lineNumber = lineNumber + 1
            If lineNumber > 6 And UBound(elements) >= 6 Then
                d = elements(0)
                x = CDbl(elements(1))
                y = CDbl(elements(2))
                z = CDbl(elements(3))
                vx = CDbl(elements(4))
                vy = CDbl(elements(5))
                vz = CDbl(elements(6))

                added = Result.AddEphemerisAtEpoch( "UTCG", d, x, y, z, vx, vy, vz, Array() )
                If added Then
                    numPointsRead = numPointsRead + 1
                Else
                    Call m_AgUtPluginSite.Message( eUtLogMsgWarning, "Could not read point #" & numPointsRead)
                End If
            End If
        Next

        Call m_AgUtPluginSite.Message( eLogMsgInfo, "VBScript Data Report Reader read " & numPointsRead & " points" )

    End If

End Sub

'======================
' ReadMetaData Method
'======================
Sub ReadMetaData( Result )

    If ( Not Result is Nothing ) Then
        Call Result.AddMetaData("My Metadata", "Custom")
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
            Result.Message = "File must have a .txt extension"
            Return
        End If

        Set textReader = fileSystemObject.OpenTextFile( Result.Filename )
        textReader.ReadLine()
        secondLine = textReader.ReadLine()
        textReader.close()

        If InStr(1, secondLine, "ECF Position & Velocity") = 0 Then
            Result.IsValid = False
            Result.Message = "File doesn't seem to be an ECF Position & Velocity Data Report"
        End If

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

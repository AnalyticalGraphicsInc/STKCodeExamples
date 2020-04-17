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
        result.FormatID = "JplHorizonsFileReader"
        result.Name = "JPL Horizons Ephemeris Reader"
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
        Result.CoordinateSystem = "ICRF"
        Result.InterpolationMethod = eAsEphemInterpolationMethodLagrange
        Result.InterpolationOrder = 5
        Result.CovarianceRepresentation = eAsCovRepRIC

        Call Result.SetUnits( eAsEphemFileDistanceUnitKilometer, eAsEphemFileTimeUnitSecond )

        Set fileSystemObject = CreateObject("Scripting.FileSystemObject")
        Set textReader = fileSystemObject.OpenTextFile( Result.Filename )
        fileLines = Split(textReader.ReadAll(), vbLf)


		'''''''''''''''''''''''''''''''''''''''''''
		' split data into header, data and footer '
		'''''''''''''''''''''''''''''''''''''''''''
		Dim inHeader, inData, inFooter
		inHeader = True
		inData = False
		inFooter = False

		Dim header()
		Dim data()
		Dim footer()
		Dim counter
		counter = 0

		for each thisLine in fileLines
			if inHeader then
				if InStr(thisLine, "$$SOE") > 0 then
					inHeader = False
					inData = True
					counter = 0
				else
					ReDim preserve header(counter)
					header(counter) = thisLine
					counter = counter + 1
				end if
			elseif inData then
				if InStr(thisLine, "$$EOE") > 0 then
					inData = False
					inFooter = True
					counter = 0
				else
					ReDim preserve data(counter)
					data(counter) = thisLine
					counter = counter + 1
				end if
			elseif inFooter then
				ReDim preserve footer(counter)
				footer(counter) = thisLine
				counter = counter + 1
			end if
		next


		'''''''''''''
		' read data '
		'''''''''''''
		' read header and footer at some later point
        numPointsRead = 0
        added = False
		

        ' Go through each line in the data report and parse it
        For i = 0 to UBound(data)
			' sample
			' 2457801.500000000, A.D. 2017-Feb-17 00:00:00.0000, -1.813063023405156E+07,  9.932122175138062E+06, -1.715085602021687E+06,  1.593256237411539E+01, -8.753392121129698E+00,  1.447695166819649E+00,
			' 2457801.541666667, A.D. 2017-Feb-17 01:00:00.0000, -1.807328045127538E+07,  9.900615006963653E+06, -1.709873607417260E+06,  1.592842932344187E+01, -8.750591988577961E+00,  1.447857331684521E+00,

			' split line
			thisLineSplit = Split(Trim(data(i)), ",")
			dateSplit = Split(thisLineSplit(1), "-")
			timeSplit = Split(dateSplit(2))

			d = Trim(timeSplit(0)) & " " & dateSplit(1) & " " & Right(Trim(dateSplit(0)), 4) & " " & Trim(timeSplit(1))

			x = Trim(thisLineSplit(2))
			y = Trim(thisLineSplit(3))
			z = Trim(thisLineSplit(4))
			vx = Trim(thisLineSplit(5))
			vy = Trim(thisLineSplit(6))
			vz = Trim(thisLineSplit(7))

			added = Result.AddEphemerisAtEpoch( "TDBG", d, x, y, z, vx, vy, vz, Array() )
			If added Then
				numPointsRead = numPointsRead + 1
			Else
				Call m_AgUtPluginSite.Message( eUtLogMsgWarning, "Could not read point #" & numPointsRead)
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

    End If

End Sub

'=================
' Free Method
'=================
Sub Free()

    Set m_AgUtPluginSite = Nothing

End Sub
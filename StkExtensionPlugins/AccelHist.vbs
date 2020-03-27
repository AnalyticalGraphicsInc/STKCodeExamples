' this script takes a report containing 
' Astrogator Maneuver Ephemeris Block History - Top - Epoch
' Astrogator Maneuver Ephemeris Block History - Top - Time
' Astrogator Maneuver Ephemeris Block History - Maneuver - Thrust_Vector_X
' Astrogator Maneuver Ephemeris Block History - Maneuver - Thrust_Vector_Y
' Astrogator Maneuver Ephemeris Block History - Maneuver - Thrust_Vector_Z
' Astrogator Maneuver Ephemeris Block History - Maneuver - Total_Mass
'
' and reformats it into an accesHist file. Note that Epoch is expected to be in UTCG and Time in EpSec
' 
' author: jens ramrath
' date: 17 Mar 2020


' read the report
set oArgs = wscript.arguments
path = oArgs(0)
set oArgs = nothing
fileArray = getFileToArray(path)



dataStr = ""
epochTime = ""
epochSec = 0
counter = 0
headerSize = 9

' loop through report
for i = 0 to ubound(fileArray)
	' ignore header
	if i > headerSize then
		' ignore blank
		if Trim(fileArray(i)) <> "" then

			' remove empty elements
			for j = 1 to 10
				fileArray(i) = Replace(fileArray(i), "  ", " ")
			next
		
			' split line assemble parts
			thisLineSplit = Split(Trim(fileArray(i)), " ")			

			timeString  = thisLineSplit(0) - epochSec
			epochString = thisLineSplit(1) & " " & thisLineSplit(2) & " " & thisLineSplit(3) & " " & thisLineSplit(4)
			accX		= thisLineSplit(5)/thisLineSplit(8)
			accY		= thisLineSplit(6)/thisLineSplit(8)
			accZ		= thisLineSplit(7)/thisLineSplit(8)

      		if i = headerSize + 1 then
      			epochTime = epochString
      			epochSec = timeString
      			timeString = 0
      		end if

      		dataStr = dataStr & timeString & " " & accX & " " & accY & " " & accZ & vbcrlf
      		
      		counter = counter + 1
		end if
	end if
next



' write everything to report
' header
fileStr = "stkv4.3" & vbcrlf
fileStr = fileStr & "BEGIN AccelHistory" & vbcrlf
fileStr = fileStr & "NumberOfEphemerisPoints		" & counter & vbcrlf
fileStr = fileStr & "ScenarioEpoch			" & epoch & vbcrlf
fileStr = fileStr & "CoordinateSystem		        J2000" & vbcrlf
fileStr = fileStr & "InterpolationOrder		        1" & vbcrlf
fileStr = fileStr & "EPHEMERISTIMEACC" & vbcrlf

' data
fileStr = fileStr & dataStr

' footer
fileStr = fileStr & "End AccelHistory" & vbcrlf



writeStrToFile path, fileStr

' the end




' ***** read report and write it to array *****
Function getFileToArray(path)
   set fso = createObject("Scripting.FileSystemObject")
   set fIn = fso.OpenTextFile(path, 1)
   i = 0
   while not fIn.AtEndOfStream
      redim preserve fileArray(i)
      fileArray(i) = fIn.ReadLine
      i = i + 1
   wend
   set fIn = nothing
   getFileToArray = fileArray
End Function


' ***** write everything to report *****
Sub writeStrToFile(path,string) 'saves a string to the given path
   dim fso, f
   set fso = CreateObject("Scripting.FileSystemObject")
   set f = fso.CreateTextFile(path)

   f.write string

   set f = nothing
   set fso = nothing
End Sub

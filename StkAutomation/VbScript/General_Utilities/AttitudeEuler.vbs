' this script will take the "Euler Angles" report and generate an attitude
' file from it. The script will set the epoch to be the date of the first data
' point. It expects a gregorian date format
'
' author: jens ramrath
' date: 30 Sep 2009, updated 1/15/21

set oArgs = wscript.arguments
path = oArgs(0)
set oArgs = nothing
fileArray = getFileToArray(path)

fileLength = UBound(fileArray)
numberOfDataPoints = fileLength - 6

' get time of first point
firstData = trim(fileArray(6))
firstDataArray = split(firstData, " ")
firstDate = firstDataArray(0) & " " & firstDataArray(1) & " " & firstDataArray(2) & " " & firstDataArray(3)

firstDateValue = DateValue(firstDataArray(0) & " " & firstDataArray(1) & " " & firstDataArray(2))
firstTimeValue = split(firstDataArray(3), ":")
firstHour = firstTimeValue(0)
firstMins = firstTimeValue(1)
firstSecs = firstTimeValue(2)


' write header
fileStr = "stk.v.5.0" & vbcrlf
fileStr = fileStr & vbcrlf
fileStr = fileStr & "BEGIN Attitude" & vbcrlf
fileStr = fileStr & "ScenarioEpoch             " & firstDate & vbcrlf
fileStr = fileStr & "NumberOfAttitudePoints    " & numberOfDataPoints & vbcrlf
fileStr = fileStr & "InterpolationOrder        1" & vbcrlf
fileStr = fileStr & "CentralBody               Earth" & vbcrlf
fileStr = fileStr & "CoordinateAxes        	  J2000" & vbcrlf
fileStr = fileStr & "Sequence                  313" & vbcrlf
fileStr = fileStr & vbcrlf
fileStr = fileStr & "AttitudeTimeEulerAngles" & vbcrlf
fileStr = fileStr & vbcrlf


' write data
for i = 6 to ubound(fileArray)
   ' get time difference
   thisData = trim(fileArray(i))
   thisDataArray = split(thisData, " ")

   thisDateValue = DateValue(thisDataArray(0) & " " & thisDataArray(1) & " " & thisDataArray(2))
   thisTimeValue = split(thisDataArray(3), ":")
   thisHour = thisTimeValue(0)
   thisMins = thisTimeValue(1)
   thisSecs = thisTimeValue(2)

   diffDays = DateDiff("s", firstDateValue, thisDateValue)
   diffHour = thisHour - firstHour
   diffMins = thisMins - firstMins
   diffSecs = thisSecs - firstSecs

   totalDiff = diffDays + diffHour*3600 + diffMins*60 + diffSecs

   y = 99999
   p = 99999
   r = 99999
   for j = 4 to UBound(thisDataArray)
      if StrComp(thisDataArray(j), "") <> 0 then
         if y = 99999 then
            y = thisDataArray(j)
         elseif p = 99999 then
            p = thisDataArray(j)
         else
         	r = thisDataArray(j)
         end if
      end if
   next

   fileStr = fileStr & vbTab & totalDiff & vbTab & y & vbTab & p & vbTab & r & vbcrlf

next


' write footer
fileStr = fileStr & vbcrlf
fileStr = fileStr & "END Attitude" & vbcrlf   


' write everything to report
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

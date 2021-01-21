' to use this script use a regular access report. This script will compute
' the access times per day. This script currently only works for access from
' one object to one other, not multiple in the same report. 
'
' Date needs to be in Gregorian format, time needs to be in seconds
' 
' author: jens ramrath
' date: 8 Jan 2010


set oArgs = wscript.arguments
path = oArgs(0)
set oArgs = nothing
fileArray = getFileToArray(path)


' ***** header *****
' cust copy date
fileStr = fileArray(0) & vbcrlf

' change report name
fromToName = split(trim(fileArray(1)), ":")
fileStr = fileStr & fromToName(0) & ":  Access Duration Per Day" & vbcrlf

' copy from and to object names
for i = 2 to 5
	fileStr = fileStr & fileArray(i) & vbcrlf
next

' column header
fileStr = fileStr & vbcrlf
fileStr = fileStr & "   day         access duration (sec) " & vbcrlf
fileStr = fileStr & "  -----       -----------------------" & vbcrlf


' ***** access times *****
' initialize things
Set whichMonth = CreateObject("Scripting.Dictionary")
whichMonth.add "Jan", 1
whichMonth.add "Feb", 2
whichMonth.add "Mar", 3
whichMonth.add "Apr", 4
whichMonth.add "May", 5
whichMonth.add "Jun", 6
whichMonth.add "Jul", 7
whichMonth.add "Aug", 8
whichMonth.add "Sep", 9
whichMonth.add "Oct", 10
whichMonth.add "Nov", 11
whichMonth.add "Dec", 12


' use UK date format which most closely matches STK dateTime format
setLocale(2057)

thisLineSplit = splitDataLine(fileArray(8))
prevTimeSplit = split(trim(thisLineSplit(4)), ".")
prevDateTime = CDate(thisLineSplit(1) & "/" & whichMonth(thisLineSplit(2)) & "/" & thisLineSplit(3)) & " " & prevTimeSplit(0)
prevSec = prevTimeSplit(1)
prevDur = 0.0

for i = 8 to ubound(fileArray) - 7
   thisLineSplit = splitDataLine(fileArray(i))
   ' start time
   thisStartTimeSplit = split(trim(thisLineSplit(4)), ".")
   thisStartDateTime = CDate(thisLineSplit(1) & "/" & whichMonth(thisLineSplit(2)) & "/" & thisLineSplit(3)) & " " & thisStartTimeSplit(0)
   thisStartSec = thisStartTimeSplit(1)
   ' stop time
   thisStopTimeSplit = split(trim(thisLineSplit(8)), ".")
   thisStopDateTime = CDate(thisLineSplit(5) & "/" & whichMonth(thisLineSplit(6)) & "/" & thisLineSplit(7)) & " " & thisStopTimeSplit(0)
   thisStopSec = thisStopTimeSplit(1)



   ' if this data is not for prevDay then write it to file
   if DateValue(prevDateTime) <> DateValue(thisStartDateTime) then
      fileStr = fileStr & DateValue(prevDateTime) & "    " & prevDur & vbcrlf
      prevDateTime = thisStartDateTime
      prevDur = 0.0
   end if
   
   
   ' *** option 1: start and stop time are on the same day
   if DateValue(thisStartDateTime) = DateValue(thisStopDateTime) then
      prevDur = prevDur + thisLineSplit(9)
   end if
   
   
   ' *** option 2: start day and stop day are not on the same day
   if DateValue(thisStartDateTime) <> DateValue(thisStopDateTime) then
      ' figure out how much time goes with 1st day
      midnight = CDate(DateAdd("d", 1, DateValue(thisStartDateTime)) & " 00:00:00")
      secsThisDay = DateDiff("s", thisStartDateTime, midnight)
      secsThisDay = secsThisDay - CDbl("0." & CStr(thisStartSec))

      prevDur = prevDur + secsThisDay
      
      fileStr = fileStr & DateValue(prevDateTime) & "    " & prevDur & vbcrlf
	  prevDateTime = thisStopDateTime


      ' if there are days between ist and last add them here
      numDays = DateDiff("d", DateValue(thisStartDateTime), DateValue(thisStopDateTime))
      if numDays > 2 then
         for j = 2 to numDays
         	thisDate = CDate(DateAdd("d", j - 1, DateValue(thisStartDateTime)))
            fileStr = fileStr & DateValue(thisDate) & "    86400.000" & vbcrlf
         next
      end if
      
      
      ' figure out how much time goes with last day
      midnight = CDate(DateValue(thisStopDateTime) & " 00:00:00")
      secsThisDay = DateDiff("s", midnight, thisStopDateTime)
      secsThisDay = secsThisDay + CDbl("0." & CStr(thisStopSec))
	        
      prevDur = secsThisDay
   end if
   
next

' write out remainder
fileStr = fileStr & DateValue(prevDateTime) & "    " & prevDur & vbcrlf


writeStrToFile path, fileStr




Function splitDataLine(dataString)
	Dim returnArray(9)
	
	' access number
	splitData1 = split(trim(dataString), " ", 2)
	returnArray(0) = splitData1(0)
	
	' start Date
	splitData2 = split(trim(splitData1(1)), " ", 5)
	returnArray(1) = splitData2(0)		' day
	returnArray(2) = splitData2(1)		' month
	returnArray(3) = splitData2(2)		' year
	returnArray(4) = splitData2(3)		' time
	
	' stop Date
	splitData3 = split(trim(splitData2(4)), " ", 5)
	returnArray(5) = splitData3(0)		' day
	returnArray(6) = splitData3(1)		' month
	returnArray(7) = splitData3(2)		' year
	returnArray(8) = splitData3(3)		' time
	
	' duration
	returnArray(9) = trim(splitData3(4))

	splitDataLine = returnArray
End Function



sub writeStrToFile(path,string) 'saves a string to the given path
   dim fso, f
   set fso = CreateObject("Scripting.FileSystemObject")
   set f = fso.CreateTextFile(path)

   f.write string

   set f = nothing
   set fso = nothing
end sub



function getFileToArray(path)
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
end function

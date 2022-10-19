Set objFSO = CreateObject("Scripting.FileSystemObject") 

'
' this is the temp file used by STK
'
inputfile = WScript.Arguments.Item(0)

'
' open/read/close the file
'
Set objFile = objFSO.OpenTextFile(inputfile, 1)
Dim inData()
i = 0
Do Until objFile.AtEndOfStream
	Redim Preserve inData(i)
	inData(i) = objFile.ReadLine
	i = i + 1
Loop
objFile.Close

'
' open the new file (overwite input file) for writing
'
i = 1
Set objFile = objFSO.CreateTextFile(inputfile, 1)
For Each line in inData
	' do something with the data
	objFile.Write i & " " & line & vbCrLf
	i = i + 1
Next

'
' close the output file
'
objFile.Close

'
' when the script ends, STK will disply whatever is in the inputfile
'
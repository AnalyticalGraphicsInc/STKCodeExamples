echo off

REM
REM this is the temp file used by STK
REM
set inputfile=%1   
set inputfile=%inputfile:"=%

REM
REM create temp file
REM
set tmpfile=%1".tmp"
echo % >> %tmpfile%

REM
REM loop through input file - pushing edits to output file
REM
set x=1
setlocal EnableDelayedExpansion
for /f "delims=|" %%i in (%inputfile%) do (
	echo !x! %%i >> %tmpfile%
	set /a x=!x!+1
	)

REM
REM replace input with temp file
REM
copy /A %tmpfile% %inputfile%

REM
REM delete temp file
REM
del %tmpfile%
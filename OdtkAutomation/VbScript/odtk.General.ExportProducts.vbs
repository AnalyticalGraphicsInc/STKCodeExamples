Dim uiApp
Dim ODTK
Const ppLayoutText = 12

Set uiApp = GetObject(, "ODTK.Application")
Set ODTK = uiApp.Personality
 
 'run all filters'
for each fil in ODTK.Scenario(0).Children.Filter
	'fil.Go()
Next

'run all smoothers'
for each smooth in ODTK.Scenario(0).Children.Smoother
	'smooth.Go()
Next

'get todays date string for folder name ex: 2015-03-14
months = Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")

nowDate = CDate(Now())
twoDaysAgoDate = CDate(Date-2)

nowYear = DatePart("yyyy", nowDate)
nowMonth = string(2 - Len(DatePart("m", nowDate)), "0") & DatePart("m", nowDate)
nowDay = string(2 - Len(DatePart("d", nowDate)), "0") & DatePart("d", nowDate)
todayFolderName =   nowYear & "-" & nowMonth & "-" & nowDay

twoDaysAgo = DatePart("d", twoDaysAgoDate) & " " & months(DatePart("m", twoDaysAgoDate)-1) & " " & DatePart("yyyy", twoDaysAgoDate) & " 00:00:00"

shiftEnd = DatePart("d", nowDate) & " " & months(DatePart("m", nowDate)-1) & " " & DatePart("yyyy", nowDate) & " 20:00:00"

workingDir = "C:\ProgramData\AGI\ODTK 6\Output\"
todayDir = workingdir & "\" & todayFolderName & "\" & ODTK.Scenario(0).Name & "\"

Set objPPT = CreateObject("PowerPoint.Application")

Set objPresentation = objPPT.presentations.Add

Set WshShell = CreateObject("Wscript.Shell")
myDocumentsPath = WshShell.RegRead("HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders\Personal")
objPresentation.ApplyTemplate myDocumentsPath & "\ODTK 6\Styles\comspocSlideMaster.pptx"

'add Title slide
lastSlide = objPresentation.Slides.Count
nextSlide = LastSlide +1
Set objSlide = objPresentation.Slides.Add(nextSlide, 32)
'edit slide title
objSlide.Shapes.Title.TextFrame.TextRange.Text = ODTK.Scenario(0).Satellite(0).Description & " OD Results"
objSlide.Shapes(2).TextFrame.TextRange.Text = nowYear & "-" & nowMonth & "-" & nowDay


Set objFSO = CreateObject("Scripting.FileSystemObject")

If NOT objFSO.FolderExists(workingdir ) Then
	objFSO.CreateFolder(workingdir )
end if

If NOT objFSO.FolderExists(workingdir & "\" & todayFolderName) Then
	objFSO.CreateFolder(workingdir & "\" & todayFolderName)
end if

If NOT objFSO.FolderExists(todayDir) Then
	objFSO.CreateFolder(todayDir)
end if

for each Product in ODTK.ProductBuilder.DataProducts

	if instr(Product.Name, "Today") > 0 OR instr(Product.Name, "today") > 0 Then
		Product.Inputs.TimePeriod.Enabled = true 		
		Product.Inputs.TimePeriod.StartTime = ODTK.NewDate(twoDaysAgo, "UTCG")	
		Product.Inputs.TimePeriod.StopTime = ODTK.NewDate(shiftEnd, "UTCG")
	end if

	if Product.Name = "F Residual Ratios" OR _ 
		Product.Name = "F Residual Ratios_6" OR _ 
		Product.Name = "F Residual Ratios Today" OR _ 
		Product.Name = "F Position Uncertainty" OR _ 
		Product.Name = "S Position Uncertainty" OR _ 
		Product.Name = "D Position Consistency" OR _ 
		Product.Name = "D Velocity Consistency" OR _ 
		Product.Name = "S Mnvr Total Thrust" OR _ 
		Product.Name = "S Mnvr % and Dir"  OR _
		Product.Name = "Measurement Values" OR _ 
		Product.Name = "F SRP" OR _ 
		Product.Name = "F Range Residuals" OR _ 
		Product.Name = "F Transponder Bias" OR _ 
		Product.Name = "F Ballistic Coeff" OR _ 
		Product.Name = "D SRP Consistency" then
		
			'Set graph products to export and not display
			Product.Outputs.Display = false
			Product.Outputs.Export.Enabled = true
			Product.Outputs.Export.Format = "PNG"
			Product.Outputs.Export.DestinationType = "File"
			Product.Outputs.Export.GraphSize.X = 1098
			Product.Outputs.Export.GraphSize.Y = 547
			
			exportFileName = todayDir & Product.Name & ".png"
			Product.Outputs.Export.FileName = exportFileName
			ODTK.ProductBuilder.GenerateProduct(Product.Name)
			Product.Outputs.Export.Enabled = false
			Product.Outputs.Display = true
			
			'add new slide, '
			lastSlide = objPresentation.Slides.Count
			nextSlide = LastSlide +1

			Set objSlide = objPresentation.Slides.Add(nextSlide, 16)
			'add picture and set scale to 120% '
			set pic = objSlide.Shapes.AddPicture(exportFileName, false, true, 75,75,-1,-1)
			pic.Line.Weight = 4
			pic.Line.ForeColor.RGB = RGB(204,229,255)
			'edit slide title
			objSlide.Shapes.Title.TextFrame.TextRange.Text = Mid(Product.Name, 2 )

	end if
next 

'save presentation'
objPresentation.SaveAs(todayDir & ODTK.Scenario(0).Name & "_Results.ppt")
objPPT.Quit
'msgbox "Done"

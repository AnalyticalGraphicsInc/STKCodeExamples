Dim uiApp 
Set uiApp = GetObject(,"ODTK6.Application")

Dim ODTK
Set ODTK = uiapp.Personality

ODTK.ProductBuilder.LoadDataProductList("C:\Users\jreicher\Documents\ODTK 6\Styles\Default3.dpl")

'get todays date string for folder name ex: 2015-03-14
months = Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")

nowDate = CDate(Now())
twoDaysAgoDate = CDate(Date-2)
twoDaysAgo = DatePart("d", twoDaysAgoDate) & " " & months(DatePart("m", twoDaysAgoDate)-1) & " " & DatePart("yyyy", twoDaysAgoDate) & " 00:00:00"
shiftEnd = DatePart("d", nowDate) & " " & months(DatePart("m", nowDate)-1) & " " & DatePart("yyyy", nowDate) & " 20:00:00"

for each product in ODTK.Application.ProductBuilder.DataProducts


	if instr(product.Name, "Today") > 0 OR instr(product.Name, "today") > 0 Then
		product.Inputs.TimePeriod.Enabled = true 		
		product.Inputs.TimePeriod.StartTime = ODTK.NewDate(twoDaysAgo, "UTCG")	
		product.Inputs.TimePeriod.StopTime = ODTK.NewDate(shiftEnd, "UTCG")
	end if

	if Left(product.Name, 1) = "F" then
		test =  ODTK.Scenario(0).Filter(0).Output.DataArchive.Filename
		product.Inputs.DataSources(0).Filename = test 'ODTK.Scenario(0).Filter(0).Output.DataArchive.Filename
		product.Inputs.DataSources(0).Enabled = true
	end if

	if Left(product.Name, 1) = "S" then
		test =  ODTK.Scenario(0).Smoother(0).Output.DataArchive.Filename
		product.Inputs.DataSources(0).Filename =test
		product.Inputs.DataSources(0).Enabled = true
	end if

	if Left(product.Name, 1) = "D" then
		test = ODTK.Scenario(0).Smoother(0).Output.FilterDifferencingControls.Filename
		product.Inputs.DataSources(0).Filename = test
		product.Inputs.DataSources(0).Enabled = true
	end if	
next 

ODTK.Application.ProductBuilder.SaveDataProductList(ODTK.Application.CurrentScenarioPath & "\" & ODTK.Scenario(0).Name & ".dpl")

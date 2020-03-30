Dim uiApp 
'   Create AGI Application object
Set uiApp = GetObject(,"ODTK6.Application")

Dim ODTK
Set ODTK = uiapp.Personality

directory = "C:\Users\jreicher\Documents\ODTK 6\DataArchive\"
scenarioName = ODTK.Scenario(0).Name

for each product in ODTK.Application.ProductBuilder.DataProducts

   product.Inputs.DataSources.Clear()



next 
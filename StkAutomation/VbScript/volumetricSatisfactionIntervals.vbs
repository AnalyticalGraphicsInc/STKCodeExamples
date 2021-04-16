''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Variables
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Dim volumetricName
volumetricName = "Volumetric1"

Dim intervalListName
intervalListName = "ShipUncertaintyAccess"

Dim timeStep
timeStep = 1  'sec


'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Script 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Get open instance of STK and grab scenario
Dim app
Set app = GetObject(, "STK.Application")
Dim root
set root = app.Personality2
Dim scen
Set scen = root.CurrentScenario

' Grab volumetric object and recompute
Dim volumetric
Set volumetric = root.GetObjectFromPath("*/Volumetric/" + volumetricName)
volumetric.Compute()

' Get satisfaction data providers from volumetric
root.UnitPreferences.SetCurrentUnit "DateFormat", "EpSec"
Dim satisfactionData
Set satisfactionData = volumetric.DataProviders("Satisfaction Volume").Exec(scen.StartTime, scen.StopTime, timeStep)

' Isolate times and percent satisfied
Dim times 
times = satisfactionData.DataSets.GetDataSetByName("Time").GetValues()
Dim satisfied
satisfied = satisfactionData.DataSets.GetDataSetByName("Percent Satisfied").GetValues()

' Set counting variables and empty arrays
Dim i
i = 0
Dim intervalCount
intervalCount = 0
Dim previous
' Initialize array, assign bogus value to denote no change
ReDim intervalArray(0)
intervalArray(0) = -1

' For each value returned in the data provider, check for nonzero. 
' Store times where the satisfaction switches from 0 to nonzero and nonzero to 0. Edge cases taken care of using If statements
For Each value in satisfied
    If (i = 0) Then
        previous = value
        If (previous <> 0) Then
            intervalArray(0) = times(i)
        End If
        i = i + 1
    Else 
        If (previous = 0) And value <> 0 Then
            If (intervalArray(0) = -1) Then
                intervalArray(0) = times(i)
            Else 
                ReDim Preserve intervalArray(UBound(intervalArray) + 1)
                intervalArray(UBound(intervalArray)) = times(i)

            End If
        ElseIf (previous <> 0) And (value = 0) Then
            ReDim Preserve intervalArray(UBound(intervalArray) + 1)
            intervalArray(UBound(intervalArray)) = times(i)
        End If
        previous = value
        i = i + 1
    End If
Next

If scen.Vgt.EventIntervalLists.Contains(intervalListName) Then
    scen.Vgt.EventIntervalLists.Remove(intervalListName)
End If

Dim intervals
Set intervals = scen.Vgt.EventIntervalLists.Factory.CreateEventIntervalListFixed(intervalListName, "Volumetric satisfaction interval")
intervals.SetIntervals(intervalArray)
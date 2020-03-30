Imports System.Windows.Forms
Imports AGI.Ui.Plugins

Public Class CustomGUI
    Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl

    Private m_pEmbeddedControlSite As AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite
    Private m_odtk As Object
    Private m_uiPlugin As CustomDataEditUiPlugin.CustomDataEditCopy
    Private m_bFilter As Boolean
    Private m_bLS As Boolean
    Private m_filNames(10) As String
    Private m_lsNames(10) As String
    Private m_nFilters As Integer = 0
    Private m_nLS As Integer = 0
    Private m_cdeSource As String
    Private m_cdeTargtes(10) As String
    Private m_bCDElist As Boolean

    Public Function GetIcon() As stdole.IPictureDisp Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.GetIcon
        Return Nothing
    End Function

    Public Sub OnClosing() Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.OnClosing

    End Sub

    Public Sub OnSaveModified() Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.OnSaveModified

    End Sub

    Public Sub SetSite(ByVal Site As AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite) Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.SetSite
        m_pEmbeddedControlSite = Site
        m_uiPlugin = TryCast(m_pEmbeddedControlSite.Plugin, CustomDataEditUiPlugin.CustomDataEditCopy)
        m_odtk = m_uiPlugin.ODRoot
        ResetLists()
    End Sub

    Public Sub ResetLists()
        CheckedListBox1.ClearSelected()
        CheckedListBox1.Items.Clear()

        CheckedListBox2.ClearSelected()
        CheckedListBox2.Items.Clear()

        m_bCDElist = RadioButton1.Checked
        DebugMsg("rb1 checked?: " & RadioButton1.Checked)
        ' Configure data
        Dim nChildren As Integer = m_odtk.Children.Count

        If nChildren = 0 Then
            m_odtk.WriteMessage("You need to load a scenario to use this capability", "debug")
            SetStatus("You need to load a scenario to use this capability")
            Return
        End If

        Dim scen As Object = m_odtk.Scenario(0)
        Dim cdeSchedule As Object = GetCDESchedule(scen.name.Value)

        ' If the scenario has a CDE schedule, add it to the Source list
        If (m_bCDElist) Then
            If (cdeSchedule.count > 0) Then
                CheckedListBox1.Items.Add(scen.name.Value, False)
            End If

            ' Add it to the Target list
            CheckedListBox2.Items.Add(scen.name.Value, False)
        End If

        Dim nScChildren As Integer = scen.Children.Count

        '
        ' The following section of code iterates all of the children classes.
        '
        Array.Clear(m_filNames, 0, m_filNames.Length)
        Array.Clear(m_lsNames, 0, m_lsNames.Length)
        m_nFilters = 0
        m_nLS = 0

        For i As Integer = 0 To nScChildren - 1

            Dim childClass As Object = scen.Children(i)
            Dim className As String = childClass.Name.Value
            DebugMsg(className)

            If (String.Compare(className, "Filter", True) = 0) Then
                ' Iterate the filters to see how many there are and get their names
                For k As Integer = 0 To childClass.Count - 1
                    Dim filObj As Object = childClass(k)
                    DebugMsg("name: " & filObj.Name.Value)
                    Try
                        If (m_nFilters >= m_filNames.Length) Then
                            DebugMsg("redim filter: " & m_filNames.Length)
                            ReDim Preserve m_filNames(m_filNames.Length + 10)
                        End If
                        m_filNames(m_nFilters) = filObj.Name.Value
                        m_nFilters = m_nFilters + 1
                    Catch e As Exception
                        m_odtk.WriteMessage("array set error", "debug")
                    End Try

                    Try
                        If filObj.CustomDataEditing.Schedule.Count > 0 Then
                            DebugMsg(filObj.Name.Value & " has CDE")
                        Else
                            DebugMsg(filObj.Name.Value & " has empty CDE")
                        End If
                    Catch e As Exception
                        m_odtk.WriteMessage("an error occurred9", "debug")
                    End Try
                Next

            ElseIf (String.Compare(className, "Satellite", True) = 0) Then

                ' Iterate the satellites and each of their LS objects
                For k As Integer = 0 To childClass.Count - 1

                    Dim satObj As Object = childClass(k)
                    DebugMsg("sat name: " & satObj.Name.Value)
                    Dim nSatKids As Integer = satObj.Children.Count
                    DebugMsg("num kids: " & nSatKids)
                    For m As Integer = 0 To nSatKids - 1
                        Dim satChildClass As Object = satObj.Children(m)
                        DebugMsg("satChildClass: " & satChildClass.Name.Value)

                        If (String.Compare(satChildClass.Name.Value, "LeastSquares", True) = 0) Then
                            For n As Integer = 0 To satChildClass.Count - 1
                                Dim lsObj As Object = satChildClass(n)
                                DebugMsg("k & m & n: " & k & " " & m & " " & n)
                                Try
                                    If (m_nLS >= m_lsNames.Length) Then
                                        'DebugMsg("redim LS: " & m_lsNames.Length)
                                        ReDim Preserve m_lsNames(m_lsNames.Length + 10)
                                    End If
                                    m_lsNames(m_nLS) = satObj.Name.Value & "." & lsObj.Name.Value
                                    DebugMsg(m_lsNames(m_nLS))
                                    m_nLS = m_nLS + 1
                                    If lsObj.CustomDataEditing.Schedule.Count > 0 Then
                                        DebugMsg(lsObj.Name.Value & " has CDE")
                                    Else
                                        DebugMsg(lsObj.Name.Value & " has empty CDE")
                                    End If
                                Catch e As Exception
                                    m_odtk.WriteMessage("an error occurred10", "debug")
                                End Try
                            Next
                        End If

                    Next

                Next

            End If
        Next

        ' Add names to Source and Target lists
        'DebugMsg(m_nFilters & " numFil")
        For j As Integer = 0 To m_nFilters - 1
            If Not (String.IsNullOrEmpty(m_filNames(j))) Then
                cdeSchedule = GetCDESchedule(m_filNames(j))
                If (cdeSchedule.count > 0) Then
                    CheckedListBox1.Items.Add(m_filNames(j), False)
                End If
                CheckedListBox2.Items.Add(m_filNames(j), False)
            End If
        Next
        For j As Integer = 0 To m_nLS - 1
            If Not (String.IsNullOrEmpty(m_lsNames(j))) Then
                cdeSchedule = GetCDESchedule(m_lsNames(j))
                If (cdeSchedule.count > 0) Then
                    CheckedListBox1.Items.Add(m_lsNames(j), False)
                End If
                CheckedListBox2.Items.Add(m_lsNames(j), False)
            End If
        Next

        If (CheckedListBox1.Items.Count > 0 And CheckedListBox2.Items.Count > 1) Then
            If (m_bCDElist) Then
                SetStatus("Select a custom data editing schedule Source and Target(s) and press the Copy button")
            Else
                SetStatus("Select a residual editing schedule Source and Target(s) and press the Copy button")
            End If
        ElseIf (CheckedListBox1.Items.Count = 0) Then
            ' Inform user there are no sources of data
            If (m_nFilters > 0 And m_nLS > 0) Then
                If (m_bCDElist) Then
                    SetStatus("None of any scenario, filter or least squares objects has a custom data editing schedule")
                Else
                    SetStatus("None of the filter or least squares objects has a residual editing schedule")
                End If
            ElseIf (m_nFilters > 0) Then
                If (m_bCDElist) Then
                    SetStatus("Neither the scenario nor any filter objects has a custom data editing schedule")
                Else
                    SetStatus("None of the filter objects has a residual editing schedule")
                End If
            ElseIf (m_nLS > 0) Then
                If (m_bCDElist) Then
                    SetStatus("Neither the scenario nor any least squares objects has a custom data editing schedule")
                Else
                    SetStatus("None of the least squares objects has a residual editing schedule")
                End If
            Else
                If (m_bCDElist) Then
                    SetStatus("The scenario has no custom data editing schedule, nor are there any objects to copy it to")
                Else
                    SetStatus("The scenario needs filter and least squares objects to copy residual editing schedules")
                End If
            End If
        Else
            If (m_bCDElist) Then
                SetStatus("Create filter or least squares objects to copy a custom data editing schedule to")
            Else
                SetStatus("Create filter or least squares objects to copy a residual editing schedule to")
            End If
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If (CheckedListBox1.CheckedItems.Count > 0 And CheckedListBox2.CheckedItems.Count > 0) Then

            Dim odName As String = CheckedListBox1.CheckedItems(0)
            'DebugMsg("odName: " & odName)
            Dim cdeSource As Object = GetCDESchedule(odName)
            'DebugMsg("got cde? ")

            Dim odNameTgt As String
            Dim cdeTarget As Object
            Dim cdeElem As Object
            Dim cdeNew As Object
            For Each odNameTgt In CheckedListBox2.CheckedItems
                If (String.Compare(odNameTgt, odName, True) = 0) Then
                    Continue For
                End If
                ' DebugMsg("odName2: " & odName)
                cdeTarget = GetCDESchedule(odNameTgt)
                'DebugMsg("got cde tgt? ")
                If (CheckBox1.Checked) Then
                    cdeTarget.Clear()
                End If

                ' Copy the elements of the source CDE to the target CDE
                For Each cdeElem In cdeSource
                    cdeNew = cdeTarget.NewElem()
                    cdeNew = cdeElem
                    cdeTarget.push_back(cdeNew)
                Next

            Next
            If (m_bCDElist) Then
                SetStatus("Success: the custom data editing schedule has been copied")
            Else
                SetStatus("Success: the residual editing schedule has been copied")
            End If
        ElseIf (CheckedListBox1.CheckedItems.Count < 1) Then
            SetStatus("Nothing to do: you need to select a source to copy from")
        ElseIf (CheckedListBox2.CheckedItems.Count < 1) Then
            SetStatus("Nothing to do: you need to select a target to copy to")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        ' This resets the lists to pick up any changes to objects

        ResetLists()

    End Sub

    Private Sub CheckedListBox1_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        ' There can only be one item selected in the Source list, 
        ' and if that item is checked in the target list it will be unchecked

        If (m_bCDElist) Then
            SetStatus("Select a custom data editing schedule Source and Target(s) and press the Copy button")
        Else
            SetStatus("Select a residual editing schedule Source and Target(s) and press the Copy button")
        End If

        Dim newStr As String = CheckedListBox1.Items(e.Index)
        Dim nIndex As Integer
        If (e.NewValue = CheckState.Checked) Then
            For Each item As String In CheckedListBox1.CheckedItems
                nIndex = CheckedListBox1.Items.IndexOf(item)
                If (nIndex <> e.Index) Then
                    CheckedListBox1.SetItemChecked(nIndex, False)
                    Exit For
                End If
            Next

            Dim item2 As String
            For i As Integer = 0 To CheckedListBox2.Items.Count - 1
                item2 = CheckedListBox2.Items(i)
                If (String.Compare(item2, newStr, True) = 0) Then
                    nIndex = CheckedListBox2.Items.IndexOf(item2)
                    ' Is item currently checked?
                    Dim in2 As Integer = CheckedListBox2.CheckedItems.IndexOf(item2)
                    If (in2 >= 0) Then
                        CheckedListBox2.SetItemChecked(nIndex, False)
                        SetStatus("The corresponding target has been unchecked")
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub CheckedListBox2_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox2.ItemCheck
        ' If the item checked in the target list is the same as the one
        ' selected in the source list, the one in the target list will be unchecked
        If (m_bCDElist) Then
            SetStatus("Select a custom data editing schedule Source and Target(s) and press the Copy button")
        Else
            SetStatus("Select a residual editing schedule Source and Target(s) and press the Copy button")
        End If

        Dim item1Str As String = CheckedListBox2.Items(CheckedListBox2.SelectedIndex)
        If (e.NewValue = CheckState.Checked) Then
            If (CheckedListBox1.CheckedItems.Count = 1) Then

                Dim item2 As String = CheckedListBox2.Items(e.Index)
                Dim nIndex As Integer = 0

                For Each item As String In CheckedListBox1.CheckedItems
                    If (String.Compare(item, item2, True) = 0) Then
                        CheckedListBox2.SetItemChecked(e.Index, False)
                        e.NewValue = CheckState.Unchecked
                        SetStatus("Error: The target cannot be the same as the source")
                    End If
                Next

            End If

        End If
    End Sub

    Private Function GetCDESchedule(ByVal objName As String) As Object
        Dim scen As Object = m_odtk.Scenario(0)

        If (String.Compare(scen.name.Value, objName, True) = 0) Then
            If (m_bCDElist) Then
                Return scen.Measurements.ViewAndSave.CustomDataEditing.Schedule
            Else
                Return Nothing
            End If
        End If

        Dim nScChildren As Integer = scen.Children.Count

        For i As Integer = 0 To nScChildren - 1
            Dim childClass As Object = scen.Children(i)
            Dim className As String = childClass.Name.Value

            If (String.Compare(className, "Filter", True) = 0) Then
                ' Iterate the filters to see how many there are and get their names
                For k As Integer = 0 To childClass.Count - 1
                    Dim filObj As Object = childClass(k)
                    If (String.Compare(filObj.Name.Value, objName, True) = 0) Then
                        If (m_bCDElist) Then
                            Return filObj.CustomDataEditing.Schedule
                        Else
                            Return filObj.ResidualEditing.Schedule
                        End If
                    End If
                Next

            ElseIf (String.Compare(className, "Satellite", True) = 0) Then

                ' Iterate the satellites and each of their LS objects
                For k As Integer = 0 To childClass.Count - 1

                    Dim satObj As Object = childClass(k)

                    Dim nSatKids As Integer = satObj.Children.Count
                    For m As Integer = 0 To nSatKids - 1
                        Dim satChildClass As Object = satObj.Children(m)

                        If (String.Compare(satChildClass.Name.Value, "LeastSquares", True) = 0) Then
                            For n As Integer = 0 To satChildClass.Count - 1
                                Dim lsObj As Object = satChildClass(n)
                                Dim lsName As String = satObj.Name.Value & "." & lsObj.Name.Value
                                If (String.Compare(lsName, objName, True) = 0) Then
                                    If (m_bCDElist) Then
                                        Return lsObj.CustomDataEditing.Schedule
                                    Else
                                        Return lsObj.ResidualEditing.Schedule
                                    End If
                                End If
                            Next
                        End If

                    Next

                Next

            End If
        Next

        Return Nothing
    End Function

    Private Sub SetStatus(ByVal stringOut As String)
        StatusBox.Text = stringOut
    End Sub

    Private Sub DebugMsg(ByVal stringOut As String)
        'm_odtk.WriteMessage(stringOut, "debug")
    End Sub


    Private Sub RadioButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton2.Click
        ResetLists()
    End Sub

    Private Sub RadioButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.Click
        ResetLists()
    End Sub
End Class

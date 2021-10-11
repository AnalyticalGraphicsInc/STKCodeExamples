' this utility will convert all the selected terrain data to .pdtt format
'
' author: jens ramrath
' date: 23-feb-07
' updated: 10-nov-2011 to work with STKv10 and various
' updated: 18 aug 2020  - to work with STKv12 
'                       - now using the registry
'                       - now split this into two steps - terrain file to .datt and then .datt to .pdtt

Imports System
Imports System.IO
Imports Microsoft.Win32

Public Class Form1

    Private Sub SelectDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectInput.Click
        SelectInputDirBrowser.ShowDialog()
        DisplayInput.Text = SelectInputDirBrowser.SelectedPath
    End Sub


    Private Sub SelectOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectOutput.Click
        SelectOutputDirBrowser.ShowDialog()
        DisplayOutput.Text = SelectOutputDirBrowser.SelectedPath
    End Sub

    Private Sub GetFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetFiles.Click
        'search through directory and all subdirectories
        SearchForFiles(DisplayInput.Text)
    End Sub

    Private Sub SearchForFiles(ByVal dir)
        Dim i
        Dim SubDir = Directory.GetDirectories(dir)
        Dim File As String
        'Dim files = Directory.GetFiles(dir, "*.dt1")


        ' write files in current folder that satisfy criteria to ToConvert window
        ' ##### DTE #####
        If DTE.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dte")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DEM #####
        If DEM.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dem")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### HDR #####
        If HDR.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.hdr")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DMED #####
        If DMED.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dmed")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### G98 #####
        If G98.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.g98")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DT0 #####
        If DT0.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dt0")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DT1 #####
        If DT1.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dt1")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DT2 #####
        If DT2.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dt2")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DT3 #####
        If DT3.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dt3")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DT4 #####
        If DT4.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dt4")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If
        ' ##### DT5 #####
        If DT5.Checked = True Then
            Dim files = Directory.GetFiles(dir, "*.dt5")

            For Each File In files
                Console.WriteLine(File)

                ' add file to ToConvert window
                If ToConvert.Text = "" Then
                    ToConvert.Text = File
                Else
                    ToConvert.Text = ToConvert.Text & vbCrLf & File
                End If
            Next
        End If




        ' search for subfolders
        If SubDirs.Checked = True Then
            Try
                For i = 0 To SubDir.Length - 1
                    Console.WriteLine(SubDir(i))

                    SearchForFiles(SubDir(i))
                Next
            Catch ex As Exception
                Console.WriteLine("no subdirs in {0}", SubDir)
            End Try
        End If


    End Sub


    Private Sub OutputSame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputSame.CheckedChanged
        If OutputSame.Checked = True Then
            SelectOutput.Enabled = False
            DisplayOutput.ReadOnly = True
        Else
            SelectOutput.Enabled = True
            DisplayOutput.ReadOnly = False
        End If
    End Sub


    Private Sub ClearSelectedFIles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearSelectedFIles.Click
        ToConvert.Text = ""
    End Sub

    Private Sub Convert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Convert.Click
        Dim i
        Dim InputFiles, InputFileExtension, OutputFile, OutputDir
        Dim WarningDattReturn, WarningPdttReturn
        Dim processinfo As Process

        ' split input file string
        InputFiles = Split(ToConvert.Text, vbCrLf)

        ' assemble conversion command
        For i = 0 To UBound(InputFiles)
            Console.WriteLine(InputFiles(i))

            If Path.GetExtension(InputFiles(i)) = ".dte" Then
                InputFileExtension = "MUSE"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dem" Then
                InputFileExtension = "USGS"
            ElseIf Path.GetExtension(InputFiles(i)) = ".hdr" Then
                InputFileExtension = "GTOPO30"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dmed" Then
                InputFileExtension = "NIMA"
            ElseIf Path.GetExtension(InputFiles(i)) = ".g98" Then
                InputFileExtension = "GEODAS"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dt0" Then
                InputFileExtension = "NIM0"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dt1" Then
                InputFileExtension = "NIM1"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dt2" Then
                InputFileExtension = "NIM2"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dt3" Then
                InputFileExtension = "NIM3"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dt4" Then
                InputFileExtension = "NIM4"
            ElseIf Path.GetExtension(InputFiles(i)) = ".dt5" Then
                InputFileExtension = "NIM5"
            Else
                InputFileExtension = "error"
            End If

            OutputFile = Path.GetFileNameWithoutExtension(InputFiles(i))
            If OutputSame.Checked = True Then
                OutputDir = Path.GetDirectoryName(InputFiles(i))
            Else
                OutputDir = DisplayOutput.Text
            End If


            ' check if .datt output file already exists
            WarningDattReturn = 6
            WarningPdttReturn = 6
            If File.Exists(OutputDir & "\" & OutputFile & ".datt") = True And OverwritePdtt.Checked = False Then
                WarningDattReturn = MsgBox(OutputFile & ".datt already exists - overwrite?", MsgBoxStyle.YesNo)
            End If
            If File.Exists(OutputDir & "\" & OutputFile & ".pdtt") = True And OverwritePdtt.Checked = False Then
                WarningPdttReturn = MsgBox(OutputFile & ".pdtt already exists - overwrite?", MsgBoxStyle.YesNo)
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''' STEP 1 - convert from original terrain file to .datt '''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If WarningDattReturn = 6 Then
                ' build the .datt files
                processinfo = System.Diagnostics.Process.Start("" & converterTerrainToDattPath & "", "-i """ & InputFiles(i) & """ -o """ & OutputDir & "\" & OutputFile & ".datt"" -p """ & InputFileExtension & """ -cb ""Earth""")
                processinfo.WaitForExit()
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''
            ''''' STEP 2 - convert from .datt to .pdtt '''''
            ''''''''''''''''''''''''''''''''''''''''''''''''
            If WarningPdttReturn = 6 Then
                ' build the .pdtt files
                processinfo = System.Diagnostics.Process.Start("" & converterDattToPdttPath & "", "-i """ & OutputDir & "\" & OutputFile & ".datt"" -o """ & OutputDir & "\" & OutputFile & ".pdtt""")
                processinfo.WaitForExit()


                ' remove .datt files
                If KeepDatt.Checked = False Then
                    System.IO.File.Delete(OutputDir & "\" & OutputFile & ".datt")
                End If
            End If
        Next

    End Sub

    Private Sub test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult

        msg = "Do you want to continue?"   ' Define message.
        style = MsgBoxStyle.DefaultButton2 'Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        title = "MsgBox Demonstration"   ' Define title.
        ' Display message.
        response = MsgBox(msg, style, title)
    End Sub


    Dim converterTerrainToDattPath As String
    Dim converterDattToPdttPath As String

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' search for AgGx3dTerrainCreate
        ' TODO: update to read from registry
        If Not My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\13.0", "STKBinaryFolder", Nothing) Is Nothing Then
            converterTerrainToDattPath = Path.Combine(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\13.0", "STKBinaryFolder", Nothing), "AgAsDtedConvert.exe")
            converterDattToPdttPath = Path.Combine(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\13.0", "STKBinaryFolder", Nothing), "AgGx3dTerrainCreate.exe")
        ElseIf Not My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\12.0", "STKBinaryFolder", Nothing) Is Nothing Then
            converterTerrainToDattPath = Path.Combine(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\12.0", "STKBinaryFolder", Nothing), "AgAsDtedConvert.exe")
            converterDattToPdttPath = Path.Combine(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\12.0", "STKBinaryFolder", Nothing), "AgGx3dTerrainCreate.exe")
        ElseIf Not My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\11.0", "STKBinaryFolder", Nothing) Is Nothing Then
            converterTerrainToDattPath = Path.Combine(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\11.0", "STKBinaryFolder", Nothing), "AgAsDtedConvert.exe")
            converterDattToPdttPath = Path.Combine(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\AGI\STK\11.0", "STKBinaryFolder", Nothing), "AgGx3dTerrainCreate.exe")

            MsgBox("no STK install found, please browse to AgGx3dTerrainCreate.exe")

            Dim fileDialog As New OpenFileDialog()
            fileDialog.Filter = "exe files(*.exe)|*.exe|All files (*.*)|*.*"
            fileDialog.FilterIndex = 0
            fileDialog.RestoreDirectory = True

            If fileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then

                If fileDialog.CheckFileExists Then
                    converterTerrainToDattPath = fileDialog.FileName
                End If
            End If
        End If

    End Sub
End Class

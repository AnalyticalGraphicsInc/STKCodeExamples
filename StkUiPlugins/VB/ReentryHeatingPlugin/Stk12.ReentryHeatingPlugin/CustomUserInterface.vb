Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports System.Drawing
Imports System.IO
Imports AGI.Ui.Plugins
Imports System.Threading
Imports Agi.STKObjects
Imports Agi.STKUtil

Public Class CustomUserInterface
    Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl

    Private m_pEmbeddedControlSite As AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite
    Private m_root As AGI.STKObjects.AgStkObjectRoot
    Private m_uiPlugin As Agi.Ui.Plugins.VB_Net.ReentryHeating.ReentryHeatingPlugin

    Public Function GetIcon() As stdole.IPictureDisp Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.GetIcon
        Return Nothing
    End Function

    Public Sub OnClosing() Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.OnClosing

    End Sub

    Public Sub OnSaveModified() Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.OnSaveModified

    End Sub

    Public Sub SetSite(ByVal Site As AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite) Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.SetSite
        m_pEmbeddedControlSite = Site
        m_uiPlugin = m_pEmbeddedControlSite.Plugin
        m_root = m_uiPlugin.STKRoot
        CustomUserInterface_Load()
    End Sub

    Private Sub CustomUserInterface_Load()

        'Get current Scenario Object
        Dim currentScenario As Agi.STKObjects.AgScenario
        currentScenario = m_root.CurrentScenario()

        'Get Start/Stop times of the scenario
        Dim scenarioStartTime As String = currentScenario.StartTime.ToString()
        Dim scenarioStopTime As String = currentScenario.StopTime.ToString()


        'Populate current Scenario's Start/Stop Time
        txtStartTime.Text = scenarioStartTime.ToString()
        txtStopTime.Text = scenarioStopTime.ToString()



    End Sub



    '------------------ Core Computations  -------------------------------

    Private Sub btnCreateSat_Click(sender As System.Object, e As System.EventArgs) Handles btnCreateSat.Click

        'Set Units for Connect to transfer data in KM
        m_root.ExecuteCommand("SetUnits / km")

        'Get current Scenario Object
        Dim currentScenario As Agi.STKObjects.AgScenario
        currentScenario = m_root.CurrentScenario()

        'Get Start/Stop times of the scenario
        Dim scenarioStartTime As String = currentScenario.StartTime.ToString()
        Dim scenarioStopTime As String = currentScenario.StopTime.ToString()

        'Create the Satellite to be used as the Reentry Vehicle
        'Check to see if it already exists, if so then remove it
        Dim cmdReturn As AgExecCmdResult = m_root.ExecuteCommand("DoesObjExist / */Satellite/Reentry_Sat")
        Dim doesExist As String = cmdReturn(0).ToString()

        If CStr(doesExist) = "1" Then
            Dim existingSat As AgSatellite
            existingSat = m_root.GetObjectFromPath("*/Satellite/Reentry_Sat")
            existingSat.Unload()
        End If

        'Create new satellite
        Dim ReentrySat As IAgSatellite = TryCast(m_root.CurrentScenario.Children.[New](AgESTKObjectType.eSatellite, "Reentry_Sat"), IAgSatellite)

        'Set the propagator to HPOP
        ReentrySat.SetPropagatorType(STKObjects.AgEVePropagatorType.ePropagatorHPOP)
        Dim hpopProp As IAgVePropagatorHPOP = TryCast(ReentrySat.Propagator, IAgVePropagatorHPOP)
        'hpopProp.StartTime = txtStartTime.Text.ToString() 'Deprecated, Use EphemerisInterval to configure the propagation interval.Start time. Uses DateFormat Dimension

        hpopProp.EphemerisInterval.SetExplicitInterval(txtStartTime.Text.ToString(), txtStopTime.Text.ToString())

        hpopProp.Step = 1


        Dim area_massRatio As Double = (CDbl(txtArea.Text.ToString()) / CDbl(txtMass.Text.ToString()))

        'Set Drag parameters
        Dim hpopForceModel As IAgVeHPOPForceModel = hpopProp.ForceModel
        Dim hpopForceModelDrag As IAgVeHPOPForceModelDrag = hpopForceModel.Drag
        hpopForceModelDrag.SetDragModelType(AgEDragModel.eDragModelSpherical)
        Dim hpopForceModelDragModel As IAgVeHPOPDragModelSpherical = hpopForceModelDrag.DragModel
        hpopForceModelDragModel.AreaMassRatio = area_massRatio
        hpopForceModelDragModel.Cd = CDbl(txtCd.Text.ToString())

        hpopForceModel.Drag.Use = True


        'Set Atmosphere Density Model (chosen by user)
        If cboAtmosphereModel.Text.ToString() = "Harris-Priester" Then
            hpopForceModel.Drag.AtmosphericDensityModel = AgEAtmosphericDensityModel.eHarrisPriester
        ElseIf cboAtmosphereModel.Text.ToString() = "Jacchia 1960" Then
            hpopForceModel.Drag.AtmosphericDensityModel = AgEAtmosphericDensityModel.eJacchia60
        ElseIf cboAtmosphereModel.Text.ToString() = "MSISE 1990" Then
            hpopForceModel.Drag.AtmosphericDensityModel = AgEAtmosphericDensityModel.eMSIS90
        ElseIf cboAtmosphereModel.Text.ToString() = "NRLMSISE 2000" Then
            hpopForceModel.Drag.AtmosphericDensityModel = AgEAtmosphericDensityModel.eMSIS00
        Else
            MessageBox.Show("Please Select Atmosphere Density Model", "Density Model", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboAtmosphereModel.Focus()
            Exit Sub
        End If


        'Set Satellite Mass
        hpopForceModel.MoreOptions.Static.SatelliteMass = CDbl(txtMass.Text.ToString())

        'Set Orbit Parameters
        Dim orbit As IAgOrbitState = hpopProp.InitialState.Representation
        orbit.AssignMixedSpherical(AgECoordinateSystem.eCoordinateSystemICRF, CDbl(txtLatitude.Text.ToString()), CDbl(txtLongitude.Text.ToString()), CDbl(txtAltitude.Text.ToString()), CDbl(txtHorizFPA.Text.ToString()), CDbl(txtAzimuth.Text.ToString()), CDbl(txtVelocity.Text.ToString()))
        orbit.Epoch = scenarioStartTime


        'Propagate the Satellite
        hpopProp.Propagate()


        'Adjust some of the Graphical properties
        'Change Orbit Frame from being drawn in Inertial Frame to Fixed Frame
        ReentrySat.VO.OrbitSystems.FixedByWindow.IsVisible = True
        ReentrySat.VO.OrbitSystems.InertialByWindow.IsVisible = False

        'Turn on some Vectors of interest (namely Velocity)
        m_root.ExecuteCommand("VO */Satellite/Reentry_Sat SetVectorGeometry Add " & Chr(34) & "Satellite/Reentry_Sat Velocity(CBF) Vector" & Chr(34))
        m_root.ExecuteCommand("VO */Satellite/Reentry_Sat SetVectorGeometry Modify " & Chr(34) & "Satellite/Reentry_Sat Velocity(CBF) Vector" & Chr(34) & " Show On ShowLabel On")
        m_root.ExecuteCommand("VO */Satellite/Reentry_Sat SetVectorGeometry Data Scale .6")

        'Set the Spacecraft Attitude to be aligned with the Central Body Fixed Velocity Vector
        m_root.ExecuteCommand("SetAttitude */Satellite/Reentry_Sat Profile ECFVelRadial Offset 0")

        'Set the 3D Model to a reentry capsule model
        Dim homedir As AgExecCmdResult = m_root.ExecuteCommand("GetSTKHomeDir /")
        Dim operatingDir As String = homedir(0).ToString()

        Dim modelFile As IAgVOModelFile = TryCast(ReentrySat.VO.Model.ModelData, IAgVOModelFile)
        Dim dataPath As String = My.Application.Info.DirectoryPath
        modelFile.Filename = dataPath & "\Resources\Apollo_Capsule.dae"

        'Focus view of 3D Window to be centered on the capsule
        m_root.ExecuteCommand("VO * ViewFromTo Normal From Satellite/Reentry_Sat To Satellite/Reentry_Sat")




        'Create Heating Calculation & Data
        CreateHeating(ReentrySat, scenarioStartTime, scenarioStopTime)

        'Display the PeakHeating graph and the Deceleration graph
        btnHeatingGraph_Click(Me, EventArgs.Empty)
        btnGLoadGraph_Click(Me, EventArgs.Empty)





    End Sub


    Private Sub CreateHeating(satellite As AgSatellite, starttime As String, stoptime As String)

        'Get Time and Atmosphere Density values
        Dim dataPrvGroup As IAgDataProviderGroup
        Dim dataPrv As IAgDataProvider
        Dim timeFunc As IAgDataPrvTimeVar
        Dim result As IAgDrResult

        dataPrvGroup = satellite.DataProviders("Astrogator Values")
        dataPrv = dataPrvGroup.Group("Environment")
        timeFunc = dataPrv

        result = timeFunc.Exec(starttime, stoptime, 1)

        Dim intList As IAgDrIntervalCollection
        Dim intvl As IAgDrInterval
        Dim ds As IAgDrDataSet
        Dim ObjTime
        Dim ObjDensity

        intList = result.Intervals
        For Each intvl In intList
            For Each ds In intvl.DataSets
                Select Case ds.ElementName
                    Case "Time"
                        ObjTime = ds.GetValues()
                    Case "AtmosDensity"
                        ObjDensity = ds.GetValues()
                End Select
            Next
        Next



        ' Getting the Velocity Values (Fixed Frame)
        Dim dataPrvGroupVel As IAgDataProviderGroup
        Dim dataPrvVel As IAgDataProvider
        Dim timeFuncVel As IAgDataPrvTimeVar
        Dim resultVel As IAgDrResult

        dataPrvGroupVel = satellite.DataProviders("Vectors(Body)")
        dataPrvVel = dataPrvGroupVel.Group("Velocity(Earth(CBF))")
        timeFuncVel = dataPrvVel

        resultVel = timeFuncVel.Exec(starttime, stoptime, 1)

        Dim intListVel As IAgDrIntervalCollection
        Dim intvlVel As IAgDrInterval
        Dim dsVel As IAgDrDataSet
        Dim ObjTimeVel
        Dim ObjVel

        intListVel = resultVel.Intervals
        For Each intvlVel In intListVel
            For Each dsVel In intvlVel.DataSets
                Select Case dsVel.ElementName
                    Case "Time"
                        ObjTimeVel = dsVel.GetValues()
                    Case "Magnitude"
                        ObjVel = dsVel.GetValues()
                End Select
            Next
        Next


        'Use the Atmosphere Density and Velocity values to compute Heating
        'Set up a User Supplied Data group to hold the results so that STK can be used to Report and Graph the data directly

        m_root.ExecuteCommand("ExternalData */Satellite/Reentry_Sat AddGroup " & Chr(34) & "Stagnation Point Heating" & Chr(34) & " 1 " & Chr(34) & "StagnationPointHeating (W/m^2)" & Chr(34) & " Custom")

        Dim qdot() As Double
        ReDim qdot(UBound(ObjDensity))
        Dim maxHeatingindex As Integer = 0
        Dim curvalue As Double = 0
        Dim maxvalue As Double = 0
        Dim i As Integer
        For i = 0 To UBound(ObjDensity) - 1 Step 1
            'qdot=1.83*10^-4 * V^3 * SQRT(Rho/Rnose)
            qdot(i) = ((1.7623 * 10 ^ (-4)) * ((CDbl(ObjVel(i)) * 1000) ^ 2) * System.Math.Sqrt((CDbl(ObjDensity(i)) * (1 / 1000000000)) / (CDbl(txtNoseRadius.Text))))

            'Add data to the External Data data provider used to hold the data for Reporting/Graphing
            m_root.ExecuteCommand("ExternalData */Satellite/Reentry_Sat AddData " & Chr(34) & "Stagnation Point Heating" & Chr(34) & " " & Chr(34) & ObjTime(i) & Chr(34) & " " & qdot(i))

            'trying to find the index of the maximum value, maybe/probably a better way to do this but I'm just and engineer!! :)
            curvalue = qdot(i)
            If curvalue > maxvalue Then
                maxvalue = curvalue
                maxHeatingindex = i
            End If

        Next



        'Calculating Deceleration/G-Load
        'a=(1/2)Rho*V^2(Cd*A/m)
        'G = a/9.798

        m_root.ExecuteCommand("ExternalData */Satellite/Reentry_Sat AddGroup " & Chr(34) & "Vehicle Deceleration" & Chr(34) & " 1 " & Chr(34) & "Deceleration (G's)" & Chr(34) & " Custom")


        Dim deceleration() As Double
        ReDim deceleration(UBound(ObjDensity))
        Dim maxDecelindex As Integer = 0
        Dim curDecelvalue As Double = 0
        Dim maxDecelvalue As Double = 0
        Dim j As Integer
        For j = 0 To UBound(ObjDensity) - 1 Step 1

            deceleration(j) = ((1 / 2) * (CDbl(ObjDensity(j)) * (1 / 1000000000)) * ((CDbl(ObjVel(j)) * 1000) ^ 2) * ((CDbl(txtCd.Text)) * (CDbl(txtArea.Text)) / (CDbl(txtMass.Text)))) / (9.798)


            'Add data to the External Data data provider used to hold the data for Reporting/Graphing
            m_root.ExecuteCommand("ExternalData */Satellite/Reentry_Sat AddData " & Chr(34) & "Vehicle Deceleration" & Chr(34) & " " & Chr(34) & ObjTime(j) & Chr(34) & " " & deceleration(j))

            'trying to find the index of the maximum value
            curDecelvalue = deceleration(j)
            If curDecelvalue > maxDecelvalue Then
                maxDecelvalue = curDecelvalue
                maxDecelindex = j
            End If

        Next


        'using the peak stagnation time to turn on the vapor trails, go to animation time, and animate slowly for effect
        Dim peakHeatingTime As IAgDate = m_root.ConversionUtility.NewDate("UTCG", ObjTime(maxHeatingindex).ToString())


        m_root.ExecuteCommand("VO */Satellite/Reentry_Sat VaporTrail Show On")
        m_root.ExecuteCommand("VO */Satellite/Reentry_Sat VaporTrail MaxPuffs 1000 Density 4000 Radius .005")
        m_root.ExecuteCommand("VO */Satellite/Reentry_Sat VaporTrail StartTime " & Chr(34) & peakHeatingTime.Format("UTCG").ToString() & Chr(34) & " EndTime " & Chr(34) & peakHeatingTime.Add("min", 1).Format("UTCG").ToString() & Chr(34))
        'Seems to be a bug with using Connect Commands to turn on the Vapor Trails.  Abandonning this for now
        m_root.ExecuteCommand("SetAnimation * CurrentTime " & Chr(34) & peakHeatingTime.Format("UTCG").ToString() & Chr(34) & " TimeStep 0.1 ")
        m_root.ExecuteCommand("Animate * Start")







    End Sub





    ' -------------------Help Button interactions------------------------

    Private Sub btnSpacecraftParamHelp_Click(sender As System.Object, e As System.EventArgs) Handles btnSpacecraftParamHelp.Click

        Dim spacecraftHelpForm As New SpacecraftParameterHelp()
        spacecraftHelpForm.ShowDialog()

    End Sub

    Private Sub btnReentryParamHelp_Click(sender As System.Object, e As System.EventArgs) Handles btnReentryParamHelp.Click
        Dim reentryHelpForm As New ReentryHelp()
        reentryHelpForm.ShowDialog()
    End Sub


    ' ------------------ Reporting & Graphing ---------------------------
    Private Sub btnHeatingReport_Click(sender As System.Object, e As System.EventArgs) Handles btnHeatingReport.Click

        'Check to see if the correct Report/Graph Style is included in the STKData/Styles/Satellite directory
        Dim directory As AgExecCmdResult = m_root.ExecuteCommand("GetDirectory / Scenario")
        Dim scenarioDir As String = directory(0).ToString()
        Dim scenarioPath As String = scenarioDir & "\Stagnation Point Heating.rst"

        Dim dataPath As String = My.Application.Info.DirectoryPath
        Dim resourcesPath As String = dataPath & "\Resources\Stagnation Point Heating.rst"

        'Check if Satellite Object does exist
        Dim cmdReturn As AgExecCmdResult = m_root.ExecuteCommand("DoesObjExist / */Satellite/Reentry_Sat")
        Dim doesExist As String = cmdReturn(0).ToString()

        'Check if the Report/Graph Style exists in the current Scenario folder - If so, generate the report/graph, otherwise move the file first and then generate
        If My.Computer.FileSystem.FileExists(scenarioPath) Then
            ' use the correct custom report style
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("ReportCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Stagnation Point Heating" & Chr(34))
            End If
        Else
            My.Computer.FileSystem.CopyFile(resourcesPath, scenarioPath)
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("ReportCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Stagnation Point Heating" & Chr(34))
            End If
        End If



    End Sub


    Private Sub btnHeatingGraph_Click(sender As System.Object, e As System.EventArgs) Handles btnHeatingGraph.Click

        'Check to see if the correct Report/Graph Style is included in the STKData/Styles/Satellite directory
        Dim directory As AgExecCmdResult = m_root.ExecuteCommand("GetDirectory / Scenario")
        Dim scenarioDir As String = directory(0).ToString()
        Dim scenarioPath As String = scenarioDir & "\Stagnation Point Heating.rsg"

        Dim dataPath As String = My.Application.Info.DirectoryPath
        Dim resourcesPath As String = dataPath & "\Resources\Stagnation Point Heating.rsg"

        'Check if Satellite Object does exist
        Dim cmdReturn As AgExecCmdResult = m_root.ExecuteCommand("DoesObjExist / */Satellite/Reentry_Sat")
        Dim doesExist As String = cmdReturn(0).ToString()

        'Check if the Report/Graph Style exists in the current Scenario folder - If so, generate the report/graph, otherwise move the file first and then generate
        If My.Computer.FileSystem.FileExists(scenarioPath) Then
            ' use the correct custom report style
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("GraphCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Stagnation Point Heating" & Chr(34))
            End If
        Else
            My.Computer.FileSystem.CopyFile(resourcesPath, scenarioPath)
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("GraphCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Stagnation Point Heating" & Chr(34))
            End If
        End If


    End Sub


    Private Sub btnGLoadReport_Click(sender As System.Object, e As System.EventArgs) Handles btnGLoadReport.Click

        'Check to see if the correct Report/Graph Style is included in the STKData/Styles/Satellite directory
        Dim directory As AgExecCmdResult = m_root.ExecuteCommand("GetDirectory / Scenario")
        Dim scenarioDir As String = directory(0).ToString()
        Dim scenarioPath As String = scenarioDir & "\Reentry Deceleration.rst"

        Dim dataPath As String = My.Application.Info.DirectoryPath
        Dim resourcesPath As String = dataPath & "\Resources\Reentry Deceleration.rst"

        'Check if Satellite Object does exist
        Dim cmdReturn As AgExecCmdResult = m_root.ExecuteCommand("DoesObjExist / */Satellite/Reentry_Sat")
        Dim doesExist As String = cmdReturn(0).ToString()

        'Check if the Report/Graph Style exists in the current Scenario folder - If so, generate the report/graph, otherwise move the file first and then generate
        If My.Computer.FileSystem.FileExists(scenarioPath) Then
            ' use the correct custom report style
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("ReportCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Reentry Deceleration" & Chr(34))
            End If
        Else
            My.Computer.FileSystem.CopyFile(resourcesPath, scenarioPath)
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("ReportCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Reentry Deceleration" & Chr(34))
            End If
        End If

    End Sub


    Private Sub btnGLoadGraph_Click(sender As System.Object, e As System.EventArgs) Handles btnGLoadGraph.Click

        'Check to see if the correct Report/Graph Style is included in the STKData/Styles/Satellite directory
        Dim directory As AgExecCmdResult = m_root.ExecuteCommand("GetDirectory / Scenario")
        Dim scenarioDir As String = directory(0).ToString()
        Dim scenarioPath As String = scenarioDir & "\Reentry Deceleration.rsg"

        Dim dataPath As String = My.Application.Info.DirectoryPath
        Dim resourcesPath As String = dataPath & "\Resources\Reentry Deceleration.rsg"

        'Check if Satellite Object does exist
        Dim cmdReturn As AgExecCmdResult = m_root.ExecuteCommand("DoesObjExist / */Satellite/Reentry_Sat")
        Dim doesExist As String = cmdReturn(0).ToString()

        'Check if the Report/Graph Style exists in the current Scenario folder - If so, generate the report/graph, otherwise move the file first and then generate
        If My.Computer.FileSystem.FileExists(scenarioPath) Then
            ' use the correct custom report style
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("GraphCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Reentry Deceleration" & Chr(34))
            End If
        Else
            My.Computer.FileSystem.CopyFile(resourcesPath, scenarioPath)
            If CStr(doesExist) = "1" Then
                m_root.ExecuteCommand("GraphCreate */Satellite/Reentry_Sat Type Display Style " & Chr(34) & "Reentry Deceleration" & Chr(34))
            End If
        End If

    End Sub
End Class

Imports ASCOM
Imports System.Reflection
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt
'Imports AGI.STKX
Imports AGI.Ui.Application
Imports System.Runtime.InteropServices
Imports System.Threading

Public Class ASCOMUtilityUI

    Dim Ch As ASCOM.Utilities.Chooser
    Dim SelectedDevice As String
    Dim selectedSatellite As AgSatellite
    Dim selectedTelescope As AgFacility
    Dim m_oSTK As AgUiApplication
    Dim m_oSTKRoot As AgStkObjectRoot
    Dim AccessThread As Thread
    Dim PointingThread As Thread
    Private Delegate Sub SetTextCallback(text As String)
    Dim AzElCalcLoop As Boolean
    Dim pointingLoop As Boolean



    'Connect To Telescope Hardware/Simulator
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        If Button1.Text = "Connected!!" Then
            'do nothing, the telescope is connected already
            Button1.Text = "Connect"
            Button1.BackColor = Color.Red
            txtScopeAlt.Text = ""
            txtScopeLat.Text = ""
            txtScopeLon.Text = ""

        Else



            'Create a new chooser and set the required device type

            Ch = New ASCOM.Utilities.Chooser ' Create a new chooser component and set its device type
            Ch.DeviceType = "Telescope"

            'Select the required device - use one of:
            SelectedDevice = Ch.Choose 'Show the chooser dialogue with no device or with your device pre-selected
            'SelectedDevice = Ch.Choose("Simulator.Telescope")

            MsgBox("The selected device is: " & SelectedDevice)

            'Clean up chooser object
            Ch.Dispose()
            Ch = Nothing


            'Create an instance of the Telescope Object
            Dim telescope As DriverAccess.Telescope
            telescope = New DriverAccess.Telescope(SelectedDevice.ToString())


            If telescope.Name <> "" Then
                Button1.Text = "Connected!!"
                Button1.BackColor = Color.Lime
            End If


            txtScopeLat.Text = telescope.SiteLatitude.ToString()
            txtScopeLon.Text = telescope.SiteLongitude.ToString()
            txtScopeAlt.Text = telescope.SiteElevation.ToString()



        End If

    End Sub

    'Connect To STK!
    Private Sub btnSTKConnect_Click(sender As System.Object, e As System.EventArgs) Handles btnSTKConnect.Click
        If btnSTKConnect.Text = "Connect" Then
            Try
                m_oSTK = Marshal.GetActiveObject("STK11.Application")
                btnSTKConnect.Text = "Connected!!"
                btnSTKConnect.BackColor = Color.Lime
                m_oSTKRoot = m_oSTK.Personality2

            Catch ex As Exception
                MessageBox.Show("Cannot Connect to STK.  Make sure you have the correct Licenses!", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            If m_oSTKRoot.CurrentScenario.InstanceName <> "" Then
                populateSTKObjects()
            Else
                btnSTKConnect.Text = "Connect"
                btnSTKConnect.BackColor = Color.Red
                MessageBox.Show("Error Connecting to STK Scenario.  Make sure a current STK Scenario Exists!")
                Exit Sub
            End If


        Else
            If btnSTKConnect.Text = "Connected!!" Then

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(m_oSTK)
                lstSTKObjects.Items.Clear()
                lstSTKSatellites.Items.Clear()
                btnSTKConnect.Text = "Connect"
                btnSTKConnect.BackColor = Color.Red
                btnAdjustFacility.Enabled = False

                'Try to kill the Access Thread
                killAccessThread()

                'Clear all the form's Text Boxes
                clearTextBoxes()


            End If


        End If


    End Sub



    'Populate STK Facility Objects to be used as "Telescope" and Satellite objects to be pointed to/tracked
    Sub populateSTKObjects()
        ' Populate the list box with all the Facility objects in the Scenario

        Dim FacObjects As IAgStkObjectElementCollection = m_oSTKRoot.CurrentScenario.Children.GetElements(AgESTKObjectType.eFacility)

        For Each facility In FacObjects
            lstSTKObjects.Items.Add(facility.InstanceName)
        Next


        'Populate the list box with all the Satellite objects in the Scenario
        Dim SatObjects As IAgStkObjectElementCollection = m_oSTKRoot.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite)

        For Each Satellite In SatObjects
            lstSTKSatellites.Items.Add(Satellite.InstanceName)
        Next


    End Sub



    'What happens when a user selects a "Telescope" Object in the listbox
    Private Sub lstSTKObjects_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstSTKObjects.SelectedIndexChanged

        btnAdjustFacility.Enabled = True

        'Set Units for return data from STK
        m_oSTKRoot.ExecuteCommand("SetUnits / METER")
        m_oSTKRoot.ExecuteCommand("SetInternalUnits * GUI")


        Dim selectedIndex = lstSTKObjects.SelectedIndex
        Dim selectedName = lstSTKObjects.Items(selectedIndex)

        Dim lat As Double
        Dim lon As Double
        Dim alt As Double
        Dim heightAboveGround As Double
        Dim totalAlt As Double



        selectedTelescope = m_oSTKRoot.GetObjectFromPath("/Facility/" & selectedName.ToString())
        selectedTelescope.Position.QueryPlanetodetic(lat, lon, alt)
        heightAboveGround = selectedTelescope.HeightAboveGround
        totalAlt = alt + heightAboveGround

        txtAlt.Text = totalAlt.ToString()
        txtLat.Text = lat.ToString()
        txtLon.Text = lon.ToString()






    End Sub

    'What happens when a user selects a Satellite Object in the listbox
    Private Sub lstSTKSatellites_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstSTKSatellites.SelectedIndexChanged

        'Try to kill any currently running Threads
        killAccessThread()


        'Set Units for return data from STK
        m_oSTKRoot.ExecuteCommand("SetUnits / METER")
        m_oSTKRoot.ExecuteCommand("SetInternalUnits * GUI")


        Dim selectedIndex = lstSTKSatellites.SelectedIndex
        Dim selectedName = lstSTKSatellites.Items(selectedIndex)



        selectedSatellite = m_oSTKRoot.GetObjectFromPath("/Satellite/" & selectedName.ToString())

        'Make sure there is an STK Telescope Object Selected
        If lstSTKObjects.SelectedIndex = -1 Then
            MessageBox.Show("Please Select an STK Telescope First", "Select Telescope First", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Sub
        End If

        'Flag for starting Thread Loop in calculateAccessAzEl Thread
        AzElCalcLoop = True

        'Start the background Thread that is constantly calculating the Access Az/El
        AccessThread = New Thread(New ThreadStart(AddressOf calculateAccessAzEl))
        AccessThread.IsBackground = True
        AccessThread.Start()





    End Sub



    'Send pointing info to the actual Telescope equipment
    Private Sub btnPointTelescope_Click(sender As System.Object, e As System.EventArgs) Handles btnPointTelescope.Click

        'Start Pointing the Telescope
        If btnPointTelescope.Text = "Point Telescope" Then
            btnPointTelescope.Text = "Pointing!!"

            'Set STK to animate in RealTime and begin animation
            ' m_oSTKRoot.ExecuteCommand("SetAnimation * AnimationMode RealTime")
            m_oSTKRoot.ExecuteCommand("Animate * Start")

            'Flag for starting the pointing Loop
            pointingLoop = True

            'Start the background Thread that is constantly calculating the Access Az/El
            PointingThread = New Thread(New ThreadStart(AddressOf sendPointing))
            PointingThread.IsBackground = True
            PointingThread.Start()

        Else

            pointingLoop = False
            btnPointTelescope.Text = "Point Telescope"


            'Kill the thread that is sending the pointing info to the Telescope
            Try
                PointingThread.Abort()

            Catch ex As Exception

            End Try

        End If






    End Sub

    'Adjust Facility Position based on the Text Box Values 
    Private Sub btnAdjustFacility_Click(sender As System.Object, e As System.EventArgs) Handles btnAdjustFacility.Click

        'Set CONNECT units to send data in Meters
        m_oSTKRoot.ExecuteCommand("SetInternalUnits * GUI")
        m_oSTKRoot.ExecuteCommand("SetUnits / METER")

        'Get Positions from the Text Boxes
        Dim lat As String = txtLat.Text.ToString()
        Dim lon As String = txtLon.Text.ToString()
        Dim alt As Double = CDbl(txtAlt.Text.ToString()) / 1000


        selectedTelescope.Position.AssignGeodetic(lat, lon, alt)


    End Sub











    'Thread for calculating Access between the selected Telescope Facility and the Satellite
    Sub calculateAccessAzEl()

        'Set Units for return data from STK
        m_oSTKRoot.ExecuteCommand("SetUnits / KM")
        m_oSTKRoot.ExecuteCommand("SetInternalUnits * GUI")
        m_oSTKRoot.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")

        Do While (AzElCalcLoop = True)


            'Get Current Scenario Time
            Dim CurAnimationTime As AgExecCmdResult
            CurAnimationTime = m_oSTKRoot.ExecuteCommand("GetAnimationData * CurrentTime")
            Dim CurrentTime As String
            CurrentTime = CurAnimationTime(0).ToString().Replace("\", " ")
            Dim CurrentTimeWithoutQuotes As String = CurrentTime.ToString().Replace(Chr(34), "")

            'MessageBox.Show(CurrentTime)

            'Calculate Access between the STK Objects representing the Telescope and Satellite Objects
            Dim Access As IAgStkAccess = TryCast(TryCast(selectedTelescope, IAgStkObject).GetAccessToObject(TryCast(selectedSatellite, IAgStkObject)), IAgStkAccess)
            Access.ComputeAccess()



            Dim accessDP As IAgDataPrvTimeVar = TryCast(Access.DataProviders.GetDataPrvTimeVarFromPath("AER Data//Default"), IAgDataPrvTimeVar)
            Dim accessResults As IAgDrResult = accessDP.ExecSingle(CurrentTimeWithoutQuotes)

            Try
                Dim Azimuth As Array = accessResults.DataSets.GetDataSetByName("Azimuth").GetValues
                Dim Elevation As Array = accessResults.DataSets.GetDataSetByName("Elevation").GetValues
                Dim Range As Array = accessResults.DataSets.GetDataSetByName("Range").GetValues

                Dim AzimuthDecimal As Decimal = CDec(Azimuth(0))
                Dim ElevationDecimal As Decimal = CDec(Elevation(0))
                Dim RangeDecimal As Decimal = CDec(Range(0))

                SetAzText(Math.Round(AzimuthDecimal, 3, MidpointRounding.AwayFromZero).ToString())
                SetElText(Math.Round(ElevationDecimal, 3, MidpointRounding.AwayFromZero).ToString())
                SetRangeText(Math.Round(RangeDecimal, 3, MidpointRounding.AwayFromZero).ToString())


            Catch ex As Exception

                SetAzText("No Access")
                SetElText("No Access")
                SetRangeText("No Access")

            End Try

            Thread.Sleep(10)
            Application.DoEvents()

        Loop




    End Sub

    'Thread for sending pointing data to the actual Telescope
    Sub sendPointing()

        Dim mytelescope As DriverAccess.Telescope
        mytelescope = New DriverAccess.Telescope(SelectedDevice.ToString())



        Do While (pointingLoop = True)

            If txtAz.Text = "No Access" Then
                'If there is no access to the Satellite of interest, the Telescop will just slew to Az=180 El=45 and sit
                mytelescope.SlewToAltAz(180, 45)
            Else
                'Otherwise get the Az/El values from the text boxes and slew the Telescope to those positions
                Dim az As Double = CDbl(txtAz.Text.ToString())
                Dim el As Double = CDbl(txtEl.Text.ToString())

                Try
                    mytelescope.SlewToAltAz(az, el)
                Catch ex As Exception

                End Try


            End If


            Thread.Sleep(10)

        Loop


    End Sub











    Function getPointingAzimuth(curTime As String)
        Dim pointingAz As Double

        pointingAz = 35


        Return pointingAz

    End Function

    Function getPointingElevation(curTime As String)
        Dim pointingEl As Double

        pointingEl = 75

        Return pointingEl

    End Function

    'Delegate Operations for populating text boxes from within another Thread

    Private Sub SetAzText(text As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If (Me.txtAz.InvokeRequired) Then

            Dim del As SetTextCallback
            del = New SetTextCallback(AddressOf SetAzText)
            Me.Invoke(del, New Object() {text})

        Else

            Me.txtAz.Text = text

        End If
    End Sub

    Private Sub SetElText(text As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If (Me.txtEl.InvokeRequired) Then

            Dim del As SetTextCallback
            del = New SetTextCallback(AddressOf SetElText)
            Me.Invoke(del, New Object() {text})

        Else

            Me.txtEl.Text = text

        End If
    End Sub

    Private Sub SetRangeText(text As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If (Me.txtRange.InvokeRequired) Then

            Dim del As SetTextCallback
            del = New SetTextCallback(AddressOf SetRangeText)
            Me.Invoke(del, New Object() {text})

        Else

            Me.txtRange.Text = text

        End If
    End Sub

    'Misc Subs used in the program

    Sub killAccessThread()
        Try
            AccessThread.Abort()
        Catch ex As Exception

        End Try
    End Sub

    Sub clearTextBoxes()

        'Try to clear the data from all the Text Boxes on the form
        Try
            txtAlt.Text = ""
            txtAz.Text = ""
            txtEl.Text = ""
            txtLat.Text = ""
            txtLon.Text = ""
            txtRange.Text = ""
        Catch ex As Exception

        End Try


    End Sub



End Class

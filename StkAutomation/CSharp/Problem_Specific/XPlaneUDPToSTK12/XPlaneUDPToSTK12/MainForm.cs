using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.STKX;


namespace XPlaneToSTK
{
    partial class MainForm : Form
    {
        // Declaring all member variables
        delegate void SetTextCallback(TextBox textBox, string text);

        Thread m_udpListener;
        Thread m_stkCommands;

        bool m_connectionIsActive = true;

        private AgUiApplication m_stkApplication;
        private AgStkObjectRoot m_stkRoot;
        private AgAircraft m_aircraft;
        private AgScenario m_scenario;

        private string m_scenarioName = "XPlaneFeed";
        private string m_aircraftName = "XPlaneFlight";

        public MainForm()
        {
            InitializeComponent();
        }

        public class Win32
        {
            // Allocates a new console for current process.
            // This is only to start a console window inside a form application.  I use it sometimes for testing purposes
            [DllImport("kernel32.dll")]
            public static extern Boolean AllocConsole();

            // Frees the console.
            [DllImport("kernel32.dll")]
            public static extern Boolean FreeConsole();
        }

        private void XPlaneToSTK_FormClosing(object sender, EventArgs e)
        {
            CloseSTK();
            Win32.FreeConsole();
        }

        // This is for setting the text values of the text boxes
        // They have to be done like this to make the application "Thread-Safe" 
        // since this app starts a seperate Thread for receiving and populating the data
        // from the UDP stream from XPlane
        private void SetTextBox(TextBox textBox, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            if (textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextBox);
                Invoke(d, new object[] { textBox, text });
            }
            else
            {
                textBox.Text = text;
            }
        }

        private void ButtonConnectXPlane_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonConnectXplane.Text.Equals("Connect"))
                {
                    // Reset text boxes
                    latitudeTextBox.Text = "";
                    longitudeTextBox.Text = "";
                    altitudeTextBox.Text = "";
                    headingTextBox.Text = "";
                    pitchTextBox.Text = "";
                    rollTextBox.Text = "";

                    m_connectionIsActive = true;

                    while (altitudeTextBox.Text.CompareTo("") == 0)
                    {
                        try
                        {
                            m_udpListener = new Thread(new ThreadStart(UDPConnect));
                            m_udpListener.IsBackground = true;
                            m_udpListener.Start();

                            //Refreshing the application so text shows up in the text boxes
                            Application.DoEvents();
                        }
                        catch
                        {
                        }
                    }

                    buttonConnectXplane.Text = "Disconnect";
                }
                else
                {
                    if (m_udpListener.IsAlive)
                    {
                        m_connectionIsActive = false;
                        m_udpListener.Abort();
                        buttonConnectXplane.Text = "Connect";
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void UDPConnect()
        {
            BinaryHelperClass binaryHelper = new BinaryHelperClass();

            int listenPort = Convert.ToInt32(txtPortNumber.Text);

            // Uncomment to bring up a Console Window:
            //Win32.AllocConsole();

            UdpClient listener = new UdpClient();
            listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Loopback, listenPort);
            listener.Client.Bind(groupEP);

            string receivedData;
            byte[] receiveByteArray;

            while (m_connectionIsActive)
            {
                // This is the line of code that receives the broadcase message.
                // It calls the receive function from the object listener (class UdpClient)
                // It passes to listener the end point groupEP.
                // It puts the data from the broadcast message into the byte array 
                // named receivedByteArray.
                // I don't know why this uses the class UdpClient and IPEndPoint like this. 
                // Contrast this with the talker code. It does not pass by reference.
                // Note that this is a synchronous or blocking call.

                receiveByteArray = listener.Receive(ref groupEP);
                Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                receivedData = Encoding.ASCII.GetString(receiveByteArray, 0, receiveByteArray.Length);
                Console.WriteLine("data follows \n{0}\n\n", receivedData);


                // Print received data
                for (int i = 0; i <= 76; i++)
                {
                    Console.WriteLine(receiveByteArray[i].ToString());
                }


                // Getting YPR Data. The bytes are reversed in XPlane so I have to take each group of four and reverse them
                // which is why I have to start my FOR loop with c+3, so each group of four bytes has the last of the four
                // appearing first in the end concatenated string

                string tempStringYPR;
                List<string> yprData = new List<string>();
                for (int c = 9; c <= 37; c += 4)
                {
                    tempStringYPR = binaryHelper.ConvertToBinary(receiveByteArray[c + 3]);
                    tempStringYPR += binaryHelper.ConvertToBinary(receiveByteArray[c + 2]);
                    tempStringYPR += binaryHelper.ConvertToBinary(receiveByteArray[c + 1]);
                    tempStringYPR += binaryHelper.ConvertToBinary(receiveByteArray[c]);

                    yprData.Add(tempStringYPR);
                }

                
                foreach (string yprString in yprData)
                {
                    Console.WriteLine(yprString);
                    Console.WriteLine(binaryHelper.BinaryStringToValue(yprString));
                }


                // Put YPR data into the text boxes on the form
                SetTextBox(headingTextBox, binaryHelper.BinaryStringToValue(yprData[2]).ToString());
                SetTextBox(pitchTextBox, binaryHelper.BinaryStringToValue(yprData[0]).ToString());
                SetTextBox(rollTextBox, binaryHelper.BinaryStringToValue(yprData[1]).ToString());

                // I have to do the same thing I did with the YPR Data, but now for LAT, LON, and ALT data
                string tempStringLLA;
                List<string> llaData = new List<string>();
                for (int d = 45; d <= 73; d += 4)
                {
                    tempStringLLA = binaryHelper.ConvertToBinary(receiveByteArray[d + 3]);
                    tempStringLLA += binaryHelper.ConvertToBinary(receiveByteArray[d + 2]);
                    tempStringLLA += binaryHelper.ConvertToBinary(receiveByteArray[d + 1]);
                    tempStringLLA += binaryHelper.ConvertToBinary(receiveByteArray[d]);

                    llaData.Add(tempStringLLA);
                }

                foreach (string llaString in llaData)
                {
                    Console.WriteLine(yprData);
                    Console.WriteLine(binaryHelper.BinaryStringToValue(llaString));
                }


                // Put LLA data into the text boxes on the form
                SetTextBox(latitudeTextBox, binaryHelper.BinaryStringToValue(llaData[0]).ToString());
                SetTextBox(longitudeTextBox, binaryHelper.BinaryStringToValue(llaData[1]).ToString());
                SetTextBox(altitudeTextBox, binaryHelper.BinaryStringToValue(llaData[2]).ToString());
            }

            SetTextBox(latitudeTextBox, "");
            SetTextBox(longitudeTextBox, "");
            SetTextBox(altitudeTextBox, "");
            SetTextBox(headingTextBox, "");
            SetTextBox(pitchTextBox, "");
            SetTextBox(rollTextBox, "");
        }

        private void SendCommandsToSTK()
        {
            //Win32.AllocConsole();  //if you want to use a console window to display info for testing

            bool looping = false;
            while (!looping)
            {
                // Allowing for User to enter an Altitude Offset (Since i dont know the Datum of the Terrain data in XPlane so there could be 
                // some discrepancy in Altitude values, which this offset could be used to correct

                double altitude = Convert.ToDouble(altitudeTextBox.Text);
                double finalAltitude;
                double altitudeOffset;
                string altitudeString;

                bool isNum = double.TryParse(altitudeOffsetTextBox.Text, out _);
                if (isNum)
                {
                        altitudeOffset = Convert.ToDouble(altitudeOffsetTextBox.Text);
                        finalAltitude = altitude + altitudeOffset;
                        altitudeString = Convert.ToString(finalAltitude);
                }
                else
                {
                    altitudeString = "0";
                    SetTextBox(altitudeOffsetTextBox, "0");
                }

                try
                {
                    m_stkRoot.ExecuteCommand($"SetPosition */Aircraft/{m_aircraftName} LLA " + @"""" + "now" + @"""" + " " + latitudeTextBox.Text.Substring(0, 10) + " " + longitudeTextBox.Text.Substring(0, 10) + " " + altitudeString.Substring(0, 10));
                    m_stkRoot.ExecuteCommand($"AddAttitude */Aircraft/{m_aircraftName} Euler " + @"""" + "now" + @"""" + " 321 " + headingTextBox.Text.Substring(0, 10) + " " + pitchTextBox.Text.Substring(0, 10) + " " + rollTextBox.Text.Substring(0, 10));
                }
                catch
                {
                }

                Application.DoEvents();
            }
        }


        private void ButtonConnectSTK_Click(object sender, EventArgs e)
        {
            if (buttonConnectStk.Text.CompareTo("Connect To STK") == 0)
            {
                buttonConnectStk.Text = "Disconnect";

                ConnectToSTK();

                if (useCurrentScenario.Checked == false || m_stkRoot.Children.Count == 0)
                {
                    CreateScenario();
                }

                m_scenario = m_stkRoot.CurrentScenario as AgScenario;

                // Maximize Application as well as the 3D Window
                m_stkRoot.ExecuteCommand("Application / Maximize");
                m_stkRoot.ExecuteCommand("Window3D * Maximize 1");

                // Set scenario to realtime
                m_stkRoot.ExecuteCommand("SetAnimation * AnimationMode RealTime RealTimeOffset 0 RefreshDelta .001 RefreshMode RefreshDelta");
                m_stkRoot.ExecuteCommand("Animate * Start");

                m_stkRoot.ExecuteCommand("ConControl / VerboseOff");

            }
            else
            {
                CloseUdpConnection();
                CloseSTK();
                buttonConnectStk.Text = "Connect To STK";
                buttonCreateAircraft.Enabled = true;
            }

        }

        private void CloseUdpConnection()
        {
            if (m_udpListener != null)
            {
                // Stop all running threads and clearing the form's text boxes
                if (m_udpListener.IsAlive)
                {
                    m_connectionIsActive = false;
                    m_udpListener.Abort();
                }
                try
                {
                    m_udpListener.Abort();
                    m_stkCommands.Abort();
                    latitudeTextBox.Text = "";
                    longitudeTextBox.Text = "";
                    altitudeTextBox.Text = "";
                    headingTextBox.Text = "";
                    pitchTextBox.Text = "";
                    rollTextBox.Text = "";
                    buttonConnectXplane.Text = "Connect";
                }
                catch
                {
                }
            }
        }

        private void ConnectToSTK()
        {
            // Try to connect to existing STK instance, otherwise open a new one
            try
            {
                m_stkApplication = Marshal.GetActiveObject("STK12.Application") as AgUiApplication;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                m_stkApplication = new AgUiApplication();

                m_stkApplication.LoadPersonality("STK");
                m_stkApplication.Visible = true;
            }

            m_stkRoot = (AgStkObjectRoot)m_stkApplication.Personality2;
        }

        private void CreateScenario()
        {
            if (m_stkRoot.Children.Count != 0)
            {
                m_stkRoot.CloseScenario();
            }

            m_stkRoot.NewScenario(m_scenarioName);
        }

        private void CloseSTK()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to close STK?", "Close STK", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    m_stkRoot.CloseScenario();
                    m_stkApplication.Quit();
                    Marshal.FinalReleaseComObject(m_stkApplication);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    // STK is already closed
                }
            }
        }

        private void ButtonCreateAircraft_Click(object sender, EventArgs e)
        {
            buttonCreateAircraft.Enabled = false;

            try
            {
                m_aircraft = m_stkRoot.GetObjectFromPath($"Aircraft/{m_aircraftName}") as AgAircraft;
                m_aircraft.Unload();
            }
            catch
            {
                // Aircraft does not exist
            }

            m_aircraft = m_scenario.Children.New(AgESTKObjectType.eAircraft, m_aircraftName) as AgAircraft;
            ((IAgVeGfxAttributesBasic)m_aircraft.Graphics.Attributes).Color = Color.White;

            m_stkRoot.ExecuteCommand($"RealTime */Aircraft/{m_aircraftName} SetProp");
            m_stkRoot.ExecuteCommand($"RealTime */Aircraft/{m_aircraftName} SetLookAhead DeadReckon 2 .1 20");
            m_stkRoot.ExecuteCommand($"RealTime */Aircraft/{m_aircraftName} SetHistory 5 1");
            m_stkRoot.ExecuteCommand($"SetAttitude */Aircraft/{m_aircraftName} RealTime Extrapolate 1 1");
            m_stkRoot.ExecuteCommand($"SetAttitude */Aircraft/{m_aircraftName} DataReference Fixed YPR 0 0 0 YPR " + @"""" + $"Aircraft/{m_aircraftName} NorthEastDown" + @"""");
            m_stkRoot.ExecuteCommand("SetUnits / FEET");

            // Start the thread to send position and attitude commands to STK
            m_stkCommands = new Thread(new ThreadStart(SendCommandsToSTK));
            m_stkCommands.IsBackground = true;
            m_stkCommands.Start();
        }
    }
}

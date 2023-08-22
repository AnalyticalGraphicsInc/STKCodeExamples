using System;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using stdole;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Stk12.UiPlugin.HorizonsEphemImporter
{
    public partial class ImporterGUI : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private HorizonsEphemImporter m_uiplugin;
        private AgStkObjectRoot m_root;
        string cmd;

        private StreamReader queryHorizons(string startTime, string stopTime, string step)
        {
            // Query JPL Horizons API and return a StreamReader representing the result
            // Requires inputs of start time, stop time, step size, and body search (cmd string)
            // Full docs here: https://ssd-api.jpl.nasa.gov/doc/horizons.html
            string seedURL = "https://ssd.jpl.nasa.gov/api/horizons.api?format=text&OBJ_DATA=%27NO%27&MAKE_EPHEM=%27YES%27&EPHEM_TYPE=%27VECTORS%27&CENTER=%27500@399%27&VEC_TABLE=%272%27&VEC_LABELS=%27NO%27&REF_PLANE=%27FRAME%27";
            string param = "&COMMAND='" + Uri.EscapeDataString(cmd) + "'&START_TIME='" + Uri.EscapeDataString(startTime)
                + "'&STOP_TIME='" + Uri.EscapeDataString(stopTime) + "'&STEP_SIZE='" + Uri.EscapeDataString(step) + "'";

            WebRequest wrGETURL = WebRequest.Create(seedURL + param);
            return new StreamReader(wrGETURL.GetResponse().GetResponseStream());
        }

        public ImporterGUI()
        {
            InitializeComponent();
            comboBoxStep.SelectedItem = comboBoxStep.Items[1];
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiplugin = m_pEmbeddedControlSite.Plugin as HorizonsEphemImporter;
            m_root = m_uiplugin.STKRoot;
        }

        public void OnClosing()
        {

        }

        public void OnSaveModified()
        {

        }

        public IPictureDisp GetIcon()
        {
            return null;
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            // This function takes the input search and queries Horizons for a body matching input
            // If body match is found query again to find the available ephemeris times
            string minStart = "BC 9999-01-01";
            string maxStop = "10000-01-01";
            string lastLine = "";
            string curLine = "";

            // Search body in textbox. If drop-down is enabled for multiple body matches, search body ID instead
            if (txtBodySearch.Enabled)
                cmd = txtBodySearch.Text;
            else
            {
                cmd = new Regex(@"^.*?(?=\s\s)").Match(comboBoxMultiBody.SelectedItem.ToString()).Value;
                comboBoxMultiBody.Enabled = false;
            }

            // Pass in start/stop times that I know will return an error because no ephemeris for any object exists during those times
            // Step size is arbitrary because I know this will return an error
            using (StreamReader objReader = queryHorizons(minStart, maxStop, "1 d"))
            {
                while (curLine != null)
                {
                    // Read StreamResult and store the second to last line
                    lastLine = curLine;
                    curLine = objReader.ReadLine();

                    // Check for a string match that indicates multiple major bodies match input string
                    Regex rxMultiMatch = new Regex("^.*?(?= \")");
                    if (curLine != null && rxMultiMatch.Match(curLine).Value == " Multiple major-bodies match string")
                    {
                        Regex rxBodyMatch = new Regex(@"(\d|-\d).*?\s\s.*?(?=\s\s)");
                        objReader.ReadLine();
                        objReader.ReadLine();
                        objReader.ReadLine();
                        curLine = objReader.ReadLine();

                        // Match found, so write all matches to combo box
                        while (rxBodyMatch.IsMatch(curLine))
                        {
                            comboBoxMultiBody.Items.Add((rxBodyMatch.Match(curLine).Value));
                            curLine = objReader.ReadLine();
                        }

                        // Replace textbox to search body with a dropdown for selection of matches found
                        txtBodySearch.Enabled = false;
                        txtBodySearch.Visible = false;
                        comboBoxMultiBody.Enabled = true;
                        comboBoxMultiBody.Visible = true;
                        comboBoxMultiBody.SelectedIndex = 0;

                        // Prompt the user to select a specific body and return
                        MessageBox.Show("Multiple bodies found matching input. Select a body from the drop-down and search again to refine.", "Multiple Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            // If search is successful, then body name is foudn in last line of the stream
            Regex rxName = new Regex("(?<=\").*?(?=\")");
            if (rxName.IsMatch(lastLine))
            {
                // Extract body name and store
                string bodyName = rxName.Match(lastLine).Value;

                // Extract ephemeris start time in Horizons database.
                // Because Horizons returns a time to 4 decimal places but inputs are only allowed to be 3 digits, we round up to the nearest thousandth.
                Regex rxStartEnd = new Regex(@"(A.D.|B.C.).*(?=\s)");
                minStart = rxStartEnd.Match(lastLine).Value;
                minStart = minStart.Substring(0, minStart.Length - 2) + (int.Parse(minStart.Substring(minStart.Length - 2, 1)) + 1).ToString();

                // Query again using new minStart time to get the error that returns the max stop time for the ephemeris
                using (StreamReader objReader = queryHorizons(minStart, maxStop, "1 d"))
                {
                    // Get last line of stream containing the stop time
                    lastLine = "";
                    curLine = "";
                    while (curLine != null)
                    {
                        lastLine = curLine;
                        curLine = objReader.ReadLine();
                    }
                    // Always truncate to 3 decimal places
                    maxStop = rxStartEnd.Match(lastLine).Value.Substring(0, rxStartEnd.Match(lastLine).Value.Length - 1);
                }
                
                // Update UI with info, disable search options, and enable import options
                lblBodyInfo.Text = "Body: " + bodyName;
                lblMinStart.Text = "Min Start Time: " + minStart;
                lblMaxStop.Text = "Max Stop Time: " + maxStop;

                txtBodySearch.Enabled = false;
                btSearch.Enabled = false;
                txtStartTime.Enabled = true;
                txtStopTime.Enabled = true;
                txtStep.Enabled = true;
                txtSTKObjName.Enabled = true;
                btSaveDialog.Enabled = true;
                btImport.Enabled = true;
                comboBoxStep.Enabled = true;

                txtStartTime.Text = minStart;
                txtStopTime.Text = maxStop;
            }
            else
            {
                // If no matches are found, catch that
                MessageBox.Show("No matching body found. Please try another input.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtStep_TextChanged(object sender, EventArgs e)
        {
            // Protect step size to ensure it is a positive integer
            if ((int.TryParse(txtStep.Text, out int x) && x > 0) || (txtStep.Text == ""))
                lblValidStep.Text = "";
            else
                lblValidStep.Text = "Step size must be a positive integer.";
        }

        private void txtBodySearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable search 'Enter' key
            if (e.KeyCode == Keys.Enter)
                btSearch_Click(sender, e);
        }

        private void btSaveDialog_Click(object sender, EventArgs e)
        {
            // Open save dialog to save .e file
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "STK Ephemeris (*.e)|*.e";
            if (saveDialog.ShowDialog() == DialogResult.OK)
                // Ensure that extension ends in .e
                if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 2) != ".e")
                    txtSaveDir.Text = saveDialog.FileName + ".e";
                else
                    txtSaveDir.Text = saveDialog.FileName;
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            // Check that all inputs are valid
            if (!(int.TryParse(txtStep.Text, out int x) && x > 0))
                MessageBox.Show("Step size must be a positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtStartTime.Text == "" | txtStopTime.Text == "")
                MessageBox.Show("Start and stop times cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!(new Regex("^[a-zA-Z_0-9]+$").IsMatch(txtSTKObjName.Text)))
                MessageBox.Show("STK Object Name can include only letters, numbers, and underscore characters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtSaveDir.Text == "")
                MessageBox.Show("Specify a valid save directory.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                // Inputs are valid - parse step size and arrange into format for Horizons API
                string step = txtStep.Text;
                if ((string)comboBoxStep.SelectedItem == "years")
                    step += " y";
                else if ((string)comboBoxStep.SelectedItem == "months")
                    step += " mo";
                else if ((string)comboBoxStep.SelectedItem == "days")
                    step += " d";
                else if ((string)comboBoxStep.SelectedItem == "hours")
                    step += " h";
                else if ((string)comboBoxStep.SelectedItem == "minutes")
                    step += " m";

                // Query Horizons for ephemeris
                bool validEphem = false;
                using (StreamReader objReader = queryHorizons(txtStartTime.Text, txtStopTime.Text, step))
                {
                    string lastLine = "";
                    string curLine = "";
                    while (curLine != null)
                    {
                        lastLine = curLine;
                        curLine = objReader.ReadLine();

                        // $$SOE marks beginning of ephemeris - open writer at save location to write ephemeris
                        if (curLine == "$$SOE")
                        {
                            int linesWritten = 0;
                            using (StreamWriter ephemWriter = new StreamWriter(txtSaveDir.Text))
                            {
                                // Set flag that result by Horiozns is valid and read until end of ephemeris is found
                                validEphem = true;
                                string time, pos, vel;
                                Regex rxTime = new Regex(@".*(?=\s=)");
                                Regex rxPosVel = new Regex(@"(?<=\s)\S.*?E.{3}");

                                // Write header
                                ephemWriter.WriteLine("stk.v.12.0");
                                ephemWriter.WriteLine("BEGIN Ephemeris");
                                ephemWriter.WriteLine("CentralBody Earth");
                                ephemWriter.WriteLine("CoordinateSystem ICRF");
                                ephemWriter.WriteLine("DistanceUnit Kilometers");
                                ephemWriter.WriteLine("TimeFormat JDTDB");
                                ephemWriter.WriteLine("EphemerisTimePosVel");

                                curLine = objReader.ReadLine();
                                while (curLine != "$$EOE")
                                {
                                    // Write ephemeris file in EphemerisTimePosVEl format
                                    // Format available here: https://help.agi.com/stk/#stk/importfiles-02.htm
                                    time = rxTime.Matches(curLine)[0].Groups[0].Value;
                                    curLine = objReader.ReadLine();
                                    pos = rxPosVel.Matches(curLine)[0].Groups[0].Value + " " + rxPosVel.Matches(curLine)[1].Groups[0].Value + " " + rxPosVel.Matches(curLine)[2].Groups[0].Value;
                                    curLine = objReader.ReadLine();
                                    vel = rxPosVel.Matches(curLine)[0].Groups[0].Value + " " + rxPosVel.Matches(curLine)[1].Groups[0].Value + " " + rxPosVel.Matches(curLine)[2].Groups[0].Value;
                                    curLine = objReader.ReadLine();
                                    ephemWriter.WriteLine(time + " " + pos + " " + vel);
                                    linesWritten++;
                                }
                                
                                // Close out ephemeris
                                ephemWriter.WriteLine("END Ephemeris");
                            }
                            
                            if (linesWritten == 1)
                                // STK cannot propagate ephemerides with only one data point, so throw an error if this happens
                                MessageBox.Show("Step size is larger than start/stop time span. Decrease your step size or increase your time span.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (m_root.CurrentScenario == null)
                            {
                                // If no scenario to import object to, then tell user that and reset form
                                MessageBox.Show("Ephemeris written successfully, but object could not be imported into STK because an active scenario was not detected. Form will reset.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                resetForm();
                            }
                            else
                            {
                                IAgSatellite sat;
                                try
                                {
                                    // Import satellite to STK and propagate
                                    sat = m_root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, txtSTKObjName.Text) as IAgSatellite;
                                    sat.SetPropagatorType(AgEVePropagatorType.ePropagatorStkExternal);
                                    IAgVePropagatorStkExternal propagator = sat.Propagator as IAgVePropagatorStkExternal;
                                    propagator.Filename = txtSaveDir.Text;
                                    propagator.Propagate();
                                    MessageBox.Show("Import succeeded. Form will reset.", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    resetForm();
                                }
                                catch
                                {
                                    // Satellite with same name already exists in STK, so prompt user to overwrite object
                                    DialogResult overwriteDialog = MessageBox.Show(txtSTKObjName.Text + " is already an STK satellite object. Importing will overwrite this object's ephemeris. Proceed?", "Overwrite Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (overwriteDialog == DialogResult.Yes)
                                    {
                                        // Overwrite object
                                        sat = m_root.CurrentScenario.Children[txtSTKObjName.Text] as IAgSatellite;
                                        sat.SetPropagatorType(AgEVePropagatorType.ePropagatorStkExternal);
                                        IAgVePropagatorStkExternal propagator = sat.Propagator as IAgVePropagatorStkExternal;
                                        propagator.Filename = txtSaveDir.Text;
                                        propagator.Propagate();
                                        MessageBox.Show("Import succeeded. Form will reset.", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        resetForm();
                                    }
                                    else
                                    {
                                        // Don't overwrite and just write ephemeris to file
                                        MessageBox.Show("Ephemeris written successfully, but object could not be imported into STK because a pre-existing object already exists. Change your STK Obj. Name and select Import again if desired.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                    if (!validEphem)
                        if (new Regex(@"^.*?(\.|$)").Match(lastLine).Value == "Cannot interpret date.")
                            // Catch error that Horizons was unable to interpret date input. Tell user to try a different format
                            MessageBox.Show("Cannot parse start or stop input date. Try YYYY-Mon-Dy {HH:MM} format.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            // Catch any other errors and display
                            MessageBox.Show(lastLine, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void resetForm()
        {
            // Reset form back to initial state
            txtBodySearch.Text = "";
            txtBodySearch.Enabled = true;
            txtBodySearch.Visible = true;
            comboBoxMultiBody.Items.Clear();
            comboBoxMultiBody.Enabled = false;
            comboBoxMultiBody.Visible = false;
            btSearch.Enabled = true;
            lblBodyInfo.Text = "Body:";
            lblMinStart.Text = "Min Start Time:";
            lblMaxStop.Text = "Max Stop Time:";
            txtStartTime.Enabled = false;
            txtStopTime.Enabled = false;
            txtStep.Enabled = false;
            txtSTKObjName.Enabled = false;
            btSaveDialog.Enabled = false;
            btImport.Enabled = false;
            comboBoxStep.Enabled = false;
            txtStartTime.Text = "";
            txtStopTime.Text = "";
            txtStep.Text = "1";
            txtSTKObjName.Text = "";
            txtSaveDir.Text = "";
            comboBoxStep.SelectedIndex = 1;
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            // Reset form
            resetForm();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using AGI.STKUtil;
using System.Threading;
using System.Collections.Specialized;
using System.IO;
using System.Globalization;

namespace DataImporter
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;

        public string fileContent;
        private string m_stkDateFormat = "dd MMM yyyy HH:mm:ss.sss";

        //create list arrays to hold the data
        public List<DateTime> time_dateTime = new List<DateTime>();
        public List<string> Time = new List<string>();
        public List<string> Lat = new List<string>();
        public List<string> Lon = new List<string>();
        public List<string> Alt = new List<string>();
        public List<string> LatRate = new List<string>();
        public List<string> LonRate = new List<string>();
        public List<string> AltRate = new List<string>();
        public List<string> Yaw = new List<string>();
        public List<string> Pitch = new List<string>();
        public List<string> Roll = new List<string>();
        public List<string> CalcScalar = new List<string>();    

        public CustomUserInterface()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Implementation
        public stdole.IPictureDisp GetIcon()
        {
            return null;
        }

        public void OnClosing()
        {
            //m_root.OnStkObjectAdded -= m_root_OnStkObjectAdded;
            //m_root.OnStkObjectDeleted -= m_root_OnStkObjectDeleted;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_root = m_uiPlugin.STKRoot;
            m_stkObjectsLibrary = new StkObjectsLibrary((IAgStkObjectRoot)m_root);

            //EXAMPLE: Hooking to STK Exents
            //m_root.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            //m_root.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);

            //EXAMPLE: Using preference value
            //m_uiPlugin.DoubleValue;

            //EXAMPLE: Populate combo box with STK Objects
            //PopulateComboBox();
        }


        #endregion
        
        private void btnInputFile_Click(object sender, EventArgs e)
        {


            //create UI for user to browse to data file
            //if (txtInputFile.Text == "")
            //{
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All Files (*.*)|*.*|Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtInputFile.Text = openFileDialog.FileName;

                    string[] path = openFileDialog.FileName.Split('.');
                    string ext = path[path.Length - 1];

                    txtOutputFile.Text = openFileDialog.FileName.Replace(ext, "e");
                }
                else
                {
                    return;
                }

            //}

            //read the data file and store in a string
            using (StreamReader reader = new StreamReader(txtInputFile.Text))
            {
                fileContent = reader.ReadToEnd();
                reader.Close();
            }

            //populate the user form with the raw data file
            this.textBox_data.Text = fileContent;

            //populate user form combo boxes with the number of columns from data file
            string[] lines = fileContent.Split('\n');
            int numCols = lines.Length;
            string[] firstRow = lines[0].Split(',');

            for (int i = 1; i < firstRow.Length; i++)
            {
                comboBox_col_1.Items.Add(i.ToString());
                comboBox_col_2.Items.Add(i.ToString());
                comboBox_col_3.Items.Add(i.ToString());
                comboBox_col_4.Items.Add(i.ToString());
            }

            //parse the data file

            parseFile();
        }

        private void parseFile()
        {


//#if DEBUG
//            System.Diagnostics.Debugger.Break();
//#endif
            //set the epoch time to the scenario epoch time
            IAgExecCmdResult execResult = m_root.ExecuteCommand("GetEpoch *");
            textBox_epochTime.Text = execResult[0].ToString();
            

            //clear the list arrays before each run
            Time.Clear();
            time_dateTime.Clear();
            Lat.Clear();
            Lon.Clear();
            Alt.Clear();
            LatRate.Clear();
            LonRate.Clear();
            AltRate.Clear();
            Yaw.Clear();
            Pitch.Clear();
            Roll.Clear();
            textBox_formattedData.Clear();
            CalcScalar.Clear();

            //get the data from the text box
            this.textBox_data.Text = fileContent;

            //create an array of strings for each line
            string[] lines = fileContent.Split('\n');

            int lineCounter = 1;
            int epochCounter = 1;

            //set the column delimiter

            char delimiter = ',';

            if (comboBox_delimiters.Text == "Comma")
            {
                delimiter = ',';
            }
            else if (comboBox_delimiters.Text == "Tab")
            {
                delimiter = '\t';
            }
            else if (comboBox_delimiters.Text == "Space")
            {
                delimiter = ' ';
            }
            else if (comboBox_delimiters.Text == "Semicolon")
            {
                delimiter = ';';
            }
            else if (comboBox_delimiters.Text == "Period")
            {
                delimiter = '.';
            }

            //Read the file to find the number of columns

            //loop through each line in the data file

            foreach (string line in lines)
            {
               
                //split each line into elements in an array by the selected delimiter
                string[] entries = line.Split(delimiter);

                //make sure that there are at least 2 columns of data 
                if (entries.Length > 1)
                {

                    //parse the data for the user selected columns and store into list arrays

                    if (entries.Length < 4 && comboBox_outputType.Text == "Ephemeris")
                    {

                        MessageBox.Show("Not enough columns of data (" + entries.Length + ") to create an Ephemeris file.\n\n  Please change output type.");

                        return;



                        //if (MessageBox.Show("Not enough columns of data to create an Ephemeris file.\n\n  Would you like to change file type to Calculation Scalar?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                        //{
                        //    //m_pEmbeddedControlSite.Window.Close();
                        //}
                        //else
                        //{
                        //    //comboBox_outputType.Text = "Calculation_Scalar";
                        //    comboBox_outputType.SelectedItem = "Calculation_Scalar";
                            
                        //}
                    }
                    
                    if (comboBox_units_1.Text == "Date Time")
                    {

                        //try to parse date time, and if that doesn't work, try to use epoch seconds

                        try
                        {
                            DateTime time = DateTime.Parse(entries[Convert.ToInt32(comboBox_col_1.Text) - 1]);

                            string time_string = System.Convert.ToString(time);
                            Time.Add(time_string);
                            time_dateTime.Add(time);

                            //populate the epoch text box on the user form with the first time in the file.
                            if (epochCounter < 2 && checkBox_epochTime.Checked == true)
                            {
                                string epochTime = time.ToString(m_stkDateFormat);
                                textBox_epochTime.Text = epochTime;
                                epochCounter++;
                            }
                        }
                        catch (FormatException)
                        {
                          
                            if (MessageBox.Show("Unable to read time in line " + lineCounter.ToString() + ":\n\n" + entries[Convert.ToInt32(comboBox_col_1.Text) - 1] + "\n\n  Would you like to change units to epoch seconds?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                //m_pEmbeddedControlSite.Window.Close();
                            }
                            else
                            {
                                comboBox_units_1.Text = "Epoch Seconds";
                            }
                        }
                    }

                    if (comboBox_units_1.Text == "Epoch Seconds")
                    {
                        try
                        {
                            Time.Add(entries[Convert.ToInt32(comboBox_col_1.Text) - 1]);
                        }
                        catch (FormatException)
                        {
                            if (MessageBox.Show("Unable to read time in line " + lineCounter.ToString() + ":\n\n" + entries[Convert.ToInt32(comboBox_col_1.Text) - 1] + "\n\n  Would you like to change units to epoch seconds?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                            }
                            else
                            {
                                comboBox_units_1.Text = "Date Time";
                            }
                        }
                    }

                    if (comboBox_outputType.Text == "Calculation_Scalar")
                    {

                        try
                        {
                            double calc1 = Double.Parse(entries[Convert.ToInt32(comboBox_col_2.Text) - 1]);
                            CalcScalar.Add(calc1.ToString());
                        }
                        catch (Exception ex)
                        {
                            if (MessageBox.Show("Unable to read line " + lineCounter.ToString() + ":\n\n" + line + "\n\n  Would you like to ignore line and continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                              
                            }
                        }
                    }
                    else if (comboBox_outputType.Text == "Ephemeris")
                    {
                        try
                        {

                            double pos1 = Double.Parse(entries[Convert.ToInt32(comboBox_col_2.Text) - 1]);
                            double pos2 = Double.Parse(entries[Convert.ToInt32(comboBox_col_3.Text) - 1]);
                            double pos3 = Double.Parse(entries[Convert.ToInt32(comboBox_col_4.Text) - 1]);
                            double mot1 = Double.Parse(entries[Convert.ToInt32(comboBox_col_5.Text) - 1]);
                            double mot2 = Double.Parse(entries[Convert.ToInt32(comboBox_col_6.Text) - 1]);
                            double mot3 = Double.Parse(entries[Convert.ToInt32(comboBox_col_7.Text) - 1]);

                            Lat.Add(pos1.ToString());
                            Lon.Add(pos2.ToString());
                            Alt.Add(pos3.ToString());
                            LatRate.Add(mot1.ToString());
                            LonRate.Add(mot2.ToString());
                            AltRate.Add(mot3.ToString());
                        }
                        catch (Exception ex)
                        {
                            if (MessageBox.Show("Unable to read line " + lineCounter.ToString() + ":\n\n" + line + "\n\n  Would you like to ignore line and continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                              
                            }
                        }
                    }

                    else if (comboBox_outputType.Text == "Attitude")
                    {

                        try
                        {
                            double ang1 = Double.Parse(entries[Convert.ToInt32(comboBox_col_2.Text) - 1]);
                            double ang2 = Double.Parse(entries[Convert.ToInt32(comboBox_col_3.Text) - 1]);
                            double ang3 = Double.Parse(entries[Convert.ToInt32(comboBox_col_4.Text) - 1]);

                            Yaw.Add(ang1.ToString());
                            Pitch.Add(ang2.ToString());
                            Roll.Add(ang3.ToString());
                        }
                        catch (Exception ex)
                        {
                            if (MessageBox.Show("Unable to read line " + lineCounter.ToString() + ":\n\n" + line + "\n\n  Would you like to ignore line and continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {

                            }
                        }

                    }

                   
                    
                }

                lineCounter++;
            }

            //MessageBox.Show("format data");

            //loop through the list arrays and populate the formatted text box with the appropriate data for the user selected data format.

            for (int i = 0; i < Time.Count; i++)
            {

                string time = (Time[i] + "                             ").Substring(0, 22);

                if (comboBox_dataFormat.Text == "Time_LLA")
                {
                    string lat = ("  " + Lat[i] + "                             ").Substring(0, 9);
                    string lon = ("  " + Lon[i] + "                             ").Substring(0, 9);
                    string alt = ("  " + Alt[i] + "                             ").Substring(0, 9);
                    string dataLine = String.Format(time + lat + lon + alt);
                    textBox_formattedData.AppendText(dataLine + "\n");
                }
                else if (comboBox_dataFormat.Text == "Time_LLA_Rates")
                {
                    string lat = ("  " + Lat[i] + "                             ").Substring(0, 9);
                    string lon = ("  " + Lon[i] + "                             ").Substring(0, 9);
                    string alt = ("  " + Alt[i] + "                             ").Substring(0, 9);
                    string lat_r = ("  " + LatRate[i] + "                             ").Substring(0, 9);
                    string lon_r = ("  " + LonRate[i] + "                             ").Substring(0, 9);
                    string alt_r = ("  " + AltRate[i] + "                             ").Substring(0, 9);
                    string dataLine = String.Format(time + lat + lon + alt + lat_r + lon_r + alt_r);
                    textBox_formattedData.AppendText(dataLine + "\n");
                }
                else if (comboBox_dataFormat.Text == "Time_YPR")
                {
                    string yaw = ("  " + Yaw[i] + "                             ").Substring(0, 9);
                    string pitch = ("  " + Pitch[i] + "                             ").Substring(0, 9);
                    string roll = ("  " + Roll[i] + "                             ").Substring(0, 9);
                    string dataLine = String.Format(time + yaw + pitch + roll);
                    textBox_formattedData.AppendText(dataLine + "\n");
                }
                else if (comboBox_dataFormat.Text == "Calculation_Scalar")
                {
                    string calcScalar = ("  " + CalcScalar[i] + "                             ").Substring(0, 9);
                    string dataLine = String.Format(time + calcScalar);
                    textBox_formattedData.AppendText(dataLine + "\n");
                }
                
            }

            textBox_formattedData.SelectionStart = 0;
            textBox_formattedData.ScrollToCaret();
        }

        private void button_createFile_Click(object sender, EventArgs e)
        {
            //Create output files for the user selected output file type

            StreamWriter outputFile = new StreamWriter(txtOutputFile.Text);

            if (comboBox_outputType.Text == "Ephemeris")
            {
                outputFile.WriteLine("stk.v.8.0");
                outputFile.WriteLine("BEGIN Ephemeris");
                outputFile.WriteLine("ScenarioEpoch\t" + textBox_epochTime.Text);
                outputFile.WriteLine("NumberofEphemerisPoints\t" + Time.Count.ToString());
                outputFile.WriteLine("InterpolationMethod\tLagrange");
                outputFile.WriteLine("CentralBody\t\tEarth");
                outputFile.WriteLine("CoordinateSystem\t Fixed");
                outputFile.WriteLine("DistanceUnit\t\t Kilometers");
                outputFile.WriteLine();
                outputFile.WriteLine("EphemerisLLATimePosVel\n");

                for (int i = 0; i < Time.Count; i++)
                {
                    if (comboBox_units_1.Text == "Epoch Seconds")
                    {
                        outputFile.WriteLine(Time[i].ToString() + " " + CalcScalar[i]);
                    }

                    else
                    {
                        TimeSpan timespan = time_dateTime[i] - time_dateTime[0];
                        double timeDifference = timespan.TotalSeconds;

                        outputFile.WriteLine(timeDifference.ToString() + " " + Lat[i] + " " + Lon[i] + " " + Alt[i] + " " + LatRate[i] + " " + LonRate[i] + " " + AltRate[i]);
                    }
                }

                outputFile.WriteLine("");
                outputFile.WriteLine("End Ephemeris");

            }
            else if (comboBox_outputType.Text == "Attitude")
            {

                outputFile.WriteLine("stk.v.8.0");
                outputFile.WriteLine("");
                outputFile.WriteLine("BEGIN Attitude");
                outputFile.WriteLine("");
                outputFile.WriteLine("NumberOfAttitudePoints\t" + Time.Count.ToString());
                outputFile.WriteLine("ScenarioEpoch\t" + textBox_epochTime.Text);
                outputFile.WriteLine("CentralBody\t\tEarth");
                outputFile.WriteLine("CoordinateAxes	 Custom TopoCentric Aircraft/customAircraft");
                outputFile.WriteLine("Sequence 321");
                outputFile.WriteLine();
                outputFile.WriteLine("AttitudeTimeYPRAngles\n");


                for (int i = 0; i < Time.Count; i++)
                {
                    if (comboBox_units_1.Text == "Epoch Seconds")
                    {
                        outputFile.WriteLine(Time[i].ToString() + " " + CalcScalar[i]);
                    }

                    else
                    {
                        TimeSpan timespan = time_dateTime[i] - time_dateTime[0];
                        double timeDifference = timespan.TotalSeconds;

                        outputFile.WriteLine(timeDifference.ToString() + " " + Yaw[i] + " " + Pitch[i] + " " + Roll[i]);
                    }
                }
                

                outputFile.WriteLine("");
                outputFile.WriteLine("End Attitude");
            }
            else if (comboBox_outputType.Text == "Calculation_Scalar")
            {
                outputFile.WriteLine("stk.v.10.0");
                outputFile.WriteLine("");
                outputFile.WriteLine("BEGIN Data");
                outputFile.WriteLine("");
                outputFile.WriteLine("ReferenceEpoch\t" + textBox_epochTime.Text);
                outputFile.WriteLine("");
                outputFile.WriteLine("NumberOfIntervals 1");
                outputFile.WriteLine("InterpolationOrder 1");
                
                outputFile.WriteLine("");
                outputFile.WriteLine("BEGIN Interval");
                outputFile.WriteLine("NumberOfPoints\t" + Time.Count.ToString());
                outputFile.WriteLine("BEGIN TimeValues");
                outputFile.WriteLine("");

                for (int i = 0; i < Time.Count; i++)
                {
                    if (comboBox_units_1.Text == "Epoch Seconds")
                    {
                        outputFile.WriteLine(Time[i].ToString() + " " + CalcScalar[i]);
                    }
                    else
                    {
                        TimeSpan timespan = time_dateTime[i] - time_dateTime[0];
                        double timeDifference = timespan.TotalSeconds;

                        outputFile.WriteLine(timeDifference.ToString() + " " + CalcScalar[i]);
                    }
                }

                outputFile.WriteLine("");
                outputFile.WriteLine("End TimeValues");
                outputFile.WriteLine("");
                outputFile.WriteLine("End Interval");
                outputFile.WriteLine("");
                outputFile.WriteLine("End Data");

            }

            outputFile.Close();

        }

        private void btnOutputFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Ephemeris files (*.e)|*.e";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtOutputFile.Text = saveFileDialog.FileName;
            }
        }  

        private void button_updateSTK_Click(object sender, EventArgs e)
        {
            //populate STK scenario with data files
            if (comboBox_outputType.Text == "Ephemeris")
            {
                

                if (m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eAircraft, textBox_newObjectName.Text))
                {
                    MessageBox.Show(textBox_newObjectName.Text + " already exists, please pick a different name");
                }
                else
                {

                    m_root.ExecuteCommand("New / Aircraft " + textBox_newObjectName.Text);
                    m_root.ExecuteCommand("SetState */Aircraft/" + textBox_newObjectName.Text + " File " + @"""" + txtOutputFile.Text + @"""");
                    m_root.ExecuteCommand("SetAnimation * StartAndCurrentTime " + @"""" + textBox_epochTime.Text + @"""");
                    m_root.ExecuteCommand("VO * ViewFromTo Normal From Aircraft/" + textBox_newObjectName.Text);
                    m_root.ExecuteCommand("VO * View Top");
                }
            }
            else if (comboBox_outputType.Text == "Attitude")
            {

                if (m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eAircraft, textBox_newObjectName.Text))
                {
                    m_root.ExecuteCommand("SetAttitude */Aircraft/" + textBox_newObjectName.Text + " ClearData");
                    m_root.ExecuteCommand("SetAttitude */Aircraft/" + textBox_newObjectName.Text + " File " + @"""" + txtOutputFile.Text + @"""");
                
                }
                else
                {
                    MessageBox.Show(textBox_newObjectName.Text + " doesn't exist, please create an ephemeris file and update STK first.");
                }
            }
            else if (comboBox_outputType.Text == "Calculation_Scalar")
            {
                //#if DEBUG
                //            System.Diagnostics.Debugger.Break();
                //#endif

                //unload scalar calculation
                //create scalar calculation
                string cmd = "CalculationTool * Aircraft/" + textBox_newObjectName.Text + " Create " + @"""" + "Scalar Calculation" + @"""" + " customData "
                              + @"""" + "File" + @"""" + " Filename " + @"""" + txtOutputFile.Text + @"""";
                //MessageBox.Show(cmd);
                m_root.ExecuteCommand(cmd);

                //unload facility
                m_root.ExecuteCommand("New / Facility covAsset");
                m_root.ExecuteCommand("SetConstraint */Facility/covAsset LineOfSight Off");
                m_root.ExecuteCommand("SetConstraint */Aircraft/customAircraft LineOfSight Off");
                m_root.ExecuteCommand("Cov */Aircraft/" + textBox_newObjectName.Text + " Asset */Facility/covAsset Assign");
                m_root.ExecuteCommand("Cov */Aircraft/" + textBox_newObjectName.Text + " Access Compute");

                string cmd2 = "Cov */Aircraft/" + textBox_newObjectName.Text + " FOMDefine Definition ScalarCalculation Scalar "
                    + @"""" + "Aircraft/" + textBox_newObjectName.Text + " customData" + @"""" + " Compute Maximum TimeStep 1";
                //MessageBox.Show(cmd2);
                m_root.ExecuteCommand(cmd2);
                m_root.ExecuteCommand("Graphics */Aircraft/" + textBox_newObjectName.Text + " FOMContours Static StartStop 0 75 25 ColorRamp red blue show on");
                m_root.ExecuteCommand("Graphics */Aircraft/" + textBox_newObjectName.Text + " Legend Static Display On Title " + @"""" + "ObjCov Legend" + @"""" + " BackgroundColor 0");


                //Graphics */CoverageDefinition/PolarIceCaps/FigureOfMerit/TwoEyes FOMContours Animation ColorRamp red white
                //Graphics */Facility/Facility1 Legend Animation ShowOnMap On TextColor magenta Title "ObjCov Legend" BackgroundColor 1



                //change type to "File"
                //set the file
                //create facility
                //turn off facility los
                //turn off aircraft los
                //assign facility as coverage asset
                //compute cov
                //change FOM to "Scalar Calculation"
                //change time step to 1 sec
                //create contours
                //create a legend

            }
            
        }

        private void CustomUserInterface_Load(object sender, EventArgs e)
        {
            //populate form user controls

            //StkObjectsLibrary stkLibrary = new StkObjectsLibrary(m_root);
            //m_stkObjectsLibrary.ExecuteCommand("New / Facility f1");

            //m_root.ExecuteCommand("New / Aircraft customAircraft");
           
            //string stkStartTime = DataImporter.StkObjectsLibrary
            //AgExecCmdResult execResult;
            //string strResult;
            //string[] strWindows;
            //execResult = (AgExecCmdResult)m_root.ExecuteCommand("VO_R * MapID");
            //strResult = execResult[0];
            //MessageBox.Show(strResult);
            //IAgExecCmdResult result = m_root.ExecuteCommand("New / Facility f1");

            //textBox_epochTime.Text = result[0].ToString();



            comboBox_objectType.Text = "Aircraft";
            comboBox_objectType.Items.Add("Aircraft");
            comboBox_objectType.Items.Add("Satellite");
            comboBox_objectType.Items.Add("Missile");
            comboBox_objectType.Items.Add("Sensor");

            comboBox_outputType.Text = "Ephemeris";
            comboBox_outputType.Items.Add("Ephemeris");
            comboBox_outputType.Items.Add("Attitude");
            comboBox_outputType.Items.Add("Calculation_Scalar");

            comboBox_dataFormat.Text = "Time_LLA";
            comboBox_dataFormat.Items.Add("Time_LLA");
            comboBox_dataFormat.Items.Add("Time_LLA_Rates");
            comboBox_dataFormat.Items.Add("Time_XYZ");
            comboBox_dataFormat.Items.Add("Time_XYZ_Rates");
            comboBox_dataFormat.Items.Add("Time_AER");

            comboBox_units_1.Text = "Date Time";
            comboBox_units_1.Items.Add("Date Time");
            comboBox_units_1.Items.Add("Epoch Seconds");

            comboBox_units_2.Text = "Deg";
            comboBox_units_2.Items.Add("Deg");
            comboBox_units_2.Items.Add("Rad");
            comboBox_units_2.Items.Add("km");
            comboBox_units_2.Items.Add("m");
            comboBox_units_2.Items.Add("ft");
            comboBox_units_2.Items.Add("kft");

            comboBox_units_3.Text = "Deg";
            comboBox_units_3.Items.Add("Deg");
            comboBox_units_3.Items.Add("Rad");
            comboBox_units_3.Items.Add("km");
            comboBox_units_3.Items.Add("m");
            comboBox_units_3.Items.Add("ft");
            comboBox_units_3.Items.Add("kft");

            comboBox_units_4.Text = "km";
            comboBox_units_4.Items.Add("km");
            comboBox_units_4.Items.Add("m");
            comboBox_units_4.Items.Add("ft");
            comboBox_units_4.Items.Add("kft");
            comboBox_units_4.Items.Add("Deg");
            comboBox_units_4.Items.Add("Rad");

            comboBox_units_5.Text = "Deg/s";
            comboBox_units_5.Items.Add("Deg/s");
            comboBox_units_5.Items.Add("Rad/s");
            comboBox_units_5.Items.Add("km/s");
            comboBox_units_5.Items.Add("m/s");
            comboBox_units_5.Items.Add("ft/s");
            comboBox_units_5.Items.Add("kft/s");

            comboBox_units_6.Text = "Deg/s";
            comboBox_units_6.Items.Add("Deg/s");
            comboBox_units_6.Items.Add("Rad/s");
            comboBox_units_6.Items.Add("km/s");
            comboBox_units_6.Items.Add("m/s");
            comboBox_units_6.Items.Add("ft/s");
            comboBox_units_6.Items.Add("kft/s");

            comboBox_units_7.Text = "km/s";
            comboBox_units_7.Items.Add("km/s");
            comboBox_units_7.Items.Add("m/s");
            comboBox_units_7.Items.Add("ft/s");
            comboBox_units_7.Items.Add("kft/s");
            comboBox_units_7.Items.Add("Deg/s");
            comboBox_units_7.Items.Add("Rad/s");

            comboBox_coordSystem.Text = "Earth Fixed";
            comboBox_coordSystem.Items.Add("Earth Fixed");
            comboBox_coordSystem.Items.Add("Custom");

            comboBox_delimiters.Text = "Comma";
            comboBox_delimiters.Items.Add("Comma");
            comboBox_delimiters.Items.Add("Tab");
            comboBox_delimiters.Items.Add("Space");
            comboBox_delimiters.Items.Add("Semicolon");
            comboBox_delimiters.Items.Add("Period");

            clearColumns567();
        }

        private void checkBox_epochTime_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_epochTime.Checked)
            {

                textBox_epochTime.Text = time_dateTime[0].ToString(m_stkDateFormat);
            }
            else
            {
                IAgExecCmdResult execResult = m_root.ExecuteCommand("GetEpoch *");
                textBox_epochTime.Text = execResult[0].ToString();
            
            }
        }

        private void comboBox_outputType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox_outputType.Text == "Ephemeris")
            {

                comboBox_col_3.Enabled = true;
                comboBox_col_4.Enabled = true;

                comboBox_units_3.Enabled = true;
                comboBox_units_4.Enabled = true;

                label_col_2.Text = "Lat";
                label_col_3.Text = "Lon";
                label_col_4.Text = "Alt";

                comboBox_dataFormat.Items.Clear();
                comboBox_dataFormat.Text = "Time_LLA";
                comboBox_dataFormat.Items.Add("Time_LLA");
                comboBox_dataFormat.Items.Add("Time_LLA_Rates");
                comboBox_dataFormat.Items.Add("Time_XYZ");
                comboBox_dataFormat.Items.Add("Time_XYZ_Rates");
                comboBox_dataFormat.Items.Add("Time_AER");


                comboBox_units_2.Text = "Deg";
                comboBox_units_3.Text = "Deg";
                comboBox_units_4.Text = "km";

                comboBox_coordSystem.Text = "Earth Fixed";
                comboBox_coordSystem.Items.Add("Earth Fixed");
                comboBox_coordSystem.Items.Add("Custom");

                string[] path = txtOutputFile.Text.Split('.');
                string ext = path[path.Length - 1];

                txtOutputFile.Text = txtOutputFile.Text.Replace("." + ext, ".e");

            }

            if (comboBox_outputType.Text == "Attitude")
            {

                comboBox_col_3.Enabled = true;
                comboBox_col_4.Enabled = true;

                comboBox_units_3.Enabled = true;
                comboBox_units_4.Enabled = true;

                label_col_2.Text = "Yaw";
                label_col_3.Text = "Pitch";
                label_col_4.Text = "Roll";

                comboBox_dataFormat.Items.Clear();
                comboBox_dataFormat.Text = "Time_YPR";
                comboBox_dataFormat.Items.Add("Time_YPR");
                comboBox_dataFormat.Items.Add("Time_Quaternions");
                comboBox_dataFormat.Items.Add("Time_DCM");

                comboBox_units_2.Text = "Deg";
                comboBox_units_3.Text = "Deg";
                comboBox_units_4.Text = "Deg";

                comboBox_coordSystem.Text = "TopoCentric";

                string[] path = txtOutputFile.Text.Split('.');
                string ext = path[path.Length - 1];

                txtOutputFile.Text = txtOutputFile.Text.Replace("."+ext, ".a");

            }

            if (comboBox_outputType.Text == "Calculation_Scalar")
            {
                label_col_2.Text = "(Data)";
                label_col_3.Text = "";
                label_col_4.Text = "";


                comboBox_dataFormat.Items.Clear();
                comboBox_dataFormat.Text = "Calculation_Scalar";
                comboBox_dataFormat.Items.Add("Calculation_Scalar");
                
                comboBox_units_2.Items.Clear();
                comboBox_units_2.Text = "(Edit)";
                comboBox_units_2.Items.Add("(Edit)");

                comboBox_col_3.Enabled = false;
                comboBox_col_4.Enabled = false;

                comboBox_units_3.Enabled = false;
                comboBox_units_4.Enabled = false;

                comboBox_coordSystem.Text = "Calculation_Scalar";
                comboBox_coordSystem.Items.Clear();
                comboBox_coordSystem.Items.Add("Calculation_Scalar");

                string[] path = txtOutputFile.Text.Split('.');
                string ext = path[path.Length - 1];

                txtOutputFile.Text = txtOutputFile.Text.Replace("."+ext, ".csc");
            }

            parseFile();
        }

        private void clearColumns567()
        {

            label_col_5.Text = "";
            label_col_6.Text = "";
            label_col_7.Text = "";
            comboBox_col_5.Enabled = false;
            comboBox_col_6.Enabled = false;
            comboBox_col_7.Enabled = false;
            comboBox_units_5.Enabled = false;
            comboBox_units_6.Enabled = false;
            comboBox_units_7.Enabled = false;
            
        }

        private void showColumns567()
        {

            if (comboBox_dataFormat.Text == "Time_LLA_Rates")
            {
                label_col_5.Text = "Lat Rate:";
                label_col_6.Text = "Lon Rate:";
                label_col_7.Text = "Alt Rate:";
                comboBox_col_5.Enabled = true;
                comboBox_col_6.Enabled = true;
                comboBox_col_7.Enabled = true;
                comboBox_units_5.Enabled = true;
                comboBox_units_6.Enabled = true;
                comboBox_units_7.Enabled = true;
            }
            else if (comboBox_dataFormat.Text == "Time_XYZ_Rates")
            {
                label_col_5.Text = "VX:";
                label_col_6.Text = "VY:";
                label_col_7.Text = "VZ:";
                comboBox_col_5.Enabled = true;
                comboBox_col_6.Enabled = true;
                comboBox_col_7.Enabled = true;
                comboBox_units_5.Enabled = true;
                comboBox_units_6.Enabled = true;
                comboBox_units_7.Enabled = true;
            }

        }

        private void comboBox_dataFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_dataFormat.Text == "Time_LLA")
            {
                clearColumns567();

                label_col_2.Text = "Lat:";
                label_col_3.Text = "Lon:";
                label_col_4.Text = "Alt:";

                comboBox_units_2.Text = "Deg";
                comboBox_units_3.Text = "Deg";
                comboBox_units_4.Text = "km";
                
            }
            else if (comboBox_dataFormat.Text == "Time_LLA_Rates")
            {
                showColumns567();

                label_col_2.Text = "Lat:";
                label_col_3.Text = "Lon:";
                label_col_4.Text = "Alt:";
                label_col_5.Text = "Lat Rate:";
                label_col_6.Text = "Lon Rate:";
                label_col_7.Text = "Alt Rate:";

                comboBox_units_2.Text = "Deg";
                comboBox_units_3.Text = "Deg";
                comboBox_units_4.Text = "km";
                comboBox_units_2.Text = "Deg/s";
                comboBox_units_3.Text = "Deg/s";
                comboBox_units_4.Text = "km/s";

            }
            else if (comboBox_dataFormat.Text == "Time_XYZ")
            {

                clearColumns567();

                label_col_2.Text = "X:";
                label_col_3.Text = "Y:";
                label_col_4.Text = "Z:";

                comboBox_units_2.Text = "km";
                comboBox_units_3.Text = "km";
                comboBox_units_4.Text = "km";

            }
            else if (comboBox_dataFormat.Text == "Time_XYZ_Rates")
            {
                showColumns567();

                label_col_2.Text = "X:";
                label_col_3.Text = "Y:";
                label_col_4.Text = "Z:";
                label_col_5.Text = "VX";
                label_col_6.Text = "VY:";
                label_col_7.Text = "VZ:";

                comboBox_units_2.Text = "Deg";
                comboBox_units_3.Text = "Deg";
                comboBox_units_4.Text = "km";
                comboBox_units_2.Text = "Deg/s";
                comboBox_units_3.Text = "Deg/s";
                comboBox_units_4.Text = "km/s";
            }
            else if (comboBox_dataFormat.Text == "Time_AER")
            {
                clearColumns567();

                label_col_2.Text = "Az:";
                label_col_3.Text = "El:";
                label_col_4.Text = "Range:";

                comboBox_units_2.Text = "Deg";
                comboBox_units_3.Text = "Deg";
                comboBox_units_4.Text = "km";
            }

        }

        private void comboBox_col_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            parseFile();
        }

        private void comboBox_col_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            parseFile();
        }

        private void comboBox_col_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            parseFile();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_pEmbeddedControlSite.Window.Close();
           
        }


    }
}

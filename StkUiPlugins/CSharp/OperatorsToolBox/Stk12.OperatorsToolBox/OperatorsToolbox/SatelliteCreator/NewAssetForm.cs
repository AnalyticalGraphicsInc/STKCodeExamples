using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;

namespace OperatorsToolbox.SatelliteCreator
{
    public partial class NewAssetForm : OpsPluginControl
    {
        private List<string> _selectedSatList;
        public FilterConfig filter1;
        public FilterConfig filter2;
        public FilterConfig filter3;

        public NewAssetForm()
        {
            InitializeComponent();
            filter1 = new FilterConfig();
            filter1.SelectedOptions = new List<string>();
            filter2 = new FilterConfig();
            filter2.SelectedOptions = new List<string>();
            filter3 = new FilterConfig();
            filter3.SelectedOptions = new List<string>();
            SMAValue.Enabled = false;
            EccValue.Enabled = false;
            IncValue.Enabled = false;
            RAANValue.Enabled = false;
            AoPValue.Enabled = false;
            TAValue.Enabled = false;
            NameValue.Enabled = false;
            DatabaseBox.Visible = false;
            ElementsBox.Visible = false;
            

            PopulateExistingConstellations();
            _selectedSatList = new List<string>();
            CommonData.SatCatItemList = new List<SatCatItem>();
            CommonData.MetadataTypeList = new List<string>();
            CommonData.MetadataOptions1 = new List<string>();
            CommonData.MetadataOptions2 = new List<string>();
            CommonData.MetadataOptions3 = new List<string>();
            CommonData.MetadataOptions4 = new List<string>();
            CommonData.MetadataOptions5 = new List<string>();
            CommonData.SatCatFofo = new List<string>();

            CoordSystem.Items.Add("Fixed");
            CoordSystem.Items.Add("Inertial");
            CoordSystem.SelectedIndex = 1;

            ImportType.Items.Add("Orbital Elements");
            ImportType.Items.Add("TLE/TCE");
            ImportType.Items.Add("Ephemeris");
            ImportType.Items.Add("Satellite Database");
            ImportType.SelectedIndex = 3;

            ConstType.Items.Add("Default Constellation");
            ConstType.Items.Add("Existing Constellation");
            ConstType.Items.Add("New Constellation");
            ConstType.SelectedIndex = 0;
            ConstName.Enabled = false;
            ConstName.Visible = false;
            NameLabel.Visible = false;
            ExistingConst.Enabled = false;
            ExistingConst.Visible = false;

            TimeSlip.Enabled = false;
            TimeSlip.Visible = false;
            SlipTime.Visible = false;
            label17.Visible = false;

            IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
            SlipTime.Text = scenario.StartTime.ToString();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer(CommonData.DirectoryStr, "Choose Input File", TCEFile);
        }

        private void PopulateExistingConstellations()
        {
            IAgExecCmdResult result;
            ExistingConst.Items.Clear();
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Constellation");
            if (result[0] != "None")
            {
                string[] constArray = result[0].Split(null);
                foreach (var item in constArray)
                {
                    string newItem = item.Split('/').Last();
                    if (newItem != "" && newItem != null)
                    {
                        ExistingConst.Items.Add(newItem);
                    }
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
            IAgConstellation myConst;
            if ((string)ImportType.SelectedItem=="TLE/TCE")
            {
                string filepath = TCEFile.Text;
                IAgScenario scenario = (IAgScenario)CommonData.StkRoot.CurrentScenario;
                string startTime = scenario.StartTime;
                string stopTime = scenario.StopTime;
                string constellation = null;
                string cmd;

                try
                {
                    IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Constellation/Assets");
                    if ((string)ConstType.SelectedItem == "Default Constellation")
                    {
                        if (result[0] == "0")
                        {
                            IAgConstellation assets = (IAgConstellation)CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, "Assets");

                        }
                        constellation = "Assets";
                    }
                    else if ((string)ConstType.SelectedItem == "Existing Constellation")
                    {
                        result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Constellation/"+ ExistingConst.Text);
                        if (result[0] == "0")
                        {
                            IAgConstellation assets = (IAgConstellation)CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, ExistingConst.Text);

                        }
                        constellation = ExistingConst.Text;
                    }
                    else if((string)ConstType.SelectedItem == "New Constellation")
                    {
                        result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Constellation/" + ConstName.Text);
                        if (result[0] == "0")
                        {
                            IAgConstellation assets = (IAgConstellation)CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, ConstName.Text);

                        }
                        constellation = ConstName.Text;
                    }
                    //must parse satellites into constellation because of bug associated with ImportTLEFile connect command
                    //Get list of sats prior to import
                    result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
                    string[] prevSatArray = null;
                    if (result[0] != "None")
                    {
                        prevSatArray = result[0].Split(null);
                    }

                    //Import TLE
                    cmd = "ImportTLEFile * " + "\"" + filepath + "\"" + " AutoPropagate On TimeStep 30.0 StartStop " + "\"" + startTime + "\" " + "\"" + stopTime + "\"";
                    CommonData.StkRoot.ExecuteCommand(cmd);

                    myConst = CommonData.StkRoot.GetObjectFromPath("Constellation/" + constellation) as IAgConstellation;
                    IAgSatellite sat;
                    //Compare prev satellite list to new satellite list
                    result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
                    string[] newSatArray = null;
                    if (result[0] != "None")
                    {
                        newSatArray = result[0].Split(null);
                    }
                    if (prevSatArray==null)
                    {
                        if (newSatArray!=null)
                        {
                            foreach (var item in newSatArray)
                            {
                                if (item!="")
                                {
                                    string newItem = item.Split('/').Last();
                                    string objPath = "Satellite/" + newItem;
                                    myConst.Objects.Add(objPath);
                                    sat = CommonData.StkRoot.GetObjectFromPath(item.ToString()) as IAgSatellite;
                                    if (CoordSystem.SelectedIndex == 0)
                                    {
                                        sat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
                                        sat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
                                    }
                                    cmd = "VO */" + item.ToString() + " ModelDetail Set ModelLabel 2000000000 MarkerLabel 2000000000";
                                    CommonData.StkRoot.ExecuteCommand(cmd);
                                }
                            }
                        }
                    }
                    else
                    {
                        bool exists = false;
                        foreach (var newSat in newSatArray)
                        {
                            if (newSat!="")
                            {
                                exists = false;
                                foreach (var prevSat in prevSatArray)
                                {
                                    if (prevSat == newSat)
                                    {
                                        exists = true;
                                    }
                                }
                                if (exists != true)
                                {
                                    string newItem = newSat.Split('/').Last();
                                    string objPath = "Satellite/" + newItem;
                                    myConst.Objects.Add(objPath);
                                    sat = CommonData.StkRoot.GetObjectFromPath(objPath) as IAgSatellite;
                                    if (CoordSystem.SelectedIndex == 0)
                                    {
                                        sat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
                                        sat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
                                    }
                                    cmd = "VO */" + objPath + " ModelDetail Set ModelLabel 2000000000 MarkerLabel 2000000000";
                                    CommonData.StkRoot.ExecuteCommand(cmd);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Could not load asset(s)");
                }
            }
            else if ((string)ImportType.SelectedItem == "Orbital Elements")
            {
                int check = FieldCheck();
                if (check==0)
                {
                    int errorNum = 0;
                    try
                    {
                        errorNum = 1;
                        IAgStkObject sat = CreatorFunctions.GetCreateSatellite(NameValue.Text);
                        IAgSatellite mySat = sat as IAgSatellite;
                        if (CoordSystem.SelectedIndex == 0)
                        {
                            mySat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
                            mySat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
                            string cmd = "VO */Satellite/" + NameValue.Text + " ModelDetail Set ModelLabel 2000000000 MarkerLabel 2000000000";
                            CommonData.StkRoot.ExecuteCommand(cmd);
                        }
                        ((IAgSatellite)mySat).SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);
                        IAgOrbitStateClassical keplerian;
                        keplerian = ((IAgOrbitStateClassical)((IAgVePropagatorJ2Perturbation)((IAgSatellite)mySat).Propagator).InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical));
                        keplerian.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
                        ((IAgVePropagatorJ2Perturbation)((IAgSatellite)mySat).Propagator).InitialState.Representation.AssignClassical(AgECoordinateSystem.eCoordinateSystemICRF, Double.Parse(SMAValue.Text), Double.Parse(EccValue.Text), Double.Parse(IncValue.Text), Double.Parse(AoPValue.Text), Double.Parse(RAANValue.Text), Double.Parse(TAValue.Text));
                        ((IAgVePropagatorJ2Perturbation)((IAgSatellite)mySat).Propagator).Propagate();
                        IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Constellation/Assets");
                        errorNum = 2;
                        if ((string)ConstType.SelectedItem == "Default Constellation")
                        {
                            IAgStkObject conste = CreatorFunctions.GetCreateConstellation("Assets");
                            IAgConstellation assets = conste as IAgConstellation;

                            string objPath = "Satellite/" + NameValue.Text;
                            assets.Objects.Add(objPath);
                        }
                        else if ((string)ConstType.SelectedItem == "Existing Constellation")
                        {
                            IAgConstellation assets = (IAgConstellation)CommonData.StkRoot.GetObjectFromPath("Constellation/" + ExistingConst.Text);
                            string objPath = "Satellite/" + NameValue.Text;
                            assets.Objects.Add(objPath);
                        }
                        else if ((string)ConstType.SelectedItem == "New Constellation")
                        {
                            IAgConstellation assets = (IAgConstellation)CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, ConstName.Text);
                            string objPath = "Satellite/" + NameValue.Text;
                            assets.Objects.Add(objPath);
                        }
                    }
                    catch (Exception)
                    {
                        string errorMes = null;
                        if (errorNum==1)
                        {
                            errorMes = "Could not load satellite- Improper Orbital Elements";
                        }
                        else if (errorNum==2)
                        {
                            errorMes = "Could not add satellite to constellation";
                        }
                        MessageBox.Show(errorMes);
                    }
                }
            }
            else if ((string)ImportType.SelectedItem == "Satellite Database")
            {
                string sscString = null;
                string satName = null;
                int errorNum = 0;
                string errorSsc = null;
                string objPath = null;
                IAgSatellite mySat = null;
                IAgConstellation assets = null;
                IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
                string startTime = scenario.StartTime;
                string stopTime = scenario.StopTime;
                string cmd;
                int errorId = 0;
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
                foreach (var item in _selectedSatList)
                {
                    //Determine the proper satellite name based on catalog
                    errorId = 5;
                    int index = CommonData.SatCatItemList.IndexOf(CommonData.SatCatItemList.Where(p => p.Scc == item).FirstOrDefault());
                    SatCatItem currentSat = CommonData.SatCatItemList[index];
                    if (currentSat.OtherName != "Unspecified")
                    {
                        string otherName = currentSat.OtherName.Replace(" ", "_");
                        otherName = Regex.Replace(otherName, @"[^0-9a-zA-Z_]+", "");
                        //CommonData.StkRoot.ExecuteCommand("Rename */" + objPath + " " + otherName);
                        objPath = "Satellite/" + otherName;
                        satName = otherName;
                    }
                    else if (currentSat.CommonName != "Unspecified")
                    {
                        string commonName = currentSat.CommonName.Replace(" ", "_");
                        commonName = Regex.Replace(commonName, @"[^0-9a-zA-Z_]+", "");
                        objPath = "Satellite/" + commonName;
                        satName = commonName;
                    }
                    else
                    {
                        objPath = "Satellite/" + item;
                        satName = item;
                    }

                    //SSCString = "SSCNumber " + item + " ";
                    //cmd = "ImportTLEFile * " + "\"" + CommonData.SatDatabaseLocation + "\"" +SSCString+ "AutoPropagate On TimeStep 30.0 StartStop " + "\"" + startTime + "\" " + "\"" + stopTime + "\"";
                    try
                    {
                        errorId = 1;
                        //Create or get handle to satellite based on given name
                        mySat = (IAgSatellite)CreatorFunctions.GetCreateSatellite(satName);
                        //Erase old TLE data and replace it with new data from specified file
                        mySat.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4);
                        IAgVePropagatorSGP4 tleprop = mySat.Propagator as IAgVePropagatorSGP4;
                        tleprop.Segments.RemoveAllSegs();//clear before adding new
                        tleprop.CommonTasks.AddSegsFromFile(item, CommonData.Preferences.SatDatabaseLocation);
                        tleprop.Propagate();
                        mySat.Graphics.Resolution.Orbit = 20;
                        //Change trajectory representation to fixed if required. Inertial by default
                        if (CoordSystem.SelectedIndex == 0)
                        {
                            mySat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
                            mySat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
                            string cmd1 = "VO */Satellite/" + satName + " ModelDetail Set ModelLabel 2000000000 MarkerLabel 2000000000";
                            CommonData.StkRoot.ExecuteCommand(cmd1);
                        }
                        else if (CoordSystem.SelectedIndex == 1)
                        {
                            mySat.VO.OrbitSystems.FixedByWindow.IsVisible = false;
                            mySat.VO.OrbitSystems.InertialByWindow.IsVisible = true;
                            string cmd1 = "VO */Satellite/" + satName + " ModelDetail Set ModelLabel 2000000000 MarkerLabel 2000000000";
                            CommonData.StkRoot.ExecuteCommand(cmd1);
                        }

                        //Get handle to constellation or create if necessary. Add satellite to constellation
                        if ((string)ConstType.SelectedItem == "Default Constellation")
                        {
                            errorId = 2;
                            assets = CreatorFunctions.GetCreateConstellation("Assets") as IAgConstellation;
                        }
                        else if ((string)ConstType.SelectedItem == "Existing Constellation")
                        {
                            errorId = 3;
                            assets = CreatorFunctions.GetCreateConstellation(ExistingConst.Text) as IAgConstellation;
                        }
                        else if ((string)ConstType.SelectedItem == "New Constellation")
                        {
                            errorId = 4;
                            assets = CreatorFunctions.GetCreateConstellation(ConstName.Text) as IAgConstellation;
                        }

                        if (!assets.Objects.Contains("Satellite/" + satName))
                        {
                            objPath = "Satellite/" + satName;
                            assets.Objects.Add(objPath);
                        }

                        errorId = 8;
                        CreatorFunctions.ChangeSatColor(objPath, index);

                        //Create sensor if applicable. Place sensor in appropiate constellation for sensors
                        if (currentSat.Fov!=0 && SensorToggle.Checked)
                        {
                            try
                            {
                                errorId = 6;
                                IAgStkObject assetsObject = assets as IAgStkObject;
                                IAgStkObject sat = CommonData.StkRoot.GetObjectFromPath(objPath);
                                IAgSensor sensor = CreatorFunctions.GetCreateSensor(sat, sat.InstanceName + "_Sensor") as IAgSensor;
                                IAgStkObject sensorObject = sensor as IAgStkObject;
                                sensor.CommonTasks.SetPatternSimpleConic(currentSat.Fov / 2, 1);
                                sensor.VO.ProjectionType = AgESnVOProjectionType.eProjectionEarthIntersections;
                                sensor.VO.PercentTranslucency = 70;

                                errorId = 7;
                                assets = CreatorFunctions.GetCreateConstellation(assetsObject.InstanceName + "_Sensors") as IAgConstellation;
                                try
                                {
                                    assets.Objects.AddObject(sensorObject);
                                }
                                catch (Exception)
                                {
                                    //Likely already in the constellation
                                }
                            }
                            catch (Exception)
                            {                               
                            }                           
                        }                        
                    }
                    catch (Exception)
                    {
                        string errorIdString = null;
                        if (errorId==1)
                        {
                            errorIdString = "- Could not load from database";
                        }
                        else if (errorId == 2)
                        {
                            errorIdString = "- Could not add to Default constellation";
                        }
                        else if (errorId == 3)
                        {
                            errorIdString = "- Could not add to existing constellation";
                        }
                        else if (errorId == 4)
                        {
                            errorIdString = "- Could not add to new constellation";
                        }
                        else if (errorId == 5)
                        {
                            errorIdString = "- Could not change satellite name";
                        }
                        else if (errorId == 6)
                        {
                            errorIdString = "- Could not load sensor or update sensor properties";
                        }
                        else if (errorId == 7)
                        {
                            errorIdString = "- Could not add sensor to constellation";
                        }
                        else if (errorId == 8)
                        {
                            errorIdString = "- Could not change satellite color";
                        }
                        errorSsc = errorSsc + item + errorIdString+"\n\r";
                        errorNum++;
                    }
                }
                if (errorNum!=0)
                {
                    MessageBox.Show("Error loading the following satellites: \n\r" + errorSsc);
                }
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
            }
            else if ((string)ImportType.SelectedItem=="Ephemeris")
            {
                try
                {
                    IAgConstellation assets=null;
                    string constellation = null;
                    if ((string)ConstType.SelectedItem == "Default Constellation")
                    {
                        IAgStkObject obj = CreatorFunctions.GetCreateConstellation("Assets");
                        assets = obj as IAgConstellation;
                        constellation = "Assets";
                    }
                    else if ((string)ConstType.SelectedItem == "Existing Constellation")
                    {
                        IAgStkObject obj = CreatorFunctions.GetCreateConstellation(ExistingConst.Text);
                        assets = obj as IAgConstellation;
                        constellation = ExistingConst.Text;
                    }
                    else if ((string)ConstType.SelectedItem == "New Constellation")
                    {
                        IAgStkObject obj = CreatorFunctions.GetCreateConstellation(ConstName.Text);
                        assets = obj as IAgConstellation;
                        constellation = ConstName.Text;
                    }
                    string filepath = TCEFile.Text;
                    string[] separator1 = { "\\" };
                    string[] separator2 = { "." };
                    string fileName = filepath.Split(separator1, StringSplitOptions.None).Last();
                    string satName = fileName.Split(separator2, StringSplitOptions.None).First();
                    satName = Regex.Replace(satName, @"[^0-9a-zA-Z_]+", "");
                    IAgStkObject satObj = CreatorFunctions.GetCreateSatellite(satName);
                    IAgSatellite sat = satObj as IAgSatellite;
                    sat.SetPropagatorType(AgEVePropagatorType.ePropagatorStkExternal);
                    IAgVePropagatorStkExternal prop = sat.Propagator as IAgVePropagatorStkExternal;
                    prop.Filename = filepath;
                    if (TimeSlip.Checked)
                    {
                        try
                        {
                            prop.Override = true;
                            prop.EphemerisStartEpoch.SetExplicitTime(SlipTime.Text);
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Could not apply time slip");
                        }
                    }
                    prop.Propagate();
                    try
                    {
                        assets.Objects.AddObject(satObj);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not add to constellation");
                    }

                    if (CoordSystem.SelectedIndex == 0)
                    {
                        sat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
                        sat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
                        string cmd = "VO */Satellite/" + satName + " ModelDetail Set ModelLabel 2000000000 MarkerLabel 2000000000";
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error loading ephemeris");
                }
            }
            CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
            PopulateExistingConstellations();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        public static void BrowseFileExplorer(string initialDirectory, string title, TextBox textBox)
        {
            // Launch file explorer:
            OpenFileDialog fileExplorer = new OpenFileDialog();
            fileExplorer.InitialDirectory = initialDirectory;
            fileExplorer.Title = title;

            if (fileExplorer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = fileExplorer.FileName;
            }
        }

        private int FieldCheck()
        {
            int check=0;
            double sma;
            double ecc;
            double inc;
            double raan;
            double aop;
            double ta;

            bool isNumerical = Double.TryParse(SMAValue.Text, out sma);
            if (!isNumerical)
            {
                MessageBox.Show("Semi-Major Axis field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(EccValue.Text, out sma);
            if (!isNumerical)
            {
                MessageBox.Show("Eccentricity field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(IncValue.Text, out sma);
            if (!isNumerical)
            {
                MessageBox.Show("Inclination field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(RAANValue.Text, out sma);
            if (!isNumerical)
            {
                MessageBox.Show("RAAN field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(AoPValue.Text, out sma);
            if (!isNumerical)
            {
                MessageBox.Show("Argument of Perigee field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(TAValue.Text, out sma);
            if (!isNumerical)
            {
                MessageBox.Show("True Anomaly field not a number");
                check = 1;
            }
            if (NameValue.Text==null||NameValue.Text=="")
            {
                MessageBox.Show("Please assign the satellite a name");
                check = 1;
            }
            return check;
        }

        private void PopulateMainFilter()
        {
            //MainFilter.Items.Clear();
            //MainFilter.Items.Add("All");
            //foreach (var item in CommonData.MetadataTypeList)
            //{
            //    MainFilter.Items.Add(item);
            //}
        }

        private void PopulateSpecificFilter()
        {
            //SpecificFilter.Items.Clear();
            //if (MainFilter.SelectedIndex==0)
            //{
            //    SpecificFilter.Items.Add("All");

            //}
            //else if (MainFilter.SelectedIndex == 1)
            //{
            //    foreach (var item in CommonData.MetadataOptions1)
            //    {
            //        SpecificFilter.Items.Add(item);
            //    }
            //}
            //else if (MainFilter.SelectedIndex == 2)
            //{
            //    foreach (var item in CommonData.MetadataOptions2)
            //    {
            //        SpecificFilter.Items.Add(item);
            //    }
            //}
            //else if (MainFilter.SelectedIndex == 3)
            //{
            //    foreach (var item in CommonData.MetadataOptions3)
            //    {
            //        SpecificFilter.Items.Add(item);
            //    }
            //}
            //else if (MainFilter.SelectedIndex == 4)
            //{
            //    foreach (var item in CommonData.MetadataOptions4)
            //    {
            //        SpecificFilter.Items.Add(item);
            //    }
            //}
            //else if (MainFilter.SelectedIndex == 5)
            //{
            //    foreach (var item in CommonData.MetadataOptions5)
            //    {
            //        SpecificFilter.Items.Add(item);
            //    }
            //}


        }

        public void PopulateSatelliteList()
        {
            _selectedSatList.Clear();
            SatelliteList.Items.Clear();
            //All filter booleans default to true, regardless if they are active
            bool filtBool1 = true;
            bool filtBool2 = true;
            bool filtBool3 = true;
            bool sccBool = true;
            //Return nothing if all filters are inactive and nothing is in the SCC box. Otherwise populate the table based on filter options/SCC
            if (!filter1.IsActive && !filter2.IsActive && !filter3.IsActive && String.IsNullOrEmpty(SccTextBox.Text))
            {

            }
            else
            {
                foreach (var item in CommonData.SatCatItemList)
                {
                    var listItem = new ListViewItem();
                    filtBool1 = ReturnFilterBool(filter1, item);
                    filtBool2 = ReturnFilterBool(filter2, item);
                    filtBool3 = ReturnFilterBool(filter3, item);
                    if (!String.IsNullOrEmpty(SccTextBox.Text))
                    {
                        if (item.Scc.Contains(SccTextBox.Text))
                        {
                            sccBool = true;
                        }
                        else
                        {
                            sccBool = false;
                        }
                    }
                    if (filtBool1 && filtBool2 & filtBool3 && sccBool)
                    {
                        listItem.Text = item.Scc;
                        listItem.SubItems.Add(item.CommonName);
                        listItem.SubItems.Add(item.Metadata1);
                        SatelliteList.Items.Add(listItem);
                    }
                }                   
            }
        }

        private bool ReturnFilterBool(FilterConfig config, SatCatItem item)
        {
            bool filtBool = true;
            if (config.IsActive)
            {
                switch (config.FilterMetadataID)
                {
                    case 1:
                        filtBool = config.SelectedOptions.Contains(item.Metadata1);
                        break;
                    case 2:
                        filtBool = config.SelectedOptions.Contains(item.Metadata2);
                        break;
                    case 3:
                        filtBool = config.SelectedOptions.Contains(item.Metadata3);
                        break;
                    case 4:
                        filtBool = config.SelectedOptions.Contains(item.Metadata4);
                        break;
                    case 5:
                        filtBool = config.SelectedOptions.Contains(item.Metadata5);
                        break;
                    case 6:
                        filtBool = config.SelectedOptions.Contains(item.Fofo);
                        break;
                    default:
                        filtBool = true;
                        break;
                }
            }
            return filtBool;
        }

        private void Select_Click(object sender, EventArgs e)
        {
            if (SatelliteList.FocusedItem != null)
            {
                foreach (int index in SatelliteList.SelectedIndices)
                {
                    SatelliteList.Items[index].Font = new Font(SatelliteList.Items[index].Font, FontStyle.Bold);
                    if (!_selectedSatList.Contains(SatelliteList.Items[index].SubItems[0].Text))
                    {
                        _selectedSatList.Add(SatelliteList.Items[index].SubItems[0].Text);
                    }
                }             
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            _selectedSatList.Clear();
            for (int i = 0; i < SatelliteList.Items.Count; i++)
            {
                SatelliteList.Items[i].Font = new Font(SatelliteList.Items[i].Font, FontStyle.Bold);
                _selectedSatList.Add(SatelliteList.Items[i].SubItems[0].Text);
            }
        }

        private void Unselect_Click(object sender, EventArgs e)
        {
            if (SatelliteList.FocusedItem != null)
            {
                foreach (int index in SatelliteList.SelectedIndices)
                {
                    SatelliteList.Items[index].Font = new Font(SatelliteList.Items[index].Font, FontStyle.Regular);
                    if (_selectedSatList.Contains(SatelliteList.Items[index].SubItems[0].Text))
                    {
                        _selectedSatList.Remove(SatelliteList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void UnselectAll_Click(object sender, EventArgs e)
        {
            _selectedSatList.Clear();
            for (int i = 0; i < SatelliteList.Items.Count; i++)
            {
                SatelliteList.Items[i].Font = new Font(SatelliteList.Items[i].Font, FontStyle.Regular);
                
            }
        }
        private void CoordSystem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ImportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)ImportType.SelectedItem=="Orbital Elements")
            {
                Browse.Enabled = false;
                TCEFile.Enabled = false;
                SMAValue.Enabled = true;
                EccValue.Enabled = true;
                IncValue.Enabled = true;
                RAANValue.Enabled = true;
                AoPValue.Enabled = true;
                TAValue.Enabled = true;
                NameValue.Enabled = true;
                ElementsBox.Visible = true;
                DatabaseBox.Visible = false;
            }
            else if ((string)ImportType.SelectedItem == "TLE/TCE")
            {
                Browse.Enabled = true;
                TCEFile.Enabled = true;
                SMAValue.Enabled = false;
                EccValue.Enabled = false;
                IncValue.Enabled = false;
                RAANValue.Enabled = false;
                AoPValue.Enabled = false;
                TAValue.Enabled = false;
                NameValue.Enabled = false;
                DatabaseBox.Visible = false;
                ElementsBox.Visible = false;
                TimeSlip.Enabled = false;
                TimeSlip.Visible = false;
                SlipTime.Visible = false;
                SlipTime.Enabled = false;
                TimeLabel.Visible = false;
            }
            else if ((string)ImportType.SelectedItem == "Satellite Database")
            {
                Browse.Enabled = false;
                TCEFile.Enabled = false;
                SMAValue.Enabled = false;
                EccValue.Enabled = false;
                IncValue.Enabled = false;
                RAANValue.Enabled = false;
                AoPValue.Enabled = false;
                TAValue.Enabled = false;
                NameValue.Enabled = false;
                ElementsBox.Visible = false;
                DatabaseBox.Visible = true;
                if (CommonData.SatCatItemList.Count == 0 || CommonData.SatCatItemList == null)
                {
                    ReadWrite.ReadSatCat();
                    SatelliteList.Columns[2].Text = CommonData.MetadataTypeList[0];
                }
                PopulateSatelliteList();
            }
            else if ((string)ImportType.SelectedItem == "Ephemeris")
            {
                Browse.Enabled = true;
                TCEFile.Enabled = true;
                SMAValue.Enabled = false;
                EccValue.Enabled = false;
                IncValue.Enabled = false;
                RAANValue.Enabled = false;
                AoPValue.Enabled = false;
                TAValue.Enabled = false;
                NameValue.Enabled = false;
                DatabaseBox.Visible = false;
                ElementsBox.Visible = false;
                TimeSlip.Enabled = true;
                TimeSlip.Visible = true;
                TimeSlip.Checked = false;
                SlipTime.Visible = true;
                SlipTime.Enabled = false;
                TimeLabel.Visible = true;
            }
        }

        private void ConstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)ConstType.SelectedItem== "Default Constellation")
            {
                ConstName.Enabled = false;
                ConstName.Visible = false;
                NameLabel.Visible = false;
                ExistingConst.Enabled = false;
                ExistingConst.Visible = false;
            }
            else if ((string)ConstType.SelectedItem == "Existing Constellation")
            {
                ConstName.Enabled = false;
                ConstName.Visible = false;
                NameLabel.Visible = true;
                ExistingConst.Enabled = true;
                ExistingConst.Visible = true;
            }
            else if ((string)ConstType.SelectedItem == "New Constellation")
            {
                ConstName.Enabled = true;
                ConstName.Visible = true;
                NameLabel.Visible = true;
                ExistingConst.Enabled = false;
                ExistingConst.Visible = false;
            }
        }

        private void WaitTimer_Tick(object sender, EventArgs e)
        {
            WaitTimer.Stop();
        }

        private void TimeSlip_CheckedChanged(object sender, EventArgs e)
        {
            if (TimeSlip.Checked)
            {
                SlipTime.Enabled = true;
            }
            else
            {
                SlipTime.Enabled = false;
            }
        }

        private void filterButton1_Click(object sender, EventArgs e)
        {
            FilterForm form = new FilterForm(filter1,CommonData.MetadataTypeList, this);
            form.StartPosition = FormStartPosition.Manual;
            Point location = filterButton1.PointToScreen(filterButton1.Location);
            form.Location = location;
            form.ShowDialog();
            //Change button image if filter is active
            if (filter1.FilterType != "None")
            {
                filterButton1.ImageIndex = 0;
            }
            else
            {
                filterButton1.ImageIndex = 1;
            }
        }

        private void filterButton2_Click(object sender, EventArgs e)
        {
            FilterForm form = new FilterForm(filter2, CommonData.MetadataTypeList, this);
            form.StartPosition = FormStartPosition.Manual;
            Point location = filterButton2.PointToScreen(filterButton2.Location);
            form.Location = new Point(location.X - 50, location.Y);
            form.ShowDialog();
            //Change button image if filter is active
            if (filter2.FilterType != "None")
            {
                filterButton2.ImageIndex = 0;
            }
            else
            {
                filterButton2.ImageIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilterForm form = new FilterForm(filter3, CommonData.MetadataTypeList, this);
            form.StartPosition = FormStartPosition.Manual;
            Point location = filterButton3.PointToScreen(filterButton3.Location);
            form.Location = new Point(location.X - 105, location.Y);
            form.ShowDialog();
            //Change button image if filter is active
            if (filter3.FilterType != "None")
            {
                filterButton3.ImageIndex = 0;
            }
            else
            {
                filterButton3.ImageIndex = 1;
            }
        }

        private void SccTextBox_TextChanged(object sender, EventArgs e)
        {
            PopulateSatelliteList();
        }
    }
}

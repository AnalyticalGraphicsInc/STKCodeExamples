﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;

namespace OperatorsToolbox.FacilityCreator
{
    public partial class FacilityCreatorPlugin : OpsPluginControl
    {
        List<SensorCadance> _cadances;

        public FacilityCreatorPlugin()
        {
            InitializeComponent();
            CommonData.Cadences = new List<SensorCadance>();
            _cadances = CommonData.Cadences;
            ConstType.Items.Add("None");
            ConstType.Items.Add("Existing Constellation");
            ConstType.Items.Add("New Constellation");
            ConstType.SelectedIndex = 0;
            ConstName.Enabled = false;
            ConstName.Visible = false;
            NameLabel.Visible = false;
            ExistingConst.Enabled = false;
            ExistingConst.Visible = false;
            Latitude.Text = "0";
            Longitude.Text = "0";
            Altitude.Text = "0";
            FacilityName.Text = "NewFacility";

            SensorType.Items.Add("None");
            SensorType.Items.Add("Optical");
            SensorType.Items.Add("Radar");
            SensorType.SelectedIndex = 0;

            PopulateCadanceList();
            PopulateExistingConstellations();
            ManualInput.Checked = true;
        }

        private void PopulateExistingConstellations()
        {
            IAgExecCmdResult result;
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
                if (ExistingConst.Items.Count>0)
                {
                    ExistingConst.SelectedIndex = 0;
                }
            }


        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null && CadanceList.FocusedItem.Index != -1)
            {
                IAgStkObject facObj;
                IAgFacility fac;
                IAgConstellation optAssets=null;
                IAgConstellation radAssets=null;
                IAgStkObject constObj;
                IAgStkObject sensor;
                if (_cadances[CommonData.CadenceSelected].NumOptical>0)
                {
                    constObj = CreatorFunctions.GetCreateConstellation(_cadances[CommonData.CadenceSelected].Name + "_Opt");
                    optAssets = constObj as IAgConstellation;
                    optAssets.Objects.RemoveAll();
                }
                if (_cadances[CommonData.CadenceSelected].NumRadars>0)
                {
                    constObj = CreatorFunctions.GetCreateConstellation(_cadances[CommonData.CadenceSelected].Name + "_Rad");
                    radAssets = constObj as IAgConstellation;
                    radAssets.Objects.RemoveAll();
                }
                foreach (var item in _cadances[CommonData.CadenceSelected].FacilityList)
                {
                    facObj = CreatorFunctions.GetCreateFacility(item.Name);
                    fac = facObj as IAgFacility;
                    fac.Position.AssignGeodetic(Double.Parse(item.Latitude), Double.Parse(item.Longitude), Double.Parse(item.Altitude));
                    fac.AltRef = AgEAltRefType.eWGS84;
                    CreatorFunctions.ChangeObjectColor(facObj.Path, (CustomUserInterface.ColorOptions)Enum.Parse(typeof(CustomUserInterface.ColorOptions), _cadances[CommonData.CadenceSelected].CadenceColor));
                    if (item.IsOpt)
                    {
                        foreach (FCSensor fsensor in item.Sensors)
                        {
                            sensor = FacilityCreatorFunctions.AttachFacilityOptical(facObj, item.Name + "_" + fsensor.SensorName, fsensor.OParams);
                            if (!optAssets.Objects.Contains(sensor.Path))
                            {
                                optAssets.Objects.AddObject(sensor);
                            }
                        }
                    }
                    else
                    {
                        foreach (FCSensor fsensor in item.Sensors)
                        {
                            sensor = FacilityCreatorFunctions.AttachFacilityRadar(facObj, item.Name + "_" + fsensor.SensorName, fsensor.RParams);
                            if (!optAssets.Objects.Contains(sensor.Path))
                            {
                                radAssets.Objects.AddObject(sensor);
                            }
                            
                        }
                    }
                }
            }
        }

        private void CadanceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null && CadanceList.FocusedItem.Index != -1)
            {
                CommonData.CadenceSelected = CadanceList.FocusedItem.Index;
            }
        }

        private void AddCadance_Click(object sender, EventArgs e)
        {
            CommonData.CadenceEdit = false;
            CommonData.CadenceSaved = false;
            CadanceDefinitionForm form = new CadanceDefinitionForm();
            form.ShowDialog();
            if (CommonData.CadenceSaved)
            {
                ReadWrite.WriteCadenceDatabase();
                PopulateCadanceList();
            }
        }

        private void EditCadance_Click(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null && CadanceList.FocusedItem.Index != -1)
            {
                CommonData.CadenceEdit = true;
                CommonData.CadenceSaved = false;
                CadanceDefinitionForm form = new CadanceDefinitionForm();
                form.ShowDialog();
                if (CommonData.CadenceSaved)
                {
                    ReadWrite.WriteCadenceDatabase();
                    PopulateCadanceList();
                }
            }
        }

        private void DeleteCadance_Click(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null && CadanceList.FocusedItem.Index != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you wish to delete this Cadance?\nIt will also be removed from the database, if applicable", "Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    CommonData.Cadences.RemoveAt(CadanceList.FocusedItem.Index);
                    CadanceList.Items.RemoveAt(CadanceList.FocusedItem.Index);
                    ReadWrite.WriteCadenceDatabase();
                }
                else if (dialogResult == DialogResult.No)
                {
                    
                }
            }
        }

        private void ConstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)ConstType.SelectedItem == "None")
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

        private void GenerateSingle_Click(object sender, EventArgs e)
        {
            Tuple<int, string> check = FieldCheck();
            if (check.Item1==0)
            {
                if (ManualInput.Checked)
                {
                    try
                    {
                        IAgStkObject facObj = CreatorFunctions.GetCreateFacility(FacilityName.Text);
                        IAgFacility fac = facObj as IAgFacility;
                        IAgStkObject sensor = null;
                        fac.Position.AssignGeodetic(Double.Parse(Latitude.Text), Double.Parse(Longitude.Text), Double.Parse(Altitude.Text));
                        fac.AltRef = AgEAltRefType.eWGS84;
                        if (SensorType.SelectedIndex == 1)
                        {
                            OpticalParams oParams = new OpticalParams();
                            oParams.MinEl = "0";
                            oParams.MaxEl = "90";
                            oParams.MinRange = "4800";
                            oParams.MaxRange = "90000";
                            oParams.LunarExAngle = "10";
                            oParams.SunElAngle = "-12";
                            oParams.HalfAngle = "70";
                            oParams.MinAz = "0";
                            oParams.MaxAz = "360";
                            sensor = FacilityCreatorFunctions.AttachFacilityOptical(facObj, FacilityName.Text + "_Opt", oParams);
                        }
                        else if (SensorType.SelectedIndex == 2)
                        {
                            RadarParams rParams = new RadarParams();
                            rParams.MinEl = "0";
                            rParams.MaxEl = "90";
                            rParams.MinRange = "1600";
                            rParams.MaxRange = "40000";
                            rParams.SolarExAngle = "10";
                            rParams.HalfAngle = "85";
                            rParams.MinAz = "0";
                            rParams.MaxAz = "360";
                            sensor = FacilityCreatorFunctions.AttachFacilityRadar(facObj, FacilityName.Text + "_Radar", rParams);
                        }
                        else
                        {

                        }
                        if (ConstType.SelectedIndex != 0)
                        {
                            IAgStkObject constObj = null;
                            IAgConstellation constel = null;
                            if (ConstType.SelectedIndex == 1)
                            {
                                constObj = CreatorFunctions.GetCreateConstellation(ExistingConst.Text);
                                constel = constObj as IAgConstellation;
                            }
                            else if (ConstType.SelectedIndex == 2)
                            {
                                constObj = CreatorFunctions.GetCreateConstellation(ConstName.Text);
                                constel = constObj as IAgConstellation;
                            }
                            if (SensorType.SelectedIndex == 0)
                            {
                                constel.Objects.AddObject(facObj);
                            }
                            else if (SensorType.SelectedIndex == 1 || SensorType.SelectedIndex == 2)
                            {
                                constel.Objects.AddObject(sensor);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not create facility");
                    }
                }
                else if (ImportFromFile.Checked)
                {
                    if (!String.IsNullOrEmpty(FilenameText.Text))
                    {
                        SensorCadance cad = new SensorCadance();
                        cad.FacilityList = new List<FcFacility>();
                        cad.CadenceColor = "Custom";
                        cad.Name = "NewCadence";
                        IAgStkObject facObj;
                        IAgFacility fac;
                        IAgStkObject sensor;
                        List<GroundLocation> locations = null;
                        if (FilenameText.Text.Contains(".json"))
                        {
                            try
                            {
                                List<SensorCadance> tempCadences = ReadWrite.ReadCadences(FilenameText.Text);
                                if (SaveData.Checked)
                                {
                                    foreach (var item in tempCadences)
                                    {
                                        CommonData.Cadences.Add(item);
                                    }
                                    ReadWrite.WriteCadenceDatabase();
                                    PopulateCadanceList();
                                }
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("Json Error");
                            }
                        }
                        else
                        {
                            locations = ReadWrite.ReadFacilityFile(FilenameText.Text);
                            foreach (GroundLocation loc in locations)
                            {
                                FcFacility fcFac = new FcFacility();
                                fcFac.Name = loc.LocationName;
                                fcFac.Latitude = loc.Latitude.ToString();
                                fcFac.Longitude = loc.Longitude.ToString();
                                fcFac.Altitude = loc.Altitude.ToString();
                                facObj = CreatorFunctions.GetCreateFacility(loc.LocationName);
                                fac = facObj as IAgFacility;
                                sensor = null;
                                fac.Position.AssignGeodetic(loc.Latitude, loc.Longitude, loc.Altitude);
                                fac.AltRef = AgEAltRefType.eWGS84;

                                FCSensor fcSensor = new FCSensor();
                                fcSensor.SensorName = loc.LocationName + "_Opt";
                                if (SensorType.SelectedIndex == 0 || SensorType.SelectedIndex == 1)
                                {
                                    OpticalParams oParams = new OpticalParams();
                                    cad.Type = "Opt";
                                    cad.NumOptical = locations.Count;
                                    cad.NumRadars = 0;
                                    fcFac.Type = "Optical";
                                    fcFac.IsOpt = true;
                                    oParams.MinEl = "0";
                                    oParams.MaxEl = "90";
                                    oParams.MinRange = "4800";
                                    oParams.MaxRange = "90000";
                                    oParams.LunarExAngle = "10";
                                    oParams.SunElAngle = "-12";
                                    oParams.HalfAngle = "70";
                                    oParams.MinAz = "0";
                                    oParams.MaxAz = "360";
                                    oParams.Az = "0";
                                    oParams.El = "90";
                                    fcSensor.OParams = oParams;
                                    if (SensorType.SelectedIndex == 1)
                                    {
                                        sensor = FacilityCreatorFunctions.AttachFacilityOptical(facObj, loc.LocationName + "_Opt", oParams);
                                    }
                                }
                                else if (SensorType.SelectedIndex == 2)
                                {
                                    RadarParams rParams = new RadarParams();
                                    fcSensor.SensorName = loc.LocationName + "_Rad";
                                    cad.Type = "Rad";
                                    cad.NumOptical = 0;
                                    cad.NumRadars = locations.Count;
                                    fcFac.Type = "Radar";
                                    fcFac.IsOpt = false;
                                    rParams.MinEl = "0";
                                    rParams.MaxEl = "90";
                                    rParams.MinRange = "1600";
                                    rParams.MaxRange = "40000";
                                    rParams.SolarExAngle = "10";
                                    rParams.HalfAngle = "85";
                                    rParams.MinAz = "0";
                                    rParams.MaxAz = "360";
                                    rParams.Az = "0";
                                    rParams.El = "90";
                                    fcSensor.RParams = rParams;
                                    sensor = FacilityCreatorFunctions.AttachFacilityRadar(facObj, loc.LocationName + "_Radar", rParams);
                                }
                                else
                                {

                                }
                                fcFac.Sensors.Add(fcSensor);
                                cad.FacilityList.Add(fcFac);
                                if (ConstType.SelectedIndex != 0)
                                {
                                    IAgStkObject constObj = null;
                                    IAgConstellation constel = null;
                                    if (ConstType.SelectedIndex == 1)
                                    {
                                        constObj = CreatorFunctions.GetCreateConstellation(ExistingConst.Text);
                                        constel = constObj as IAgConstellation;
                                    }
                                    else if (ConstType.SelectedIndex == 2)
                                    {
                                        constObj = CreatorFunctions.GetCreateConstellation(ConstName.Text);
                                        constel = constObj as IAgConstellation;
                                    }
                                    if (SensorType.SelectedIndex == 0)
                                    {
                                        constel.Objects.AddObject(facObj);
                                    }
                                    else if (SensorType.SelectedIndex == 1 || SensorType.SelectedIndex == 2)
                                    {
                                        constel.Objects.AddObject(sensor);
                                    }
                                }
                            }
                            if (SaveData.Checked)
                            {
                                CommonData.Cadences.Add(cad);
                                ReadWrite.WriteCadenceDatabase();
                                PopulateCadanceList();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please choose a valid input file");
                    }
                }
            }
            else
            {
                MessageBox.Show(check.Item2);
            }
        }

        private void Duplicate_Click(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null && CadanceList.FocusedItem.Index != -1)
            {
                SensorCadance newCadance = CreateDuplicateCadance(CommonData.Cadences[CommonData.CadenceSelected]);
                CommonData.Cadences.Add(newCadance);
                ReadWrite.WriteCadenceDatabase();
                PopulateCadanceList();
            }
        }

        private void PopulateCadanceList()
        {
            CadanceList.Items.Clear();
            string databaseFileStr = Path.Combine(@CommonData.InstallDir, "Databases\\CadenceDatabase.json");
            string localFileStr = CommonData.DirectoryStr + "\\CadenceData.json";
            List<SensorCadance> currentList = new List<SensorCadance>();
            if (File.Exists(@databaseFileStr))
            {
                currentList = ReadWrite.ReadCadences(databaseFileStr);
                if (currentList != null)
                {
                    foreach (var cadance in currentList)
                    {
                        cadance.SaveToDatabase = true;
                    }
                }
                else
                {
                    currentList = new List<SensorCadance>();
                }
            }
            //if (File.Exists(@localFileStr))
            //{
            //    List<SensorCadance> secList = new List<SensorCadance>();
            //    secList = ReadWrite.ReadCadences(localFileStr);
            //    if (secList != null)
            //    {
            //        foreach (var item in secList)
            //        {
            //            item.SaveToDatabase = false;
            //            currentList.Add(item);
            //        }
            //    }
            //}
            _cadances = currentList;
            CommonData.Cadences = currentList;
            if (CommonData.Cadences != null)
            {
                foreach (var item in CommonData.Cadences)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = item.Name;
                    listItem.SubItems.Add(item.Type);
                    listItem.SubItems.Add((item.NumOptical + item.NumRadars).ToString());
                    CadanceList.Items.Add(listItem);
                }
            }
            else
            {
                CommonData.Cadences = new List<SensorCadance>();
            }
        }

        private SensorCadance CreateDuplicateCadance(SensorCadance og)
        {
            SensorCadance newCadance = new SensorCadance();
            newCadance.Name = og.Name;
            List<FcFacility> newlist = new List<FcFacility>(og.FacilityList);
            newCadance.FacilityList = newlist;
            newCadance.NumOptical = og.NumOptical;
            newCadance.NumRadars = og.NumRadars;
            newCadance.SaveToDatabase = og.SaveToDatabase;
            newCadance.Type = og.Type;
            return newCadance;
        }

        private Tuple<int, string> FieldCheck()
        {
            int check = 0;
            string checkStr = "Input errors:\n";
            double temp = 0.0;
            bool isNumerical;
            isNumerical = Double.TryParse(Latitude.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Latitude field not a number\n";
            }
            isNumerical = Double.TryParse(Longitude.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Longitude field not a number\n";
            }
            isNumerical = Double.TryParse(Altitude.Text, out temp);
            if (!isNumerical || temp < 0)
            {
                check = 1;
                checkStr = checkStr + "Altitude field not a number or is negative\n";
            }
            if (FacilityName.Text=="" || FacilityName.Text==null)
            {
                check = 1;
                checkStr = checkStr + "Facility name required\n";
            }
            if (ConstType.SelectedIndex==2 && (ConstName.Text=="" || ConstName.Text==null))
            {
                check = 1;
                checkStr = checkStr + "Constellation name required\n";
            }
            if (ConstType.SelectedIndex == 1 && ExistingConst.SelectedIndex==-1)
            {
                check = 1;
                checkStr = checkStr + "Select a valid constellation\n";
            }
            return Tuple.Create(check, checkStr);
        }

        #region Tool Tips
        private void AddCadance_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.AddCadance, "Add Cadence");
        }

        private void Duplicate_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Duplicate, "Duplicate Cadence");
        }

        private void EditCadance_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.EditCadance, "Edit Cadence");
        }

        private void DeleteCadance_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.DeleteCadance, "Delete Cadence");
        }
        #endregion

        private void ManualInput_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualInput.Checked)
            {
                ImportFromFile.Checked = false;
                FacilityName.Enabled = true;
                Latitude.Enabled = true;
                Longitude.Enabled = true;
                Altitude.Enabled = true;
                FilenameText.Enabled = false;
                FileBrowse.Enabled = false;
                SaveData.Enabled = false;
            }
        }

        private void ImportFromFile_CheckedChanged(object sender, EventArgs e)
        {
            if (ImportFromFile.Checked)
            {
                ManualInput.Checked = false;
                FacilityName.Enabled = false;
                Latitude.Enabled = false;
                Longitude.Enabled = false;
                Altitude.Enabled = false;
                FilenameText.Enabled = true;
                FileBrowse.Enabled = true;
                SaveData.Enabled = true;
            }
        }

        private void FileBrowse_Click(object sender, EventArgs e)
        {
            SettingsForm.BrowseFileExplorer("C:\\", "Choose Input File",FilenameText);
        }
    }
}

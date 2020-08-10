using System;
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
                    if (item.IsOpt)
                    {
                        sensor = CreatorFunctions.AttachFacilityOptical(facObj, item.Name + "_Opt",item.OParams);
                        optAssets.Objects.AddObject(sensor);
                    }
                    else
                    {
                        sensor = CreatorFunctions.AttachFacilityRadar(facObj, item.Name + "_Radar", item.RParams);
                        radAssets.Objects.AddObject(sensor);
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
                try
                {
                    IAgStkObject facObj = CreatorFunctions.GetCreateFacility(FacilityName.Text);
                    IAgFacility fac = facObj as IAgFacility;
                    IAgStkObject sensor=null;
                    fac.Position.AssignGeodetic(Double.Parse(Latitude.Text), Double.Parse(Longitude.Text), Double.Parse(Altitude.Text));
                    fac.AltRef = AgEAltRefType.eWGS84;
                    if (SensorType.SelectedIndex ==1)
                    {
                        OpticalParams oParams = new OpticalParams();
                        oParams.MinEl = "0";
                        oParams.MaxEl = "90";
                        oParams.MinRange = "4800";
                        oParams.MaxRange = "90000";
                        oParams.LunarExAngle = "10";
                        oParams.SunElAngle = "-12";
                        oParams.HalfAngle = "70";
                        sensor = CreatorFunctions.AttachFacilityOptical(facObj, FacilityName.Text + "_Opt", oParams);
                    }
                    else if (SensorType.SelectedIndex==2)
                    {
                        RadarParams rParams = new RadarParams();
                        rParams.MinEl = "0";
                        rParams.MaxEl = "90";
                        rParams.MinRange = "1600";
                        rParams.MaxRange = "40000";
                        rParams.SolarExAngle = "10";
                        rParams.HalfAngle = "85";
                        sensor = CreatorFunctions.AttachFacilityRadar(facObj, FacilityName.Text + "_Radar", rParams);
                    }
                    else
                    {

                    }
                    if (ConstType.SelectedIndex!=0)
                    {
                        IAgStkObject constObj=null;
                        IAgConstellation constel=null;
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
            if (File.Exists(@localFileStr))
            {
                List<SensorCadance> secList = new List<SensorCadance>();
                secList = ReadWrite.ReadCadences(localFileStr);
                if (secList != null)
                {
                    foreach (var item in secList)
                    {
                        item.SaveToDatabase = false;
                        currentList.Add(item);
                    }
                }
            }
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
    }
}

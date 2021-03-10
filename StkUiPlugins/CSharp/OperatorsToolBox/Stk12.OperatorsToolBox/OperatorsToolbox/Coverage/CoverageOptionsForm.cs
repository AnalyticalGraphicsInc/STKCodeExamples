using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using OperatorsToolbox.FacilityCreator;
using OperatorsToolbox.GroundEvents;

namespace OperatorsToolbox.Coverage
{
    public partial class CoverageOptionsForm : Form
    {
        public string[] ParsingFormats = new string[] { "dd MMM yyyy HH:mm:ss", "d MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss.000", "d MMM yyyy HH:mm:ss.000" }; //add others to this as we find them
        private int _assetIndex;
        private int _coverageDataIndex = -1;
        private string _startTimeStk = null;
        private string _stopTimeStk = null;
        private CoverageData _currentData;
        private StkObjectsLibrary _library = new StkObjectsLibrary();
        private CoverageData _current = new CoverageData();
        private List<GroundLocation> _locations;
        private List<string> _constraintObjects;
        private List<string> _aoiObjects;
        private string _zoomObject = null;

        public CoverageOptionsForm()
        {
            _constraintObjects = new List<string>();
            _aoiObjects = new List<string>();
            InitializeComponent();
            IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
            ObjectGrid.Rows.Clear();
            PopulateTypeSelect();
            PopulateAoi();
            PopulateGroundObjects();
            PopulateAssets();
            PopulateClasses();
            PopulateFom();
            if (CountrySelect.Items.Count > 0)
            {
                CountrySelect.SelectedIndex = 0;
            }
            CommonData.CovDefFail = false;
            

            if (CommonData.CovEdit)
            {
                _currentData = new CoverageData(CommonData.CoverageList[CommonData.CoverageIndex]);
                StartTime.Text = _currentData.StartTime;
                StopTime.Text = _currentData.StopTime;
                PointGran.Text = _currentData.PointGran;
                FOMType.SelectedIndex = _currentData.FomType;
                ContourStart.Text = _currentData.ContourStart;
                ContourStep.Text = _currentData.ContourStep;
                ContourStop.Text = _currentData.ContourStop;
                CoverageName.Text = _currentData.CdName;

                if (_currentData.Type == "Object AOI")
                {
                    CoverageTypeSelect.SelectedIndex = 0;
                    if (_currentData.CoverageShape == 1)
                    {
                        BoundingBox.Checked = false;
                        EllipseSelect.Checked = true;
                        MajorAxis.Text = _currentData.MajorAxis.ToString();
                        MinorAxis.Text = _currentData.MinorAxis.ToString();
                    }
                    else if (_currentData.CoverageShape == 2)
                    {
                        BoundingBox.Checked = true;
                        EllipseSelect.Checked = false;
                        BoundingLimit.Text = _currentData.BoundSize.ToString();
                    }

                    InitializeDataGrid(_currentData);
                }
                else if (_currentData.Type == "Country/Region")
                {
                    CoverageTypeSelect.SelectedIndex = 1;
                    int index = CountrySelect.Items.IndexOf(_currentData.Country);
                    if (index != -1)
                    {
                        CountrySelect.SelectedIndex = index;
                    }
                }
                else if (_currentData.Type == "Global")
                {
                    CoverageTypeSelect.SelectedIndex = 2;
                }

                FindExistingAssets(_currentData.CdName);

                if (_currentData.UseConstraint)
                {
                    UseConstraintObject.Checked = true;
                    FindConstraintObject(_currentData.ConstraintObject);
                }

            }
            else
            {
                _currentData = new CoverageData();
                StartTime.Text = scenario.StartTime;
                StopTime.Text = scenario.StopTime;
                PointGran.Text = "1";
                CoverageName.Text = "MyCoverage";
                MajorAxis.Text = "1000";
                MinorAxis.Text = "1000";
                BoundingLimit.Text = "2";
                UseConstraintObject.Checked = false;
                EllipseSelect.Checked = true;
            }
            _startTimeStk = scenario.StartTime;
            _stopTimeStk = scenario.StopTime;
        }

        private void FindConstraintObject(string constraintObject)
        {
            string className = _library.ClassNameFromObjectPath(constraintObject);
            int index = ConstraintClass.Items.IndexOf(className);
            if (index != -1)
            {
                ConstraintClass.SelectedIndex = index;
            }
            for (int i = 0; i < _constraintObjects.Count; i++)
            {
                if (_constraintObjects[i] == constraintObject)
                {
                    ConstraintObject.SelectedIndex = i;
                }
            }


        }

        private void GenerateCompute_Click(object sender, EventArgs e)
        {
            Generate_Click(sender, e);
            CommonData.CoverageCompute = true;
        }

        //Main Function
        private void Generate_Click(object sender, EventArgs e)
        {
            CommonData.CoverageCompute = false;
            string cmd;
            IAgExecCmdResult result;
            int check = FieldCheck();
            _currentData.TargetList = new List<string>();
            if (check == 0)
            {
                string oaName = CoverageName.Text.Replace(' ', '_');
                string atName = null;
                if (CoverageTypeSelect.SelectedIndex == 0) //Object AOI Coverage
                {
                    _currentData.FomType = FOMType.SelectedIndex;
                    try
                    {
                        if (CommonData.CovEdit)
                        {
                            //Renaming for existing objects from previous runs of the tool
                            try
                            {
                                string path1 = "*/CoverageDefinition/" + _currentData.CdName;
                                string path2 = "*/CoverageDefinition/" + _currentData.CdName + "/FigureOfMerit/" + _currentData.CdName + "_FOM";
                                string path3 = "*/AreaTarget/" + _currentData.CdName + "_" + _currentData.ObjectName;
                                cmd = "Rename " + path2 + " " + CoverageName.Text.Replace(' ', '_') + "_FOM";
                                CommonData.StkRoot.ExecuteCommand(cmd);
                                cmd = "Rename " + path1 + " " + CoverageName.Text.Replace(' ', '_');
                                CommonData.StkRoot.ExecuteCommand(cmd);
                                for (int i = 0; i < ObjectGrid.Rows.Count; i++)
                                {
                                    atName = oaName + "_" + ObjectGrid.Rows[i].Cells[1].Value.ToString();
                                    path3 = "*/AreaTarget/" + _currentData.CdName + "_" + ObjectGrid.Rows[i].Cells[1].Value.ToString();
                                    cmd = "Rename " + path3 + " " + atName;
                                    CommonData.StkRoot.ExecuteCommand(cmd);
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        _currentData.CdName = oaName;
                        _currentData.Type = "Object AOI";
                        _currentData.StartTime = StartTime.Text;
                        _currentData.StopTime = StopTime.Text;
                        _currentData.PointGran = PointGran.Text;
                        _currentData.FomType = FOMType.SelectedIndex;
                        //currentData.ObjectName = SSRList.FocusedItem.Text;
                        CommonData.OaName = oaName;
                        IAgStkObject atObj = null;

                        //Create coverage definition and FOM
                        IAgStkObject cdObj = CreatorFunctions.GetCreateCoverageDefinition(oaName);
                        IAgCoverageDefinition cd = cdObj as IAgCoverageDefinition;
                        cd.Grid.BoundsType = AgECvBounds.eBoundsCustomRegions;
                        IAgCvBoundsCustomRegions boundRegion = cd.Grid.Bounds as IAgCvBoundsCustomRegions;
                        CoverageFunctions.DefineCoverage(oaName, PointGran.Text);
                        IAgCvAssetListCollection aList = cd.AssetList;
                        aList.RemoveAll();
                        IAgStkObject fomObj = CreatorFunctions.GetCreateFigureOfMerit(cdObj, oaName + "_FOM");

                        //Tie to area target, set points, clear access, turn Auto Recompute off

                        DataGridViewCheckBoxCell cell = null;
                        bool noZoom = true;
                        int index;
                        for (int i = 0; i < ObjectGrid.Rows.Count; i++)
                        {
                            atName = oaName + "_" + ObjectGrid.Rows[i].Cells[1].Value.ToString();
                            atObj = CreatorFunctions.GetCreateAreaTarget(atName);
                            IAgAreaTarget at = (IAgAreaTarget)atObj;

                            _currentData.TargetList.Add(ObjectGrid.Rows[i].Cells[1].Value.ToString());
                            cell = ObjectGrid.Rows[i].Cells[2] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(cell.Value))
                            {
                                _currentData.TargetName = ObjectGrid.Rows[i].Cells[1].Value.ToString();
                                noZoom = false;
                            }
                            index = _locations.IndexOf(_locations.Where(p => p.LocationName == (ObjectGrid.Rows[i].Cells[1].Value.ToString())).FirstOrDefault());
                            if (index != -1)
                            {
                                _currentData.Latitude = _locations[index].Latitude;
                                _currentData.Longitude = _locations[index].Longitude;
                                //Create AT boundries based on selected option and values
                                if (EllipseSelect.Checked)
                                {
                                    CoverageFunctions.DefineEllipseAt(at, _currentData.Latitude, _currentData.Longitude, Double.Parse(MajorAxis.Text), Double.Parse(MinorAxis.Text));
                                    _currentData.MajorAxis = Double.Parse(MajorAxis.Text);
                                    _currentData.MinorAxis = Double.Parse(MinorAxis.Text);
                                    _currentData.CoverageShape = 1;
                                }
                                else
                                {
                                    CoverageFunctions.DefineBoxAt(at, _currentData.Latitude, _currentData.Longitude, Double.Parse(BoundingLimit.Text));
                                    _currentData.BoundSize = Double.Parse(BoundingLimit.Text);
                                    _currentData.CoverageShape = 2;
                                }
                                boundRegion.AreaTargets.Add("AreaTarget/" + atName);
                            }
                        }
                        //In case where no zoom object is selected, enable home view instead
                        if (noZoom)
                        {
                            _currentData.TargetName = "Earth";
                        }

                        if (UseConstraintObject.Checked)
                        {
                            _currentData.UseConstraint = true;
                            if (ConstraintObject.SelectedIndex != -1)
                            {
                                _currentData.ConstraintObject = _constraintObjects[ConstraintObject.SelectedIndex];
                                CoverageFunctions.SetGridConstraint(cd, ConstraintClass.SelectedItem.ToString(), _constraintObjects[ConstraintObject.SelectedIndex]);
                            }
                        }

                        //Assign assets to coverage definition
                        cd.AssetList.RemoveAll();
                        foreach (var asset in _currentData.AssetList)
                        {
                            CoverageFunctions.AssignAsset("Constellation", oaName, asset);
                        }

                        _startTimeStk = StartTime.Text;
                        _stopTimeStk = StopTime.Text;

                        CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + oaName + " Interval \"" + _startTimeStk + "\" \"" + _stopTimeStk + "\"");

                        //FOM Limits
                        _currentData.ContourStart = ContourStart.Text;
                        _currentData.ContourStop = ContourStop.Text;
                        _currentData.ContourStep = ContourStep.Text;

                        if (FOMType.SelectedIndex != 7)
                        {
                            DefineFom(oaName);
                        }

                        CommonData.NewCoverage = true;
                        //Assign to saved list based on edit or new coverage
                        if (CommonData.CovEdit)
                        {
                            CommonData.CoverageList.RemoveAt(CommonData.CoverageIndex);
                            CommonData.CoverageList.Insert(CommonData.CoverageIndex, _currentData);
                        }
                        else
                        {
                            CommonData.CoverageList.Add(_currentData);
                        }
                    }
                    catch (Exception)
                    {
                        string mes = "Could not Define Coverage";
                        MessageBox.Show(mes);
                        CommonData.CovDefFail = true;
                    }
                }
                else if (CoverageTypeSelect.SelectedIndex == 1) //Country/Region Coverage
                {
                    try
                    {
                        if (CommonData.CovEdit)
                        {
                            //Renaming
                            try
                            {
                                string path1 = "*/CoverageDefinition/" + _currentData.CdName;
                                string path2 = "*/CoverageDefinition/" + _currentData.CdName + "/FigureOfMerit/" + _currentData.CdName + "_FOM";
                                cmd = "Rename " + path2 + " " + CoverageName.Text.Replace(' ', '_') + "_FOM";
                                CommonData.StkRoot.ExecuteCommand(cmd);
                                cmd = "Rename " + path1 + " " + CoverageName.Text.Replace(' ', '_');
                                CommonData.StkRoot.ExecuteCommand(cmd);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        //Load area target from file
                        string areaTargetName = CountrySelect.Text.Replace(' ', '_');
                        oaName = CoverageName.Text.Replace(' ', '_');
                        CommonData.OaName = oaName;
                        //update current data from text fields to be stored in commondata afterwards
                        _currentData.CdName = oaName;
                        _currentData.Country = CountrySelect.Text;
                        _currentData.TargetName = areaTargetName;
                        _currentData.FomType = FOMType.SelectedIndex;
                        _currentData.Type = "Country/Region";
                        _currentData.PointGran = PointGran.Text;

                        IAgStkObject cdObj = CreatorFunctions.GetCreateCoverageDefinition(oaName);
                        IAgCoverageDefinition cd = cdObj as IAgCoverageDefinition;
                        IAgCvAssetListCollection aList = cd.AssetList;
                        aList.RemoveAll();
                        IAgStkObject fomObj = CreatorFunctions.GetCreateFigureOfMerit(cdObj, oaName + "_FOM");
                        List<string> atNames = new List<string>();
                        if (areaTargetName.Contains("-Group"))
                        {
                            atNames = ReadWrite.ReadATGroup(CommonData.InstallDir + "\\Databases\\AreaTargets\\" + areaTargetName + ".txt");
                            foreach (var atObj in atNames)
                            {
                                try
                                {
                                    string atPath = CommonData.InstallDir + "\\Databases\\AreaTargets\\" + atObj + ".at";
                                    cmd = "Load / */AreaTarget " + "\"" + atPath + "\"";
                                    CommonData.StkRoot.ExecuteCommand(cmd);
                                    //atNames.Add(atObj);
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                        else
                        {
                            result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */AreaTarget/" + areaTargetName);
                            if (result[0] == "1")
                            {
                            }
                            else
                            {
                                //string filename = areaTargetName + ".at";
                                //string path = Path.Combine(@CommonData.InstallDir, "Databases\\AreaTargets\\", filename);
                                //CommonData.StkRoot.Children.ImportObject(path);
                                CommonData.CoverageList.Add(_currentData);
                                string atPath = CommonData.InstallDir + "\\Databases\\AreaTargets\\" +
                                    areaTargetName + ".at";
                                cmd = "Load / */AreaTarget " + "\"" + atPath + "\"";
                                CommonData.StkRoot.ExecuteCommand(cmd);
                                atNames.Add(areaTargetName);
                            }
                        }

                        //Create coverage definition and FOM
                        //Tie to area target, set points, clear access, turn Auto Recompute off
                        CoverageFunctions.DefineCoverage(oaName, atNames, PointGran.Text);

                        if (UseConstraintObject.Checked)
                        {
                            _currentData.UseConstraint = true;
                            if (ConstraintObject.SelectedIndex != -1)
                            {
                                _currentData.ConstraintObject = _constraintObjects[ConstraintObject.SelectedIndex];
                                CoverageFunctions.SetGridConstraint(cd, ConstraintClass.SelectedItem.ToString(), _constraintObjects[ConstraintObject.SelectedIndex]);
                            }
                        }

                        //Assign assets to coverage definition
                        cd.AssetList.RemoveAll();
                        foreach (var asset in _currentData.AssetList)
                        {
                            CoverageFunctions.AssignAsset("Constellation", oaName, asset);
                        }

                        _startTimeStk = StartTime.Text;
                        _currentData.StartTime = StartTime.Text;
                        _stopTimeStk = StopTime.Text;
                        _currentData.StopTime = StopTime.Text;
                        _currentData.ContourStart = ContourStart.Text;
                        _currentData.ContourStop = ContourStop.Text;
                        _currentData.ContourStep = ContourStep.Text;
                        _currentData.UseConstraint = UseConstraintObject.Checked;

                        CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + oaName + " Interval \"" + _startTimeStk + "\" \"" + _stopTimeStk + "\"");

                        //FOM Limits
                        if (FOMType.SelectedIndex != 7)
                        {
                            DefineFom(oaName);
                        }
                        CommonData.NewCoverage = true;
                        if (CommonData.CovEdit)
                        {
                            CommonData.CoverageList.RemoveAt(CommonData.CoverageIndex);
                            CommonData.CoverageList.Insert(CommonData.CoverageIndex, _currentData);
                        }
                        else
                        {
                            CommonData.CoverageList.Add(_currentData);
                        }

                    }
                    catch (Exception)
                    {
                        string mes = "Could not Define Coverage";
                        MessageBox.Show(mes);
                        CommonData.CovDefFail = true;
                    }
                }
                else if (CoverageTypeSelect.SelectedIndex == 2) //Global Coverage
                {
                    oaName = CoverageName.Text.Replace(' ', '_');
                    CommonData.OaName = oaName;
                    try
                    {
                        if (CommonData.CovEdit)
                        {
                            //Renaming
                            try
                            {
                                string path1 = "*/CoverageDefinition/" + _currentData.CdName;
                                string path2 = "*/CoverageDefinition/" + _currentData.CdName + "/FigureOfMerit/" + _currentData.CdName + "_FOM";
                                cmd = "Rename " + path2 + " " + CoverageName.Text.Replace(' ', '_') + "_FOM";
                                CommonData.StkRoot.ExecuteCommand(cmd);
                                cmd = "Rename " + path1 + " " + CoverageName.Text.Replace(' ', '_');
                                CommonData.StkRoot.ExecuteCommand(cmd);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        //update current data from text fields to be stored in commondata afterwards
                        _currentData.CdName = oaName;
                        _currentData.Country = "Global";
                        _currentData.PointGran = PointGran.Text;
                        _currentData.FomType = FOMType.SelectedIndex;
                        _currentData.Type = "Global";
                        _startTimeStk = StartTime.Text;
                        _currentData.StartTime = StartTime.Text;
                        _stopTimeStk = StopTime.Text;
                        _currentData.StopTime = StopTime.Text;
                        _currentData.ContourStart = ContourStart.Text;
                        _currentData.ContourStop = ContourStop.Text;
                        _currentData.ContourStep = ContourStep.Text;

                        IAgStkObject cdObj = CreatorFunctions.GetCreateCoverageDefinition(oaName);
                        IAgCoverageDefinition cd = cdObj as IAgCoverageDefinition;
                        IAgCvAssetListCollection aList = cd.AssetList;
                        aList.RemoveAll();
                        IAgStkObject fomObj = CreatorFunctions.GetCreateFigureOfMerit(cdObj, oaName + "_FOM");

                        //Create coverage definition and FOM
                        CoverageFunctions.DefineGlobalCoverage(oaName, PointGran.Text);

                        if (UseConstraintObject.Checked)
                        {
                            _currentData.UseConstraint = true;
                            _currentData.ConstraintObject = _constraintObjects[ConstraintObject.SelectedIndex];
                            CoverageFunctions.SetGridConstraint(cd, ConstraintClass.SelectedItem.ToString(), _constraintObjects[ConstraintObject.SelectedIndex]);
                        }

                        //Assign assets to coverage definition
                        cd.AssetList.RemoveAll();
                        foreach (var asset in _currentData.AssetList)
                        {
                            CoverageFunctions.AssignAsset("Constellation", oaName, asset);
                        }

                        CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + oaName + " Interval \"" + _startTimeStk + "\" \"" + _stopTimeStk + "\"");

                        if (FOMType.SelectedIndex != 7)
                        {
                            DefineFom(oaName);
                        }

                        CommonData.NewCoverage = true;
                        if (CommonData.CovEdit)
                        {
                            CommonData.CoverageList.RemoveAt(CommonData.CoverageIndex);
                            CommonData.CoverageList.Insert(CommonData.CoverageIndex, _currentData);
                        }
                        else
                        {
                            CommonData.CoverageList.Add(_currentData);
                        }

                    }
                    catch (Exception)
                    {
                        string mes = "Could not Define Coverage";
                        MessageBox.Show(mes);
                        CommonData.CovDefFail = true;
                    }
                }
                this.Close();
            }
        }

        //Adding assets to coverage
        private void Assign_Click(object sender, EventArgs e)
        {
            if (AssetList.FocusedItem != null)
            {
                AssetList.Items[_assetIndex].Font = new Font(AssetList.Items[_assetIndex].Font, FontStyle.Bold);
                if (!_currentData.AssetList.Contains(AssetList.Items[_assetIndex].SubItems[0].Text))
                {
                    _currentData.AssetList.Add(AssetList.Items[_assetIndex].SubItems[0].Text);
                }
            }
        }
        //Removing assets from coverage
        private void Unselect_Click(object sender, EventArgs e)
        {
            if (AssetList.FocusedItem != null)
            {
                AssetList.Items[_assetIndex].Font = new Font(AssetList.Items[_assetIndex].Font, FontStyle.Regular);
                try
                {
                    _currentData.AssetList.Remove(AssetList.Items[_assetIndex].SubItems[0].Text);
                }
                catch (Exception)
                {

                }
            }
        }

        private void BoundingBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BoundingBox.Checked == true)
            {
                BoundingLimit.Enabled = true;
                MinorAxis.Enabled = false;
                MajorAxis.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                label9.Enabled = true;
                label10.Enabled = true;
            }
        }

        private void PopulateTypeSelect()
        {
            CoverageTypeSelect.Items.Add("Object AOI");
            CoverageTypeSelect.Items.Add("Country/Region");
            CoverageTypeSelect.Items.Add("Global");

            CoverageTypeSelect.SelectedIndex = 0;

        }

        private void PopulateAoi()
        {
            string path = CommonData.Preferences.AoiLocation;
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = null;
                    line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        CountrySelect.Items.Add(line);
                        line = reader.ReadLine();
                    }
                }
                CountrySelect.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(
                    "AOIs.csv dos not exist with the current path. Check the settings and update the file path");
            }
        }

        private void PopulateGroundObjects()
        {
            _locations = new List<GroundLocation>();
            List<GroundLocation> temp = new List<GroundLocation>();

            temp = CreatorFunctions.PopulateGroundObjectList("Place");
            foreach (var item in temp)
            {
                _locations.Add(item);
            }
            temp.Clear();
            temp = CreatorFunctions.PopulateGroundObjectList("Facility");
            foreach (var item in temp)
            {
                _locations.Add(item);
            }
            temp.Clear();
            temp = CreatorFunctions.PopulateGroundObjectList("Target");
            foreach (var item in temp)
            {
                _locations.Add(item);
            }
            temp.Clear();
        }

        private void PopulateAssets()
        {
            IAgExecCmdResult result;
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Constellation");
            if (result[0] != "None")
            {
                string[] constArray = result[0].Split(null);
                foreach (var item in constArray)
                {
                    string newItem = item.Split('/').Last();
                    var listItem = new ListViewItem();
                    if (newItem != "" && newItem != null)
                    {
                        int index = GroundEventFunctions.GetAssetImageIndex(item);
                        listItem.ImageIndex = index;
                        listItem.Text = newItem;
                        AssetList.Items.Add(listItem);
                    }
                }
            }

        }

        private void PopulateFom()
        {
            FOMType.Items.Add("Coverage Time");
            FOMType.Items.Add("Revisit Time");
            FOMType.Items.Add("Navigational Accuracy (PACC)");
            FOMType.Items.Add("Dilution of Precision");
            FOMType.Items.Add("N Asset Coverage");
            FOMType.Items.Add("Simple Coverage");
            FOMType.Items.Add("Age of Data");
            FOMType.Items.Add("Custom");

            FOMType.SelectedIndex = 0;
        }

        private int FieldCheck()
        {
            int check = 0;
            double temp;
            DateTime time;
            bool isNumerical = Double.TryParse(PointGran.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Point Granularity field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(ContourStart.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Contour Start field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(ContourStep.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Contour Step field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(ContourStop.Text, out temp);
            if (!isNumerical)
            {
                MessageBox.Show("Contour Stop field not a number");
                check = 1;
            }
            //Check Start time format
            if (DateTime.TryParseExact(StartTime.Text, new string[] { "dd MMM yyyy HH:mm:ss", "d MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss.000", "d MMM yyyy HH:mm:ss.000",
                "dd MMM yyyy HH:mm:ss.00", "d MMM yyyy HH:mm:ss.00", "dd MMM yyyy HH:mm:ss.0", "d MMM yyyy HH:mm:ss.0" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
            {

            }
            else
            {
                MessageBox.Show("Invalid Start Time format");
                check = 1;
            }
            //Check Stop Time format
            if (DateTime.TryParseExact(StopTime.Text, new string[] { "dd MMM yyyy HH:mm:ss", "d MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss.000", "d MMM yyyy HH:mm:ss.000",
                "dd MMM yyyy HH:mm:ss.00", "d MMM yyyy HH:mm:ss.00", "dd MMM yyyy HH:mm:ss.0", "d MMM yyyy HH:mm:ss.0" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
            {

            }
            else
            {
                MessageBox.Show("Invalid Stop Time format");
                check = 1;
            }
            bool exists = CommonData.StkRoot.ObjectExists("CoverageDefinition/" + CoverageName.Text.Replace(" ", "_"));
            if (exists && !CommonData.CovEdit)
            {
                MessageBox.Show("Coverage with this name already exists. Please choose a different name");
                check = 1;
            }
            if (CoverageTypeSelect.SelectedIndex == 0)
            {
                if (EllipseSelect.Checked)
                {
                    isNumerical = Double.TryParse(MinorAxis.Text, out temp);
                    if (!isNumerical)
                    {
                        MessageBox.Show("Semi-Minor Axis field not a number");
                        check = 1;
                    }
                    isNumerical = Double.TryParse(MajorAxis.Text, out temp);
                    if (!isNumerical)
                    {
                        MessageBox.Show("Semi-Major Axis field not a number");
                        check = 1;
                    }

                }
                else if (BoundingBox.Checked)
                {
                    isNumerical = Double.TryParse(BoundingLimit.Text, out temp);
                    if (!isNumerical)
                    {
                        MessageBox.Show("Bound Limit field not a number");
                        check = 1;
                    }
                }
                for (int i = 0; i < ObjectGrid.Rows.Count; i++)
                {
                    if (ObjectGrid.Rows[i].Cells[1].Value == null)
                    {
                        MessageBox.Show("Invalid Object Selection");
                        check = 1;
                    }
                }
            }

            if (UseConstraintObject.Checked)
            {
                if (ConstraintObject.SelectedIndex == -1 || ConstraintObject.SelectedItem.ToString() == null)
                {
                    MessageBox.Show("Invalid Constraint Object Selection");
                    check = 1;
                }
            }


            return check;

        }

        private void EllipseSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (EllipseSelect.Checked == true)
            {
                BoundingLimit.Enabled = false;
                MinorAxis.Enabled = true;
                MajorAxis.Enabled = true;
                label5.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
                label8.Enabled = true;
                label9.Enabled = false;
                label10.Enabled = false;
            }
        }

        private void AssetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AssetList.FocusedItem != null)
            {
                _assetIndex = AssetList.FocusedItem.Index;
            }

        }

        private void CountrySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentData = new CoverageData();
            //FindExistingCoverage();

        }

        private void FindExistingAssets(string cdName)
        {
            string oaName = cdName;

            if (CommonData.CoverageList.Count > 0 && oaName != null)
            {
                _coverageDataIndex = -1;
                bool exists = false;
                int count = 0;
                foreach (var item in CommonData.CoverageList)
                {
                    if (item.CdName == oaName)
                    {
                        exists = true;
                        _coverageDataIndex = count;
                        if (item.AssetList != null)
                        {
                            foreach (ListViewItem constellation in AssetList.Items)
                            {
                                bool used = false;
                                foreach (var asset in item.AssetList)
                                {
                                    if (asset == constellation.SubItems[0].Text)
                                    {
                                        constellation.Font = new Font(constellation.Font, FontStyle.Bold);
                                        used = true;
                                    }
                                }
                                if (!used)
                                {
                                    constellation.Font = new Font(constellation.Font, FontStyle.Regular);
                                }
                            }
                        }
                        else
                        {
                            foreach (ListViewItem constellation in AssetList.Items)
                            {
                                constellation.Font = new Font(constellation.Font, FontStyle.Regular);
                            }
                        }
                    }
                    count++;
                }
                if (!exists)
                {
                    foreach (ListViewItem constellation in AssetList.Items)
                    {
                        constellation.Font = new Font(constellation.Font, FontStyle.Regular);
                    }
                }
            }
            else
            {
                foreach (ListViewItem constellation in AssetList.Items)
                {
                    constellation.Font = new Font(constellation.Font, FontStyle.Regular);
                }
            }

        }

        private string GetFomString()
        {
            string fomString = null;

            if (FOMType.SelectedIndex == 0)
            {
                fomString = "CoverageTime Percent";
            }
            else if (FOMType.SelectedIndex == 1)
            {
                fomString = "RevisitTime Average MinNumAssets 1";
            }
            else if (FOMType.SelectedIndex == 2)
            {
                fomString = "NavAccuracy Compute Average NavType PACC_3";
            }
            else if (FOMType.SelectedIndex == 3)
            {
                fomString = "DOP Compute Average DOPType PDOP_3";
            }
            else if (FOMType.SelectedIndex == 4)
            {
                fomString = "NAsset Compute Average";
            }
            else if (FOMType.SelectedIndex == 5)
            {
                fomString = "Simple";
            }
            else if (FOMType.SelectedIndex == 6)
            {
                fomString = "AgeOfData Compute Average MinNumAssets 1";
            }

            return fomString;

        }

        private void DefineFom(string oaName)
        {
            //Remove all legends for previous coverage definitions that were computed 
            CoverageFunctions.RemoveFomLegends();

            //Configure settings for new FOM
            string foMstring = GetFomString();
            string foMlimits = ContourStart.Text + " " + ContourStop.Text + " " + ContourStep.Text;
            if (FOMType.SelectedIndex == 0 || FOMType.SelectedIndex == 5)
            {
                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM  FOMDefine Definition " + foMstring);
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Static Show On");
                IAgFigureOfMerit fom = CommonData.StkRoot.GetObjectFromPath("CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM") as IAgFigureOfMerit;
                fom.Graphics.Static.IsVisible = true;
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Animation Show Off");
                if (FOMType.SelectedIndex != 5)
                {
                    CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Static StartStop 0 100 10");
                    CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Static ColorRamp red blue");
                    CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Static Type Fill");
                    CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMAttributes StaticTranslucency 30");
                    CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMAttributes StaticLegendShow On");
                    CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM Legend Static Title " + "\"" + "Coverage Time (% of Coverage Interval)" + "\"");
                }
            }
            else
            {

                CommonData.StkRoot.ExecuteCommand("Cov */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM  FOMDefine Definition " + foMstring);
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Animation Show On");
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Animation StartStop " + foMlimits);
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Animation ColorRamp blue red");
                CommonData.StkRoot.ExecuteCommand("Graphics */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMContours Animation Type Fill");
                CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMAttributes AnimationTranslucency 30");
                CommonData.StkRoot.ExecuteCommand("VO */CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM FOMAttributes DynamicLegendShow On");
                IAgFigureOfMerit fom = CommonData.StkRoot.GetObjectFromPath("CoverageDefinition/" + oaName + "/FigureOfMerit/" + oaName + "_FOM") as IAgFigureOfMerit;
                fom.Graphics.Static.IsVisible = false;
            }

        }

        private void FOMType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFomValues();
        }

        private void UpdateFomValues()
        {
            string contourStart = null;
            string contourStop = null;
            string contourStep = null;
            if (FOMType.SelectedIndex == 0)
            {
                StartContourLabel.Visible = true;
                StopContourLabel.Visible = true;
                StepContourLabel.Visible = true;
                StartContourLabel.Text = "%";
                StopContourLabel.Text = "%";
                StepContourLabel.Text = "%";
                contourStart = "0";
                contourStop = "100";
                contourStep = "10";
            }
            else if (FOMType.SelectedIndex == 1)
            {
                StartContourLabel.Visible = true;
                StopContourLabel.Visible = true;
                StepContourLabel.Visible = true;
                StartContourLabel.Text = "sec";
                StopContourLabel.Text = "sec";
                StepContourLabel.Text = "sec";
                contourStart = "0";
                contourStop = "20000";
                contourStep = "2000";
            }
            else if (FOMType.SelectedIndex == 2)
            {
                StartContourLabel.Visible = true;
                StopContourLabel.Visible = true;
                StepContourLabel.Visible = true;
                StartContourLabel.Text = "m";
                StopContourLabel.Text = "m";
                StepContourLabel.Text = "m";
                contourStart = "0";
                contourStop = "20";
                contourStep = "2";
            }
            else if (FOMType.SelectedIndex == 3)
            {
                StartContourLabel.Visible = true;
                StopContourLabel.Visible = true;
                StepContourLabel.Visible = true;
                StartContourLabel.Text = "m";
                StopContourLabel.Text = "m";
                StepContourLabel.Text = "m";
                contourStart = "0";
                contourStop = "10";
                contourStep = "1";
            }
            else if (FOMType.SelectedIndex == 4)
            {
                StartContourLabel.Visible = true;
                StopContourLabel.Visible = true;
                StepContourLabel.Visible = false;
                StartContourLabel.Text = "assets";
                StopContourLabel.Text = "assets";
                contourStart = "0";
                contourStop = "10";
                contourStep = "1";
            }
            else if (FOMType.SelectedIndex == 5 || FOMType.SelectedIndex == 7)
            {
                StartContourLabel.Visible = false;
                StopContourLabel.Visible = false;
                StepContourLabel.Visible = false;
                contourStart = "0";
                contourStop = "0";
                contourStep = "0";
            }
            else if (FOMType.SelectedIndex == 6)
            {
                StartContourLabel.Visible = true;
                StopContourLabel.Visible = true;
                StepContourLabel.Visible = true;
                StartContourLabel.Text = "sec";
                StopContourLabel.Text = "sec";
                StepContourLabel.Text = "sec";
                contourStart = "0";
                contourStop = "20000";
                contourStep = "2000";
            }

            ContourStart.Text = contourStart;
            ContourStop.Text = contourStop;
            ContourStep.Text = contourStep;

        }

        private void CoverageTypeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CoverageTypeSelect.SelectedIndex == 0)
            {
                SSROptions.Enabled = true;
                CountrySelect.Enabled = false;
                PointGran.Text = "1";
            }
            else if (CoverageTypeSelect.SelectedIndex == 1)
            {
                SSROptions.Enabled = false;
                CountrySelect.Enabled = true;
                PointGran.Text = "1";
            }
            else if (CoverageTypeSelect.SelectedIndex == 2)
            {
                SSROptions.Enabled = false;
                CountrySelect.Enabled = false;
                PointGran.Text = "6";
            }
        }

        private void PopulateClasses()
        {
            ConstraintClass.Items.Clear();
            ConstraintClass.Items.Add("Facility");
            ConstraintClass.Items.Add("Place");
            ConstraintClass.Items.Add("Target");
            ConstraintClass.Items.Add("Sensor");
            ConstraintClass.Items.Add("Transmitter");
            ConstraintClass.Items.Add("Receiver");
            ConstraintClass.Items.Add("Aircraft");
            ConstraintClass.SelectedIndex = 0;
        }

        private void FOMType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateFomValues();
        }

        private void UseConstraintObject_CheckedChanged(object sender, EventArgs e)
        {
            if (UseConstraintObject.Checked)
            {
                ConstraintClass.Enabled = true;
                ConstraintObject.Enabled = true;
            }
            else
            {
                ConstraintClass.Enabled = false;
                ConstraintObject.Enabled = false;
            }
        }

        private void ConstraintClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConstraintClass.SelectedIndex!=-1)
            {
                _constraintObjects = new List<string>();
                _constraintObjects = CreatorFunctions.PopulateCbByClass(ConstraintObject, ConstraintClass.SelectedItem.ToString());
                if (ConstraintObject.Items.Count!=0)
                {
                    ConstraintObject.SelectedIndex = 0;
                }
            }
        }

        private void AddObject_Click(object sender, EventArgs e)
        {
            ObjectGrid.Rows.Add();

            DataGridViewTextBoxCell text = ObjectGrid[0, ObjectGrid.Rows.Count - 1] as DataGridViewTextBoxCell;
            text.Style.ForeColor = Color.Black;
            int index = ObjectGrid.Rows.Count;
            text.Value = index.ToString();


            DataGridViewComboBoxCell comboCell = ObjectGrid[1, ObjectGrid.Rows.Count-1] as DataGridViewComboBoxCell;
            comboCell.Style.ForeColor = Color.White;
            comboCell.Style.BackColor = Color.DimGray;

            PopulateDataGridCb(comboCell);

            if (comboCell.Items.Count>0)
            {
                comboCell.Value = comboCell.Items[0];
            }
            if (ObjectGrid.Rows.Count == 1)
            {
                DataGridViewCheckBoxCell cell = ObjectGrid.Rows[0].Cells[2] as DataGridViewCheckBoxCell;
                cell.Value = true;
            }
        }

        private void PopulateObjectRow(string target, bool zoom)
        {
            ObjectGrid.Rows.Add();

            DataGridViewTextBoxCell text = ObjectGrid[0, ObjectGrid.Rows.Count - 1] as DataGridViewTextBoxCell;
            text.Style.ForeColor = Color.Black;
            int index = ObjectGrid.Rows.Count;
            text.Value = index.ToString();

            DataGridViewComboBoxCell comboCell = ObjectGrid[1, ObjectGrid.Rows.Count - 1] as DataGridViewComboBoxCell;
            comboCell.Style.ForeColor = Color.White;
            comboCell.Style.BackColor = Color.DimGray;

            PopulateDataGridCb(comboCell);

            for (int i = 0; i < comboCell.Items.Count; i++)
            {
                if ((string)comboCell.Items[i] == target)
                {
                    comboCell.Value = comboCell.Items[i];
                }
            }

            if (zoom)
            {
                DataGridViewCheckBoxCell cell = ObjectGrid.Rows[ObjectGrid.Rows.Count - 1].Cells[2] as DataGridViewCheckBoxCell;
                cell.Value = true;
            }
        }

        private void RemoveObject_Click(object sender, EventArgs e)
        {
            if (ObjectGrid.CurrentCell.RowIndex != -1)
            {
                DataGridViewCheckBoxCell cell = ObjectGrid.Rows[ObjectGrid.CurrentCell.RowIndex].Cells[2] as DataGridViewCheckBoxCell;
                if (ObjectGrid.Rows.Count == 1)
                {
                    ObjectGrid.Rows.RemoveAt(ObjectGrid.CurrentCell.RowIndex);
                }
                else
                {
                    if (Convert.ToBoolean(cell.Value))
                    {
                        DataGridViewCheckBoxCell cell1 = ObjectGrid.Rows[0].Cells[2] as DataGridViewCheckBoxCell;
                        cell1.Value = true;
                    }
                    ObjectGrid.Rows.RemoveAt(ObjectGrid.CurrentCell.RowIndex);
                    RenumberDataGrid();
                }
            }
        }

        private void RenumberDataGrid()
        {
            DataGridViewTextBoxCell cell = null;
            for (int i = 0; i < ObjectGrid.Rows.Count; i++)
            {
                cell = ObjectGrid.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                cell.Value = (i + 1).ToString();
            }
        }

        private void InitializeDataGrid(CoverageData data)
        {
            foreach (var target in data.TargetList)
            {
                if (target == data.TargetName)
                {
                    PopulateObjectRow(target, true);
                }
                else
                {
                    PopulateObjectRow(target, false);
                }
            }
        }

        private void ObjectGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell cell = ObjectGrid.Rows[e.RowIndex].Cells[2] as DataGridViewCheckBoxCell;
                if ((bool)cell.EditingCellFormattedValue)
                {
                    ToggleZoomObject(e.RowIndex);
                }
            }
        }

        private void ToggleZoomObject(int index)
        {
            DataGridViewCheckBoxCell cell = null;
            for (int i = 0; i < ObjectGrid.Rows.Count; i++)
            {
                if (i==index)
                {
                    cell = ObjectGrid.Rows[index].Cells[2] as DataGridViewCheckBoxCell;
                    cell.Value = true;
                    _zoomObject = ObjectGrid.Rows[index].Cells[1].Value.ToString();
                }
                else
                {
                    cell = ObjectGrid.Rows[i].Cells[2] as DataGridViewCheckBoxCell;
                    cell.Value = false;
                }
            }

        }

        private void PopulateDataGridCb(DataGridViewComboBoxCell box)
        {
            StkObjectsLibrary library = new StkObjectsLibrary();
            List<string> objectList = new List<string>();
            IAgExecCmdResult result;
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Facility");
            if (result[0] != "None")
            {
                string[] facArray = result[0].Split(null);
                foreach (var item in facArray)
                {
                    if (item != null && item != "")
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            objectList.Add(facName);
                        }
                    }
                }
            }

            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Place");
            if (result[0] != "None")
            {
                string[] facArray = result[0].Split(null);
                foreach (var item in facArray)
                {
                    if (item != null && item != "")
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            objectList.Add(facName);
                        }
                    }
                }
            }
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Target");
            if (result[0] != "None")
            {
                string[] facArray = result[0].Split(null);
                foreach (var item in facArray)
                {
                    if (item != null && item != "")
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            objectList.Add(facName);
                        }
                    }
                }
            }
            if (objectList.Count>0)
            {
                box.DataSource = objectList;
            }
        }
    }
}

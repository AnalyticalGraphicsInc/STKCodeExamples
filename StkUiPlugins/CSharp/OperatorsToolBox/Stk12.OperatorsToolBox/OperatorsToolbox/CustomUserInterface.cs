using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.IO;
using OperatorsToolbox.BetaAngleCalculator;
using OperatorsToolbox.Coverage;
using OperatorsToolbox.EpochUpdater;
using OperatorsToolbox.FacilityCreator;
using OperatorsToolbox.GroundEvents;
using OperatorsToolbox.ImportTLEfromUDL;
using OperatorsToolbox.PassiveSafety;
using OperatorsToolbox.SatelliteCreator;
using OperatorsToolbox.SmartView;
using OperatorsToolbox.StationAccess;
using OperatorsToolbox.Templates;
using OperatorsToolbox.VolumeCreator;
using OperatorsToolbox.PlaneCrossingUtility;

namespace OperatorsToolbox
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite _mPEmbeddedControlSite;
        private Setup _mUiPlugin;
        private StkObjectsLibrary _mStkObjectsLibrary;
        private static List<UserControl> _controlsList;
        int _panelHeight;
        int _panelWidth;
        int _toolbarWidth;
        bool _panelHidden = false;
        bool _onStart=true;
        public int NumPanels
        {
            get { return _controlsList.Count; }
        }

        public CustomUserInterface()
        {
            _onStart = true;
            _panelHeight = Properties.Settings.Default.PanelHeight;
            CommonData.PanelHeight = Properties.Settings.Default.PanelHeight;
            _panelWidth = _panelHeight / 2;
            _controlsList = new List<UserControl>();
            InitializeComponent();
            CommonData.PluginOptions = new List<string>();

            //Read Settings and reseting install if required
            Properties.Settings.Default.Upgrade();
            if (Directory.Exists(Properties.Settings.Default.InstallDir))
            {
                CommonData.InstallDir = Properties.Settings.Default.InstallDir;
                string prefpath = Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref");
                try
                {
                    ReadWrite.ReadPrefs(prefpath);
                }
                catch (Exception)
                {
                    DialogResult result = MessageBox.Show("Error: Could not read preferences. Check file location. Would like to reset the install directory?", "Choose Install Directory", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        string installDir = BrowseFileExplorer("C:\\", "Choose Install Directory");
                        if (!String.IsNullOrEmpty(installDir))
                        {
                            Properties.Settings.Default.InstallDir = installDir;
                            Properties.Settings.Default.Save();
                            CommonData.InstallDir = installDir;
                            prefpath = Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref");
                            try
                            {
                                ReadWrite.ReadPrefs(prefpath);
                                CommonData.Preferences.SatCatLocation = Path.Combine(@CommonData.InstallDir, "Databases\\SatelliteCatalog.xlsx");
                                CommonData.Preferences.AoiLocation = Path.Combine(@CommonData.InstallDir, "Databases\\AOIs.csv");
                                CommonData.Preferences.TemplatesDirectory = Path.Combine(@CommonData.InstallDir, "Databases\\Templates");
                                ReadWrite.WritePrefs(prefpath);
                                MessageBox.Show("Install location updated successfully. New Install Location: \n" +
                                    Properties.Settings.Default.InstallDir +
                                    "\n\nAll database directories updated based on install location.\n" +
                                    "Please check event image paths in settings. These paths do not update based on install change");
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error: Could not read preferences. Check file location.");
                            }
                        }
                    }
                }
            }
            else
            {
                string installDir = BrowseFileExplorer("C:\\", "Choose Install Directory");
                if (installDir != null)
                {
                    Properties.Settings.Default.InstallDir = installDir;
                    Properties.Settings.Default.Save();
                    CommonData.InstallDir = installDir;
                    string prefpath = Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref");
                    try
                    {
                        ReadWrite.ReadPrefs(prefpath);
                        CommonData.Preferences.SatCatLocation = Path.Combine(@CommonData.InstallDir, "Databases\\SatelliteCatalog.xlsx");
                        CommonData.Preferences.AoiLocation = Path.Combine(@CommonData.InstallDir, "Databases\\AOIs.csv");
                        CommonData.Preferences.TemplatesDirectory = Path.Combine(@CommonData.InstallDir, "Databases\\Templates");
                        ReadWrite.WritePrefs(prefpath);
                        MessageBox.Show("Install location updated successfully. New Install Location: \n" +
                            Properties.Settings.Default.InstallDir +
                            "\n\nAll database directories updated based on install location.\n" +
                            "Please check event image paths in settings. These paths do not update based on install change");
                    }
                    catch (Exception)
                    {
                        DialogResult result = MessageBox.Show("Error: Could not read preferences. Check file location. Would like to reset the install directory?", "Choose Install Directory", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            installDir = BrowseFileExplorer("C:\\", "Choose Install Directory");
                            if (!String.IsNullOrEmpty(installDir))
                            {
                                Properties.Settings.Default.InstallDir = installDir;
                                Properties.Settings.Default.Save();
                                CommonData.InstallDir = installDir;
                                prefpath = Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref");
                                try
                                {
                                    ReadWrite.ReadPrefs(prefpath);
                                    CommonData.Preferences.SatCatLocation = Path.Combine(@CommonData.InstallDir, "Databases\\SatelliteCatalog.xlsx");
                                    CommonData.Preferences.AoiLocation = Path.Combine(@CommonData.InstallDir, "Databases\\AOIs.csv");
                                    CommonData.Preferences.TemplatesDirectory = Path.Combine(@CommonData.InstallDir, "Databases\\Templates");
                                    ReadWrite.WritePrefs(prefpath);
                                    MessageBox.Show("Install location updated successfully. New Install Location: \n" +
                                        Properties.Settings.Default.InstallDir +
                                        "\n\nAll database directories updated based on install location.\n" +
                                        "Please check event image paths in settings. These paths do not update based on install change");
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Error: Could not read preferences. Check file location.");
                                }
                            }
                        }
                    }
                }
            }

            //Initializing plugin options as enumeration
            for (int i = 0; i < CommonData._numPlugins; i++)
            {
                CommonData.PluginOptions.Add(((PluginType)i).ToString());
            }
            _toolbarWidth = this.tableLayoutPanel1.Width;
            PluginConfigList = CommonData.Preferences.PluginConfigList;

            //Set buttons based on user settings
            SetButtonImagesAndVisability();

        }

        #region Plugin Enumeration Functions
        public List<int> PluginConfigList = new List<int>();
        public enum PluginType
        {
            Templates = 0,
            SatelliteCreator = 1,
            UdlTleImport = 2,
            EpochUpdate = 3,
            FacilityCreator = 4,
            GroundEvents = 5,
            SmartView = 6,
            StationAccess = 7,
            Coverage = 8,
            PassiveSafety = 9,
            VolumeCreator = 10,
            SolarAnglesUtility = 11,
            PlaneCrossingTimes = 12
        }

        public UserControl OpenTool(PluginType pluginType, Control control)
        {
            switch (pluginType)
            {
                case PluginType.Templates:
                    TemplatesPlugin templatesPlugin = new TemplatesPlugin();
                    templatesPlugin.PanelClose += PanelRemoved;
                    return templatesPlugin;
                case PluginType.SatelliteCreator:
                    NewAssetForm newAssetForm = new NewAssetForm();
                    newAssetForm.PanelClose += PanelRemoved;
                    return newAssetForm;
                case PluginType.UdlTleImport:
                    InsertTleFromUdl insertTleFromUdl = new InsertTleFromUdl();
                    insertTleFromUdl.PanelClose += PanelRemoved;
                    return insertTleFromUdl;
                case PluginType.EpochUpdate:
                    SatelliteEpochUpdatePlugin satelliteEpochUpdatePlugin = new SatelliteEpochUpdatePlugin();
                    satelliteEpochUpdatePlugin.PanelClose += PanelRemoved;
                    return satelliteEpochUpdatePlugin;
                case PluginType.FacilityCreator:
                    FacilityCreatorPlugin facilityCreatorPlugin = new FacilityCreatorPlugin();
                    facilityCreatorPlugin.PanelClose += PanelRemoved;
                    return facilityCreatorPlugin;
                case PluginType.GroundEvents:
                    GroundEventsPlugin groundEventsPlugin = new GroundEventsPlugin();
                    groundEventsPlugin.PanelClose += PanelRemoved;
                    return groundEventsPlugin;
                case PluginType.SmartView:
                    PopulateContextViews();
                    SmartViewDropdown.Show(control, new Point(0, control.Height));
                    return null;
                case PluginType.StationAccess:
                    StationAccessPlugin stationAccessPlugin = new StationAccessPlugin();
                    stationAccessPlugin.PanelClose += PanelRemoved;
                    return stationAccessPlugin;
                case PluginType.Coverage:
                    CoveragePlugin coveragePlugin = new CoveragePlugin();
                    coveragePlugin.PanelClose += PanelRemoved;
                    return coveragePlugin;
                case PluginType.PassiveSafety:
                    PassiveSafetyPlugin passiveSafetyPlugin = new PassiveSafetyPlugin();
                    passiveSafetyPlugin.PanelClose += PanelRemoved;
                    return passiveSafetyPlugin;
                case PluginType.VolumeCreator:
                    VolumePlugin volumePlugin = new VolumePlugin();
                    volumePlugin.PanelClose += PanelRemoved;
                    return volumePlugin;
                case PluginType.SolarAnglesUtility:
                    SolarPhasePlugin solarPhasePlugin = new SolarPhasePlugin();
                    solarPhasePlugin.PanelClose += PanelRemoved;
                    return solarPhasePlugin;
                case PluginType.PlaneCrossingTimes:
                    PlaneCrossingPlugin planeCrossingPlugin = new PlaneCrossingPlugin();
                    planeCrossingPlugin.PanelClose += PanelRemoved;
                    return planeCrossingPlugin;
                default:
                    return null;
            }
        }

        public static int StringToPluginType(string pluginName)
        {
            switch (pluginName)
            {
                case "Templates":
                    return 0;
                case "SatelliteCreator":
                    return 1;
                case "UdlTleImport":
                    return 2;
                case "EpochUpdate":
                    return 3;
                case "FacilityCreator":
                    return 4;
                case "GroundEvents":
                    return 5;
                case "SmartView":
                    return 6;
                case "StationAccess":
                    return 7;
                case "Coverage":
                    return 8;
                case "PassiveSafety":
                    return 9;
                case "VolumeCreator":
                    return 10;
                case "SolarAnglesUtility":
                    return 11;
                case "PlaneCrossingTimes":
                    return 12;
                default:
                    return -1;
            }
        }

        public static string GetPluginDescription(PluginType plugin)
        {
            string description = null;

            switch (plugin)
            {
                case PluginType.Templates:
                    break;
                case PluginType.SatelliteCreator:
                    break;
                case PluginType.UdlTleImport:
                    break;
                case PluginType.EpochUpdate:
                    break;
                case PluginType.FacilityCreator:
                    break;
                case PluginType.GroundEvents:
                    break;
                case PluginType.SmartView:
                    break;
                case PluginType.StationAccess:
                    break;
                case PluginType.Coverage:
                    break;
                case PluginType.PassiveSafety:
                    break;
                case PluginType.VolumeCreator:
                    break;
                case PluginType.SolarAnglesUtility:
                    break;
                case PluginType.PlaneCrossingTimes:
                    break;
                default:
                    break;
            }


            return description;
        }

        public static string GetPluginToolTip(PluginType plugin)
        {
            switch (plugin)
            {
                case PluginType.Templates:
                    return "Templates";
                case PluginType.SatelliteCreator:
                    return "Satellite Creator";
                case PluginType.UdlTleImport:
                    return "UDL TLE Import";
                case PluginType.EpochUpdate:
                    return "Satellite Epoch Updater";
                case PluginType.FacilityCreator:
                    return "Facility Creator";
                case PluginType.GroundEvents:
                    return "Ground Events";
                case PluginType.SmartView:
                    return "Smart View";
                case PluginType.StationAccess:
                    return "Station Access";
                case PluginType.Coverage:
                    return "Coverage";
                case PluginType.PassiveSafety:
                    return "Passive Safety";
                case PluginType.VolumeCreator:
                    return "Volume Creator";
                case PluginType.SolarAnglesUtility:
                    return "Beta Angle Calculator";
                case PluginType.PlaneCrossingTimes:
                    return "Plane Crossing Times";
                default:
                    return null;
            }
        }

        public enum ColorOptions
        {
            Blue = 0,
            Cyan = 1,
            Red = 2,
            Green = 3,
            Yellow = 4,
            Orange = 5,
            Purple = 6,
            White = 7,
            Custom = 8
        }
        #endregion

        #region IAgUiPluginEmbeddedControl Implementation
        public stdole.IPictureDisp GetIcon()
        {
            return null;
        }

        public void OnClosing()
        {
            CommonData.StkRoot.OnStkObjectAdded -= m_root_OnStkObjectAdded;
            CommonData.StkRoot.OnStkObjectDeleted -= m_root_OnStkObjectDeleted;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite site)
        {
            _mPEmbeddedControlSite = site;
            _mUiPlugin = _mPEmbeddedControlSite.Plugin as Setup;
            _mStkObjectsLibrary = new StkObjectsLibrary();

            //EXAMPLE: Hooking to STK Exents
            CommonData.StkRoot.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            CommonData.StkRoot.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);

            CommonData.DirectoryStr = _mStkObjectsLibrary.GetScenarioDirectory();

            CommonData.SavedViewList = new List<ViewData>();
            CommonData.InitialObjectData = SmartViewFunctions.GetObjectData();

            PopulateContextViews();
            _mPEmbeddedControlSite.Window.Width = _toolbarWidth + 5;
        }


        #endregion

        #region Main Plugin interface Functions
        public static string BrowseFileExplorer(string initialDirectory, string title)
        {
            
            string dirStr = null;
            // Launch file explorer:
            FolderBrowserDialog fileExplorer = new FolderBrowserDialog();
            fileExplorer.Description = "Choose Install Directory";

            if (fileExplorer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dirStr = fileExplorer.SelectedPath;
            }
            return dirStr;
        }


        private void SetButtonSizePosition(Button button, int width, int height, int x, int y)
        {
            button.Width = width;
            button.Height = height;
            button.Location = new Point(x, y);
        }

        private void SetButtonImagesAndVisability()
        {
            SetButton(ToolbarButton0, PluginConfigList[0]);
            SetButton(ToolbarButton1, PluginConfigList[1]);
            SetButton(ToolbarButton2, PluginConfigList[2]);
            SetButton(ToolbarButton3, PluginConfigList[3]);
            SetButton(ToolbarButton4, PluginConfigList[4]);
            SetButton(ToolbarButton5, PluginConfigList[5]);
            SetButton(ToolbarButton6, PluginConfigList[6]);
            SetButton(ToolbarButton7, PluginConfigList[7]);
            SetButton(ToolbarButton8, PluginConfigList[8]);
            SetButton(ToolbarButton9, PluginConfigList[9]);
        }
        private void SetButton(Button control, int index)
        {
            if (index == -1)
            {
                control.Visible = false;
                control.Enabled = false;
            }
            else
            {
                control.Visible = true;
                control.Enabled = true;
                control.ImageIndex = index;
            }
        }

        private void AutoSize()
        {
            PluginPanel.Controls.Clear();

            _mPEmbeddedControlSite.Window.Width = 20 + _toolbarWidth + _panelWidth * NumPanels;

            for (var index = 0; index < _controlsList.Count; index++)
            {
                var userControl = _controlsList[index];

                userControl.Location = new Point(_panelWidth * index + 1, 1);
                userControl.Width = _panelWidth - 2;
                userControl.Height = this.Height;
                userControl.BorderStyle = BorderStyle.Fixed3D;
                userControl.MinimumSize = new Size(_panelWidth, _panelHeight);
                userControl.MaximumSize = new Size(_panelWidth, _panelHeight + 500);
                PluginPanel.Controls.Add(userControl);
            }
        }

        private void PanelRemoved(UserControl sender)
        {
            _controlsList.Remove(sender);
            AutoSize();
        }


        private void CustomUserInterface_Resize(object sender, EventArgs e)
        {
            foreach (var userControl in _controlsList)
            {
                userControl.Height = PluginPanel.Height - 2;
            }
        }
        #endregion

        #region Toolbar Button Click Events
        private void ToolbarButton0_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[0];
            UserControl control = OpenTool(type, ToolbarButton0);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton1_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[1];
            UserControl control = OpenTool(type, ToolbarButton1);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton2_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[2];
            UserControl control = OpenTool(type, ToolbarButton2);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton3_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[3];
            UserControl control = OpenTool(type, ToolbarButton3);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton4_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[4];
            UserControl control = OpenTool(type, ToolbarButton4);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton5_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[5];
            UserControl control = OpenTool(type, ToolbarButton5);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton6_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[6];
            UserControl control = OpenTool(type, ToolbarButton6);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton7_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[7];
            UserControl control = OpenTool(type, ToolbarButton7);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton8_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[8];
            UserControl control = OpenTool(type, ToolbarButton8);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void ToolbarButton9_Click(object sender, EventArgs e)
        {
            PluginType type = (PluginType)PluginConfigList[9];
            UserControl control = OpenTool(type, ToolbarButton9);
            if (control != null)
            {
                _controlsList.Add(control);
                AutoSize();
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Yes)
            {
                Properties.Settings.Default.PanelHeight = CommonData.PanelHeight;
                Properties.Settings.Default.Save();
                PluginConfigList = CommonData.Preferences.PluginConfigList;
                SetButtonImagesAndVisability();
            }
        }

        #endregion

        #region Plugin Specific Functions
        private void SmartViewDropDown_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Open Smart View")
            {
                SmartViewPlugin smartViewPlugin = new SmartViewPlugin();
                smartViewPlugin.PanelClose += PanelRemoved;
                _controlsList.Add(smartViewPlugin);
                AutoSize();
            }
            else
            {
                ChangeView(e.ClickedItem.Text);
            }
        }

        private void SatelliteImportDropdown_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Satellite Creator")
            {
                NewAssetForm newAssetForm = new NewAssetForm();
                newAssetForm.PanelClose += PanelRemoved;
                _controlsList.Add(newAssetForm);
                AutoSize();
            }
            else if (e.ClickedItem.Text == "UDL TLE Import")
            {
                InsertTleFromUdl insertTleFromUdl = new InsertTleFromUdl();
                insertTleFromUdl.PanelClose += PanelRemoved;
                _controlsList.Add(insertTleFromUdl);
                AutoSize();
            }
            else if (e.ClickedItem.Text == "Satellite Epoch Updater")
            {
                SatelliteEpochUpdatePlugin satelliteEpochUpdatePlugin = new SatelliteEpochUpdatePlugin();
                satelliteEpochUpdatePlugin.PanelClose += PanelRemoved;
                _controlsList.Add(satelliteEpochUpdatePlugin);
                AutoSize();
            }
        }

        private void AWBToolsDropdown_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Beta Angle Calculator")
            {
                SolarPhasePlugin solarPhasePlugin = new SolarPhasePlugin();
                solarPhasePlugin.PanelClose += PanelRemoved;
                _controlsList.Add(solarPhasePlugin);
                AutoSize();
            }
            else if (e.ClickedItem.Text == "Plane Crossing Times")
            {
                PlaneCrossingPlugin planeCrossingPlugin = new PlaneCrossingPlugin();
                planeCrossingPlugin.PanelClose += PanelRemoved;
                _controlsList.Add(planeCrossingPlugin);
                AutoSize();
            }
        }

        private void PopulateAWBTools()
        {
            AWBToolsDropdown.Items.Clear();
            AWBToolsDropdown.Items.Add("Beta Angle Calculator");
            AWBToolsDropdown.Items.Add("-");
            AWBToolsDropdown.Items.Add("Plane Crossing Times");
        }
        private void PopulateContextViews()
        {
            SmartViewDropdown.Items.Clear();
            CommonData.SavedViewList.Clear();
            SmartViewDropdown.Items.Add("Open Smart View");
            SmartViewDropdown.Items.Add("-");
            if (File.Exists(CommonData.DirectoryStr + "\\StoredViewData.json"))
            {
                if (CommonData.SavedViewList.Count == 0)
                    CommonData.SavedViewList =
                        ReadWrite.ReadSavedViews(CommonData.DirectoryStr + "\\StoredViewData.json");

                foreach (var item in CommonData.SavedViewList) SmartViewDropdown.Items.Add(item.Name);
            }
        }

        private void PopulateSatOptions()
        {
            SatelliteImportDropdown.Items.Clear();
            SatelliteImportDropdown.Items.Add("Satellite Creator");
            SatelliteImportDropdown.Items.Add("-");
            SatelliteImportDropdown.Items.Add("UDL TLE Import");
            SatelliteImportDropdown.Items.Add("-");
            SatelliteImportDropdown.Items.Add("Satellite Epoch Updater");
        }

        private void ChangeView(string viewName)
        {
            foreach (var item in CommonData.SavedViewList)
            {
                if (item.Name != viewName) continue;

                CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
                switch (item.ViewType)
                {
                    case "2D":
                        SmartViewFunctions.Change2DView(item);
                        break;
                    case "3D":
                        SmartViewFunctions.Change3DView(item);
                        break;
                    case "Target/Threat":
                        SmartViewFunctions.ChangeTargetThreatView(item);
                        break;
                    case "GEODrift":
                        SmartViewFunctions.ChangeGeoDriftView(item);
                        break;
                }
                CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
                break;
            }

        }

        #endregion

        #region Tool tips 
        private void ToolbarButton0_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[0]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton0, tip);
            }
        }
        private void ToolbarButton1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[1]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton1, tip);
            }
        }

        private void ToolbarButton2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[2]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton2, tip);
            }
        }

        private void ToolbarButton3_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[3]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton3, tip);
            }
        }

        private void ToolbarButton4_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[4]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton4, tip);
            }
        }

        private void ToolbarButton5_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[5]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton5, tip);
            }
        }

        private void ToolbarButton6_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[6]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton6, tip);
            }
        }

        private void ToolbarButton7_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[7]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton7, tip);
            }
        }

        private void ToolbarButton8_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[8]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton8, tip);
            }
        }

        private void ToolbarButton9_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            string tip = GetPluginToolTip((PluginType)PluginConfigList[9]);
            if (tip != null)
            {
                toolTip1.SetToolTip(this.ToolbarButton9, tip);
            }
        }

        private void Settings_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Settings, "Settings");
        }
        #endregion

        #region Sample code
        void m_root_OnStkObjectDeleted(object sender)
        {
            string objectPath = sender.ToString();
            string simpleName = _mStkObjectsLibrary.SimplifiedObjectPath(objectPath);
            //cbStkObjects.Items.Remove(simpleName);
        }

        void m_root_OnStkObjectAdded(object sender)
        {
            string objectPath = sender.ToString();
            string simpleName = _mStkObjectsLibrary.SimplifiedObjectPath(objectPath);
            //cbStkObjects.Items.Add(simpleName);
        }
        #endregion
    }
}

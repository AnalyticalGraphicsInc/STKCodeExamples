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
using OperatorsToolbox.SensorBoresightPlugin;
using System.Diagnostics;

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
        public List<string> validLicenses;
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
            Properties.Settings.Default.Reload();
            if (Directory.Exists(Properties.Settings.Default.InstallDir))
            {
                CommonData.InstallDir = Properties.Settings.Default.InstallDir;
                string prefpath = Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref");
                try
                {
                    if (File.Exists(prefpath))
                    {
                        ReadWrite.ReadPrefs(prefpath);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Error: Could not find preferences file. Would like to reset the install directory?", "Choose Install Directory", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            FirstTimeUseWizard ftu = new FirstTimeUseWizard();
                            ftu.ShowDialog();
                        }
                    }
                }
                catch (Exception)
                {
                    DialogResult result = MessageBox.Show("Error: Could not read preferences. Check file location. Would like to reset the install directory?", "Choose Install Directory", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        FirstTimeUseWizard ftu = new FirstTimeUseWizard();
                        ftu.ShowDialog();
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                FirstTimeUseWizard ftu = new FirstTimeUseWizard();
                ftu.ShowDialog();
            }
            validLicenses = ReadWrite.GetLicensingData();
            //Initializing plugin options as enumeration
            for (int i = 0; i < CommonData._numPlugins; i++)
            {
                CommonData.PluginOptions.Add(((PluginType)i).ToString());
            }
            _toolbarWidth = this.tableLayoutPanel1.Width;
            PluginConfigList = CommonData.Preferences.PluginConfigList;

            //Set buttons based on user settings
            SetButtonImagesAndVisability();

            //System.Diagnostics.Process.Start(@"C:\GitHub\EngineeringLab\OperatorsToolBox\Stk12.OperatorsToolBox\Plugin Files\Test.py").WaitForExit();
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
            PlaneCrossingUtility = 12,
            SensorBoresightPlugin = 13
        }

        public UserControl OpenTool(PluginType pluginType, Control control)
        {
            bool validLicenses = CheckLicenses(pluginType);
            if (validLicenses)
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
                    case PluginType.PlaneCrossingUtility:
                        PlaneCrossingPlugin planeCrossingPlugin = new PlaneCrossingPlugin();
                        planeCrossingPlugin.PanelClose += PanelRemoved;
                        return planeCrossingPlugin;
                    case PluginType.SensorBoresightPlugin:
                        SensorBoresightUtility boresightPlugin = new SensorBoresightUtility();
                        boresightPlugin.PanelClose += PanelRemoved;
                        return boresightPlugin;
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<string> GetRequiredLicenses(PluginType pluginType)
        {
            List<string> requiredLicenses = new List<string>();

            switch (pluginType)
            {
                case PluginType.Templates:
                    break;
                case PluginType.SatelliteCreator:
                    requiredLicenses.Add("STKProfessional");
                    break;
                case PluginType.UdlTleImport:
                    requiredLicenses.Add("STKProfessional");
                    break;
                case PluginType.EpochUpdate:
                    requiredLicenses.Add("ASTG");
                    break;
                case PluginType.FacilityCreator:
                    requiredLicenses.Add("STKProfessional");
                    break;
                case PluginType.GroundEvents:
                    requiredLicenses.Add("STKProfessional");
                    requiredLicenses.Add("AnalysisWB");
                    break;
                case PluginType.SmartView:
                    break;
                case PluginType.StationAccess:
                    requiredLicenses.Add("STKProfessional");
                    break;
                case PluginType.Coverage:
                    requiredLicenses.Add("STKProfessional");
                    requiredLicenses.Add("COV");
                    break;
                case PluginType.PassiveSafety:
                    requiredLicenses.Add("ASTG");
                    break;
                case PluginType.VolumeCreator:
                    requiredLicenses.Add("STKProfessional");
                    break;
                case PluginType.SolarAnglesUtility:
                    requiredLicenses.Add("AnalysisWB");
                    break;
                case PluginType.PlaneCrossingUtility:
                    requiredLicenses.Add("AnalysisWB");
                    break;
                case PluginType.SensorBoresightPlugin:
                    break;
                default:
                    break;
            }

            return requiredLicenses;
        }

        public bool CheckLicenses(PluginType pluginType)
        {
            bool check = true;
            string msg = "Tool Unavailable. The Following Licenses are Missing: \n";
            List<string> requiredLicenses = GetRequiredLicenses(pluginType);
            foreach (var item in requiredLicenses)
            {
                if (!validLicenses.Contains(item))
                {
                    check = false;
                    msg = msg + item + "\n";
                }
            }
            if (check == false)
            {
                MessageBox.Show(msg);
            }
            return check;
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
                case "PlaneCrossingUtility":
                    return 12;
                case "SensorBoresightPlugin":
                    return 13;
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
                case PluginType.PlaneCrossingUtility:
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
                    return "Solar Angle Utility";
                case PluginType.PlaneCrossingUtility:
                    return "Plane Crossing Utility";
                case PluginType.SensorBoresightPlugin:
                    return "Sensor Boresight View";
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
            CommonData.StkRoot.OnScenarioSave -= m_root_OnStkSave;
            CommonData.StkRoot.OnScenarioBeforeClose -= m_root_OnStkScenarioClose;
            CommonData.StkRoot.OnAnimationPlayback -= m_root_OnStkAnimationPlayback;
            CommonData.StkRoot.OnAnimationPause -= m_root_OnStkAnimationPause;
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
            CommonData.StkRoot.OnScenarioSave += new IAgStkObjectRootEvents_OnScenarioSaveEventHandler(m_root_OnStkSave);
            CommonData.StkRoot.OnScenarioBeforeClose += new IAgStkObjectRootEvents_OnScenarioBeforeCloseEventHandler(m_root_OnStkScenarioClose);
            CommonData.StkRoot.OnAnimationPlayback += new IAgStkObjectRootEvents_OnAnimationPlaybackEventHandler(m_root_OnStkAnimationPlayback);
            CommonData.StkRoot.OnAnimationPause += new IAgStkObjectRootEvents_OnAnimationPauseEventHandler(m_root_OnStkAnimationPause);

            CommonData.DirectoryStr = _mStkObjectsLibrary.GetScenarioDirectory();

            CommonData.SavedViewList = new List<ViewData>();
            CommonData.InitialObjectData = SmartViewFunctions.GetObjectData();

            PopulateContextViews();
            AutoSize();
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
            if (_mPEmbeddedControlSite.Window.DockStyle == AGI.Ui.Core.AgEDockStyle.eDockStyleDockedBottom 
                || _mPEmbeddedControlSite.Window.DockStyle == AGI.Ui.Core.AgEDockStyle.eDockStyleDockedTop 
                || _mPEmbeddedControlSite.Window.DockStyle == AGI.Ui.Core.AgEDockStyle.eDockStyleIntegrated)
            {

            }
            else
            {
                if (_controlsList.Count > 0)
                {
                    _mPEmbeddedControlSite.Window.Width = 20 + _toolbarWidth + _panelWidth * NumPanels;
                }
                else
                {
                    _mPEmbeddedControlSite.Window.Width = 15 + _toolbarWidth;
                }
            }

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
                        //SmartViewFunctions.ChangeTargetThreatView(item);
                        break;
                    case "GEODrift":
                        //SmartViewFunctions.ChangeGeoDriftView(item);
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
            string objPath = sender.ToString();
            if (CommonData.Preferences.ActiveObjDeletedScript)
            {
                ReadWrite.ExecuteScript(CommonData.Preferences.ObjDeletedScriptPath, objPath, true);
            }
            //CommonData.InitialObjectData = SmartViewFunctions.GetObjectData();
        }

        void m_root_OnStkObjectAdded(object sender)
        {
            string objPath = sender.ToString();
            if (CommonData.Preferences.ActiveObjAddedScript)
            {
                ReadWrite.ExecuteScript(CommonData.Preferences.ObjAddedScriptPath, objPath, true);
            }
            //CommonData.InitialObjectData = SmartViewFunctions.GetObjectData();
        }

        void m_root_OnStkSave(object sender)
        {
            if (CommonData.Preferences.ActiveSaveScript)
            {
                ReadWrite.ExecuteScript(CommonData.Preferences.SaveScriptPath, CommonData.Preferences.SaveScriptArgs, false);
            }
            //CommonData.InitialObjectData = SmartViewFunctions.GetObjectData();
        }

        void m_root_OnStkScenarioClose()
        {
            if (CommonData.Preferences.ActiveCloseScript)
            {
                ReadWrite.ExecuteScript(CommonData.Preferences.CloseScriptPath, CommonData.Preferences.CloseScriptArgs, false);
            }
        }

        void m_root_OnStkAnimationPlayback(double time, AgEAnimationActions actions, AgEAnimationDirections directions)
        {
            if (CommonData.Preferences.ActivePlaybackScript)
            {
                ReadWrite.ExecuteScript(CommonData.Preferences.PlaybackScriptPath, CommonData.Preferences.PlaybackScriptArgs, true);
            }
        }

        void m_root_OnStkAnimationPause(double time)
        {
            if (CommonData.Preferences.ActivePauseScript)
            {
                ReadWrite.ExecuteScript(CommonData.Preferences.PauseScriptPath, CommonData.Preferences.PauseScriptArgs, false);
            }
        }
        #endregion
    }
}

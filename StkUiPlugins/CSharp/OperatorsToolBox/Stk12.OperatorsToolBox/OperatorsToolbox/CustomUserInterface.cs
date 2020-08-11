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
using System.Threading;
using System.Collections.Specialized;
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
                    MessageBox.Show("Error: Could not read preferences. Check file location");
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
                    MessageBox.Show(Properties.Settings.Default.InstallDir);
                    string prefpath = Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref");
                    try
                    {
                        ReadWrite.ReadPrefs(prefpath);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Could not read preferences. Check file location");
                    }
                }
            }

            _toolbarWidth = this.tableLayoutPanel1.Width+4;

        }

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

        private void SetButton(Button button, int width, int height, int x, int y)
        {
            button.Width = width;
            button.Height = height;
            button.Location = new Point(x, y);
        }

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
        //EXAMPLE: Progress bar
        private void TestProgressBar()
        {
            _mUiPlugin.ProgressBar.BeginTracking(AgEProgressTrackingOptions.eProgressTrackingOptionNoCancel, AgEProgressTrackingType.eTrackAsProgressBar);
            for (int i = 0; i <= 100; i++)
            {
                _mUiPlugin.ProgressBar.SetProgress(i, string.Format("Progress is at {0}...", i));
                Thread.Sleep(100);
                if (!_mUiPlugin.ProgressBar.Continue)
                    break;
            }
            _mUiPlugin.ProgressBar.EndTracking();
        }


        private void SatelliteCreator_Click(object sender, EventArgs e)
        {
            PopulateSatOptions();
            SatelliteImportDropdown.Show(SatelliteCreator, new Point(0, SatelliteCreator.Height));

        }

        private void FacilityCreator_Click(object sender, EventArgs e)
        {
            FacilityCreatorPlugin facilityCreatorPlugin = new FacilityCreatorPlugin();
            facilityCreatorPlugin.PanelClose += PanelRemoved;
            _controlsList.Add(facilityCreatorPlugin);
            AutoSize();
        }

        private void SmartView_Click(object sender, EventArgs e)
        {
            PopulateContextViews();
            SmartViewDropdown.Show(SmartView, new Point(0, SmartView.Height));
        }

        private void GroundStationAccess_Click(object sender, EventArgs e)
        {
            StationAccessPlugin stationAccessPlugin = new StationAccessPlugin();
            stationAccessPlugin.PanelClose += PanelRemoved;
            _controlsList.Add(stationAccessPlugin);
            AutoSize();
        }

        private void PassiveSafety_Click(object sender, EventArgs e)
        {
            PassiveSafetyPlugin passiveSafetyPlugin = new PassiveSafetyPlugin();
            passiveSafetyPlugin.PanelClose += PanelRemoved;
            _controlsList.Add(passiveSafetyPlugin);
            AutoSize();
        }

        private void ThreatVolume_Click(object sender, EventArgs e)
        {
            VolumePlugin volumePlugin = new VolumePlugin();
            volumePlugin.PanelClose += PanelRemoved;
            _controlsList.Add(volumePlugin);
            AutoSize();
        }

        private void SolarPhase_Click(object sender, EventArgs e)
        {
            SolarPhasePlugin solarPhasePlugin = new SolarPhasePlugin();
            solarPhasePlugin.PanelClose += PanelRemoved;
            _controlsList.Add(solarPhasePlugin);
            AutoSize();
        }
        private void GroundEvents_Click(object sender, EventArgs e)
        {
            GroundEventsPlugin groundEventsPlugin = new GroundEventsPlugin();
            groundEventsPlugin.PanelClose += PanelRemoved;
            _controlsList.Add(groundEventsPlugin);
            AutoSize();
        }

        private void Coverage_Click_1(object sender, EventArgs e)
        {
            CoveragePlugin coveragePlugin = new CoveragePlugin();
            coveragePlugin.PanelClose += PanelRemoved;
            _controlsList.Add(coveragePlugin);
            AutoSize();
        }

        private void Templates_Click(object sender, EventArgs e)
        {
            TemplatesPlugin templatesPlugin = new TemplatesPlugin();
            templatesPlugin.PanelClose += PanelRemoved;
            _controlsList.Add(templatesPlugin);
            AutoSize();
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
                userControl.MinimumSize = new Size(_panelWidth,_panelHeight);
                userControl.MaximumSize = new Size(_panelWidth, _panelHeight+500);
                PluginPanel.Controls.Add(userControl);
            }
        }

        private void PanelRemoved(UserControl sender)
        {
            _controlsList.Remove(sender);
            AutoSize();
        }

        private void SmartViewDropdown_Opening(object sender, CancelEventArgs e)
        {

        }
        
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

        private void Settings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Yes)
            {
                Properties.Settings.Default.PanelHeight = CommonData.PanelHeight;
                Properties.Settings.Default.Save();
            }
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
        #region Tool tips 
        private void SatelliteCreator_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.SatelliteCreator, "Satellite Creator");
        }

        private void FacilityCreator_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.FacilityCreator, "Facility Creator");
        }

        private void SmartView_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.SmartView, "Smart View");
        }

        private void GroundStationAccess_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.GroundStationAccess, "Ground Station Access");
        }

        private void PassiveSafety_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.PassiveSafety, "Passive Safety Checker");
        }

        private void ThreatVolume_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.ThreatVolume, "Volume Creator");
        }

        private void SolarPhase_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.SolarPhase, "Beta Angle Calculator");
        }

        private void GroundEvents_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.GroundEvents, "Ground Events");
        }

        private void Coverage_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Coverage, "Coverage");
        }

        private void Settings_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Settings, "Settings");
        }

        private void Templates_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.Templates, "Templates");
        }
        #endregion

        private void CustomUserInterface_Resize(object sender, EventArgs e)
        {
            foreach (var userControl in _controlsList)
            {
                userControl.Height = PluginPanel.Height-2;
            }
        }
    }
}

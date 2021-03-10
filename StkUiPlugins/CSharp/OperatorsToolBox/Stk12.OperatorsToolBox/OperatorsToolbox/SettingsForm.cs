using System;
using System.IO;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using OperatorsToolbox.Coverage;

namespace OperatorsToolbox
{
    public partial class SettingsForm : Form
    {
        private StkObjectsLibrary _library = new StkObjectsLibrary();
        private int _ssrSelectedIndex = 0;
        private bool _onStart = false;     
        public SettingsForm()
        {
            _onStart = true;
            this.DialogResult = DialogResult.No;
            InitializeComponent();
            if (CommonData.Preferences.SensorGraphicsDisplay)
            {
                SensorGraphics.Checked = true;
            }
            else
            {
                SensorGraphics.Checked = false;
            }
            if (CommonData.Preferences.BordersDisplay)
            {
                BordersToggle.Checked = true;
            }
            else
            {
                BordersToggle.Checked = false;
            }
            if (CommonData.Preferences.IslandDisplay)
            {
                IslandToggle.Checked = true;
            }
            else
            {
                IslandToggle.Checked = false;
            }
            PopulateTypes();
            SatCatPath.Text = CommonData.Preferences.SatCatLocation;
            SatDataPath.Text = CommonData.Preferences.SatDatabaseLocation;
            AreaTargetPath.Text = CommonData.Preferences.AoiLocation;
            UdlAddress.Text = CommonData.Preferences.UdlUrl;
            TemplatesPath.Text = CommonData.Preferences.TemplatesDirectory;
            PanelHeightPixels.Text = CommonData.PanelHeight.ToString();
            _onStart = false;
        }

        private void SatDataBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer(CommonData.Preferences.SatDatabaseLocation, "Satellite Database Location", SatDataPath);
        }

        private void SatCatBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer(CommonData.Preferences.SatCatLocation, "Satellite Catalog Location", SatCatPath);
        }

        private void AreaTargetBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer(CommonData.Preferences.AoiLocation, "AOI File Location", AreaTargetPath);
        }

        private void AddType_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("NewType");
            CommonData.Preferences.EventTypeList.Add("NewType");
            CommonData.Preferences.EventImageLocations.Add("Unspecified");
            listBox1.SelectedIndex=listBox1.Items.Count - 1;
            _ssrSelectedIndex = listBox1.Items.Count - 1;
        }

        private void SensorGraphics_CheckedChanged(object sender, EventArgs e)
        {
            if (!_onStart)
            {
                IAgExecCmdResult result;
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Sensor");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            string path = _library.SimplifiedObjectPath(item);
                            IAgSensor sensor = CommonData.StkRoot.GetObjectFromPath(path) as IAgSensor;
                            if (SensorGraphics.Checked)
                            {
                                sensor.Graphics.InheritFromScenario = false;
                                sensor.Graphics.IsObjectGraphicsVisible = true;
                                CommonData.Preferences.SensorGraphicsDisplay = true;
                            }
                            else
                            {
                                sensor.Graphics.InheritFromScenario = false;
                                CommonData.Preferences.SensorGraphicsDisplay = false;
                                sensor.Graphics.IsObjectGraphicsVisible = false;
                            }
                        }
                    }
                }
            }
        }

        private void RemoveType_Click(object sender, EventArgs e)
        {
            if (_ssrSelectedIndex!=-1)
            {
                CommonData.Preferences.EventTypeList.RemoveAt(_ssrSelectedIndex);
                CommonData.Preferences.EventImageLocations.RemoveAt(_ssrSelectedIndex);
                listBox1.Items.RemoveAt(_ssrSelectedIndex);
            }
            
        }
        private void PopulateTypes()
        {
            foreach (var type in CommonData.Preferences.EventTypeList)
            {
                listBox1.Items.Add(type);
            }

            for (int i = 0; i < CommonData._numPlugins; i++)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.SubItems.Add(i.ToString());
                listItem.Text = ((CustomUserInterface.PluginType)i).ToString();
                AvailablePluginList.Items.Add(listItem);
            }
            AvailablePluginList.Items.Add("Empty_Slot");

            foreach (int item in CommonData.Preferences.PluginConfigList)
            {
                string pluginName = null;
                ListViewItem listItem = new ListViewItem();
                listItem.SubItems.Add(item.ToString());
                if (item != -1)
                {
                    pluginName = ((CustomUserInterface.PluginType)item).ToString();
                    //ToolbarPlugins.Items.Add(pluginName);
                    for (int i = 0; i < AvailablePluginList.Items.Count; i++)
                    {
                        if (AvailablePluginList.Items[i].Text.ToString() == pluginName)
                        {
                            AvailablePluginList.Items.RemoveAt(i);
                        }
                    }
                    listItem.Text = ((CustomUserInterface.PluginType)item).ToString();
                }
                else
                {
                    listItem.Text = "Empty_Slot";
                }
                ToolbarPlugins.Items.Add(listItem);
            }
        }
        public static void BrowseFileExplorer(string initialDirectory, string title, TextBox textBox)
        {
            // Launch file explorer:
            OpenFileDialog fileExplorer = new OpenFileDialog();
            fileExplorer.InitialDirectory = new FileInfo(initialDirectory).DirectoryName;
            fileExplorer.Title = title;

            if (fileExplorer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = fileExplorer.FileName;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            int check = FieldCheck();
            if (check!=1)
            {
                int type = -1;
                CommonData.Preferences.PluginConfigList.Clear();
                foreach (ListViewItem item in ToolbarPlugins.Items)
                {
                    if (String.IsNullOrEmpty(item.Text) || item.Text == "Empty_Slot")
                    {
                        type = -1;
                    }
                    else
                    {
                        type = CustomUserInterface.StringToPluginType(item.Text);
                    }
                    CommonData.Preferences.PluginConfigList.Add(type);
                }
                if (ToolbarPlugins.Items.Count < 10)
                {
                    while (CommonData.Preferences.PluginConfigList.Count < 10)
                    {
                        CommonData.Preferences.PluginConfigList.Add(-1);
                    }
                }
                CommonData.Preferences.UdlUrl = UdlAddress.Text;
                CommonData.Preferences.SatCatLocation = SatCatPath.Text;
                CommonData.Preferences.SatDatabaseLocation = SatDataPath.Text;
                CommonData.Preferences.AoiLocation = AreaTargetPath.Text;
                CommonData.Preferences.TemplatesDirectory = TemplatesPath.Text;
                CommonData.PanelHeight = Int32.Parse(PanelHeightPixels.Text);
                ReadWrite.WritePrefs(Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref"));
                ReadWrite.ReadPrefs(Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref"));
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }
        
        private int FieldCheck()
        {
            int check = 0;
            int temp = 0;

            bool isInt = Int32.TryParse(PanelHeightPixels.Text, out temp);
            if (!isInt)
            {
                MessageBox.Show("Panel Height not an integer");
                check = 1;
            }
            if (temp<300)
            {
                MessageBox.Show("Pixel size must be greater than 300");
                check = 1;
            }
            return check;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                _ssrSelectedIndex = listBox1.SelectedIndex;
                NameText.Text = CommonData.Preferences.EventTypeList[_ssrSelectedIndex];
                ImagePath.Text = CommonData.Preferences.EventImageLocations[_ssrSelectedIndex];
            }
        }

        private void NameText_TextChanged(object sender, EventArgs e)
        {
            CommonData.Preferences.EventTypeList[_ssrSelectedIndex] = NameText.Text.Replace(" ","");
            listBox1.Items[_ssrSelectedIndex]= NameText.Text.Replace(" ", "");
        }

        private void ImageLocationBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer("C:\\", "Image Location", ImagePath);
        }

        private void ImagePath_TextChanged(object sender, EventArgs e)
        {
            CommonData.Preferences.EventImageLocations[_ssrSelectedIndex] = ImagePath.Text;
        }

        private void BordersToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (BordersToggle.Checked==true)
            {
                try
                {
                    string cmd = "VO * GlobeDetails MapDetail Show On Map RWDB2_Coastlines ShowDetail On DetailColor white";
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    cmd = "VO * GlobeDetails MapDetail Show On Map RWDB2_International_Borders ShowDetail On DetailColor white";
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommonData.Preferences.BordersDisplay = true;
                }
                catch (Exception)
                {

                }
            }
            else
            {
                try
                {
                    string cmd = "VO * GlobeDetails MapDetail Show Off";
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommonData.Preferences.BordersDisplay = false;
                }
                catch (Exception)
                {

                }
            }
        }

        private void IslandToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (IslandToggle.Checked==true)
            {
                try
                {
                    string cmd = "VO * GlobeDetails MapDetail Show On Map RWDB2_Islands ShowDetail On DetailColor white";
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommonData.Preferences.IslandDisplay = true;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    string cmd = "VO * GlobeDetails MapDetail Show On Map RWDB2_Islands ShowDetail Off";
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommonData.Preferences.IslandDisplay = false;
                }
                catch (Exception)
                {
                }
            }
        }

        private void RemoveLegends_Click(object sender, EventArgs e)
        {
            CoverageFunctions.RemoveFomLegends();
        }

        private void TemplatesBrowse_Click(object sender, EventArgs e)
        {
            string dirStr = null;
            // Launch file explorer:
            FolderBrowserDialog fileExplorer = new FolderBrowserDialog();
            fileExplorer.Description = "Choose Template Directory";

            if (fileExplorer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dirStr = fileExplorer.SelectedPath;
            }

            if (dirStr != null)
            {
                TemplatesPath.Text = dirStr;
            }
            else
            {
                TemplatesPath.Text = "";
            }
        }

        private void AddTool_Click(object sender, EventArgs e)
        {
            if (AvailablePluginList.SelectedItems != null && AvailablePluginList.FocusedItem != null)
            {
                if (ToolbarPlugins.Items.Count >= 10)
                {
                    MessageBox.Show("You cannot have more than 10 tools on the toolbar at once. Please remove a tool to add another");
                }
                else
                {
                    ToolbarPlugins.Items.Add(AvailablePluginList.SelectedItems[0].Text);
                    if (AvailablePluginList.SelectedItems[0].Text != "Empty_Slot")
                    {
                        AvailablePluginList.Items.RemoveAt(AvailablePluginList.SelectedIndices[0]);
                    }
                }
            }
        }

        private void RemoveTool_Click(object sender, EventArgs e)
        {
            if (ToolbarPlugins.SelectedItems != null && ToolbarPlugins.FocusedItem != null)
            {
                ListViewItem item = new ListViewItem();
                item.Text = ToolbarPlugins.SelectedItems[0].Text;
                if (item.Text != "Empty_Slot")
                {
                    AvailablePluginList.Items.Add(item);
                }
                ToolbarPlugins.Items.RemoveAt(ToolbarPlugins.SelectedIndices[0]);
            }
        }

        private void ToolbarUp_Click(object sender, EventArgs e)
        {
            if (ToolbarPlugins.SelectedItems !=null && ToolbarPlugins.FocusedItem !=null)
            {
                ListViewItem copyItem = new ListViewItem(ToolbarPlugins.SelectedItems[0].Text);
                int index = ToolbarPlugins.SelectedIndices[0];
                ToolbarPlugins.Items.RemoveAt(index);
                ToolbarPlugins.Items.Insert(index - 1, copyItem);
                ToolbarPlugins.FocusedItem = ToolbarPlugins.Items[index - 1];
                ToolbarPlugins.FocusedItem.Selected = true;
            }
        }

        private void ToolbarDown_Click(object sender, EventArgs e)
        {
            if (ToolbarPlugins.SelectedItems != null && ToolbarPlugins.FocusedItem != null)
            {
                ListViewItem copyItem = new ListViewItem(ToolbarPlugins.SelectedItems[0].Text);
                int index = ToolbarPlugins.SelectedIndices[0];
                ToolbarPlugins.Items.RemoveAt(index);
                ToolbarPlugins.Items.Insert(index + 1, copyItem);
                ToolbarPlugins.FocusedItem = ToolbarPlugins.Items[index + 1];
                ToolbarPlugins.FocusedItem.Selected = true;
            }
        }

        private void ToolbarPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AvailablePluginList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

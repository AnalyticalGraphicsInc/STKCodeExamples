using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox
{
    public partial class FirstTimeUseWizard : Form
    {
        private int _pageCounter;
        public FirstTimeUseWizard()
        {
            InitializeComponent();
            _pageCounter = 1;
            InstallGroup.Visible = true;
            InstallGroup.Enabled = true;
            DatabaseGroup.Visible = false;
            DatabaseGroup.Enabled = false;
            ImageGroup.Visible = false;
            ImageGroup.Enabled = false;
            Back.Enabled = false;
            Back.Visible = false;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            string prefpath = null;
            if (_pageCounter == 1)
            {
                if (!String.IsNullOrEmpty(InstallText.Text))
                {
                    Properties.Settings.Default.InstallDir = InstallText.Text;
                    Properties.Settings.Default.Save();
                    //Properties.Settings.Default.Upgrade();
                    CommonData.InstallDir = InstallText.Text;
                    prefpath = Path.Combine(@CommonData.InstallDir, "PluginPreferences.pref");
                    try
                    {
                        ReadWrite.ReadPrefs(prefpath);
                        CommonData.Preferences.SatCatLocation = Path.Combine(@CommonData.InstallDir, "Databases\\SatelliteCatalog.xlsx");
                        CommonData.Preferences.AoiLocation = Path.Combine(@CommonData.InstallDir, "Databases\\AreaTargets\\AOIs.csv");
                        CommonData.Preferences.TemplatesDirectory = Path.Combine(@CommonData.InstallDir, "Databases\\Templates");
                        ReadWrite.WritePrefs(prefpath);

                        _pageCounter = 2;
                        Back.Visible = true;
                        Back.Enabled = true;
                        InstallGroup.Visible = false;
                        InstallGroup.Enabled = false;
                        DatabaseGroup.Visible = true;
                        DatabaseGroup.Enabled = true;

                        SatDataPath.Text = CommonData.Preferences.SatDatabaseLocation;
                        SatCatPath.Text = CommonData.Preferences.SatCatLocation;
                        AreaTargetPath.Text = Path.GetDirectoryName(CommonData.Preferences.AoiLocation);
                        TemplatesPath.Text = CommonData.Preferences.TemplatesDirectory;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Could not read preferences. Check file location.");
                    }
                }
                else
                {
                    MessageBox.Show("Valid Directory Path Required");
                }
            }
            else if (_pageCounter == 2)
            {
                if (!String.IsNullOrEmpty(SatDataPath.Text) && !String.IsNullOrEmpty(SatCatPath.Text) && !String.IsNullOrEmpty(AreaTargetPath.Text) && !String.IsNullOrEmpty(TemplatesPath.Text))
                {
                    CommonData.Preferences.SatDatabaseLocation = SatDataPath.Text;
                    CommonData.Preferences.SatCatLocation = SatCatPath.Text;
                    CommonData.Preferences.AoiLocation = Path.Combine(@AreaTargetPath.Text, "AOIs.csv");
                    CommonData.Preferences.TemplatesDirectory = TemplatesPath.Text;

                    _pageCounter = 3;
                    DatabaseGroup.Visible = false;
                    DatabaseGroup.Enabled = false;
                    ImageGroup.Visible = true;
                    ImageGroup.Enabled = true;
                    ImageDirText.Text = Path.Combine(CommonData.InstallDir, "Images");
                    Next.Text = "Finish";
                }
                else
                {
                    MessageBox.Show("Valid Paths Required");
                }
            }
            else if (_pageCounter == 3)
            {
                if (UpdateImages.Checked)
                {
                    if (!String.IsNullOrEmpty(ImageDirText.Text))
                    {
                        string fileName;
                        for (int i = 0; i < CommonData.Preferences.EventImageLocations.Count; i++)
                        {
                            fileName = Path.GetFileName(CommonData.Preferences.EventImageLocations[i]);
                            CommonData.Preferences.EventImageLocations[i] = Path.Combine(ImageDirText.Text, fileName);
                        }

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Valid Path Required");
                    }
                }
                else
                {
                    _pageCounter = 4;
                    this.Close();
                }
            }
            else if (_pageCounter == 4)
            {
                this.Close();
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (_pageCounter == 2)
            {
                _pageCounter = 1;
                Back.Visible = false;
                Back.Enabled = false;
                InstallGroup.Visible = true;
                InstallGroup.Enabled = true;
                DatabaseGroup.Visible = false;
                DatabaseGroup.Enabled = false;
            }
            else if (_pageCounter == 3)
            {
                _pageCounter = 2;
                Back.Visible = true;
                Back.Enabled = true;
                InstallGroup.Visible = false;
                InstallGroup.Enabled = false;
                DatabaseGroup.Visible = true;
                DatabaseGroup.Enabled = true;
                ImageGroup.Visible = false;
                ImageGroup.Enabled = false;
                Next.Text = "Next";
            }
            else if (_pageCounter == 4)
            {
                _pageCounter = 3;
                Back.Visible = true;
                Back.Enabled = true;
                InstallGroup.Visible = false;
                InstallGroup.Enabled = false;
                DatabaseGroup.Visible = false;
                DatabaseGroup.Enabled = false;
                ImageGroup.Visible = true;
                ImageGroup.Enabled = true;
            }
        }

        private void SatDataBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer(CommonData.Preferences.SatDatabaseLocation, "Satellite Database Location", SatDataPath);
        }

        private void SatCatBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer(CommonData.Preferences.SatDatabaseLocation, "Satellite Catalog Location", SatDataPath);
        }

        private void AreaTargetBrowse_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Choose Area Target Directory", AreaTargetPath);
        }

        private void TemplatesBrowse_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Choose Templates Directory", TemplatesPath);
        }

        private void InstallBrowse_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Choose Install Directory", InstallText);
        }

        private void ImageBrowse_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Choose Ground Event Image Directory", ImageDirText);
        }

        private void UpdateImages_CheckedChanged(object sender, EventArgs e)
        {
            if (UpdateImages.Checked)
            {
                ImageDirText.Enabled = true;
                ImageBrowse.Enabled = true;
            }
            else
            {
                ImageDirText.Enabled = false;
                ImageBrowse.Enabled = false;
            }
        }

        private void BrowseFileExplorer(string initialDirectory, string title, TextBox textBox)
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

        private void BrowseDirectory(string title, TextBox textBox)
        {
            FolderBrowserDialog fileExplorer = new FolderBrowserDialog();
            fileExplorer.Description = title;

            if (fileExplorer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = fileExplorer.SelectedPath;
            }
        }
    }
}

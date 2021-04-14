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

namespace OperatorsToolbox.Templates
{
    public partial class ModifyScriptForm : Form
    {
        private string _templateName;
        public ModifyScriptForm(TemplateScriptData data, string TemplateName)
        {
            InitializeComponent();
            _templateName = TemplateName;
            if (data.PreImportScriptActive)
            {
                UsePreImportScript.Checked = true;
                PreImportScriptPath.Text = data.PreImportScriptPath;
                if (!String.IsNullOrEmpty(data.PreImportArgs))
                {
                    PreArgsText.Text = data.PreImportArgs;
                }
            }
            if (data.PostImportScriptActive)
            {
                UsePostImportScript.Checked = true;
                PostImportScriptPath.Text = data.PostImportScriptPath;
                if (!String.IsNullOrEmpty(data.PostImportArgs))
                {
                    PostArgsText.Text = data.PostImportArgs;
                }
            }
        }

        private void PreImportBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer("C:\\", "Pre-Import Script", PreImportScriptPath);
        }

        private void PostImportBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer("C:\\", "Post-Import Script", PostImportScriptPath);
        }

        private void UsePreImportScript_CheckedChanged(object sender, EventArgs e)
        {
            if (UsePreImportScript.Checked)
            {
                PreImportScriptPath.Enabled = true;
                PreImportBrowse.Enabled = true;
            }
            else
            {
                PreImportScriptPath.Enabled = false;
                PreImportBrowse.Enabled = false;
            }
        }

        private void UsePostImportScript_CheckedChanged(object sender, EventArgs e)
        {
            if (UsePostImportScript.Checked)
            {
                PostImportScriptPath.Enabled = true;
                PostImportBrowse.Enabled = true;
            }
            else
            {
                PostImportScriptPath.Enabled = false;
                PostImportBrowse.Enabled = false;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            TemplateScriptData data = new TemplateScriptData();
            if (UsePreImportScript.Checked)
            {
                data.PreImportScriptActive = true;
                data.PreImportScriptPath = PreImportScriptPath.Text;
                if (!String.IsNullOrEmpty(PreArgsText.Text))
                {
                    data.PreImportArgs = PreArgsText.Text;
                }
            }
            else
            {
                data.PreImportScriptActive = false;
                data.PreImportScriptPath = "";
            }

            if (UsePostImportScript.Checked)
            {
                data.PostImportScriptActive = true;
                data.PostImportScriptPath = PostImportScriptPath.Text;
                if (!String.IsNullOrEmpty(PostArgsText.Text))
                {
                    data.PostImportArgs = PostArgsText.Text;
                }
            }
            else
            {
                data.PostImportScriptActive = false;
                data.PostImportScriptPath = "";
            }
            ReadWrite.WriteTemplateScriptData(Path.Combine(@CommonData.Preferences.TemplatesDirectory, _templateName + "\\", "ScriptData.json"), data);
            this.Close();
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

        private void TestingBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer("C:\\", "Select Script", TestingScriptText);
        }

        private void ExecuteTestScript_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TestingScriptText.Text))
            {
                if (!String.IsNullOrEmpty(TestingArgsText.Text))
                {
                    ReadWrite.ExecuteScript(TestingScriptText.Text, TestingArgsText.Text, false);
                }
                else
                {
                    ReadWrite.ExecuteScript(TestingScriptText.Text, null, false);
                }
            }
            else
            {
                MessageBox.Show("Command Path Required");
            }
        }
    }
}

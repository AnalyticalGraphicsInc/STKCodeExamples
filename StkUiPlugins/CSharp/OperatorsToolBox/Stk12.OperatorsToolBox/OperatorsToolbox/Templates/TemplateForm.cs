using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.Templates
{
    public partial class TemplateForm : Form
    {
        public List<string> TemplateObjects;
        public List<string> AvailableObjects;
        public bool TemplateSaved;
        public TemplateForm()
        {
            InitializeComponent();
            TemplateSaved = false;
            TemplateObjects = new List<string>();
            AvailableObjects = new List<string>();
            PopulateAvailableObjects();
            TemplateName.Text = "MyTemplate";
        }
        public TemplateForm(string templateName)
        {
            InitializeComponent();
            TemplateSaved = false;
            TemplateObjects = new List<string>();
            AvailableObjects = new List<string>();
            PopulateAvailableObjects();
            //PopulateTemplateObjects(templateName);

            TemplateName.Text = templateName;
        }

        private void AddToTemplate_Click(object sender, EventArgs e)
        {
            if (AvailableList.FocusedItem!=null && AvailableList.FocusedItem.Index!=-1)
            {
                List<int> indices = new List<int>();
                foreach (int index in AvailableList.SelectedIndices)
                {
                    if (TemplateObjects.Contains(AvailableList.Items[index].Text))
                    {
                        DialogResult result = MessageBox.Show(AvailableList.Items[index].Text + " already exists in template. Would you like to replace it?","Warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            indices.Add(index);
                        }
                    }
                    else
                    {
                        indices.Add(index);
                        ListViewItem item = new ListViewItem();
                        item.Text = AvailableObjects[index];
                        TemplateList.Items.Add(item);
                        TemplateObjects.Add(AvailableObjects[index]);
                    }
                }
                for (int i = indices.Count; i > 0; i--)
                {
                    AvailableList.Items.RemoveAt(indices[i-1]);
                    AvailableObjects.RemoveAt(indices[i-1]);
                }
            }
        }

        private void RemoveFromTemplate_Click(object sender, EventArgs e)
        {
            if (TemplateList.FocusedItem != null && TemplateList.FocusedItem.Index != -1)
            {
                List<int> indices = new List<int>();
                foreach (int index in TemplateList.SelectedIndices)
                {
                    if (AvailableObjects.Contains(TemplateList.Items[index].Text))
                    {
                        indices.Add(index);
                        ListViewItem item = new ListViewItem();
                        item.Text = TemplateObjects[index];
                        AvailableList.Items.Add(item);
                        AvailableObjects.Add(TemplateObjects[index]);
                    }
                    else
                    {
                        indices.Add(index);
                        ListViewItem item = new ListViewItem();
                        item.Text = TemplateObjects[index];
                        AvailableList.Items.Add(item);
                        AvailableObjects.Add(TemplateObjects[index]);
                    }
                }
                for (int i = indices.Count; i > 0; i--)
                {
                    TemplateList.Items.RemoveAt(indices[i - 1]);
                    TemplateObjects.RemoveAt(indices[i - 1]);
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (TemplateObjects.Count>0)
            {
                if (UsePreImportScript.Checked && string.IsNullOrEmpty(PreImportScriptPath.Text))
                {
                    MessageBox.Show("Script Path Required");
                }
                else if (UsePostImportScript.Checked && string.IsNullOrEmpty(PostImportScriptPath.Text))
                {
                    MessageBox.Show("Script Path Required");
                }
                else if (!string.IsNullOrEmpty(TemplateName.Text))
                {
                    System.IO.Directory.CreateDirectory(Path.Combine(@CommonData.Preferences.TemplatesDirectory,TemplateName.Text));
                    ReadWrite.WriteTemplate(TemplateObjects, Path.Combine(@CommonData.Preferences.TemplatesDirectory, TemplateName.Text + "\\"));
                    if (UsePreImportScript.Checked || UsePostImportScript.Checked)
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
                        ReadWrite.WriteTemplateScriptData(Path.Combine(@CommonData.Preferences.TemplatesDirectory, TemplateName.Text + "\\","ScriptData.json"), data);
                    }
                    TemplateSaved = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Template name not valid");
                }
            }
            else
            {
                MessageBox.Show("There are no objects chosen for the template.");
            }
        }

        private void PopulateAvailableObjects()
        {
            IAgStkObjectCollection children = CommonData.StkRoot.CurrentScenario.Children;
            foreach (IAgStkObject child in children)
            {
                AvailableObjects.Add(child.InstanceName);
                ListViewItem item = new ListViewItem();
                item.Text = child.InstanceName;
                AvailableList.Items.Add(item);
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

        private void PopulateTemplateObjects(string templateName)
        {
            string fileStr = CommonData.Preferences.TemplatesDirectory + "\\" + templateName + "\\";
            List<string> objectNames = ReadWrite.GetTemplateObjectNames(fileStr);
            foreach (var name in objectNames)
            {
                TemplateObjects.Add(name);
                ListViewItem item = new ListViewItem();
                item.Text = name;
                TemplateList.Items.Add(item);
            }
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

        private void PreImportBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer("C:\\", "Pre-Import Script", PreImportScriptPath);
        }

        private void PostImportBrowse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer("C:\\", "Post-Import Script", PostImportScriptPath);
        }
    }
}

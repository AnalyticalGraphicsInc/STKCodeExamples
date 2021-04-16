﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OperatorsToolbox.Templates
{
    public partial class TemplatesPlugin : OpsPluginControl
    {
        private string _fileStr = CommonData.Preferences.TemplatesDirectory + "\\";
        //string _fileStr = Path.Combine(@CommonData.InstallDir, "Databases\\Templates\\");

        public TemplatesPlugin()
        {
            InitializeComponent();
            PopulateTemplates();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void NewTemplate_Click(object sender, EventArgs e)
        {
            TemplateForm form = new TemplateForm();
            form.ShowDialog();
            PopulateTemplates();
        }

        private void RemoveTemplate_Click(object sender, EventArgs e)
        {
            if (TemplateList.FocusedItem != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this Template?\nThis cannot be undone", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string dir = _fileStr + TemplateList.FocusedItem.Text + "\\";
                    string[] filePaths = Directory.GetFiles(dir);
                    foreach (string filePath in filePaths)
                        File.Delete(filePath);
                    Directory.Delete(dir);

                    PopulateTemplates();
                    //ObjectList.Nodes.Clear();
                    ObjectList.Items.Clear();
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if (TemplateList.FocusedItem.Index != -1 && TemplateList.FocusedItem != null && ObjectList.Items.Count!=0)
            {
                List<string> objectNames = new List<string>();
                foreach (ListViewItem item in ObjectList.CheckedItems)
                {
                    objectNames.Add(item.Text.ToString());
                    //if (item.Checked)
                    //{
                    //    objectNames.Add(item.Text);
                    //}
                }
                ReadWrite.ImportTemplate(_fileStr + TemplateList.FocusedItem.Text + "\\", objectNames, EraseReplace.Checked);
            }
        }

        private void TemplateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TemplateList.SelectedItems != null && TemplateList.SelectedItems.Count > 0)
            {
                List<string> objectNames = ReadWrite.GetTemplateObjectNames(_fileStr + TemplateList.FocusedItem.Text + "\\");
                ObjectList.Items.Clear();
                int count = 0;
                foreach (var item in objectNames)
                {
                    ObjectList.Items.Add(item);
                    ObjectList.Items[count].Checked = true;
                    //node.Checked = true;
                    count++;
                }
            }
        }

        private void PopulateTemplates()
        {
            TemplateList.Items.Clear();
            bool dirExists = Directory.Exists(_fileStr);
            if (dirExists)
            {
                List<string> tempNames = ReadWrite.FindTemplates(_fileStr);
                foreach (var item in tempNames)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = item;
                    TemplateList.Items.Add(listItem);
                }
            }
            else
            {
                MessageBox.Show("File Error. Check Templates Directory in Settings");
            }
        }

        private void ScriptingOptions_Click(object sender, EventArgs e)
        {
            if (TemplateList.FocusedItem != null)
            {
                TemplateScriptData data = new TemplateScriptData();
                if (File.Exists(_fileStr + TemplateList.FocusedItem.Text + "\\ScriptData.json"))
                {
                    data = ReadWrite.GetTemplateScriptData(_fileStr + TemplateList.FocusedItem.Text + "\\ScriptData.json");
                }
                else
                {
                    data.PostImportScriptActive = false;
                    data.PreImportScriptActive = false;
                    data.PreImportScriptPath = "";
                    data.PostImportScriptPath = "";
                }
                ModifyScriptForm form = new ModifyScriptForm(data, TemplateList.FocusedItem.Text);
                form.Show();
            }
        }
    }
}

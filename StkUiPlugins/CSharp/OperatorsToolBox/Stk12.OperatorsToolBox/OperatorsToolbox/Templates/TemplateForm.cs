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
                if (!string.IsNullOrEmpty(TemplateName.Text))
                {
                    System.IO.Directory.CreateDirectory(Path.Combine(@CommonData.Preferences.TemplatesDirectory,TemplateName.Text));
                    ReadWrite.WriteTemplate(TemplateObjects, Path.Combine(@CommonData.Preferences.TemplatesDirectory, TemplateName.Text + "\\"));
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
    }
}

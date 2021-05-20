using System;
using System.Collections.Generic;
using System.Windows.Forms;



namespace StkDataProviderExec
{
    public partial class Form1 : Form
    {
        private StkDataProviders dataProviders;

        public Form1()
        {
            InitializeComponent();
            
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "Data";
            col1.Width = 120;
            dataGridView1.Columns.AddRange(col1);
            dataProviders = new StkDataProviders();
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            PopulateStkObjects();
            PopulateDPs();
            PopulateDPGroups();
            PopulateDPElements();
        }

        private void PopulateStkObjects()
        {
            comboBox_stkObjects.Items.Clear();

            foreach (string item in dataProviders.GetAvailableStkObjects())
            {
                comboBox_stkObjects.Items.Add(item);
            }
            foreach (string item in dataProviders.GetAvailableStkAccesses())
            {
                comboBox_stkObjects.Items.Add("Access: " + item);
            }
            comboBox_stkObjects.Text = comboBox_stkObjects.Items[0].ToString();
        }
        private void PopulateDPs()
        {
            comboBox_DataProviders.Items.Clear();
            foreach (string item in dataProviders.GetAvailableDataProviders(comboBox_stkObjects.Text))
            {
                comboBox_DataProviders.Items.Add(item);
            }
            comboBox_DataProviders.Text = comboBox_DataProviders.Items[0].ToString();

        }
        private void PopulateDPGroups()
        {
            comboBox_Groups.Items.Clear();
            foreach (string item in dataProviders.GetAvailableGroups(
                comboBox_stkObjects.Text, comboBox_DataProviders.Text))
            {
                comboBox_Groups.Items.Add(item);
            }
            if (comboBox_Groups.Items.Count > 0)
            {
                comboBox_Groups.Text = comboBox_Groups.Items[0].ToString();
            }
            else
            {
                comboBox_Groups.Text = "----------";
            }
        }
        private void PopulateDPElements()
        {
            comboBox_Elements.Items.Clear();
            foreach (string item in dataProviders.GetAvailableDataProviderElements(
                comboBox_stkObjects.Text, comboBox_DataProviders.Text, comboBox_Groups.Text))
            {
                comboBox_Elements.Items.Add(item);
            }
            if (comboBox_Elements.Items.Count > 0)
            {
                comboBox_Elements.Text = comboBox_Elements.Items[0].ToString();
            }
            else
            {
                comboBox_Elements.Text = "----------";
            }           

        }

        private void comboBox_stkObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDPs();
            PopulateDPGroups();
            PopulateDPElements();
        }

        private void comboBox_DataProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDPGroups();
            PopulateDPElements();
        }

        private void button_GetData_Click(object sender, EventArgs e)
        {
            Array results = dataProviders.GetDataProviders(comboBox_stkObjects.Text, comboBox_DataProviders.Text,
                comboBox_Groups.Text, comboBox_Elements.Text);

            dataGridView1.Rows.Clear();

            foreach (var item in results)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                DataGridViewTextBoxCell dataCell = new DataGridViewTextBoxCell();
                dataCell.Value = item.ToString();

                newRow.Cells.Add(dataCell);
                dataGridView1.Rows.Add(newRow);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> test = dataProviders.GetAvailableStkAccesses();

            foreach (var item in test)
            {
                MessageBox.Show(item);    
            }
            
        }     
        

    }
}

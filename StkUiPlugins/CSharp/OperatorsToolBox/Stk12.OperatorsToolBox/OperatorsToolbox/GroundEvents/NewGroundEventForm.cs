using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.GroundEvents
{
    public partial class NewGroundEventForm : Form
    {
        private GroundEvent _contactEvent;
        public NewGroundEventForm()
        {
            InitializeComponent();
            _contactEvent = new GroundEvent();
            ManualSSR.Checked = true;
            SSRFromFile.Checked = false;
            FileText.Enabled = false;
            Browse.Enabled = false;

            foreach (var type in CommonData.Preferences.EventTypeList)
            {
                TypeSelect.Items.Add(type);
            }
            TypeSelect.SelectedIndex = 0;

            IAgScenario scenario = (IAgScenario)CommonData.StkRoot.CurrentScenario;
            StartTimeText.Text = scenario.StartTime;
            StopTimeText.Text = scenario.StopTime;
            ImportActive.Checked = true;
            IDText.Text = "NewEvent";
            Latitude.Text = "0";
            Longitude.Text = "0";
            CountryText.Text = "Global";

            foreach (string c in Enum.GetNames(typeof(CustomUserInterface.ColorOptions)))
            {
                ColorSelection.Items.Add(c);
                SheetColor.Items.Add(c);
            }
            ColorSelection.SelectedIndex = 0;
            SheetColor.SelectedIndex = 0;
        }

        private void SSRFromFile_CheckedChanged(object sender, EventArgs e)
        {
            if (SSRFromFile.Checked==true)
            {
                ManualSSR.Checked = false;
                SSRFromFile.Checked = true;
                IDText.Enabled = false;
                CountryText.Enabled = false;
                Latitude.Enabled = false;
                Longitude.Enabled = false;
                StartTimeText.Enabled = false;
                StopTimeText.Enabled = false;
                DesciptionText.Enabled = false;
                TypeSelect.Enabled = false;
                FileText.Enabled = true;
                Browse.Enabled = true;
                SheetColor.Enabled = true;
                ColorSelection.Enabled = false;

            }
        }

        private void ManualSSR_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualSSR.Checked==true)
            {
                ManualSSR.Checked = true;
                SSRFromFile.Checked = false;
                IDText.Enabled = true;
                CountryText.Enabled = true;
                Latitude.Enabled = true;
                Longitude.Enabled = true;
                StartTimeText.Enabled = true;
                StopTimeText.Enabled = true;
                DesciptionText.Enabled = true;
                FileText.Enabled = false;
                Browse.Enabled = false;
                TypeSelect.Enabled = true;
                SheetColor.Enabled = false;
                ColorSelection.Enabled = true;
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (ManualSSR.Checked)
            {
                int fieldCheck = FieldCheck();
                if (fieldCheck == 0)
                {
                    GroundEvent current = new GroundEvent();
                    current.Id  = Regex.Replace(IDText.Text, @"[^0-9a-zA-Z_]+", "");
                    current.Country = CountryText.Text;
                    current.Latitude = Latitude.Text;
                    current.Longitude = Longitude.Text;
                    string start = ReadWrite.CheckTimeCell(StartTimeText.Text);
                    if (start == "Unspecified")
                    {
                        current.StartTime = "Unspecified";
                        current.MilStartTime = "Unspecified";
                    }
                    else
                    {
                        current.MilStartTime = StartTimeText.Text;
                        current.StartTime = StartTimeText.Text;
                    }

                    string stop = ReadWrite.CheckTimeCell(StopTimeText.Text);
                    if (stop == "Unspecified")
                    {
                        current.StopTime = "Unspecified";
                        current.MilStopTime = "Unspecified";
                    }
                    else
                    {
                        current.MilStopTime = StopTimeText.Text;
                        current.StopTime = StopTimeText.Text;
                    }
                    current.Description = DesciptionText.Text;
                    current.SsrType = TypeSelect.Text;

                    if (!String.IsNullOrEmpty(_contactEvent.Poc))
                    {
                        current.Poc = _contactEvent.Poc;
                    }
                    if (!String.IsNullOrEmpty(_contactEvent.PocPhone))
                    {
                        current.PocPhone = _contactEvent.PocPhone;
                    }
                    if (!String.IsNullOrEmpty(_contactEvent.PocEmail))
                    {
                        current.PocEmail = _contactEvent.PocEmail;
                    }

                    current.ColorOption = ColorSelection.Text;
                    CommonData.CurrentEvents.Add(current);

                    GroundEventFunctions.CreateGroundEvent(current);
                    CreatorFunctions.ChangeObjectColor("Place/" + current.Id, (CustomUserInterface.ColorOptions)Enum.Parse(typeof(CustomUserInterface.ColorOptions), ColorSelection.Text));

                    ReadWrite.WriteEventFile(CommonData.EventFileStr);
                }
                CommonData.NewSsrCreated = true;
            }
            else if(SSRFromFile.Checked)
            {
                int importOption = 0;
                if (ImportAll.Checked)
                {
                    importOption = 1;
                }
                ReadWrite.ImportEventSheet(FileText.Text, importOption, SheetColor.Text);
                CommonData.NewSsrCreated = true;
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FileText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            BrowseFileExplorer(CommonData.DirectoryStr, "Choose Input File", FileText);
        }
        // Function to launch file explorer and write filepath to text box:
        public static void BrowseFileExplorer(string initialDirectory, string title, TextBox textBox)
        {
            // Launch file explorer:
            OpenFileDialog fileExplorer = new OpenFileDialog();
            fileExplorer.InitialDirectory = initialDirectory;
            fileExplorer.Title = title;

            if (fileExplorer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = fileExplorer.FileName;
            }
        }
        private int FieldCheck()
        {
            int check = 0;
            double latitude;
            double longitude;

            bool isNumerical = Double.TryParse(Latitude.Text, out latitude);
            if (!isNumerical)
            {
                MessageBox.Show("Latitude field not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(Longitude.Text, out longitude);
            if (!isNumerical)
            {
                MessageBox.Show("Longitude field not a number");
                check = 1;
            }
            bool exists = CommonData.StkRoot.ObjectExists("Place/" + Regex.Replace(IDText.Text, @"[^0-9a-zA-Z_]+", ""));
            if (exists)
            {
                MessageBox.Show("Object with this name already exists in the scenario");
                check = 1;
            }


            return check;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SubObjects_Click(object sender, EventArgs e)
        {

        }

        private void ImportActive_CheckedChanged(object sender, EventArgs e)
        {
            if (ImportActive.Checked)
            {
                ImportAll.Checked = false;
            }
        }

        private void ImportAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ImportAll.Checked)
            {
                ImportActive.Checked = false;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void Contact_Click(object sender, EventArgs e)
        {
            ContactInfoForm form = new ContactInfoForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                _contactEvent.Poc = form.contactEvent.Poc;
                _contactEvent.PocPhone = form.contactEvent.PocPhone;
                _contactEvent.PocEmail = form.contactEvent.PocEmail;
            }
        }
    }
}

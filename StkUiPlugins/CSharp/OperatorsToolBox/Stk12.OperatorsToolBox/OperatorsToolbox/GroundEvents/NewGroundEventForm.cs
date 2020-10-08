using System;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.GroundEvents
{
    public partial class NewGroundEventForm : Form
    {
        public NewGroundEventForm()
        {
            InitializeComponent();

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
                    current.Id = IDText.Text;
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

                    CommonData.CurrentEvents.Add(current);
                    GroundEventFunctions.CreateGroundEvent(current);
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
                ReadWrite.ImportEventSheet(FileText.Text, importOption);
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
    }
}

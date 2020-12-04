using System;
using System.Windows.Forms;
using AGI.STKObjects;

namespace OperatorsToolbox.GroundEvents
{
    public partial class EditForm : Form
    {
        private GroundEvent _contactEvent { get; set; }
        public EditForm()
        {
            InitializeComponent();
            _contactEvent = new GroundEvent();
            IDText.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id;
            CountryText.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Country;
            Latitude.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Latitude;
            Longitude.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Longitude;
            StartTimeText.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].StartTime;
            StopTimeText.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].StopTime;
            if (!String.IsNullOrEmpty(CommonData.CurrentEvents[CommonData.EventSelectedIndex].Poc))
            {
                _contactEvent.Poc = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Poc;
            }
            if (!String.IsNullOrEmpty(CommonData.CurrentEvents[CommonData.EventSelectedIndex].PocPhone))
            {
                _contactEvent.PocPhone = CommonData.CurrentEvents[CommonData.EventSelectedIndex].PocPhone;
            }
            if (!String.IsNullOrEmpty(CommonData.CurrentEvents[CommonData.EventSelectedIndex].PocEmail))
            {
                _contactEvent.PocPhone = CommonData.CurrentEvents[CommonData.EventSelectedIndex].PocEmail;
            }
            //Populate description if not null
            if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].Description !=null)
            {
                DesciptionText.Text = CommonData.CurrentEvents[CommonData.EventSelectedIndex].Description;
            }
            //Select correct index for Type ComboBox
            foreach (var type in CommonData.Preferences.EventTypeList)
            {
                TypeSelect.Items.Add(type);
            }
            int index = GroundEventFunctions.GetImageIndex(CommonData.CurrentEvents[CommonData.EventSelectedIndex]);
            TypeSelect.SelectedIndex = index;

        }

        private void Apply_Click(object sender, EventArgs e)
        {
            CommonData.TypeChanged = false;
            int check = FieldCheck();
            if (check==0)
            {
                string path = "*/Place/" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id;
                GroundEventFunctions.RemoveTimelineComponent(CommonData.CurrentEvents[CommonData.EventSelectedIndex]);
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id = IDText.Text;
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].Country = CountryText.Text;
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].Latitude = Latitude.Text;
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].Longitude = Longitude.Text;
                string start = GroundEventFunctions.ConvertMilTime(StartTimeText.Text);
                if (start == "Unspecified")
                {
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].StartTime = "Unspecified";
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].MilStartTime = "Unspecified";
                }
                else
                {
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].MilStartTime = StartTimeText.Text;
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].StartTime = StartTimeText.Text;
                }

                string stop = ReadWrite.CheckTimeCell(StopTimeText.Text);
                if (stop == "Unspecified")
                {
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].StopTime = "Unspecified";
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].MilStopTime = "Unspecified";
                }
                else
                {
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].MilStopTime = StopTimeText.Text;
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].StopTime = StopTimeText.Text;
                }
                CommonData.CurrentEvents[CommonData.EventSelectedIndex].Description = DesciptionText.Text;
                
                //Rename Object/sub-objects
                string cmd = "Rename " + path + " " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id;
                CommonData.StkRoot.ExecuteCommand(cmd);
                if (CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects != null)
                {
                    foreach (SubObject item in CommonData.CurrentEvents[CommonData.EventSelectedIndex].SubObjects)
                    {
                        cmd = "Rename " + path + "-" + item.Name + " " + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + "-" + item.Name;
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                }


                //Reassign position
                path = "Place/" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id;
                IAgPlace place = CommonData.StkRoot.GetObjectFromPath(path) as IAgPlace;
                place.Position.AssignGeodetic(Double.Parse(Latitude.Text), Double.Parse(Longitude.Text), 0);

                //Change object marker if type changed
                string currentType = CommonData.CurrentEvents[CommonData.EventSelectedIndex].SsrType;
                if (currentType != TypeSelect.Text)
                {
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].SsrType = TypeSelect.Text;
                    CommonData.TypeChanged = true;

                    string filePath = GroundEventFunctions.GetImagePath(CommonData.CurrentEvents[CommonData.EventSelectedIndex].SsrType);

                    cmd = "VO */Place/" + CommonData.CurrentEvents[CommonData.EventSelectedIndex].Id + " marker show on markertype imagefile imagefile \"" + filePath + "\" Size 32";
                    CommonData.StkRoot.ExecuteCommand(cmd);
                }
                else
                {
                    CommonData.CurrentEvents[CommonData.EventSelectedIndex].SsrType = TypeSelect.Text;
                }
                ReadWrite.WriteEventFile(CommonData.EventFileStr);
                this.Close();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void Contact_Click(object sender, EventArgs e)
        {
            ContactInfoForm form = new ContactInfoForm(_contactEvent);
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

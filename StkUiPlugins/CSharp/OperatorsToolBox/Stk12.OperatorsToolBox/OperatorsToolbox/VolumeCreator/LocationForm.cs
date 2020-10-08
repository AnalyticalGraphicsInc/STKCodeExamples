using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OperatorsToolbox.VolumeCreator
{
    public partial class LocationForm : Form
    {
        public LocationForm()
        {
            InitializeComponent();
            PopulateBoxes();
            if (CommonData.FromEdit)
            {
                Create.Text = "Apply";
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            int check = FieldCheck();
            if (check==0)
            {
                LocationConfig current;
                if (CommonData.FromEdit)
                {
                    current = CommonData.LocationList[CommonData.LocationIndex];
                }
                else
                {
                    current = new LocationConfig();
                }
                current.Latitude = Lat.Text;
                current.Longitude = Long.Text;
                string name = Regex.Replace(LocationName.Text, @"\s", "_");
                current.Name = name;
                CommonData.LocationName = name;
                if (!CommonData.FromEdit)
                {
                    CommonData.LocationList.Add(current);
                }
                this.Close();
            }
        }

        private void SelectFacility_Click(object sender, EventArgs e)
        {
            CommonData.FacilitySelected = false;
            FacilitySelectForm form = new FacilitySelectForm();
            form.ShowDialog();
            if (CommonData.FacilitySelected)
            {
                Lat.Text = CommonData.FacilityLat;
                Long.Text = CommonData.FacilityLong;
                LocationName.Text = CommonData.FacilityName;
            }

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateBoxes()
        {
            if (CommonData.FromEdit)
            {
                Lat.Text = CommonData.LocationList[CommonData.LocationIndex].Latitude;
                Long.Text = CommonData.LocationList[CommonData.LocationIndex].Longitude;
                LocationName.Text = CommonData.LocationList[CommonData.LocationIndex].Name;
            }
            else
            {
                Lat.Text = "0";
                Long.Text = "0";
                LocationName.Text = "New_Launch_Facility";
            }

        }
        private int FieldCheck()
        {
            int check = 0;
            double value = 0.0;
            bool isNumerical = Double.TryParse(Lat.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Latitude field is not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(Long.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Longitude field is not a number");
                check = 1;
            }
            if (LocationName.Text == null || LocationName.Text == "")
            {
                MessageBox.Show("Name Required");
                check = 1;
            }
            return check;
        }
    }
}

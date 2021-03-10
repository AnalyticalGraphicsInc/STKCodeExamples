using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OperatorsToolbox.VolumeCreator
{
    public partial class NewConfigForm : Form
    {
        public NewConfigForm()
        {
            InitializeComponent();
            PopulateBoxes();
            if (CommonData.FromEdit)
            {
                Create.Text = "Apply";
                ConfigName.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].Name;
            }
            else
            {
                ConfigName.Text = "New_Threat_Volume";
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            int check = FieldCheck();
            if (check==0)
            {
                VolumeConfig current;
                if (CommonData.FromEdit)
                {
                    current = CommonData.VolumeList[CommonData.TvSelectedIndex];
                }
                else
                {
                    current = new VolumeConfig();
                }
                current.MinAlt = MinAlt.Text;
                current.MaxAlt = MaxAlt.Text;
                current.MinEl = MinEl.Text;
                current.MaxEl = MaxEl.Text;
                current.MinRange = MinRange.Text;
                current.MaxRange = MaxRange.Text;
                current.MinAz = MinAz.Text;
                current.MaxAz = MaxAz.Text;
                string name = Regex.Replace(ConfigName.Text, @"\s", "_");
                current.Name = name;
                CommonData.VolumeName = name;
                if (!CommonData.FromEdit)
                {
                    CommonData.VolumeList.Add(current);
                }
                this.Close();
            }
            
        }

        private void PopulateBoxes()
        {
            if (CommonData.FromEdit)
            {
                MinAlt.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MinAlt;
                MaxAlt.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MaxAlt;
                MinRange.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MinRange;
                MaxRange.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MaxRange;
                MinEl.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MinEl;
                MaxEl.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MaxEl;
                MinAz.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MinAz;
                MaxAz.Text = CommonData.VolumeList[CommonData.TvSelectedIndex].MaxAz;
            }
            else
            {
                MinAlt.Text = "100";
                MaxAlt.Text = "1200";
                MinRange.Text = "250";
                MaxRange.Text = "2000";
                MinEl.Text = "10";
                MaxEl.Text = "80";
                MinAz.Text = "0";
                MaxAz.Text = "360";
            }

        }

        private int FieldCheck()
        {
            int check = 0;
            double value=0.0;
            bool isNumerical = Double.TryParse(MinAlt.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Minimum Altitude field is not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(MaxAlt.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Maximum Altitude field is not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(MinRange.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Minimum Range field is not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(MaxRange.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Maximum Range field is not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(MinEl.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Minimum Elevation field is not a number");
                check = 1;
            }
            isNumerical = Double.TryParse(MaxEl.Text, out value);
            if (!isNumerical)
            {
                MessageBox.Show("Maximum Elevation field is not a number");
                check = 1;
            }
            if (ConfigName.Text ==null ||ConfigName.Text =="")
            {
                MessageBox.Show("Name Required");
                check = 1;
            }

            return check;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

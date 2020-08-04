using System;
using System.Windows.Forms;

namespace OperatorsToolbox.FacilityCreator
{

    public partial class ChangeConstraintsForm : Form
    {
        public OpticalParams OParams { get; set; }
        public RadarParams RParams { get; set; }
        public ChangeConstraintsForm(OpticalParams oParams)
        {
            InitializeComponent();
            OParams = new OpticalParams();
            OpticalControlPanel.Visible = true;
            OpticalControlPanel.Enabled = true;
            RadarControlPanel.Visible = false;
            RadarControlPanel.Enabled = false;
            MinRange.Text = oParams.MinRange;
            MaxRange.Text = oParams.MaxRange;
            MinEl.Text = oParams.MinEl;
            MaxEl.Text = oParams.MaxEl;
            LunExAngle.Text = oParams.LunarExAngle;
            MaxSunEl.Text = oParams.SunElAngle;
            HalfAngle.Text = oParams.HalfAngle;
        }
        public ChangeConstraintsForm(RadarParams rParams)
        {
            InitializeComponent();
            RParams = new RadarParams();
            OpticalControlPanel.Visible = false;
            OpticalControlPanel.Enabled = false;
            RadarControlPanel.Visible = true;
            RadarControlPanel.Enabled = true;
            RMinRange.Text = rParams.MinRange;
            RMaxRange.Text = rParams.MaxRange;
            RMinEl.Text = rParams.MinEl;
            RMaxEl.Text = rParams.MaxEl;
            SolarExAngle.Text = rParams.SolarExAngle;
            RHalfAngle.Text = rParams.HalfAngle;

        }

        private void Save_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (OpticalControlPanel.Enabled)
            {
                Tuple<int, string> check = FieldCheckOptical();
                if (check.Item1==0)
                {
                    OParams.MinRange = MinRange.Text;
                    OParams.MaxRange = MaxRange.Text;
                    OParams.MinEl = MinEl.Text;
                    OParams.MaxEl = MaxEl.Text;
                    OParams.LunarExAngle = LunExAngle.Text;
                    OParams.SunElAngle = MaxSunEl.Text;
                    OParams.HalfAngle = HalfAngle.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(check.Item2);
                }
            }
            else if (RadarControlPanel.Enabled)
            {
                Tuple<int, string> check = FieldCheckRadar();
                if (check.Item1==0)
                {
                    RParams.MinRange = RMinRange.Text;
                    RParams.MaxRange = RMaxRange.Text;
                    RParams.MinEl = RMinEl.Text;
                    RParams.MaxEl = RMaxEl.Text;
                    RParams.SolarExAngle = SolarExAngle.Text;
                    RParams.HalfAngle = RHalfAngle.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(check.Item2);
                }
            }
        }

        private Tuple<int, string> FieldCheckOptical()
        {
            int check = 0;
            string checkStr = "Input errors:\n";
            double temp = 0.0;
            bool isNumerical;
            isNumerical = Double.TryParse(MinRange.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Min Range field not a number\n";
            }
            isNumerical = Double.TryParse(MaxRange.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Max Range field not a number\n";
            }
            isNumerical = Double.TryParse(MinEl.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Min Elevation Angle field not a number\n";
            }
            isNumerical = Double.TryParse(MaxEl.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Max Elevation Angle field not a number\n";
            }
            isNumerical = Double.TryParse(HalfAngle.Text, out temp);
            if (!isNumerical || temp < 0)
            {
                check = 1;
                checkStr = checkStr + "Half Angle field not a number or is negative\n";
            }
            isNumerical = Double.TryParse(MaxSunEl.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Solar Elevation Angle field not a number\n";
            }
            isNumerical = Double.TryParse(LunExAngle.Text, out temp);
            if (!isNumerical || temp < 0)
            {
                check = 1;
                checkStr = checkStr + "Lunar Exclusion Angle field not a number or is negative\n";
            }
            return Tuple.Create(check, checkStr);
        }

        private Tuple<int, string> FieldCheckRadar()
        {
            int check = 0;
            string checkStr = "Input errors:\n";
            double temp = 0.0;
            bool isNumerical;
            isNumerical = Double.TryParse(RMinRange.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Min Range field not a number\n";
            }
            isNumerical = Double.TryParse(RMaxRange.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Max Range field not a number\n";
            }
            isNumerical = Double.TryParse(RMinEl.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Min Elevation Angle field not a number\n";
            }
            isNumerical = Double.TryParse(RMaxEl.Text, out temp);
            if (!isNumerical)
            {
                check = 1;
                checkStr = checkStr + "Max Elevation Angle field not a number\n";
            }
            isNumerical = Double.TryParse(RHalfAngle.Text, out temp);
            if (!isNumerical || temp < 0)
            {
                check = 1;
                checkStr = checkStr + "Half Angle field not a number or is negative\n";
            }
            isNumerical = Double.TryParse(SolarExAngle.Text, out temp);
            if (!isNumerical || temp < 0)
            {
                check = 1;
                checkStr = checkStr + "Solar Exclusion Angle field not a number or is negative\n";
            }
            return Tuple.Create(check, checkStr);
        }
    }
}

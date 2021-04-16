using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OperatorsToolbox.FacilityCreator
{

    public partial class ChangeConstraintsForm : Form
    {
        public List<FCSensor> sensors { get; set; }
        public bool opt { get; set; }

        private bool _onMenuSwitch {get; set;}
        public ChangeConstraintsForm(List<FCSensor> sensorList, bool isOpt)
        {
            InitializeComponent();
            sensors = new List<FCSensor>();
            foreach (var sensor in sensorList)
            {
                _onMenuSwitch = true;
                sensors.Add(new FCSensor(sensor));
                SensorList.Items.Add(sensor.SensorName);
                if (isOpt)
                {
                    opt = true;
                    OpticalControlPanel.Visible = true;
                    OpticalControlPanel.Enabled = true;
                    RadarControlPanel.Visible = false;
                    RadarControlPanel.Enabled = false;
                }
                else
                {
                    opt = false;
                    OpticalControlPanel.Visible = false;
                    OpticalControlPanel.Enabled = false;
                    RadarControlPanel.Visible = true;
                    RadarControlPanel.Enabled = true;
                }
                if (SensorList.Items.Count != 0)
                {
                    SensorList.SelectedIndex = 0;
                }
            }
            _onMenuSwitch = false;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (OpticalControlPanel.Enabled)
            {
                Tuple<int, string> check = FieldCheckOptical();
                if (check.Item1==0)
                {
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
            string sensorName = null;
            foreach (FCSensor sensor in sensors)
            {
                isNumerical = Double.TryParse(sensor.OParams.MinRange, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Min Range field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.MaxRange, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Max Range field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.MinEl, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Min Elevation Angle field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.MaxEl, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Max Elevation Angle field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.HalfAngle, out temp);
                if (!isNumerical || temp < 0)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Half Angle field not a number or is negative\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.SunElAngle, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Solar Elevation Angle field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.LunarExAngle, out temp);
                if (!isNumerical || temp < 0)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Lunar Exclusion Angle field not a number or is negative\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.MinAz, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Min Azimuth Angle field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.OParams.MaxAz, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Max Azimuth Angle field not a number\n";
                }
            }
            return Tuple.Create(check, checkStr);
        }

        private Tuple<int, string> FieldCheckRadar()
        {
            int check = 0;
            string checkStr = "Input errors:\n";
            double temp = 0.0;
            bool isNumerical;
            string sensorName = null;
            foreach (FCSensor sensor in sensors)
            {
                sensorName = sensor.SensorName;
                isNumerical = Double.TryParse(sensor.RParams.MinRange, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Min Range field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.RParams.MaxRange, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Max Range field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.RParams.MinEl, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Min Elevation Angle field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.RParams.MaxEl, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Max Elevation Angle field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.RParams.HalfAngle, out temp);
                if (!isNumerical || temp < 0)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Half Angle field not a number or is negative\n";
                }
                isNumerical = Double.TryParse(sensor.RParams.SolarExAngle, out temp);
                if (!isNumerical || temp < 0)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Solar Exclusion Angle field not a number or is negative\n";
                }
                isNumerical = Double.TryParse(sensor.RParams.MinAz, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Min Azimuth Angle field not a number\n";
                }
                isNumerical = Double.TryParse(sensor.RParams.MaxAz, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    checkStr = checkStr + sensorName + "- " + "Max Azimuth Angle field not a number\n";
                }
            }
            return Tuple.Create(check, checkStr);
        }

        private void SensorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SensorList.SelectedIndex != -1 && SensorList.SelectedItem != null)
            {
                _onMenuSwitch = true;
                if (opt)
                {
                    OpticalParams oParams = sensors[SensorList.SelectedIndex].OParams;
                    MinRange.Text = oParams.MinRange;
                    MaxRange.Text = oParams.MaxRange;
                    MinEl.Text = oParams.MinEl;
                    MaxEl.Text = oParams.MaxEl;
                    LunExAngle.Text = oParams.LunarExAngle;
                    MaxSunEl.Text = oParams.SunElAngle;
                    HalfAngle.Text = oParams.HalfAngle;
                    MinAz.Text = oParams.MinAz;
                    MaxAz.Text = oParams.MaxAz;
                    PointingAz.Text = oParams.Az;
                    PointingEl.Text = oParams.El;
                    OName.Text = sensors[SensorList.SelectedIndex].SensorName;
                }
                else
                {
                    RadarParams rParams = sensors[SensorList.SelectedIndex].RParams;
                    RMinRange.Text = rParams.MinRange;
                    RMaxRange.Text = rParams.MaxRange;
                    RMinEl.Text = rParams.MinEl;
                    RMaxEl.Text = rParams.MaxEl;
                    SolarExAngle.Text = rParams.SolarExAngle;
                    RHalfAngle.Text = rParams.HalfAngle;
                    RMinAz.Text = rParams.MinAz;
                    RMaxAz.Text = rParams.MaxAz;
                    RPointingAz.Text = rParams.Az;
                    RPointingEl.Text = rParams.El;
                    RName.Text = sensors[SensorList.SelectedIndex].SensorName;
                }
                _onMenuSwitch = false;
            }

        }

        private void OName_TextChanged(object sender, EventArgs e)
        {
            if (SensorList.SelectedIndex != -1 && SensorList.SelectedItem != null && !_onMenuSwitch)
            {
                UpdateSensor(sender, e);
                //OName.Text = Regex.Replace(OName.Text, @"[^0-9a-zA-Z_]+", "");
                SensorList.Items[SensorList.SelectedIndex] = Regex.Replace(OName.Text, @"[^0-9a-zA-Z_]+", "");
            }
        }

        private void RName_TextChanged(object sender, EventArgs e)
        {
            if (SensorList.SelectedIndex != -1 && SensorList.SelectedItem != null && !_onMenuSwitch)
            {
                UpdateSensor(sender, e);
                //RName.Text = Regex.Replace(RName.Text, @"[^0-9a-zA-Z_]+", "");
                SensorList.Items[SensorList.SelectedIndex] = Regex.Replace(RName.Text, @"[^0-9a-zA-Z_]+", "");
            }
        }

        private void AddSensor_Click(object sender, EventArgs e)
        {
            FCSensor newSensor = new FCSensor();
            bool exists = SensorList.Items.Contains("Sensor1");
            if (exists)
            {
                for (int i = 0; i < 99; i++)
                {
                    exists = SensorList.Items.Contains("Sensor" + i.ToString());
                    if (!exists)
                    {
                        newSensor.SensorName = "Sensor" + i.ToString();
                        break;
                    }
                }
            }
            else
            {
                newSensor.SensorName = "Sensor1";
            }
            RadarParams rParams = new RadarParams();
            rParams.MinEl = "0";
            rParams.MaxEl = "90";
            rParams.MinRange = "300";
            rParams.MaxRange = "40000";
            rParams.SolarExAngle = "10";
            rParams.HalfAngle = "85";
            rParams.MinAz = "0";
            rParams.MaxAz = "360";
            rParams.Az = "0";
            rParams.El = "90";
            newSensor.RParams = rParams;
            OpticalParams oParams = new OpticalParams();
            oParams.MinEl = "0";
            oParams.MaxEl = "90";
            oParams.MinRange = "4800";
            oParams.MaxRange = "90000";
            oParams.LunarExAngle = "10";
            oParams.SunElAngle = "-12";
            oParams.HalfAngle = "70";
            oParams.MinAz = "0";
            oParams.MaxAz = "360";
            oParams.Az = "0";
            oParams.El = "90";
            newSensor.OParams = oParams;

            sensors.Add(newSensor);
            SensorList.Items.Add(newSensor.SensorName);
            SensorList.SelectedIndex = SensorList.Items.Count - 1;
        }

        private void Duplicate_Click(object sender, EventArgs e)
        {
            if (SensorList.SelectedIndex != -1 && SensorList.SelectedItem != null)
            {
                FCSensor newSensor = new FCSensor(sensors[SensorList.SelectedIndex]);
                bool exists = SensorList.Items.Contains(newSensor.SensorName + "1");
                if (exists)
                {
                    for (int i = 0; i < 99; i++)
                    {
                        exists = SensorList.Items.Contains("Sensor" + i.ToString());
                        if (!exists)
                        {
                            newSensor.SensorName = newSensor.SensorName + i.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    newSensor.SensorName = newSensor.SensorName + "1";
                }
                sensors.Add(newSensor);
                SensorList.Items.Add(newSensor.SensorName);
                SensorList.SelectedIndex = SensorList.Items.Count - 1;
            }
        }

        private void DeleteSensor_Click(object sender, EventArgs e)
        {
            if (SensorList.SelectedIndex != -1 && SensorList.SelectedItem != null)
            {
                if (SensorList.Items.Count > 1)
                {
                    sensors.RemoveAt(SensorList.SelectedIndex);
                    SensorList.Items.RemoveAt(SensorList.SelectedIndex);
                    SensorList.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("There must be at least one sensor in the configuration");
                }
            }
        }

        private void UpdateSensor(object sender, EventArgs e)
        {
            if (SensorList.SelectedIndex != -1 && SensorList.SelectedItem != null && !_onMenuSwitch)
            {
                if (opt)
                {
                    sensors[SensorList.SelectedIndex].SensorName = Regex.Replace(OName.Text, @"[^0-9a-zA-Z_]+", "");
                    sensors[SensorList.SelectedIndex].OParams.MinRange = MinRange.Text;
                    sensors[SensorList.SelectedIndex].OParams.MaxRange = MaxRange.Text;
                    sensors[SensorList.SelectedIndex].OParams.MinEl = MinEl.Text;
                    sensors[SensorList.SelectedIndex].OParams.MaxEl = MaxEl.Text;
                    sensors[SensorList.SelectedIndex].OParams.LunarExAngle = LunExAngle.Text;
                    sensors[SensorList.SelectedIndex].OParams.SunElAngle = MaxSunEl.Text;
                    sensors[SensorList.SelectedIndex].OParams.HalfAngle = HalfAngle.Text;
                    sensors[SensorList.SelectedIndex].OParams.MinAz = MinAz.Text;
                    sensors[SensorList.SelectedIndex].OParams.MaxAz = MaxAz.Text;
                    sensors[SensorList.SelectedIndex].OParams.Az = PointingAz.Text;
                    sensors[SensorList.SelectedIndex].OParams.El = PointingEl.Text;
                }
                else
                {
                    sensors[SensorList.SelectedIndex].SensorName = Regex.Replace(RName.Text, @"[^0-9a-zA-Z_]+", "");
                    sensors[SensorList.SelectedIndex].RParams.MinRange = RMinRange.Text;
                    sensors[SensorList.SelectedIndex].RParams.MaxRange = RMaxRange.Text;
                    sensors[SensorList.SelectedIndex].RParams.MinEl = RMinEl.Text;
                    sensors[SensorList.SelectedIndex].RParams.MaxEl = RMaxEl.Text;
                    sensors[SensorList.SelectedIndex].RParams.SolarExAngle = SolarExAngle.Text;
                    sensors[SensorList.SelectedIndex].RParams.HalfAngle = RHalfAngle.Text;
                    sensors[SensorList.SelectedIndex].RParams.MinAz = RMinAz.Text;
                    sensors[SensorList.SelectedIndex].RParams.MaxAz = RMaxAz.Text;
                    sensors[SensorList.SelectedIndex].RParams.Az = RPointingAz.Text;
                    sensors[SensorList.SelectedIndex].RParams.El = RPointingEl.Text;
                }
            }
        }
    }
}

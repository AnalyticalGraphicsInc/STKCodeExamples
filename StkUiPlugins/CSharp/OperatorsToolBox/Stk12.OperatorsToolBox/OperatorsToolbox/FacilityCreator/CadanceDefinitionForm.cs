using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OperatorsToolbox.FacilityCreator
{
    public partial class CadanceDefinitionForm : Form
    {
        private List<FcFacility> CurFacilityList{ get; set; }
        bool _onStart = true;
        public CadanceDefinitionForm()
        {
            InitializeComponent();
            CurFacilityList = new List<FcFacility>();
            if (CommonData.CadenceEdit)
            {
                SensorCadance cadance = CreateDuplicateCadance(CommonData.Cadences[CommonData.CadenceSelected]);
                CurFacilityList = cadance.FacilityList;
                PopulateCadance();
                NumOptical.Text = cadance.NumOptical.ToString();
                NumRadar.Text = cadance.NumRadars.ToString();
                CadanceName.Text = cadance.Name;
                if (cadance.SaveToDatabase)
                {
                    SaveToDatabase.Checked = true;
                }
                else
                {
                    SaveToDatabase.Checked = false;
                }
            }
            else
            {
                NumOptical.Text = "0";
                NumRadar.Text = "0";
                CadanceName.Text = "MyCadence";
            }
            SensorType.Items.Add("Optical");
            SensorType.Items.Add("Radar");
            SensorType.SelectedIndex = 0;
            DefaultConstraints.Checked = true;
            DefineConstraints.Enabled = false;

            _onStart = false;
        }

        private void CadanceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem!=null && CadanceList.FocusedItem.Index!=-1)
            {
                FacilityName.Text = CurFacilityList[CadanceList.FocusedItem.Index].Name;
                Latitude.Text = CurFacilityList[CadanceList.FocusedItem.Index].Latitude;
                Longitude.Text = CurFacilityList[CadanceList.FocusedItem.Index].Longitude;
                Altitude.Text = CurFacilityList[CadanceList.FocusedItem.Index].Altitude;
                if (CurFacilityList[CadanceList.FocusedItem.Index].IsOpt)
                {
                    SensorType.SelectedIndex = 0;
                }
                else
                {
                    SensorType.SelectedIndex = 1;
                }
                if (CurFacilityList[CadanceList.FocusedItem.Index].UseDefaultCnst)
                {
                    DefaultConstraints.Checked = true;
                }
                else
                {
                    DefaultConstraints.Checked = false;
                }
            }
        }

        private void AddFacility_Click(object sender, EventArgs e)
        {
            FcFacility facility = new FcFacility();
            facility.Name = "New_Facility";
            facility.Latitude = "0";
            facility.Longitude = "0";
            facility.Altitude = "0";
            facility.Type = "Optical";
            facility.IsOpt = true;
            RadarParams rParams = new RadarParams();
            rParams.MinEl = "0";
            rParams.MaxEl = "90";
            rParams.MinRange = "1600";
            rParams.MaxRange = "40000";
            rParams.SolarExAngle = "10";
            rParams.HalfAngle = "85";
            facility.RParams = rParams;
            OpticalParams oParams = new OpticalParams();
            oParams.MinEl = "0";
            oParams.MaxEl = "90";
            oParams.MinRange = "4800";
            oParams.MaxRange = "90000";
            oParams.LunarExAngle = "10";
            oParams.SunElAngle = "-12";
            oParams.HalfAngle = "70";
            facility.OParams = oParams;
            facility.UseDefaultCnst = true;
            CurFacilityList.Add(facility);
            PopulateCadance();
            CadanceList.FocusedItem = CadanceList.Items[CadanceList.Items.Count - 1];
            PopulateNumSensors();
        }

        private void DeleteFacility_Click(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem!=null)
            {
                CurFacilityList.RemoveAt(CadanceList.FocusedItem.Index);
                CadanceList.Items.RemoveAt(CadanceList.FocusedItem.Index);
                PopulateNumSensors();
            }
        }

        private void Duplicate_Click(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null && CadanceList.FocusedItem.Index != -1)
            {
                FcFacility fac = new FcFacility(CurFacilityList[CadanceList.FocusedItem.Index]);
                CurFacilityList.Add(fac);
                PopulateCadance();
                CadanceList.FocusedItem = CadanceList.Items[CadanceList.Items.Count - 1];
                PopulateNumSensors();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Tuple<int, string> check = FieldCheck();
            if (check.Item1==0)
            {
                SensorCadance cadance = new SensorCadance();
                cadance.NumOptical = Int32.Parse(NumOptical.Text);
                cadance.NumRadars = Int32.Parse(NumRadar.Text);
                if (cadance.NumOptical>0&&cadance.NumRadars>0)
                {
                    cadance.Type = "Opt/Rad";
                }
                else if (cadance.NumOptical > 0)
                {
                    cadance.Type = "Optical";
                }
                else
                {
                    cadance.Type = "Radar";
                }
                string newName = CadanceName.Text.Replace(" ", "_");
                newName = Regex.Replace(newName, @"[^0-9a-zA-Z_]+", "");
                cadance.Name = newName;
                cadance.FacilityList = CurFacilityList;

                if (SaveToDatabase.Checked)
                {
                    cadance.SaveToDatabase = true;
                }
                else
                {
                    cadance.SaveToDatabase = false;
                }
                if (CommonData.CadenceEdit)
                {
                    CommonData.Cadences[CommonData.CadenceSelected] = cadance;
                }
                else
                {
                    CommonData.Cadences.Add(cadance);
                }
                CommonData.CadenceSaved = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(check.Item2);
            }
        }

        private void PopulateCadance()
        {
            CadanceList.Items.Clear();
            foreach (var item in CurFacilityList)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Text = item.Name;
                listItem.SubItems.Add(item.Type);
                listItem.SubItems.Add(item.Latitude);
                listItem.SubItems.Add(item.Longitude);
                listItem.SubItems.Add(item.Altitude);
                CadanceList.Items.Add(listItem);
            }


        }

        private void PopulateNumSensors()
        {
            int numOpt = 0;
            int numRad = 0;

            foreach (var item in CurFacilityList)
            {
                if (item.IsOpt)
                {
                    numOpt++;
                }
                else
                {
                    numRad++;
                }
            }
            NumOptical.Text = numOpt.ToString();
            NumRadar.Text = numRad.ToString();
        }

        private void SensorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SensorType.SelectedIndex!=-1 && CadanceList.FocusedItem!=null)
            {
                if (SensorType.SelectedIndex==0)
                {
                    CurFacilityList[CadanceList.FocusedItem.Index].IsOpt = true;
                    CurFacilityList[CadanceList.FocusedItem.Index].Type = "Optical";
                }
                else if (SensorType.SelectedIndex == 1)
                {
                    CurFacilityList[CadanceList.FocusedItem.Index].IsOpt = false;
                    CurFacilityList[CadanceList.FocusedItem.Index].Type = "Radar";
                }
                PopulateNumSensors();
                CadanceList.Items[CadanceList.FocusedItem.Index].SubItems[1].Text = CurFacilityList[CadanceList.FocusedItem.Index].Type;
            }
        }

        private void Altitude_TextChanged(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null)
            {
                CurFacilityList[CadanceList.FocusedItem.Index].Altitude = Altitude.Text;
                CadanceList.Items[CadanceList.FocusedItem.Index].SubItems[4].Text = Altitude.Text;
            }
        }

        private void Longitude_TextChanged(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null)
            {
                CurFacilityList[CadanceList.FocusedItem.Index].Longitude = Longitude.Text;
                CadanceList.Items[CadanceList.FocusedItem.Index].SubItems[3].Text = Longitude.Text;
            }
        }

        private void Latitude_TextChanged(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null)
            {
                CurFacilityList[CadanceList.FocusedItem.Index].Latitude = Latitude.Text;
                CadanceList.Items[CadanceList.FocusedItem.Index].SubItems[2].Text = Latitude.Text;
            }
        }

        private void FacilityName_TextChanged(object sender, EventArgs e)
        {
            if (CadanceList.FocusedItem != null)
            {
                string newName = FacilityName.Text.Replace(" ", "_");
                newName = Regex.Replace(newName, @"[^0-9a-zA-Z_]+", "");
                CurFacilityList[CadanceList.FocusedItem.Index].Name = newName;
                CadanceList.Items[CadanceList.FocusedItem.Index].SubItems[0].Text = newName;
            }
        }

        private Tuple<int,string> FieldCheck()
        {
            int check = 0;
            int curCheck = 0;
            string checkStr = "Input errors with the following facilities:\n";
            double temp = 0.0;
            bool isNumerical;
            foreach (var item in CurFacilityList)
            {
                curCheck = 0;
                isNumerical = Double.TryParse(item.Latitude, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    curCheck = 1;
                }
                isNumerical = Double.TryParse(item.Longitude, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    curCheck = 1;
                }
                isNumerical = Double.TryParse(item.Altitude, out temp);
                if (!isNumerical)
                {
                    check = 1;
                    curCheck = 1;
                }
                if (curCheck == 1)
                {
                    checkStr = checkStr + item.Name +"\n";
                }
            }
            return Tuple.Create(check,checkStr);
        }

        private void DefineConstraints_Click(object sender, EventArgs e)
        {
            if (SensorType.SelectedIndex==0)
            {
                if (CurFacilityList[CadanceList.FocusedItem.Index].OParams == null)
                {
                    OpticalParams oParams = new OpticalParams();
                    oParams.MinEl = "0";
                    oParams.MaxEl = "90";
                    oParams.MinRange = "4800";
                    oParams.MaxRange = "90000";
                    oParams.LunarExAngle = "10";
                    oParams.SunElAngle = "-12";
                    oParams.HalfAngle = "70";
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams = oParams;
                }
                ChangeConstraintsForm form = new ChangeConstraintsForm(CurFacilityList[CadanceList.FocusedItem.Index].OParams);
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams.HalfAngle = form.OParams.HalfAngle;
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams.LunarExAngle = form.OParams.LunarExAngle;
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams.MinEl = form.OParams.MinEl;
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams.MaxEl = form.OParams.MaxEl;
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams.MinRange = form.OParams.MinRange;
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams.MaxRange = form.OParams.MaxRange;
                    CurFacilityList[CadanceList.FocusedItem.Index].OParams.SunElAngle = form.OParams.SunElAngle;
                }
            }
            else if(SensorType.SelectedIndex==1)
            {
                if (CurFacilityList[CadanceList.FocusedItem.Index].RParams == null)
                {
                    RadarParams rParams = new RadarParams();
                    rParams.MinEl = "0";
                    rParams.MaxEl = "90";
                    rParams.MinRange = "1600";
                    rParams.MaxRange = "40000";
                    rParams.SolarExAngle = "10";
                    rParams.HalfAngle = "85";
                    CurFacilityList[CadanceList.FocusedItem.Index].RParams = rParams;
                }
                ChangeConstraintsForm form = new ChangeConstraintsForm(CurFacilityList[CadanceList.FocusedItem.Index].RParams);
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CurFacilityList[CadanceList.FocusedItem.Index].RParams.HalfAngle = form.RParams.HalfAngle;
                    CurFacilityList[CadanceList.FocusedItem.Index].RParams.SolarExAngle = form.RParams.SolarExAngle;
                    CurFacilityList[CadanceList.FocusedItem.Index].RParams.MinEl = form.RParams.MinEl;
                    CurFacilityList[CadanceList.FocusedItem.Index].RParams.MaxEl = form.RParams.MaxEl;
                    CurFacilityList[CadanceList.FocusedItem.Index].RParams.MinRange = form.RParams.MinRange;
                    CurFacilityList[CadanceList.FocusedItem.Index].RParams.MaxRange = form.RParams.MaxRange;
                }
            }

        }

        private void DefaultConstraints_CheckedChanged(object sender, EventArgs e)
        {
            if (!_onStart)
            {
                if (DefaultConstraints.Checked)
                {
                    DefineConstraints.Enabled = false;
                    CurFacilityList[CadanceList.FocusedItem.Index].UseDefaultCnst = true;
                }
                else
                {
                    DefineConstraints.Enabled = true;
                    CurFacilityList[CadanceList.FocusedItem.Index].UseDefaultCnst = false;
                }
            }
        }

        private SensorCadance CreateDuplicateCadance(SensorCadance og)
        {
            SensorCadance newCadance = new SensorCadance();
            newCadance.Name = og.Name;
            List<FcFacility> newlist = new List<FcFacility>(og.FacilityList);
            newCadance.FacilityList = newlist;
            newCadance.NumOptical = og.NumOptical;
            newCadance.NumRadars = og.NumRadars;
            newCadance.SaveToDatabase = og.SaveToDatabase;
            newCadance.Type = og.Type;
            return newCadance;
        }

    }
}

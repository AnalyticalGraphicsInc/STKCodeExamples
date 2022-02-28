using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;
using System.Collections.Specialized;
using AGI.STKUtil;


namespace Stk12.UiPlugin.Articulations
{
    public partial class EditArticForm : Form
    {
        public EditArticForm(int sectionNumber)
        {
            InitializeComponent();
            PopulatePossibleArtics();

            StartTimeValue.Text = CommonData.sectionList[sectionNumber].startTimeValue;
            DurationValue.Text = CommonData.sectionList[sectionNumber].durationValue;
            StartValue.Text = CommonData.sectionList[sectionNumber].startValue;
            EndValue.Text = CommonData.sectionList[sectionNumber].endValue;
            DBValue.Text = CommonData.sectionList[sectionNumber].deadbandValue;
            AcValue.Text = CommonData.sectionList[sectionNumber].accelValue;
            DCDeltaValue.Text = CommonData.sectionList[sectionNumber].dutyValue;
            DcValue.Text = CommonData.sectionList[sectionNumber].decelValue;
            Period.Text = CommonData.sectionList[sectionNumber].periodValue;
            sectionName.Text = CommonData.sectionList[sectionNumber].sectionName;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            CommonData.applied = true;
            Section current = CommonData.sectionList[CommonData.selectedArtic];
            //Section current = new Section();

            string articFullName = PossibleArtics.Text;
            string[] articNameSplit = articFullName.Split(new Char[] {'-'}, 2);
            CommonData.objectName = articNameSplit[0];
            CommonData.articName = articNameSplit[1];

            current.startTimeValue = StartTimeValue.Text;
            current.durationValue = DurationValue.Text;
            current.startValue = StartValue.Text;
            current.endValue = EndValue.Text;
            current.deadbandValue = DBValue.Text;
            current.accelValue = AcValue.Text;
            current.decelValue = DcValue.Text;
            current.dutyValue = DCDeltaValue.Text;
            current.periodValue = Period.Text;
            current.sectionName = sectionName.Text;
            current.articName = CommonData.articName;
            current.objectName = CommonData.objectName;

            string section = ArticFunctions.CreateSection(CommonData.objectName, CommonData.articName, current.startTimeValue, current.durationValue, current.startValue, current.endValue, current.deadbandValue, current.accelValue, current.decelValue, current.dutyValue, current.periodValue, current.sectionName);
            current.sectionText = section;
            CommonData.sectionList[CommonData.selectedArtic] = current;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartTimeValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void UseCurrentTime_Click(object sender, EventArgs e)
        {
            IAgAnimation animationRoot = (IAgAnimation)CommonData.StkRoot;
            double currentTime = animationRoot.CurrentTime;
            StartTimeValue.Text = currentTime.ToString();
        }

        private void PossibleArtics_SelectedIndexChanged(object sender, EventArgs e)
        {
            string articFullName = PossibleArtics.Text;
            string[] articNameSplit = articFullName.Split('-');
            CommonData.objectName = articNameSplit[0];
            CommonData.articName = articNameSplit[1];
        }
        private void PopulatePossibleArtics()
        {
            string objectPath = CommonData.objectClass + "/" + CommonData.simpleName;
            IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);
            Array names = ArticFunctions.GetArticulations(obj);
            foreach (var item in names)
            {
                if (item.ToString().Contains("Time"))
                {

                }
                else
                {
                    PossibleArtics.Items.Add(item);
                }
            }
            if (CommonData.selectedArtic != -1)
            {
                try
                {
                    int index = PossibleArtics.FindString(CommonData.sectionList[CommonData.selectedArtic].objectName + "-" + CommonData.sectionList[CommonData.selectedArtic].articName);
                    PossibleArtics.SelectedIndex = index;
                }
                catch (Exception)
                {

                }
            }


        }
    }
}

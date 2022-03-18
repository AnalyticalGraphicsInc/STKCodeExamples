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
    public partial class AddArticForm : Form
    {
        public string startTimeValue;
        public string durationValue;
        public string startValue;
        public string endValue;
        public string deadbandValue;
        public string accelValue;
        public string decelValue;
        public string dutyValue;
        public string periodValue;


        public AddArticForm()
        {
            InitializeComponent();
            PopulatePossibleArtics();
        }

        private void StartTimeValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartTimeText_Click(object sender, EventArgs e)
        {

        }

        private void DurationValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void EndValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void DBValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void AcValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void DcValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void DCDeltaValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void Period_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CommonData.added = true;
            Section current = new Section();
            current.isLinked = false;
            current.sectionNumber = CommonData.totalSectionCount + 1;
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

            string section= ArticFunctions.CreateSection(CommonData.objectName,CommonData.articName, current.startTimeValue, current.durationValue, current.startValue, current.endValue, current.deadbandValue, current.accelValue, current.decelValue, current.dutyValue, current.periodValue,current.sectionName);
            current.sectionText = section;
            CommonData.sectionList.Add(current);
            this.Close();


        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sectionName_TextChanged(object sender, EventArgs e)
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
                    int index = PossibleArtics.FindString(CommonData.objectName + "-" + CommonData.articName);
                    PossibleArtics.SelectedIndex = index;
                }
                catch (Exception)
                {

                }
            }


        }
    }
}

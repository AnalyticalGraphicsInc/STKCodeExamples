using System;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;

namespace OperatorsToolbox.VolumeCreator
{
    public partial class FacilitySelectForm : Form
    {
        public FacilitySelectForm()
        {
            InitializeComponent();
            PopulateFacilities();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Select_Click(object sender, EventArgs e)
        {
            if (FacilityList.Items.Count==0)
            {
                MessageBox.Show("Please create a facility prior to selection");
                this.Close();
            }
            else if (FacilityList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a facility");
            }
            else
            {
                CommonData.FacilitySelected = true;
                IAgFacility facility = CommonData.StkRoot.GetObjectFromPath("Facility/" + FacilityList.SelectedItem.ToString()) as IAgFacility;
                IAgStkObject facilityObj = facility as IAgStkObject;
                CommonData.FacilityName = facilityObj.InstanceName;
                Array position = facility.Position.QueryPlanetodeticArray();
                CommonData.FacilityLat = position.GetValue(0).ToString();
                CommonData.FacilityLong = position.GetValue(1).ToString();
                this.Close();
            }
        }

        private void PopulateFacilities()
        {
            IAgExecCmdResult result;
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Facility");
            if (result[0] != "None")
            {
                string[] constArray = result[0].Split(null);
                foreach (var item in constArray)
                {
                    string newItem = item.Split('/').Last();
                    if (newItem != "" && newItem != null)
                    {
                        FacilityList.Items.Add(newItem);
                    }
                }
            }

        }

        private void FacilitySelectForm_Load(object sender, EventArgs e)
        {

        }
    }
}

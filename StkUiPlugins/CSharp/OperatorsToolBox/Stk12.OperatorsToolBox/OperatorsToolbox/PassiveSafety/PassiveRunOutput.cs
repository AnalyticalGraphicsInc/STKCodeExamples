using System;
using System.Drawing;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKObjects.Astrogator;
using AGI.STKVgt;

namespace OperatorsToolbox.PassiveSafety
{
    public partial class PassiveRunOutput : Form
    {
        public PassiveRunOutput()
        {
            InitializeComponent();
            PopulateTable();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PopulateTable()
        {
            int count = 1;
            foreach (var item in CommonData.RunList)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.SubItems[0].Text = count.ToString();
                listItem.SubItems.Add(item.ManeuverTime.ToString());
                listItem.SubItems.Add(item.MinRange.ToString());
                listItem.SubItems.Add(item.MinRadial.ToString());
                listItem.SubItems.Add(item.MinIntrack.ToString());
                listItem.SubItems.Add(item.MinCrosstrack.ToString());

                if (item.Safe)
                {
                    listItem.BackColor = Color.LimeGreen;
                }
                else
                {
                    listItem.BackColor = Color.Red;
                }

                listView1.Items.Add(listItem);
                count++;
            }

        }

        private void DisplaySelected_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null && listView1.FocusedItem.Index != -1)
            {
                if (CommonData.HasManeuvers)
                {
                    IAgStkObject passiveSatObj = CreatorFunctions.GetCreateSatellite("PassiveCheck");
                    IAgSatellite passiveSat = passiveSatObj as IAgSatellite;
                    passiveSat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
                    passiveSat.VO.OrbitSystems.Add("Satellite/" + CommonData.TargetName + " VVLH System");
                    passiveSat.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator);
                    IAgVADriverMCS passiveDriver = passiveSat.Propagator as IAgVADriverMCS;

                    IAgVAMCSInitialState intState = passiveDriver.MainSequence[0] as IAgVAMCSInitialState;
                    IAgVAMCSPropagate prop = passiveDriver.MainSequence[1] as IAgVAMCSPropagate;
                    IAgVAStoppingConditionElement sc1 = prop.StoppingConditions[0];
                    IAgVAStoppingCondition sc = sc1.Properties as IAgVAStoppingCondition;
                    sc.Trip = CommonData.RunList[listView1.FocusedItem.Index].PropTime;

                    AgVAElementCartesian element = intState.Element as AgVAElementCartesian;
                    intState.OrbitEpoch = CommonData.RunList[listView1.FocusedItem.Index].ManeuverTime;
                    element.Vx = CommonData.RunList[listView1.FocusedItem.Index].Vx;
                    element.Vy = CommonData.RunList[listView1.FocusedItem.Index].Vy;
                    element.Vz = CommonData.RunList[listView1.FocusedItem.Index].Vz;
                    element.X = CommonData.RunList[listView1.FocusedItem.Index].X;
                    element.Y = CommonData.RunList[listView1.FocusedItem.Index].Y;
                    element.Z = CommonData.RunList[listView1.FocusedItem.Index].Z;

                    passiveDriver.RunMCS();
                }
                else
                {
                    MessageBox.Show("Actor satellite has no maneuvers. Visualization is simply the Actor's current trjectory");
                }
            }
        }

        private void ProxGeometry_CheckedChanged(object sender, EventArgs e)
        {
            IAgSatellite sat = CommonData.StkRoot.GetObjectFromPath("Satellite/" + CommonData.TargetName) as IAgSatellite;
            IAgStkObject satObj = sat as IAgStkObject;
            if (ProxGeometry.Checked)
            {
                if (CommonData.RunList[0].IsSpherical)
                {
                    sat.VO.Proximity.Ellipsoid.IsVisible = true;
                    IAgCrdnAxes axes = satObj.Vgt.Axes["RIC"];
                    sat.VO.Proximity.Ellipsoid.ReferenceFrame = axes;
                    sat.VO.Proximity.Ellipsoid.XSemiAxisLength = CommonData.RunList[0].UserMinRange;
                    sat.VO.Proximity.Ellipsoid.YSemiAxisLength = CommonData.RunList[0].UserMinRange;
                    sat.VO.Proximity.Ellipsoid.ZSemiAxisLength = CommonData.RunList[0].UserMinRange;
                }
                else
                {
                    sat.VO.Proximity.ControlBox.IsVisible = true;
                    IAgCrdnAxes axes = satObj.Vgt.Axes["RIC"];
                    sat.VO.Proximity.ControlBox.ReferenceFrame = axes;
                    sat.VO.Proximity.ControlBox.XAxisLength = CommonData.RunList[0].UserMinR;
                    sat.VO.Proximity.ControlBox.YAxisLength = CommonData.RunList[0].UserMinI;
                    sat.VO.Proximity.ControlBox.ZAxisLength = CommonData.RunList[0].UserMinC;
                }
            }
            else
            {
                sat.VO.Proximity.Ellipsoid.IsVisible = false;
                sat.VO.Proximity.ControlBox.IsVisible = false;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

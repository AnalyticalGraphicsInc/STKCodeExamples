using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

namespace OperatorsToolbox.PlaneCrossingUtility
{
    public partial class PlaneCrossingPlugin : OpsPluginControl
    {
        public List<string> crossingObjects;
        private bool compComplete = false;
        public List<PlaneCrossingGroup> AllPlaneCrossingsList;
        public PlaneCrossingPlugin()
        {
            InitializeComponent();
            crossingObjects = new List<string>();
            AllPlaneCrossingsList = new List<PlaneCrossingGroup>();
            PopulatePlaneReferences();
            PopulateClasses();
            SetDefaults();
        }

        private void PopulateClasses()
        {
            ObjectClass.Items.Clear();
            ObjectClass.Items.Add("Satellite");
            ObjectClass.Items.Add("Missile");
            ObjectClass.Items.Add("LaunchVehicle");
            ObjectClass.Items.Add("Facility");
            ObjectClass.Items.Add("Place");
            ObjectClass.Items.Add("Target");
            ObjectClass.SelectedIndex = 0;
        }

        private void PopulatePlaneReferences()
        {
            CreatorFunctions.PopulateCbByClass(PlaneSatellite, "Satellite");
            if (PlaneSatellite.Items.Count > 0)
            {
                PlaneSatellite.SelectedIndex = 0;
            }
        }

        private void PopulateCrossingObjects(string classType)
        {
            CreatorFunctions.PopulateObjectListByClass(CrossingObjectsList, classType);
            //Remove plane reference satellite from list if required
            foreach (ListViewItem item in CrossingObjectsList.Items)
            {
                if (item.Text == PlaneSatellite.Text)
                {
                    CrossingObjectsList.Items.Remove(item);
                }
            }
        }

        private void SetDefaults()
        {
            ConditionalCrossing.Checked = false;
            ConditionalLB.Text = "-1";
            ConditionalUB.Text = "1";
            ConditionalLB.Enabled = false;
            ConditionalUB.Enabled = false;
            //OnOffSat.Enabled = false;
            //OffOnSat.Enabled = false;
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            int check = FieldCheck();
            if (check == 0)
            {
                compComplete = false;
                AllPlaneCrossingsList.Clear();
                IAgCrdnPlaneNormal orbitPlane;
                IAgStkObject crossObj;
                IAgCrdnProvider crossVgt;
                IAgStkObject satRefObj = CreatorFunctions.GetCreateSatellite(PlaneSatellite.Text);
                IAgSatellite satRef = (IAgSatellite) satRefObj;
                IAgCrdnProvider vgtPrv = satRefObj.Vgt;

                //Create reference plane
                string planeName = PlaneSatellite.Text + "_OrbitPlane";
                orbitPlane = AWBFunctions.GetCreatePlaneNormal(vgtPrv, vgtPrv.Points["Center"], vgtPrv.Vectors["Velocity"], vgtPrv.Vectors["Orbit_Normal"], planeName, "Orbit plane referencing orbit normal");

                //Loop for each crossing object. Create angle, calc scalar, extrema time array, and conditionals if required 
                string angleName;
                string extremaName;
                string conditionName;
                string cScalName;
                string condEventArrayName;
                foreach (var cObject in crossingObjects)
                {
                    //Initialize new plane crossing group
                    PlaneCrossingGroup crossingGroup = new PlaneCrossingGroup();
                    IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
                    crossingGroup.AnalysisStartTime = scenario.StartTime;
                    crossingGroup.AnalysisStopTime = scenario.StopTime;
                    crossingGroup.PlaneReferenceObjectName = satRefObj.InstanceName;
                    crossingGroup.CrossingObjectName = cObject;

                    //Compute required VGT component names
                    crossObj = CommonData.StkRoot.GetObjectFromPath(ObjectClass.Text + "/" + cObject);
                    crossVgt = crossObj.Vgt;
                    angleName = crossObj.InstanceName + "x" + satRefObj.InstanceName + "_Angle";
                    cScalName = angleName;
                    extremaName = crossObj.InstanceName + "x" + satRefObj.InstanceName + "_CrossingTimes";
                    conditionName = crossObj.InstanceName + "x" + satRefObj.InstanceName + "_CrossingBounds";
                    condEventArrayName = crossObj.InstanceName + "x" + satRefObj.InstanceName + "_BoundedCrossingTimes";

                    //Create angle from current crossing object position to reference plane
                    IAgCrdnAngleToPlane planeAngle = AWBFunctions.GetCreateAngleToPlane(crossVgt, vgtPrv.Planes[planeName], crossVgt.Vectors["Position"], angleName, "Angle from satellite position to" + satRefObj.InstanceName + "orbit plane");

                    //Create calc scalar of angle
                    IAgCrdnCalcScalarAngle calcScalarAngle = AWBFunctions.GetCreateAngleCalcScalar(crossVgt, (IAgCrdnAngle)planeAngle, cScalName);

                    //Create Extrema time array to find exact crossing times
                    IAgCrdnEventArrayExtrema extrema = AWBFunctions.GetCreateEventArrayExtrema(crossVgt, (IAgCrdnCalcScalar)calcScalarAngle, extremaName, AgECrdnExtremumConstants.eCrdnExtremumMinimum, false);

                    //If conditional then create condition and condition crossing event
                    IAgCrdnEventArrayConditionCrossings conditionCrossings = null;
                    if (ConditionalCrossing.Checked)
                    {
                        //create condition
                        IAgCrdnConditionScalarBounds condition = AWBFunctions.GetCreateConditionScalarBounds(crossVgt, (IAgCrdnCalcScalar)calcScalarAngle, conditionName, AgECrdnConditionThresholdOption.eCrdnConditionThresholdOptionInsideMinMax);
                        AWBFunctions.SetAngleConditionScalarBounds(condition, Double.Parse(ConditionalLB.Text), Double.Parse(ConditionalUB.Text));

                        //Create condition crossing event
                        if (crossVgt.EventArrays.Contains(condEventArrayName))
                        {
                            conditionCrossings = (IAgCrdnEventArrayConditionCrossings)crossVgt.EventArrays[condEventArrayName];
                        }
                        else
                        {
                            conditionCrossings = (IAgCrdnEventArrayConditionCrossings)crossVgt.EventArrays.Factory.CreateEventArrayConditionCrossings(condEventArrayName,"");   
                        }
                        conditionCrossings.Condition = condition as IAgCrdnCondition;
                        conditionCrossings.SatisfactionCrossing =
                                AgECrdnSatisfactionCrossing.eCrdnSatisfactionCrossingNone;
                        //if (OffOnSat.Checked && OnOffSat.Checked)
                        //{
                        //    conditionCrossings.SatisfactionCrossing =
                        //        AgECrdnSatisfactionCrossing.eCrdnSatisfactionCrossingNone;
                        //}
                        //else if (OffOnSat.Checked && !OnOffSat.Checked)
                        //{
                        //    conditionCrossings.SatisfactionCrossing =
                        //        AgECrdnSatisfactionCrossing.eCrdnSatisfactionCrossingIn;
                        //}
                        //else if (!OffOnSat.Checked && OnOffSat.Checked)
                        //{
                        //    conditionCrossings.SatisfactionCrossing =
                        //        AgECrdnSatisfactionCrossing.eCrdnSatisfactionCrossingOut;
                        //}
                    }
                    //Extract Data
                    IAgCrdnEventArray extremaArray = (IAgCrdnEventArray) extrema;
                    IAgCrdnFindTimesResult extremaTimes = extremaArray.FindTimes();
                    IAgCrdnEventArray boundedArray = null;
                    IAgCrdnFindTimesResult boundedTimes = null;
                    if (extremaTimes.IsValid)
                    {
                        int numTimes = extremaTimes.Times.Length;
                        if (ConditionalCrossing.Checked)
                        {
                            boundedArray = conditionCrossings as IAgCrdnEventArray;
                            boundedTimes = boundedArray.FindTimes();
                            if (boundedTimes.Times.Length != 2 * numTimes)
                            {
                                MessageBox.Show("A bounded crossing time may be outside the analysis interval. Check results.");
                            }
                        }
                        PlaneCrossing crossing;
                        for (int i = 0; i < numTimes; i++)
                        {
                            crossing = new PlaneCrossing();
                            crossing.CrossingTime = extremaTimes.Times.GetValue(i).ToString();
                            if (ConditionalCrossing.Checked)
                            {
                                crossing.IsBounded = true;
                                crossing.LowerBound = Double.Parse(ConditionalLB.Text);
                                crossing.UpperBound = Double.Parse(ConditionalUB.Text);

                                if (boundedTimes.Times.Length != 2 * numTimes)
                                {
                                    try
                                    {
                                        crossing.LowerBoundCrossingTime = boundedTimes.Times.GetValue(2 * i).ToString();
                                        crossing.UpperBoundCrossingTime = boundedTimes.Times.GetValue(2 * i + 1).ToString();
                                    }
                                    catch (Exception) //Index out of range
                                    {
                                        crossing.LowerBoundCrossingTime = "";
                                        crossing.UpperBoundCrossingTime = "";
                                    }
                                }
                                else
                                {
                                    crossing.LowerBoundCrossingTime = boundedTimes.Times.GetValue(2 * i).ToString();
                                    crossing.UpperBoundCrossingTime = boundedTimes.Times.GetValue(2 * i + 1).ToString();
                                }
                            }
                            crossingGroup.PlaneCrossings.Add(crossing);
                        }
                    }
                    AllPlaneCrossingsList.Add(crossingGroup);
                    compComplete = true;

                    //Add components to timeline view
                    if (AddToTimeline.Checked)
                    {
                        string cmd1 = null;
                        string cmd2 = null;
                        if (ConditionalCrossing.Checked)
                        {
                            cmd1 = "Timeline * TimeComponent Remove ContentView \"Scenario Availability\"" + " \"" + ObjectClass.Text + "/" + crossObj.InstanceName + " " + condEventArrayName + " Time Array\"";
                            cmd2 = "Timeline * TimeComponent Add ContentView \"Scenario Availability\" DisplayName \"" + condEventArrayName + "\"" + " \"" + ObjectClass.Text + "/" + crossObj.InstanceName + " " + condEventArrayName + " Time Array\"";
                        }
                        else
                        {
                            cmd1 = "Timeline * TimeComponent Remove ContentView \"Scenario Availability\"" + " \"" + ObjectClass.Text + "/" + crossObj.InstanceName + " " + extremaName + " Time Array\"";
                            cmd2 = "Timeline * TimeComponent Add ContentView \"Scenario Availability\" DisplayName \"" + extremaName + "\"" + " \"" + ObjectClass.Text + "/" + crossObj.InstanceName + " " + extremaName + " Time Array\"";
                        }

                        try
                        {
                            CommonData.StkRoot.ExecuteCommand(cmd1);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            CommonData.StkRoot.ExecuteCommand(cmd2);
                            CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");
                        }
                        catch (Exception exception)
                        {
                            //likely no timeline exists
                        }
                    }
                }

                //Export options
                if (compComplete && ExportToTxt.Checked)
                {
                    ReadWrite.WritePlaneCrossingOutput(satRefObj.InstanceName, AllPlaneCrossingsList);
                }
                MessageBox.Show("Computation Complete!");
            }

        }

        private int FieldCheck()
        {
            int check = 0;
            bool isNumeric;
            double LBtemp;
            double UBtemp;
            if (String.IsNullOrEmpty(PlaneSatellite.Text))
            {
                check = 1;
                MessageBox.Show("Plane reference not a valid selection");
            }

            if (crossingObjects.Count == 0)
            {
                check = 1;
                MessageBox.Show("No crossing objects selected");
            }

            isNumeric = Double.TryParse(ConditionalLB.Text, out LBtemp);
            if (!isNumeric)
            {
                check = 1;
                MessageBox.Show("Conditional Lower Bound not a valid number");
            }
            else
            {
                isNumeric = Double.TryParse(ConditionalUB.Text, out UBtemp);
                if (!isNumeric)
                {
                    check = 1;
                    MessageBox.Show("Conditional Upper Bound not a valid number");
                }
                else
                {
                    if (LBtemp == UBtemp)
                    {
                        check = 1;
                        MessageBox.Show("Conditional Upper Bound and Upper Bound cannot be the same value");
                    }
                }
            }

            //if (ConditionalCrossing.Checked && !OnOffSat.Checked && !OffOnSat.Checked)
            //{
            //    check = 1;
            //    MessageBox.Show("At least one satisfaction type must be enabled");
            //}

            return check;
        }
        private void ShowTimes_Click(object sender, EventArgs e)
        {
            if (compComplete)
            {
                CrossingOutputForm form = new CrossingOutputForm(AllPlaneCrossingsList);
                form.Show();
            }
        }

        private void AddObject_Click(object sender, EventArgs e)
        {
            if (CrossingObjectsList.FocusedItem != null)
            {
                foreach (int index in CrossingObjectsList.SelectedIndices)
                {
                    CrossingObjectsList.Items[index].Font = new Font(CrossingObjectsList.Items[index].Font, FontStyle.Bold);
                    if (!crossingObjects.Contains(CrossingObjectsList.Items[index].SubItems[0].Text))
                    {
                        crossingObjects.Add(CrossingObjectsList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void RemoveObject_Click(object sender, EventArgs e)
        {
            if (CrossingObjectsList.FocusedItem != null)
            {
                foreach (int index in CrossingObjectsList.SelectedIndices)
                {
                    CrossingObjectsList.Items[index].Font = new Font(CrossingObjectsList.Items[index].Font, FontStyle.Regular);
                    if (crossingObjects.Contains(CrossingObjectsList.Items[index].SubItems[0].Text))
                    {
                        crossingObjects.Remove(CrossingObjectsList.Items[index].SubItems[0].Text);
                    }
                }
            }
        }

        private void ObjectClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ObjectClass.Text))
            {
                CrossingObjectsList.Items.Clear();
                crossingObjects.Clear();
                PopulateCrossingObjects(ObjectClass.Text);
            }
        }

        private void ConditionalCrossing_CheckedChanged(object sender, EventArgs e)
        {
            if (ConditionalCrossing.Checked)
            {
                ConditionalLB.Enabled = true;
                ConditionalUB.Enabled = true;
                //OnOffSat.Enabled = true;
                //OffOnSat.Enabled = true;
            }
            else
            {
                ConditionalLB.Enabled = false;
                ConditionalUB.Enabled = false;
                //OnOffSat.Enabled = false;
                //OffOnSat.Enabled = false;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void PlaneSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(PlaneSatellite.Text))
            {
                CrossingObjectsList.Items.Clear();
                crossingObjects.Clear();
                PopulateCrossingObjects(ObjectClass.Text);
            }
        }
    }
}

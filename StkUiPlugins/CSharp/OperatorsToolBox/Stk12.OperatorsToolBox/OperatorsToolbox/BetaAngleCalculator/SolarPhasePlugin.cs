using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

namespace OperatorsToolbox.BetaAngleCalculator
{
    public partial class SolarPhasePlugin : OpsPluginControl
    {
        public SolarPhasePlugin()
        {
            InitializeComponent();
            CommonData.SelectedObservers = new List<string>();
            CommonData.SelectedTargets = new List<string>();
            PopulateObserverType();
            ObserverType.SelectedIndex = 0;
            PopulateTargetType();
            TargetType.SelectedIndex = 0;
            PopulateObservers();
            PopulateTargets();
            EnableConstraint.Checked = false;

            AngleType.Items.Add("Beta Angle");
            AngleType.Items.Add("Solar Phase/CATS");
            AngleType.SelectedIndex = 0;
            ConstraintMin.Text = "0";
            ConstraintMax.Text = "180";
        }

        private void ObserverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateObservers();
        }

        private void SelectObserver_Click(object sender, EventArgs e)
        {
            foreach (int index in ObserversList.SelectedIndices)
            {
                var listItem = new ListViewItem();
                ObserversList.Items[index].Font = new Font(ObserversList.Items[index].Font, FontStyle.Bold);
                if (!CommonData.SelectedObservers.Any(p => p == ObserversList.Items[index].Text))
                {
                    CommonData.SelectedObservers.Add(ObserversList.Items[index].Text);
                }
            }
        }

        private void UnselectObserver_Click(object sender, EventArgs e)
        {
            foreach (int index in ObserversList.SelectedIndices)
            {
                var listItem = new ListViewItem();
                ObserversList.Items[index].Font = new Font(ObserversList.Items[index].Font, FontStyle.Regular);
                if (CommonData.SelectedObservers.Any(p => p == ObserversList.Items[index].Text))
                {
                    CommonData.SelectedObservers.Remove(ObserversList.Items[index].Text);
                }
            }
        }

        private void SelectTarget_Click(object sender, EventArgs e)
        {
            foreach (int index in TargetsList.SelectedIndices)
            {
                var listItem = new ListViewItem();
                TargetsList.Items[index].Font = new Font(TargetsList.Items[index].Font, FontStyle.Bold);
                if (!CommonData.SelectedTargets.Any(p => p == TargetsList.Items[index].Text))
                {
                    CommonData.SelectedTargets.Add(TargetsList.Items[index].Text);
                }
            }
        }

        private void UnselectTarget_Click(object sender, EventArgs e)
        {
            foreach (int index in TargetsList.SelectedIndices)
            {
                var listItem = new ListViewItem();
                TargetsList.Items[index].Font = new Font(TargetsList.Items[index].Font, FontStyle.Regular);
                if (CommonData.SelectedTargets.Any(p => p == TargetsList.Items[index].Text))
                {
                    CommonData.SelectedTargets.Remove(TargetsList.Items[index].Text);
                }
            }
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            int check = FieldCheck();
            if (check == 0)
            {
                IAgStkObject obj = null;
                IAgStkObject tar = null;
                foreach (var observer in CommonData.SelectedObservers)
                {
                    foreach (var target in CommonData.SelectedTargets)
                    {
                        if (observer != target)
                        {
                            if (TargetType.SelectedIndex == 0)
                            {
                                tar = CommonData.StkRoot.GetObjectFromPath("Satellite/" + target);
                            }
                            else if (TargetType.SelectedIndex == 1)
                            {
                                tar = CommonData.StkRoot.GetObjectFromPath("Missile/" + target);
                            }
                            else if (TargetType.SelectedIndex == 2)
                            {
                                tar = CommonData.StkRoot.GetObjectFromPath("Aircraft/" + target);
                            }

                            if (ObserverType.SelectedIndex == 0)
                            {
                                obj = CommonData.StkRoot.GetObjectFromPath("Satellite/" + observer);
                            }
                            else if (ObserverType.SelectedIndex == 1)
                            {
                                obj = CommonData.StkRoot.GetObjectFromPath("Facility/" + observer);
                            }
                            else
                            {
                                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Sensor");
                                if (result[0] != "None")
                                {
                                    string[] sensorArray = result[0].Split(null);
                                    foreach (var item in sensorArray)
                                    {
                                        if (item.Contains(observer))
                                        {
                                            int scenarioPos = item.LastIndexOf("/Scenario/");
                                            int scenarioNameSlashPos = item.IndexOf("/", scenarioPos + 10);
                                            item.Remove(0, scenarioNameSlashPos);
                                            obj = CommonData.StkRoot.GetObjectFromPath(item);
                                        }
                                    }
                                }
                            }
                            IAgCrdnProvider objVgtPrv = obj.Vgt;
                            IAgCrdnProvider tarVgt = tar.Vgt;
                            IAgCrdnAngleBetweenVectors angle = null;
                            IAgCrdnVectorDisplacement dispVector = AWBFunctions.GetCreateDisplacementVector(objVgtPrv, objVgtPrv.Points["Center"], tarVgt.Points["Center"], "To_" + target);

                            //Create either solar phase or beta angle depending on user specified selection
                            string angleName = null;
                            if (AngleType.SelectedIndex == 0)
                            {
                                angleName = "To_" + target + "BetaAngle";
                                angle = AWBFunctions.GetCreateAngleBetweenVectors(objVgtPrv, dispVector as IAgCrdnVector, objVgtPrv.Vectors["Sun"], angleName, "");
                            }
                            else if (AngleType.SelectedIndex == 1)
                            {
                                angleName = "To_" + target + "SolarPhaseAngle";
                                angle = AWBFunctions.GetCreateAngleBetweenVectors(objVgtPrv, dispVector as IAgCrdnVector, tarVgt.Vectors["Sun"], angleName, "");
                            }
                            //Create Calc Scalar of angle
                            IAgCrdnCalcScalarAngle cScalar = AWBFunctions.GetCreateAngleCalcScalar(objVgtPrv, angle as IAgCrdnAngle, angleName);

                            //If a conditional is requested, then create it here
                            if (EnableConstraint.Checked)
                            {
                                IAgCrdnConditionScalarBounds condition = AWBFunctions.GetCreateConditionScalarBounds(objVgtPrv, cScalar as IAgCrdnCalcScalar, angleName + "_Condition", AgECrdnConditionThresholdOption.eCrdnConditionThresholdOptionInsideMinMax);
                                AWBFunctions.SetAngleConditionScalarBounds(condition, Double.Parse(ConstraintMin.Text), Double.Parse(ConstraintMax.Text));
                            }
                        }
                    }
                }
                MessageBox.Show("Component Creation Completed");
            }
        }

        private int FieldCheck()
        {
            int check = 0;
            double tempmin = 0.0;
            double tempmax = 0.0;
            bool isNumeric = Double.TryParse(ConstraintMin.Text, out tempmin);
            if (!isNumeric)
            {
                check = 1;
                MessageBox.Show("Condition minimum not a valid number");
            }
            isNumeric = Double.TryParse(ConstraintMin.Text, out tempmax);
            if (!isNumeric)
            {
                check = 1;
                MessageBox.Show("Condition maximum not a valid number");
            }
            if (tempmin > tempmax)
            {
                check = 1;
                MessageBox.Show("Conditional minimum cannot be above the maximum");
            }
            if (tempmin < 0 || tempmin > 180 || tempmax < 0 || tempmax > 180)
            {
                check = 1;
                MessageBox.Show("Conditional bounds must be between 0-180 deg");
            }
            return check;
        }

        private void PopulateObservers()
        {
            ObserversList.Items.Clear();
            CommonData.SelectedObservers.Clear();
            if (ObserverType.SelectedIndex == 0)
            {
                CreatorFunctions.PopulateObjectListByClass(ObserversList, "Satellite");
            }
            else if (ObserverType.SelectedIndex == 1)
            {
                CreatorFunctions.PopulateObjectListByClass(ObserversList, "Facility");
            }
            else if (ObserverType.SelectedIndex == 2)
            {
                CreatorFunctions.PopulateObjectListByClass(ObserversList, "Sensor");
            }
        }

        private void PopulateTargets()
        {
            TargetsList.Items.Clear();
            CommonData.SelectedTargets.Clear();
            if (TargetType.SelectedIndex==0)
            {
                CreatorFunctions.PopulateObjectListByClass(TargetsList, "Satellite");
            }
            else if (TargetType.SelectedIndex == 1)
            {
                CreatorFunctions.PopulateObjectListByClass(TargetsList, "Missile");
            }
            else if (TargetType.SelectedIndex == 2)
            {
                CreatorFunctions.PopulateObjectListByClass(TargetsList, "Aircraft");
            }
        }

        private void PopulateObserverType()
        {
            ObserverType.Items.Add("Satellite");
            ObserverType.Items.Add("Facility");
            ObserverType.Items.Add("Sensor");
        }

        private void PopulateTargetType()
        {
            TargetType.Items.Add("Satellite");
            TargetType.Items.Add("Missile");
            TargetType.Items.Add("Aircraft");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        private void TargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateTargets();
        }

        private void EnableConstraint_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableConstraint.Checked)
            {
                ConstraintOptions.Enabled = true;
            }
            else
            {
                ConstraintOptions.Enabled = false;
            }
        }
    }
}

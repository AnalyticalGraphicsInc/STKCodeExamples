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
            IAgStkObject obj = null;
            IAgStkObject tar = null;
            foreach (var observer in CommonData.SelectedObservers)
            {
                foreach (var target in CommonData.SelectedTargets)
                {
                    if (observer != target)
                    {
                        if (TargetType.SelectedIndex==0)
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
                        IAgCrdnVectorDisplacement dispVector;
                        if (objVgtPrv.Vectors.Contains("To_" + target))
                        {
                            dispVector = objVgtPrv.Vectors["To_" + target] as IAgCrdnVectorDisplacement;
                        }
                        else
                        {
                            dispVector = objVgtPrv.Vectors.Factory.CreateDisplacementVector("To_" + target, objVgtPrv.Points["Center"], tarVgt.Points["Center"]);
                        }
                        if (!objVgtPrv.Angles.Contains("To_" + target + "SolarPhaseAngle"))
                        {
                            IAgCrdnAngleBetweenVectors angle = (IAgCrdnAngleBetweenVectors)objVgtPrv.Angles.Factory.Create("To_" + target + "SolarPhaseAngle", "", AgECrdnAngleType.eCrdnAngleTypeBetweenVectors);
                            angle.FromVector.SetVector(dispVector as IAgCrdnVector);
                            angle.ToVector.SetVector(objVgtPrv.Vectors["Sun"]);
                        }
                    }
                }
            }
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
    }
}

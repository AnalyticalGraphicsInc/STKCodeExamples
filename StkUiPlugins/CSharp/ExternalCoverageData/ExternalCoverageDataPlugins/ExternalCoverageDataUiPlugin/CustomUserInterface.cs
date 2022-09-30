using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;
using System.IO;

namespace LoadExternalCoverageData
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private LoadExternalCoverageDataPlugin m_uiPlugin;

        public CustomUserInterface()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Members

        public stdole.IPictureDisp GetIcon()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnClosing()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnSaveModified()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as LoadExternalCoverageDataPlugin;
            m_root = m_uiPlugin.STKRoot;


        }

        #endregion



        private void Button1_Click(object sender, EventArgs e)
        {
            /// check if a scenario exists
            if (m_root.CurrentScenario == null)
            {
                MessageBox.Show("please open a scenario first");
                return;
            }


            /// check if selected external data file exists
            if (!File.Exists(externalDataFilePathTextBox.Text))
            {
                MessageBox.Show("The selected external data file does not exist");
                return;
            }



            /// create and assign .pnt file
            if (createPntFileCheckBox.Checked)
            {
                if (File.Exists(externalDataFilePathTextBox.Text))
                {
                    CreatePointFile.Program.CreatePointFile(externalDataFilePathTextBox.Text);
                }
                else
                {
                    MessageBox.Show("please specify a valid path to the .pnt file");
                }
            }
            
            


            /// set up grid constraint object
            if (!m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eFacility, "ExternalCoverageData"))
            {
                m_root.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "ExternalCoverageData");
            }
            IAgStkObject gridConstraintFac = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eFacility)["ExternalCoverageData"];
            gridConstraintFac.AccessConstraints.RemoveConstraint(AgEAccessConstraints.eCstrLineOfSight);
            ((IAgFacility)gridConstraintFac).Graphics.IsObjectGraphicsVisible = false;
            if (!gridConstraintFac.AccessConstraints.IsNamedConstraintActive("ExternalCoverageData"))
            {
                gridConstraintFac.AccessConstraints.AddNamedConstraint("ExternalCoverageData");
            }
            IAgAccessCnstrPluginMinMax externalConstraint = gridConstraintFac.AccessConstraints.GetActiveNamedConstraint("ExternalCoverageData") as IAgAccessCnstrPluginMinMax;
            externalConstraint.EnableMin = true;
            externalConstraint.Min = 0;
            externalConstraint.SetProperty("CsvPath", externalDataFilePathTextBox.Text);
            // do not remove the line below. For some magical reason you need to turn something meaningless 
            // on or off so the line above will get applied properly. Don't ask
            externalConstraint.SetProperty("DebugMode", false);


            /// set up asset
            if (!m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, "ExternalCoverageData"))
            {
                m_root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "ExternalCoverageData");
            }
            IAgStkObject gridConstraintSat = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eSatellite)["ExternalCoverageData"];
            ((IAgSatellite)gridConstraintSat).Graphics.IsObjectGraphicsVisible = false;
            gridConstraintSat.AccessConstraints.RemoveConstraint(AgEAccessConstraints.eCstrLineOfSight);

            IAgSatellite sat = gridConstraintSat as IAgSatellite;
            sat.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);
            IAgVePropagatorTwoBody prop = sat.Propagator as IAgVePropagatorTwoBody;
            prop.Propagate();





            /// set up coverage
            if (!m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eCoverageDefinition, "ExternalCoverageData"))
            {
                m_root.CurrentScenario.Children.New(AgESTKObjectType.eCoverageDefinition, "ExternalCoverageData");
            }

            IAgStkObject oCoverage = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eCoverageDefinition)["ExternalCoverageData"];
            IAgCoverageDefinition coverage = oCoverage as IAgCoverageDefinition;
            coverage.Grid.BoundsType = AgECvBounds.eBoundsGlobal;
            coverage.PointDefinition.PointLocationMethod = AgECvPointLocMethod.eSpecifyCustomLocations;
            coverage.PointDefinition.PointFileList.RemoveAll();
            coverage.PointDefinition.PointFileList.Add(pntFilePathTextBox.Text);

            coverage.PointDefinition.GridClass = AgECvGridClass.eGridClassFacility;
            coverage.PointDefinition.UseGridSeed = true;
            coverage.PointDefinition.SeedInstance = "Facility/ExternalCoverageData";

            coverage.AssetList.RemoveAll();
            coverage.AssetList.Add("Satellite/ExternalCoverageData");


            /// set up FOM
            if (!oCoverage.Children.Contains(AgESTKObjectType.eFigureOfMerit, "ExternalCoverageData"))
            {
                oCoverage.Children.New(AgESTKObjectType.eFigureOfMerit, "ExternalCoverageData");
            }
            IAgFigureOfMerit fom = oCoverage.Children.GetElements(AgESTKObjectType.eFigureOfMerit)["ExternalCoverageData"] as IAgFigureOfMerit;

            fom.SetAccessConstraintDefinitionName("ExternalCoverageData");

            fom.Graphics.Static.IsVisible = false;
            fom.Graphics.Animation.IsVisible = true;
            fom.Graphics.Animation.Contours.IsVisible = true;
            fom.Graphics.Animation.Contours.LevelAttributes.AddLevelRange(0, 4, 1);
            fom.Graphics.Animation.Contours.RampColor.StartColor = Color.Red;
            fom.Graphics.Animation.Contours.RampColor.EndColor = Color.Blue;



            /// compute coverage
            coverage.ComputeAccesses();


        }

        private void externalDataFileSelectionButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    externalDataFilePathTextBox.Text = openFileDialog.FileName;
                }
            }

            // also write this to .pnt path
            if (File.Exists(externalDataFilePathTextBox.Text) && createPntFileCheckBox.Checked)
            {
                pntFilePathTextBox.Text = Path.ChangeExtension(externalDataFilePathTextBox.Text, ".pnt");
            }
        }




        private IAgStkObject CreateNewObject(AgESTKObjectType objectType, IAgStkObject parent, string newObjectName)
        {
            IAgStkObject newObject = null;

            if (!parent.Children.Contains(objectType, newObjectName))
            {
                newObject = parent.Children.New(objectType, newObjectName);
            }
            else
            {
                bool createdNewObj = false;
                Int32 counter = 2;

                while (!createdNewObj)
                {
                    if (!parent.Children.Contains(objectType, newObjectName + counter.ToString()))
                    {
                        newObject = parent.Children.New(objectType, newObjectName + counter.ToString());
                        createdNewObj = true;
                    }
                    else
                    {
                        counter += 1;
                    }
                }
            }

            return newObject;
        }

        private void selectPntFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "csv files (*.pnt)|*.pnt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    pntFilePathTextBox.Text = openFileDialog.FileName;
                }
            }
        }



    }
}

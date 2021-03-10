using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using AGI.STKVgt;
using System.Threading;
using System.Collections.Specialized;

namespace Stk12.UiPlugin.ObjectModelTutorial
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;


        public CustomUserInterface()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Implementation
        public stdole.IPictureDisp GetIcon()
        {
            return null;
        }

        public void OnClosing()
        {
            CommonData.StkRoot.OnStkObjectAdded -= m_root_OnStkObjectAdded;
            CommonData.StkRoot.OnStkObjectDeleted -= m_root_OnStkObjectDeleted;
        }

        public void OnSaveModified()
        {

        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as Setup;
            m_stkObjectsLibrary = new StkObjectsLibrary();

            //EXAMPLE: Hooking to STK Exents
            CommonData.StkRoot.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            CommonData.StkRoot.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);

            //EXAMPLE: Using preference value
            //m_uiPlugin.DoubleValue;

            //EXAMPLE: Populate combo box with STK Objects
            PopulateComboBox();
        }


        #endregion

        #region Sample code
        void m_root_OnStkObjectDeleted(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            cbAccessFrom.Items.Remove(simpleName);
            cbAccessTo.Items.Remove(simpleName);
        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            cbAccessFrom.Items.Add(simpleName);
            cbAccessTo.Items.Add(simpleName);
        }

        void PopulateComboBox()
        {
            string simpleName;
            string className;
            StringCollection objectNames = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");

            foreach (string objectName in objectNames)
            {
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario")
                {
                    simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectName);
                    cbAccessFrom.Items.Add(simpleName);
                    cbAccessTo.Items.Add(simpleName);
                }
            }

            if (cbAccessFrom.Items.Count > 0)
            {
                cbAccessFrom.SelectedIndex = 0;
            }
            if (cbAccessTo.Items.Count > 0)
            {
                cbAccessTo.SelectedIndex = 0;
            }
        }

        IAgVePropagatorGreatArc createAircraftFromWaypoints()
        {
            //Create vehicle (aircraft) using GreatArc propagator
            AgAircraft aircraft = null;
            IAgVePropagatorGreatArc greatArcPropagator = null;
            if (CommonData.StkRoot.CurrentScenario.Children.Contains(AgESTKObjectType.eAircraft, "MyAircraft"))
            {
                aircraft = CommonData.StkRoot.GetObjectFromPath("Aircraft/MyAircraft") as AgAircraft;
                greatArcPropagator = aircraft.Route as IAgVePropagatorGreatArc;
            }
            else
            {
                aircraft = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, "MyAircraft") as AgAircraft;
                aircraft.SetRouteType(AgEVePropagatorType.ePropagatorGreatArc);
                greatArcPropagator = aircraft.Route as IAgVePropagatorGreatArc;
            }

            //greatArcPropagator.Propagate();
            return greatArcPropagator;
        }
        #endregion
        //EXAMPLE: Progress bar
        private void TestProgressBar()
        {
            m_uiPlugin.ProgressBar.BeginTracking(AgEProgressTrackingOptions.eProgressTrackingOptionNoCancel, AgEProgressTrackingType.eTrackAsProgressBar);
            for (int i = 0; i <= 100; i++)
            {
                m_uiPlugin.ProgressBar.SetProgress(i, string.Format("Progress is at {0}...", i));
                Thread.Sleep(100);
                if (!m_uiPlugin.ProgressBar.Continue)
                    break;
            }
            m_uiPlugin.ProgressBar.EndTracking();
        }

        private void btnTestProgressBar_Click(object sender, EventArgs e)
        {
            TestProgressBar();
        }

        private void computeAccess_Click(object sender, EventArgs e)
        {
            string accessToName = cbAccessTo.GetItemText(cbAccessTo.SelectedItem);
            string accessFromName = cbAccessTo.GetItemText(cbAccessFrom.SelectedItem);

            IAgStkObject toObj = CommonData.StkRoot.GetObjectFromPath(accessToName);
            IAgStkObject fromObj = CommonData.StkRoot.GetObjectFromPath(accessFromName);
            IAgStkAccess access = toObj.GetAccessToObject(fromObj);
            access.ComputeAccess();

            // Get access results from data provider
            IAgIntervalCollection accessIntervals = access.ComputedAccessIntervalTimes;
            IAgDataPrvTimeVar accessDataProvider = access.DataProviders.GetDataPrvTimeVarFromPath("AER Data//Default");
            Array dataProviderElements = new object[] { "Time", "Azimuth", "Elevation", "Range" };

            dataGridViewAccess.Rows.Clear();
            dataGridViewAccess.Rows.Add();
            for (int i = 0; i < accessIntervals.Count; i++)
            {
                object startTime = null, stopTime = null;
                accessIntervals.GetInterval(i, out startTime, out stopTime);
                IAgDrResult dataProviderResult = accessDataProvider.ExecElements(startTime, stopTime, 1, ref dataProviderElements);
                Array timeValues = dataProviderResult.DataSets[0].GetValues();
                Array azimuthValues = dataProviderResult.DataSets[1].GetValues();
                Array elevationValues = dataProviderResult.DataSets[2].GetValues();
                Array rangeValues = dataProviderResult.DataSets[3].GetValues();
                

                for (int j = 0; j < timeValues.Length; j++)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewAccess.Rows[0].Clone();
                    row.Cells[0].Value = timeValues.GetValue(j).ToString();
                    row.Cells[1].Value = azimuthValues.GetValue(j).ToString();
                    row.Cells[2].Value = elevationValues.GetValue(j).ToString();
                    row.Cells[3].Value = rangeValues.GetValue(j).ToString();
                    dataGridViewAccess.Rows.Add(row);
                }

            }


            ////Get built in Calucation object from Analysis Workbench
            //// Could put into another dataGridView
            //var parameterSets = access.Vgt.ParameterSets["From-To-AER(Body)"];

            ////Get magnitude vector
            //IAgCrdnCalcScalar magnitude = ((IAgCrdn)parameterSets).EmbeddedComponents["From-To-AER(Body).Cartesian.Magnitude"] as IAgCrdnCalcScalar;

            ////Get times of the minimum value for each access interval
            //AgCrdnEventArrayExtrema minTimes = ((IAgCrdn)parameterSets).EmbeddedComponents["From-To-AER(Body).Cartesian.Magnitude.TimesOfLocalMin"] as AgCrdnEventArrayExtrema;
            //Array timeArray = minTimes.FindTimes().Times;
            //for (int i = 0; i < timeArray.Length; i++)
            //{
            //    double result = magnitude.Evaluate(timeArray.GetValue(i)).Value;
            //}

        }

        private void animate_Click(object sender, EventArgs e)
        {
            // Alternative to root.ExecuteCommand("connect command string"), which implements a try and catch
            m_stkObjectsLibrary.ExecuteCommand("Animate * Start");

            //Execute connect command
            //CommonData.StkRoot.ExecuteCommand("VO * Annotation Frame Show Off");


        }

        private void pause_Click(object sender, EventArgs e)
        {
            m_stkObjectsLibrary.ExecuteCommand("Animate * Pause");
        }

        private void reset_Click(object sender, EventArgs e)
        {
            m_stkObjectsLibrary.ExecuteCommand("Animate * Reset");
        }

        private void facilityNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void createFacility_Click(object sender, EventArgs e)
        {

            //Add child object (sensor)
            AgFacility facility = null;
            if (CommonData.StkRoot.CurrentScenario.Children.Contains(AgESTKObjectType.eFacility, facilityName.Text))
            {
                facility = (AgFacility)CommonData.StkRoot.GetObjectFromPath("Facility/" + facilityName.Text);
            }
            else
            {
                facility = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, facilityName.Text) as AgFacility;
            }

            // Assign lat, lon and alt
            double lat = Convert.ToDouble(latTb.Text);
            double lon = Convert.ToDouble(lonTb.Text);
            double alt = Convert.ToDouble(altTb.Text);
            CommonData.StkRoot.UnitPreferences.SetCurrentUnit("LatitudeUnit", "deg");
            CommonData.StkRoot.UnitPreferences.SetCurrentUnit("LongitudeUnit", "deg");
            facility.UseTerrain = false; // Altitude is ignored if terrain is on. Height above ground can be used with terrain
            facility.Position.AssignPlanetodetic(lat, lon, alt);

            // Query lat, lon and alt
            //object latOut, lonOut; double altValue, latValue, lonValue;
            //facility.Position.QueryPlanetodetic(out latOut, out lonOut, out altValue);
            //latValue = double.Parse(latOut.ToString());
            //lonValue = double.Parse(lonOut.ToString());

            facility.Graphics.UseInstNameLabel = false;
            facility.Graphics.LabelName = facilityName.Text + "CustomLabel";

        }

        private void lonTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void createSensorButton_Click(object sender, EventArgs e)
        {

            AgFacility facility = null;
            if (CommonData.StkRoot.CurrentScenario.Children.Contains(AgESTKObjectType.eFacility, facilityName.Text))
            {
                facility = (AgFacility)CommonData.StkRoot.GetObjectFromPath("Facility/" + facilityName.Text);
            }
            else
            {
                facility = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, facilityName.Text) as AgFacility;
            }

            //Add child object (sensor)
            IAgSensor sensor = null;
            if (facility.Children.Contains(AgESTKObjectType.eSensor, "MySensor"))
            {
                sensor = (IAgSensor)CommonData.StkRoot.GetObjectFromPath("*/Facility/"+ facilityName.Text + "/Sensor/MySensor");
            }
            else
            {
                sensor = facility.Children.New(AgESTKObjectType.eSensor, "MySensor") as IAgSensor;
            }


            // Set Sensor type and shape
            double InnerConeHalfAngle = 50;
            double OuterConeHalfAngle = 90;
            double MinimumClockAngle = 0;
            double MaximumClockAngle = 90;
            sensor.SetPatternType(AgESnPattern.eSnComplexConic);
            sensor.CommonTasks.SetPatternComplexConic(InnerConeHalfAngle,OuterConeHalfAngle,MinimumClockAngle,MaximumClockAngle);


            //Add range constraint to sensor
            double rangeConstraintValue = 40;
            IAgAccessCnstrMinMax rangeConstraint = null;
            if (!sensor.AccessConstraints.IsConstraintActive(AgEAccessConstraints.eCstrRange))
            {
                rangeConstraint = sensor.AccessConstraints.AddConstraint(AgEAccessConstraints.eCstrRange) as IAgAccessCnstrMinMax;
            }
            else
            {
                rangeConstraint = sensor.AccessConstraints.GetActiveConstraint(AgEAccessConstraints.eCstrRange) as IAgAccessCnstrMinMax;
            }
            rangeConstraint.EnableMax = true;
            rangeConstraint.Max = rangeConstraintValue;


        }

        private void addSatelliteButton_Click(object sender, EventArgs e)
        {
            //Create vehicle (satellite)
            string satName = "TLE" + tleNumberTb.Text;
            AgSatellite satellite = null;
            if (CommonData.StkRoot.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, satName))
            {
                satellite = CommonData.StkRoot.GetObjectFromPath("Satellite/" + satName) as AgSatellite;
            }
            else
            {
                satellite = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, satName) as AgSatellite;
                //Using SPG4 propagator
                satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4);
                IAgVePropagatorSGP4 propagator = satellite.Propagator as IAgVePropagatorSGP4;
                propagator.CommonTasks.AddSegsFromOnlineSource(tleNumberTb.Text);
                propagator.Propagate();
            }



            //using Two Body propagator
            //satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);
            //IAgVePropagatorTwoBody twoBodyPropagator = satellite.Propagator as IAgVePropagatorTwoBody;
            //twoBodyPropagator.Propagate();

            //Add access constraints to object
            IAgAccessCnstrCondition lightingConstraint = null;
            if (!satellite.AccessConstraints.IsConstraintActive(AgEAccessConstraints.eCstrLighting))
            {
                lightingConstraint = satellite.AccessConstraints.AddConstraint(AgEAccessConstraints.eCstrLighting) as IAgAccessCnstrCondition;
                
            }
            else
            {
                lightingConstraint = satellite.AccessConstraints.GetActiveConstraint(AgEAccessConstraints.eCstrLighting) as IAgAccessCnstrCondition;
            }
            lightingConstraint.Condition = AgECnstrLighting.eDirectSun;

        }

        private void dataGridViewAccess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void createAircraft_Click(object sender, EventArgs e)
        {
            IAgVePropagatorGreatArc greatArcPropagator = createAircraftFromWaypoints();
            greatArcPropagator.Propagate();
            
        }

        private void addLLAWaypointBtn_Click(object sender, EventArgs e)
        {
            IAgVePropagatorGreatArc greatArcPropagator = createAircraftFromWaypoints();
            double lat = Convert.ToDouble(latTb.Text);
            double lon = Convert.ToDouble(lonTb.Text);
            double alt = Convert.ToDouble(altTb.Text);

            IAgVeWaypointsElement waypoint2 = greatArcPropagator.Waypoints.Add();
            waypoint2.Latitude = lat;
            waypoint2.Longitude = lon;
            waypoint2.Altitude = alt;

            greatArcPropagator.Propagate();
        }

        private void createVectorButton_Click(object sender, EventArgs e)
        {
            //Create vector between objects
            string accessFromName = cbAccessFrom.GetItemText(cbAccessFrom.SelectedItem);
            string accessToName = cbAccessTo.GetItemText(cbAccessTo.SelectedItem);
            
            IAgStkObject fromObj = CommonData.StkRoot.GetObjectFromPath(accessFromName);
            IAgStkObject toObj = CommonData.StkRoot.GetObjectFromPath(accessToName);

            IAgCrdnVectorGroup vectors = fromObj.Vgt.Vectors;
            IAgCrdnVectorDisplacement vector = null;
            string vectorName = "From" + fromObj.InstanceName + "To" + toObj.InstanceName;
            if (vectors.Contains(vectorName))
            {
                vector = (IAgCrdnVectorDisplacement)vectors[vectorName];
            }
            else 
            {
                vector = (IAgCrdnVectorDisplacement)vectors.Factory.Create(vectorName, "description", AGI.STKVgt.AgECrdnVectorType.eCrdnVectorTypeDisplacement);
            }
            vector.Destination.SetPoint(toObj.Vgt.Points["Center"]);
            vector.Origin.SetPoint(fromObj.Vgt.Points["Center"]);

            // Visualize

            //if (fromObj.ClassName == "Facility")
            //{
            //    fromObj2 = fromObj as IAgFacility;
            //}
            //else if (fromObj.ClassName == "Satellite")
            //{
            //    fromObj2 = fromObj as IAgSatellite;
            //}
            //else if (fromObj.ClassName == "Sensor")
            //{
            //    fromObj2 = fromObj as IAgSensor;
            //}
            //else if (fromObj.ClassName == "Aircraft")
            //{
            //    fromObj2 = fromObj as IAgAircraft;
            //}
            //else
            //{
            //    return;
            //}

            try
            {
                IAgFacility fromObj2 = fromObj as IAgFacility;
                IAgVORefCrdnVector boresightVector = fromObj2.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eVectorElem, fromObj.ClassName + "/" + fromObj.InstanceName + " " + vectorName + " Vector") as IAgVORefCrdnVector;
                fromObj2.VO.Vector.VectorSizeScale = 4.0;
            }
            catch
            {

            }

            try
            {
                IAgSatellite fromObj2 = fromObj as IAgSatellite;
                IAgVORefCrdnVector boresightVector = fromObj2.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eVectorElem, fromObj.ClassName + "/" + fromObj.InstanceName + " " + vectorName + " Vector") as IAgVORefCrdnVector;
                fromObj2.VO.Vector.VectorSizeScale = 4.0;
            }
            catch
            {

            }

            try
            {
                IAgAircraft fromObj2 = fromObj as IAgAircraft;
                IAgVORefCrdnVector boresightVector = fromObj2.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eVectorElem, fromObj.ClassName + "/" + fromObj.InstanceName + " " + vectorName + " Vector") as IAgVORefCrdnVector;
                fromObj2.VO.Vector.VectorSizeScale = 4.0;
            }
            catch
            {

            }

            try
            {
                IAgSensor fromObj2 = fromObj as IAgSensor;
                IAgVORefCrdnVector boresightVector = fromObj2.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eVectorElem, fromObj.ClassName + "/" + fromObj.InstanceName + " " + vectorName + " Vector") as IAgVORefCrdnVector;
                fromObj2.VO.Vector.VectorSizeScale = 4.0;
            }
            catch
            {

            }

        }

        private void CustomUserInterface_Load(object sender, EventArgs e)
        {

        }
    }
}

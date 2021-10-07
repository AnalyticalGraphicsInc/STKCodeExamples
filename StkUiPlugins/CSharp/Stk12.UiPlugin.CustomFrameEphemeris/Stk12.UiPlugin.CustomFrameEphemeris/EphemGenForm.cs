using AGI.STKObjects;
using AGI.Ui.Plugins;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Stk12.UiPlugin.CustomFrameEphemeris
{


    public partial class EphemGenForm : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private Setup m_uiPlugin;
        private StkObjectsLibrary m_stkObjectsLibrary;

        public EphemGenForm()
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

            //EXAMPLE: Populate combo box with STK Objects
            PopulateComboBox();

            // Set ToolTips
            ToolTipInitialization();
        }


        #endregion
  
        private void ToolTipInitialization()
        {
            ToolTip.AutoPopDelay = 5000;
            ToolTip.InitialDelay = 700;
            ToolTip.ReshowDelay = 500;
            ToolTip.ShowAlways = true;
            ToolTip.SetToolTip(this.coordinateSysLabel, "Choose a coordinate system listed in Analysis Workbench. Include full path, i.e. CentralBody/Earth J2000");
            ToolTip.SetToolTip(this.fileNameLabel, "Specify file name for resulting .e");
            ToolTip.SetToolTip(this.stepSizeLabel, "Step size of ephemeris in seconds");
        }


        #region Combo Box Population Code
        void m_root_OnStkObjectDeleted(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            cbStkObjects.Items.Remove(simpleName);
        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            string objectPath = Sender.ToString();
            string simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectPath);
            cbStkObjects.Items.Add(simpleName);
        }

        void PopulateComboBox()
        {
            string simpleName;
            string className;
            StringCollection objectNames = m_stkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");

            foreach (string objectName in objectNames)
            {
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (ObjectWeSupport(className))
                {
                    simpleName = m_stkObjectsLibrary.SimplifiedObjectPath(objectName);
                    cbStkObjects.Items.Add(simpleName);
                }
            }

            if (cbStkObjects.Items.Count > 0)
            {
                cbStkObjects.SelectedIndex = 0;
            }
        }

        #endregion


        static bool ObjectWeSupport(string className)
        {
            if ((className == "Aircraft") || (className == "GroundVehicle") || (className == "LaunchVehicle") || (className == "Missile") 
                || (className == "Planet") || (className == "Satellite") || (className == "Ship"))
            {
                return true;
            }

            else return false;
        }

        static string SetUnits()
        {
            var currentUnit = CommonData.StkRoot.UnitPreferences["DateFormat"].CurrentUnit.Abbrv;
            CommonData.StkRoot.UnitPreferences["DateFormat"].SetCurrentUnit("EpSec");
            return currentUnit;
        }

        static double GetObjStartTime(IAgStkObject ephemObject, IAgScenario scen, string objectPath)
        {
            double startTime;
            try
            {
                var provider = ephemObject.DataProviders["Time Instant"] as IAgDataProviderGroup;
                startTime = ((IAgDataPrvInterval)provider.Group["AvailabilityStartTime"]).Exec(scen.StartTime, scen.StopTime).DataSets.GetDataSetByName("Time Instant").GetValues().GetValue(0);
            }
            catch
            {
                // If error occurs above, object is Ship, Ground Vehicle or Aircraft object
                var provider = CommonData.StkRoot.VgtRoot.GetProvider(objectPath);
                startTime = provider.Events["EphemerisStartTime"].FindOccurrence().Epoch;
            }
            return startTime;
        }

        static double GetObjStopTime(IAgStkObject ephemObject, IAgScenario scen, string objectPath)
        {
            double stopTime;
            try
            {
                var provider = ephemObject.DataProviders["Time Instant"] as IAgDataProviderGroup;
                stopTime = ((IAgDataPrvInterval)provider.Group["AvailabilityStopTime"]).Exec(scen.StartTime, scen.StopTime).DataSets.GetDataSetByName("Time Instant").GetValues().GetValue(0);
            }
            catch
            {
                // If error occurs above, object is Ship, Ground Vehicle or Aircraft object
                var provider = CommonData.StkRoot.VgtRoot.GetProvider(objectPath);
                stopTime = provider.Events["EphemerisStopTime"].FindOccurrence().Epoch;
            }
            return stopTime;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "STK Ephemeris (*.e)|*.e";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileNameBox.Text = Path.GetFullPath(saveFileDialog.FileName);
            }
        }

        static IAgDrDataSetCollection GetDataSets(IAgStkObject ephemObject, string coordSys, double startTime, double stopTime, double stepSize)
        {
            var dp = ephemObject.DataProviders["Points Choose System"] as IAgDataProviderGroup;
            var dpCen = dp.Group["Center"] as IAgDataProvider;
            dpCen.PreData = coordSys;
            var dpCenTimeVar = dpCen as IAgDataPrvTimeVar;
            var results = dpCenTimeVar.Exec(startTime, stopTime, stepSize);
            var dataSets = results.DataSets;
            return dataSets;
        }

        private void Ephem2Text(string filePath, Array times, IAgScenario scen, IAgStkObject ephemObject, string coordSys, Array x, 
            Array y, Array z, Array xVel, Array yVel, Array zVel)
        {
            // Print ephem data to text file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (StreamWriter sw = File.CreateText(filePath))
            {
                var ephemLength = times.Length;
                sw.WriteLine("stk.v.5.0");
                sw.WriteLine("BEGIN Ephemeris");
                sw.WriteLine("NumberOfEphemerisPoints    " + ephemLength.ToString());
                var StartTimeUTCG = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", scen.StartTime.ToString());
                sw.WriteLine("ScenarioEpoch         " + StartTimeUTCG);
                if ((ephemObject.ClassName == "Missile") || (ephemObject.ClassName == "LaunchVehicle"))
                {
                    sw.WriteLine("InterpolationMethod       Lagrange");
                    sw.WriteLine("InterpolationSamplesM1        5");
                }
                else if (ephemObject.ClassName == "Satellite")
                {
                    sw.WriteLine("InterpolationSamplesM1        7");
                }
                else
                {
                    try
                    {
                        var ephemObjAC = ephemObject as IAgAircraft;
                        if (ephemObjAC.RouteType.ToString() == "ePropagatorAviator")
                        {
                            sw.WriteLine("InterpolationMethod       Hermite");
                        }
                    }

                    catch
                    {
                        if (coordSys == "CentralBody/Earth Fixed")
                        {
                            sw.WriteLine("InterpolationMethod       GreatArc");
                        }
                    }
                    sw.WriteLine("InterpolationSamplesM1        1");
                }
                sw.WriteLine("DistanceUnit      Kilometers");
                sw.WriteLine("CentralBody        " + ephemObject.CentralBodyName);
                string[] objs = coordSys.Split(' ');
                sw.WriteLine("CoordinateSystem       Custom " + objs[1] + " " + objs[0]);
                sw.WriteLine("EphemerisTimePosVel");
                for (int k = 0; k < ephemLength; k++)
                {
                    sw.WriteLine("{0}   {1}   {2}   {3}   {4}   {5}   {6}", times.GetValue(k), x.GetValue(k),
                        y.GetValue(k), z.GetValue(k), xVel.GetValue(k), yVel.GetValue(k), zVel.GetValue(k));
                }
                sw.WriteLine("END Ephemeris");
            }
        }

        private void CreateNew(AgESTKObjectType ephemObjType, string objScenName, string objectPath, string ephemObjName, string filePath)
        {
            IAgStkObject newEphObj;

            if (CommonData.StkRoot.CurrentScenario.Children.Contains(ephemObjType, objScenName + "_FromEphem"))
            {
                newEphObj = CommonData.StkRoot.GetObjectFromPath(objectPath + "_FromEphem");
            }
            else
            {
                newEphObj = CommonData.StkRoot.CurrentScenario.Children.New(ephemObjType, objScenName + "_FromEphem");
            }

            var propExt = AgEVePropagatorType.ePropagatorStkExternal;

            if (ephemObjName == "Satellite")
            {
                ((IAgSatellite)newEphObj).SetPropagatorType(propExt);
                ((IAgVePropagatorStkExternal)((IAgSatellite)newEphObj).Propagator).Filename = filePath;
                ((IAgVePropagatorStkExternal)((IAgSatellite)newEphObj).Propagator).Propagate();
            }
            else if (ephemObjName == "Missile")
            {
                ((IAgMissile)newEphObj).SetTrajectoryType(propExt);
                ((IAgVePropagatorStkExternal)((IAgMissile)newEphObj).Trajectory).Filename = filePath;
                ((IAgVePropagatorStkExternal)((IAgMissile)newEphObj).Trajectory).Propagate();
            }
            else if (ephemObjName == "LaunchVehicle")
            {
                ((IAgLaunchVehicle)newEphObj).SetTrajectoryType(propExt);
                ((IAgVePropagatorStkExternal)((IAgLaunchVehicle)newEphObj).Trajectory).Filename = filePath;
                ((IAgVePropagatorStkExternal)((IAgLaunchVehicle)newEphObj).Trajectory).Propagate();
            }
            else if (ephemObjName == "Aircraft")
            {
                ((IAgAircraft)newEphObj).SetRouteType(propExt);
                ((IAgVePropagatorStkExternal)((IAgAircraft)newEphObj).Route).Filename = filePath;
                ((IAgVePropagatorStkExternal)((IAgAircraft)newEphObj).Route).Propagate();
            }
            else if (ephemObjName == "GroundVehicle")
            {
                ((IAgGroundVehicle)newEphObj).SetRouteType(propExt);
                ((IAgVePropagatorStkExternal)((IAgGroundVehicle)newEphObj).Route).Filename = filePath;
                ((IAgVePropagatorStkExternal)((IAgGroundVehicle)newEphObj).Route).Propagate();
            }
            else if (ephemObjName == "Ship")
            {
                ((IAgShip)newEphObj).SetRouteType(propExt);
                ((IAgVePropagatorStkExternal)((IAgShip)newEphObj).Route).Filename = filePath;
                ((IAgVePropagatorStkExternal)((IAgShip)newEphObj).Route).Propagate();
            }
        }

        private void computeEphemerisButton_Click(object sender, EventArgs e)
        {

            #region EphemGenSetup
            
            #region TestStepSize
            if (stepSizeBox.TextLength == 0) 
            {
                MessageBox.Show("Step Size Required");
                return;
            }
            double stepSize;
            try
            {
                stepSize = Convert.ToDouble(stepSizeBox.Text);
            }
            catch
            {
                MessageBox.Show("Step Size Must Be a Number");
                return;
            }
            #endregion

            #region GrabRemainingValues
            var objectPath = cbStkObjects.Text.Replace("*/", "");
            var coordSys = coordinateSysBox.Text;
            var createNew = createNewCheckBox.Checked;
            var filePath = fileNameBox.Text;
            #endregion

            // Check coordinate system
            try
            {
                string[] objs = coordSys.Split(' ');
                var provider = CommonData.StkRoot.VgtRoot.GetProvider(objs[0]);
                var sys = provider.Systems[objs[1]];
            }
            catch (Exception coordinateSystemError)
            {
                MessageBox.Show(coordinateSystemError.Message);
                return;
            }

            // Grab scenario
            var scen = CommonData.StkRoot.CurrentScenario as IAgScenario;

            // Grab object to make ephemeris for
            var ephemObject = CommonData.StkRoot.GetObjectFromPath(objectPath);
            var ephemObjType = ephemObject.ClassType;
            var ephemObjName = ephemObject.ClassName;
            var objScenName = (ephemObject.Path).Split('/')[6];

            // Set unit preferences
            var currentUnit = SetUnits();

            // Get start and stop time of object using VGT
            var startTime = GetObjStartTime(ephemObject, scen, objectPath);
            var stopTime = GetObjStopTime(ephemObject, scen, objectPath);
            #endregion

            #region PullEphemData
            // Grab ephemeris data
            var dataSets = GetDataSets(ephemObject, coordSys, startTime, stopTime, stepSize);

            // Get Values
            Array times = dataSets.GetDataSetByName("Time").GetValues();
            Array x = dataSets.GetDataSetByName("x").GetValues();
            Array y = dataSets.GetDataSetByName("y").GetValues();
            Array z = dataSets.GetDataSetByName("z").GetValues();
            Array xVel = dataSets.GetDataSetByName("Velocity x").GetValues();
            Array yVel = dataSets.GetDataSetByName("Velocity y").GetValues();
            Array zVel = dataSets.GetDataSetByName("Velocity z").GetValues();
            #endregion

            Ephem2Text(filePath, times, scen, ephemObject, coordSys, x, y, z, xVel, yVel, zVel);

            if (createNew) {CreateNew(ephemObjType, objScenName, objectPath, ephemObjName, filePath);}

            CommonData.StkRoot.UnitPreferences["DateFormat"].SetCurrentUnit(currentUnit);
        }
    }
}

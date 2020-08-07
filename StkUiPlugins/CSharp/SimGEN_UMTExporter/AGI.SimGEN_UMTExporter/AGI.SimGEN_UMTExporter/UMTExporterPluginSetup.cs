using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.Ui.Plugins;

namespace AGI.SimGEN_UMTExporter
{
    [Guid("7256A79C-65C9-4493-81A6-CBE5766E0AB8")]
    [ProgId("AGI.SimGEN_UMTExporter")]
    [ClassInterface(ClassInterfaceType.None)]
    public class UMTExporterPluginSetup: AGI.Ui.Plugins.IAgUiPlugin, AGI.Ui.Plugins.IAgUiPluginCommandTarget
    {
        private IAgUiPluginSite m_psite;
        private IAgStkObjectRoot m_root;
        public IAgStkObjectRoot Root
        {
            get
            {
                return m_root;
            }
        }

        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_psite = PluginSite;
            m_root = (IAgStkObjectRoot)m_psite.Application.Personality2;
        }

        public void OnShutdown()
        {
            m_psite = null;
        }

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            //throw new NotImplementedException();
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            //the current Selected Object
            IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_psite.Selection[0].Path);

            if (oSelectedObject.ClassName.Equals("Satellite") | oSelectedObject.ClassName.Equals("Aircraft") | oSelectedObject.ClassName.Equals("GroundVehicle") | oSelectedObject.ClassName.Equals("LaunchVehicle") | oSelectedObject.ClassName.Equals("Ship") | oSelectedObject.ClassName.Equals("Missile"))
            {
                MenuBuilder.AddMenuItem("ExportUMT", "Export UMT File...", "Exports a UMT File", null);
            }
        }

        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //throw new NotImplementedException();
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            if (m_psite.Selection.Count == 1)
            {
                IAgStkObject oSelectedObject = m_root.GetObjectFromPath(m_psite.Selection[0].Path);
                //Only enable menu items for Scenario object menu
                if (oSelectedObject.ClassName.Equals("Satellite") | oSelectedObject.ClassName.Equals("Aircraft") | oSelectedObject.ClassName.Equals("GroundVehicle") | oSelectedObject.ClassName.Equals("LaunchVehicle") | oSelectedObject.ClassName.Equals("Ship") | oSelectedObject.ClassName.Equals("Missile"))
                {
                    if (string.Compare(CommandName, "ExportUMT", true) == 0)
                    {
                        return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
                    }
                }
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Command Name from Right-click Menu
            if (string.Compare(CommandName, "ExportUMT", true) == 0)
            {
                //Launch the Custom User Interface
                try
                {
                    ExportUMTFile();
                    MessageBox.Show("Create successfully created.", "File Creation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Could not create file.  " + ex.Message, "Error Creating File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void ExportUMTFile()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "UMT files (*.umt)|*.umt";
            saveDialog.FilterIndex = 0;
            saveDialog.RestoreDirectory = true;
            string fileName = "";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveDialog.FileName;
            }

            if (fileName == "")
            {
                return;
            }

            //
            // init
            //
            m_root.UnitPreferences.SetCurrentUnit("Time", "sec");
            m_root.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");
            m_root.UnitPreferences.SetCurrentUnit("LatitudeUnit", "rad");
            m_root.UnitPreferences.SetCurrentUnit("LongitudeUnit", "rad");
            m_root.UnitPreferences.SetCurrentUnit("AngleUnit", "rad");
            m_root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");
            m_root.ExecuteCommand("SetUnits / EpSec");

            //
            // get select obj
            //
            IAgStkObject mySelObj = m_root.GetObjectFromPath(m_psite.Selection[0].Path);


            //
            // get obj class, and roll-offset
            //
            double rOffset = 0.0;
            if ((mySelObj.ClassName == "Ship") || (mySelObj.ClassName == "GroundVehicle"))
            {
                rOffset = Math.PI;
            }


            Console.WriteLine("Create times..." + DateTime.Now.ToString());
            //
            // get object start/stop times
            //
            // If I use ObjModel properties, this would be class specific, so I will use connect
            //
            string[] times = m_root.ExecuteCommand("GetTimePeriod " + mySelObj.Path)[0].Replace("\"", "").Split(',');
            //step = 1; // set = 0 to use native ephemeris time step

            //DAN - Changing this to UTCG times
            string startTime = m_root.ConversionUtility.ConvertDate("EpSec", "UTCG", times[0]);
            string stopTime = m_root.ConversionUtility.ConvertDate("EpSec", "UTCG", times[1]);

            //DAN - Update the object model to expect UTCG dates
            m_root.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG");


            IAgDataPrvTimeVar dp = mySelObj.DataProviders.GetDataPrvTimeVarFromPath("Cartesian Position/Fixed");
            Array elements = new object[] { "Time" };
            IAgDrResult result = dp.ExecElements(startTime, stopTime, 1.0, elements);
            Array timesArray = result.DataSets.GetDataSetByName("Time").GetValues();


            Console.WriteLine("Data Providers..." + DateTime.Now.ToString());
            //
            // get position data
            //
            elements = new object[] { "x", "y", "z" };
            dp = mySelObj.DataProviders.GetDataPrvTimeVarFromPath("Cartesian Position/Fixed");
            IAgDrResult results = dp.ExecElements(startTime, stopTime, 1.0, ref elements);
            Array x = results.DataSets.GetDataSetByName("x").GetValues();
            Array y = results.DataSets.GetDataSetByName("y").GetValues();
            Array z = results.DataSets.GetDataSetByName("z").GetValues();



            //
            // get velocity
            //
            elements = new object[] { "x", "y", "z" };
            dp = mySelObj.DataProviders.GetDataPrvTimeVarFromPath("Cartesian Velocity/Fixed");
            results = dp.ExecElements(startTime, stopTime, 1.0, ref elements);
            Array xd = results.DataSets.GetDataSetByName("x").GetValues();
            Array yd = results.DataSets.GetDataSetByName("y").GetValues();
            Array zd = results.DataSets.GetDataSetByName("z").GetValues();



            //
            // get acceleration
            //
            dp = mySelObj.DataProviders.GetDataPrvTimeVarFromPath("Cartesian Acceleration/Fixed");
            results = dp.ExecElements(startTime, stopTime, 1.0, ref elements);
            Array xdd = results.DataSets.GetDataSetByName("x").GetValues();
            Array ydd = results.DataSets.GetDataSetByName("y").GetValues();
            Array zdd = results.DataSets.GetDataSetByName("z").GetValues();


            //
            // get jerk - need to calcualate this: dA/dT
            //
            List<string> xddd = new List<string>();
            List<string> yddd = new List<string>();
            List<string> zddd = new List<string>();
            for (int j = 0; j <= timesArray.Length - 1; j++)
            {
                xddd.Add("0");
                yddd.Add("0");
                zddd.Add("0");
            }


            //
            // get heading, elevation, bank (rad)
            //
            dp = mySelObj.DataProviders.GetDataPrvTimeVarFromPath("Body Axes Orientation/NorthEastDown");
            Array eulerElements = new object[] { "Euler321 precession", "Euler321 nutation", "Euler321 spin" };
            results = dp.ExecElements(startTime, stopTime, 1.0, ref eulerElements);
            Array h = results.DataSets.GetDataSetByName("Euler321 precession").GetValues();
            Array e = results.DataSets.GetDataSetByName("Euler321 nutation").GetValues();
            Array b = results.DataSets.GetDataSetByName("Euler321 spin").GetValues();




            //
            // get angular vel about x,y,z body axes (rad/s)
            //
            dp = mySelObj.DataProviders.GetDataPrvTimeVarFromPath("Body Axes Orientation/Earth Fixed");
            Array avElements = new object[] { "wx", "wy", "wz" };
            results = dp.ExecElements(startTime, stopTime, 1.0, ref avElements);
            Array avel_x = results.DataSets.GetDataSetByName("wx").GetValues();
            Array avel_y = results.DataSets.GetDataSetByName("wy").GetValues();
            Array avel_z = results.DataSets.GetDataSetByName("wz").GetValues();



            //
            // calcualate angular acc about x,y,z body axes (rad/s^2) - from angVel
            //
            List<string> aacc_x = new List<string>();
            List<string> aacc_y = new List<string>();
            List<string> aacc_z = new List<string>();
            for (int j = 0; j <= timesArray.Length - 1; j++)
            {
                aacc_x.Add("0");
                aacc_y.Add("0");
                aacc_z.Add("0");
            }


            //
            // calcualate angular jerk about x,y,z body axes (rad/s^3)  - from angAcc
            //
            List<string> ajerk_x = new List<string>(); ;
            List<string> ajerk_y = new List<string>(); ;
            List<string> ajerk_z = new List<string>(); ;
            for (int j = 0; j <= timesArray.Length - 1; j++)
            {
                ajerk_x.Add("0");
                ajerk_y.Add("0");
                ajerk_z.Add("0");
            }



            //
            // format data (convert time from secs to hh:mm:ss.sss)
            //
            Console.WriteLine("Data Loop... " + DateTime.Now.ToString());
            string[] data = new string[timesArray.Length];
            for (int j = 0; j <= timesArray.Length - 1; j++)
            {
                DateTime iTime = DateTime.Parse(timesArray.GetValue(j).ToString());

                string t = iTime.ToString("HH:mm:ss.fff");
                data[j] = t + ",mot,V1_M1," + Convert.ToString(x.GetValue(j)) + "," + Convert.ToString(y.GetValue(j)) + "," + Convert.ToString(z.GetValue(j)) + "," + Convert.ToString(xd.GetValue(j)) + "," + Convert.ToString(yd.GetValue(j)) + "," + Convert.ToString(zd.GetValue(j)) + "," + Convert.ToString(xdd.GetValue(j)) + "," + Convert.ToString(ydd.GetValue(j)) + "," + Convert.ToString(zdd.GetValue(j)) + "," + xddd[j] + "," + yddd[j] + "," + zddd[j] + "," + Convert.ToString(h.GetValue(j)) + "," + Convert.ToString(e.GetValue(j)) + "," + Convert.ToString(((double)b.GetValue(j) + rOffset)) + "," + Convert.ToString(avel_x.GetValue(j)) + "," + Convert.ToString(avel_y.GetValue(j)) + "," + Convert.ToString(avel_z.GetValue(j)) + "," + aacc_x[j] + "," + aacc_y[j] + "," + aacc_z[j] + "," + ajerk_x[j] + "," + ajerk_y[j] + "," + ajerk_z[j];
            }
            Console.WriteLine("Writing File..." + DateTime.Now.ToString());
            System.IO.File.WriteAllLines(fileName, data);
            Console.WriteLine("Complete. " + DateTime.Now.ToString());

        }

    }
}

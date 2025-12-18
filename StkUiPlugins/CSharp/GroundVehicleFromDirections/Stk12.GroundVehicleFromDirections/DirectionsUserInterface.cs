using AGI.STKObjects;
using AGI.Ui.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


/// this UI plugin will load a GroundVehicle into STK using directions from Bing
/// NOTE: 
///    - there is a length limit on the return string from Bing. If you exceed the length 
///      then increase the buffer size in the app.config
/// 
/// author: jens ramrath, agi
/// date: 7 oct 2011
/// updated: 30 oct 2017 
///             - updated to work with STK11 
///             - switch from SOAP to REST services
///             - allows user to use their own Bing maps key
///             - added traffic tab
/// updated 16 dec 2025
///             - moved from Bing maps to Azure maps

namespace Agi.Ui.Directions
{
    public partial class DirectionsUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private BasicCSharpPlugin m_uiPlugin;


        // Azure maps keys are available here: https://learn.microsoft.com/en-us/azure/azure-maps/how-to-manage-account-keys
        private string m_azureMapKey;


        public DirectionsUserInterface()
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
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as BasicCSharpPlugin;
            m_root = m_uiPlugin.STKRoot;
        }

        #endregion

        private double speedUnitMultiplier;
        private double altUnitMultiplier;

        private void Button1_Click(object sender, EventArgs e)
        {
            // check custom Azure Maps Key
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString(), "AzureMapsKey.config"));
            string mapKey = configDoc.GetElementsByTagName("AzureMapKey")[0].InnerText;

            if (mapKey != "XXX")
            {
                m_azureMapKey = mapKey;
            }
            else
            {
                MessageBox.Show("please enter a valid Azure Maps key in AzureMapsKey.config");
            }

            // check if scenario is open
            if (m_root.CurrentScenario == null)
            {
                MessageBox.Show("no scenario is open.");
                return;
            }


            // check if using address
            if (trafficDirectionsTabControl.SelectedIndex == 0)
            {
                // directions
                #region empty address error check
                if (fromTextBox.Text == "")
                {
                    MessageBox.Show("no start address entered");
                    return;
                }
                if (toTextBox.Text == "")
                {
                    MessageBox.Show("no end address entered");
                    return;
                }
                #endregion

                // get lat/lon from addresses
                MyWaypoint startPoint = Geocode(fromTextBox.Text);
                MyWaypoint endPoint = Geocode(toTextBox.Text);

                #region address found error check
                if (startPoint.Longitude == 0.0 && startPoint.Latitude == 0.0 && startPoint.Altitude == 0.0)
                {
                    MessageBox.Show("start address not found");
                    return;
                }
                if (endPoint.Longitude == 0.0 && endPoint.Latitude == 0.0 && endPoint.Altitude == 0.0)
                {
                    MessageBox.Show("end address not found");
                    return;
                }
                #endregion

                AddRoute(startPoint, endPoint, nameTextBox.Text);
            }

            // check if using points
            if (trafficDirectionsTabControl.SelectedIndex == 1)
            {
                // START
                IAgStkObject oStart = m_root.GetObjectFromPath(fromObjectComboBox.Items[fromObjectComboBox.SelectedIndex].ToString());

                object startLat = 0;
                object startLon = 0;
                double startAlt = 0;

                switch (oStart.ClassType)
                {
                    case AgESTKObjectType.eFacility:
                        ((IAgFacility)oStart).Position.QueryPlanetodetic(out startLat, out startLon, out startAlt);
                        break;
                    case AgESTKObjectType.eTarget:
                        ((IAgTarget)oStart).Position.QueryPlanetodetic(out startLat, out startLon, out startAlt);
                        break;
                    case AgESTKObjectType.ePlace:
                        ((IAgPlace)oStart).Position.QueryPlanetodetic(out startLat, out startLon, out startAlt);
                        break;
                }

                MyWaypoint start = new MyWaypoint();
                start.Latitude = Convert.ToDouble(startLat);
                start.Longitude = Convert.ToDouble(startLon);
                start.Altitude = Convert.ToDouble(startAlt);


                // STOP
                IAgStkObject oStop = m_root.GetObjectFromPath(toObjectComboBox.Items[toObjectComboBox.SelectedIndex].ToString());

                object stopLat = 0.0;
                object stopLon = 0.0;
                double stopAlt = 0.0;

                switch (oStop.ClassType)
                {
                    case AgESTKObjectType.eFacility:
                        ((IAgFacility)oStop).Position.QueryPlanetodetic(out stopLat, out stopLon, out stopAlt);
                        break;
                    case AgESTKObjectType.eTarget:
                        ((IAgTarget)oStop).Position.QueryPlanetodetic(out stopLat, out stopLon, out stopAlt);
                        break;
                    case AgESTKObjectType.ePlace:
                        ((IAgPlace)oStop).Position.QueryPlanetodetic(out stopLat, out stopLon, out stopAlt);
                        break;
                }

                MyWaypoint stop = new MyWaypoint();
                stop.Latitude = Convert.ToDouble(stopLat);
                stop.Longitude = Convert.ToDouble(stopLon);
                stop.Altitude = Convert.ToDouble(stopAlt);


                AddRoute(start, stop, nameTextBox.Text);
            }

                // check if using traffic
            if (trafficDirectionsTabControl.SelectedIndex == 2)
            {
                // traffic
                #region empty lat/lon textbos check
                if (minLatTextBox.Text == "")
                {
                    MessageBox.Show("no southern limit specified");
                    return;
                }
                if (maxLatTextBox.Text == "")
                {
                    MessageBox.Show("no northern limit specified");
                    return;
                }
                if (minLonTextBox.Text == "")
                {
                    MessageBox.Show("no eastern limit specified");
                    return;
                }
                if (maxLonTextBox.Text == "")
                {
                    MessageBox.Show("no western limit specified");
                    return;
                }
                #endregion

                Double minLat = Convert.ToDouble(minLatTextBox.Text);
                Double maxLat = Convert.ToDouble(maxLatTextBox.Text);
                Double minLon = Convert.ToDouble(minLonTextBox.Text);
                Double maxLon = Convert.ToDouble(maxLonTextBox.Text);

                Random rand = new Random(DateTime.Now.Millisecond);

                for (int i = 0; i < Convert.ToInt32(numVehicleTextBox.Text); i++)
                {
                    MyWaypoint startPoint = new MyWaypoint();
                    startPoint.Latitude = minLat + rand.NextDouble() * (maxLat - minLat);
                    startPoint.Longitude = minLon + rand.NextDouble() * (maxLon - minLon);

                    MyWaypoint endPoint = new MyWaypoint();
                    endPoint.Latitude = minLat + rand.NextDouble() * (maxLat - minLat);
                    endPoint.Longitude = minLon + rand.NextDouble() * (maxLon - minLon);

                    AddRoute(startPoint, endPoint, nameTextBox.Text + i.ToString());
                }
            }
        }
            


        private void AddRoute(MyWaypoint startPoint, MyWaypoint endPoint, string gvName)
        { 
            List<MyWaypoint> routePoints = GetRoute(startPoint, endPoint);

            // create new GV and add the waypoints
            if (m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eGroundVehicle, gvName))
            {
                MessageBox.Show(gvName + " already exists, please pick a different name");
            } 
            else
	        {
                if (routePoints != null && routePoints.Count > 0)
                {
                    double turnRadius = 2.0;    // meter
                    double granularity = 1.1;   // meter

                    m_root.UnitPreferences.SetCurrentUnit("Distance", "m");

                    switch (speedUnitsComboBox.SelectedItem.ToString())
                    {
                        case "km/h":
                            //m_root.UnitPreferences.SetCurrentUnit("Distance", "km");
                            m_root.UnitPreferences.SetCurrentUnit("Time", "hr");
                            //turnRadius /= 1000.0;
                            //granularity /= 1000.0;
                            speedUnitMultiplier = 1000.0;
                            break;
                        case "mph":
                            //m_root.UnitPreferences.SetCurrentUnit("Distance", "mi");
                            m_root.UnitPreferences.SetCurrentUnit("Time", "hr");
                            //turnRadius /= 1609.44;
                            //granularity /= 1609.44;
                            speedUnitMultiplier = 1609.44;
                            break;
                        case "m/s":
                            //m_root.UnitPreferences.SetCurrentUnit("Distance", "m");
                            m_root.UnitPreferences.SetCurrentUnit("Time", "sec");
                            speedUnitMultiplier = 1.0;
                            break;
                    }

                    switch (altUnitsComboBox.SelectedItem.ToString())
                    {
                        case "m":
                            altUnitMultiplier = 1.0;
                            break;
                        case "km":
                            altUnitMultiplier = 1000.0;
                            break;
                        case "ft":
                            altUnitMultiplier = 0.3048;
                            break;
                    }


                    IAgStkObject gvObject = m_root.CurrentScenario.Children.New(AgESTKObjectType.eGroundVehicle, gvName);
                    IAgGroundVehicle gv = gvObject as IAgGroundVehicle;

                    gv.SetRouteType(AgEVePropagatorType.ePropagatorGreatArc);
                    IAgVePropagatorGreatArc prop = gv.Route as IAgVePropagatorGreatArc;

                    foreach (MyWaypoint thisPt in routePoints)
                    {
                        IAgVeWaypointsElement thisVeWaypoint = prop.Waypoints.Add();
                        thisVeWaypoint.Latitude = thisPt.Latitude;
                        thisVeWaypoint.Longitude = thisPt.Longitude;
                        thisVeWaypoint.Altitude = thisPt.Altitude * altUnitMultiplier;
                        thisVeWaypoint.Speed = Convert.ToDouble(speedTextBox.Text) * speedUnitMultiplier;
                        thisVeWaypoint.TurnRadius = turnRadius;
                    }

                    if (terrainCheckBox.Checked)
                    {
                        prop.SetAltitudeRefType(AgEVeAltitudeRef.eWayPtAltRefTerrain);
                        IAgVeWayPtAltitudeRefTerrain altRef = prop.AltitudeRef as IAgVeWayPtAltitudeRefTerrain;
                        altRef.Granularity = granularity;
                        altRef.InterpMethod = AgEVeWayPtInterpMethod.eWayPtEllipsoidHeight;
                    }

                    prop.Propagate();
                }
	        }
            

        }


        public struct MyWaypoint
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double Altitude { get; set; }
        }


        // convert address to lat/lon
        private MyWaypoint Geocode(string address)
        {
            // build URL
            string addressPadded = address.Replace(",", "%20").Replace(" ", "%20");
            string urlString = "https://atlas.microsoft.com/geocode?api-version=2025-01-01&subscription-key=" + m_azureMapKey + "&addressLine=" + addressPadded;
            Uri geocodeRequest = new Uri(urlString);

            // send request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(geocodeRequest);
            request.Method = "GET";

            // get response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string returnString = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    returnString = sr.ReadToEnd();
                }
            }

            // parse output
            MyWaypoint returnPoint = new MyWaypoint();
            try
            {
                var returnJson = (JObject)JsonConvert.DeserializeObject(returnString);

                returnPoint.Latitude = Convert.ToDouble(returnJson["features"][0]["geometry"]["coordinates"][1]);
                returnPoint.Longitude = Convert.ToDouble(returnJson["features"][0]["geometry"]["coordinates"][0]);
            }
            catch (Exception)
            {
                throw;
            }

            return returnPoint;
        }



        // call Azure REST service with start and end lat/lon
        private List<MyWaypoint> GetRoute(MyWaypoint startPoint, MyWaypoint endPoint)
        {
            // build URL
            string waypointString = startPoint.Latitude.ToString() + "," + startPoint.Longitude.ToString() + ":" + endPoint.Latitude.ToString() + "," + endPoint.Longitude.ToString();
            string urlString = "https://atlas.microsoft.com/route/directions/json?subscription-key=" + m_azureMapKey + "&api-version=1.0&query=" + waypointString + "&travelMode=car";
            Uri routeRequest = new Uri(urlString);

            // send request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(routeRequest);
            request.Method = "GET";

            // get response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string returnString = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    returnString = sr.ReadToEnd();
                }
            }

            List<MyWaypoint> waypoints = new List<MyWaypoint>();

            try
            {
                var returnJson = (JObject)JsonConvert.DeserializeObject(returnString);
                var points = returnJson["routes"][0]["legs"][0]["points"];

                foreach (var point in points)
                {
                    MyWaypoint thisPt = new MyWaypoint();
                    thisPt.Latitude = Convert.ToDouble(point["latitude"]);
                    thisPt.Longitude = Convert.ToDouble(point["longitude"]);

                    waypoints.Add(thisPt);
                }
            
                return waypoints;
            }
            catch (Exception)
            {
                MessageBox.Show("No route found from " + fromTextBox.Text + " to " + toTextBox.Text);
                return null;
            }
            
        }

        // update points pull-down to include all Facilities, Targets and Place
        private void trafficDirectionsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trafficDirectionsTabControl.SelectedIndex == 1)
            {
                IAgStkObjectElementCollection facs = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eFacility);
                IAgStkObjectElementCollection tgts = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.eTarget);
                IAgStkObjectElementCollection plcs = m_root.CurrentScenario.Children.GetElements(AgESTKObjectType.ePlace);

                fromObjectComboBox.Items.Clear();
                toObjectComboBox.Items.Clear();

                foreach (IAgStkObject thisFac in facs)
                {
                    fromObjectComboBox.Items.Add("Facility/" + thisFac.InstanceName);
                    toObjectComboBox.Items.Add("Facility/" + thisFac.InstanceName);
                }
                
                foreach (IAgStkObject thisTgt in tgts)
                {
                    fromObjectComboBox.Items.Add("Target/" + thisTgt.InstanceName);
                    toObjectComboBox.Items.Add("Target/" + thisTgt.InstanceName);
                }

                foreach (IAgStkObject thisPlc in plcs)
                {
                    fromObjectComboBox.Items.Add("Place/" + thisPlc.InstanceName);
                    toObjectComboBox.Items.Add("Place/" + thisPlc.InstanceName);
                }

                // populate default
                if (fromObjectComboBox.Items.Count > 0)
                {
                    fromObjectComboBox.SelectedIndex = 0;
                    toObjectComboBox.SelectedIndex = 0;
                }
                
            }
        }
    }
}

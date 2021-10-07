using AGI.STKObjects;
using AGI.STKObjects.Astrogator;
using AGI.STKUtil;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OperatorsToolbox.FacilityCreator;

namespace OperatorsToolbox
{
    public class CreatorFunctions
    {
        //Get/Create Functions
        public static IAgStkObject GetCreateFacility(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Facility/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Facility/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreatePlace(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Place/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlace, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Place/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateTarget(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Target/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eTarget, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Target/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateSatellite(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Satellite/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Satellite/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateAircraft(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Aircraft/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Aircraft/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateMissile(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Missile/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eMissile, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Missile/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateSensor(IAgStkObject parent, string objectName)
        {
            IAgStkObject obj = null;
            string className = parent.ClassName;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */" + className + "/" + parent.InstanceName + "/Sensor/" + objectName);
                if (result[0] == "0")
                {
                    obj = parent.Children.New(AgESTKObjectType.eSensor, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath(className + "/" + parent.InstanceName + "/Sensor/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateTransmitter(IAgStkObject parent, string objectName)
        {
            IAgStkObject obj = null;
            string className = parent.ClassName;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */" + className + "/" + parent.InstanceName + "/Transmitter/" + objectName);
                if (result[0] == "0")
                {
                    obj = parent.Children.New(AgESTKObjectType.eTransmitter, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath(className + "/" + parent.InstanceName + "/Transmitter/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateReceiver(IAgStkObject parent, string objectName)
        {
            IAgStkObject obj = null;
            string className = parent.ClassName;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */" + className + "/" + parent.InstanceName + "/Receiver/" + objectName);
                if (result[0] == "0")
                {
                    obj = parent.Children.New(AgESTKObjectType.eReceiver, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath(className + "/" + parent.InstanceName + "/Receiver/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateRadar(IAgStkObject parent, string objectName)
        {
            IAgStkObject obj = null;
            string className = parent.ClassName;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */" + className + "/" + parent.InstanceName + "/Radar/" + objectName);
                if (result[0] == "0")
                {
                    obj = parent.Children.New(AgESTKObjectType.eRadar, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath(className + "/" + parent.InstanceName + "/Radar/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateConstellation(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Constellation/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eConstellation, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Constellation/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;

        }

        public static IAgStkObject GetCreateCommSystem(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */CommSystem/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eCommSystem, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("CommSystem/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;

        }

        public static IAgStkObject GetCreateCoverageDefinition(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */CoverageDefinition/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eCoverageDefinition, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("CoverageDefinition/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;

        }

        public static IAgStkObject GetCreateFigureOfMerit(IAgStkObject parent, string objectName)
        {
            IAgStkObject obj = null;
            string className = parent.ClassName;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */" + className + "/" + parent.InstanceName + "/FigureOfMerit/" + objectName);
                if (result[0] == "0")
                {
                    obj = parent.Children.New(AgESTKObjectType.eFigureOfMerit, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath(className + "/" + parent.InstanceName + "/FigureOfMerit/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static IAgStkObject GetCreateAreaTarget(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */AreaTarget/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eAreaTarget, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("AreaTarget/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;

        }

        public static IAgStkObject GetCreateChain(string objectName)
        {
            IAgStkObject obj = null;
            try
            {
                IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Chain/" + objectName);
                if (result[0] == "0")
                {
                    obj = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eChain, objectName);
                }
                else
                {
                    obj = CommonData.StkRoot.GetObjectFromPath("Chain/" + objectName);
                }
            }
            catch (Exception)
            {

            }
            return obj;

        }

        public static IAgStkObject GetCreateGroundLocation(GroundLocation location, string className)
        {
            IAgStkObject newLocation = null;
            if (className == "Facility")
            {
                newLocation = GetCreateFacility(location.LocationName);
                IAgFacility fac = newLocation as IAgFacility;
                fac.Position.AssignGeodetic(location.Latitude, location.Longitude, location.Altitude);
            }
            else if (className == "Place")
            {
                newLocation = GetCreatePlace(location.LocationName);
                IAgPlace place = newLocation as IAgPlace;
                place.Position.AssignGeodetic(location.Latitude, location.Longitude, location.Altitude);
            }
            else if (className == "Target")
            {
                newLocation = GetCreateTarget(location.LocationName);
                IAgTarget tar = newLocation as IAgTarget;
                tar.Position.AssignGeodetic(location.Latitude, location.Longitude, location.Altitude);
            }
            return newLocation;
        }

        //Constraint functions
        public static void SetCnstMinMax(IAgAccessCnstrMinMax constraint, double min,double max)
        {
            constraint.EnableMax = true;
            constraint.Max = max;
            constraint.EnableMin = true;
            constraint.Min = min;        
        }

        public static IAgAccessCnstrMinMax GetRangeCnst(IAgAccessConstraintCollection constraints)
        {
            IAgAccessCnstrMinMax rangeConstraint;
            if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrRange))
            {
                rangeConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrRange) as IAgAccessCnstrMinMax;
            }
            else
            {
                rangeConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrRange) as IAgAccessCnstrMinMax;
            }

            return rangeConstraint;
        }

        public static IAgAccessCnstrMinMax GetElCnst(IAgAccessConstraintCollection constraints)
        {
            IAgAccessCnstrMinMax elConstraint;
            if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrElevationAngle))
            {
                elConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrElevationAngle) as IAgAccessCnstrMinMax;
            }
            else
            {
                elConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrElevationAngle) as IAgAccessCnstrMinMax;
            }

            return elConstraint;
        }

        public static IAgAccessCnstrMinMax GetAzCnst(IAgAccessConstraintCollection constraints)
        {
            IAgAccessCnstrMinMax azConstraint;
            if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrAzimuthAngle))
            {
                azConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrAzimuthAngle) as IAgAccessCnstrMinMax;
            }
            else
            {
                azConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrAzimuthAngle) as IAgAccessCnstrMinMax;
            }

            return azConstraint;
        }

        public static IAgAccessCnstrMinMax GetSunElCnst(IAgAccessConstraintCollection constraints)
        {
            IAgAccessCnstrMinMax elConstraint;
            if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrSunElevationAngle))
            {
                elConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrSunElevationAngle) as IAgAccessCnstrMinMax;
            }
            else
            {
                elConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrSunElevationAngle) as IAgAccessCnstrMinMax;
            }

            return elConstraint;
        }

        public static IAgAccessCnstrMinMax GetAltCnst(IAgAccessConstraintCollection constraints)
        {
            IAgAccessCnstrMinMax altConstraint;
            if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrAltitude))
            {
                altConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrAltitude) as IAgAccessCnstrMinMax;
            }
            else
            {
                altConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrAltitude) as IAgAccessCnstrMinMax;
            }

            return altConstraint;
        }

        public static IAgAccessCnstrAngle GetLunExCnst(IAgAccessConstraintCollection constraints)
        {
            IAgAccessCnstrAngle lunConstraint;
            if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrLOSLunarExclusion))
            {
                lunConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrLOSLunarExclusion) as IAgAccessCnstrAngle;
            }
            else
            {
                lunConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrLOSLunarExclusion) as IAgAccessCnstrAngle;
            }

            return lunConstraint;
        }

        public static IAgAccessCnstrAngle GetSunExCnst(IAgAccessConstraintCollection constraints)
        {
            IAgAccessCnstrAngle sunConstraint;
            if (constraints.IsConstraintActive(AgEAccessConstraints.eCstrLOSSunExclusion))
            {
                sunConstraint = constraints.GetActiveConstraint(AgEAccessConstraints.eCstrLOSSunExclusion) as IAgAccessCnstrAngle;
            }
            else
            {
                sunConstraint = constraints.AddConstraint(AgEAccessConstraints.eCstrLOSSunExclusion) as IAgAccessCnstrAngle;
            }

            return sunConstraint;
        }

        //Interface population functions
        public static List<GroundLocation> PopulateGroundObjectList(ListView view, string className)
        {
            List<GroundLocation> locations = new List<GroundLocation>();
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            string simpleName;
            string classNameTemp;
            StringCollection objectNames = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string objectName in objectNames)
            {
                classNameTemp = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);
                if (classNameTemp == className)
                {
                    GroundLocation current = new GroundLocation();
                    ListViewItem item = new ListViewItem();
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    if (view.FindItemWithText(simpleName)==null)
                    {
                        current.LocationName = simpleName;
                        string simplePath = mStkObjectsLibrary.SimplifiedObjectPath(objectName);
                        current.SimplePath = simplePath;
                        List<double> location = GetGroundLocation(current.SimplePath);
                        current.Latitude = location[0];
                        current.Longitude = location[1];
                        current.Altitude = location[2];
                        locations.Add(current);
                        item.Text = simpleName;
                        view.Items.Add(item);
                    }
                }
            }

            return locations;
        }

        public static List<GroundLocation> PopulateGroundObjectList(string className)
        {
            List<GroundLocation> locations = new List<GroundLocation>();
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            string simpleName;
            string classNameTemp;
            StringCollection objectNames = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string objectName in objectNames)
            {
                classNameTemp = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);
                if (classNameTemp == className)
                {
                    GroundLocation current = new GroundLocation();
                    simpleName = mStkObjectsLibrary.ObjectName(objectName);
                    current.LocationName = simpleName;
                    string simplePath = mStkObjectsLibrary.SimplifiedObjectPath(objectName);
                    current.SimplePath = simplePath;
                    List<double> location = GetGroundLocation(current.SimplePath);
                    current.Latitude = location[0];
                    current.Longitude = location[1];
                    current.Altitude = location[2];
                    locations.Add(current);
                }
            }
            return locations;
        }

        public static List<double> GetGroundLocation(string path)
        {
            List<double> location = new List<double>();
            object lat = 0.0;
            object longi = 0.0;
            double alt = 0.0;
            if (path.Contains("Place"))
            {
                IAgPlace place = CommonData.StkRoot.GetObjectFromPath(path) as IAgPlace;
                place.Position.QueryPlanetodetic(out lat, out longi, out alt);
            }
            else if (path.Contains("Facility"))
            {
                IAgFacility place = CommonData.StkRoot.GetObjectFromPath(path) as IAgFacility;
                place.Position.QueryPlanetodetic(out lat, out longi, out alt);
            }
            else if (path.Contains("Target"))
            {
                IAgTarget place = CommonData.StkRoot.GetObjectFromPath(path) as IAgTarget;
                place.Position.QueryPlanetodetic(out lat, out longi, out alt);
            }
            location.Add(Double.Parse(lat.ToString()));
            location.Add(Double.Parse(longi.ToString()));
            location.Add(alt);
            return location;
        }

        public static void PopulateObjectListByClass(ListView view, string className)
        {
            view.Items.Clear();
            IAgExecCmdResult result;
            if (className =="Satellite")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = facName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Facility")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Facility");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = facName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Place")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Place");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = facName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Target")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Target");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = facName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Aircraft")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Aircraft");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = facName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Missile")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Missile");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = facName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "LaunchVehicle")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class LaunchVehicle");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        string facName = item.Split('/').Last();
                        if (facName != "" && facName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = facName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Sensor")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Sensor");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        string sensorName = item.Split('/').Last();
                        if (sensorName != "" && sensorName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = sensorName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Transmitter")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Transmitter");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        string sensorName = item.Split('/').Last();
                        if (sensorName != "" && sensorName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = sensorName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Receiver")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Receiver");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        string sensorName = item.Split('/').Last();
                        if (sensorName != "" && sensorName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = sensorName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Antenna")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Antenna");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        string sensorName = item.Split('/').Last();
                        if (sensorName != "" && sensorName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = sensorName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Radar")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Radar");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        string sensorName = item.Split('/').Last();
                        if (sensorName != "" && sensorName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = sensorName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
            else if (className == "Constellation")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Constellation");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        string sensorName = item.Split('/').Last();
                        if (sensorName != "" && sensorName != null)
                        {
                            var listItem = new ListViewItem();
                            listItem.Text = sensorName;
                            view.Items.Add(listItem);
                        }
                    }
                }
            }
        }

        public static List<string> PopulateCbByClass(ComboBox box, string className)
        {
            box.Items.Clear();
            //Returning full object path for each object
            List<string> objectPaths = new List<string>();
            StkObjectsLibrary library = new StkObjectsLibrary();
            IAgExecCmdResult result;
            if (className == "Satellite")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string facName = item.Split('/').Last();
                            if (facName != "" && facName != null)
                            {
                                box.Items.Add(facName);
                            }
                        }
                    }
                }
            }
            else if (className == "Facility")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Facility");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string facName = item.Split('/').Last();
                            if (facName != "" && facName != null)
                            {
                                box.Items.Add(facName);
                            }
                        }
                    }
                }
            }
            else if (className == "Place")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Place");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string facName = item.Split('/').Last();
                            if (facName != "" && facName != null)
                            {
                                box.Items.Add(facName);
                            }
                        }
                    }
                }
            }
            else if (className == "Target")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Target");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string facName = item.Split('/').Last();
                            if (facName != "" && facName != null)
                            {
                                box.Items.Add(facName);
                            }
                        }
                    }
                }
            }
            else if (className == "Aircraft")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Aircraft");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string facName = item.Split('/').Last();
                            if (facName != "" && facName != null)
                            {
                                box.Items.Add(facName);
                            }
                        }
                    }
                }
            }
            else if (className == "Missile")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Missile");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string facName = item.Split('/').Last();
                            if (facName != "" && facName != null)
                            {
                                box.Items.Add(facName);
                            }
                        }
                    }
                }
            }
            else if (className == "Sensor")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Sensor");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string sensorName = item.Split('/').Last();
                            if (sensorName != "" && sensorName != null)
                            {
                                box.Items.Add(sensorName);
                            }
                        }
                    }
                }
            }
            else if (className == "Receiver")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Receiver");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string sensorName = item.Split('/').Last();
                            if (sensorName != "" && sensorName != null)
                            {
                                box.Items.Add(sensorName);
                            }
                        }
                    }
                }
            }
            else if (className == "Transmitter")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Transmitter");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                            string sensorName = item.Split('/').Last();
                            if (sensorName != "" && sensorName != null)
                            {
                                box.Items.Add(sensorName);
                            }
                        }
                    }
                }
            }

            return objectPaths;

        }

        public static List<string> PopulateListByClass(string className)
        {
            //Returning full object path for each object
            List<string> objectPaths = new List<string>();
            StkObjectsLibrary library = new StkObjectsLibrary();
            IAgExecCmdResult result;
            if (className == "Satellite")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Facility")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Facility");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Place")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Place");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Target")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Target");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Aircraft")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Aircraft");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Missile")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Missile");
                if (result[0] != "None")
                {
                    string[] facArray = result[0].Split(null);
                    foreach (var item in facArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Sensor")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Sensor");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Receiver")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Receiver");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Transmitter")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Transmitter");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Radar")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Radar");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Constellation")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Constellation");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "LaunchVehicle")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class LaunchVehicle");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "GroundVehicle")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class GroundVehicle");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "Ship")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Ship");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            else if (className == "AreaTarget")
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class AreaTarget");
                if (result[0] != "None")
                {
                    string[] sensorArray = result[0].Split(null);
                    foreach (var item in sensorArray)
                    {
                        if (item != null && item != "")
                        {
                            objectPaths.Add(library.TruncatedObjectPath(item));
                        }
                    }
                }
            }
            return objectPaths;
        }

        //Satellite helpers
        public static void ChangeSatelliteInterval(IAgSatellite sat, string startTime, string stopTime, bool astgRun)
        {
            AgEVePropagatorType propType = sat.PropagatorType;
            //IAgVePropagator prop = sat.Propagator;

            switch (propType)
            {
                case AgEVePropagatorType.eUnknownPropagator:
                    break;
                case AgEVePropagatorType.ePropagatorHPOP:
                    IAgVePropagatorHPOP prop = sat.Propagator as IAgVePropagatorHPOP;
                    prop.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorJ2Perturbation:
                    IAgVePropagatorJ2Perturbation prop1 = sat.Propagator as IAgVePropagatorJ2Perturbation;
                    prop1.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop1.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorJ4Perturbation:
                    IAgVePropagatorJ4Perturbation prop2 = sat.Propagator as IAgVePropagatorJ4Perturbation;
                    prop2.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop2.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorLOP:
                    IAgVePropagatorLOP prop3 = sat.Propagator as IAgVePropagatorLOP;
                    prop3.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop3.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorSGP4:
                    IAgVePropagatorSGP4 prop4 = sat.Propagator as IAgVePropagatorSGP4;
                    prop4.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop4.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorSPICE:
                    IAgVePropagatorSPICE prop5 = sat.Propagator as IAgVePropagatorSPICE;
                    prop5.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop5.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorStkExternal:
                    IAgVePropagatorStkExternal prop6 = sat.Propagator as IAgVePropagatorStkExternal;
                    //prop6.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    break;
                case AgEVePropagatorType.ePropagatorTwoBody:
                    IAgVePropagatorTwoBody prop7 = sat.Propagator as IAgVePropagatorTwoBody;
                    prop7.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop7.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorUserExternal:
                    break;
                case AgEVePropagatorType.ePropagatorGreatArc:
                    IAgVePropagatorGreatArc prop8 = sat.Propagator as IAgVePropagatorGreatArc;
                    prop8.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop8.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorBallistic:
                    break;
                case AgEVePropagatorType.ePropagatorSimpleAscent:
                    break;
                case AgEVePropagatorType.ePropagatorAstrogator:
                    if (astgRun)
                    {
                        IAgVADriverMCS driver = sat.Propagator as IAgVADriverMCS;
                        driver.RunMCS();
                    }
                    break;
                case AgEVePropagatorType.ePropagatorRealtime:
                    break;
                case AgEVePropagatorType.ePropagatorGPS:
                    IAgVePropagatorGPS prop9 = sat.Propagator as IAgVePropagatorGPS;
                    prop9.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop9.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorAviator:
                    break;
                case AgEVePropagatorType.ePropagator11Param:
                    IAgVePropagator11Param prop10 = sat.Propagator as IAgVePropagator11Param;
                    prop10.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    prop10.Propagate();
                    break;
                case AgEVePropagatorType.ePropagatorSP3:
                    IAgVePropagatorSP3 prop11 = sat.Propagator as IAgVePropagatorSP3;
                    //prop11.EphemerisInterval.SetExplicitInterval(startTime, stopTime);
                    break;
                default:
                    break;
            }
            
        }

        public static void ChangeSatColor(string satPath, int satCatIndex)
        {
            if (!String.IsNullOrEmpty(satPath) && satCatIndex != -1)
            {
                string fofo = CommonData.SatCatItemList[satCatIndex].Fofo;
                dynamic sat = CommonData.StkRoot.GetObjectFromPath(satPath);
                IAgVeGfxAttributesBasic graphics = sat.Graphics.Attributes as IAgVeGfxAttributesBasic;
                string colorRgb = null;
                string colorBgr = null;
                if (fofo == "Blue")
                {
                    colorRgb = "00ffff";
                    graphics.Color = System.Drawing.Color.Cyan;
                }
                else if (fofo == "Red")
                {
                    colorRgb = "ff0000";
                    graphics.Color = System.Drawing.Color.Red;
                }
                else if (fofo == "Grey")
                {
                    colorRgb = "ffffff";
                    graphics.Color = System.Drawing.Color.White;
                }
                else if (fofo == "Green")
                {
                    colorRgb = "00ff00";
                    graphics.Color = System.Drawing.Color.Green;
                }
                else
                {
                    colorRgb = "ffff00";
                    graphics.Color = System.Drawing.Color.Yellow;
                }
            }
            //colorBGR = colorRGB.Substring(4, 5) +colorRGB.Substring(2,3)+colorRGB.Substring(0,1);
            //int decColor = Convert.ToInt32(colorBGR, 16);
        }

        public static void ChangeObjectColor(string objPath, CustomUserInterface.ColorOptions option)
        {
            dynamic sat = CommonData.StkRoot.GetObjectFromPath(objPath);
            dynamic graphics;
            try
            {
                graphics = sat.Graphics.Attributes as IAgVeGfxAttributesBasic;
            }
            catch (Exception)
            {
                graphics = sat.Graphics;
            }
            switch (option)
            {
                case CustomUserInterface.ColorOptions.Blue:
                    graphics.Color = System.Drawing.Color.Blue;
                    break;
                case CustomUserInterface.ColorOptions.Cyan:
                    graphics.Color = System.Drawing.Color.Cyan;
                    break;
                case CustomUserInterface.ColorOptions.Red:
                    graphics.Color = System.Drawing.Color.Red;
                    break;
                case CustomUserInterface.ColorOptions.Green:
                    graphics.Color = System.Drawing.Color.Green;
                    break;
                case CustomUserInterface.ColorOptions.Yellow:
                    graphics.Color = System.Drawing.Color.Yellow;
                    break;
                case CustomUserInterface.ColorOptions.Orange:
                    graphics.Color = System.Drawing.Color.Orange;
                    break;
                case CustomUserInterface.ColorOptions.Purple:
                    graphics.Color = System.Drawing.Color.Purple;
                    break;
                case CustomUserInterface.ColorOptions.White:
                    graphics.Color = System.Drawing.Color.White;
                    break;
                default:
                    break;
            }
        }
    }
}

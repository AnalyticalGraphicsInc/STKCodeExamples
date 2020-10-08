using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

namespace OperatorsToolbox.SmartView
{
    public static class SmartViewFunctions
    {
        public static void Change3DView(ViewData view)
        {
            IAgSatellite sat;
            IAgExecCmdResult result;
            string cmd = null;
            //Change View
            int windowId = GetWindowId(view.WindowName,1);
            SetAnimation(view);
            if (view.UseStoredView)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO * UseStoredView \"" + view.StoredViewName + "\" " + windowId.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not Modify View");
                }
            }
            else
            {
                try
                {
                    if (view.ViewTarget == "CentralBody/Earth")
                    {
                        CommonData.StkRoot.ExecuteCommand("VO * View Home");
                    }
                    else
                    {
                        string objectPath = view.ViewTarget;
                        cmd = "VO * View FromTo FromRegName \"STK Object\" FromName \"" + objectPath + "\" ToRegName  \"STK Object\" ToName  \"" + objectPath + "\" WindowID " + windowId.ToString();
                        CommonData.StkRoot.ExecuteCommand(cmd);
                        CommonData.StkRoot.ExecuteCommand("VO * View Top WindowID " + windowId.ToString());
                        CommonData.StkRoot.ExecuteCommand("VO * View Zoom WindowID " + windowId.ToString() + " FractionofCB -1");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not Modify View");
                }
            }
            //Update Satellite Graphics
            if (view.EnableUniversalOrbitTrack)
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
                string[] newSatArray = null;
                if (result[0] != "None")
                {
                    newSatArray = result[0].Split(null);
                }
                if (newSatArray != null)
                {
                    foreach (var item in newSatArray)
                    {
                        if (item != "")
                        {
                            string newItem = item.Split('/').Last();
                            string objPath = "Satellite/" + newItem;
                            sat = CommonData.StkRoot.GetObjectFromPath(objPath) as IAgSatellite;
                            cmd = "VO */" + objPath + " Pass3d Inherit Off OrbitLead " + view.LeadType + " OrbitTrail " + view.TrailType;
                            try
                            {
                                CommonData.StkRoot.ExecuteCommand(cmd);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Could not update Lead/Trail Settings");
                            }
                        }
                    }
                }
            }
            foreach (ObjectData item in CommonData.InitialObjectData)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO " + item.SimplePath + " DynDataText RemoveAll");
                }
                catch (Exception)
                {

                }
            }

            if (view.DataDisplayActive)
            {
                try
                {
                    if (view.DataDisplayReportName == "RIC")
                    {
                        cmd = "VO */" + view.DataDisplayObject + " DynDataText DataDisplay \"" + view.DataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.DataDisplayLocation + " Window " + windowId.ToString() + " PreData \"" + view.ViewTarget + "\"";
                        CommonData.StkRoot.ExecuteCommand(cmd);
                        CommonData.PreviousDataDisplayObject = view.DataDisplayObject;
                    }
                    else
                    {
                        cmd = "VO */" + view.DataDisplayObject + " DynDataText DataDisplay \"" + view.DataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.DataDisplayLocation + " Window " + windowId.ToString();
                        CommonData.StkRoot.ExecuteCommand(cmd);
                        CommonData.PreviousDataDisplayObject = view.DataDisplayObject;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not create data display");
                }
            }

            if (view.ObjectHideShow)
            {
                try
                {
                    if (view.ViewObjectData.Count != 0)
                    {
                        foreach (ObjectData item in view.ViewObjectData)
                        {
                            SetObjectVisibility(item);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not update Hide/Show Options");
                }
            }
        }

        public static void Change2DView(ViewData view)
        {
            IAgSatellite sat;
            IAgExecCmdResult result;
            string cmd = null;
            int windowId = GetWindowId(view.WindowName, 0);
            SetAnimation(view);
            try
            {
                if (view.ViewType2D == "SpecifyCenter")
                {
                    cmd = "Zoom * LatLon " + view.ZoomCenterLat + " " + view.ZoomCenterLong + " " + view.ZoomCenterDelta + " " + windowId;
                }
                else if (view.ViewType2D == "ObjectCenter")
                {
                    cmd = "Zoom * Object */" + view.ViewTarget + " " + view.ZoomCenterDelta + " " + windowId;
                }
                else
                {
                    cmd = "Zoom * AllOut";
                }
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not re-center view");
            }

            //Update Satellite Graphics
            if (view.EnableUniversalGroundTrack)
            {
                result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Satellite");
                string[] newSatArray = null;
                if (result[0] != "None")
                {
                    newSatArray = result[0].Split(null);
                }
                if (newSatArray != null)
                {
                    foreach (var item in newSatArray)
                    {
                        if (item != "")
                        {
                            string newItem = item.Split('/').Last();
                            string objPath = "Satellite/" + newItem;
                            sat = CommonData.StkRoot.GetObjectFromPath(objPath) as IAgSatellite;
                            cmd = "Graphics */" + objPath + " Pass2D GrndLead " + view.LeadType + " GrndTrail " + view.TrailType;
                            try
                            {
                                CommonData.StkRoot.ExecuteCommand(cmd);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Could not update Lead/Trail Settings");
                            }
                        }
                    }
                }
            }

            if (view.ShowGroundSensors)
            {

            }

            if (view.ShowAerialSensors)
            {

            }

            if (view.ObjectHideShow)
            {
                try
                {
                    if (view.ViewObjectData.Count != 0)
                    {
                        foreach (ObjectData item in view.ViewObjectData)
                        {
                            SetObjectVisibility(item);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not update Hide/Show Options");
                }
            }
        }

        public static void ChangeTargetThreatView(ViewData view)
        {
            IAgSatellite sat;
            IAgExecCmdResult result;
            string cmd = null;
            int windowId = GetWindowId(view.WindowName, 1);
            SetAnimation(view);

            try
            {
                string objectPath = view.TargetSatellite;
                cmd = "VO * View FromTo FromRegName \"STK Object\" FromName \"" + objectPath + "\" ToRegName  \"STK Object\" ToName  \"" + objectPath + "\" WindowID " + windowId.ToString();
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommonData.StkRoot.ExecuteCommand("VO * View Top WindowID " + windowId.ToString());
                CommonData.StkRoot.ExecuteCommand("VO * View Zoom WindowID " + windowId.ToString() + " FractionofCB -0.3");
            }
            catch (Exception)
            {

            }

            string objPath = view.TargetSatellite;
            string objPath1 = view.TargetSatellite;
            sat = CommonData.StkRoot.GetObjectFromPath(objPath) as IAgSatellite;
            sat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
            sat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
            cmd = "VO */" + objPath + " Pass3d Inherit Off OrbitLead OnePass OrbitTrail SameAsLead";
            try
            {
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not update Lead/Trail Settings");
            }

            foreach (var item in view.ThreatSatNames)
            {
                if (item != "")
                {
                    objPath = item;
                    sat = CommonData.StkRoot.GetObjectFromPath(objPath) as IAgSatellite;
                    cmd = "VO */" + objPath + " Pass3d Inherit Off OrbitLead None OrbitTrail Quarter";
                    try
                    {
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not update Lead/Trail Settings");
                    }
                }
            }

            if (view.EnableProximityBox)
            {
                try
                {
                    sat = CommonData.StkRoot.GetObjectFromPath(objPath1) as IAgSatellite;
                    IAgStkObject sat1 = CommonData.StkRoot.GetObjectFromPath(objPath1);
                    IAgCrdnPlaneNormal plane = sat1.Vgt.Planes.Factory.Create("ProximityPlane", "", AgECrdnPlaneType.eCrdnPlaneTypeNormal) as IAgCrdnPlaneNormal;
                    //IAgVORefCrdn newPlane = plane as IAgVORefCrdn;
                    //string planeName = newPlane.Name;
                    plane.NormalVector.SetPath(view.TargetSatellite + " Nadir(Detic)");
                    plane.ReferencePoint.SetPath(view.TargetSatellite + " Center");
                    plane.ReferenceVector.SetPath(view.TargetSatellite + " Velocity");

                    sat.VO.Vector.RefCrdns.Add(AgEGeometricElemType.ePlaneElem, objPath1 + " ProximityPlane");
                    IAgVORefCrdn newPlane = sat.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.ePlaneElem, objPath1 + " ProximityPlane Plane");
                    newPlane.Visible = true;
                    newPlane.LabelVisible = false;
                    newPlane.Color = System.Drawing.Color.LimeGreen;
                    sat.VO.Vector.VectorSizeScale = 5;
                    IAgVORefCrdnPlane newPlane1 = newPlane as IAgVORefCrdnPlane;
                    newPlane1.CircGridVisible = true;
                    newPlane1.Size = 3;
                    newPlane1.PlaneGridSpacing = 50; //km
                    newPlane1.TransparentPlaneVisible = true;
                    newPlane1.DrawAtObject = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not create proximity box");
                }
            }

            if (view.EnableProximityEllipsoid)
            {
                try
                {
                    sat = CommonData.StkRoot.GetObjectFromPath(objPath1) as IAgSatellite;
                    IAgStkObject sat1 = CommonData.StkRoot.GetObjectFromPath(objPath1);
                    sat.VO.Proximity.Ellipsoid.IsVisible = true;
                    sat.VO.Proximity.Ellipsoid.XSemiAxisLength = Double.Parse(view.EllipsoidX);
                    sat.VO.Proximity.Ellipsoid.YSemiAxisLength = Double.Parse(view.EllipsoidY);
                    sat.VO.Proximity.Ellipsoid.ZSemiAxisLength = Double.Parse(view.EllipsoidZ);
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not create proximity ellipsoid");
                }
            }

            foreach (ObjectData item in CommonData.InitialObjectData)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO " + item.SimplePath + " DynDataText RemoveAll");
                }
                catch (Exception)
                {

                }
            }
            //if (CommonData.PreviousDataDisplayObject != null)
            //{
            //    try
            //    {
            //        CommonData.StkRoot.ExecuteCommand("VO */" + CommonData.PreviousDataDisplayObject + " DynDataText RemoveAll");
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}

            if (view.TtDataDisplayActive)
            {
                if (view.TtDataDisplayObject=="AllThreat")
                {
                    List<string> locations = new List<string>();
                    int count = 0;
                    locations.Add("TopLeft"); locations.Add("TopRight"); locations.Add("BottomLeft"); locations.Add("BottomRight");

                    foreach (string item in view.ThreatSatNames)
                    {
                        if (count<4)
                        {
                            try
                            {
                                if (view.TtDataDisplayReportName=="RIC")
                                {
                                    cmd = "VO */" + view.ThreatSatNames[count] + " DynDataText DataDisplay \"" + view.TtDataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + locations[count] + " Window " + windowId.ToString() + " PreData \"" + view.TargetSatellite + "\"";
                                    CommonData.StkRoot.ExecuteCommand(cmd);
                                    CommonData.PreviousDataDisplayObject = view.TtDataDisplayObject;
                                }
                                else
                                {
                                    cmd = "VO */" + view.ThreatSatNames[count] + " DynDataText DataDisplay \"" + view.TtDataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + locations[count] + " Window " + windowId.ToString();
                                    CommonData.StkRoot.ExecuteCommand(cmd);
                                    CommonData.PreviousDataDisplayObject = view.TtDataDisplayObject;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        count++;
                    }
                }
                else
                {
                    try
                    {
                        if (view.TtDataDisplayReportName == "RIC")
                        {
                            cmd = "VO */" + view.TtDataDisplayObject + " DynDataText DataDisplay \"" + view.TtDataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.TtDataDisplayLocation + " Window " + windowId.ToString() + " PreData \"" + view.TargetSatellite+"\"";
                            CommonData.StkRoot.ExecuteCommand(cmd);
                            CommonData.PreviousDataDisplayObject = view.TtDataDisplayObject;
                        }
                        else
                        {
                            cmd = "VO */" + view.TtDataDisplayObject + " DynDataText DataDisplay \"" + view.TtDataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.TtDataDisplayLocation + " Window " + windowId.ToString();
                            CommonData.StkRoot.ExecuteCommand(cmd);
                            CommonData.PreviousDataDisplayObject = view.TtDataDisplayObject;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not create data display");
                    }
                }
            }

            if (view.ObjectHideShow)
            {
                try
                {
                    if (view.ViewObjectData.Count != 0)
                    {
                        foreach (ObjectData item in view.ViewObjectData)
                        {
                            SetObjectVisibility(item);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not update Hide/Show Options");
                }
            }

        }

        public static void ChangeGeoDriftView(ViewData view)
        {
            IAgSatellite sat;
            IAgExecCmdResult result;
            string cmd = null;
            int windowId = GetWindowId(view.WindowName, 1);
            SetAnimation(view);
            try
            {
                double radius = Double.Parse(view.GeoRadius);
                double alt = radius - 6371;
                cmd = "VO * View FromTo FromToRegName \"Latitude, Longitude, Altitude\" FromToName \"Latitude, Longitude, Altitude\" FromToCallData \"Lat 0.0 Lon "+view.GeoLongitude+" Alt "+alt.ToString()+"\"";
                string objectPath = view.ViewTarget;
                //cmd = "VO * View FromTo FromRegName \"STK Object\" FromName \"" + objectPath + "\" ToRegName  \"STK Object\" ToName  \"" + objectPath + "\" WindowID " + windowID.ToString();
                CommonData.StkRoot.ExecuteCommand(cmd);
                CommonData.StkRoot.ExecuteCommand("VO * View Top WindowID " + windowId.ToString());
                CommonData.StkRoot.ExecuteCommand("VO * View Zoom WindowID " + windowId.ToString() + " FractionofCB -7.4");
            }
            catch (Exception)
            {

            }
            string objPath = view.ViewTarget;
            sat = CommonData.StkRoot.GetObjectFromPath(objPath) as IAgSatellite;
            sat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
            sat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
            cmd = "VO */" + objPath + " Pass3d Inherit Off OrbitLead Quarter OrbitTrail All";
            try
            {
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not update Lead/Trail Settings");
            }

            if (view.EnableGeoBox)
            {
                sat.VO.Proximity.GeoBox.IsVisible = true;
                sat.VO.Proximity.GeoBox.Longitude = Double.Parse(view.GeoLongitude);
                sat.VO.Proximity.GeoBox.Radius = Double.Parse(view.GeoRadius);
                sat.VO.Proximity.GeoBox.NorthSouth = Double.Parse(view.GeoNorthSouth);
                sat.VO.Proximity.GeoBox.EastWest = Double.Parse(view.GeoEastWest);
                sat.VO.Proximity.GeoBox.Color = System.Drawing.Color.Red;
            }
            
            foreach (ObjectData item in CommonData.InitialObjectData)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("VO " + item.SimplePath + " DynDataText RemoveAll");
                }
                catch (Exception)
                {

                }
            }

            if (view.GeoDataDisplayActive)
            {
                try
                {

                    cmd = "VO */" + view.GeoDataDisplayObject + " DynDataText DataDisplay \"" + view.GeoDataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.GeoDataDisplayLocation + " Window " + windowId.ToString();
                    CommonData.StkRoot.ExecuteCommand(cmd);
                    CommonData.PreviousDataDisplayObject = view.GeoDataDisplayObject;
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not create data display");
                }
            }

            if (view.ObjectHideShow)
            {
                try
                {
                    if (view.ViewObjectData.Count != 0)
                    {
                        foreach (ObjectData item in view.ViewObjectData)
                        {
                            SetObjectVisibility(item);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not update Hide/Show Options");
                }
            }
        }

        public static void SetAnimation(ViewData view)
        {
            if (view.UseAnimationTime)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("SetAnimation * CurrentTime \"" + view.AnimationTime + "\"");
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not change animation time");
                }
            }
        }

        public static int GetWindowId(string windowName, int windowType)
        {
            //window type is 0 for 2D and 1 for 3D
            int windowId = -1;
            IAgExecCmdResult result=null;
            string newItem = null;

            if (windowType==0)
            {
                result = CommonData.StkRoot.ExecuteCommand("MapID_R *");
            }
            else
            {
                result = CommonData.StkRoot.ExecuteCommand("VO_R * MapID");
            }
            string[] windowInfo = result[0].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in windowInfo)
            {
                if (item!=null && item!=" ")
                {
                    newItem = item.Replace("\n", "");
                    string[] windowInfoParts = newItem.Split(new string[] { " - " },2, StringSplitOptions.RemoveEmptyEntries);
                    if (windowInfoParts[1] == windowName)
                    {
                        windowId = Int32.Parse(windowInfoParts[0]);
                    }
                }
            }
            if (windowId==-1)
            {
                windowId = 1;
            }
            return windowId;
        }

        public static List<string> GetWindowNames(int windowType)
        {
            List<string> windowNames = new List<string>();
            IAgExecCmdResult result = null;

            if (windowType == 0)
            {
                result = CommonData.StkRoot.ExecuteCommand("MapID_R *");
            }
            else
            {
                result = CommonData.StkRoot.ExecuteCommand("VO_R * MapID");
            }
            string[] windowInfo = result[0].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in windowInfo)
            {
                if (item!=null &&item!=" ")
                {
                    string[] windowInfoParts = item.Split(new string[] { " - " },2, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        string newName = windowInfoParts[1].Replace("\n", "");
                        windowNames.Add(newName);
                    }
                    catch (Exception)
                    {
                        windowNames.Add(windowInfoParts[1]);
                    }
                }
            }
            return windowNames;
        }

        public static string GetObjectPath(string name)
        {
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            string objectPath = null;
            string simpleName;
            string className;
            simpleName = name;
            StringCollection objectPaths = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string path in objectPaths)
            {
                string objectName = mStkObjectsLibrary.ObjectName(path);
                className = mStkObjectsLibrary.ClassNameFromObjectPath(path);
                if (objectName == simpleName && className != "Scenario")
                {
                    className = mStkObjectsLibrary.ClassNameFromObjectPath(path);
                    objectPath = className + "/" + objectName;
                }
            }

            return objectPath;
        }

        public static string GetClassName(string name)
        {
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            string simpleName;
            string classNameTemp=null;
            string className = null;
            simpleName = name;
            StringCollection objectPaths = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            if (simpleName.Contains("Earth"))
            {
                className = "CentralBody";
            }
            else
            {
                foreach (string path in objectPaths)
                {
                    string objectName = mStkObjectsLibrary.ObjectName(path);
                    classNameTemp = mStkObjectsLibrary.ClassNameFromObjectPath(path);
                    if (objectName == simpleName && className != "Scenario")
                    {
                        className = mStkObjectsLibrary.ClassNameFromObjectPath(path);
                    }
                }
            }
            return className;
        }

        public static bool GetObjectVisibility(string className,string simplePath)
        {
            bool visible = false;
            if (className == "Satellite")
            {
                IAgSatellite myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgSatellite;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "Aircraft")
            {
                IAgAircraft myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgAircraft;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "Facility")
            {
                IAgFacility myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgFacility;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "Missile")
            {
                IAgMissile myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgMissile;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "GroundVehicle")
            {
                IAgGroundVehicle myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgGroundVehicle;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "Sensor")
            {
                IAgSensor myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgSensor;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "Transmitter")
            {
                IAgTransmitter myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgTransmitter;
                visible = myObject.Graphics.Show;
            }
            else if (className == "Receiver")
            {
                IAgReceiver myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgReceiver;
                visible = myObject.Graphics.Show;
            }
            else if (className == "CoverageDefinition")
            {
                IAgCoverageDefinition myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgCoverageDefinition;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "FigureOfMerit")
            {
                IAgFigureOfMerit myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgFigureOfMerit;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "CommSystem")
            {
                IAgCommSystem myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgCommSystem;
                visible = myObject.Graphics.Show;
            }
            else if (className == "Chain")
            {
                IAgChain myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgChain;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "Place")
            {
                IAgPlace myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgPlace;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }
            else if (className == "AreaTarget")
            {
                IAgAreaTarget myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgAreaTarget;
                visible = myObject.Graphics.IsObjectGraphicsVisible;
            }

            return visible;
        }

        public static void SetObjectVisibility(ObjectData objectData)
        {
            string className = objectData.ClassName;
            string simplePath = objectData.SimplePath;
            if (className == "Satellite")
            {
                IAgSatellite myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgSatellite;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "Aircraft")
            {
                IAgAircraft myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgAircraft;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "Facility")
            {
                IAgFacility myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgFacility;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "Missile")
            {
                IAgMissile myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgMissile;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "GroundVehicle")
            {
                IAgGroundVehicle myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgGroundVehicle;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "Sensor")
            {
                IAgSensor myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgSensor;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "Transmitter")
            {
                IAgTransmitter myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgTransmitter;
                if (objectData.HideShow)
                {
                    myObject.Graphics.Show = true;
                }
                else
                {
                    myObject.Graphics.Show = false;
                }
            }
            else if (className == "Receiver")
            {
                IAgReceiver myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgReceiver;
                if (objectData.HideShow)
                {
                    myObject.Graphics.Show = true;
                }
                else
                {
                    myObject.Graphics.Show = false;
                }
            }
            else if (className == "CoverageDefinition")
            {
                IAgCoverageDefinition myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgCoverageDefinition;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "FigureOfMerit")
            {
                IAgFigureOfMerit myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgFigureOfMerit;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "CommSystem")
            {
                IAgCommSystem myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgCommSystem;
                if (objectData.HideShow)
                {
                    myObject.Graphics.Show = true;
                }
                else
                {
                    myObject.Graphics.Show = false;
                }
            }
            else if (className == "Chain")
            {
                IAgChain myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgChain;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "Place")
            {
                IAgPlace myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgPlace;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
            else if (className == "AreaTarget")
            {
                IAgAreaTarget myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgAreaTarget;
                if (objectData.HideShow)
                {
                    myObject.Graphics.IsObjectGraphicsVisible = true;
                }
                else
                {
                    myObject.Graphics.IsObjectGraphicsVisible = false;
                }
            }
        }

        public static List<AgELeadTrailData> GetLeadTrailData(string objectPath,string className)
        {
            List<AgELeadTrailData> leadTrailData = new List<AgELeadTrailData>();

            if (className=="Satellite")
            {
                IAgSatellite mySat = CommonData.StkRoot.GetObjectFromPath(objectPath) as IAgSatellite;
                leadTrailData.Add(mySat.VO.Pass.TrackData.PassData.Orbit.LeadDataType);
                leadTrailData.Add(mySat.VO.Pass.TrackData.PassData.Orbit.TrailDataType);
                leadTrailData.Add(mySat.Graphics.PassData.GroundTrack.LeadDataType);
                leadTrailData.Add(mySat.Graphics.PassData.GroundTrack.TrailDataType);
            }
            else if (className == "Aircraft")
            {
                IAgAircraft myAircraft = CommonData.StkRoot.GetObjectFromPath(objectPath) as IAgAircraft;
                leadTrailData.Add(myAircraft.VO.Route.TrackData.LeadDataType);
                leadTrailData.Add(myAircraft.VO.Route.TrackData.TrailDataType);
                leadTrailData.Add(myAircraft.Graphics.PassData.Route.LeadDataType);
                leadTrailData.Add(myAircraft.Graphics.PassData.Route.TrailDataType);
            }
            else if (className == "Missile")
            {
                IAgMissile myMissile = CommonData.StkRoot.GetObjectFromPath(objectPath) as IAgMissile;
                leadTrailData.Add(myMissile.VO.Trajectory.TrackData.PassData.Trajectory.LeadDataType);
                leadTrailData.Add(myMissile.VO.Trajectory.TrackData.PassData.Trajectory.TrailDataType);
                leadTrailData.Add(myMissile.Graphics.PassData.Trajectory.LeadDataType);
                leadTrailData.Add(myMissile.Graphics.PassData.Trajectory.TrailDataType);
            }
            else if (className == "GroundVehicle")
            {
                IAgGroundVehicle myGv = CommonData.StkRoot.GetObjectFromPath(objectPath) as IAgGroundVehicle;
                leadTrailData.Add(myGv.VO.Route.TrackData.LeadDataType);
                leadTrailData.Add(myGv.VO.Route.TrackData.TrailDataType);
                leadTrailData.Add(myGv.Graphics.PassData.Route.LeadDataType);
                leadTrailData.Add(myGv.Graphics.PassData.Route.TrailDataType);
            }
            else if (className == "Ship")
            {
                IAgShip myShip = CommonData.StkRoot.GetObjectFromPath(objectPath) as IAgShip;
                leadTrailData.Add(myShip.VO.Route.TrackData.LeadDataType);
                leadTrailData.Add(myShip.VO.Route.TrackData.TrailDataType);
                leadTrailData.Add(myShip.Graphics.PassData.Route.LeadDataType);
                leadTrailData.Add(myShip.Graphics.PassData.Route.TrailDataType);
            }
            else if (className == "LaunchVehicle")
            {
                IAgLaunchVehicle myLv = CommonData.StkRoot.GetObjectFromPath(objectPath) as IAgLaunchVehicle;
                leadTrailData.Add(myLv.VO.Trajectory.TrackData.PassData.Trajectory.LeadDataType);
                leadTrailData.Add(myLv.VO.Trajectory.TrackData.PassData.Trajectory.TrailDataType);
                leadTrailData.Add(myLv.Graphics.PassData.GroundTrack.LeadDataType);
                leadTrailData.Add(myLv.Graphics.PassData.GroundTrack.TrailDataType);
            }
            else
            {
            }

            return leadTrailData;
        }

        public static void SetLeadTrailData(ObjectData stkObject)
        {
            List<string> leadTrailData = new List<string>();

            if (stkObject.ClassName == "Satellite")
            {
                IAgSatellite mySat = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgSatellite;
                mySat.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(stkObject.LeadSetting3D);
                mySat.VO.Pass.TrackData.PassData.Orbit.SetTrailDataType(stkObject.TrailSetting3D);
                mySat.Graphics.PassData.GroundTrack.SetLeadDataType(stkObject.LeadSetting2D);
                mySat.Graphics.PassData.GroundTrack.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "Aircraft")
            {
                IAgAircraft myAircraft = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgAircraft;
                myAircraft.VO.Route.TrackData.SetLeadDataType(stkObject.LeadSetting3D);
                myAircraft.VO.Route.TrackData.SetTrailDataType(stkObject.TrailSetting3D);
                myAircraft.Graphics.PassData.Route.SetLeadDataType(stkObject.LeadSetting2D);
                myAircraft.Graphics.PassData.Route.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "Missile")
            {
                IAgMissile myMissile = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgMissile;
                myMissile.VO.Trajectory.TrackData.PassData.Trajectory.SetLeadDataType(stkObject.LeadSetting3D);
                myMissile.VO.Trajectory.TrackData.PassData.Trajectory.SetTrailDataType(stkObject.TrailSetting3D);
                myMissile.Graphics.PassData.Trajectory.SetLeadDataType(stkObject.LeadSetting2D);
                myMissile.Graphics.PassData.Trajectory.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "GroundVehicle")
            {
                IAgGroundVehicle myGv = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgGroundVehicle;
                myGv.VO.Route.TrackData.SetLeadDataType(stkObject.LeadSetting3D);
                myGv.VO.Route.TrackData.SetTrailDataType(stkObject.TrailSetting3D);
                myGv.Graphics.PassData.Route.SetLeadDataType(stkObject.LeadSetting2D);
                myGv.Graphics.PassData.Route.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "Ship")
            {
                IAgShip myShip = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgShip;
                myShip.VO.Route.TrackData.SetLeadDataType(stkObject.LeadSetting3D);
                myShip.VO.Route.TrackData.SetTrailDataType(stkObject.TrailSetting3D);
                myShip.Graphics.PassData.Route.SetLeadDataType(stkObject.LeadSetting2D);
                myShip.Graphics.PassData.Route.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "LaunchVehicle")
            {
                IAgLaunchVehicle myLv = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgLaunchVehicle;
                myLv.VO.Trajectory.TrackData.PassData.Trajectory.SetLeadDataType(stkObject.LeadSetting3D);
                myLv.VO.Trajectory.TrackData.PassData.Trajectory.SetTrailDataType(stkObject.TrailSetting3D);
                myLv.Graphics.PassData.GroundTrack.SetLeadDataType(stkObject.LeadSetting2D);
                myLv.Graphics.PassData.GroundTrack.SetTrailDataType(stkObject.TrailSetting2D);
            }
        }

        public static string GetOrbitSystemData(string objectPath)
        {
            //Satellites only       
            string systemName = null;
            IAgSatellite sat = CommonData.StkRoot.GetObjectFromPath(objectPath) as IAgSatellite;
            IAgVeVOSystemsCollection orbitSystems = sat.VO.OrbitSystems;
            int numberSys = orbitSystems.Count;
            for (int i = 0; i < numberSys; i++)
            {
                if (orbitSystems[i].IsVisible)
                {
                    systemName = orbitSystems[i].Name;
                }
            }
            return systemName;
        }

        public static void SetOrbitSystem(ObjectData data)
        {
            IAgSatellite sat = CommonData.StkRoot.GetObjectFromPath(data.SimplePath) as IAgSatellite;
            IAgVeVOSystemsCollection orbitSystems = sat.VO.OrbitSystems;
            int numberSys = orbitSystems.Count;
            for (int i = 0; i < numberSys; i++)
            {
                if (orbitSystems[i].Name==data.CoordSys)
                {
                    orbitSystems[i].IsVisible = true;
                }
                else
                {
                    orbitSystems[i].IsVisible = false;
                }
            }

        }

        public static void SetAllObjectData(ObjectData data)
        {
            CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
            SetObjectVisibility(data);
            SetLeadTrailData(data);
            if (data.ClassName=="Satellite")
            {
                SetOrbitSystem(data);
            }
            CommonData.StkRoot.ExecuteCommand("BatchGraphics * Off");
        }

        public static List<ObjectData> GetObjectData()
        {
            List<ObjectData> objectData = new List<ObjectData>();
            StkObjectsLibrary mStkObjectsLibrary = new StkObjectsLibrary();
            string simpleName;
            string className;
            StringCollection objectNames = mStkObjectsLibrary.GetObjectPathListFromInstanceNamesXml(CommonData.StkRoot.AllInstanceNamesToXML(), "");
            foreach (string objectName in objectNames)
            {
                className = mStkObjectsLibrary.ClassNameFromObjectPath(objectName);

                if (className != "Scenario"  && className != "Antenna" && className != "Radar" && className != "Constellation" && className != "Volumetric")
                {
                    ObjectData current = new ObjectData();

                    current.SimpleName = mStkObjectsLibrary.ObjectName(objectName);
                    current.ClassName = className;
                    current.LongPath = objectName;
                    current.SimplePath = mStkObjectsLibrary.SimplifiedObjectPath(objectName);

                    current.HideShow = GetObjectVisibility(current.ClassName, current.SimplePath);

                    List<AgELeadTrailData> leadtrailData = GetLeadTrailData(current.SimplePath, current.ClassName);
                    if (leadtrailData.Count!=0)
                    {
                        current.LeadSetting3D = leadtrailData[0];
                        current.TrailSetting3D = leadtrailData[1];
                        current.LeadSetting2D = leadtrailData[2];
                        current.TrailSetting2D = leadtrailData[3];
                    }

                    if (current.ClassName=="Satellite")
                    {
                        current.CoordSys = GetOrbitSystemData(current.SimplePath);
                    }
                    objectData.Add(current);
                }
            }


            return objectData;
        }

        public static AgELeadTrailData GetLeadTrailObject(string typeStr)
        {
            AgELeadTrailData type = AgELeadTrailData.eDataNone;
            if (typeStr.Contains("None"))
            {
                type = AgELeadTrailData.eDataNone;
            }
            else if (typeStr.Contains("All"))
            {
                type = AgELeadTrailData.eDataAll;
            }
            else if (typeStr.Contains("Full"))
            {
                type = AgELeadTrailData.eDataFull;
            }
            else if (typeStr.Contains("Half"))
            {
                type = AgELeadTrailData.eDataHalf;
            }
            else if (typeStr.Contains("OnePass"))
            {
                type = AgELeadTrailData.eDataOnePass;
            }
            else if (typeStr.Contains("Quarter"))
            {
                type = AgELeadTrailData.eDataQuarter;
            }
            else if (typeStr.Contains("Time"))
            {
                type = AgELeadTrailData.eDataTime;

            }
            return type;
        }
    }
}

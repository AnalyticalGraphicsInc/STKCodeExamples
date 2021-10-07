using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using OperatorsToolbox.SensorBoresightPlugin;

namespace OperatorsToolbox.SmartView
{
    public static class SmartViewFunctions
    {
        public static void Change3DView(ViewData view)
        {
            IAgStkObject obj = null;           
            IAgSatellite sat;
            IAgExecCmdResult result;
            string cmd = null;
            //Change View
            int windowId = GetWindowId(view.WindowName,1);

            //Change to stored view if required or set correct viewing based on object specification
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

            //Execute Camera Path
            if (view.UseCameraPath)
            {
                cmd = "VO * CameraControl Follow \"" + view.CameraPathName + "\" WindowID "+view.WindowId.ToString();
                try
                {
                    CommonData.StkRoot.ExecuteCommand(cmd);
                }
                catch (Exception)
                {
                }
            }

            //Set Animation Time
            if (!view.UseStoredView)
            {
                SetAnimation(view);
            }

            //Primary loop: Set Object Hide/show, vectors, vector scaling, and remove previous data displays
            string message = "Error changing Hide/Show for following objects: \n";
            int errorCount = 0;
            if (view.ViewObjectData.Count != 0)
            {
                IAgVORefCrdnCollection vgtPrv;
                dynamic vo = null;
                //Main loop for all objects
                foreach (ObjectData item in view.ViewObjectData)
                {
                    if (CommonData.StkRoot.ObjectExists(item.SimplePath))
                    {
                        //Remove all active data displays for all ojects
                        try
                        {
                            CommonData.StkRoot.ExecuteCommand("VO " + item.SimplePath + " DynDataText RemoveAll");
                        }
                        catch (Exception)
                        {

                        }

                        obj = CommonData.StkRoot.GetObjectFromPath(item.SimplePath);
                        vo = GetObjectVO(obj);
                        //Apply vector scaling to object
                        if (view.ApplyVectorScaling && vo != null)
                        {
                            vo.Vector.VectorSizeScale = view.VectorScalingValue;
                        }

                        //Object Hide/Show
                        if (view.ObjectHideShow)
                        {
                            try
                            {
                                SetObjectVisibility(item);
                            }
                            catch (Exception e)
                            {
                                message = message + item.SimpleName + "\n";
                                errorCount++;
                            }
                        }

                        //Vectors
                        if (view.VectorHideShow && vo != null)
                        {
                            vgtPrv = vo.Vector.RefCrdns;
                            for (int i = 0; i < vgtPrv.Count; i++)
                            {
                                if (item.ActiveVgtComponents.Keys.Contains(vgtPrv[i].Name))
                                {
                                    vgtPrv[i].Visible = true;
                                }
                                else
                                {
                                    vgtPrv[i].Visible = false;
                                }
                            }
                        }

                        //Orbit systems and lead/trail
                        if (view.EnableUniversalOrbitTrack && (item.ClassName == "Satellite" || item.ClassName == "Aircraft" || item.ClassName == "Missile" 
                            || item.ClassName == "Ship" || item.ClassName == "LaunchVehicle" || item.ClassName == "GroundVehicle"))
                        {
                            if (view.UniqueLeadTrail)//each object custom
                            {
                                if (item.ModifyLeadTrail)
                                {
                                    SetLeadTrailData3D(item);
                                    if (item.ClassName.Contains("Satellite"))
                                    {
                                        SetOrbitSystem(item, view.WindowName);
                                    }
                                }
                            }
                            else//set all objects the same
                            {
                                item.LeadSetting3D = GetLeadTrailObject(view.LeadType);
                                //check for SameAsLead
                                if (GetLeadTrailObject(view.TrailType) == AgELeadTrailData.eDataUnknown)
                                {
                                    item.TrailSetting3D = GetLeadTrailObject(view.LeadType);
                                }
                                else
                                {
                                    item.TrailSetting3D = GetLeadTrailObject(view.TrailType);
                                }
                                //Check for time
                                if (item.LeadSetting3D == AgELeadTrailData.eDataTime)
                                {
                                    item.LeadTime = Double.Parse(view.LeadTime);
                                }
                                if (item.TrailSetting3D == AgELeadTrailData.eDataTime)
                                {
                                    item.TrailTime = Double.Parse(view.TrailTime);
                                }
                                //Check for type compatibility with object class (OnePass is the only issue)
                                if (item.ClassName != "Satellite" && (GetLeadTrailObject(view.LeadType) == AgELeadTrailData.eDataOnePass || GetLeadTrailObject(view.TrailType) == AgELeadTrailData.eDataOnePass))
                                {
                                    if (item.LeadSetting3D == AgELeadTrailData.eDataOnePass)
                                    {
                                        item.LeadSetting3D = AgELeadTrailData.eDataAll;
                                    }

                                    if (item.TrailSetting3D == AgELeadTrailData.eDataOnePass)
                                    {
                                        item.TrailSetting3D = AgELeadTrailData.eDataAll;
                                    }
                                }

                                SetLeadTrailData3D(item);
                            }
                        }
                    }
                }

                if (errorCount != 0)
                {
                    MessageBox.Show(message);
                }
            }

            //Primary data display
            if (view.PrimaryDataDisplay.DataDisplayActive)
            {
                try
                {
                    if (view.PrimaryDataDisplay.PredataObject != "None")
                    {
                        cmd = "VO */" + view.PrimaryDataDisplay.DataDisplayObject + " DynDataText DataDisplay \"" + view.PrimaryDataDisplay.DataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.PrimaryDataDisplay.DataDisplayLocation + " Window " + windowId.ToString() + " PreData \"" + view.PrimaryDataDisplay.PredataObject + "\"";
                        CommonData.StkRoot.ExecuteCommand(cmd);
                        CommonData.PreviousDataDisplayObject = view.PrimaryDataDisplay.DataDisplayObject;
                    }
                    else
                    {
                        cmd = "VO */" + view.PrimaryDataDisplay.DataDisplayObject + " DynDataText DataDisplay \"" + view.PrimaryDataDisplay.DataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.PrimaryDataDisplay.DataDisplayLocation + " Window " + windowId.ToString();
                        CommonData.StkRoot.ExecuteCommand(cmd);
                        CommonData.PreviousDataDisplayObject = view.PrimaryDataDisplay.DataDisplayObject;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not create data display");
                }
            }

            //Secondary data display
            if (view.SecondaryDataDisplay.DataDisplayActive)
            {
                try
                {
                    if (view.SecondaryDataDisplay.PredataObject != "None")
                    {
                        cmd = "VO */" + view.SecondaryDataDisplay.DataDisplayObject + " DynDataText DataDisplay \"" + view.SecondaryDataDisplay.DataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.SecondaryDataDisplay.DataDisplayLocation + " Window " + windowId.ToString() + " PreData \"" + view.SecondaryDataDisplay.PredataObject + "\"";
                        CommonData.StkRoot.ExecuteCommand(cmd);
                        CommonData.PreviousDataDisplayObject = view.SecondaryDataDisplay.DataDisplayObject;
                    }
                    else
                    {
                        cmd = "VO */" + view.SecondaryDataDisplay.DataDisplayObject + " DynDataText DataDisplay \"" + view.SecondaryDataDisplay.DataDisplayReportName + "\" Show On Font Medium Color yellow Pos " + view.SecondaryDataDisplay.DataDisplayLocation + " Window " + windowId.ToString();
                        CommonData.StkRoot.ExecuteCommand(cmd);
                        CommonData.PreviousDataDisplayObject = view.PrimaryDataDisplay.DataDisplayObject;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not create data display");
                }
            }

            //Proximity plane
            if (view.EnableProximityBox)
            {
                CreateProximityPlane(view);
            }

            //Prximity Ellipsoid
            if (view.EnableProximityEllipsoid)
            {
                EnableProxEllipse(view);
            }

            //GEO Box
            if (view.EnableGeoBox)
            {
                EnableGEOBox(view);
            }

            //Sensor Boresight View Plugin option
            if (view.LinkToSensorView)
            {
                CreateSensorBoresightView(view);
            }

            if (view.OverrideTimeStep)
            {
                ((IAgScenario)CommonData.StkRoot.CurrentScenario).Animation.AnimStepValue = Double.Parse(view.TimeStep);
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

            if (view.ObjectHideShow)
            {
                string message = "Error changing Hide/Show for following objects: \n";
                int errorCount = 0;
                if (view.ViewObjectData.Count != 0)
                {
                    foreach (ObjectData item in view.ViewObjectData)
                    {
                        try
                        {
                            SetObjectVisibility(item);
                        }
                        catch (Exception e)
                        {
                            message = message + item.SimpleName + "\n";
                            errorCount++;
                        }
                    }

                    if (errorCount != 0)
                    {
                        MessageBox.Show(message);
                    }
                }
            }
        }

        public static void CreateSensorBoresightView(ViewData view)
        {
            IAgStkObject sensor = null;
            SensorViewClass sensorViewCreator;
            SensorViewData viewData = view.SensorBoresightData;
            bool exists = CommonData.StkRoot.ObjectExists(viewData.SelectedSensor);
            if (exists)
            {
                sensor = CommonData.StkRoot.GetObjectFromPath(viewData.SelectedSensor);
            }
            if (sensor != null)
            {
                sensorViewCreator = new SensorViewClass(CommonData.StkRoot, sensor);
                if (viewData.AutoUpVector)
                {
                    sensorViewCreator.CreateSensorWindow(Convert.ToInt16(viewData.VertWinSize));
                }
                else
                {
                    sensorViewCreator.CreateSensorWindow(Convert.ToInt16(viewData.VertWinSize), viewData.UpVector);
                }

                if (viewData.ShowCompass)
                {
                    sensorViewCreator.EnableCompass();
                }
                if (viewData.ShowLatLon)
                {
                    sensorViewCreator.EnableLLA();
                }
                if (viewData.ShowRulers)
                {
                    sensorViewCreator.EnableRulers();
                }
                if (viewData.ShowCrosshairs)
                {
                    viewData.ShowCrosshairs = true;
                    if (viewData.CrosshairType == SensorViewClass.CrosshairType.Square)
                    {
                        sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Square);
                    }
                    else if (viewData.CrosshairType == SensorViewClass.CrosshairType.Grid)
                    {
                        sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Grid);
                    }
                    else if (viewData.CrosshairType == SensorViewClass.CrosshairType.Circular)
                    {
                        sensorViewCreator.EnableCrosshairs(SensorViewClass.CrosshairType.Circular);
                    }
                }
            }
        }

        public static void CreateProximityPlane(ViewData view)
        {
            try
            {
                if (view.ViewTarget.Contains("Satellite"))
                {
                    IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(view.ViewTarget);
                    IAgCrdnPlaneNormal plane = null;
                    if (obj.Vgt.Planes.Contains("ProximityPlane"))
                    {
                        plane = (IAgCrdnPlaneNormal)obj.Vgt.Planes["ProximityPlane"];
                    }
                    else
                    {
                        plane = obj.Vgt.Planes.Factory.Create("ProximityPlane", "", AgECrdnPlaneType.eCrdnPlaneTypeNormal) as IAgCrdnPlaneNormal;
                    }
                    //IAgVORefCrdn newPlane = plane as IAgVORefCrdn;
                    //string planeName = newPlane.Name;
                    plane.NormalVector.SetPath(view.ViewTarget + " Nadir(Detic)");
                    plane.ReferencePoint.SetPath(view.ViewTarget + " Center");
                    plane.ReferenceVector.SetPath(view.ViewTarget + " Velocity");
                    dynamic vo = GetObjectVO(obj);
                    vo.Vector.RefCrdns.Add(AgEGeometricElemType.ePlaneElem, view.ViewTarget + " ProximityPlane");
                    IAgVORefCrdn newPlane = vo.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.ePlaneElem, view.ViewTarget + " ProximityPlane Plane");
                    newPlane.Visible = true;
                    newPlane.LabelVisible = false;
                    newPlane.Color = System.Drawing.Color.LimeGreen;
                    vo.Vector.VectorSizeScale = 5;
                    IAgVORefCrdnPlane newPlane1 = newPlane as IAgVORefCrdnPlane;
                    newPlane1.CircGridVisible = true;
                    newPlane1.Size = 3;
                    newPlane1.PlaneGridSpacing = Double.Parse(view.ProxGridSpacing); //km
                    newPlane1.TransparentPlaneVisible = true;
                    newPlane1.DrawAtObject = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not create proximity plane");
            }
        }

        public static void EnableProxEllipse(ViewData view)
        {
            try
            {
                IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(view.ViewTarget);
                dynamic vo = GetObjectVO(obj);
                vo.Proximity.Ellipsoid.IsVisible = true;
                vo.Proximity.Ellipsoid.XSemiAxisLength = Double.Parse(view.EllipsoidX);
                vo.Proximity.Ellipsoid.YSemiAxisLength = Double.Parse(view.EllipsoidY);
                vo.Proximity.Ellipsoid.ZSemiAxisLength = Double.Parse(view.EllipsoidZ);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not create proximity ellipsoid");
            }
        }

        public static void EnableGEOBox(ViewData view)
        {
            IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(view.ViewTarget);
            dynamic vo = GetObjectVO(obj);
            if (view.ViewTarget.Contains("Satellite"))
            {
                vo.Proximity.GeoBox.IsVisible = true;
                vo.Proximity.GeoBox.Longitude = Double.Parse(view.GeoLongitude);
                vo.Proximity.GeoBox.Radius = Double.Parse(view.GeoRadius);
                vo.Proximity.GeoBox.NorthSouth = Double.Parse(view.GeoNorthSouth);
                vo.Proximity.GeoBox.EastWest = Double.Parse(view.GeoEastWest);
                vo.Proximity.GeoBox.Color = System.Drawing.Color.Red;
            }
        }

        public static dynamic GetObjectVO(IAgStkObject obj)
        {
            dynamic classObj;
            switch (obj.ClassType)
            {
                case AgESTKObjectType.eAircraft:
                    classObj = obj as IAgAircraft;
                    return classObj.VO as IAgGreatArcVO;
                case AgESTKObjectType.eFacility:
                    classObj = obj as IAgFacility;
                    return classObj.VO;
                case AgESTKObjectType.eGroundVehicle:
                    classObj = obj as IAgGroundVehicle;
                    return classObj.VO as IAgGreatArcVO;
                case AgESTKObjectType.eLaunchVehicle:
                    classObj = obj as IAgLaunchVehicle;
                    return classObj.VO;
                case AgESTKObjectType.eMissile:
                    classObj = obj as IAgMissile;
                    return classObj.VO;
                case AgESTKObjectType.eRadar:
                    classObj = obj as IAgRadar;
                    return classObj.VO;
                case AgESTKObjectType.eReceiver:
                    classObj = obj as IAgReceiver;
                    return classObj.VO;
                case AgESTKObjectType.eSatellite:
                    classObj = obj as IAgSatellite;
                    return classObj.VO;
                case AgESTKObjectType.eSensor:
                    classObj = obj as IAgSensor;
                    return classObj.VO;
                case AgESTKObjectType.eShip:
                    classObj = obj as IAgShip;
                    return classObj.VO as IAgGreatArcVO;
                case AgESTKObjectType.eTarget:
                    classObj = obj as IAgTarget;
                    return classObj.VO;
                case AgESTKObjectType.eTransmitter:
                    classObj = obj as IAgTransmitter;
                    return classObj.VO;
                case AgESTKObjectType.eAntenna:
                    classObj = obj as IAgAntenna;
                    return classObj.VO;
                case AgESTKObjectType.ePlace:
                    classObj = obj as IAgPlace;
                    return classObj.VO;
                default:
                    return null;
            }
        }

        public static void SetAnimation(ViewData view)
        {
            if (view.UseAnimationTime)
            {
                try
                {
                    //string newTime = CommonData.StkRoot.ConversionUtility.ConvertDate("EpSec", "UTCG", view.AnimationTime);
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
                    if (windowInfoParts[1].Contains(windowName))
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

        public static Tuple<bool,int> DoesWindowExist(string windowName, int windowType)
        {
            Tuple<bool, int> exists = new Tuple<bool, int>(false, -1);
            List<string> names = GetWindowNames(windowType);
            foreach (var item in names)
            {
                if (item.Contains(windowName))
                {
                    int id = GetWindowId(windowName, 1);
                    exists = new Tuple<bool, int>(true, id);
                }
            }

            return exists;
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
                //Try/Catch for when object above it is already turned off
                try
                {
                    if (objectData.HideShow)
                    {
                        myObject.Graphics.Show = true;
                    }
                    else
                    {
                        myObject.Graphics.Show = false;
                    }
                }
                catch (Exception e)
                {

                }
            }
            else if (className == "Receiver")
            {
                IAgReceiver myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgReceiver;
                //Try/Catch for when object above it is already turned off
                try
                {
                    if (objectData.HideShow)
                    {
                        myObject.Graphics.Show = true;
                    }
                    else
                    {
                        myObject.Graphics.Show = false;
                    }
                }
                catch (Exception e)
                {

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
            else if (className == "Ship")
            {
                IAgShip myObject = CommonData.StkRoot.GetObjectFromPath(simplePath) as IAgShip;
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

        public static void SetLeadTrailData3D(ObjectData stkObject)
        {
            IAgVeVOLeadTrailData leadTrailData = null;
            if (stkObject.ClassName == "Satellite")
            {
                IAgSatellite mySat = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgSatellite;                
                mySat.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(stkObject.LeadSetting3D);
                mySat.VO.Pass.TrackData.PassData.Orbit.SetTrailDataType(stkObject.TrailSetting3D);
                leadTrailData = mySat.VO.Pass.TrackData.PassData.Orbit;
            }
            else if (stkObject.ClassName == "Aircraft")
            {
                IAgAircraft myAircraft = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgAircraft;
                myAircraft.VO.Route.TrackData.SetLeadDataType(stkObject.LeadSetting3D);
                myAircraft.VO.Route.TrackData.SetTrailDataType(stkObject.TrailSetting3D);
                leadTrailData = myAircraft.VO.Route.TrackData;
            }
            else if (stkObject.ClassName == "Missile")
            {
                IAgMissile myMissile = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgMissile;
                myMissile.VO.Trajectory.TrackData.PassData.Trajectory.SetLeadDataType(stkObject.LeadSetting3D);
                myMissile.VO.Trajectory.TrackData.PassData.Trajectory.SetTrailDataType(stkObject.TrailSetting3D);
                leadTrailData = myMissile.VO.Trajectory.TrackData.PassData.Trajectory;
            }
            else if (stkObject.ClassName == "GroundVehicle")
            {
                IAgGroundVehicle myGv = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgGroundVehicle;
                myGv.VO.Route.TrackData.SetLeadDataType(stkObject.LeadSetting3D);
                myGv.VO.Route.TrackData.SetTrailDataType(stkObject.TrailSetting3D);
                leadTrailData = myGv.VO.Route.TrackData;
            }
            else if (stkObject.ClassName == "Ship")
            {
                IAgShip myShip = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgShip;
                myShip.VO.Route.TrackData.SetLeadDataType(stkObject.LeadSetting3D);
                myShip.VO.Route.TrackData.SetTrailDataType(stkObject.TrailSetting3D);
                leadTrailData = myShip.VO.Route.TrackData;
            }
            else if (stkObject.ClassName == "LaunchVehicle")
            {
                IAgLaunchVehicle myLv = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgLaunchVehicle;
                myLv.VO.Trajectory.TrackData.PassData.Trajectory.SetLeadDataType(stkObject.LeadSetting3D);
                myLv.VO.Trajectory.TrackData.PassData.Trajectory.SetTrailDataType(stkObject.TrailSetting3D);
                leadTrailData = myLv.VO.Trajectory.TrackData.PassData.Trajectory;
            }
            //Check for time
            if (leadTrailData != null)
            {
                if (leadTrailData.LeadDataType == AgELeadTrailData.eDataTime)
                {
                    IAgVeLeadTrailDataTime data = leadTrailData.LeadData as IAgVeLeadTrailDataTime;
                    data.Time = stkObject.LeadTime;
                }
                if (leadTrailData.TrailDataType == AgELeadTrailData.eDataTime)
                {
                    IAgVeLeadTrailDataTime data = leadTrailData.TrailData as IAgVeLeadTrailDataTime;
                    data.Time = stkObject.LeadTime;
                }
            }
        }

        public static void SetLeadTrailData2D(ObjectData stkObject)
        {
            List<string> leadTrailData = new List<string>();

            if (stkObject.ClassName == "Satellite")
            {
                IAgSatellite mySat = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgSatellite;
                mySat.Graphics.PassData.GroundTrack.SetLeadDataType(stkObject.LeadSetting2D);
                mySat.Graphics.PassData.GroundTrack.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "Aircraft")
            {
                IAgAircraft myAircraft = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgAircraft;
                myAircraft.Graphics.PassData.Route.SetLeadDataType(stkObject.LeadSetting2D);
                myAircraft.Graphics.PassData.Route.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "Missile")
            {
                IAgMissile myMissile = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgMissile;
                myMissile.Graphics.PassData.Trajectory.SetLeadDataType(stkObject.LeadSetting2D);
                myMissile.Graphics.PassData.Trajectory.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "GroundVehicle")
            {
                IAgGroundVehicle myGv = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgGroundVehicle;
                myGv.Graphics.PassData.Route.SetLeadDataType(stkObject.LeadSetting2D);
                myGv.Graphics.PassData.Route.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "Ship")
            {
                IAgShip myShip = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgShip;
                myShip.Graphics.PassData.Route.SetLeadDataType(stkObject.LeadSetting2D);
                myShip.Graphics.PassData.Route.SetTrailDataType(stkObject.TrailSetting2D);
            }
            else if (stkObject.ClassName == "LaunchVehicle")
            {
                IAgLaunchVehicle myLv = CommonData.StkRoot.GetObjectFromPath(stkObject.SimplePath) as IAgLaunchVehicle;
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
            if (orbitSystems.InertialByWindow.IsVisible)
            {
                systemName = "Inertial";
            }
            else if(orbitSystems.FixedByWindow.IsVisible)
            {
                systemName = "Fixed";
            }
            else
            {
                systemName = "Inertial";
            }
            return systemName;
        }

        public static void SetOrbitSystem(ObjectData data, string windowName)
        {
            IAgSatellite sat = CommonData.StkRoot.GetObjectFromPath(data.SimplePath) as IAgSatellite;
            IAgVeVOSystemsCollection orbitSystems = sat.VO.OrbitSystems;
            int numberSys = orbitSystems.Count;
            if (data.CoordSys.Contains("Inertial"))
            {
                orbitSystems.InertialByWindow.IsVisible = true;
                orbitSystems.InertialByWindow.VOWindow = windowName;
                if (orbitSystems.FixedByWindow.IsVisible)
                {
                    if (orbitSystems.FixedByWindow.VOWindow == windowName || orbitSystems.FixedByWindow.VOWindow == "All")
                    {
                        orbitSystems.FixedByWindow.IsVisible = false;
                    }
                }
                for (int i = 0; i < numberSys; i++)
                {
                    if (orbitSystems[i].VOWindow == windowName || orbitSystems[i].VOWindow == "All")
                    {
                        orbitSystems[i].IsVisible = false;
                    }
                }
            }
            else if (data.CoordSys.Contains("Fixed"))
            {
                orbitSystems.FixedByWindow.IsVisible = true;
                orbitSystems.FixedByWindow.VOWindow = windowName;
                if (orbitSystems.InertialByWindow.IsVisible)
                {
                    if (orbitSystems.InertialByWindow.VOWindow == windowName || orbitSystems.InertialByWindow.VOWindow == "All")
                    {
                        orbitSystems.InertialByWindow.IsVisible = false;
                    }
                }
                for (int i = 0; i < numberSys; i++)
                {
                    if (orbitSystems[i].VOWindow == windowName || orbitSystems[i].VOWindow == "All")
                    {
                        orbitSystems[i].IsVisible = false;
                    }
                }
            }
            else if (data.CoordSys.Contains("VVLH"))
            {
                try
                {
                    orbitSystems.Remove(data.CoordSys);
                }
                catch (Exception)
                {

                }
                if (!orbitSystems.Contains(data.CoordSys))
                {
                    IAgVeVOSystemsElement elem = orbitSystems.Add(data.CoordSys);
                    elem.IsVisible = true;
                    elem.VOWindow = windowName;
                }
                if (orbitSystems.InertialByWindow.IsVisible)
                {
                    if (orbitSystems.InertialByWindow.VOWindow == windowName || orbitSystems.InertialByWindow.VOWindow == "All")
                    {
                        orbitSystems.InertialByWindow.IsVisible = false;
                    }
                }
                if (orbitSystems.FixedByWindow.IsVisible)
                {
                    if (orbitSystems.FixedByWindow.VOWindow == windowName || orbitSystems.FixedByWindow.VOWindow == "All")
                    {
                        orbitSystems.FixedByWindow.IsVisible = false;
                    }
                }
                for (int i = 0; i < numberSys; i++)
                {
                    if ((orbitSystems[i].VOWindow == windowName || orbitSystems[i].VOWindow == "All") && orbitSystems[i].Name != data.CoordSys)
                    {
                        orbitSystems[i].IsVisible = false;
                    }
                }
            }
        }

        public static void SetAllObjectData(ObjectData data)
        {
            CommonData.StkRoot.ExecuteCommand("BatchGraphics * On");
            SetObjectVisibility(data);
            SetLeadTrailData3D(data);
            if (data.ClassName=="Satellite")
            {
                //SetOrbitSystem(data);
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

                if (className != "Scenario"  && className != "Constellation" && className != "Volumetric")
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
            else if (typeStr.Contains("SameAsLead"))
            {
                type = AgELeadTrailData.eDataUnknown;
            }
            return type;
        }

        public static string GetLeadTrailString(AgELeadTrailData data)
        {
            switch (data)
            {
                case AgELeadTrailData.eDataUnknown:
                    return "SameAsLead";
                case AgELeadTrailData.eDataNone:
                    return "None";
                case AgELeadTrailData.eDataAll:
                    return "All";
                case AgELeadTrailData.eDataFull:
                    return "Full";
                case AgELeadTrailData.eDataHalf:
                    return "half";
                case AgELeadTrailData.eDataOnePass:
                    return "OnePass";
                case AgELeadTrailData.eDataQuarter:
                    return "Quarter";
                case AgELeadTrailData.eDataTime:
                    return "Time";
                default:
                    return null;
            }
        }

        public static List<string> GetCameraPaths()
        {
            List<string> paths = new List<string>();
            IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("VO_R * CameraControl GetPaths");
            string resultStr = result[0].Replace("\"", "");
            string[] pathNames = resultStr.Split(null);
            foreach (var path in pathNames)
            {
                paths.Add(path);
            }

            return paths;
        }
    }
}

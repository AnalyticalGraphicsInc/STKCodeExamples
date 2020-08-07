using System;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

namespace OperatorsToolbox.GroundEvents
{
    public static class GroundEventFunctions
    {
        public static void CreateGroundEvent(GroundEvent currentGroundEvent)
        {
            IAgPlace place;
            IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Place/" + currentGroundEvent.Id);
            if (result[0]=="0")
            {
                place = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlace, currentGroundEvent.Id) as IAgPlace;
                
            }
            else
            {
                place = CommonData.StkRoot.GetObjectFromPath("Place/" + currentGroundEvent.Id) as IAgPlace;
            }
            place.Position.AssignGeodetic(Double.Parse(currentGroundEvent.Latitude), Double.Parse(currentGroundEvent.Longitude), 0);
            string filePath = GetImagePath(currentGroundEvent.SsrType);
            string cmd = "VO */Place/"+currentGroundEvent.Id +" marker show on markertype imagefile imagefile \"" + filePath + "\" Transparent Off Size 32";
            if (filePath != null)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand(cmd);
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not update image. Check image file path in settings");
                }
            }
            if (currentGroundEvent.StartTime == "Unspecified" || currentGroundEvent.StopTime == "Unspecified")
            {
            }
            else
            {
                CreateTimelineComponent(currentGroundEvent);
            }
        }
        public static void CreateSubObject(GroundEvent currentGroundEvent, SubObject currentSub)
        {
            string placeName = currentGroundEvent.Id + "-" + currentSub.Name;
            IAgPlace place;
            IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Place/" + placeName);
            if (result[0] == "0")
            {
                place = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlace, placeName) as IAgPlace;

            }
            else
            {
                place = CommonData.StkRoot.GetObjectFromPath("Place/" + placeName) as IAgPlace;
            }
            place.Position.AssignGeodetic(Double.Parse(currentSub.Latitude), Double.Parse(currentSub.Longitude), 0);
            string filePath = GetImagePath(currentSub.Type);
            string cmd = "VO */Place/" + placeName + " marker show on markertype imagefile imagefile \"" + filePath + "\" Transparent Off Size 32";
            if (filePath!=null)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand(cmd);
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not update image. Check image file path in settings");
                }
            }
            string zoom = currentSub.ZoomLevel + "000";
            try
            {
                cmd = "VO */Place/" + placeName + " ModelDetail Set ModelLabel "+zoom+" MarkerLabel " + zoom + " Marker "+ zoom + " Point "+ zoom;
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                string mes = "Could not Modify Zoom for SubObject";
                MessageBox.Show(mes);
            }
        }
        public static int GetImageIndex(GroundEvent currentGroundEvent)
        {
            int index = -1;
            for (int i = 0; i < CommonData.Preferences.EventTypeList.Count; i++)
            {
                if (currentGroundEvent.SsrType.Contains(CommonData.Preferences.EventTypeList[i]))
                {
                    index = i;
                }
            }

            return index;
        }
        public static int GetSubObjectImageIndex(SubObject currentSsr)
        {
            int index = -1;
            for (int i = 0; i < CommonData.Preferences.EventTypeList.Count; i++)
            {
                if (currentSsr.Type.Contains(CommonData.Preferences.EventTypeList[i]))
                {
                    index = i;
                }
            }

            return index;
        }
        public static int GetAssetImageIndex(string assetName)
        {
            int index = -1;
            if (assetName.Contains("Sensor"))
            {
                index = 1;
            }
            else
            {
                index = 0;
            }

            return index;
        }
        public static string GetImagePath(string type)
        {
            string filePath = null;
            for (int i = 0; i < CommonData.Preferences.EventTypeList.Count; i++)
            {
                if (type.Contains(CommonData.Preferences.EventTypeList[i]))
                {
                    filePath = CommonData.Preferences.EventImageLocations[i];
                }
            }
            if (filePath == null)
            {
                filePath = "";
            }

            return filePath;
        }
        public static void CreateTimeLineView()
        {
            try
            {
                CommonData.StkRoot.ExecuteCommand("Timeline * ContentView Remove \"Event_Timeline\"");
            }
            catch (Exception)
            {

            }
            try
            {
                CommonData.StkRoot.ExecuteCommand("Timeline * ContentView Add \"Event_Timeline\"");
                CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");
                CommonData.StkRoot.ExecuteCommand("Timeline * ContentView Select \"Event_Timeline\" WindowID 1");
                CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");
            }
            catch (Exception)
            {

            }

        }

        public static void CreateTimelineComponent(GroundEvent currentGroundEvent)
        {
            IAgStkObject place = CommonData.StkRoot.GetObjectFromPath("Place/"+currentGroundEvent.Id);
            if (!place.Vgt.EventIntervals.Contains(currentGroundEvent.Id + "-Interval"))
            {
                IAgCrdnEventInterval interval = place.Vgt.EventIntervals.Factory.CreateEventIntervalFixed(currentGroundEvent.Id + "-Interval", "");
                IAgCrdnEventIntervalFixed fixedInstant = interval as IAgCrdnEventIntervalFixed;
                fixedInstant.SetInterval(currentGroundEvent.StartTime, currentGroundEvent.StopTime);
            }
            string cmd = "Timeline * TimeComponent Add ContentView \"Event_Timeline\" DisplayName \"" + currentGroundEvent.Id + "-Interval\"" + " \"Place/" + currentGroundEvent.Id + " " + currentGroundEvent.Id + "-Interval Interval\"";
            CommonData.StkRoot.ExecuteCommand(cmd);
            CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");

        }

        //Uses a satellite object to create a time component if AWB is not available 
        public static void CreateTimelineComponentNoAwb(GroundEvent currentGroundEvent)
        {
            IAgSatellite sat; 
            string mes = null;
            bool error = false;
            string satName = "z" + currentGroundEvent.Id + "-TimelineObject";
            try
            {
                string cmd1 = "Timeline * TimeComponent Remove ContentView \"Event_Timeline\" \"Satellite/" + satName + " AvailabilityTimeSpan Interval\"";
                CommonData.StkRoot.ExecuteCommand(cmd1);
            }
            catch (Exception)
            {

            }

            IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("DoesObjExist / */Satellite/" +satName);
            if (result[0] == "0")
            {
                sat = CommonData.StkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, satName) as IAgSatellite;
            }
            else
            {
                sat = CommonData.StkRoot.GetObjectFromPath("Satellite/" + satName) as IAgSatellite;
            }
            try
            {
                ((IAgSatellite)sat).SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);
                IAgVePropagatorJ2Perturbation prop = sat.Propagator as IAgVePropagatorJ2Perturbation;
                prop.EphemerisInterval.SetExplicitInterval(currentGroundEvent.StartTime, currentGroundEvent.StopTime);
                prop.Propagate();
            }
            catch (Exception)
            {
                error = true;
                mes = "Error with "+currentGroundEvent.Id+": Could not update or create time component- Error with Start or Stop Time";
            }

            sat.Graphics.IsObjectGraphicsVisible = false;
            try
            {
                string cmd = "Timeline * TimeComponent Add ContentView \"Event_Timeline\" DisplayName \"" + currentGroundEvent.Id + "-Interval\" \"Satellite/" + satName + " AvailabilityTimeSpan Interval\"";
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                error = true;
                mes = "Error with " + currentGroundEvent.Id + ": Could not update or create time component- Error with Start or Stop Time";
            }
            CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");
            if (error)
            {
                MessageBox.Show(mes);
            }
        }

        public static void UpdateTimelineComponent(GroundEvent currentGroundEvent)
        {
            IAgStkObject place = CommonData.StkRoot.GetObjectFromPath("Place/" + currentGroundEvent.Id);
            IAgCrdnEventIntervalGroup intervals = place.Vgt.EventIntervals;
            IAgCrdnEventInterval interval;
            if (!place.Vgt.EventIntervals.Contains(currentGroundEvent.Id + "-Interval"))
            {
                interval = place.Vgt.EventIntervals.Factory.CreateEventIntervalFixed(currentGroundEvent.Id + "-Interval", "");
            }
            else
            {
                interval = intervals[currentGroundEvent.Id + "-Interval"];
            }
            IAgCrdnEventIntervalFixed fixedInterval =(IAgCrdnEventIntervalFixed)interval;
            fixedInterval.SetInterval(currentGroundEvent.StartTime, currentGroundEvent.StopTime);
            string cmd = "Timeline * TimeComponent Add ContentView \"Event_Timeline\" DisplayName \"" + currentGroundEvent.Id + "-Interval\"" + " \"Place/" + currentGroundEvent.Id + " " + currentGroundEvent.Id + "-Interval Interval\"";
            CommonData.StkRoot.ExecuteCommand(cmd);
            CommonData.StkRoot.ExecuteCommand("Timeline * Refresh");
        }

        public static void RemoveTimelineComponent(GroundEvent currentGroundEvent)
        {
            IAgStkObject place = CommonData.StkRoot.GetObjectFromPath("Place/" + currentGroundEvent.Id);
            IAgCrdnEventIntervalGroup intervals = place.Vgt.EventIntervals;
            if (!intervals.Contains(currentGroundEvent.Id + "-Interval")) return;
            try
            {
                intervals.Remove(currentGroundEvent.Id + "-Interval");
            }
            catch (Exception)
            {
                // ignored
            }

        }

        public static void RemoveTimelineComponentNoAwb(GroundEvent currentGroundEvent)
        {
            string satName = "z" + currentGroundEvent.Id + "-TimelineObject";
            try
            {
                IAgStkObject sat = CommonData.StkRoot.GetObjectFromPath("Satellite/" + satName);
                sat.Unload();

            }
            catch (Exception)
            {

            }
        }

        public static string ConvertMilTime(string inputTime)
        {
            string outputTime=null;
            try
            {
                outputTime = inputTime.Substring(0, 2) + " " + inputTime.Substring(9, 3) + " 20" + inputTime.Substring(13, 2) + " " + inputTime.Substring(3, 2) + ":" + inputTime.Substring(5, 2) + ":00.000";
            }
            catch (Exception)
            {

            }

            return outputTime;
        }
        public static string ConvertStkTime(string inputTime)
        {
            string outputTime = null;
            if (inputTime!=null && inputTime!="Unspecified")
            {
                string[] inputParts = inputTime.Split(null);
                string part1 = null;
                if (inputParts[0].Length == 1)
                {
                    part1 = "0" + inputParts[0];
                }
                else
                {
                    part1 = inputParts[0];
                }

                try
                {
                    outputTime = part1 + "/" + inputParts[3].Substring(0, 2) + inputParts[3].Substring(3, 2) + "z " + inputParts[1] + " " + inputParts[2].Substring(2, 2);
                }
                catch (Exception)
                {

                }
            }

            return outputTime;
        }
    }
}

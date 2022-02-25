using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using AGI.STKVgt;

namespace Stk12.UiPlugin.Articulations
{
    public static class ArticFunctions
    {
        public static void CreateFile(string fileStr)
        {
            File.WriteAllText(fileStr, String.Empty);
            string fullText=null;
            string line1 = "#VO_V110 \n";
            fullText = line1;
            string linkedText = null;
            foreach (Section item in CommonData.sectionList)
            {
                if (item.isLinked)
                {
                    linkedText = item.linkString;
                    if (item.linkedToAttitude)
                    {
                        fullText = fullText + linkedText +"ATTITUDE"+ item.sectionText;
                    }
                    else
                    {
                        fullText = fullText + linkedText + item.sectionText;
                    }
                    
                }
                else if (item.linkedToList)
                {
                    for (int i = 0; i < item.linkedToListStrings.Count; i++)
                    {
                        if (item.isIncremented)
                        {
                            if (item.linkedToAttitude)
                            {
                                fullText = fullText + item.linkedToListStrings[i] + "ATTITUDE"+ item.linkedListSections[i].sectionText;
                            }
                            else
                            {
                                fullText = fullText + item.linkedToListStrings[i] + item.linkedListSections[i].sectionText;
                            }
                            
                        }
                        else
                        {
                            if (item.linkedToAttitude)
                            {
                                fullText = fullText + item.linkedToListStrings[i] +"ATTITUDE"+ item.sectionText;
                            }
                            else
                            {
                                fullText = fullText + item.linkedToListStrings[i] + item.sectionText;
                            }
                            
                        }
                    }
                }
                else
                {
                    string scenarioName = CommonData.StkRoot.CurrentScenario.InstanceName;
                    string relativePath = "RelativePath Scenario/" + scenarioName + "\n";
                    //string relativePath = "RelativePath \n" ;
                    linkedText = "BEGIN SMARTEPOCH \n" + "BEGIN EVENT \n" + "Type EVENT_LINKTO \n" + "Name ReferenceEpoch \n" + relativePath + "END EVENT \n" + "END SMARTEPOCH \n";
                    if (item.linkedToAttitude)
                    {
                        fullText = fullText + linkedText +"ATTITUDE"+ item.sectionText;
                    }
                    else
                    {
                        fullText = fullText + linkedText + item.sectionText;
                    }
                    
                }
                using (StreamWriter writer = new StreamWriter(@fileStr))
                {
                    writer.WriteLine(fullText);

                }
            }


        }
        public static Array GetArticulations(IAgStkObject parent)

        {
            IAgDataPrvFixed articDP = parent.DataProviders["Model LOD 0 Articulations"] as IAgDataPrvFixed;
            IAgDrResult result = articDP.Exec();
            Array names = result.DataSets.ElementNames;

            return names;
        }
        public static string CreateSection(string articObject,string articName,string startTimeValue, string durationValue, string startValue, string endValue, string deadbandValue, string accelValue, string decelValue, string dutyValue, string periodValue,string sectionName)
        {
            string line2 = "NEW_ARTICULATION \n";
            string line3 = "STARTTIME " + startTimeValue + "\n";
            string line4 = "DURATION " + durationValue + "\n";
            string line5 = "DEADBANDDURATION " + deadbandValue + "\n";
            string line6 = "ACCELDURATION " + accelValue + "\n";
            string line7 = "DECELDURATION " + decelValue + "\n";
            string line8 = "DUTYCYCLEDELTA " + dutyValue + "\n";
            string line9 = "PERIOD " + periodValue + "\n";
            string line10 = "ARTICULATION " + articObject + "\n";
            string line11 = "TRANSFORMATION " + articName + "\n";
            string line12 = "STARTVALUE " + startValue + "\n";
            string line13 = "ENDVALUE " + endValue + "\n";
            string line14 = "NAME " + sectionName + "\n";

            string outputSection = line2 + line3 + line4 + line5 + line6 + line7 + line8 + line9 + line10 + line11 + line12 + line13 + line14;

            return outputSection;



        }
        public static List<Section> ReadFile(string fileStr)
        {
            List<Section> fileSections = new List<Section>();

            using (StreamReader reader = new StreamReader(@fileStr))
            {
                string line=null;
                string text= null;
                int count= 0;
                Section current=null;
                LinkedListSection linkedCurrent = null;
                line = reader.ReadLine();
                int ind = 0;
                int islinked = 0;
                int linkedToList = 0;
                int sameLink = 0;
                string listName = null;
                string previousLinkArticName = null;
                int sectionsInList = 0;
                string linkedText = null;
                while ((line != null))
                {
                    ind = 0;
                    
                    if (line.Contains("BEGIN SMARTEPOCH"))
                    {
                        linkedText = null;
                        islinked = 0;
                        linkedToList = 0;
                        for (int i = 0; i < 7; i++)
                        {
                            linkedText = linkedText +line + "\n";
                            line = reader.ReadLine();
                        }
                        if (!linkedText.Contains("ReferenceEpoch") && !linkedText.Contains("ArticCreatorList"))
                        {
                            islinked = 1;


                        }
                        if (linkedText.Contains("ArticCreatorList"))
                        {
                            linkedToList = 1;
                            int start = linkedText.IndexOf("ArticCreatorList_") + "ArticCreatorList_".Length;
                            int stop = start + 5; //Arbitrary value until new one is assigned
                            if (linkedText.Contains("_StartTime"))
                            {
                                stop = linkedText.IndexOf("_StartTime");
                            }
                            else if (linkedText.Contains("_StopTime"))
                            {
                                stop = linkedText.IndexOf("_StopTime");
                            }
                            string currentListName = linkedText.Substring(start, stop - start);
                            if (listName==null)
                            {
                                listName = currentListName;
                                sectionsInList=1;
                            }
                            else if(currentListName==listName)
                            {
                                sectionsInList++;

                            }
                            else if(currentListName!=listName)
                            {
                                listName = currentListName;
                                sectionsInList = 1;


                            }

                        }

                    }
                    else if (line.Contains("NEW_ARTICULATION"))
                    {
                        //reads main section of each articulation
                        text = "NEW_ARTICULATION \n";
                        current = new Section();
                        linkedCurrent = new LinkedListSection();
                        current.sectionNumber = count;
                            
                            //CommonData.totalSectionCount = CommonData.totalSectionCount++;
                        line = reader.ReadLine();
                        current.startTimeValue = line.Split(null).Last();
                        linkedCurrent.startTimeValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.durationValue = line.Split(null).Last();
                        linkedCurrent.durationValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.deadbandValue = line.Split(null).Last();
                        linkedCurrent.deadbandValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.accelValue = line.Split(null).Last();
                        linkedCurrent.accelValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.decelValue = line.Split(null).Last();
                        linkedCurrent.decelValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.dutyValue = line.Split(null).Last();
                        linkedCurrent.dutyValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.periodValue = line.Split(null).Last();
                        linkedCurrent.periodValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.objectName = line.Split(null).Last();
                        linkedCurrent.objectName = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.articName = line.Split(null).Last();
                        linkedCurrent.articName = line.Split(null).Last();
                        text = text + line + "\n";
                        //checks for links so they can be added to the list
                        if (linkedToList==1)
                        {
                            if (previousLinkArticName == null)
                            {
                                previousLinkArticName = current.articName;
                                count++;
                            }
                            else if (previousLinkArticName == current.articName)
                            {
                                sameLink = 1;
                            }
                            else
                            {
                                sameLink = 0;
                                previousLinkArticName = current.articName;
                                count++;
                            }
                        }
                        else
                        {
                            count++;
                        }
                        line = reader.ReadLine();
                        current.startValue = line.Split(null).Last();
                        linkedCurrent.startValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        current.endValue = line.Split(null).Last();
                        linkedCurrent.endValue = line.Split(null).Last();
                        text = text + line + "\n";

                        line = reader.ReadLine();
                        //check for name since the old format does not have a name field
                        if (line.Contains("NAME"))
                        {
                            current.sectionName = line.Substring(line.IndexOf(' ') + 1);
                            text = text + line + "\n";
                            ind = 1;
                        }
                        current.sectionText = text;
                        if (ind == 1)
                        {
                            line = reader.ReadLine();
                        }
                        if (islinked == 1)
                        {
                            //Get link name and populate event names in section class
                            current.linkString = linkedText;
                            int startInd = linkedText.IndexOf("Name ") + "Name ".Length;
                            int stopInd = linkedText.IndexOf(" \nRelativePath");
                            string eventName = linkedText.Substring(startInd, stopInd - startInd);
                            current.linkTimeInstanceName = eventName;
                            current.isLinked = true;
                         }
                        if (linkedToList==1)
                        {
                            //Get link name and populate event names in section class
                            current.linkString = linkedText;
                            int startInd = linkedText.IndexOf("Name ") + "Name ".Length;
                            int stopInd = linkedText.IndexOf(" \nRelativePath");
                            string eventName = linkedText.Substring(startInd, stopInd - startInd);
                            current.linkedToListInstantNames.Add(eventName);
                            linkedCurrent.sectionText = text;
                            current.linkedToList = true;
                            current.linkedListSections.Add(linkedCurrent);
                            if (sameLink==1)
                            {
                                fileSections[count - 1].linkedToListStrings.Add(linkedText);
                            }
                            else
                            {
                                fileSections.Add(current);
                            }
                                
                        }
                        else
                        {
                            fileSections.Add(current);
                        }
  
                    }
                    else
                    {
                        line = reader.ReadLine();
                    }

                }

            }
            return fileSections;



        }
        //Get file extention for file based on class
        public static string GetExtension(string className)
        {
            string ext;
            if (className=="Aircraft")
            {
                ext = ".acma";
            }
            else if (className == "Facility")
            {
                ext = ".fma";
            }
            else if (className=="GroundVehicle")
            {
                ext = ".gvma";
            }
            else if (className == "LaunchVehicle")
            {
                ext = ".lvma";
            }
            else if (className == "Missile")
            {
                ext = ".mima";
            }
            else if (className == "Place")
            {
                ext = ".plcma";
            }
            else if (className == "Satellite")
            {
                ext = ".sama";
            }
            else if (className == "Ship")
            {
                ext = ".shma";
            }
            else if (className == "Target")
            {
                ext = ".tma";
            }
            else
            {
                ext = null;
            }
            return ext;

        }
        public static void RemoveAll(string fileStr)
        {
            if (CommonData.sectionList.Count!=0)
            {
                File.WriteAllText(fileStr, String.Empty);
                for (int i = (CommonData.sectionList.Count - 1); i > -1; i--)
                {
                    CommonData.sectionList.RemoveAt(i);
                }
                CreateFile(fileStr);
            }

        }
        public static void RemoveSection(string fileStr, int sectionNum)
        {
            if (sectionNum!=-1)
            {
                CommonData.sectionList.RemoveAt(sectionNum);
                CreateFile(fileStr);
            }
            



        }
        //pulls data providers for the aircraft in ICRF frame
        public static List<Array> GetAttitudeData(IAgStkObject obj,double startTime,double timeStep)
        {
            List<Array> attitudeData = new List<Array>();
            IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;
            IAgDrDataSetCollection datasets = null;
            if (CommonData.objectClass=="Aircraft")
            {
                IAgDataProviderGroup attitudeDP1 = obj.DataProviders["Body Axes Orientation:YPR 321"] as IAgDataProviderGroup;
                IAgDataPrvTimeVar attitudeDP2 = attitudeDP1.Group["ICRF"] as IAgDataPrvTimeVar;
                IAgDrResult result = attitudeDP2.Exec(startTime, scenario.StopTime, timeStep);
                datasets = result.DataSets;
            }
            else if (CommonData.objectClass == "Satellite")
            {
                IAgDataProviderGroup attitudeDP1 = obj.DataProviders["Attitude YPR"] as IAgDataProviderGroup;
                IAgDataPrvTimeVar attitudeDP2 = attitudeDP1.Group["Seq YPR"] as IAgDataPrvTimeVar;
                IAgDrResult result = attitudeDP2.Exec(startTime, scenario.StopTime, timeStep);
                datasets = result.DataSets;
            }

            for (int i = 0; i < datasets.Count; i++)
            {
                attitudeData.Add(datasets[i].GetValues());
            }

            return attitudeData;

        }
        public static void WriteICRFAttitudeFile(string fileName)
        {
            File.WriteAllText(fileName, String.Empty);
            CommonData.StkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");
            string fullText = null;
            string line = null;
            double currentStartTime = 0.0;
            double newYaw = 0.0;
            double newPitch = 0.0;
            double newRoll = 0.0;
            int count = 0;
            string header = "stk.v.11.0 \n" + "BEGIN Attitude \n"+ "Sequence 321 \n" + "AttitudeTimeEulerAngles \n";
            fullText = header;

            string objectPath = CommonData.objectClass + "/" + CommonData.simpleName;
            IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);

            Tuple<int, double, double> timestepValues = GetStartTimeAndMinimumTimeStep();
            count = timestepValues.Item1;
            double timeStep1 = timestepValues.Item2;
            double startTime = timestepValues.Item3;

            if (count!=0)
            {
                CommonData.AttitudeData = GetAttitudeData(obj, startTime, timeStep1);
                Array time = CommonData.AttitudeData[0];
                Array yaw = CommonData.AttitudeData[1];
                Array pitch = CommonData.AttitudeData[2];
                Array roll = CommonData.AttitudeData[3];
                double timestep = Convert.ToDouble(time.GetValue(1)) - Convert.ToDouble(time.GetValue(0));

                double[,] oldDCM = MatrixFunctions.Creat3x3Identity();
                double[,] rotationMatrix;
                double[,] newDCM = new double[3, 3];

                foreach (Section item in CommonData.sectionList)
                {

                    if (item.linkedToAttitude && item.objectName == CommonData.MainBody)
                    {
                        int index = Array.IndexOf(time, Convert.ToDouble(item.startTimeValue));
                        if (index!=-1)
                        {
                            List<double> newAngles = new List<double>();
                            count = 0;
                            double numSteps = Convert.ToDouble(item.durationValue)/timestep;
                            double startValue = Convert.ToDouble(item.startValue);
                            double endValue = Convert.ToDouble(item.endValue);
                            double change = (endValue - startValue);
                            double incrament = (endValue - startValue) / numSteps;
                            for (int i = index; i < (time.Length-index); i++)
                            {
                                count++;
                                if (count<=numSteps+1)
                                {
                                    if (item.articName.Contains("Yaw"))
                                    {
                                        if (i == 0)
                                        {
                                            rotationMatrix = MatrixFunctions.RotateAbout(3, 0, "deg");
                                        }
                                        else
                                        {
                                            rotationMatrix = MatrixFunctions.RotateAbout(3, incrament, "deg");
                                        }
                                        newDCM = MatrixFunctions.MatrixMultiply(rotationMatrix, oldDCM);
                                        newAngles = MatrixFunctions.GetNewAngles(newDCM);
                                    }
                                    else if (item.articName.Contains("Pitch"))
                                    {
                                        if (i == 0)
                                        {
                                            rotationMatrix = MatrixFunctions.RotateAbout(2, 0, "deg");
                                        }
                                        else
                                        {
                                            rotationMatrix = MatrixFunctions.RotateAbout(2, incrament, "deg");
                                        }
                                        newDCM = MatrixFunctions.MatrixMultiply(rotationMatrix, oldDCM);
                                        newAngles = MatrixFunctions.GetNewAngles(newDCM);
                                    }
                                    else if (item.articName.Contains("Roll"))
                                    {
                                        if (i == 0)
                                        {
                                            rotationMatrix = MatrixFunctions.RotateAbout(1, 0, "deg");
                                        }
                                        else
                                        {
                                            rotationMatrix = MatrixFunctions.RotateAbout(1, incrament, "deg");
                                        }
                                        newDCM = MatrixFunctions.MatrixMultiply(rotationMatrix, oldDCM);
                                        newAngles = MatrixFunctions.GetNewAngles(newDCM);
                                    }
                                    newYaw = Convert.ToDouble(yaw.GetValue(index + i)) + ((180 / Math.PI) * newAngles[0]);
                                    newPitch = Convert.ToDouble(pitch.GetValue(index + i)) + ((180 / Math.PI) * newAngles[1]);
                                    newRoll = Convert.ToDouble(roll.GetValue(index + i)) + ((180 / Math.PI) * newAngles[2]);

                                    yaw.SetValue(newYaw, index + i);
                                    pitch.SetValue(newPitch, index + i);
                                    roll.SetValue(newRoll, index + i);
                                }
                                else
                                {
                                    newYaw = Convert.ToDouble(yaw.GetValue(index + i)) + ((180 / Math.PI) * newAngles[0]);
                                    newPitch = Convert.ToDouble(pitch.GetValue(index + i)) + ((180 / Math.PI) * newAngles[1]);
                                    newRoll = Convert.ToDouble(roll.GetValue(index + i)) + ((180 / Math.PI) * newAngles[2]);

                                    yaw.SetValue(newYaw, index + i);
                                    pitch.SetValue(newPitch, index + i);
                                    roll.SetValue(newRoll, index + i);
                                }

                            }
                        }
                    }
                }
                using (StreamWriter writer = new StreamWriter(@fileName))
                {
                    writer.WriteLine(header);
                    for (int i = 0; i < time.Length; i++)
                    {
                        line = time.GetValue(i).ToString() + " " + yaw.GetValue(i).ToString() + " " + pitch.GetValue(i).ToString() + " " + roll.GetValue(i).ToString() +"\n";
                        writer.WriteLine(line);
                    }
                    writer.WriteLine("END Attitude");
                }
            }



        }
        public static void WriteVVLHAttitudeFile(string fileName)
        {
            File.WriteAllText(fileName, String.Empty);
            CommonData.StkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");
            string fullText = null;
            string line = null;
            int count = 0;
            string header = "stk.v.11.0 \n" + "BEGIN Attitude \n" + "CoordinateAxes AWB VVLH(CBF) " + CommonData.objectClass + "/" + CommonData.simpleName + "\n" + "Sequence 321 \n" + "AttitudeTimeEulerAngles \n";
            fullText = header;

            string objectPath = CommonData.objectClass + "/" + CommonData.simpleName;
            IAgStkObject obj = CommonData.StkRoot.GetObjectFromPath(objectPath);

            //Get timestep information based on created articulations
            Tuple<int, double, double> timestepValues = GetStartTimeAndMinimumTimeStep();
            count = timestepValues.Item1;
            double timeStep1 = timestepValues.Item2;
            double startTime = timestepValues.Item3;

            if (count != 0)
            {
                CommonData.AttitudeData = GetAttitudeData(obj, startTime, timeStep1);
                Array time = CommonData.AttitudeData[0];
                Array yawICRF = CommonData.AttitudeData[1];
                Array pitchICRF = CommonData.AttitudeData[2];
                Array rollICRF = CommonData.AttitudeData[3];

                List<string> yaw = new List<string>();
                List<string> pitch = new List<string>();
                List<string> roll = new List<string>();
                for (int i = 0; i < time.Length; i++)
                {
                    
                    yaw.Add(Convert.ToString(0.0));
                    pitch.Add(Convert.ToString(0.0));
                    roll.Add(Convert.ToString(0.0));
                }

                double timestep = Convert.ToDouble(time.GetValue(1)) - Convert.ToDouble(time.GetValue(0));

                double[,] oldDCM = MatrixFunctions.Creat3x3Identity();
                double[,] rotationMatrix;
                double[,] newDCM = new double[3,3];
                foreach (Section item in CommonData.sectionList)
                {

                    if (item.linkedToAttitude && item.objectName==CommonData.MainBody)
                    {
                        List<double> startTimes = GetStartTimes(item);
                        for (int j = 0; j < startTimes.Count; j++)
                        {
                            int index = Array.IndexOf(time, startTimes[j]);
                            if (index != -1)
                            {
                                count = 0;
                                double numSteps = Convert.ToDouble(item.durationValue) / timestep;
                                double startValue = Convert.ToDouble(item.startValue);
                                double endValue = Convert.ToDouble(item.endValue);
                                double change = (endValue - startValue);
                                double incrament = (endValue - startValue) / numSteps;
                                List<double> newAngles = new List<double>();
                                for (int i = 0; i < (time.Length - index); i++)
                                {
                                    count++;
                                    if (count <= numSteps + 1)
                                    {


                                        if (item.articName.Contains("Yaw"))
                                        {
                                            if (i == 0)
                                            {
                                                rotationMatrix = MatrixFunctions.RotateAbout(3, 0, "deg");
                                            }
                                            else
                                            {
                                                rotationMatrix = MatrixFunctions.RotateAbout(3, incrament, "deg");
                                            }
                                            newDCM = MatrixFunctions.MatrixMultiply(rotationMatrix, oldDCM);
                                            newAngles = MatrixFunctions.GetNewAngles(newDCM);
                                        }
                                        else if (item.articName.Contains("Pitch"))
                                        {
                                            if (i == 0)
                                            {
                                                rotationMatrix = MatrixFunctions.RotateAbout(2, 0, "deg");
                                            }
                                            else
                                            {
                                                rotationMatrix = MatrixFunctions.RotateAbout(2, incrament, "deg");
                                            }
                                            newDCM = MatrixFunctions.MatrixMultiply(rotationMatrix, oldDCM);
                                            newAngles = MatrixFunctions.GetNewAngles(newDCM);
                                        }
                                        else if (item.articName.Contains("Roll"))
                                        {
                                            if (i == 0)
                                            {
                                                rotationMatrix = MatrixFunctions.RotateAbout(1, 0, "deg");
                                            }
                                            else
                                            {
                                                rotationMatrix = MatrixFunctions.RotateAbout(1, incrament, "deg");
                                            }
                                            newDCM = MatrixFunctions.MatrixMultiply(rotationMatrix, oldDCM);
                                            newAngles = MatrixFunctions.GetNewAngles(newDCM);
                                        }


                                        yaw[index + i] = ((180 / Math.PI) * newAngles[0]).ToString();
                                        pitch[index + i] = ((180 / Math.PI) * newAngles[1]).ToString();
                                        roll[index + i] = ((180 / Math.PI) * newAngles[2]).ToString();
                                        oldDCM = newDCM;

                                    }
                                    else
                                    {
                                        yaw[index + i] = ((180 / Math.PI) * newAngles[0]).ToString();
                                        pitch[index + i] = ((180 / Math.PI) * newAngles[1]).ToString();
                                        roll[index + i] = ((180 / Math.PI) * newAngles[2]).ToString();
                                    }

                                }
                            }
                        }
                        
                    }
                }
                using (StreamWriter writer = new StreamWriter(@fileName))
                {
                    writer.WriteLine(header);
                    for (int i = 0; i < time.Length; i++)
                    {
                        line = time.GetValue(i).ToString() + " " + yaw[i] + " " + pitch[i] + " " + roll[i] + "\n";
                        writer.WriteLine(line);
                    }
                    writer.WriteLine("END Attitude");
                }
            }


        }
        //get start time and timestep to pull attitude data
        public static Tuple<int,double,double> GetStartTimeAndMinimumTimeStep()
        {
            int count = 0;
            double timeStep1 = 1.0;
            double timeStepCurrent = 1.0;
            double startTime = 10000000000;
            double currentStartTime = 0.0;
            string startTimeStr = null;
            List<IAgCrdn> events = new List<IAgCrdn>();
            IAgCrdn accessEvents;
            foreach (Section item in CommonData.sectionList)
            {
                count++;
                if (item.linkedToAttitude && item.objectName==CommonData.MainBody)
                {
                    if (item.isLinked && !item.linkedToList)
                    {
                        events = GetAllEventInstances();
                        int eventCount = events.Count;

                        for (int i = 0; i < eventCount; i++)
                        {
                            IAgCrdnEvent currentEvent = (IAgCrdnEvent)events[i];
                            if (events[i].Name==item.linkTimeInstanceName)
                            {
                                IAgCrdnEventFindOccurrenceResult epochResult = (IAgCrdnEventFindOccurrenceResult)currentEvent.FindOccurrence();
                                double epoch = epochResult.Epoch;
                                string epochStr = epoch.ToString();
                                try
                                {
                                    string cmd = "Units_Convert * Date GregorianUTC EpSec \"" + epochStr + "\"";
                                    string epochInEpSec = CommonData.StkRoot.ExecuteCommand(cmd).ToString();
                                    currentStartTime = Convert.ToDouble(epochInEpSec);
                                    timeStepCurrent = GetTimestep(epochInEpSec);
                                }
                                catch (Exception)
                                {
                                    currentStartTime = Convert.ToDouble(epoch);

                                    timeStepCurrent = GetTimestep(epochStr);
                                }
                            }
                        }
                    }
                    else if (item.linkedToList)
                    {
                        double listCurrentStart = startTime;
                        double listCurrentTimestep = timeStepCurrent;
                        events = GetAllEventInstances();
                        int eventCount = events.Count;
                        for (int i = 0; i < item.linkedToListInstantNames.Count; i++)
                        {
                            for (int j = 0; j < eventCount; j++)
                            {
                                IAgCrdnEvent currentEvent = (IAgCrdnEvent)events[j];
                                if (events[j].Name == item.linkedToListInstantNames[i])
                                {
                                    
                                    IAgCrdnEventFindOccurrenceResult epochResult = (IAgCrdnEventFindOccurrenceResult)currentEvent.FindOccurrence();
                                    double epoch = epochResult.Epoch;
                                    string epochStr = epoch.ToString();
                                    try
                                    {
                                        string cmd = "Units_Convert * Date GregorianUTC EpSec \"" + epochStr + "\"";
                                        epochStr = CommonData.StkRoot.ExecuteCommand(cmd).ToString();
                                        listCurrentStart = Convert.ToDouble(epochStr);
                                        timeStepCurrent = GetTimestep(epochStr);
                                    }
                                    catch (Exception)
                                    {
                                        listCurrentStart = Convert.ToDouble(epoch);

                                        timeStepCurrent = GetTimestep(epochStr);
                                    }
                                    if (count == 1)
                                    {
                                        currentStartTime = listCurrentStart;
                                    }
                                    else if (listCurrentStart<currentStartTime)
                                    {
                                        currentStartTime = listCurrentStart;
                                    }
                                    count++;

                                    //check decimals to get timestep
                                    if (listCurrentTimestep<timeStepCurrent)
                                    {
                                        timeStepCurrent=listCurrentTimestep;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        startTimeStr = item.startTimeValue;
                        timeStepCurrent = GetTimestep(startTimeStr);
                        currentStartTime = Convert.ToDouble(item.startTimeValue);
                    }

                    //Check timestep from this articulation to the previous ones to get the smallest one
                    if (timeStepCurrent < timeStep1)
                    {
                        timeStep1 = timeStepCurrent;
                    }
                    //Check current startime to previous ones to get the first one
                    if (count == 1)
                    {
                        startTime = currentStartTime;
                    }
                    else if (currentStartTime < startTime)
                    {
                        startTime = currentStartTime;
                    }
                }
            }
            Tuple<int, double, double> timestepValues = new Tuple<int,double,double>(count, timeStep1, startTime);
            return timestepValues;


        }
        public static double GetTimestep(string startTimeStr)
        {
            double timeStepCurrent = 1.0;
            if (startTimeStr.Contains("."))
            {
                string subStr = startTimeStr.Split('.').Last();
                if (subStr.Length == 1)
                {
                    timeStepCurrent = 0.1;
                }
                else if (subStr.Length == 2)
                {
                    timeStepCurrent = 0.01;
                }
                else if (subStr.Length == 3)
                {
                    timeStepCurrent = 0.001;
                }
                else
                {
                    timeStepCurrent = 1.0;
                }
            }
            return timeStepCurrent;

        }
        //Get all events in scenario
        public static List<IAgCrdn> GetAllEventInstances()
        {
            StkObjectsLibrary m_stkObjectsLibrary = new StkObjectsLibrary();
            List<IAgCrdn> events = new List<IAgCrdn>();

            IAgScenario scenario = CommonData.StkRoot.CurrentScenario as IAgScenario;

            //Get all access events available in scenario
            Array accesses = scenario.GetExistingAccesses();
            int numAccesses = accesses.GetLength(0);
            for (int i = 0; i < numAccesses; i++)
            {
                string object1 = accesses.GetValue(i, 0).ToString();
                string shortobject1 = object1.Substring(object1.IndexOf('/') + 1);
                string object2 = accesses.GetValue(i, 1).ToString();
                string shortobject2 = object2.Substring(object2.IndexOf('/') + 1);

                IAgStkAccess access = scenario.GetAccessBetweenObjectsByPath(object1, object2);
                IAgCrdnEventGroup accessEvents = access.Vgt.Events;
                for (int j = 0; j < accessEvents.Count; j++)
                {
                    IAgCrdn currentEvent = (IAgCrdn)accessEvents[j];
                    events.Add(currentEvent);
                }
            }
            //Get all object events available in scenario
            string simpleName;
            string className;

            foreach (string path in CommonData.objectPaths)
            {
                int instanceNameStartPos = path.LastIndexOf("/") + 1;
                simpleName = path.Substring(instanceNameStartPos);
                className = m_stkObjectsLibrary.ClassNameFromObjectPath(path);
                string objectPath = className + "/" + simpleName;
                IAgStkObject obj=null;
                try
                {
                    obj = CommonData.StkRoot.GetObjectFromPath(objectPath);
                    IAgCrdnEventGroup eventGroup = obj.Vgt.Events;
                    int eventCount = obj.Vgt.Events.Count;

                    for (int i = 0; i < eventCount; i++)
                    {

                        IAgCrdn currentEvent = (IAgCrdn)eventGroup[i];
                        events.Add(currentEvent);
                    }
                }
                catch (Exception)
                {

                }
            }
            return events;
        }
        //get start times from event or interval list
        public static List<double> GetStartTimes(Section item)
        {
            List<IAgCrdn> events = new List<IAgCrdn>();
            List<double> startTimes = new List<double>();
            if (item.isLinked && !item.linkedToList)
            {
                events = GetAllEventInstances();
                int eventCount = events.Count;

                for (int i = 0; i < eventCount; i++)
                {
                    IAgCrdnEvent currentEvent = (IAgCrdnEvent)events[i];
                    if (events[i].Name == item.linkTimeInstanceName)
                    {
                        IAgCrdnEventFindOccurrenceResult epochResult = (IAgCrdnEventFindOccurrenceResult)currentEvent.FindOccurrence();
                        double epoch = epochResult.Epoch;
                        string epochStr = epoch.ToString();
                        try
                        {
                            string epochInEpSec = CommonData.StkRoot.ExecuteCommand("Units_Convert * Date GregorianUTC EpSec \"" + epochStr + "\"").ToString();
                            if (startTimes.Contains(Convert.ToDouble(epochInEpSec)))
                            {

                            }
                            else
                            {
                                startTimes.Add(Convert.ToDouble(epochInEpSec));
                            }
                        }
                        catch (Exception)
                        {
                            if (startTimes.Contains(epoch))
                            {

                            }
                            else
                            {
                                startTimes.Add(epoch);
                            }
                        }
                    }
                }
            }
            else if (item.linkedToList)
            {
                events = GetAllEventInstances();
                int eventCount = events.Count;
                for (int i = 0; i < item.linkedToListInstantNames.Count; i++)
                {
                    for (int j = 0; j < eventCount; j++)
                    {
                        IAgCrdnEvent currentEvent = (IAgCrdnEvent)events[j];
                        if (events[j].Name == item.linkedToListInstantNames[i])
                        {
                            IAgCrdnEventFindOccurrenceResult epochResult = (IAgCrdnEventFindOccurrenceResult)currentEvent.FindOccurrence();
                            double epoch = epochResult.Epoch;
                            string epochStr = epoch.ToString();
                            try
                            {
                                string epochStrTry = CommonData.StkRoot.ExecuteCommand("Units_Convert * Date GregorianUTC EpSec \"" + epochStr + "\"").ToString();
                                if (startTimes.Contains(Convert.ToDouble(epochStrTry)))
                                {

                                }
                                else
                                {
                                    startTimes.Add(Convert.ToDouble(epochStrTry));
                                }
                            }
                            catch (Exception)
                            {
                                if (startTimes.Contains(epoch))
                                {

                                }
                                else
                                {
                                    startTimes.Add(epoch);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                startTimes.Add(Convert.ToDouble(item.startTimeValue));
            }
            //sort start times in ascending order
            double temp1 = 0.0;
            double temp2 = 0.0;
            for (int i = 0; i < startTimes.Count; i++)
            {
                temp1 = startTimes[i];
                for (int j = 0; j < startTimes.Count; j++)
                {
                    temp2 = startTimes[j];
                    if (temp1==temp2)
                    {

                    }
                    else if (temp2<temp1 && j>i)
                    {
                        startTimes[i] = temp2;
                        startTimes[j] = temp1;
                        temp1 = startTimes[i];
                    }
                    else
                    {

                    }
                }
            }


            return startTimes;


        }
        public static void LoadArticFile()
        {
            string objectStr = "*/" + CommonData.objectClass + "/" + CommonData.simpleName;
            string cmd = "VO " + objectStr + " ArticulationFile EnableArticFile Yes FilePath " + "\"" + CommonData.fileStr + "\"";
            try
            {
                CommonData.StkRoot.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not load file", "Error");
            }
        }
    }
}

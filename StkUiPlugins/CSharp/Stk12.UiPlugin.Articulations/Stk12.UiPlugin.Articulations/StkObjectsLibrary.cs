using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AGI.STKUtil;
using System.Collections.Specialized;
using System.Xml;
using AGI.STKObjects;

namespace Stk12.UiPlugin.Articulations
{
    public class StkObjectsLibrary
    {

        public StkObjectsLibrary()
        {

        }

        public string ClassNameFromObjectPath(string objectPath)
        {
            string pathWithoutName = ObjectPathWithoutName(objectPath);
            int classNameStartPos = pathWithoutName.LastIndexOf("/");
            return pathWithoutName.Substring(classNameStartPos + 1);
        }

        public string ObjectPathWithoutName(string objectPath)
        {
            int instanceNameStartPos = objectPath.LastIndexOf("/") + 1;
            return objectPath.Substring(0, instanceNameStartPos - 1);
        }

        public string TruncatedObjectPath(string objectPath)
        {
            string simplePath = SimplifiedObjectPath(objectPath);
            return simplePath.Replace("*/", "");
        }

        public string ObjectName(string objectPath)
        {
            int instanceNameStartPos = objectPath.LastIndexOf("/") + 1;
            return objectPath.Substring(instanceNameStartPos);
        }

        public string SimplifiedObjectPath(string objectPath)
        {
            int scenarioPos = objectPath.LastIndexOf("/Scenario/");
            if (scenarioPos < 0)
            {
                return objectPath;
            }
            else
            {
                int scenarioNameSlashPos = objectPath.IndexOf("/", scenarioPos + 10);
                if (scenarioNameSlashPos < 0)
                {
                    return "*/";
                }
                else
                {
                    return "*" + objectPath.Remove(0, scenarioNameSlashPos);
                }
            }
        }

        public StringCollection GetObjectPathListFromInstanceNamesXml(string objectListXml, string filterObjectClassName)
        {
            StringCollection objectList = new StringCollection();

            XmlDocument objectPathsDoc = new XmlDocument();
            XmlNodeList objectPathNodes;
            // XmlNode objectPathNode;
            string objectPathFilter = "//object[@class!='Application'";

            //set class name filter
            if (filterObjectClassName.Length > 0)
            {
                objectPathFilter += "&& @class!='" + filterObjectClassName + "'";
            }
            objectPathFilter += "]/@path";
            objectPathsDoc.LoadXml(objectListXml);
            objectPathNodes = objectPathsDoc.SelectNodes(objectPathFilter);
            foreach (XmlNode objectPathNode in objectPathNodes)
            {
                objectList.Add(objectPathNode.InnerText);
            }
            return objectList;
        }

        public string GetObjectTypeFromObjectName(StringCollection objectList, string objectName)
        {
            foreach (string stkObjectPath in objectList)
            {
                if (ObjectName(stkObjectPath) == objectName)
                {
                    return ClassNameFromObjectPath(stkObjectPath);
                }
            }

            return "";
        }

        public string GetScenarioDirectory()
        {
            IAgExecCmdResult result = CommonData.StkRoot.ExecuteCommand("GetDirectory / Scenario");
            return result[0].ToString();
        }

        public void ZoomToObject(string objectPath)
        {
            string command = string.Format("VO * View FromTo FromRegName \"STK Object\" FromName \"{0}\" ToRegName \"STK Object\" ToName \"{0}\"", objectPath);
            ExecuteCommand(command);
        }

        public void AnimateToTime(DateTime newTime)
        {
            string command = string.Format("SetAnimation * CurrentTime \"{0}\"", newTime.ToString(CommonData.StkDateFormat));
        }

        public void ExecuteCommand(string command, bool suppressErrorMessage = false)
        {
            try
            {
                IAgExecCmdResult cmdResult = CommonData.StkRoot.ExecuteCommand(command);
            }
            catch (Exception)
            {
                if (!suppressErrorMessage)
                {
                    System.Windows.Forms.MessageBox.Show("Command failed: " + command);
                }
            }
        }

        public bool TryExecuteCommand(string command, out string result)
        {
            bool success;
            result = "";
            try
            {
                IAgExecCmdResult cmdResult = CommonData.StkRoot.ExecuteCommand(command);
                if (cmdResult.Count > 0)
                {
                    for (int i = 0; i < cmdResult.Count; i++)
                    {
                        result += cmdResult[i];
                    }
                }
                success = cmdResult.IsSucceeded;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                success = false;
            }
            return success;
        }
    }
}

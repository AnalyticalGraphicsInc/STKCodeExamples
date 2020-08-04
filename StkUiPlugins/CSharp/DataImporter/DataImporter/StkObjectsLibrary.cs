using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AGI.STKUtil;
using System.Collections.Specialized;
using System.Xml;
using AGI.STKObjects;

namespace DataImporter
{
    public class StkObjectsLibrary
    {
        IAgStkObjectRoot m_root;
        public StkObjectsLibrary(IAgStkObjectRoot root)
        {
            m_root = root;
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
            IAgExecCmdResult result = m_root.ExecuteCommand("GetDirectory / Scenario");
            return result[0].ToString();
        }

        public void ExecuteCommand(string command)
        {
            try
            {
                IAgExecCmdResult result = m_root.ExecuteCommand(command);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Command failed: " + command);
            }

        }
    }
}

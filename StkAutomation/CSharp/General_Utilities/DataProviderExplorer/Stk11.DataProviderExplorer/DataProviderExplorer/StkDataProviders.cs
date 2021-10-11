using System;
using System.Collections.Generic;
using AGI.STKObjects;
using System.Windows.Forms;

namespace StkDataProviderExec
{

    class StkDataProviders
    {
        Object app;
        private AgStkObjectRoot root;
        private IAgScenario scen;

        public StkDataProviders()
        {
            ConnectToStk();
            
        }

        private void ConnectToStk()
        {
            app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application");
            root = app.GetType().InvokeMember("Personality2", System.Reflection.BindingFlags.GetProperty, null, app, null) as AgStkObjectRoot;
            scen = (IAgScenario)root.CurrentScenario;
        }

        public Array GetDataProviders(string stkObjectPath, string DataProviderName, 
            string GroupName, string ElementName)
        {

            string startTime = scen.StartTime.ToString();
            string stopTime = scen.StopTime.ToString();
            double stepSize = 60;

            IAgStkObject stkObject = root.GetObjectFromPath(stkObjectPath);
            
            IAgDataProviderInfo dpInfo = stkObject.DataProviders[DataProviderName];

            IAgDataProvider dataProvider = stkObject.DataProviders[DataProviderName] as IAgDataProvider;

            if (dpInfo.IsGroup())
            {
                IAgDataProviderGroup dpGroup = dpInfo as IAgDataProviderGroup;
                IAgDataProviders dpAvailable = dpGroup.Group;

                for (int i = 0; i < dpAvailable.Count; ++i)
                {
                    if (dpAvailable[i].Name == GroupName)
                    {
                        dataProvider = dpAvailable[i] as IAgDataProvider;
                        break;
                    }
                }
            }

            IAgDrResult dpResult = null;
            switch (dpInfo.Type)
            {
                case AgEDataProviderType.eDrFixed:
                    //Fixed data doesnt change over time
                    IAgDataPrvFixed dpFixed = dataProvider as IAgDataPrvFixed;                    
                    dpResult = dpFixed.Exec();                    
                    break;
                case AgEDataProviderType.eDrIntvl:
                    //Interval data is given as a list of intervals with start, stop and duration
                    IAgDataPrvInterval dpInterval = dataProvider as IAgDataPrvInterval;
                    //Must provide analysis start and stop time
                    
                    dpResult = dpInterval.Exec(startTime, stopTime);
                    break;
                case AgEDataProviderType.eDrTimeVar:
                    //Time varyign data is given as an array of time based values 
                    IAgDataPrvTimeVar dpTimeVarying = dataProvider as IAgDataPrvTimeVar;
                    //Must provide analysis start and stop time plus an evaluation step size
                    dpResult = dpTimeVarying.Exec(startTime, stopTime, stepSize);
                    break;
                default:
                    break;
            }

            Array dataValues = null;

            IAgDrDataSetCollection datasets = dpResult.DataSets;

            if (datasets.Count > 0)
            {
                IAgDrDataSet thisDataset = datasets.GetDataSetByName(ElementName);

                dataValues = thisDataset.GetValues();
            }
            return dataValues;
        }

        public List<string> GetAvailableStkAccesses()
        {
            List<string> accesses = new List<string>();
            string scenarioString = "/Application/STK/Scenario/" + ((IAgStkObject)scen).InstanceName;
            AGI.STKUtil.IAgExecCmdResult result = root.ExecuteCommand("GetAccesses /");
            foreach (string item in result)
            {
                string[] lineElements = item.Split(' ');
                accesses.Add(lineElements[0].Replace(scenarioString, "*") + "," + lineElements[2].Replace(scenarioString, "*"));
            }
            
            return accesses;

        }

        public List<string> GetAvailableStkObjects()
        {
            List<string> stkObjects = new List<string>();

            string scenarioString = "/Application/STK/Scenario/" + ((IAgStkObject)scen).InstanceName;

            foreach (IAgStkObject stkObject in ((IAgStkObject)scen).Children)
            {
                if (stkObject.ClassType != AgESTKObjectType.eConstellation)
                {
                    stkObjects.Add(stkObject.Path.Replace(scenarioString, "*"));
                    foreach (IAgStkObject stkChildObject in stkObject.Children)
                    {
                        stkObjects.Add(stkChildObject.Path.Replace(scenarioString, "*"));
                        foreach (IAgStkObject stkGrandChildObject in stkChildObject.Children)
                        {
                            stkObjects.Add(stkGrandChildObject.Path.Replace(scenarioString, "*"));
                        }
                    }
                }
            }

            return stkObjects;

        }


        public List<string> GetAvailableDataProviders(string stkObjectPath)
        {
            List<string> dataProviders = new List<string>();

            if (stkObjectPath.StartsWith("Access: "))
            {
                string[] objects = stkObjectPath.Substring(8).Split(',');
                IAgStkObject stkObject1 = root.GetObjectFromPath((string)objects.GetValue(0));
                IAgStkAccess access = stkObject1.GetAccess((string)objects.GetValue(1));

                for (int i = 0; i < access.DataProviders.Count; ++i)
                {
                    dataProviders.Add(access.DataProviders[i].Name);
                }
            }
            else
            {
                IAgStkObject stkObject = root.GetObjectFromPath(stkObjectPath);
                
                for (int i = 0; i < stkObject.DataProviders.Count; ++i)
                {
                    dataProviders.Add(stkObject.DataProviders[i].Name);
                }
            }
            return dataProviders;

        }

        public List<string> GetAvailableDataProviderElements(
            string stkObjectPath, string DataProviderName, string GroupName)
        {
            IAgStkObject stkObject = root.GetObjectFromPath(stkObjectPath);
            IAgDataProviderInfo dpInfo = stkObject.DataProviders[DataProviderName];

            IAgDataProvider dataProvider = stkObject.DataProviders[DataProviderName] as IAgDataProvider;

            if (dpInfo.IsGroup())
            {
                IAgDataProviderGroup dpGroup = dpInfo as IAgDataProviderGroup;
                IAgDataProviders dpAvailable = dpGroup.Group;

                for (int i = 0; i < dpAvailable.Count; ++i)
                {
                    if (dpAvailable[i].Name == GroupName)
                    {
                        dataProvider = dpAvailable[i] as IAgDataProvider;
                        break;
                    }
                }
            }

            List<string> dataElements = new List<string>();

            for (int i = 0; i < dataProvider.Elements.Count; ++i)
            {
                dataElements.Add(dataProvider.Elements[i].Name);
            }
            return dataElements;

        }


        public List<String> GetAvailableGroups(string stkObjectPath, string DataProviderName)
        {
            IAgStkObject stkObject = root.GetObjectFromPath(stkObjectPath);

            IAgDataProviderInfo dataProvider = stkObject.DataProviders[DataProviderName];
            
            List<String> groups = new List<string>();
            if (dataProvider.IsGroup())
            {
                IAgDataProviderGroup dpGroup = dataProvider as IAgDataProviderGroup;
                IAgDataProviders dpAvailable = dpGroup.Group;
                
                for(int i = 0; i<dpAvailable.Count;++i )
                {
                    groups.Add(dpAvailable[i].Name);
                }
            }

            return groups;
             
        }

    }

}

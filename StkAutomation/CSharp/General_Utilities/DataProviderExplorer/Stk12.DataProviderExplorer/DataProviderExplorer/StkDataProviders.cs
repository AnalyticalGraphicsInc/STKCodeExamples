using System;
using System.Collections.Generic;
using AGI.STKObjects;
using System.Windows.Forms;

namespace DataProviderExplorer
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

        public double StepSize { get; set; } = 60;

        private void ConnectToStk()
        {
            app = System.Runtime.InteropServices.Marshal.GetActiveObject("STK12.Application");
            root = app.GetType().InvokeMember("Personality2", System.Reflection.BindingFlags.GetProperty, null, app, null) as AgStkObjectRoot;
            scen = (IAgScenario)root.CurrentScenario;
        }

        public void SetUnitPreferences(string unitType, string unitAbbreviation)
        {
            root.UnitPreferences.SetCurrentUnit(unitType, unitAbbreviation);
        }

        public Array GetDataProviders(string stkObjectPath, string DataProviderName,
            string GroupName, string ElementName)
        {

            string startTime = scen.StartTime.ToString();
            string stopTime = scen.StopTime.ToString();
            var stkObject = root.GetObjectFromPath(stkObjectPath);
            var dpInfo = stkObject.DataProviders[DataProviderName];

            var dataProvider = stkObject.DataProviders[DataProviderName] as IAgDataProvider;

            if (dpInfo.IsGroup())
            {
                var dpGroup = dpInfo as IAgDataProviderGroup;
                var dpAvailable = dpGroup.Group;

                for (var i = 0; i < dpAvailable.Count; ++i)
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
                    var dpFixed = dataProvider as IAgDataPrvFixed;
                    dpResult = dpFixed.Exec();
                    break;
                case AgEDataProviderType.eDrIntvl:
                    //Interval data is given as a list of intervals with start, stop and duration
                    var dpInterval = dataProvider as IAgDataPrvInterval;
                    //Must provide analysis start and stop time

                    dpResult = dpInterval.Exec(startTime, stopTime);
                    break;
                case AgEDataProviderType.eDrTimeVar:
                    //Time varyign data is given as an array of time based values
                    var dpTimeVarying = dataProvider as IAgDataPrvTimeVar;
                    //Must provide analysis start and stop time plus an evaluation step size
                    dpResult = dpTimeVarying.Exec(startTime, stopTime, StepSize);
                    break;
                default:
                    break;
            }

            Array dataValues = null;

            var datasets = dpResult.DataSets;

            if (datasets.Count <= 0) return dataValues;

            var thisDataset = datasets.GetDataSetByName(ElementName);

            dataValues = thisDataset.GetValues();
            return dataValues;
        }

        public List<string> GetAvailableStkAccesses()
        {
            var accesses = new List<string>();
            var scenarioString = "/Application/STK/Scenario/" + ((IAgStkObject)scen).InstanceName;
            var result = root.ExecuteCommand("GetAccesses /");
            foreach (string item in result)
            {
                var lineElements = item.Split(' ');
                accesses.Add(lineElements[0].Replace(scenarioString, "*") + "," + lineElements[2].Replace(scenarioString, "*"));
            }

            return accesses;
        }

        public List<string> GetAvailableStkObjects()
        {
            var stkObjects = new List<string>();

            var scenarioString = "/Application/STK/Scenario/" + ((IAgStkObject)scen).InstanceName;

            foreach (IAgStkObject stkObject in ((IAgStkObject)scen).Children)
            {
                if (stkObject.ClassType == AgESTKObjectType.eConstellation) continue;

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

            return stkObjects;
        }


        public List<string> GetAvailableDataProviders(string stkObjectPath)
        {
            var dataProviders = new List<string>();

            if (stkObjectPath.StartsWith("Access: "))
            {
                var objects = stkObjectPath.Substring(8).Split(',');
                var stkObject1 = root.GetObjectFromPath((string)objects.GetValue(0));
                var access = stkObject1.GetAccess((string)objects.GetValue(1));

                for (var i = 0; i < access.DataProviders.Count; ++i)
                {
                    dataProviders.Add(access.DataProviders[i].Name);
                }
            }
            else
            {
                var stkObject = root.GetObjectFromPath(stkObjectPath);

                for (var i = 0; i < stkObject.DataProviders.Count; ++i)
                {
                    dataProviders.Add(stkObject.DataProviders[i].Name);
                }
            }

            return dataProviders;
        }

        public List<string> GetAvailableDataProviderElements(
            string stkObjectPath, string DataProviderName, string GroupName)
        {
            var stkObject = root.GetObjectFromPath(stkObjectPath);
            var dpInfo = stkObject.DataProviders[DataProviderName];

            var dataProvider = stkObject.DataProviders[DataProviderName] as IAgDataProvider;

            if (dpInfo.IsGroup())
            {
                var dpGroup = dpInfo as IAgDataProviderGroup;
                var dpAvailable = dpGroup.Group;

                for (var i = 0; i < dpAvailable.Count; ++i)
                {
                    if (dpAvailable[i].Name != GroupName) continue;

                    dataProvider = dpAvailable[i] as IAgDataProvider;
                    break;
                }
            }

            var dataElements = new List<string>();

            for (var i = 0; i < dataProvider.Elements.Count; ++i)
            {
                dataElements.Add(dataProvider.Elements[i].Name);
            }

            return dataElements;
        }


        public List<string> GetAvailableGroups(string stkObjectPath, string DataProviderName)
        {
            var stkObject = root.GetObjectFromPath(stkObjectPath);

            var dataProvider = stkObject.DataProviders[DataProviderName];

            var groups = new List<string>();
            if (!dataProvider.IsGroup()) return groups;

            var dpGroup = dataProvider as IAgDataProviderGroup;
            var dpAvailable = dpGroup.Group;

            for(var i = 0; i<dpAvailable.Count;++i )
            {
                groups.Add(dpAvailable[i].Name);
            }

            return groups;
        }
    }
}

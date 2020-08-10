using AGI.STKObjects;
using AGI.STKX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StkMetadataExtractor
{
    public static class StkMetadataExtractor
    {
        //Get a reference to the STK Engine Root
        public static AgStkObjectRoot Root
        {
            get
            {
                // if it's already been created, just return it
                if (_root != null) return _root;

                // otherwise, start a new engine application
                stkxApplication = new AgSTKXApplication();
                // and capture the root
                _root = new AgStkObjectRoot();

                return _root;
            }

            set => _root = value;
        }


        public static void SummarizeScenario(string scenarioFilePath, string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
            {
                Console.WriteLine("Output directory does not exist.");
                return;
            }

            if (!File.Exists(scenarioFilePath))
            {
                Console.WriteLine("Scenario file not found.");
                return;
            }

            // Load the scenario
            Root.Load(scenarioFilePath);

            //Summarize the scenario
            var summary = StkObjectSummary.SummarizeStkObject(Root.CurrentScenario);
            
            var xmlPath = Path.Combine(outputDirectory, Path.GetFileName(Path.ChangeExtension(scenarioFilePath, ".xml")));
            using (var stringWriter = new StreamWriter(xmlPath))
            {
                // Serialize the resulting summary object
                var serializer = new XmlSerializer(typeof(StkObjectSummary));
                serializer.Serialize(stringWriter, summary);
            }

            // Determine what version of STK engine you are using 
            var version = stkxApplication.Version.Split('v').Last();
            var versionInts = version.Split('.').Select(int.Parse).ToList();

            // 12.0.0 doesn't support Cesium Export so if that's what you have, skip the czml export
            if (versionInts[0] >= 12 && versionInts[1] >= 0 && versionInts[2] > 0)
            {
                var outputCzmlPath = Path.Combine(outputDirectory,
                    Path.GetFileName(Path.ChangeExtension(scenarioFilePath, ".czml")));
                var cmd = $"ExportCZML * \"{outputCzmlPath}\" http://assets.agi.com/models/";
                try
                {
                    Root.ExecuteCommand(cmd);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            // Close the scenario and exit STK
            Root.CloseScenario();
            stkxApplication.Terminate();
        }

        private static AgSTKXApplication stkxApplication;
        private static AgStkObjectRoot _root;
    }
}

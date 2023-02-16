using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePointFile
{

    /// <summary>
    /// this script will read a csv file for external data and create a .pt file from it
    /// 
    /// author: jens ramrath
    /// date: 26 aug 2022
    /// </summary>

    class Program
    {

        /// <param name="args"></param>
        public static void CreatePointFile(string csvPath)
        {
            // .pnt file will be in same folder as csv input
            string pntPath = Path.ChangeExtension(csvPath, "pnt");

            string[] fileContents = File.ReadAllLines(csvPath);

            List<FomData> allFomData = new List<FomData>();
            foreach (string thisLine in fileContents)
            {
                if (!thisLine.Contains("Time"))
                {
                    allFomData.Add(new FomData(thisLine));
                }
                
            }

            // find unique combinations

            List<FomData> uniqueLocation = new List<FomData>();

            // loop through each FOM value
            foreach (FomData thisFomData in allFomData) 
            {
                // check each gridpoint
                bool foundIt = false;
                foreach (FomData thisLocation in uniqueLocation)
                {
                    if (thisFomData.Lat == thisLocation.Lat && thisFomData.Lon == thisLocation.Lon && thisFomData.Alt == thisLocation.Alt)
                    {
                        foundIt = true;
                    }
                }

                if (!foundIt)
                {
                    uniqueLocation.Add(thisFomData);
                }
            }


            // write all unique locations to .pt file
            TextWriter tw = new StreamWriter(pntPath);
            tw.WriteLine("stk.v.4.2.1");
            tw.WriteLine("Begin PointList");

            foreach (FomData thisLocation in uniqueLocation)
            {
                tw.WriteLine(thisLocation.Lat.ToString() + " " + thisLocation.Lon.ToString() + " " + thisLocation.Alt.ToString());
            }

            tw.WriteLine("End PointList");
            tw.Close();

        }


    }

    public class FomData
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Alt { get; set; }
        public double FomValue { get; set; }

        public FomData(string lat, string lon, string alt, string fomValue)
        {
            Lat = Math.Round(Convert.ToDouble(lat), 5);
            Lon = Math.Round(Convert.ToDouble(lon), 5);
            Alt = Math.Round(Convert.ToDouble(alt), 5);
            FomValue = Convert.ToDouble(fomValue);
        }

        public FomData(string fileContents)
        {
            string[] lineSplit = fileContents.Split(',');

            Lat = Math.Round(Convert.ToDouble(lineSplit[1]), 5);
            Lon = Math.Round(Convert.ToDouble(lineSplit[2]), 5);
            Alt = Math.Round(Convert.ToDouble(lineSplit[3]), 5);
            FomValue = Convert.ToDouble(lineSplit[4]);

        }
    }
}

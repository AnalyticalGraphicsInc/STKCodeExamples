using ComspocDownloader.ConjunctionAssessment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace ComspocDownloader
{
    public static class ComspocHelper
    {

        public static void GetConjunctionsForJobAndUri(string workingPath, params long[] caJobNumbers)
        {
            //string workingPath = @"C:\temp\CDMs\";
            Directory.CreateDirectory(workingPath);
            var caService = new ConjunctionAssessment.CAServiceSOAPClient();

            List<Conjunction> jobConjunctions = new List<Conjunction>();
            foreach (long jobNumber in caJobNumbers)
            {
                try
                {
                    jobConjunctions.AddRange(caService.GetJobConjunctionReport(jobNumber, 
                        false,
                        CoordinateAxes.trueOfDate, 
                        RelativeCoordinateAxes.radialInTrackCrossTrack, 
                        null, null, null, null,null));
                }
                catch { }
            }
            
            var jobConjunctionIds = jobConjunctions.Select(js => js.JobConjunctionId);
            var ephemerisIdHash = new HashSet<long>();
            var ephemerisIdNameMap = new Dictionary<long, string>();
            Console.WriteLine("Total conjunctions " + jobConjunctionIds.Count());
            foreach (var cId in jobConjunctionIds)
            {
                Console.WriteLine("Conjunction ID" + cId);
                Conjunction report = caService.GetUserReportDetail(cId, ConjunctionAssessment.CoordinateAxes.trueOfDate, ConjunctionAssessment.RelativeCoordinateAxes.radialInTrackCrossTrack, null, null);
                long? primaryEphemerisID = report.PrimarySpaceObject.EphemerisDetail.EphemerisId;
                if (primaryEphemerisID.HasValue && !ephemerisIdHash.Contains(primaryEphemerisID.Value))
                {
                    string primaryEphemUri = @"http://comspocws/Ops/Ephemeris/" + primaryEphemerisID + @"/File";
                    WebRequest request = (HttpWebRequest)WebRequest.Create(primaryEphemUri);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var fileStream = File.Create(Path.Combine(workingPath, report.PrimarySpaceObject.SpaceObjectUri.network + "." + report.PrimarySpaceObject.SpaceObjectUri.objectID + "_"
                            + report.PrimarySpaceObject.CommonName.ToValidPathName() + "_ephemerisID_" + primaryEphemerisID + ".e")))
                        {
                            response.GetResponseStream().CopyTo(fileStream);
                        }
                    }
                }
                var secondaryEphemerisID = report.SecondarySpaceObject.EphemerisDetail.EphemerisId;
                if (secondaryEphemerisID.HasValue && !ephemerisIdHash.Contains(secondaryEphemerisID.Value))
                {
                    var secondaryEphemUri = @"http://comspocws/Ops/Ephemeris/" + secondaryEphemerisID + @"/File";
                    var request = (HttpWebRequest)WebRequest.Create(secondaryEphemUri);
                    var response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var fileStream = File.Create(Path.Combine(workingPath, report.SecondarySpaceObject.SpaceObjectUri.network + "." + report.SecondarySpaceObject.SpaceObjectUri.objectID + "_"
                            + report.SecondarySpaceObject.CommonName.ToValidPathName() + "_ephemerisID_" + secondaryEphemerisID + ".e")))
                        {
                            response.GetResponseStream().CopyTo(fileStream);
                        }
                    }
                }
                //download the CDM
                //var events = caService.GetJobConjunctionEventByJobConjunction(cId, CoordinateAxes.trueOfDate, RelativeCoordinateAxes.radialInTrackCrossTrack, null);
                
                //foreach (var caEvent in events)
                //{
                try
                {
                    Console.WriteLine("Event id " + cId);
                    string cdmUri = @"http://comspocws/Ops/Conjunctions/DownloadCustomReport/" + cId + @"?name=CDM";//&jobConjunctionEventId=" + caEvent.JobConjunctionEventId;
                    var webRequest = (HttpWebRequest)WebRequest.Create(cdmUri);
                    var webResponse = (HttpWebResponse)webRequest.GetResponse();
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (var fileStream = File.Create(Path.Combine(workingPath, "CDM_CJ-" + report.JobConjunctionId + ".xml")))
                        {
                            webResponse.GetResponseStream().CopyTo(fileStream);
                        }
                    }
                }
                catch
                {

                }
                //}
            }
        }
    }

    public static class StringExtensions
    {
        public static string ToValidPathName(this string input)
        {
            var illegalCharacters = Path.GetInvalidFileNameChars();
            return new string(input.Where(x => !illegalCharacters.Contains(x)).ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComspocDownloader
{
    public static class JspocHelper
    {
        public class WebClientEx : WebClient
        {
            // Create the container to hold all Cookie objects
            private CookieContainer _cookieContainer = new CookieContainer();

            // Override the WebRequest method so we can store the cookie 
            // container as an attribute of the Web Request object
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);

                if (request is HttpWebRequest)
                    (request as HttpWebRequest).CookieContainer = _cookieContainer;

                return request;
            }
        }   // END WebClient Class




        public static void GetConjunctionsForCreateDate(string workingPath, string createDate, string userName, string password)
        {
            //https://www.space-track.org/expandedspacedata/query/class/cdm/CONSTELLATION/agi/limit/100/creation_date/%3E2015-03-27T00:00:00/orderby/creation_date%20desc/format/kvn

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string uriBase = "https://www.space-track.org";
            string requestController = "/expandedspacedata";
            string requestAction = "/query";
            // URL to retrieve all the latest tle's for the provided NORAD CAT 
            // IDs for the provided Dates
            //string predicateValues   = "/class/tle_latest/ORDINAL/1/NORAD_CAT_ID/" + string.Join(",", norad) + "/orderby/NORAD_CAT_ID%20ASC/format/tle";
            // URL to retrieve all the latest 3le's for the provided NORAD CAT 
            // IDs for the provided Dates
            string predicateValues = "/class/cdm/CONSTELLATION/agi/limit/100/creation_date/%3E" + createDate + @"/orderby/creation_date%20desc/format/kvn";
            string request = uriBase + requestController + requestAction + predicateValues;

            string[] test;

            using (var client = new WebClientEx())
            {
                // Store the user authentication information
                var data = new NameValueCollection
                {
                    { "identity", userName },
                    { "password", password },
                };

                // Generate the URL for the API Query and return the response
                var response2 = client.UploadValues("https://www.space-track.org/ajaxauth/login", data);
                var response4 = client.DownloadData(request);

                test = Regex.Split((System.Text.Encoding.Default.GetString(response4)),"\r\n");
            }
            List<string> cdmFile = null;
            string fileName = null;
            foreach (string item in test)
            {   
                if (item.Contains("CCSDS_CDM_VERS"))
                {
                    if (cdmFile != null 
                        && cdmFile.Count > 0
                        && !string.IsNullOrWhiteSpace(fileName))
	                {
                        File.WriteAllLines(Path.Combine(workingPath,fileName), cdmFile.ToArray());		 
	                }
                    
                    cdmFile = new List<string>();
                    cdmFile.Add(item);
                }
                else
                {
                    if (item.Contains("MESSAGE_ID"))
                    {
                        fileName = item.Split('=').LastOrDefault().Trim() + ".cfe";
                    }
                    cdmFile.Add(item);
                }
            }

            if (cdmFile != null
                        && cdmFile.Count > 0
                        && !string.IsNullOrWhiteSpace(fileName))
            {
                File.WriteAllLines(Path.Combine(workingPath, fileName), cdmFile.ToArray());
            }


            ////string createDate = "2015-03-27T00:00:00";
            //string requestLink = @"https://www.space-track.org/expandedspacedata/identity=Comspoc-ops@agi.com&password=*SpaceBook2015*your_password&query/class/cdm/CONSTELLATION/agi/limit/100/creation_date/%3E" + createDate + @"/orderby/creation_date%20desc/format/kvn";

            //string cdmsFilePath = Downloader.DownloadLinkToFile(requestLink, workingPath);


        }

    }
}



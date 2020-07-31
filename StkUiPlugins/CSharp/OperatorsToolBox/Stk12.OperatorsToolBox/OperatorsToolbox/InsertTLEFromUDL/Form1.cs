using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Configuration;

using AGI.STKObjects;
using AGI.STKUtil;
using AGI.Ui.Application;
using AGI.Ui.Core;

namespace InsertTLEFromUDL
{

    //delegate function to update label outside form thread
    //needed so it actually works
    delegate void UpdateLabelText(string str);


    public partial class Form1 : Form
    {

        //global stuff ... yes this is wrong and should be in 
        //a config file... TODO, create CONFIG and put values in it

        AgUiApplication uiApp;
        IAgStkObjectRoot stkRoot;
        IAgScenario zscen;
        bool stkconnected = false;
        string workingUserDirectory = "";
        string httpbaseURL = "";


        public string[] parsingFormats = new string[] { "dd MMM yyyy HH:mm:ss", "d MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss.000", "d MMM yyyy HH:mm:ss.000" }; //add others to this as we find them


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                InitUserProperties();
                InitConnection();
                //SafelyUpdateLabel("Connected to STK...ready to go");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Is STK Open with a Scenario loaded?" + Environment.NewLine + ex.Message);
                btn_refresh.BackColor = Color.Red;
            }
        }

        //reads settings and populates
        //appropriate fields
        private void InitUserProperties()
        {

            try
            {

                tb_userName.Text = Properties.Settings.Default.UserName;
                tb_password.Text = Properties.Settings.Default.Password;
                httpbaseURL = Properties.Settings.Default.BaseURL;
                workingUserDirectory = Properties.Settings.Default.WorkingDirectory;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                SafelyUpdateLabel("Error reading initial values" + Environment.NewLine + "Check App settings file...");
            }

        }

        //populates some form values, gets scenario timeframe
        private void InitConnection()
        {
            if (!stkconnected)
            {
                // Get reference to running STK instance 
                uiApp = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AgUiApplication;

                // Get our IAgStkObjectRoot interface 
                stkRoot = uiApp.Personality2 as IAgStkObjectRoot;
                zscen = stkRoot.CurrentScenario as IAgScenario;
                stkconnected = true;

                dtp_start.Value = DateTime.ParseExact(zscen.StartTime, parsingFormats, null, System.Globalization.DateTimeStyles.None);
                dtp_end.Value = DateTime.ParseExact(zscen.StopTime, parsingFormats, null, System.Globalization.DateTimeStyles.None);

                btn_refresh.BackColor = Color.LimeGreen;
            }
            else
            {
                //refresh scenario times
                dtp_start.Value = DateTime.ParseExact(zscen.StartTime, parsingFormats, null, System.Globalization.DateTimeStyles.None);
                dtp_end.Value = DateTime.ParseExact(zscen.StopTime, parsingFormats, null, System.Globalization.DateTimeStyles.None);
            }


            if (string.IsNullOrEmpty(workingUserDirectory))
            {
                //get user directory for working purposes
                IAgExecCmdResult conRes = stkRoot.ExecuteCommand(@"GetDirectory / UserData");
                workingUserDirectory = conRes[0];
                
            }

            SafelyUpdateLabel("Connected and using: " + Environment.NewLine + workingUserDirectory);

        }

        //stuff for updating labels...
        private void UpdateText(string str)
        {
            lbl_status.Text = str;
        }

        private void SafelyUpdateLabel(string txt)
        {
            UpdateLabelText lblupdate = new UpdateLabelText(UpdateText);
            this.Invoke(lblupdate, new object[] { txt });
        }

        //main function to do stuff
        private void btn_makeRequest_Click(object sender, EventArgs e)
        {

            try
            {
                //encode password and username
                string creds = "Basic " + Base64Encode(tb_userName.Text + ":" + tb_password.Text);

                List<string> allSSCs = tb_ssc.Text.Split(new char[] { '\r', '\n', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (allSSCs.Count > 0)
                {

                    //build requestString
                    //https://unifieddatalibrary.com/udl/elset?epoch=2020-01-01T00:00:00.000000Z..2020-01-05T00:00:00.000000Z&satNo=40258

                    string timeString = "epoch=" + dtp_start.Value.AddDays(-1).ToString("yyyy-MM-ddTHH:mm:ss.000000Z") + ".." + dtp_end.Value.ToString("yyyy-MM-ddTHH:mm:ss.000000Z");

                    string satString = "";
                    for (int i = 0; i < allSSCs.Count; i++)
                    {
                        satString += "&satNo=" + int.Parse(allSSCs[i]).ToString("00000");
                    }

                    //init connection and form request
                    //TODO -- change to HTTPClient Class at some point
                    HttpWebRequest zrequest = (HttpWebRequest)WebRequest.Create(httpbaseURL + timeString + satString);
                    zrequest.Method = "GET";
                    zrequest.Headers["Authorization"] = creds;

                    //get response
                    WebResponse apiresponse = zrequest.GetResponse();
                    Stream zcontent = apiresponse.GetResponseStream();
                    if (zcontent != null)
                    {
                        //write file to temp directory
                        StreamReader sr = new StreamReader(zcontent);
                        string tempData = sr.ReadToEnd();
                        //StreamWriter tempfile = new StreamWriter(Path.Combine(workingUserDirectory, "UDLResponse.txt"));
                        //tempfile.Write(sr.ReadToEnd());
                        //tempfile.Close();
                        sr.Close();
                        zcontent.Close();
                        apiresponse.Close();
                        //create satellites from source file

                        JavaScriptSerializer zjss = new JavaScriptSerializer();
                        List<UdlJsonTleResponse> responseDataInFormat = zjss.Deserialize<List<UdlJsonTleResponse>>(tempData);
                        Dictionary<string,List<UdlJsonTleResponse>> sourceListandData = new Dictionary<string, List<UdlJsonTleResponse>>();

                        for (int zz = 0; zz < responseDataInFormat.Count; zz++)
                        {

                            if (!sourceListandData.ContainsKey(responseDataInFormat[zz].source))
                            {
                                //doesnt exist, create it
                                sourceListandData.Add(responseDataInFormat[zz].source, new List<UdlJsonTleResponse>());
                            }
                            //add to existing list
                            sourceListandData[responseDataInFormat[zz].source].Add(responseDataInFormat[zz]);

                        }

                        //create a tle file for each source
                        foreach (string zsource in sourceListandData.Keys)
                        {

                            using (StreamWriter sw = new StreamWriter(Path.Combine(workingUserDirectory, zsource + "_UDLResponse.tce")))
                            {

                                for (int zz = 0; zz < sourceListandData[zsource].Count; zz++)
                                {
                                    sw.WriteLine(sourceListandData[zsource][zz].line1);
                                    sw.WriteLine(sourceListandData[zsource][zz].line2);
                                }

                                sw.Close();
                            }

                            //create satellites
                            foreach (string ssc in allSSCs)
                            {
                                CreateSatellite(ssc, zsource, Path.Combine(workingUserDirectory, zsource + "_UDLResponse.tce"));
                            }

                        }
                        
                        //update label before creating satellites
                        SafelyUpdateLabel("TLE File written " + Environment.NewLine + "Created Satellites");



                        //stkRoot.ExecuteCommand(string.Format("ImportTLEFile * \"{0}\" Merge On AutoPropagate On", Path.Combine(workingUserDirectory, "UDLResponse.tce")));

                    }
                    else
                    {
                        SafelyUpdateLabel("No data returned " + Environment.NewLine + "Not Creating Satellites");
                    }


                }
                else
                {
                    SafelyUpdateLabel("No SSCs were input...");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            

        }
        
        
        //create satellite function
        private void CreateSatellite(string objID, string source, string fpath)
        {

            string sname = objID + "_" + source.Replace(' ', '_').Trim();

            if (!stkRoot.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite,sname))
            {
                //create new if non-existant
                stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, sname);
            }

            //handle to satellite
            IAgSatellite zsat = stkRoot.CurrentScenario.Children[sname] as IAgSatellite;
            zsat.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4);
            IAgVePropagatorSGP4 tleprop = zsat.Propagator as IAgVePropagatorSGP4;
            tleprop.Segments.RemoveAllSegs();//clear before adding new
            tleprop.CommonTasks.AddSegsFromFile(objID, fpath);
            tleprop.Propagate();


        }
        
        //quick helper functions to encode the username and password
        //for the request
        public static string Base64Encode(string ztext)
        {
            var textasbytes = Encoding.UTF8.GetBytes(ztext);
            return Convert.ToBase64String(textasbytes);
        }

        public static string Base64Decode(string zencodedData)
        {
            var dataasbytes = Convert.FromBase64String(zencodedData);
            return Encoding.UTF8.GetString(dataasbytes);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            InitConnection();
        }
    }
}

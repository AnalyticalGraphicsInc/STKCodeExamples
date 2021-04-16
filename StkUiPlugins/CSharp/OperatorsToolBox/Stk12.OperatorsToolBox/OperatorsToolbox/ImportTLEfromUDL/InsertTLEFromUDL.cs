using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.Ui.Application;
using OperatorsToolbox.SatelliteCreator;

namespace OperatorsToolbox.ImportTLEfromUDL
{
    //delegate function to update label outside form thread
    //needed so it actually works
    delegate void UpdateLabelText(string str);

    public partial class InsertTleFromUdl : OpsPluginControl
    {
        //global stuff ... yes this is wrong and should be in 
        //a config file... TODO, create CONFIG and put values in it

        AgUiApplication _uiApp;
        AgStkObjectRoot _stkRoot = CommonData.StkRoot;
        IAgScenario _zscen = CommonData.StkRoot.CurrentScenario as IAgScenario;
        bool _stkconnected = false;
        string _workingUserDirectory = "";
        string _httpbaseUrl = "";
        string _baseUrl = CommonData.Preferences.UdlUrl;


        public string[] ParsingFormats = new string[] { "dd MMM yyyy HH:mm:ss", "d MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss.000", "d MMM yyyy HH:mm:ss.000" }; //add others to this as we find them


        public InsertTleFromUdl()
        {
            InitializeComponent();
            InitUserProperties();
            PopulateExistingConstellations();

            ConstType.Items.Add("Default Constellation");
            ConstType.Items.Add("Existing Constellation");
            ConstType.Items.Add("New Constellation");
            ConstType.SelectedIndex = 0;
            ConstName.Enabled = false;
            ConstName.Visible = false;
            NameLabel.Visible = false;
            ExistingConst.Enabled = false;
            ExistingConst.Visible = false;

            CoordSystem.Items.Add("Fixed");
            CoordSystem.Items.Add("Inertial");
            CoordSystem.SelectedIndex = 1;

            CreateSats.Checked = true;
        }

        //reads settings and populates
        //appropriate fields
        private void InitUserProperties()
        {
            try
            {

                tb_userName.Text = "";
                tb_password.Text = "";
                _httpbaseUrl = _baseUrl;
                _workingUserDirectory = CommonData.DirectoryStr;

                //refresh scenario times
                dtp_start.Value = DateTime.ParseExact(_zscen.StartTime, ParsingFormats, null, System.Globalization.DateTimeStyles.None);
                dtp_end.Value = DateTime.ParseExact(_zscen.StopTime, ParsingFormats, null, System.Globalization.DateTimeStyles.None);
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
            if (!_stkconnected)
            {
                // Get reference to running STK instance 
                _uiApp = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AgUiApplication;

                // Get our IAgStkObjectRoot interface 
                _stkRoot = _uiApp.Personality2 as AgStkObjectRoot;
                _zscen = _stkRoot.CurrentScenario as IAgScenario;
                _stkconnected = true;

                dtp_start.Value = DateTime.ParseExact(_zscen.StartTime, ParsingFormats, null, System.Globalization.DateTimeStyles.None);
                dtp_end.Value = DateTime.ParseExact(_zscen.StopTime, ParsingFormats, null, System.Globalization.DateTimeStyles.None);

                //btn_refresh.BackColor = Color.LimeGreen;
            }
            else
            {
                //refresh scenario times
                dtp_start.Value = DateTime.ParseExact(_zscen.StartTime, ParsingFormats, null, System.Globalization.DateTimeStyles.None);
                dtp_end.Value = DateTime.ParseExact(_zscen.StopTime, ParsingFormats, null, System.Globalization.DateTimeStyles.None);
            }


            if (string.IsNullOrEmpty(_workingUserDirectory))
            {
                //get user directory for working purposes
                IAgExecCmdResult conRes = _stkRoot.ExecuteCommand(@"GetDirectory / UserData");
                _workingUserDirectory = conRes[0];

            }

            SafelyUpdateLabel("Connected and using: " + Environment.NewLine + _workingUserDirectory);

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
            if (NameBasedOnCatalog.Checked)
            {
                if (CommonData.SatCatItemList !=null)
                {
                    if (!(CommonData.SatCatItemList.Count > 0))
                    {
                        ReadWrite.ReadSatCat();
                    }
                }
                else
                {
                    CommonData.SatCatItemList = new List<SatCatItem>();
                    CommonData.MetadataTypeList = new List<string>();
                    CommonData.MetadataOptions1 = new List<string>();
                    CommonData.MetadataOptions2 = new List<string>();
                    CommonData.MetadataOptions3 = new List<string>();
                    CommonData.MetadataOptions4 = new List<string>();
                    CommonData.MetadataOptions5 = new List<string>();
                    CommonData.SatCatFofo = new List<string>();
                    ReadWrite.ReadSatCat();
                }
            }
            try
            {
                //encode password and username
                string creds = "Basic " + Base64Encode(tb_userName.Text + ":" + tb_password.Text);

                List<string> allSsCs = tb_ssc.Text.Split(new char[] { '\r', '\n', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < allSsCs.Count; i++)
                {
                    allSsCs[i] = allSsCs[i].Trim();
                }

                if (allSsCs.Count > 0)
                {

                    //build requestString
                    //https://unifieddatalibrary.com/udl/elset?epoch=2020-01-01T00:00:00.000000Z..2020-01-05T00:00:00.000000Z&satNo=40258

                    string timeString = "epoch=" + dtp_start.Value.AddDays(-1).ToString("yyyy-MM-ddTHH:mm:ss.000000Z") + ".." + dtp_end.Value.ToString("yyyy-MM-ddTHH:mm:ss.000000Z");

                    string satString = "";
                    for (int i = 0; i < allSsCs.Count; i++)
                    {
                        satString += "&satNo=" + int.Parse(allSsCs[i]).ToString("00000");
                    }

                    //init connection and form request
                    //TODO -- change to HTTPClient Class at some point
                    HttpWebRequest zrequest = (HttpWebRequest)WebRequest.Create(_httpbaseUrl + timeString + satString);
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
                        Dictionary<string, List<UdlJsonTleResponse>> sourceListandData = new Dictionary<string, List<UdlJsonTleResponse>>();

                        for (int zz = 0; zz < responseDataInFormat.Count; zz++)
                        {

                            if (!sourceListandData.ContainsKey(responseDataInFormat[zz].Source))
                            {
                                //doesnt exist, create it
                                sourceListandData.Add(responseDataInFormat[zz].Source, new List<UdlJsonTleResponse>());
                            }
                            //add to existing list
                            sourceListandData[responseDataInFormat[zz].Source].Add(responseDataInFormat[zz]);

                        }

                        //create a tle file for each source
                        foreach (string zsource in sourceListandData.Keys)
                        {

                            using (StreamWriter sw = new StreamWriter(Path.Combine(_workingUserDirectory, zsource + "_UDLResponse.tce")))
                            {

                                for (int zz = 0; zz < sourceListandData[zsource].Count; zz++)
                                {
                                    sw.WriteLine(sourceListandData[zsource][zz].Line1);
                                    sw.WriteLine(sourceListandData[zsource][zz].Line2);
                                }

                                sw.Close();
                            }

                            //create satellites
                            foreach (string ssc in allSsCs)
                            {
                                if (CreateSats.Checked)
                                {
                                    try
                                    {
                                        CreateSatellite(ssc, zsource, Path.Combine(_workingUserDirectory, zsource + "_UDLResponse.tce"));
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message.ToString());
                                    }
                                }
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
            PopulateExistingConstellations();

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaisePanelCloseEvent();
        }

        //create satellite function
        private void CreateSatellite(string objId, string source, string fpath)
        {
            IAgStkObject sat = null;
            try
            {
                int index = -1;
                string sname = null;
                string objPath = null;
                if (NameBasedOnCatalog.Checked)
                {
                    index = CommonData.SatCatItemList.IndexOf(CommonData.SatCatItemList.Where(p => p.Scc == objId).FirstOrDefault());
                    if (index != -1)
                    {
                        SatCatItem currentSat = CommonData.SatCatItemList[index];
                        if (currentSat.OtherName != "Unspecified")
                        {
                            string otherName = currentSat.OtherName.Replace(" ", "_");
                            otherName = Regex.Replace(otherName, @"[^0-9a-zA-Z_]+", "");
                            objPath = "Satellite/" + otherName;
                            sname = otherName;
                        }
                        else if (currentSat.CommonName != "Unspecified")
                        {
                            string commonName = currentSat.CommonName.Replace(" ", "_");
                            commonName = Regex.Replace(commonName, @"[^0-9a-zA-Z_]+", "");
                            objPath = "Satellite/" + commonName;
                            sname = commonName;
                        }
                        else
                        {
                            sname = objId + "_" + source.Replace(' ', '_').Trim();
                        }
                    }
                    else
                    {
                        sname = objId + "_" + source.Replace(' ', '_').Trim();
                    }
                }
                else
                {
                    sname = objId + "_" + source.Replace(' ', '_').Trim();
                }

                //create new if non-existant or get handle
                sat = CreatorFunctions.GetCreateSatellite(sname);

                //handle to satellite
                IAgSatellite zsat = sat as IAgSatellite;
                //Erase old TLE data and replace it with new data from specified file
                zsat.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4);
                IAgVePropagatorSGP4 tleprop = zsat.Propagator as IAgVePropagatorSGP4;
                tleprop.Segments.RemoveAllSegs();//clear before adding new
                tleprop.CommonTasks.AddSegsFromFile(objId, fpath); //objID is scc, fpath is the filepath to the tle file
                tleprop.Propagate();
                zsat.Graphics.Resolution.Orbit = 10;
                if (NameBasedOnCatalog.Checked && index != -1)
                {
                    CreatorFunctions.ChangeSatColor(objPath, index);
                }

                if (CoordSystem.SelectedIndex == 0)
                {
                    zsat.VO.OrbitSystems.FixedByWindow.IsVisible = true;
                    zsat.VO.OrbitSystems.InertialByWindow.IsVisible = false;
                    string cmd = "VO */Satellite/" + sname + " ModelDetail Set ModelLabel 2000000000 MarkerLabel 2000000000";
                    try
                    {
                        CommonData.StkRoot.ExecuteCommand(cmd);
                    }
                    catch (Exception)
                    {
                    }
                }

                //Place into constellation
                IAgConstellation assets = null;
                if ((string)ConstType.SelectedItem == "Default Constellation")
                {
                    IAgStkObject conste = CreatorFunctions.GetCreateConstellation("Assets");
                    assets = conste as IAgConstellation;
                }
                else if ((string)ConstType.SelectedItem == "Existing Constellation")
                {
                    assets = (IAgConstellation)CommonData.StkRoot.GetObjectFromPath("Constellation/" + ExistingConst.Text);
                }
                else if ((string)ConstType.SelectedItem == "New Constellation")
                {
                    IAgStkObject conste = CreatorFunctions.GetCreateConstellation(ConstName.Text.Replace(" ","_"));
                    assets = conste as IAgConstellation;
                }
                objPath = "Satellite/" + sname;
                if (assets.Objects.IndexOf(objPath) == -1 && assets != null)
                {
                    assets.Objects.Add(objPath);
                }
            }
            catch (Exception ex)
            {
                //Identify the satellite with an issue
                MessageBox.Show(ex.Message + "\n"+"SSC: "+objId);
                //unload satellite if possible so there is not an empty object in STK from an error
                //try
                //{
                //    sat.Unload();
                //}
                //catch (Exception)
                //{
                //}
            }
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

        private void PopulateExistingConstellations()
        {
            IAgExecCmdResult result;
            ExistingConst.Items.Clear();
            result = CommonData.StkRoot.ExecuteCommand("ShowNames * Class Constellation");
            if (result[0] != "None")
            {
                string[] constArray = result[0].Split(null);
                foreach (var item in constArray)
                {
                    string newItem = item.Split('/').Last();
                    if (newItem != "" && newItem != null)
                    {
                        ExistingConst.Items.Add(newItem);
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tb_userName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tb_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void InsertTLEFromUDL_Load(object sender, EventArgs e)
        {

        }

        private void ConstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)ConstType.SelectedItem == "Default Constellation")
            {
                ConstName.Enabled = false;
                ConstName.Visible = false;
                NameLabel.Visible = false;
                ExistingConst.Enabled = false;
                ExistingConst.Visible = false;
            }
            else if ((string)ConstType.SelectedItem == "Existing Constellation")
            {
                ConstName.Enabled = false;
                ConstName.Visible = false;
                NameLabel.Visible = true;
                ExistingConst.Enabled = true;
                ExistingConst.Visible = true;
            }
            else if ((string)ConstType.SelectedItem == "New Constellation")
            {
                ConstName.Enabled = true;
                ConstName.Visible = true;
                NameLabel.Visible = true;
                ExistingConst.Enabled = false;
                ExistingConst.Visible = false;
            }
        }
    }
}

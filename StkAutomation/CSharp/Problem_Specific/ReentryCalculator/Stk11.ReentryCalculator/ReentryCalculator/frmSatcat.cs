using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReentryCalculator
{
    public partial class frmSatcat : Form
    {
        public frmSatcat()
        {
            InitializeComponent();
        }

        private void updateSatcatDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblStatus.Text = "Downloading data...";
                this.statusStrip1.Update();
                WebClient client = new WebClient();
                String htmlText = client.DownloadString("https://celestrak.com/pub/satcat.txt");
                System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\satcat.txt", htmlText);
                MessageBox.Show("SATCAT data successfully downloaded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.lblStatus.Text = "";
            this.statusStrip1.Update();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.tbKeyword.Text.ToUpper();
            string satcatFilePath = Directory.GetCurrentDirectory() + "\\data\\satcat.txt";

            if (!File.Exists(satcatFilePath))
            {
                MessageBox.Show("SATCAT file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.dgvSatList.Rows.Clear();
                StreamReader satcatFile = new StreamReader(satcatFilePath);
                while (satcatFile.Peek() > -1)
                {
                    string line = satcatFile.ReadLine();
                    if (line.Contains(keyword))
                    {
                        this.dgvSatList.Rows.Add(line.Substring(0, 11).TrimEnd(' '), line.Substring(13, 5).TrimEnd(' '), line.Substring(23, 26).TrimEnd(' '));
                    }
                }
            }
        }
    }
}

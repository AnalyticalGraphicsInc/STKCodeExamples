using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace STK12.UIPlugin.Copier.TetkExample
{
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
            TryToPopulate();
        }

        private void TryToPopulate()
        {
            //get current running directory
            string startDir = Directory.GetCurrentDirectory();

            //get project folder
            string folderDir = GetProjectDirectory(startDir);

            //get solution folder
            DirectoryInfo parentDir = Directory.GetParent(folderDir);

            //get all project folders
            DirectoryInfo[] dirs = parentDir.GetDirectories();

            //make sure there are only two projects
            if (dirs.Length == 2)
            {
                string uipluginDirectory;
                if (dirs[0].FullName != folderDir)
                {
                    uipluginDirectory = dirs[0].FullName;
                }
                else
                {
                    uipluginDirectory = dirs[1].FullName;
                }

                string setup = File.ReadAllText(Path.Combine(uipluginDirectory, "Setup.cs"));
                int pos = setup.IndexOf("namespace");
                pos += 10;
                if (pos > -1)
                {
                    int pos1 = setup.IndexOf("\r", pos);
                    string namespaceString = setup.Substring(pos, pos1 - pos);
                    txtAssemblyName.Text = namespaceString;
                    txtTypeName.Text = namespaceString + "." + "Setup";
                }


            }
        }


        private void CreateZipFile(string pluginDirectory, string folderPath)
        {
            string[] files = System.IO.Directory.GetFiles(pluginDirectory);
            foreach (string file in files)
            {
                Zip.AddFileToZip("Bin.zip", file);
            }
            File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "Bin.zip"), Path.Combine(folderPath, "Bin.Zip"), true);

        }

        private void SetupInstall()
        {
            string startDir = Directory.GetCurrentDirectory();
            string folderDir = GetProjectDirectory(startDir);
            string xmlFile;

            using (StreamReader reader = new StreamReader(Path.Combine(folderDir, "app.config")))
            {
                xmlFile = reader.ReadToEnd();
                reader.Close();
            }

            xmlFile = xmlFile.Replace("<value>AssemblyName</value>", "<value>" + txtAssemblyName.Text + "</value>");
            xmlFile = xmlFile.Replace("<value>DisplayName</value>", "<value>" + txtDisplayName.Text + "</value>");
            xmlFile = xmlFile.Replace("<value>TypeName</value>", "<value>" + txtTypeName.Text + "</value>");

            using (StreamWriter writer = new StreamWriter(Path.Combine(folderDir, "app.config")))
            {
                writer.Write(xmlFile);
                writer.Close();
            }



            if (txtUIPluginLocation.Text != "")
            {
                CreateZipFile(txtUIPluginLocation.Text, folderDir);
            }
        }

        private string GetProjectDirectory(string startPath)
        {
            bool found = false;
            string[] files = Directory.GetFiles(startPath);
            foreach (string file in files)
            {
                if (file == Path.Combine(startPath, "app.config"))
                {
                    found = true;
                }
            }
            if (found)
            {
                return startPath;
            }
            else
            {
                DirectoryInfo parentDir = Directory.GetParent(startPath);
                return GetProjectDirectory(parentDir.FullName);
            }
        }


        private void btnUIPluginLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();



            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtUIPluginLocation.Text = folderBrowserDialog.SelectedPath;
            }

        }

        private void btnRunSetup_Click(object sender, EventArgs e)
        {
            SetupInstall();
            MessageBox.Show("Done.");
        }

    }
}

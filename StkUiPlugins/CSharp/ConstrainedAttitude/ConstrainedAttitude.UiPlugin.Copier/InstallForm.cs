using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace ConstrainedAttitude.UiPlugin.Copier
{
    public partial class InstallForm : Form
    {
        string m_programData32;
        string m_programData64;
        public InstallForm()
        {
            InitializeComponent();
            string assemblyName = Properties.Settings.Default.AssemblyName;
            m_programData32 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            m_programData64 = Path.Combine(m_programData32, @"AGI\STK 12\Plugins");

            txtBinaryLocation.Text = Path.Combine(m_programData64, assemblyName);



        }

        private void WriteXMLFile(string installDirectory, string pluginDirectory)
        {
            string displayName = ConstrainedAttitude.UiPlugin.Copier.Properties.Settings.Default.DisplayName;
            string typeName = ConstrainedAttitude.UiPlugin.Copier.Properties.Settings.Default.TypeName;
            string assemblyName = ConstrainedAttitude.UiPlugin.Copier.Properties.Settings.Default.AssemblyName;

            using (StreamWriter writer = new StreamWriter(Path.Combine(pluginDirectory, typeName + ".xml")))
            {
                writer.Write(CreateXMLFile(displayName, typeName, assemblyName, installDirectory));
                writer.Close();
            }
        }


        private string CreateXMLFile(string displayName, string typeName, string assemblyName, string installDirectory)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<?xml version = \"1.0\"?>");
            stringBuilder.AppendLine("<AGIRegistry version = \"1.0\">");
            stringBuilder.AppendLine("<CategoryRegistry>");
            stringBuilder.AppendLine("<Category Name = \"UiPlugins\">");
            stringBuilder.AppendLine("<NETUiPlugin");
            stringBuilder.AppendLine("DisplayName=\"" + displayName + "\"");
            stringBuilder.AppendLine("TypeName=\"" + typeName + "\"");
            stringBuilder.AppendLine("AssemblyName=\"" + assemblyName + "\"");
            stringBuilder.AppendLine("CodeBase=\"" + installDirectory + "\">");
            stringBuilder.AppendLine("</NETUiPlugin>");
            stringBuilder.AppendLine("</Category>");
            stringBuilder.AppendLine("</CategoryRegistry>");
            stringBuilder.AppendLine("</AGIRegistry>");
            return stringBuilder.ToString();
        }

        private void UnZIPBinnaries(string installDirectory)
        {
            string zipLocation = Path.Combine(installDirectory, "bin.zip");
            File.WriteAllBytes(zipLocation, Properties.Resources.Bin);

            Zip.UnzipFiles(zipLocation, installDirectory);
            //FileInfo zipFile = new FileInfo(zipLocation);
            //Zip.Decompress(zipFile);

        }

        private void InstallPlugin()
        {

            string installDirectory = txtBinaryLocation.Text;
            if (installDirectory != "")
            {
                if (!Directory.Exists(installDirectory))
                {
                    Directory.CreateDirectory(installDirectory);
                }
                UnZIPBinnaries(installDirectory);
                WriteXMLFile(installDirectory, m_programData64);

                MessageBox.Show("Done.");
            }

        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            InstallPlugin();
        }

        private void btnBinaryLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtBinaryLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}

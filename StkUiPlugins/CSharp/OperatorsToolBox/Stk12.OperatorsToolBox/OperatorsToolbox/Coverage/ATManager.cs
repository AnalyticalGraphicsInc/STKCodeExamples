using AGI.STKObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox.Coverage
{
    public partial class ATManager : Form
    {
        private List<string> atNames;
        public ATManager()
        {
            InitializeComponent();
            AdditionType.Items.Add("Single");
            AdditionType.Items.Add("Group");
            AdditionType.SelectedIndex = 0;
            atNames = CreatorFunctions.PopulateListByClass("AreaTarget");
            string simpleName;
            foreach (var item in atNames)
            {
                simpleName = item.Split('/').Last();
                ATList.Items.Add(simpleName);
                SingleATName.Items.Add(simpleName);
            }
            if (atNames.Count>0)
            {
                SingleATName.SelectedIndex = 0;
            }
        }

        private void AdditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AdditionType.SelectedIndex == 0)
            {
                GroupGroup.Visible = false;
                GroupGroup.Enabled = false;
                SingleGroup.Visible = true;
                SingleGroup.Enabled = true;
            }
            else if (AdditionType.SelectedIndex == 1)
            {
                GroupGroup.Visible = true;
                GroupGroup.Enabled = true;
                SingleGroup.Visible = false;
                SingleGroup.Enabled = false;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            List<string> names = new List<string>();
            string dir = Path.GetDirectoryName(CommonData.Preferences.AoiLocation);
            string pathName;
            IAgStkObject myObj;
            if (AdditionType.SelectedIndex == 0)
            {
                myObj = CommonData.StkRoot.GetObjectFromPath("AreaTarget/" + SingleATName.Text);
                pathName = Path.Combine(dir, SingleATName.Text + ".at");
                myObj.Export(pathName);
                names.Add(SingleATName.Text);
                File.AppendAllLines(CommonData.Preferences.AoiLocation, names);
            }
            else if (AdditionType.SelectedIndex == 1)
            {
                string groupNameStr = GroupName.Text + "-Group";
                string text = null;
                foreach (var item in ATList.CheckedItems)
                {
                    text = text + item.ToString() + "\n";
                    names.Add(item.ToString());
                    myObj = CommonData.StkRoot.GetObjectFromPath("AreaTarget/" + item.ToString());
                    pathName = Path.Combine(dir, item.ToString() + ".at");
                    myObj.Export(pathName);
                }
                File.WriteAllText(Path.Combine(dir, groupNameStr + ".txt"),text);
                names.Clear();
                names.Add(groupNameStr);
                File.AppendAllLines(CommonData.Preferences.AoiLocation, names);

            }
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox.PlaneCrossingUtility
{
    public partial class CrossingOutputForm : Form
    {
        public CrossingOutputForm(List<PlaneCrossingGroup> allCrossings)
        {
            InitializeComponent();
            PopulateTable(allCrossings);
        }

        private void SetAnimationTime_Click(object sender, EventArgs e)
        {
            int index = CrossingOutputTable.FocusedItem.Index;
            if (CrossingOutputTable.FocusedItem != null && index != -1)
            {
                try
                {
                    CommonData.StkRoot.ExecuteCommand("SetAnimation * CurrentTime \"" + CrossingOutputTable.Items[index].SubItems[1].Text + "\"");
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not update animation time");
                }
            }
        }

        private void PopulateTable(List<PlaneCrossingGroup> allCrossingData)
        {
            foreach (var group in allCrossingData)
            {
                foreach (var crossing in group.PlaneCrossings)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.SubItems[0].Text = group.CrossingObjectName;
                    listItem.SubItems.Add(crossing.CrossingTime);
                    if (crossing.IsBounded)
                    {
                        listItem.SubItems.Add(crossing.LowerBoundCrossingTime);
                        listItem.SubItems.Add(crossing.UpperBoundCrossingTime);
                    }
                    else
                    {
                        listItem.SubItems.Add("N/A");
                        listItem.SubItems.Add("N/A");
                    }
                    CrossingOutputTable.Items.Add(listItem);
                }
            }
        }
    }
}

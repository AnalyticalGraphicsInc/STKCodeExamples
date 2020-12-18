using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsToolbox.GroundEvents
{
    public partial class ContactInfoForm : Form
    {
        public GroundEvent contactEvent { get; set; }
        public ContactInfoForm()
        {
            InitializeComponent();
            contactEvent = new GroundEvent();
        }
        public ContactInfoForm(GroundEvent groundEvent)
        {
            InitializeComponent();
            contactEvent = new GroundEvent();
            if (!String.IsNullOrEmpty(groundEvent.Poc))
            {
                POCName.Text = groundEvent.Poc;
            }
            if (!String.IsNullOrEmpty(groundEvent.PocPhone))
            {
                POCPhone.Text = groundEvent.PocPhone;
            }
            if (!String.IsNullOrEmpty(groundEvent.PocEmail))
            {
                POCEmail.Text = groundEvent.PocEmail;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(POCName.Text))
            {
                contactEvent.Poc = POCName.Text;
            }
            else
            {
                contactEvent.Poc = "";
            }
            if (!String.IsNullOrEmpty(POCPhone.Text))
            {
                contactEvent.PocPhone = POCPhone.Text;
            }
            else
            {
                contactEvent.PocPhone = "";
            }
            if (!String.IsNullOrEmpty(POCEmail.Text))
            {
                contactEvent.PocEmail = POCEmail.Text;
            }
            else
            {
                contactEvent.PocEmail = "";
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}

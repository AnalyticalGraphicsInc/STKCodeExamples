using System;
using System.Windows.Forms;

namespace Stk12.UiPlugin.ModelPixelUpdater
{
    public partial class ScaleFactorForm : Form
    {
        public double scaleFactor;
        public bool accepted;

        public ScaleFactorForm()
        {
            InitializeComponent();
            scaleFactor = 1;
            textBox_ScaleFactor.Text = scaleFactor.ToString();
        }

        private void textBox_ScaleFactor_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox_ScaleFactor.Text, out scaleFactor))
            {
                MessageBox.Show("Please Enter a valid number for scale");
            }
            
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            accepted = false;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            accepted = true;
            this.Close();
        }
    }
}

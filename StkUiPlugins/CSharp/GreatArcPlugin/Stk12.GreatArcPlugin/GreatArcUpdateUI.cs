using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Agi.Ui.GreatArc.Stk12
{
    public partial class GreatArcUpdateUI : Form
    {

        public double WaypointParameter{get;set;}
        public string ParameterUnits;


        public GreatArcUpdateUI(string updateProperty)
        {
            InitializeComponent();



            string[] speedUnits = new string[3] { "mph", "knots", "km/sec" };
            string[] distanceUnits = new string[3] { "ft", "m", "km" };
            string[] angleUnits = new string[2]{"deg", "rad"};

            combobox_Units.Items.Clear();
            switch (updateProperty)
            {
                case "Speed":
                    this.Text = "Set Speed";
                    textbox_Parameter.Text = "60";
                    label_Property.Text = "Speed:";
                    foreach (string unit in speedUnits)
                    {
                        combobox_Units.Items.Add(unit);
                    }
                    combobox_Units.Text = combobox_Units.Items[0].ToString();
                    ParameterUnits = combobox_Units.Items[0].ToString();
                    break;
                case "Altitude":
                    this.Text = "Set Altitude";
                    textbox_Parameter.Text = "15000";
                    label_Property.Text = "Altitude:";
                    foreach (string unit in distanceUnits)
                    {
                        combobox_Units.Items.Add(unit);
                    }
                    combobox_Units.Text = combobox_Units.Items[0].ToString();
                    ParameterUnits = combobox_Units.Items[0].ToString();
                    break;
                case "TurnRadius":
                    this.Text = "Set Turn Radius";
                    textbox_Parameter.Text = "15";
                    label_Property.Text = "Turn Radius:";
                    foreach (string unit in distanceUnits)
                    {
                        combobox_Units.Items.Add(unit);
                    }
                    combobox_Units.Text = combobox_Units.Items[0].ToString();
                    ParameterUnits = combobox_Units.Items[0].ToString();
                    break;
                case "Latitude":
                case "Longitude":

                    this.Text = "Shift " + updateProperty + ":";
                    textbox_Parameter.Text = "15";
                    label_Property.Text = "Shift Position:";
                    foreach (string unit in angleUnits)
                    {
                        combobox_Units.Items.Add(unit);
                    }
                    combobox_Units.Text = combobox_Units.Items[0].ToString();
                    ParameterUnits = combobox_Units.Items[0].ToString();
                    break;
                
                default:
                    break;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            WaypointParameter = double.Parse(textbox_Parameter.Text);
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void combobox_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParameterUnits = combobox_Units.Text;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace motor_control
{
    public partial class PortSelector : Form
    {
        public PortSelector()
        {
            InitializeComponent();
            InsertPortNames();

            comboBoxArduinoPort.SelectedIndex = 0;
        }

        //get array of strings
        void InsertPortNames()
        {
            comboBoxArduinoPort.Items.Clear();

            string[] portNames = SerialComChannel.GetPortNames();
            foreach(string name in portNames)
            {
                comboBoxArduinoPort.Items.Add(name);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Returns selected port for the Arduino
        public string arduinoPortName()
        {
            return comboBoxArduinoPort.Text;
        }

        public bool testingModeChecked()
        {
            return testingMode.Checked;
        }

        private void refreshbutton_Click(object sender, EventArgs e)
        {
            InsertPortNames();
        }

        private void comboBoxNavPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxArduinoPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class ArduinoError : Form
    {

        // Store the verdict from the buttons
        private int _verdict;

        public ArduinoError(String version)
        {
            InitializeComponent();
            versionLabel.Text = version;
        }
        
        // Public property to allow access to _verdict
        public verdicts verdict
        {
            get { return (verdicts) _verdict; }
        }

        private void yes_Click(object sender, EventArgs e)
        {
            _verdict = 0;
            this.Close();
        }

        private void disable_Click(object sender, EventArgs e)
        {
            _verdict = 1;
            this.Close();
        }

        private void ignore_Click(object sender, EventArgs e)
        {
            _verdict = 2;
            this.Close();
        }

    }

    public enum verdicts { Compatibliy, Disable, Ignore }
}

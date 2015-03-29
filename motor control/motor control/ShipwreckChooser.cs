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
    public partial class ShipwreckChooser : Form
    {
        public ShipwreckChooser()
        {
            InitializeComponent();
        }


        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            label6.Text = "Sault";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            label6.Text = "Detroit";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "Wooden";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "Steam";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "Propeller";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "Grain";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "Coal";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "1912";
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "1909";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "1881";
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "1873";
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "1854";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //calculates total surface of ship
            Double Length = double.Parse(textBox1.Text);
            Double Width = double.Parse(textBox2.Text);
            Double Height = double.Parse(textBox3.Text);
            Double Two_Sides = (Length * Height) * 2;
            Double Front = Width * Height;
            Double Back = Width * Height;
            Double Top = Length * Width;
            Double TotalSurfaceArea = Front + Back + Top + Two_Sides;
            //Units change depending on the Unit used to multiply
            label16.Text = TotalSurfaceArea.ToString();
            //label15.Text = "Total Surface Area: " + TotalSurfaceArea.ToString() +" Square Meters";
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Double Mussels = double.Parse(textBox4.Text);
            Mussels *= 4;
            Double Total_Mussels;
            try
            {
                Total_Mussels = Mussels * double.Parse(label16.Text);
            }
            catch
            {
                Total_Mussels = 0;
            }
            label10.Text = "Total Amount Of Mussels: " + (Total_Mussels);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool Cargo_Coal = radioButton5.Checked;
            bool Cargo_Grain = radioButton4.Checked;
            bool Date_1854 = radioButton12.Checked;
            bool Date_1873 = radioButton14.Checked;
            bool Date_1881 = radioButton10.Checked;
            bool Date_1909 = radioButton13.Checked;
            bool Date_1912 = radioButton11.Checked;
            bool Detroit = radioButton8.Checked;
            bool Sault = radioButton6.Checked;
            bool Type_Wooden = radioButton1.Checked;
            bool Type_Steam = radioButton2.Checked;
            bool Type_Propeller = radioButton3.Checked;
            if (Type_Propeller && Date_1912 && Detroit && Cargo_Grain)
            {
                label1.Text = "A Empleo";
            }
            else if (Type_Propeller && Date_1912 && Detroit && Cargo_Coal)
            {
                label1.Text = "Addison W";
            }
            else if (Type_Steam && Date_1881 && Sault && Cargo_Coal)
            {
                label1.Text = "D Breaux";
            }
            else if (Type_Wooden && Date_1854 && Detroit && Cargo_Coal)
            {
                label1.Text = "Deidre E Sullivan";
            }
            else if (Type_Steam && Date_1873 && Sault && Cargo_Grain)
            {
                label1.Text = "Double R Rupan";
            }
            else if (Type_Steam && Date_1873 && Detroit && Cargo_Grain)
            {
                label1.Text = "Erica Moulton";
            }
            else if (Type_Propeller && Date_1909 && Sault && Cargo_Coal)
            {
                label1.Text = "Fraser";
            }
            else if (Type_Steam && Date_1881 && Detroit && Cargo_Coal)
            {
                label1.Text = "Gordon G";
            }
            else if (Type_Wooden && Date_1854 && Detroit && Cargo_Grain)
            {
                label1.Text = "J Gray";
            }
            else if (Type_Propeller && Date_1912 && Sault && Cargo_Grain)
            {
                label1.Text = "J Hertzberg";
            }
            else if (Type_Wooden && Date_1854 && Sault && Cargo_Grain)
            {
                label1.Text = "Jann Hooyer";
            }
            else if (Type_Steam && Date_1873 && Detroit && Cargo_Coal)
            {
                label1.Text = "Jill M Zande";
            }
            else if (Type_Propeller && Date_1912 && Sault && Cargo_Coal)
            {
                label1.Text = "Justin M";
            }
            else if (Type_Steam && Date_1881 && Sault && Cargo_Grain)
            {
                label1.Text = "Kathryn L";
            }
            else if (Type_Wooden && Date_1873 && Detroit && Cargo_Coal)
            {
                label1.Text = "L Herbert";
            }
            else if (Type_Propeller && Date_1909 && Detroit && Cargo_Grain)
            {
                label1.Text = "T Lunsford";
            }
            else if (Type_Steam && Date_1873 && Sault && Cargo_Coal)
            {
                label1.Text = "Matthew E";
            }
            else if (Type_Wooden && Date_1873 && Sault && Cargo_Grain)
            {
                label1.Text = "Rachel G";
            }
            else if (Type_Propeller && Date_1909 && Detroit && Cargo_Coal)
            {
                label1.Text = "S Gandulla";
            }
            else if (Type_Steam && Date_1881 && Detroit && Cargo_Grain)
            {
                label1.Text = "Sarah W";
            }
            else if (Type_Propeller && Date_1909 && Sault && Cargo_Grain)
            {
                label1.Text = "Stahr Liner";
            }
            else if (Type_Wooden && Date_1873 && Sault && Cargo_Coal)
            {
                label1.Text = "T Sinclair";
            }
            else if (Type_Wooden && Date_1854 && Detroit && Cargo_Coal)
            {
                label1.Text = "Tony S";
            }
            else if (Type_Wooden && Date_1873 && Detroit && Cargo_Grain)
            {
                label1.Text = "W Thompson";
            }
            else
            {
                label1.Text = "Unrecognized Ship";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void measurementCalculate_Click(object sender, EventArgs e)
        {
            measurement.Text = GetCalVal().ToString();
        }

        private float GetCalVal()
        {
            try
            {
                return MeasurementCalibration.GetCalibrationValue(float.Parse(screenUnitsBox.Text));
            }
            catch
            {
                return 0;
            }
        }

        private void popLength_Click(object sender, EventArgs e)
        {
            float f = GetCalVal() + .6f;
            textBox1.Text = f.ToString();
        }

        private void popWidth_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetCalVal().ToString();
        }

        private void popHeight_Click(object sender, EventArgs e)
        {
            textBox3.Text = GetCalVal().ToString();
        }
    }
}

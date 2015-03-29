//#define ENABLE_DIGITAL_GRAPH

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
    public partial class TemperatureGraph : Form
    {
        private double currentX;
        private int interval;
        private int count;
        delegate void SetTextCallback(double y);
        

        public TemperatureGraph(int intervalInMs)
        {
            InitializeComponent();
            interval = intervalInMs;
            Load += new EventHandler(TemperatureGraph_Load);
            currentX = 0;
#if ENABLE_DIGITAL_GRAPH
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[2].Name = "Temp (D)";
#endif //ENABLE_DIGITAL_GRAPH
#if !ENABLE_DIGITAL_GRAPH
            dataGridView1.ColumnCount = 2;
#endif //!ENABLE_DIGITAL_GRAPH
            dataGridView1.Columns[1].Name = "Temp (A)";
            dataGridView1.Columns[0].Name = "Mins";
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 50;
            }
        }


        public int PopulateGraph(Temperatures temps)
        {
            try
            {
#if ENABLE_DIGITAL_GRAPH
                chart1.Series["Digital"].Points.AddXY(currentX, temps.digital);
#endif //ENABLE_DIGITAL_GRAPH
                chart1.Series["Analog"].Points.AddXY(currentX, temps.analog);

#if ENABLE_DIGITAL_GRAPH
                string[] data = { currentX.ToString(), temps.digital.ToString(), temps.analog.ToString() };
#endif //ENABLE_DIGITAL_GRAPH
#if !ENABLE_DIGITAL_GRAPH
                string[] data = { currentX.ToString(), temps.analog.ToString() };
#endif //!ENABLE_DIGITAL_GRAPH
                DataGridViewRowCollection rows = dataGridView1.Rows;
                rows.Add(data);

                currentX += interval / 1000.0f / 60.0f;
            }
            catch
            {
            }
            count++;
            return count;
        }


        

        private void TemperatureGraph_Load(object sender, System.EventArgs e)
        {
            chart1.Series["Digital"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["Digital"].Color = Color.Red;
            chart1.Series["Analog"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["Analog"].Color = Color.Blue;
        }


        private void useLineGraph_Click(object sender, EventArgs e)
        {
#if ENABLE_DIGITAL_GRAPH
            chart1.Series["Digital"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
#endif //ENABLE_DIGITAL_GRAPH
            chart1.Series["Analog"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        private void useBarGraph_Click(object sender, EventArgs e)
        {
#if ENABLE_DIGITAL_GRAPH
            chart1.Series["Digital"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
#endif //ENABLE_DIGITAL_GRAPH
            chart1.Series["Analog"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

    }

    public struct Temperatures
    {
        public float digital;
        public float analog;
    }
}

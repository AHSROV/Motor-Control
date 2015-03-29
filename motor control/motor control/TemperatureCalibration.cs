using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace motor_control
{
    class TemperatureCalibration
    {
        CalibrationValue[] calibrationValues;

        /// <summary>
        /// Sets the calibration values
        /// </summary>
        /// <param name="cv">An array of calibration values. IN ORDER FROM LEAST TO GREATEST (readings)</param>
        public TemperatureCalibration(CalibrationValue[] cv)
        {
            calibrationValues = cv;
        }

        /// <summary>
        /// Gets a temperature from a specific reading
        /// </summary>
        /// <param name="reading">Number the Arduino spits out</param>
        /// <returns>Correct temperature</returns>
        public float getTemperatureFromReading(int reading)
        {
            /*
             * (x1,y1): Smaller reference point, (x2,y2): larger reference point
             * 
             * Y = MX + B
             * Y is the temperature
             * X is the reading from the Arduino minus the x2 because we shift the whole graph over so the relevant portion is sitting against the Y axis
             * M is the slope of the line, use (y2-y1)/(x2-x1)
             * B is Y2
             */

            int i;
            // Loop through the calibration values until we find one that is above our reading. Subtracting one will result in the value just below. 
            // Therefore, i is the value above and i - 1 is the value below. i + 1 is two values ahead.
            for (i = 0; i < calibrationValues.Length; i++)
            {
                if (reading == calibrationValues[i].analogReading)
                    return calibrationValues[i].temperature;
                if (reading < calibrationValues[i].analogReading)
                    break;
            }
            if (i == calibrationValues.Length)
                i--;
            float y1, y2, x1, x2;
            if (i == 0)
            {
                // If our reading is less than our lowest data point, then instead pretend that it is above (so we'll be using the slope from the nearest section of graph)
                y1 = calibrationValues[i + 1].temperature;
                y2 = calibrationValues[i].temperature;
                x1 = calibrationValues[i + 1].analogReading;
                x2 = calibrationValues[i].analogReading;
            }
            else
            {
                // If our reading is within the range we tested, just use the section of graph relevant. i refers to the coordinates higher than the section of 
                y1 = calibrationValues[i - 1].temperature;
                y2 = calibrationValues[i].temperature; 
                x1 = calibrationValues[i - 1].analogReading;
                x2 = calibrationValues[i].analogReading;
            }
            float b = y2; // Y intercept, and we're moving the whole graph over so the larger values are on the left
            float m = (y2 - y1) / (x2 - x1); // Slope
            float temperature = m * (reading - x2) + b; 
            return temperature;
        }

    }

    struct CalibrationValue
    {
        public int analogReading;
        public float temperature;

        /// <summary>
        /// Create the Calibration Value with specific values
        /// </summary>
        /// <param name="reading">Reading from Arduino</param>
        /// <param name="temperature">Known Temperature</param>
        public CalibrationValue(int reading, float temperature)
        {
            this.analogReading = reading;
            this.temperature = temperature;
        }
    }
}

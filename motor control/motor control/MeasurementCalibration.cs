using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace motor_control
{
    class MeasurementCalibration
    {
        CalibrationValue[] calibrationValues;
        public MeasurementCalibration(CalibrationValue[] values)
        {
            this.calibrationValues = values;
        }

        static CalibrationValue[] calibrationData = 
        {
            new CalibrationValue(9.5f, 1),
            new CalibrationValue(19, 2),
            new CalibrationValue(24.5f, 2.5f)
        };

        public static float GetCalibrationValue(float screenValue)
        {
            MeasurementCalibration calibration = new MeasurementCalibration(calibrationData);
            return calibration.GetMeasurementFromReading(screenValue)*0.8f;
        }

        public float GetMeasurementFromReading(float reading)
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
                if (reading == calibrationValues[i].ScreenValue)
                    return calibrationValues[i].RealValue;
                if (reading < calibrationValues[i].ScreenValue)
                    break;
            }
            if (i == calibrationValues.Length)
                i--;
            float y1, y2, x1, x2;
            if (i == 0)
            {
                // If our reading is less than our lowest data point, then instead pretend that it is above (so we'll be using the slope from the nearest section of graph)
                y1 = calibrationValues[i + 1].RealValue;
                y2 = calibrationValues[i].RealValue;
                x1 = calibrationValues[i + 1].ScreenValue;
                x2 = calibrationValues[i].ScreenValue;
            }
            else
            {
                // If our reading is within the range we tested, just use the section of graph relevant. i refers to the coordinates higher than the section of 
                y1 = calibrationValues[i - 1].RealValue;
                y2 = calibrationValues[i].RealValue;
                x1 = calibrationValues[i - 1].ScreenValue;
                x2 = calibrationValues[i].ScreenValue;
            }
            float b = y2; // Y intercept, and we're moving the whole graph over so the larger values are on the left
            float m = (y2 - y1) / (x2 - x1); // Slope
            float measurement = m * (reading - x2) + b;
            return measurement;
        }
    }

    struct CalibrationValue
    {
        private float screenValue;
        private float realValue;

        public CalibrationValue(float screenValue, float realValue)
        {
            this.screenValue = screenValue;
            this.realValue = realValue;
        }

        public float ScreenValue
        {
            get { return screenValue; }
        }

        public float RealValue
        {
            get { return realValue; }
        }
    }

}

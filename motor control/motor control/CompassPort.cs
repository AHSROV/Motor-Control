using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace motor_control
{
    public class CompassPort : SerialComChannel
    {
        new public void SetUp(string portName)
        {
            base.SetUp(portName);

            if (!blank)
            {
                port.DataReceived += DataReceivedHandler;
            }
        }

        private string lastCompleteString = "";
        private string newString;
        private bool compassWorking = false;

        // Allow the caller to restart compass operations if we miss an answer for some reason
        public void Reset()
        {
            // Stop waiting for the compass (maybe it won't ever answer?)
            compassWorking = false;
        }

        public void Send(byte command1, byte command2) //send comand without data to the port
        {
            //sending the the polulu id, device nuber and the actual comand to send.
            byte[] comandBytes = { 
                (byte)'$', (byte)'s', (byte)'u', (byte)'r', (byte)'e', 
                (byte)' ', command1, command2, 0x0d, 0x0a};
            SendBytes(comandBytes, 0, comandBytes.Count()); //count tells how many bytes are in the array
        }

        public string GetHeading()
        {
            if (blank)
            {
                return "no compass";
            }
            else
            {
                if (compassWorking != true)
                {
                    Send((byte)'g', (byte)'a');

                    compassWorking = true;
                }

                Match result = Regex.Match(lastCompleteString, "\\d+");
                if (result.Success)
                {
#if false // use this to get raw compass reading to build correction table
                    return result.Value;
#else
                    return CorrectedHeading(result.Value);
#endif
                }

                return lastCompleteString;
            }
        }

        struct CompassPoints
        {
            public int compassReading;
            public float trueHeading;
        }


        string CorrectedHeading(string degrees)
        {
            int input = Convert.ToInt32(degrees);

            CompassPoints[] dataTable = new CompassPoints[]{
#if false  // Old quick calibration at Aptos High Pool
                new CompassPoints() { compassReading = -128, trueHeading = 0 },
                new CompassPoints() { compassReading = 21, trueHeading = 180 },
                new CompassPoints() { compassReading = 71, trueHeading = 270 },
                new CompassPoints() { compassReading = 232, trueHeading = 360 },
                new CompassPoints() { compassReading = 332, trueHeading = 450 },
                new CompassPoints() { compassReading = 381, trueHeading = 540 },
#else   // High detail calibration at Orlando YMCA
                new CompassPoints() { compassReading = 0,   trueHeading = 135 },
                new CompassPoints() { compassReading = 13,  trueHeading = 157.5f },
                new CompassPoints() { compassReading = 27,  trueHeading = 180 },
                new CompassPoints() { compassReading = 40,  trueHeading = 202.5f },
                new CompassPoints() { compassReading = 52,  trueHeading = 225 },
                new CompassPoints() { compassReading = 67,  trueHeading = 247.5f },
                new CompassPoints() { compassReading = 82,  trueHeading = 270 },
                new CompassPoints() { compassReading = 99,  trueHeading = 292.5f },
                new CompassPoints() { compassReading = 120, trueHeading = 315 },
                new CompassPoints() { compassReading = 146, trueHeading = 337.5f },
                new CompassPoints() { compassReading = 186, trueHeading = 360 },
                new CompassPoints() { compassReading = 246, trueHeading = 382.5f },
                new CompassPoints() { compassReading = 284, trueHeading = 405 },
                new CompassPoints() { compassReading = 310, trueHeading = 427.5f },
                new CompassPoints() { compassReading = 329, trueHeading = 450 },
                new CompassPoints() { compassReading = 345, trueHeading = 472.5f },
                new CompassPoints() { compassReading = 360, trueHeading = 495 },
#endif
            };

            int i;
            for (i=0; i < dataTable.Length; i++)
            {
                if (input < dataTable[i].compassReading)
                {
                    break;
                }
            }

            float x1 = dataTable[i-1].compassReading;
            float y1 = dataTable[i-1].trueHeading;
            float x2 = dataTable[i].compassReading;
            float y2 = dataTable[i].trueHeading;

            // y = mx + b
            float trueHeading = ((y2-y1)/(x2-x1)) * (input-x1) + y1;

            trueHeading = trueHeading % 360;
       
            return String.Format("Corrected Mag Hg = {0}", trueHeading);
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        { 
            newString = ReadString();

            int last = newString[newString.Length - 1];

            if (last == 13)
            {
                lastCompleteString = newString;
                compassWorking = false;             
            }
        }
    }
}

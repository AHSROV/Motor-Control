using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//percent of power being used by motor = motor speed
namespace motor_control
{
    class Motor
    {
        public int percentSpeed;

        int deviceNumber;

        ArduinoPort port;

        //start up
        //make the object usalble by the main program
        public void SetUp(ArduinoPort thePort, int motorID)
        {
            deviceNumber = motorID;
            port = thePort;
        }

        char direction;

        public void Reset()
        {
            SetPercentSpeed(0);
        }

        //shut down
        public void ShutDown()
        {
            SetPercentSpeed(0);
        }

        //ask which device number we are
        public int GetID()
        {
            return deviceNumber;
        }

        //get speed
        public float GetPercentSpeed()
        {
            return percentSpeed;
        }

        //set speed 
        public void SetPercentSpeed(float desiredSpeed)// percent of possible speed between -1 and 1
        {
            int newPercentSpeed = (int) (desiredSpeed * 100);

            // don't send command if nothing has changed
            if (percentSpeed == newPercentSpeed)
            {
                return;
            }

            //store the information
            percentSpeed = newPercentSpeed;

            //decide if the motor is going forward or backwards
            if (percentSpeed >= 0)
            {
                direction = 'f';
            }
            else
            {
                direction = 'r';
            }

            String percentSpeedFormatted = Math.Abs(percentSpeed).ToString("000");

            String msg = String.Format("m{0}{1}{2}", deviceNumber, direction, percentSpeedFormatted);
            Console.WriteLine(msg);
            port.SendString(msg);
        }
    }
}

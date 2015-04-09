using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace motor_control
{
    public class ArduinoPort : SerialComChannel
    {
        new public void SetUp(string portName)
        {

            base.SetUp(portName);
            System.Threading.Thread.Sleep(200);
            nameOfPort = portName;

            if (!blank)
            {
                ResetArduinoCPU();

                port.DataReceived += DataReceivedHandler;
                arduinoConnected = true;
            }
            else
            {
                arduinoConnected = false;
            }
        }

        public string nameOfPort;
        private string newString;
        private string lastCompleteString;
        private bool arduinoConnected;

        public delegate void EmergecnyEventHandler(object sender, EventArgs e);
        public event EmergecnyEventHandler Emergency;

        // This resets the entire connection to the Arduino not just the processor
        public void ResetConnection()
        {
            try
            {
                ShutDown();
                arduinoConnected = false;
                Console.WriteLine("Shut down Arduino connection.");
            }
            catch
            {
            }

            // Arbitrary delay to give things a change to settle
            System.Threading.Thread.Sleep(250);

            try
            {
                SetUp(nameOfPort);
                arduinoConnected = true;
                Console.WriteLine("Established Arduino connection.");
            }
            catch
            {
                Console.WriteLine("Reset failed.  :(");
            }

            ResetArduinoCPU();
        }


        public void ResetArduinoCPU()
        {
            // pull DTR low to reset the Arduino
            port.DtrEnable = false;

            // Arbitrary delay to give things a change to settle
            System.Threading.Thread.Sleep(200);

            // Pull DTR high again to get rolling
            port.DtrEnable = true;
        }


        // Returns whether the Arduino is connected or not
        public bool Connected
        {
            get
            {
                return arduinoConnected;
            }
        }
        
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Put the read string in newString
            newString = ReadString();

            if (newString.Length == 0)
            {
                return;
            }

            // Gets the last byte of the read string.
            int last = newString[newString.Length - 1];
            if (last == 13 || last == 10) // Checks if the last character in the string is a carriage return
            {
                if (newString.Length == 1)
                {
                    // Its an empty message (extra line feed at the end?)
                    return;
                }
                lastCompleteString = newString;
            }
            else
            {
                Console.WriteLine("Deformed message received: " + newString);
                return;
            }
            Console.WriteLine("Message received: " + lastCompleteString);
        }

        public void SendString(String s)
        {
            String command = "." + s + "*";
            Console.WriteLine(command);
            base.SendString(command);
        }

    }


    public delegate void MyEventHandler(object source, MyEventArgs e);

    public class MyEventArgs : EventArgs
    {
        private string EventInfo;
        public MyEventArgs(string Text)
        {
            EventInfo = Text;
        }
        public string GetInfo()
        {
            return EventInfo;
        }
    }
}
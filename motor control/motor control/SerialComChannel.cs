using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace motor_control
{
    public class SerialComChannel : IComChannel
    {
        public SerialPort port;    // for talking to the serial port

        public bool blank;   //fake port

        public const string BlankName = "None"; //name of fake port

        public bool IsBlank { get { return blank; } }


        //get list of useable ports
        public static string[] GetPortNames()
        {
            var results = new List<string>();

            results.Add("None");

            string[] portNames = SerialPort.GetPortNames();
            foreach (string name in portNames)
            {
                results.Add(name);
            }

            return results.ToArray();
        }

        public static bool IsBlankName(string portName)
        {
            return portName == BlankName;
        }
        // set up the port for the program
        //portName is the port to use (for example, COM1)
        public void SetUp(string portName)
        {
            if (IsBlankName(portName))
            {
                //make it so that the program stops trying to use startup
                blank = true;
                return;
            }
            else
            {
                port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);

                // set a time out limit for the serial port so that the program will not hang if the motor controllers are not attached.
                port.ReadTimeout = 50;
                    port.Open(); //open the port for use
            }
        }

        public void ShutDown()
        {
            //if the port is blank then skip this step
            if (blank)
            {
                return;
            }
            try
            {
                port.Close(); //close the port
            }
            catch { }

            port = null;  //tells the computer that we no longer need the serial port object
        }

        public void SendBytes(byte[] buffer, int offset, int count)
        {
            if(blank)
            {
               return;
            }
            try
            {
                port.Write(buffer, offset, count);
            }
            catch
            {
                Console.WriteLine("SendBytes write error on "+port.PortName);
            }
        }

        public void SendString(string message)
        {
            if (blank) return;
            try
            {
                port.Write(message);
            }
            catch
            {
                Console.WriteLine("SendString write error on " + port.PortName);
            }
        }

        public void SendStringln(string message)
        {
            if (blank) return;
            try
            {
                port.WriteLine(message);
            }
            catch
            {
                Console.WriteLine("SendStringln write error on " + port.PortName);
            }
        }


        public string ReadString()
        {
            if (blank)
            {
                return "not connected";
            }
            try
            {
                return port.ReadLine();
            }
            catch
            {
                return "exception";
            }
        }

        public bool ReceiveByte(ref byte response)
        {
            if (blank)
            {
                return false;
            }

            response = 0;
            try
            {
                int result = port.ReadByte();

                if (result < 0)
                {
                    return false;
                }
                response = (byte)result;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ReceiveTwoBytes(ref int response)
        {
            if (blank)
            {
                return false;
            }
            byte b = 0;

            // Get the first byte of our response, or return false is something goes wrong
            if (ReceiveByte(ref b) != true)
            {
                return false;
            }
            response = b;

            // Get the second byte of our response, or return false is something goes wrong
            if (ReceiveByte(ref b) != true)
            {
                return false;
            }
            response += 256 * b;

            return true;
        }
    }
}

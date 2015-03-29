using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace motor_control
{
    interface IComChannel
    {
        void SetUp(string PortName);
        void ShutDown();
        void SendBytes(byte[] buffer, int offset, int count);
        void SendString(string message);
        void SendStringln(string message);
        string ReadString();
        bool ReceiveByte(ref byte response);
        bool ReceiveTwoBytes(ref int response);
    }
}

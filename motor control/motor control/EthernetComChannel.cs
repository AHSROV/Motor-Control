using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace motor_control
{
    class EthernetComChannel : IComChannel
    {
        private Stream stream;
        private TcpClient tcpclient;

        public void SetUp(string inputName)
        {
            tcpclient = new TcpClient();
            String[] IPPort = inputName.Split(':');
            tcpclient.Connect(IPPort[0], Int16.Parse(IPPort[1]));
            stream = tcpclient.GetStream();
        }

        public void ShutDown()
        {
            tcpclient.Close();
        }

        public void SendBytes(byte[] buffer, int offset, int count)
        {
            stream.Write(buffer, offset, count);
        }

        public void SendString(string message)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(message);
            stream.Write(ba, 0, ba.Length);
        }

        public void SendStringln(string message)
        {
            SendString(message + "/r/n");
        }

        public string ReadString()
        {
            throw new NotImplementedException();
        }

        public bool ReceiveByte(ref byte response)
        {
            throw new NotImplementedException();
        }

        public bool ReceiveTwoBytes(ref int response)
        {
            throw new NotImplementedException();
        }
    }
}

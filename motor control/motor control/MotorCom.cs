using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
namespace motor_control

{
    class MotorCom:SerialComChannel
    {
        public void Send(byte deviceNumber, byte command) //send command without data to the port
        {
            //sending the polulu id, device number and the actual command to send.
            byte[] comandBytes = { 0xAA, deviceNumber, command };
            SendBytes(comandBytes, 0, comandBytes.Count()); //count tells how many bytes are in the array
        }

        public void Send(byte deviceNumber, byte command, byte[] data) //send command with data to the port
        {
            Send(deviceNumber, command);
            SendBytes(data, 0, data.Count());
        }
    }
}

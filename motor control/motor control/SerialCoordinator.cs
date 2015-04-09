using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;

namespace motor_control
{
    public delegate void SendCommand();

    class SerialCoordinator
    {
        int interval;
        int total;
        Dictionary<int, SendCommand> map;

        Timer timer;

        public SerialCoordinator(int intervalMillis, int totalMotors)
        {
            timer = new Timer();

            interval = intervalMillis;
            total = totalMotors;

            timer.Interval = intervalMillis;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Run);

            map = new Dictionary<int,SendCommand>();
        }

        public void SetUpMotor(int motorNumber, SendCommand commandDelegate)
        {
            int motorOffset = (interval / total) * motorNumber;
            map.Add(motorOffset, commandDelegate);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Run(Object source, ElapsedEventArgs e)
        {
            foreach (KeyValuePair<int, SendCommand> entry in map)
            {
                SendCommand command = (SendCommand)entry.Value;
                command();
            }
        }
    }
}

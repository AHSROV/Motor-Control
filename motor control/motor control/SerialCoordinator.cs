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
        Dictionary<int, int> offsetMap;
        Dictionary<int, SendCommand> delegateMap;

        Timer timer;

        public SerialCoordinator(int intervalMillis, int totalMotors)
        {
            timer = new Timer();

            interval = intervalMillis;
            total = totalMotors;

            timer.Interval = intervalMillis;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Run);

            offsetMap = new Dictionary<int, int>();
            delegateMap = new Dictionary<int, SendCommand>();
        }

        public void SetUpMotor(int motorNumber, SendCommand commandDelegate)
        {
            int motorOffset = (interval / total) * (motorNumber+1);
            offsetMap.Add(motorNumber, motorOffset);
            delegateMap.Add(motorNumber, commandDelegate);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Run(Object source, ElapsedEventArgs e)
        {
            foreach (KeyValuePair<int, int> entry in offsetMap)
            {
                SendCommand command;
                delegateMap.TryGetValue(entry.Key, out command);

                Timer individualTimer = new Timer(entry.Value);
                individualTimer.AutoReset = false;
                individualTimer.Elapsed += new ElapsedEventHandler((sender, d) => SendIndividualCommand(sender, d, command));
                individualTimer.Start();
            }
        }

        private void SendIndividualCommand(object sender, ElapsedEventArgs e, SendCommand sendCommand)
        {
            sendCommand();
        }
    }
}

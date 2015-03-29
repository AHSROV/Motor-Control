using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace motor_control
{
    class ConductivitySensor
    {
        private ArduinoPort port;
        private bool state;
        private int reading;

        public ConductivitySensor(ArduinoPort arduino)
        {
            this.port = arduino;
            SetState(false);
        }

        public void SetState(bool newState)
        {
            if (this.state != newState)
            {
                this.state = newState;
                if (state)
                {
                    port.SendStringln("c1");
                }
                else
                {
                    port.SendStringln("c0");
                }
            }
        }

        public bool GetState()
        {
            return state;
        }

        public int getReading()
        {
            return reading;
        }
    }
}

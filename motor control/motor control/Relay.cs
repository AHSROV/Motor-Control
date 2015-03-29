using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace motor_control
{
    class Relay
    {
        private ArduinoPort port;
        private int relay;

        private bool state;

        /// <summary>
        /// Set the state of the relay
        /// </summary>
        /// <param name="state">State of relay</param>
        public void SetState(bool newState)
        {
            if (this.state != newState)
            {
                this.state = newState;
                port.SendStringln("r" + relay + (newState ? "1" : "0"));
            }
        }

        /// <summary>
        /// allows other parts of the program to ask what the state of the relay is.
        /// note: we are not checking the hardware state, but only reporting what we most recently asked it to do.
        /// </summary>
        /// <returns></returns>
        public bool GetState()
        {
            return state;
        }

        /// <summary>
        /// Turn off the relay
        /// </summary>
   
        public void ShutDown()
        {
            SetState(false);
        }

        internal void SetUp(ArduinoPort arduino, int p)
        {
            this.relay = p;
            this.port = arduino;
        }
    }
}

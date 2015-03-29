using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace motor_control
{
    class Valve : Motor
    {
        public void SetState(bool state)
        {
            if (state)
            {
                SetPercentSpeed(-1);
            }
            else
            {
                SetPercentSpeed(0);
            }
        }

        public bool GetState()
        {
            return (Math.Abs(percentSpeed) > .5);
        }
    
    }
}

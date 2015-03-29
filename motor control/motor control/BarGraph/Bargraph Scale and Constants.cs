using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace motor_control
{
    public class BarGraph_Scale_and_Constants
        
    {

        public float ScaleValue = 1.0f;
        public float HeightScale = .5f;
        public float WidthScale = .5f;

        public float FLeftUDMotorBarGraph_X = 0;
        public float FLeftUDMotorBarGraph_Y = 0;

        public float FRightUDMotorBarGraph_X = 60;
        public float FRightUDMotorBarGraph_Y = 0;

        public float BLeftUDMotorBarGraph_X = 120;
        public float BLeftUDMotorBarGraph_Y = 0;

        public float BRightUDMotorBarGraph_X = 180;
        public float BRightUDMotorBarGraph_Y = 0;

        public float FwdLeftMotorBarGraph_X = 240;
        public float FwdLeftMotorBarGraph_Y = 0;

        public float FwdRightMotorBarGraph_X = 300;
        public float FwdRightMotorBarGraph_Y = 0;

        public float StrafeBackBarGraph_X = 300 + 60;
        public float StrafeBackBarGraph_Y = 0;

        public float StrafeFrontBarGraph_X = 420;
        public float StrafeFrontBarGraph_Y = 0;
     
    }
}


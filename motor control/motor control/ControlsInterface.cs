using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace motor_control
{
    struct MotorSpeeds//make a new type that is a struct 
    {
        //list of the motors that we will be useing
        public float upFront;
        public float upBack;
        public float frontLeft;
        public float frontRight;
        public float backRight;
        public float backLeft;

        public bool claw;
        public bool rack;
        public bool stomper;
        public bool leech;
    }

    interface IControls
    {
        string GetName();
        MotorSpeeds Update(GamePadState padState, GamePadState lastPadState, KeyboardState keyState, KeyboardState lastKeyState, GameTime gameTime, MotorSpeeds motorSpeed);
    }
}

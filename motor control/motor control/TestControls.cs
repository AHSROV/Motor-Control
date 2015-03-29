using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace motor_control
{
    class TestControls : IControls
    {
        KeyboardState oldState;

        public string GetName()
        {
            return "Test Controls";
        }

        public MotorSpeeds Update(GamePadState padState, GamePadState lastPadState, KeyboardState keyState, KeyboardState lastKeyState, GameTime gameTime, MotorSpeeds motorSpeed)
        {
            //give the motors their respective controlers
            motorSpeed.upFront      = padState.Triggers.Left;
            motorSpeed.upBack       = padState.Triggers.Right;
            motorSpeed.frontLeft      = padState.ThumbSticks.Left.Y;
            motorSpeed.frontRight     = padState.ThumbSticks.Right.Y;
            motorSpeed.backRight  = padState.ThumbSticks.Left.X;
            motorSpeed.backLeft   = padState.ThumbSticks.Right.X;

            //decide speed that the motors should run at
            float testSpeed = 0.5f;

            //find what buttons are pressed and set motor speeds
            if (keyState.IsKeyDown(Keys.A))
            {
                motorSpeed.upFront = testSpeed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                motorSpeed.upBack = testSpeed;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                motorSpeed.frontLeft = testSpeed;
            }
            if (keyState.IsKeyDown(Keys.F))
            {
                motorSpeed.frontRight = testSpeed;
            }
            if (keyState.IsKeyDown(Keys.G))
            {
                motorSpeed.backRight = testSpeed;
            }
            if (keyState.IsKeyDown(Keys.H))
            {
                motorSpeed.backLeft = testSpeed;
            }
            if (keyState.IsKeyDown(Keys.J) && !oldState.IsKeyDown(Keys.J))
            {
                motorSpeed.claw = !motorSpeed.claw;
            }
            if (keyState.IsKeyDown(Keys.K) && !oldState.IsKeyDown(Keys.K))
            {
                motorSpeed.leech = !motorSpeed.leech;
            }
            if (keyState.IsKeyDown(Keys.L) && !oldState.IsKeyDown(Keys.L))
            {
                motorSpeed.rack = !motorSpeed.rack;
            }
            if (keyState.IsKeyDown(Keys.OemSemicolon) && !oldState.IsKeyDown(Keys.OemSemicolon))
            {
                motorSpeed.stomper = !motorSpeed.stomper;
            }

            oldState = keyState;

            return motorSpeed;
        }
    }
}

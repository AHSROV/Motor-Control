using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace motor_control
{
    class BackXControls : IControls
    {
        public string GetName()
        {
            return "Back X-Controls";
        }

        public MotorSpeeds Update(GamePadState padState, GamePadState lastPadState, KeyboardState keyState, KeyboardState lastKeyState, GameTime gameTime, MotorSpeeds motorSpeed)
        {
            //Up down is controlled by the triggers
            motorSpeed.upBack  = padState.Triggers.Left - padState.Triggers.Right;
            motorSpeed.upFront = padState.Triggers.Left - padState.Triggers.Right;

            //Straffing is controlled by the left x-axis
            motorSpeed.frontLeft  = padState.ThumbSticks.Left.X;
            motorSpeed.frontRight = -padState.ThumbSticks.Left.X;
            motorSpeed.backLeft   = padState.ThumbSticks.Left.X;
            motorSpeed.backRight  = -padState.ThumbSticks.Left.X;

            //forward and backward movement it controlled by the left y-axis
            motorSpeed.frontRight += padState.ThumbSticks.Left.Y;
            motorSpeed.frontLeft  += padState.ThumbSticks.Left.Y;
            motorSpeed.backLeft   += -padState.ThumbSticks.Left.Y;
            motorSpeed.backRight  += -padState.ThumbSticks.Left.Y;

            //rotation is controlled by the right thumbstick x-axis
            motorSpeed.frontLeft  += -padState.ThumbSticks.Right.X;
            motorSpeed.frontRight += padState.ThumbSticks.Right.X;
            motorSpeed.backLeft   += padState.ThumbSticks.Right.X;
            motorSpeed.backRight  += -padState.ThumbSticks.Right.X;

            //Pitch is controlled by the right thumbstick y-axis
            motorSpeed.upBack  += -padState.ThumbSticks.Right.Y;
            motorSpeed.upFront += padState.ThumbSticks.Right.Y;

            //find the largest value for the lateral motors
            float maxLateral = Math.Max(1, Math.Abs(motorSpeed.frontLeft));
            maxLateral = Math.Max(maxLateral, Math.Abs(motorSpeed.frontRight));
            maxLateral = Math.Max(maxLateral, Math.Abs(motorSpeed.backRight));
            maxLateral = Math.Max(maxLateral, Math.Abs(motorSpeed.backLeft));

            //divide by the largest number
            motorSpeed.backLeft   /= maxLateral;
            motorSpeed.backRight  /= maxLateral;
            motorSpeed.frontLeft  /= maxLateral;
            motorSpeed.frontRight /= maxLateral;

            //Find the largest value for the up/downs
            float maxUp = Math.Max(1, Math.Abs(motorSpeed.upBack));
            maxUp = Math.Max(maxUp, Math.Abs(motorSpeed.upFront));

            //Divide by the largest number
            motorSpeed.upBack /= maxUp;
            motorSpeed.upFront /= maxUp;

            if (lastPadState.Buttons.X != padState.Buttons.X)
            {
                if (padState.Buttons.X == ButtonState.Pressed)
                {
                    motorSpeed.claw = !motorSpeed.claw;
                }
            }
            if (lastPadState.Buttons.A != padState.Buttons.A)
            {
                if (padState.Buttons.A == ButtonState.Pressed)
                {
                    //if the rack is up then do not cycle the leech
                    if (motorSpeed.rack == false)
                    {
                    }
                    else
                    {
                        motorSpeed.leech = !motorSpeed.leech;
                    }
                }
            }
            if (lastPadState.Buttons.Y != padState.Buttons.Y)
            {
                if (padState.Buttons.Y == ButtonState.Pressed)
                {
                    motorSpeed.stomper = !motorSpeed.stomper;
                }
            }
            if (lastPadState.DPad.Up != padState.DPad.Up)
            {
                if (padState.DPad.Up == ButtonState.Pressed)
                {
                    motorSpeed.rack = true;
                }
            }
            if (lastPadState.DPad.Down != padState.DPad.Down)
            {
                if (padState.DPad.Down == ButtonState.Pressed)
                {
                    motorSpeed.rack = false;
                }
            }

            //make lastPadState equal to the current padState for the next loop
            lastPadState = padState;

            //make motorSpeed so that it can be used else where
            return motorSpeed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace motor_control
{
    class ChrisControls : IControls
    {
        public string GetName()
        {
            return "Mixing controls";
        }

        public MotorSpeeds Update(GamePadState padState, GamePadState lastPadState, KeyboardState keyState, KeyboardState lastKeyState, GameTime gameTime, MotorSpeeds motorSpeed)
        {
            //up down is controled by the triggers
            motorSpeed.upFront = padState.Triggers.Left - padState.Triggers.Right;
            motorSpeed.upBack  = padState.Triggers.Left - padState.Triggers.Right;

            //forward and backward are controled by the left y axis
            motorSpeed.frontLeft  = -padState.ThumbSticks.Left.Y;
            motorSpeed.frontRight = -padState.ThumbSticks.Left.Y;

            //side to side is controled by the left x axis
            motorSpeed.backRight  = -(padState.ThumbSticks.Left.X);
            motorSpeed.backLeft   = -(padState.ThumbSticks.Left.X);

            //yaw is controled by the right x axis
            motorSpeed.backRight  -= padState.ThumbSticks.Right.X;
            motorSpeed.backLeft   += (padState.ThumbSticks.Right.X);
            motorSpeed.frontLeft   -= padState.ThumbSticks.Right.X;
            motorSpeed.frontRight  += (padState.ThumbSticks.Right.X);

            //pitch is controled by the right y axis
            motorSpeed.upFront  += (padState.ThumbSticks.Right.Y);
            motorSpeed.upBack   += -padState.ThumbSticks.Right.Y;

            //if the up front is less than upback then make upback the max.
            float max;
            max = Math.Abs(motorSpeed.upFront);
            if (max < Math.Abs(motorSpeed.upBack))
            {
                max = Math.Abs(motorSpeed.upBack);
            }

            //divide by max so that imput is not greater than one
            if (max > 1)
            {
                motorSpeed.upFront = motorSpeed.upFront / max;
                motorSpeed.upBack = motorSpeed.upBack / max;
            }
            
            max = Math.Abs(motorSpeed.frontLeft);
            if (max < Math.Abs(motorSpeed.frontRight))
            {
                max = Math.Abs(motorSpeed.frontRight);
            }
            if (max < Math.Abs(motorSpeed.backRight))
            {
                max = Math.Abs(motorSpeed.backRight);
            }
            if (max < Math.Abs(motorSpeed.backLeft))
            {
                max = Math.Abs(motorSpeed.backLeft);
            }
            if (max > 1)
            {
                motorSpeed.frontLeft  /= max;
                motorSpeed.frontRight /= max;
                motorSpeed.backRight  /= max;
                motorSpeed.backLeft   /= max;
            }

            /*
            //A / B buttons turn electron magnet on/off
            if (padState.Buttons.A == ButtonState.Pressed)
            {
                motorSpeed.eMagnet = true;
            }
            if (padState.Buttons.B == ButtonState.Pressed)
            {
                motorSpeed.eMagnet = false;
            }

            //left D-pad is the toggle for the left piston
            if (lastPadState.DPad.Left != padState.DPad.Left)
            {
                if (padState.DPad.Left == ButtonState.Pressed)
                {
                    motorSpeed.pistonLeft = !motorSpeed.pistonLeft;
                }
            }

            //right D-pad is the toggle for the right piston
            if (lastPadState.DPad.Right != padState.DPad.Right)
            {
                if (padState.DPad.Right == ButtonState.Pressed)
                {
                    motorSpeed.pistonRight = !motorSpeed.pistonRight;
                }
            }*/

            //x is the claw gripping control
            if (lastPadState.Buttons.X != padState.Buttons.X)
            {
                if (padState.Buttons.X == ButtonState.Pressed)
                {
                    motorSpeed.claw = !motorSpeed.claw;
                }
            }

            // B is ?

            //save the pad state for use next time through the program
            lastPadState = padState;

            return motorSpeed;
        }
    }
}

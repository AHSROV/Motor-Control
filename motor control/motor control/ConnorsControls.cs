using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace motor_control
{
    class ConnorsControls : IControls
    {
        public string GetName()
        {
            return "Connors Controls";
        }

        public MotorSpeeds Update(GamePadState padState, GamePadState lastPadState, KeyboardState keyState, KeyboardState lastKeyState, GameTime gameTime, MotorSpeeds motorSpeed)
        {
            //up down is controled by the triggers
            motorSpeed.upFront = padState.Triggers.Right - padState.Triggers.Left;
            motorSpeed.upBack = padState.Triggers.Right - padState.Triggers.Left;

            //forward and backward are controled by the left y axis
            motorSpeed.left = -(padState.ThumbSticks.Right.Y);
            motorSpeed.right = padState.ThumbSticks.Right.Y;

            //side to side is controled by the left x axis
            motorSpeed.front = padState.ThumbSticks.Right.X;
            motorSpeed.back = padState.ThumbSticks.Left.X;

            //yaw is controled by the right x axis
            motorSpeed.front += padState.ThumbSticks.Right.X;
            motorSpeed.back += -(padState.ThumbSticks.Right.X);
            motorSpeed.right += padState.ThumbSticks.Right.X;
            motorSpeed.left += -(padState.ThumbSticks.Right.X);

            //pitch is controled by the right y axis
            motorSpeed.upFront += padState.Triggers.Right;
            motorSpeed.upBack += -(padState.Triggers.Left);

            return motorSpeed;
        }
    }
}
 
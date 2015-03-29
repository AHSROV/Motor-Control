using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//These references are so we don't have to type as much..
//Example, Draw, instead of System.Console

namespace motor_control


{
    class BarGraph_RightLeft: Sprite
    {
        enum State
        {
            Moving1
        }
        State StateNow1 = State.Moving1;

        Vector2 DirectionMoving1 = Vector2.Zero;
        Vector2 DirectionalSpeed1 = Vector2.Zero;

        KeyboardState KeyboardStateBefore1;
        
        /*Currently, our Yellow Line only has one state, "Moving". 
       StateNow is used to track the current state of the sprite and it's set to 
       "Walking" when it's created.
       DirectionMoving is used to store the Direction the Line is moving in and Directional Speed 
       is used to store the Speed the sprite is moving at in those directions.
         
       KeyboardStateBefore is used to store the previous state the Keyboard was in last time 
       we checked. 
         * This is useful for times when we don't want to repeat an action unless a 
       player has pressed a key on the keyboard again instead of just holding it down. 
         * With knowing the previous state of the keyboard we can verify the key was released before, 
           but is now pressed and then do whatever action we need to do for that key.

        */
        const string YellowLinesName1 = "Yellow Lines";
        const int X_startPosition1 = 125;
        const int Y_startPosition1 = 245;
        const int SpeedofMovement1 = 160;
        const int MovingUp1 = -1;
        const int MovingDown1 = 1;
        const int MovingLeft1 = -1;
        const int MovingRight1 = 1;

        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(X_startPosition1, Y_startPosition1);
            base.LoadContent(theContentManager, YellowLinesName1);
        }



        public void Update(GameTime gameTime, float value)

        {
            KeyboardState aCurrentKeyboardState1 = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState1);

            KeyboardStateBefore1 = aCurrentKeyboardState1;

            base.Update(gameTime, DirectionalSpeed1, DirectionMoving1);
        }



        private void UpdateMovement(KeyboardState aCurrentKeyboardState1)
        {
            if (StateNow1 == State.Moving1)
            {
                DirectionalSpeed1 = Vector2.Zero;
                DirectionMoving1 = Vector2.Zero;

                if (aCurrentKeyboardState1.IsKeyDown(Keys.Left) == true)
                {
                    DirectionalSpeed1.Y = SpeedofMovement1;
                    DirectionMoving1.Y = MovingLeft1;
                }
                else if (aCurrentKeyboardState1.IsKeyDown(Keys.Right) == true)
                {
                    DirectionalSpeed1.Y = SpeedofMovement1;
                    DirectionMoving1.Y = MovingRight1;
                }

            }
        }
    }
}


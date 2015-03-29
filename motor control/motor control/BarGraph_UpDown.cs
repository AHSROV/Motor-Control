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
//Example, Draw, instead of System.Console.Draw

namespace motor_control
{
    class BarGraph_UpDown : Sprite
    {
        enum State
        {
            Moving
        }
        State StateNow = State.Moving;

        Vector2 DirectionMoving = Vector2.Zero;
        Vector2 DirectionalSpeed = Vector2.Zero;
        Vector2 Constant = new Vector2(100);
        KeyboardState KeyboardStateBefore;

        /*Currently, our Yellow Line only has one state, "Moving". 
       StateNow is used to track the current state of the sprite and it's set to 
       "Moving" when it's created.
       DirectionMoving is used to store the Direction the Line is moving in and Directional Speed 
       is used to store the Speed the sprite is moving at in those directions.
         
       KeyboardStateBefore is used to store the previous state the Keyboard was in last time 
       we checked. 
         * This is useful for times when we don't want to repeat an action unless a 
       player has pressed a key on the keyboard again instead of just holding it down. 
         * With knowing the previous state of the keyboard we can verify the key was released before, 
           but is now pressed and then do whatever action we need to do for that key.

        */
        const string YellowLinesName = "Yellow Lines";
        const int X_startPosition = 125;
        const int Y_startPosition = 245;
        const int SpeedofMovement = 160;
        const int MovingUp = -1;
        const int MovingDown = 1;
        const int MovingLeft = -1;
        const int MovingRight = 1;




        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(X_startPosition, Y_startPosition);
            base.LoadContent(theContentManager, YellowLinesName);
        }


        public void Update(GameTime gameTime, float value)
        {
           KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);

            KeyboardStateBefore = aCurrentKeyboardState;

            base.Update(gameTime, DirectionalSpeed, DirectionMoving);
        }


        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {



            if (StateNow == State.Moving)
            {
                DirectionalSpeed = Vector2.Zero;
                DirectionMoving = Vector2.Zero;

               


                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                {
                    DirectionalSpeed.Y = SpeedofMovement;
                    DirectionMoving.Y = MovingUp;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                {
                    DirectionalSpeed.Y = SpeedofMovement;
                    DirectionMoving.Y = MovingDown;
                }
            }
        }
    }
}
    



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

//These references are so we don't have to type as much..
//Example, Draw, instead of System.Console.Draw

namespace motor_control
{
    class BarGraph_Load : Sprite
    {
        BarGraph_Scale_and_Constants Values = new BarGraph_Scale_and_Constants();
        
        const string YellowLinesName = "Yellow Lines";
        const int X_startPosition = 0;
        const int Y_startPosition = 0;


        public void LoadContent(ContentManager theContentManager)
        {

            Position = new Vector2(X_startPosition, Y_startPosition);
            base.LoadContent(theContentManager, YellowLinesName);
        }

    }
}
   

    



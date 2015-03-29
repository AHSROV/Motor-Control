using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace motor_control
{
    class BarGraph
    {
        
        
        //The current position of the Sprite
        public Vector2 BarGraphPosition = new Vector2(0, 0);
              

        //The texture object used when drawing the sprite
        public Texture2D Texture2DGraph;
        public Texture2D Texture2DYellowLine;
               

        #region rectangle
        //The asset name for the Sprite's Texture
        public string GraphFileAssetName;
        public string YellowLineFileAssetName;

        //The Size of the Sprite with scale applied
        public Rectangle SizeBarGraph;
        public Rectangle SizeYellowLine;


        //Position of the Rectangle BarGraph
        public int RectangleX = 0;
        public int RectangleY = 0;

 
       //Declare        
       public BarGraph_Load YellowLines = new BarGraph_Load();
       public BarGraph_Scale_and_Constants Values = new BarGraph_Scale_and_Constants();

        //-------------------------------------------------------------
        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return Values.ScaleValue; }
            set
            {
                Values.ScaleValue = Values.HeightScale + Values.HeightScale;
                //Recalculate the Size of the Sprite with the new scale
                SizeBarGraph = new Rectangle(0, 0, (int)(Texture2DGraph.Width * Values.ScaleValue * Values.HeightScale), (int)(Texture2DGraph.Height * Values.ScaleValue * Values.WidthScale));
            }
        }


        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string BarGraph, string YellowLine)
        {

            Texture2DGraph = theContentManager.Load<Texture2D>(BarGraph);
            GraphFileAssetName = BarGraph;
            SizeBarGraph = new Rectangle(0, 0, (int)(0), (int)(0));

            Texture2DYellowLine = theContentManager.Load<Texture2D>(YellowLine);
            YellowLineFileAssetName = YellowLine;
            SizeYellowLine = new Rectangle(0, 0, (int)(0), (int)(0));
            
        }

        //Draw the sprite to the screen


        public void Draw(SpriteBatch ActualGraph, SpriteBatch YellowLineinGraph)
        {

            //Draw BarGraph when arguments are passed in
            ActualGraph.Draw(Texture2DGraph, BarGraphPosition,
                  new Rectangle(0,0,Texture2DGraph.Width,Texture2DGraph.Height),
                  Color.White, 0.0f, Vector2.Zero, new Vector2(Values.WidthScale, Values.HeightScale), SpriteEffects.None, 0);

            
            //Draw YellowLine when arguments are passed in
            YellowLineinGraph.Draw(Texture2DYellowLine, YellowLines.Position,
                  new Rectangle(0, 0, Texture2DYellowLine.Width,Texture2DYellowLine.Height),
                  Color.White, 0.0f, Vector2.Zero, new Vector2(Values.WidthScale, Values.HeightScale), SpriteEffects.None, 0);
            
            
        }

    }
}
        #endregion

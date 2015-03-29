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
    {   //The current position of the Sprite
        public Vector2 BarGraphPosition = new Vector2(0,0);

        //The texture object used when drawing the sprite
        public Texture2D Graph1;

        

        #region rectangle
        //The asset name for the Sprite's Texture
        public string AssetName;

        //The Size of the Sprite with scale applied
        public Rectangle Size;


       //Position of the Rectangle BarGraph
       public int RectangleX = 0; 
       public int RectangleY = 0;
        
        //The amount to increase/decrease the size of the original sprite. 
        public float mHeightScale = .5f;
        public float mWidthScale = .5f;
        private float mScale = .5f;
        
        
                //-------------------------------------------------------------
        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = mHeightScale+mWidthScale;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(Graph1.Width*mScale*mHeightScale), (int)(Graph1.Height* mScale*mWidthScale));
            }
        }

                       
        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            
            Graph1 = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(0), (int)(0));
           
        }

        //Draw the sprite to the screen

                
        public void Draw(SpriteBatch BarGraphSB)
        { BarGraphSB.Draw(Graph1, BarGraphPosition,
                new Rectangle(RectangleX, RectangleY, Graph1.Width, Graph1.Height),
                Color.White, 0.0f, Vector2.Zero,new Vector2(mWidthScale, mHeightScale), SpriteEffects.None, 0);
        
           }
       
    }
}
        #endregion

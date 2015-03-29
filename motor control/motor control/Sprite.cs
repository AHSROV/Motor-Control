using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace motor_control
{
    class Sprite

    #region class
    {    //The asset name for the Sprite's Texture
        public string AssetName;

        BarGraph Graph = new BarGraph();

        //The Size of the Sprite with scale applied
        public Rectangle Size;

        //The amount to increase/decrease the size of the original sprite. 
        private float mScale = 1.0f;


        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;

        //-------------------------------------------------------------
        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            }
        }

    #endregion

        #region LoadContent()
        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }

        #endregion

        #region Update(....)

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }

        /*
          Position = Direction(Vector2 object Direction. left= -1 or right = 1)
          times Speed(1 times a magnitude) * Gametime, to keep it consistent with
          many computers 
        */



        #endregion

        #region Draw(...)


        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position,
                new Rectangle(Graph.RectangleX, Graph.RectangleY, mSpriteTexture.Width, mSpriteTexture.Height),
                Color.White, 0.0f, Vector2.Zero, new Vector2(Graph.mWidthScale, Graph.mHeightScale)
                , SpriteEffects.None, 0);
        }
    }
}
        #endregion

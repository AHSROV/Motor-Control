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
    {  //Declare Scale
        BarGraph_Scale_and_Constants SpriteScale = new BarGraph_Scale_and_Constants();
     
        
        //The asset name for the Sprite's Texture
        public string AssetName;

        
        //The Size of the Sprite with scale applied
        public Rectangle Size;

        //The amount to increase/decrease the size of the original sprite. 
        private float ScaleValue = 1.0f;


        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D SpriteTexture;

        //-------------------------------------------------------------
        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return ScaleValue; }
            set
            {
                ScaleValue = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(SpriteTexture.Width * Scale), (int)(SpriteTexture.Height * Scale));
            }
        }

    #endregion

    #region LoadContent()

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            SpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(SpriteTexture.Width * Scale), (int)(SpriteTexture.Height * Scale));
        }

    #endregion

    #region Update(....)

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime GameTime, float ThumbstickFloatValue, float X)
        {
            float YPosition = (-218 / 2) * ThumbstickFloatValue + 218 / 2;
            Position = new Vector2(X, (float)YPosition);                      
        }
        
    #endregion

    #region Draw(...)

        //Draw the sprite to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(SpriteTexture, Position,
                new Rectangle(0, 0, SpriteTexture.Width, SpriteTexture.Height),
                Color.White, 0.0f, Vector2.Zero, new Vector2(SpriteScale.WidthScale, SpriteScale.HeightScale)
                , SpriteEffects.None, 0);

    #endregion

        }
    }
}
        

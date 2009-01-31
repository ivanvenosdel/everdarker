using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EverDarker
{
    class Sprite
    {
        #region Members
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);
        //The texture object used when drawing the sprite
        public Texture2D spriteTexture;
        //The size of the Sprite
        public Rectangle Size;
        //Used to size the Sprite up or down from the original image
        public float Scale = 1.0f;
        //For the center of the sprite
        public Vector2 origin;
        
        #endregion Members

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            //Load and Calc sprite size
            spriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            origin.X = spriteTexture.Width / 2;
            origin.Y = spriteTexture.Height / 2;
            Size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
        }
        
        //Draw the sprite to the screen 
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(spriteTexture, Position, 
                new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height), Color.White, 
                0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}

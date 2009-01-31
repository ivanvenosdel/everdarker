using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EverDarker
{
    class WallSprite
    {
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D spriteTexture;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            spriteTexture = theContentManager.Load<Texture2D>(theAssetName);
        }
        
        //Draw the sprite to the screen 
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(spriteTexture, Position, Color.White);
        }
    }
}

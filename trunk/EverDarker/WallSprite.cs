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
        //The current position of the WallSprite
        public Vector2 Position = new Vector2(0,0);

        //The Bounding Box of the WallSprite
        public Rectangle boundingBox;

        //The texture object used when drawing the sprite
        public Texture2D spriteTexture;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager)
        {
            spriteTexture = theContentManager.Load<Texture2D>("Cubicle-TwoSided");
            boundingBox = new Rectangle((int)Position.X, (int)Position.Y, spriteTexture.Width, spriteTexture.Height);
        }
        
        //Draw the sprite to the screen 
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(spriteTexture, Position, Color.White);
            //theSpriteBatch.Draw(spriteTexture, Position, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

        public void Move(int horz, int vert)
        {
            Position.X += horz;
            Position.Y += vert;
        }
    }
}

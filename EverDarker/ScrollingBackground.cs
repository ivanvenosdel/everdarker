using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EverDarker
{
    public class ScrollingBackground : Sprite
    {
        // class ScrollingBackground
        private Vector2 screenpos, origin, texturesize;
        private int screenheight, screenwidth;
        public float RotationAngle = 0f;

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            this.spriteTexture = backgroundTexture;
            screenheight = device.Viewport.Height;
            screenwidth = device.Viewport.Width;
            // Set the origin so that we're drawing from the 
            // center of the top edge.
            origin = new Vector2(this.spriteTexture.Width / 2, 0);
            // Set the screen position to the center of the screen.
            screenpos = new Vector2(screenwidth / 2, screenheight / 2);
            // Offset to draw the second texture, when necessary.
            texturesize = new Vector2(0, this.spriteTexture.Height);
        }
        
        // ScrollingBackground.Draw
        public void Draw(SpriteBatch batch)
        {
            // Draw the texture, if it is still onscreen.
            if (screenpos.Y < screenheight  && screenpos.X < screenwidth)
            {
                batch.Draw(this.spriteTexture, screenpos, null,
                     Color.White, RotationAngle, origin, 1, SpriteEffects.None, 0f);
            }
            // Draw the texture a second time, behind the first,
            // to create the scrolling illusion.
            batch.Draw(this.spriteTexture, screenpos - texturesize, null,
                 Color.White, RotationAngle, origin, 1, SpriteEffects.None, 0f);
        }
    }
}

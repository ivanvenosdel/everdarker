using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EverDarker
{
    class Character : Sprite
    {
        public float RotationAngle = 0f;
        public Rectangle boundingBox;

        public void LoadContent(ContentManager theContentManager, string theAssetName, Viewport viewPort)
        {
            base.LoadContent(theContentManager, theAssetName);
            this.Position = new Vector2(viewPort.Width / 2, viewPort.Height / 2);
            this.boundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.spriteTexture.Width, this.spriteTexture.Height);
        }

        //Draw the sprite to the screen 
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(spriteTexture, Position,
                null, Color.White, RotationAngle, origin, Scale, SpriteEffects.None, 0);
        }
    }
}

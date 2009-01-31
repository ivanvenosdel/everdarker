using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Timers;

namespace EverDarker
{
    class Character : Sprite
    {
        public float RotationAngle = 0f;
        public Rectangle boundingBox;
        public List<Texture2D> textures;
        int texturesIndex = 1;
        public bool walking = false;
        public DateTime lastWalk;

        public void LoadContent(ContentManager theContentManager, string theAssetName, Viewport viewPort)
        {
            textures = new List<Texture2D>();
            base.LoadContent(theContentManager, theAssetName);
            textures.Add(this.spriteTexture);
            textures.Add(theContentManager.Load<Texture2D>("Character-2-RightFoot"));
            textures.Add(theContentManager.Load<Texture2D>("Character-2-LeftFoot"));

            this.Position = new Vector2(viewPort.Width / 2, viewPort.Height / 2);
            this.boundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.spriteTexture.Width, this.spriteTexture.Height);
        }

        //Draw the sprite to the screen 
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(spriteTexture, Position,
                null, Color.White, RotationAngle, origin, Scale, SpriteEffects.None, 0);
        }

        public void Walk(SpriteBatch theSpriteBatch)
        {
            DateTime now = DateTime.Now;
            if (now.Ticks > (this.lastWalk.Ticks + 2000000))
            {
                texturesIndex++;
                if (texturesIndex > 2)
                    texturesIndex = 1;
                this.lastWalk = now;
            }
            theSpriteBatch.Draw(textures[texturesIndex], Position,
                    null, Color.White, RotationAngle, origin, Scale, SpriteEffects.None, 0);
        }
    }
}

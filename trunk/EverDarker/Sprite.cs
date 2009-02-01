using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EverDarker
{
    public class Sprite
    {
        #region Members
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);
        //The texture object used when drawing the sprite
        public Texture2D spriteTexture;
        //The size of the Sprite
        public Rectangle Bounds;
        //Used to size the Sprite up or down from the original image
        public float Scale = 0f;
        //For the center of the sprite
        public Vector2 origin;
        
        #endregion Members

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            spriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            origin.X = spriteTexture.Width / 2;
            origin.Y = spriteTexture.Height / 2;
            this.Bounds = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.spriteTexture.Width, this.spriteTexture.Height);
        }
        
        //Draw the sprite to the screen 
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(spriteTexture, Position, null, Color.White);
        }

        public bool CheckCollision(Rectangle bounds)
        {
            bool collision = this.Bounds.Intersects(bounds);
            return collision;
        }

        // ScrollingBackground.Update
        public void UpdateX(float deltaX)
        {
            this.Position.X += deltaX;
            this.Bounds.X = (int)this.Position.X;
        }
        public void UpdateY(float deltaY)
        {
            this.Position.Y += deltaY;
            this.Bounds.Y = (int)this.Position.Y;
        }
    }
}

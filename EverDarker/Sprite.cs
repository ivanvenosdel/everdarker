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
        //public float RotationAngle = (float) (3 * Math.PI / 4);
        public float RotationAngle = 0;
        public Vector2 Position = new Vector2(0, 0);
        private Vector2 lastPosition;
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

        public bool CheckCollision(Rectangle bounds, Sprite shadow)
        {
            bool collision = this.Bounds.Intersects(bounds);
            if (collision)
            {
                MoveToLastPosition(shadow);
            }
            return collision;
        }

        public void MoveToLastPosition(Sprite shadow)
        {
            shadow.Position = shadow.lastPosition;
            this.Position = this.lastPosition;
            this.Bounds.X = (int)this.lastPosition.X;
            this.Bounds.Y = (int)this.lastPosition.Y;
        }

        public void UpdateX(Sprite shadow)
        {
            int gamespeed = 4;

            shadow.lastPosition = new Vector2(shadow.Position.X, shadow.Position.Y);

            this.lastPosition = new Vector2(this.Position.X, this.Position.Y);
            if (this.RotationAngle == 0)
            {
                this.Position.Y -= gamespeed;
                shadow.Position.Y -= gamespeed;
            }

            else if(this.RotationAngle == (float)(Math.PI / 8))
            {
                this.Position.X += (1 / 4) * gamespeed;
                this.Position.Y -= (3 / 4) * gamespeed;

                shadow.Position.X += (1 / 4) * gamespeed;
                shadow.Position.Y -= (3 / 4) * gamespeed;
            }

            else if (this.RotationAngle == (float)(Math.PI / 4))
            {
                this.Position.X += (1 / 2) * gamespeed;
                this.Position.Y -= (1 / 2) * gamespeed;

                shadow.Position.X += (1 / 2) * gamespeed;
                shadow.Position.Y -= (1 / 2) * gamespeed;
            }

            else if(this.RotationAngle == (float)(3 * Math.PI / 8))
            {
                this.Position.X += (3 / 4) * gamespeed;
                this.Position.Y -= (1 / 4) * gamespeed;

                shadow.Position.X += (3 / 4) * gamespeed;
                shadow.Position.Y -= (1 / 4) * gamespeed;
            }

            else if (this.RotationAngle == (float)(Math.PI / 2))
            {
                this.Position.X += gamespeed;

                shadow.Position.X += gamespeed;
            }

            else if (this.RotationAngle == (float)(5 * Math.PI / 8))
            {
                this.Position.X += (3 / 4) * gamespeed;
                this.Position.Y -= (1 / 4) * gamespeed;

                shadow.Position.X += (3 / 4) * gamespeed;
                shadow.Position.Y -= (1 / 4) * gamespeed;
            }

            else if (this.RotationAngle == (float)(3 * Math.PI / 4))
            {
                this.Position.X += (1 / 2) * gamespeed;
                this.Position.Y -= (1 / 2) * gamespeed;

                shadow.Position.X += (1 / 2) * gamespeed;
                shadow.Position.Y -= (1 / 2) * gamespeed;
            }

            else if (this.RotationAngle == (float)(7 * Math.PI / 8))
            {
                this.Position.X += (1 / 4) * gamespeed;
                this.Position.Y -= (3 / 4) * gamespeed;

                shadow.Position.X += (1 / 4) * gamespeed;
                shadow.Position.Y -= (3 / 4) * gamespeed;
            }

            else if (this.RotationAngle == (float) (Math.PI))
            {
                this.Position.Y += gamespeed;

                shadow.Position.Y += gamespeed;
            }

            else if(this.RotationAngle == (float) (9 * Math.PI / 8))
            {
                this.Position.X -= (1 / 4) * gamespeed;
                this.Position.Y += (3 / 4) * gamespeed;

                shadow.Position.X -= (1 / 4) * gamespeed;
                shadow.Position.Y += (3 / 4) * gamespeed;
            }

            else if (this.RotationAngle == (float)(5 * Math.PI / 4))
            {
                this.Position.X -= (1 / 2) * gamespeed;
                this.Position.Y += (1 / 2) * gamespeed;

                shadow.Position.X -= (1 / 2) * gamespeed;
                shadow.Position.Y += (1 / 2) * gamespeed;
            }

            else if (this.RotationAngle == (float)(11 * Math.PI / 8))
            {
                this.Position.X -= (3 / 4) * gamespeed;
                this.Position.Y += (1 / 4) * gamespeed;

                shadow.Position.X -= (3 / 4) * gamespeed;
                shadow.Position.X += (1 / 4) * gamespeed;
            }

            else if (this.RotationAngle == (float)(3 * Math.PI / 2))
            {
                this.Position.X -= gamespeed;
                shadow.Position.X -= gamespeed;
            }

            else if (this.RotationAngle == (float)(13 * Math.PI / 8))
            {
                this.Position.X -= (3 / 4) * gamespeed;
                this.Position.Y += (1 / 4) * gamespeed;

                shadow.Position.X -= (3 / 4) * gamespeed;
                shadow.Position.Y += (1 / 4) * gamespeed;
            }

            else if (this.RotationAngle == (float)(7 * Math.PI / 4))
            {
                this.Position.X -= (1 / 2) * gamespeed;
                this.Position.Y += (1 / 2) * gamespeed;

                shadow.Position.X -= (1 / 2) * gamespeed;
                shadow.Position.Y += (1 / 2) * gamespeed;
            }

            else if (this.RotationAngle == (float)(15 * Math.PI / 8))
            {
                this.Position.X -= (1 / 4) * gamespeed;
                this.Position.Y += (3 / 4) * gamespeed;

                shadow.Position.X -= (1 / 4) * gamespeed;
                shadow.Position.Y += (3 / 4) * gamespeed;
            }

            this.Bounds.X = (int)this.Position.X;
            this.Bounds.Y = (int)this.Position.Y;
        }

        public void UpdateY(float deltaY, Sprite shadow)
        {
            shadow.lastPosition = new Vector2(shadow.Position.X, shadow.Position.Y);
            shadow.Position.Y += deltaY;

            this.lastPosition = new Vector2(this.Position.X, this.Position.Y);
            this.Position.Y += deltaY;
            this.Bounds.Y = (int)this.Position.Y;
        }
    }
}

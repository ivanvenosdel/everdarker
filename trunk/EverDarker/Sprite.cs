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

        public void UpdatePosition(Sprite shadow, bool backwards)
        {
            int gamespeed = 2;
            float byPi = 0;

            if (backwards)
                gamespeed = gamespeed * (-1);

            shadow.lastPosition = new Vector2(shadow.Position.X, shadow.Position.Y);
            this.lastPosition = new Vector2(this.Position.X, this.Position.Y);

            //For the change in the X Position
            if (this.RotationAngle <= (Math.PI / 2))
            {
                byPi = (float)(this.RotationAngle / (Math.PI / 2));
                this.Position.X += (float)(byPi * gamespeed);
                this.Position.Y -= (float)((1 - byPi) * gamespeed);

                shadow.Position.X += (float)(byPi * gamespeed);
                shadow.Position.Y -= (float)((1 - byPi) * gamespeed);
            }
            else if ((this.RotationAngle > (Math.PI / 2)) && (this.RotationAngle <= (Math.PI)))
            {
                if (this.RotationAngle == (Math.PI))
                {
                    this.Position.Y += gamespeed;
                    shadow.Position.Y += gamespeed;
                }
                else
                {
                    byPi = (float)(this.RotationAngle / Math.PI);
                    this.Position.X += (float)((1 - byPi) * gamespeed);
                    this.Position.Y += (float)(byPi * gamespeed);

                    shadow.Position.X += (float)((1 - byPi) * gamespeed);
                    shadow.Position.Y += (float)(byPi * gamespeed);
                }
            }
            else if ((this.RotationAngle > (Math.PI)) && (this.RotationAngle <= (3 * Math.PI / 2)))
            {
                byPi = (float)(this.RotationAngle / (Math.PI / 2) - 2);

                this.Position.X -= (float)(byPi * gamespeed);
                this.Position.Y += (float)((1 - byPi) * gamespeed);

                shadow.Position.X -= (float)(byPi * gamespeed); 
                shadow.Position.Y += (float)((1 - byPi) * gamespeed);
            }
            else if((this.RotationAngle > (3 * Math.PI / 2)) && (this.RotationAngle <= (2 * Math.PI)))
            {
                byPi = (float)(this.RotationAngle / (Math.PI / 2) - 3);

                this.Position.X -= (float)(byPi * gamespeed);
                this.Position.Y += (float)((1 - byPi) * gamespeed);

                shadow.Position.X -= (float)(byPi * gamespeed);
                shadow.Position.Y += (float)((1 - byPi) * gamespeed);
            }
            else
                this.RotationAngle = 0;

            this.Bounds.X = (int)this.Position.X;
            this.Bounds.Y = (int)this.Position.Y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EverDarker
{
    class Tile
    {
        #region Members
        public Dictionary<Edges, WallSprite> Walls;
        public Rectangle area;
        // Rectangle((int)this.Position.X, (int)this.Position.Y, this.spriteTexture.Width, this.spriteTexture.Height);
        #endregion Members

        public Tile(int x, int y)
        {
            this.Walls = new Dictionary<Edges, WallSprite>();
            area = new Rectangle(x, y, 128, 128); //Try to make tile size base 2
        }

        public void LoadContent(ContentManager theContentManager, Viewport viewPort)
        {
            WallSprite wall = new WallSprite();
            Random rand = new Random();
            //Edges randomSide = (Edges)rand.Next(3);
            Edges randomSide = Edges.top;
            switch (randomSide)
            {
                case Edges.top:
                    wall.Position = new Vector2(area.Left, area.Top);
                    wall.LoadContent(theContentManager, "Cubicle-TwoSided", viewPort);
                    this.Walls[randomSide] = wall;
                    break;
                case Edges.bottom:
                    wall.Position = new Vector2(area.Left, area.Bottom);
                    wall.LoadContent(theContentManager, "Cubicle-TwoSided", viewPort);
                    wall.UpdateY(-wall.spriteTexture.Height);
                    this.Walls[randomSide] = wall;
                    break;
                case Edges.left:
                    //TODO: Need image
                    //wall.Position = new Vector2(area.Left, area.Bottom);
                    //wall.LoadContent(theContentManager, theAssetName, viewPort);
                    break;
                case Edges.right:
                    break;
                default:
                    break;
            }
        }
    }
}
enum Edges
{
    top,
    bottom,
    left,
    right
}
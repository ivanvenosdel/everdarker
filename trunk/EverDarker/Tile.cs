using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace EverDarker
{
    class Tile
    {
        #region Members
        public Dictionary<Edges, WallSprite> Walls;
        public Rectangle area;
        public Cell mazeCell;
        public Maze theMaze;
        // Rectangle((int)this.Position.X, (int)this.Position.Y, this.spriteTexture.Width, this.spriteTexture.Height);
        #endregion Members

        public Tile(int x, int y, Cell cell, Maze theMaze)
        {
            this.mazeCell = cell;
            this.theMaze = theMaze;
            this.Walls = new Dictionary<Edges, WallSprite>();
            area = new Rectangle(x, y, 100, 100); //Try to make tile size base 2
        }

        public void LoadContent(ContentManager theContentManager, Viewport viewPort)
        {
            if (mazeCell.Walls[1] == 1)
            {
                //Left
                WallSprite wall = new WallSprite();
                wall.Position = new Vector2(area.Left, area.Top);
                wall.LoadContent(theContentManager, "Cubicle-TwoSided-Verticle", viewPort);
                this.Walls[Edges.left] = wall;
            }
            if (mazeCell.Walls[0] == 1)
            {
                //top
                WallSprite wall = new WallSprite();
                wall.Position = new Vector2(area.Left, area.Top);
                wall.LoadContent(theContentManager, "Cubicle-TwoSided", viewPort);
                this.Walls[Edges.top] = wall;
            }
            if (mazeCell.Walls[2] == 1)
            {
                //Bottom
                WallSprite wall = new WallSprite();
                wall.Position = new Vector2(area.Left, area.Bottom - 9);
                wall.LoadContent(theContentManager, "Cubicle-TwoSided", viewPort);
                this.Walls[Edges.bottom] = wall;
            }
            if (mazeCell.Walls[3] == 1)
            {
                //Right
                WallSprite wall = new WallSprite();
                wall.Position = new Vector2(area.Right - 9, area.Top);
                wall.LoadContent(theContentManager, "Cubicle-TwoSided-Verticle", viewPort);
                this.Walls[Edges.right] = wall;
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
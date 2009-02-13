using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XNAGifAnimationLibrary;

namespace EverDarker
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Various 
        float rotateSpeed = (float)Math.PI / 16;

        //gif animation
        GifAnimation gif = null;

        //Audio Objects
        SoundEffect bgMusic;
        SoundEffect walkSFX;
        bool walkSound;
        bool mainSoundOn = false;
        DateTime lastWalk;

        //Background
        Sprite floor;

        //Grid
        List<List<Tile>> grid = new List<List<Tile>>();

        //Character
        Character player;

        //Maze generation
        Maze theMaze  = new Maze();

        //Shadow
        List<Texture2D> shadows = new List<Texture2D>();
        Texture2D shadow1;
        Texture2D shadow2;
        Texture2D shadow3;
        Texture2D shadow4;
        Texture2D shadow5;
        Texture2D shadow6;
        Texture2D shadow7;
        Texture2D shadow8;
        Texture2D shadow9;
        Texture2D shadow10;
        Sprite shadowSprite;
        DateTime LastShadow;
        int shadowFrame = 0;
        uint levelLength = 1500000000;
        int numOfFrames = 10;

        //Vectors
        Vector2 ZeroPosition;

        //Win Sequence
        Rectangle WinRec = new Rectangle(680, 680, 40, 40);
        Texture2D WinTex;
        bool hasWon = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 700;
            this.graphics.PreferredBackBufferHeight = 700;
            //this.graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Initial starting points
            ZeroPosition.X = 0;
            ZeroPosition.Y = 0;

            //floor
            floor = new Sprite();
            //character
            player = new Character();
            //Shadow
            shadowSprite = new Sprite();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //gif animation
            gif = Content.Load<GifAnimation>("IntroToGame-Everdarker");

            //Win Sequence
            WinTex = Content.Load<Texture2D>("YouWin");

            //Audio
            bgMusic = Content.Load<SoundEffect>("Audio-main");
            walkSFX = Content.Load<SoundEffect>("myfootstep");

            //Floor
            floor.LoadContent(Content, "Carpet");

            //Player
            Viewport viewPort = graphics.GraphicsDevice.Viewport;
            player.Scale = 0.1f;
            player.LoadContent(this.Content, "Character-2-Green", viewPort);
            
            //Grid
            theMaze.Initialize();
            theMaze.Generate();
            this.grid = new List<List<Tile>>(7);
            int x = 0;
            for (int i = 0; i < 7; i++)
            {
                List<Tile> column = new List<Tile>(7);
                int y = 0;
                for (int j = 0; j < 7; j++)
                {
                    Tile tile = new Tile(x, y, theMaze.Cells[i, j], theMaze);
                    tile.LoadContent(Content, viewPort);
                    column.Add(tile);
                    y = tile.area.Bottom;
                }
                this.grid.Add(column);
                x = column[column.Count() - 1].area.Right;
            }

            //Shadow Images
            shadow1 = Content.Load<Texture2D>("background-1");
            shadow2 = Content.Load<Texture2D>("background-2");
            shadow3 = Content.Load<Texture2D>("background-3");
            shadow4 = Content.Load<Texture2D>("background-4");
            shadow5 = Content.Load<Texture2D>("background-5");
            shadow6 = Content.Load<Texture2D>("background-6");
            shadow7 = Content.Load<Texture2D>("background-7");
            shadow8 = Content.Load<Texture2D>("background-8");
            shadow9 = Content.Load<Texture2D>("background-9");
            shadow10 = Content.Load<Texture2D>("background-10");
            shadows.Add(shadow1);
            shadows.Add(shadow2);
            shadows.Add(shadow3);
            shadows.Add(shadow4);
            shadows.Add(shadow5);
            shadows.Add(shadow6);
            shadows.Add(shadow7);
            shadows.Add(shadow8);
            shadows.Add(shadow9);
            shadows.Add(shadow10);

            shadowSprite.Position = new Vector2(-graphics.PreferredBackBufferWidth, -graphics.PreferredBackBufferHeight);
            shadowSprite.Position.X -= 300;
        }

        void Move(KeyboardState direction)
        {
            bool collision = false;
            for (int row = 0; row < grid.Count(); row++)
            {
                for (int i = 0; i < grid[row].Count(); i++)
                {
                    foreach (WallSprite wall in grid[row][i].Walls.Values)
                    {
                        collision = this.player.CheckCollision(wall.Bounds, shadowSprite);
                    }
                }
            }
            if (!collision)
            {
                if (direction.IsKeyDown(Keys.Up))
                    player.UpdatePosition(shadowSprite, false);
                else if (direction.IsKeyDown(Keys.Down))
                    player.UpdatePosition(shadowSprite, true);

                if (direction.IsKeyDown(Keys.Right))
                {
                    if (player.RotationAngle >= (float) (2* Math.PI))
                        player.RotationAngle = 0;

                    player.RotationAngle += rotateSpeed;
                }
                else if (direction.IsKeyDown(Keys.Left))
                {
                    if (player.RotationAngle >= (float) (2 * Math.PI))
                        player.RotationAngle = 0;

                    player.RotationAngle -= rotateSpeed;
                }
            }
            else
                shadowSprite.MoveToLastPosition(shadowSprite);
        }

        void BasicMovement(GameTime gameTime)
        {
            KeyboardState newstate = Keyboard.GetState();
            Viewport viewport = graphics.GraphicsDevice.Viewport;

            player.walking = (newstate.IsKeyDown(Keys.Up) || newstate.IsKeyDown(Keys.Down) || newstate.IsKeyDown(Keys.Right) || newstate.IsKeyDown(Keys.Left));

            if (!player.walking)
                walkSFX.Play(0);

            Move(newstate);
        }

        //Win Sequence
        void WinSequence(GameTime gameTime)
        {
            if (WinRec.Intersects(player.Bounds))
            {
                player.RotationAngle = (float)(Math.PI / 2);
                while (player.Position.X < 700)
                    player.Position.X += 1;

                hasWon = true;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            while (!hasWon)
            {
                BasicMovement(gameTime);
                WinSequence(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (!mainSoundOn)
            {
                bgMusic.Play(.8f);
                mainSoundOn = true;
            }

            spriteBatch.Begin();
            floor.Draw(this.spriteBatch);
            DateTime walkNow = DateTime.Now;
            if (player.walking)
            {
                player.Walk(this.spriteBatch);

                if (!walkSound)
                {
                    walkSFX.Play(1);
                    walkSound = true;
                    lastWalk = walkNow;
                }
                else
                {
                    if (walkNow.Ticks > (lastWalk.Ticks + 5000000))
                    {
                        walkSound = false;
                    }
                }

            }
            else
                player.Draw(this.spriteBatch);

            //drawing the cubes
            foreach (List<Tile> row in grid)
            {
                foreach (Tile tile in row)
                {
                    foreach (WallSprite wall in tile.Walls.Values)
                    {
                        wall.Draw(this.spriteBatch);
                    }
                }
            }
            
            DateTime shadowNow = DateTime.Now;
            if(shadowNow.Ticks > (LastShadow.Ticks + levelLength/numOfFrames))
            {
                LastShadow = shadowNow;
                if (shadowFrame != numOfFrames - 1)
                {
                    shadowFrame++;
                }
            }
            spriteBatch.Draw(shadows[shadowFrame], shadowSprite.Position, Color.White);
            spriteBatch.End();

            if (hasWon)
                spriteBatch.Draw(WinTex, new Vector2(350, 350), Color.White);

            //if (gameTime.TotalGameTime.TotalSeconds < 18)
            //{
            //    spriteBatch.Begin();
            //    spriteBatch.Draw(gif.GetTexture(), new Rectangle(0, 0, graphics.PreferredBackBufferWidth , graphics.PreferredBackBufferHeight), Color.White);
            //    spriteBatch.End();

            //    gif.Update(30000);
            //}
           
            base.Draw(gameTime);
        }
    }
}
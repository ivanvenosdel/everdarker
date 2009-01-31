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

namespace EverDarker
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Background
        ScrollingBackground floor;

        //Cubicle Wall
        WallSprite wall;
        List<List<WallSprite>> grid = new List<List<WallSprite>>();

        //Character
        Character player;

        //Vectors
        Vector2 ZeroPosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            floor = new ScrollingBackground();
            //floor = new Sprite();
            //floor.Scale = .5f;
            //character
            player = new Character();
            
            //Cubicle Wall
            wall = new WallSprite();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Floor
            Texture2D floorTexture = Content.Load<Texture2D>("Carpet");
            floor.Load(graphics.GraphicsDevice, floorTexture);

            //Player
            Viewport viewPort = graphics.GraphicsDevice.Viewport;
            player.LoadContent(this.Content, "Character-1-Blue", viewPort);

            //Cubicle Walls
            wall.LoadContent(this.Content);

            grid.Add(new List<WallSprite>());
            grid[0].Add(wall);
        }

        

        void BasicMovement(GameTime gameTime)
        {
            KeyboardState newstate = Keyboard.GetState();
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            // The time since Update was called last.
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
           
            if(newstate.IsKeyDown(Keys.Up))
            {
                floor.UpdateY(elapsed * 100);

                for (int i = 0; i <= grid.Count-1; i++)
                {
                    for (int j = 0; j <= grid[i].Count-1; j++)
                    {
                        grid[i][j].Position.Y += 3;
                    }
                }
            }

            if (newstate.IsKeyDown(Keys.Down))
            {
                floor.UpdateY(elapsed * -100);
                grid[0][0].Position.Y -= 5;
            }

            if (newstate.IsKeyDown(Keys.Right))
            {
                floor.UpdateX(elapsed * -100);
                grid[0][0].Position.X += 5;
            }

            if (newstate.IsKeyDown(Keys.Left))
            {
                floor.UpdateX(elapsed * 100);
                grid[0][0].Position.X -= 5;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            

            // TODO: Add your update logic here
            BasicMovement(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            floor.Draw(this.spriteBatch);
            player.Draw(this.spriteBatch);
            /*
            spriteBatch.Draw(this.player.texture, screenPos, null, Color.White, rotationAngle,
        origin, 0.45f, SpriteEffects.None, 0f);
            */

            //drawing the cubes
            for (int i = 0; i <= grid.Count-1; i++)
            {
                for (int j = 0; j <= grid[i].Count-1; j++)
                {
                    grid[i][j].Draw(this.spriteBatch);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

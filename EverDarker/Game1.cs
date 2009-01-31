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
        Sprite floor;

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
            floor = new Sprite();
            floor.Scale = .5f;
            //character
            player = new Character();
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Floor
            floor.LoadContent(this.Content, "Carpet");
            floor.Position = new Vector2(ZeroPosition.X, ZeroPosition.Y);

            //Player
            Viewport viewPort = graphics.GraphicsDevice.Viewport;
            player.LoadContent(this.Content, "Character-1-Blue", viewPort);

            //Floor tiles:
            //List<List<Sprite>> grid = new List<List<Sprite>>();
            //grid[0][1]

            // TODO: use this.Content to load your game content here
        }

        void BasicMovement(GameTime gameTime)
        {
            KeyboardState newstate = Keyboard.GetState();
            Viewport viewport = graphics.GraphicsDevice.Viewport;
           
            if(newstate.IsKeyDown(Keys.Up))
            {
                
            }

            if (newstate.IsKeyDown(Keys.Down))
            {

            }

            if (newstate.IsKeyDown(Keys.Right))
            {
                float elapsed = .1f;
                //rotationAngle += elapsed;
                float circle = MathHelper.Pi * 2;
                //rotationAngle = rotationAngle % circle;
            }

            if (newstate.IsKeyDown(Keys.Left))
            {
                float elapsed = -.1f;
                //rotationAngle += elapsed;
                float circle = MathHelper.Pi * 2;
                //rotationAngle = rotationAngle % circle;
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

            // The time since Update was called last.

            // TODO: Add your update logic here

            if (floor.Position.X < -floor.Size.Width)
            {
                floor.Position.X = floor.Position.X + floor.Size.Width;
            }
            Vector2 aDirection = new Vector2(-1, 0);
            Vector2 aSpeed = new Vector2(160, 0);
            floor.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //BasicMovement(gameTime);

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

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

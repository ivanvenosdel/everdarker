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

        Texture2D carpetTexture;
        Character player = new Character();
        private Vector2 origin;
        private Vector2 screenPos;
        private float rotationAngle = 0f;

        //Vectors
        Vector2 ZeroPosition;
        Vector2 mainCharacterPosition;

        //Random Variables
        int characterSpeed;

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

            characterSpeed = 4;

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
            carpetTexture = Content.Load<Texture2D>("Carpet");
            this.player.texture = Content.Load<Texture2D>("Character-1-Blue");
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            origin.X = this.player.texture.Width / 2;
            origin.Y = this.player.texture.Height / 2;
            screenPos.X = viewport.Width / 2;
            screenPos.Y = viewport.Height / 2;
            
            //mainCharacterTexture = Content.Load<Texture2D>("MainCharacter");

            // TODO: use this.Content to load your game content here
        }

        void BasicMovement()
        {
            KeyboardState newstate = Keyboard.GetState();

            if(newstate.IsKeyDown(Keys.Up))
            {
                screenPos.Y -= characterSpeed;
            }

            if (newstate.IsKeyDown(Keys.Down))
            {
                screenPos.Y += characterSpeed;
            }

            if (newstate.IsKeyDown(Keys.Right))
            {
                screenPos.X += characterSpeed;
            }

            if (newstate.IsKeyDown(Keys.Left))
            {
                screenPos.X -= characterSpeed;
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
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;

            BasicMovement();

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
            spriteBatch.Draw(carpetTexture,ZeroPosition, Color.White);
            spriteBatch.Draw(this.player.texture, screenPos, null, Color.White, rotationAngle,
        origin, 1.0f, SpriteEffects.None, 0f);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

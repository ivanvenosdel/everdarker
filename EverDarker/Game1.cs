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

        //Audio Objects
        SoundEffect soundEffect;
        bool walkSound;
        DateTime lastWalk;

        //Background
        ScrollingBackground floor;

        //Cubicle Wall
        WallSprite wall;
        List<List<WallSprite>> grid = new List<List<WallSprite>>();
        int wallHeightPadding = 65;
        bool noDown = false;
        bool noUp = false;
        bool noRight = false;
        bool noLeft = false;

        //Character
        Character player;

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
        Texture2D shadow11;
        Texture2D shadow12;
        Rectangle shadowRectangle;
        DateTime LastShadow;
        int shadowFrame = 0;
        uint levelLength = 3000000000;
        int numOfFrames = 12;

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
            Random rand = new Random();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Floor
            Texture2D floorTexture = Content.Load<Texture2D>("Carpet");
            floor.Load(graphics.GraphicsDevice, floorTexture);

            //Player
            Viewport viewPort = graphics.GraphicsDevice.Viewport;
            player.Scale = 0.1f;
            player.LoadContent(this.Content, "Character-2-Green", viewPort);

            //Cubicle Walls
            wall.LoadContent(this.Content);

            grid.Add(new List<WallSprite>());
            grid[0].Add(wall);
            grid[0][0].Position.X = rand.Next(viewPort.Width - grid[0][0].spriteTexture.Width);

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
            shadow11 = Content.Load<Texture2D>("background-11");
            shadow12 = Content.Load<Texture2D>("background-11");
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
            shadows.Add(shadow11);
            shadows.Add(shadow12);

            shadowRectangle = new Rectangle((int)0, (int)0, viewPort.Width, viewPort.Height);

            //Audio
            soundEffect = Content.Load<SoundEffect>("myfootstep");
        }

        void RotateX(KeyboardState newstate, float upMax, float downMax, float xMax)
        {
            float rotateSpeed = .03f;
            float margin = .03f;
            if (newstate.IsKeyDown(Keys.Up))
            {
                if (player.RotationAngle < upMax - margin)
                    player.RotationAngle += rotateSpeed;
                else if (player.RotationAngle > upMax + margin)
                    player.RotationAngle -= rotateSpeed;
            }
            if (newstate.IsKeyDown(Keys.Down))
            {
                if (player.RotationAngle < downMax - margin)
                    player.RotationAngle += rotateSpeed;
                else if (player.RotationAngle > downMax + margin)
                    player.RotationAngle -= rotateSpeed;
            }
            if (newstate.IsKeyUp(Keys.Down) && newstate.IsKeyUp(Keys.Up))
            {
                if (player.RotationAngle < xMax - margin)
                    player.RotationAngle += rotateSpeed;
                else if (player.RotationAngle > xMax + margin)
                    player.RotationAngle -= rotateSpeed;
            }
        }

        void BasicMovement(GameTime gameTime)
        {
            KeyboardState newstate = Keyboard.GetState();
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            float moveSpeed = 1;

            player.walking = (newstate.IsKeyDown(Keys.Up) || newstate.IsKeyDown(Keys.Down) || newstate.IsKeyDown(Keys.Right) || newstate.IsKeyDown(Keys.Left));

            if (newstate.IsKeyDown(Keys.Up))
            {
                //Rotate
                float rotateSpeed = .03f;
                float margin = .03f;
                if (newstate.IsKeyUp(Keys.Right) && newstate.IsKeyUp(Keys.Left))
                {
                    if (player.RotationAngle < 0f - margin)
                        player.RotationAngle += rotateSpeed;
                    else if (player.RotationAngle > 0f + margin)
                        player.RotationAngle -= rotateSpeed;
                }
                //Movement
                
                for (int i = 0; i <= grid.Count - 1; i++)
                {
                    for (int j = 0; j <= grid[i].Count - 1; j++)
                    {
                        Rectangle wallRectangle = new Rectangle((int)grid[i][j].Position.X, 
                            (int)grid[i][j].Position.Y, grid[i][j].spriteTexture.Width, 
                            grid[i][j].spriteTexture.Height);

                        if (!player.boundingBox.Intersects(wallRectangle) || (noDown))
                        {
                            grid[i][j].Move(0, 3);
                            floor.UpdateY(moveSpeed);

                            if (!player.boundingBox.Intersects(wallRectangle))
                            {
                                noDown = false;
                                noRight = false;
                                noLeft = false;
                            }
                        }
                        else
                        {
                            noUp = true;
                        }
                    }
                }
            }
            if (newstate.IsKeyDown(Keys.Down))
            {
                for (int i = 0; i <= grid.Count - 1; i++)
                {
                    for (int j = 0; j <= grid[i].Count - 1; j++)
                    {
                        Rectangle wallRectangle = new Rectangle((int)grid[i][j].Position.X,
                            (int)grid[i][j].Position.Y + 55, grid[i][j].spriteTexture.Width,
                            grid[i][j].spriteTexture.Height);

                        if (!player.boundingBox.Intersects(wallRectangle) || (noUp))
                        {
                            grid[i][j].Move(0, -3);
                            floor.UpdateY(-moveSpeed);
                            if (!player.boundingBox.Intersects(wallRectangle))
                            {
                                noUp = false;
                                noRight = false;
                                noLeft = false;
                            }
                        }
                        else
                        {
                            noDown = true;
                        }
                    }
                }
            }
            if (newstate.IsKeyDown(Keys.Right))
            {
                RotateX(newstate, .785f, 2.355f, 1.57f);

                for (int i = 0; i <= grid.Count - 1; i++)
                {
                    for (int j = 0; j <= grid[i].Count - 1; j++)
                    {
                        Rectangle wallRectangle = new Rectangle((int)grid[i][j].Position.X,
                           (int)grid[i][j].Position.Y, grid[i][j].spriteTexture.Width,
                           grid[i][j].spriteTexture.Height + wallHeightPadding);

                        if (!player.boundingBox.Intersects(wallRectangle) || (noLeft))
                        {
                            grid[i][j].Move(-3, 0);
                            floor.UpdateX(-moveSpeed);
                        }
                        else
                        {
                            noRight = true;
                        }
                    }
                }
            }
            if (newstate.IsKeyDown(Keys.Left))
            {
                RotateX(newstate, -.785f, -2.355f, -1.57f);
                floor.UpdateX(moveSpeed);
                for (int i = 0; i <= grid.Count - 1; i++)
                {
                    for (int j = 0; j <= grid[i].Count - 1; j++)
                    {
                        grid[i][j].Move(3, 0);
                    }
                }
            }
            float circle = MathHelper.Pi * 2;
            player.RotationAngle = player.RotationAngle % circle;
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
            DateTime walkNow = DateTime.Now;
            if (player.walking)
            {
                player.Walk(this.spriteBatch);

                if (walkSound == false)
                {
                    soundEffect.Play();
                    walkSound = true;
                    lastWalk = walkNow;
                }
                else
                {
                    if(walkNow.Ticks > (lastWalk.Ticks + 5000000))
                    {
                        walkSound = false;
                    }
                }
    
            }
            else
                player.Draw(this.spriteBatch);

            //drawing the cubes
            for (int i = 0; i <= grid.Count-1; i++)
            {
                for (int j = 0; j <= grid[i].Count-1; j++)
                {
                    grid[i][j].Draw(this.spriteBatch);
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

            spriteBatch.Draw(shadows[shadowFrame], shadowRectangle, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using PlayerNamespace;
using BaseObjectNamespace;
using System;
using System.Collections.Generic;

namespace Gutwood
{
    public class Game1 : Game
    {
        List<Bullet> killTheseBullets = new List<Bullet>();
        List<List<Rectangle>> allCollisiodnRectangles = new List<List<Rectangle>>();
        List<List<Rectangle>> allCollisionRectangles = new List<List<Rectangle>>();
        DateTime date;
        Random randomNumberGenerator = new Random();
        private FrameCounter _frameCounter = new FrameCounter();
        private SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int CurrentDirection;

        Player player;
        BaseObject background;
        BaseObject mouse;
        BaseObject tree;
        BaseObject tree2;

        List<Bullet> bullets = new List<Bullet>();

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        MouseState currentMouseState;
        MouseState previousMouseState;

        Texture2D bulletTexture;




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            font = Content.Load<SpriteFont>("Score");
            player = new Player();
            background = new BaseObject();
            mouse = new BaseObject();
            tree = new BaseObject();
            tree2 = new BaseObject();
            background.SpriteScale = 4f; //HACK FIGURE OUT ASAP!!!!
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            bulletTexture = Content.Load<Texture2D>("Bullet");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y +
                GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(Content.Load<Texture2D>("Mario"), playerPosition);
            background.Initialize(Content.Load<Texture2D>("grass-1"), new Vector2(0, 0));
            mouse.Initialize(Content.Load<Texture2D>("crosshair"), new Vector2(0, 0));
            tree.Initialize(Content.Load<Texture2D>("Tree"), new Vector2(250, 250), "Tree", isCollidable: true);
            tree2.Initialize(Content.Load<Texture2D>("Tree"), new Vector2(500, 100), "Tree2", isCollidable: true);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;

            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentMouseState = Mouse.GetState();

            mouse.Position.X = currentMouseState.X - mouse.BaseObjectTexture.Width / 2; mouse.Position.Y = currentMouseState.Y - mouse.BaseObjectTexture.Height / 2;


            //HACK FOR TESTING COLLISION ALGORITHM!
            //

            allCollisionRectangles.Add(tree.CollisionRectangles);
            allCollisionRectangles.Add(tree2.CollisionRectangles);
            player.UpdateCollision(allCollisionRectangles);
            
            UpdatePlayer();

            base.Update(gameTime);
        }      

        public void UpdatePlayer()
        {

            player.IsRunning = currentKeyboardState.IsKeyDown(Keys.LeftShift) ? false : true;
            if (currentMouseState.RightButton == ButtonState.Released) {
                if (!player.CollidingLeft && (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A) ||
                     currentGamePadState.DPad.Left == ButtonState.Pressed || (currentGamePadState.ThumbSticks.Left.X >= -1 && currentGamePadState.ThumbSticks.Left.X < -.3)))
                {
                    player.Position.X -= player.Speed;
                }

                if (!player.CollidingRight && (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D) ||
                     currentGamePadState.DPad.Right == ButtonState.Pressed || (currentGamePadState.ThumbSticks.Left.X <= 1 && currentGamePadState.ThumbSticks.Left.X > .3)))
                {
                    player.Position.X += player.Speed;
                }

                if (!player.CollidingTop && (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W) ||
                    currentGamePadState.DPad.Up == ButtonState.Pressed || (currentGamePadState.ThumbSticks.Left.Y <= 1 && currentGamePadState.ThumbSticks.Left.Y > .3)))
                {
                    player.Position.Y -= player.Speed;
                }

                if (!player.CollidingBottom && (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S) ||
                     currentGamePadState.DPad.Down == ButtonState.Pressed || (currentGamePadState.ThumbSticks.Left.Y >= -1 && currentGamePadState.ThumbSticks.Left.Y < -.3)))
                {
                    player.Position.Y += player.Speed;
                }
            }

            if(player.IsRunning)
            {
                player.Speed = 5;
            }
            else
            {
                player.Speed = 3;
            }

            //Make sure we don't go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);


            if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.RightButton == ButtonState.Pressed)
            {
                Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
                Vector2 middleOfPlayer = new Vector2(player.Position.X+player.Width/2, player.Position.Y+player.Height/2);
                bullets.Add(new Bullet(bulletTexture, middleOfPlayer, mousePosition));
            }
            UpdateBullets();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //Begin sprite batch
            spriteBatch.Begin();

            //Draw background
            background.Draw(spriteBatch);

            //Draw mouse
            mouse.Draw(spriteBatch);

            //Draw a debugging tree
            tree.Draw(spriteBatch);
            tree2.Draw(spriteBatch);

            //Draw the player
            player.Draw(spriteBatch);

            //Draw text
            DrawFPS(gameTime);

            //Draw the bullets
            foreach (Bullet b in bullets)
            {
                if (b.Position.X > GraphicsDevice.Viewport.Width || b.Position.Y > GraphicsDevice.Viewport.Height
                    || b.Position.X < 0 || b.Position.Y < 0)
                {
                    killTheseBullets.Add(b);
                }
                b.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        protected void DrawFPS(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
            date = DateTime.Now;
            spriteBatch.DrawString(font, fps, new Vector2(1, 1), Color.Black);
            spriteBatch.DrawString(font, date.Hour + ":" + date.Minute.ToString().PadLeft(2, '0'), new Vector2(1, 20), Color.Black);
            spriteBatch.DrawString(font, currentGamePadState.ThumbSticks.Left.ToString(), new Vector2(1, 50), Color.Black);

            // other draw code here
        }
        

        private void UpdateBullets()
        {
            //Kill off dead bullets
            foreach (Bullet b in killTheseBullets)
            {
                bullets.Remove(b);
            }
            killTheseBullets.Clear();
        }

        private void CheckThumbsticks()
        {
            player.movingDown = player.movingLeft = player.movingRight = player.movingUp = false;
            if (currentGamePadState.ThumbSticks.Left.Y == 1)
            {
                player.movingUp = true;
            }
            if (currentGamePadState.ThumbSticks.Left.Y == -1)
            {
                player.movingDown = true;
            }
            if (currentGamePadState.ThumbSticks.Left.X == 1)
            {
                player.movingRight = true;
            }
            if (currentGamePadState.ThumbSticks.Left.X == -1)
            {
                player.movingLeft = true;
            }
        }
    }
}

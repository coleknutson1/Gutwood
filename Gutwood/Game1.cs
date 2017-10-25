using Microsoft.Xna.Framework;
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

        private SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        BaseObject background;
        BaseObject mouse;
        List<Bullet> bullets = new List<Bullet>();

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        MouseState currentMouseState;
        MouseState previousMouseState;

        
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            bullets.Add(new Bullet());
            font = Content.Load<SpriteFont>("Score");
            player = new Player();
            background = new BaseObject();
            mouse = new BaseObject();
            background.SpriteScale = 4f; //HACK FIGURE OUT ASAP!!!!
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y +
                GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(Content.Load<Texture2D>("Mario"), playerPosition);
            background.Initialize(Content.Load<Texture2D>("grass-1"), new Vector2(0, 0));
            mouse.Initialize(Content.Load<Texture2D>("crosshair"), new Vector2(0, 0));
            bullets[0].Initialize(Content.Load<Texture2D>("Bullet"), new Vector2(220, 220));
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

            mouse.Position.X = currentMouseState.X - mouse.BaseObjectTexture.Width/2; mouse.Position.Y = currentMouseState.Y - mouse.BaseObjectTexture.Height/2;
            player.UpdatePlayer(gameTime, player, GraphicsDevice, currentGamePadState, currentKeyboardState, currentMouseState);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            mouse.Draw(spriteBatch);
            player.Draw(spriteBatch);
            bullets[0].Draw(spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }



        
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseObjectNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerNamespace
{
    class Player : BaseObject
    {
        public Player()
        {}
        public void UpdatePlayer(GameTime gametime, Player player, GraphicsDevice graphicsDevice, GamePadState currentGamePadState, KeyboardState currentKeyboardState, MouseState currentMouseState)
        {
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * Speed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * Speed;

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A) ||
                 currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= Speed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D) ||
                 currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += Speed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W) ||
                currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= Speed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S) ||
                 currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += Speed;
            }

            //Make sure we don't go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, graphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, graphicsDevice.Viewport.Height - player.Height);

            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            if (currentMouseState.LeftButton == ButtonState.Pressed )
            {
                Vector2 posDelta = mousePosition - player.Position;
                posDelta.Normalize();
                posDelta = posDelta * Speed;
                player.Position = player.Position + posDelta;
            }

        }


    }
}

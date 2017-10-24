using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    class LivingEntity
    {
        public Texture2D LivingEntityTexture;

        public Vector2 Position;

        public bool Active;

        public int Health;

        public float Scale = 1f;

        public enum DirectionFacing { UP = 1, RIGHT = 2, DOWN = 3, LEFT = 4 };

        private SpriteEffects directionFacing = SpriteEffects.None;

        public int Width
        {
            get { return LivingEntityTexture.Width; }
        }

        public int Height
        {
            get { return LivingEntityTexture.Height; }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            LivingEntityTexture = texture;

            Position = position;

            Active = true;

            Health = 100;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LivingEntityTexture, Position, null, Color.White, 0f, Vector2.Zero, Scale, directionFacing, 0f);
        }

    }
}

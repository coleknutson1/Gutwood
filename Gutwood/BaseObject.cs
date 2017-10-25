using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjectNamespace
{
    class BaseObject
    {
        public Texture2D BaseObjectTexture;

        public Vector2 Position;

        public bool Active;

        public int Health;

        public float SpriteScale = 1f;

        public float Speed = 10f;

        public enum DirectionFacing { UP = 1, RIGHT = 2, DOWN = 3, LEFT = 4 };

        private SpriteEffects directionFacing = SpriteEffects.None;

        public int Width
        {
            get { return BaseObjectTexture.Width; }
        }

        public int Height
        {
            get { return BaseObjectTexture.Height; }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            BaseObjectTexture = texture;

            Position = position;

            Active = true;
        }

        public void Update()
        {

        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BaseObjectTexture, Position, null, Color.White, 0f, Vector2.Zero, SpriteScale, directionFacing, 0f);
        }

    }
}

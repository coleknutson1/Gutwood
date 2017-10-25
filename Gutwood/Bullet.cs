using BaseObjectNamespace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gutwood
{
    class Bullet : BaseObject
    {
        Vector2 initialPosition, destinationPosition;
        public Bullet(Texture2D bulletTexture, Vector2 initial, Vector2 destination) //Pass in vector eventually....
        {
            Speed = 25;
            initialPosition = initial;
            destinationPosition = destination;
            this.Initialize(bulletTexture, initialPosition);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 posDelta = initialPosition - destinationPosition;
            posDelta.Normalize();
            posDelta = posDelta * Speed;
            Position = Position - posDelta;
            spriteBatch.Draw(BaseObjectTexture, Position, null, Color.NavajoWhite, 1f, Vector2.Zero, SpriteScale, SpriteEffects.None, 0f);
        }
    }
}

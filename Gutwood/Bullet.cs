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
        Random rand = new Random();
        float DestinationX, DestinationY;
        Vector2 initialPosition, destinationPosition;
        public Bullet(Texture2D bulletTexture, Vector2 initial, Vector2 destination) //Pass in vector eventually....
        {
            initialPosition = initial;
            destinationPosition = destination;
            this.Initialize(bulletTexture, initialPosition);
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 posDelta = initialPosition - destinationPosition;
            posDelta.Normalize();
            posDelta = posDelta * Speed;
            destinationPosition = destinationPosition + posDelta;
            spriteBatch.Draw(BaseObjectTexture, Position, null, new Color(rand.Next(1, 255), rand.Next(1, 255), rand.Next(1, 255)), rand.Next(1,15), Vector2.Zero, SpriteScale, SpriteEffects.None, 0f);
        }
    }
}

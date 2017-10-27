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

        public bool UpdateCollision(List<List<Rectangle>> collidableObjectsLists)
        {
            foreach (List<Rectangle> collidableObjectsList in collidableObjectsLists)
            {
                foreach (Rectangle collidableObject in collidableObjectsList)
                {
                    if ((Position.X <= collidableObject.Right && Position.X > collidableObject.Center.X && Position.Y + Height >= collidableObject.Top && Position.Y <= collidableObject.Bottom)
                     || (Position.X + Width >= collidableObject.Left && Position.X < collidableObject.Center.X && Position.Y + Height >= collidableObject.Top && Position.Y <= collidableObject.Bottom)
                     || (Position.Y <= collidableObject.Bottom && Position.Y > collidableObject.Center.Y && Position.X + Width >= collidableObject.Left && Position.X <= collidableObject.Right)
                     || (Position.Y + Height >= collidableObject.Top && Position.Y + Height <= collidableObject.Bottom && Position.X + Width >= collidableObject.Left && Position.X <= collidableObject.Right))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

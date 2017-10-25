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
        public Bullet(Texture2D bulletTexture, int x, int y/* Vector2 gun, Vector2 crosshair*/)
        {
           this. Initialize(bulletTexture, new Vector2(x, y));

        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 posDelta = mousePosition - player.Position;
            //posDelta.Normalize();
            //posDelta = posDelta * Speed;
            //player.Position = player.Position + posDelta;
            spriteBatch.Draw(BaseObjectTexture, Position, null, new Color(rand.Next(1, 255), rand.Next(1, 255), rand.Next(1, 255)), rand.Next(1,15), Vector2.Zero, SpriteScale, SpriteEffects.None, 0f);
        }
    }
}

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
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BaseObjectTexture, Position, null, Color.White, 70f, Vector2.Zero, SpriteScale, SpriteEffects.None, 0f);
        }
    }
}

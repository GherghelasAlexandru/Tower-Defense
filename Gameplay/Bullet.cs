using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class Bullet : Sprite
    {
        private float _timer;
        
        public Bullet(Texture2D texture)
          : base(texture)
        {
            xVelocity = 4f;
            yVelocity = 4f;
        }
        
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            Position += Direction * xVelocity;
        }
    }
}

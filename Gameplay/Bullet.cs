using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public abstract class Bullet : Sprite
    {
        private float _timer;
        protected int dmg;
        public Bullet(Texture2D texture)
          : base(texture)
        {
            
        }

        public int getDmg()
        {
            return dmg;
        }

        public void setDmg(int dmg)
        {
            this.dmg = dmg;
        }
        
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsActive = true;

            _position += Direction * xVelocity;
        }
    }
}

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
        public bool bulletIsDead = false;
        protected int dmg;
        public Bullet(Texture2D texture)
          : base(texture)
        {
            this.LifeSpan = 1f;
        }
    
        
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
           
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsActive = true;

            _position += Direction * xVelocity;
        }

        // get and set methods

        public void SetDmg(int dmg)
        {
            this.dmg = dmg;
        }

        public int GetDmg()
        {
            return dmg;
        }

        public void SetBulletIsDead(bool bulletIsDead)
        {
            this.bulletIsDead = bulletIsDead;
        }

        public bool GetBulletIsDead()
        {
            return this.bulletIsDead;
        }
    }
}

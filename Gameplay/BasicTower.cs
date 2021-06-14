using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public abstract class BasicTower : Sprite
    {
        public Bullet Bullet;
        public float timer;         //Initialize a 10 second timer
        public float TIMER;
        public int towerPrice;
        private List<Bullet> bullets;
        public int towerRange;
        
        public MouseState mouseState;
        
  
       

        public BasicTower(Texture2D texture)
          : base(texture)
        {
            bullets = new List<Bullet>();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            CenterOrigin();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                //Timer expired, execute action
                AddBullet(sprites);
                timer = TIMER;   //Reset Timer
            }
        }
        
        public int GetPrice()
        {
            return towerPrice;
        }


        private void AddBullet(List<Sprite> sprites)
        {
            //Bullet newBullet = new Bullet()
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet._position = this._position;
            bullet.xVelocity = xVelocity * 2;
            bullet.LifeSpan = 0.1f;
            bullet.Parent = this;
            sprites.Add(bullet);
            bullets.Add(bullet);
        }

        public List<Bullet> GetBullets() {
            return bullets;
        }

        public void RemoveBullet()
        {
            foreach(Bullet bullet in GetBullets())
            {
                if(bullet.bulletIsDead)
                {
                    bullets.Remove(bullet);
                }
            }    
        }
    }
}

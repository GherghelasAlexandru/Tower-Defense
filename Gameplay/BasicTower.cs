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
       //Initialize a 10 second timer
        public float TIMER;
        public int towerPrice;
        public List<Bullet> bullets;

        public MouseState mouseState;
        
        public BasicTower(Texture2D texture)
          : base(texture)
        {
            bullets = new List<Bullet>();

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Bullet.CenterOrigin();
            CenterOrigin();
           
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _timer -= elapsed;
                if (_timer < 0 && firing)
                {
                    
                    AddBullet();
                    _timer = TIMER;   //Reset Timer
                }

            
        }

        public void RemoveBullet()
        {
            bullets.Remove(Bullet);
        }
        
        public int GetPrice()
        {
            return towerPrice;
        }

   
    
        private void AddBullet()
        {
            //Bullet newBullet = new Bullet()
            var bullet = Bullet.Clone() as Bullet;
           
            bullet.Position = _position;  
            bullet.LifeSpan = 0f;
            bullet.Parent = this;
            bullets.Add(bullet);
        
        }

        public List<Bullet> GetBullets() {
            return bullets;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bullets)
            {
                if (bullet.IsActive)
                {
                    spriteBatch.Draw(bullet._texture, bullet._position, Color.White);
                }
            }
                base.Draw(spriteBatch);
            
        }
    }
}

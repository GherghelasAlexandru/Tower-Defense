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
        protected List<Bullet> bullets;

        
        public MouseState mouseState;
        
        public BasicTower(Texture2D texture)
          : base(texture)
        {
            this.bullets = new List<Bullet>();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            CenterOrigin();

            //  maybe move  firing inside basic tower?? cause only the tower is shooting
            if(firing)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed;
                if (timer < 0)
                {
                    //Timer expired, execute action
                    AddBullet(sprites);
                    timer = TIMER;   //Reset Timer
                }
            }
        }
       
      

        private void AddBullet(List<Sprite> sprites)
        {
            //Bullet newBullet = new Bullet()
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet._position = this._position;
            bullet.xVelocity = xVelocity * 2;
            bullet.LifeSpan = 0f;
            bullet.Parent = this;
            sprites.Add(bullet);
            bullets.Add(bullet);
        }

        public void RemoveBullet()
        {

            foreach(Bullet bullet in GetBullets())
            {
                if(bullet.bulletIsDead == true)
                {
                    bullets.Remove(bullet);
                }
            }    
        }

        // get and set methods

        public void SetPrice(int towerPrice)
        {
            this.towerPrice = towerPrice;
        }
        public int GetPrice()
        {
            return towerPrice;
        }

        public void Settimer(float timer)
        {
            this.timer = timer;
        }

        public float Gettimer()
        {
            return this.timer;
        }

        public void SetTIMER(float TIMER)
        {
            this.TIMER = TIMER;
        }

        public float GetTIMER()
        {
            return this.TIMER;
        }

        public void SetBullets(List<Bullet> bullets)
        {
            this.bullets = bullets;
        }

        public List<Bullet> GetBullets()
        {
            return this.bullets;
        }

        public void SetBullet(Bullet bullet)
        {
            this.Bullet = bullet;
        }

        public Bullet GetBullet()
        {
            return this.Bullet;
        }

        public void SetMouseState(MouseState mouse)
        {
            this.mouseState = mouse;
        }

        public MouseState GetMouseState()
        {
            return this.mouseState;
        }

    }
}

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
        public List<Sprite> bullets;

        public MouseState mouseState;
        
        public BasicTower(Texture2D texture)
          : base(texture)
        {
            this.bullets = new List<Sprite>();

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Bullet.CenterOrigin();
            CenterOrigin();
           
      
        }

        public void RemoveBullet()
        {
            bullets.Remove(Bullet);
        }
        
 

   
    
        public void AddBullet()
        {
            //Bullet newBullet = new Bullet()
            var bullet = Bullet.Clone() as Bullet;
            bullet.IsActive = true;
            bullet.Position = _position;  
            bullet.LifeSpan = 0f;
            bullet.Parent = this;
            bullets.Add(bullet);
        
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

        public void SetPrice(int towerPrice)
        {
            this.towerPrice = towerPrice;
        }
        public int GetPrice()
        {
            return towerPrice;
        }

     /*   public void Settimer(float timer)
        {
            this.timer = timer;
        }

        public float Gettimer()
        {
            return this.timer;
        }
*/
        public void SetTIMER(float TIMER)
        {
            this.TIMER = TIMER;
        }

        public float GetTIMER()
        {
            return this.TIMER;
        }

        public void SetBullets(List<Sprite> bullets)
        {
            this.bullets = bullets;
        }

        public List<Sprite> GetBullets()
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

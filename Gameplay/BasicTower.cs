using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class BasicTower : Sprite
    {
        public Bullet Bullet;
        public float timer = 3;         //Initialize a 10 second timer
        public const float TIMER = 3;
        public int towerPrice;
        public BasicTower(Texture2D texture,int towerPrice)
          : base(texture)
        {
            
            Bullet = new Bullet(_texture);
            this.towerPrice = towerPrice;
            xVelocity += Bullet.xVelocity;
            yVelocity += Bullet.yVelocity;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

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
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet._position = this._position;
            bullet.xVelocity = xVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }


        public void DrawBasicTower(SpriteBatch spriteBatch)

        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}

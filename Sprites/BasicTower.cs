
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tutorial006.Sprites
{
    public class BasicTower : Sprite
    {
        public Bullet Bullet;
        public float timer = 3;         //Initialize a 10 second timer
        public const float TIMER = 3;
        public BasicTower(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

           

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));


            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                //Timer expired, execute action
                AddBullet(sprites);

                timer = TIMER;   //Reset Timer
            }
            

            /*  if (_currentKey.IsKeyDown(Keys.Space) &&
                  _previousKey.IsKeyUp(Keys.Space))
              {
                  AddBullet(sprites);
              }*/


        }

        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }
    }
}
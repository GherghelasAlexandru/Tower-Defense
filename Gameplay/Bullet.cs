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
      
        protected int dmg;
        public Bullet(Texture2D texture)
          : base(texture)
        {
            this.LifeSpan = 10f;
        }
        
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
           

        }

        public  void FollowEnemy(Enemy enemy)
        {
            var distance = enemy.Position - Position;
            SetRotation((float)Math.Atan2(distance.Y, distance.X));
            SetDirection(new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation)));
            var currentDistance = Vector2.Distance(Position, enemy.InteractionBox.Location.ToVector2());

            var t = MathHelper.Min((float)Math.Abs(currentDistance), xVelocity);
            var velocity = GetDirection() * t;

            SetPosition(GetPosition() + velocity);
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

/*        public void SetBulletIsDead(bool bulletIsDead)
        {
            this.bulletIsDead = bulletIsDead;
        }

        public bool GetBulletIsDead()
        {
            return this.bulletIsDead;
        }*/
    }
}

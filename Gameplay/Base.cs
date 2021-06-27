using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class Base:AnimationManager
    {
        public int health;
       

        public Base(Animation animation):base(animation)
        {
            _animation = animation;
            _animationManager = new AnimationManager(animation);
            health = 100;
            Position = new Vector2(30, 780);
            _animationManager.Scale = 2.2f;
  
        }

        public override void Update(GameTime gameTime)
        {
            
            SetAnimations();
            
        }

        protected override void SetAnimations()
        {
            
                _animationManager.Play(_animation);

            if (GetBaseHealth() <= 100 && GetBaseHealth() >= 88)
            {
                _animationManager.GetAnimation().CurrentFrame = 0;
            }
            else if (GetBaseHealth() < 88 && GetBaseHealth() >= 76)
            {
                _animationManager.GetAnimation().CurrentFrame = 1;
            }
            else if (GetBaseHealth() < 76 && GetBaseHealth() >= 64)
            {
                _animationManager.GetAnimation().CurrentFrame = 2;
            }
            else if (GetBaseHealth() < 64 && GetBaseHealth() >= 52)
            {
                _animationManager.GetAnimation().CurrentFrame = 3;
            }
            else if (GetBaseHealth() < 52 && GetBaseHealth() >= 40)
            {
                _animationManager.GetAnimation().CurrentFrame = 4;
            }
            else if (GetBaseHealth() < 40 && GetBaseHealth() >= 28)
            {
                _animationManager.GetAnimation().CurrentFrame = 5;
            }
            else if (GetBaseHealth() < 28 && GetBaseHealth() > 16)
            {
                _animationManager.GetAnimation().CurrentFrame = 6;
            }
            else if (GetBaseHealth() < 16 && GetBaseHealth() > 0)
            {
                _animationManager.GetAnimation().CurrentFrame = 7;
            }
            else if (GetBaseHealth() <= 0)
            {
                _animationManager.GetAnimation().CurrentFrame = 8;


            }
            _animationManager.UpdateBoundingBox();
        }


        public override void Draw(SpriteBatch spriteBatch,SpriteEffects spriteEffects)
        {
            if (_animationManager != null)
            {
                _animationManager.Draw(spriteBatch,SpriteEffects.None);
            }
            
        }
        public void SetBaseHealth(int health)
        {
            this.health = health;
        }

        public int GetBaseHealth()
        {
            return this.health;
        }
    }
}

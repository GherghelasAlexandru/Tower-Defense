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
            health = 120;
            Position = new Vector2(90, 90);
            _animationManager.Scale = 1.5f;
        }

        public override void Update(GameTime gameTime)
        {
            switch(health)
            {

                case 90:
                    _animationManager._animation.CurrentFrame = 1;
                    break;
                case 75:
                    _animationManager._animation.CurrentFrame = 2;
                    break;
                case 60:
                    _animationManager._animation.CurrentFrame = 3;
                    break;
                case 45:
                    _animationManager._animation.CurrentFrame = 4;
                    break;
                case 30:
                    _animationManager._animation.CurrentFrame = 5;
                    break;
                case 15:
                    _animationManager._animation.CurrentFrame = 6;
                    break;
                case 0:
                    _animationManager._animation.CurrentFrame = 7;
                    break;

                default:
                    _animationManager._animation.CurrentFrame = 0;
                    break;


            }



        }

        protected override void SetAnimations()
        {
            
                _animationManager.Play(_animation);
            
            
                _animationManager.UpdateBoundingBox();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_animationManager != null)
            {
                _animationManager.Draw(spriteBatch);
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

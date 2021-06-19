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
            this.health = 120;
            Position = new Vector2(200, 200);
            _animationManager.Scale = 2;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            SetAnimations();
            _animationManager.Update(gameTime);
            base.Update(gameTime, sprites);
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

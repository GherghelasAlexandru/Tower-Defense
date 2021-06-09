﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Engine
{
    public class AnimationManager:Sprite
    {
        protected Animation _animation;

        private float _timer;



        public AnimationManager(Dictionary<string, Animation> animations):base(animations.ElementAt(0).Value.Texture)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
          
            spriteBatch.Draw(_animation.Texture,
                            _position,
                             new Rectangle(_animation.CurrentFrame * _animation.FrameWidth,
                                           0,
                                           _animation.FrameWidth,
                                           _animation.FrameHeight),
                             Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);

        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
         
        }

        public override void UpdateBoundingBox()
        {
            int width = 0;
            int height = 0;

            if (_animation != null)
            {
                width = _animation.FrameWidth;
                height = _animation.FrameHeight;
            }

            BoundingBox = new Rectangle(
                (int)(_position.X - (int)Math.Ceiling(Origin.X)),
                (int)(_position.Y - (int)Math.Ceiling(Origin.Y)),
                width, height);
        }
    }
}

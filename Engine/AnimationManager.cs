using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Engine
{
    public class AnimationManager : Sprite
    {
        public Animation _animation;




        public AnimationManager(Dictionary<string, Animation> animations) : base(animations.ElementAt(0).Value.Texture)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            int row = _animation.CurrentFrame / _animation.Columns;
            int column = _animation.CurrentFrame % _animation.Columns;
            spriteBatch.Draw(_animation.Texture,
                            Position,
                             new Rectangle(_animation.FrameWidth * column,
                                           _animation.FrameHeight * row,
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



            if (_timer > _animation.FrameSpeed && _animation.IsLooping == true)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                {
                    _animation.CurrentFrame = 0;

                }
            }
            if (_timer > _animation.FrameSpeed && _animation.IsLooping == false && _animation.IsDone == false)
            {
                _timer = 0f;
                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                {
                    _animation.IsDone = true;
                    _animation.CurrentFrame = _animation.FrameCount;
                }
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
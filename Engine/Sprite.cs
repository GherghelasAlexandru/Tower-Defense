using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelDefense.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelDefense.Gameplay
{
    public class Sprite : ICloneable
    {

        public Texture2D _texture;

        protected float _rotation;

        protected Vector2 _position;

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public Vector2 Origin;

        public Vector2 Direction;

        public float yVelocity;

        public float xVelocity;

        public Sprite Parent;

        public float LifeSpan = 0f;

        public bool IsRemoved = false;
        public Rectangle BoundingBox { get; set; }

  /*      public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)(Position.X - (int)Math.Ceiling(Origin.X)),
                    (int)(Position.Y - (int)Math.Ceiling(Origin.Y)),
                    (int)Math.Ceiling((double)_texture.Width),
                    (int)Math.Ceiling((double)_texture.Height)
                    );
            }
        }*/

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            // The default origin in the centre of the sprite
            Origin = new Vector2(0, 0);
            UpdateBoundingBox();
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            UpdateBoundingBox();
        }
     

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            
        }

        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
                spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
 
        }

        public virtual void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle(
                (int)(Position.X - (int)Math.Ceiling(Origin.X)),
                (int)(Position.Y - (int)Math.Ceiling(Origin.Y)),
                (int)Math.Ceiling((double)_texture.Width),
                (int)Math.Ceiling((double)_texture.Height)
                );
        }

        

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
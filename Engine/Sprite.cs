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
        public float _timer;

        public bool firing = false;

        public Texture2D _texture;

        public float _rotation;

        public float radius;

        public Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
                UpdateBoundingBox();
            }
        }

        public float rotationVelocity = 3f;

        public float LinearVelocity = 4f;

        public Vector2 _movement;

        public Vector2 _destination;

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;
        public bool IsPlaced = false;

        public bool dragging = true;

        public Vector2 Origin;

        public Vector2 Direction;

        public float yVelocity;

        public float xVelocity;

        public Sprite Parent;

        public float LifeSpan = 0f;

        public bool IsActive = false;
        public Rectangle BoundingBox { get; protected set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)(_position.X - (int)Math.Ceiling(Origin.X)),
                    (int)(_position.Y - (int)Math.Ceiling(Origin.Y)),
                    (int)Math.Ceiling((double)_texture.Width),
                    (int)Math.Ceiling((double)_texture.Height)
                    );
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            // The default origin in the centre of the sprite
            Origin = new Vector2(0, 0);
            IsActive = true;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            
        }

        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
 
        }

        public virtual void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle(
                (int)(_position.X - (int)Math.Ceiling(Origin.X)),
                (int)(_position.Y - (int)Math.Ceiling(Origin.Y)),
                (int)Math.Ceiling((double)_texture.Width),
                (int)Math.Ceiling((double)_texture.Height)
                );
        }


        public void CenterOrigin()
        {
            Origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);

            UpdateBoundingBox();
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
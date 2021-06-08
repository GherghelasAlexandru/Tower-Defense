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

        public Vector2 _position;

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

        public float rotationVelocity;

        public Vector2 _movement;

        public Vector2 _destination;

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        public bool dragging = true;

        public Vector2 Origin;

        public Vector2 Direction;

        public float yVelocity;

        public float xVelocity;

        public Sprite Parent;

        public Vector2 Velocity;

        public float LifeSpan = 0f;

        public bool IsActive = false;
        public Rectangle BoundingBox { get; set; }


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
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
            
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
           
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

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
    }
}
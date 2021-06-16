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
        public float _rotation;
        public float radius;
        public float yVelocity;
        public float xVelocity;
        public float LifeSpan = 0f;
  

        public Vector2 Origin;
        public Vector2 Direction;
        public Vector2 _position;
        public Vector2 _movement;
        public Vector2 _destination;

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
        

        public Texture2D _texture;
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;

        public bool IsPlaced = false;
        public bool IsActive = false;
        public bool dragging = true;
        public bool firing = false;


        public Sprite Parent;
 
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
            this._texture = texture;

            // The default origin in the centre of the sprite
            this.Origin = new Vector2(0, 0);
            this.IsActive = true;
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


        // get and set methods

        public void SetIsActive(bool isActive)
        {
            this.IsActive = isActive;
        }

        public bool GetIsActive()
        {
            return this.IsActive;
        }

        public void SetLifeSpan(float lifeSpan)
        {
            this.LifeSpan = lifeSpan;
        }

        public float GetLifeSpan()
        {
            return this.LifeSpan;
        }

        public void SetXvelocity(float velocity)
        {
            this.xVelocity = velocity;
        }

        public float GetXvelocity()
        {
            return this.xVelocity;
        }

        public void SetYvelocity(float velocity)
        {
            this.yVelocity = velocity;
        }

        public float GetYvelocity()
        {
            return this.yVelocity;
        }

        public void SetOrigin(Vector2 origin)
        {
            this.Origin = origin;
        }

        public Vector2 GetOrigin()
        {
            return this.Origin;
        }

        public void SetDirection(Vector2 direction)
        {
            this.Direction = direction;
        }

        public Vector2 GetDirection()
        {
            return this.Direction;
        }

        public void SetIsDragged(bool isdrag)
        {
            this.dragging = isdrag;
        }

        public bool GetDragged()
        {
            return this.dragging;
        }

        public void SetIsPlaced(bool isplaced)
        {
            this.IsPlaced = isplaced;
        }

        public bool GetIsPlaced()
        {
            return this.IsPlaced;
        }

        public void SetAnimation(Dictionary<string, Animation> _animations)
        {
            this._animations = _animations;
        }

        public Dictionary<string, Animation> GetAnimation()
        {
            return this._animations;
        }

        public void SetAnimationManager(AnimationManager animation)
        {
            this._animationManager = animation;
        }

        public AnimationManager GetAnimationManager()
        {
            return this._animationManager;
        }

        public void SetMovement(Vector2 movement)
        {
            this._movement = movement;
        }

        public Vector2 GetMovement()
        {
            return this._movement;
        }

        public void SetDestination(Vector2 destiantion)
        {
            this._destination = destiantion;
        }

        public Vector2 GetDestination()
        {
            return this._destination;
        }

        public void SetPosition(Vector2 position)
        {
            this._position = position;
        }

        public Vector2 GetPosition()
        {
            return this._position;
        }

        public void SetRotation(float rotation)
        {
            this._rotation = rotation;
        }

        public float GetRotation()
        {
            return this._rotation;
        }


        public void SetRadius(float radius)
        {
            this.radius = radius;
        }

        public float GetRadius()
        {
            return this.radius;
        }

        public void SetIsFiring(bool isfiring)
        {
            this.firing = isfiring;
        }

        public bool GetFiring()
        {
            return this.firing;       
        }
        public void SetTexture(Texture2D texture)
        {
            this._texture = texture;
        }

        public Texture2D GetTexture()
        {
            return this._texture;
        }

    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelDefense.Engine;
using System;
using System.Collections.Generic;

namespace PixelDefense.Gameplay
{
    public class Sprite : ICloneable
    {
        protected Texture2D _texture;

        protected float _rotation;

        //protected KeyboardState _currentKey;

        //protected KeyboardState _previousKey;

        public Vector2 Position;

        public Vector2 Origin;

        public Vector2 Direction;

        public float RotationVelocity;

        public float LinearVelocity;

        public Sprite Parent;

        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            // The default origin in the centre of the sprite
            Origin = new Vector2(0, 0);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public abstract class BasicTower : Sprite
    {
        public Bullet Bullet;
        public float timer = 3;         //Initialize a 10 second timer
        public const float TIMER = 3;
        public int towerPrice;
        public BasicTower(Texture2D texture)
          : base(texture)
        {
            
            Bullet = new Bullet(_texture);
            
            xVelocity += Bullet.xVelocity;
            yVelocity += Bullet.yVelocity;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                //Timer expired, execute action
                AddBullet(sprites);

                timer = TIMER;   //Reset Timer
            }

            DragTower();

        }

        public void DragTower()
        {
            UpdateBoundingBox();

            MouseState mouseState = Mouse.GetState();

            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            if (BoundingBox.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    dragging = true;

                    if (dragging)
                    {
                        _position.X = mouseState.X;
                        _position.Y = mouseState.Y;

                    }
                }

            }
        }

        public int GetPrice()
        {
            return towerPrice;
        }

        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet._position = this._position;
            bullet.xVelocity = xVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }


        public void DrawBasicTower(SpriteBatch spriteBatch)

        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}

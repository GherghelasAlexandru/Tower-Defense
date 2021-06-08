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
        public float timer;         //Initialize a 10 second timer
        public float TIMER;
        public int towerPrice;
        
        public MouseState mouseState;

        public Rectangle TowerCollision;

        private List<Bullet> bullets;
            
       

        public BasicTower(Texture2D texture)
          : base(texture)
        {

           bullets = new List<Bullet>();


        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            CenterOrigin();
           
            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                //Timer expired, execute action
                AddBullet(sprites);

                timer = TIMER;   //Reset Timer
            }

            PlaceTower();
        }

        public void PlaceTower()
        {
            TowerCollision = new Rectangle(
                    (int)(_position.X - (int)Math.Ceiling(Origin.X)),
                    (int)(_position.Y - (int)Math.Ceiling(Origin.Y)),
                    (int)Math.Ceiling((double)_texture.Width),
                    (int)Math.Ceiling((double)_texture.Height)
                    );

            mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            {
                if (mouseState.LeftButton == ButtonState.Released && dragging)
                {
                    _position.X = mouseState.X;
                    _position.Y = mouseState.Y;  
                }
                else if (mouseState.LeftButton == ButtonState.Pressed  && BoundingBox.Contains(mousePosition) && dragging)
                {
                        dragging = false;
                        _position.X = mouseState.X;
                        _position.Y = mouseState.Y;

                    /*if (TowerCollision.Intersects(BoundingBox))
                    {
                        Console.WriteLine("NOOO!");
                    }*/
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
            bullets.Add(bullet);
        }

        public List<Bullet> getBullets() {
            return bullets;
        }

        public void DrawBasicTower(SpriteBatch spriteBatch)

        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{


    public class Enemy : Animation
    {

        int health;
        bool isDead = false;
        float mSpeed = 1f;
        int Width = 8;
        int Height = 13;
        public enum ECollisionSide { LEFT, RIGHT, TOP, BOTTOM }
        public enum EAnimState { RUN, ATTACK, TAKE_HIT, DEATH, NONE }

        public Enemy(Texture2D texture,int frameCount, int health):base(texture,frameCount)
        {
            this.health = health;
  
        }



        public void SpawnEnemy()
        {


        }

        public override void Update(GameTime gameTime,List<Sprite>sprites)
        {
            xVelocity = 0;
            yVelocity = 0;

            float velocityValue = mSpeed * 60 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            /*if (isDead == false)
            {
                yVelocity -= velocityValue;
                AnimState = EAnimState.RUN;
            }
            else if (Input.GetKey(Keys.S))
            {
                yVelocity += velocityValue;
                AnimState = EAnimState.ATTACK;
            }

            if (Input.GetKey(Keys.A))
            {
                health -= 1;
                AnimState = EAnimState.TAKE_HIT;
            }
            else if (health == 0)
            {
                isDead = true;
                xVelocity += velocityValue;
                AnimState = EAnimState.DEATH;
            }

            if (xVelocity == 0 && xVelocity == 0)
            {
                StopAnim();
                CurrentAnimation.CurrentFrame = 0;
            }*/

            base.Update(gameTime,sprites);
        }


        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            var destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            spriteBatch.Draw(_texture, destinationRectangle, Color.White);

        }

    }
}

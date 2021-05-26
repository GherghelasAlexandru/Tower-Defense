using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{


    class Enemy: AnimatedSprite
    {
        Texture2D enemyTexture;
        int health;
        bool isDead = false;
        float mSpeed = 1f;
        int Width =8;
        int Height = 13;
        public enum ECollisionSide { LEFT, RIGHT, TOP, BOTTOM }
        public enum EAnimState { RUN, ATTACK, TAKE_HIT, DEATH, NONE }

        public Enemy(ContentManager content, int health)
        {
            this.health = health;

            enemyTexture = content.Load<Texture2D>("spritesheets/Run");
            enemyTexture = content.Load<Texture2D>("spritesheets/Attack");
            enemyTexture = content.Load<Texture2D>("spritesheets/Take_Hit");
            enemyTexture = content.Load<Texture2D>("spritesheets/Death");

        }

        public EAnimState AnimState
        {
            get
            {
                switch (CurrentKeyAnim)
                {
                    case "Run":
                        return EAnimState.RUN;
                    case "Attack":
                        return EAnimState.ATTACK;
                    case "Take_Hit":
                        return EAnimState.TAKE_HIT;
                    case "Death":
                        return EAnimState.DEATH;
                    default:
                        return EAnimState.NONE;
                }
            }
            set
            {
                switch (value)
                {
                    case EAnimState.RUN:
                        SetAnimation("Run");
                        break;
                    case EAnimState.ATTACK:
                        SetAnimation("Attack");
                        break;
                    case EAnimState.TAKE_HIT:
                        SetAnimation("Take_Hit");
                        break;
                    case EAnimState.DEATH:
                        SetAnimation("Death");
                        break;
                    default:
                        break;
                }
            }
        }

       

        public void SpawnEnemy()
        {


        }

        public override void Update(GameTime gameTime)
        {
            vx = 0;
            vy = 0;

            float velocityValue = mSpeed * 60 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isDead == false)
            {
                vy -= velocityValue;
                AnimState = EAnimState.RUN;
            }
            /*else if (Input.GetKey(Keys.S))
            {
                vy += velocityValue;
                AnimState = EAnimState.ATTACK;
            }

            if (Input.GetKey(Keys.A))
            {
                health -= 1;
                AnimState = EAnimState.TAKE_HIT;
            }*/
            else if (health == 0)
            {
                isDead = true;
                vx += velocityValue;
                AnimState = EAnimState.DEATH;
            }

            if (vx == 0 && vy == 0)
            {
                StopAnim();
                CurrentAnimation.CurrentFrame = 0;
            }

            base.Update(gameTime);
        }


        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            var destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            spriteBatch.Draw(enemyTexture, destinationRectangle, Color.White);
            
        }
       
    }
}

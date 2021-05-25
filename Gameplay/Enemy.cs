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

        float mSpeed = 1f;
        public enum ECollisionSide { LEFT, RIGHT, TOP, BOTTOM }
        public enum EAnimState { RUN, ATTACK, TAKE_HIT, DEATH, NONE }
        public Enemy(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Run");
            Dictionary<string, Animation> dicAnim = new Dictionary<string, Animation>
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheet/Run"), 8)},
                {"Attack",new Animation(content.Load<Texture2D>("spritesheet/Attack"),8 )},
                {"Take_Hit",new Animation(content.Load<Texture2D>("spritesheet/Take_Hit"),8 )},
                {"Death",new Animation(content.Load<Texture2D>("spritesheet/Death"),8 )}
            };
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
        /*public void DrawEnemy(SpriteBatch spritebatch)
        {
            spritebatch.Draw(enemyTexture, Position, new Rectangle);
        }*/
    }
}

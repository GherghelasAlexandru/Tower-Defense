﻿using Microsoft.Xna.Framework;
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


    public abstract class Enemy:AnimationManager
    {

        public int health;
        public bool isDead = false;
        public float mSpeed = 1f;
        int Width = 8;
        int Height = 13;


        public Queue<Vector2> path;


        public Enemy(Dictionary<string,Animation>animations) : base(animations)

        {
            
            _animations = animations;
            _animationManager = new AnimationManager(animations);

           
        }


        
           
        

        //box for enemy to interact with surroundings
        public Rectangle InteractionBox
        {
            get
            {
                // interactionBox need to be bigger than bounding box
                Rectangle interactionBox = BoundingBox;

                interactionBox.X -= 4;
                interactionBox.Y -= 4;
                interactionBox.Width += 8;
                interactionBox.Height += 8;

                return interactionBox;
            }
        }

        

        public void SpawnEnemy(Vector2 pos,Queue<Vector2> p)
        {

         
            _position = pos;
            IsActive = true;
            _movement = new Vector2(0, 0);
            path = p;

        }


    

        public override void Update(GameTime gameTime,List<Sprite>sprites)
        {


            if (IsActive)
            {
                Vector2 difference = (_destination - _position);
                if (difference.X > -1 && difference.X < 1 && difference.Y > -1 && difference.Y < 1)
                {
                    path.Dequeue();
                    if (path.Count == 0)
                        IsActive = false;
                    else
                        IsActive = true;
                    Console.WriteLine(path);
                }

                _position += _movement;
            }


            SetAnimations();
            _animationManager.Update(gameTime);
            base.Update(gameTime,sprites);
        }


        protected virtual void SetAnimations()
        {

            
            if (xVelocity > 0)
                _animationManager.Play(_animations["Run"]);
            else if (xVelocity == 0)
            {
                _animationManager.Play(_animations["Idle"]);
            }
            else _animationManager.Stop();
            _animationManager.UpdateBoundingBox();

          
        }

        public override void UpdateBoundingBox()
        {
            // The collision is at the feet of the enemy
            BoundingBox = new Rectangle(
            (int)(_position.X + 5),
            (int)(_position.Y + 21),
            10,
            10
            );
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_animationManager != null)
            {
                _animationManager.Draw(spriteBatch);
            }
            else throw new Exception("wait a second, who are you?!");
            // runAnimation.DrawAnimation(spriteBatch);
        }

    }
}

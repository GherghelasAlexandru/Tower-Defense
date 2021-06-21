using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PixelDefense.Gameplay
{


    public abstract class Enemy:AnimationManager
    {

        public int health;
        public bool isDead = false;
        public float mSpeed = 1f;

        public int goldDrop;
        public Queue<Vector2> path;
        public bool active;
        public int damage;
        public float timer;



        public Enemy(Dictionary<string,Animation>animations) : base(animations)

        {
            this.LifeSpan = 2f;
            this._animations = animations;
            this._animationManager = new AnimationManager(animations);
            this.active = false;
            this.path = new Queue<Vector2>();
            
          
        }    
        

        //box for enemy to interact with surroundings
        public Rectangle InteractionBox
        {
            get
            {
                // interactionBox need to be bigger than bounding box
                Rectangle interactionBox = _animationManager.BoundingBox;

                interactionBox.X += 0;
                interactionBox.Y += 5;
                interactionBox.Width -= 20;
                interactionBox.Height -= 15;

                return interactionBox;
            }
        }

        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                if (path.Count() > 0)
                
                    _destination = path.FirstOrDefault<Vector2>();
                Vector2 difference = _destination - Position;
                _movement = difference / Vector2.Distance(_destination, Position);

            }
        }

        public void SpawnEnemy(Vector2 pos,Queue<Vector2> p)
        {

           
            Position = pos;
            //IsActive = true;
      
            path = p;
            Active = true;
            

        }


    

        public override void Update(GameTime gameTime,List<Sprite>sprites)
        {
            
            

            if(isDead)
            {
                _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                
            }
            if(_timer < 0)
                {
                    _timer = 0f;
                    IsActive = false;
                }

            if (Active)
            {
                Vector2 difference = _destination - Position;
                if (difference.X > -1 && difference.X < 1 && difference.Y > -1 && difference.Y < 1)
                {
                    //Console.WriteLine(path.Peek());
                    if (path.Count > 0)
                    {
                        path.Dequeue();
                    }
                    if (path.Count == 0)
                    {
                        // active need to be changed, here must be implemented the atack logic
                        xVelocity = 0;
                       

                    }
                    else if (health <= 0)
                    {
                        IsActive = false;
                    }
                    else
                    {
                        Active = true;
                    }

                }

                Position += _movement * xVelocity;

            }


            SetAnimations();
            _animationManager.Update(gameTime);
            base.Update(gameTime,sprites);
        }

        public void AttackBase(Base MainBase, GameTime GameTime)
        {

            timer += (float)GameTime.ElapsedGameTime.TotalSeconds;
            if (GetIsActive())

                    if (GetPath().Count == 0)
                    {
                    // pus timer? 
                    // Timer increment
                    Console.WriteLine(MainBase.health);
                    //Console.WriteLine(timer);

                    if (timer > 1)
                    {
                        
                        MainBase.SetBaseHealth(MainBase.GetBaseHealth() - damage);
                        timer = 0;
                    }

                }
           
        }

        protected override void SetAnimations()
        {



            if (health > 0 && path.Count > 0)
            {
                _animationManager.Play(_animations["Run"]);
            }
            else if (GetPath().Count == 0)
            {
                _animationManager.Play(_animations["Attack"]);
            }
            else if (isDead)
            {

                _animationManager.Play(_animations["Death"]);
               
                _animationManager._animation.IsLooping = false;
                
            }

            else _animationManager.Stop();

            _animationManager.UpdateBoundingBox();
        }
          
        

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            
            if (_animationManager != null && IsActive)
            {
                _animationManager.Draw(spriteBatch, spriteEffects);
            }
     
            // runAnimation.DrawAnimation(spriteBatch);
        }

        // get and set

        public void SetHealth(int health)
        {
            this.health = health;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public void SetSpeed(float speed)
        {
            this.mSpeed = speed;
        }

        public float GetSpeed()
        {
            return this.mSpeed;
        }

        public void SetVelocity(float velocity)
        {
            this.xVelocity = velocity;
        }

        public float GetVelocity()
        {
            return this.xVelocity;
        }

        public void SetGoldDrop(int gold)
        {
            this.goldDrop = gold;
        }

        public int GetGoldDrop()
        {
            return this.goldDrop;
        }

        public void SetTimer(float timer)
        {
            this._timer = timer;
        }

        public float GetTimer()
        {
            return this._timer;
        }

        /*public void SetPosition(Vector2 position)
        {
            this.Position = position;
        }

        public Vector2 GetPosition()
        {
            return this.Position;
        }*/

        public void SetIsDead(bool isdead)
        {
            this.isDead = isdead;
        }

        public bool GetIsDead()
        {

            return this.isDead;

        }






        public void SetPath(Queue<Vector2> path)
        {
            this.path = path;
        }

        public Queue<Vector2> GetPath()
        {
            return this.path;
        }



    }
}

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


    public abstract class Enemy:AnimationManager
    {

        public int health;
        public bool isDead = false;
        public float mSpeed = 1f;

        public int goldDrop;
        public Queue<Vector2> path;
        public bool active;



        public Enemy(Dictionary<string,Animation>animations) : base(animations)

        {
            LifeSpan = 2f;
            _animations = animations;
            _animationManager = new AnimationManager(animations);
            active = false;
            path = new Queue<Vector2>();

          
        }

        public int getHealth()
        {
            return health;
        }

        public void setHealth( int health)
        {
            this.health = health;
        }


        
           
        

        //box for enemy to interact with surroundings
        public Rectangle InteractionBox
        {
            get
            {
                // interactionBox need to be bigger than bounding box
                Rectangle interactionBox = _animationManager.BoundingBox;

                interactionBox.X += 45;
                interactionBox.Y += 40;
                interactionBox.Width -= 55;
                interactionBox.Height -= 60;

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
                 
                    path.Dequeue();
                    if (path.Count == 0)
                    {
                        // active need to be changed, here must be implemented the atack logic
                        Active = false;
                        xVelocity = 0;
                    }

                    else
                        Active = true;

                }

                Position += _movement;

            }


            SetAnimations();
            _animationManager.Update(gameTime);
            base.Update(gameTime,sprites);
        }


        protected virtual void SetAnimations()
        {



            if (health > 0)
            {
                _animationManager.Play(_animations["Run"]);
            }
            else if (xVelocity == 0)
            {
                _animationManager.Play(_animations["Attack"]);
            }

            else if (health <= 0)
            {

                _animationManager.Play(_animations["Death"]);

                _animationManager._animation.IsLooping = false;
                isDead = true;
            }

            else _animationManager.Stop();

            _animationManager.UpdateBoundingBox();
        }
          
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            if (_animationManager != null && IsActive)
            {
                _animationManager.Draw(spriteBatch);
            }
     
            // runAnimation.DrawAnimation(spriteBatch);
        }

    }
}

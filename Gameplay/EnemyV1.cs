using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
   public class EnemyV1
    {
        public Texture2D texture;
        Queue<Vector2> path = new Queue<Vector2>();

       public Vector2 position;
       public Vector2 movement;
       public Vector2 destination;

        bool active = false;

        public EnemyV1()
        {

        }

        public void Initialize(Texture2D text, Vector2 pos, Queue<Vector2> p)
        {
            texture = text;
            position = pos;
            movement = new Vector2(0, 0);
            path = p;
            Active = true;
        }

        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                if (path.Count() > 0)
                    destination = path.FirstOrDefault<Vector2>();
                Vector2 difference = (destination - position);
                movement = difference / Vector2.Distance(destination, position);
            }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                Vector2 difference = (destination - position);
                if (difference.X > -1 && difference.X < 1 && difference.Y > -1 && difference.Y < 1)
                {
                    path.Dequeue();
                    if (path.Count == 0)
                        Active = false;
                    else
                        Active = true;
                }

                position += movement;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
                spriteBatch.Draw(texture, position);
        }

    }
}

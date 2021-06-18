using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class Base
    {
        protected int health;

        public Base(int health) 
        {
            this.health = health;
        }

        public void SetBaseHealth(int health)
        {
            this.health = health;
        }

        public int GetBaseHealth()
        {
            return this.health;
        }
    }
}

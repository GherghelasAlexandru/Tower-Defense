using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    class Slime : Enemy
    {

        public Slime(Dictionary<string, Animation> animations) : base(animations)
        {

            health = 3;
            mSpeed = 1f;
            xVelocity = 1f;
            goldDrop = 10;
        }
    }
}

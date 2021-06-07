using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class Rat:Enemy
    {

        public Rat(Dictionary<string, Animation> animations) : base(animations)
        {

            health = 3;
            mSpeed = 1f;
            xVelocity = 1f;

        }
    }
}

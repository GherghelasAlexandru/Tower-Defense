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

            this.health = 3;
            this.mSpeed = 1f;
            this.xVelocity = 1f;
            this.goldDrop = 10;
            this._timer = 1;
        }

    }
}

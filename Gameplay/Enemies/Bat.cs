using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    class Bat:Enemy
    {

        public Bat(Dictionary<string, Animation> animations) : base(animations)
        {

            this.health = 3;
            this.mSpeed = 1f;
            this.xVelocity = 2f;
            this.goldDrop = 19;
            this._timer = 1;
            this.damage = 1;
        }
    }
}

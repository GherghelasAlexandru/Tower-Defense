using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class Crab : Enemy
    {

        public Crab(Dictionary<string, Animation> animations) : base(animations)
        {

            this.health = 3;
            this.mSpeed = 1f;
            this.xVelocity = 2f;
            this.goldDrop = 5;
            this._timer = 1;
        }



    }
}

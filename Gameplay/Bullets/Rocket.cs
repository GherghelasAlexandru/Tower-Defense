using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    class Rocket : Bullet
    {
        public Rocket(Texture2D texture) : base(texture)
        {
            this.dmg = 3;
            this.xVelocity = 1f;
            this.yVelocity = 1f;
        }
    }
}

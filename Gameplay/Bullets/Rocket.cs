using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    public class Rocket : Bullet
    {
        public Rocket(Texture2D texture) : base(texture)
        {
            dmg = 3;
            xVelocity = 1f;
            yVelocity = 1f;
        }
    }
}

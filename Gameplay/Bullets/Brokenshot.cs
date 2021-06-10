using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    class Brokenshot: Bullet
    {
        public Brokenshot(Texture2D texture):base(texture)
        {
            dmg = 1;
            xVelocity = 5f;
            yVelocity = 5f;

        }
        
    }
}

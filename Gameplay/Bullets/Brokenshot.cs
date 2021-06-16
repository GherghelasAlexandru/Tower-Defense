using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    public class Brokenshot: Bullet
    {
        public Brokenshot(Texture2D texture):base(texture)
        {
            this.dmg = 1;
            this.xVelocity = 5f;
            this.yVelocity = 5f;

        }
        
    }
}

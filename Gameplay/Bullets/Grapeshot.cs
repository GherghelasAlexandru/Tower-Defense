using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    public class Grapeshot : Bullet
    {
        public Grapeshot(Texture2D texture) : base(texture)
        {
            this.dmg = 1;
            this.xVelocity = 3f;
            this.yVelocity = 3f;
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    public class The556 : Bullet
    {
        public The556(Texture2D texture) : base(texture)
        {
            this.dmg = 2;
            this.xVelocity = 10f;
            this.yVelocity = 10f;
        }
    }
}

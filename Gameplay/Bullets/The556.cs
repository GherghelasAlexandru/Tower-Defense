using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    class The556 : Bullet
    {
        public The556(Texture2D texture) : base(texture)
        {
            dmg = 3;
            xVelocity = 10f;
            yVelocity = 10f;
        }
    }
}

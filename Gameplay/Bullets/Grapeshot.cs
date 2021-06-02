using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Bullets
{
    class Grapeshot : Bullet
    {
        public Grapeshot(Texture2D texture) : base(texture)
        {
            
            xVelocity = 4f;
            yVelocity = 4f;
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay.Bullets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Towers
{
    class BrokenGun:BasicTower
    {
        public BrokenGun(Texture2D texture) : base(texture)
        {
            Bullet = new Brokenshot(_texture);

            towerPrice = 45;
        }
    }
}

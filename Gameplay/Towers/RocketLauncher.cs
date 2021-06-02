using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay.Bullets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Towers
{
    class RocketLauncher : BasicTower
    {
        public RocketLauncher(Texture2D texture) : base(texture)
        {
            Bullet = new Rocket(_texture);
            towerPrice = 30;
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Towers
{
    class DoubleCannon : BasicTower
    {
        public DoubleCannon(Texture2D texture):base(texture)
        {
            towerPrice = 25;
        }
    }
}

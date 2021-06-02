using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay.Bullets;
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
            Bullet = new Grapeshot(_texture);
            timer = 3;
            TIMER = 3;
            xVelocity += Bullet.xVelocity;
            yVelocity += Bullet.yVelocity;
            towerPrice = 25;
        }
    }
}

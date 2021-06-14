using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay.Bullets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay.Towers
{
    class MachineGun : BasicTower
    {
        public MachineGun(Texture2D texture) : base(texture)
        {
            timer = 2;
            TIMER = 1;
            Bullet = new The556(_texture);
            xVelocity += Bullet.xVelocity;
            yVelocity += Bullet.yVelocity;
            towerPrice = 20;
            radius = 200;

        }
    }
}

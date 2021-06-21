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
            this._timer = 2;
            this.TIMER = 1;
            this.Bullet = new The556(_texture);
            this.xVelocity += Bullet.xVelocity;
            this.yVelocity += Bullet.yVelocity;
            this.towerPrice = 100;
            this.radius = 200;

        }
    }
}

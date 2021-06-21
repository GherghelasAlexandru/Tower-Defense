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
            this.Bullet = new Brokenshot(_texture);
            this._timer = 3;
            this.TIMER = 3/2;
            this.xVelocity += Bullet.xVelocity;
            this.yVelocity += Bullet.yVelocity;
            this.towerPrice = 450;
            this.radius = 250;
        }
    }
}

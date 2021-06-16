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
            this.timer = 4;
            this.TIMER = 4;
            this.Bullet = new Rocket(_texture);
            this.xVelocity += Bullet.xVelocity;
            this.yVelocity += Bullet.yVelocity;
            this.towerPrice = 30;
            this.radius = 300;
        }
    }
}

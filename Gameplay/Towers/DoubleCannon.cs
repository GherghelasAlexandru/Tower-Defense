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
            this.Bullet = new Grapeshot(_texture);
            this._timer = 3;
            this.TIMER = 3;
            this.xVelocity += Bullet.xVelocity;
            this.yVelocity += Bullet.yVelocity;
            this.towerPrice = 150;
            this.radius = 300;
        }
    }
}

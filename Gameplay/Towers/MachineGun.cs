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
            Bullet = new The556(_texture);
            towerPrice = 20;
           
            
        }
    }
}

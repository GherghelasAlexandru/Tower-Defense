﻿using Microsoft.Xna.Framework.Graphics;
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
            towerPrice = 20;
           
            
        }
    }
}

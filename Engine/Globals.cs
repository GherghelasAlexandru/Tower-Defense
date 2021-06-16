using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PixelDefense.States;

namespace PixelDefense.Engine
{
    public delegate void PassObject(object i);
    class Globals
    {
        public static ContentManager content;
        public static string appDataFilePath;
        public static Save save;

    }
}

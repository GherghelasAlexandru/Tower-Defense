using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class Animation
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get;  set; }

        public int Rows { get; set; }
        public int Columns { get; set; }
        public int FrameHeight { get { return Texture.Height / Rows; } }
        public int EmptyFrame { get; set; }
        public float FrameSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width / Columns; } }
        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture,int column, int row, int emptyFrames)
        {

            this.EmptyFrame = emptyFrames;

            this.Rows = row;

            this.Columns = column;

            this.CurrentFrame = 0;

            this.Texture = texture;

            this.FrameCount = (Rows * Columns) - EmptyFrame;

            this.IsLooping = true;

            this.FrameSpeed = 0.15f;
           
        }


    }
}

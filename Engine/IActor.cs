using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Engine
{
    public interface IActor
    {
        Vector2 Position { get; set; }

        /// <summary>
        /// Indicate the rectangle box to use in collision
        /// </summary>
        Rectangle BoundingBox { get; }

        /// <summary>
        /// Indicate that this actor need to be remove
        /// </summary>
        bool ToRemove { get; set; }

        /// <summary>
        /// If IsActive is equal to false this actor will not be updated and draw
        /// </summary>
        bool IsActive { get; set; }
        bool IsRemoved { get; }

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime,SpriteBatch spriteBatch);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PixelDefense.Gameplay
{
    public interface Sprite
    {
        Texture2D Texture { get; }
        Rectangle Bounds { get; }
    }
}

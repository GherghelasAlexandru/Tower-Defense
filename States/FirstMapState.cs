using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;

namespace PixelDefense.States
{
    

    public class FirstMapState : State
    {
        readonly Map map;

        public FirstMapState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            map = new Map(content, "Content/FinishedVersion.tmx");
        }


        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            map.DrawGrass(spriteBatch);

            map.DrawPath(spriteBatch);

            map.DrawShadow(spriteBatch);

            map.DrawBase(spriteBatch);

            map.DrawDecorations(spriteBatch);

        }
    }
}

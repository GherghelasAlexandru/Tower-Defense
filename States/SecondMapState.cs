﻿using System;
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
    class SecondMapState : State
    {
        readonly Map map;

        public SecondMapState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            map = new Map(content, "Content/SecondMap.tmx");
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            map.DrawGrass(spriteBatch);

            map.DrawPath(spriteBatch);

            map.DrawShadow(spriteBatch);

            map.DrawBase(spriteBatch);

            map.DrawDecorations(spriteBatch);

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.States
{
    class GameState : State
    {

        List<Map> maps;
        
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            maps = new List<Map>();
            
           
        }

        
        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public void AddMap(Map map)
        {
            maps.Add(map);
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            foreach(var map in maps)
            {
                
                
                    map.DrawGrass(spriteBatch);

                    map.DrawPath(spriteBatch);

                    map.DrawShadow(spriteBatch);

                    map.DrawBase(spriteBatch);

                    map.DrawDecorations(spriteBatch);
                
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

           
                MapSelectionState mapSelection = new MapSelectionState(_game, _graphicsDevice, _content);

            DrawMap(spriteBatch);
            
        
        }
    }
}

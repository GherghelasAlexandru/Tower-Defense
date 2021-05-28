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
        //shooting sprites
        private List<Sprite> _sprites;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            maps = new List<Map>();

            var basicTowerTexture = content.Load<Texture2D>("Tower/tower");

            _sprites = new List<Sprite>()
      {
        new BasicTower(basicTowerTexture)
        {
          Position = new Vector2(100, 100),
          Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")),
        },
      };



        }

        
        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public void postupdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
            postupdate();
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

        public void DrawSprites(SpriteBatch spriteBatch)

        {
            foreach (var sprite in _sprites.ToArray())
                sprite.Draw(spriteBatch);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

           
          
            DrawMap(spriteBatch);
            DrawSprites(spriteBatch);



        }
    }
}

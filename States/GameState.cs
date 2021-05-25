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
    

    public class GameState : State
    {
        readonly Map map;
        Enemy enemy;
        
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            map = new Map(content, "Content/FinishedVersion.tmx");
            
            enemy = new Enemy(content, 2);
            Dictionary<string, Animation> dicAnim = new Dictionary<string, Animation>
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Run"), 8)},
                {"Attack",new Animation(content.Load<Texture2D>("spritesheets/Attack"),8 )},
                {"Take_Hit",new Animation(content.Load<Texture2D>("spritesheets/Take_Hit"),8 )},
                {"Death",new Animation(content.Load<Texture2D>("spritesheets/Death"),8 )}
            };
            enemy.Init(dicAnim);
        }


        protected  void Initialize()
        {
            // TODO: Add your initialization logic here

            //enemy.Position = 10;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            enemy.DrawEnemy(spriteBatch);
            Console.WriteLine(enemy.Position);
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

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelDefense.Controls;
using Microsoft.Xna.Framework.Input;


namespace PixelDefense.States
{
    public class GameState : State
    {
        private List<Button> _button;
        
        //shooting sprites
        private List<Sprite> _towers;
        //private BasicTower tower1;
        Texture2D goblinTexture;
        Goblin goblin;
        ContentManager content;

        Wave wave;
        Mushroom mushroom;
        Dictionary<string, Animation> goblinAnimations;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

            goblinAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Goblin_Run"),8) },
                {"Idle", new Animation(content.Load<Texture2D>("spritesheets/Goblin_Idle"),4) }
            };

            var mushroomAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Mushroom_Run"),8) },
                {"Idle", new Animation(content.Load<Texture2D>("spritesheets/Mushroom_Idle"),4) }
            };
          //  map1.AddPath();
          //  goblinTexture = _content.Load<Texture2D>("spritesheets/Goblin_Run");
            goblin = new Goblin(goblinAnimations);
            mushroom = new Mushroom(mushroomAnimations)  ;

            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");


            _towers = new List<Sprite>();
            wave = new Wave(map1);


            this.content = content;

            //goblin.SpawnEnemy(map1.GetStartingPoint(), map1.GetPath());
           // mushroom.SpawnEnemy(map1.GetStartingPoint(), map1.GetPath());
           // AddEnemy(goblin);
            wave.LoadEnemyOption(goblin);
            wave.LoadEnemyOption(mushroom);

            Console.WriteLine(goblin._position);
           // AddEnemy(mushroom);


            /*                tower1 = new BasicTower(basicTowerTexture,10)
                            {
                                Position = new Vector2(200, 200),
                                Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")),

                        };*/


            var chooseSurrenderButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(480, 0),
                Text = "Surrender",
            };

            chooseSurrenderButton.Click += ChooseSurrenderButton_Click;

            var shopButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(320, 0),
                Text = "Shop",
            };

            shopButton.Click += ShopButton_click;

            _button = new List<Button>()
            {
            chooseSurrenderButton,
            shopButton,
            };

        }

        private void ChooseSurrenderButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(_game.gameOverState);
        }

        public List<Sprite> getSprites()
        {
            return _towers;
        }

        public void AddTower(BasicTower tower)
        {
            _towers.Add(tower);
        }

        public void AddEnemy(Enemy enemy)
        {
            _towers.Add(enemy);
        }

        private void ShopButton_click(object sender, EventArgs e)
        {
            // to be modified to change back to the gameState
            _game.ChangeState(_game.shopState);

        }

        public override void PostUpdate(GameTime gameTime)
        {

            for (int i = 0; i < _towers.Count; i++)
            {
                if (_towers[i].IsActive)
                {
                    _towers.RemoveAt(i);
                    i--;
                }
            }
        }

        public void postUpdate()
        {

            for (int i = 0; i < _towers.Count; i++)
            {
                if (_towers[i].IsActive)
                {
                    _towers.RemoveAt(i);
                    i--;
                }
            }

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _towers.ToArray())
                sprite.Update(gameTime, _towers);
         

            foreach (var button in _button)
                button.Update(gameTime);

            postUpdate();

           wave.Update(gameTime,_towers,content);

            //postupdate();
        }

       
        public void DrawMap(SpriteBatch spriteBatch)
        {
            foreach(var map in _maps)
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
            foreach (var sprite in _towers.ToArray())
                sprite.Draw(spriteBatch);

        }

        public void DrawButtons(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var button in _button)
                button.Draw(gameTime, spriteBatch);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            
            DrawMap(spriteBatch);
            DrawButtons(gameTime, spriteBatch);
            DrawSprites(spriteBatch);
            wave.Draw(spriteBatch);

           

        }
    }
}

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
        private List<BasicTower> basicTowers;
        //private BasicTower tower1;
        Crab crab;
        Bat bat;
        private int hit = 0;
        Dictionary<string, Animation> crabAnimations;

        public int gold;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            gold = 30;

            crabAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Crab_Run"),4,2) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Crab_AttackB"),4,2) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Crab_Death"),4,2) }
            };

            /*var mushroomAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Mushroom_Run"),8) },
                {"Idle", new Animation(content.Load<Texture2D>("spritesheets/Mushroom_Idle"),4) }
            };*/
            map1.AddPath();
       
            crab = new Crab(crabAnimations);
            /*mushroom = new Mushroom(mushroomAnimations)  ;*/

            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");


            _towers = new List<Sprite>();

            basicTowers = new List<BasicTower>();
            
            crab.SpawnEnemy(map1.GetStartingPoint(), map1.GetPath());
            //mushroom.SpawnEnemy(map1.GetStartingPoint(), map1.GetPath());
            AddEnemy(crab);

            Console.WriteLine(crab._position);
            //AddEnemy(mushroom);


            /*                tower1 = new BasicTower(basicTowerTexture,10)
                            {
                                Position = new Vector2(200, 200),
                                Bullet = new Bullet(content.Load<Texture2D>("Tower/bullet")),

                        };*/


            var chooseSurrenderButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1110, 740),
                Text = "Surrender",
            };

            chooseSurrenderButton.Click += ChooseSurrenderButton_Click;

            var shopButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(945, 740),
                Text = "Shop",
            };

            shopButton.Click += ShopButton_click;

            _button = new List<Button>()
            {
            chooseSurrenderButton,
            shopButton,
            };

        }

        private void addBasicTowers() {
            foreach (var basicTower in _towers) {
                if (basicTower is BasicTower) {
                    basicTowers.Add((BasicTower)basicTower);
                }
            }
        }

        public int GetGold()
        {
            return gold;
        }

        public void SetGold(int goldToAdd)
        {
            gold += goldToAdd;
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



        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _towers.ToArray())
                sprite.Update(gameTime, _towers);
         

            foreach (var button in _button)
                button.Update(gameTime);

            PostUpdate(gameTime);

            //postupdate();
            addBasicTowers();

            foreach (var tower in basicTowers)
            {
                foreach (var bullet in tower.getBullets())
                {
                    
                    if (bullet.Rectangle.Intersects(crab.Rectangle))
                    {

                        crab.health -= bullet.dmg;
                        bullet._position += crab.Position;
                        if (crab.health < 1)
                        {
                            Console.WriteLine("hit");
                            crab._movement = new Vector2(0, 0);
                            gold += 10;
                        }
                        
                       
                        
                    }
                }
            }
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

            spriteBatch.DrawString(textFont, "Gold = " + _game.gameState.GetGold(), new Vector2(10, 10), Color.Black);


        }
    }
}

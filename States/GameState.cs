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
        
        private List<Sprite> _sprites;
        public Map map1;
        public Map map2;
        public List<Map> _maps;
        public Crab crab;

        public bool IsOnPath;

        Bat bat;
        readonly Dictionary<string, Animation> crabAnimations;
        readonly Dictionary<string, Animation> slimeAnimations;
        readonly Dictionary<string, Animation> batAnimations;
        readonly Dictionary<string, Animation> ratAnimations;

        public int gold;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _sprites = new List<Sprite>();
            gold = 100;
            _maps = new List<Map>();

            map1 = new Map(content, "Content/Test.tmx");
            map2 = new Map(content, "Content/SecondMap2.tmx");
            crabAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Crab_Run"),4,2) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Crab_AttackB"),4,2) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Crab_Death"),4,2) }
            };

            slimeAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Slime_Spiked_Run"),4,1) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Slime_Spiked_Ability"),4,1) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Slime_Spiked_Death"),4,2) }
            };

            batAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Bat_Fly"),4,1) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Bat_Attack"),4,2) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Bat_Death"),4,3) }
            };

            ratAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Rat_Run"),4,2) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Rat_Attack"),4,2) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Rat_Death"),4,2) }
            };

            crab = new Crab(crabAnimations);
            bat = new Bat(batAnimations);

            map1.AddPath();
            map2.AddPath();

            map1.AddCollisionPath();
            
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

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

        public void AddMap(Map map)
        {
            _maps.Add(map);
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
            return _sprites;
        }

        public void PlaceTower()
        {
            
            _game.mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(_game.mouseState.X, _game.mouseState.Y);

            {
                foreach (var tower in _sprites)
                    
                if (tower is BasicTower)
                {
                    if (_game.mouseState.LeftButton == ButtonState.Released && tower.dragging)
                    {
                        tower._position.X = _game.mouseState.X;
                        tower._position.Y = _game.mouseState.Y;
                    }
                    else if (_game.mouseState.LeftButton == ButtonState.Pressed && tower.BoundingBox.Contains(mousePosition) && tower.dragging)
                    {     
                            if (map1.IsCollision(tower.BoundingBox) == true)
                            {
                                IsOnPath = true;
                                tower.dragging = true;
                                Console.WriteLine("FOOK");
                            }
                            else 
                            {
                                IsOnPath = false;
                                tower.dragging = false;
                                tower._position.X = _game.mouseState.X;
                                tower._position.Y = _game.mouseState.Y;
                            }
                        }
                }

            }
        }


        public void AddTower(BasicTower tower)
        {
            _sprites.Add(tower);
        }

        public void AddEnemy(Enemy enemy)
        {

          
                _sprites.Add(enemy);
           
            if (_maps.Contains(map1))
            {

                enemy.SpawnEnemy(map1.GetStartingPoint(), map1.GetPath());

            }
            else if (_maps.Contains(map2))
            {

                enemy.SpawnEnemy(map2.GetStartingPoint(), map2.GetPath());
            }
        }

        private void ShopButton_click(object sender, EventArgs e)
        {

            // to be modified to change back to the gameState
            _game.ChangeState(_game.shopState);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsActive)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }


        public void RemoveMap(Map map)
        {
            _maps.Remove(map);
        }

        public override void Update(GameTime gameTime)
        {
            
            PlaceTower();

            if (_game.mapSelection.chooseFirstMapButton.Clicked)
            {
                AddMap(map1);
                RemoveMap(map2);
                AddEnemy(bat);
               
                _game.mapSelection.chooseFirstMapButton.Clicked = false;
            }
            else if (_game.mapSelection.chooseSecondMapButton.Clicked)
            {
                AddMap(map2);
                RemoveMap(map1);
                AddEnemy(crab);

                _game.mapSelection.chooseSecondMapButton.Clicked = false;
            }


            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
         

            foreach (var button in _button)
                button.Update(gameTime);

            

                PostUpdate(gameTime);
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
            foreach (var sprite in _sprites.ToArray())
                
                   
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
            if(IsOnPath)
            {
                spriteBatch.DrawString(textFont, "Can't place on path", new Vector2(150, 10), Color.Black);
            }
            spriteBatch.DrawString(textFont, "Gold = " + _game.gameState.GetGold(), new Vector2(10, 10), Color.Black);
        }
    }
}

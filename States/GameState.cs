using Microsoft.Xna.Framework;
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
        public List<Rectangle> towerPlacements;
        public List<Map> _maps;

        public Map map1;
        public Map map2;

        protected WavesManager waves;
        protected Button startWaveButton;

        public Wave wave;

        public bool IsOnAnotherTower;
        public bool IsOnPath;
        public float FollowDistance;
        public int gold;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            gold = 100;

            _sprites = new List<Sprite>();
            towerPlacements = new List<Rectangle>();
            _maps = new List<Map>();

            map1 = new Map(content, "Content/Test.tmx");
            map2 = new Map(content, "Content/SecondMap2.tmx");

            waves = new WavesManager(content);
            wave = new Wave(content);

            map1.AddCollisionPath();
            map2.AddCollisionPath();
            
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

            this.startWaveButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(779, 740),
                //Text = " ",
            };
            startWaveButton.Click += StartWaveButton_click;

            _button = new List<Button>()
            {
            chooseSurrenderButton,
            shopButton,
            startWaveButton
            };
        }
        public void AddTowerPlacement()
        {
            foreach (var tower in _sprites)
            {
                if(tower.IsPlaced)
                {          
                    towerPlacements.Add(new Rectangle((int)tower._position.X, (int)tower._position.Y, (int)tower._texture.Width, (int)tower._texture.Height));
                }
            }
        }
        public bool IsTowerColliding(Rectangle target)
        {
            foreach (Rectangle rec in towerPlacements)
                if (rec.Intersects(target))
                {
                    return true;
                }
            return false;
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

        public void StartWaveButton_click(object sender, EventArgs e)
        {
            waves.StartWave(false);
        }

        private void ChooseSurrenderButton_Click(object sender, EventArgs e)
        {

            _game.ChangeState(_game.gameOverState);
        }

        public List<Sprite> getSprites()
        {
            return _sprites;
        }

        public void SetStartWaveButton()
        {
           this.startWaveButton.Text = "Start Wave" + " " + waves.GetWaveNumber();
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
                            foreach (var map in _maps)
                            {
                                if (map.IsCollision(tower.BoundingBox) == true)
                                {
                                    IsOnPath = true;
                                    tower.dragging = true;

                                }
                                else if (IsTowerColliding(tower.BoundingBox))
                                {
                                    IsOnAnotherTower = true;
                                    tower.dragging = true;
                                }
                                else
                                {
                                    IsOnPath = false;
                                    IsOnAnotherTower = false;
                                    tower.dragging = false;
                                    tower._position.X = _game.mouseState.X;
                                    tower._position.Y = _game.mouseState.Y;
                                    tower.IsPlaced = true;
                                }
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
                if (!_sprites[i].IsActive)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }
        public void RemoveEnemy()
        {
            for (int i = 0; i < wave.enemies.Count; i++)
            {
                if (!wave.enemies[i].IsActive)
                {
                    wave.enemies.RemoveAt(i);
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
            SetStartWaveButton();
            PlaceTower();
            AddTowerPlacement();
            if (_game.mapSelection.chooseFirstMapButton.Clicked)
            {
                waves.SetAttackingPath(map1);
                //wave.CreateEnemy();
                AddMap(map1);
                RemoveMap(map2);
                
               
                _game.mapSelection.chooseFirstMapButton.Clicked = false;
            }
            else if (_game.mapSelection.chooseSecondMapButton.Clicked)
            {
                waves.SetAttackingPath(map2); 

                AddMap(map2);
                RemoveMap(map1);

                _game.mapSelection.chooseSecondMapButton.Clicked = false;
            }

            foreach (BasicTower tower in _game.shopState.basicTowers)
            {

                foreach (Enemy enemy in waves.GetEnemies())
                {
                    foreach (var towers in _sprites)
                    {
                        var enemyloc = new Vector2(enemy._position.X, enemy._position.Y);
                        Vector2 direction = towers._position - enemyloc;
                        towers._rotation = (float)Math.Atan2(direction.Y, direction.X);

                        float dis = Vector2.Distance(enemyloc, towers._position);
                        // Console.WriteLine(dis);
                        //  Console.WriteLine(towers.radius);
                        if (dis <= towers.radius)
                        {
                            towers.firing = true;

                        }
                        else { towers.firing = false; }


                        foreach (Bullet bullet in tower.GetBullets())
                        {

                            if (bullet.Bounds.Intersects(enemy.InteractionBox))
                            {
                                bullet.bulletIsDead = true;
                                //bullet._position = enemy._position;
                                enemy.setHealth(enemy.getHealth() - bullet.getDmg());

                                if (enemy.getHealth() == 0)
                                {

                                    enemy._movement = new Vector2(0, 0);
                                    gold += enemy.goldDrop;


                                }

                            }

                            if (!bullet.bulletIsDead)
                            {

                                var distance = enemy._position - bullet._position;
                                bullet._rotation = (float)Math.Atan2(distance.Y, distance.X);
                                Vector2 Direction = new Vector2((float)Math.Cos(bullet._rotation), (float)Math.Sin(bullet._rotation));
                                var currentDistance = Vector2.Distance(bullet._position, enemy._position);
                                if (currentDistance > FollowDistance)
                                {
                                    var t = MathHelper.Min((float)Math.Abs(currentDistance), enemy.xVelocity);
                                    var velocity = Direction * t;

                                    bullet._position += velocity;
                                }
                            }
                            else { bullet._position = tower._position; }


                        }


                    }


                }
            }


            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
         

            foreach (var button in _button)
                button.Update(gameTime);


            waves.Update(gameTime, _game.shopState.basicTowers);
            PostUpdate(gameTime);

            
            
            RemoveEnemy();
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
            waves.Draw(spriteBatch);
            if(IsOnPath)
            {
                spriteBatch.DrawString(textFont, "You can only place a tower on grass!", new Vector2(10, 760), Color.Black);
            }
            else if (IsOnAnotherTower)
            {
                spriteBatch.DrawString(textFont, "You can not stack towers on top of eachother!", new Vector2(10, 760), Color.Black);
            }
            spriteBatch.DrawString(textFont, "Gold = " + _game.gameState.GetGold(), new Vector2(10, 10), Color.Black);
        }
    }
}

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
using PixelDefense.Engine;

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
        protected Button startWaveButton;
        public Base mainBase;
        public Wave wave;

        public bool IsOnAnotherTower;
        public bool IsOnPath;
        public float FollowDistance;
        public int gold;
        public int score;
        public float timer;
        public Animation healthBar;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            healthBar = new Animation(content.Load<Texture2D>("spritesheets/Stitched_HP_Bar"), 8, 1, 0) { IsLooping = false };
           
            mainBase = new Base(healthBar);
            gold = 1000;
            //Globals.soundControl.ChangeMusic("Sounds/bgMusic2");
            _sprites = new List<Sprite>();
            towerPlacements = new List<Rectangle>();
            _maps = new List<Map>();

            map1 = new Map(content, "Content/Test.tmx");
            map2 = new Map(content, "Content/SecondMap2.tmx");

            wave = new Wave(content);
            
            

            map1.AddCollisionPath();
            map2.AddCollisionPath();
            
            var buttonTexture = _content.Load<Texture2D>("Controls/button3");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var chooseSurrenderButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1110, 774),
                Text = "Surrender",
            };
            chooseSurrenderButton.Click += ChooseSurrenderButton_Click;

            var shopButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(945, 774),
                Text = "Shop",
            };
            shopButton.Click += ShopButton_click;

            this.startWaveButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(779, 774),
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
                    towerPlacements.Add(new Rectangle((int)tower.GetPosition().X, (int)tower.GetPosition().Y, (int)tower.GetTexture().Width/2, (int)tower.GetTexture().Height/4));
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
            wave.StartWave(true);
        }

        private void ChooseSurrenderButton_Click(object sender, EventArgs e)
        {
            Globals.soundControl.stopMusic();
            Globals.soundControl.playSound("game over");
            _game.ChangeState(_game.gameOverState);
        }

        public void SetStartWaveButton()
        {
            this.startWaveButton.Text = "Start Wave" + " " + wave.GetWaveNumber();
        }

        public void PlaceTower()
        {
            _game.mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(_game.mouseState.X, _game.mouseState.Y);
                foreach (var tower in _sprites)

                    if (tower is BasicTower)
                    {
                        if (_game.mouseState.LeftButton == ButtonState.Released && tower.GetDragged())
                        {
                            tower._position.X = _game.mouseState.X;
                            tower._position.Y = _game.mouseState.Y;
                        }
                        else if (_game.mouseState.LeftButton == ButtonState.Pressed && tower.BoundingBox.Contains(mousePosition) && tower.GetDragged())
                        {
                            foreach (var map in _maps)
                            {
                                if (map.IsCollision(tower.BoundingBox) == true)
                                {
                                    IsOnPath = true;
                                    tower.SetIsDragged(true);

                                }
                                else if (IsTowerColliding(tower.BoundingBox))
                                {
                                    Globals.soundControl.playSound("negative");
                                    IsOnAnotherTower = true;
                                    tower.SetIsDragged(true);
                                }
                                else
                                {
                                    IsOnPath = false;
                                    IsOnAnotherTower = false;
                                    tower.SetIsDragged(false); 
                                    tower._position.X = _game.mouseState.X;
                                    tower._position.Y = _game.mouseState.Y;
                                    tower.SetIsPlaced(true);
                                }
                            }
                            
                        }
                    }
        }

        public void AddBullets(GameTime gameTime)
        {
            foreach (BasicTower tower in _sprites)
            {
                foreach (Enemy enemy in wave.GetEnemies())
                {
                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    tower._timer -= elapsed;
                    if (enemy.IsActive && tower._timer < 0 && tower.firing)
                    {

                        tower.AddBullet();
                        tower._timer = tower.TIMER;
                    }
                    else if (!enemy.IsActive || !tower.firing)
                    {
                        tower.Bullet.IsActive = false;
                        RemoveBullets(gameTime);
                    }
                }
            }
        }

        public void AddTower(BasicTower tower)
        {
            _sprites.Add(tower);
        }


        private void ShopButton_click(object sender, EventArgs e)
        {
            //shop can not be used when wave is active
            if(wave.GetWaveBreak() == false)
            {
                Globals.soundControl.playSound("shop");
                _game.ChangeState(_game.shopState);
            }
            
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


        public  void RemoveBullets(GameTime gameTime)
        {
            foreach(BasicTower tower in _sprites)
                
            for (int i = 0; i < tower.GetBullets().Count; i++)
            {
                if (tower.GetBullets()[i].IsActive)
                {
                        tower.GetBullets().RemoveAt(i);
                    i--;
                }
            }
        }

        public void RemoveEnemy()
        {
            for (int i = 0; i < wave.GetEnemies().Count; i++)
            {
                if (!wave.GetEnemies()[i].IsActive)
                {
                    wave.GetEnemies().RemoveAt(i);
                    i--;
                    wave.IncreaseDeadEnemies();
                    score++;
                }
            }
        }
        public void RemoveMap(Map map)
        {
            _maps.Remove(map);
        }

        public override void Update(GameTime gameTime)
        {
            AddBullets(gameTime);
            if (mainBase.health == 0)
            {
                Globals.soundControl.stopMusic();
                Globals.soundControl.playSound("game over");
                _game.ChangeState(_game.gameOverState);
            }
            if(mainBase.health > 0)
            {
                AttackBase(gameTime);
            }

            SetStartWaveButton();
            PlaceTower();
            AddTowerPlacement();
            if (_game.mapSelection.chooseFirstMapButton.Clicked)
            {
                wave.SetAttackingPath(map1);
                AddMap(map1);
                RemoveMap(map2);
                _game.mapSelection.chooseFirstMapButton.Clicked = false;
            }
            else if (_game.mapSelection.chooseSecondMapButton.Clicked)
            {
                wave.SetAttackingPath(map2); 

                AddMap(map2);
                RemoveMap(map1);

                _game.mapSelection.chooseSecondMapButton.Clicked = false;
            }
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            foreach (var button in _button)
                button.Update(gameTime);

            wave.Update(gameTime, _game.shopState.basicTowers);
            mainBase.Update(gameTime);
            foreach (BasicTower tower in _game.shopState.basicTowers)
            {
                foreach (Enemy enemy in wave.GetEnemies())
                {
                    
                    if (enemy.GetIsActive())
 
                    foreach (var towers in _sprites)
                    {
                        var enemyloc = new Vector2(enemy.Position.X, enemy.Position.Y);
                        Vector2 direction = towers.GetPosition() - enemyloc;

                        float dis = Vector2.Distance(enemyloc, towers._position);
          
                        if (dis <= towers.GetRadius())
                        {
                            towers.firing = true;
                            towers.SetRotation((float)Math.Atan2(direction.Y, direction.X));

                            foreach (Bullet bullet in tower.GetBullets())
                            {
                                var distance = enemy.Position - bullet.Position;
                                bullet.SetRotation((float)Math.Atan2(distance.Y, distance.X));
                                bullet.SetDirection(new Vector2((float)Math.Cos(bullet._rotation), (float)Math.Sin(bullet._rotation)));
                                var currentDistance = Vector2.Distance(bullet.Position, enemy.Position);

                                var t = MathHelper.Min((float)Math.Abs(currentDistance), bullet.xVelocity);
                                var velocity = bullet.GetDirection() * t;

                                bullet.SetPosition(bullet.GetPosition() + velocity);                               

                                    if (bullet.Bounds.Intersects(enemy.BoundingBox) && bullet.IsActive)
                                    {
                                        Globals.soundControl.playSound("shoot");
  
                                        bullet.SetIsActive(false);
                                        enemy.SetHealth(enemy.GetHealth() - bullet.GetDmg());
 
                                        Console.WriteLine(enemy.health);
                                    }
                                    if (enemy.GetHealth() == 0 && !enemy.isDead)
                                    {
                                        Globals.soundControl.playSound("click");
                                        bullet.SetIsActive(false);
                                        enemy.SetMovement(new Vector2(0, 0));
                                        enemy.SetIsDead(true);
                                        gold += enemy.GetGoldDrop();

                                    }
                                    
                                    
                                }
                        }
                        else { towers.SetIsFiring(false); }
                    }
                }
            }
           
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

        public void AttackBase(GameTime GameTime)
        {
            //timer = (float)GameTime.ElapsedGameTime.TotalSeconds;
            foreach (Enemy enemy in wave.GetEnemies())
            {

                /*if (enemy.GetIsActive())
                
                    if (enemy.GetPath().Count == 0)
                    {
                        
                            Console.WriteLine(mainBase.health);
                            mainBase.health -= enemy.damage;
                           
                        
                    }*/
                    enemy.AttackBase(mainBase,GameTime);
                
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
            if(_maps.Contains(map1))
            wave.Draw(spriteBatch,SpriteEffects.FlipHorizontally);
            if(_maps.Contains(map2))
                wave.Draw(spriteBatch, SpriteEffects.None);
            mainBase.Draw(spriteBatch,SpriteEffects.None);
            if(IsOnPath)
            {
                spriteBatch.DrawString(textFont, "You can only place a tower on grass!", new Vector2(10, 780), Color.Black);
            }
            else if (IsOnAnotherTower)
            {  
                spriteBatch.DrawString(textFont, "You can not stack towers on top of eachother!", new Vector2(10, 780), Color.Black);
            }
            spriteBatch.DrawString(textFont, "Gold  " + _game.gameState.GetGold(), new Vector2(20, 10), Color.Black);
            spriteBatch.DrawString(textFont, "Wave number  " + wave.GetWaveNumber(), new Vector2(560, 10), Color.Black);
            spriteBatch.DrawString(textFont, "Score  " + score, new Vector2(1100, 10), Color.Black);
        }
    }
}

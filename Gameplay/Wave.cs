using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
   public class Wave
    {
        public ForgeMaster forge;
        public Map map;
        public List<Enemy> enemies;
        public ContentManager content;

        protected bool waveBreak;
        public float timer;
        public int waveNumber;
        public float timebeetweenspawn;
        public int enemyNumbers;
        public int deadEnemies;


        public Wave(ContentManager content)
        {
           
            //create and render enemies
            this.content = content;
            this.forge = new ForgeMaster(this.content);
            this.enemies = new List<Enemy>();

            // required for wave lenght,difficulty,
            this.timebeetweenspawn = 0.7f;
            this.enemyNumbers = 1;
            this.waveBreak = false;
            this.waveNumber = 1;


        }

        public void SetWaveBreak(bool waveBreak)
        {
            this.waveBreak = waveBreak;
        }

        public int GetWaveNumber()
        {
            return this.waveNumber;
        }

        public bool GetWaveBreak()
        {
            return this.waveBreak;
        }

        public List<Enemy> GetEnemies()
        {
            return this.enemies;
        }

        public void IncreaseDifficulty()
        {
            enemyNumbers += 1;
            // timebeetweenspawn -= 1;
        }

        public void SetMap(Map map)
        {
            //get the map 
            this.map = map;

        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            
            this.waveNumber = GetWaveNumber();
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Console.WriteLine("Enemie Count: " + GetEnemies().Count);
            Console.WriteLine("Dead enemies: " + GetDeadEnemies());

            if(GetWaveBreak() == true)
            {
                CreateEnemy();

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isDead)
                    {
                        deadEnemies++;
                    }
                    if (enemy.Active)
                    {
                        enemy.Update(gameTime, sprites);
                    }
                }

            }

            WaveBreak();
        }

        public void StartWave(bool waveBreak)
        {
            SetWaveBreak(waveBreak);
        }

        public void SetAttackingPath(Map map)
        {
            this.SetMap(map);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public void CreateEnemy()
        {
            if( timer > timebeetweenspawn)
            {
                
                if(enemies.Count + deadEnemies < enemyNumbers)
                {
                    Enemy enemy = forge.ForgeEnemy(forge.GetRandomEnemy());
                    enemy.SpawnEnemy(map.GetStartingPoint(), map.GetPath());
                    this.enemies.Add(enemy);
                }
                timer = 0;
            }
        }

        public void WaveBreak()
        {
            if(enemies.Count == 0 && waveBreak == true)
            {
                
                deadEnemies = 0;
                IncreaseDifficulty();
                waveNumber++;
                waveBreak = false;
            }
        }

        public int GetDeadEnemies()
        {
            return deadEnemies;
        }
    }
}

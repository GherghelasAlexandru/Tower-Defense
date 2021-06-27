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
        protected ForgeMaster forge;
        protected Map map;
        protected List<Enemy> enemies;
        public ContentManager content;

        protected bool waveBreak;
        protected float timer;
        protected int waveNumber;
        protected float timebeetweenspawn;
        protected int enemyNumbers;
        protected int deadEnemies;
        protected Base mainBase;


        public Wave(ContentManager content)
        {
           
            //required for create and render enemies
            this.content = content;
            forge = new ForgeMaster(this.content);
            enemies = new List<Enemy>();
            

            // required for wave lenght,difficulty,
            timebeetweenspawn = 0.7f;
            enemyNumbers = 1;
            waveBreak = false;
            waveNumber = 1;

        }

        public void IncreaseDeadEnemies()
        {
            deadEnemies += 1;
        }


        public void IncreaseDifficulty()
        {
            enemyNumbers += 1;
            // timebeetweenspawn -= 1;
        }


        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            
            waveNumber = GetWaveNumber();
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Console.WriteLine("Enemie Count: " + GetEnemies().Count);
            Console.WriteLine("Dead enemies: " + GetDeadEnemies());

            if(GetWaveBreak() == true)
            {
                CreateEnemy();

                foreach (Enemy enemy in enemies)
                {
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
            SetMap(map);

        }

        public void Draw(SpriteBatch spriteBatch,SpriteEffects spriteEffects)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch, spriteEffects);
            }
        }

        public void CreateEnemy()
        {
            if( timer > timebeetweenspawn)
            {
                
                if(enemies.Count + deadEnemies  < enemyNumbers)
                {
                    Enemy enemy = forge.ForgeEnemy(forge.GetRandomEnemy());
                    
                    enemy.SpawnEnemy(map.GetStartingPoint(), map.GetPath());
                    enemies.Add(enemy);
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

        // get and set methods

        public void SetDeadEnemies(int deadEnemies)
        {
            this.deadEnemies = deadEnemies;
        }



        public int GetDeadEnemies()
        {
            return deadEnemies;
        }

        public void SetWaveBreak(bool waveBreak)
        {
            this.waveBreak = waveBreak;
        }

        public bool GetWaveBreak()
        {
            return waveBreak;
        }

        public void SetWaveNumber(int waveNumber)
        {
            this.waveNumber = waveNumber;
        }

        public int GetWaveNumber()
        {
            return waveNumber;
        }

        public void SetEnemyNumbers(int enemyNumbers)
        {
            this.enemyNumbers = enemyNumbers;
        }

        public int GetEnemyNumbers()
        {
            return enemyNumbers;
        }

        public void SetTimeBetweenSpawn(float timebeetweenspawn)
        {
            this.timebeetweenspawn = timebeetweenspawn;
        }

        public float GetTimeBetweenSpawn()
        {
            return timebeetweenspawn;
        }

        public void SetEnemies(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }

        public void SetForge(ForgeMaster forge)
        {
            this.forge = forge;
        }

        public ForgeMaster GetForge()
        {
            return forge;
        }

        public void SetMap(Map map)
        {
            this.map = map;
        }

        public Map GetMap()
        {
            return map;
        }
    }
}

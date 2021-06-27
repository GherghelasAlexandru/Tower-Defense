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
            this.deadEnemies += 1;
        }


        public void IncreaseDifficulty()
        {
            enemyNumbers += 1;
            // timebeetweenspawn -= 1;
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
                    /*if (enemy.isDead)
                    {
                        deadEnemies++;
                    }*/
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

        // get and set methods

        public void SetDeadEnemies(int deadEnemies)
        {
            this.deadEnemies = deadEnemies;
        }



        public int GetDeadEnemies()
        {
            return this.deadEnemies;
        }

        public void SetWaveBreak(bool waveBreak)
        {
            this.waveBreak = waveBreak;
        }

        public bool GetWaveBreak()
        {
            return this.waveBreak;
        }

        public void SetWaveNumber(int number)
        {
            this.waveNumber = number;
        }

        public int GetWaveNumber()
        {
            return this.waveNumber;
        }

        public void SetEnemyNumbers(int number)
        {
            this.enemyNumbers = number;
        }

        public int GetEnemyNumbers()
        {
            return this.enemyNumbers;
        }

        public void SetTimeBetweenSpawn(float time)
        {
            this.timebeetweenspawn = time;
        }

        public float GetTimeBetweenSpawn()
        {
            return this.timebeetweenspawn;
        }

        public void SetEnemies(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        public List<Enemy> GetEnemies()
        {
            return this.enemies;
        }

        public void SetForge(ForgeMaster forge)
        {
            this.forge = forge;
        }

        public ForgeMaster GetForge()
        {
            return this.forge;
        }

        public void SetMap(Map map)
        {
            this.map = map;
        }

        public Map GetMap()
        {
            return this.map;
        }
    }
}

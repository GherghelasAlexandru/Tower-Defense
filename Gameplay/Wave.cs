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
            this.enemyNumbers = 3;
           // this.waveTime = 50f;
            this.waveBreak = false;
            this.waveNumber = 1;


        }

        public void AddEnemy(Enemy enemy)
        {
            this.enemies.Add(enemy);
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
           // waveTime += 20;
            enemyNumbers += 4;
            // timebeetweenspawn -= 1;
        }

        public void SetMap(Map map)
        {
            //get the map 
            this.map = map;

        }


        public void Update(GameTime gameTime, List<Sprite> sprites)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CreateEnemy();


            foreach (Enemy enemy in enemies)
            {
                if(enemy.isDead)
                {
                    deadEnemies++;
                }
                if (enemy.Active)
                {
                    enemy.Update(gameTime, sprites);
                }
            }
        }

        public void RemoveEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].IsActive)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
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
            if(deadEnemies ==  enemyNumbers)
            {
                //enemies.Clear();
              
                    deadEnemies = 0;
                    enemies.Clear();
                    IncreaseDifficulty();

                    //aici e buba 
                    waveBreak = false;
             
            }

        }




    }
}

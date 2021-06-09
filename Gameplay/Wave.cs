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

        public float timer;
        public float waveTime;
        public float waveBreak;
        public float timebeetweenspawn;
        public int enemyNumbers;
        

        public Wave(ContentManager content)
        {
           
            //create and render enemies
            this.content = content;
            this.forge = new ForgeMaster(this.content);
            this.enemies = new List<Enemy>();

            // required for wave lenght,difficulty,
            this.timebeetweenspawn = 3f;
            this.enemyNumbers = 3;
            this.waveTime = 50f;
            this.waveBreak = 25f;

        }

        public void AddEnemy(Enemy enemy)
        {
            this.enemies.Add(enemy);
        }

        public void IncreaseDifficulty()
        {
            waveTime += 20;
            enemyNumbers += 2;
            // timebeetweenspawn -= 1;
        }

        public void SetMap(Map map)
        {
            //get the map and create the path
            this.map = map;
            map.AddPath();

        }


        public void Update(GameTime gameTime, List<Sprite> sprites)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CreateEnemy();


            foreach (Enemy enemy in enemies)
            {
                if (enemy.Active)
                {
                    enemy.Update(gameTime, sprites);
                    
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

                if(enemies.Count < enemyNumbers)
                {
                    Enemy enemy = forge.ForgeEnemy(forge.GetRandomEnemy());
                    enemy.SpawnEnemy(map.GetStartingPoint(), map.GetPath());
                    this.enemies.Add(enemy);
                }
                timer = 0;
            }
        }




    }
}

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
        public Enemy enemy;
        public Map map;
        public List<Enemy> enemiesOptions;
        public List<Enemy> enemies;
        public Random random;



        public float timebeetweenspawn;
        public int difficulty;
        public float waveTime;
        public float waveBreak;
        public float timer;


        public Wave(Map map)
        {
            enemies = new List<Enemy>();
            enemiesOptions = new List<Enemy>();
            random = new Random();
            timebeetweenspawn = 2f;
            difficulty = 1;
            waveTime = 50f;
            waveBreak = 25f;
            this.map = map;
            this.map.AddPath();
        }

        public void AddEnemy(Enemy enemy)
        {
            enemy.SpawnEnemy(map.GetStartingPoint(),map.GetPath());
            enemies.Add(enemy);
        }

        public void LoadEnemyOption(Enemy enemy)
        {
            enemiesOptions.Add(enemy);
        }

       /* public void SpawnEnemies(Map map)
        {
            var index = random.Next(enemies.Count);
            enemies[index].SpawnEnemy(map.GetStartingPoint(), map.GetPath());

        }*/

        public void IncreaseDifficulty()
        {
            difficulty++;
            waveTime += 30f;
        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
             timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Console.WriteLine(timer);
            if(timer > timebeetweenspawn)
            {
                // aici e buba, doar 2 elemente. mai trebuie instante
                //SpawnEnemies(map);

                // var index = random.Next(enemiesOptions.Count());
                //Console.WriteLine(enemiesOptions.Count());
                //  Console.WriteLine(index);

                // Enemy inamic = enemiesOptions[index];
                //  Console.WriteLine(inamic);
                //AddEnemy(inamic);

                CreateEnemy(enemies);

                //Enemy enemy = enemiesOptions[index];
               // enemy.SpawnEnemy(map.GetStartingPoint(), map.GetPath());
                //Enemy enemy = (Enemy)enemies[index].Clone();
                //AddEnemy((Enemy)enemy.Clone());

                // ENEMIES increase, but the elements are on top of each other
                Console.WriteLine(enemies.Count);
              
                timer = 0;
            }

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

        public void CreateEnemy(List<Enemy> enemies)
        {
            var index = random.Next(enemiesOptions.Count());
            Enemy enemy = enemiesOptions[index].Clone() as Enemy;
            enemy.SpawnEnemy(map.GetStartingPoint(), map.GetPath());
            enemies.Add(enemy);
    
            // maybe create the enemy from scratch her?

        }










    }
}

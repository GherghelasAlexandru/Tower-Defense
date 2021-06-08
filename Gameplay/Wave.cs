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
        public float spawn;


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
            spawn = 0;
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
           
                // aici e buba, doar 2 elemente. mai trebuie instante
                //SpawnEnemies(map);

                var index = random.Next(enemies.Count());
            //Console.WriteLine(enemiesOptions.Count());
            //  Console.WriteLine(index);

            //Enemy inamic = enemiesOptions[index];
            //  Console.WriteLine(inamic);
            //AddEnemy(inamic);



            CreateEnemy(enemies);









            //Enemy enemy = enemiesOptions[index];
            // enemy.SpawnEnemy(map.GetStartingPoint(), map.GetPath());
            //Enemy enemy = (Enemy)enemies[index].Clone();
            // AddEnemy(enemy);

            // ENEMIES increase, but the elements are on top of each other
            Console.WriteLine(enemies.Count);


            foreach (Enemy enemy in enemies)
            {
                if (enemy.Active)
                {
                    enemy.Update(gameTime, sprites);
                    Console.WriteLine(enemy._position);
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
            if (timer > timebeetweenspawn)
            {
                var index = random.Next(enemiesOptions.Count);
                //Console.WriteLine(enemies[index]._position);

                // spawn same enemy???????
                Enemy Newenemy = enemiesOptions[index].Clone() as Enemy;
                Newenemy.SpawnEnemy(map.GetStartingPoint(), map.GetPath());
                Newenemy.Active = true;
                //Console.WriteLine(Newenemy._position);

                if (enemies.Count < 4)
                {
                    enemies.Add(Newenemy);
                    
                    // maybe create the enemy from scratch her?
                }

                timer = 0;

            }
        }

        public void CreateGoblin(ContentManager content)
        {

        }










    }
}

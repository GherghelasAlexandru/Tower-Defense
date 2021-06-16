using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Gameplay
{
    public class ForgeMaster
    {
        public List<string> enemiesOptions;
        public Random random;
        public ContentManager content;
        public Enemy enemy;

       

        public ForgeMaster(ContentManager content)
        {
            this.enemiesOptions = new List<string>();
            LoadEnemyOptions();
            this.random = new Random();
            this.content = content;

            


        }

        public void LoadEnemyOptions()
        {
            // add extra enemy if necessary

            this.enemiesOptions.Add("Slime");
            this.enemiesOptions.Add("Rat");
            this.enemiesOptions.Add("Crab");
            this.enemiesOptions.Add("Bat");
        }

        public string GetRandomEnemy()
        {
            var index = random.Next(enemiesOptions.Count);

            return enemiesOptions[index];
        }

        public Enemy ForgeEnemy(String enemyName)
        {
 
            switch (enemyName)
            {
                case "Slime":
                   this.enemy = ForgeSlime();
                    break;

                case "Rat":
                    this.enemy = ForgeRat();
                    break;

                case "Crab":
                     this.enemy = ForgeCrab ();
                    break;

                case "Bat":
                    this.enemy = ForgeBat();
                    break;

                default:
                   // ForgeRat();
                    break;
            }

            return enemy;
            
        }

        public Enemy ForgeCrab()
        {
            var crabAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Crab_Run"),4,2,2) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Crab_AttackB"),4,2,2) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Crab_Death"),4,2,4){ IsLooping = false } }
            };


            Enemy crab = new Crab(crabAnimations);

            return crab;
        }

        public Enemy ForgeSlime()
        {
             var slimeAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Slime_Spiked_Run"),4,1,0) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Slime_Spiked_Ability"),4,1,0) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Slime_Spiked_Death"),4,2,3){ IsLooping = false } }
            };

            Enemy slime = new Slime(slimeAnimations);

            return slime;

        }

        public Enemy ForgeBat()
        {
            var batAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Bat_Fly"),4,1,0) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Bat_Attack"),4,2,1) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Bat_Death"),4,3,2){ IsLooping = false } }
            };

            Enemy bat = new Bat(batAnimations);

            return bat;

        }

        public Enemy ForgeRat()
        {
            var ratAnimations = new Dictionary<string, Animation>()
            {
                {"Run", new Animation(content.Load<Texture2D>("spritesheets/Rat_Run"),4,2,2) },
                {"Attack", new Animation(content.Load<Texture2D>("spritesheets/Rat_Attack"),4,2,0) },
                {"Death", new Animation(content.Load<Texture2D>("spritesheets/Rat_Death"),4,2,4) { IsLooping = false } }
            };

            Enemy rat = new Rat(ratAnimations);

            return rat;

        }

        // get and set methods
        public void SetEnemiesOptions(List<String> enemiesOpions)
        {
            this.enemiesOptions = enemiesOpions;
        }

        public List<String> GetEnemies()
        {
            return this.enemiesOptions;
        }

        public void SetContent(ContentManager content)
        {
            this.content = content;
        }

        public ContentManager GetContent()
        {
            return this.content;
        }




    }
}

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
    public class WavesManager
    {
        protected ContentManager content;
        protected Wave wave;
        protected int waveNumber;

        public WavesManager(ContentManager content)
        {
            this.content = content;
            this.wave = new Wave(content);
            this.waveNumber = 1;
        }
        

        public void SetAttackingPath(Map map)
        {
            this.wave.SetMap(map);
        
        }

        public void StartWave(bool waveBreak)
        {
            wave.SetWaveBreak(waveBreak);
        }


        public int GetWaveNumber()
        {
            return this.waveNumber;
        }

        public List<Enemy> GetEnemies()
        {
            return wave.GetEnemies();
        }

         public void Update(GameTime gameTime, List<Sprite> sprites)
        {

            this.waveNumber = wave.GetWaveNumber();
            Console.WriteLine(wave.GetWaveNumber());
            Console.WriteLine(wave.GetWaveBreak());

            //wave.RemoveEnemies();

            if (wave.GetWaveBreak() == true)
            {
                wave.Update(gameTime, sprites);
                Console.WriteLine(wave.GetWaveBreak());
                wave.WaveBreak();
            }

            //wave.RemoveEnemies();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            wave.Draw(spriteBatch);
        }

        //public void











        }
}

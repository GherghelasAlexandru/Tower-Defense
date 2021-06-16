using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Engine
{
    public class SoundItem
    {
        public float volume;
        public string name;
        public SoundEffect sound;
        public SoundEffectInstance instance;

        public SoundItem(string name, string path, float volume)
        {
            this.name = name;
            this.volume = volume;
            sound = Globals.content.Load<SoundEffect>(path);
            createInstance();
        }

        public virtual void createInstance()
        {
            instance = sound.CreateInstance();
        }
    }
}

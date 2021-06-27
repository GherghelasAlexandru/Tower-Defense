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
        protected float volume;
        protected string name;
        protected SoundEffect sound;
        protected SoundEffectInstance instance;

        public SoundItem(string name, string path, float volume)
        {
            this.name = name;
            this.volume = volume;
            sound = Globals.content.Load<SoundEffect>(path);
            CreateInstance();
        }

        public virtual void CreateInstance()
        {
            instance = sound.CreateInstance();
        }

        public void SetVolume( float volume)
        {
            this.volume = volume;
        }

        public float GetVolume()
        {
            return volume;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetSound(SoundEffect sound)
        {
            this.sound = sound;
        }

        public SoundEffect GetSound()
        {
            return sound;
        }

        public void SetSoundInstance(SoundEffectInstance instance)
        {
            this.instance = instance;
        }

        public SoundEffectInstance GetSoundInstance()
        {
            return instance;
        }


    }
}

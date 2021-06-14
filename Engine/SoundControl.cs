using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PixelDefense.Engine
{
    public class SoundControl
    {
        public float volume;
        public SoundEffect sound;
        public SoundEffectInstance instance;

        public SoundControl(string soundPath)
        {

            sound = null;
            instance = null;

            if (soundPath != null)
            {
                ChangeMusic(soundPath);
            }
        }

        public virtual void ChangeMusic(string soundPath)
        {
            sound = Globals.content.Load<SoundEffect>(soundPath);
            instance = sound.CreateInstance();
            volume = .25f;
            instance.Volume = volume;
            instance.IsLooped = true;
            instance.Play();

        }

        public void setVolume(float volume)
        {
            this.volume = volume;
        }

        public void playSound()
        {
            instance.Play();
        }


    }
}
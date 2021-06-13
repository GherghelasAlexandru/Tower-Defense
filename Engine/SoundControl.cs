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
        public SoundControl(string soundPath, ContentManager content)
        {
            sound = null;
            instance = null;

            if (soundPath != null)
            {
                ChangeMusic(soundPath, content);
            }
        }

        public virtual void ChangeMusic(string soundPath, ContentManager content)
        {
            sound = content.Load<SoundEffect>(soundPath);
            instance = sound.CreateInstance();
            volume = .01f;
            instance.Volume = volume;

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
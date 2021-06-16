using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelDefense.Gameplay;
using PixelDefense.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelDefense.States;

namespace PixelDefense.Engine
{
    public class SoundControl
    {
        public float maxVolume;
        public SoundEffect sound;
        public SoundEffectInstance instance;
        public ContentManager content;
        public SettingsState settingsState;

        public SoundControl(string soundPath, ContentManager content, SettingsState settingsState)
        {
            this.content = content;
            sound = null;
            instance = null;
            this.settingsState = settingsState;

            if (soundPath != null)
            {
                ChangeMusic(soundPath);
            }
        }

        public virtual void ChangeMusic(string soundPath)
        {
            sound = content.Load<SoundEffect>(soundPath);
            instance = sound.CreateInstance();
            maxVolume = .25f;

            FormOption musicVolume = settingsState.GetOptionValue("Bg Music");
            Console.WriteLine(musicVolume.value);
            //Console.WriteLine(settingsState.GetOptionValue("Bg Music"));
            float musicVolumePercent = 1.0f;
            if(musicVolume != null)
            {
                musicVolumePercent = (float)Convert.ToDecimal(musicVolume.value)/180.0f;
            }
            Console.WriteLine(musicVolumePercent);
            AdjustVolume(musicVolumePercent);
            instance.Volume = musicVolumePercent * maxVolume;
            instance.IsLooped = true;
            instance.Play();

        }

        public virtual void AdjustVolume(float percentage)
        {
            if(instance != null)
            {
                instance.Volume = percentage * maxVolume;
            }
        }


        public void playSound()
        {
            instance.Play();
        }


    }
}
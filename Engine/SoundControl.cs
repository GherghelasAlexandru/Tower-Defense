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
        public ContentManager content;
        public SettingsState settingsState;
        public SoundEffect bgMusicMain;
        public SoundEffectInstance instance;
        public float volume;
        public List<SoundItem> sounds= new List<SoundItem>();

        public SoundControl(string soundPath, ContentManager content, SettingsState settingsState)
        {
            this.content = content;
            this.settingsState = settingsState;

            if (soundPath != null)
            {
                ChangeMusic(soundPath);
            }
        }

        public virtual void AddSound(string name, string path, float volume)
        {
            sounds.Add(new SoundItem(name, path, volume));
        }

        public virtual void playSound(string name)
        {
            foreach(var sound in sounds)
            {
                if(sound.name == name)
                {
                    //sound.createInstance();
                    RunSound(sound.sound, sound.instance, sound.volume);
                }
            }
        }

        public void RunSound(SoundEffect sound, SoundEffectInstance instance, float volume)
        {
            FormOption soundVolume = settingsState.GetOptionValue("Sound");
            Console.WriteLine(soundVolume.GetValue());
            float soundcVolumePercent = 1.0f;
            if (soundVolume != null)
            {
                soundcVolumePercent = (float)Convert.ToDecimal(soundVolume.GetValue()) / 30.0f;
            }

            instance.Volume = soundcVolumePercent * volume;
            instance.Play();
        }

        public virtual void ChangeMusic(string soundPath)
        {
            bgMusicMain = Globals.content.Load<SoundEffect>(soundPath);
            instance = bgMusicMain.CreateInstance();
            volume = .25f;

            FormOption musicVolume = settingsState.GetOptionValue("Bg Music");
            float musicVolumePercent = 1.0f;
            if(musicVolume != null)
            {
                musicVolumePercent = (float)Convert.ToDecimal(musicVolume.GetValue())/30.0f;
            }
            AdjustVolume(musicVolumePercent);
            instance.IsLooped = true;
            instance.Play();

        }

        public virtual void stopMusic()
        {
            instance.Stop();
        }

        public virtual void AdjustVolume(float percentage)
        {
            if(instance != null)
            {
                instance.Volume = percentage * volume;
            }
        }
    }
}
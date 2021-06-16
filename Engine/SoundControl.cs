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
        public SoundItem bgMusicMain;
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

        public virtual void addSound(string name, string path, float volume)
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
            Console.WriteLine(soundVolume.value);
            float soundcVolumePercent = 1.0f;
            if (soundVolume != null)
            {
                soundcVolumePercent = (float)Convert.ToDecimal(soundVolume.value) / 30.0f;
            }

            instance.Volume = soundcVolumePercent * volume;
            instance.Play();
        }

        public virtual void ChangeMusic(string soundPath)
        {
            bgMusicMain = new SoundItem("BG main music", soundPath, .10f);
            bgMusicMain.createInstance();

            FormOption musicVolume = settingsState.GetOptionValue("Bg Music");
            //Console.WriteLine(settingsState.GetOptionValue("Bg Music"));
            float musicVolumePercent = 1.0f;
            if(musicVolume != null)
            {
                musicVolumePercent = (float)Convert.ToDecimal(musicVolume.value)/180.0f;
            }
            AdjustVolume(musicVolumePercent);
            bgMusicMain.instance.IsLooped = true;
            bgMusicMain.instance.Play();

        }

        public virtual void stopMusic()
        {
            bgMusicMain.instance.Stop();
        }

        public virtual void AdjustVolume(float percentage)
        {
            if(bgMusicMain.instance != null)
            {
                bgMusicMain.instance.Volume = percentage * bgMusicMain.volume;
            }
        }
    }
}
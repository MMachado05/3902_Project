﻿using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Project.Packages.Sounds
{
    public class SoundEffectManager
    {
        private Song dungeonMusic;
        private Song bossTheme;

        private SoundEffect swordSlash;
        private SoundEffect fireball;
        private SoundEffect boomerang;
        private SoundEffect heal;
        private SoundEffect damage;
        private SoundEffect gameOver;
        private SoundEffect deathSound;
        
        private static readonly SoundEffectManager instance = new SoundEffectManager();

        public static SoundEffectManager Instance => instance;

        // --- Osama:
        private bool isMusicEnabled = true;

        private Boolean dungeonMusicPlaying = false; // Osama: Maybe rename to "dungonMusic" or something?
        private Boolean bossThemePlaying = false;
        public void LoadContent(ContentManager content)
        {
            dungeonMusic = content.Load<Song>("sfx/Monkeys Spinning Monkeys");
            bossTheme = content.Load<Song>("sfx/Boss Theme");
            swordSlash = content.Load<SoundEffect>("sfx/swordSlash");
            fireball = content.Load<SoundEffect>("sfx/fireball");
            boomerang = content.Load<SoundEffect>("sfx/boomerang");
            heal = content.Load<SoundEffect>("sfx/heal");
            damage = content.Load<SoundEffect>("sfx/damage");
            gameOver = content.Load<SoundEffect>("sfx/gameOver");
            deathSound = content.Load<SoundEffect>("sfx/deathSound");
        }

        public void ToggleMusic()
        {
            isMusicEnabled = !isMusicEnabled;
            if (!isMusicEnabled)
            {
                // Toggling off all music tracks.
                MediaPlayer.Stop();
                dungeonMusicPlaying = false;
                bossThemePlaying = false;
            }

            /* Will need to enahance this later so when re-toggling music back on, we find the corret track */
            else
            {
                if (!dungeonMusicPlaying)
                {
                    MediaPlayer.Play(dungeonMusic);
                }
            }
        }
        public void playDungeonMusic()
        {
            if (!dungeonMusicPlaying && isMusicEnabled)
            {
                MediaPlayer.Play(dungeonMusic);
            }
            bossThemePlaying = false;
            dungeonMusicPlaying = true;
        }
        public void playBossMusic()
        {
            if (!bossThemePlaying && isMusicEnabled)
            {
                MediaPlayer.Play(bossTheme);
            }
            dungeonMusicPlaying = false;
            bossThemePlaying = true;
        }
        public void playSword()
        {
            swordSlash.Play();
        }
        public void playFireball()
        {
            fireball.Play();
        }
        public void playBoomerang()
        {
            boomerang.Play();
        }
        public void playHeal()
        {
            heal.Play();
        }
        public void playDamage()
        {
            damage.Play();
        }
        public void playGameOver()
        {
            gameOver.Play();
        }

        public void playDeathSound()
        {
            deathSound.Play();
        }

    }
}

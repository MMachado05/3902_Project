using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Project.Renderer;
using Project.Rooms;

namespace Project.Packages.Sounds
{
    public class SoundEffectManager
    {
        private GameRenderer gameRender;
        private RoomManager roomManager;
        private Song song;
        private Song bossTheme;

        // --- Osama:
        private bool isMusicEnabled = true;

        //Replace this later
        private Boolean songPlaying = false; // Osama: Maybe rename to "dungonMusic" or something?
        private Boolean bossThemePlaying = false;
        public SoundEffectManager(GameRenderer gameRender, RoomManager roomManager)
        {
            this.gameRender = gameRender;
            this.roomManager = roomManager;
        }
        public void LoadContent(ContentManager content)
        {
            song = content.Load<Song>("Monkeys Spinning Monkeys");
            bossTheme = content.Load<Song>("Boss Theme");
        }
        public void Update()
        {
            // --- Osama:
            if (!isMusicEnabled)
            {
                // I hope im using this MediaPlayer correctly lol
                if (MediaPlayer.State != MediaState.Stopped)
                {
                    MediaPlayer.Stop();
                }
                songPlaying = false;
                bossThemePlaying = false;
                return;
            }

            //Replace this later
            if (roomManager.CurrentRoomRow == 0 && roomManager.CurrentRoomColumn == 0 && songPlaying == false)
            {
                if (MediaPlayer.State != MediaState.Stopped)
                {
                    MediaPlayer.Stop(); // stop current audio playback if playing or paused.
                }
                bossThemePlaying = false;
                MediaPlayer.Play(song);
                songPlaying = true;
            }
            if (roomManager.CurrentRoomRow == 1 && roomManager.CurrentRoomColumn == 0 && bossThemePlaying == false)
            {
                if (MediaPlayer.State != MediaState.Stopped)
                {
                    MediaPlayer.Stop(); // stop current audio playback if playing or paused.
                }
                songPlaying = false;
                MediaPlayer.Play(bossTheme);
                bossThemePlaying = true;
            }
        }


        public void ToggleMusic()
        {
            isMusicEnabled = !isMusicEnabled;
            if (!isMusicEnabled)
            {
                // Toggling off all music tracks.
                MediaPlayer.Stop();
                songPlaying = false;
                bossThemePlaying = false;
            }
        }

    }
}

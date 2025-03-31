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
        public SoundEffectManager(GameRenderer gameRender, RoomManager roomManager)
        {
            this.gameRender = gameRender;
            this.roomManager = roomManager;
        }
        void LoadContent(ContentManager content)
        {
            song = content.Load<Song>("Diddy Monster Theme");
        }
        public void Update()
        {
            if (MediaPlayer.State != MediaState.Stopped)
            {
                MediaPlayer.Stop(); // stop current audio playback if playing or paused.

                if (roomManager.CurrentRoomRow == 0 && roomManager.CurrentRoomColumn == 1)
                {
                    MediaPlayer.Play(song);
                }
            }
        }
    }
}

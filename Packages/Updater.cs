using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.rooms;

namespace Project
{
    /// <summary>
    /// Utility class for centralizing update logic.
    /// </summary>
    public class Updater
    {
        private RoomManager _roomManager;
        private Player _player;
        private List<IController> _controllers;

        public Updater(RoomManager roomManager, Player player)
        {
            this._player = player;
            this._roomManager = roomManager;
            this._controllers = new List<IController>();
        }

        public void RegisterController(IController controller)
        {
            this._controllers.Add(controller);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IController c in this._controllers)
            {
                c.Update();
            }

            this._player.Update(gameTime); // Keep this here because update logic
                                           // might change *outside* of a room
            this._roomManager.Update(gameTime);
        }
    }
}

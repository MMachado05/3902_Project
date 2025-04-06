using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Characters;
using Project.Commands;
using Project.Controllers;
using Project.Packages.Sounds;
using Project.Rooms;

namespace Project
{
    /// <summary>
    /// Utility class for centralizing update logic.
    /// </summary>
    public class Updater
    {
        private RoomManager _roomManager;
        private Player _player;
        private ICommand _restartCommand;
        private List<IController> _controllers;

        public Updater(RoomManager roomManager, Player player, ICommand restart)
        {
            this._player = player;
            this._roomManager = roomManager;
            this._restartCommand = restart;
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

            if (_player.health <= 0)
            {
                _restartCommand.Execute();
                SoundEffectManager.Instance.playGameOver();
            }
        }
    }
}

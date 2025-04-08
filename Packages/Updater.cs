using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Characters;
using Project.Commands;
using Project.Controllers;
using Project.Rooms;
using Project.Rooms.Blocks;
using Project.Rooms.Blocks.ConcreteClasses;

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
        private List<IController> _rooms;


        public Updater(RoomManager roomManager, Player player, ICommand restart)
        {
            this._player = player;
            this._roomManager = roomManager;
            this._restartCommand = restart;
            this._controllers = new List<IController>();
            this._rooms = new List<IController>();

        }

        public void RegisterController(IController controller)
        {
            this._controllers.Add(controller);
        }
        public void RegisterRoomCommands(IController controller)
        {
            _rooms.Add(controller);
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
            IBlock door = _roomManager.GetCurrentRoom().currentDoor();
            if ((this._player.Location.X > door.Location.X) && door.SwitchRoom)
            {
                _rooms[1].Update();
                _roomManager.GetCurrentRoom().currentDoor().SwitchRoom = false;

            }
            else if ((this._player.Location.X < door.Location.X) && door.SwitchRoom)
            {
                _rooms[0].Update();
                _roomManager.GetCurrentRoom().currentDoor().SwitchRoom = false;
            }
            else if ((this._player.Location.Y < door.Location.Y) && door.SwitchRoom)
            {
                _rooms[2].Update();
                _roomManager.GetCurrentRoom().currentDoor().SwitchRoom = false;
            }
            else if ((this._player.Location.Y > door.Location.Y) && door.SwitchRoom)
            {
                _rooms[3].Update();
                _roomManager.GetCurrentRoom().currentDoor().SwitchRoom = false;
            }



            if (_player.health <= 0)
            {
                _restartCommand.Execute();
            }
        }
    }
}

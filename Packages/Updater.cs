using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Characters;
using Project.Commands;
using Project.Controllers;
using Project.Packages.Sounds;
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
        private GameStateMachine _gameState;
        private List<IController> _rooms;


        public Updater(RoomManager roomManager, Player player, ICommand restart, GameStateMachine gameState)
        {
            this._player = player;
            this._roomManager = roomManager;
            this._restartCommand = restart;
            this._gameState = gameState;
            this._controllers = new List<IController>();
            this._rooms = new List<IController>();

        }

        public void RegisterController(IController controller)
        {
            // TODO: It would be advisable to "lock" the player during certain game states,
            // so it might be good to register controllers along with GameState enums,
            // and then add them to a dictionary where the enum is the key. Then, we could,
            // instead of doing the if statement, just update the controllers in the Dictionary
            // slot matching the current game state
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


            if (this._gameState.State == GameState.Playing)
            {
                this._player.Update(gameTime); // Keep this here because update logic might change *outside* of a room
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
                    //SoundEffectManager.Instance.playGameOver();
                    SoundEffectManager.Instance.playDeathSound();
                }
            }
        }
    }
}

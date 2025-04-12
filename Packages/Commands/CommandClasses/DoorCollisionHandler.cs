using System.Collections.Generic;
using Project.Characters;
using Project.Commands;
using Project.Commands.RoomCommands;
using Project.Rooms;
using Project.Rooms.Blocks;
using Project.Rooms.Blocks.ConcreteClasses;
using static Project.Rooms.Blocks.ConcreteClasses.DoorBlock;

namespace Project.Packages.Commands.CommandClasses
{
    public class DoorCollisionHandler
    {
        private RoomManager _roomManager;
        private Player _player;
        private ICommand _roomChangeCommand;

        public DoorCollisionHandler(RoomManager roomManager, Player player, ICommand roomChangeCommand)
        {
            _roomManager = roomManager;
            _player = player;
            _roomChangeCommand = roomChangeCommand;
        }

        public void HandleCollisionsWithDoors()
        {
            List<IBlock> doors = _roomManager.GetCurrentRoom().currentDoor(); // Always get the current doors

            foreach (DoorBlock door in doors)
            {
                if (door.Location.Intersects(_player.Location) && door.SwitchRoom)
                {
                    //door.SwitchRoom = false;
                    SwitchRoom(door);
                    door.SwitchRoom = false;
                }
            }
        }

        private void SwitchRoom(DoorBlock door)
        {
            // Determine the direction to switch the room based on player and door position
            if (door.Direction == DoorDirection.Right)
            {
                _roomChangeCommand = new RoomRightCommand(_roomManager);
            }
            else if (door.Direction == DoorDirection.Left)
            {
                _roomChangeCommand = new RoomLeftCommand(_roomManager);
            }
            else if (door.Direction == DoorDirection.Down)
            {
                _roomChangeCommand = new RoomDownCommand(_roomManager);
            }
            else if (door.Direction == DoorDirection.Up)
            {
                _roomChangeCommand = new RoomUpCommand(_roomManager);
            }

            _roomChangeCommand.Execute();
        }

    }
}
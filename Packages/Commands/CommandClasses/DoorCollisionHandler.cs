using System.Collections.Generic;
using Project.Characters;
using Project.Commands;
using Project.Commands.RoomCommands;
using Project.Rooms;
using Project.Rooms.Blocks;
using Project.Rooms.Blocks.ConcreteClasses;

namespace Project.Packages.Commands.CommandClasses
{
    public class DoorCollisionHandler
    {
        private RoomManager RoomManager;
        private Player _player;
        private ICommand _roomChangeCommand;

        public DoorCollisionHandler(RoomManager roomManager, Player player, ICommand roomChangeCommand)
        {
            RoomManager = roomManager;
            _player = player;
            _roomChangeCommand = roomChangeCommand;
        }

        public void HandleCollisionsWithDoors()
        {
            List<IBlock> doors = RoomManager.GetCurrentRoom().GetCurrentDoors(); 

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
            if (door.Direction == Direction.Right)
            {
                _roomChangeCommand = new RoomRightCommand(RoomManager);
            }
            else if (door.Direction == Direction.Left)
            {
                _roomChangeCommand = new RoomLeftCommand(RoomManager);
            }
            else if (door.Direction == Direction.Down)
            {
                _roomChangeCommand = new RoomDownCommand(RoomManager);
            }
            else if (door.Direction == Direction.Up)
            {
                _roomChangeCommand = new RoomUpCommand(RoomManager);
            }

            _roomChangeCommand.Execute();
        }

    }
}

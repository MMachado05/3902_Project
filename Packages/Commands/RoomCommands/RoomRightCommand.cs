using Project.Rooms;

namespace Project.Commands.RoomCommands
{
    class RoomRightCommand : ICommand
    {
        private RoomManager _roomManager;

        public RoomRightCommand(RoomManager rm)
        {
            this._roomManager = rm;
        }

        public void Execute()
        {
            this._roomManager.GotoRoomToRight();
        }
    }
}

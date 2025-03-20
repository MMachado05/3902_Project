using Project.rooms;

namespace Project
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

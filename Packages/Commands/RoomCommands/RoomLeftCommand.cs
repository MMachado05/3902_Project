using Project.rooms;

namespace Project
{
    class RoomLeftCommand : ICommand
    {
        private RoomManager _roomManager;

        public RoomLeftCommand(RoomManager rm)
        {
            this._roomManager = rm;
        }

        public void Execute()
        {
            this._roomManager.GotoRoomToLeft();
        }
    }
}

using Project.rooms;

namespace Project
{
    class RoomDownCommand : ICommand
    {
        private RoomManager _roomManager;

        public RoomDownCommand(RoomManager rm)
        {
            this._roomManager = rm;
        }

        public void Execute()
        {
            this._roomManager.GotoRoomBelow();
        }
    }
}

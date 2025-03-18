using Project.rooms;

namespace Project
{
    class RoomUpCommand : ICommand
    {
        private RoomManager _roomManager;

        public RoomUpCommand(RoomManager rm)
        {
            this._roomManager = rm;
        }

        public void Execute()
        {
            this._roomManager.GotoRoomAbove();
        }
    }
}

using Project.Rooms;

namespace Project.Commands.RoomCommands
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

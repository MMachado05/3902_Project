using Project.rooms;

namespace Project.Commands
{
    public class NextRoomCommand : ICommand
    {

        private RoomManager Manager;
        public NextRoomCommand(RoomManager manager)
        {
            Manager = manager;
        }
        public void Execute()
        {
            Manager.SwitchToNextRoom();
        }
    }

}

using Project.rooms;

namespace Project.Commands
{
    public class NextRoomCommand : ICommand
    {
    
        private  RoomsManager Manager;
        public NextRoomCommand(RoomsManager manager)
        {
            Manager = manager;
        }
        public void Execute()
        {
            Manager.SwitchToNextRoom();
        }
    }
        
    }

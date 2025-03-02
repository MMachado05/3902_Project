using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Blocks;
using Project.Packages;

namespace Project.rooms
{
    public class RoomsManager
    {
        private List<IRoom> RoomsList;
        private int currentRoomIndex;

        public RoomsManager(SolidBlockManager manager)
        {
            RoomsList = new List<IRoom>
            {new LevelOneRoom(manager),new LevelTwoRoom(manager)
            };
            currentRoomIndex = 0;
        }

        public void SwitchToPreviousRoom()
        {
            if (RoomsList.Count == 0) return;
            if (currentRoomIndex <= 0)
                currentRoomIndex = RoomsList.Count - 1;
            else
                currentRoomIndex--;
        }

        public void SwitchToNextRoom()
        {
            if (RoomsList.Count == 0) return;
            if (currentRoomIndex >= RoomsList.Count - 1)
                currentRoomIndex = 0;
            else
                currentRoomIndex++;
        }

        public IRoom GetCurrentRoom()
        {
            return RoomsList.Count > 0 ? RoomsList[currentRoomIndex] : null;
        }
    }
}

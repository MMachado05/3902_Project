using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Project.Blocks;
using Project.Enemies;
using Project.Enemies.EnemyClasses;
using Project.Packages;

namespace Project.rooms
{
    public class RoomsManager
    {
        // NOTE: From Boggus: consider using 2D Array or Graph data structure
        // to store rooms,  so as to have built-in adjacency
        public List<IRoom> RoomsList{get;set;}
        public int currentRoomIndex{get;set;}

        public RoomsManager(SolidBlockManager manager,EnemyManager enemyManager, Game1 game)
        {
            
            RoomsList = new List<IRoom>
            {new LevelOneRoom(manager,enemyManager,game),new LevelTwoRoom(manager,enemyManager,game)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Project.Enemies;
using Project.Items;
using Project.Packages.Characters;
using Project.Rooms.Blocks;

namespace Project.Packages.Rooms
{
    public class RoomData
    {
        public CollisionManager CollisionManager { get; set; }
        public ItemManager ItemManager { get; set; }
        public EnemyManager EnemyManager { get; set; }
        public Rectangle PlayerStartLocation { get; set; }
        public IBlock[,] BlockMap { get; set; }
        public IBlock Background { get; set; }
        public IBlock MiniMap { get; set; }
        public string RoomName { get; set; }
    }
}

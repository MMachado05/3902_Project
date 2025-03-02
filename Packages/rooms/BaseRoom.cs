using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Enemies;
using Project.rooms;
using Project.Blocks;
namespace Project.Packages
{
    public abstract class BaseRoom : IRoom
    {
        private SolidBlockManager manager;

        public Dictionary<Vector2, String> room;
        public RoomParser parser;
        public BaseRoom(SolidBlockManager manager)
        {
            this.manager = manager;
            parser = new RoomParser();
            room = parser.loadRoom("../../../Data/room1.csv");

        }
        public List<Object> roomMap()
        {
            List<object> result = new List<object>();
            foreach (var item in room)
            {
                Rectangle dest;
                SolidBlock block;

                switch (item.Value)
                {
                    case "bl":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.boardersBrick(dest);
                        result.Add(block);
                        break;
                    case "ob":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.obstacleBlock(dest);
                        result.Add(block);


                        break;
                    case "dr":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = manager.doorBlock(dest);
                        result.Add(block);
                        break;
                    case "en":
                        break;
                    case "pl":
                        break;
                }

            }
            return result;
        }
        public void Draw()
        {
            List<object> itemList = roomMap();

            foreach (var items in itemList)
            {
                switch (items)
                {
                    case SolidBlock solidBlock:
                        solidBlock.Draw();
                        break;
                    case Player player:
                        Console.WriteLine("p");
                        break;
                    case IItem item:
                        Console.WriteLine("I");

                        // nothing 
                        break;
                    case IEnemy:
                        Console.WriteLine("E");

                        // nothing
                        break;
                    default:

                        break;
                }

            }

        }


    }
}
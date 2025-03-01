using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Project.Blocks;
using Project.Enemies;
using Project.Packages.Items;
using Project.rooms;
namespace Project.Packages
{
    public class LevelOneRoom : IRoom
    {
        SolidBlockManager blocks;
        private Dictionary<Vector2, String> room;
        RoomParser parser;
        public LevelOneRoom(SolidBlockManager blocks)
        {
            this.blocks = blocks;
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
                        block = blocks.boardersBrick(dest);
                        result.Add(block);
                        break;
                    case "ob":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = blocks.obstacleBlock(dest);
                        result.Add(block);


                        break;
                    case "dr":
                        dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                        block = blocks.doorBlock(dest);
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
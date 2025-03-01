using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Project.Blocks;
using Project.rooms;
namespace Project.Packages
{
    public class LevelOneRoom : IRoom
    {
        SolidBlockManager blocks;
        private Dictionary<Vector2,String> room;
        RoomParser parser;
        public LevelOneRoom( SolidBlockManager blocks)
        {
            this.blocks = blocks;
            parser = new RoomParser();
            room = parser.loadRoom("../../../Data/room1.csv");
            
        }
        public List<SolidBlock> roomMap(){
            List <SolidBlock> result = new List<SolidBlock>();
            foreach(var item in room){
                Rectangle dest = new ((int)item.Key.X*32,(int)item.Key.Y*32,32,32);
                SolidBlock block =blocks.boardersBrick(dest);
                result.Add(block);
            }
            return result ;
        }
        public void Draw(){
            List<SolidBlock> blockslist = roomMap();

            foreach(var block in blockslist){
                block.Draw();
            }

            /*
            List <SolidBlock> blocksList = blocks.topBoarders();
            foreach(var block in blocksList){
                block.Draw();
            }
            blocksList = blocks.bottomBoarders();
             foreach(var block in blocksList){
                block.Draw();
            }
            blocksList = blocks.rightBoarders();
             foreach(var block in blocksList){
                block.Draw();
            }
            blocksList = blocks.leftBoarders();
             foreach(var block in blocksList){
                block.Draw();
            }
            blocksList = blocks.obstacles();
             foreach(var block in blocksList){
                block.Draw();
            }*/
        }
        
      
    }
}
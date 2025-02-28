using System.Collections.Generic;
using Project.Blocks;
using Project.rooms;
namespace Project.Packages
{
    public class LevelOneRoom : IRoom
    {
        SolidBlockManager blocks;
        public LevelOneRoom( SolidBlockManager blocks)
        {
            this.blocks = blocks;
        }
        public void Draw(){
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
            }
        }
        
      
    }
}
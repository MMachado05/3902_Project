using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Blocks;

namespace Project.Packages
{
    public class LevelTwoRoom : BaseRoom
    {
        public LevelTwoRoom(SolidBlockManager blocks) : base(blocks)
        {
            room = parser.loadRoom("../../../Data/room2.csv");
        }
    }
}
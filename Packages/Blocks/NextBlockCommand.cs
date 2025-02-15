using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Blocks
{
    public class NextBlockCommand : ICommand
    {
        private Game1 game1;
         public NextBlockCommand(Game1 game)
        {
            this.game1 = game;
           
        }
        public void Execute()
        {
            game1.nextBlock();
        }
    }
}
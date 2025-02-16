using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Blocks
{
    public class PreviousBlockCommand : ICommand
    {
        private SolidBlockManager Manager;
        public PreviousBlockCommand(SolidBlockManager manager)
        {
            Manager = manager;

        }
        public void Execute()
        {
            Manager.SwitchToPreviousBlock();
        }
    }
}
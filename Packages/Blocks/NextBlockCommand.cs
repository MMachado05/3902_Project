namespace Project.Blocks
{
    public class NextBlockCommand : ICommand
    {
        private SolidBlockManager Manager;
        public NextBlockCommand(SolidBlockManager manager)
        {
            Manager = manager;
        }
        public void Execute()
        {
            //Manager.SwitchToNextBlock();
        }
    }
}
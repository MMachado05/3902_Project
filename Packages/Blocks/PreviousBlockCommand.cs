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
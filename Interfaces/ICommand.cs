namespace Project
{
    public interface ICommand
    {
        /// <summary>
        /// Each implementation of ICommand will ask for certain parameters in its constructor
        /// so that it can appropriately execute its command (be it a Game object, Character
        /// object, etc.)
        /// </summary>
        void Execute();
    }
}

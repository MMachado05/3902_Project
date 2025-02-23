namespace Project
{
    public interface ICommand
    {
        /// <summary>
        /// Execute the contents of this Command. Exact execution behaviors will depend
        /// on the implementation of this function, and the composited objects associated
        /// with the Command object.
        /// </summary>
        void Execute();
    }
}

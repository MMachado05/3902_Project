namespace Project.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        protected Game1 game;

        public AbstractCommand(Game1 game)
        {
            this.game = game;
        }

        public abstract void Execute();
    }
}

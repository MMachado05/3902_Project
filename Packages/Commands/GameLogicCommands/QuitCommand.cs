namespace Project.Commands.GameLogicCommands
{
    public class QuitCommand : ICommand
    {
        private Game1 _game1;
        public QuitCommand(Game1 game)
        {
            _game1 = game;
        }

        public void Execute()
        {
            _game1.Exit();
        }
    }
}

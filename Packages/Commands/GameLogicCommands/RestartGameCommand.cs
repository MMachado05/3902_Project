namespace Project.Commands
{
    public class RestartGameCommand : ICommand
    {
        private Game1 _game;
        public RestartGameCommand(Game1 game)
        {
            _game = game;

        }

        public void Execute()
        {

            _game.restart();


        }
    }
}
namespace Project
{
    public class AttackCommand : ICommand
    {
        private Game1 _game;

        public AttackCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Attack();
        }
    }
}

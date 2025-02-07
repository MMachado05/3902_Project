namespace Project
{
    // Move Character DOWN
    public class SetCharacterMoveDown : ICommand
    {
        private CharacterManager _characterManager;
        private int _printscale;
        public SetCharacterMoveDown(Game1 game)
        {
            _characterManager = game.CharacterManager;
            _printscale = game.Printscale;
        }

        public void Execute()
        {
            _characterManager.ReplaceCharacter(CharacterManager.CharacterState.Animated, Movement.Direction.Down, _printscale);
        }
    }
}
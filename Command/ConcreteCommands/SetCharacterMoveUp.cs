namespace Project
{
    // Move Character UP
    public class SetCharacterMoveUp : ICommand
    {
        private CharacterManager _characterManager;
        private int _printscale;
        public SetCharacterMoveUp(Game1 game)
        {
            _characterManager = game.CharacterManager;
            _printscale = game.Printscale;
        }

        public void Execute()
        {
            _characterManager.ReplaceCharacter(CharacterManager.CharacterState.Animated, Movement.Direction.Up, _printscale);
        }
    }
}
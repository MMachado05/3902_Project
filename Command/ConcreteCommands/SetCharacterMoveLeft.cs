namespace Project
{
    // Move Character LEFT
    public class SetCharacterMoveLeft : ICommand
    {
        private CharacterManager _characterManager;
        private int _printscale;

        public SetCharacterMoveLeft(Game1 game)
        {
            _characterManager = game.CharacterManager;
            _printscale = game.Printscale;
        }

        public void Execute()
        {
            _characterManager.ReplaceCharacter(CharacterManager.CharacterState.Animated, Movement.Direction.Left, _printscale);
        }
    }
}
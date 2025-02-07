namespace Project
{
    // Move Character RIGHT
    public class SetCharacterMoveRight : ICommand
    {
        private CharacterManager _characterManager;
        private int _printscale;

        public SetCharacterMoveRight(Game1 game)
        {
            _characterManager = game.CharacterManager;
            _printscale = game.Printscale;
        }

        public void Execute()
        {
            _characterManager.ReplaceCharacter(CharacterManager.CharacterState.Animated, Movement.Direction.Right, _printscale);
        }
    }
}
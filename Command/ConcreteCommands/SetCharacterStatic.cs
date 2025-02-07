namespace Project
{
    public class SetCharacterStatic : ICommand
    {
        private CharacterManager _characterManager;
        private int _printscale;
        public SetCharacterStatic(Game1 game)
        {
            _characterManager = game.CharacterManager;
            _printscale = game.Printscale;
        }

        public void Execute()
        {
            _characterManager.ReplaceCharacter(CharacterManager.CharacterState.Static, _printscale);
        }

    }
}

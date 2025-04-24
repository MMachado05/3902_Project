using Project.Commands;

namespace Project.Packages.Commands.GameLogicCommands
{
    public class StartGameCommand : ICommand
    {
        private GameStateMachine _gameState;

        public StartGameCommand(GameStateMachine gameState)
        {
            this._gameState = gameState;
        }

        public void Execute()
        {
            this._gameState.StartGame();
        }
    }
}

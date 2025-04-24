using Project.Commands;

namespace Project.Packages.Commands.GameLogicCommands
{
    public class PauseGameCommand : ICommand
    {
        private GameStateMachine _gameState;
        private object _pauseDesktop;

        public PauseGameCommand(GameStateMachine gameState)
        {
            this._gameState = gameState;
        }

        public void Execute()
        {
            this._gameState.TogglePause();
        }
    }
}

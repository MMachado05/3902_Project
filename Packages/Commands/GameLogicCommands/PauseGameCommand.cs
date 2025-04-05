using Project.Commands;
using System.Diagnostics;

namespace Project.Packages.Commands.GameLogicCommands
{
    public class PauseGameCommand : ICommand
    {
        private GameStateMachine _gameState;

        public PauseGameCommand(GameStateMachine gameState)
        {
            this._gameState = gameState;
        }

        public void Execute()
        {
            if (this._gameState.State == GameState.Paused)
            {
                this._gameState.State = GameState.Playing;
                Debug.WriteLine("Game is now playing.");
            }
            else
            {
                this._gameState.State = GameState.Paused;
                Debug.WriteLine("Game is now paused.");
            }
        }
    }
}

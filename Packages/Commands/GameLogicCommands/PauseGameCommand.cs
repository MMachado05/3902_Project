using Project.Commands;
using System.Diagnostics;

namespace Project.Packages.Commands.GameLogicCommands
{
    public class PauseGameCommand : AbstractCommand
    {
        public PauseGameCommand(Game1 game) : base(game)
        {
        }

        public override void Execute()
        {
            if (game.State == GameState.Paused)
            {
                game.State = GameState.Playing;
                Debug.WriteLine("Game is now playing.");
            }
            else
            {
                game.State = GameState.Paused;
                Debug.WriteLine("Game is now paused.");
            }
        }
    }
}

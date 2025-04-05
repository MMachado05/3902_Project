using Project.Packages.Commands;
using Project;
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
            // Toggle IsPaused after every press
            Game1.IsPaused = !Game1.IsPaused;

            if (Game1.IsPaused)
            {
                // TODO: Implement UI using Myra; for now just use OOTB text boxes
                Debug.WriteLine("P is pressed");
            }
            else
            {
                // TODO: Hide the pause menu UI...
            }
        }
    }
}

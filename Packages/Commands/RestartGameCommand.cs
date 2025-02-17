
using Microsoft.Xna.Framework.Graphics;
using Project.Blocks;
using Project.Enemies;

namespace Project.Commands
{
    public class RestartGameCommand : ICommand
    {
        private Game1 _game;
        private SpriteBatch spriteBatch;
        public static RestartGameCommand instance;
        public RestartGameCommand(Game1 game, SpriteBatch spriteBatch)
        {
            _game = game;
            this.spriteBatch = spriteBatch;

        }

        public void Execute()
        {

            _game.restart();


        }
    }
}
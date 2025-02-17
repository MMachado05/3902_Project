
using Microsoft.Xna.Framework.Graphics;
using Project.Blocks;
using Project.Enemies;

namespace Project.Commands
{
    public class RestartGameCommand : ICommand
    {
        private Game1 _game;
        private SpriteBatch spriteBatch;
        private RestartGameCommand(Game1 game, SpriteBatch spriteBatch)
        {
            _game = game;
            this.spriteBatch = spriteBatch;
        }

        public void Execute()
        {

            _game.player = new Player();
            _game.enemyManager = new EnemyManager();
            _game.manager = new SolidBlockManager(spriteBatch);

        }
    }
}
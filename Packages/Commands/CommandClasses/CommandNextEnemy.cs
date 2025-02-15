using Project.Enemies;

namespace Project.Commands.CommandClasses
{
    public class CommandNextEnemy : AbstractCommand
    {
        private EnemyManager enemyManager;

        public CommandNextEnemy(Game1 game, EnemyManager enemyManager) : base(game)
        {
            this.enemyManager = enemyManager;
        }

        public override void Execute()
        {
            enemyManager.SwitchToNextEnemy();
        }
    }
}

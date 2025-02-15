using Project.Enemies;

namespace Project.Commands.CommandClasses
{
    public class CommandPreviousEnemy : AbstractCommand
    {
        private EnemyManager enemyManager;

        public CommandPreviousEnemy(Game1 game, EnemyManager enemyManager) : base(game)
        {
            this.enemyManager = enemyManager;
        }

        public override void Execute()
        {
            enemyManager.SwitchToPreviousEnemy();
        }
    }
}

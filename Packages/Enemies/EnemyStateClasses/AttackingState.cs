using Project.Items;

namespace Project.Enemies.EnemyStateClasses
{
    public class AttackingState : IEnemyState
    {
        private float timer;
        private bool hasAttacked;

        public bool IsDone => timer >= 4;
        public StateId Id => StateId.Attacking;

        public void Execute(IEnemy enemy, ItemManager itemManager)
        {
            timer += 0.1f;

            if (!hasAttacked)
            {
                enemy.Attack(itemManager);
                hasAttacked = true;
            }

            if (IsDone)
                enemy.ResetAttackState();
        }
    }

}


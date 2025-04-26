using Project.Items;

namespace Project.Enemies.EnemyStateClasses
{
    public class AttackingState : IEnemyState
    {
        private float timer;
        private bool hasAttacked;
        private bool done;

        public StateId Id => StateId.Attacking;
        public bool IsDone => done;

        public void Execute(IEnemy enemy, float deltaTime, ItemManager itemManager = null)
        {
            timer += deltaTime;

            if (!hasAttacked)
            {
                enemy.Attack(itemManager);
                hasAttacked = true;
            }

            if (timer >= enemy.GetAttackDuration())
            {
                enemy.ResetAttackState();
                done = true;
            }
        }
    }
}

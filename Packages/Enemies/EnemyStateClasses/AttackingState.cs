namespace Project.Enemies.EnemyStateClasses
{
    public class AttackingState : IEnemyState
    {
        private float timer;
        private bool hasAttacked;

        public bool IsDone => timer >= 4;
        public StateId Id => StateId.Attacking;

        public void Execute(IEnemy enemy)
        {
            timer += 0.1f;

            if (!hasAttacked)
            {
                enemy.Attack();
                hasAttacked = true;
            }

            if (IsDone)
                enemy.ResetAttackState();
        }
    }

}


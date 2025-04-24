using Project.Enemies.EnemyClasses;
using Project.Items;

namespace Project.Enemies.EnemyStateClasses
{
    public class IdleState : IEnemyState
    {
        private float timer;

        public bool IsDone => timer > 1f;
        public StateId Id => StateId.Idle;

        public void Execute(IEnemy enemy, ItemManager itemManager)
        {
            timer += 0.1f;

            if (enemy is Enemy e)
                e.SetIdleAnimation();
        }
    }
}


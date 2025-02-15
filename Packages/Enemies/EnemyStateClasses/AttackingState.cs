using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class AttackingState : IEnemyState
    {
        private float attackDuration = 0;
        private float attackFrameTime = 1f;

        public void Update(IEnemy enemy)
        {
            attackDuration += 0.1f;

            if (enemy is Enemy e)
            {
                e.SetAttackAnimation();
            }

            if (attackDuration >= attackFrameTime * 4)
            {
                attackDuration = 0;
                enemy.SetState(new IdleState());
            }
        }
    }
}

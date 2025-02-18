using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class AttackingState : IEnemyState
    {
        private float attackDuration = 0;
        private const float AttackFrameTime = 1f;
        private bool hasAttacked = false;

        public void Update(IEnemy enemy)
        {
            attackDuration += 0.1f;

            if (!hasAttacked)
            {
                enemy.Attack();
                hasAttacked = true;
            }

            if (attackDuration >= enemy.GetAttackDuration() * 4)
            {
                ResetAttack(enemy);
            }
        }

        private void ResetAttack(IEnemy enemy)
        {
            attackDuration = 0;
            hasAttacked = false;
            enemy.ResetAttackState();
            enemy.SetState(new IdleState());
        }
    }
}

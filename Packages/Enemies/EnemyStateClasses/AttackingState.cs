using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class AttackingState : IEnemyState
    {
        private float attackDuration = 0;
        private float attackFrameTime = 1f;

        private bool hasAttacked = false;

        public void Update(IEnemy enemy)
        {
            attackDuration += 0.1f;
            if (enemy is Enemy e)
            {
                // e.SetAttackAnimation(); // will use this when using sprite sheets with attack sequences

                if (!hasAttacked && enemy is Dragon dragon)
                {
                    dragon.ShootProjectiles();
                    hasAttacked = true;
                }
            }

            if (attackDuration >= attackFrameTime * 4)
            {
                attackDuration = 0;
                hasAttacked = false;

                if (enemy is Dragon dragonReset)
                {
                    dragonReset.hasShot = false;
                }

                enemy.SetState(new IdleState());
            }
        }

    }
}

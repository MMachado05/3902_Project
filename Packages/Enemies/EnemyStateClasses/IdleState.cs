using System;
using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class IdleState : IEnemyState
    {
        private float timer = 0;
        private static readonly Random random = new Random();

        public void Update(IEnemy enemy)
        {
            timer += 0.25f;

            if (enemy is Enemy e)
            {
                e.SetIdleAnimation();
            }

            if (timer > 6)
            {
                timer = 0;
                if (random.Next(0, 5) == 0)
                {
                    enemy.SetState(new AttackingState());
                }
                else
                {
                    enemy.SetState(new MovingState());
                }
            }
        }
    }
}

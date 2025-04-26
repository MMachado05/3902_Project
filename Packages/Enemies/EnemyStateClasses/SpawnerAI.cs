using System;

namespace Project.Enemies.EnemyStateClasses
{
    public class SpawnerAI : IEnemyAI
    {
        private static readonly Random rand = new Random();

        public IEnemyState DecideNextState(IEnemy enemy, IEnemyState current, float deltaTime)
        {
            if (rand.NextDouble() < 0.5)
            {
                return new AttackingState();
            }
            else
            {
                return new IdleState();
            }
        }
    }
}

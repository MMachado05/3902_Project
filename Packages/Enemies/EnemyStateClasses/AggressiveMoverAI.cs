using System;

namespace Project.Enemies.EnemyStateClasses
{
    public class AggressiveMoverAI : IEnemyAI
    {
        private static readonly Random rng = new Random();

        public IEnemyState DecideNextState(IEnemy enemy, IEnemyState current, float deltaTime)
        {
            if (current.IsDone)
            {
                if (current.Id == StateId.Idle)
                {
                    // After idle, always move
                    return new MovingState(enemy);
                }

                return rng.NextDouble() < 0.9
                    ? new MovingState(enemy) // 90% move
                    : new IdleState();        // 10% idle
            }

            return current;
        }
    }
}

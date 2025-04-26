using System;

namespace Project.Enemies.EnemyStateClasses
{
    public class SimpleRandomAI : IEnemyAI
    {
        private static readonly Random rng = new Random();

        public IEnemyState DecideNextState(IEnemy enemy, IEnemyState current, float deltaTime)
        {
            if (current.IsDone)
            {
                int roll = rng.Next(0, 10); // 0–9
                if (roll == 0) // 10% chance
                    return new IdleState();
                else if (roll <= 6) // 70% chance
                    return new MovingState(enemy);
                else // 20% chance
                    return new AttackingState();
            }

            return current;
        }
    }
}

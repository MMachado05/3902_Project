using System;

namespace Project.Enemies.EnemyStateClasses
{
    public class SimpleRandomAI : IEnemyAI
    {
        private float timer;
        private readonly Random rng = new();

        public IEnemyState DecideNextState(IEnemy enemy, IEnemyState current)
        {
            timer += 0.1f;

            if (current.Id == StateId.Idle && timer > 1f)
            {
                timer = 0f;
                return rng.Next(0, 5) == 0
                    ? new AttackingState()
                    : new MovingState(enemy);
            }

            return new IdleState();
        }
    }
}

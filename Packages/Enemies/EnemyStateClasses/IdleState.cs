using Project.Items;
using System;
using Project.Characters;

namespace Project.Enemies.EnemyStateClasses
{
    public class IdleState : IEnemyState
    {
        private float timer;
        private readonly float duration;
        private static readonly Random rng = new Random();

        public StateId Id => StateId.Idle;
        public bool IsDone => timer >= duration;

        public IdleState()
        {
            // Idle lasts between 0.05 and 0.15 seconds
            duration = 0.05f + (float)rng.NextDouble() * 0.1f;
        }

        public void Execute(IEnemy enemy, float deltaTime, ItemManager itemManager = null)
        {
            timer += deltaTime;
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Project.Characters;
using Project.Enemies;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class MovingState : IEnemyState
    {
        private readonly Direction moveDirection;
        private readonly float duration;
        private float timer;

        public bool IsDone => timer > duration;
        public StateId Id => StateId.Moving;

        public MovingState(IEnemy enemy)
        {
            var rng = new Random();
            duration = rng.Next(2, 13);
            moveDirection = GenerateDirection(enemy, rng);
        }

        public void Execute(IEnemy enemy)
        {
            timer += 0.1f;

            if (enemy is Enemy e)
                e.MoveInDirection(moveDirection);
        }

        private Direction GenerateDirection(IEnemy enemy, Random rng)
        {
            if (enemy is Aquamentus)
                return rng.Next(2) == 0 ? Direction.Left : Direction.Right;

            bool horizontal = rng.Next(2) == 0;
            if (horizontal)
                return rng.Next(2) == 0 ? Direction.Left : Direction.Right;
            else
                return rng.Next(2) == 0 ? Direction.Up : Direction.Down;
        }
    }
}

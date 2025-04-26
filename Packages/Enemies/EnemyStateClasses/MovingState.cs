using System;
using System.Collections.Generic;
using Project.Characters;
using Project.Items;

namespace Project.Enemies.EnemyStateClasses
{
    public class MovingState : IEnemyState
    {
        private readonly Direction moveDirection;
        private readonly float duration;
        private float timer;
        private static readonly Random rng = new Random();

        private bool forceDone = false;

        public StateId Id => StateId.Moving;
        public bool IsDone => timer >= duration || forceDone;

        public MovingState(IEnemy enemy, Direction? forcedDirection = null)
        {
            duration = 2.0f + (float)rng.NextDouble() * 3.0f;

            if (forcedDirection.HasValue)
            {
                moveDirection = forcedDirection.Value;
            }
            else
            {
                moveDirection = GenerateDirection(enemy.PossibleMovementDirections(), rng);
            }
        }

        public void Execute(IEnemy enemy, float deltaTime, ItemManager itemManager = null)
        {
            timer += deltaTime;
            enemy.MoveInDirection(moveDirection);
        }

        private Direction GenerateDirection(List<Direction> validDirections, Random rng)
        {
            return validDirections[rng.Next(validDirections.Count - 1)];
        }

        public void ForceEnd()
        {
            forceDone = true;
        }
    }
}

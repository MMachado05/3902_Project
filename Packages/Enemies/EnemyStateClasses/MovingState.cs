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

        public StateId Id => StateId.Moving;
        public bool IsDone => timer >= duration;

        public MovingState(IEnemy enemy)
        {
            duration = 2.0f + (float)rng.NextDouble() * 3.0f;
            moveDirection = PickDirection(enemy.PossibleMovementDirections());
        }

        public void Execute(IEnemy enemy, float deltaTime, ItemManager itemManager = null)
        {
            timer += deltaTime;
            enemy.MoveInDirection(moveDirection);
        }

        private Direction PickDirection(List<Direction> validDirections)
        {
            if (validDirections.Count == 0) return Direction.None;
            return validDirections[rng.Next(validDirections.Count)];
        }
    }
}

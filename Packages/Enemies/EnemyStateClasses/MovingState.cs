using System;
using Project.Characters;
using System.Collections.Generic;
using Project.Items;

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
            Random rng = new Random();
            duration = rng.Next(2, 13);
            moveDirection = GenerateDirection(enemy.PossibleMovementDirections(), rng);
        }

        public void Execute(IEnemy enemy, ItemManager itemManager)
        {
            timer += 0.1f;
            enemy.MoveInDirection(moveDirection);
        }

        private Direction GenerateDirection(List<Direction> validDirections, Random rng)
        {
            return validDirections[rng.Next(validDirections.Count - 1)];
        }
    }
}

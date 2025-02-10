using System;
using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class MovingState : IEnemyState
    {
        private static readonly Random random = new Random();
        private Vector2 direction;
        private float moveTimer = 0;

        public MovingState()
        {
            direction = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
            if (direction == Vector2.Zero)
            {
                direction = new Vector2(1, 0);
            }
        }

        public void Update(IEnemy enemy)
        {
            moveTimer += 1f;
            enemy.SetPosition(enemy.Position + direction * enemy.Speed);

            if (moveTimer > 3)
            {
                enemy.SetState(new IdleState());
                moveTimer = 0;
            }
        }
    }
}

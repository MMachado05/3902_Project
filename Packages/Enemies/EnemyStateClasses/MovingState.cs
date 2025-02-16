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

        private float moveDuration;

        public MovingState(IEnemy enemy)
        {
            SetNewMovement(enemy);
        }

        private void SetNewMovement(IEnemy enemy)
        {

            moveDuration = random.Next(2, 13);

            if (enemy is Aquamentus)
            {
                direction = new Vector2(random.Next(2) == 0 ? -1 : 1, 0);
            }
            else
            {
                bool moveHorizontally = random.Next(2) == 0;

                if (moveHorizontally)
                {
                    direction = new Vector2(random.Next(2) == 0 ? -1 : 1, 0);
                }
                else
                {
                    direction = new Vector2(0, random.Next(2) == 0 ? -1 : 1);
                }
            }
            Console.WriteLine($"Enemy: {enemy.GetType().Name}, Direction: {direction}");
        }

        public void Update(IEnemy enemy)
        {
            moveTimer += 0.1f;
            if (enemy is Enemy e)
            {
                string moveDir = GetMoveDirection(direction);
                e.MoveInDirection(moveDir);
            }

            enemy.SetPosition(enemy.Position + direction * enemy.Speed);

            if (moveTimer > moveDuration)
            {
                enemy.SetState(new IdleState());
                moveTimer = 0;
                SetNewMovement(enemy);
            }
        }

        private static string GetMoveDirection(Vector2 dir)
        {
            if (dir.Y < 0) return "Up";
            if (dir.Y > 0) return "Down";
            if (dir.X < 0) return "Left";
            return "Right";
        }
    }
}

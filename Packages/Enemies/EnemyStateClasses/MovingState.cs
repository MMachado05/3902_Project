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

        public MovingState()
        {
            SetNewMovement();
        }

        private void SetNewMovement()
        {
            // Set a new random movement duration between 2 and 12 seconds
            moveDuration = random.Next(2, 13); // 2 to 12 inclusive

            // Choose between horizontal (X) or vertical (Y) movement
            bool moveHorizontally = random.Next(2) == 0;

            if (moveHorizontally)
            {
                direction = new Vector2(random.Next(2) == 0 ? -1 : 1, 0); // Left or Right
            }
            else
            {
                direction = new Vector2(0, random.Next(2) == 0 ? -1 : 1); // Up or Down
            }
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
                SetNewMovement();
            }
        }

        private string GetMoveDirection(Vector2 dir)
        {
            if (dir.Y < 0) return "Up";
            if (dir.Y > 0) return "Down";
            if (dir.X < 0) return "Left";
            return "Right";
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Project.Enemies.EnemyStateClasses;

namespace Project.Enemies.EnemyClasses
{
    public abstract class Enemy : IEnemy
    {
        public Vector2 Position { get; private set; }
        public float Speed { get; set; }
        private IEnemyState CurrentState { get; set; }
        private Random random;

        public Enemy(Vector2 startPosition)
        {
            Position = startPosition;
            Speed = 1.5f;
            CurrentState = new IdleState();
            random = new Random();
        }

        public void SetState(IEnemyState newState)
        {
            CurrentState = newState;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update(this);
            }
        }

        public void Attack()
        {
            SetState(new AttackingState());
        }
    }
}

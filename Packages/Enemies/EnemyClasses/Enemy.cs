using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Enemies.EnemyStateClasses;

namespace Project.Enemies.EnemyClasses
{
    public abstract class Enemy : IEnemy
    {
        public Vector2 Position { get; private set; }
        public float Speed { get; set; }
        private IEnemyState CurrentState { get; set; }
        protected string lastDirection = "Left";

        public ISprite idleUp, idleDown, idleLeft, idleRight;
        public ISprite walkUp, walkDown, walkLeft, walkRight;
        public ISprite attackUp, attackDown, attackLeft, attackRight;
        public ISprite currentAnimation;

        public Enemy(Vector2 startPosition)
        {
            Position = startPosition;
            Speed = 0.5f;
            CurrentState = new IdleState();

            LoadAnimations();
            currentAnimation = idleDown;
        }

        protected abstract void LoadAnimations();

        public void SetState(IEnemyState newState)
        {
            CurrentState = newState;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void MoveInDirection(string direction)
        {
            lastDirection = direction;

            switch (direction)
            {
                case "Up": currentAnimation = walkUp; break;
                case "Down": currentAnimation = walkDown; break;
                case "Left": currentAnimation = walkLeft; break;
                case "Right": currentAnimation = walkRight; break;
            }
        }

        public void SetIdleAnimation()
        {
            switch (lastDirection)
            {
                case "Up": currentAnimation = idleUp; break;
                case "Down": currentAnimation = idleDown; break;
                case "Left": currentAnimation = idleLeft; break;
                case "Right": currentAnimation = idleRight; break;
            }
        }

        public virtual void SetAttackAnimation()
        {
            switch (lastDirection)
            {
                case "Up": currentAnimation = attackUp; break;
                case "Down": currentAnimation = attackDown; break;
                case "Left": currentAnimation = attackLeft; break;
                case "Right": currentAnimation = attackRight; break;
            }
        }

        public void UpdateState(GameTime gameTime)
        {
            CurrentState.Update(this);
        }

        public virtual void UpdateAnimation(GameTime gameTime)
        {
            currentAnimation.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch, Position);
        }
    }
}

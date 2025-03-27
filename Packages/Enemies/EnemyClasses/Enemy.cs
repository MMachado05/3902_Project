using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Enemies.EnemyStateClasses;

namespace Project.Enemies.EnemyClasses
{
    public abstract class Enemy : IEnemy
    {
        public Rectangle Position { get; private set; }
        public float Speed { get; set; }
        private IEnemyState CurrentState { get; set; }
        protected Direction lastDirection = Direction.Left;

        public ISprite idleUp, idleDown, idleLeft, idleRight;
        public ISprite walkUp, walkDown, walkLeft, walkRight;
        public ISprite attackUp, attackDown, attackLeft, attackRight;
        public ISprite currentAnimation;

        public Enemy(Rectangle initialPosition)
        {
            Position = initialPosition;
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

        // TODO: Either the property should be public, or unsettable. We could also
        // just have some sort of "move" command.
        /*public void SetPosition(Vector2 newPosition)*/
        /*{*/
        /*    Position = newPosition;*/
        /*}*/

        public void MoveInDirection(Direction direction)
        {
            lastDirection = direction;

            switch (direction)
            {
                case Direction.Up: currentAnimation = walkUp; break;
                case Direction.Down: currentAnimation = walkDown; break;
                case Direction.Left: currentAnimation = walkLeft; break;
                case Direction.Right: currentAnimation = walkRight; break;
            }
        }

        public void SetIdleAnimation()
        {
            switch (lastDirection)
            {
                case Direction.Up: currentAnimation = idleUp; break;
                case Direction.Down: currentAnimation = idleDown; break;
                case Direction.Left: currentAnimation = idleLeft; break;
                case Direction.Right: currentAnimation = idleRight; break;
            }
        }

        public virtual void SetAttackAnimation()
        {
            switch (lastDirection)
            {
                case Direction.Up: currentAnimation = attackUp; break;
                case Direction.Down: currentAnimation = attackDown; break;
                case Direction.Left: currentAnimation = attackLeft; break;
                case Direction.Right: currentAnimation = attackRight; break;
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

        public void CollideWith(IGameObject collider)
        {
            // TODO: Implement, need to check what the collision is with
        }

        public virtual void Attack() { }
        public virtual void ResetAttackState() { }
        public virtual float GetAttackDuration() => 4f;
    }
}

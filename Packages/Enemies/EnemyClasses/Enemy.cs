using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies.EnemyStateClasses;
using Project.Sprites;

namespace Project.Enemies.EnemyClasses
{
    public abstract class Enemy : IEnemy
    {
        public Rectangle Location { get; set; }
        public virtual int PlayerHealthEffect { get => -1; }
        public bool IsPassable { get => true; }

        public float Speed { get; set; }
        private IEnemyState CurrentState { get; set; }

        protected Direction lastDirection = Direction.Left;

        public ISprite idleUp, idleDown, idleLeft, idleRight;
        public ISprite walkUp, walkDown, walkLeft, walkRight;
        public ISprite attackUp, attackDown, attackLeft, attackRight;
        public ISprite currentAnimation;
        private float elapsedTime;

        private Rectangle lastLocation;

        public Enemy(Rectangle initialPosition)
        {
            Location = initialPosition;
            Speed = 1.0f;
            CurrentState = new IdleState();

            LoadAnimations();
            currentAnimation = idleRight;
            // TODO: Each enemy should be able to decide this on its own
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
            lastLocation = Location;

            int movementX = 0;
            int movementY = 0;

            switch (direction)
            {
                case Direction.Up:
                    movementY = -1;
                    currentAnimation = walkUp;
                    break;
                case Direction.Down:
                    movementY = 1;
                    currentAnimation = walkDown;
                    break;
                case Direction.Left:
                    movementX = -1;
                    currentAnimation = walkLeft;
                    break;
                case Direction.Right:
                    movementX = 1;
                    currentAnimation = walkRight;
                    break;
            }

            Location = new Rectangle(Location.X + movementX * (int)(Speed), Location.Y + movementY * (int)(Speed), Location.Width, Location.Height);
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
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 0.25f)
            {
                currentAnimation.Update(gameTime);
                elapsedTime = 0f;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch, Location);
        }

        public void CollideWith(IGameObject collider)
        {
            // TODO: Implement, need to check what the collision is with
            if (!collider.IsPassable)
            {
                Location = lastLocation;
            }
        }

        public virtual void Attack() { }
        public virtual void ResetAttackState() { }
        public virtual float GetAttackDuration() => 4f;

        public virtual void Update(GameTime gameTime)
        {
            UpdateState(gameTime);
            UpdateAnimation(gameTime);
        }
    }
}

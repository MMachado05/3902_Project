using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies.EnemyStateClasses;
using Project.Items;
using Project.Sprites;

namespace Project.Enemies.EnemyClasses
{
    public abstract class Enemy : IEnemy
    {
        public Rectangle Location { get; set; }
        public virtual int PlayerHealthEffect { get => -1; }
        public bool IsPassable { get => true; }

        public float Speed { get; set; }
        public int Health { get; set; }
        public virtual bool IsDead => Health <= 0;
        //private IEnemyState CurrentState { get; set; }
        private EnemyStateMachine stateMachine;

        protected Direction lastDirection = Direction.Left;

        public ISprite idleUp, idleDown, idleLeft, idleRight;
        public ISprite walkUp, walkDown, walkLeft, walkRight;
        public ISprite attackUp, attackDown, attackLeft, attackRight;
        public ISprite currentAnimation;
        private float elapsedTime;

        private float hurtCooldown = 0f;
        private const float HurtDelay = 1.0f;

        private Rectangle lastLocation;

        public Enemy(Rectangle initialPosition)
        {
            Location = initialPosition;
            Speed = 1.0f;
            stateMachine = new EnemyStateMachine(new SimpleRandomAI(), GetInitialState());

            LoadAnimations();
            currentAnimation = idleRight;
            // TODO: Each enemy should be able to decide this on its own
        }

        protected abstract void LoadAnimations();


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
            stateMachine.Update(this);
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
            if (collider is BasicAttack)
            {
                TakeDamage(1);
            }
            else if (collider is Arrow)
            {
                TakeDamage(2);
            }
            else if (collider is ThrownBoomerang)
            {
                TakeDamage(3);
            }
        }

        public virtual void Attack() { }
        public virtual void ResetAttackState() { }
        public virtual float GetAttackDuration() => 4f;

        public virtual void Update(GameTime gameTime)
        {
            if (hurtCooldown > 0)
                hurtCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateState(gameTime);
            UpdateAnimation(gameTime);
        }

        public virtual void TakeDamage(int amount)
        {
            if (hurtCooldown <= 0)
            {
                Health -= amount;
                hurtCooldown = HurtDelay;
            }
        }

        public abstract List<ProjectileItem> GetProjectiles();
        protected virtual IEnemyState GetInitialState() => new IdleState();
        public virtual List<Direction> PossibleMovementDirections()
        {
            return new List<Direction>{Direction.Up, Direction.Down, Direction.Right,
          Direction.Left};
        }
    }
}

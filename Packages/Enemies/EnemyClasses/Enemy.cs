using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies.EnemyStateClasses;
using Project.Items;
using Project.Sprites;
using Project.Enemies.Helper;

namespace Project.Enemies.EnemyClasses
{
    public abstract class Enemy : IEnemy
    {
        public Rectangle Location { get; set; }
        public virtual int PlayerHealthEffect => 0;
        public bool IsPassable => true;
        public float Speed { get; set; } = 1.0f;
        public int Health { get; set; }
        public virtual bool IsDead => Health <= 0;

        protected EnemyStateMachine stateMachine;
        protected Direction lastDirection = Direction.Left;
        protected ISprite idleUp, idleDown, idleLeft, idleRight;
        protected ISprite walkUp, walkDown, walkLeft, walkRight;
        protected ISprite attackUp, attackDown, attackLeft, attackRight;
        protected ISprite currentAnimation;
        private float elapsedTime;
        private readonly CooldownTimer hurtCooldown = new CooldownTimer(1.0f);

        private Rectangle lastLocation;

        protected Enemy(Rectangle initialPosition)
        {
            Location = initialPosition;
            stateMachine = new EnemyStateMachine(new SimpleRandomAI(), GetInitialState());
            LoadAnimations();
            currentAnimation = idleRight;
        }

        protected abstract void LoadAnimations();
        protected virtual IEnemyState GetInitialState() => new IdleState();

        public void MoveInDirection(Direction direction)
        {
            lastLocation = Location;
            lastDirection = direction;
            Vector2 movement = direction switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Down => new Vector2(0, 1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => Vector2.Zero,
            };

            Location = new Rectangle(
                Location.X + (int)(movement.X * Speed),
                Location.Y + (int)(movement.Y * Speed),
                Location.Width,
                Location.Height
            );

            currentAnimation = GetWalkAnimation(direction);
        }

        private ISprite GetWalkAnimation(Direction direction) => direction switch
        {
            Direction.Up => walkUp,
            Direction.Down => walkDown,
            Direction.Left => walkLeft,
            Direction.Right => walkRight,
            _ => walkDown,
        };

        public void SetIdleAnimation()
        {
            currentAnimation = lastDirection switch
            {
                Direction.Up => idleUp,
                Direction.Down => idleDown,
                Direction.Left => idleLeft,
                Direction.Right => idleRight,
                _ => idleDown,
            };
        }

        public virtual void SetAttackAnimation()
        {
            currentAnimation = lastDirection switch
            {
                Direction.Up => attackUp,
                Direction.Down => attackDown,
                Direction.Left => attackLeft,
                Direction.Right => attackRight,
                _ => attackDown,
            };
        }

        public virtual void Attack(ItemManager itemManager) { }
        public virtual void ResetAttackState() { }
        public virtual float GetAttackDuration() => 4f;

        public virtual void Update(GameTime gameTime, ItemManager itemManager)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            hurtCooldown.Update(delta);

            stateMachine.Update(this, itemManager);
            UpdateAnimation(gameTime);
        }

        public virtual void UpdateAnimation(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= 0.25f)
            {
                currentAnimation.Update(gameTime);
                elapsedTime = 0;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch, Location);
        }

        public void TakeDamage(int amount)
        {
            if (hurtCooldown.IsReady)
            {
                Health = System.Math.Max(Health - amount, 0);
                hurtCooldown.Reset();
            }
        }

        public void CollideWith(IGameObject collider, Vector2 from)
        {
            if (!collider.IsPassable)
            {
                Location = lastLocation;
            }
            if (collider is Arrow or Explosion or ThrownBoomerang)
            {
                TakeDamage(1);
            }
        }

        public virtual List<Direction> PossibleMovementDirections()
        {
            return new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        }
    }
}

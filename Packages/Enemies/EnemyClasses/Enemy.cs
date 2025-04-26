using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies.Helper;
using Project.Enemies.EnemyStateClasses;
using Project.Items;
using System;

namespace Project.Enemies.EnemyClasses
{
    public abstract class Enemy : IEnemy
    {
        protected EnemyMovement Movement { get; private set; }
        protected EnemyAnimation Animation { get; private set; }
        protected EnemyStateMachine StateMachine { get; private set; }
        protected CooldownTimer HurtCooldown { get; } = new CooldownTimer(1.0f);
        protected ProjectileShooter Shooter { get; set; }
        public int Health { get; set; }
        public float Speed
        {
            get => Movement.Speed;
            set => Movement.Speed = value;
        }
        public bool IsDead => Health <= -1;
        public Rectangle Location
        {
            get => Movement.Location;
            set => Movement.SetLocation(value);
        }

        public int PlayerHealthEffect { get; } = 0;
        public bool IsPassable { get; } = true;

        public Enemy(Rectangle spawnArea)
        {
            Movement = CreateMovement(spawnArea);
            Animation = CreateAnimation();
            StateMachine = CreateStateMachine();
        }

        protected abstract EnemyMovement CreateMovement(Rectangle spawnArea);
        protected abstract EnemyAnimation CreateAnimation();
        protected virtual EnemyStateMachine CreateStateMachine()
        {
            return new EnemyStateMachine(new SimpleRandomAI(), new IdleState());
        }

        public virtual void Update(GameTime gameTime, ItemManager itemManager)
        {
#if DEBUG
    System.Console.WriteLine($"[Enemy Debug] {this.GetType().Name} | Speed: {Movement.Speed} | Direction: {Movement.LastDirection} | Moving: {Movement.IsMoving()} | Location: {Location}");
#endif

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            HurtCooldown.Update(deltaTime);

            StateMachine.Update(this, deltaTime, itemManager);
            Movement.Update(deltaTime);
            Animation.Update(deltaTime, Movement.IsMoving(), Movement.LastDirection);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, Location);
        }

        protected Direction FacingDirection = Direction.Left;

        public virtual void MoveInDirection(Direction direction)
        {
            if (direction == Direction.None)
            {
                Movement.Stop();
            }
            else
            {
                Movement.SetDirection(direction);
                FacingDirection = direction;
            }
        }

        public virtual void Attack(ItemManager itemManager = null) { }
        public virtual void ResetAttackState() { }
        public virtual float GetAttackDuration() => 0.5f;

        public void TakeDamage(int amount)
        {
            if (HurtCooldown.IsReady)
            {
                Health = System.Math.Max(Health - amount, 0);
                HurtCooldown.Reset();
            }
        }

        public void CollideWith(IGameObject collider, Vector2 from)
        {
            if (!collider.IsPassable)
            {
                Direction currentDirection = Movement.LastDirection;

                Direction oppositeDirection = currentDirection switch
                {
                    Direction.Up => Direction.Down,
                    Direction.Down => Direction.Up,
                    Direction.Left => Direction.Right,
                    Direction.Right => Direction.Left,
                    _ => Direction.None
                };

                const int pushDistance = 5;

                Vector2 safePush = oppositeDirection switch
                {
                    Direction.Up => new Vector2(0, -pushDistance),
                    Direction.Down => new Vector2(0, pushDistance),
                    Direction.Left => new Vector2(-pushDistance, 0),
                    Direction.Right => new Vector2(pushDistance, 0),
                    _ => Vector2.Zero
                };

                Movement.SetLocation(new Rectangle(
                    Movement.Location.X + (int)safePush.X,
                    Movement.Location.Y + (int)safePush.Y,
                    Movement.Location.Width,
                    Movement.Location.Height
                ));

                StateMachine.OverrideState(new MovingState(this, oppositeDirection));
            }



            if (collider is Arrow or Explosion or ThrownBoomerang)
                TakeDamage(1);
        }

        public virtual List<Direction> PossibleMovementDirections()
        {
            return new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        }
    }
}

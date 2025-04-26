using Microsoft.Xna.Framework;
using Project.Characters;

namespace Project.Enemies.Helper
{
    public class EnemyMovement
    {
        public Rectangle Location { get; private set; }
        public Rectangle PreviousLocation { get; private set; }
        private Vector2 velocity;
        private Vector2 direction;
        public float Speed { get; set; }
        public Direction LastDirection { get; private set; } = Direction.Left;

        public EnemyMovement(Rectangle initialPosition, float initialSpeed)
        {
            Location = initialPosition;
            PreviousLocation = initialPosition;
            Speed = initialSpeed;
            velocity = Vector2.Zero;
            direction = Vector2.Zero;
        }

        public void SetDirection(Direction dir)
        {
            LastDirection = dir;
            direction = dir switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Down => new Vector2(0, 1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => Vector2.Zero
            };
        }

        public void Update(float deltaTime)
        {
            PreviousLocation = Location;

            if (direction != Vector2.Zero)
            {
                var normalized = Vector2.Normalize(direction);
                velocity = normalized * Speed;
            }
            else
            {
                velocity = Vector2.Zero;
            }

            Vector2 movement = velocity * deltaTime;

            Location = new Rectangle(
                (int)(Location.X + movement.X),
                (int)(Location.Y + movement.Y),
                Location.Width,
                Location.Height
            );
        }

        public void RevertToPreviousPosition()
        {
            Location = PreviousLocation;
        }

        public bool IsMoving()
        {
            return direction != Vector2.Zero;
        }

        public void SetLocation(Rectangle newLocation)
        {
            Location = newLocation;
        }

        public void Stop()
        {
            direction = Vector2.Zero;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Enemies.EnemyClasses
{
    public class Projectile
    {
        public Vector2 Position { get; private set; }
        private Vector2 Direction;
        private float Speed;
        private ISprite sprite;
        private Vector2 initialPosition;
        private float maxDistance;
        private bool returning;
        private Vector2? returnTarget;

        public Projectile(Vector2 position, Vector2 direction, ISprite sprite, float speed, float maxDistance, Vector2? returnTarget = null)
        {
            Position = position;
            Direction = direction;
            this.sprite = sprite;
            Speed = speed;
            this.maxDistance = maxDistance;
            this.returnTarget = returnTarget;
            initialPosition = position;
            returning = false;
        }

        public void Update()
        {
            if (!returning && Vector2.Distance(initialPosition, Position) >= maxDistance)
            {
                returning = returnTarget.HasValue;
            }

            // Move forward or return
            Vector2 moveDirection = returning && returnTarget.HasValue
                ? Vector2.Normalize(returnTarget.Value - Position)
                : Direction;

            Position += moveDirection * Speed;
            sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }

        public bool HasReturned() => returning && returnTarget.HasValue && Vector2.Distance(Position, returnTarget.Value) < 5.0f;
    }
}
